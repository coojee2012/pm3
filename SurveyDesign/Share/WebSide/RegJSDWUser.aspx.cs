using System;
using System.Web;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Collections;
using System.Data;
using Approve.Common;
using System.Text;
using Seaskyer.Strings;
using Seaskyer.WebApp.Utility;
using ProjectData;
using System.Linq;
using System.Data.SqlClient;
using cn.gov.scjst.zw;

public partial class Share_WebSide_RegJSDWUser : System.Web.UI.Page
{
    Share sh = new Share();
    ShareTool st = new ShareTool();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.liC_TechSupport.Text = ComFunction.GetValueByName("TechSupport");//技术支持
            this.liC_webtel.Text = ComFunction.GetValueByName("WebTel");//电话
            this.liC_Developer.Text = ComFunction.GetValueByName("Developer");//开发单位
            this.t_FName.Text = GetUserName();
            ViewState["FID"] = Guid.NewGuid().ToString();
            ControlBind();
            t_FSystemId.SelectedValue = "100";//默认选择建设单位
            t_FSystemId_SelectedIndexChanged(sender, e);
            Govdept1.fNumber = ComFunction.GetDefaultDept();
            Govdept1.Dis(1);
        }

    }

    private void ControlBind()
    {
        ProjectDB db = new ProjectDB();
        var v = from t in db.CF_Sys_SystemName
                where t.FPlatId == 800
                && t.FNumber == 100
                orderby t.FOrder
                select new
                {
                    t.FNumber,
                    FDesc = t.FDesc.Replace("资质办理", ""),
                };


        t_FSystemId.DataSource = v;
        t_FSystemId.DataTextField = "FDesc";
        t_FSystemId.DataValueField = "FNumber";
        t_FSystemId.DataBind();

        //Govdept1.fNumber = ComFunction.GetDefaultCityDept();
        //Govdept1.Dis(1);
        //Govdept1.Dis(3);
        //Govdept1.Dis(1);

        //查询建设单位性质
        var dic = db.Dic.Where(t => t.FParentId == 181).OrderBy(t => t.FOrder);
        t_FEntTypeId.DataSource = dic;
        t_FEntTypeId.DataTextField = "FName";
        t_FEntTypeId.DataValueField = "FNumber";
        t_FEntTypeId.DataBind();
        t_FEntTypeId.Items.Insert(0, new ListItem("-请选择-", ""));
    }

    //保存
    private void SaveInfo()
    {
        ProjectDB db = new ProjectDB();
        string FSystemId = t_FSystemId.SelectedValue;
        pageTool tool = new pageTool(this.Page);

        #region 验证
        if (!isPass())
        {
            tool.showMessage("对不起，您今天的注册次数已达到上限！");
            return;
        }
        if (!vilideCode.IsPass(YZM.Text.Trim().ToLower()))
        {
            tool.showMessage("验证码输入错误");
            return;
        }
        t_FManageDeptId.Value = this.Govdept1.FNumber;//主管部门number
        if (t_FManageDeptId.Value == "")
        {
            Govdept1.FNumber = ComFunction.GetDefaultDept();
            tool.showMessage("请选择主管部门");
            return;
        }

        string FID = EConvert.ToString(ViewState["FID"]);
        if (!string.IsNullOrEmpty(t_FCompany.Text))
        {
            int n = db.CF_Sys_User.Count(t => t.FID != FID && t.FCompany == t_FCompany.Text && t.FSystemId == FSystemId);
            if (n > 0)
            {
                tool.showMessage("企业名称已存在！");
                t_FCompany.Focus();
                return;
            }
            n = db.CF_User_Reg.Count(t => t.FID != FID && t.FCompany == t_FCompany.Text && t.FIsApp.GetValueOrDefault(0) == 0);
            if (n > 0)
            {
                tool.showMessage("该企业名称正在已注册过，状态为等待审核！");
                t_FCompany.Focus();
                return;
            }
        }
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            int n = db.CF_Sys_User.Count(t => t.FID != FID && t.FName == t_FName.Text);
            if (n > 0)
            {
                tool.showMessage("用户名已存在！");
                t_FName.Focus();
                return;
            }
            n = db.CF_User_Reg.Count(t => t.FID != FID && t.FName == t_FName.Text && t.FSystemId.ToString() == FSystemId && t.FIsApp.GetValueOrDefault(0) == 0);
            if (n > 0)
            {
                tool.showMessage("该企业名称正在注册，状态为等待审核！");
                t_FCompany.Focus();
                return;
            }
        }

        if (!string.IsNullOrEmpty(t_FJuridcialCode.Text))//组织机构代码
        {
            int n = db.CF_Sys_User.Count(t => t.FID != FID && t.FJuridcialCode == t_FJuridcialCode.Text && t.FSystemId == FSystemId);
            if (n > 0)
            {
                tool.showMessage("组织机构代码已存在！");
                t_FJuridcialCode.Focus();
                return;
            }
            else
            {
                n = db.CF_User_Reg.Count(t => t.FID != FID && t.FJuridcialCode == t_FJuridcialCode.Text && t.FSystemId.ToString() == FSystemId && t.FIsApp.GetValueOrDefault(0) == 0);
                if (n > 0)
                {
                    tool.showMessage("该组织机构代码正在注册，状态为等待审核！");
                    t_FJuridcialCode.Focus();
                    return;
                }
            }
        }
        if (!string.IsNullOrEmpty(t_FLicence.Text))
        {
            int n = db.CF_Sys_User.Count(t => t.FID != FID && t.FSystemId == FSystemId && t.FLicence == t_FLicence.Text);
            if (n > 0)
            {
                tool.showMessage("营业执照号已存在！");
                t_FJuridcialCode.Focus();
                return;
            }
            else
            {
                n = db.CF_User_Reg.Count(t => t.FID != FID && t.FLicence == t_FLicence.Text && t.FSystemId.ToString() == FSystemId && t.FIsApp.GetValueOrDefault(0) == 0);
                if (n > 0)
                {
                    tool.showMessage("该营业执照号正在注册，状态为等待审核！");
                    t_FJuridcialCode.Focus();
                    return;
                }
            }
        }
        #endregion
        DateTime d = DateTime.Now;
        CF_User_Reg reg = new CF_User_Reg();
        reg.FID = Guid.NewGuid().ToString();
        reg.FBaseInfoId = Guid.NewGuid().ToString();
        reg.FIsDeleted = 0;
        reg.FCreateTime = d;
        reg.FTime = d;
        reg.FType = 2; //2:企业用户新申请 8:企业用户增开系统申请
        reg.FState = 0;
        reg.FIsApp = 0;//是否已审核
        reg.FSystemId = EConvert.ToInt(t_FSystemId.SelectedValue);
        reg.FCompany = t_FCompany.Text;
        reg.FManageDeptId = EConvert.ToInt(Govdept1.FNumber);
        reg.FAddressDept = EConvert.ToInt(Govdept1.FNumber);//地址
        reg.FLinkMan = t_FLinkMan.Text;//联系人
        reg.FTel = t_FTel.Text;//电话
        reg.FName = t_FName.Text;//用户名
        reg.FPassWord = SecurityEncryption.DESEncrypt(t_FPassWord.Value.Trim());//加密
        reg.FJuridcialCode = t_FJuridcialCode.Text;//组织机构代码
        reg.FLicence = t_FLicence.Text;//营业执照号
        reg.FBeginTime = d;
        reg.FEndTime = d.AddYears(1);
        db.CF_User_Reg.InsertOnSubmit(reg);

        int m = 0;
        foreach (ListItem item in r_FSysList.Items)
        {
            if (item.Selected)
            {
                CF_User_RegRight regRight = new CF_User_RegRight();
                regRight.FID = Guid.NewGuid().ToString();
                regRight.FRegId = reg.FID;
                regRight.FSystemId = EConvert.ToInt(item.Value);
                regRight.FType = reg.FType;
                regRight.FBaseInfoId = Guid.NewGuid().ToString();
                regRight.FState = reg.FState;
                regRight.FIsApp = reg.FIsApp;
                regRight.FCreateTime = d;
                regRight.FTime = d;
                regRight.FIsDeleted = 0;
                db.CF_User_RegRight.InsertOnSubmit(regRight);
                m++;
                //添加baseInfo表
                CF_Ent_BaseInfo ent = new CF_Ent_BaseInfo();
                ent.FId = regRight.FBaseInfoId;
                ent.FName = t_FCompany.Text;
                ent.FEntTypeId = EConvert.ToInt(t_FEntTypeId.SelectedValue);
                ent.FRegistAddress = t_FRegistAddress.Text.Trim();
                ent.FUpDeptId = ent.FRegistDeptId
                    = EConvert.ToInt(Govdept1.fNumber);
                ent.FOTxt5 = t_FOTxt5.Text.Trim();
                ent.FMobile = t_FMobile.Text.Trim();
                ent.FLinkMan = t_FLinkMan.Text.Trim();
                ent.FTel = t_FTel.Text.Trim();
                ent.FJuridcialCode = t_FJuridcialCode.Text.Trim();
                ent.FLicence = t_FLicence.Text.Trim();
                ent.FIdCard = t_FIdCard.Text.Trim();
                ent.FCall = t_FCall.Text.Trim();
                ent.FEmail = t_FEmail.Text.Trim();
                ent.FRemark = t_FRemark.Text.Trim();
                ent.FSystemId = EConvert.ToInt(t_FSystemId.SelectedValue);
                ent.FCreateTime = d;
                ent.FState = 0;
                ent.FTime = d;
                ent.FIsDeleted = false;
                db.CF_Ent_BaseInfo.InsertOnSubmit(ent);
            }
        }
        if (m < 1)
        {
            tool.showMessage("请选择要开通的系统权限！");
            return;
        }
        //判断企业类型是否为施工(包含外来施工)
        //如果是施工企业，则添加安全生产的权限
        if (FSystemId == "101" || FSystemId == "180")
        {
            //查询是否有该组织机构代码的安全生产企业
            CF_Sys_User user150 = db.CF_Sys_User.Where(t => t.FJuridcialCode == t_FJuridcialCode.Text.Trim() && t.FSystemId == "150").FirstOrDefault();
            if (user150 == null)
            {

                user150 = new CF_Sys_User();
                user150.FID = Guid.NewGuid().ToString();
                user150.FBaseInfoId = Guid.NewGuid().ToString();
                user150.FTime = user150.FCreateTime = d;
                user150.FIsDeleted = 0;
                user150.FLockLabelNumber = user150.FLockNumber = d.Ticks.ToString();
                user150.FName = d.Ticks.ToString();
                user150.FPassWord = reg.FPassWord;
                user150.FManageDeptId = reg.FManageDeptId;
                user150.FState = 1;
                user150.FType = 2;
                user150.FBeginTime = d;
                user150.FEndTime = d.AddYears(1);
                user150.FCompany = t_FCompany.Text.Trim();
                user150.FLinkMan = t_FLinkMan.Text;//联系人
                user150.FTel = t_FTel.Text;//电话
                user150.FJuridcialCode = t_FJuridcialCode.Text;//组织机构代码
                user150.FLicence = t_FLicence.Text;//营业执照号 
                user150.FSystemId = "150";
                db.CF_Sys_User.InsertOnSubmit(user150);

                //添加right表
                CF_Sys_UserRight right150 = new CF_Sys_UserRight();
                right150.FId = Guid.NewGuid().ToString();
                right150.FBaseinfoID = Guid.NewGuid().ToString();
                right150.FLockNumber = right150.FLockLabelNumber = user150.FLockNumber;
                right150.FName = user150.FName;
                right150.FPassWord = user150.FPassWord;
                right150.FUserId = user150.FID;
                right150.FIsDeleted = false;
                right150.FTime = right150.FCreateTime = d;
                right150.FDeptFrom = 1;
                right150.FBeginTime = user150.FBeginTime;
                right150.FEndTime = user150.FEndTime;
                right150.FSystemId = 150;
                right150.FRoleId = "651";
                right150.FMenuRoleId = "1651";
                right150.FState = 660101;
                db.CF_Sys_UserRight.InsertOnSubmit(right150);
            }
        }
        //查询是否有关联的企业（企业名称、营业执照、组织机构代码任一相同）
        //企业名称 

        string fCompanyId = string.Empty;
        string sCon = string.Empty;
        RCenter jst = new RCenter("dbJST");
        //if (!string.IsNullOrEmpty(t_FCompany.Text.Trim()))
        //{
        //    sCon = " FName='" + t_FCompany.Text.Trim() + "' ";
        //    string rn = string.Empty;
        //    DataTable dt = st.GetTABLE(sCon, "帐号表（网站）", out rn);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        fCompanyId = dt.Rows[0]["FCompanyId"].ToString();
        //        if (!string.IsNullOrEmpty(fCompanyId))
        //            fCompanyId = fCompanyId.Trim();
        //    }
        //}
        //if (string.IsNullOrEmpty(fCompanyId)
        //    && !string.IsNullOrEmpty(t_FLicence.Text.Trim()))//营业执照
        //{
        //    sCon = " FLicence='" + t_FLicence.Text.Trim() + "' ";
        //    string rn = string.Empty;
        //    DataTable dt = st.GetTABLE(sCon, "帐号表（网站）", out rn);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        fCompanyId = dt.Rows[0]["FCompanyId"].ToString();
        //        if (!string.IsNullOrEmpty(fCompanyId))
        //            fCompanyId = fCompanyId.Trim();
        //    }
        //}
        //if (string.IsNullOrEmpty(fCompanyId)
        //   && !string.IsNullOrEmpty(t_FJuridcialCode.Text.Trim()))//组织机构代码
        //{
        //    sCon = " FJuridcialCode='" + t_FJuridcialCode.Text.Trim() + "' ";
        //    string rn = string.Empty;
        //    DataTable dt = st.GetTABLE(sCon, "帐号表（网站）", out rn);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        fCompanyId = dt.Rows[0]["FCompanyId"].ToString();
        //        if (!string.IsNullOrEmpty(fCompanyId))
        //            fCompanyId = fCompanyId.Trim();
        //    }
        //}
        if (!string.IsNullOrEmpty(fCompanyId))
        {
            reg.FIsApp = 1;
        }
        db.SubmitChanges();
        //如果有关联的企业
        if (!string.IsNullOrEmpty(fCompanyId))
        {
            //设置为已经审核 
            //将数据同步到cf_Sys_User\cf_Sys_Userright表 
            //更新建设单位基本信息到建设厅数据库
            AppUserInfo(reg.FID, fCompanyId);
        }
        else
        {
            //建设单位可以不用审核直接登陆
            AppUserInfo(reg.FID, fCompanyId);
        }
        saveLoginTimes(); //保存登陆次数
        REdirect(reg.FID); //加密ID并跳转到注册成功页面。
    }
    //获取用户名
    private string GetUserName()
    {
        ProjectDB db = new ProjectDB();
        string FID = EConvert.ToString(ViewState["FID"]);
        string FName = GetRandomString();
        int n = db.CF_Sys_User.Count(t => t.FID != FID && t.FName == FName);
        if (n > 0)//重复
        {
            FName = GetUserName();
        }
        return FName;
    }
    //随机生成6为字符串
    private string GetRandomString()
    {
        Random rd = new Random();
        string str = "0123456789";
        string result = "";
        for (int i = 0; i < 6; i++)
        {
            result += str[rd.Next(str.Length)];
        }
        return result;
    }

    //审核用户
    private void AppUserInfo(string FID, string fCompanyId)
    {
        ProjectDB db = new ProjectDB();
        pageTool tool = new pageTool(this.Page);
        string FSystemId = t_FSystemId.SelectedValue;
        t_FManageDeptId.Value = this.Govdept1.FNumber;//主管部门number   
        string AppOpen = "1";
        string AppUserName = "";
        #region 转入User和UserRight
        DateTime d = DateTime.Now;
        CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == FID).FirstOrDefault();
        if (user == null) //看是否已经加有这个用户（user表）
        {
            user = new CF_Sys_User();
            user.FID = FID;
            user.FBaseInfoId = Guid.NewGuid().ToString();
            user.FTime = d;
            user.FCreateTime = d;
            user.FIsDeleted = 0;
            user.FState = 1;
            user.FType = 2;
            user.FBeginTime = d;
            user.FEndTime = AppOpen == "1" ? d.AddYears(1) : d.AddYears(1);
            user.FSystemId = t_FSystemId.SelectedValue;
            user.FLockNumber = t_FName.Text;
            user.FLockLabelNumber = t_FName.Text;
            user.FName = t_FName.Text;
            user.FPassWord = SecurityEncryption.DESEncrypt(t_FPassWord.Value.Trim());
            user.FManageDeptId = EConvert.ToInt(Govdept1.FNumber);
            user.FCompany = t_FCompany.Text;
            user.FCompanyId = fCompanyId;//关联的FId
            user.FLinkMan = t_FLinkMan.Text;
            user.FTel = t_FTel.Text;
            user.FJuridcialCode = t_FJuridcialCode.Text;
            user.FLicence = t_FLicence.Text;
            user.FIsUserName = 1; // 允许用户名登陆
            db.CF_Sys_User.InsertOnSubmit(user);
        }
        //查询权限
        var rightIdList = db.CF_User_RegRight.Where(t => t.FRegId == FID).Select(t => new { t.FID, t.FBaseInfoId, t.FSystemId }).ToList();
        foreach (var item in rightIdList)
        {
            string FBaseId = item.FBaseInfoId;
            if (string.IsNullOrEmpty(FBaseId))
                FBaseId = Guid.NewGuid().ToString();
            string FState = "1";
            if (FState == "1")//通过
            {
                CF_Sys_UserRight userRight = db.CF_Sys_UserRight.Where(t => t.FUserId == user.FID && t.FSystemId == item.FSystemId).FirstOrDefault();
                if (userRight != null)
                { }
                else
                {
                    //插入到userRight 表
                    userRight = new CF_Sys_UserRight();
                    userRight.FId = item.FID;
                    userRight.FUserId = user.FID;
                    userRight.FBaseinfoID = FBaseId;
                    userRight.FState = 660201;
                    userRight.FLockLabelNumber = user.FName;
                    userRight.FLockNumber = user.FName;
                    userRight.FName = user.FName;
                    userRight.FPassWord = user.FPassWord;
                    userRight.FDeptFrom = 1;
                    userRight.FBeginTime = d;
                    userRight.FEndTime = AppOpen == "1" ? d.AddYears(1) : d.AddDays(1);
                    userRight.FCreateTime = d;
                    userRight.FTime = d;
                    userRight.FIsDeleted = false;
                    userRight.FSystemId = item.FSystemId;
                    userRight.FIsUserName = 1;
                    db.CF_Sys_UserRight.InsertOnSubmit(userRight);

                    //更新厅数据库 如果是建设单位|建设单位子系统
                    //if (t_FSystemId.SelectedValue == "100"
                    //    && item.FSystemId == 1001)
                    //    UploadJSEnt(FBaseId);
                }
            }
            //更新这个注册RegRight
            CF_User_RegRight regRight = db.CF_User_RegRight.Where(t => t.FID == item.FID).FirstOrDefault();
            if (regRight != null)
            {
                regRight.FState = EConvert.ToInt(FState);
                regRight.FIsApp = 1;
                regRight.FAppUserName = AppUserName;
                regRight.FAppDate = d;
            }
        }
        #endregion
        db.SubmitChanges();
    }
    //审核通过的时候向厅数据库更新建设单位用户信息
    bool UploadJSEnt(string fBaseId)
    {
        //更新数据信息
        RCenter jst = new RCenter("dbJST");
        RCenter njs = new RCenter("dbNJS");
        SortedList sl = new SortedList();
        Approve.EntityBase.SaveOptionEnum so = Approve.EntityBase.SaveOptionEnum.Update;
        //查询
        if (jst.GetSQLCount("select count(*) from xm_JSDW_USER where fid='" + fBaseId + "'") <= 0)
        {
            sl.Add("CreateTime", DateTime.Now);
            sl.Add("CJSJ", DateTime.Now);
            so = Approve.EntityBase.SaveOptionEnum.Insert;
        }
        sl.Add("FID", fBaseId);
        sl.Add("FName", t_FName.Text.Trim());
        sl.Add("FPassWord", t_FPassWord.Value.Trim());
        //查询本地的数据信息，上传到建设厅数据库
        RCenter rc = new RCenter();
        StringBuilder sb = new StringBuilder();
        sb.Append("select b.FName FCompany,b.FRegistDeptId SSQYID,");
        sb.Append("b.FRegistAddress DWDZ,b.FOTxt5 FRXM,b.FMobile FRSJ,");
        sb.Append("b.FLinkMan LXR,b.FTel LXDH,b.FJuridcialCode ZZJGDM,");
        sb.Append("b.FLicence YYZZZCH,(select top 1 FName from cf_Sys_Dic where fNumber=b.FEntTypeId and FParentId=181)DWXZ,b.FEmail DZYX,");
        sb.Append("b.FIdCard identityNumber,b.FCall personPhone ");
        sb.Append("from cf_Ent_BaseInfo b where fid='" + fBaseId + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            foreach (DataColumn c in dt.Columns)
            {
                sl.Add(c.ColumnName, dr[c.ColumnName]);
            }
        }
        try
        {
            //jst.SaveEBase("xm_JSDW_USER", sl, "FID", so);
            njs.SaveEBase("xm_JSDW_USER", sl, "FID", so);
            return true;

        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// 加密ID并跳转到注册成功页面。
    /// </summary>
    /// <param name="UserId"></param>
    private void REdirect(string ID)
    {
        if (!string.IsNullOrEmpty(ID))
        {
            DateTime time = DateTime.Now.AddHours(1);
            string key = SecurityEncryption.DesEncrypt(ID + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
            this.Response.Redirect("RegJSDWUserSuccess.aspx?Key=" + HttpUtility.UrlEncode(key, Encoding.UTF8));
        }
    }

    /// <summary>
    /// 从cookies验证当前用户是否允许注册(判断当天注册次数)
    /// </summary>
    /// <returns></returns>
    private bool isPass()
    {
        bool rv = true;
        int m = EConvert.ToInt(sh.GetSysObjectContent("_Sys_EntRegTimes"));
        int n = 0;
        if (Request.Cookies["_SYS_QS_REGTimes"] != null)
        {
            string Key = Server.HtmlEncode(Request.Cookies["_SYS_QS_REGTimes"].Value);
            string[] strArray = Key.Split('|');
            if (strArray.Length == 2)
            {
                if (EConvert.ToDateTime(strArray[1]) == EConvert.ToDateTime(DateTime.Now.ToShortDateString()))
                    n = EConvert.ToInt(strArray[0]);
            }
        }

        if (n >= m)
        {
            rv = false;
        }
        return rv;
    }

    /// <summary>
    /// 保存注册次数到cookies
    /// </summary>
    private void saveLoginTimes()
    {
        int m = EConvert.ToInt(sh.GetSysObjectContent("_Sys_EntRegTimes"));
        int n = 0;
        if (Request.Cookies["_SYS_QS_REGTimes"] != null)
        {
            string Key = Server.HtmlEncode(Request.Cookies["_SYS_QS_REGTimes"].Value);
            string[] strArray = Key.Split('|');
            if (strArray.Length == 2)
            {
                if (EConvert.ToDateTime(strArray[1]) == EConvert.ToDateTime(DateTime.Now.ToShortDateString()))
                    n = EConvert.ToInt(strArray[0]);
            }
        }

        n++;

        Response.Cookies["_SYS_QS_REGTimes"].Value = n.ToString() + "|" + DateTime.Now.ToShortDateString();
        Response.Cookies["_SYS_QS_REGTimes"].Expires = DateTime.Now.AddDays(1);
    }

    protected void btnREG_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }

    //选择单位类型时
    protected void t_FSystemId_SelectedIndexChanged(object sender, EventArgs e)
    {
        r_FSysList.Items.Clear();
        ProjectDB db = new ProjectDB();
        string FSystemId = t_FSystemId.SelectedValue;
        string rSysList = db.CF_Sys_SystemName.Where(t => t.FNumber.ToString() == FSystemId).Select(t => t.FQUrl).FirstOrDefault();
        if (!string.IsNullOrEmpty(rSysList))
        {

            var v = (from s in db.CF_Sys_SystemName
                     where !s.FIsDeleted.Value && rSysList.Split(',').ToArray().Contains(s.FNumber.ToString())
                             && s.FNumber != 167//排除“注册城市规划师管理信息系统”，这个被合到“规划企业”系统了。
                     orderby s.FOrder
                     select new { s.FNumber, FName = s.FName == "建设单位" ? "选址意见管理" : s.FName }).ToList();

            r_FSysList.DataSource = v;
            r_FSysList.DataTextField = "FName";
            r_FSysList.DataValueField = "FNumber";
            r_FSysList.DataBind();

            if (FSystemId == "100" && r_FSysList.Items.Count > 1)
            {
                r_FSysList.Items.RemoveAt(1);
            }

            if (r_FSysList.Items.Count == 1)
            {//只有一个的话自动选择了。
                r_FSysList.Items[0].Selected = true;
            }

        }
    }
}
