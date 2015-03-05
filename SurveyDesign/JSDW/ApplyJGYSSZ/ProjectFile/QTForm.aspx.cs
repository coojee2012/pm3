using Approve.Common;
using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class JSDW_ApplyJGYS_ProjectFile_QTForm : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql = string.Format("select top 1 * from XM_QTZL where YWBM='{0}'", Id);
            DataTable table = rc.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                hfId.Value = row["ID"].ToString();
                txtQTZL.Text = row["QTZL"].ToString();
                txtSFJBSGTJ.Text = row["SFJBSGTJ"].ToString();
                ShowFile(row["ID"].ToString());
            }
            EnabledControl();
        }
    }
    private void EnabledControl()
    {
        if (FIsApprove == "1"||Audit=="1") //审核页面跳转
        {
            foreach (Control control in this.form1.Controls)
            {
                WebHelper.SetControlEnabled(control);
            }
        }
    }
    private string Audit
    {
        get
        {
            return Request.QueryString["audit"];
        }
    }
    private void ShowFile(string YWBM)
    {
        if (!string.IsNullOrEmpty(YWBM))
        {
            string sql = string.Format("select * from YW_FILE_DETAIL where YWBM='{0}'", YWBM);
            DataTable table = rc.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                StringBuilder _builder = new StringBuilder();
                foreach (DataRow row in table.Rows)
                {
                    _builder.Append("<tr>");
                    string url = "../../ApplyXZYJS/DownLoad.aspx?filePath=" + row["DownLoadPath"] + "&fileName=" + row["FILE_NAME"];
                    if (FIsApprove == "1")
                        _builder.AppendFormat("<td>{0}</td><td>{2}</td><td width='100'><a href='{1}' target='_blank'>查看附件</a></td>", row["FILE_NAME"], url, Convert.ToDateTime(row["CreateTime"]));
                    else
                        _builder.AppendFormat("<td>{0}</td><td>{2}</td><td width='100'><a href='javascript:void(0)' onclick=\"DelFile('{1}',this)\">删 除</a>&nbsp;&nbsp;<a href='" + url + "' target='_blank'>查看附件</a></td>", row["FILE_NAME"], row["ID"], Convert.ToDateTime(row["CreateTime"]));
                    _builder.Append("</tr>");
                }
                ltrFile.Text = _builder.ToString();
            }
        }
    }
    private string GetFileSql(string YWBM)
    {
        string sql = string.Empty;
        string[] items = hfUpadFile.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in items)
        {
            if (!string.IsNullOrEmpty(item))
            {
                string downLoadPath = item.Split('|')[0];
                string fileSize = item.Split('|')[1];
                string fileName = Path.GetFileName(downLoadPath);
                sql += string.Format("INSERT INTO YW_FILE_DETAIL(ID,FileId,YWBM,[FILE_NAME],FILE_SIZE,DownLoadPath)VALUES(NEWID(),{0},'{1}','{2}','{3}','{4}');", 0, YWBM, fileName, fileSize, downLoadPath);
            }
        }
        return sql;
    }
    private string Id
    {
        get
        {
            return Request.QueryString["JG_Id"];
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
        string sql = string.Empty;
        string YWBM = string.Empty;
        List<SqlParameter> list = new List<SqlParameter>();
        if (string.IsNullOrEmpty(hfId.Value))//新增
        {
            YWBM = Guid.NewGuid().ToString();
            sql = string.Format(@"INSERT INTO [dbCenter].[dbo].[XM_QTZL]
                                       ([ID]
                                       ,[YWBM]
                                       ,[QTZLBM]
                                       ,[QTZL]
                                       ,[SFJBSGTJ])
                                 VALUES(@ID,@YWBM,@QTZLBM,@QTZL,@SFJBSGTJ)");
            list.Add(new SqlParameter() { ParameterName = "@ID",Value=YWBM,SqlDbType =SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YWBM", Value = Id, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@QTZLBM", Value = Guid.NewGuid().ToString(), SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@QTZL", Value = txtQTZL.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SFJBSGTJ", Value = txtSFJBSGTJ.Text, SqlDbType = SqlDbType.VarChar });
        }
        else
        { //编辑
            sql = @"UPDATE [dbCenter].[dbo].[XM_QTZL]
                    SET [QTZL] = @QTZL,
                        [SFJBSGTJ] = @SFJBSGTJ
                    WHERE YWBM=@YWBM";
            list.Add(new SqlParameter() { ParameterName = "@YWBM", Value = Id, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@QTZL", Value = txtQTZL.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SFJBSGTJ", Value = txtSFJBSGTJ.Text, SqlDbType = SqlDbType.VarChar });
            YWBM = hfId.Value;
        }
        pageTool tool = new pageTool(this.Page, "txt");
        sql += GetFileSql(YWBM);
        bool success = rc.PExcute(sql,list.ToArray());
        if (success)
        {
            hfId.Value = YWBM;
            ShowFile(YWBM);
            hfUpadFile.Value = "";
            tool.showMessage("保存成功");
        }
        else
            tool.showMessage("保存失败");
    }
}