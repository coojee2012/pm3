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

public partial class JZDW_ApplyXMJZ_AddReport : Page
{
    ProjectDB db = new ProjectDB();
    int FManageTypeId = 29202;//初步设计人员意见
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            btnSave.Attributes["onclick"] = "return checkInfo();";
            showInfo();
        }
    }

    //显示
    private void showInfo()
    {
        string FID = EConvert.ToString(Request.QueryString["FID"]);
        string FAppId = EConvert.ToString(Request.QueryString["FAppId"]);
        var v = (from t in db.CF_App_List
                 join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                 where t.FId == FAppId
                 select new
                 {
                     t.FId,
                     t.FLinkId,
                     t.FState,
                     d.FPrjName
                 }).FirstOrDefault();
        if (v != null)
        {
            //工程名称
            liPrjName.Text = v.FPrjName;

            //显示人员信息
            string FId = Request.QueryString["FId"];
            CF_Prj_Emp emp = db.CF_Prj_Emp.Where(t => t.FId == FId).FirstOrDefault();
            if (emp != null)
            {
                pageTool tool = new pageTool(this, "li_");
                tool.fillPageControl(emp);

                ViewState["FEmpBaseInfo"] = emp.FEmpBaseInfo;

                //显示个人意见信息
                var idea = (from t in db.CF_App_Idea
                            where (t.FLinkId == FAppId && t.FUserId == emp.FEmpBaseInfo)
                            select new { t.FContent, t.FResult, t.FResultInt, t.FAppTime }).FirstOrDefault();
                if (idea != null)
                {
                    t_FAppTime.Text = idea.FAppTime.GetValueOrDefault().ToShortDateString();
                    //t_FResultInt.SelectedValue = idea.FResultInt.ToString();
                    t_FContent.Text = idea.FContent;
                }
            }

            //显示附件
            ShowFileInfo();


            //已提交不能修改
            if (v.FState > 1)
            {
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("btnEnable();");
            }
        }
    }

    #region 附件

    //显示附件
    private void ShowFileInfo()
    {
        string FEmpBaseinfoId = EConvert.ToString(ViewState["FEmpBaseInfo"]);
        string FAppId = EConvert.ToString(Request.QueryString["FAppId"]);
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
                    FIsMust = t.FIsMust == 1 ? "<font color='red'>是</font>" : "否",
                    AppFile = db.CF_AppPrj_FileOther.Where(f => t.FId == f.FPrjFileId && f.FAppId == FAppId && f.FUserId == FEmpBaseinfoId)
                };

        Pager1.RecordCount = v.Count();
        rep_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        rep_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页时隐藏分页控件
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
                ((Literal)e.Item.FindControl("lit_Count")).Text = "0";
                ((Literal)e.Item.FindControl("lit_Has")).Text = "<font color='red'>否</font>";
            }

            string FAppId = EConvert.ToString(Request.QueryString["FAppId"]);
            string FEmpBaseInfo = EConvert.ToString(ViewState["FEmpBaseInfo"]);
            //((Literal)e.Item.FindControl("lit_Has")).Text += "<input type=\"button\" class=\"m_btn_w4\" value=\"上传文件\" onclick='showAddWindow(\"../AppMain/FileUp.aspx?FPrjFileId=" + FID + "&FAppId=" + FAppId + "&FUserId=" + FEmpBaseInfo + "\",500,250);' />";

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
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }

    #endregion

    /// <summary>
    /// 验证附件是否上传
    /// </summary>
    /// <returns></returns>
    bool IsUploadFile(int? FMTypeId, string FAppId)
    {
        var v = db.CF_Sys_PrjList.Count(t => t.FIsMust == 1
            && t.FManageType == FMTypeId
            && db.CF_AppPrj_FileOther.Count(o => o.FPrjFileId == t.FId
                && o.FAppId == FAppId) < 1) > 0;
        return v;
    }

    //保存
    private void saveInfo()
    {
        SMS sms = new SMS();
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;

        string FID = Request.QueryString["FID"];
        string FAppId = EConvert.ToString(Request.QueryString["FAppId"]);
        string FEmpBaseInfo = EConvert.ToString(ViewState["FEmpBaseInfo"]);
        CF_App_List app = db.CF_App_List.Where(t => t.FId == FAppId).FirstOrDefault();
        if (app != null)
        {
            //验证必需的附件是否上传
            if (IsUploadFile(app.FManageTypeId, FAppId))
            {
                tool.showMessage("“附件上传”菜单中存在未上传的附件(必需上传的)，请先上传！");
                return;
            }
        }

        CF_Prj_Emp emp = db.CF_Prj_Emp.Where(t => t.FId == FID).FirstOrDefault();
        if (emp != null)
        {
            emp.FTxt1 = li_FTxt1.Text; //代填人
        }

        //保存意见
        CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == FAppId && t.FUserId == FEmpBaseInfo).FirstOrDefault();
        if (idea == null)
        {
            idea = new CF_App_Idea();
            db.CF_App_Idea.InsertOnSubmit(idea);
            idea.FId = Guid.NewGuid().ToString();
            idea.FIsdeleted = 0;
            idea.FLinkId = FAppId;
            idea.FUserId = FEmpBaseInfo;
            idea.FType = 29202;
            idea.FCreateTime = dTime;
        }
        idea.FTime = dTime;
        idea.FContent = t_FContent.Text;
        //idea.FResultInt = EConvert.ToInt(t_FResultInt.SelectedValue);
        //idea.FResult = t_FResultInt.SelectedItem.Text;
        idea.FResultInt = 1;
        idea.FAppTime = EConvert.ToDateTime(t_FAppTime.Text);


        db.SubmitChanges();
        tool.showMessageAndRunFunction("操作成功！", "window.returnValue=1;");
        showInfo();
    }

    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }


}
