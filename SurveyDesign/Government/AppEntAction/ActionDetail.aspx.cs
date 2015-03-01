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
public partial class Government_AppEntAction_ActionDetail : govBasePage
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            if (Request["fid"] != null && Request["fid"] != null)
            { 
                ShowInfo();
            }
        }
    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Sys_BadActionCode where fid='" + this.Request["fid"].ToString() + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            sb.Remove(0, sb.Length);
            sb.Append("fnumber='" + dt.Rows[0]["Fparentid"].ToString() + "'");
            DataTable dtt= rc.GetTable(EntityTypeEnum.EsBadActionCode, "FName,FParentId", sb.ToString());
            if (dtt != null && dtt.Rows.Count > 0)
            {
                this.txtPFName.Text = dtt.Rows[0]["FName"].ToString();
                sb.Remove(0, sb.Length);
                sb.Append("FNumber='" + dtt.Rows[0]["FParentId"].ToString() + "'");
                this.txtPPFName.Text = rc.GetSignValue(EntityTypeEnum.EsBadActionCode, "FName", sb.ToString());
            } 
        }
    } 
}
