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

public partial class KcsjSgt_ApplyKCJSXSC_EmpReport : Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    RCenter rq = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("tab(" + t_FResultInt.SelectedValue + ");changeBZ('" + t_FTxt1.SelectedValue + "');");
        if (!IsPostBack)
        {
            btnSave.Attributes["onclick"] = "return checkInfo();";
            showInfo();
        }
    }

    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string FId = Request.QueryString["FId"];
        //本业务
        string FAppId = Request.QueryString["FAppId"];

        var app = (from t in db.CF_App_List
                   join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                   where t.FId == FAppId
                   select new
                   {
                       t.FId,
                       d.FPrjName,
                       t.FState,
                       t.FManageTypeId
                   }).FirstOrDefault();
        if (app != null)
        {
            ViewState["FManageTypeId"] = app.FManageTypeId;
            liPrjName.Text = app.FPrjName;

            if (app.FState > 0)
            {
                tool.ExecuteScript("btnEnable();");
            }

            var v = (from e in db.CF_Prj_Emp
                     join id in db.CF_App_Idea.Where(t => t.FLinkId == FAppId)
                         on new { FEmpBaseInfo = e.FEmpBaseInfo } equals new { FEmpBaseInfo = id.FUserId } into idea
                     from i in idea.DefaultIfEmpty()
                     where e.FId == FId
                     select new
                     {
                         e.FId,
                         e.FEmpBaseInfo,
                         e.FAppId,
                         e.FName,//审查人 
                         e.FMajor,//专业 
                         e.FFunction,//主要职责
                         e.FTxt1,//是否需要补正材料
                         e.FTxt2,//补正说明
                         e.FTxt3,//违反工程建设强制性标准编号及条文编号
                         FOrder = i == null ? "" : i.FOrder.ToString(),//违返强条数量
                         FResultInt = i == null ? "" : i.FResultInt.GetValueOrDefault().ToString(),//审查结论
                         FResult = i == null ? "" : i.FResult,//审查结论
                         FContent = i == null ? "" : i.FContent,//审查意见
                         FAppTime = i == null ? DateTime.Now : i.FAppTime.Value,//审查时间
                     }).FirstOrDefault();
            if (v != null)
            {
                ViewState["FEmpBaseInfo"] = v.FEmpBaseInfo;
                tool.fillPageControl(v);
            }

            //问题列表
            showWT();
        }
        ShowFileInfo();
    }

    #region 问题列表

    //显示问题列表
    private void showWT()
    {
        string FAppId = Request.QueryString["FAppId"];
        string FEmpBaseInfo = EConvert.ToString(ViewState["FEmpBaseInfo"]);

        var v = from t in db.CF_Prj_Text
                where t.FAppId == FAppId && t.FUserId == FEmpBaseInfo
                select new
                {
                    t.FId,
                    t.FTxt1,
                    t.FTxt2,
                    t.FRemark,
                    t.FContent
                };

        DGList.DataSource = v;
        DGList.DataBind();
    }

    //列表
    protected void DGList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string FTxt1 = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FTxt1"));
            string FRemark = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FRemark"));


            e.Row.Cells[1].Text = db.getDicName(FTxt1);
            e.Row.Cells[4].Text = db.getDicName(FRemark);
        }
    }

    //列表删除
    protected void DGList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            string FID = e.CommandArgument.ToString();
            CF_Prj_Text v = db.CF_Prj_Text.Where(t => t.FId == FID).FirstOrDefault();
            if (v != null)
            {
                db.CF_Prj_Text.DeleteOnSubmit(v);
                db.SubmitChanges();

                pageTool tool = new pageTool(this.Page);
                tool.showMessage("删除成功");
                showWT();
            }
        }
    }
    //刷新按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showWT();
    }
    #endregion


    #region 附件

    //附件
    private void ShowFileInfo()
    {
        //当前业务类型
        int FManageTypeId = 28803;//技术性审查（勘察）
        string FAppId = Request.QueryString["FAppId"];
        string EmpId = EConvert.ToString(ViewState["FEmpBaseInfo"]);
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
                    AppFile = db.CF_AppPrj_FileOther.Where(f => t.FId == f.FPrjFileId && f.FAppId == FAppId && f.FUserId == EmpId)
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
            string FAppId = Request.QueryString["FAppId"];
            string EmpId = EConvert.ToString(ViewState["FEmpBaseInfo"]);
            //((Literal)e.Item.FindControl("lit_Has")).Text += "<input type=\"button\" class=\"m_btn_w4\" value=\"上传文件\" onclick='showAddWindow(\"../AppMain/FileUp.aspx?FPrjFileId=" + FID + "&FAppId=" + FAppId + "&FUserId=" + EmpId + "\",500,250);' />";
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
        string FAppId = Request.QueryString["FAppId"];
        string FID = Request.QueryString["FID"];
        string EmpId = EConvert.ToString(ViewState["FEmpBaseInfo"]);
        int FManageTypeId = EConvert.ToInt(ViewState["FManageTypeId"]);
         CF_App_List appList = db.CF_App_List.Where(t => t.FId == FAppId ).FirstOrDefault();
         if (appList != null)
         {
             //验证必需的附件是否上传
             if (IsUploadFile(appList.FManageTypeId, FAppId))
             {
                 tool.showMessage("“附件上传”菜单中存在未上传的附件(必需上传的)，请先上传！");
                 return;
             }
         }
        CF_Prj_Emp emp = db.CF_Prj_Emp.Where(t => t.FId == FID).FirstOrDefault();
        if (emp != null)
        {
            emp.FTxt1 = t_FTxt1.Text;
            emp.FTxt2 = t_FTxt2.Text;
            emp.FTxt3 = t_FTxt3.Text;
        }

        //保存意见
        CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == FAppId && t.FUserId == EmpId).FirstOrDefault();
        if (idea == null)
        {
            idea = new CF_App_Idea();
            db.CF_App_Idea.InsertOnSubmit(idea);
            idea.FId = Guid.NewGuid().ToString();
            idea.FIsdeleted = 0;
            idea.FLinkId = FAppId;
            idea.FUserId = EmpId;
            idea.FType = FManageTypeId;
        }
        idea.FTime = dTime;
        idea.FResultInt = EConvert.ToInt(t_FResultInt.SelectedValue);
        idea.FResult = t_FResultInt.SelectedItem.Text;
        idea.FOrder = EConvert.ToInt(t_FOrder.Text);
        idea.FContent = t_FContent.Text;
        idea.FAppTime = EConvert.ToDateTime(t_FAppTime.Text);


        //补正材料时，将业务终止。
        if (t_FResultInt.SelectedValue == "7")
        {
            CF_App_List app = db.CF_App_List.Where(t => t.FId == FAppId).FirstOrDefault();
            if (app != null)
            {
                app.FState = 7;
                app.FAppDate = dTime;
            }
            //保存意见
            CF_App_Idea id = db.CF_App_Idea.Where(t => t.FLinkId == FAppId && t.FUserId == null).FirstOrDefault();
            if (id == null)
            {
                id = new CF_App_Idea();
                db.CF_App_Idea.InsertOnSubmit(id);
                id.FId = Guid.NewGuid().ToString();
                id.FIsdeleted = 0;
                id.FLinkId = app.FId;
                id.FTime = dTime;
                id.FType = FManageTypeId;
            }
            id.FResultInt = EConvert.ToInt(t_FResultInt.SelectedValue);
            id.FResult = t_FResultInt.SelectedItem.Text;
            id.FAppTime = EConvert.ToDateTime(t_FAppTime.Text);
        }





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
