using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Approve.Common;
using System.Text;
using Approve.EntityBase;
using System.Data;
using System.Collections;

public partial class Share_Sys_RoleAdd : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            ControlBind();
            if (Request["fparentid"] != null && Request["fparentid"] != "")
            {
                ShowParentInfo();
                this.trParentName.Visible = true;
                this.trParentNumber.Visible = true;
            }
            else
            {
                this.trParentName.Visible = false;
                this.trParentNumber.Visible = false;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["FID"]))
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
            }
            //ShowSystemList();
        }
    }
    void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fname,fnumber from CF_Sys_Platform order by forder");
        DataTable dt = sh.GetTable(sb.ToString());
        t_FPlatId.DataSource = dt;
        t_FPlatId.DataTextField = "FName";
        t_FPlatId.DataValueField = "FNumber";
        t_FPlatId.DataBind();
        t_FPlatId.Items.Insert(0, new ListItem("所有", ""));
    }
    void ShowSystemList()
    {
        t_FSystemId.Items.Clear();
        if (!string.IsNullOrEmpty(t_FPlatId.SelectedValue))
        {
            DataTable dt = sh.GetTable("select fname,fnumber from cf_sys_SystemName where fplatId='" + t_FPlatId.SelectedValue + "' order by forder");
            t_FSystemId.DataSource = dt;
            t_FSystemId.DataTextField = "FName";
            t_FSystemId.DataValueField = "FNumber";
            t_FSystemId.DataBind();
            t_FSystemId.Items.Insert(0, new ListItem("所有", ""));
        }
    }
    protected void t_FPlatId_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowSystemList();
    }
    private void ShowParentInfo()
    {
        DataTable dt = sh.GetTable(EntityTypeEnum.EsRole, "FName,FNumber,FPlatId", "fid='" + Request["fparentid"] + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["FName"] != DBNull.Value)
            {
                this.text_FParentName.Text = dt.Rows[0]["FName"].ToString();
            }
            if (dt.Rows[0]["FNumber"] != DBNull.Value)
            {
                this.text_FParentNumber.Text = dt.Rows[0]["FNumber"].ToString();
            }
            if (dt.Rows[0]["FPlatId"] != DBNull.Value)
            {
                this.t_FPlatId.SelectedIndex = t_FPlatId.Items.IndexOf(t_FPlatId.Items.FindByValue(dt.Rows[0]["FPlatId"].ToString()));
                //t_FPlatId.Enabled = false;
            }
        }
    }
    //显示基本信息
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from cf_sys_role where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            ShowSystemList();
            t_FSystemId.SelectedIndex = t_FSystemId.Items.IndexOf(t_FSystemId.Items.FindByValue(dt.Rows[0]["FSystemId"].ToString()));
        }
    }
    //保存
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        #region 验证
        DataTable dt = sh.GetTable("select * from CF_Sys_Role where FNumber='" + t_FNumber.Text + "' and fid<>'" + EConvert.ToString(ViewState["FID"]) + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.showMessage("角色编号重复！");
            t_FName.Focus();
            return;
        }
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
            if (Request["fparentid"] != null && Request["fparentid"] != "")
                sl.Add("FParentId", text_FParentNumber.Text);
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FTypeId", Request.QueryString["FType"]);
        }
        if (sh.SaveEBase("CF_Sys_Role", sl, "FID", so))
        {
            tool.showMessage("保存成功");
            HSaveResult.Value = "1";
            ViewState["FID"] = sl["FID"].ToString();
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
}
