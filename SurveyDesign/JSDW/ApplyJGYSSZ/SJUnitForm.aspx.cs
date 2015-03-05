using Approve.Common;
using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyJGYS_SJUnitForm : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfId.Value = Id;
            ShowInfo();
            EnabledControl();
        }
    }
    private void EnabledControl()
    {
        if (Audit == "1" || FIsApprove == "1")
        {
            foreach (Control control in this.form1.Controls)
            {
                WebHelper.SetControlEnabled(control);
            }
        }
    }
    private void ShowInfo()
    {
        string sql = string.Format(@"select * from YW_COMPANY where JGYS_ID='{0}' and TypeId=5", Id);
        DataTable table = rc.GetTable(sql);
        DG_List.DataSource = table;
        DG_List.DataBind();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
            if (FIsApprove == "1")
            {
                e.Item.Cells[e.Item.Cells.Count - 2].Text = "";
            }
        }
    }
    private string Id
    {
        get
        {
            return Request.QueryString["JG_Id"];
        }
    }
    private string Audit {
        get { return Request.QueryString["audit"]; }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            string companyId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string sql = string.Format(@"delete from YW_COMPANY where ID='{0}';", companyId);
            sql += string.Format("delete from YW_PERSON where COMPANYID='{0}'", companyId);
            pageTool tool = new pageTool(this.Page, "txt");
            bool success = rc.PExcute(sql);
            if (success)
            {
                tool.showMessage("删除成功");
                ShowInfo();
            }
            else
                tool.showMessage("删除失败");
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ShowInfo();
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