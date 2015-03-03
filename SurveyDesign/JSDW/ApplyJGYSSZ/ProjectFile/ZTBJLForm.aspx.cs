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

public partial class JSDW_ApplyJGYS_ProjectFile_ZYBJLForm : System.Web.UI.Page
{
    private ProjectDB db = new ProjectDB();
    private RCenter rc = new RCenter();
    private RCenter rcJST = new RCenter("JST_XZSPBaseInfo");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();

            string sql = string.Format("select top 1 * from XM_ZTB where YWBM='{0}'", YWBM);
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
                sql = string.Format("select top 1 XMMC,GCMC,JSDW,XMBH,JSGM,JZMJ ZMJ,XMBM from YW_JGYS where ID='{0}'", YWBM);
                DataTable dt = rc.GetTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                    hfXMBM.Value = row["XMBM"].ToString();
                }
            }
            if (!string.IsNullOrEmpty(Id))//编辑
            {
                sql = string.Format(@"select top 1 * from XM_JLDWZTB where ID='{0}'", Id);
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                    ddlZBFS.SelectedValue = row["ZBFS"].ToString();
                    ddlZBLX.SelectedValue = row["ZBLX"].ToString();
                    ddlZJLGCSZJLX.SelectedValue = row["ZJLGCSZJLX"].ToString();
                    hfId.Value = row["Id"].ToString();
                    ShowFile(Id, ltrFile);
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
        ddlZBFS.Items.Insert(0, new ListItem("--请选择--", ""));

        var ZBLXData = db.getDicList(112208);//招标类别
        ddlZBLX.DataSource = ZBLXData;
        ddlZBLX.DataTextField = "FName";
        ddlZBLX.DataValueField = "FNumber";
        ddlZBLX.DataBind();
        ddlZBLX.Items.Insert(0, new ListItem("--请选择--", ""));


        var ZJLX = db.getDicList(112203);
        ddlZJLGCSZJLX.DataSource = ZJLX;
        ddlZJLGCSZJLX.DataTextField = "FName";
        ddlZJLGCSZJLX.DataValueField = "FNumber";
        ddlZJLGCSZJLX.DataBind();
        ddlZJLGCSZJLX.Items.Insert(0, new ListItem("--请选择--", ""));

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "txt");
        if (string.IsNullOrEmpty(hfZTBID.Value))//新增
        {
            #region 新增
            string guid = Guid.NewGuid().ToString();
            hfJLId.Value = Guid.NewGuid().ToString();
            string sql = string.Format(@"INSERT INTO [dbCenter].[dbo].[XM_ZTB]
                                               ([ID]
                                               ,[YWBM]
                                               ,[ZTBBM]
                                               ,[XMMC]
                                               ,[GCMC]
                                               ,[JSDW]
                                               ,[XMBH]
                                               ,[JSGM]
                                               ,[ZMJ])
                                         VALUES(@ID,@YWBM,@ZTBBM,@XMMC,@GCMC,@JSDW,@XMBH,@JSGM,@ZMJ);");
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
            rc.PExcute(sql, list.ToArray());
            hfZTBID.Value = guid;
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
                                          ,[JSGM] = @JSGM
                                          ,[ZMJ] = @ZMJ
                                     WHERE ID=@ID;");
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = hfZTBID.Value, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMMC", Value = txtXMMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@GCMC", Value = txtGCMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSDW", Value = txtJSDW.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMBH", Value = txtXMBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSGM", Value = txtJSGM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZMJ", Value = txtZMJ.Text, SqlDbType = SqlDbType.VarChar });
            rc.PExcute(sql, list.ToArray());
            #endregion
        }
        bool success = false;
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
        string JLId = Guid.NewGuid().ToString();
       string sql = string.Format(@"INSERT INTO [dbCenter].[dbo].[XM_JLDWZTB]
                                           ([ID]
                                           ,[YWBM]
                                           ,[ZTBID]
                                           ,[ZBFS]
                                           ,[ZBLX]
                                           ,[ZBLXMC]
                                           ,[ZBDW]
                                           ,[ZBTZSBH]
                                           ,[ZBDWZZJGDM]
                                           ,[ZBQYZZZSH]
                                           ,[ZBQYZZDJ]
                                           ,[ZBJE]
                                           ,[ZBRQ]
                                           ,[ZJLGCSXM]
                                           ,[ZJLGCSZJLX]
                                           ,[ZJLGCSZJH]
                                           ,[BASJ]
                                           ,[ZBDLDWMC]
                                           ,[ZBDLDWZZJGDM])
                                     VALUES(@ID,@YWBM,@ZTBID,@ZBFS,@ZBLX,@ZBLXMC,@ZBDW,@ZBTZSBH,@ZBDWZZJGDM,@ZBQYZZZSH,@ZBQYZZDJ,@ZBJE,@ZBRQ,@ZJLGCSXM,@ZJLGCSZJLX,@ZJLGCSZJH,@BASJ,@ZBDLDWMC,@ZBDLDWZZJGDM);");
       
        List<SqlParameter> listJL = new List<SqlParameter>();

        listJL.Add(new SqlParameter() { ParameterName = "@ID", Value = JLId, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@YWBM", Value = YWBM, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZTBID", Value = ZTBID, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBFS", Value = ddlZBFS.SelectedValue, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBLX", Value = ddlZBLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBLXMC", Value = ddlZBLX.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBDW", Value = txtZBDW.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBTZSBH", Value = txtZBTZSBH.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBDWZZJGDM", Value = txtZBDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBQYZZZSH", Value = txtZBQYZZZSH.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBQYZZDJ", Value = txtZBQYZZDJ.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBJE", Value = txtZBJE.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBRQ", Value = txtZBRQ.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZJLGCSXM", Value = txtZJLGCSXM.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZJLGCSZJLX", Value = ddlZJLGCSZJLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZJLGCSZJH", Value = txtZJLGCSZJH.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@BASJ", Value = txtBASJ.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBDLDWMC", Value = txtZBDLDWMC.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBDLDWZZJGDM", Value = txtZBDLDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
        sql += GetFileSql(JLId, hfUpadFile.Value);
        bool success = rc.PExcute(sql, listJL.ToArray());
        hfId.Value = ZTBID;
        hfUpadFile.Value = "";
        ShowFile(JLId, ltrFile);
        return success;
    }
    private bool Update()
    {
        string sql = @"UPDATE [dbCenter].[dbo].[XM_JLDWZTB]
                        SET [ZBFS] = @ZBFS
                            ,[ZBLX] = @ZBLX
                            ,[ZBLXMC] = @ZBLXMC
                            ,[ZBDW] = @ZBDW
                            ,[ZBTZSBH] = @ZBTZSBH
                            ,[ZBDWZZJGDM] = @ZBDWZZJGDM
                            ,[ZBQYZZZSH] = @ZBQYZZZSH
                            ,[ZBQYZZDJ] = @ZBQYZZDJ
                            ,[ZBJE] = @ZBJE
                            ,[ZBRQ] = @ZBRQ
                            ,[ZJLGCSXM] = @ZJLGCSXM
                            ,[ZJLGCSZJLX] = @ZJLGCSZJLX
                            ,[ZJLGCSZJH] = @ZJLGCSZJH
                            ,[BASJ] = @BASJ
                            ,[ZBDLDWMC] = @ZBDLDWMC
                            ,[ZBDLDWZZJGDM] = @ZBDLDWZZJGDM
                        WHERE ID=@ID;";
        List<SqlParameter> listJL = new List<SqlParameter>();
        listJL.Add(new SqlParameter() { ParameterName = "@ID", Value = Id , SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBFS", Value = ddlZBFS.SelectedValue, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBLX", Value = ddlZBLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBLXMC", Value = ddlZBLX.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBDW", Value = txtZBDW.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBTZSBH", Value = txtZBTZSBH.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBDWZZJGDM", Value = txtZBDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBQYZZZSH", Value = txtZBQYZZZSH.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBQYZZDJ", Value = txtZBQYZZDJ.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBJE", Value = txtZBJE.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBRQ", Value = txtZBRQ.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZJLGCSXM", Value = txtZJLGCSXM.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZJLGCSZJLX", Value = ddlZJLGCSZJLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZJLGCSZJH", Value = txtZJLGCSZJH.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@BASJ", Value = txtBASJ.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBDLDWMC", Value = txtZBDLDWMC.Text, SqlDbType = SqlDbType.VarChar });
        listJL.Add(new SqlParameter() { ParameterName = "@ZBDLDWZZJGDM", Value = txtZBDLDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });

        sql += GetFileSql(Id, hfUpadFile.Value);
        bool success = rc.PExcute(sql, listJL.ToArray());

        hfUpadFile.Value = "";
        ShowFile(Id, ltrFile);
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
    private string ZTBBM {
        get { return Request.QueryString["ZTBID"]; }
    }
    private string IsShow
    {
        get
        {
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
    protected void btnZBDW_Click(object sender, EventArgs e)
    {
        string QYZSID = hfQYZSID.Value;
        if (!string.IsNullOrEmpty(QYZSID))
        {
            string sql = string.Format(@"select top 1 A.ZSBH,B.QYMC,B.JGDM,B.QYLXBM,B.QYBM from QY_QYZSXX A LEFT JOIN QY_JBXX B ON A.QYBM=B.QYBM WHERE A.QYZSID = '{0}'", QYZSID);
            DataTable dt = rcJST.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtZBDW.Text = row["QYMC"].ToString();
                txtZBDWZZJGDM.Text = row["JGDM"].ToString();
                txtZBQYZZZSH.Text = row["ZSBH"].ToString();

                string QYLXBM = row["QYLXBM"].ToString().Trim();
                string QYBM = row["QYBM"].ToString();
                if (!string.IsNullOrEmpty(QYLXBM) && !string.IsNullOrEmpty(QYBM))
                {
                    txtZBQYZZDJ.Text = GetZZX(QYLXBM, QYBM);
                }
            }
        }
    }
    //获取资质等级
    private string GetZZX(string QYLXBM,string QYBM)
    {
        string ZZDJ = string.Empty;
        string sql = string.Empty;
        string[] SingleItems = { "120", "121", "130", "135", "136", "185", "186", "187", "202" };//单项资质
        if (QYLXBM == "101")//施工企业，只提取主项资质
        {
            sql = string.Format("select ZZMC,SFZX,ZZLB,ZZDJ from [dbo].QY_QYZZXX where QYBM='{0}' and SFZX=1",QYBM);
            DataTable table = rcJST.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                ZZDJ = row["ZZMC"].ToString() + row["ZZLB"].ToString() + row["ZZDJ"].ToString();
            }
        }
        else if (SingleItems.FirstOrDefault(x => x == QYLXBM) != null)//单项资质
        {
            sql = string.Format("select top 1 ZZDJ from [QY_QYZZXX] where QYBM='{0}'", QYBM);
            DataTable table = rcJST.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                ZZDJ = row["ZZDJ"].ToString();
            }
        }
        else
        { //多项资质
            sql = string.Format("select ZZMC,SFZX,ZZLB,ZZDJ from [dbo].QY_QYZZXX where QYBM='{0}'", QYBM);
            DataTable table = rcJST.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                List<string> list = new List<string>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    string value = row["ZZMC"].ToString() + row["ZZLB"].ToString() + row["ZZDJ"].ToString();
                    if (!string.IsNullOrEmpty(value))
                        list.Add(value);
                }
                ZZDJ = string.Join(",", list.ToArray());
            }
        }
        return ZZDJ;
    }
    protected void btnZBDLDW_Click(object sender, EventArgs e)
    {
        string QYBM = hfZBDLDWQYBM.Value;
        if (!string.IsNullOrEmpty(QYBM))
        {
            string sql = string.Format("select top 1* from QY_JBXX where QYBM='{0}'", QYBM);
            DataTable dt = rcJST.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtZBDLDWMC.Text = row["QYMC"].ToString();
                txtZBDLDWZZJGDM.Text = row["JGDM"].ToString();
            }
        }
    }
    protected void btnChoosePerson_Click(object sender, EventArgs e)
    {
        string RYBH = hfRYBH.Value;
        if (!string.IsNullOrEmpty(RYBH))
        {
            string sql = string.Format("select top 1 * from [RY_RYZSXX] where RYZSXXID='{0}'", RYBH);
            DataTable table = rcJST.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                txtZJLGCSXM.Text = row["XM"].ToString();
                txtZJLGCSZJH.Text = row["SFZH"].ToString();
            }
        }
    }
}