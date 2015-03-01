using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Approve.RuleBase;
using Approve.RuleCenter;
using Approve.EntityBase;



public partial class Admin_MainOther_LogInfo : System.Web.UI.Page
{
    Share rb = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        //RBase rb=new RCenter();        

        if (!IsPostBack)
        {

            if(!String.IsNullOrEmpty(Request.QueryString["FID"]))
            {
                string fid=Request.QueryString["FID"];


                string sql = "SELECT * FROM CF_Sys_Log WHERE FID='"+fid+"'";

                DataTable dtLogs = rb.GetTable(sql);

                if (dtLogs.Rows.Count > 0)
                {
                    txt_Explorer.Text = EConvert.ToString(dtLogs.Rows[0]["FBrowserType"]);
                    txt_Info.Text = EConvert.ToString(dtLogs.Rows[0]["FDescription"]);
                    txt_Ip.Text = EConvert.ToString(dtLogs.Rows[0]["FIPAddress"]);
                    txt_LogSort.Text = getOperation(EConvert.ToInt(dtLogs.Rows[0]["FOperation"]));
                    txt_LogTime.Text = EConvert.ToString(dtLogs.Rows[0]["FLogTime"]);
                    txt_LogTitle.Text = EConvert.ToString(dtLogs.Rows[0]["FTitle"]);
                    txt_LogType.Text = getLogType(EConvert.ToInt(dtLogs.Rows[0]["FLogType"]));
                    txt_Operator.Text = EConvert.ToString(dtLogs.Rows[0]["FOperator"]);
                    txt_ServerName.Text = EConvert.ToString(dtLogs.Rows[0]["FServerName"]);
                    txt_Link.Text = EConvert.ToString(dtLogs.Rows[0]["FDirectory"]);
                    txt_SystemType.Text = EConvert.ToString(dtLogs.Rows[0]["FSystemType"]);
                    //txt_Info.Enabled = false;
                    //所属系统
                    getSysCode(EConvert.ToString(dtLogs.Rows[0]["FSysCode"]));
                }
            }
        }
    }


    private string getLogType(int? tid)
    {
        switch (tid)
        {
            case 1:
                return "信息日志";
            case 2:
                return "错误日志";
            case 3:
                return "警告日志";
        }
        return "";
    }

    private string getOperation(int? tid)
    {
        switch (tid)
        {

            case 1:
                return "系统";
            case 2:
                return "安全";
            case 3:
                return "应用";
            case 4:
                return "操作";
        }
        return "";
    }

    private string getSysCode(string code)
    {
        if (!string.IsNullOrEmpty(code))
        {
           string sysName = rb.GetSignValue("select fname from cf_sys_systemname where fnumber="+code);
           if (!string.IsNullOrEmpty(sysName))
           {
               this.txt_SysName.Text = sysName;
           }
           else
           {
               sysName = rb.GetSignValue("select fname from cf_sys_platform where fnumber="+code);
               if (!string.IsNullOrEmpty(sysName))
                   this.txt_SysName.Text = sysName;
           }
        }
        return "";
    }

}
