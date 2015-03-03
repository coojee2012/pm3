using Approve.Common;
using Approve.RuleApp;
using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyYDGH_Report : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private ProjectDB db = new ProjectDB();
    private RApp ra = new RApp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(FIsApprove))
            {
                if (FIsApprove == "1")
                {
                    btnSave.Enabled = false;
                }
            }
            showInfo();
        }
    }
    public void showInfo()
    {
        string guid = fAppId;
        if (!string.IsNullOrEmpty(guid))
        {
            var appList = db.CF_App_List.FirstOrDefault(x => x.FId == guid);
            if (appList != null)
            {
                t_FYear.Text = appList.FYear.ToString();
                t_FName.Text = appList.FName;
            }
            if (!string.IsNullOrEmpty(JG_Id))
            {
                string sql = "select top 1 GCMC,XMSD,XMSDMC,SBBM,IsAudit from YW_JGYS where ID='" + JG_Id + "'";
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    hfIsAudit.Value = row["IsAudit"].ToString();
                    t_GCGHMC.Text = row["GCMC"].ToString();
                   // bindDep(row["XMSD"].ToString(), row["XMSDMC"].ToString());
                    DataTable dt = ra.GetCanReportDeptXM(row["XMSD"].ToString(), "1122", appList.FManageTypeId.Value.ToString(), ComFunction.GetDefaultDept());
                    ddlLevel.DataSource = dt;
                    ddlLevel.DataTextField = "fname";
                    ddlLevel.DataValueField = "fnumber";
                    ddlLevel.DataBind();
                    if (!string.IsNullOrEmpty(row["SBBM"].ToString()))
                        ddlLevel.SelectedValue = row["SBBM"].ToString();
                    //ddlLevel.SelectedValue = row["XMSD"].ToString();
                }
            }
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (hfIsAudit.Value != "1")//验证通过
        {
            tool.showMessage("上报信息未完善，请完善后提交");
            return;
        }
        btnSave.Text = "请稍后...";
        btnSave.Enabled = false;
        string IsVerify = VerifyXMCL();

        if (!string.IsNullOrEmpty(IsVerify))
        {
            if (IsVerify == "0")
                tool.showMessage("“项目环节材料”相关信息还未补填，请完善后上报");
            else
                tool.showMessage("项目环节材料：“" + IsVerify + "”相关信息还未补填，请完善后上报");
            btnSave.Text = "提交";
            btnSave.Enabled = true;
            return;
        }
        string fDeptNumber = ComFunction.GetDefaultDept();
        if (fDeptNumber == null || fDeptNumber == "")
        {
            tool.showMessage("系统出错,请配置默认管理部门");
            btnSave.Enabled = false;
            return;
        }
        DateTime dTime = DateTime.Now;
        //设计端业务
        CF_App_List appList = db.CF_App_List.Where(t => t.FId == fAppId).FirstOrDefault();
        SortedList[] sl = new SortedList[1];
        if (appList != null)
        {
            sl[0] = new SortedList();
            sl[0].Add("FID", appList.FId);
            sl[0].Add("FAppId", appList.FId);
            sl[0].Add("FBaseInfoId", appList.FBaseinfoId);
            sl[0].Add("FManageTypeId", appList.FManageTypeId);
            sl[0].Add("FListId", "1930100");
            sl[0].Add("FTypeId", "1930100");
            sl[0].Add("FLevelId", "1930100");
            sl[0].Add("FIsPrime", 0);
            sl[0].Add("FAppTime", DateTime.Now);
            sl[0].Add("FIsNew", 0);
            sl[0].Add("FIsBase", 0);
            sl[0].Add("FIsTemp", 0);
            sl[0].Add("FUpDept", ddlLevel.SelectedValue);
            sl[0].Add("FEmpId", appList.FLinkId);//CF_Prj_Data.FID
            sl[0].Add("FEmpName", t_GCGHMC.Text.Trim());
            sl[0].Add("FLeadId", "");//建设单位ID（这里没有）
            sl[0].Add("FLeadName", "");
            StringBuilder sb = new StringBuilder();

            sb.Append("update CF_App_List set FUpDeptId=" + ddlLevel.SelectedValue + ",");
            sb.Append("ftime=getdate(),FState=1,FReportDate=getdate(),FIsDeleted=0 where fid = '" + fAppId + "';");
            sb.AppendFormat("update YW_JGYS set SBBM='{0}',SBRQ='{2}' where ID='{1}';", ddlLevel.SelectedValue, JG_Id,DateTime.Now);
            rc.PExcute(sb.ToString());
        }
        else
        {
            btnSave.Enabled = true;
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }
        if (ra.EntStartProcessKCSJ(appList.FBaseinfoId, fAppId, appList.FYear.ToString(), DateTime.Now.Month.ToString(), "1122", fDeptNumber, ddlLevel.SelectedValue, sl))
        {
            Session["FIsApprove"] = 1;
            tool.showMessageAndRunFunction("提交成功！", "location.href=location.href");
        }
        else
            btnSave.Enabled = true;
    }
    private string VerifyXMCL()
    {
        bool IsVerify = true;
        string result = string.Empty;
        string sql = string.Format("select * from XM_JGYS_TRANS where YWBM='{0}'", JG_Id);
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            string typeId = string.Empty;//row["TypeId"].ToString();
            sql = string.Format("select top 1 ID from XM_XZYJS where YWBM='{0}'", JG_Id);
            bool isExists = ValidExists(sql);
            if (!isExists)
            {
                IsVerify = ValidTrans("0", dt);
                if (!IsVerify)
                {
                    result = "选址意见书";
                    return result;
                }
            }

            //if (typeId == "1")
            sql = string.Format("select top 1 ID from XM_YDGH where YWBM='{0}'", JG_Id);
            isExists = ValidExists(sql);
            if (!isExists)
            {
                IsVerify = ValidTrans("1", dt);
                if (!IsVerify)
                {
                    result = "用地规划";
                    return result;
                }
            }
            // if (typeId == "2")
            sql = string.Format("select top 1 ID from XM_GCGH where YWBM='{0}'", JG_Id);
            isExists = ValidExists(sql);
            if (!isExists)
            {
                IsVerify = ValidTrans("2", dt);
                if (!IsVerify)
                {
                    result = "工程规划";
                    return result;
                }
            }
            // if (typeId == "3")
            sql = string.Format("select top 1 ID from XM_ZTB where YWBM='{0}'", JG_Id);
            isExists = ValidExists(sql);
            if (!isExists)
            {
                IsVerify = ValidTrans("3", dt);
                if (!IsVerify)
                {
                    result = "招投标";
                    return result;
                }
            }
            // if (typeId == "4")
            sql = string.Format("select top 1 ID from XM_HTBA where YWBM='{0}'", JG_Id);
            isExists = ValidExists(sql);
            if (!isExists)
            {
                IsVerify = ValidTrans("4", dt);
                if (!IsVerify)
                {
                    result = "合同备案";
                    return result;
                }
            }
            //  if (typeId == "5")
            sql = string.Format("select top 1 ID from XM_SGTSCXX where YWBM='{0}'", JG_Id);
            isExists = ValidExists(sql);
            if (!isExists)
            {
                IsVerify = ValidTrans("5", dt);
                if (!IsVerify)
                {
                    result = "施工图审查信息";
                    return result;
                }
            }
            // if (typeId == "6")
            sql = string.Format("select top 1 ID from XM_SGXKZ where YWBM='{0}'", JG_Id);
            isExists = ValidExists(sql);
            if (!isExists)
            {
                IsVerify = ValidTrans("6", dt);
                if (!IsVerify)
                {
                    result = "施工许可证";
                    return result;
                }
            }

            sql = string.Format("select top 1 ID from XM_ZLAQJDBA where YWBM='{0}'", JG_Id);
            isExists = ValidExists(sql);
            if (!isExists)
            {
                IsVerify = ValidTrans("7", dt);
                if (!IsVerify)
                {
                    result = "质量安全监督备案";
                    return result;
                }
            }
        }
        else
            result = "0";
        return result;
    }
    private bool ValidTrans(string typeId, DataTable dt)
    {
        bool success = true;
        DataRow[] rows = dt.Select(string.Format("TypeId={0}",typeId));
        if (rows.Length > 0)
        {
            DataRow row = rows[0];
            string isTrans = row["IsTrans"].ToString();
            if (isTrans == "0")
                success = false;
        }
        else
            success = false;
        return success;
    }
    private bool ValidExists(string sql)
    {
        bool isExists = true;
        DataTable table = rc.GetTable(sql);
        if (table == null || table.Rows.Count == 0)
        {
            isExists = false;
        }
        return isExists;
    }
    public void readOnly()
    {
        btnSave.Enabled = false;
    }
    public string JG_Id
    {
        get
        {
            return Request.QueryString["JG_Id"];
        }
    }
    private string FIsApprove
    {
        get
        {
            string value = Session["FIsApprove"] == null ? "" : Session["FIsApprove"].ToString();
            if (string.IsNullOrEmpty(value))
                return "0";
            return value;
        }
    }
    private string fAppId
    {
        get
        {
            return Request.QueryString["fAppId"];
        }
    }
}