using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WYDW_XMQK_aBottom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.liC_TechSupport.Text = ComFunction.GetValueByName("TechSupport");//技术支持
            this.liC_webtel.Text = ComFunction.GetValueByName("WebTel");//电话
            this.liC_Developer.Text = ComFunction.GetValueByName("Developer");//开发单位
        }
    }
}