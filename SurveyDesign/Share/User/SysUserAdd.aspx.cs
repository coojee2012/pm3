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

public partial class Share_User_ManUserAdd : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            ControlBind();
            if (!string.IsNullOrEmpty(Request.QueryString["FID"]))
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();//显示基本信息 
            }
        }
    }


    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from cf_sys_systemName ");
        sb.Append("where FType=1 and fisdeleted=0  order by fnumber ");
        DataTable dt = sh.GetTable(sb.ToString());

        t_FRoleId.DataSource = dt;
        t_FRoleId.DataTextField = "FName";
        t_FRoleId.DataValueField = "FNumber";
        t_FRoleId.DataBind();

        sb.Remove(0, sb.Length);
        sb.Append("select fid,Fname,fnumber from cf_sys_role where fisdeleted=0");
        if (Request["fmatypeid"] != null && Request["fmatypeid"] != "")
        {
            sb.Append(" and FMTypeId=" + Request["fmatypeid"]);
        }
        sb.Append(" order by ftime desc");
        dt = sh.GetTable(sb.ToString());
        t_FMenuRoleId.DataSource = dt;
        t_FMenuRoleId.DataTextField = "FName";
        t_FMenuRoleId.DataValueField = "FNumber";
        t_FMenuRoleId.DataBind();
        t_FMenuRoleId.Items.Insert(0, new ListItem("请选择", ""));

        Govdept1.fNumber = ComFunction.GetDefaultCityDept();
        Govdept1.Dis(1);
    }

    //显示基本信息
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from cf_sys_user where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            if (!string.IsNullOrEmpty(t_FPassWord.Text.Trim()))
            {
                t_FPassWord.Text = SecurityEncryption.DESDecrypt(t_FPassWord.Text.Trim());
            }
            this.Govdept1.FNumber = this.t_FManageDeptId.Value.Trim();
        }
    }


    //保存
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        t_FManageDeptId.Value = this.Govdept1.FNumber;//主管部门number
        #region 验证
        if (t_FManageDeptId.Value == "")
        {
            Govdept1.FNumber = ComFunction.GetDefaultDept();
            tool.showMessage("请选择主管部门");
            return;
        }
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            DataTable dt = sh.GetTable("select fid from CF_Sys_User where FName='" + t_FName.Text + "' and fid<>'" + EConvert.ToString(ViewState["FID"]) + "' and FType in(6,0)");//in(6,0)系统管理员和超级管理员 
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("用户名已存在！");
                t_FCompany.Focus();
                return;
            }
            dt = sh.GetTable("select fid from CF_Sys_UserRight where FName='" + t_FName.Text + "' and FUserId<>'" + EConvert.ToString(ViewState["FID"]) + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("用户名已存在！");
                t_FName.Focus();
                return;
            }
        }
        if (!string.IsNullOrEmpty(t_FLockNumber.Text))
        {
            DataTable dt = sh.GetTable("select fid from CF_Sys_User where FLockNumber='" + t_FLockNumber.Text + "' and fid<>'" + EConvert.ToString(ViewState["FID"]) + "'");//in(6,0)系统管理员和超级管理员 
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("加密锁硬件编号已存在！");
                t_FCompany.Focus();
                return;
            }

            dt = sh.GetTable("select fid from CF_Sys_UserRight where flocknumber='" + t_FLockNumber.Text + "' and FUserId<>'" + EConvert.ToString(ViewState["FID"]) + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("加密锁硬件编号已存在！");
                t_FLockNumber.Focus();
                return;
            }
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
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FType", 6);//系统管理员用户
        }
        //加密密码
        sl.Remove("FPASSWORD");
        string FPassWord = t_FPassWord.Text.Trim();   //密码
        FPassWord = SecurityEncryption.DESEncrypt(FPassWord);
        sl.Add("FPassWord", FPassWord);

        if (sh.SaveEBase(EntityTypeEnum.EsUser, sl, "FID", so))
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string fDeptNumber = this.Govdept1.FNumber;
        SaveInfo();
        this.Govdept1.FNumber = fDeptNumber;
    }


    //选择未发锁返回
    protected void btn_LockID_Click(object sender, EventArgs e)
    {
        string lockID = hidd_LockID.Value;
        if (!string.IsNullOrEmpty(lockID))
        {
            DataTable dt = sh.GetTable("select * from cf_sys_lock where fid='" + lockID + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                t_FLockLabelNumber.Text = dt.Rows[0]["FLockLabelNumber"].ToString();
                t_FLockNumber.Text = dt.Rows[0]["FLockNumber"].ToString();
            }
        }
    }


    //得到系统类型
    public string getSysName(string number)
    {
        return sh.getSystemName(number);
    }


}
