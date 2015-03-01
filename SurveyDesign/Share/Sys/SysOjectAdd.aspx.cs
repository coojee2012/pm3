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

public partial class Share_sys_SysOjectAdd : Page
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
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * from CF_Sys_Object where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            pageTool tool = new pageTool(this.Page);
            tool.fillPageControl(dt.Rows[0]);
        }
    }
    private void SaveInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select count(*) from CF_Sys_Object where 1=1 ");
        sb.Append(" and fnumber ='" + this.t_FNumber.Text + "'");
        pageTool tool = new pageTool(this.Page);
        SortedList sl = tool.getPageValue();
        SaveOptionEnum so = SaveOptionEnum.Insert;
        if (this.ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
            sb.Append(" and fid<>'" + this.ViewState["FID"].ToString() + "' ");
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FIsDeleted", 0);
        }
        if (sh.GetSQLCount(sb.ToString()) > 0)
        {
            tool.showMessage("编码重复");
            return;
        }
        if (sh.SaveEBase(EntityTypeEnum.EsObject, sl, "FID", so))
        {
            tool.showMessage("保存成功");
            this.ViewState["FID"] = sl["FID"].ToString();
            this.HSaveResult.Value = "1";
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SaveInfo();
        Tools.DataCache.RemoveCache("Object");
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {

        this.ViewState["FID"] = null;
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("clearPage()");
    }
}
