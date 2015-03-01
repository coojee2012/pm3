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

public partial class Admin_main_BadActionAdd : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request["fparentid"] == null || Request["fparentid"] == "")
            {
                return;
            }
            ShowParentInfo();
            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");

            if (Request["fid"] != null && Request["fid"] != null)
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
            }
        }
    }
    private void ShowParentInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fparentid,fname from CF_Sys_BadActionCode where fid='" + Request["fparentid"] + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            this.tPName.Text = dt.Rows[0]["FName"].ToString();
            sb.Remove(0, sb.Length);
            sb.Append("fnumber='" + dt.Rows[0]["FParentid"].ToString() + "'");
            this.tPPName.Text = sh.GetSignValue(EntityTypeEnum.EsBadActionCode, "FName", sb.ToString());
        }
    }

    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Sys_BadActionCode where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);

        }
    }
    private void SaveInfo()
    {
        if (Request["fparentid"] == null || Request["fparentid"] == "")
        {
            return;
        }
        string fParentNumber = sh.GetSignValue(EntityTypeEnum.EsBadActionCode, "FNumber", "fid='" + Request["fparentid"] + "'");
        if (fParentNumber == null || fParentNumber == "")
        {
            return;
        }
        pageTool tool = new pageTool(this.Page);
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
            sl.Add("FCreateTime", DateTime.Now);

        }
        sl.Add("FParentId", fParentNumber);
        if (sh.SaveEBase(EntityTypeEnum.EsBadActionCode, sl, "FID", so))
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
