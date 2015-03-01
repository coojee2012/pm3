using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Data;
using System.Text;
using Approve.RuleCenter;
public partial class Government_EmpData_EmpTJ : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            ShowEmpInfo();
            
        }
    }
    void ShowEmpInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select t1.FName,t2.FCount,t1.FNumber ");
        sb.Append("from cf_Sys_Dic t1 left join (");
        sb.Append("select FPersonTypeId FType,count(*) FCount ");
        sb.Append("from CF_Emp_BaseInfo ");
        sb.Append("where 1=1 ");
        sb.Append("group by FPersonTypeId) t2 on t1.FNumber=t2.FType ");
        sb.Append("and isnull(t2.FType,'0')!='0' ");
        sb.Append("where t1.FParentId='123' and t2.fcount>0 order by t1.FOrder,t1.FNumber");
        DataTable dt = rc.GetTable(sb.ToString());
        sb.Remove(0, sb.Length);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append("<tr>");
            for (int ii = 0; ii < 2; ii++)
            {
                if (ii > 0)
                {
                    if (!(i < dt.Rows.Count - 1))
                        break;
                    else
                        i++;
                }
                sb.Append("<td class='t_c t_bg'>" + dt.Rows[i][0] + "</td>");
                int iCount = EConvert.ToInt(dt.Rows[i][1]);
                if (iCount > 0)
                    sb.Append("<td class='t_c'><b><a href=\"javascript:showAddWindow('Baseinfo2.aspx?fbid=&fType=" + dt.Rows[i][2] + "',900,600);\">" + iCount + "</a></b></td>");
                else
                    sb.Append("<td class='t_c'>0</td>");
            }
            sb.Append("</tr>");
        }
        litEmp.Text = sb.ToString();
    }
}
