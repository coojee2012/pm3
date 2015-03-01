using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ApproveWeb_main_HTBA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }

    private void showInfo()
    {
        //合同备案跳转地址专用。

        string url = "";
        string sysid = Request.QueryString["sysid"];
        switch (sysid)
        {
            case "15501"://勘察
                url = "../../KC/AppMain/aIndex.aspx?";
                break;
            case "145"://施工图
                url = "../../KcsjSgt/AppMain/aIndex.aspx?";
                break;
            case "155"://设计
                url = "../../SJ/AppMain/aIndex.aspx?";
                break;
            case "126"://见证
                url = "../../JZDW/AppMain/aIndex.aspx?";
                break;
        }

        Response.Redirect(url + Request.QueryString.ToString());
    }
}
