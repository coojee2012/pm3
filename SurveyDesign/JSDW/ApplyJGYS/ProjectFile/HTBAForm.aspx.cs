using Approve.Common;
using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyJGYS_ProjectFile_HTBAForm : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private RCenter rcJST = new RCenter("JST_XZSPBaseInfo");
    private ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            if (!string.IsNullOrEmpty(BAId))//编辑
            {
                string sql = string.Format("select top 1 * from XM_HTBA where ID='{0}'", BAId);
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                    hfId.Value = row["ID"].ToString();
                    ddlHTLB.SelectedValue = row["HTLB"].ToString();
                }
            }
            else {
                string sql = string.Format("select top 1 XMBH,JSGM from YW_JGYS where ID='{0}'", YWBM);
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
        ddlHTLB.DataSource = db.getDicList(511);
        ddlHTLB.DataTextField = "FName";
        ddlHTLB.DataValueField = "FNumber";
        ddlHTLB.DataBind();
        ddlHTLB.Items.Insert(0, new ListItem("请选择", ""));
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = string.Empty;
        string XMId = string.Empty;
        if (string.IsNullOrEmpty(hfId.Value))//新增
        {
            XMId = Guid.NewGuid().ToString();
            sql = string.Format(@"INSERT INTO [dbCenter].[dbo].[XM_HTBA]
                                       ([ID]
                                       ,[YWBM]
                                       ,[HTBABM]
                                       ,[HTBABH]
                                       ,[XMBH]
                                       ,[HTBH]
                                       ,[HTLB]
                                       ,[HTJE]
                                       ,[JSGM]
                                       ,[HTQDRQ]
                                       ,[FBDWMC]
                                       ,[FBDWZZJGDM]
                                       ,[CBDWMC]
                                       ,[CBDWZZJGDM]
                                       ,[LHTCBDWMC]
                                       ,[LHTCBDWZZJGDM],[HTLBMC])
                                 VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')", XMId, YWBM, "NEWID()", txtHTBABH.Text, txtXMBH.Text, txtHTBH.Text, ddlHTLB.SelectedValue, txtHTJE.Text, txtJSGM.Text, txtHTQDRQ.Text, txtFBDWMC.Text, txtFBDWZZJGDM.Text, txtCBDWMC.Text, txtCBDWZZJGDM.Text, txtLHTCBDWMC.Text, txtLHTCBDWZZJGDM.Text, ddlHTLB.SelectedItem.Text);
        }
        else {
            sql = string.Format(@"UPDATE [dbCenter].[dbo].[XM_HTBA]
                                       SET [HTBABH] = '{0}'
                                          ,[XMBH] = '{1}'
                                          ,[HTBH] = '{2}'
                                          ,[HTLB] = '{3}'
                                          ,[HTJE] = '{4}'
                                          ,[JSGM] = '{5}'
                                          ,[HTQDRQ] = '{6}'
                                          ,[FBDWMC] = '{7}'
                                          ,[FBDWZZJGDM] = '{8}'
                                          ,[CBDWMC] = '{9}'
                                          ,[CBDWZZJGDM] = '{10}'
                                          ,[LHTCBDWMC] = '{11}'
                                          ,[LHTCBDWZZJGDM] =  '{12}'
                                          ,[HTLBMC]='{13}'
                                     WHERE ID = '{14}'", txtHTBABH.Text, txtXMBH.Text, txtHTBH.Text, ddlHTLB.SelectedValue, txtHTJE.Text, txtJSGM.Text, txtHTQDRQ.Text, txtFBDWMC.Text, txtFBDWZZJGDM.Text, txtCBDWMC.Text, txtCBDWZZJGDM.Text, txtLHTCBDWMC.Text, txtLHTCBDWZZJGDM.Text, ddlHTLB.SelectedItem.Text, BAId);
            XMId = hfId.Value;
        }
        pageTool tool = new pageTool(this.Page, "txt");
        bool success = rc.PExcute(sql);
        if (success)
        {
            hfId.Value = XMId;
            tool.showMessage("保存成功");
           // tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';window.close();");
        }
        else
            tool.showMessage("保存失败");
    }
    private string YWBM
    {
        get
        {
            return Request.QueryString["JG_Id"];
        }
    }
    private string BAId {
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
        get { return Request.QueryString["IsShow"]; }
    }
    protected void btnFBDW_Click(object sender, EventArgs e)
    {
        string QYBM = hfFBDWQYBM.Value;
        if (!string.IsNullOrEmpty(QYBM))
        {
            string sql = string.Format("select top 1* from QY_JBXX where QYBM='{0}'", QYBM);
            DataTable dt = rcJST.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtFBDWMC.Text = row["QYMC"].ToString();
                txtFBDWZZJGDM.Text = row["JGDM"].ToString();
            }
        }
    }
    protected void btnCBDW_Click(object sender, EventArgs e)
    {
        string QYBM = hfCBDWQYBM.Value;
        if (!string.IsNullOrEmpty(QYBM))
        {
            string sql = string.Format("select top 1* from QY_JBXX where QYBM='{0}'", QYBM);
            DataTable dt = rcJST.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtCBDWMC.Text = row["QYMC"].ToString();
                txtCBDWZZJGDM.Text = row["JGDM"].ToString();
            }
        }
    }
    protected void btnLHTCBDW_Click(object sender, EventArgs e)
    {
        string QYBM = hfLHTQYBM.Value;
        if (!string.IsNullOrEmpty(QYBM))
        {
            string sql = string.Format("select top 1* from QY_JBXX where QYBM='{0}'", QYBM);
            DataTable dt = rcJST.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtLHTCBDWMC.Text = row["QYMC"].ToString();
                txtLHTCBDWZZJGDM.Text = row["JGDM"].ToString();
            }
        }
    }
}