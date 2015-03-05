using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Data;
using Approve.Common;
using System.Data.SqlClient;

public partial class JNCLEnt_AppJNCL_YCLJSCGYInfo : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql = @"SELECT TOP 1 [YCLJSCGY] FROM [YW_JNCL_PRODUCT] WHERE YWBM='" + YWBM + "'";
            DataTable table = rc.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                txtYCLJSCGY.Text = row["YCLJSCGY"].ToString();
            }
            EnabledControl();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(YWBM))
        {
            string sql = @"UPDATE [dbo].[YW_JNCL_PRODUCT]
                           SET [YCLJSCGY] = @YCLJSCGY
                          WHERE [YWBM] = @YWBM";
            pageTool tool = new pageTool(this.Page);
            List<SqlParameter> listParam = new List<SqlParameter>();
            listParam.Add(new SqlParameter() { ParameterName = "@YWBM", Value = YWBM, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@YCLJSCGY", Value = txtYCLJSCGY.Text, SqlDbType = SqlDbType.VarChar });
            bool success = rc.PExcute(sql, listParam.ToArray());
            if (success)
                tool.showMessage("保存成功");
            else
                tool.showMessage("保存失败");
        }
    }
    private void EnabledControl()
    {
        if (Audit == "1" || FIsApprove == "1") //审核页面跳转
        {
            foreach (Control control in this.form1.Controls)
            {
                WebHelper.SetControlEnabled(control);
            }
        }
    }
    private string YWBM
    {
        get
        {
            return Request.QueryString["YWBM"];
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
    private string Audit
    {
        get
        {
            return Request.QueryString["audit"];
        }
    }
}