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

public partial class EmpJZDW_appmain_Report : Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FIsApprove"] != null && Session["FIsApprove"].ToString() == "1")
        {
            this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>FIsApprove();</script>");
        }
        pageTool tool = new pageTool(this.Page);

        if (!IsPostBack)
        {
            string FAppId = EConvert.ToString(Session["FAppId"]);//见证人员安排的业务ID


            var result = db.CF_Prj_Emp.Where(t => t.FAppId == FAppId);
            int iCount = result.Count();
            //
            if (iCount == 1)
            {
                Response.Redirect(string.Format("AddReport.aspx?FId={0}&FAppId={1}", result.FirstOrDefault().FId, FAppId));
                return;
            }
            else
            {   //判断是否是项目负责人
                result = db.CF_Prj_Emp.Where(t => t.FAppId == FAppId && t.FType == 1 && t.FEmpBaseInfo == CurrentEmpUser.EmpId);
                iCount = result.Count();
                if (iCount > 0)
                {


                }
                else
                {
                    CF_Prj_Emp emp = db.CF_Prj_Emp.Where(t => t.FAppId == FAppId && t.FEmpBaseInfo == CurrentEmpUser.EmpId).FirstOrDefault();
                    if (emp != null)
                    {
                        Response.Redirect(string.Format("AddReport.aspx?FId={0}&FAppId={1}", emp.FId, FAppId));
                        return;
                    }
                }
            }
            showInfo();
        }
    }
    public string FAppId
    {
        get { return EConvert.ToString(ViewState["FAppId"]); }
        set { ViewState["FAppId"] = value; }
    }
    public string CurrentAppId
    {
        get { return EConvert.ToString(ViewState["CurrentAppId"]); }
        set { ViewState["CurrentAppId"] = value; }
    }
    //显示
    private void showInfo()
    {

        FAppId = EConvert.ToString(Session["FAppId"]);
        int fMType_01 = EConvert.ToInt(Session["FManageTypeId"]);
        var app = (from t in db.CF_App_List
                   where t.FLinkId == FAppId && t.FManageTypeId == fMType_01
                   select new
                   {
                       t.FId,
                       t.FName,
                       t.FYear,
                       t.FLinkId,
                       t.FBaseName
                   }).FirstOrDefault();
        if (app != null)
        {

            CurrentAppId = app.FId;
            //显示意见信息
            shoData(app.FId);

            ShowEmpInfo();
        }
    }

    //显示意见信息
    private void shoData(string FLinkId)
    {
        CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == FLinkId && t.FUserId == CurrentEmpUser.EmpId).FirstOrDefault();
        if (idea != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(idea);
        }


    }



    //保存
    private void saveInfo(int FState)
    {
        SMS sms = new SMS();
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;

        int fMType_01 = EConvert.ToInt(Session["FManageTypeId"]);
        CF_App_List app = db.CF_App_List.Where(t => t.FLinkId == FAppId && t.FManageTypeId == fMType_01).FirstOrDefault();
        if (app != null)
        {
            if (FState == 6)
            {
                app.FState = FState;
                app.FReportDate = dTime;
                Session["FIsApprove"] = 1;
            }



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
        dg_List.DataKeyField = "FEmpBaseInfo";
        string fAppId = EConvert.ToString(Session["FAppId"]);
        var App = db.CF_Prj_Emp.Where(t => t.FAppId == fAppId).OrderBy(t => t.FType);
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();

    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 2].Controls[0] as LinkButton;
            if (lb != null)
            {
                lb.Text = "保存";
                lb.Attributes.Add("onclick", "return doSave(this);");
            }
            Literal txtResult = e.Item.FindControl("txtResult") as Literal;
 

            if (txtResult != null)
            {
                int fMType_01 = EConvert.ToInt(Session["FManageTypeId"]);
                CF_App_List app = db.CF_App_List.Where(t => t.FLinkId == FAppId && t.FManageTypeId == fMType_01).FirstOrDefault();
                if (app != null)
                {

                    string FUserId = EConvert.ToString(dg_List.DataKeys[e.Item.ItemIndex]);
                    CF_App_Idea Idea = db.CF_App_Idea.Where(t => t.FLinkId == app.FId && t.FUserId == FUserId).FirstOrDefault();
                    if (Idea != null)
                    {
                        e.Item.Cells[e.Item.Cells.Count - 3].Text = EConvert.ToShortDateString(Idea.FAppTime);
                        if (Idea.FResultInt == 1)
                        {
                            txtResult.Text = "合格";
                        }
                        else if (Idea.FResultInt == 2)
                        {
                            txtResult.Text = "不合格";
                        }

                    }

                }
            }
   
        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowEmpInfo();
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

}
