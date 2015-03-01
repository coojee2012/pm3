using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Approve.EntityBase;

public partial class Government_maintable_t2 : System.Web.UI.UserControl
{
    public string DeptNumber = "";
    DataTable dtUser = null;
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        showInfo();
    }


    private void showInfo()
    {
        lit_Title.Text = rc.GetSignValue("select FName from cf_sys_systemName where FNumber=@FNumber", new SqlParameter("@FNumber", Request.QueryString["sysId"]));

        if (!string.IsNullOrEmpty(Request.QueryString["DeptId"]))
        {
            DeptNumber = Request.QueryString["DeptId"];
        }
        else
        {
            DeptNumber = EConvert.ToString(Session["DFId"]);
        }
        if (!string.IsNullOrEmpty(DeptNumber))
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select u.FID,convert(varchar,u.FManageDeptId) FManageDeptId,r.FSystemId,r.fstate  ");
            sb.Append("from CF_Sys_User u,CF_Sys_UserRight r ");
            sb.Append("where u.FID=r.FUserId and u.ftype=2 and u.FManageDeptId like'" + DeptNumber + "%' ");
            sb.Append("and r.FSystemId='" + Request.QueryString["sysId"] + "'");
            dtUser = rc.GetTable(sb.ToString());
        }
    }
}
