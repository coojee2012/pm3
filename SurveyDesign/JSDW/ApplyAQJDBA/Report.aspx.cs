using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using System.Data;
using EgovaBLL;
using EgovaDAO;
using Tools;
using Approve.RuleApp;
using Approve.RuleCenter;

public partial class JSDW_ApplyAQJDBA_Report : System.Web.UI.Page
{
    EgovaDB db = new EgovaDB();
    RCenter rc = new RCenter();
    string fAppId = "";
    WorkFlowApp wfApp = new WorkFlowApp();
    protected  void Page_Load(object sender, EventArgs e)
    {
        fAppId = EConvert.ToString(Session["FAppId"]);
        if (!IsPostBack)
        {
            
            BindControl();
            ShowEntInfo();
            showInfo();
            pageTool tool1 = new pageTool(this.Page);
        }
    }
    //绑定
    private void BindControl()
    {
        //备案部门
        string deptId = ComFunction.GetDefaultDept();
        StringBuilder sb = new StringBuilder();
        sb.Append("select case FLevel when 1 then FFullName when 2 then FName when 3 then FName end FName,");
        sb.Append("FNumber from cf_Sys_ManageDept ");
        sb.Append("where fnumber like '" + deptId + "%' ");
        sb.Append("and fname<>'市属' ");
        sb.Append("order by left(FNumber,4),flevel");
        DataTable dt = rc.GetTable(sb.ToString());
        p_FManageDeptId.DataSource = dt;
        p_FManageDeptId.DataTextField = "FName";
        p_FManageDeptId.DataValueField = "FNumber";
        p_FManageDeptId.DataBind();
    }
    /// <summary>
    /// 显示企业信息
    /// </summary>
    void ShowEntInfo()
    {
       // string fAppId = EConvert.ToString(Session["FAppId"]);
        var tt = from t in db.CF_Ent_BaseInfo
                  join a in db.CF_App_List
                  on t.FId equals a.FBaseinfoId
                  where a.FId == fAppId
                  select new
                  {
                      t.FName,
                      FBaseInfoId = t.FId
                  };
        var ent = tt.FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, "k_");
            tool.fillPageControl(ent);
        }
        TC_AJBA_Record qa = db.TC_AJBA_Record.Where(t => t.FAppId.Equals(fAppId)).FirstOrDefault();
        TC_Prj_Info prjInfo = db.TC_Prj_Info.Where(t => t.FId == qa.FPrjId).FirstOrDefault();
        if (prjInfo.AddressDept != null)
        {
            govd_FRegistDeptId.fNumber = prjInfo.AddressDept;
        }
        else
        {
            govd_FRegistDeptId.fNumber = "51";
        }
        if (prjInfo.AddressDept != null)
        {
            if (prjInfo.AddressDept.Length == 2)
            {
                ddlLevel.Items.Clear();
                ddlLevel.Items.Insert(0, new ListItem("四川省", "51"));
            }
            else if (prjInfo.AddressDept.Length == 4)
            {
                ddlLevel.Items.Clear();
                ddlLevel.Items.Insert(0, new ListItem("四川省", "51"));
                ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(prjInfo.AddressDept)).Select(d => d.FName).FirstOrDefault(), prjInfo.AddressDept));
            }
            else if (prjInfo.AddressDept.Length == 6)
            {
                string sj = prjInfo.AddressDept.Substring(0, 4);

                ddlLevel.Items.Clear();
                ddlLevel.Items.Insert(0, new ListItem("四川省", "51"));
                ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(sj)).Select(d => d.FName).FirstOrDefault(), sj));
                ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(prjInfo.AddressDept)).Select(d => d.FName).FirstOrDefault(), prjInfo.AddressDept));
            }
        }
        else  //如果项目所在地址为空，则默认为四川省
        {
            ddlLevel.Items.Clear();
            ddlLevel.Items.Insert(0, new ListItem("四川省", "51"));
        }
    }
    //显示
    private void showInfo()
    {
       // string fAppId = EConvert.ToString(Session["FAppId"]);
        var app = db.CF_App_List.Where(t => t.FId == fAppId)
            .Select(t => new { t.FName, t.FYear, t.FPrjId, t.FState, t.FUpDeptId }).FirstOrDefault();
        if (app != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(app);
            txtFPrjName.Text = db.TC_AJBA_Record.Where(t => t.FAppId == fAppId).Select(t => t.PrjItemName).FirstOrDefault();
            //已提交不能修改
            if (app.FState == 1 || app.FState == 6)
            {
                govd_FRegistDeptId.fNumber = app.FUpDeptId.ToString();
                if (app.FUpDeptId.ToString().Length == 2)
                {
                    ddlLevel.Items.Clear();
                    ddlLevel.Items.Insert(0, new ListItem("四川省", "51"));
                }
                else if (app.FUpDeptId.ToString().Length == 4)
                {
                    ddlLevel.Items.Clear();
                    ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(app.FUpDeptId.ToString())).Select(d => d.FName).FirstOrDefault(), app.FUpDeptId.ToString()));
                }
                else if (app.FUpDeptId.ToString().Length == 6)
                {
                    ddlLevel.Items.Clear();
                    ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(app.FUpDeptId.ToString())).Select(d => d.FName).FirstOrDefault(), app.FUpDeptId.ToString()));
                }
                tool.ExecuteScript("btnEnable();");
            }
        }
    }
    /// <summary>
    /// 验证附件是否上传
    /// </summary>
    /// <returns></returns>
    bool IsUploadFile(int? FMTypeId, string FAppId)
    {
        //var v = db.CF_Sys_PrjList.Count(t => t.FIsMust == 1
        //    && t.FManageType == FMTypeId
        //    && db.CF_AppPrj_FileOther.Count(o => o.FPrjFileId == t.FId
        //        && o.FAppId == FAppId) < 1) > 0;
        //return v;
        return false;
    }
    //保存--废弃不用了
    private void saveInfo()
    {
        RApp ra = new RApp();
        string dept = ComFunction.GetDefaultDept();
        SMS sms = new SMS();
        pageTool tool = new pageTool(this.Page);
        string fDeptNumber = ComFunction.GetDefaultDept();
        if (fDeptNumber == null || fDeptNumber == "")
        {
            tool.showMessage("系统出错,请配置默认管理部门");
            return;
        }
        if(! wfApp.ValidateReport(fAppId))
        {
            tool.showMessage("该条业务已经上报");
            return;
        }
        DateTime dTime = DateTime.Now;
        //设计端业务
        CF_App_List app = db.CF_App_List.Where(t => t.FId == fAppId).FirstOrDefault();
        SortedList[] sl = new SortedList[1];
        if (app != null)
        {
            //验证必需的附件是否上传
            if (IsUploadFile(app.FManageTypeId, fAppId))
            {
                tool.showMessage("“附件上传”菜单中存在未上传的附件(必需上传的)，请先上传！");
                return;
            }
            //app.FState = 1;
            //app.FReportDate = DateTime.Now;
            //app.FReportCount++;
            //app.FIsDeleted = false;
            //app.FToBaseinfoId = k_FBaseInfoId.Value;
            //app.FBarCode = ra.GetBarCode(dept, "155");

            sl[0] = new SortedList();
            sl[0].Add("FID", app.FId);
            sl[0].Add("FAppId", app.FId);
            sl[0].Add("FBaseInfoId", app.FBaseinfoId);
            sl[0].Add("FManageTypeId", app.FManageTypeId);
            sl[0].Add("FListId", "19301");
            sl[0].Add("FTypeId", "1930100");
            sl[0].Add("FLevelId", "1930100");
            sl[0].Add("FIsPrime", 0);

            //sl.Add("FAppDeptId", row["FAppDeptId"].ToString());
            //sl.Add("FAppDeptName", row["FAppDeptName"].ToString());
            sl[0].Add("FAppTime", DateTime.Now);
            sl[0].Add("FIsNew", 0);
            sl[0].Add("FIsBase", 0);
            sl[0].Add("FIsTemp", 0);
            //  sl[0].Add("FUpDept", p_FManageDeptId.SelectedValue);
            sl[0].Add("FUpDept", govd_FRegistDeptId.fNumber);
            //sl[0].Add("FEmpId", app.FLinkId);//CF_Prj_Data.FID
            //sl[0].Add("FEmpName", app.FPrjName);
            //sl[0].Add("FLeadId", "");//建设单位ID（这里没有）
            //sl[0].Add("FLeadName", app.FTxt1);
            sl[0].Add("FEmpId", app.FLinkId);//CF_Prj_Data.FID
            sl[0].Add("FEmpName", "");
            sl[0].Add("FLeadId", "");//建设单位ID（这里没有）
            sl[0].Add("FLeadName", "");
            StringBuilder sb = new StringBuilder();

            sb.Append("update CF_App_List set FUpDeptId=" + govd_FRegistDeptId.fNumber + ",");
            sb.Append("ftime=getdate() where fid = '" + fAppId + "'");
            rc.PExcute(sb.ToString());

            //  string fsystemid = CurrentEntUser.SystemId;
            string fsystemid = CurrentEntUser.URSystemId; ;

            //if (ra.EntStartProcessKCSJ(app.FBaseinfoId, fAppId, app.FYear.ToString(), DateTime.Now.Month.ToString(), fsystemid, fDeptNumber, govd_FRegistDeptId.fNumber, sl))
            //{
            //    sb.Remove(0, sb.Length);

            //    this.Session["FIsApprove"] = 1;
            //    发送系统消息
            //    sms.SendMessage(app.FToBaseinfoId, "“" + CurrentEntUser.EntName + "”给您发来了“" + txtFPrjName.Text + "”工程的质量监督备案，请及时处理。");
            //    tool.showMessageAndRunFunction("上报成功！", "location.href=location.href");
            //}
            //else
            //{
            //    tool.showMessage("上报失败！");
            //    return;
            //}
            //   WorkFlowApp wfApp = new WorkFlowApp();
            if (wfApp.Report(app.FBaseinfoId, fAppId, app.FYear.ToString(), DateTime.Now.Month.ToString(), fsystemid, fDeptNumber, govd_FRegistDeptId.fNumber, govd_FRegistDeptId.fNumber))
            {
                sb.Remove(0, sb.Length);

                this.Session["FIsApprove"] = 1;
                //发送系统消息
                //  sms.SendMessage(app.FToBaseinfoId, "“" + CurrentEntUser.EntName + "”给您发来了“" + txtFPrjName.Text + "”工程的质量监督备案，请及时处理。");
                tool.showMessageAndRunFunction("上报成功！", "location.href=location.href");
            }
            else
            {
                tool.showMessage("上报失败！");
                return;
            }


        }
        else
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
      //  this.saveInfo();
        this.Report();
    }
    private void Report()
    {
        string fDeptNumber = ComFunction.GetDefaultDept();

        if (!ValidateInfo(fAppId))
        {
            MyPageTool.showMessage("上报失败，请先保存基本信息！",this.Page);
            return;
        }
        if (string.IsNullOrEmpty(fDeptNumber))
        {
           MyPageTool.showMessage("系统出错,请配置默认管理部门",this.Page);
            return;
        }
        if (!WFApp.ValidateReport(fAppId))
        {
            MyPageTool.showMessage("该条业务已经上报或者不符合上报条件，不能继续上报操作",this.Page);
            return;
        }
        if (!ValidateCallVideo(fAppId))
        {
            MyPageTool.showMessage("是否点名处未设置或者设置超过1个点位，不能上报", this.Page);
            return;
        }
        if (!ValidateVideo(fAppId))
        {
            MyPageTool.showMessage("摄像头个数如果未达到规定数量，不能上报", this.Page);
            return;
        }
        string fSystemId = CurrentEntUser.URSystemId;      
        if(string.IsNullOrEmpty(fSystemId))
        {
            MyPageTool.showMessage("系统出错,获取不到当前登录系统的编码", this.Page);
            return;
        }
        string fNumber = ddlLevel.SelectedValue;
        //if (ddlLevel.SelectedValue == "1")//省级
        //{
        //    if (fNumber.Length < 2)
        //    {
        //        MyPageTool.showMessage("上报部门不存在省级", this.Page);
        //        return;
        //    }
        //    fNumber = fNumber.Substring(0, 2);
        //}
        //else if (ddlLevel.SelectedValue == "2")//市级
        //{
        //    if (fNumber.Length < 4)
        //    {
        //        MyPageTool.showMessage("上报部门不存在市级", this.Page);
        //        return;
        //    }
        //    fNumber = fNumber.Substring(0, 4);
        //}
        //else if (ddlLevel.SelectedValue == "3")//县级
        //{
        //    if (fNumber.Length < 6)
        //    {
        //        MyPageTool.showMessage("上报部门不存在县级", this.Page);
        //        return;
        //    }
        //    fNumber = fNumber.Substring(0, 6);
        //}
        
        if (WFApp.Report(fAppId, fSystemId, fDeptNumber, fNumber))
        {
            Session["FIsApprove"] = 1;
            MyPageTool.showMessage("上报成功", this.Page);
            showInfo();
        }
        else
        {
            MyPageTool.showMessage("上报失败", this.Page);
        }
    }
    public static bool ValidateVideo(string fAppId)
    {
        bool flag = false;
        EgovaDB db = new EgovaDB();
        TC_AJBA_Record ar = db.TC_AJBA_Record.Where(t => t.FAppId == fAppId).FirstOrDefault();
        TC_PrjItem_Info prj = db.TC_PrjItem_Info.Where(t => t.FId == ar.FPrjItemId).FirstOrDefault();
        string sql = @" select count(*) from TC_AJBA_Video where FAppId = '{0}' ";
        sql = string.Format(sql, fAppId);
        int count = SConvert.ToInt(db.ExecuteQuery<int>(sql).FirstOrDefault());
        if (prj.PrjItemType == "2000101" || prj.PrjItemType == "2000103")//房屋建筑
        {
            if ((5000 <= prj.Area) && (50000 >= prj.Area))
            {
                if (count < 3)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            else if ((50000 <= prj.Area) && (100000 >= prj.Area))
            {
                if (count < 5)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            else if (100000 <= prj.Area)
            {
                if (count < 6)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
        }
        else if (prj.PrjItemType == "2000102")//市政设施
        {
            if ((10000000 <= prj.Cost) && (100000000 >= prj.Cost))
            {
                if (count < 3)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            else if ((100000000 <= prj.Cost) && (300000000 >= prj.Cost))
            {
                if (count < 4)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            else if (300000000 <= prj.Cost)
            {
                if (count < 5)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
        }
        else
        {
            flag = true;
        }
        return flag;
    }
    public static bool ValidateCallVideo(string fAppId)
    {
        bool flag = false;
        EgovaDB db = new EgovaDB();
        string sql = @" select count(*) from TC_AJBA_Video where FAppId = '{0}' and IsCallPlace = 1 ";
        sql = string.Format(sql, fAppId);
        int count = SConvert.ToInt(db.ExecuteQuery<int>(sql).FirstOrDefault());
        //if (count != 1) {
        if (count < 1)
        {
            flag = false;
        } else {
            flag = true;
        }
        return flag;
    }

    public static bool ValidateInfo(string fAppId)
    {       
        EgovaDB db = new EgovaDB();
        TC_AJBA_Record ar = db.TC_AJBA_Record.FirstOrDefault(t => t.FAppId == fAppId);

        if (ar != null && string.IsNullOrEmpty(ar.FJSDWID))
            return false;
        else
        {

            return true;
        }

    }
}