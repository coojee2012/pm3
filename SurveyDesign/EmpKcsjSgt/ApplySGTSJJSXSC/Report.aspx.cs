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
using Approve.RuleApp;
public partial class KcsjSgt_ApplyKCCXXSC_Report : Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Attributes["onclick"] = "return checkInfo();";
            btnReport.Attributes["onclick"] = "if (confirm('确定提交吗？')){return checkInfo(1);}else{ return false;}";

            showInfo();
        }
    }

    public int isOver = 0;

    //显示
    private void showInfo()
    {
        string FAppId = Request.QueryString["FAppId"];
        var v = (from t in db.CF_App_List
                 join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                 where t.FId == FAppId
                 select new
                 {
                     t.FName,
                     t.FPrjId,
                     t.FYear,
                     t.FLinkId,
                     t.FBaseName,
                     d.FPrjName,
                     d.FDate6,
                     d.FTxt21,
                     d.FTxt3,
                     d.FInt3,
                     d.FInt4,
                     t.FState
                 }).FirstOrDefault();

        if (v != null)
        {
            if (v.FState != 0)
            {
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("btnEnable();");
                isOver = 1;
            }


            txtFPrjName.Text = v.FPrjName;
            hidd_FPrjId.Value = v.FPrjId;
            hidd_DataFId.Value = v.FLinkId;
            t_FDate6.Text = string.Format("{0:d}", v.FDate6);
            t_FInt4.SelectedValue = v.FInt4.ToString();
            t_FTxt21.Text = v.FTxt21;
            t_FInt3.Text = v.FInt3.ToString();
            t_FTxt3.Text = v.FTxt3;

            //找出建设单位FBaseinfoId
            hidd_JSDWFBaseinfoId.Value = db.CF_Prj_BaseInfo.Where(t => t.FId == v.FPrjId).Select(t => t.FBaseinfoId).FirstOrDefault();

            //各步骤办理情况 
            showIdea();

            //各人员意见
            showEmpYJ();



            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Where(st => st.FPrjId == v.FPrjId).FirstOrDefault();
            if (stop != null)
            {
                btnSave.Enabled = false;
                btnSave.ToolTip = "该项目已被中止，所有业务停止进行。";
                btnReport.Enabled = false;
                btnReport.ToolTip = "该项目已被中止，所有业务停止进行。";
            }
        }
        //显示汇总人 
        if (!(Request.QueryString["Self"] == "1")
            && string.IsNullOrEmpty(t_FTxt3.Text.Trim()))
        {
            string fEmpId = CurrentEmpUser.EmpId;
            if (!string.IsNullOrEmpty(fEmpId))
            {
                t_FTxt3.Text = db.CF_Emp_BaseInfo.Where(t => t.FId == fEmpId).Select(t => t.FName).FirstOrDefault();
                t_FTxt3.Enabled = false;
            }
        }
        if (string.IsNullOrEmpty(t_FDate6.Text))
            t_FDate6.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }



    #region 各步骤办理情况

    //各步骤办理情况 
    private void showIdea()
    {
        string FLinkId = hidd_DataFId.Value;
        var v = (from a in db.CF_App_List
                 join i in db.CF_App_Idea on a.FId equals i.FLinkId
                 where a.FLinkId == FLinkId && a.FState > 0
                 && ((a.FManageTypeId == 30103 && i.FUserId == null) || a.FManageTypeId != 30103)
                 orderby a.FCreateTime
                 select new
                 {
                     i.FId,
                     a.FManageTypeId,
                     i.FResult,
                     i.FContent,
                     i.FAppTime
                 }).ToList();

        DG_List.DataSource = v;
        DG_List.DataBind();
    }
    //各步骤办理情况 列表
    protected void DG_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string s = "";
            string FManageTypeId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FManageTypeId"));
            switch (FManageTypeId)
            {
                case "300":
                    s = "受理";
                    break;
                case "30101":
                    s = "程序性审查";
                    break;
                case "30102":
                    s = "审查人员安排";
                    break;
                case "30103":
                    s = "技术性审查";
                    break;
                case "305":
                    s = "勘察文件审查备案";
                    break;
            }
            e.Row.Cells[1].Text = s;
        }
    }

    #endregion

    #region 各人员意见



    //显示人员意见
    private void showEmpYJ()
    {
        string FAppId = Request.QueryString["FAppId"];
        //得从上一步“审查人员安排”中取安排的人员列表
        var v = from a in db.CF_App_List
                join e in db.CF_Prj_Emp on a.FId equals e.FAppId
                join ll in db.CF_App_List on a.FLinkId equals ll.FLinkId
                join id in db.CF_App_Idea.Where(t => t.FLinkId == FAppId)
                      on new { FEmpBaseInfo = e.FEmpBaseInfo } equals new { FEmpBaseInfo = id.FUserId } into idea
                from i in idea.DefaultIfEmpty()
                where a.FManageTypeId == 30102 && a.FLinkId == hidd_DataFId.Value
                    && a.FState == 6 && ll.FManageTypeId == 30103
                    && (e.FType == 2 || e.FType == 3)//注册人员、非注册人员
                    && !e.FIsDeleted.GetValueOrDefault()
                select new
                {
                    e.FId,
                    self = e.FEmpBaseInfo == CurrentEmpUser.EmpId ? "0" : "1",
                    FYJ = (ll.FState > 2 || e.FEmpBaseInfo != CurrentEmpUser.EmpId) ? "查看" : "填写",
                    e.FAppId,
                    e.FName,//审查人 
                    e.FMajor,//专业 
                    e.FAppMajor,
                    FOrder = i == null ? "" : i.FOrder.ToString(),//违返强条数量
                    FResult = i == null ? "" : i.FResult,//审查结论
                    FResultInt = i == null ? "" : i.FResultInt.GetValueOrDefault().ToString(),//审查结论
                    FContent = i == null ? "" : i.FContent,//审查意见
                    FAppTime = i == null ? DateTime.Now : i.FAppTime.GetValueOrDefault(),//审查时间
                };

        DGList.DataSource = v;
        DGList.DataBind();
    }

    protected void DGList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string FResult = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FResult"));
            string FResultInt = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FResultInt"));
            if (string.IsNullOrEmpty(FResult))
            {
                e.Row.Cells[6].Text = "";
            }


            string s = "";
            if (FResultInt == "6")
            {
                s = "<font color='green'>" + FResult + "</font>";
            }
            else if (FResultInt == "3")
            {
                s = "<font color='red'>" + FResult + "</font>";

                //人员有不合格结果时。这里必须是不合格。
                t_FInt4.SelectedValue = "3";
                t_FInt4.Enabled = false;
            }
            else if (FResultInt == "7") //人员给出 补正材料 业务终止重来。
            {
                s = "<font color='red'>" + FResult + "</font>";


                string FName = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FName"));

                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("btnEnable();");
                lit_TS.Text = "<div class='ts'>注意：已被审查人“" + FName + "”给出“补正材料”意见，业务已终止！</div>";

            }
            e.Row.Cells[2].Text = "<span name=\"span_FResult\">" + s + "</span>";
        }
    }

    //刷新 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showEmpYJ();
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("window.returnValue=1;");
    }

    #endregion


    //保存
    private void saveInfo(bool isReport)
    {
        //保存受理信息
        CF_Prj_Data data = db.CF_Prj_Data.Where(t => t.FId == hidd_DataFId.Value).FirstOrDefault();
        if (data != null)
        {
            data.FInt4 = EConvert.ToInt(t_FInt4.SelectedValue);//审查结果
            data.FTxt21 = t_FTxt21.Text;//审查意见
            data.FTxt3 = t_FTxt3.Text;//汇总人
            data.FInt3 = EConvert.ToInt(t_FInt3.Text);//审查次数
        }
        if (!isReport)
            db.SubmitChanges();
    }



    //提交
    private void Report()
    {
        RApp ra = new RApp();
        string dept = ComFunction.GetDefaultDept();
        SMS sms = new SMS();
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        string FAppId = Request.QueryString["FAppId"];

        CF_App_List app = db.CF_App_List.Where(t => t.FId == FAppId).FirstOrDefault();
        if (app != null)
        {
            //保存业务状态
            app.FState = EConvert.ToInt(t_FInt4.SelectedValue);
            app.FReportDate = dTime;
            app.FAppDate = dTime;
            app.FBarCode = ra.GetBarCode(dept, "145");

            //保存意见
            CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == FAppId && t.FUserId == null).FirstOrDefault();
            if (idea == null)
            {
                idea = new CF_App_Idea();
                db.CF_App_Idea.InsertOnSubmit(idea);
                idea.FId = Guid.NewGuid().ToString();
                idea.FIsdeleted = 0;
                idea.FLinkId = app.FId;
                idea.FTime = dTime;
                idea.FType = app.FManageTypeId;
                idea.FSystemId = EConvert.ToInt(CurrentEntUser.SystemId);
            }
            idea.FResultInt = EConvert.ToInt(t_FInt4.SelectedValue);//结果
            idea.FResult = t_FInt4.SelectedItem.Text;//结果  文字
            idea.FContent = t_FTxt21.Text;//意见
            idea.FAppTime = EConvert.ToDateTime(t_FDate6.Text);//办理时间

            string s = "";//发送系统消息 


            if (app.FState == 6)//合格
            {
                //办完“技术性审查(设计)30103”后自动创建下一个业务“施工图设计文件备案305”
                if (!CreateNewApp(app.FPrjId, app.FLinkId, txtFPrjName.Text, 305))
                {
                    tool.showMessage("创建下一个业务失败！业务已存在。");
                    return;
                }
                s = "“" + CurrentEntUser.EntName + "”对工程“" + txtFPrjName.Text + "”办施工图设计文件“技术性审查”的结果为“" + idea.FResult + "”。";
            }
            else if (app.FState == 3)//修改 (打回建设单位，要重新做勘察文件审查合同备案、业务重新从受理来。)
            {
                s = "“" + CurrentEntUser.EntName + "”对工程“" + txtFPrjName.Text + "”办施工图设计文件“技术性审查”的结果为“" + idea.FResult + "”，您需要重新办理勘察合同备案。";
            }

            sms.SendMessage(hidd_JSDWFBaseinfoId.Value, s);
        }
        else
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }

        db.SubmitChanges();
        Session["FIsApprove"] = 1;
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
        app.FLinkId = FDataFID;
        string aid = Request.QueryString["FAppId"];
        string fbid = db.CF_App_List.Where(t => t.FId == aid).Select(t => t.FBaseinfoId).FirstOrDefault();
        if (string.IsNullOrEmpty(fbid))
        {
            tool.showMessage("数据有误，不能进行下一步的业务办理！");
            return false;
        }
        app.FBaseinfoId = fbid;
        app.FPrjId = FPrjId;
        app.FName = dTime.Year + " " + db.getManageTypeName(fMType);
        app.FManageTypeId = fMType;
        app.FwriteDate = dTime;
        app.FState = 0;
        app.FIsDeleted = false;
        app.FYear = dTime.Year;
        app.FMonth = dTime.Month;
        app.FBaseName = db.CF_Ent_BaseInfo.Where(t => t.FId == fbid).Select(t => t.FName).FirstOrDefault();
        app.FTime = dTime;
        app.FCreateTime = dTime;
        app.FReportCount = 1;
        db.CF_App_List.InsertOnSubmit(app);

        return true;
    }


    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        saveInfo(false);
        tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
    }

    //提交
    protected void btnReport_Click(object sender, EventArgs e)
    {
        saveInfo(true);
        Report();
    }
}
