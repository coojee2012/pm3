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
public partial class JSDW_appmain_Report : Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("tab();");
        if (!IsPostBack)
        {
            btnSave.Attributes["onclick"] = "return checkInfo();";
            btnReport.Attributes["onclick"] = "if (confirm('确定提交吗？')){return checkInfo();}else {return false;}";
            BindControl();
            showInfo();
        }
    }


    //受理时检查业务状态是否发生变化（是否已被撤回...）
    protected override void OnPreInit(EventArgs e)
    {
        if (CurrentEntUser.SystemId != "100")//排除建设单位的
        {
            string FBaseinfoID = CurrentEntUser.EntId;
            string FAppId = Request.QueryString["FAppId"];
            var v = db.CF_App_List.Where(t => t.FToBaseinfoId == FBaseinfoID && t.FId == FAppId && t.FState >= 1).Select(t => t.FId).FirstOrDefault();
            if (v == null)
            {
                pageTool tool = new pageTool(this.Page);
                tool.showMessageAndRunFunction("该业务已被建设单位撤回，无法继续办理！", "window.returnValue=1;window.close();");
            }
        }
    }

    //绑定
    private void BindControl()
    {
        txtFDate2.Text = txtFDate6.Text = txtFDate7.Text = EConvert.ToShortDateString(DateTime.Now);

        string FAppId = Request.QueryString["FAppId"];
    }


    //显示
    private void showInfo()
    {
        string FAppId = Request.QueryString["FAppId"];
        var app = (from t in db.CF_App_List
                   join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                   where t.FId == FAppId
                   select new
                   {
                       t.FName,
                       t.FYear,
                       t.FLinkId,
                       t.FBaseName,
                       t.FAppDate,
                       t.FPrjId,
                       t.FState,
                       d.FPrjName,
                       d.FDate2,
                       d.FDate6,
                       d.FDate7,
                       d.FTxt16,
                       d.FTxt17,
                       d.FTxt19,
                       d.FTxt20,
                       d.FInt1
                   }).FirstOrDefault();
        if (app != null)
        {
            txtJSDW.Text = app.FBaseName;
            txtFPrjName.Text = app.FPrjName;
            t_FInt1.SelectedValue = app.FInt1.GetValueOrDefault().ToString();

            t_FTxt16.Text = app.FTxt16;
            t_FTxt17.Text = app.FTxt17;
            t_FTxt19.Text = app.FTxt19;
            t_FTxt20.Text = app.FTxt20;

            if (app.FDate2.GetValueOrDefault() != DateTime.MinValue)
                txtFDate2.Text = app.FDate2.GetValueOrDefault().ToShortDateString();
            if (app.FDate6.GetValueOrDefault() != DateTime.MinValue)
                txtFDate6.Text = app.FDate6.GetValueOrDefault().ToShortDateString();
            if (app.FDate7.GetValueOrDefault() != DateTime.MinValue)
                txtFDate7.Text = app.FDate7.GetValueOrDefault().ToShortDateString();
            hidd_FDataID.Value = app.FLinkId;

            //显示受理信息
            if (app.FState != 1)
                shoData(app.FLinkId);

            //显示附件
            showFileList(FAppId);

            //只有在上报、未受理状态，才能进行提交
            if (app.FState == 1)
            {
                btnSave.Enabled = true;
            }
            else
            {
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("btnEnable();");
            }


            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Where(st => st.FPrjId == app.FPrjId).FirstOrDefault();
            if (stop != null)
            {
                btnSave.Enabled = false;
                btnSave.ToolTip = "该项目已被中止，所有业务停止进行。";
                btnReport.Enabled = false;
                btnReport.ToolTip = "该项目已被中止，所有业务停止进行。";
            }
        }
    }

    //显示受理信息
    private void shoData(string FLinkId)
    {
        var v = (from t in db.CF_Prj_Data
                 where t.FId == FLinkId
                 select t).FirstOrDefault();
        if (v != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(v);
        }
    }


    #region 附件

    //显示附件
    private void showFileList(string FAppId)
    {
        //当前业务类型
        int FManageTypeId = 292;
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
                    AppFile = db.CF_AppPrj_FileOther.Where(f => t.FId == f.FPrjFileId && f.FAppId == FAppId)
                };


        rep_List.DataSource = v;
        rep_List.DataBind();
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


            ((Literal)e.Item.FindControl("lit_Has")).Text += "<input type=\"button\" class=\"m_btn_w4\" value=\"上传文件\" onclick='showAddWindow(\"../AppMain/FileUp.aspx?FAppId=" + Request.QueryString["FAppId"] + "&FPrjFileId=" + FID + "\",500,250);' />";

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

                string FAppId = Request.QueryString["FAppId"];
                showFileList(FAppId);
            }
        }
    }

    //上传合同附件
    protected void btnShowFile_Click(object sender, EventArgs e)
    {
        string FAppId = Request.QueryString["FAppId"];
        showFileList(FAppId);
    }

    #endregion

    //保存
    private void saveInfo(bool isReport)
    {
        string FAppId = Request.QueryString["FAppId"];
        //保存受理信息
        CF_Prj_Data data = db.CF_Prj_Data.Where(t => t.FAppId == FAppId).FirstOrDefault();
        if (data != null)
        {
            data.FInt1 = EConvert.ToInt(t_FInt1.SelectedValue);

            data.FDate2 = EConvert.ToDateTime(txtFDate2.Text);
            data.FDate6 = EConvert.ToDateTime(txtFDate6.Text);
            data.FDate7 = EConvert.ToDateTime(txtFDate7.Text);

            data.FTxt16 = t_FTxt16.Text;
            data.FTxt19 = t_FTxt19.Text;
            data.FTxt20 = t_FTxt20.Text;
            data.FTxt17 = t_FTxt17.Text;

        }
        if (!isReport)
            db.SubmitChanges();
    }
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

    //提交
    private void Report()
    {
        SMS sms = new SMS();
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        string FAppId = Request.QueryString["FAppId"];

        CF_App_List app = db.CF_App_List.Where(t => t.FId == FAppId).FirstOrDefault();
        if (app != null)
        {
            //验证必需的附件是否上传
            if (IsUploadFile(292, FAppId))
            {
                tool.showMessage("“附件上传”菜单中存在未上传的附件(必需上传的)，请先上传！");
                return;
            }
            //保存业务状态
            app.FState = EConvert.ToInt(t_FInt1.SelectedValue);
            app.FAppDate = dTime;

            //保存意见
            CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == app.FId && t.FReportCount == app.FReportCount).FirstOrDefault();
            if (idea == null)
            {
                idea = new CF_App_Idea();
                idea.FId = Guid.NewGuid().ToString();
                idea.FIsdeleted = 0;
                idea.FLinkId = app.FId;
                idea.FTime = dTime;
                idea.FType = app.FManageTypeId;
                idea.FSystemId = EConvert.ToInt(CurrentEntUser.SystemId);
                idea.FReportCount = app.FReportCount;
                db.CF_App_Idea.InsertOnSubmit(idea);
            }
            idea.FResultInt = EConvert.ToInt(t_FInt1.SelectedValue);
            idea.FResult = t_FInt1.SelectedItem.Text;
            string s = "";//发送系统消息
            if (t_FInt1.SelectedValue == "6")//受理
            {
                s = "“" + CurrentEntUser.EntName + "”接受了工程“" + txtFPrjName.Text + "”的初步设计合同申请。";

                idea.FContent = t_FTxt19.Text;
                idea.FAppTime = EConvert.ToDateTime(txtFDate6.Text);


                //办完“受理”后自动创建下一个业务“初步设计人员安排29201”
                if (!CreateNewApp(app.FPrjId, app.FLinkId, txtFPrjName.Text, 29201))
                {
                    tool.showMessage("创建下一个业务失败！业务已存在。");
                    return;
                }


                //自动创建合同备案业务“合同备案414”
                CreateBackApp(app.FPrjId, app.FLinkId, txtFPrjName.Text, 414, app.FManageTypeId.GetValueOrDefault(),app.FId);
            }
            else if (t_FInt1.SelectedValue == "2")//退回 
            {
                s = "“" + CurrentEntUser.EntName + "”退回了工程“" + txtFPrjName.Text + "”的初步设计合同备案申请。";
                idea.FResultInt = 2;
                idea.FResult = "退回";
                idea.FAppTime = EConvert.ToDateTime(txtFDate2.Text);
                idea.FContent = t_FTxt20.Text;
            }
            else if (t_FInt1.SelectedValue == "7")//不予受理 
            {
                s = "“" + CurrentEntUser.EntName + "”对工程“" + txtFPrjName.Text + "”初步设计合同备案申请的办理结果为“不予接受”。";
                idea.FResultInt = 7;
                idea.FResult = "不予接受";
                idea.FAppTime = EConvert.ToDateTime(txtFDate7.Text);
                idea.FContent = t_FTxt17.Text;
            }
            sms.SendMessage(app.FBaseinfoId, s);
        }
        else
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }

        db.SubmitChanges();
        tool.showMessageAndRunFunction("提交成功！", "window.returnValue=1;");
        showInfo();
    }

    /// <summary>
    /// 自动创建下一个业务
    /// </summary>
    /// <param name="FPrjId">CF_Prj_Baseinfo.FID</param>
    /// <param name="FDataFID">CF_Prj_Data.FID</param>
    /// <param name="FPrjName">工程名称</param>
    /// <param name="fMType">CF_App_List.FManageTypeId</param>
    /// <returns></returns>
    private bool CreateNewApp(string FPrjId, string FDataFID, string FPrjName, int fMType)
    {
        pageTool tool = new pageTool(this.Page);
        var v = db.CF_App_List.Where(t => t.FLinkId == FDataFID && t.FManageTypeId == fMType).Select(t => t.FId).ToList();
        if (v.Count > 0)
            return false;

        string FAppId = Guid.NewGuid().ToString();
        DateTime dTime = DateTime.Now;

        //业务表
        CF_App_List app = new CF_App_List();
        app.FId = FAppId;
        app.FLinkId = FDataFID;//关联受理合同备案时的CF_Prj_Data.FID，表示这这一次审查（受理、初步设计人员安排、初步设计成果移交）。
        app.FBaseinfoId = CurrentEntUser.EntId;
        app.FPrjId = FPrjId;
        app.FName = dTime.Year + " " + db.getManageTypeName(fMType);
        app.FManageTypeId = fMType;
        app.FwriteDate = dTime;
        app.FState = 0;
        app.FIsDeleted = false;
        app.FYear = dTime.Year;
        app.FMonth = dTime.Month;
        app.FBaseName = CurrentEntUser.EntName;
        app.FTime = dTime;
        app.FCreateTime = dTime;
        app.FReportCount = 1;
        db.CF_App_List.InsertOnSubmit(app);

        return true;
    }

    /// <summary>
    /// 自动创建合同备案业务
    /// </summary>
    /// <param name="FPrjId">CF_Prj_Baseinfo.FID</param>
    /// <param name="FDataFID">CF_Prj_Data.FID</param>
    /// <param name="FPrjName">工程名称</param>
    /// <param name="fMType">创建业务类型CF_App_List.FManageTypeId</param>
    /// <param name="thisMType">本次业务类型</param>
    /// <param name="OldFAppId">本业务FAppId</param>
    /// <returns></returns>
    private bool CreateBackApp(string FPrjId, string FDataFID, string FPrjName, int fMType, int thisMType, string OldFAppId)
    {
        DateTime dTime = DateTime.Now;
        string FAppId = Guid.NewGuid().ToString();
        CF_App_List app = new CF_App_List();//业务
        app.FId = FAppId;
        app.FLinkId = FAppId;
        app.FManageTypeId = fMType;
        app.FwriteDate = dTime;
        app.FState = 0;
        app.FIsDeleted = false;
        app.FName = dTime.Year + " " + db.getManageTypeName(fMType);//业务名
        app.FYear = dTime.Year;//年份
        app.FMonth = dTime.Month;//月份
        app.FBaseName = CurrentEntUser.EntName;//单位名
        app.FBaseinfoId = CurrentEntUser.EntId;//单位ID
        app.FPrjId = FPrjId;//关联项目
        app.FTime = dTime;
        app.FCreateTime = dTime;
        app.FReportCount = 1;
        db.CF_App_List.InsertOnSubmit(app);

        CF_Prj_Data data = new CF_Prj_Data();
        data.FId = FAppId;
        data.FAppId = FAppId;
        data.FInt3 = thisMType;//本次业务类型
        data.FPrjId = FPrjId;//关联项目
        data.FPrjName = FPrjName;//工程名
        data.FTxt1 = txtJSDW.Text;//建设单位
        data.FTxt7 = CurrentEntUser.EntName;//勘察单位（自已）
        data.FDate1 = dTime;//合同备案确认时间
        data.FPriItemId = FDataFID;//这个是和合同备案业务关联起来和手工加的区分（合同备案业务的CF_Prj_Data.FID）
        data.FBaseInfoId = CurrentEntUser.EntId;
        data.FType = fMType;
        data.FTime = dTime;
        data.FCreateTime = dTime;
        data.FIsDeleted = false;
        db.CF_Prj_Data.InsertOnSubmit(data);

        //导入联合体单位
        IQueryable<CF_Prj_Ent> OldEnt = db.CF_Prj_Ent.Where(t => t.FAppId == OldFAppId && t.FEntType == 15503);
        foreach (CF_Prj_Ent t in OldEnt)
        {
            CF_Prj_Ent ent = new CF_Prj_Ent();
            db.CF_Prj_Ent.InsertOnSubmit(ent);
            ent = t.Copy(ent);
            ent.FId = Guid.NewGuid().ToString();
            ent.FAppId = FAppId;
            ent.FTime = dTime;
            ent.FCreateTime = dTime;
            ent.FIsDeleted = false;
        }
        return true;
    }


    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        saveInfo(false);
        tool.showMessage("保存成功");
    }

    //提交
    protected void btnReport_Click(object sender, EventArgs e)
    {
        saveInfo(true);
        Report();
    }

}
