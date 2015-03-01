using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.EntityBase;
using System.Drawing;
using System.Linq;
using ProjectData;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Collections;

public partial class audit_XM_XMList : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowInfo();
        }
    }
    private void ShowInfo()
    {
        string sql = "select * from JKC_V_view where isnull(jsgm,'')<>'' and isnull(XMZTZ,0)<>0 ";
        if (Request.QueryString["FNumber"] != null && !string.IsNullOrEmpty(Request.QueryString["FNumber"]))
        { sql += " and FNumber like '" + Request.QueryString["FNumber"] + "%' "; }
        sql += " order by spCou desc,cou desc ";
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string XMBH = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "XMBH"));
            string XMMC = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "gcmc"));
            string spid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "spid"));
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            //传入项目名称 业务编码 摄像头型号
            //e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='javascript:void(0)' onclick=\"javascript:window.open('ipVideo.aspx?XMMC=" + XMMC + "&YWBM=" + XMBH + "');\" >视频</a>";
            if (!string.IsNullOrEmpty(spid) && spid != "&nbsp;")
                e.Item.Cells[e.Item.Cells.Count - 3].Text = "<a href='http://" + spid + "' target='_blank' >视频</a>";

            e.Item.Cells[1].Text = "<a href=\"javascript:showAddWindow('xmDetailFrameset.aspx?XMBH=" + e.Item.Cells[e.Item.Cells.Count - 2].Text + "',900,600);\" >" + e.Item.Cells[1].Text + "</a>";
        }
    }

}