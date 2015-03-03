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
using ProjectData;
public partial class JSDW_ApplyJGYS_ProjectFile_YDGHForm : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            string sql = string.Format("select top 1* from XM_JGYS_TRANS where YWBM='{0}' and TypeId=1", Id);
            DataTable table = rc.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row1 = table.Rows[0];
                txtLY.Text = row1["LY"].ToString();
                ddlIsTrans.SelectedValue = row1["IsTrans"].ToString();
            }
            sql = string.Format("select top 1 * from XM_YDGH where YWBM='{0}'", Id);
            table.Clear();
            table = rc.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                pageTool tool = new pageTool(this.Page, "txt");
                tool.fillPageControl(row);
                hfId.Value = row["ID"].ToString();
                hfSrouce.Value = row["IsSource"].ToString();
                ddlType.SelectedValue = row["QTGG"].ToString();
                ddlYDXZ.SelectedValue = row["YDXZ"].ToString();
                ShowFile(row["ID"].ToString());
            }
            else
            {
                string sqlStr = string.Format("select * from YW_JGYS where ID='{0}'",Id);
                DataTable dt = rc.GetTable(sqlStr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtXMMC.Text = row["XMMC"].ToString();
                    txtJSDZ.Text = row["GCDD"].ToString();
                    txtYDDW.Text = "";
                    txtYDMJ.Text = row["JZMJ"].ToString();
                    txtJSGM.Text = row["JSGM"].ToString();
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
        if (FIsApprove == "1" || Audit == "1"||hfSrouce.Value=="True") //审核页面跳转
        {
            foreach (Control control in this.form1.Controls)
            {
                WebHelper.SetControlEnabled(control);
            }
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
    private string Audit
    {
        get
        {
            return Request.QueryString["audit"];
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
        string XMId = string.Empty;
        List<SqlParameter> list = new List<SqlParameter>();
        if (string.IsNullOrEmpty(hfId.Value))//新增
        {
            XMId = Guid.NewGuid().ToString();
            sql = @"INSERT INTO [dbCenter].[dbo].[XM_YDGH]
                        ([ID]
                        ,[YWBM]
                        ,[YDGHBM]
                        ,[XMMC]
                        ,[JSDZ]
                        ,[YDDW]
                        ,[YDMJ]
                        ,[JSGM]
                        ,[QT]
                        ,[QTGG]
                        ,[YDXZ]
                        ,[YDGHXKZBH]
                        ,[FZJG]
                        ,[FZRQ])
                    VALUES(@ID,@YWBM,@YDGHBM,@XMMC,@JSDZ,@YDDW,@YDMJ,@JSGM,@QT,@QTGG,@YDXZ,@YDGHXKZBH,@FZJG,@FZRQ);";
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = XMId, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YWBM", Value = Id, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YDGHBM", Value = Guid.NewGuid().ToString(), SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMMC", Value = txtXMMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSDZ", Value = txtJSDZ.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YDDW", Value = txtYDDW.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YDMJ", Value = txtYDMJ.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSGM", Value = txtJSGM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@QT", Value = txtQT.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@QTGG", Value = ddlType.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YDXZ", Value = ddlYDXZ.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YDGHXKZBH", Value = txtYDGHXKZBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@FZJG", Value = txtFZJG.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@FZRQ", Value = txtFZRQ.Text, SqlDbType = SqlDbType.VarChar });

            sql += string.Format(@"INSERT INTO [dbo].[XM_JGYS_TRANS]
                       ([ID]
                       ,[YWBM]
                       ,[TypeId]
                       ,[IsTrans]
                       ,[LY])
                        values(NEWID(),'{0}','{1}','{2}','{3}');", Id, 1, ddlIsTrans.SelectedValue, txtLY.Text.Replace("'", string.Empty));
        }
        else
        { //编辑
            sql = @"UPDATE [dbCenter].[dbo].[XM_YDGH]
                        SET [XMMC] = @XMMC
                            ,[JSDZ] = @JSDZ
                            ,[YDDW] = @YDDW
                            ,[YDMJ] = @YDMJ
                            ,[JSGM] = @JSGM
                            ,[QT] = @QT
                            ,[QTGG] = @QTGG
                            ,[YDXZ] = @YDXZ
                            ,[YDGHXKZBH] = @YDGHXKZBH
                            ,[FZJG] = @FZJG
                            ,[FZRQ] = @FZRQ
                        WHERE YWBM=@YWBM;";
            XMId = hfId.Value;
            list.Add(new SqlParameter() { ParameterName = "@YWBM", Value = Id, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMMC", Value = txtXMMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSDZ", Value = txtJSDZ.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YDDW", Value = txtYDDW.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YDMJ", Value = txtYDMJ.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSGM", Value = txtJSGM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@QT", Value = txtQT.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@QTGG", Value = ddlType.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YDXZ", Value = ddlYDXZ.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YDGHXKZBH", Value = txtYDGHXKZBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@FZJG", Value = txtFZJG.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@FZRQ", Value = txtFZRQ.Text, SqlDbType = SqlDbType.VarChar });

            sql += string.Format(@"UPDATE [dbo].[XM_JGYS_TRANS]
                                       SET [IsTrans] = {0}
                                          ,[LY] = '{1}'
                                     WHERE YWBM='{2}' and TypeId=1;", ddlIsTrans.SelectedValue, txtLY.Text.Replace("'", string.Empty), Id);
        }
        pageTool tool = new pageTool(this.Page, "txt");
        sql += GetFileSql(XMId);
        bool success = rc.PExcute(sql,list.ToArray());
        if (success)
        {
            hfId.Value = XMId;
            ShowFile(XMId);
            hfUpadFile.Value = "";
            tool.showMessage("保存成功");
        }
        else
            tool.showMessage("保存失败");
    }
}