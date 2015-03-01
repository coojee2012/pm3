using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Approve.EntityBase;
using System.Collections;
using System.Data.SqlClient;
using System.Drawing;
using Approve.EntitySys;
using ProjectData;
using Tools;
using ProjectBLL;
using Approve.RuleCenter;

public partial class Share_User_RegUserEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "if(CheckInfo()){if(checkCount()==0){alert('请选择'); return false;}else{return true;}}else{return false;}");
            ControlBind();
            if (!string.IsNullOrEmpty(Request.QueryString["FID"]))
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();//显示基本信息
            }
        }
    }

    //列表绑定
    private void ControlBind()
    {
        ProjectDB db = new ProjectDB();
        var v = from t in db.CF_Sys_SystemName
                where t.FPlatId == 800
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
    }

    //显示基本信息
    private void ShowInfo()
    {
        ProjectDB db = new ProjectDB();
        pageTool tool = new pageTool(this.Page);
        CF_User_Reg reg = db.CF_User_Reg.Where(t => t.FID == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
        if (reg != null)
        {
            tool.fillPageControl(reg);
            Govdept1.FNumber = t_FManageDeptId.Value.Trim();
            t_FPassWord.Text = SecurityEncryption.DESDecrypt(reg.FPassWord);//解密


            //开通的权限
            var v = from r in db.CF_User_RegRight
                    join s in db.CF_Sys_SystemName on r.FSystemId equals s.FNumber
                    where r.FRegId == reg.FID
                    orderby s.FOrder
                    select new
                    {
                        r.FID,
                        r.FBaseInfoId,
                        FName = s.FName,
                        s.FNumber,
                        r.FState,
                        r.FIsApp
                    };

            DG_Rights.DataKeyNames = new string[] { "FID", "FNumber", "FIsApp", "FBaseInfoId" };
            DG_Rights.DataSource = v;
            DG_Rights.DataBind();

            string str = "";
            int n = v.Count(t => t.FIsApp == 1);
            if (n == 0)
            {
                str = "<span style='color:red'>未审核</span>";
            }
            else if (n < v.Count())
            {
                str = "<span style='color:blue'>审核中</span>";
            }
            else
            {
                str = "<span style='color:green;'>已审核</span>";
            }
            if (reg.FIsApp == 1)
            {
                str = "<span style='color:green;'>已审核</span>";
            }
            lit_FState.Text = str;
        }
    }

    //保存
    private void SaveInfo()
    {
        ProjectDB db = new ProjectDB();
        pageTool tool = new pageTool(this.Page);
        string FSystemId = t_FSystemId.SelectedValue;
        #region 系统审核.验证

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
        }
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            int n = db.CF_Sys_User.Count(t => t.FID != FID && t.FName == t_FName.Text && t.FSystemId == FSystemId);
            if (n > 0)
            {
                tool.showMessage("用户名已存在！");
                t_FName.Focus();
                return;
            }
        }
        if (!string.IsNullOrEmpty(t_FJuridcialCode.Text))
        {
            int n = db.CF_Sys_User.Count(t => t.FID != FID && t.FJuridcialCode == t_FJuridcialCode.Text && t.FSystemId == FSystemId);
            if (n > 0)
            {
                tool.showMessage("组织机构代码已存在！");
                t_FJuridcialCode.Focus();
                return;
            }
        }
        if (!string.IsNullOrEmpty(t_FLicence.Text))
        {
            int n = db.CF_Sys_User.Count(t => t.FID != FID && t.FLicence == t_FLicence.Text && t.FSystemId == FSystemId);
            if (n > 0)
            {
                tool.showMessage("营业执照号已存在！");
                t_FJuridcialCode.Focus();
                return;
            }
        }
        #endregion

        string AppOpen = RBase.GetSysObjectName("_Sys_AppOpen");
        string AppUserName = getUserName();//审核人

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
            user.FPassWord = SecurityEncryption.DESEncrypt(t_FPassWord.Text.Trim());//加密
            user.FManageDeptId = EConvert.ToInt(Govdept1.FNumber);
            user.FCompany = t_FCompany.Text;
            user.FLinkMan = t_FLinkMan.Text;
            user.FTel = t_FTel.Text;
            user.FJuridcialCode = t_FJuridcialCode.Text;
            user.FLicence = t_FLicence.Text;
            user.FIsUserName = 1; // 允许用户名登陆
            db.CF_Sys_User.InsertOnSubmit(user);
        }

        int count = 0;
        int no = 0;
        int m = 0;
        for (int i = 0; i < DG_Rights.Rows.Count; i++)
        {
            FID = DG_Rights.DataKeys[i][0].ToString();
            string FNumber = DG_Rights.DataKeys[i][1].ToString();
            string FIsApp = DG_Rights.DataKeys[i][2].ToString();
            string FBaseId = DG_Rights.DataKeys[i][3].ToString();
            if (string.IsNullOrEmpty(FBaseId))
                FBaseId = Guid.NewGuid().ToString();

            DropDownList dList = (DropDownList)DG_Rights.Rows[i].FindControl("drop_State");
            CheckBox cBox = (CheckBox)DG_Rights.Rows[i].FindControl("CheckItem");
            if (cBox.Checked)
            {
                m++;
                count++;
                string FState = dList.SelectedValue;
                if (string.IsNullOrEmpty(FState))
                {
                    DG_Rights.Rows[i].BackColor = Color.Red;
                    no = 1;
                    continue;
                }
                if (FState == "1")//通过
                {
                    CF_Sys_UserRight userRight = db.CF_Sys_UserRight.Where(t => t.FUserId == user.FID && t.FSystemId == EConvert.ToInt(FNumber)).FirstOrDefault();
                    if (userRight != null)
                    {
                        tool.showMessage("该用户已有“" + DG_Rights.Rows[i].Cells[2].Text + "”权限。\\n\\r \\n\\r 该系统申请应为“不通过”！");
                        return;
                    }
                    else
                    {
                        //插入到userRight 表
                        userRight = new CF_Sys_UserRight();
                        userRight.FId = FID;
                        userRight.FUserId = user.FID;
                        userRight.FBaseinfoID = FBaseId;
                        userRight.FState = 660101;
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
                        userRight.FSystemId = EConvert.ToInt(FNumber);
                        userRight.FIsUserName = 1;
                        db.CF_Sys_UserRight.InsertOnSubmit(userRight);

                        //更新厅数据库 如果是建设单位|建设单位子系统
                        if (t_FSystemId.SelectedValue == "100"
                            && FNumber == "1001")
                            UploadJSEnt(FBaseId);
                    }
                }
                //更新这个注册RegRight
                CF_User_RegRight regRight = db.CF_User_RegRight.Where(t => t.FID == FID).FirstOrDefault();
                if (regRight != null)
                {
                    regRight.FState = EConvert.ToInt(FState);
                    regRight.FIsApp = 1;
                    regRight.FAppUserName = AppUserName;
                    regRight.FAppDate = d;

                }
            }
            else if (FIsApp == "1")
            {
                count++;
            }
        }
        if (m < 1)
        {
            tool.showMessage("请选择");
            return;
        }
        if (no == 1)
        {
            tool.showMessage("请选择审核结果");
            return;
        }

        #endregion


        CF_User_Reg reg = db.CF_User_Reg.Where(t => t.FID == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
        if (reg != null)
        {
            if (count == DG_Rights.Rows.Count)
                reg.FIsApp = 1;

        }

        db.SubmitChanges();
        tool.showMessageAndRunFunction("审核成功", "window.returnValue=1;");
        ShowInfo();
    }
    //审核通过的时候向厅数据库更新建设单位用户信息
    bool UploadJSEnt(string fBaseId)
    {
        //更新数据信息
        RCenter jst = new RCenter("dbJST");
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
        sl.Add("FPassWord", t_FPassWord.Text.Trim());
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
        return jst.SaveEBase("xm_JSDW_USER", sl, "FID", so);
    }
    //得到用户名
    private string getUserName()
    {
        ProjectDB db = new ProjectDB();
        return db.CF_Sys_User.Where(t => t.FID == EConvert.ToString(Session["SH_UserID"])).Select(t => t.FName).FirstOrDefault();
    }

    protected void DG_Rights_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[1].Text = (e.Row.RowIndex + 1).ToString();
            string FIsApp = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FIsApp"));//是否审核
            string FState = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FState"));//审核结果
            if (FIsApp == "1")
            {
                e.Row.Cells[3].Text = "<span style='color:green'>已审核</span>";
                CheckBox cBox = (CheckBox)e.Row.FindControl("CheckItem");
                cBox.Enabled = false;

                DropDownList dList = (DropDownList)e.Row.FindControl("drop_State");
                try
                {
                    dList.SelectedValue = FState;
                }
                catch (Exception ex) { }
            }
            else
            {
                e.Row.Cells[3].Text = "<span style='color:red'>未审核</span>";
            }
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }


    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
