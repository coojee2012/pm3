using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;

public partial class WYDW_main_aTop : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            showInfo();
            a5.Visible = EConvert.ToInt(Session["FromMKey"]) == 1;//主账户可返回
        }
    }
    private void showInfo()
    {
        ProjectDB db = new ProjectDB();
        liter_FBaseName.Text = CurrentEntUser.EntName;
        li_UserType.Text = " 用户类型：" + db.getSystemName(CurrentEntUser.SystemId);


        lDate.Text = "今天是：" + string.Format("{0:yyyy年MM月dd日}", DateTime.Now) + " " + DateTime.Now.DayOfWeek.ToString();
        lLockInfo.Text += "" + "<a href='../../GFEnt/四川省施工企业工法申报管理信息系统操作手册_企业用户.doc' target='_blank' >“操作手册”下载</a>";

        string FID = EConvert.ToString(Session["FUserId"]);
        string UserRightID = EConvert.ToString(Session["FUserRightId"]);


        #region 返回选择系统链接

        string selectURL = db.getSysObjectContent("_sys_Ent_ShareLoginSelect");//选择系统页面。
        DateTime time = DateTime.Now.AddHours(3);
        string keyLog = SecurityEncryption.DesEncrypt(FID + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
        keyLog = keyLog.Replace("+", "%20");
        a5.HRef = selectURL + "?userID=" + HttpUtility.UrlEncode(keyLog, Encoding.UTF8) + "&e=" + (new Random()).Next();

        #endregion



    }


    protected void bntExit_Click(object sender, EventArgs e)
    {
        Session.Remove("FBaseId");
        Session.Remove("FType");
        Session.Remove("FUserId");
        Session.Remove("FUserRightId");
        Session.Remove("FMenuRoleId");
        Session.Remove("FRoleId");
        Session.Remove("FSystemId");
        Session.Remove("FBaseName");
        Session.Remove("FIsApprove");
        Session.Remove("FCanMod");
        Session.Remove("FAppId");
        Session.Remove("FBaseinfoId");
        Session.Remove("EntUserId");
        Session.Remove("FManageTypeId");
        Session.Remove("fly");
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>top.close();</script>");

    }

    private string getTimeStr(DateTime dTime)
    {
        string str = "";
        int h = dTime.Hour;
        if (h > 18)
            str = "晚上好";
        else if (h > 13)
            str = "下午好";
        else if (h > 12)
            str = "中午好";
        else if (h > 9)
            str = "上午好";
        else if (h > 0)
            str = "早上好";
        return str;
    }
}