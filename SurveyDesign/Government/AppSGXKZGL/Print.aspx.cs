﻿using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using Tools;

public partial class Gov_AppSGXKZGL_Print : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string ReportServer = rc.GetSysObjectContent("_ReportServer");
            string fAppId = "";
            if (string.IsNullOrEmpty(ReportServer))
            {
                //本地部署的服务
                ReportServer = "http://" + Request.Url.Host + ":8080/WebReport/ReportServer?reportlet=";
            }
            if (string.IsNullOrEmpty(Request.QueryString["FAppId"]))
            {
                fAppId = EConvert.ToString(Session["FAppId"]);
                Response.Redirect(string.Format("{0}{1}{2}", ReportServer, getFileName(fAppId), "&YWBM=" + fAppId));
            }
            else
            {
                fAppId = EConvert.ToString(Request.QueryString["FAppId"]);
                Response.Redirect(string.Format("{0}{1}{2}", ReportServer, getFileName(fAppId), "&YWBM=" + fAppId));
            }
        }
        

    }

    private string getFileName(string fAppId)
    {
        //EgovaDB db = new EgovaDB();
        string printType = Request.QueryString["printType"];
        return printType == "1" ? "SGXKZSLTZS.cpt" : "SGXKZBSLTZS.cpt";
    }
}