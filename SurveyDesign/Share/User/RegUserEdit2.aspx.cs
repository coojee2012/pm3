using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
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

public partial class Share_User_RegUserEdit2 : System.Web.UI.Page
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
            hidd_FRFID.Value = reg.FRFID;
            tool.fillPageControl(reg);

            //开通的权限
            var v = from r in db.CF_User_RegRight
                    join s in db.CF_Sys_SystemName on r.FSystemId equals s.FNumber
                    where r.FRegId == reg.FID
                    orderby s.FOrder
                    select new
                    {
                        r.FID,
                        r.FBaseInfoId,
                        s.FName,
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
                str = "<span style='color:red'>未开始</span>";
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
        string AppUserName = getUserName();//审核人



        string AppOpen = RBase.GetSysObjectName("_Sys_AppOpen");
        int no = 0, n = 0, count = 0;
        string UserId = hidd_FRFID.Value;
        DateTime d = DateTime.Now;
        CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == UserId).FirstOrDefault();
        if (user != null)
        {
            for (int i = 0; i < DG_Rights.Rows.Count; i++)
            {
                string FID = DG_Rights.DataKeys[i][0].ToString();
                string FNumber = DG_Rights.DataKeys[i][1].ToString();
                string FIsApp = DG_Rights.DataKeys[i][2].ToString();
                string FBaseId = DG_Rights.DataKeys[i][3].ToString();
                if (string.IsNullOrEmpty(FBaseId))
                    FBaseId = Guid.NewGuid().ToString();
                DropDownList dList = (DropDownList)DG_Rights.Rows[i].FindControl("drop_State");
                CheckBox cBox = (CheckBox)DG_Rights.Rows[i].FindControl("CheckItem");
                if (cBox.Checked)
                {
                    n++;
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
                        //插入到userRight 表
                        CF_Sys_UserRight userRight = new CF_Sys_UserRight();
                        userRight.FId = FID;
                        userRight.FUserId = user.FID;
                        user.FBaseInfoId = FBaseId;
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
                        db.CF_Sys_UserRight.InsertOnSubmit(userRight);
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
            if (n < 1)
            {
                tool.showMessage("请选择");
                return;
            }
            if (no == 1)
            {
                tool.showMessage("请选择审核结果");
                return;
            }
        }
        else
        {
            tool.showMessage("审核失败");
            return;
        }

        //CF_User_Reg reg = db.CF_User_Reg.Where(t => t.FID == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
        //if (reg != null)
        //{
        //    if (count == DG_Rights.Rows.Count)
        //        reg.FIsApp = 1;
        //    reg.FPassWord = SecurityEncryption.DESEncrypt(reg.FPassWord);//加密
        //}

        db.SubmitChanges();
        tool.showMessageAndRunFunction("审核成功", "window.returnValue=1;");
        ShowInfo();
    }

    //得到用户名
    private string getUserName()
    {
        EsUser user = (EsUser)sh.GetEBase(EntityTypeEnum.EsUser, "", "fid='" + Session["SH_UserID"] + "' ");
        if (user != null)
            return user.FName;
        else
            return "";
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