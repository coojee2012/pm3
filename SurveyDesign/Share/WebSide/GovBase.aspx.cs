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
    readonly RCenter _rc = new RCenter();

    string _strpageid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string strusername = "";

            if (!string.IsNullOrEmpty(Request["username"]))
            {
                strusername = Request["username"].ToString();
            }
            else
            {
                Fail();
                return;
            }
            //得到需要跳转的页面
            if (!string.IsNullOrEmpty(Request["pageid"]))
            {
                _strpageid = Request["pageid"].ToString();
            }
            else
            {
                Fail();
                return;
            }

     
            //根据不同method值，调用不同的方法。
            if (!string.IsNullOrEmpty(Request["method"]))
            {
                var strkeyvalue=Request["keyvalue"].ToString();

                //验证
                if ((Request["method"].ToString() == "verify") && VerifyKey(strusername, strkeyvalue))
                {
                    Response.Write("true");
                    Response.End();
                }
                else
                {
                    Response.Write("false");
                    Response.End();
                }
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "mygetLockId('" + strusername + "','" + _strpageid + "');", true);
           
        }
    }

    public bool VerifyKey(string username,string keyvalue)
    {
        string getnumber = "";
        username = HttpUtility.UrlDecode(username, Encoding.GetEncoding("utf-8"));
        StringBuilder sb = new StringBuilder();
        DataTable dt = new DataTable();
        sb.Remove(0, sb.Length);
        sb.Append(" select fType as sType,FManageDeptID,froleid,flocknumber as sReadLock from Linker_dbCenterSC.dbCenterSC.DBO.cf_sys_user ");
        sb.Append(" where fname='" + username + "'");
        dt = _rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            getnumber  = dt.Rows[0]["sReadLock"].ToString();
        }

        if (keyvalue.Trim() == getnumber.Trim() && getnumber.Trim().Length > 0)
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
