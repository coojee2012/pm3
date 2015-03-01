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

public partial class Government_AppMain_blank : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fMenuRole = EConvert.ToString(Session["DFMenuRoleId"]);
        //判断显示统计数据的菜单
        RCenter rc = new RCenter();
        string sysObjRole = rc.GetSysObjectContent("_Sys_Manager_Menu");
        bool flag = false;
        if (!string.IsNullOrEmpty(sysObjRole))
        {
            string[] tags = sysObjRole.Split(',');
            for (int i = 0; i < tags.Length; i++)
            {
                if (tags[i].Trim() == fMenuRole.Trim())
                {
                    flag = true;
                    break;
                }
            }
        }
        if (flag)
        {
            //跳转到另外的页面了
            Server.Transfer("blankInfo.aspx");
        }
        else
        {
            
        }
    }
}
