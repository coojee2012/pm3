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
public partial class KcsjSgt_ApplyKCCXXSC_Report : Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Attributes["onclick"] = "return checkInfo();";

            conBind();
            showInfo();
        }
    }

    //绑定默认
    private void conBind()
    {
        if (Request.QueryString["c"] == "1")//是来变更的来着
        {
            Title += "[变更]";
            lit_title.Text += "[<tt>变更</tt>]";

            btnSave.CssClass = "m_btn_w4";
            btnSave.Text = "保存变更";
            btnReport.Visible = false;

            t_FDate3.Enabled = false;
            t_FDate3.Enabled = false;
        }

    }

    public int isOver = 0;

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
                     d.FDate3,
                     d.FDate4,
                     d.FDate5,
                     t.FState
                 }).FirstOrDefault();

        if (v != null)
        {

            if (v.FState != 0 && Request.QueryString["c"] != "1")
            {
                tool.ExecuteScript("btnEnable();");
                EmpList1.Columns[EmpList1.Columns.Count - 2].Visible = false;
                EmpList2.Columns[EmpList2.Columns.Count - 2].Visible = false;
                isOver = 1;
            }

            txtFPrjName.Text = v.FPrjName;
            hidd_FPrjId.Value = v.FPrjId;
            hidd_DataFId.Value = v.FLinkId;
            t_FDate5.Text = string.Format("{0:d}", v.FDate5);//办时是
            t_FDate3.Text = string.Format("{0:d}", v.FDate3);
            t_FDate4.Text = string.Format("{0:d}", v.FDate4);

            //找出建设单位FBaseinfoId
            hidd_JSDWFBaseinfoId.Value = db.CF_Prj_BaseInfo.Where(t => t.FId == v.FPrjId).Select(t => t.FBaseinfoId).FirstOrDefault();


            //各步骤办理情况 
            showIdea();

            //人员
            //项目负责人 
            showEmp(FAppId, 1);
            //注册人员
            showEmpList(FAppId, 2);
            //非注册人员
            showEmpList(FAppId, 3);

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

        if (string.IsNullOrEmpty(t_FDate5.Text))
            t_FDate5.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }

    #region 各步骤办理情况

    //各步骤办理情况 
    private void showIdea()
    {
        string FLinkId = hidd_DataFId.Value;
        string FBaseinfoId = CurrentEntUser.EntId;
        var v = (from a in db.CF_App_List
                 join i in db.CF_App_Idea on a.FId equals i.FLinkId
                 where a.FLinkId == FLinkId && a.FState > 0 && (a.FBaseinfoId == FBaseinfoId || a.FToBaseinfoId == FBaseinfoId)
                 && ((a.FManageTypeId == 28803 && i.FUserId == null) || a.FManageTypeId != 28803)
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
                case "287":
                    s = "合同备案确认";
                    break;
                case "28801":
                    s = "程序性审查";
                    break;
                case "28802":
                    s = "审查人员安排";
                    break;
                case "28803":
                    s = "技术性审查";
                    break;
                case "290":
                    s = "勘察文件审查备案";
                    break;
            }
            e.Row.Cells[1].Text = s;
        }
    }

    #endregion

    #region 人员

    //项目负责人
    private void showEmp(string FAppId, int FType)
    {
        var emp = (from t in db.CF_Prj_Emp
                   where t.FAppId == FAppId && t.FType == FType
                   select new
                   {
                       t.FName,
                       t.FTel,
                       t.FCall,
                       t.FEmail,
                       t.FCertiNo,
                       t.FFunction,
                       t.FEmpBaseInfo,
                       t.FDataFrom,
                   }).FirstOrDefault();

        if (emp != null)
        {
            pageTool tool = new pageTool(this.Page, "emp_");
            tool.fillPageControl(emp);

            if (emp.FDataFrom == 1)
            { //如果是变更后的人，则显示首次经理是谁
                string oldFZR = db.CF_Prj_Emp.Where(t => t.FAppId == FAppId && t.FType == 1 && t.FIsDeleted.GetValueOrDefault()).Select(t => t.FName).FirstOrDefault();
                lit_oldEmp.Text = "首次安排的项目负责人：<tt>" + oldFZR + "</tt><br />";
            }
        }
    }

    //人员列表
    private void showEmpList(string FAppId, int FType)
    {
        var v = (from t in db.CF_Prj_Emp
                 where t.FAppId == FAppId && t.FType == FType
                 select new
                 {
                     t.FId,
                     t.FName,
                     t.FTel,
                     t.FCall,
                     t.FEmail,
                     t.FMajor,
                     t.FFunction,
                     t.FEmpBaseInfo,
                     t.FDataFrom,
                     t.FIsDeleted,
                 }).ToList();

        //查出是否有变更（有删除、有新增）
        if (v.Count(t => t.FDataFrom == 1 || t.FIsDeleted.GetValueOrDefault()) > 0)
        {
            string s = "";
            foreach (var t in v.Where(t => t.FDataFrom.GetValueOrDefault() != 1))
            {
                s += (!string.IsNullOrEmpty(s) ? "、" : "") + "<tt>" + t.FName + "</tt>";
            }
            if (FType == 2)//注册人员
            {
                tr_oldEmp2.Visible = true;
                lit_oldEmpList2.Text = "首次安排的人员：" + s;
            }
            else if (FType == 3)//非注册人员
            {
                tr_oldEmp3.Visible = true;
                lit_oldEmpList3.Text = "首次安排的人员：" + s;
            }
        }
        else
        {
            if (FType == 2)//注册人员
            {
                tr_oldEmp2.Visible = false;
            }
            else if (FType == 3)//非注册人员
            {
                tr_oldEmp3.Visible = false;
            }
        }


        //最后显示的是没删除的
        v = v.Where(t => !t.FIsDeleted.GetValueOrDefault()).ToList();

        if (FType == 2)//注册人员
        {
            EmpList1.DataSource = v;
            EmpList1.DataBind();
        }
        else if (FType == 3)//非注册人员
        {
            EmpList2.DataSource = v;
            EmpList2.DataBind();
        }

    }

    //选择项目负责人
    protected void btnSelectEmp_Click(object sender, EventArgs e)
    {
        var v = (from t in db.CF_Emp_BaseInfo
                 where t.FId == emp_FEmpBaseInfo.Value
                 select new
                 {
                     t.FName,
                     t.FCertiNo,
                     t.FRegistSpecialId,
                     FEmpBaseInfo = t.FId
                 }).FirstOrDefault();

        if (v != null)
        {
            emp_FEmpBaseInfo.Value = v.FEmpBaseInfo;
            emp_FName.Text = v.FName;
            emp_FCertiNo.Text = v.FCertiNo;
            emp_FFunction.Text = v.FRegistSpecialId; //注册专业
        }
    }
    //选择人员
    protected void btnSelEmpList_Click(object sender, EventArgs e)
    {
        string FAppId = Request.QueryString["FAppId"];

        showEmpList(FAppId, EConvert.ToInt(((Button)sender).CommandArgument));
    }
    //删除人员
    protected void btnDel_Click(object sender, EventArgs e)
    {
        string FAppId = Request.QueryString["FAppId"];
        pageTool tool = new pageTool(this.Page);
        string n = ((Button)sender).CommandArgument;
        if (n == "2")
        {
            for (int i = 0; i < EmpList1.Rows.Count; i++)
            {
                string FID = EConvert.ToString(EmpList1.DataKeys[i]["FId"]);
                //已选中的
                if (((CheckBox)EmpList1.Rows[i].FindControl("CheckItem_ZC")).Checked)
                {
                    CF_Prj_Emp v = db.CF_Prj_Emp.Where(t => t.FId == FID).FirstOrDefault();
                    if (v != null)
                    {
                        if (v.FDataFrom == 1 || Request.QueryString["c"] != "1")//不是变更时随便删啦
                        { //删除变更选择的人（物理删除）
                            db.CF_Prj_Emp.DeleteOnSubmit(v);
                        }
                        else
                        {//删除首次选择的人（逻辑删除）
                            v.FIsDeleted = true;
                        }
                    }
                }
            }
            db.SubmitChanges();
            tool.showMessageAndRunFunction("操作成功！", "window.returnValue=1;");
            showEmpList(FAppId, 2);
        }
        if (n == "3")
        {
            for (int i = 0; i < EmpList2.Rows.Count; i++)
            {
                string FID = EConvert.ToString(EmpList2.DataKeys[i]["FId"]);
                //已选中的
                if (((CheckBox)EmpList2.Rows[i].FindControl("CheckItem_FZC")).Checked)
                {
                    CF_Prj_Emp v = db.CF_Prj_Emp.Where(t => t.FId == FID).FirstOrDefault();
                    if (v != null)
                    {
                        if (v.FDataFrom == 1 || Request.QueryString["c"] != "1")//不是变更时随便删啦
                        { //删除变更选择的人（物理删除）
                            db.CF_Prj_Emp.DeleteOnSubmit(v);
                        }
                        else
                        {//删除首次选择的人（逻辑删除）
                            v.FIsDeleted = true;
                        }
                    }
                }
            }
            db.SubmitChanges();
            tool.showMessageAndRunFunction("操作成功！", "window.returnValue=1;");
            showEmpList(FAppId, 3);
        }

    }
    #endregion

    #region 保存

    //保存
    private void saveInfo(bool isReport)
    {
        if (this.EmpList1.Rows.Count < 1 && EmpList2.Rows.Count < 1)
        {
            RegisterStartupScript("key", "<script>alert('注册人员和非注册人员尚未填写')</script>");
            return;
        }
        DateTime dTime = DateTime.Now;
        //保存受理信息
        CF_Prj_Data data = db.CF_Prj_Data.Where(t => t.FId == hidd_DataFId.Value).FirstOrDefault();
        if (data != null)
        {
            data.FDate3 = EConvert.ToDateTime(t_FDate3.Text);
            data.FDate4 = EConvert.ToDateTime(t_FDate4.Text);
            data.FDate5 = EConvert.ToDateTime(t_FDate5.Text);//办理时间
        }

        //项目负责人
        string FAppId = Request.QueryString["FAppId"];
        pageTool tool = new pageTool(this.Page, "emp_");
        CF_Prj_Emp emp = db.CF_Prj_Emp.Where(t => t.FAppId == FAppId && t.FType == 1).FirstOrDefault();
        if (emp == null)
        {
            emp = new CF_Prj_Emp();
            db.CF_Prj_Emp.InsertOnSubmit(emp);
            emp.FId = Guid.NewGuid().ToString();
            emp.FIsDeleted = false;
            emp.FTime = dTime;
            emp.FCreateTime = dTime;
            emp.FType = 1;
            emp.FAppId = FAppId;
            emp.FPrjId = hidd_FPrjId.Value;
        }
        emp = tool.getPageValue(emp);

        //注册人员
        for (int i = 0; i < EmpList1.Rows.Count; i++)
        {
            string FID = EConvert.ToString(EmpList1.DataKeys[i]["FId"]);
            string FFunction = ((TextBox)EmpList1.Rows[i].FindControl("t_FFunction")).Text;

            CF_Prj_Emp v = db.CF_Prj_Emp.Where(t => t.FId == FID).FirstOrDefault();
            if (v != null)
            {
                v.FFunction = FFunction;
            }
        }

        //非注册人员
        for (int i = 0; i < EmpList2.Rows.Count; i++)
        {
            string FID = EConvert.ToString(EmpList2.DataKeys[i]["FId"]);
            string FFunction = ((TextBox)EmpList2.Rows[i].FindControl("t_FFunction")).Text;

            CF_Prj_Emp v = db.CF_Prj_Emp.Where(t => t.FId == FID).FirstOrDefault();
            if (v != null)
            {
                v.FFunction = FFunction;
            }
        }

        if (!isReport)
        {
            db.SubmitChanges();
            tool.showMessage("保存成功");
        }
        else
        {
            Report();//提交
        }
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
            app.FState = 6;//直接就办结了。
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
            idea.FResultInt = 1;//结果
            idea.FResult = "同意";//结果  文字
            idea.FContent = "同意，已安排";//意见
            idea.FAppTime = EConvert.ToDateTime(t_FDate5.Text);//办理时间

            //办完“审查人员安排28802”后自动创建下一个业务“技术性审查28803”
            //但下一步要所有人员办理完后才能继续办理。
            if (!CreateNewApp(app.FPrjId, app.FLinkId, txtFPrjName.Text, 28803))
            {
                tool.showMessage("创建下一个业务失败！业务已存在。");
                return;
            }
            string s = "";//发送系统消息 
            s = "“" + CurrentEntUser.EntName + "”对工程“" + txtFPrjName.Text + "”办理了勘察文件审查的“审查人员安排”。";
            sms.SendMessage(hidd_JSDWFBaseinfoId.Value, s);

            // //创建负责人人员消息
            //发送系统消息 
            sms.SendMessage(emp_FEmpBaseInfo.Value, "“" + CurrentEntUser.EntName + "”给您安排了“" + txtFPrjName.Text + "”工程的“项目负责人”职责，请及时处理。");
            //创建注册人员消息 
            for (int i = 0; i < EmpList1.Rows.Count; i++)
            {
                string empId = EmpList1.Rows[i].Cells[EmpList1.Columns.Count - 1].Text;
                sms.SendMessage(empId, "“" + CurrentEntUser.EntName + "”给您安排了“" + txtFPrjName.Text + "”工程的“审查人”职责，请及时处理。");
            }
            //创建非注册人员消息 
            for (int i = 0; i < EmpList2.Rows.Count; i++)
            {
                string empId = EmpList2.Rows[i].Cells[EmpList2.Columns.Count - 1].Text;
                sms.SendMessage(empId, "“" + CurrentEntUser.EntName + "”给您安排了“" + txtFPrjName.Text + "”工程的“审查人”职责，请及时处理。");
            }
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
        if (Request.QueryString["c"] == "1")
        {//如果是变更。 做以下保存操作 
            if (this.EmpList1.Rows.Count < 1 && EmpList2.Rows.Count < 1)
            {
                RegisterStartupScript("key", "<script>alert('注册人员和非注册人员尚未填写')</script>");
                return;
            }
            pageTool tool = new pageTool(this.Page, "emp_");
            string FAppId = Request.QueryString["FAppId"];

            //查出所有安排的项目经理（FDataFrom=1：表示是变变化后的人，反之为首次安排的人员）
            IQueryable<CF_Prj_Emp> allEmp = db.CF_Prj_Emp.Where(t => t.FAppId == FAppId && t.FType == 1);

            //查询首次项目经理
            CF_Prj_Emp oldFZR = allEmp.Where(t => t.FType == 1 && t.FDataFrom.GetValueOrDefault() != 1).FirstOrDefault();
            //查出变更后的项目经理
            CF_Prj_Emp newFZR = allEmp.Where(t => t.FType == 1 && t.FDataFrom.GetValueOrDefault() == 1).FirstOrDefault();
            if (oldFZR != null)
            {
                //比较项目负责人是否和首次有变化（有变化时变为删除，没有时恢复）
                oldFZR.FIsDeleted = oldFZR.FEmpBaseInfo != emp_FEmpBaseInfo.Value;

                if (oldFZR.FIsDeleted.GetValueOrDefault())//有变化
                {
                    if (newFZR == null)
                    {
                        newFZR = new CF_Prj_Emp();
                        db.CF_Prj_Emp.InsertOnSubmit(newFZR);
                        newFZR.FId = Guid.NewGuid().ToString();
                        newFZR.FDataFrom = 1;//变更后的人员标记
                        newFZR.FCreateTime = DateTime.Now;
                        newFZR.FIsDeleted = false;
                        newFZR.FType = 1;//负责人 
                        newFZR.FAppId = FAppId;
                    }
                    newFZR = tool.getPageValue(newFZR);

                    lit_oldEmp.Text = "首次安排的项目经理：<tt>" + oldFZR.FName + "</tt><br />";
                }
                else //没变化
                {
                    oldFZR = tool.getPageValue(oldFZR);
                    if (newFZR != null)
                    {//如果有变更后的，变回去时要删除之后的
                        db.CF_Prj_Emp.DeleteOnSubmit(newFZR);
                    }
                    lit_oldEmp.Text = "";
                }
            }

            db.SubmitChanges();
            tool.showMessageAndRunFunction("操作成功！", "window.returnValue=1;");
        }
        else
            saveInfo(false);
    }

    //提交
    protected void btnReport_Click(object sender, EventArgs e)
    {
        saveInfo(true);
        //Report();
    }

    #endregion
    protected void EmpList2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "doSave")
        {
            string fid = e.CommandArgument.ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("update cf_Prj_Emp set ");
            sb.Append("FFunction='" + h_FFunction.Value + "' ");
            sb.Append("where FId='" + fid + "'");
            RCenter rc = new RCenter();
            rc.PExcute(sb.ToString());
            pageTool tool = new pageTool(this.Page);
            tool.showMessage("保存成功！");
        }
    }
}
