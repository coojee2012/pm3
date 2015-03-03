using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Data;
using System.Text;
using Approve.Common;

public partial class UserAuthority : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                Response.Write("参数错误");
                Response.End();
            }
            else
            {
                string sql = string.Format("select top 1* from cf_sys_user where fid='{0}'", UserId);
                DataTable dt = rc.GetTable(sql);
                if (dt == null || dt.Rows.Count < 1)
                {
                    Response.Write("userId: " + UserId + " 用户不存在");
                    Response.End();
                }
                else
                {
                    ltrName.Text = dt.Rows[0]["fname"].ToString();
                    BindRole();
                }
            }
        }
    }
    private string UserId
    {
        get
        {
            return Request.QueryString["userId"];
        }
    }
    private string SystemId {
        get { 
            string result = "1122";
            if (!string.IsNullOrEmpty(Request.QueryString["SystemId"]))
                result = Request.QueryString["SystemId"];
            return result;
        }
    }
    private void BindRole()
    {
        StringBuilder _builder = new StringBuilder();
        string sql = string.Format(@"select fid,fname,fnumber,fParentId from cf_sys_role where fisdeleted=0 and ftypeid=2 and FMTypeId=100 and FSystemId={0} order by forder",SystemId);
        List<string> userAuthority = GetUserAuthority();//用户权限
        DataTable table = rc.GetTable(sql);
        if (table != null && table.Rows.Count > 0)
        {
            DataRow[] firstRows = table.Select(" fParentId is null or fParentId=0");
            int num = 0;
            foreach (DataRow item in firstRows)
            {
                num++;
                string className = "role_" + num.ToString() + "_";
                _builder.Append("<tr>");
                _builder.AppendFormat("<td class=\"t_r t_bg\" width=\"20%\">{0}：&nbsp;<input type='checkbox' value='' onclick='ChooseRole(this)' name='{1}' /></td>", item["fname"], className);
                _builder.Append("<td>");
                DataRow[] seconedRows = table.Select(" fParentId='" + item["fnumber"] + "'");
                int count = 0;
                foreach (DataRow row in seconedRows)
                {
                    count++;
                    string childName = className +  count.ToString();
                    string fNumber = row["fnumber"].ToString();
                    if (userAuthority.Contains(fNumber))
                        _builder.AppendFormat("&nbsp;&nbsp;<input type='checkbox' checked='checked' value='{1}' name='{2}' />{0}", row["fname"], fNumber, childName);
                    else
                        _builder.AppendFormat("&nbsp;&nbsp;<input type='checkbox' value='{1}' name='{2}' />{0}", row["fname"], fNumber, childName);
                }
                _builder.Append("</td>");
                _builder.Append("</tr>");
            }
        } 
        ltrText.Text = _builder.ToString();
    }
    private List<string> GetUserAuthority()
    {
       string sql = string.Format("select (STUFF((select ','+FMenuRoleId FROM CF_Sys_UserRight WHERE FUserId ='{0}' FOR XML PATH('')),1,1,'')) as roleAuthority", UserId);
       DataTable table = rc.GetTable(sql);
       if (table != null && table.Rows.Count > 0)
           return table.Rows[0]["roleAuthority"].ToString().Split(',').ToList();
       return new List<string>();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        string sql = string.Format("select top 1* from CF_Sys_UserRight where FUserId='{0}'", UserId);
        DataTable table = rc.GetTable(sql);
        if (table == null || table.Rows.Count == 0)//该用户在权限表中无记录 则新增
        {
            sql = string.Format("insert into CF_Sys_UserRight(Fid,FbaseinfoId,FName,FPassword,FUserId,FMenuRoleId,FSystemId,FRoleId)values(NEWID(),NEWID(),'{2}','111111','{0}','{1}','{3}','100')", UserId, hfFnumber.Value, DateTime.Now.ToString("yyyyMMddhhmmssfff"),SystemId);
        }
        else
            sql = string.Format("update CF_Sys_UserRight set FMenuRoleId='{0}' where FUserId='{1}'",hfFnumber.Value, UserId);
        bool success = rc.PExcute(sql);
        if (success)
        {
            BindRole();
            tool.showMessage("操作成功");
        }
        else
            tool.showMessage("操作失败");
    }
}