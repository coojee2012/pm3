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
using EgovaDAO;
using EgovaBLL;
public partial class JSDW_APPLYZBBA_YBBAList : System.Web.UI.Page
{
    RApp ra = new RApp();
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    WorkFlowApp wfApp = new WorkFlowApp();
    
    public int fMType = 11222;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            conBind();
            ShowInfo();
        }
    }

    //绑定选项
    private void conBind()
    {
        t_FYear.Text = DateTime.Now.Year.ToString();
        t_FName.Text = t_FYear.Text + "年 " + db.getManageTypeName(fMType);
    }

    private void ShowInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        string FBaseinfoID = CurrentEntUser.EntId;
        var v = from t in dbContext.CF_App_List
                join a in dbContext.TC_AJBA_Record
                on t.FId equals a.FAppId
                where t.FBaseinfoId == FBaseinfoID && t.FManageTypeId == fMType
                orderby t.FReportDate
                select new
                {
                    t.FId,
                    t.FwriteDate,
                    t.FReportDate,
                    t.FCreateTime,
                    t.FState,
                    t.FResult,
                    a.FPrjId,
                    t.FLinkId,
                    a.RecordNo,
                    a.PrjItemName,
                    a.ProjectName
                };
        if (!string.IsNullOrEmpty(this.txtFPrjItemName.Text.Trim()))
        {
            v = v.Where(t => t.ProjectName.Contains(this.txtFPrjItemName.Text.Trim()));
        }
        //if (!string.IsNullOrEmpty(this.txtFProjectName.Text.Trim()))
        //{
        //    v = v.Where(t => t.PrjItemName.Contains(this.txtFProjectName.Text.Trim()));
        //}
        //if (!string.IsNullOrEmpty(this.txtRecordNo.Text.Trim()))
        //{
        //    v = v.Where(t => t.RecordNo.Contains(this.txtRecordNo.Text.Trim()));
        //}
        if (!string.IsNullOrEmpty(this.txtSDate.Text.Trim()))
        {
            v = v.Where(t => t.FReportDate >= DateTime.Parse(this.txtSDate.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.txtEDate.Text.Trim()))
        {
            v = v.Where(t => t.FReportDate <= DateTime.Parse(this.txtEDate.Text.Trim()));
        }
        if (this.ddlFState.SelectedIndex > 0)
        {
            v = v.Where(t => t.FState.Equals(ddlFState.SelectedValue));
        }
        Pager1.RecordCount = v.Count();
        this.gv_list.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        gv_list.DataBind();

    }


    protected void btn_Click(object sender, EventArgs e)
    {
        appTab.Visible = false;
        applyInfo.Visible = true;
    }
    //取消
    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        appTab.Visible = true;
        applyInfo.Visible = false;
    }

    //创建业务
    private void SaveInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        if (!wfApp.ValidateNewBiz(t_FPriItemId.Value, fMType))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "js", "alert('同一个工程不能创建两条备案信息！');", true);
            return;
        }
        if (string.IsNullOrEmpty(CurrentEntUser.EntId))
            return;
        //添加业务
        DateTime dTime = DateTime.Now;
        string FAppId = Guid.NewGuid().ToString();
        EgovaDAO.CF_App_List app = new EgovaDAO.CF_App_List();//业务
        app.FId = FAppId;
        app.FLinkId = t_FPriItemId.Value;
        app.FManageTypeId = fMType;
        app.FwriteDate = dTime;
        app.FState = 0;
        app.FIsDeleted = false;
        app.FName = t_FName.Text.Trim();//业务名
        app.FYear = EConvert.ToInt(t_FYear.Text.Trim());//年份
        app.FMonth = DateTime.Now.Month;//月份
        app.FBaseName = CurrentEntUser.EntName;//单位名
        app.FBaseinfoId = CurrentEntUser.EntId;//单位ID
        app.FTime = dTime;
        app.FCreateTime = dTime;
        app.FReportCount = 1;
        dbContext.CF_App_List.InsertOnSubmit(app);
        dbContext.SubmitChanges();
        //添加备案信息
        dbContext = new EgovaDB();
        TC_AJBA_Record record = new TC_AJBA_Record();
        string FRecordId = Guid.NewGuid().ToString();
        record.FId = FRecordId;
        record.FAppId = FAppId;
        record.FPrjId = t_FPrjId.Value;
        record.FPrjItemId = t_FPriItemId.Value;
        record.PrjItemName = t_FPrjItemName.Text;
        record.RecordNo = getBANumber();
        record.ProjectName = t_FPrjName.Value;
        dbContext.TC_AJBA_Record.InsertOnSubmit(record);
        //提交修改
        dbContext.SubmitChanges();

        Session["FAppId"] = FAppId;
        Session["FManageTypeId"] = fMType;
        Session["FIsApprove"] = 0;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "js", "top.document.location='../Appmain/aindex.aspx';", true);
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        this.SaveInfo();
    }


    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowInfo();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void gv_list_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //序号
            Label lbautoid = (Label)e.Row.FindControl("lbautoid");
            lbautoid.Text = (e.Row.RowIndex + 1).ToString();

            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FState"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPrjId"));
            DateTime FReportDate = EConvert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "FReportDate"));

            //本业务办理状态
            string t = "", s = "";
            LinkButton btnOp = (LinkButton)e.Row.FindControl("btnOp");


            switch (FState)
            {
                case "0"://未上报 
                    t = "<font color='#888888'>--</font>";
                    s = "<font color='#888888'>未上报</font>";
                    break;
                case "1"://已上报 
                    t = FReportDate.ToShortDateString();
                    s = "<font color='blue'>已上报</font>";
                    break;
                case "2"://被退回 
                    btnOp.Attributes["onclick"] = "return false;";
                    btnOp.Text = "--";
                    t = FReportDate.ToShortDateString();
                    s = "<a href=\"javascript:showApproveWindow('../main/JGLookIdea.aspx?FAppId=" + FID + "',600,400);\"><font color='red'>退回</font></a>";
                    break;
                default:
                    btnOp.Attributes["onclick"] = "return false;";
                    btnOp.Text = "--";
                    break;
            }

            e.Row.Cells[4].Text = t;
            e.Row.Cells[5].Text = s;
        }
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        var result = (from t in dbContext.TC_PrjItem_Info
                      where t.FId == this.t_FPriItemId.Value
                      select t).SingleOrDefault();
        t_FPrjItemName.Text = result.PrjItemName;
        t_FPrjId.Value = result.FPrjId;
        t_FJSDW.Text = result.JSDW;
        t_FPrjName.Value = result.ProjectName;
    }
    private string getBANumber()
    {
        EgovaDB dbContext = new EgovaDB();
        string recordNo = "AQJD" + string.Format("{0:yyyyMMdd}", DateTime.Now);
        var result = (from t in dbContext.TC_QA_Record
                      where t.RecordNo.Contains(recordNo)
                      select t).Count();
        return recordNo + (result + 1);

    }
    protected void gv_list_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "See")
        {
            string FId = (string) e.CommandArgument;
            this.Session["FAppId"] = FId;
            this.Session["FManageTypeId"] = fMType;
            //if (fState != 0 && fState != 2)
            //    Session["FIsApprove"] = 1;
            //else
            //    Session["FIsApprove"] = 0;
            Response.Write("<script language='javascript'>top.document.location='../Appmain/aindex.aspx';</script>");

        }
    }
    protected void gv_list_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //把row的index添加到CommandArgument中
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton LinkButton1 = (LinkButton)e.Row.FindControl("btnItemSee");
            LinkButton1.CommandArgument = e.Row.RowIndex.ToString();
        }
    }
}
