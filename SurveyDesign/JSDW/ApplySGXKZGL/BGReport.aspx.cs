using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using EgovaBLL;
using EgovaDAO;
using Tools;
using Approve.RuleApp;
using Approve.RuleCenter;
using System.Data.SqlClient;

public partial class JSDW_ApplySGXKZGL_BGReport : System.Web.UI.Page
{
    EgovaDB db = new EgovaDB();
    RCenter rc = new RCenter();
    string fAppId = "";
    WorkFlowApp wfApp = new WorkFlowApp();
    protected void Page_Load(object sender, EventArgs e)
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
        TC_SGXKZ_BGPrjInfo qa = db.TC_SGXKZ_BGPrjInfo.Where(t => t.FAppId.Equals(fAppId)).FirstOrDefault();
        TC_SGXKZ_PrjInfo sp = db.TC_SGXKZ_PrjInfo.Where(t => t.PrjId == qa.FPrjInfoId).FirstOrDefault();    //Fid  变成 PrjId by zyd 2015.5.1
        ViewState["PrjItemName"] = sp.PrjItemName;
        //TC_Prj_Info prjInfo = db.TC_Prj_Info.Where(t => t.FId == sp.PrjId).FirstOrDefault();
        govd_FRegistDeptId.fNumber = qa.PrjAddressDept;
        if (qa.PrjAddressDept != null)
        {
            if (qa.PrjAddressDept.Length == 2)
            {
                //ddlLevel.Items.Clear();
                //ddlLevel.Items.Insert(0, new ListItem("四川省", "51"));
            }
            else if (qa.PrjAddressDept.Length == 4)
            {
                ddlLevel.Items.Clear();
                //ddlLevel.Items.Insert(0, new ListItem("四川省", "51"));
                ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(qa.PrjAddressDept)).Select(d => d.FName).FirstOrDefault(), qa.PrjAddressDept));
            }
            else if (qa.PrjAddressDept.Length == 6)
            {
                string sj = qa.PrjAddressDept.Substring(0, 4);

                ddlLevel.Items.Clear();
                //ddlLevel.Items.Insert(0, new ListItem("四川省", "51"));
                ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(sj)).Select(d => d.FName).FirstOrDefault(), sj));
                ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(qa.PrjAddressDept)).Select(d => d.FName).FirstOrDefault(), qa.PrjAddressDept));
            }
        }
        else
        {
            //ddlLevel.Items.Clear();
            //ddlLevel.Items.Insert(0, new ListItem("四川省", "51"));
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
            txtFPrjName.Text = EConvert.ToString(ViewState["PrjItemName"]);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //  this.saveInfo();
        this.Report();
    }


    /// <summary>
    /// 检测上报人员是否在跨地区参与其他项目
    /// </summary>
    /// <returns>true:有人员参与其他地区的项目被锁定，false: 无锁定情况 </returns>
    private string checkpersonlockinfo()
    {
        string lockpersonlist = "";//锁定人员列表，使用","分割
        //获取到当前工程编号 
                         
        string prjitemid = db.TC_SGXKZ_BGPrjInfo.Where(t => t.FAppId == fAppId).FirstOrDefault().FPrjItemId;
        //当前工程所属地
        string prjarea = db.TC_PrjItem_Info.Where(t => t.FId == prjitemid).FirstOrDefault().AddressDept;
        //当前工程增加的人员
        using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            DataSet ds = new DataSet();
            string sql = "";
            sql = @"select  a.FAppId,d.FId FPrjId,a.FPrjItemId,b.ProjectName,c.QYBM FEntId,'' FEntName,c.SFZH FIdCard,c.XM FHumanName  from  TC_SGXKZ_RYBGJG a,TC_PrjItem_Info b,JST_XZSPBaseInfo.dbo.RY_RYJBXX c,TC_Prj_Info d
                    where a.FPrjItemId = b.FId
                    and a.FLinkId = c.RYBH
                    and b.FPrjId = d.FId
                    and a.BGQK = '增加'  and FAppId  = '" + fAppId + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(ds, "ds");
            DataTable dt = ds.Tables[0];

            if (dt != null)
            {
                //先判断不同地区的未竣工的项目当前人员是否参与了
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //判断锁定表中是否存在不同区域的项目当前项目是否参与了的情况，已经上报的项目，c.FState = 1
                    string sql1 = @"select  1  from  TC_PrjItem_Emp a,TC_PrjItem_Info b,CF_App_List c
                                where a.FPrjItemId = b.FId
                                and  c.FId = a.FAppId
                                and  c.FState = 1
                                and  (c.FManageTypeId  = '11223' or c.FManageTypeId  = '11224' or c.FManageTypeId  = '11225')
                                and  a.FIdCard = '" + dt.Rows[i]["FIdCard"].ToString() + "'" +
                                    " and b.AddressDept != '" + prjarea + "'" + " and a.FAppId != '" + fAppId + "'";
                    DataTable dt1 = rc.GetTable(sql1);
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        lockpersonlist += dt.Rows[i]["FHumanName"].ToString() + ",";
                    }
                }
                if (lockpersonlist != "")
                {
                    return lockpersonlist;
                }
                //判断锁定表中是否存在不同区域的项目当前项目是否参与了的情况
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string sql2 = @"select  1  from  TC_PrjItem_Emp_Lock a,TC_PrjItem_Info b
                             where a.FPrjItemId = b.FId
                             and  a.FIdCard = '" + dt.Rows[i]["FIdCard"].ToString() + "'" + "  and b.AddressDept != '" + prjarea + "'   and a.IsLock = '1'";
                    DataTable dt2 = rc.GetTable(sql2);
                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        lockpersonlist += dt.Rows[i]["FHumanName"].ToString() + ",";
                    }
                }
                if (lockpersonlist != "")
                {
                    return lockpersonlist;
                }
            }
            return lockpersonlist;
        }
    }

    private void Report()
    {
        //检查当前工程参与人员是否在其他地方参与项目。
        string slockperson = checkpersonlockinfo();
        if (slockperson != "")
        {
            slockperson = slockperson.Substring(0, slockperson.Length - 1);
            tr_lockrow.Visible = true;
            lbl_lockpsersoncontent.InnerText = slockperson;
            MyPageTool.showMessage("当前工程项目申报有人员被其他项目锁定，请检查人员信息", this.Page);
            return;
        }
        else
        {
            tr_lockrow.Visible = false;
        }
        string fDeptNumber = ComFunction.GetDefaultDept();
        if (fDeptNumber == null || fDeptNumber == "")
        {
            MyPageTool.showMessage("系统出错,请配置默认管理部门", this.Page);
            return;
        }
        if (!WFApp.ValidateReport(fAppId))
        {
            MyPageTool.showMessage("该条业务已经上报或者不符合上报条件，不能继续上报操作", this.Page);
            return;
        }
        //if (!ValidateCallVideo(fAppId))
        //{
        //    MyPageTool.showMessage("是否点名处未设置或者设置超过1个点位，不能上报", this.Page);
        //    return;
        //}
        //if (!ValidateVideo(fAppId))
        //{
        //    MyPageTool.showMessage("摄像头个数如果未达到规定数量，不能上报", this.Page);
        //    return;
        //}
        string fSystemId = CurrentEntUser.URSystemId;
        if (string.IsNullOrEmpty(fSystemId))
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
}