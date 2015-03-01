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

public partial class Admin_User_UserAdd : Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string sTemp = "";
            t_FBeginTime.Text = DateTime.Now.ToShortDateString();
            t_FEndTime.Text = DateTime.Now.AddYears(5).ToShortDateString();

            if (Request["type"] == null || Request["type"] == "")
            {
                return;
            }
            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            ControlBind();
            if (Request["fid"] != null && Request["fid"] != null)
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
                //ShowUserRole();
            }
        }
    }


    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Remove(0, sb.Length);
        sb.Append(" select fid ,fname,fnumber from cf_sys_role where fisdeleted=0 ");
        sb.Append(" and fnumber='2001'");
        DataTable dt = rc.GetTable(sb.ToString());
        this.t_FMenuRoleId.DataSource = dt;
        this.t_FMenuRoleId.DataTextField = "FName";
        this.t_FMenuRoleId.DataValueField = "FNumber";
        this.t_FMenuRoleId.DataBind();
    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from cf_sys_user where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            this.Govdept1.FNumber = this.t_FManageDeptId.Value.Trim();
            this.t_FBeginTime.Text = rc.StrToDate(t_FBeginTime.Text);
            this.t_FEndTime.Text = rc.StrToDate(this.t_FEndTime.Text);
            this.txt_FPassWord.Text = SecurityEncryption.DESDecrypt(dt.Rows[0]["FPassword"].ToString());
        }
    }

    private void SaveInfo()
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sb1 = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();

        sb.Append(" select count(*) from cf_sys_user where fname='" + this.t_FName.Text + "' ");
        sb1.Append(" select count(*) from cf_sys_user where flocknumber='" + this.t_FLockNumber.Text + "' ");

        pageTool tool = new pageTool(this.Page);
        t_FManageDeptId.Value = this.Govdept1.FNumber;
        if (t_FManageDeptId.Value == "")
        {
            Govdept1.FNumber = ComFunction.GetDefaultDept();
            tool.showMessage("请选择管理部门");
            return;
        }

        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = tool.getPageValue();
        sl.Add("FPassWord", SecurityEncryption.DESEncrypt(this.txt_FPassWord.Text.Trim()));
        sl.Add("ftype", Request.QueryString["type"]);//资源管理人员
        if (this.ViewState["FID"] != null)
        {
            sb.Append(" and fid<> '" + this.ViewState["FID"].ToString() + "' ");
            sb1.Append(" and fid<> '" + this.ViewState["FID"].ToString() + "' ");
            sb2.Append(" and fid<> '" + this.ViewState["FID"].ToString() + "' ");

            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FBaseInfoId", Guid.NewGuid().ToString());
            sl.Add("FCreateTime", DateTime.Now);
            //sl.Add("FType", Request["type"]); 

        }
        int iCount = 0;
        if (this.t_FName.Text != "")
        {
            iCount = rc.GetSQLCount(sb.ToString());
            if (iCount > 0)
            {
                tool.showMessage("用户名重复");
                return;
            }
        }

        iCount = rc.GetSQLCount(sb1.ToString());
        if (iCount > 0)
        {
            tool.showMessage("加密锁硬件编号重复");
            return;
        }

        if (rc.SaveEBase(EntityTypeEnum.EsUser, sl, "FID", so))
        {
            tool.showMessage("保存成功");
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
