using System;
using System.Data;
using System.Linq;
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
using ProjectData;

public partial class Share_User_AddCAEntUser : Page
{
    Share sh = new Share();
    ShareTool st = new ShareTool();
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
            if (sh.GetSysObjectContent("_Download_Binding") == "1")
            {
                btnDownload.Visible = true;
            }
        }
    }


    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select replace(FDesc,'资质办理','') FName,FNumber from CF_Sys_SystemName where FPlatId=800 and FName !='建设单位' order by FOrder ");
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

        sb.Remove(0, sb.Length);
        sb.Append(" select * from CF_Sys_ManageDept where fparentid = 51 and flevel =2");
        dt = sh.GetTable(sb.ToString());
        this.CityList.DataSource = dt;
        CityList.DataTextField = "FName";
        CityList.DataValueField = "FNumber";
        CityList.DataBind();

    }

    //显示基本信息
    private void ShowInfo()
    {
        ProjectDB db = new ProjectDB();
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from cf_sys_user where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {

            tool.fillPageControl(dt.Rows[0]);
            txtFPassWord.Text = SecurityEncryption.DESDecrypt(dt.Rows[0]["FPassWord"].ToString());
            this.Govdept1.FNumber = this.t_FManageDeptId.Value.Trim();

            string FAcceptMaterial = EConvert.ToString(dt.Rows[0]["FAcceptMaterial"]);
            string[] strArray = FAcceptMaterial.Split(',');




            foreach (string s in strArray)
            {

                CheckBox checkbox = this.FindControl("CheckBox" + s) as CheckBox;
                if (checkbox != null)
                {
                    checkbox.Checked = true;
                }
            }


            IQueryable<CF_Sys_UserCA> cas = db.CF_Sys_UserCA.Where(t => t.FUserID == this.ViewState["FID"].ToString());
            RepCA.DataKeyNames = new string[] { "FID" };
            RepCA.DataSource = cas;
            RepCA.DataBind();

            //if (dt.Rows[0]["FListType"] != null)
            //{
            //    if (EConvert.ToInt(dt.Rows[0]["FListType"]) == 0)
            //    {
            //        this.btnReadCA.Visible = true;
            //        this.btnSaveCA.Visible = false;

            //    }


            //    if (EConvert.ToInt(dt.Rows[0]["FListType"]) == 1)
            //    {
            //        this.btnReadCA.Visible = false;
            //        this.btnSaveCA.Visible = false;
            //        if (dt.Rows[0]["FCACardId"] != null && EConvert.ToString(dt.Rows[0]["FCACardId"]) != "")
            //        {
            //            string bh = sh.GetSignValue("select CASZZSBH from JKCWFDB_WORK_NJS.dbo.ZZJGAndCAInfo  where ZZJGDM='" + EConvert.ToString(dt.Rows[0]["FJuridcialCode"]) + "' and CAMMKH ='" + EConvert.ToString(dt.Rows[0]["FCACardId"]) + "' ");
            //            if (bh != null && bh != "")
            //            {
            //                if (sh.PExcute("update cf_sys_user set FCANumber='" + bh + "',FListType=2 where fid = '" + this.ViewState["FID"].ToString() + "'"))
            //                {
            //                    this.t_FCANumber.Text = bh;
            //                }
            //            }
            //        }


            //    }


            //    //if (EConvert.ToInt(dt.Rows[0]["FListType"]) == 2)
            //    //{
            //    //    this.btnReadCA.Visible = false;
            //    //    this.btnSaveCA.Visible = false;

            //    //}
            //}
            if (dt.Rows[0]["FSTAddress"] != null && EConvert.ToString(dt.Rows[0]["FSTAddress"]) != "")
            {
                string[] citys = EConvert.ToString(dt.Rows[0]["FSTAddress"]).Split(',');
                if (citys.Length > 0)
                {
                    foreach (string str in citys)
                    {
                        foreach (ListItem item in CityList.Items)
                        {
                            if (item.Value == str)
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
            }

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
            sl.Add("FListType", 0);
            sl.Add("FLockNumber", t_FName.Text);
        }
        StringBuilder sb = new StringBuilder();
        foreach (ListItem item in CityList.Items)
        {
            if (item.Selected)
            {
                sb.Append(item.Value + ",");
            }
        }
        if (sb != null && sb.ToString() != "")
        {
            sl.Add("FSTAddress", sb.ToString());
        }

        //菜单角色
        string fMenuRoleId = sh.GetSignValue("select FNumber from cf_Sys_Role where fsystemId='" + t_FSystemId.SelectedValue + "' and FTypeId=2");
        sl.Add("FMenuRoleId", fMenuRoleId);
        sl.Add("FIsUserName", 1);
        sl.Add("FPassWord", SecurityEncryption.DESEncrypt(txtFPassWord.Text.Trim()));
        string FAcceptMaterial = "";
        if (CheckBox1.Checked)
        {
            FAcceptMaterial += "1,";
        }
        if (CheckBox2.Checked)
        {
            FAcceptMaterial += "2,";
        }
        if (CheckBox3.Checked)
        {
            FAcceptMaterial += "3,";
        }
        if (CheckBox4.Checked)
        {
            FAcceptMaterial += "4,";
        }

        if (CheckBox5.Checked)
        {
            FAcceptMaterial += "5,";
        }
        sl.Add("FAcceptMaterial", FAcceptMaterial);
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
        //if (!string.IsNullOrEmpty(oldlockNumber))
        //{
        //    sb.Append(" update cf_sys_lock set fstate=0 where flocknumber ='" + oldlockNumber + "'");
        //}

        //sh.PExcute(sb.ToString());
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
        //string rn="";
        //cn.gov.scjst.zw.JSTJKWebService jstweb = new cn.gov.scjst.zw.JSTJKWebService();
        //DataTable dt1 = jstweb.GetTABLE("", "", "", 0, "", out rn);


        ShareTool st = new ShareTool();
        st.DownloadSingleUser(hiFUserId.Value, EConvert.ToInt(t_FSystemId.SelectedValue));
        ViewState["FID"] = hiFUserId.Value;
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "$('#btnReload').click()", true);

    }
    protected void btnReadCA_Click(object sender, EventArgs e)
    {
        pageTool op = new pageTool(this.Page);
        ShareTool st = new ShareTool();
        ProjectDB db = new ProjectDB();
        //t_FCANumber.Text = st.ReadCA(CaCerti.Value);
        if (this.t_FCACardId.Text == null || this.t_FCACardId.Text == "")
        {
            op.showMessage("请输入CA卡号。");
            return;
        }
        else
        {

            //CF_Sys_UserCA ca = db.CF_Sys_UserCA.Where(t => t.FCACardId == this.t_FCACardId.Text).FirstOrDefault();
            //if (ca != null)
            //{
            //    op.showMessage("CA卡号已注册过，请换另一张CA卡。");
            //    return;
            //}
        }


        if (this.ViewState["FID"] == null)
        {
            op.showMessage("请先保存基本信息。");
            return;
        }

        cn.org.bjca.userweb.BjcaCertService oSrv = new cn.org.bjca.userweb.BjcaCertService();
        oSrv.Url = "http://userweb.bjca.org.cn/bjcacertservice/BJCAService.asmx";
        oSrv.CredentialSoapHeaderValue = new cn.org.bjca.userweb.CredentialSoapHeader();
        oSrv.CredentialSoapHeaderValue.UserName = "scjst_tongbu";
        oSrv.CredentialSoapHeaderValue.PassWord = "scjst_tongbu";
        cn.org.bjca.userweb.CCertUserInfo oU = new cn.org.bjca.userweb.CCertUserInfo();
        InitUInfo(oU);
        if (string.IsNullOrEmpty(oU.UserId)) //用户姓名或单位名称，改内容为签发的证书名称
        {
            op.showMessage("错误：用户ID为空");
            return;
        }
        if (string.IsNullOrEmpty(oU.CommonName)) //用户姓名或单位名称，改内容为签发的证书名称
        {
            op.showMessage("单位名称不能为空");
            return;
        }
        if (string.IsNullOrEmpty(oU.PaperID)) //组织机构代码
        {
            op.showMessage("组织机构代码不能为空");
            return;
        }


        if (string.IsNullOrEmpty(oU.PostalAddress)) //邮政地址
        {
            op.showMessage("邮政地址不能为空");
            return;
        }
        if (string.IsNullOrEmpty(oU.TelephoneNumber)) //用户电话号码
        {
            op.showMessage("用户电话号码不能为空");
            return;
        }
        if (string.IsNullOrEmpty(oU.E_mail)) //用户电子邮件
        {
            op.showMessage("用户电子邮件不能为空");
            return;
        }
        if (string.IsNullOrEmpty(oU.Envsn)) //CA卡号
        {
            op.showMessage("CA卡号不能为空");
            return;
        }
        oU.TransName = this.t_FLinkMan.Text;
        if (string.IsNullOrEmpty(oU.TransName)) //经办人
        {
            op.showMessage("经办人不能为空");
            return;
        }
        if (string.IsNullOrEmpty(oU.TransTel)) //单位电话号码
        {
            op.showMessage("单位电话号码不能为空");
            return;
        }

        string sToSignData = getToSignData(oU);
        string sSigned = getSignedData(sToSignData);
        string[] sRet = oSrv.CertRequestReceive(oU, sSigned);
        string sMsg = string.Empty;
        if (sRet.Length > 0 && sRet[0] == "0")
        {

            //this.btnSaveCA.Visible = true;
            //this.btnReadCA.Visible = false;

        }
        else
        {

            switch (sRet[0].ToUpper())
            {
                case "EC302":
                    sMsg = "录入的数据有误";
                    break;
                case "EC303":
                case "EC304":
                    sMsg = "已经申请过了";
                    break;
                case "EC520":
                    sMsg = "证书系统内部错误";
                    break;
                default:
                    sMsg = sRet[0];
                    break;
            }
            //op.showMessage(sMsg);
        }

        CF_Sys_UserCA userCA = db.CF_Sys_UserCA.Where(t => t.FCACardId == t_FCACardId.Text).FirstOrDefault();
        if (userCA != null)
        {
            if (userCA.FUserID != EConvert.ToString(this.ViewState["FID"]))
            {
                op.showMessage("CA卡号重复");
            }
        }
        else
        {
            userCA = new CF_Sys_UserCA()
            {
                FID = Guid.NewGuid().ToString(),

                FCreateTime = DateTime.Now,
                FIsDeleted = 0,
                FListType = 1,
                FUserID = EConvert.ToString(this.ViewState["FID"]),
                FCACardId = this.t_FCACardId.Text,
                FJuridcialCode = this.t_FJuridcialCode.Text
            };
            db.CF_Sys_UserCA.InsertOnSubmit(userCA);
        }
        userCA.FTime = DateTime.Now;

        if (!string.IsNullOrEmpty(t_FCANumber.Text))
        {
            userCA.FCANumber = t_FCANumber.Text;
        }

        db.SubmitChanges();

        //sh.PExcute("update CF_Sys_User set FListType=1 where fid='" + EConvert.ToString(this.ViewState["FID"]) + "'");
        if (string.IsNullOrEmpty(sMsg))
        {
            op.showMessage("推送成功！");
        }
        else
        {
            if (string.IsNullOrEmpty(t_FCANumber.Text))
            {
                op.showMessage("推送失败," + sMsg);
            }
            else
            {
                op.showMessage("推送完成！");
            }
        }
        ShowInfo();
    }






    public String getToSignData(cn.org.bjca.userweb.CCertUserInfo userinfo)//演示获取用户对象组合的待签名数据
    {
        String toSignData = userinfo.TradeGuid +
                            userinfo.RuleSn +
                            userinfo.UserId +
                            userinfo.CommonName +
                            userinfo.PaperType +
                            userinfo.PaperID +
                            userinfo.ProvinceName +
                            userinfo.LocalityName +
                            userinfo.UnitName +
                            userinfo.DepartmentName +
                            userinfo.PostalAddress +
                            userinfo.PostalCode +
                            userinfo.AgentMan +
                            userinfo.TelephoneNumber +
                            userinfo.Fax +
                            userinfo.E_mail +
                            userinfo.MobileTelephone +

                            userinfo.InsuranceNumber +
                            userinfo.IcregistCode +
                            userinfo.LeagalPerson +
                            userinfo.TaxRegisterID +
                            userinfo.ExtendValue1 +
                            userinfo.ExtendValue2 +
                            userinfo.ExtendValue3 +
                            userinfo.ExtendValue4 +
                            userinfo.ExtendValue5 +
                            userinfo.ExtendValue6 +
                            userinfo.ExtendValue7 +
                            userinfo.ExtendValue8 +
                            userinfo.ExtendValue9 +
                            userinfo.ExtendValue10 +

                            userinfo.Envsn +
                            userinfo.TransName +
                            userinfo.TransTel +
                            userinfo.TransEmail +
                            userinfo.TransMobile +
                            userinfo.TransPertype +
                            userinfo.TransPaperID +
                            userinfo.TradeTime;

        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(toSignData));
    }


    /**
     * @param toSignData
     * @return
     */
    public string getSignedData(String toSignData)//演示数据签名
    {
        BCACOMLib.SecurityEngineV2Class bca = st.InitBCA();
        bca.SetWebAppName("scjstca");
        string strRan = bca.GenRandom(24);
        string strSignedData = bca.SignData(strRan);
        return strSignedData;
    }

    //private BCACOMLib.SecurityEngineV2Class InitBCA()
    //{
    //    BCACOMLib.SecurityEngineV2Class bjca;
    //    if (HttpContext.Current.Application["bjca"] == null)
    //    {
    //        HttpContext.Current.Application.Lock();
    //        try
    //        {
    //            bjca = new BCACOMLib.SecurityEngineV2Class();

    //            bjca.SetWebAppName("SecXV3COM");
    //            HttpContext.Current.Application["bjca"] = bjca;
    //        }
    //        finally
    //        {
    //            HttpContext.Current.Application.UnLock();
    //        }
    //    }
    //    //string strRan, strSignedData, strServerCert;

    //    //bjca = (BCACOMLib.SecurityEngineV2Class)Application["bjca"];
    //    //strRan = bjca.GenRandom(24);
    //    //strSignedData = bjca.SignData(strRan);
    //    //strServerCert = bjca.GetServerCertificate(2);
    //    //Session["Ran"] = strRan;
    //    bjca = (BCACOMLib.SecurityEngineV2Class)HttpContext.Current.Application["bjca"];
    //    return bjca;

    //}


    private void InitUInfo(cn.org.bjca.userweb.CCertUserInfo oU)
    {
        string sRuleSn = "1223";
        oU.RuleSn = sRuleSn;// "2063";//业务规则ID，由BJCA分配，文件证书和KEY证书使用不同的规则号
        string sV = EConvert.ToString(this.ViewState["FID"]);
        oU.TradeGuid = sV.Length > 20 ? sV.Substring(0, 20) : sV;//业务系统流水号(唯一)

        string sUID = EConvert.ToString(this.ViewState["FID"]);
        oU.UserId = sUID; //业务系统中的用户ID号, RA系统认为PaperID和UserId及PaperType相同的证书请求是同一个用户的同一次申请 。在证书下载完成后同步证书信息时，业务系统可以使用该字段值确定用户与证书的关联
        oU.CommonName = this.t_FCompany.Text; //用户姓名或单位名称，改内容为签发的证书名称
        oU.PaperType = "JJ"; //组织机构代码
        oU.PaperID = this.t_FJuridcialCode.Text;
        oU.IcregistCode = this.t_FJuridcialCode.Text; //
        oU.UnitName = this.t_FCompany.Text; //单位名称;
        oU.PostalAddress = this.t_FCompany.Text; //邮政地址
        oU.PostalCode = "610041"; //邮政编码
        oU.TelephoneNumber = this.t_FMobile.Text; //用户电话号码
        oU.E_mail = this.t_FEmail.Text; //用户电子邮件
        oU.MobileTelephone = this.t_FTel.Text; //用户手机号码
        oU.TradeTime = System.Text.RegularExpressions.Regex.Replace(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "-|\\s+|:", ""); //业务系统提交时间，格式如:20071120133456
        oU.Envsn = this.t_FCACardId.Text; //"318000100021360"; //Envsn        
        oU.ExtendValue1 = t_FIdCard.Text;
        oU.TransName = t_FLinkMan.Text;
        oU.TransEmail = this.t_FEmail.Text;
        oU.TransMobile = t_FTel.Text;
        oU.TransTel = t_FMobile.Text;
        //处理UserId 让同一企业可以重复申请
        oU.UserId = oU.UserId + "_" + oU.TradeTime;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        pageTool op = new pageTool();
        //t_FCANumber.Text = st.ReadCA(CaCerti.Value);
        if (this.t_FCACardId.Text == null || this.t_FCACardId.Text == "")
        {
            op.showMessage("请输入CA卡号。");
            return;
        }
        if (this.ViewState["FID"] == null)
        {
            op.showMessage("请先保存基本信息。");
            return;
        }
        //t_FCANumber.Text = st.ReadCA(CaCerti.Value);
        if (this.t_FCANumber.Text == null || this.t_FCANumber.Text == "")
        {
            op.showMessage("CA数字证书编号。");
            return;
        }

        sh.PExcute("update CF_Sys_User set FListType=2,FCANumber='" + this.t_FCANumber.Text + "' where fid='" + EConvert.ToString(this.ViewState["FID"]) + "'");

        op.showMessage("推送成功！");

        ShowInfo();
    }
    protected void RepCA_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
        string FJuridcialCode = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FJuridcialCode"));
        string FUserID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FUserID"));
        string FCACardId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FCACardId"));
        string FCANumber = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FCANumber"));
        Label lb1 = e.Item.FindControl("caid") as Label;
        lb1.Text = FCACardId;
        lb1 = e.Item.FindControl("canum") as Label;
        lb1.Text = FCANumber;

    }

    protected void RepCA_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        RepCA.EditIndex = -1;
        ShowInfo();
    }
    protected void RepCA_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        ProjectDB db = new ProjectDB();
        string FID = EConvert.ToString(RepCA.DataKeys[e.RowIndex].Value);
        CF_Sys_UserCA userCA = db.CF_Sys_UserCA.Where(t => t.FID == FID).FirstOrDefault();
        if (userCA != null)
        {
            TextBox txtFCACardId = RepCA.Rows[e.RowIndex].FindControl("txtFCACardId") as TextBox;
            if (txtFCACardId != null)
            {
                userCA.FCACardId = txtFCACardId.Text.ToString().Trim();
            }
            TextBox txtFCANumber = RepCA.Rows[e.RowIndex].FindControl("txtFCANumber") as TextBox;
            if (txtFCANumber != null)
            {
                userCA.FCANumber = txtFCANumber.Text.ToString().Trim();
            }
         
            db.SubmitChanges();
        }
        RepCA.EditIndex = -1;
        ShowInfo();
    }
    protected void RepCA_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RepCA.EditIndex = e.NewEditIndex;
        ShowInfo();
    }
    protected void RepCA_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ProjectDB db = new ProjectDB();
        string FID = EConvert.ToString(RepCA.DataKeys[e.RowIndex].Value);
        CF_Sys_UserCA userCA = db.CF_Sys_UserCA.Where(t => t.FID == FID).FirstOrDefault();
        if (userCA != null)
        {
            db.CF_Sys_UserCA.DeleteOnSubmit(userCA);
            db.SubmitChanges();
        }
        ShowInfo();
    }
}
