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
using ProjectData;
using System.Linq;


public partial class Share_User_ManUserAdd : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            partBindInfo(String.IsNullOrEmpty(Govdept1.FNumber) ? EConvert.ToInt(ComFunction.GetDefaultCityDept()) : EConvert.ToInt(Govdept1.FNumber));
            btnSave.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            ControlBind();
            if (!string.IsNullOrEmpty(Request.QueryString["FID"]))
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();//显示基本信息
                showList();//显示权限列表
               
            }
            showFileList();//显示附件
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
        //ddlPartType.Items.Clear();
        //DataTable dt = sh.GetTable("select fname,FNumber from CF_Sys_Department where FParentId='" + FParentId + "' order by fnumber ");
        //ddlPartType.DataSource = dt;
        //ddlPartType.DataTextField = "FName";
        //ddlPartType.DataValueField = "FNumber";
        //ddlPartType.DataBind();
        //ddlPartType.Items.Insert(0, new ListItem("", ""));

    }

    private void comBindInfo()
    {
        //t_FCompany.Items.Clear();
        //DataTable dt = sh.GetTable("select fname,FNumber from CF_Sys_Department where FParentId='" + ddlPartType.SelectedValue + "' order by fnumber");
        //t_FCompany.DataSource = dt;
        //t_FCompany.DataTextField = "FName";
        //t_FCompany.DataValueField = "FNumber";
        //t_FCompany.DataBind();
        //t_FCompany.Items.Insert(0, new ListItem("", ""));
    }

    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        OA oa = new OA();
        //oa部门 
        //sb.Append(" select FName,FNumber from CF_OA_Organization where fisdeleted=0 and flevel>1 order by flevel,forder,fnumber ");
        //DataTable dt = oa.GetTable(sb.ToString());
        //t_FOAorg.DataSource = dt;
        //t_FOAorg.DataTextField = "FName";
        //t_FOAorg.DataValueField = "FNumber";
        //t_FOAorg.DataBind();
        //t_FOAorg.Items.Insert(0, new ListItem("请选择", ""));




        //sb.Append("select fnumber,");
        //sb.Append(" ''+rtrim(fname) fname ");
        //sb.Append(" from CF_OA_Organization where flevel=2 ");
        //sb.Append(" order by forder");
        //DataTable dt = oa.GetTable(sb.ToString());

        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    string num = dt.Rows[i]["fnumber"].ToString().Trim();
        //    ListItem li = new ListItem(dt.Rows[i]["fname"].ToString(), num);
        //    this.t_FOAorg.Items.Add(li);


        //    sb.Remove(0, sb.Length);
        //    sb.Append("select fnumber,'--'+rtrim(fname) fname  from CF_OA_Organization  where flevel=3 and fparent='" + num + "' ");
        //    sb.Append(" order by forder");
        //    DataTable dt2 = oa.GetTable(sb.ToString());
        //    for (int i2 = 0; i2 < dt2.Rows.Count; i2++)
        //    {
        //        num = dt2.Rows[i2]["fnumber"].ToString().Trim();
        //        li = new ListItem(dt2.Rows[i2]["fname"].ToString(), num);
        //        this.t_FOAorg.Items.Add(li);

        //        sb.Remove(0, sb.Length);
        //        sb.Append("select fnumber,'----'+rtrim(fname) fname  from CF_OA_Organization ");
        //        sb.Append(" where flevel=4 and fparent='" + num + "' ");
        //        sb.Append(" order by forder");
        //        DataTable dt3 = oa.GetTable(sb.ToString());
        //        for (int i3 = 0; i3 < dt3.Rows.Count; i3++)
        //        {
        //            num = dt3.Rows[i3]["fnumber"].ToString().Trim();
        //            li = new ListItem(dt3.Rows[i3]["fname"].ToString(), num);
        //            this.t_FOAorg.Items.Add(li);

        //        }
        //    }
        //}


        ////oa菜单角色
        //sb.Remove(0, sb.Length);
        //sb.Append(" select FName,FID from CF_OA_UserGroup where fisdeleted=0 order by fnumber ");
        //dt = oa.GetTable(sb.ToString());
        //t_FOAmenuRole.DataSource = dt;
        //t_FOAmenuRole.DataTextField = "FName";
        //t_FOAmenuRole.DataValueField = "FID";
        //t_FOAmenuRole.DataBind();
        //t_FOAmenuRole.Items.Insert(0, new ListItem("请选择", ""));

        Govdept1.fNumber = ComFunction.GetDefaultCityDept();
        Govdept1.Dis(1);
    }

    string FAppId = string.Empty;
    //显示附件
    private void showFileList()
    {

        if (!string.IsNullOrEmpty(Request["fid"]))
        {
            FAppId = Request["fid"];
        }
        
        //当前业务类型
        int FManageTypeId = 29800;
        ProjectDB db = new ProjectDB();
        var v = from t in db.CF_Sys_PrjList
                where t.FManageType == FManageTypeId
                orderby t.FOrder
                select new
                {
                    t.FId,
                    t.FFileName,
                    t.FFileAmount,
                    t.FRemark,
                    t.FOrder,
                    FIsMust = t.FIsMust == 1 ? "<font color='red'>是</font>" : "否",
                    AppFile = db.CF_AppPrj_FileOther.Where(f => t.FId == f.FPrjFileId && f.FAppId == FAppId)
                };


        rep_List.DataSource = v;
        rep_List.DataBind();
    }
    //一层列表
    protected void rep_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            IQueryable<CF_AppPrj_FileOther> AppFile = DataBinder.Eval(e.Item.DataItem, "AppFile") as IQueryable<CF_AppPrj_FileOther>;
            if (AppFile != null && AppFile.Count() > 0)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = AppFile.Count().ToString();
                ((Literal)e.Item.FindControl("lit_Has")).Text = "<font color='green'>是</font>";


                Repeater rep_File = (Repeater)e.Item.FindControl("rep_File");
                rep_File.DataSource = AppFile;
                rep_File.DataBind();
            }
            else
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = "0";
                ((Literal)e.Item.FindControl("lit_Has")).Text = "<font color='red'>否</font>";
            }
            if (string.IsNullOrEmpty(FAppId))
            {
                ((Literal)e.Item.FindControl("lit_Has")).Text += "<input type=\"button\" class=\"m_btn_w4\" value=\"上传文件\" onclick='alert(\"请先保存上方用户信息\")' />";
            }
            else
            {
                ((Literal)e.Item.FindControl("lit_Has")).Text += "<input type=\"button\" class=\"m_btn_w4\" value=\"上传文件\" onclick='showAddWindow(\"../AppMain/FileUp.aspx?FAppId=" + FAppId + "&FPrjFileId=" + FID + "\",500,250);' />";
            }
        }

    }

    //二层列表 事件
    protected void rep_File_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "cnDel")
        {
            string FID = e.CommandArgument.ToString();
            ProjectDB db = new ProjectDB();

            CF_AppPrj_FileOther v = db.CF_AppPrj_FileOther.Where(t => t.FId == FID).FirstOrDefault();
            if (v != null)
            {
                db.CF_AppPrj_FileOther.DeleteOnSubmit(v);
                db.SubmitChanges();

                pageTool tool = new pageTool(this.Page);
                tool.showMessage("删除成功");

               
                showFileList();
            }
        }
    }
    //上传合同附件
    protected void btnShowFile_Click(object sender, EventArgs e)
    {
        showFileList();
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
            partBindInfo(EConvert.ToInt(String.IsNullOrEmpty(Govdept1.FNumber) ? "0" : Govdept1.FNumber));
            //ddlPartType.SelectedIndex = ddlPartType.Items.IndexOf(ddlPartType.Items.FindByValue(dt.Rows[0]["FDepartmentID"].ToString()));
            ddlPartType.Text = dt.Rows[0]["FDepartment"].ToString();
            comBindInfo();
            //t_FCompany.SelectedValue = dt.Rows[0]["FCompany"].ToString();
            t_FCompanyName.Text = dt.Rows[0]["FCompanyName"].ToString();
        }


    }

    //显示权限列表
    private void showList()
    {
        string FUserId = EConvert.ToString(ViewState["FID"]);
        if (!string.IsNullOrEmpty(FUserId))
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from CF_Sys_UserRight where fisdeleted=0 and fuserid='" + FUserId + "'");
            DataTable dt = sh.GetTable(sb.ToString());
            DG_Rights.DataSource = dt;
            DG_Rights.DataBind();
            if (DG_Rights != null && DG_Rights.Rows.Count > 0)
            {
                //btnRoleSet.Visible = true;
                btnRoleSet.Attributes.Add("onclick", "showAddWindow('ManMenuRoleSet.aspx?FUserId=" + FUserId + "',800,500)");
            }
            else
                btnRoleSet.Visible = false;
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
        SortedList sl = tool.getPageValue();
        //记录日志
        CF_Prj_Log log = new CF_Prj_Log();
        string soo = "insert";
        if (this.ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());

            soo = "Update";
            log.FId = Guid.NewGuid().ToString();
            log.FUserId = this.ViewState["FID"].ToString();
            log.FCreateTime = DateTime.Now;
            log.FType = 2;//修改
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FType", 1);//管理部门用户
            FAppId = sl["FID"].ToString();

            log.FId = Guid.NewGuid().ToString();
            log.FUserId = sl["FID"].ToString();
            log.FCreateTime = DateTime.Now;
            log.FType = 1;//新增

        }

        sl.Add("FIsUserName", Check_FIsUserName.Checked);
        sl.Add("FDepartment", ddlPartType.Text.Trim());
        //sl.Add("FCompanyName", t_FCompanyName.Text.Trim());
        sl.Add("FPassWord", SecurityEncryption.DESEncrypt(txtFPassWord.Text.Trim()));
        if (sh.SaveEBase(EntityTypeEnum.EsUser, sl, "FID", so))
        {
            //GetOAUser(sl["FID"].ToString(), t_FLinkMan.Text, t_FOAorg.SelectedValue);//向OA更新信息
            UpDateLock();
            tool.showMessage("保存成功");
            HSaveResult.Value = "1";
            this.ViewState["FID"] = sl["FID"].ToString();
        }
        else
        {
            tool.showMessage("保存失败");
        }
        log.FUserName = t_FName.Text.Trim();
        log.FLockNumber = t_FLockNumber.Text.Trim();

        //保存到日志表中
        StringBuilder sb = new StringBuilder();
        if (soo == "insert")
        {
            sb.Append("insert CF_Prj_Log (fid,fcreatetime,fusername,flocknumber,fuserid,fcreateid,fcreatename,fisdeleted,ftype) values('" + log.FId + "','" + log.FCreateTime + "'");
            sb.Append(",'" + log.FUserName + "','" + log.FLockNumber + "','" + log.FUserId + "','" + Session["CreateID"] + "','" + Session["CreateName"] + "'," + 0 + "," + log.FType + ")");

        }
        if (soo == "Update")
        {
            sb.Append("insert CF_Prj_Log (fid,fcreatetime,fusername,flocknumber,fuserid,fcreateid,fcreatename,fisdeleted,ftype) values('" + log.FId + "','" + log.FCreateTime + "'");
            sb.Append(",'" + log.FUserName + "','" + log.FLockNumber + "','" + log.FUserId + "','" + Session["CreateID"] + "','" + Session["CreateName"] + "'," + 0 + "," + log.FType + ")");

        }
        sh.PExcute(sb.ToString());

        showFileList();
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

    #region OA相关的
    private void GetOAUser(string UserId, string linkMan, string oaorg)
    {
        OA oa = new OA();
        StringBuilder sb = new StringBuilder();
        DataTable oadt = oa.GetTable("select fid from cf_oa_emp where fid='" + UserId + "'");
        if (oadt != null && oadt.Rows.Count > 0)
        {
            sb.Append("update cf_oa_emp set FName='" + linkMan + "',FOrganId='" + oaorg + "',FTime=getdate()  ");
            sb.Append("where fid='" + UserId + "'");
        }
        else
        {
            sb.Append("insert into cf_oa_emp(FID,FName,FOrganId,FIsDeleted,FCreateTime,FTime) ");
            sb.Append("values ('" + UserId + "','" + linkMan + "','" + oaorg + "',0,getdate(),getdate()) ");
        }
        if (oa.PExcute(sb.ToString()))
        {
            Session["FEmpID"] = UserId;
        }

    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string fDeptNumber = this.Govdept1.FNumber;
        SaveInfo();
        this.Govdept1.FNumber = fDeptNumber;
    }



    //得到系统类型
    public string getSysName(string number)
    {
        return sh.getSystemName(number);
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showList();
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


    //列表
    protected void DG_Rights_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            string FState = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FState"));
            string FLockNumber = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FLockNumber"));
            DateTime FEndTime = EConvert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "FEndTime"));

            e.Row.Cells[3].Text = FState == "1" ? "正常" : FState == "0" ? "注销" : "";
            if (FEndTime < DateTime.Now)
            {
                e.Row.Cells[2].Text += "<font color='red'>已过期</font>";
            }

            Button del = (Button)e.Row.FindControl("btnDel");
            if (del != null)
            {
                del.Attributes.Add("onclick", "return confirm('确认删除吗？');");
            }
        }
    }
    protected void DG_Rights_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cnDel")
        {
            pageTool tool = new pageTool(this.Page);
            string FID = e.CommandArgument.ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(" delete from CF_Sys_UserRight where fid='" + FID + "'");
            if (sh.PExcute(sb.ToString()))
            {
                tool.showMessage("删除成功");
                showList();
            }
        }
    }

}
