using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using ProjectData;
using Tools;

public partial class WYDW_XMQK_RYQKList : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    public ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //conBind();
            showInfo();
        }
    }

    public void showInfo()
    {
        if (Session["XMQK_XMBH"] != null)
        {
            string sql = "select FId,XMBH,fPersonName,fSex,fCardType,fCardID,fPosition,fgrdh from WY_RY_JBXX where FBaseInfoID='" + CurrentEntUser.EntId + "' and XMBH='" + (string)Session["XMQK_XMBH"] + "'";
            this.Pager1.className = "dbShare";
            this.Pager1.sql = sql;
            this.Pager1.pagecount = 20;
            this.Pager1.controltopage = "DG_List";
            this.Pager1.controltype = "DataGrid";
            this.Pager1.dataBind();
        }

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FId = e.Item.Cells[7].Text;
            if (e.CommandName == "See")
            {
                this.Session["RYID"] = FId;
                ClientScript.RegisterStartupScript(GetType(), "", "<script>ShowAppPage('RYQKInfo.aspx',900,650);</script>");
            }
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
        }
    }
    protected void btnSXRY_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}