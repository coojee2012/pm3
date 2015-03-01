using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Web.UI.HtmlControls;
using Approve.EntityBase;

public partial class EmpJZDW_appmain_AddReport : Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    RCenter rq = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FIsApprove"] != null && Session["FIsApprove"].ToString() == "1")
        {
            this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>FIsApprove();</script>");
        }
        pageTool tool = new pageTool(this.Page);

        if (!IsPostBack)
        {

            btnSave.Attributes["onclick"] = "return checkInfo();";
            btnReport.Visible = false;
            string FAppId = EConvert.ToString(Request.QueryString["FAppId"]);
            showInfo();
            int iCount = db.CF_Prj_Emp.Where(t => t.FAppId == FAppId).Count();
            //
            if (iCount == 1)
            {

                btnReport.Visible = true;
            }
            else
            {   //判断是否是项目负责人
                iCount = db.CF_Prj_Emp.Where(t => t.FAppId == FAppId && t.FType == 1 && t.FEmpBaseInfo == EmpId).Count();
                if (iCount > 0)
                {

                    btnReport.Visible = true;

                }

            }
        }
    }
    private int fMType_01
    {
        get { return EConvert.ToInt(ViewState["fMType_01"]); }
        set { ViewState["fMType_01"] = value; }
    }
    private string EmpId
    {
        get { return EConvert.ToString(ViewState["EmpId"]); }
        set { ViewState["EmpId"] = value; }
    }
    private string FCurrentAppId
    {
        get { return EConvert.ToString(ViewState["FAppId"]); }
        set { ViewState["FAppId"] = value; }
    }
    //显示
    private void showInfo()
    {
        string FAppId = EConvert.ToString(Request.QueryString["FAppId"]);
        fMType_01 = 28004;
        var app = (from t in db.CF_App_List
                   where t.FLinkId == FAppId && t.FManageTypeId == fMType_01
                   select t).FirstOrDefault();
        if (app != null)
        {

            FCurrentAppId = app.FId;
            ShowEmpInfo();
            //显示意见信息
            shoData(app.FId);


            liPrjName.Text = db.CF_Prj_BaseInfo.Where(t => t.FId == app.FPrjId).Select(t => t.FPrjName).FirstOrDefault();
        }

        ShowFileInfo();
    }

    //显示意见信息
    private void shoData(string FLinkId)
    {
        CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == FLinkId && t.FUserId == EmpId).FirstOrDefault();
        if (idea != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(idea);
        }


    }

    private void ShowFileInfo()
    {


        //当前业务类型
        int FManageTypeId = fMType_01;
        string FAppId = FCurrentAppId;
        ProjectDB db = new ProjectDB();
        var v = from t in db.CF_Sys_PrjList
                where t.FManageType == FManageTypeId
                orderby t.FOrder
                select new
                {
                    t.FId,
                    t.FFileName,
                    t.FFileAmount,
                    t.FRemark,
                    t.FOrder,
                    AppFile = db.CF_AppPrj_FileOther.Where(f => t.FId == f.FPrjFileId && f.FAppId == FAppId)
                };

        Pager1.RecordCount = v.Count();
        rep_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        rep_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页时隐藏分页控件

    }


    //保存
    private void saveInfo(int FState)
    {
        SMS sms = new SMS();
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;


        CF_App_List app = db.CF_App_List.Where(t => t.FId == FCurrentAppId).FirstOrDefault();
        if (app != null)
        {
            if (FState == 6)
            {
                app.FState = FState;
                app.FReportDate = dTime;
                Session["FIsApprove"] = 1;
            }
            //保存意见
            CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == app.FId && t.FUserId == EmpId).FirstOrDefault();

            if (idea == null)
            {
                idea = new CF_App_Idea();
                db.CF_App_Idea.InsertOnSubmit(idea);
                idea.FId = Guid.NewGuid().ToString();
                idea.FIsdeleted = 0;
                idea.FLinkId = app.FId;
                idea.FUserId = EmpId;
                idea.FType = app.FManageTypeId;

                int SystemId = EConvert.ToInt((from t in db.CF_Emp_BaseInfo
                                               join b in db.CF_Ent_BaseInfo on t.FBaseInfoID equals b.FId
                                               where t.FId == EmpId
                                               select b.FSystemId).FirstOrDefault());

                idea.FSystemId = SystemId;
            }
            idea = tool.getPageValue(idea);
            idea.FTime = dTime;
            idea.FContent = t_FContent.Text;


        }
        else
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }

        db.SubmitChanges();
        showInfo();
        tool.showMessageAndRunFunction("操作成功！", "location.href=location.href");
    }
    void ShowEmpInfo()
    {
        string FId = Request.QueryString["FId"];
        CF_Prj_Emp emp = db.CF_Prj_Emp.Where(t => t.FId == FId).FirstOrDefault();
        if (emp != null)
        {
            EmpId = emp.FEmpBaseInfo;
            pageTool tool = new pageTool(this, "li_");
            tool.fillPageControl(emp);
        }
    }
    //一层列表
    protected void rep_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            IQueryable<CF_AppPrj_FileOther> AppFile = DataBinder.Eval(e.Item.DataItem, "AppFile") as IQueryable<CF_AppPrj_FileOther>;
            if (AppFile != null && AppFile.Count() > 0)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = AppFile.Count().ToString();
                ((Literal)e.Item.FindControl("lit_Has")).Text = "<font color='green'>是</font>";


                Repeater rep_File = (Repeater)e.Item.FindControl("rep_File");
                rep_File.DataSource = AppFile;
                rep_File.DataBind();
            }
            else
            {
                ((Literal)e.Item.FindControl("lit_Has")).Text = "<font color='red'>否</font>";
            }

            if (EConvert.ToString(Session["FIsApprove"]) == "0")
            {
                ((Literal)e.Item.FindControl("lit_Has")).Text += "<input type=\"button\" class=\"m_btn_w4\" value=\"上传文件\" onclick='showAddWindow(\"FileUp.aspx?FPrjFileId=" + FID + "&FAppId="+FCurrentAppId+"&FUserId="+EmpId+"\",500,250);' />";
            }
        }
    }
    //二层列表
    protected void rep_File_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (EConvert.ToString(Session["FIsApprove"]) == "1")
            {
                ((LinkButton)e.Item.FindControl("btnDel")).Visible = false;
            }
            else
            {

                ((LinkButton)e.Item.FindControl("btnDel")).Visible = true;
            }
        }
    }
    //二层列表 事件
    protected void rep_File_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "cnDel")
        {
            string FID = e.CommandArgument.ToString();
            ProjectDB db = new ProjectDB();

            CF_AppPrj_FileOther v = db.CF_AppPrj_FileOther.Where(t => t.FId == FID).FirstOrDefault();
            if (v != null)
            {
                db.CF_AppPrj_FileOther.DeleteOnSubmit(v);
                db.SubmitChanges();

                pageTool tool = new pageTool(this.Page);
                tool.showMessage("删除成功");
                showInfo();
            }
        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo(0);
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        saveInfo(6);
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
