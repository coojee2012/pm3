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

public partial class Share_WebSide_RegEntUser : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.liC_TechSupport.Text = ComFunction.GetValueByName("TechSupport");//技术支持
            this.liC_webtel.Text = ComFunction.GetValueByName("WebTel");//电话
            this.liC_Developer.Text = ComFunction.GetValueByName("Developer");//开发单位
            ViewState["FID"] = Guid.NewGuid().ToString();
            ControlBind();
            t_FSystemId_SelectedIndexChanged(sender, e);
        }
    }

    private void ControlBind()
    {
        ProjectDB db = new ProjectDB();
        var v = from t in db.CF_Sys_SystemName
                where t.FPlatId == 800
                && t.FNumber ==100
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
        t_FSystemId.Items.Insert(0, new ListItem("请选择", ""));
        t_FSystemId.SelectedValue = Request.QueryString["sys"];
      
        //Govdept1.fNumber = ComFunction.GetDefaultCityDept();
        //Govdept1.Dis(1);
        //Govdept1.Dis(3);
        //Govdept1.Dis(1);
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
                tool.showMessage("该企业名称正在注册，状态为等待审核！");
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
                regRight.FState = reg.FState;
                regRight.FIsApp = reg.FIsApp;
                regRight.FCreateTime = d;
                regRight.FTime = d;
                regRight.FIsDeleted = 0;
                db.CF_User_RegRight.InsertOnSubmit(regRight);
                m++;
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
        db.SubmitChanges();
        saveLoginTimes(); //保存登陆次数
        REdirect(reg.FID); //加密ID并跳转到注册成功页面。
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
            this.Response.Redirect("RegEntUserSuccess.aspx?Key=" + HttpUtility.UrlEncode(key, Encoding.UTF8));
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
        //如果选择了建设单位  
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

            if (FSystemId == "100")
            {
                ListItem l = r_FSysList.Items.FindByValue("8800");
                if (l != null)
                {
                    r_FSysList.Items.Remove(l);
                    l.Enabled = false;
                    l.Selected = true;
                    r_FSysList.Items.Insert(0, l);
                }
            }

            if (r_FSysList.Items.Count == 1)
            {//只有一个的话自动选择了。
                r_FSysList.Items[0].Selected = true;
            }

        }
    }
}
