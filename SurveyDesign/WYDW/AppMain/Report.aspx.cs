using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using EgovaBLL;

public partial class WYDW_AppMain_Report : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    private string fappid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FAppId"] != null)
        {
            fappid = (string)Session["FAppId"];
        }
        if (!IsPostBack)
        {
            bindDep();
            binddata();
            CheckKZXX();
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
        }
    }

    private void binddata()
    {
        if (Session["FAppId"] != null)
        {
            string sql = "select * from YW_WY_XM_JBXX where FAppID='" + fappid + "' ";
            DataTable dt = rc.GetTable(sql);
            string strsql = "select * from CF_App_List where FID='" + (string)Session["FAppId"] + "'";
            DataTable dt2 = new DataTable();
            dt2 = rc.GetTable(strsql);
            t_FYear.Text = dt2.Rows[0]["FYear"].ToString();
            t_FName.Text = dt2.Rows[0]["FName"].ToString();
            t_XMMC.Text = dt.Rows[0]["XMMC"].ToString();
            t_DW.Text = dt2.Rows[0]["FBaseName"].ToString();

        }
        else
        {
            this.RegisterStartupScript(new Guid().ToString(), "<script>alert('数据加载失败！');parent.location.href='../main/Index.aspx';</script>");
        }
    }
    public void bindDep()
    {
        string sql = "select * from YW_WY_XM_JBXX where FAppID='" + fappid + "' ";
        DataTable dt3 = rc.GetTable(sql);
        if (dt3.Rows.Count == 1)
        {
            if (dt3 != null && dt3.Rows.Count > 0)
            {
                if (dt3.Rows[0]["XMSD"].ToString() != "")
                {
                    string deptId = dt3.Rows[0]["XMSD"].ToString();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select case FLevel when 1 then FFullName when 2 then FName when 3 then FName end FName,");
                    sb.Append("FNumber from cf_Sys_ManageDept ");
                    sb.Append("where fnumber like '" + deptId + "%' ");
                    sb.Append("and fname<>'市辖区' ");
                    sb.Append("order by left(FNumber,4),flevel");
                    DataTable dt = rc.GetTable(sb.ToString());
                    t_FUpDeptName.DataSource = dt;
                    t_FUpDeptName.DataTextField = "FName";
                    t_FUpDeptName.DataValueField = "FNumber";
                    t_FUpDeptName.DataBind();
                    notices.InnerHtml = "";
                }
                else
                {
                    notices.InnerHtml = "<tt>* 缺少项目基本信息！</tt>";
                    btnSave.Enabled = false;
                }
            }
            else
            {
                notices.InnerHtml = "<tt>* 缺少项目基本信息！</tt>";
                btnSave.Enabled = false;
            }
        }
        else
        {
            notices.InnerHtml = "<tt>* 缺少项目基本信息！</tt>";
            btnSave.Enabled = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Report();
    }

    private void Report()
    {
        string fDeptNumber = ComFunction.GetDefaultDept();
        if (fDeptNumber == null || fDeptNumber == "")
        {
            MyPageTool.showMessage("系统出错,请配置默认管理部门", this.Page);
            return;
        }
        if (!WFApp.ValidateReport(fappid))
        {
            MyPageTool.showMessage("该条业务已经上报或者不符合上报条件，不能继续上报操作", this.Page);
            return;
        }
        string fSystemId = CurrentEntUser.URSystemId;
        if (string.IsNullOrEmpty(fSystemId))
        {
            MyPageTool.showMessage("系统出错,获取不到当前登录系统的编码", this.Page);
            return;
        }
        if (t_FUpDeptName.SelectedValue == "")
        {
            MyPageTool.showMessage("系统出错,没有上报部门", this.Page);
            return;
        }
        string fNumber = t_FUpDeptName.SelectedValue;
        if (WFApp.Report(fappid, fSystemId, fDeptNumber, fNumber))
        {
            Session["FIsApprove"] = 1;
            MyPageTool.showMessage("上报成功", this.Page);
        }
        else
        {
            MyPageTool.showMessage("上报失败", this.Page);
        }
    }

    private void readOnly()
    {
        btnSave.Enabled = false;
    }

    private void CheckKZXX()
    {
        string strsql = "select fid from yw_wy_xm_kzxx where FAppID='" + fappid + "'";
        string fid = rc.GetSignValue(strsql);
        if (fid == "")
        {
            notice2.InnerHtml = "<tt>* 缺少项目扩展信息！</tt>";
            btnSave.Enabled = false;
        }
    }
}