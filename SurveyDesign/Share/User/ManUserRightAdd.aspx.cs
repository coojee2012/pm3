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

public partial class Share_User_ManUserRightAdd : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "return CheckInfo(this);");
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

    //绑定项
    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fname,fnumber from CF_Sys_SystemName where fisdeleted=0 order by fnumber");
        DataTable dt = sh.GetTable(sb.ToString());
        t_FSystemId.DataSource = dt;
        t_FSystemId.DataTextField = "FName";
        t_FSystemId.DataValueField = "FNumber";
        t_FSystemId.DataBind();
        //int index = t_FSystemId.Items.IndexOf(t_FSystemId.Items.FindByValue("8015"));
        //if (index != -1)
        //{
        //    t_FSystemId.SelectedIndex = index;
        //    t_FSystemId.Enabled = false;
        //}
        if (!string.IsNullOrEmpty(t_FSystemId.SelectedValue))
            ShowRole(t_FSystemId.SelectedValue);

        sb = new StringBuilder();
        sb.Append("select fid,Fname,fnumber from cf_sys_role where fisdeleted=0");
        sb.Append(" and FMTypeId=100");
        sb.Append(" and ftypeid=1 ");
        sb.Append(" order by forder, ftime desc");
        dt = sh.GetTable(sb.ToString());
        t_FRoleId.DataSource = dt;
        t_FRoleId.DataTextField = "FName";
        t_FRoleId.DataValueField = "FNumber";
        t_FRoleId.DataBind();
    }

    //根据系统显示各类角色
    private void ShowRole(string fsystemId)
    {
        string fplatId = sh.GetSignValue("select fplatId from cf_sys_systemName where fnumber='" + fsystemId + "'");
        StringBuilder sb = new StringBuilder();
        //审核角色 
        sb.Append(" select fid ,fname,fnumber from cf_sys_role where fisdeleted=0 ");
        sb.Append(" and ftypeid=1 and FMtypeId=100 and ");
        sb.Append(" (FSystemId='" + fsystemId + "' or FPlatId=800) ");
        sb.Append(" order by forder,ftime desc ");
        DataTable dt = sh.GetTable(sb.ToString());
        this.t_FRoleId.DataSource = dt;
        this.t_FRoleId.DataTextField = "FName";
        this.t_FRoleId.DataValueField = "FNumber";
        this.t_FRoleId.DataBind();

        ShowMenuRoleId();
    }
    void ShowMenuRoleId()
    {
        DataTable dtSystemId = GetMenuRoleGroup();
        rpMenuRoleId.DataSource = dtSystemId;
        rpMenuRoleId.DataBind();

    }
    protected void rpMenuRoleId_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            //菜单角色
            string fSystemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSystemId"));
            string fName = sh.GetSignValue("select fname from cf_Sys_SystemName where fnumber='" + fSystemId + "' order by forder");
            Label lbl = e.Item.FindControl("ltTitle") as Label;
            lbl.Text = fName;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select fid ,fname,fnumber from cf_sys_role where fisdeleted=0 ");
            sb.Append(" and ftypeid=2 and FMTypeId=100 and FSystemId='" + fSystemId + "' order by forder,ftime desc ");
            DataTable dt = sh.GetTable(sb.ToString());
            CheckBoxList cbl = e.Item.FindControl("txtFMenuRoleId") as CheckBoxList;
            cbl.DataSource = dt;
            cbl.DataTextField = "FName";
            cbl.DataValueField = "FNumber";
            cbl.DataBind();
        }
    }
    DataTable GetMenuRoleGroup()
    {
        DataTable dt = sh.GetTable("select fsystemId from cf_sys_role where fisdeleted=0 and ftypeid=2 and FMTypeId=100 group by fsystemId order by max(forder) ");
        return dt;
    }

    //显示系统权限
    void ShowSystemRights(string FPlaId)
    {
        tr_Pope1.Visible = false;
        tr_Pope2.Visible = false;

        tr_App0.Visible = false;
        tr_Jzs0.Visible = false;
        tr_App1.Visible = false;
        tr_App2.Visible = false;

        //建筑市场监管系统
        if (FPlaId == "401")
        {
            tr_Pope1.Visible = true;
            tr_Pope2.Visible = true;
        }
        else if (FPlaId == "701")
        {
            tr_App0.Visible = true;
            tr_App1.Visible = true;
            tr_App2.Visible = true;
        }
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
            string fsystemId = dt.Rows[0]["fsystemId"].ToString();

            if (!string.IsNullOrEmpty(fsystemId))
            {
                ShowRole(fsystemId);
                ShowSystemRights(fsystemId);
            }
            ShowMenuRoleBySave(dt.Rows[0]["FMenuRoleId"].ToString());
            tool.fillPageControl(dt.Rows[0]);
            txtFPassWord.Text = SecurityEncryption.DESDecrypt(EConvert.ToString(dt.Rows[0]["FPassWord"]));
            hidd_oldLockNumber.Value = t_FLockNumber.Text;
            t_FSystemId.Enabled = false;
            Check_FIsUserName.Checked = dt.Rows[0]["FIsUserName"].ToString() == "1";

            //建筑市场监管
            t_FIsPub.Checked = dt.Rows[0]["FIsPub"].ToString() == "1";//发布权限
            t_FIsPope.Checked = dt.Rows[0]["FIsPope"].ToString() == "1";//特殊权限
            Hid_RightId.Value = dt.Rows[0]["FID"].ToString();
            showPope();



            //审核系统
            string FSelType = dt.Rows[0]["FSelType"].ToString();
            check_JZS.Checked = !string.IsNullOrEmpty(FSelType);//是否指定审核建造师专业
            showJzsPope();
            if (!string.IsNullOrEmpty(EConvert.ToString(FSelType)))
            {
                DataTable dtType = sh.GetTable("select FName from cf_sys_dic where fnumber in (" + FSelType + ")");
                string str = "";
                if (dt != null)
                {
                    foreach (DataRow dr in dtType.Rows)
                    {
                        if (str.Length > 0)
                            str += ",";
                        str += dr["FName"].ToString();
                    }
                }
                txt_FSelType.Text = str;
                check_JZS.Checked = true;
                tr_Jzs0.Visible = true;
            }
            else
            {
                txt_FSelType.Text = string.Empty;
                check_JZS.Checked = false;
                tr_Jzs0.Visible = false;
            }
        }
    }



    //保存
    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page);

        #region 验证
        if (!string.IsNullOrEmpty(t_FLockNumber.Text))
        {
            DataTable dt = sh.GetTable("select fid from CF_Sys_UserRight where flocknumber='" + t_FLockNumber.Text + "' and FUserId<>'" + Request.QueryString["FUserId"] + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("加密锁硬件编号已存在！");
                t_FLockNumber.Focus();
                return;
            }
        }
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            DataTable dt = sh.GetTable("select fid from CF_Sys_UserRight where FName='" + t_FName.Text + "' and FUserId<>'" + Request.QueryString["FUserId"] + "' union select fid from CF_Sys_User where FName='" + t_FName.Text + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("用户名已存在！");
                t_FName.Focus();
                return;
            }
        }
        if (!string.IsNullOrEmpty(t_FSystemId.SelectedValue))
        {
            DataTable dt = sh.GetTable("select fid from CF_Sys_UserRight where FuserId='" + Request.QueryString["FUserId"] + "' and fsystemid='" + t_FSystemId.SelectedValue + "' and fid<>'" + EConvert.ToString(ViewState["FID"]) + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("该用户已存在“" + t_FSystemId.SelectedItem.Text + "”权限！");
                t_FSystemId.Focus();
                return;
            }
        }
        //if (!string.IsNullOrEmpty(t_FEndTime.Text))//验证子帐户有效结束日期 必需 小于等主帐户有效结束日期
        //{
        //    DataTable dt = sh.GetTable("select Convert(varchar(100),max(FEndTime),23) FEndTime from cf_sys_User where FID=@FID", new SqlParameter("@FID", Request.QueryString["FUserId"]));
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        if (EConvert.ToDateTime(dt.Rows[0]["FEndTime"].ToString()) < EConvert.ToDateTime(t_FEndTime.Text))
        //        {
        //            tool.showMessage("子帐户有效结束日期不能大于主帐户的有效结束日期（" + EConvert.ToDateTime(dt.Rows[0]["FEndTime"].ToString()).ToShortDateString() + "）！");
        //            t_FEndTime.Focus();
        //            return;
        //        }
        //    }
        //}
        #endregion

        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = tool.getPageValue();
        if (ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FBaseinfoID", Guid.NewGuid().ToString());//新产生FBaseinfoID(是Share库的企业主FID)
            sl.Add("FDeptFrom", 1);//1：省级用户
            sl.Add("FUserId", Request.QueryString["FUserId"]);//用户ID
        }
        sl.Add("FPassWord", SecurityEncryption.DESEncrypt(txtFPassWord.Text));
        sl.Add("FMenuRoleId", GetMenuRoleBySel());
        //建筑市场监管
        sl.Add("FIsPub", t_FIsPub.Checked);//发布权限
        sl.Add("FIsPope", t_FIsPope.Checked);//特殊权限

        //审核系统
        sl.Add("FCanMod", t_FCanMod.SelectedValue);//是否有权限修改企业数据
        sl.Add("FPri", t_FPri.SelectedValue);//是否可审核企业市场行为
        sl.Add("FSelType", check_JZS.Checked ? t_FSelType.Value.Trim() : "");//审核建造师专业

        sl.Add("FIsUserName", Check_FIsUserName.Checked);
        if (sh.SaveEBase(EntityTypeEnum.EsUserRight, sl, "FID", so))
        {
            saveUserEndTime(Request.QueryString["FUserId"]);
            UpDateLock();//更新锁库
            hidd_oldLockNumber.Value = t_FLockNumber.Text;

            ViewState["FID"] = sl["FID"].ToString();
            Hid_RightId.Value = sl["FID"].ToString();
            tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
        }
        else
        {
            tool.showMessage("保存失败");
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


    string GetMenuRoleBySel()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < rpMenuRoleId.Items.Count; i++)
        {
            CheckBoxList cbl = rpMenuRoleId.Items[i].FindControl("txtFMenuRoleId") as CheckBoxList;
            foreach (ListItem item in cbl.Items)
            {
                if (item.Selected)
                {
                    if (sb.Length > 0)
                        sb.Append(",");
                    sb.Append(item.Value);
                }
            }
        }
        return sb.ToString();
    }
    void ShowMenuRoleBySave(string fMenuRoleId)
    {
        if (!string.IsNullOrEmpty(fMenuRoleId))
        {
            string[] menus = fMenuRoleId.Split(',');
            for (int i = 0; i < rpMenuRoleId.Items.Count; i++)
            {
                CheckBoxList cbl = rpMenuRoleId.Items[i].FindControl("txtFMenuRoleId") as CheckBoxList;
                foreach (ListItem item in cbl.Items)
                {
                    item.Selected = menus.Contains(item.Value);
                }
            }
        }
    }
    //更新锁库
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

    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
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

    //选择系统时刷新各类角色
    protected void t_FSystemId_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowRole(t_FSystemId.SelectedValue);
        ShowSystemRights(t_FSystemId.SelectedValue);
    }

    #region 市场监管特殊权限

    //特殊权限 
    protected void t_FIsPope_CheckedChanged(object sender, EventArgs e)
    {
        showPope();
    }

    //特殊权限
    private void showPope()
    {
        if (t_FIsPope.Checked)
        {
            t_FIsPub.Enabled = false;
            btnCheckPope.Visible = true;
            la_Pope.Visible = true;
        }
        else
        {
            t_FIsPub.Enabled = true;
            btnCheckPope.Visible = false;
            la_Pope.Visible = false;
        }
    }

    #endregion

    #region 审核系统 建造师权限

    //是否指定审核建造师专业
    protected void check_JZS_CheckedChanged(object sender, EventArgs e)
    {
        showJzsPope();
    }

    //建造师权限
    private void showJzsPope()
    {
        if (check_JZS.Checked)
        {
            tr_Jzs0.Visible = true;
        }
        else
        {
            tr_Jzs0.Visible = false;
        }
    }


    #endregion
}
