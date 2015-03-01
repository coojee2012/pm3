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

public partial class JSDW_applycbsjwt_PlanPerson : Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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

            f_FBeginTime.Enabled = false;
            f_FEndTime.Enabled = false;
        }

        t_FAppDate.Text = EConvert.ToShortDateString(DateTime.Now);
    }


    //显示
    private void showInfo()
    {
        string FAppId = Request.QueryString["FAppId"];

        var v = (from t in db.CF_App_List
                 where t.FId == FAppId
                 select new
                 {
                     t.FState,
                     t.FAppDate,
                     t.FId,
                     t.FLinkId
                 }).FirstOrDefault();
        if (v != null)
        {
            hidd_FDataID.Value = v.FLinkId;
            if (v.FState == 6 && Request.QueryString["c"] != "1")
            {
                t_FAppDate.Text = EConvert.ToShortDateString(v.FAppDate);
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("btnEnable();");
            }

            txtFPrjName.Text = db.CF_Prj_Data.Where(t => t.FId == v.FLinkId).Select(t => t.FPrjName).FirstOrDefault();

            //显示负责人信息
            var fzr = db.CF_Prj_Emp.Where(t => t.FAppId == FAppId && t.FType == 1 && !t.FIsDeleted.GetValueOrDefault()).FirstOrDefault();
            if (fzr != null)
            {
                pageTool tool = new pageTool(this.Page, "f_");
                tool.fillPageControl(fzr);

                if (fzr.FDataFrom == 1)
                { //如果是变更后的人，则显示首次经理是谁
                    string oldFZR = db.CF_Prj_Emp.Where(t => t.FAppId == FAppId && t.FType == 1 && t.FIsDeleted.GetValueOrDefault()).Select(t => t.FName).FirstOrDefault();
                    lit_oldEmp.Text = "首次安排的项目经理：<tt>" + oldFZR + "</tt><br />";
                }
            }

            //显示人员列表
            ShowEmpInfo();

            //显示受理时的信息
            showSlInfo(v.FLinkId);
        }
    }

    #region 人员列表

    //显示人员列表
    private void ShowEmpInfo()
    {
        string fAppId = Request.QueryString["FAppId"];
        var v = (from t in db.CF_Prj_Emp
                 where (t.FAppId == fAppId && t.FType == 2)
                 orderby t.FCreateTime descending
                 select new
                 {
                     t.FId,
                     t.FIsDeleted,
                     t.FDataFrom,
                     t.FEmpBaseInfo,
                     t.FName,
                     t.FMajor,
                     t.FFunction
                 }).ToList();

        //查出是否有变更（有删除、有新增）
        if (v.Count(t => t.FDataFrom == 1 || t.FIsDeleted.GetValueOrDefault()) > 0)
        {
            tr_oldEmp.Visible = true;
            string s = "";
            foreach (var t in v.Where(t => t.FDataFrom.GetValueOrDefault() != 1))
            {
                s += (!string.IsNullOrEmpty(s) ? "、" : "") + "<tt>" + t.FName + "</tt>";
            }

            lit_oldEmpList.Text = "首次安排的人员：" + s;
        }
        else
            tr_oldEmp.Visible = false;

        //最后显示的是没删除的
        v = v.Where(t => !t.FIsDeleted.GetValueOrDefault()).ToList();

        Pager1.RecordCount = v.Count();
        dg_List.DataKeyField = "FEmpBaseInfo";
        dg_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }
    //人员列表
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
        }
    }
    //保存
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            if (e.CommandName == "Save")
            {
                string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
                TextBox box = e.Item.Cells[e.Item.Cells.Count - 3].Controls[1] as TextBox;
                if (box != null)
                {
                    string sql = "update cf_Prj_Emp set FFunction='" + box.Text.Trim() + "' where fid='" + fid + "'";
                    rc.PExcute(sql);
                    pageTool tool = new pageTool(this.Page);
                    tool.showMessage("保存成功！");
                    ShowEmpInfo();
                }
            }
        }
    }
    //删除人员
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);

        //保存选择的人员列表
        for (int i = 0; i < dg_List.Items.Count; i++)
        {
            string FID = dg_List.Items[i].Cells[dg_List.Columns.Count - 1].Text;
            //已选中的
            if (((CheckBox)dg_List.Items[i].FindControl("CheckItem")).Checked)
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
        ShowEmpInfo();
    }
    //刷新人员列表
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowEmpInfo();
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowEmpInfo();
    }

    #endregion

    /// <summary>
    /// 显示项目受理情况
    /// </summary>
    private void showSlInfo(string FLinkId)
    {
        //从初步设计合同备案业务查受理情况 
        var v = (from a in db.CF_App_List
                 join d in db.CF_Prj_Data on a.FLinkId equals d.FId
                 where a.FLinkId == FLinkId && a.FManageTypeId == 291 && a.FState == 6
                 select new
                 {
                     d.FDate2,
                     d.FTxt16,
                     d.FTxt19,
                 }).FirstOrDefault();
        if (v != null)
        {
            s_FAppIdea.Text = v.FTxt19;
            s_FAppDate.Text = EConvert.ToShortDateString(v.FDate2);
            s_FTxt16.Text = v.FTxt16;
        }
    }

    //选择人员
    protected void btnSel_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        if (btn.CommandName == "SJ")//项目负责人
        {
            var App = (from b in db.CF_Emp_BaseInfo
                       where b.FId == f_FEmpBaseInfo.Value
                       select new
                       {
                           b.FName,
                           FMajor = b.FRegistSpecialId,
                           b.FCertiNo
                       }).FirstOrDefault();
            if (App != null)
            {
                pageTool tool = new pageTool(this.Page, "f_");
                tool.fillPageControl(App);
            }
        }
        else if (btn.CommandName == "ZC")//注册人员
        {
            var App = (from b in db.CF_Emp_BaseInfo
                       where b.FId == f_FEmpBaseInfo.Value
                       select new
                       {
                           b.FName,
                           FMajor = b.FRegistSpecialId,
                           b.FCertiNo
                       }).FirstOrDefault();
            if (App != null)
            {
                pageTool tool = new pageTool(this.Page, "f_");
                tool.fillPageControl(App);
            }
            ShowEmpInfo();
        }
    }



    //保存或提交
    private void SaveInfo(int FState)
    {
        //安排设计人员不能为空
        if (this.dg_List.Items.Count < 1)
        {
            RegisterStartupScript("key", "<script>alert('请安排人员')</script>");
            return;
        }
        pageTool tool = new pageTool(this.Page, "f_");
        //保存负责人信息
        string FAppId = Request.QueryString["FAppId"];

        CF_Prj_Emp emp = db.CF_Prj_Emp.Where(t => t.FAppId == FAppId && t.FType == 1).FirstOrDefault();
        if (emp == null)
        {
            emp = new CF_Prj_Emp();
            db.CF_Prj_Emp.InsertOnSubmit(emp);

            emp.FId = Guid.NewGuid().ToString();
            f_FId.Value = emp.FId;
            emp.FCreateTime = DateTime.Now;
            emp.FIsDeleted = false;
            emp.FType = 1;//负责人 
            emp.FAppId = FAppId;
        }
        emp = tool.getPageValue(emp);

        //保存选择的人员列表
        for (int i = 0; i < dg_List.Items.Count; i++)
        {
            string FID = dg_List.Items[i].Cells[dg_List.Columns.Count - 1].Text;
            string FFunction = ((TextBox)dg_List.Items[i].FindControl("t_FFunction")).Text;

            CF_Prj_Emp v = db.CF_Prj_Emp.Where(t => t.FId == FID).FirstOrDefault();
            if (v != null)
            {
                v.FFunction = FFunction;
            }
        }

        if (FState == 6)
        { //提交，办结
            CF_App_List app = db.CF_App_List.Where(t => t.FId == FAppId).FirstOrDefault();
            if (app == null)
            {
                tool.showMessage("该业务数据有误，请重新申请！");
            }
            else
            {
                app.FState = FState;
                app.FReportDate = app.FAppDate = EConvert.ToDateTime(t_FAppDate.Text);
                //办完“初步设计人员安排”后自动创建下一个“初步设计人员意见29202”
                if (!CreateNewApp(app.FPrjId, app.FLinkId, txtFPrjName.Text, 29202))
                {
                    tool.showMessage("创建下一个业务失败！业务已存在。");
                    return;
                }
                //创建见证负责人人员消息
                //发送系统消息
                SMS sms = new SMS();
                sms.SendMessage(f_FEmpBaseInfo.Value, "“" + CurrentEntUser.EntName + "”给您安排了“" + txtFPrjName.Text + "”工程的“项目负责人”职责，请及时处理。");
                //创建一般人员消息 
                for (int i = 0; i < dg_List.Items.Count; i++)
                {
                    string empId = dg_List.DataKeys[i].ToString();
                    sms.SendMessage(empId, "“" + CurrentEntUser.EntName + "”给您安排了“" + txtFPrjName.Text + "”工程的“初步设计人员”职责，请及时处理。");
                }
            }
        }

        //保存 
        db.SubmitChanges();
        tool.showMessageAndRunFunction("操作成功！", "window.returnValue=1;");
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["c"] == "1")
        {//如果是变更。 做以下保存操作
            if (this.dg_List.Items.Count < 1)
            {
                RegisterStartupScript("key", "<script>alert('请安排人员')</script>");
                return;
            }
            pageTool tool = new pageTool(this.Page, "f_");
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
                oldFZR.FIsDeleted = oldFZR.FEmpBaseInfo != f_FEmpBaseInfo.Value;

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
                        f_FId.Value = newFZR.FId;//必加
                    }
                    newFZR = tool.getPageValue(newFZR);

                    lit_oldEmp.Text = "首次安排的项目经理：<tt>" + oldFZR.FName + "</tt><br />";
                }
                else //没变化
                {
                    f_FId.Value = oldFZR.FId;//必加
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
            SaveInfo(0);

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        SaveInfo(6);
    }
}
