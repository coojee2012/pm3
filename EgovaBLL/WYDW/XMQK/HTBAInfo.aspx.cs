using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;

public partial class WYDW_XMQK_HTBAInfo : System.Web.UI.Page
{
    public string UploadFiles = "";
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        showInfo();
        ShwoFiles();
    }
    private void showInfo()
    {
        string Fid = Request.QueryString["FID"];
        if (!string.IsNullOrEmpty(Fid))
        {
            string strsql = "select * from WY_XM_HTBA where FID='" + Fid + "'";
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
            }
        }
        else
        {
            this.RegisterStartupScript(new Guid().ToString(), "<script>alert('数据加载失败！');parent.location.href='../main/Index.aspx';</script>");
        }
    }

    private void ShwoFiles()
    {
        //string Fid = Request.QueryString["FID"];
        //if (Fid != "")
        //{
        //    string strsql = "select FFileName,FFileUrl from WY_FileList where FLinkid='" + Fid + "'";

        //}
        StringBuilder str = new StringBuilder();
        str.Append("<a href='#'>合同备案文件</a>");
        UploadFiles = str.ToString();
    }
}