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

public partial class JSDW_ApplyYDGH_LandPlanForm : System.Web.UI.Page
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
                string sql = @"select top 1 * from [YW_YDGH] where ID='" + Id + "'";
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                    ddlYDXZ.SelectedValue = row["YDXZ"].ToString();
                    ddlJZXZ.SelectedValue = row["JZXZ"].ToString();
                    ucProjectPlace.fNumber = row["XMSD"].ToString();
                    cbDT.Checked = Convert.ToBoolean(row["DT"] == DBNull.Value ? false : row["DT"]);
                    cbGX.Checked = Convert.ToBoolean(row["GX"] == DBNull.Value ? false : row["GX"]);
                    cbHD.Checked = Convert.ToBoolean(row["HD"] == DBNull.Value ? false : row["HD"]);
                    cbKK.Checked = Convert.ToBoolean(row["KK"] == DBNull.Value ? false : row["KK"]);
                    cbQLSD.Checked = Convert.ToBoolean(row["QLSD"] == DBNull.Value ? false : row["QLSD"]);
                    cbQT.Checked = Convert.ToBoolean(row["QT"] == DBNull.Value ? false : row["QT"]);
                    cbZHSZ.Checked = Convert.ToBoolean(row["ZHSZ"] == DBNull.Value ? false : row["ZHSZ"]);
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

        var JZXZ = db.getDicList(305);
        ddlJZXZ.DataSource = JZXZ;
        ddlJZXZ.DataTextField = "fname";
        ddlJZXZ.DataValueField = "fnumber";
        ddlJZXZ.DataBind();
        ddlJZXZ.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });
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
            ucProjectPlace.fNumber = row["XMSD"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        StringBuilder _builder = new StringBuilder();
        _builder.Append(@"UPDATE [dbCenter].[dbo].[YW_YDGH]
                                   SET [JSDWMC] = @JSDWMC
                                      ,[JSDWDZ] = @JSDWDZ
                                      ,[LXR] = @LXR
                                      ,[LXDH] = @LXDH
                                      ,[XMMC] = @XMMC
                                      ,[JSDZ] = @JSDZ
                                      ,[XMSD] = @XMSD
                                      ,[XMSDMC] = @XMSDMC
                                      ,[YDMJ] = @YDMJ
                                      ,[JZXZ] = @JZXZ
                                      ,[LXWH] = @LXWH
                                      ,[LXSJ] = @LXSJ
                                      ,[XMNR] = @XMNR
                                      ,ZHSZ =@ZHSZ
	                                  ,DT =@DT
	                                  ,QLSD =@QLSD
	                                  ,HD =@HD
	                                  ,GX =@GX
	                                  ,KK =@KK
	                                  ,QT =@QT
                                      ,[OtherRemark] = @OtherRemark
                                      ,[FJMC] = @FJMC
                                      ,[BZ] = @BZ
                                      ,[BH] = @BH
                                      ,[YDXZ] = @YDXZ
                                      ,[YDXZMC] = @YDXZMC
                                      ,[JSGM] = @JSGM
                                      ,[IsAudit] = 1
                                 WHERE ID=@ID");
        List<SqlParameter> list = new List<SqlParameter>();
        list.Add(new SqlParameter() { ParameterName = "@JSDWMC", Value = txtJSDWMC.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSDWDZ", Value = txtJSDWDZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXR", Value = txtLXR.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXDH", Value = txtLXDH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMMC", Value = txtXMMC.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSDZ", Value = txtJSDZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMSD", Value = ucProjectPlace.fNumber, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMSDMC", Value = ucProjectPlace.DeptFullName, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@YDMJ", Value = txtYDMJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JZXZ", Value = ddlJZXZ.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXWH", Value = txtLXWH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXSJ", Value = txtLXSJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMNR", Value = txtXMNR.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZHSZ", Value = cbZHSZ.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@DT", Value = cbDT.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@QLSD", Value = cbQLSD.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@HD", Value = cbHD.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@GX", Value = cbGX.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@KK", Value = cbKK.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@QT", Value = cbQT.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@OtherRemark", Value = txtOtherRemark.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@FJMC", Value = txtFJMC.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@BZ", Value = txtBZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@BH", Value = txtBH.Text, SqlDbType = SqlDbType.VarChar });
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
            return Request.QueryString["YD_Id"];
        }
    }
    private string Audit {
        get {
            return Request.QueryString["audit"];
        }
    }
}