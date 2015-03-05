using Approve.Common;
using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyJGYS_CheckPutForm : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            if (!string.IsNullOrEmpty(Id))
            {
                string sql = @"select top 1 * from [YW_JGYS] where ID='" + Id + "'";
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                    ucProjectPlace.fNumber = row["XMSD"].ToString();
                    ddlYDXZ.SelectedValue = row["YDXZ"].ToString();
                    ddlJGLX.SelectedValue = row["JGLX"].ToString();
                }
            }
            EnabledControl();
        }
    }
    private void BindControl()
    {
        //结构类型
        var data = db.getDicList(509);
        ddlJGLX.DataSource = data;
        ddlJGLX.DataTextField = "fname";
        ddlJGLX.DataValueField = "fnumber";
        ddlJGLX.DataBind();
        ddlJGLX.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });

        //用地性质
        var YDXZ = db.getDicList(500);
        ddlYDXZ.DataSource = YDXZ;
        ddlYDXZ.DataTextField = "fname";
        ddlYDXZ.DataValueField = "fnumber";
        ddlYDXZ.DataBind();
        ddlYDXZ.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });

        //结构体系
        //var JGTX = db.getDicList(560);
        //ddlJGTX.DataSource = JGTX;
        //ddlJGTX.DataTextField = "fname";
        //ddlJGTX.DataValueField = "fnumber";
        //ddlJGTX.DataBind();
        //ddlJGTX.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });
    }
    private void EnabledControl()
    {
        if (Audit == "1" || FIsApprove == "1")
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
    protected void btnChoose_Click(object sender, EventArgs e)
    {
        string XMBM = hfXMBM.Value;
        string sql = @"SELECT TOP 1 * FROM XM_XMInfo WHERE XMBM='" + XMBM + "'";
        DataTable table = rc.GetTable(sql);
        if (table != null && table.Rows.Count > 0)
        {
            pageTool tool = new pageTool(this.Page, "txt");
            DataRow row = table.Rows[0];

            tool.fillPageControl(row);
            ucProjectPlace.fNumber = row["XMSD"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = @"UPDATE [dbCenter].[dbo].[YW_JGYS]
                        SET [XMSD] = @XMSD
                            ,[XMSDMC] = @XMSDMC
                            ,[GCDD] = @GCDD
                            ,[JGLX] = @JGLX
                            ,[JGLXMC] = @JGLXMC
                            ,[GCZJ] = @GCZJ
                            ,[KGRQ] = @KGRQ
                            ,[JGYSRQ] = @JGYSRQ
                            ,[GHXKZH] = @GHXKZH
                            ,[SGXKZH] = @SGXKZH
                            ,[SGTSJWJSCJG] = @SGTSJWJSCJG
                            ,[SGTSJWJSCPZWH] = @SGTSJWJSCPZWH
                            ,[ZLJDJGMC] = @ZLJDJGMC
                            ,[ZLJDJGZZJGDM] = @ZLJDJGZZJGDM
                            ,[JDDJH] = @JDDJH
                            ,[JSGM] = @JSGM
                            ,[YDXZ] = @YDXZ
                            ,[YDXZMC] = @YDXZMC
                            ,[JSDW] = @JSDW
                            ,[XMFZR] = @XMFZR
                            ,[LXDH] = @LXDH
                            ,[IsAudit] = 1
                            ,[GCDJ] = @GCDJ
                            ,[XFHGYSWJBH] = @XFHGYSWJBH
                            ,[ZZDJ] = @ZZDJ
                            ,[JSDWFR] = @JSDWFR
                        WHERE ID=@ID";
        List<SqlParameter> list = new List<SqlParameter>();
        list.Add(new SqlParameter() { ParameterName = "@ID", Value = Id, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMSD", Value = ucProjectPlace.fNumber, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMSDMC", Value = ucProjectPlace.DeptFullName, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@GCDD", Value = txtGCDD.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JGLX", Value = ddlJGLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JGLXMC", Value = ddlJGLX.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@GCZJ", Value = txtGCZJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@KGRQ", Value = txtKGRQ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JGYSRQ", Value = txtJGYSRQ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@GHXKZH", Value = txtGHXKZH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@SGXKZH", Value = txtSGXKZH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@SGTSJWJSCJG", Value = txtSGTSJWJSCJG.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@SGTSJWJSCPZWH", Value = txtSGTSJWJSCPZWH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZLJDJGMC", Value = txtZLJDJGMC.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZLJDJGZZJGDM", Value = txtZLJDJGZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JDDJH", Value = txtJDDJH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSGM", Value = txtJSGM.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@YDXZ", Value = ddlYDXZ.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@YDXZMC", Value = ddlYDXZ.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSDW", Value = txtJSDW.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMFZR", Value = txtXMFZR.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXDH", Value = txtLXDH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@GCDJ", Value = txtGCDJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XFHGYSWJBH", Value = txtXFHGYSWJBH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZZDJ", Value = txtZZDJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSDWFR", Value = txtJSDWFR.Text, SqlDbType = SqlDbType.VarChar });
        pageTool tool = new pageTool(this.Page);
        bool success = rc.PExcute(sql,list.ToArray());
        if (success)
            tool.showMessageAndRunFunction("保存成功", "window.close();window.returnValue='1';");
        else
            tool.showMessage("保存失败");
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
    private string Id
    {
        get
        {
            return Request.QueryString["JG_Id"];
        }
    }
    private string Audit
    {
        get {
            return Request.QueryString["audit"];
        }
    }
}