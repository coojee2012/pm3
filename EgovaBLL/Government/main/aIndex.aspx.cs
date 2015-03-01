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
using System.Data.SqlClient;
using Approve.RuleCenter;
using System.Text;
using System.Linq;
using System.Collections.Specialized;
using System.Collections.Generic;
using ProjectData;
using Approve.Common;

public partial class Government_AppMain_aIndex : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfTime.Value = DateTime.Now.ToString();
            showInfo();


            //if (!string.IsNullOrEmpty(liMessage.Text))
            //{
            //    pageTool tools = new pageTool(this);
            //    tools.ExecuteScript("showMSG('','有新信息');");
            //}
        }


    }

    private void showInfo()
    {
        //查业务 
        //统计待审批事项
        string DFMenuRoleId = String.IsNullOrEmpty(EConvert.ToString(Session["DFMenuRoleId"])) ? "1" : EConvert.ToString(Session["DFMenuRoleId"]);
        StringBuilder sb = new StringBuilder();
        List<string> roleList = DFMenuRoleId.Split(',').ToList();
        string message = "";
        var result1 =
      db.Menu.Where(t => t.FIsDis == 1 && t.FParentId == "45002").Union
      (
      from t in db.Menu
      join t1 in db.Menu on t.FNumber equals t1.FParentId
      where t1.FIsDis == 1 && t1.FIsDis == 1 && t.FParentId == "45002"
      select t1)
      .Union(
          from t in db.Menu
          join t1 in db.Menu on t.FNumber equals t1.FParentId
          join t2 in db.Menu on t1.FNumber equals t2.FParentId
          where t1.FIsDis == 1 && t1.FIsDis == 1 && t.FParentId == "45002"
          select t2
      )
      ;
        int iCount = 0;
        var result =
        from t in result1
        where t.FIsDeleted == false && t.FIsDis == 1
        //&& (t.FUrl.Contains("SGseeAppList.aspx") || t.FUrl.Contains("seeAppList.aspx"))
              && roleList.Any(x => t.FRoleId.Split(',').Contains(x))
        group t by t.FUrl into g
        select new { FName = g.Max(m => m.FName), FNumber = g.Max(m => m.FNumber), FUrl = g.Key };
        var AddressList = new[] { new { Name = "", Count = 0, FNumber = "", FUrl = "" } }.Where(t => false).ToList();
        foreach (var item in result)
        {
            Uri baseUri = Request.Url;
            Uri absoluteUri = new Uri(baseUri, item.FUrl);
            NameValueCollection col = HttpUtility.ParseQueryString(absoluteUri.Query);

            int icount = result.Count();
            sb.Remove(0, sb.Length);
            sb.Append("select count(*) from (");
            sb.Append("select count(*)fcount from cf_App_ProcessInstance ep ");
            sb.Append(" inner join cf_App_ProcessRecord er ");
            sb.Append(" on ep.FId=er.FProcessInstanceId and ep.FState=1 ");
            sb.Append(" and er.FRoleId in (" + Session["DFRoleId"] + ") ");
            sb.Append(" and ep.FManageDeptId like '" + Session["DFId"] + "%' ");
            sb.Append(" and ep.FSystemId  =" + EConvert.ToInt(col["fsystemid"]));
            sb.Append(" and er.FMeasure in (0,1,4) and er.ftypeid=1 group by ");
            sb.Append(" ep.FLinkId ");
            sb.Append(")t");
            int count = rc.GetSQLCount(sb.ToString());
            if (count > 0)
            {
                iCount += count;
                AddressList.Add(new { Name = item.FName, Count = count, item.FNumber, item.FUrl });
            }
        }


        if (iCount > 0)
        {
            //if (AddressList.GroupBy(t => t.FUrl).Count() == 1)
            //{
            //    liMessage.Text = "<a href=\"" + AddressList.FirstOrDefault().FUrl + "\"    target='main' title='点击进入办理'>";
            //}
            message += "<tt><b>" + iCount.ToString() + "</b></tt>个待受理事项";
            //if (AddressList.GroupBy(t => t.FUrl).Count() == 1)
            //{
            //    liMessage.Text += "</a>";
            //}
            message += " <br />";
        }
        iCount = 0;

        result = from t in result1
                 where t.FIsDeleted == false && t.FIsDis == 1 
                 //&& (t.FUrl.Contains("SGwaitForAppList.aspx") || t.FUrl.Contains("waitForAppList.aspx"))
                       && roleList.Any(x => t.FRoleId.Split(',').Contains(x))
                 group t by t.FUrl.ToLower() into g
                 select new { FName = g.Max(m => m.FName), FNumber = g.Max(m => m.FNumber), FUrl = g.Key };

        AddressList = new[] { new { Name = "", Count = 0, FNumber = "", FUrl = "" } }.Where(t => false).ToList();
        sb.Remove(0, sb.Length);
        int i = 0;
        foreach (var item in result)
        {
            Uri baseUri = Request.Url;
            Uri absoluteUri = new Uri(baseUri, item.FUrl);
            NameValueCollection col = HttpUtility.ParseQueryString(absoluteUri.Query);

            int icount = result.Count();


            sb.Append("select ep.FLinkId  from cf_App_ProcessInstance ep ");
            sb.Append(" inner join cf_App_ProcessRecord er ");
           sb.Append(" on ep.FId=er.FProcessInstanceId and ep.FState=1 ");
            sb.Append(" and er.FRoleId in (" + Session["DFRoleId"] + ") ");
            sb.Append(" and ep.FManageDeptId like '" + Session["DFId"] + "%' ");
            if (!string.IsNullOrEmpty(col["fsystemid"]))
            {
                sb.Append(" and (ep.FSystemId  =" + EConvert.ToInt(col["fsystemid"]));
                if (!string.IsNullOrEmpty(col["fmanagetypeid"]))
                {
                    string[] strManange = col["FManageTypeId".ToLower()].Split(',');
                    for (int j = 0; j < strManange.Length; j++)
                    {
                        sb.Append(" or ep.fmanagetypeid  =" + EConvert.ToInt(strManange[j]));
                    }
                }
                sb.AppendLine(" )");
            }

            sb.Append(" and er.FMeasure in (0,1,4) and er.ftypeid=" + EConvert.ToInt(col["ftypeid"]) + " group by ");
            sb.AppendLine(" ep.FLinkId ");
            if (result.Count() > 1 && result.Count() - 1 > i)
            {
                sb.AppendLine(" union ");
            }
            i++;
        }
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null)
        {
            iCount = dt.Rows.Count;
        }
        //if (count > 0)
        //{
        //    iCount += count;
        //    AddressList.Add(new { Name = item.FName, Count = count, item.FNumber, item.FUrl });
        //}

        if (iCount > 0)
        {
            //if (AddressList.GroupBy(t=>t.FUrl).Count() == 1)
            //{
            //    liMessage.Text = "<a href=\"" + AddressList.FirstOrDefault().FUrl + "\"    target='main' title='点击进入办理'>";
            //}
            message += "<div><tt><b>" + iCount.ToString() + "</b></tt>个待审批事项</div>";
            //if (AddressList.GroupBy(t => t.FUrl).Count() == 1)
            //{
            //    liMessage.Text += "</a>";
            //}
        }
        if (!string.IsNullOrEmpty(message) && liMessage.Text != message)
        {
            hfTime.Value = DateTime.Now.ToString();
            liMessage.Text = message;
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "test", "showMSG('','有新信息');", true);

        }
        else
        {
            DateTime time = EConvert.ToDateTime(hfTime.Value);
            if ((DateTime.Now - time).TotalSeconds > 30)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "test", "if(!diag.closed&&diag.opened){message.clear(); diag.close();}", true);
            }
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        showInfo();

    }


}
