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
public partial class Admin_main_SubFlowAdd : Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        { 
            BindControl();
            if (Request["fprocessid"] == null && Request["fprocessid"] == "")
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
    private void BindControl()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fname,fnumber from cf_sys_role where fisdeleted=0 and ftypeid=1 and fmtypeid=100 order by forder,ftime desc");
        this.t_FRoleId.DataSource = rc.GetTable(sb.ToString());
        this.t_FRoleId.DataTextField = "FName";
        this.t_FRoleId.DataValueField = "Fnumber";
        this.t_FRoleId.DataBind();
    }
    private void ShowParentInfo()
    {
        DataTable dt = rc.GetTable(EntityTypeEnum.EaProcess, "FName", "fid='" + Request["fprocessid"] + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["FName"] != DBNull.Value)
            {
                this.text_parentProcess.Text = dt.Rows[0]["FName"].ToString();

            }
        }
    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_App_SubFlow where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            //if (dt.Rows[0]["FIsSend"].ToString() == "1")
            //{
            //    this.ManamgeDep.Visible = true;
            //    this.Govdept1.FNumber = this.t_FSendDeptId.Value;
            //}
            //else
            //{
            //    this.ManamgeDep.Visible = false;
            //}
        }
    }
    private void SaveInfo()
    {
        if (Request["fprocessid"] == null && Request["fprocessid"] == "")
        {
            return;
        }
        //if (this.ManamgeDep.Visible == true)
        //{
        //    this.t_FSendDeptId.Value = this.Govdept1.FNumber;
        //}
        //else
        //{
        //    this.t_FSendDeptId.Value = "";
        //}
        pageTool tool = new pageTool(this.Page);
        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = tool.getPageValue();
        if (this.ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
            //--------
            sl.Remove("FISPRINT");
            sl.Add("FISPRINT", this.t_FIsEnd.SelectedValue);
            //--------
        }
        else
        {
            sl.Add("fprocessid", Request["fprocessid"]);
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
            //--------
            sl.Remove("FISPRINT");
            sl.Add("FISPRINT", this.t_FIsEnd.SelectedValue);
            //--------
        }
        if (rc.SaveEBase(EntityTypeEnum.EaSubFlow, sl, "FID", so))
        {
            //this.Govdept1.FNumber = this.t_FSendDeptId.Value;
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

    //protected void t_FIsSend_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (this.t_FIsSend.SelectedValue == "1")
    //    {
    //        this.ManamgeDep.Visible = true;
    //        this.Govdept1.FNumber = this.t_FSendDeptId.Value;
    //    }
    //    else
    //    {
    //        this.ManamgeDep.Visible = false;
    //    }
    //}
    protected void t_FIsEnd_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
}