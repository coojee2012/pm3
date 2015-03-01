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
            btnReport.Attributes["onclick"] = "if(checkInfo()) return confirm('确定提交吗？');else{ return false;}";
            showInfo();
        }
    }


    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
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
                     d.FDate2,
                     d.FInt2,
                     d.FTxt20,
                     t.FState
                 }).FirstOrDefault();

        if (v != null)
        {
            txtFPrjName.Text = v.FPrjName;
            hidd_FPrjId.Value = v.FPrjId;
            hidd_DataFId.Value = v.FLinkId;
            t_FDate2.Text = string.Format("{0:d}", v.FDate2);
            t_FInt2.SelectedValue = v.FInt2.ToString();
            t_FTxt20.Text = v.FTxt20;

            //各步骤办理情况 
            showIdea();


            //找出建设单位FBaseinfoId
            hidd_JSDWFBaseinfoId.Value = db.CF_Prj_BaseInfo.Where(t => t.FId == v.FPrjId).Select(t => t.FBaseinfoId).FirstOrDefault();


            if (v.FState != 0)
            {
                tool.ExecuteScript("btnEnable();");
            }

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

        if (string.IsNullOrEmpty(t_FDate2.Text))
            t_FDate2.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }


    //各步骤办理情况 
    private void showIdea()
    {
        string FLinkId = hidd_DataFId.Value;
        string FBaseinfoId = CurrentEntUser.EntId;
        var v = (from a in db.CF_App_List
                 join i in db.CF_App_Idea on a.FId equals i.FLinkId
                 where a.FLinkId == FLinkId && a.FState > 0 && (a.FBaseinfoId == FBaseinfoId || a.FToBaseinfoId == FBaseinfoId)
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
                    s = "合同备案确认";
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


    //保存
    private void saveInfo(bool isReport)
    {
        //保存受理信息
        CF_Prj_Data data = db.CF_Prj_Data.Where(t => t.FId == hidd_DataFId.Value).FirstOrDefault();
        if (data != null)
        {
            data.FInt2 = EConvert.ToInt(t_FInt2.SelectedValue);
            data.FTxt20 = t_FTxt20.Text;
            data.FDate2 = EConvert.ToDateTime(t_FDate2.Text);//办理时间

            if (isReport)
            {//业务流水号
                RApp ra = new RApp();
                string FAppId = Request.QueryString["FAppId"];
                data.FTxt8 = ra.GetAutoNO(FAppId);
            }
        }
        if (!isReport)
            db.SubmitChanges();
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
            //保存业务状态           
            app.FReportDate = dTime;
            app.FAppDate = dTime;

            //保存意见
            CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == FAppId).FirstOrDefault();
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
            idea.FResultInt = EConvert.ToInt(t_FInt2.SelectedValue);//结果
            idea.FResult = t_FInt2.SelectedItem.Text;//结果  文字
            idea.FContent = t_FTxt20.Text;//意见
            idea.FAppTime = EConvert.ToDateTime(t_FDate2.Text);//办理时间

            string s = "";//发送系统消息
            if (t_FInt2.SelectedValue == "1")//合格
            {
                //办完“程序性审查30101”后自动创建下一个业务“审查人员安排30102”
                if (!CreateNewApp(app.FPrjId, app.FLinkId, txtFPrjName.Text, 30102))
                {
                    tool.showMessage("创建下一个业务失败！业务已存在。");
                    return;
                }
                s = "“" + CurrentEntUser.EntName + "”对工程“" + txtFPrjName.Text + "”办理施工图设计文件“程序性审查”结果为“" + idea.FResult + "”。";

                app.FState = 6;//直接就办结了。
            }
            else
            {
                s = "“" + CurrentEntUser.EntName + "”对工程“" + txtFPrjName.Text + "”办理施工图设计文件“程序性审查”结果为“" + idea.FResult + "”，请查看并重新办理勘察合同备案申请。";
                app.FState = 3;//不合格。
            }
            sms.SendMessage(hidd_JSDWFBaseinfoId.Value, s);//发送系统消息
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
