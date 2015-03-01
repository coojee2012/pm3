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

public partial class JZDW_ApplyXMJZ_Report : Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnReport.Attributes["onclick"] = "if (checkInfo()){return confirm('确定已经完毕吗？完毕后将不能再修改。')}else{return false;}";
            showInfo();
        }
    }

    //显示
    private void showInfo()
    {
        string FAppId = Request.QueryString["FAppId"];
        var v = (from t in db.CF_App_List
                 join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                 where t.FId == FAppId
                 select new
                 {
                     t.FId,
                     t.FName,
                     t.FLinkId,
                     t.FBaseName,
                     t.FState,
                     t.FPrjId,
                     d.FPrjName
                 }).FirstOrDefault();
        if (v != null)
        {
            txtFPrjName.Text = v.FPrjName;

            hidd_FDataID.Value = v.FLinkId;//合同备案主线FID
            hidd_FState.Value = v.FState.ToString();//状态 

            //已安排人员列表
            showEmpList();


            //已提交不能修改
            if (v.FState > 1)
            {
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("btnEnable();");
            }
            

            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Where(st => st.FPrjId == v.FPrjId).FirstOrDefault();
            if (stop != null)
            { 
                btnReport.Enabled = false;
                btnReport.ToolTip = "该项目已被中止，所有业务停止进行。";
            }
        }
    }

    //显示人员列表
    private void showEmpList()
    {
        string FAppId = Request.QueryString["FAppId"];
        //从人员安排（29201）业务中取出人员
        var v = (from t in db.CF_Prj_Emp
                 join a in db.CF_App_List on t.FAppId equals a.FId
                 where t.FType == 2 && a.FLinkId == hidd_FDataID.Value && a.FManageTypeId == 29201 && a.FState == 6
                  && !t.FIsDeleted.GetValueOrDefault()
                 select new
                 {
                     t.FId,
                     t.FName,
                     t.FMajor,
                     t.FFunction,
                     iDea = db.CF_App_Idea.Where(i => i.FLinkId == FAppId && i.FUserId == t.FEmpBaseInfo).FirstOrDefault()
                 }).ToList();


        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();

        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }
    //列表
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            string FAppId = Request.QueryString["FAppId"];


            string n = "查看意见";
            string FState = hidd_FState.Value;
            if (FState == "6")
            {
                n = "查看意见";
            }

            string s = "", d = "";
            CF_App_Idea iDea = DataBinder.Eval(e.Item.DataItem, "iDea") as CF_App_Idea;
            if (iDea != null)
            {
                s = iDea.FResult;
                d = iDea.FAppTime.GetValueOrDefault().ToString("yyyy-MM-dd");
            }
            else
            {
                s = "<font color='#888888' name='noidea'>还未填写</font>";
                d = "<font color='#888888'>--</font>";
            }
            e.Item.Cells[4].Text = s;
            e.Item.Cells[5].Text = d;

            string o = "<a href='javascript:showAddWindow(\"AddReport.aspx?FId=" + FId + "&FAppId=" + FAppId + "\",800,600);'>";
            e.Item.Cells[6].Text = o + n + "</a>";
        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showEmpList();
    }


    //保存
    private void saveInfo(int FState)
    {
        string FAppId = Request.QueryString["FAppId"];
        SMS sms = new SMS();
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        CF_App_List app = db.CF_App_List.Where(t => t.FId == FAppId).FirstOrDefault();
        if (app != null)
        {
            if (FState == 6)
            {
                app.FState = FState;
                app.FReportDate = dTime;
                app.FAppDate = dTime;

                //办完“ 人员意见 ”后自动创建下一个业务“成果移交293”
                if (!CreateNewApp(app.FPrjId, app.FLinkId, txtFPrjName.Text, 293))
                {
                    tool.showMessage("创建下一个业务失败！业务已存在。");
                    return;
                }
            }

            db.SubmitChanges();
            tool.showMessageAndRunFunction("操作成功！", "window.returnValue=1;");
            showInfo();

        }
        else
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }
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
        app.FLinkId = FDataFID;//关联受理合同备案时的CF_Prj_Data.FID(合同备案主线表)
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


        //基本信息表
        CF_Prj_Data data = new CF_Prj_Data();
        data.FId = Guid.NewGuid().ToString();
        data.FAppId = app.FId;
        data.FCreateTime = dTime;
        data.FTime = dTime;
        data.FType = app.FManageTypeId;
        data.FPrjName = FPrjName;
        db.CF_Prj_Data.InsertOnSubmit(data);


        return true;
    }

    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo(0);
    }

    //提交
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (DG_List.Items.Count > 0)
        {
            for (int i = 0; i < DG_List.Items.Count; i++)
            {
                if (DG_List.Items[i].Cells[5].Text == "<font color='#888888'>--</font>")
                {
                    RegisterStartupScript("key", "<script>alert('请先填写完整所有设计人员的意见')</script>");
                    return;
                }
            }
        }
        saveInfo(6);
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showEmpList();
    }
}
