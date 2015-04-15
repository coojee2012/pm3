using Approve.RuleCenter;
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
                Response.Redirect(string.Format("{0}{1}{2}", ReportServer, getFileName(fAppId), "&FAppId=" + fAppId));
            }
            else
            {
                fAppId = EConvert.ToString(Request.QueryString["FAppId"]);
                Response.Redirect(string.Format("{0}{1}{2}", ReportServer, getFileName(fAppId), "&FAppId=" + fAppId));
            }
        }
        

    }

    private string getFileName(string fAppId)
    {
        //EgovaDB db = new EgovaDB();
        string printType = Request.QueryString["printType"];
        return printType == "1" ? "SGXKZSLTZS.cpt" : "SGXKZBSLTZS.cpt";
        //var record = (from a in db.CF_App_ProcessInstance
        //             join b in db.CF_App_ProcessRecord
        //               on a.FID equals b.FProcessinstanceId
        //             where (a.FID == fAppId)
        //             select new {a.FState,a.FResult}).FirstOrDefault(); 
        //.Where(t => t.FAppId.Equals(fAppId)).FirstOrDefault();
        //if (!string.IsNullOrEmpty(record.FResult))
        //{
        //    受理通知书
        //    if (record.FResult.Equals(1))
        //    {
        //        return "SGXKZSLTZS.cpt";
        //    }
        //    不受理通知书
        //    if (record.FResult.Equals(3))
        //    {
        //        return "SGXKZBSLTZS.cpt";
        //    }
        //}
        //return "SGXKZBSLTZS.cpt";
    }
}