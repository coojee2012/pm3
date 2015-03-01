using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Text;
using Tools;

public partial class WYDW_ApplyLPB_LPBList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //synchronous();
            ShowInfo();
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            LinkButton btnOp = (LinkButton)e.Item.FindControl("btnItemdel");
            btnOp.Text = "删除";
            btnOp.CommandName = "Del";
            btnOp.Attributes.Add("onclick", "return confirm('确认要删除该楼幢?');");

            foreach (Control con in e.Item.Cells[3].Controls)
            {
                if (con.ToString() == "System.Web.UI.WebControls.DataGridLinkButton")
                {
                    System.Web.UI.WebControls.LinkButton lbtn = (System.Web.UI.WebControls.LinkButton)con;
                    lbtn.Attributes.Add("onclick", "return ShowLPBInfo('" + e.Item.Cells[5].Text + "','NEW')");
                }
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
        string FBaseinfoID = CurrentEntUser.EntId;
        string sql = "select * from YW_WY_XM_LZXX where FAppID='" + (string)Session["FAppId"] + "' order by BuildName desc";
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
        string sql2 = "select l.* from WY_XM_LZXX l,YW_WY_XM_JBXX wl where l.XMBH=wl.XMBH and wl.FAppID='" + (string)Session["FAppId"] + "' order by l.BuildName desc";
        this.Pager2.className = "dbShare";
        this.Pager2.sql = sql2;
        this.Pager2.pagecount = 20;
        this.Pager2.controltopage = "DG_GDList";
        this.Pager2.controltype = "DataGrid";
        this.Pager2.dataBind();
    }

    //同步归档表数据
    //private void synchronous()
    //{

    //    string fappid = (string)Session["FAppId"];

    //    string strsql2 = "select Fid from YW_WY_XM_LZXX where FAppID='" + fappid + "'";
    //    DataTable dtlzxx = new DataTable();
    //    dtlzxx = rc.GetTable(strsql2);
    //    if (dtlzxx != null && dtlzxx.Rows.Count > 0) { }
    //    else
    //    {
    //        StringBuilder str = new StringBuilder();
    //        string strsql =
    //            "select lz.XMBH,lz.BuildId from WY_XM_LZXX lz,YW_WY_XM_JBXX j where lz.XMBH=j.XMBH and J.FAppID='" +
    //            fappid + "'";
    //        DataTable dt = new DataTable();
    //        dt = rc.GetTable(strsql);
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            str.Append("Insert into YW_WY_XM_LZXX(BuildId,FAppID,BuildName,ZTS,ZJZMJ,XMBH,FBaseInfoID,FID) ");
    //            str.Append("Select BuildId,'" + fappid + "',BuildName,ZTS,ZJZMJ,XMBH,FBaseInfoID,NEWID() ");
    //            str.Append("From WY_XM_LZXX where XMBH='" + dt.Rows[0]["XMBH"] + "'");

    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                str.Append(";");
    //                str.Append("Insert into YW_WY_XM_FWXX(HouseId,BuildId,FAppID,ZH,FH,DY,RHC,SH,JZMJ,FID) ");
    //                str.Append("Select HouseId,BuildId,'" + fappid + "',ZH,FH,DY,RHC,SH,JZMJ,NEWID() ");
    //                str.Append("From WY_XM_FWXX where BuildId='" + dt.Rows[i]["BuildId"] + "'");
    //            }
    //            rc.PExcute(str.ToString(), true);
    //        }
    //    }
    //}
    protected void DG_GDList_ItemCommand(object source, DataGridCommandEventArgs e)
    {

    }
    protected void DG_GDList_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            foreach (Control con in e.Item.Cells[3].Controls)
            {
                if (con.ToString() == "System.Web.UI.WebControls.DataGridLinkButton")
                {
                    System.Web.UI.WebControls.LinkButton lbtn = (System.Web.UI.WebControls.LinkButton)con;
                    lbtn.Attributes.Add("onclick", "return ShowLPBInfo('" + e.Item.Cells[4].Text + "','OLD')");
                }
            }
        }
    }
}