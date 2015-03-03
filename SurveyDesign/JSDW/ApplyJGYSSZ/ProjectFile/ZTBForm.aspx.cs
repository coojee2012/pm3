using ProjectData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Approve.RuleCenter;
using Approve.Common;
using System.Text;
using System.IO;

public partial class JSDW_ApplyJGYS_ProjectFile_ZTBForm : System.Web.UI.Page
{
    private ProjectDB db = new ProjectDB();
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            if (!string.IsNullOrEmpty(Id))//编辑
            {
                string sql = string.Format(@"select top 1 a.*,b.ID JLId,b.ZBFS,b.ZBLX,b.ZBDW,b.ZBDWZZJGDM,b.ZBQYZZZSH,b.ZBQYZZDJ,b.ZBJE,b.ZBRQ,b.ZJLGCSXM,b.ZJLGCSZJLX,b.ZJLGCSZJH,
b.BASJ,c.ID SGId,c.SGZBFS,c.SGZBLX,c.SGZBDW,c.SGZBDWZZJGDM,c.SGZBQYZZZSH,c.SGZBQYZZDJ,c.SGZBJE,c.SGZBRQ,c.SGXMJLXM,c.SGXMJLZJLX,c.SGXMJLZJHM,c.SGBASJ
from XM_ZTB a left join XM_JLDWZTB b on a.ID = b.ZTBID left join XM_SGDWZTB c on a.ID = c.ZTBID where a.ID='{0}'", Id);
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                    hfId.Value = row["ID"].ToString();
                    ddlZBFS.SelectedValue = row["ZBFS"].ToString();
                    ddlZBLX.SelectedValue = row["ZBLX"].ToString();
                    ddlSGZBFS.SelectedValue = row["SGZBFS"].ToString();
                    ddlSGZBLX.SelectedValue = row["SGZBLX"].ToString();
                    ddlXMJLZJLX.SelectedValue = row["SGXMJLZJLX"].ToString();
                    ddlZJLGCSZJLX.SelectedValue = row["ZJLGCSZJLX"].ToString();

                    hfJLId.Value = row["JLId"].ToString();
                    hfSGId.Value = row["SGId"].ToString();

                    ShowFile(hfJLId.Value, ltrFile);
                    ShowFile(hfSGId.Value, ltrSGText);
                }
            }
            else {
                string sql = string.Format("select top 1 XMMC,GCMC,JSDW,XMBH,JSGM,JZMJ ZMJ from YW_JGYS where ID='{0}'", YWBM);
                DataTable dt = rc.GetTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
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
        ddlZBFS.DataSource = ZBFSData;
        ddlZBFS.DataTextField = "FName";
        ddlZBFS.DataValueField = "FNumber";
        ddlZBFS.DataBind();
        ddlZBFS.Items.Insert(0, new ListItem("--请选择--", "-1"));

        ddlSGZBFS.DataSource = ZBFSData;
        ddlSGZBFS.DataTextField = "FName";
        ddlSGZBFS.DataValueField = "FNumber";
        ddlSGZBFS.DataBind();
        ddlSGZBFS.Items.Insert(0, new ListItem("--请选择--", "-1"));

        var ZBLXData = db.getDicList(112208);//招标类别
        ddlZBLX.DataSource = ZBLXData;
        ddlZBLX.DataTextField = "FName";
        ddlZBLX.DataValueField = "FNumber";
        ddlZBLX.DataBind();
        ddlZBLX.Items.Insert(0, new ListItem("--请选择--", "-1"));

        ddlSGZBLX.DataSource = ZBLXData;
        ddlSGZBLX.DataTextField = "FName";
        ddlSGZBLX.DataValueField = "FNumber";
        ddlSGZBLX.DataBind();
        ddlSGZBLX.Items.Insert(0, new ListItem("--请选择--", "-1"));

        var ZJLX = db.getDicList(112203);
        ddlZJLGCSZJLX.DataSource = ZJLX;
        ddlZJLGCSZJLX.DataTextField = "FName";
        ddlZJLGCSZJLX.DataValueField = "FNumber";
        ddlZJLGCSZJLX.DataBind();
        ddlZJLGCSZJLX.Items.Insert(0, new ListItem("--请选择--", "-1"));

        ddlXMJLZJLX.DataSource = ZJLX;
        ddlXMJLZJLX.DataTextField = "FName";
        ddlXMJLZJLX.DataValueField = "FNumber";
        ddlXMJLZJLX.DataBind();
        ddlXMJLZJLX.Items.Insert(0, new ListItem("--请选择--", "-1"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "txt");
        if (string.IsNullOrEmpty(hfId.Value))//新增
        {
            #region 新增
            string ZTBID = Guid.NewGuid().ToString();
            hfJLId.Value = Guid.NewGuid().ToString();
            hfSGId.Value =Guid.NewGuid().ToString();
            string sql = string.Format(@"INSERT INTO [dbCenter].[dbo].[XM_ZTB]
                                               ([ID]
                                               ,[YWBM]
                                               ,[ZTBBM]
                                               ,[XMMC]
                                               ,[XMBM]
                                               ,[GCMC]
                                               ,[JSDW]
                                               ,[XMBH]
                                               ,[ZBTZSBH]
                                               ,[JSGM]
                                               ,[ZMJ]
                                               ,[ZBDLDWMC]
                                               ,[ZBDLDWZZJGDM])
                                         VALUES(@ID,@YWBM,@ZTBBM,@XMMC,@XMBM,@GCMC,@JSDW,@XMBH,@ZBTZSBH,@JSGM,@ZMJ,@ZBDLDWMC,@ZBDLDWZZJGDM);");
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = ZTBID, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YWBM", Value = YWBM, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZTBBM", Value = Guid.NewGuid().ToString(), SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMMC", Value = txtXMMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMBM", Value = "", SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@GCMC", Value = txtGCMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSDW", Value = txtJSDW.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMBH", Value = txtXMBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZBTZSBH", Value = txtZBTZSBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSGM", Value = txtJSGM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZMJ", Value = txtZMJ.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZBDLDWMC", Value = txtZBDLDWMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZBDLDWZZJGDM", Value = txtZBDLDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
            if (rc.PExcute(sql, list.ToArray()))
            {
                sql = string.Empty;
                sql = string.Format(@"INSERT INTO [dbCenter].[dbo].[XM_JLDWZTB]
                                           ([ID]
                                           ,[YWBM]
                                           ,[ZTBID]
                                           ,[ZBFS]
                                           ,[ZBLX]
                                           ,[ZBDW]
                                           ,[ZBDWZZJGDM]
                                           ,[ZBQYZZZSH]
                                           ,[ZBQYZZDJ]
                                           ,[ZBJE]
                                           ,[ZBRQ]
                                           ,[ZJLGCSXM]
                                           ,[ZJLGCSZJLX]
                                           ,[ZJLGCSZJH]
                                           ,[BASJ])
                                     VALUES(@ID,@YWBM,@ZTBID,@ZBFS,@ZBLX,@ZBDW,@ZBDWZZJGDM,@ZBQYZZZSH,@ZBQYZZDJ,@ZBJE,@ZBRQ,@ZJLGCSXM,@ZJLGCSZJLX,@ZJLGCSZJH,@BASJ);");
                sql += GetFileSql(hfJLId.Value, hfUpadFile.Value);
                List<SqlParameter> listJL = new List<SqlParameter>();
                
                listJL.Add(new SqlParameter() { ParameterName = "@ID", Value = hfJLId.Value, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@YWBM", Value = YWBM, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZTBID", Value = ZTBID, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBFS", Value = ddlZBFS.SelectedValue, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBLX", Value = ddlZBLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBDW", Value = txtZBDW.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBDWZZJGDM", Value = txtZBDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBQYZZZSH", Value = txtZBQYZZZSH.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBQYZZDJ", Value = txtZBQYZZDJ.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBJE", Value = txtZBJE.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBRQ", Value = txtZBRQ.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZJLGCSXM", Value = txtZJLGCSXM.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZJLGCSZJLX", Value = ddlZJLGCSZJLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZJLGCSZJH", Value = txtZJLGCSZJH.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@BASJ", Value = txtBASJ.Text, SqlDbType = SqlDbType.VarChar });
                rc.PExcute(sql, listJL.ToArray());

                List<SqlParameter> listSG = new List<SqlParameter>();
                sql = string.Empty;
                sql = string.Format(@"INSERT INTO [dbCenter].[dbo].[XM_SGDWZTB]
                                           ([ID]
                                           ,[YWBM]
                                           ,[ZTBID]
                                           ,[SGZBFS]
                                           ,[SGZBLX]
                                           ,[SGZBDW]
                                           ,[SGZBDWZZJGDM]
                                           ,[SGZBQYZZZSH]
                                           ,[SGZBQYZZDJ]
                                           ,[SGZBJE]
                                           ,[SGZBRQ]
                                           ,[SGXMJLXM]
                                           ,[SGXMJLZJLX]
                                           ,[SGXMJLZJHM]
                                           ,[SGBASJ])
                                     VALUES(@ID,@YWBM,@ZTBID,@SGZBFS,@SGZBLX,@SGZBDW,@SGZBDWZZJGDM,@SGZBQYZZZSH,@SGZBQYZZDJ,@SGZBJE,@SGZBRQ,@SGXMJLXM,@SGXMJLZJLX,@SGXMJLZJHM,@SGBASJ);");
                listSG.Add(new SqlParameter() { ParameterName = "@ID", Value = hfSGId.Value, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@YWBM", Value = YWBM, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@ZTBID", Value = ZTBID, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBFS", Value = ddlSGZBFS.SelectedValue, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBLX", Value = ddlSGZBLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBDW", Value = txtSGZBDW.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBDWZZJGDM", Value = txtSGZBDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBQYZZZSH", Value = txtSGZBQYZZZSH.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBQYZZDJ", Value = txtSGZBQYZZDJ.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBJE", Value = txtSGZBJE.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBRQ", Value = txtSGZBRQ.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGXMJLXM", Value = txtSGXMJLXM.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGXMJLZJLX", Value = ddlXMJLZJLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGXMJLZJHM", Value = txtSGXMJLZJHM.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGBASJ", Value = txtSGBASJ.Text, SqlDbType = SqlDbType.VarChar });
                //list.Add(new SqlParameter() { ParameterName = "@YWBM", Value = Id, SqlDbType = SqlDbType.VarChar });
                sql += GetFileSql(hfSGId.Value, hfSGUpadFile.Value);
                rc.PExcute(sql, listSG.ToArray());
                hfId.Value = ZTBID;
                hfUpadFile.Value = "";
                hfSGUpadFile.Value = "";
                ShowFile(hfJLId.Value, ltrFile);
                ShowFile(hfSGId.Value, ltrSGText);
                tool.showMessage("保存成功");
            }
            else
                tool.showMessage("保存失败");
            #endregion
        }
        else
        {
            #region 编辑
            string sql = string.Empty;
            List<SqlParameter> list = new List<SqlParameter>();
            sql = string.Format(@"UPDATE [dbCenter].[dbo].[XM_ZTB]
                                       SET [XMMC] = @XMMC
                                          ,[GCMC] = @GCMC
                                          ,[JSDW] = @JSDW
                                          ,[XMBH] = @XMBH
                                          ,[ZBTZSBH] = @ZBTZSBH
                                          ,[JSGM] = @JSGM
                                          ,[ZMJ] = @ZMJ
                                          ,[ZBDLDWMC] = @ZBDLDWMC
                                          ,[ZBDLDWZZJGDM] = @ZBDLDWZZJGDM
                                     WHERE ID=@ID;");
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = hfId.Value, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMMC", Value = txtXMMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@GCMC", Value = txtGCMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSDW", Value = txtJSDW.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMBH", Value = txtXMBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZBTZSBH", Value = txtZBTZSBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSGM", Value = txtJSGM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZMJ", Value = txtZMJ.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZBDLDWMC", Value = txtZBDLDWMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZBDLDWZZJGDM", Value = txtZBDLDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
            if (rc.PExcute(sql,list.ToArray()))
            {
                sql = string.Empty;
                sql = string.Format(@"UPDATE [dbCenter].[dbo].[XM_JLDWZTB]
                                       SET [ZBFS] = @ZBFS
                                          ,[ZBLX] = @ZBLX
                                          ,[ZBDW] = @ZBDW
                                          ,[ZBDWZZJGDM] = @ZBDWZZJGDM
                                          ,[ZBQYZZZSH] = @ZBQYZZZSH
                                          ,[ZBQYZZDJ] = @ZBQYZZDJ
                                          ,[ZBJE] = @ZBJE
                                          ,[ZBRQ] = @ZBRQ
                                          ,[ZJLGCSXM] = @ZJLGCSXM
                                          ,[ZJLGCSZJLX] = @ZJLGCSZJLX
                                          ,[ZJLGCSZJH] = @ZJLGCSZJH
                                          ,[BASJ] = @BASJ
                                     WHERE ZTBID=@ZTBID;");
                List<SqlParameter> listJL = new List<SqlParameter>();
                listJL.Add(new SqlParameter() { ParameterName = "@ZTBID", Value = hfId.Value, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBFS", Value = ddlZBFS.SelectedValue, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBLX", Value = ddlZBLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBDW", Value = txtZBDW.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBDWZZJGDM", Value = txtZBDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBQYZZZSH", Value = txtZBQYZZZSH.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBQYZZDJ", Value = txtZBQYZZDJ.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBJE", Value = txtZBJE.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZBRQ", Value = txtZBRQ.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZJLGCSXM", Value = txtZJLGCSXM.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZJLGCSZJLX", Value = ddlZJLGCSZJLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@ZJLGCSZJH", Value = txtZJLGCSZJH.Text, SqlDbType = SqlDbType.VarChar });
                listJL.Add(new SqlParameter() { ParameterName = "@BASJ", Value = txtBASJ.Text, SqlDbType = SqlDbType.VarChar });
              
                sql += GetFileSql(hfJLId.Value, hfUpadFile.Value);
                rc.PExcute(sql, listJL.ToArray());

                sql = string.Empty;
                sql = string.Format(@"UPDATE [dbCenter].[dbo].[XM_SGDWZTB]
                                           SET [SGZBFS] = @SGZBFS
                                              ,[SGZBLX] = @SGZBLX
                                              ,[SGZBDW] = @SGZBDW
                                              ,[SGZBDWZZJGDM] = @SGZBDWZZJGDM
                                              ,[SGZBQYZZZSH] = @SGZBQYZZZSH
                                              ,[SGZBQYZZDJ] = @SGZBQYZZDJ
                                              ,[SGZBJE] = @SGZBJE
                                              ,[SGZBRQ] = @SGZBRQ
                                              ,[SGXMJLXM] = @SGXMJLXM
                                              ,[SGXMJLZJLX] = @SGXMJLZJLX
                                              ,[SGXMJLZJHM] = @SGXMJLZJHM
                                              ,[SGBASJ] = @SGBASJ
                                         WHERE ZTBID=@ZTBID;");
                List<SqlParameter> listSG = new List<SqlParameter>();
                listSG.Add(new SqlParameter() { ParameterName = "@ZTBID", Value = hfId.Value, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBFS", Value = ddlSGZBFS.SelectedValue, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBLX", Value = ddlSGZBLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBDW", Value = txtSGZBDW.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBDWZZJGDM", Value = txtSGZBDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBQYZZZSH", Value = txtSGZBQYZZZSH.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBQYZZDJ", Value = txtSGZBQYZZDJ.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBJE", Value = txtSGZBJE.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGZBRQ", Value = txtSGZBRQ.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGXMJLXM", Value = txtSGXMJLXM.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGXMJLZJLX", Value = ddlXMJLZJLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGXMJLZJHM", Value = txtSGXMJLZJHM.Text, SqlDbType = SqlDbType.VarChar });
                listSG.Add(new SqlParameter() { ParameterName = "@SGBASJ", Value = txtSGBASJ.Text, SqlDbType = SqlDbType.VarChar });
                sql += GetFileSql(hfSGId.Value, hfSGUpadFile.Value);
                rc.PExcute(sql, listSG.ToArray());
                hfUpadFile.Value = "";
                hfSGUpadFile.Value = "";
                ShowFile(hfJLId.Value, ltrFile);
                ShowFile(hfSGId.Value, ltrSGText);
                tool.showMessage("保存成功");
            }
            else
                tool.showMessage("保存失败");
            #endregion
        }
    }
    private void ShowFile(string YWBM,Literal ltrText)
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
    private string GetFileSql(string Id,string upFile)
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
    private string Id {
        get {
            return Request.QueryString["Id"];
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