using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using ProjectBLL;

public partial class Enterprise_main_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (CurrentEntUser.SystemId)
        {
            case "199":
                this.Title = "建筑节能新材料认定管理系统";
                break;
        }

        //清空多余session
        Session["FCanMod"] = null;
        Session["FAppId"] = null;
        Session["FManageTypeId"] = null;
        Session["fly"] = null;
    }
}
