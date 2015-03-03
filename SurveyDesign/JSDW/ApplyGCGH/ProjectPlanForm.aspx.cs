using Approve.Common;
using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ProjectData;

public partial class JSDW_ApplyGCGH_ProjectPlanForm : System.Web.UI.Page
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
                string sql = @"select top 1 * from [YW_GCGH] where ID='" + Id + "'";
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                    ddlSFSW.SelectedValue = row["SFSW"].ToString();
                    ddlJGLX.SelectedValue = row["JGLX"].ToString();
                    ddlYDXZ.SelectedValue = row["YDXZ"].ToString();
                    ucProjectPlace.fNumber = row["XMSD"].ToString();
                    cbGYTDSYZ.Checked = Convert.ToBoolean(row["GYTDSYZ"] == DBNull.Value ? false : row["GYTDSYZ"]);
                    cbQT.Checked = Convert.ToBoolean(row["QT"] == DBNull.Value ? false : row["QT"]);
                    cbTDHBPZWJ.Checked = Convert.ToBoolean(row["TDHBPZWJ"] == DBNull.Value ? false : row["TDHBPZWJ"]);
                    cbTDSYCRHT.Checked = Convert.ToBoolean(row["TDSYCRHT"] == DBNull.Value ? false : row["TDSYCRHT"]);
                }
            }
            EnabledControl();
        }
    }
    private void BindControl()
    {
        //用地性质
        var YDXZ = db.getDicList(500);
        ddlYDXZ.DataSource = YDXZ;
        ddlYDXZ.DataTextField = "fname";
        ddlYDXZ.DataValueField = "fnumber";
        ddlYDXZ.DataBind();
        ddlYDXZ.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });

        var JGLX = db.getDicList(509);
        ddlJGLX.DataSource = JGLX;
        ddlJGLX.DataTextField = "fname";
        ddlJGLX.DataValueField = "fnumber";
        ddlJGLX.DataBind();
        ddlJGLX.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });
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
            txtJSDWMC.Text = row["JSDW"].ToString();
            txtJSDZ.Text = row["XMDZ"].ToString();

            txtJZZMJ.Text = row["JZZMJ"].ToString();
           // txtZJZMJDS.Text = row["DSJZMJ"].ToString();
           // txtZJZMJDX.Text = row["DXJZMJ"].ToString();
            //txtJZCS.Text = row["JZZCS"].ToString();
            txtCSDS.Text = row["DSCS"].ToString();
            txtCSDX.Text = row["DXCS"].ToString();
            txtCSGD.Text = row["JZGD"].ToString();
            ddlSFSW.SelectedValue = string.IsNullOrEmpty(row["XMSFSW"].ToString()) ? "-1" : row["XMSFSW"].ToString();
            ucProjectPlace.fNumber = row["XMSD"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        StringBuilder _builder = new StringBuilder();
        _builder.Append(@"UPDATE [dbCenter].[dbo].[YW_GCGH]
                                   SET [JSDWMC] = @JSDWMC
                                      ,[JSDWDZ] = @JSDWDZ
                                      ,[LXR] = @LXR
                                      ,[LXDH] = @LXDH
                                      ,[XMMC] = @XMMC
                                      ,[SFSW] = @SFSW
                                      ,[BH] = @BH
                                      ,[LXWH] = @LXWH
                                      ,[LXSJ] = @LXSJ
                                      ,[XMSD] = @XMSD
                                      ,[XMSDMC] = @XMSDMC
                                      ,[JSDZ] = @JSDZ
                                      ,[XMNR] = @XMNR
                                      ,[GYTDSYZ] = @GYTDSYZ
                                      ,[TDSYCRHT] = @TDSYCRHT
                                      ,[TDHBPZWJ] = @TDHBPZWJ
                                      ,[QT] = @QT
                                      ,[JZWMC] = @JZWMC
                                      ,[JGLX] = @JGLX
                                      ,[CSDS] = @CSDS
                                      ,[CSDX] = @CSDX
                                      ,[CSGD] = @CSGD
                                      ,[DS] = @DS
                                      ,[JZDCMJ] = @JZDCMJ
                                      ,[JZZMJ] = @JZZMJ
                                      ,[FJMC] = @FJMC
                                      ,[BZ] = @BZ
                                      ,[YDXZ] = @YDXZ
                                      ,[YDXZMC] = @YDXZMC
                                      ,[JSGM] = @JSGM
                                      ,[IsAudit] = 1
                                 WHERE ID=@ID");
        List<SqlParameter> list = new List<SqlParameter>();
        list.Add(new SqlParameter() { ParameterName = "@JSDWMC", Value = txtJSDWMC.Text,SqlDbType = SqlDbType.VarChar});
        list.Add(new SqlParameter() { ParameterName = "@JSDWDZ", Value = txtJSDWDZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXR", Value = txtLXR.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXDH", Value = txtLXDH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMMC", Value = txtXMMC.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@SFSW", Value = ddlSFSW.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@BH", Value = txtBH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXWH", Value = txtLXWH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXSJ", Value = txtLXSJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMSD", Value = ucProjectPlace.fNumber, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMSDMC", Value = ucProjectPlace.DeptFullName, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSDZ", Value = txtJSDZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMNR", Value = txtXMNR.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@GYTDSYZ", Value = cbGYTDSYZ.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@TDSYCRHT", Value = cbTDSYCRHT.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@TDHBPZWJ", Value = cbTDHBPZWJ.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@QT", Value = cbQT.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@JZWMC", Value = txtJZWMC.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JGLX", Value = ddlJGLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@CSDS", Value = txtCSDS.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@CSDX", Value = txtCSDX.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@CSGD", Value = txtCSGD.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@DS", Value = txtDS.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JZDCMJ", Value = txtJZDCMJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JZZMJ", Value = txtJZZMJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@FJMC", Value = txtFJMC.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@BZ", Value = txtBZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@YDXZ", Value = ddlYDXZ.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@YDXZMC", Value = ddlYDXZ.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSGM", Value = txtJSGM.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ID", Value = Id, SqlDbType = SqlDbType.VarChar });
        pageTool tool = new pageTool(this.Page);
        bool success = rc.PExcute(_builder.ToString(),list.ToArray());
        if (success)
            tool.showMessage("保存成功");
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
            return Request.QueryString["GC_Id"];
        }
    }
    private string Audit {
        get {
            return Request.QueryString["audit"];
        }
    }
}