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
using System.Linq;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using System.Data.SqlClient;
using ProjectData;

public partial class Share_User_EmpUserAdd : Page
{
    ProjectDB db = new ProjectDB();
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //     if (db.getSysObjectContent("_CanModifyBaseInfo") == "1")
            //     {
            //         t_FName.ReadOnly
            //         = t_FIdCard.ReadOnly
            //         = t_FCertiNo.ReadOnly
            //         = t_FEndTime.ReadOnly
            //         = t_FPrintNo.ReadOnly
            //         = t_FRegistSpecialId.ReadOnly
            //         = t_FSealNo.ReadOnly
            //         = txtEntName.ReadOnly
            //             = false
            //             ;
            //         btnSelectEnt.Visible =
            //         t_FPersonTypeId.Enabled =
            //             t_FTechId.Enabled =
            //             t_FEndTime.Enabled = true;
            //     }
            //     else
            //     {
            //         t_FName.ReadOnly
            //       = t_FIdCard.ReadOnly
            //       = t_FCertiNo.ReadOnly
            //       = t_FEndTime.ReadOnly
            //       = t_FPrintNo.ReadOnly
            //       = t_FRegistSpecialId.ReadOnly
            //       = t_FSealNo.ReadOnly
            //       = txtEntName.ReadOnly
            //           = true;
            //         btnSelectEnt.Visible=
            //         t_FPersonTypeId.Enabled =
            //t_FTechId.Enabled =
            //t_FEndTime.Enabled = false;
            //     }
            BindControl();
            btnSave.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            if (!string.IsNullOrEmpty(Request.QueryString["FID"]))
            {
                ViewState["FID"] = Request["fid"];
                
                ShowInfo();//显示基本信息 
            }
        }
    }
    void BindControl()
    {
        var dic = db.Dic.Where(t => t.FParentId == 123).OrderBy(t => t.FOrder).Select(t => new { t.FName, t.FNumber }).ToList();
        t_FPersonTypeId.DataSource = dic;
        t_FPersonTypeId.DataTextField = "FName";
        t_FPersonTypeId.DataValueField = "FNumber";
        t_FPersonTypeId.DataBind();
        t_FPersonTypeId.Items.Insert(0, new ListItem("--请选择--", ""));

        dic = db.Dic.Where(t => t.FParentId == 108).OrderBy(t => t.FOrder).Select(t => new { t.FName, t.FNumber }).ToList();
        t_FTechId.DataSource = dic;
        t_FTechId.DataTextField = "FName";
        t_FTechId.DataValueField = "FNumber";
        t_FTechId.DataBind();
        t_FTechId.Items.Insert(0, new ListItem("--请选择--", ""));
    }
    //显示基本信息
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Emp_BaseInfo where FID='" + ViewState["FID"] + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            txtFPassWord.Text = SecurityEncryption.DESDecrypt(dt.Rows[0]["FPassWord"].ToString());
            ShowEnt();
            //展示用户信息
            sb.Remove(0, sb.Length);
            sb.Append("select FTel,FName,FPassWord,FCANumber,FCACardId,FCAStartTime,FCAEndTime from cf_Sys_User where fid='" + ViewState["FID"] + "'");
            dt = sh.GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                tool = new pageTool(this, "tt_");
                tool.fillPageControl(dt.Rows[0]);
                t_FUserName.Text = dt.Rows[0]["FName"].ToString();
                txtFPassWord.Text = SecurityEncryption.DESDecrypt(dt.Rows[0]["FPassWord"].ToString());
            }
        }
    }

    //保存
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page, "tt_");
        #region 验证

        if (!string.IsNullOrEmpty(t_FUserName.Text))
        {
            DataTable dt = sh.GetTable("select fid from CF_Sys_UserRight where FName='" + t_FUserName.Text + "' and FUserId<>'" + EConvert.ToString(ViewState["FID"]) + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("用户名已存在！");
                t_FUserName.Focus();
                return;
            }
            dt = sh.GetTable("select fid from CF_Sys_User where FName='" + t_FUserName.Text + "' and FID<>'" + EConvert.ToString(ViewState["FID"]) + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("用户名已存在！");
                t_FUserName.Focus();
                return;
            }
            dt = sh.GetTable("select fid from CF_Emp_Baseinfo where FUserName='" + t_FUserName.Text + "' and FID<>'" + EConvert.ToString(ViewState["FID"]) + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("用户名已存在！");
                t_FUserName.Focus();
                return;
            }
        }

        #endregion
        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = tool.getPageValue();
        if (this.ViewState["FID"] != null)
        {
            sl.Add("FID", ViewState["FID"]);
            //查询是否有该用户了
            if (sh.GetSQLCount("select count(*) from cf_Sys_User where fid='" + ViewState["FID"] + "'") > 0)
                so = SaveOptionEnum.Update;
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            //sl.Add("FBaseInfoId", Guid.NewGuid().ToString()); 
            sl.Add("FCreateTime", DateTime.Now);
        }
        string pwd = SecurityEncryption.DESEncrypt(txtFPassWord.Text.Trim());
        sl.Add("FPassWord", pwd);
        sl.Add("FName", t_FUserName.Text.Trim());
        if (sh.SaveEBase(EntityTypeEnum.EsUser, sl, "FID", so))
        {
            //查询是否有人员
            tool = new pageTool(this.Page, "t_");
            sl = tool.getPageValue();
            so = SaveOptionEnum.Update;
            string fid = EConvert.ToString(ViewState["FID"]);
            if (string.IsNullOrEmpty(fid))
            {
                so = SaveOptionEnum.Insert;
                sl.Add("FCreateTime", DateTime.Now);
                sl.Add("FIsDeleted", 0);
            }
            sl.Add("FID", fid);
            sl.Add("FPassWord", pwd);
            sh.SaveEBase(EntityTypeEnum.EeBaseinfo, sl, "FID", so);
            tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
            ViewState["FID"] = sl["FID"].ToString();
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }


    protected void btnSelectEnt_Click(object sender, EventArgs e)
    {
        ShowEnt();
    }

    private void ShowEnt()
    {
        ProjectDB db = new ProjectDB();
        txtEntName.Text = (from u in db.CF_Sys_User
                           join r in db.CF_Sys_UserRight
                           on u.FID equals r.FUserId
                           where r.FBaseinfoID == t_FBaseinfoId.Value
                           select u.FCompany).FirstOrDefault();
    }
}
