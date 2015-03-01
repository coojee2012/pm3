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
public partial class Share_SysSet_ManageDeptAdd : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            if (Request["fparentid"] != null && Request["fparentid"] != "")
            {
                ShowParentInfo();
            }
            else
            {
                trParentName.Visible = false;
                trParentNumber.Visible = false;
            }
            if (Request["fid"] != null && Request["fid"] != null)
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
            }
        }
    }
    private void ControlBind()
    {
        DataTable dt = sh.getDicTbByFNumber("103");
        this.t_FLevel.DataSource = dt;
        this.t_FLevel.DataTextField = "FName";
        this.t_FLevel.DataValueField = "FNumber";
        this.t_FLevel.DataBind();

        dt = sh.getDicTbByFNumber("102");
        this.t_FClassNumber.DataSource = dt;
        this.t_FClassNumber.DataTextField = "FName";
        this.t_FClassNumber.DataValueField = "FNumber";
        this.t_FClassNumber.DataBind();
    }
    private void ShowParentInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FName,FNumber from CF_Sys_ManageDept Where FId='" + Request["fparentid"] + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            this.text_FParentName.Text = dt.Rows[0]["FName"].ToString();
            this.text_FParentNumber.Text = dt.Rows[0]["FNumber"].ToString();
        }
    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Sys_ManageDept where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
        }
    }
    private void SaveInfo()
    {
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

            if (Request["fparentid"] != null && Request["fparentid"] != "")
            {
                sl.Add("FParentId", sh.GetSignValue(EntityTypeEnum.EsManageDept, "FNumber", "Fid='" + Request["fparentid"] + "'"));
            }
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
        }
        if (sh.SaveEBase(EntityTypeEnum.EsManageDept, sl, "FID", so))
        {
            tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
            HSaveResult.Value = "1";
            this.ViewState["FID"] = sl["FID"].ToString();
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
