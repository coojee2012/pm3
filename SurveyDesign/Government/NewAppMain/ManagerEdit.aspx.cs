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

public partial class Government_NewAppMain_ManagerEdit : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    RCenter sh = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string sTemp = "";
            Govdept1.fNumber = ComFunction.GetDefaultCityDept();
            Govdept1.Dis(1);
            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");

            if (Request.QueryString["fid"] != null &&
                Request.QueryString["fid"] != "")
            {
                this.ViewState["MFId"] = Request.QueryString["fid"];
                ShowInfo();
            }
        }
    }

    protected void govdeptid1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        partBindInfo(String.IsNullOrEmpty(Govdept1.FNumber) ? EConvert.ToInt(ComFunction.GetDefaultDept()) : EConvert.ToInt(Govdept1.FNumber));
    }
    protected void ddlPartType_SelectedIndexChanged(object sender, EventArgs e)
    {
        comBindInfo();
    }
    private void partBindInfo(int FParentId)
    {
        ddlPartType.Items.Clear();
        DataTable dt = sh.GetTable("select fname,FNumber from CF_Sys_Department where FParentId='" + FParentId + "' order by fnumber ");
        ddlPartType.DataSource = dt;
        ddlPartType.DataTextField = "FName";
        ddlPartType.DataValueField = "FNumber";
        ddlPartType.DataBind();
        ddlPartType.Items.Insert(0, new ListItem("", ""));

    }

    private void comBindInfo()
    {
        t_FCompany.Items.Clear();
        DataTable dt = sh.GetTable("select fname,FNumber from CF_Sys_Department where FParentId='" + ddlPartType.SelectedValue + "' order by fnumber");
        t_FCompany.DataSource = dt;
        t_FCompany.DataTextField = "FName";
        t_FCompany.DataValueField = "FNumber";
        t_FCompany.DataBind();
        t_FCompany.Items.Insert(0, new ListItem("", ""));
    }


    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from cf_sys_user where fid='" + this.ViewState["MFId"].ToString() + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            this.Govdept1.FNumber = dt.Rows[0]["FManageDeptId"].ToString();
            partBindInfo(EConvert.ToInt(Govdept1.fNumber));
            tr_Name.Visible = tr_pwd.Visible = btnAdd.Visible = btnAdd.Enabled = (EConvert.ToString(Session["DFUserId"]) == dt.Rows[0]["FId"].ToString());

            ddlPartType.SelectedIndex = ddlPartType.Items.IndexOf(ddlPartType.Items.FindByValue(dt.Rows[0]["FDepartmentID"].ToString()));
            comBindInfo();
            t_FCompany.SelectedValue = dt.Rows[0]["FCompany"].ToString();
            if (tr_pwd.Visible)
            {
                txtFName.Text = dt.Rows[0]["FName"].ToString();
                string pwd = dt.Rows[0]["FPassWord"].ToString();
                if (!string.IsNullOrEmpty(pwd))
                    pwd = SecurityEncryption.DESDecrypt(pwd);
                txtFPwd.Text = pwd;
            }
        }
    }
    bool IsRepeatByName()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select count(*) from cf_Sys_User ");
        sb.Append("where fName='" + txtFName.Text.Trim() + "' ");
        if (this.ViewState["MFId"] != null)
            sb.Append("and fid!='" + ViewState["MFId"] + "' ");
        return rc.GetSQLCount(sb.ToString()) > 0;
    }
    private void SaveInfo()
    {
        StringBuilder sb = new StringBuilder();
        pageTool tool = new pageTool(this.Page);
        SaveOptionEnum so = SaveOptionEnum.Insert;
        string fNumber = Govdept1.fNumber;
        Govdept1.fNumber = fNumber;
        SortedList sl = tool.getPageValue();
        //验证用户名是否重复
        if (IsRepeatByName())
        {
            tool.showMessage("用户名重复，请重新填写!");
            return;
        }
        if (this.ViewState["MFId"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["MFId"].ToString());
        }
        sl.Add("FManageDeptId", Govdept1.fNumber);
        sl.Add("FDepartmentID", ddlPartType.SelectedValue);
        sl.Add("FCompany", t_FCompany.SelectedValue);
        if (tr_pwd.Visible)
        {
            sl.Add("FName", txtFName.Text.Trim());
            sl.Add("FPassword", SecurityEncryption.DESEncrypt(txtFPwd.Text.Trim()));
        }
        if (rc.SaveEBase(EntityTypeEnum.EsUser, sl, "FID", so))
        {
            tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
            this.ViewState["MFId"] = sl["FID"].ToString();
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
}
