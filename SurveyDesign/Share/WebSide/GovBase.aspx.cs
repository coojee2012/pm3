using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using Approve.RuleCenter;
using System.Web.UI.WebControls;

public partial class Share_Main_GovBase : System.Web.UI.Page
{
    RCenter rc = new RCenter();

    protected void Page_Load(object sender, EventArgs e)
    {
        string strusername = "" ,strkeyvalue = "";

        if (Request["username"] != null && !string.IsNullOrEmpty(Request["username"]))
        {
           strusername = Request["username"].ToString();
        }
        else
        {
           Fail();
           return; 
        }

        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "getLockId();alert('完成');", true);

        strkeyvalue = LabKey.Value;

        if (string.IsNullOrEmpty(strkeyvalue.Trim()))
        {
            //成功，跳转到指定页面
            Fail();
        }
        else
        {
            if (VerifyKey(strusername, strkeyvalue))
            {
                //成功，跳转到指定页面
                Response.Redirect("~/Government/AppZBBA/ZBWJBAList.aspx", true);
            }
            else
            {
                Fail();
                return;
            }
        }
    }

    private bool VerifyKey(string username,string keyvalue)
    {
        string getnumber = "";
        StringBuilder sb = new StringBuilder();
        DataTable dt = new DataTable();
        sb.Remove(0, sb.Length);
        sb.Append(" select fType as sType,FManageDeptID,froleid,flocknumber as sReadLock from Linker_dbCenterSC.dbCenterSC.DBO.cf_sys_user ");
        sb.Append(" where fname='" + username + "'");
        dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            getnumber  = dt.Rows[0]["sReadLock"].ToString();
        }

        if (getnumber != null && keyvalue.Trim() == getnumber.Trim() && getnumber.Trim().Length > 0)
        {
            return true;
        }
        else
        { 
            return false; 
        }
    }

    private void Fail()
    {
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('必须传递合法的用户帐号！');", true);
     }
}
