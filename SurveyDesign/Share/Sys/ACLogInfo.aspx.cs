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

public partial class Share_Sys_ACLogInfo : System.Web.UI.Page
{
    Share rb = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (!String.IsNullOrEmpty(Request.QueryString["FID"]))
            {
                string fid = Request.QueryString["FID"];
                
                string sql = "SELECT * FROM CF_Com_News WHERE FID='" + fid + "'";

                DataTable dtLogs = rb.GetTable(sql);

                if (dtLogs.Rows.Count > 0)
                {                
                    txt_Title.Text = EConvert.ToString(dtLogs.Rows[0]["Title"]);
                    
                    txt_LogType.Text=getLogType(EConvert.ToInt(dtLogs.Rows[0]["FLogType"]));
                    txt_errmsg.Text = EConvert.ToString(dtLogs.Rows[0]["errmsg"]);
                    txt_Content.Text = EConvert.ToString(dtLogs.Rows[0]["content"]);
                    
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
}
