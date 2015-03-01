using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.Common;
using Approve.RuleCenter;

public partial class WYDW_XMQK_LPBList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowInfo();
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        //if (e.Item.ItemIndex > -1)
        //{
        //    LinkButton btnOp = (LinkButton)e.Item.FindControl("btnItemdel");
        //    btnOp.Text = "删除";
        //    btnOp.CommandName = "Del";
        //    btnOp.Attributes.Add("onclick", "return confirm('确认要删除该楼幢?');");

        foreach (Control con in e.Item.Cells[3].Controls)
        {
            if (con.ToString() == "System.Web.UI.WebControls.DataGridLinkButton")
            {
                System.Web.UI.WebControls.LinkButton lbtn = (System.Web.UI.WebControls.LinkButton)con;
                lbtn.Attributes.Add("onclick", "return ShowLPBInfo('" + e.Item.Cells[4].Text + "')");
            }
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FId = e.Item.Cells[e.Item.Cells.Count - 1].Text;

            if (e.CommandName == "Del")
            {
                //删除申报信息和企业数据
                StringBuilder sb = new StringBuilder();
                sb.Append("delete YW_WY_XM_LZXX where FAppID='" + (string)Session["FAppId"] + "' and BuildId='" + FId + "'");
                rc.PExcute(sb.ToString());
                pageTool tool = new pageTool(this.Page);
                ShowInfo();
                tool.showMessage("删除成功！");
            }
        }
    }

    private void ShowInfo()
    {
        string sql = "select * from WY_XM_LZXX where XMBH='" + (string)Session["XMBH"] + "' order by BuildName desc";
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
}