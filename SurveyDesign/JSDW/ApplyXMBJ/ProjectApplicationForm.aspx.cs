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
using ProjectData;
using System.Data.SqlClient;

public partial class JSDW_ApplyXMBJ_ProjectApplicationForm : System.Web.UI.Page
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
                string sql = @"select top 1 * from [YW_XMBJ] where ID='" + Id + "'";
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                    hfZTZ.Value = row["ZTZ"].ToString();
                    ucProjectPlace.fNumber = row["XMSD"].ToString();
                    cbGWY.Checked = Convert.ToBoolean(row["GWY"] == DBNull.Value ? false : row["GWY"]);
                    cbSHENG.Checked = Convert.ToBoolean(row["SHENG"] == DBNull.Value ? false : row["SHENG"]);
                    cbSHI.Checked = Convert.ToBoolean(row["SHI"] == DBNull.Value ? false : row["SHI"]);
                    cbQU.Checked = Convert.ToBoolean(row["QU"] == DBNull.Value ? false : row["QU"]);
                    ddlFBFS.SelectedValue = row["FBFS"].ToString();
                    ddlGCLB.SelectedValue = row["GCLB"].ToString();
                    ddlJGLX.SelectedValue = row["JGLX"].ToString();
                    ddlJZXZ.SelectedValue = row["JZXZ"].ToString();
                    //cbSJMGMS.Checked = Convert.ToBoolean(row["SJMGMS"] == DBNull.Value ? false : row["SJMGMS"]);
                    //cbSJCSZYB.Checked = Convert.ToBoolean(row["SJCSZYB"] == DBNull.Value ? false : row["SJCSZYB"]);
                    //cbSJWWBH.Checked = Convert.ToBoolean(row["SJWWBH"] == DBNull.Value ? false : row["SJWWBH"]);
                    //cbSJWXHXP.Checked = Convert.ToBoolean(row["SJWXHXP"] == DBNull.Value ? false : row["SJWXHXP"]);
                    //cbSW.Checked = Convert.ToBoolean(row["SW"] == DBNull.Value ? false : row["SW"]);
                }
            }
            EnabledControl();
        }
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
    private void BindControl()
    {
        var GCLB = db.getDicList(20001);
        ddlGCLB.DataSource = GCLB;
        ddlGCLB.DataTextField = "fname";
        ddlGCLB.DataValueField = "fnumber";
        ddlGCLB.DataBind();
        ddlGCLB.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });
        //结构类型
        var JGLX = db.getDicList(509);
        ddlJGLX.DataSource = JGLX;
        ddlJGLX.DataTextField = "fname";
        ddlJGLX.DataValueField = "fnumber";
        ddlJGLX.DataBind();
        ddlJGLX.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });
        
        //发包方式
        var FBFS = db.getDicList(112209);
        ddlFBFS.DataSource = FBFS;
        ddlFBFS.DataTextField = "fname";
        ddlFBFS.DataValueField = "fnumber";
        ddlFBFS.DataBind();
        ddlFBFS.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });
        
        //建筑性质
        var JZXZ = db.getDicList("20005");
        ddlJZXZ.DataSource = JZXZ;
        ddlJZXZ.DataTextField = "fname";
        ddlJZXZ.DataValueField = "fnumber";
        ddlJZXZ.DataBind();
        ddlJZXZ.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        StringBuilder _builder = new StringBuilder();
        _builder.Append(@"UPDATE [dbCenter].[dbo].[YW_XMBJ]
                                       SET [XMMC] = @XMMC
                                          ,[BH] = @BH
                                          ,[XMSD] = @XMSD
                                          ,[XMSDMC] = @XMSDMC
                                          ,[TBRQ] = @TBRQ
                                          ,[JSDD] = @JSDD
                                          ,[GCLB] = @GCLB
                                          ,[JGLX] = @JGLX
                                          ,[ZFTZ] = @ZFTZ
                                          ,[ZCTZ] = @ZCTZ
                                          ,[WSTZ] = @WSTZ
                                          ,[DKTZ] = @DKTZ
                                          ,[QTTZ] = @QTTZ
                                          ,[ZTZ] = @ZTZ
                                          ,[YDMJ] = @YDMJ
                                          ,[JZMJ] = @JZMJ
                                          ,[DS] = @DS
                                          ,[CSDS] = @CSDS
                                          ,[CSDX] = @CSDX
                                          ,[ZDGD] = @ZDGD
                                          ,[ZDKD] = @ZDKD
                                          ,[JHKGRQ] = @JHKGRQ
                                          ,[JHJGRQ] = @JHJGRQ
                                          ,[FBFS] = @FBFS
                                          ,[JZXZ] = @JZXZ
                                          ,[XMJSNR] = @XMJSNR
                                          ,[JSGCYDXKZH] = @JSGCYDXKZH
                                          ,[JSGCGHXKZH] = @JSGCGHXKZH
                                          ,[JSDW] = @JSDW
                                          ,[JSDWDZ] = @JSDWDZ
                                          ,[FDDBR] = @FDDBR
                                          ,[JSDWXZ] = @JSDWXZ
                                          ,[LXR] =@LXR
                                          ,[LXDH] = @LXDH
                                          ,[JSDWYHXDZM] = @JSDWYHXDZM
                                          ,[SJZGBM] = @SJZGBM
                                          ,[GWY] =@GWY
                                          ,[SHENG] = @SHENG
                                          ,[SHI] = @SHI
                                          ,[QU] = @QU
                                          ,[LXWJ] = @LXWJ
                                          ,[LXWH] = @LXWH
                                          ,[PZDW] = @PZDW
                                          ,[PZSJ] = @PZSJ
                                          ,[LXPZMJ] = @LXPZMJ
                                          ,[LXPZGM] = @LXPZGM
                                          ,[DNTZ] = @DNTZ
                                          ,[GCCBQQ] = @GCCBQQ
                                          ,[BZ] = @BZ
                                          ,[IsAudit] = 1
                                 WHERE ID=@ID");
        List<SqlParameter> list = new List<SqlParameter>();
        list.Add(new SqlParameter() { ParameterName = "@XMMC", Value = txtXMMC.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@BH", Value = txtBH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMSD", Value = ucProjectPlace.fNumber, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMSDMC", Value = ucProjectPlace.DeptFullName, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@TBRQ", Value = txtTBRQ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSDD", Value = txtJSDD.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@GCLB", Value = ddlGCLB.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JGLX", Value = ddlJGLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZFTZ", Value = txtZFTZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZCTZ", Value = txtZCTZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@WSTZ", Value = txtWSTZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@DKTZ", Value = txtDKTZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@QTTZ", Value = txtQTTZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZTZ", Value = hfZTZ.Value, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@YDMJ", Value = txtYDMJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JZMJ", Value = txtJZMJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@DS", Value = txtDS.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@CSDS", Value = txtCSDS.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@CSDX", Value = txtCSDX.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZDGD", Value = txtZDGD.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZDKD", Value = txtZDKD.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JHKGRQ", Value = txtJHKGRQ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JHJGRQ", Value = txtJHJGRQ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@FBFS", Value = ddlFBFS.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JZXZ", Value = ddlJZXZ.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@XMJSNR", Value = txtXMJSNR.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSGCYDXKZH", Value = txtJSGCYDXKZH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSGCGHXKZH", Value = txtJSGCGHXKZH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSDW", Value = txtJSDW.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSDWDZ", Value = txtJSDWDZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@FDDBR", Value = txtFDDBR.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSDWXZ", Value = txtJSDWXZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXR", Value = txtLXR.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXDH", Value = txtLXDH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@JSDWYHXDZM", Value = txtJSDWYHXDZM.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@SJZGBM", Value = txtSJZGBM.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@GWY", Value = cbGWY.Checked ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@SHENG", Value = cbSHENG.Checked ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@SHI", Value = cbSHI.Checked ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@QU", Value = cbQU.Checked ? 1 : 0, SqlDbType = SqlDbType.Bit });
        list.Add(new SqlParameter() { ParameterName = "@LXWJ", Value = txtLXWJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXWH", Value = txtLXWH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@PZDW", Value = txtPZDW.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@PZSJ", Value = txtPZSJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXPZMJ", Value = txtLXPZMJ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXPZGM", Value = txtLXPZGM.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@DNTZ", Value = txtDNTZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@GCCBQQ", Value = txtGCCBQQ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@BZ", Value = txtBZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ID", Value = Id, SqlDbType = SqlDbType.VarChar });
        pageTool tool = new pageTool(this.Page);
        bool success = rc.PExcute(_builder.ToString(),list.ToArray());
        if (success)
            tool.showMessage("保存成功");
        else
            tool.showMessage("保存失败");
        txtZTZ.Text = hfZTZ.Value;
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
            return Request.QueryString["XM_Id"];
        }
    }
    private string Audit {
        get {
            return Request.QueryString["audit"];
        }
    }
}