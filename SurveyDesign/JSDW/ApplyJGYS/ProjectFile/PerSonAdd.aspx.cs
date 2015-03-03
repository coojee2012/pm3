using ProjectData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Approve.RuleCenter;
using Approve.Common;
using System.Data.SqlClient;

public partial class JSDW_ApplyJGYS_ProjectFile_PerSonAdd : System.Web.UI.Page
{
    private ProjectDB db = new ProjectDB();
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            BindControl();
            if (!string.IsNullOrEmpty(Id))
            {
                string sql = string.Format("select top 1* from XM_SGTKCSJMX where ID='{0}'", Id);
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page,"txt");
                    tool.fillPageControl(row);
                    ddlZCLXDJ.SelectedValue = row["ZCLXDJ"].ToString();
                    ddlZJLX.SelectedValue = row["ZJLX"].ToString();
                }
            }
            EnabledControl();
        }
    }
    private void EnabledControl()
    {
        if (IsShow == "1" || FIsApprove=="1") //审核页面跳转
        {
            foreach (Control control in this.form1.Controls)
            {
                WebHelper.SetControlEnabled(control);
            }
        }
    }
    private void BindControl()
    {
        //证件类型
        var ZJLX = db.getDicList(1129);
        ddlZJLX.DataSource = ZJLX;
        ddlZJLX.DataTextField = "fname";
        ddlZJLX.DataValueField = "fnumber";
        ddlZJLX.DataBind();
        ddlZJLX.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });
        //注册类型或等级
        var ZCLXDJ = db.getDicList(117);
        ddlZCLXDJ.DataSource = ZCLXDJ;
        ddlZCLXDJ.DataTextField = "fname";
        ddlZCLXDJ.DataValueField = "fnumber";
        ddlZCLXDJ.DataBind();
        ddlZCLXDJ.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });
        //承担角色
        //var CDJS = db.getDicList(100801);
        //ddlCDJS.DataSource = CDJS;
        //ddlCDJS.DataTextField = "fname";
        //ddlCDJS.DataValueField = "fnumber";
        //ddlCDJS.DataBind();
        //ddlCDJS.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });
    }
    private string Id {
        get {
            return Request.QueryString["ID"];
        }
    }
    private string IsShow {
        get {
            return Request.QueryString["IsShow"];
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"UPDATE [dbCenter].[dbo].[XM_SGTKCSJMX]
                                       SET [SSDWMC] = @SSDWMC
                                          ,[SSDWZZJGDM] = @SSDWZZJGDM
                                          ,[RYXM] = @RYXM
                                          ,[ZJLX] = @ZJLX
                                          ,[ZJLXMC] = @ZJLXMC
                                          ,[ZJHM] = @ZJHM
                                          ,[ZCLXDJ] = @ZCLXDJ
                                          ,[ZCLXDJMC] = @ZCLXDJMC
                                          ,[CDJS] = @CDJS
                                     WHERE ID=@ID");
        List<SqlParameter> list = new List<SqlParameter>();
        list.Add(new SqlParameter() { ParameterName = "@SSDWMC",Value=txtSSDWMC.Text,SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@SSDWZZJGDM", Value = txtSSDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@RYXM", Value = txtRYXM.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZJLX", Value = ddlZJLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZJLXMC", Value = ddlZJLX.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZJHM", Value = txtZJHM.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZCLXDJ", Value = ddlZCLXDJ.SelectedValue, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZCLXDJMC", Value = ddlZCLXDJ.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@CDJS", Value = txtCDJS.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ID", Value = Id, SqlDbType = SqlDbType.VarChar });
        bool success = rc.PExcute(sql, list.ToArray());
        pageTool tool = new pageTool(this.Page,"txt");
        if (success)
            tool.showMessage("操作成功");
        else
            tool.showMessage("操作失败");

    }
}