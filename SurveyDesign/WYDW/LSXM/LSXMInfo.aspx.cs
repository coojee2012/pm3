using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WYDW_LSXM_LSXMInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }
    protected void showInfo()
    {
        DataTable dt = new DataTable();
        if (Request.QueryString["Fid"] != null)
        {
            string fid = Request.QueryString["Fid"].ToString();
            string sql = "";
            if (Request.QueryString["type"] == "3")
            {
                sql = "select * from WY_XM_JBXX where fid='" + fid + "'";
            }
            else
            {
                sql = "select * from wy_xm_jbxx_history where fid='" + fid + "'";
            }
            dt = rc.GetTable(sql);


            if (dt != null && dt.Rows.Count > 0)
            {
                t_ProjectName.Text = objectToString(dt.Rows[0]["XMMC"]);
                t_Address.Text = dt.Rows[0]["XMDZ"].ToString();

                if (objectToString(dt.Rows[0]["XMSD"]).Trim() != "")
                {
                    t_XMSD.Text = rc.GetSignValue("select FName from cf_sys_managedept where fnumber=" + objectToString(dt.Rows[0]["XMSD"]));
                }
                t_JSDW.Text = objectToString(dt.Rows[0]["JSDW"]);
                t_JSDWDM.Text = objectToString(dt.Rows[0]["JSDWZZJFDM"]);
                t_JSDWDZ.Text = objectToString(dt.Rows[0]["JSDWDZ"]);

                t_Contacts.Text = objectToString(dt.Rows[0]["JSDWFR"]);
                t_Mobile.Text = objectToString(dt.Rows[0]["JSDWFRDH"]);
                t_JSDWJSFZR.Text = objectToString(dt.Rows[0]["JSDWJSFZR"]);
                t_JSDWJSFZRZC.Text = objectToString(dt.Rows[0]["JSDWJSFZRZC"]);
                t_JSDWJSFZRDH.Text = objectToString(dt.Rows[0]["JSDWJSFZRDH"]);

                string xmzlx = objectToString(dt.Rows[0]["XMZLX"]);
                string jsxz = objectToString(dt.Rows[0]["JSXZ"]);
                string jsms = objectToString(dt.Rows[0]["JSMS"]);
                if (xmzlx != "") { t_XMZLX.Text = rc.GetSignValue("select fname from cf_sys_dic where fnumber=" + xmzlx); }
                if (jsxz != "") { t_JSXZ.Text = rc.GetSignValue("select fname from cf_sys_dic where fnumber=" + jsxz); }
                if (jsms != "") { t_JSMS.Text = rc.GetSignValue("select fname from cf_sys_dic where fnumber=" + jsms); }

                t_XMZTZ.Text = objectToString(dt.Rows[0]["XMZTZ"]);
                t_JSGM.Text = objectToString(dt.Rows[0]["JSGM"]);
                t_JSNR.Text = objectToString(dt.Rows[0]["JSNR"]);
            }
        }
    }

    //将对象转化成字符串
    protected string objectToString(object obj)
    {
        string result = "";
        if (obj != null && obj.ToString() != "")
        {
            result = obj.ToString();
        }
        return result;
    }
}