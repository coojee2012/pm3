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

public partial class Common_FFileShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                this.ShowFile(Request.QueryString["fid"]);
            }
        }
    }
    void ShowFile(string fid)
    {
        string sql = "select top 1 FFile from CF_Pub_Text where FId='" + fid + "'";
        object objValue = RPhoto.GetSignValue(sql);
        if (objValue != null && objValue != DBNull.Value)
        {
            byte[] buffer = (byte[])objValue;
            Response.BinaryWrite(buffer);
            Response.Flush();
        }
    }
}
