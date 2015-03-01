using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;

public partial class WYDW_XMQK_HTBAInfo : WYPage
{
    public string filecount = "0";
    public string UploadFiles = "";
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckSession();
        showInfo();
        ShwoFiles();
        getFilecount();
    }
    private void showInfo()
    {
        //string Fid = Request.QueryString["FID"];
        string strsql = "select * from WY_XM_HTBA where XMBH='" + (string)Session["XMBH"] + "'";
        DataTable dt = new DataTable();
        dt = rc.GetTable(strsql);
        if (dt != null && dt.Rows.Count > 0)
        {
            ViewState["FID"] = dt.Rows[0]["FID"].ToString();
            t_HTBH.Text = dt.Rows[0]["HTBH"].ToString();
            t_WTDW.Text = dt.Rows[0]["WTDW"].ToString();
            t_XMHQFS.SelectedValue = dt.Rows[0]["XMHQFS"].ToString();
            t_JGRQ.Text = DateTime.Parse(dt.Rows[0]["JGRQ"].ToString()).ToString("yyyy-MM-dd");
            t_HTQDRQ.Text = DateTime.Parse(dt.Rows[0]["HTQDRQ"].ToString()).ToString("yyyy-MM-dd");
            t_HTSXRQ.Text = DateTime.Parse(dt.Rows[0]["HTSXRQ"].ToString()).ToString("yyyy-MM-dd");
            t_HTJZRQ.Text = DateTime.Parse(dt.Rows[0]["HTJZRQ"].ToString()).ToString("yyyy-MM-dd");
            hidFId.Value = dt.Rows[0]["FLinkID"].ToString();
        }

    }

    private void ShwoFiles()
    {
        //if (Session["XMBH"] != null)
        //{
        //    StringBuilder str = new StringBuilder();
        //    string strsql = "select f.FFileName,f.FSize,f.FFileUrl from WY_FileList f,WY_XM_HTBA h where h.FLinkID=f.FLinkid and h.XMBH='" + (string)Session["XMBH"] + "'";
        //    DataTable dt = new DataTable();
        //    dt = rc.GetTable(strsql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            str.Append("<a href='" + dt.Rows[i]["FFileUrl"] + "'>" + dt.Rows[i]["FFileName"] + "," + dt.Rows[i]["FSize"] + "&nbsp;bytes;</a><p>");
        //        }
        //    }
        //    else
        //    {
        //        str.Append("<a href='##'>无备案文件</a>");
        //    }
        //    UploadFiles = str.ToString();
        //}
    }
    private void getFilecount()
    {
        string strsql = "select COUNT(f.FFileName) as FileCount from WY_FileList f,WY_XM_HTBA h where h.FLinkID=f.FLinkid and h.XMBH='" + (string)Session["XMBH"] + "'";
        DataTable dt = rc.GetTable(strsql);
        if (dt != null && dt.Rows.Count > 0)
        {
            filecount = dt.Rows[0]["FileCount"].ToString();
            hidfilecount.Value = filecount;
            UpdateBtn.Value = "查看文件(" + filecount + ")";
        }
    }
}