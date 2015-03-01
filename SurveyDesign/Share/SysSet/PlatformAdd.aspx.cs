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

public partial class Share_SysSet_PlatformAdd : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            if (Request["fid"] != null && Request["fid"] != "")
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
            }
        }
    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Sys_Platform where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
        }
    }
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        #region 验证
        StringBuilder sb = new StringBuilder();
        sb.Append(" select count(*) from CF_Sys_Platform where fnumber='" + t_FNumber.Text + "' and fid<>'" + EConvert.ToString(ViewState["FID"]) + "'");
        if (sh.GetSQLCount(sb.ToString()) > 0)
        {
            tool.showMessage("编码重复");
            t_FNumber.Focus();
            return;
        }
        sb.Remove(0, sb.Length);

        #endregion

        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = tool.getPageValue();
        if (this.ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
        }
        if (sh.SaveEBase("CF_Sys_Platform", sl, "FID", so))
        {
            tool.showMessage("保存成功");
            this.ViewState["FID"] = sl["FID"].ToString();
            HSaveResult.Value = "1";
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        this.ViewState["FID"] = null;
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("clearPage()");
    }
}
