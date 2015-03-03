using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Data;
using System.Data.SqlClient;
using Approve.Common;
using ProjectData;

public partial class JNCLEnt_AppJNCL_QYJBInfo : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            string sql = @"SELECT TOP 1* FROM YW_JNCL_QYJBXX WHERE YWBM='" + YWBM + "'";
            DataTable table = rc.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                pageTool tool = new pageTool(this.Page,"txt");
                tool.fillPageControl(row);
                ddlJJXZ.SelectedValue = row["JJXZBM"].ToString();
                ucProjectPlace.fNumber = row["QYSZD"].ToString();
            }
            EnabledControl();
        }
    }
    private void BindControl()
    {
        var JJXZ = db.getDicList(2001301);
        ddlJJXZ.DataSource = JJXZ;
        ddlJJXZ.DataTextField = "fname";
        ddlJJXZ.DataValueField = "fnumber";
        ddlJJXZ.DataBind();
        ddlJJXZ.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });
    }
    private void EnabledControl()
    {
        if (Audit == "1" || FIsApprove == "1") //审核页面跳转
        {
            var Province = ucProjectPlace.FindControl("FProvince");
            var city = ucProjectPlace.FindControl("FCity");
            var country = ucProjectPlace.FindControl("FCountry");
            if (Province != null)
                WebHelper.SetControlEnabled(Province);
            if (city != null)
                WebHelper.SetControlEnabled(city);
            if (country != null)
                WebHelper.SetControlEnabled(country);
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
            string sql = @"UPDATE [dbo].[YW_JNCL_QYJBXX]
                       SET [QYMC] = @QYMC
                          ,[QYDZ] = @QYDZ
                          ,[QYSZD] = @QYSZD
                          ,[QYSZDMC] = @QYSZDMC
                          ,[SCDZ] = @SCDZ
                          ,[YYZZH] = @YYZZH
                          ,[ZZJGDM] = @ZZJGDM
                          ,[YZBM] = @YZBM
                          ,[DZYJ] = @DZYJ
                          ,[LXR] = @LXR
                          ,[LXDH] = @LXDH
                          ,[FRDB] = @FRDB
                          ,[SJ] = @SJ
                          ,[CZ] = @CZ
                          ,[JJXZBM] =@JJXZBM
                          ,[JJXZMC] = @JJXZMC
                          ,[ZCZJ] = @ZCZJ
                          ,[QYRS] = @QYRS
                          ,[NSCNL] = @NSCNL
                          ,[LJSCL] = @LJSCL
                     WHERE [YWBM] = @YWBM";
            List<SqlParameter> listParam = new List<SqlParameter>();
            listParam.Add(new SqlParameter() { ParameterName = "@QYMC", Value = txtQYMC.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@QYDZ", Value = txtQYDZ.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@QYSZD", Value = ucProjectPlace.fNumber, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@QYSZDMC", Value = ucProjectPlace.DeptFullName, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@SCDZ", Value = txtSCDZ.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@YYZZH", Value = txtYYZZH.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@ZZJGDM", Value = txtZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@YZBM", Value = txtYZBM.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@DZYJ", Value = txtDZYJ.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@LXR", Value = txtLXR.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@LXDH", Value = txtLXDH.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@FRDB", Value = txtFRDB.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@SJ", Value = txtSJ.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@CZ", Value = txtCZ.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@JJXZBM", Value = ddlJJXZ.SelectedValue, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@JJXZMC", Value = ddlJJXZ.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@ZCZJ", Value = txtZCZJ.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@QYRS", Value = txtQYRS.Text, SqlDbType = SqlDbType.Int });
            listParam.Add(new SqlParameter() { ParameterName = "@NSCNL", Value = txtNSCNL.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@LJSCL", Value = txtLJSCL.Text, SqlDbType = SqlDbType.VarChar });
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