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
using Approve.EntityBase;
using Approve.RuleCenter;
using ProjectData;

public partial class Admin_main_aTop : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            showInfo();
        }

    }

    private void showInfo()
    {
        string sql = @"select fnumber,fname from cf_sys_tree where     FIsShow=0 ";
        div_155001.Visible = div_155003.Visible = div_155005.Visible
               = div_155006.Visible = div_155007.Visible = div_155008.Visible=false;
        if (Session["SH_IsAdmin"] == null || Session["SH_IsAdmin"].ToString() != "1")//不是超级管理员
        {
            string froleId = sh.GetSignValue("select fMenuRoleId from cf_Sys_user where fid='" + Session["SH_UserID"] + "'");
            sql = @"select fnumber,fname from cf_sys_tree where fnumber in (select fcolnumber from CF_Sys_RoleRight where froleid='" + froleId + "' and ftype=1 and fisdeleted=0) and FIsShow=0 ";
      div_155001.Visible = div_155003.Visible = div_155005.Visible
               = div_155006.Visible = div_155007.Visible = div_155008.Visible
               = div_lmcd.Visible = div_xtcd.Visible = false;
        }
        DataTable dtTree = sh.GetTable(sql);

        if (dtTree != null && dtTree.Rows.Count > 0)
        {
            if (dtTree.Select("fname like '%用户管理%'").Length > 0)
                div_155001.Visible = true;
            if (dtTree.Select("fname like '%编码管理%'").Length > 0)
                div_155005.Visible = true;
            if (dtTree.Select("fname like '%标准管理%'").Length > 0)
                div_155006.Visible = true;
            if (dtTree.Select("fname like '%流程管理%'").Length > 0)
                div_155007.Visible = true;
            if (dtTree.Select("fname like '%建设资讯%'").Length > 0)
                div_155003.Visible = true;
            if (dtTree.Select("fname like '%日志管理%'").Length > 0)
                div_155008.Visible = true;
        }
        lDate.Text = "今天是：" + string.Format("{0:yyyy年MM月dd日}", DateTime.Now) + " " + DateTime.Now.DayOfWeek.ToString();
        string str = "";
        string UserId = EConvert.ToString(Session["SH_UserId"]);
        DataTable dt = sh.GetTable(EntityTypeEnum.EsUser, "FName,FMenuRoleId", "FID='" + UserId + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            str = dt.Rows[0]["FName"].ToString();
            if (EConvert.ToString(Session["SH_IsAdmin"]) != "1")
                str += "  角色：" + sh.GetSignValue("select FName from CF_Sys_MenuRole where Fnumber ='" + dt.Rows[0]["FMenuRoleId"] + "'");

        }
        lUserName.Text = str;

    }
    protected void bntExit_Click(object sender, EventArgs e)
    {
        string FName = sh.GetSignValue("select fname from cf_sys_user where fid='" + Session["SH_UserId"].ToString() + "'");
        //写入日志
        DataLog.Write(LogType.Info, LogSort.Safety, "安全退出系统", string.Format("用户[{0}]于[{1}]退出四川省勘察设计科技信息系统", FName + "(" + Session["SH_UserId"] + ")", DateTime.Now.ToString()));
        Session.Clear();
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>parent.close();</script>");
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
