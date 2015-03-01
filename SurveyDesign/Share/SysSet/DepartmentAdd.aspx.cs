using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
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

public partial class Share_SysSet_DepartmentAdd : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
            this.btnSave.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            if (!String.IsNullOrEmpty(Request.QueryString["fparentid"]))
            {
                ShowParentInfo();
            }
            else
            {
                trParentName.Visible = false;
                trParentNumber.Visible = false;
            }
            if (!String.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                this.ViewState["FID"] = Request.QueryString["fid"];
                ShowInfo();
            }
        }
    }
    private void ControlBind()
    {
        DataTable dt = sh.getDicTbByFNumber("103");
        t_FLevel.DataSource = dt;
        t_FLevel.DataTextField = "FName";
        t_FLevel.DataValueField = "FNumber";
        t_FLevel.DataBind();
        t_FLevel.Items.Insert(0, new ListItem("请选择", ""));

        dt = sh.getDicTbByFNumber("102");
        this.t_FClassNumber.DataSource = dt;
        this.t_FClassNumber.DataTextField = "FName";
        this.t_FClassNumber.DataValueField = "FNumber";
        this.t_FClassNumber.DataBind();
        this.t_FClassNumber.Items.Insert(0, new ListItem("请选择", ""));

    }
    private void ShowParentInfo()
    {
        DataTable result = sh.GetTable("select fname,fnumber from CF_Sys_Department where fid='" + Request.QueryString["fparentid"] + "'");

        if (result != null && result.Rows.Count > 0)
        {
            this.text_FParentName.Text = result.Rows[0]["fname"].ToString();
            this.text_FParentNumber.Text = result.Rows[0]["fnumber"].ToString();
        }
    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Sys_Department where fid='" + this.ViewState["FID"].ToString() + "'");
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
                sl.Add("FParentId", sh.GetSignValue("select fnumber from CF_Sys_Department where Fid='" + Request["fparentid"] + "'"));
                sl.Add("FIsDeleted", 0);
                sl.Add("FCreateTime", DateTime.Now);
            }
        }
        string old = sh.GetSignValue("select fid from CF_Sys_Department where FId<>'" + ViewState["FID"] + "' and FNumber='" + EConvert.ToInt(t_FNumber.Text.Trim()) + "'");
        if (!string.IsNullOrEmpty(old))
        {
            tool.showMessage("编码重复!");
            t_FNumber.Focus();
            return;
        }

        if (sh.SaveEBase("CF_Sys_Department", sl, "FID", so))
        {
            tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
            this.ViewState["FID"] = sl["FID"].ToString();
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
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
