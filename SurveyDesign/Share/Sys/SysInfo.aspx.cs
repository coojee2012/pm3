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
using Approve.Common;
using Approve.EntityBase;
using Approve.RuleCenter;
using System.Text;

public partial class Admin_main_SysInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            ShowInfo();
        }
    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * from cf_sys_user where fid='" + Session["Admin_FID"] + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            if (!string.IsNullOrEmpty(t_FPassWord.Text.Trim()))
            {
                t_FPassWord.Text = SecurityEncryption.DESDecrypt(t_FPassWord.Text.Trim());
            }
        }
    }
    private void SaveIfno()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select count(*) from cf_sys_user where fname='" + this.t_FName.Text + "' ");

        pageTool tool = new pageTool(this.Page);
        SortedList sl = tool.getPageValue();
        if (!string.IsNullOrEmpty(t_FPassWord.Text.Trim()) && (sl.Contains("FPASSWORD") | sl.Contains("fpassword")))
        {
            sl["FPASSWORD"] = SecurityEncryption.DESEncrypt(t_FPassWord.Text.Trim());
        }
        EntityTypeEnum en = EntityTypeEnum.EsUser;
        string fkey = "FID";
        SaveOptionEnum so = SaveOptionEnum.Update;

        string fId = rc.GetSignValue(EntityTypeEnum.EsUser, "FID", "fid='" + Session["Admin_FID"].ToString() + "'");
        if (fId == null || fId == "")
        {
            return;
        }
        sl.Add("FID", fId);
        sb.Append(" and fid <> '" + fId + "' ");

        int iCount = rc.GetSQLCount(sb.ToString());
        if (iCount > 0)
        {
            tool.showMessage("用户名重复");
            return;
        }

        if (rc.SaveEBase(en, sl, fkey, so))
        {
            tool.showMessage("保存成功");
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SaveIfno();
    }
}
