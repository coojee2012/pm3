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

    string strpageid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string strusername = "",  strkeyvalue="";

            if (Request["username"] != null && !string.IsNullOrEmpty(Request["username"]))
            {
                strusername = Request["username"].ToString();
            }
            else
            {
                Fail();
                return;
            }

            if (Request["pageid"] != null && !string.IsNullOrEmpty(Request["pageid"]))
            {
                strpageid = Request["pageid"].ToString();
            }
            else
            {
                Fail();
                return;
            }

            strpageid = Request["pageid"].ToString();
            //根据不同method值，调用不同的方法。
            if (Request["method"] != null && !string.IsNullOrEmpty(Request["method"])) {
                strkeyvalue = Request["keyvalue"].ToString();  

                //验证
                if ((Request["method"].ToString() == "verify") && VerifyKey(strusername, strkeyvalue))
               {
                   //成功，跳转到指定页面
                   Response.Redirect("~/Government/AppZBBA/"+strpageid.Trim()+".aspx", true);
               }
               else
               {
                   Fail();
                   return;
               }
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "getLockId('" + strusername + "');", true);


            //strkeyvalue = LabKey.Value;

            //if (string.IsNullOrEmpty(strkeyvalue.Trim()))
            //{
            //    //成功，跳转到指定页面
            //    Fail();
            //}
            //else
            //{
            //    if (VerifyKey(strusername, strkeyvalue))
            //    {
            //        //成功，跳转到指定页面
            //        Response.Redirect("~/Government/AppZBBA/ZBWJBAList.aspx", true);
            //    }
            //    else
            //    {
            //        Fail();
            //        return;
            //    }
            //}
        }
    }

    public bool VerifyKey(string username,string keyvalue)
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
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "alert('必须传递合法的用户帐号！');", true);
     }
}
