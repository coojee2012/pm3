using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using ProjectData;
using System.Data;
using Approve.Common;
using System.Data.SqlClient;

public partial class JNCLEnt_AppJNCL_ProductInfo : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            string sql = @"SELECT TOP 1* FROM [YW_JNCL_PRODUCT] WHERE YWBM='" + YWBM + "'";
            DataTable table = rc.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                pageTool tool = new pageTool(this.Page, "txt");
                tool.fillPageControl(row);
                ddlBSDJ.SelectedValue = row["BSDJBM"].ToString();
                cbCPLX.SelectedValue = row["CPLBBM"].ToString();
            }
            EnabledControl();
        }
    }
    private void BindControl()
    {
        var BSDJ = db.getDicList(2001302);
        ddlBSDJ.DataSource = BSDJ;
        ddlBSDJ.DataTextField = "fname";
        ddlBSDJ.DataValueField = "fnumber";
        ddlBSDJ.DataBind();
        ddlBSDJ.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });

        var CPLX = db.getDicList(2001303);
        cbCPLX.DataSource = CPLX;
        cbCPLX.DataTextField = "fname";
        cbCPLX.DataValueField = "fnumber";
        cbCPLX.DataBind();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(YWBM))
        {
            string sql = @"UPDATE [dbo].[YW_JNCL_PRODUCT]
                           SET [BSDJBM] = @BSDJBM
                              ,[BSDJMC] = @BSDJMC
                              ,[SQCPMC] = @SQCPMC
                              ,[GGXH] = @GGXH
                              ,[SCNL] = @SCNL
                              ,[CPLBBM] = @CPLBBM
                              ,[CPLBMC] = @CPLBMC
                              ,[CPZXBZ] = @CPZXBZ
                              ,[ZYCZ] = @ZYCZ
                              ,[FZCZ] = @FZCZ
                         WHERE [YWBM] = @YWBM";
            List<SqlParameter> listParam = new List<SqlParameter>();
            listParam.Add(new SqlParameter() { ParameterName = "@BSDJBM", Value = ddlBSDJ.SelectedValue, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@BSDJMC", Value = ddlBSDJ.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@SQCPMC", Value = txtSQCPMC.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@GGXH", Value = txtGGXH.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@SCNL", Value = txtSCNL.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@CPLBBM", Value = cbCPLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@CPLBMC", Value = cbCPLX.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@CPZXBZ", Value = txtCPZXBZ.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@ZYCZ", Value = txtZYCZ.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@FZCZ", Value = txtFZCZ.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@YWBM", Value = YWBM, SqlDbType = SqlDbType.VarChar });
            pageTool tool = new pageTool(this.Page);
            bool success = rc.PExcute(sql, listParam.ToArray());
            if (success)
                tool.showMessage("保存成功");
            else
                tool.showMessage("保存失败");
        }
    }
}