using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Approve.Common;
using ProjectData;
public partial class JSDW_ApplyJGYS_ProjectFile_SGXKZFormAdd : System.Web.UI.Page
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
                string sql = string.Format("select top 1 * from XM_SGXKZSGCYRY where ID='{0}' ",Id);
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                    ddlAQSCGLRYLX.SelectedValue = row["AQSCGLRYLX"].ToString();
                    ddlZJLX.SelectedValue = row["ZJLX"].ToString();
                    hfId.Value = row["ID"].ToString();
                }
            }
            EnabledControl();
        }
    }
    private void EnabledControl()
    {
        if (IsShow == "1" || FIsApprove == "1") //审核页面跳转
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
        var RYLX = db.getDicList(112203);
        ddlZJLX.DataSource = RYLX;
        ddlZJLX.DataTextField = "fname";
        ddlZJLX.DataValueField = "fnumber";
        ddlZJLX.DataBind();
        ddlZJLX.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = string.Empty;
        List<SqlParameter> list = new List<SqlParameter>();
        string SGId = string.Empty;
        if (string.IsNullOrEmpty(hfId.Value))//新增
        {
            SGId = Guid.NewGuid().ToString();
            sql = string.Format(@"INSERT INTO [dbCenter].[dbo].[XM_SGXKZSGCYRY]
                                       ([ID]
                                       ,[SGXKZID]
                                       ,[RYXM]
                                       ,[ZJLX]
                                       ,[ZJLXMC]
                                       ,[ZJHM]
                                       ,[AQSCKHHGZBH]
                                       ,[AQSCGLRYLX]
                                       ,[AQSCGLRYLXMC])
                                 VALUES(@ID,@SGXKZID,@RYXM,@ZJLX,@ZJLXMC,@ZJHM,@AQSCKHHGZBH,@AQSCGLRYLX,@AQSCGLRYLXMC)");

            list.Add(new SqlParameter() { ParameterName = "@ID", Value = SGId, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SGXKZID", Value = SGXKZID, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@RYXM", Value = txtRYXM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZJLX", Value = ddlZJLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZJLXMC", Value = ddlZJLX.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZJHM", Value = txtZJHM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@AQSCKHHGZBH", Value = txtAQSCKHHGZBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@AQSCGLRYLX", Value = ddlAQSCGLRYLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@AQSCGLRYLXMC", Value = ddlAQSCGLRYLX.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
        }
        else {
            sql = string.Format(@"UPDATE [dbCenter].[dbo].[XM_SGXKZSGCYRY]
                                   SET [RYXM] = @RYXM
                                      ,[ZJLX] = @ZJLX
                                      ,[ZJLXMC] = @ZJLXMC
                                      ,[ZJHM] = @ZJHM
                                      ,[AQSCKHHGZBH] = @AQSCKHHGZBH
                                      ,[AQSCGLRYLX] = @AQSCGLRYLX
                                      ,[AQSCGLRYLXMC] = @AQSCGLRYLXMC
                                 WHERE ID=@ID");
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = Id, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@RYXM", Value = txtRYXM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZJLX", Value = ddlZJLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZJLXMC", Value = ddlZJLX.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZJHM", Value = txtZJHM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@AQSCKHHGZBH", Value = txtAQSCKHHGZBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@AQSCGLRYLX", Value = ddlAQSCGLRYLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@AQSCGLRYLXMC", Value = ddlAQSCGLRYLX.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
            SGId = Id;
        }
        hfId.Value = SGId;
        pageTool tool = new pageTool(this.Page,"txt");
       bool success = rc.PExcute(sql, list.ToArray());
       if (success)
           tool.showMessage("操作成功");
       else
           tool.showMessage("操作失败");

    }
    private string Id {
        get {
            return Request.QueryString["ID"];
        }
    }
    private string SGXKZID {
        get {
            return Request.QueryString["SGXKZID"];
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
}