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
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using Seaskyer.Strings;
using Seaskyer.WebApp.Utility;
using System.Data.SqlClient;
public partial class OA_Talk_relationEmp : System.Web.UI.Page
{
    OA oa = new OA();
    protected void Page_Load(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (!IsPostBack)
        {
            if (this.Request["fid"] != null)
            {
                showinfo();
                if (this.Request["NOone"] != null && this.Request["NOone"].ToString() == "1")
                {
                    this.t_toEmp.Visible = false;
                    this.Label1.Visible = true;
                }
                else
                {
                    showEmp(this.Request["fid"].ToString());
                }
            }
        }
    }
    private void showinfo()
    {
        DataTable dt = oa.GetTable(EntityTypeEnum.ETalkManage, "ftalkname,case ftalkstate when 'State01' then '草稿箱' when 'State02' then '讨论中' when 'State03' then '已中止' end statename ", "fid='" + Request["fid"] + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            this.Label_Name.Text = dt.Rows[0]["ftalkname"].ToString();
            this.Label_FState.Text = dt.Rows[0]["statename"].ToString();
        }
    }
    private void showEmp(string talkID)
    {
        string empID = "";
        DataTable dt = oa.GetTable(EntityTypeEnum.ETalkRelation, "(select fname from cf_oa_emp where fid=fempid) fempname", "ftalkid='" + talkID + "'");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (empID.Length > 0)
            {
                empID += ",";
            }
            empID += dt.Rows[i]["fempname"].ToString();
        }
        this.t_toEmp.Text = empID;
    }
}
