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
public partial class Share_Sys_CardNoPubList : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            ControlBind();
            ShowInfo();
        }
    }


    private void ControlBind()
    {
        //批次
        StringBuilder sb = new StringBuilder();
        sb.Append("select FId,FNo FName ");
        sb.Append("From CF_Sys_UserLockInfo ");
        sb.Append("Where FIsDeleted=0 ");
        sb.Append("Order By FDate Desc");
        DataTable dt = sh.GetTable(sb.ToString());
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
        if (!string.IsNullOrEmpty(t_FLockNumber.Text.Trim()))
        {
            sb.Append(" and flocknumber like '%" + t_FLockNumber.Text.Trim() + "%'");
        }
        if (!string.IsNullOrEmpty(t_FBatchId.SelectedValue))
        {
            sb.Append(" and FBatchId = '" + t_FBatchId.SelectedValue + "'");
        }
        if (!string.IsNullOrEmpty(t_FState.SelectedValue))
        {
            sb.Append(" and FState = '" + t_FState.SelectedValue + "'");
        }
        return sb.ToString();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * from CF_Sys_Lock where fisdeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" Order By FBatchId,Flocklabelnumber ");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }



    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FBatchId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBatchId"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));

            e.Item.Cells[4].Text = sh.getBatchName(FBatchId);
            e.Item.Cells[5].Text = FState == "0" ? "未分配" : FState == "1" ? "<font color='red'>已分配</font>" : "";
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(this.DG_List, EntityTypeEnum.SHsLock, "dbShare");
        ShowInfo();
    }

}
