using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.EntityBase;
using ProjectData;
using System.Linq;
using ProjectBLL;
using Tools;

public partial class KC_ApplySGTSCYJHF_SgtScyjHfList : System.Web.UI.Page
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

    }

    //显示列表
    private void ShowInfo()
    {
        int SC = 28803;//审查业务类型
        int KCSJ = 280;//勘察设计业务类型
        if (Request.QueryString["n"] == "2")//施工图设计审查
        {
            SC = 30103;
            KCSJ = 296;
            DG_List.Columns[2].HeaderText = "设计单位";
            DG_List.Columns[5].HeaderText = "设计单位<br/>回复意见";
            Title = lit_Title.Text = "施工图设计文件审查意见回复确认";
            lit_TS.Text = "设计";
        }


        string FBaseinfoId = CurrentEntUser.EntId;
        //参与项目（需要从技术性审查28803 那边查询，因为审查了2次要回复两次，要有两条记录。）
        var v = from a in db.CF_App_List
                join d in db.CF_Prj_Data on a.FLinkId equals d.FId
                join p in db.CF_Prj_BaseInfo on d.FPrjId equals p.FId
                join aa in db.CF_App_List on d.FLinkId equals aa.FLinkId
                where a.FManageTypeId == SC && a.FState > 0 && a.FBaseinfoId == FBaseinfoId
                   && aa.FManageTypeId == KCSJ && aa.FState == 6
                select new
                {
                    d.FId,
                    d.FPrjName,
                    p.FType, //工程类别
                    aa.FBaseName, //建设单位
                    aa.FYear,
                    a.FState, //技术性审查结果
                    FAppId = a.FId,//技术性审查业务id
                    KCSJBaseName = db.CF_Ent_BaseInfo.Where(t => t.FId == aa.FToBaseinfoId).Select(t => t.FName).FirstOrDefault(),
                    //回复情况表
                    r = db.CF_Prj_Reply.Where(t => t.FType == 1 && t.FLinkId == d.FId).FirstOrDefault(),
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


            //查询办理情况
            string s = (FState == "3" ? "<tt>不合格</tt>" : "<font color='green'>合格</font>");
            string sUrl = Request.QueryString["n"] == "1" ? "ApplyKCJSXSC" : "ApplySGTSJJSXSC";
            e.Item.Cells[4].Text = "<a href=\"javascript:showAddWindow('../../KcsjSgt/" + sUrl + "/Report.aspx?FAppId=" + FAppId + "',700,600);\">" + s + "</a>";

            //回复情况  //确认情况

            s = ""; string n = "";
            CF_Prj_Reply r = DataBinder.Eval(e.Item.DataItem, "r") as CF_Prj_Reply;
            if (r != null && r.FState > 0)
            {
                s = "<a href=\"javascript:showAddWindow('Reply.aspx?FID=" + r.FID + "',700,600);\"><font color='green'>已回复</font></a>";

                n = r.FState == 2 ? "<font color='green'>已确认</font>" : "<font color='#666666'>未确认</font>";
                n = "<a href=\"javascript:showAddWindow('Reply.aspx?FID=" + r.FID + "',700,600);\">" + n + "</a>";
            }
            else
            {
                s = "<font color='#666666'>未回复</font>";
                n = "--";
            }
            e.Item.Cells[5].Text = s;
            e.Item.Cells[6].Text = n;

            //打印
            string d = "--";
            if (r != null && r.FState == 2)
            {
                string ss = "HFB-KCDW";
                if (Request.QueryString["n"] == "2")//施工图设计审查
                    ss = "HFB-SJDW";
                d = "<a href=\"" + ReportServer + ss + ".cpt&ReplyFID=" + r.FID + "\" target=\"_blank\">打印</a>";
            }
            e.Item.Cells[7].Text = d;
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
