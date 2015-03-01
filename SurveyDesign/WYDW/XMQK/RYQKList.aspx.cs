using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using ProjectData;
using Tools;

public partial class WYDW_XMQK_RYQKList : WYPage
{
    RCenter rc = new RCenter(); Share sh = new Share();
    public ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckSession();
        if (!IsPostBack)
        {
            //conBind();
            showInfo();
        }
    }

    public void showInfo()
    {
        string sql = "select FId,XMBH,fPersonName,fSex,fCardType,fCardID,fPosition,fgrdh from WY_RY_JBXX where FBaseInfoID='" + CurrentEntUser.EntId + "' and XMBH='" + (string)Session["XMBH"] + "'";
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        //if (e.Item.ItemIndex > -1)
        //{
        //    string FId = e.Item.Cells[7].Text;
        //    if (e.CommandName == "See")
        //    {
        //        this.Session["RYID"] = FId;
        //        ClientScript.RegisterStartupScript(GetType(), "", "<script>ShowAppPage('RYQKInfo.aspx',900,650);</script>");
        //    }
        //}
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[1].Text = "<a href='#' onclick = \"ShowAppPage('RYQKInfo.aspx?FID=" + e.Item.Cells[7].Text + "','900','640')\">" + e.Item.Cells[1].Text + "</a>";
            string fpositionNum = e.Item.Cells[9].Text;
            //显示职务类型
            if (fpositionNum != "" && fpositionNum != "-1")
            {
                e.Item.Cells[5].Text = rc.GetSignValue("select fname from cf_sys_dic where fnumber=" + fpositionNum);
            }
            //显示证件类型
            string fCardTypeNum = e.Item.Cells[10].Text;
            if (fCardTypeNum != "" && fCardTypeNum != "-1")
            {
                e.Item.Cells[3].Text = rc.GetSignValue("select fname from cf_sys_dic where fnumber=" + fCardTypeNum);
            }
        }
    }
    protected void btnSXRY_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}