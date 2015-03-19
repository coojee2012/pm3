using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Approve.Common;
using System.Text;
using System.Data;
using Approve.EntityBase;
using System.Collections;
using System.Data.SqlClient;
using ProjectData;

public partial class Share_User_EntUserRightAdd : System.Web.UI.Page
{
    Share sh = new Share();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("showCheck();");
        if (!Page.IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            ControlBind();
            if (string.IsNullOrEmpty(Request.QueryString["FUserId"]))
            {
                Response.Write("失败，请重试");
                Response.Clear();
                Response.End();
                return;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["FID"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                showInfo();
            }
        }
    }

    private void ControlBind()
    {
        DataTable dt = null;

        t_FSystemId.DataSource = sh.GetTable("select * from CF_Sys_SystemName where FIsDeleted<>1");
        t_FSystemId.DataTextField = "FName";
        t_FSystemId.DataValueField = "FNumber";
        t_FSystemId.DataBind();
        t_FSystemId.Items.Insert(0, new ListItem("请选择", ""));

        l_Company.Text = sh.GetSignValue(EntityTypeEnum.EsUser, "FCompany", "FID='" + Request.QueryString["FUserId"] + "'");
        l_FJuridcialCode.Text = sh.GetSignValue(EntityTypeEnum.EsUser, "FJuridcialCode", "FID='" + Request.QueryString["FUserId"] + "'");

        dt = sh.getDicTbByFNumber("6602");
        t_FState.DataSource = dt;
        t_FState.DataTextField = "FName";
        t_FState.DataValueField = "FNumber";
        t_FState.DataBind();
        t_FState.Items.Insert(0, new ListItem("请选择", ""));
    }

    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from cf_sys_userRight where fid='" + ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            txtFPassWord.Text = SecurityEncryption.DESDecrypt(dt.Rows[0]["FPassWord"].ToString());
            hidd_oldLockNumber.Value = t_FLockNumber.Text;
            t_FSystemId.Enabled = false;
            if (t_FSystemId.Items.FindByValue(dt.Rows[0]["FSystemId"].ToString()) != null)
                t_FSystemId.SelectedValue = t_FSystemId.Items.FindByValue(dt.Rows[0]["FSystemId"].ToString()).Value;
            Check_FIsUserName.Checked = dt.Rows[0]["FIsUserName"].ToString() == "1";
        }
    }

    //保存
    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        //#region 验证
        //if (!string.IsNullOrEmpty(t_FLockNumber.Text))
        //{
        //    DataTable dt = sh.GetTable("select fid from CF_Sys_UserRight where flocknumber='" + t_FLockNumber.Text + "' and FUserId<>'" + Request.QueryString["FUserId"] + "'");
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        tool.showMessage("加密锁硬件编号已存在！");
        //        t_FLockNumber.Focus();
        //        return;
        //    }
        //}
        //if (!string.IsNullOrEmpty(t_FName.Text))
        //{
        //    DataTable dt = sh.GetTable("select fid from CF_Sys_UserRight where FName='" + t_FName.Text + "' and FUserId<>'" + Request["FUserId"] + "' union select fid from CF_Sys_User where FName='" + t_FName.Text + "' and FID<>'" + Request["FUserId"] + "'");
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        tool.showMessage("用户名已存在！");
        //        t_FName.Focus();
        //        return;
        //    }
        //}
        //if (!string.IsNullOrEmpty(t_FSystemId.SelectedValue))
        //{
        //    DataTable dt = sh.GetTable("select fid from CF_Sys_UserRight where FuserId='" + Request.QueryString["FUserId"] + "' and fsystemid='" + t_FSystemId.SelectedValue + "' and fid<>'" + EConvert.ToString(ViewState["FID"]) + "'");
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        tool.showMessage("该用户已存在“" + t_FSystemId.SelectedItem.Text + "”权限！");
        //        t_FSystemId.Focus();
        //        return;
        //    }
        //}
        //#endregion

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
            sl.Add("FBaseinfoID", db.CF_Sys_User.Where(t => t.FID == Request.QueryString["FUserId"]).Select(t => t.FBaseInfoId).FirstOrDefault());//新产生FBaseinfoID(是Share库的企业主FID)
            sl.Add("FDeptFrom", 1);//1：省级用户
            sl.Add("FUserId", Request.QueryString["FUserId"]);//用户ID
        }
        sl.Add("FIsUserName", Check_FIsUserName.Checked);
        sl.Add("FPassWord", SecurityEncryption.DESEncrypt(txtFPassWord.Text.Trim()));
        string froleId = ComFunction.GetRoleId(t_FSystemId.SelectedValue);
        if (!string.IsNullOrEmpty(froleId))
        {
            sl.Add("FRoleId", froleId);
            sl.Add("FMenuRoleId", "1" + froleId);
            if (sh.SaveEBase(EntityTypeEnum.EsUserRight, sl, "FID", so))
            {
                saveUserEndTime(Request.QueryString["FUserId"]);
                UpDateLock();
                hidd_oldLockNumber.Value = t_FLockNumber.Text;
                ViewState["FID"] = sl["FID"].ToString();
                tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
            }
            else
            {
                tool.showMessage("保存失败");
            }
        }
        else
        {
            tool.showMessage("保存失败[系统角色不正确]");
        }
    }

    //主帐户有效期更新为子帐户中最大的截止日期
    private void saveUserEndTime(string FUserId)
    {
        ProjectDB db = new ProjectDB();
        DateTime EndTime = EConvert.ToDateTime(t_FEndTime.Text);
        CF_Sys_User u = db.CF_Sys_User.Where(t => t.FID == FUserId).FirstOrDefault();
        if (u != null)
        {
            //得到最大截止日期
            DateTime MaxEndTime = db.CF_Sys_UserRight.Where(t => t.FUserId == FUserId).Max(t => t.FEndTime.Value);
            if (EndTime > MaxEndTime)
                MaxEndTime = EndTime;
            u.FEndTime = MaxEndTime;//更新为子帐户中最大的截止日期

            db.SubmitChanges();
        }
    }

    private void UpDateLock()
    {
        StringBuilder sb = new StringBuilder();
        string oldlockNumber = hidd_oldLockNumber.Value;
        if (!string.IsNullOrEmpty(oldlockNumber))
        {
            sb.Append(" update cf_sys_lock set fstate=0 where flocknumber ='" + oldlockNumber + "'");
        }
        string lockNumber = t_FLockNumber.Text;
        if (!string.IsNullOrEmpty(lockNumber))
        {
            sb.Append(" update cf_sys_lock set fstate=1 where flocknumber ='" + lockNumber + "'");
        }
        sh.PExcute(sb.ToString());
    }

    bool CheckFJuridcialCode()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select count(*) from cf_Sys_User u inner join cf_Sys_UserRight r ");
        sb.Append("on u.FId=r.FUserId where u.FJuridcialCode='" + l_FJuridcialCode.Text.Trim() + "' ");
        sb.Append("and r.FSystemId='" + t_FSystemId.SelectedValue + "' ");
        if (this.ViewState["FID"] != null)
            sb.Append(" and r.fId<>'" + this.ViewState["FID"] + "'");
        return sh.GetSQLCount(sb.ToString()) > 0;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (string.IsNullOrEmpty(l_FJuridcialCode.Text.Trim()))
            tool.showMessage("组织机构代码不能为空，请完善用户基本信息！");
        else
        {
            //if (CheckFJuridcialCode())
            //{

            //    tool.showMessage("该企业已经存在该类型的用户！(经组织机构代码验证)");
            //}
            //else
            saveInfo();
        }
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
}
