using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyYDGH_fileUpadload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var url = "FileAdd.aspx?typeId=" + TypeId + "&typeName=" +HttpUtility.UrlEncode(TypeName,System.Text.Encoding.Default) + "&FIsApprove=" + FIsApprove + "&GC_Id=" + GC_Id;
            ltrText.Text = "<iframe src=" + url + " width=\"100%\" name='fileupload' height=\"500\"></iframe>";
        }

    }
    private string TypeId
    {
        get
        {
            return Request.QueryString["typeId"];
        }
    }
    private string TypeName
    {
        get
        {
            return Request.QueryString["typeName"];
        }
    }
    private string FIsApprove
    {
        get
        {
            return Request.QueryString["FIsApprove"];
        }
    }
    private string GC_Id
    {
        get
        {
            return Request.QueryString["GC_Id"];
        }
    }
}