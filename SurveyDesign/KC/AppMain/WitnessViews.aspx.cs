using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using Approve.RuleApp;
using ProjectData;
using System.Linq;
using ProjectBLL;

public partial class JZDW_main_WitnessViews : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    public int fMType = 28301;//勘察人员意见
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            conBind();
            showInfo();
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



    private void showInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;

        var v = from t in db.CF_App_List
                join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                where t.FBaseinfoId == FBaseinfoID && t.FManageTypeId == fMType
                orderby t.FTime descending
                select new
                {
                    t.FId,
                    t.FPrjId,
                    t.FLinkId,
                    d.FPrjName,
                    d.FPriItemId,
                    t.FName,
                    t.FCreateTime,
                    t.FState,
                    t.FManageTypeId,
                    t.FReportDate,
                    t.FUpDeptId,
                    t.FYear,
                    FBaseName = db.CF_Ent_BaseInfo.Where(b => b.FId == (db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FBaseinfoId).FirstOrDefault())).Select(b => b.FName).FirstOrDefault(),
                    t.FAppDate,

                    app = db.CF_App_List.Where(a => a.FLinkId == t.FLinkId && a.FManageTypeId == 28001).FirstOrDefault(),

                    //查询勘察信息备案业务是否已变更并删除
                    BakAppIsDeleted = (db.CF_App_List.Where(l => l.FId == t.FLinkAppId).Select(l => l.FIsDeleted.GetValueOrDefault()).FirstOrDefault())
                };
        if (!string.IsNullOrEmpty(ttFPrjName.Text.Trim()))
            v = v.Where(t => t.FPrjName.Contains(ttFPrjName.Text.Trim()));
        if (!string.IsNullOrEmpty(ddlFState.SelectedValue))
            v = v.Where(t => t.FState.ToString() == ddlFState.SelectedValue);
        if (!string.IsNullOrEmpty(drop_FYear.SelectedValue))
            v = v.Where(t => t.FYear.ToString() == drop_FYear.SelectedValue);

        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页不显示
    }





    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));

            //状态办理结果
            string s = "";
            string o = "<a href='javascript:showAddWindow(\"../ApplyRYYJ/Report.aspx?FAppId=" + FID + "\",700,500);'>";
            switch (FState)
            {
                case "0":
                case "1":
                    s = "<font color='#888888'>还未完成</font>";
                    o += "查看人员意见";
                    e.Item.Cells[4].Text = "<font color='#888888'>--</font>";
                    break;
                case "6":
                    s = "<font color='green'>已完成</font>";
                    o += "查看人员意见";
                    break;
            }

            CF_App_List app = DataBinder.Eval(e.Item.DataItem, "app") as CF_App_List;
            if (app != null)
            {
                if (app.FState == 2)
                {
                    s += "</br><font color='red'>（已被见证单位退回，业务终止）</font>";
                }
                else if (app.FState == 7)
                {
                    s += "</br><font color='red'>（合同备案的见证单位不予受理，业务终止）</font>";
                }



                //是否二次。 
                string FPriItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPriItemId"));
                if (app.FCount > 1)
                {
                    //查询出不合格的意见
                    var v = db.CF_App_List.Where(a => a.FLinkId == FPriItemId && a.FManageTypeId == 28803).FirstOrDefault();
                    if (v != null)
                    {
                        string txt = "<a style=\"text-decoration:underline;\" ";
                        txt += "href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCJSXSC/Report.aspx?FAppId=" + v.FId + "',700,680);\">";
                        txt += "查看审图机构意见</a>";
                        ((Literal)e.Item.FindControl("lit_Count")).Text = ("(" + app.FCount + "次," + txt + ")");
                    }
                }
            }
            e.Item.Cells[5].Text = s;
            e.Item.Cells[6].Text = o + "</a>";

            //查询项目的变更时间  
            var prjBG = db.CF_Prj_BaseInfo.Where(st => st.FId == FPrjId)
                .Select(st => new { st.FIsBG, st.FBGTime, st.FCount })
                .FirstOrDefault();
            if (prjBG.FCount > 0)
            {
                ((Literal)e.Item.FindControl("prj_Count")).Text = ("<br/>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")");
            }
            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Count(st => st.FPrjId == FPrjId) > 0;
            if (stop)
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Item.ToolTip = "该项目已被中止，所有业务停止进行。";
            }
            else if (prjBG != null && prjBG.FIsBG == 1)//判断该项目是否变更
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Item.ToolTip = "该项目已经做了变更，变更前的业务停止进行。";
            }

            int BakAppIsDeleted = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "BakAppIsDeleted"));
            if (BakAppIsDeleted == 1)
            {
                ((Literal)e.Item.FindControl("lit_TS")).Text = "<img title=\"上一步“勘察项目信息备案”已重办，本业务也无效。”\" src=\"../../image/info.jpg\" style=\"cursor:pointer;\"/>";
                e.Item.Cells[6].Text = "--";
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
            }
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
        showInfo();
    }
}
