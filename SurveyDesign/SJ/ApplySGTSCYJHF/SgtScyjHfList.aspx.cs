using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.EntityBase;
using ProjectData;
using System.Linq;
using ProjectBLL;
using Tools;

public partial class SJ_ApplySGTSCYJHF_SgtScyjHfList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    public string ReportServer = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportServer = db.getSysObjectContent("_ReportServer");
        if (string.IsNullOrEmpty(ReportServer))
        {
            ReportServer = "http://" + Request.Url.Host + ":8075/WebReport/ReportServer?reportlet=";
        }
        if (!Page.IsPostBack)
        {
            conBind();
            ShowInfo();
        }
    }

    //绑定选项
    private void conBind()
    {
        for (int i = DateTime.Now.Year; i >= 2013; i--)
        {
            drop_FYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        drop_FYear.Items.Insert(0, new ListItem("--全部--", ""));
    }

    //显示列表
    private void ShowInfo()
    {
        string FBaseinfoId = CurrentEntUser.EntId;
        //参与项目（需要从技术性审查30103 那边查询，因为审查了2次要回复两次，要有两条记录。）
        var v = from a in db.CF_App_List
                join d in db.CF_Prj_Data on a.FLinkId equals d.FId
                join p in db.CF_Prj_BaseInfo on d.FPrjId equals p.FId
                join aa in db.CF_App_List on d.FLinkId equals aa.FLinkId
                where a.FManageTypeId == 30103 && a.FState > 0
                   && aa.FManageTypeId == 296 && aa.FState == 6 && aa.FToBaseinfoId == FBaseinfoId
                select new
                {
                    d.FId,
                    d.FPrjName,
                    d.FPrjId,
                    p.FType, //工程类别
                    aa.FBaseName, //建设单位
                    aa.FYear,
                    a.FState, //技术性审查结果
                    FAppId = a.FId,//技术性审查业务id
                    SGTBaseName = a.FBaseName,
                    a.FBaseinfoId,
                    //回复情况表
                    r = db.CF_Prj_Reply.Where(t => t.FType == 1 && t.FLinkId == d.FId && t.FBaseinfoId == FBaseinfoId).FirstOrDefault(),
                };
        if (!string.IsNullOrEmpty(ttFPrjName.Text))
        {
            v = v.Where(t => t.FPrjName.Contains(ttFPrjName.Text));
        }

        if (!string.IsNullOrEmpty(ddlFState.SelectedValue))
        {
            int n = EConvert.ToInt(ddlFState.SelectedValue);
            v = v.Where(t => n == 0 ? t.r.FState == 0 : t.r.FState > 0);
        }

        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));//CF_Prj_Data.ID 
            string FType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FType"));
            string FAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            string SGTBaseName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "SGTBaseName"));
            string SGTFBaseinfoId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBaseinfoId"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));

            //工程类别
            e.Item.Cells[2].Text = db.getDicName(FType);

            //查询办理情况
            string p = "", nn = "告知书";
            if (FState == "3")
            { //不合格，打告知书
                p = "SCYJGZS-JZGC.cpt";//房屋建筑
                if (FType == "2000102")//市政基础
                    p = "SCYJGZS-SZGC.cpt";
            }
            else if (FState == "6")
            { //合格，打合格书
                p = "SCHGS-JZGC.cpt";//房屋建筑
                if (FType == "2000102")//市政基础
                    p = "SCHGS-SZGC.cpt";
                nn = "合格书";
            }

            string s = SGTBaseName + "：" + (FState == "3" ? "<tt>不合格</tt>" : "<font color='green'>合格</font>");
            e.Item.Cells[4].Text = "<a title=\"点击查看施工图设计文件审查" + nn + "\" href=\"" + ReportServer + p + "&FAppId=" + FAppId + "&FBaseId=" + SGTFBaseinfoId + "&FPrjId=" + FPrjId + "&PrjId=" + FPrjId + "\" target=\"_blank\">" + s + "</a>";

            //回复情况
            s = ""; string n = "回复";
            CF_Prj_Reply r = DataBinder.Eval(e.Item.DataItem, "r") as CF_Prj_Reply;
            if (r != null)
            {
                s += r.FState == 0 ? "<font color='#666666'>未回复</font>" : "<font color='green'>已回复</font>";
                n = r.FState == 0 ? "回复" : "查看";
            }
            else
            {
                s += "<font color='#666666'>未回复</font>";
            }
            s += "<a style=\"margin-left:20px;\" href=\"javascript:showAddWindow('Reply.aspx?FLinkId=" + FID + "',700,600);\">" + n + "</a>";
            e.Item.Cells[5].Text = s;

        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
    }

    //查询按钮
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
