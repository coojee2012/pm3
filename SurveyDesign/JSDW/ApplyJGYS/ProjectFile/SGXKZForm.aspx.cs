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
using ProjectData;
public partial class JSDW_ApplyJGYS_ProjectFile_SGXKZForm : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private RCenter rcJST = new RCenter("JST_XZSPBaseInfo");
    private ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            if (!string.IsNullOrEmpty(SGXKZId))//编辑
            {
                string sql = string.Format("select top 1 * from XM_SGXKZ where ID='{0}'", SGXKZId);
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                    hfId.Value = row["ID"].ToString();
                    hfJLDWBM.Value = row["JLDWBM"].ToString();
                    hfKCDWBM.Value = row["KCDWBM"].ToString();
                    hfSGDWBM.Value = row["SGDWBM"].ToString();
                    hfSJDWBM.Value = row["SJDWBM"].ToString();
                    ShowInfo(row["ID"].ToString());
                    ddlXMJLZJLX.SelectedValue = row["XMJLZJLX"].ToString();
                    ddlZJLGCSZJLX.SelectedValue = row["ZJLGCSZJLX"].ToString();
                }
            }
            else//新增
            {
                DataTable table = new DataTable();
                DG_List.DataSource = table;
                DG_List.DataBind();
                string sql = string.Format("select top 1 JZMJ MJ,JSGM,XMBH,GCMC,XMBM from YW_JGYS where ID='{0}'", Id);
                DataTable dt = rc.GetTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    hfXMBH.Value = row["XMBM"].ToString();//项目编码
                    hfBH.Value = row["XMBH"].ToString(); //项目编号
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
        //项目经理证件类型
        var XMJL = db.getDicList(112203);
        ddlXMJLZJLX.DataSource = XMJL;
        ddlXMJLZJLX.DataTextField = "fname";
        ddlXMJLZJLX.DataValueField = "fnumber";
        ddlXMJLZJLX.DataBind();
        ddlXMJLZJLX.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });

        ddlZJLGCSZJLX.DataSource = XMJL;
        ddlZJLGCSZJLX.DataTextField = "fname";
        ddlZJLGCSZJLX.DataValueField = "fnumber";
        ddlZJLGCSZJLX.DataBind();
        ddlZJLGCSZJLX.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });
    }
    private string Id
    {
        get
        {
            return Request.QueryString["JG_Id"];
        }
    }
    private string SGXKZId {
        get {
            return Request.QueryString["Id"];
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
    private string IsShow {
        get {
            return Request.QueryString["IsShow"];
        }
    }
    private void ShowInfo(string SGXKZID)
    {
        string sql = string.Format(@"select * from XM_SGXKZSGCYRY where SGXKZID='{0}'",SGXKZID);
        DataTable table = rc.GetTable(sql);
        DG_List.DataSource = table;
        DG_List.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = string.Empty;
        string XMId = string.Empty;
        List<SqlParameter> list = new List<SqlParameter>();
        if (string.IsNullOrEmpty(hfId.Value))//新增
        {
            XMId = Guid.NewGuid().ToString();
            sql = @"INSERT INTO [dbCenter].[dbo].[XM_SGXKZ]
                                       ([ID]
                                       ,[YWBM]
                                       ,[SGXKZBM]
                                       ,XMBH
                                       ,BH
                                       ,[SGXKZBH]
                                       ,[FZRQ]
                                       ,[GCMC]
                                       ,[HTJE]
                                       ,[MJ]
                                       ,[JSGM]
                                       ,[KCDWMC]
                                       ,[KCDWZZJGDM]
                                       ,[SJDWMC]
                                       ,[SJDWZZJGDM]
                                       ,[SGDWMC]
                                       ,[SGDWZZJGDM]
                                       ,[SGDWAQSCXKZ]
                                       ,[JLDWMC]
                                       ,[JLDWZZJGDM]
                                       ,[XMJLXM]
                                       ,[XMJLZJLX]
                                       ,XMJLZJLXMC
                                       ,[XMJLZJHM]
                                       ,[ZJLGCSZXZ]
                                       ,[ZJLGCSZJLX]
                                       ,ZJLGCSZJLXMC
                                       ,[ZJLGCSZJHM]
                                       ,[KCDWBM]
                                       ,[SJDWBM]
                                       ,[SGDWBM]
                                       ,[JLDWBM])
                                 VALUES(@ID,@YWBM,@SGXKZBM,@XMBH,@BH,@SGXKZBH,@FZRQ,@GCMC,@HTJE,@MJ,@JSGM,@KCDWMC,@KCDWZZJGDM,@SJDWMC,@SJDWZZJGDM,@SGDWMC,@SGDWZZJGDM,@SGDWAQSCXKZ,@JLDWMC,@JLDWZZJGDM,@XMJLXM,@XMJLZJLX,@XMJLZJLXMC,@XMJLZJHM,@ZJLGCSZXZ,@ZJLGCSZJLX,@ZJLGCSZJLXMC,@ZJLGCSZJHM,@KCDWBM,@SJDWBM,@SGDWBM,@JLDWBM)";
            list.Add(new SqlParameter() { ParameterName = "@ID",Value=XMId });
            list.Add(new SqlParameter() { ParameterName = "@SGXKZBM", Value = Guid.NewGuid().ToString() });
            list.Add(new SqlParameter() { ParameterName = "@YWBM", Value = Id });
            list.Add(new SqlParameter() { ParameterName = "@XMBH", Value = hfXMBH.Value });
            list.Add(new SqlParameter() { ParameterName = "@BH", Value = hfBH.Value });
            list.Add(new SqlParameter() { ParameterName = "@SGXKZBH", Value = txtSGXKZBH.Text });
            list.Add(new SqlParameter() { ParameterName = "@FZRQ", Value = txtFZRQ.Text });
            list.Add(new SqlParameter() { ParameterName = "@GCMC", Value = txtGCMC.Text });
            list.Add(new SqlParameter() { ParameterName = "@HTJE", Value = txtHTJE.Text });
            list.Add(new SqlParameter() { ParameterName = "@MJ", Value = txtMJ.Text });
            list.Add(new SqlParameter() { ParameterName = "@JSGM", Value = txtJSGM.Text });
            list.Add(new SqlParameter() { ParameterName = "@KCDWMC", Value = txtKCDWMC.Text });
            list.Add(new SqlParameter() { ParameterName = "@KCDWZZJGDM", Value = txtKCDWZZJGDM.Text });
            list.Add(new SqlParameter() { ParameterName = "@SJDWMC", Value = txtSJDWMC.Text });
            list.Add(new SqlParameter() { ParameterName = "@SJDWZZJGDM", Value = txtSJDWZZJGDM.Text });
            list.Add(new SqlParameter() { ParameterName = "@SGDWMC", Value = txtSGDWMC.Text });
            list.Add(new SqlParameter() { ParameterName = "@SGDWZZJGDM", Value = txtSGDWZZJGDM.Text });
            list.Add(new SqlParameter() { ParameterName = "@SGDWAQSCXKZ", Value = txtSGDWAQSCXKZ.Text });
            list.Add(new SqlParameter() { ParameterName = "@JLDWMC", Value = txtJLDWMC.Text });
            list.Add(new SqlParameter() { ParameterName = "@JLDWZZJGDM", Value = txtJLDWZZJGDM.Text });
            list.Add(new SqlParameter() { ParameterName = "@XMJLXM", Value = txtXMJLXM.Text });
            list.Add(new SqlParameter() { ParameterName = "@XMJLZJLX", Value = ddlXMJLZJLX.SelectedValue });
            list.Add(new SqlParameter() { ParameterName = "@XMJLZJLXMC", Value = ddlXMJLZJLX.SelectedItem.Text });
            list.Add(new SqlParameter() { ParameterName = "@XMJLZJHM", Value = txtXMJLZJHM.Text });
            list.Add(new SqlParameter() { ParameterName = "@ZJLGCSZXZ", Value = txtZJLGCSZXZ.Text });
            list.Add(new SqlParameter() { ParameterName = "@ZJLGCSZJLX", Value = ddlZJLGCSZJLX.SelectedValue });
            list.Add(new SqlParameter() { ParameterName = "@ZJLGCSZJLXMC", Value = ddlZJLGCSZJLX.SelectedItem.Text });
            list.Add(new SqlParameter() { ParameterName = "@ZJLGCSZJHM", Value = txtZJLGCSZJHM.Text });
            list.Add(new SqlParameter() { ParameterName = "@KCDWBM", Value = hfKCDWBM.Value });
            list.Add(new SqlParameter() { ParameterName = "@SJDWBM", Value = hfSJDWBM.Value });
            list.Add(new SqlParameter() { ParameterName = "@SGDWBM", Value = hfSGDWBM.Value });
            list.Add(new SqlParameter() { ParameterName = "@JLDWBM", Value = hfJLDWBM.Value });
        }
        else
        {
            sql = @"UPDATE [dbCenter].[dbo].[XM_SGXKZ]
                    SET [SGXKZBH] = @SGXKZBH
                        ,[FZRQ] = @FZRQ
                        ,[GCMC] = @GCMC
                        ,[HTJE] = @HTJE
                        ,[MJ] = @MJ
                        ,[JSGM] = @JSGM
                        ,[KCDWMC] = @KCDWMC
                        ,[KCDWZZJGDM] = @KCDWZZJGDM
                        ,[SJDWMC] = @SJDWMC
                        ,[SJDWZZJGDM] = @SJDWZZJGDM
                        ,[SGDWMC] = @SGDWMC
                        ,[SGDWZZJGDM] = @SGDWZZJGDM
                        ,[SGDWAQSCXKZ] = @SGDWAQSCXKZ
                        ,[JLDWMC] = @JLDWMC
                        ,[JLDWZZJGDM] = @JLDWZZJGDM
                        ,[XMJLXM] = @XMJLXM
                        ,[XMJLZJLX] = @XMJLZJLX
                        ,XMJLZJLXMC = @XMJLZJLXMC
                        ,[XMJLZJHM] = @XMJLZJHM
                        ,[ZJLGCSZXZ] = @ZJLGCSZXZ
                        ,[ZJLGCSZJLX] = @ZJLGCSZJLX
                        ,ZJLGCSZJLXMC = @ZJLGCSZJLXMC
                        ,[ZJLGCSZJHM] = @ZJLGCSZJHM
                        ,[KCDWBM] = @KCDWBM
                        ,[SJDWBM] = @SJDWBM
                        ,[SGDWBM] = @SGDWBM
                        ,[JLDWBM] = @JLDWBM
                        WHERE ID=@ID";
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = SGXKZId });
            list.Add(new SqlParameter() { ParameterName = "@SGXKZBH", Value = txtSGXKZBH.Text });
            list.Add(new SqlParameter() { ParameterName = "@FZRQ", Value = txtFZRQ.Text });
            list.Add(new SqlParameter() { ParameterName = "@GCMC", Value = txtGCMC.Text });
            list.Add(new SqlParameter() { ParameterName = "@HTJE", Value = txtHTJE.Text });
            list.Add(new SqlParameter() { ParameterName = "@MJ", Value = txtMJ.Text });
            list.Add(new SqlParameter() { ParameterName = "@JSGM", Value = txtJSGM.Text });
            list.Add(new SqlParameter() { ParameterName = "@KCDWMC", Value = txtKCDWMC.Text });
            list.Add(new SqlParameter() { ParameterName = "@KCDWZZJGDM", Value = txtKCDWZZJGDM.Text });
            list.Add(new SqlParameter() { ParameterName = "@SJDWMC", Value = txtSJDWMC.Text });
            list.Add(new SqlParameter() { ParameterName = "@SJDWZZJGDM", Value = txtSJDWZZJGDM.Text });
            list.Add(new SqlParameter() { ParameterName = "@SGDWMC", Value = txtSGDWMC.Text });
            list.Add(new SqlParameter() { ParameterName = "@SGDWZZJGDM", Value = txtSGDWZZJGDM.Text });
            list.Add(new SqlParameter() { ParameterName = "@SGDWAQSCXKZ", Value = txtSGDWAQSCXKZ.Text });
            list.Add(new SqlParameter() { ParameterName = "@JLDWMC", Value = txtJLDWMC.Text });
            list.Add(new SqlParameter() { ParameterName = "@JLDWZZJGDM", Value = txtJLDWZZJGDM.Text });
            list.Add(new SqlParameter() { ParameterName = "@XMJLXM", Value = txtXMJLXM.Text });
            list.Add(new SqlParameter() { ParameterName = "@XMJLZJLX", Value = ddlXMJLZJLX.SelectedValue });
            list.Add(new SqlParameter() { ParameterName = "@XMJLZJLXMC", Value = ddlXMJLZJLX.SelectedItem.Text });
            list.Add(new SqlParameter() { ParameterName = "@XMJLZJHM", Value = txtXMJLZJHM.Text });
            list.Add(new SqlParameter() { ParameterName = "@ZJLGCSZXZ", Value = txtZJLGCSZXZ.Text });
            list.Add(new SqlParameter() { ParameterName = "@ZJLGCSZJLX", Value = ddlZJLGCSZJLX.SelectedValue });
            list.Add(new SqlParameter() { ParameterName = "@ZJLGCSZJLXMC", Value = ddlZJLGCSZJLX.SelectedItem.Text });
            list.Add(new SqlParameter() { ParameterName = "@ZJLGCSZJHM", Value = txtZJLGCSZJHM.Text });
            list.Add(new SqlParameter() { ParameterName = "@KCDWBM", Value = hfKCDWBM.Value });
            list.Add(new SqlParameter() { ParameterName = "@SJDWBM", Value = hfSJDWBM.Value });
            list.Add(new SqlParameter() { ParameterName = "@SGDWBM", Value = hfSGDWBM.Value });
            list.Add(new SqlParameter() { ParameterName = "@JLDWBM", Value = hfJLDWBM.Value });
            XMId = hfId.Value;
        }
        pageTool tool = new pageTool(this.Page, "txt");
        bool success = rc.PExcute(sql,list.ToArray());
        if (success)
        {
            hfId.Value = XMId;
            tool.showMessage("保存成功");
        }
        else
            tool.showMessage("保存失败");
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
            if (FIsApprove == "1" || IsShow == "1") //审核页面跳转
            {
                string ItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ID"));
                e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='#' onclick=\"Show('" + ItemId + "')\">查 看</a>";
            }
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
           
            pageTool tool = new pageTool(this.Page, "txt");
            string SGId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string sql = string.Format(@"delete from XM_SGXKZSGCYRY where Id='{0}'", SGId);
            bool success = rc.PExcute(sql);
            if (success)
            {
                tool.showMessage("删除成功");
                ShowInfo(hfId.Value);
            }
            else
                tool.showMessage("删除失败");
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ShowInfo(hfId.Value);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ShowInfo(hfId.Value);
    }
    protected void btnKCDW_Click(object sender, EventArgs e)
    {
        string QYBM = hfKCDWBM.Value;
        if (!string.IsNullOrEmpty(QYBM))
        {
            string sql = string.Format("select top 1* from QY_JBXX where QYBM='{0}'", QYBM);
            DataTable dt = rcJST.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtKCDWMC.Text = row["QYMC"].ToString();
                txtKCDWZZJGDM.Text = row["JGDM"].ToString();
            }
        }
    }
    protected void btnSJDW_Click(object sender, EventArgs e)
    {
        string QYBM = hfSJDWBM.Value;
        if (!string.IsNullOrEmpty(QYBM))
        {
            string sql = string.Format("select top 1* from QY_JBXX where QYBM='{0}'", QYBM);
            DataTable dt = rcJST.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtSJDWMC.Text = row["QYMC"].ToString();
                txtSJDWZZJGDM.Text = row["JGDM"].ToString();
            }
        }
    }
    protected void btnSGDW_Click(object sender, EventArgs e)
    {
        string QYBM = hfSGDWBM.Value;
        if (!string.IsNullOrEmpty(QYBM))
        {
            string sql = string.Format("select TOP 1 A.QYMC,A.JGDM,B.ZSBH from QY_JBXX A LEFT JOIN (SELECT * FROM QY_QYZSXX WHERE ZSLXBM='150') B ON A.QYBM=B.QYBM where A.QYBM='{0}'", QYBM);
            DataTable dt = rcJST.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtSGDWMC.Text = row["QYMC"].ToString();
                txtSGDWZZJGDM.Text = row["JGDM"].ToString();
                txtSGDWAQSCXKZ.Text = row["ZSBH"].ToString();
            }
        }
    }
    protected void btnJLDW_Click(object sender, EventArgs e)
    {
        string QYBM = hfJLDWBM.Value;
        if (!string.IsNullOrEmpty(QYBM))
        {
            string sql = string.Format("select top 1* from QY_JBXX where QYBM='{0}'", QYBM);
            DataTable dt = rcJST.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtJLDWMC.Text = row["QYMC"].ToString();
                txtJLDWZZJGDM.Text = row["JGDM"].ToString();
            }
        }
    }
    protected void btnZJL_Click(object sender, EventArgs e)
    {
        string RYBH = hfZJLRYBH.Value;
        if (!string.IsNullOrEmpty(RYBH))
        {
            string sql = string.Format(@"select top 1* from RY_RYZSXX where RYZSXXID='{0}'", RYBH);
            DataTable table = rcJST.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                txtZJLGCSZXZ.Text = row["XM"].ToString();
                txtZJLGCSZJHM.Text = row["SFZH"].ToString();
            }
        }
    }
    protected void btnXMJL_Click(object sender, EventArgs e)
    {
        string RYBH = hfXMJLRYBH.Value;
        if (!string.IsNullOrEmpty(RYBH))
        {
            string sql = string.Format(@"select top 1* from RY_RYZSXX where RYZSXXID='{0}'", RYBH);
            DataTable table = rcJST.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                txtXMJLXM.Text = row["XM"].ToString();
                txtXMJLZJHM.Text = row["SFZH"].ToString();
            }
        }
    }
}