using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// PagerBasePager 的摘要说明
/// </summary>
public class PagerBasePager : System.Web.UI.UserControl
{
    public static string _sScriptName = "";
    public static string _sSessionName = "";
    public static string _sSessionValue = "";
    public PagerBasePager()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CehckSession();
    }
    private void CehckSession()
    {

        if (Session[sSessionName] != null && Session[sSessionName].ToString() == sSessionValue)
        {
            this.Page.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>try{document.body.onload=function(){" + sScriptName + "();}}catch(err){}</script>");
        }
    }
    public string sScriptName
    {
        get { return _sScriptName; }
        set { _sScriptName = value; }
    }
    public string sSessionName
    {
        get { return _sSessionName; }
        set { _sSessionName = value; }
    }
    public string sSessionValue
    {
        get { return _sSessionValue; }
        set { _sSessionValue = value; }
    }
}
