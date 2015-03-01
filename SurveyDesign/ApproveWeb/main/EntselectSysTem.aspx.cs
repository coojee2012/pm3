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
using Approve.Common;
using Approve.EntityBase;
using Approve.RuleCenter;
using System.Text;
using System.Data.SqlClient;
using ProjectBLL;
using Approve.RuleApp;
using ProjectData;
public partial class ApproveWeb_Main_EntselectSysTem : System.Web.UI.Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
        }
    }
    private void ControlBind()
    {
        this.liC_TechSupport.Text = ComFunction.GetValueByName("TechSupport");//技术支持
        this.liC_webtel.Text = ComFunction.GetValueByName("WebTel");//电话
        this.liC_Developer.Text = ComFunction.GetValueByName("Developer");//开发单位


        string userID = "";
        string code = Request.QueryString["key"];
        string[] strArray = SecurityEncryption.DesDecrypt(code, "32165498").Split('|');
        if (strArray.Length == 2)
        {
            if (EConvert.ToInt(strArray[1]) > SecurityEncryption.ConvertDateTimeInt(DateTime.Now))
                userID = strArray[0];
        }

        if (string.IsNullOrEmpty(userID))
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        SortedList slP = new SortedList();
        sb.Append("select r.fid,s.fname,s.FShareKey,s.FLUrl,s.FNumber,u.FType,u.FBaseInfoId,u.FCompany ");
        sb.Append("from cf_sys_systemname s,cf_sys_userright r,cf_Sys_User u ");
        sb.Append("where r.fsystemid=s.fnumber and s.fisdeleted=0 and r.FUserId=u.FID ");
        sb.Append("and r.fUserId=@userID ");
        string rightID = Request.QueryString["rightID"];
        if (!string.IsNullOrEmpty(rightID))
        {
            sb.Append(" and r.FId=@rightID");
            slP.Add("rightID", rightID);
        }
        sb.Append(" order by forder ");
        slP.Add("userID", userID);
        DataTable dt = rc.GetTable(sb.ToString(), new RApp().ConvertParameters(slP));
        if (dt != null && dt.Rows.Count > 0)
        {
            string FCompany = dt.Rows[0]["FCompany"].ToString();
            string fType = dt.Rows[0]["FType"].ToString();
            string fRightId = dt.Rows[0]["FId"].ToString();
            if (fType == "1")//主管部门
            {
                //登录地址
                string fLUrl = dt.Rows[0]["FLUrl"].ToString();
                string fShareKey = dt.Rows[0]["FShareKey"].ToString();
                string fsysId = dt.Rows[0]["FNumber"].ToString();
                if (!string.IsNullOrEmpty(fLUrl))
                {
                    DateTime time = DateTime.Now.AddHours(3);
                    if (!(!string.IsNullOrEmpty(fShareKey) && fShareKey.Length == 8))
                        fShareKey = "12345678";
                    string key = SecurityEncryption.DesEncrypt(fRightId + "|" + SecurityEncryption.ConvertDateTimeInt(time), fShareKey);
                    fLUrl = string.Format(fLUrl + "?UserRightID={0}&sysId={1}", key, fsysId);
                    Response.Redirect(fLUrl);//登录到管理部门
                }
                else
                    Response.Write("<script>alert('登录地址无效!');window.close();</script>");
            }
            else if (fType == "2")//企业
            {
                #region  记录登陆日志
                StringBuilder ss = new StringBuilder();
                ss.Append("\n企业：" + FCompany);
                ss.Append("\nUserID：" + userID);
                ss.Append("\nIP：" + Request.UserHostAddress);
                ss.Append("\n时间：" + DateTime.Now);
                ss.Append("\n事件：通过后台登陆");
                DataLog.Write(LogType.EntLoginBack, LogSort.Safety, "统一认证主帐户登陆", ss.ToString(), userID);
                #endregion

                CurrentEntUser.UserId = userID;
                CurrentEntUser.EntId = dt.Rows[0]["FBaseInfoId"].ToString();
                dt = rc.GetTable("select r.fId,n.FName,n.fNumber from cf_Sys_Userright r inner join cf_Sys_SystemName n on r.FSystemId=n.FNumber where r.FUserId='" + userID + "' order by fnumber");
                dbSystem.DataSource = dt;
                dbSystem.DataTextField = "FName";
                dbSystem.DataValueField = "fId";
                dbSystem.DataBind();
                if (dt != null && dt.Rows.Count == 1)
                {
                    Login();
                }
            }
        }
        if (!string.IsNullOrEmpty(rightID))
        {
            dbSystem.SelectedIndex = dbSystem.Items.IndexOf(dbSystem.Items.FindByValue(rightID));
            Login();
        }
    }

    private void Login()
    {
        string fId = dbSystem.SelectedValue;
        if (!string.IsNullOrEmpty(fId))
        {
            string fsysId = rc.GetSignValue("select FSystemId from cf_Sys_Userright where fid='" + fId + "'");
            Response.Redirect(getUrlCode(fsysId, fId));
        }
        else
        {
            pageTool tool = new pageTool(this.Page);
            tool.showMessage("您目前还没有该系统使用权限！");
            return;
        }
    }
    /// <summary>
    /// 得到链接登陆地址
    /// </summary>
    /// <param name="dr"></param>
    /// <returns></returns>
    private string getUrlCode(string FSystemId, string rFID)
    {
        string str = "";
        string FID = rFID;
        //得到配置的地址
        string Url = rc.getSystemLoginURL(FSystemId);
        if (!string.IsNullOrEmpty(Url))
        {
            try
            {
                UriBuilder uri = new UriBuilder(Url);
                if (uri.Host == "localhost")
                {
                    uri.Host = Request.Url.Host;
                }

                Url = uri.ToString();
            }
            catch (Exception ex)
            { }
            DateTime time = DateTime.Now.AddHours(1);
            string key = SecurityEncryption.DesEncrypt(FID + "|" + SecurityEncryption.ConvertDateTimeInt(time), rc.getSystemKey(FSystemId));
            str = Url + "?sysId=" + FSystemId + "&UserRightID=" + HttpUtility.UrlEncode(key, Encoding.UTF8)+"&fhuid="+Request.QueryString["fhuid"];
        }
        return str;
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        Login();
    }
}
