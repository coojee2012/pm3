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
using System.Data.SqlClient;
using System.Threading;

public partial class Admin_User_EntUserAdd : Page
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
                showList();//显示权限列表
                tabSetup1.Visible = false;
                divSetup2.Visible = true;
                t_FSystemId.Enabled = false;
                btnSelectEnt.Visible = false;
            }
            else
            {
                btnDownload.Visible = false;
                tabSetup1.Visible = true;
                divSetup2.Visible = false;

            }

        }
    }


    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select replace(FDesc,'资质办理','') FName,FNumber from CF_Sys_SystemName where FPlatId=800");
        if (string.IsNullOrEmpty(Request.QueryString["FID"]))
        {
            sb.AppendLine("  and FName !='建设单位'  ");
        }
        
        sb.AppendLine(" order by FOrder ");
        DataTable dt = sh.GetTable(sb.ToString());
        t_FSystemId.DataSource = dt;
        t_FSystemId.DataTextField = "FName";
        t_FSystemId.DataValueField = "FNumber";
        t_FSystemId.DataBind();
        t_FSystemId.Items.Insert(0, new ListItem("请选择", ""));

        t_FEntType.DataSource = dt;
        t_FEntType.DataTextField = "FName";
        t_FEntType.DataValueField = "FNumber";
        t_FEntType.DataBind();
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
            txtFPassWord.Text = SecurityEncryption.DESDecrypt(dt.Rows[0]["FPassWord"].ToString());
            this.Govdept1.FNumber = this.t_FManageDeptId.Value.Trim();
            Check_FIsUserName.Checked = dt.Rows[0]["FIsUserName"].ToString() == "1";

        } hiFUserId.Value = EConvert.ToString(ViewState["FID"]);
    }

    //显示权限列表
    private void showList()
    {
        string FUserId = EConvert.ToString(ViewState["FID"]);
        if (!string.IsNullOrEmpty(FUserId))
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select r.*, n.fName sysName from CF_Sys_UserRight r,cf_sys_systemName n ");
            sb.Append("where r.FSystemId=n.FNumber and r.fisdeleted=0 and r.fuserid='" + FUserId + "' ");
            sb.Append("order by n.Forder ");
            DataTable dt = sh.GetTable(sb.ToString());
            DG_Rights.DataSource = dt;
            DG_Rights.DataBind();
        }
    }
    //列表
    protected void DG_Rights_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            string FState = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FState"));
            string FLockNumber = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FLockNumber"));
            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
            DateTime FEndTime = EConvert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "FEndTime"));

            e.Row.Cells[4].Text = sh.GetDicName(FState);
            if (FEndTime < DateTime.Now)
            {
                e.Row.Cells[3].Text += "<font color='red'>已过期</font>";
            }

            Button del = (Button)e.Row.FindControl("btnDel");
            if (del != null)
            {
                del.Attributes.Add("onclick", "return confirm('确认删除吗？');");
            }

            e.Row.Attributes.Add("ondblclick", "addRight('" + ViewState["FID"] + "','" + FID + "');");
        }
    }
    protected void DG_Rights_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cnDel")
        {
            pageTool tool = new pageTool(this.Page);
            string FID = e.CommandArgument.ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(" delete from CF_Sys_UserRight where fid='" + FID + "' update cf_user_reg set fstate=0 where frfid='" + FID + "'");
            if (sh.PExcute(sb.ToString()))
            {
                tool.showMessage("删除成功");
                showList();
            }
        }
    }


    //保存
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        #region 验证
        t_FManageDeptId.Value = this.Govdept1.FNumber;//主管部门number
        if (t_FManageDeptId.Value == "")
        {
            Govdept1.FNumber = ComFunction.GetDefaultDept();
            tool.showMessage("请选择主管部门");
            return;
        }
        if (!string.IsNullOrEmpty(t_FCompany.Text))
        {
            DataTable dt = sh.GetTable("select fid from CF_Sys_User where FCompany='" + t_FCompany.Text + "' and fid<>'" + EConvert.ToString(ViewState["FID"]) + "' and FSystemId='" + t_FSystemId.SelectedValue + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("企业名称已存在！");
                t_FCompany.Focus();
                return;
            }
        }
        if (!string.IsNullOrEmpty(t_FLockNumber.Text))
        {
            DataTable dt = sh.GetTable("select fid from CF_Sys_UserRight where flocknumber='" + t_FLockNumber.Text + "' and FUserId<>'" + EConvert.ToString(ViewState["FID"]) + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("加密锁硬件编号已存在！");
                t_FLockNumber.Focus();
                return;
            }
            dt = sh.GetTable("select fid from cf_sys_user where flocknumber='" + t_FLockNumber.Text + "' and FID<>'" + EConvert.ToString(ViewState["FID"]) + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("加密锁硬件编号已存在！");
                t_FLockNumber.Focus();
                return;
            }
        }
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            DataTable dt = sh.GetTable("select fid from CF_Sys_UserRight where FName='" + t_FName.Text + "' and FUserId<>'" + EConvert.ToString(ViewState["FID"]) + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("用户名已存在！");
                t_FName.Focus();
                return;
            }
            dt = sh.GetTable("select fid from CF_Sys_User where FName='" + t_FName.Text + "' and FID<>'" + EConvert.ToString(ViewState["FID"]) + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("用户名已存在！");
                t_FName.Focus();
                return;
            }
        }
        if (!string.IsNullOrEmpty(t_FEndTime.Text))//验证子帐户有效结束日期 必需 小于等主帐户有效结束日期
        {
            DataTable dt = sh.GetTable("select Convert(varchar(100),max(FEndTime),23) FEndTime from cf_sys_UserRight where FUserID=@FUserId", new SqlParameter("@FUserId", ViewState["FID"]));
            if (dt != null && dt.Rows.Count > 0)
            {
                if (EConvert.ToDateTime(dt.Rows[0]["FEndTime"].ToString()) > EConvert.ToDateTime(t_FEndTime.Text))
                {
                    tool.showMessage("主帐户有效结束日期必需大于等于所有子帐户的有效结束日期！");
                    t_FEndTime.Focus();
                    return;
                }
            }
        }

        #endregion
        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = tool.getPageValue(divSetup2);
        if (this.ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FBaseInfoId", Guid.NewGuid().ToString());//新产生FBaseinfoID(是Share库的企业主FID)
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FType", 2);//企业用户
        }
        //菜单角色
        string fMenuRoleId = sh.GetSignValue("select FNumber from cf_Sys_Role where fsystemId='" + t_FSystemId.SelectedValue + "' and FTypeId=2");
        sl.Add("FMenuRoleId", fMenuRoleId);
        sl.Add("FIsUserName", Check_FIsUserName.Checked);
        sl.Add("FPassWord", SecurityEncryption.DESEncrypt(txtFPassWord.Text.Trim()));
        if (sh.SaveEBase(EntityTypeEnum.EsUser, sl, "FID", so))
        {
            UpDateLock();
            tool.showMessage("保存成功");
            HSaveResult.Value = "1";
            this.ViewState["FID"] = sl["FID"].ToString();
        }
        else
        {
            tool.showMessage("保存失败");
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        string fDeptNumber = this.Govdept1.FNumber;
        if (string.IsNullOrEmpty(fDeptNumber))
        {
            tool.showMessage("请选择主管部门！");
            return;
        }
        if (CheckFJuridcialCode())
        {
            tool.showMessage("该企业已经存在该类型的用户！(经组织机构代码验证)");
        }
        else
            SaveInfo();
        this.Govdept1.FNumber = fDeptNumber;
    }

    bool CheckFJuridcialCode()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select count(*) from cf_Sys_User where ");
        sb.Append("FJuridcialCode='" + t_FJuridcialCode.Text.Trim() + "' ");
        sb.Append("and FSystemId='" + t_FSystemId.SelectedValue + "' ");
        if (this.ViewState["FID"] != null)
            sb.Append(" and fId<>'" + this.ViewState["FID"] + "'");
        return sh.GetSQLCount(sb.ToString()) > 0;
    }


    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
        showList();//显示权限列表
    }


    //选择注册用户返回时操作按钮
    protected void btn_FRFID_Click(object sender, EventArgs e)
    {
        string RegFID = hidd_RegFID.Value;
        DataTable dt = sh.GetTable("select * from CF_User_Reg where FID='" + RegFID + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            pageTool tool = new pageTool(this.Page);
            SaveOptionEnum so = SaveOptionEnum.Insert;
            SortedList sl = new SortedList();

            sl.Add("FID", dt.Rows[0]["FRFID"]);
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FYBaseinfoID", dt.Rows[0]["FBaseInfoId"]);
            sl.Add("FDeptFrom", 1);//1：省级用户
            sl.Add("FUserId", ViewState["FID"]);//用户ID
            sl.Add("FName", dt.Rows[0]["FName"]);
            sl.Add("FPassWord", dt.Rows[0]["FPassWord"]);
            sl.Add("FState", 1);
            sl.Add("FSystemId", dt.Rows[0]["FSystemId"]);

            if (sh.SaveEBase(EntityTypeEnum.EsUserRight, sl, "FID", so) && sh.PExcute("update cf_user_reg set fstate=1 where FID='" + RegFID + "'"))
            {
                tool.showMessage("选取成功");
                HSaveResult.Value = "1";
                showList();
            }
            else
            {
                tool.showMessage("选取失败");
            }
        }
    }


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
    //void ShowFEntTypes()
    //{
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append(" select fname,fcnumber fnumber from cf_sys_dic where fisdeleted=0 ");
    //    sb.Append(" and fparentId=100 ");
    //    if (!string.IsNullOrEmpty(t_FEntTypes.Value.Trim()))
    //        sb.Append(" and FCnumber in (" + t_FEntTypes.Value.Trim() + ")");
    //    else
    //        sb.Append(" and 1=2 ");
    //    sb.Append(" order by fnumber");
    //    DataTable dt = sh.GetTable(sb.ToString());
    //    sb = new StringBuilder();
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            if (sb.Length > 0)
    //                sb.Append(",");
    //            sb.Append(dt.Rows[i]["FName"].ToString());
    //        }
    //    }
    //    txt_FEntTypes.Text = sb.ToString();
    //}
    //protected void btnSel_Click(object sender, EventArgs e)
    //{
    //    ShowFEntTypes();
    //}
    protected void btnNext_Click(object sender, EventArgs e)
    {
        tabSetup1.Visible = false;
        divSetup2.Visible = true;
        t_FSystemId.ClearSelection();
        t_FSystemId.SelectedValue = t_FEntType.SelectedValue;
        t_FSystemId.Enabled = false;

        if (t_FSystemId.SelectedValue == "155" || t_FSystemId.SelectedValue == "15501" || t_FSystemId.SelectedValue == "145" || t_FSystemId.SelectedValue == "126")
        {
            btnSelectEnt.Visible = true;
            t_FCompany.ReadOnly = true;
        }
        else if (t_FSystemId.SelectedValue == "100")
        {

        }
        else
        {
            btnDownload.Visible = false;
            btnSelectEnt.Visible = false;
        }
    }
    protected void btnSelectEnt_Click(object sender, EventArgs e)
    {
        ShareTool st = new ShareTool();
        st.DownloadSingleUser(hiFUserId.Value, EConvert.ToInt(t_FSystemId.SelectedValue));
        ViewState["FID"] = hiFUserId.Value;
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "$('#btnReload').click()", true);

    }
    protected void btnReadCA_Click(object sender, EventArgs e)
    {
        ShareTool st = new ShareTool();
        t_FCANumber.Text = st.ReadCA(CaCerti.Value);
    }
}
