using System;
using System.Web;
using System.Web.UI;
using Approve.RuleCenter;
using System.Text;
using System.Data;

public partial class Common_ValidatePayUser : System.Web.UI.UserControl
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hfFUserId.Value = Convert.ToString(Session["Pay_FUserId"]);
            hfFCompany.Value = Server.UrlEncode(string.Format("{0}", Session["Pay_FName"]));
            DataTable dt = rc.GetTable("select * from cf_sys_user where fid='" + Session["DFUserId"].ToString() + "'");
            if (dt.Rows.Count > 0 && dt != null)
            {
                string Department = rc.GetSignValue(" select FName from CF_Sys_Department where FNumber=" + EConvert.ToInt(dt.Rows[0]["FDepartmentID"]));
                hfFCompany.Value = " 当前用户：" + dt.Rows[0]["FLinkMan"];
            }
        }
    }
    public bool ValidateUserId()
    {
        if (this.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.Form[hfFUserId.UniqueID]))
            {
                if (Request.Form[hfFUserId.UniqueID] != Convert.ToString(Session["Pay_FUserId"]))
                {
                    return false;
                }
            }
        }
        return true;
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (!ValidateUserId())
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", string.Format("alert('有新的用户进来了，窗口\\n({0})\\n必须退出！点确定按钮之后将强制退出。');parent.close();", HttpUtility.UrlDecode(hfFCompany.Value, Encoding.Default)), true);
        }
    }
}
