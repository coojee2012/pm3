﻿using System;
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
      

        //清空多余session
        Session["FCanMod"] = null;
        Session["FAppId"] = null;
        Session["FManageTypeId"] = null;
        Session["fly"] = null;
    }
}
