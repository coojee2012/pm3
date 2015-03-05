using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Approve.Common;
using System.Data;
using ProjectData;
using Approve.RuleCenter;
using System.Text;
using System.IO;

public partial class JSDW_ApplyJGYS_ProjectFile_ZTBSGForm : System.Web.UI.Page
{
    private ProjectDB db = new ProjectDB();
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            string sql=string.Format("select * from XM_ZTB where YWBM='{0}'",YWBM);
            DataTable dt1 = rc.GetTable(sql);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                DataRow row = dt1.Rows[0];
                pageTool tool = new pageTool(this.Page, "txt");
                tool.fillPageControl(row);
                hfZTBID.Value = row["ID"].ToString();
            }
            else
            {
                sql = string.Format("select top 1 XMMC,GCMC,JSDW,XMBH,JSGM,JZMJ ZMJ from YW_JGYS where ID='{0}'", YWBM);
                DataTable dt = rc.GetTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                }
            }
            if (!string.IsNullOrEmpty(Id))//编辑
            {
                sql = string.Format(@"select * from XM_SGDWZTB where ID='{0}'", Id);
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                    ddlSGZBFS.SelectedValue = row["SGZBFS"].ToString();
                    ddlSGZBLX.SelectedValue = row["SGZBLX"].ToString();
                    ddlXMJLZJLX.SelectedValue = row["SGXMJLZJLX"].ToString();
                    hfId.Value = Id;
                    ShowFile(Id, ltrSGText);
                }
            }
            EnabledControl();
        }
    }
    private void EnabledControl()
    {
        if (FIsApprove == "1" || IsShow == "1") //审核页面跳转
        {
            foreach (Control control in this.form1.Controls)
            {
                WebHelper.SetControlEnabled(control);
            }
        }
    }
    private void BindControl()
    {
        var ZBFSData = db.getDicList(112206); //招标方式
        ddlSGZBFS.DataSource = ZBFSData;
        ddlSGZBFS.DataTextField = "FName";
        ddlSGZBFS.DataValueField = "FNumber";
        ddlSGZBFS.DataBind();
        ddlSGZBFS.Items.Insert(0, new ListItem("--请选择--", ""));

        var ZBLXData = db.getDicList(112208);//招标类别
        ddlSGZBLX.DataSource = ZBLXData;
        ddlSGZBLX.DataTextField = "FName";
        ddlSGZBLX.DataValueField = "FNumber";
        ddlSGZBLX.DataBind();
        ddlSGZBLX.Items.Insert(0, new ListItem("--请选择--", ""));

        var ZJLX = db.getDicList(112203);
        ddlXMJLZJLX.DataSource = ZJLX;
        ddlXMJLZJLX.DataTextField = "FName";
        ddlXMJLZJLX.DataValueField = "FNumber";
        ddlXMJLZJLX.DataBind();
        ddlXMJLZJLX.Items.Insert(0, new ListItem("--请选择--", ""));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "txt");
        if (string.IsNullOrEmpty(hfZTBID.Value))//新增
        {
            #region 新增
            string guid = Guid.NewGuid().ToString();
            string sql = @"INSERT INTO [dbCenter].[dbo].[XM_ZTB]
                                    ([ID]
                                    ,[YWBM]
                                    ,[ZTBBM]
                                    ,[XMMC]
                                    ,[GCMC]
                                    ,[JSDW]
                                    ,[XMBH]
                                    ,[JSGM]
                                    ,[ZMJ])
                           VALUES(@ID,@YWBM,@ZTBBM,@XMMC,@GCMC,@JSDW,@XMBH,@JSGM,@ZMJ);";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = guid, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YWBM", Value = YWBM, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZTBBM", Value = Guid.NewGuid().ToString(), SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMMC", Value = txtXMMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@GCMC", Value = txtGCMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSDW", Value = txtJSDW.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMBH", Value = txtXMBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSGM", Value = txtJSGM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZMJ", Value = txtZMJ.Text, SqlDbType = SqlDbType.VarChar });
            hfZTBID.Value = guid;
            rc.PExcute(sql, list.ToArray());
            #endregion
        }
        else
        {
            #region 编辑
            List<SqlParameter> list = new List<SqlParameter>();
           string sql = @"UPDATE [dbCenter].[dbo].[XM_ZTB]
                            SET [XMMC] = @XMMC
                                ,[GCMC] = @GCMC
                                ,[JSDW] = @JSDW
                                ,[XMBH] = @XMBH
                                ,[JSGM] = @JSGM
                                ,[ZMJ] = @ZMJ
                            WHERE ID=@ID;";
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = ZTBID, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMMC", Value = txtXMMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@GCMC", Value = txtGCMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSDW", Value = txtJSDW.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMBH", Value = txtXMBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSGM", Value = txtJSGM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZMJ", Value = txtZMJ.Text, SqlDbType = SqlDbType.VarChar });
            bool abc = rc.PExcute(sql, list.ToArray());
            #endregion
        }
        bool success=false;
        if (string.IsNullOrEmpty(hfId.Value))
            success = Insert(hfZTBID.Value);
        else
            success = Update();
        if (success)
        {
            tool.showMessage("保存成功");
        }
        else
            tool.showMessage("保存失败");
    }
    private bool Insert(string ZTBID)
    {

        List<SqlParameter> listSG = new List<SqlParameter>();
        string SGId = Guid.NewGuid().ToString();
       string sql = @"INSERT INTO [dbCenter].[dbo].[XM_SGDWZTB]
                                    ([ID]
                                    ,[YWBM]
                                    ,[ZTBID]
                                    ,[SGZBFS]
                                    ,[SGZBLX]
                                    ,[SGZBDW]
                                    ,[ZBTZSBH]
                                    ,[SGZBDWZZJGDM]
                                    ,[SGZBQYZZZSH]
                                    ,[SGZBQYZZDJ]
                                    ,[SGZBJE]
                                    ,[SGZBRQ]
                                    ,[SGXMJLXM]
                                    ,[SGXMJLZJLX]
                                    ,[SGXMJLZJHM]
                                    ,[SGBASJ]
                                    ,[ZBDLDWMC]
                                    ,[ZBDLDWZZJGDM])
                                VALUES(@ID,@YWBM,@ZTBID,@SGZBFS,@SGZBLX,@SGZBDW,@ZBTZSBH,@SGZBDWZZJGDM,@SGZBQYZZZSH,@SGZBQYZZDJ,@SGZBJE,@SGZBRQ,@SGXMJLXM,@SGXMJLZJLX,@SGXMJLZJHM,@SGBASJ,@ZBDLDWMC,@ZBDLDWZZJGDM);";
       listSG.Add(new SqlParameter() { ParameterName = "@ID", Value = SGId, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@YWBM", Value = YWBM, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@ZTBID", Value = ZTBID, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBFS", Value = ddlSGZBFS.SelectedValue, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBLX", Value = ddlSGZBLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBDW", Value = txtSGZBDW.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@ZBTZSBH", Value = txtZBTZSBH.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBDWZZJGDM", Value = txtSGZBDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBQYZZZSH", Value = txtSGZBQYZZZSH.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBQYZZDJ", Value = txtSGZBQYZZDJ.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBJE", Value = txtSGZBJE.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBRQ", Value = txtSGZBRQ.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGXMJLXM", Value = txtSGXMJLXM.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGXMJLZJLX", Value = ddlXMJLZJLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGXMJLZJHM", Value = txtSGXMJLZJHM.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGBASJ", Value = txtSGBASJ.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@ZBDLDWMC", Value = txtZBDLDWMC.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@ZBDLDWZZJGDM", Value = txtZBDLDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
        //list.Add(new SqlParameter() { ParameterName = "@YWBM", Value = Id, SqlDbType = SqlDbType.VarChar });
        sql += GetFileSql(SGId, hfSGUpadFile.Value);
        bool success = rc.PExcute(sql, listSG.ToArray());
        hfId.Value = ZTBID;
        hfSGUpadFile.Value = "";
        ShowFile(SGId, ltrSGText);
        return success;
    }
    private bool Update()
    {
       string sql = string.Format(@"UPDATE [dbCenter].[dbo].[XM_SGDWZTB]
                                           SET [SGZBFS] = @SGZBFS
                                              ,[SGZBLX] = @SGZBLX
                                              ,[SGZBDW] = @SGZBDW
                                              ,[ZBTZSBH] = @ZBTZSBH
                                              ,[SGZBDWZZJGDM] = @SGZBDWZZJGDM
                                              ,[SGZBQYZZZSH] = @SGZBQYZZZSH
                                              ,[SGZBQYZZDJ] = @SGZBQYZZDJ
                                              ,[SGZBJE] = @SGZBJE
                                              ,[SGZBRQ] = @SGZBRQ
                                              ,[SGXMJLXM] = @SGXMJLXM
                                              ,[SGXMJLZJLX] = @SGXMJLZJLX
                                              ,[SGXMJLZJHM] = @SGXMJLZJHM
                                              ,[SGBASJ] = @SGBASJ
                                              ,[ZBDLDWMC] = @ZBDLDWMC
                                              ,[ZBDLDWZZJGDM] = @ZBDLDWZZJGDM
                                         WHERE ID=@ID;");
        List<SqlParameter> listSG = new List<SqlParameter>();
        listSG.Add(new SqlParameter() { ParameterName = "@ID", Value = Id, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBFS", Value = ddlSGZBFS.SelectedValue, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBLX", Value = ddlSGZBLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBDW", Value = txtSGZBDW.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@ZBTZSBH", Value = txtZBTZSBH.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBDWZZJGDM", Value = txtSGZBDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBQYZZZSH", Value = txtSGZBQYZZZSH.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBQYZZDJ", Value = txtSGZBQYZZDJ.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBJE", Value = txtSGZBJE.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGZBRQ", Value = txtSGZBRQ.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGXMJLXM", Value = txtSGXMJLXM.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGXMJLZJLX", Value = ddlXMJLZJLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGXMJLZJHM", Value = txtSGXMJLZJHM.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@SGBASJ", Value = txtSGBASJ.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@ZBDLDWMC", Value = txtZBDLDWMC.Text, SqlDbType = SqlDbType.VarChar });
        listSG.Add(new SqlParameter() { ParameterName = "@ZBDLDWZZJGDM", Value = txtZBDLDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
        sql += GetFileSql(Id, hfSGUpadFile.Value);
        bool success = rc.PExcute(sql, listSG.ToArray());
        hfSGUpadFile.Value = "";
        ShowFile(Id, ltrSGText);
        return success;
    }
    private void ShowFile(string YWBM, Literal ltrText)
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
                ltrText.Text = _builder.ToString();
            }
        }
    }
    private string GetFileSql(string Id, string upFile)
    {
        string sql = string.Empty;
        if (!string.IsNullOrEmpty(upFile))
        {
            string[] items = upFile.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string downLoadPath = item.Split('|')[0];
                    string fileSize = item.Split('|')[1];
                    string fileName = Path.GetFileName(downLoadPath);
                    sql += string.Format("INSERT INTO YW_FILE_DETAIL(ID,FileId,YWBM,[FILE_NAME],FILE_SIZE,DownLoadPath)VALUES(NEWID(),{0},'{1}','{2}','{3}','{4}');", 0, Id, fileName, fileSize, downLoadPath);
                }
            }
        }
        return sql;
    }
    private string YWBM
    {
        get
        {
            return Request.QueryString["JG_Id"];
        }
    }
    private string Id
    {
        get
        {
            return Request.QueryString["Id"];
        }
    }
    private string IsShow
    {
        get
        {
            return Request.QueryString["IsShow"];
        }
    }
    private string ZTBID
    {
        get
        {
            return Request.QueryString["ZTBID"];
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