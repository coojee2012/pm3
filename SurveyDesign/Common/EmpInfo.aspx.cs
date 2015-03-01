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
using Approve.EntityBase;
using Approve.EntitySys;
using ProjectData;
using System.Linq;

public partial class Common_UserInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (db.getSysObjectContent("_CanModifyBaseInfo") == "1")
            {
                t_FName.ReadOnly
                = t_FIdCard.ReadOnly
                = t_FCertiNo.ReadOnly
                = t_FEndTime.ReadOnly
                = t_FPrintNo.ReadOnly
                = t_FRegistSpecialId.ReadOnly
                = t_FSealNo.ReadOnly
                = t_FUserName.ReadOnly
                    = false
                    ;
                t_FPersonTypeId.Enabled =
                    t_FTechId.Enabled =
                    t_FEndTime.Enabled = true;
            }
            else
            {
                t_FName.ReadOnly
              = t_FIdCard.ReadOnly
              = t_FCertiNo.ReadOnly
              = t_FEndTime.ReadOnly
              = t_FPrintNo.ReadOnly
              = t_FRegistSpecialId.ReadOnly
              = t_FSealNo.ReadOnly
              = t_FUserName.ReadOnly
                  = true;
                t_FPersonTypeId.Enabled =
       t_FTechId.Enabled =
       t_FEndTime.Enabled = false;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["fUserId"]))
            {
                ViewState["FID"] = Request.QueryString["fUserId"];
                BindControl();
                showInfo();
            }
            else
            {
                Response.Clear();
                Response.End();
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
    //显示
    private void showInfo()
    {
        Tools.pageTool tool = new Tools.pageTool(this.Page);
        CF_Emp_BaseInfo emp = db.CF_Emp_BaseInfo.Where(t => t.FId == Convert.ToString(ViewState["FID"])).FirstOrDefault();
        if (emp != null)
        {
            tool.fillPageControl(emp);
            string fPwd = emp.FPassword;
            if (!string.IsNullOrEmpty(fPwd))
                fPwd = SecurityEncryption.DESDecrypt(fPwd);
            txtFPassWord.Text = fPwd;
        }
    }
    //保存
    private void saveInfo()
    {
        Tools.pageTool tool = new Tools.pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        string fId = EConvert.ToString(ViewState["FID"]);
        //验证用户名是否重复
        if (db.CF_Emp_BaseInfo.Count(t => t.FId != Convert.ToString(ViewState["FID"]) && t.FUserName == t_FUserName.Text.Trim()) > 0)
        {
            tool.showMessage("用户名重复，请重新输入！");
            return;
        }
        CF_Emp_BaseInfo Emp = new CF_Emp_BaseInfo();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = db.CF_Emp_BaseInfo.Where(t => t.FId == fId).FirstOrDefault();
        }
        if (Emp == null)
        {
            tool.showMessage("数据有误，不能保存！");
            return;
        }
        Emp = tool.getPageValue(Emp);
        string fPwd = txtFPassWord.Text.Trim();
        if (!string.IsNullOrEmpty(fPwd))
            fPwd = SecurityEncryption.DESEncrypt(fPwd);
        Emp.FPassword = fPwd;
        Emp.FTime = dTime;
        db.SubmitChanges();
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}
