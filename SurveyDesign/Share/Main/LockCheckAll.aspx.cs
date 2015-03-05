using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.EntityCenter;
using Approve.EntityBase;
using Approve.Common;
using System.Data.SqlClient;

public partial class Share_Main_LockCheckAll : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string str = "";
            string code = Request.QueryString["UserID"];
            string[] strArray = SecurityEncryption.DesDecrypt(code, "32165498").Split('|');
            if (strArray.Length == 2)
            {
                if (EConvert.ToInt(strArray[1]) > SecurityEncryption.ConvertDateTimeInt(DateTime.Now))
                    str = strArray[0];
            }

            GetLogIn(str);
        }
    }

    //登陆
    private void GetLogIn(string UserID)
    {

        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select FID,FType,FName,FPassWord,FMenuRoleId,FManageDeptId ");
        sb.Append("from cf_sys_user ");
        sb.Append("where fid=@FID ");
        DataTable dt = sh.GetTable(sb.ToString(), new SqlParameter("@FID", UserID));
        if (dt != null && dt.Rows.Count > 0)
        {

            string FType = dt.Rows[0]["ftype"].ToString();//用户类型
            #region

            if (FType == "6")  // 系统管理员
            {
                Session["SH_UserID"] = dt.Rows[0]["FId"].ToString();
                Session["FUserId"] = dt.Rows[0]["FId"].ToString();
                Session["SH_IsAdmin"] = 0;
                //this.Response.Redirect("aIndex.aspx");
                //增加老项目管理登录 ljr 2014-11-25
                string fname = dt.Rows[0]["FName"].ToString();
                fname = System.Web.HttpUtility.UrlEncode(fname);
                string pass = dt.Rows[0]["FPassWord"].ToString();
                pass = SecurityEncryption.DESEncrypt(pass);
                pass = System.Web.HttpUtility.UrlEncode(pass);
                string web =System.Configuration.ConfigurationSettings.AppSettings["dbPage"];
                this.Response.Redirect(web + "?U=" + fname + "&W=" + pass + "&S=12");
                //this.Response.Redirect("http://localhost:8111/NJSWeb/Login_Tran.aspx?U=" + fname + "&W=" + pass + "&S=12");
            }
            else if (FType == "0")//超级管理员
            {
                Session["SH_UserID"] = dt.Rows[0]["FId"].ToString();
                Session["Admin_FID"] = dt.Rows[0]["FId"].ToString();
                Session["SH_IsAdmin"] = 1;
                this.Response.Redirect("aIndex.aspx");
            }
            else if (FType == "100") //交换资源管理用户
            {
                Session["FType"] = FType;
                Session["DFUserName"] = dt.Rows[0]["FName"].ToString();
                Session["DFUserId"] = dt.Rows[0]["FId"].ToString();
                Session["DFMenuRoleId"] = dt.Rows[0]["FMenuRoleId"].ToString();
                Session["DFId"] = dt.Rows[0]["FManageDeptId"].ToString();
                this.Response.Redirect("../../DataStandards/main/aIndex.aspx");
            }
            #endregion

        }
        else
        {
            this.Response.Write("<script>window.history.go(-1);</script>");
        }
    }

    #region
    public string sUrl
    {
        get { return this.HFUrl.Text; }
    }

    public DateTime sEndTime
    {
        get { return EConvert.ToDateTime(this.HFEndTime.Text); }
    }
    //判断字符串是否是数字
    public bool IsNumeric(string str)
    {

        bool isNum = true;
        char[] chars = str.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if (!Char.IsNumber(chars[i]))
                isNum = false;
        }
        return isNum;
    }
    #endregion


}
