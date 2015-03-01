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
using Approve.RuleCenter;
using System.Text;
using Approve.EntityBase;
using System.Linq;
using System.Collections.Generic;
using ProjectData;



public partial class Admin_main_aTop : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["DFUserId"] != null)
            {
                DataTable dt = rc.GetTable("select * from cf_sys_user where fid='" + Session["DFUserId"].ToString() + "'");
                if (dt.Rows.Count > 0 && dt != null)
                {
                    string Department = rc.GetSignValue(" select FName from CF_Sys_Department where FNumber=" + EConvert.ToInt(dt.Rows[0]["FDepartmentID"]));
                    this.lab_User.Text = " 当前用户：" + dt.Rows[0]["FLinkMan"]
                        + "<a  href='javascript:void(0)' onclick=\"showAddWindow('../NewAppMain/ManagerEdit.aspx?fid=" + Session["DFUserId"] + "',580,380);\">[编辑]</a>&nbsp;&nbsp;&nbsp;单位：" +
                      Department + "&nbsp;&nbsp;&nbsp;职务：" + dt.Rows[0]["FFunction"];

                

                }
            }
            lDate.Text = "今天是：" + string.Format("{0:yyyy年MM月dd日}", DateTime.Now) + " " + DateTime.Now.DayOfWeek.ToString();
            showMenu();

            //菜单角色
            string DFMenuRoleId = String.IsNullOrEmpty(EConvert.ToString(Session["DFMenuRoleId"])) ? "1" : EConvert.ToString(Session["DFMenuRoleId"]);

            if ("1030".Split(',').Count(t => DFMenuRoleId.Split(',').Contains(t)) > 0)
            {//厅长

            }


        }

    }

    private void showMenu()
    {
        string DFMenuRoleId = String.IsNullOrEmpty(EConvert.ToString(Session["DFMenuRoleId"])) ? "1" : EConvert.ToString(Session["DFMenuRoleId"]);
        StringBuilder sb = new StringBuilder();
        sb.Append("select fnumber ");
        sb.Append("from cf_sys_role ");
        sb.Append("where fnumber in (" + DFMenuRoleId + ")");
        DataTable dtRole = rc.GetTable(sb.ToString());

        sb.Remove(0, sb.Length);
        sb.Append("select FID,froleid,FNumber,FName,FPicName,FOrder,FUrl,FQurl,FTarget ");
        sb.Append("from cf_sys_menu ");
        sb.Append("where fparentid = '450' and flevel=2 and FIsDis=1 and FNumber<>'8050' ");
        DataTable dtMenu = rc.GetTable(sb.ToString());

        string str = "";
        for (int i = 0; i < dtRole.Rows.Count; i++)
        {
            if (!string.IsNullOrEmpty(str))
                str += " or ";
            str += "froleid like '%" + dtRole.Rows[i]["FNumber"].ToString() + "%'";
        }

        var v = dtMenu.Select(string.IsNullOrEmpty(str) ? "1=2" : str).AsEnumerable().Select(t => new { FName = t["FName"], FPicName = t["FPicName"], FOrder = t["FOrder"], FUrl = t["FUrl"], FQurl = t["FQurl"], fnumber = t["fnumber"], FTarget = t["FTarget"] }).OrderBy(t => t.FOrder);
        re_Menu.DataSource = v;
        re_Menu.DataBind();


        //综合查询子菜单45029 

        if (v.Count(t => t.fnumber.ToString().Trim() == "45029") > 0)
        {
            ProjectDB db = new ProjectDB();
            var vMenu = db.Menu.Where(t => t.FParentId == "45029" && t.FIsDis == 1).OrderBy(t => t.FOrder);
            string s = "<div id=\"bar_45029\" style='display:none;'>";
            foreach (var m in vMenu)
            {
                if (s.Length > 30)
                    s += "<tt></tt>";
                s += "<a showID='subA' style='font-weight:bold;float:left;' href='" + m.FUrl + "' target='" + (string.IsNullOrEmpty(m.FTarget) ? "main" : m.FTarget) + "'  qurl='" + m.FQUrl + "' >" + m.FName + "</a> ";
            }
            s += "</div>";
            lit_Bar.Text = s;
        }
    }

    /// <summary>
    /// 安全退出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void bntExit_Click(object sender, EventArgs e)
    {
        Session["FType"] = null;
        Session["DFSystemId"] = null;
        Session["DFName"] = null;
        Session["DFUserId"] = null;
        Session["DFUserRightId"] = null;
        Session["DFRoleId"] = null;
        Session["DFMenuRoleId"] =null;
        Session["DFId"] = null;
        Session["DFLevel"] = null;
        Session["DFIsTown"] = null;
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>top.close();</script>");
    }

}
