using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;

public partial class Share_Sys_CardNoSelect : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
            ShowInfo();
        }
    }


    private void ControlBind()
    {
        //批次
        DataTable dt = sh.getBatchTable("666");
        t_FBatchId.DataSource = dt;
        t_FBatchId.DataTextField = "FName";
        t_FBatchId.DataValueField = "FID";
        t_FBatchId.DataBind();
        t_FBatchId.Items.Insert(0, new ListItem("请选择", ""));
    }

    //条件
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FLockLabelNumber.Text.Trim()))
        {
            sb.Append(" and FLockLabelNumber like '%" + t_FLockLabelNumber.Text.Trim() + "%'");
        }
        if (!string.IsNullOrEmpty(t_FBatchId.SelectedValue))
        {
            sb.Append(" and FBatchId = '" + t_FBatchId.SelectedValue + "'");
        }
        if (!string.IsNullOrEmpty(t_FBatchId.SelectedValue))
        {
            sb.Append(" and FBatchId = '" + t_FBatchId.SelectedValue + "'");
        }
        return sb.ToString();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * from CF_Sys_Lock where fisdeleted=0  and fstate=0 ");//fstate=0：未使用的
        sb.Append(GetCon());
        sb.Append(" Order By FBatchId ");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 10;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }




    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FBatchId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBatchId"));
            e.Item.Cells[3].Text = sh.getBatchName(FBatchId);
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "cnSelect")
        {
            string fId = e.CommandArgument.ToString();
            this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>returnValue='" + fId + "';window.close();</script>");
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

}
