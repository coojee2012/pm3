using System;
using Approve.RuleCenter;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ProjectData;
using System.Linq;
using Tools;

public partial class Share_WebSide_RegJSDWUserSuccess : System.Web.UI.Page
{
    Share sh = new Share();
    public string img = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.liC_TechSupport.Text = ComFunction.GetValueByName("TechSupport");//技术支持
            this.liC_webtel.Text = ComFunction.GetValueByName("WebTel");//电话 
            showInfo();
        }
    }

    private void showInfo()
    {
        ProjectDB db = new ProjectDB();
        string FID = "";
        string Key = Request.QueryString["Key"];
        if (!string.IsNullOrEmpty(Key))
        {
            Key = Key.Replace("%20", "+");
            string[] strArray = SecurityEncryption.DesDecrypt(Key, "32165498").Split('|');
            if (strArray.Length == 2)
            {
                if (EConvert.ToInt(strArray[1]) > SecurityEncryption.ConvertDateTimeInt(DateTime.Now))
                    FID = strArray[0];
            }

            if (!string.IsNullOrEmpty(FID))
            {


                pageTool tool = new pageTool(this.Page);
                CF_User_Reg reg = db.CF_User_Reg.Where(t => t.FID == FID).FirstOrDefault();
                if (reg != null)
                {
                    tool.fillPageControl(reg);
                    t_FManageDeptName.Text = sh.getDept(t_FManageDeptId.Value, 1);
                    t_FPassWord.Text = SecurityEncryption.DESDecrypt(reg.FPassWord);//解密


                    //开通的权限
                    string str = "";
                    var v = from t in db.CF_User_RegRight
                            join s in db.CF_Sys_SystemName on t.FSystemId equals s.FNumber
                            where t.FRegId == reg.FID
                            select new { s.FName };
                    foreach (var t in v)
                    {
                        if (!string.IsNullOrEmpty(str))
                            str += "<br/>";
                        str += t.FName;
                    }
                    t_FSysList.Text = str;
                }
            }
            else
            {
                Response.Clear();
                Response.Write("页面已过期");
                Response.End();
            }
        }
        else
        {
            Response.Clear();
            Response.Write("页面已过期");
            Response.End();
        }
    }

    protected void btnREG_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegJSDWUser.aspx");
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("JSDWJGLogin.aspx");
    }
}
