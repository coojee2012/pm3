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

public partial class JSDW_ApplyXZYJSSZ_IDeaBookForm : System.Web.UI.Page
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
                string sql = @"select top 1 * from [YW_XZYJS] where ID='" + Id + "'";
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                    ddlXMSFSW.SelectedValue = row["SFSW"].ToString();
                    ucProjectPlace.fNumber = row["XMSD"].ToString();
                    ddlGG.SelectedValue = row["OtherGG"].ToString();
                    ddlYDXZ.SelectedValue = row["YDXZ"].ToString();
                    txtJSDZ.Text = row["JSDZ"].ToString();
                    cbZHSZ.Checked = Convert.ToBoolean(row["ZHSZ"] == DBNull.Value ? false : row["ZHSZ"]);
                    cbDT.Checked = Convert.ToBoolean(row["DT"] == DBNull.Value ? false : row["DT"]);
                    cbQLSD.Checked = Convert.ToBoolean(row["QLSD"] == DBNull.Value ? false : row["QLSD"]);
                    cbHD.Checked = Convert.ToBoolean(row["HD"] == DBNull.Value ? false : row["HD"]);
                    cbGX.Checked = Convert.ToBoolean(row["GX"] == DBNull.Value ? false : row["GX"]);
                    cbKK.Checked = Convert.ToBoolean(row["KK"] == DBNull.Value ? false : row["KK"]);
                    cbQT.Checked = Convert.ToBoolean(row["QT"] == DBNull.Value ? false : row["QT"]);
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string YJS_ID = Id;
        if (string.IsNullOrEmpty(Id))
            YJS_ID = Guid.NewGuid().ToString();
        pageTool tool = new pageTool(this.Page, "t_");
        StringBuilder _builder = new StringBuilder();
        _builder.Append(@"UPDATE [dbCenter].[dbo].[YW_XZYJS]
                           SET [JSDWMC] = @JSDWMC
                              ,[JSDWDZ] = @JSDWDZ
                              ,[LXR] = @LXR
                              ,[LXDH] = @LXDH
                              ,[XMMC] = @XMMC
                              ,[XMSD] = @XMSD
                              ,XMSDMC = @XMSDMC
                              ,[JSGMMJ] = @JSGMMJ
                              ,[JSGMGD] = @JSGMGD
                              ,[Other] = @Other
                              ,[OtherGG] = @OtherGG
                              ,[JSMJ] = @JSMJ
                              ,[LXWH] = @LXWH
                              ,[LXSJ] = @LXSJ
                              ,[SFSW] = @SFSW
                              ,[BH] = @BH
                              ,[JSYJ] = @JSYJ
                              ,[XMNR] = @XMNR
                              ,ZHSZ =@ZHSZ
	                          ,DT =@DT
	                          ,QLSD =@QLSD
	                          ,HD =@HD
	                          ,GX =@GX
	                          ,KK =@KK
	                          ,QT =@QT
                              ,[FJMC] = @FJMC
                              ,[BZ] = @BZ
                              ,[JSDZ] = @JSDZ
                              ,[YDXZ] = @YDXZ
                              ,[YDXZMC] = @YDXZMC
                              ,[IsAudit] = 1
                         WHERE ID=@ID");
        List<SqlParameter> list = new List<SqlParameter>();
        list.Add(new SqlParameter() { ParameterName = "@JSDWMC", Value = txtJSDWMC.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSDWDZ", Value = txtJSDWDZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXR", Value = txtLXR.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXDH", Value = txtLXDH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMMC", Value = txtXMMC.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMSD", Value = ucProjectPlace.fNumber, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMSDMC", Value = ucProjectPlace.DeptFullName, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSGMMJ", Value = txtJSGMMJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSGMGD", Value = txtJSGMGD.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@Other", Value = txtOther.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@OtherGG", Value = ddlGG.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSMJ", Value = txtJSMJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXWH", Value = txtLXWH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXSJ", Value = txtLXSJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@SFSW", Value = ddlXMSFSW.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@BH", Value = txtBH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSYJ", Value = txtJSYJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMNR", Value = txtXMNR.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZHSZ", Value = cbZHSZ.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@DT", Value = cbDT.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@QLSD", Value = cbQLSD.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@HD", Value = cbHD.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@GX", Value = cbGX.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@KK", Value = cbKK.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@QT", Value = cbQT.Checked == true ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@FJMC", Value = txtFJMC.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@BZ", Value = txtBZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSDZ", Value = txtJSDZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@YDXZ", Value = ddlYDXZ.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@YDXZMC", Value = ddlYDXZ.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ID", Value = Id, SqlDbType = SqlDbType.VarChar });
        bool success = rc.PExcute(_builder.ToString(),list.ToArray());
        if (success)
            tool.showMessage("保存成功");
        else
            tool.showMessage("保存失败");
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
            ddlXMSFSW.SelectedValue = string.IsNullOrEmpty(row["XMSFSW"].ToString()) ? "-1" : row["XMSFSW"].ToString();
            ucProjectPlace.fNumber = row["XMSD"].ToString();
            txtJSDZ.Text = row["XMDZ"].ToString();
            txtJSGMMJ.Text = row["JSGM"].ToString();
            txtJSGMGD.Text = row["JZGD"].ToString();
            txtJSMJ.Text = row["JZZMJ"].ToString();
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
    private string Id
    {
        get
        {
            return Request.QueryString["YJS_GUID"];
        }
    }
    private string Audit {
        get { return Request.QueryString["audit"]; }
    }
}