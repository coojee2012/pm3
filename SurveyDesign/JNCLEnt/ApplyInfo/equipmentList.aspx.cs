using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Approve.Common;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class JNCLEnt_mangeInfo_equipment : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["FAppId"] != null && !string.IsNullOrEmpty(Session["FAppId"].ToString()))
            { t_fappid.Value = Session["FAppId"].ToString(); }
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
            ShowInfo();
        }
    }
    private void ShowInfo()
    {
        string sql = "select * from YW_JN_AppEquipment where fappid='" + t_fappid.Value + "'  and type='0' order by ftime desc ";
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        sl.Add("YW_JN_AppEquipment", "FID");
        tool.DelInfoFromGrid(this.DG_List, sl, "dbShare"); ShowInfo();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string SBMC = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "SBMC"));
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('editEquipment.aspx?fid=" + FID + "',800,400);\" >" + SBMC + "</a>";
        }
    }
    public void readOnly() 
    {
        btnDel.Enabled = false; Submit1.Attributes.Add("disabled", "disabled");
    }
}