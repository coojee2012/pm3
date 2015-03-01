using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;

public partial class Admin_main_BackIdeaAdd : System.Web.UI.Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlBind();
            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            if (Request["fid"] != null && Request["fid"] != "")
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
            }
        }
    }
    void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fname,fnumber from CF_Sys_Platform where fisdeleted=0 order by fnumber");//ftype=2：管理部门系统类型
        DataTable dt = rc.GetTable(sb.ToString());
        t_FPlatId.DataSource = dt;
        t_FPlatId.DataTextField = "FName";
        t_FPlatId.DataValueField = "FNumber";
        t_FPlatId.DataBind();
        t_FPlatId.Items.Insert(0, new ListItem("", ""));
    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from cf_App_BackIdea where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            ShowSystemList();
            t_FSystemId.SelectedIndex = t_FSystemId.Items.IndexOf(t_FSystemId.Items.FindByValue(dt.Rows[0]["FSystemId"].ToString()));
        }
    }
    void ShowSystemList()
    {
        t_FSystemId.Items.Clear();
        if (!string.IsNullOrEmpty(t_FPlatId.SelectedValue))
        {
            DataTable dt = rc.GetTable("select fname,fnumber from cf_sys_SystemName where fplatId='" + t_FPlatId.SelectedValue + "' order by fnumber");
            t_FSystemId.DataSource = dt;
            t_FSystemId.DataTextField = "FName";
            t_FSystemId.DataValueField = "FNumber";
            t_FSystemId.DataBind();
            t_FSystemId.Items.Insert(0, new ListItem("", ""));
        }
    }
    protected void t_FPlatId_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowSystemList();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
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
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
        }
        if (rc.SaveEBase(EntityTypeEnum.EaAppBackIdea, sl, "FID", so))
        {
            tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
            HSaveResult.Value = "1";
            ViewState["FID"] = sl["FID"];
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }
}
