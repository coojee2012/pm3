using System;
using System.Web;
using System.Web.UI;
using Approve.EntityBase;
using Approve.RuleCenter;
using System.Text;
using System.Data.SqlClient;
using ProjectBLL;
using System.Linq;
using ProjectData;


public partial class Admin_main_aTop : System.Web.UI.Page
{
    RCenter rc = new RCenter();
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
        liter_FBaseName.Text = CurrentEmpUser.Name;

        li_Logs.Text = "所属单位：" + CurrentEmpUser.EntName;



        lDate.Text = "今天是：" + string.Format("{0:yyyy年MM月dd日}", DateTime.Now) + " " + DateTime.Now.DayOfWeek.ToString();
        lLockInfo.Text += "" + "<a href='../../helpDoc/四川省勘察设计科技信息系统--勘察单位版.pdf' target='_blank' >操作说明下载</a>";


        //#region 登陆日志

        //string str = "历史登陆次数：" + RBase.getLoginTimes(CurrentEntUser.EntUserId) + "次 ";
        //DateTime LastLoginDate = RBase.getLastLoginDate(CurrentEntUser.EntUserId);
        //str += LastLoginDate != DateTime.MinValue ? ("，上次登陆时间：" + LastLoginDate) : "";
        //li_Logs.Text = str;

        //#endregion

        string FID = EConvert.ToString(Session["FUserId"]);
        string UserRightID = EConvert.ToString(Session["FUserRightId"]);


        #region 返回选择系统链接

        string selectURL = rc.GetSysObjectContent("_sys_Ent_ShareLoginSelect");//选择系统页面。
        DateTime time = DateTime.Now.AddHours(3);
        string keyLog = SecurityEncryption.DesEncrypt(FID + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
        keyLog = keyLog.Replace("+", "%20");
        a5.HRef = selectURL + "?userID=" + HttpUtility.UrlEncode(keyLog, Encoding.UTF8) + "&e=" + (new Random()).Next();

        #endregion


        switch (CurrentEntUser.SystemId)
        {
            case "199":
                div_BG.Attributes.Add("class", "top_backimgJN");
                break;
        }

    }


    protected void bntExit_Click(object sender, EventArgs e)
    {
        Session["FBaseId"] = null;
        Session["FType"] = null;
        Session["FUserId"] = null;
        Session["FUserRightId"] = null;
        Session["FMenuRoleId"] = null;
        Session["FSystemId"] = null;
        Session["FBaseName"] = null;



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