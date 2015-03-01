using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Data;
public partial class JSDW_QMain_AddEmpInfo : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["IsView"]) && Request.QueryString["IsView"] == "1")
        {
            this.ClientScript.RegisterStartupScript(this.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>try{btnEnable();}catch(e){}</script>");

        }
        if (!IsPostBack)
        {
            if (db.getSysObjectContent("_CanModifyBaseInfo") == "1")
            {
                t_FName.ReadOnly
                = t_FIdCard.ReadOnly
                = t_FRegistSpecialId.ReadOnly
                = t_FSealNo.ReadOnly
                = t_FUserName.ReadOnly
                    = false
                    ;

                    t_FTechId.Enabled =true;
            }
            else
            {
                t_FName.ReadOnly
              = t_FIdCard.ReadOnly

              = t_FRegistSpecialId.ReadOnly
              = t_FSealNo.ReadOnly
              = t_FUserName.ReadOnly
                  = true;
       t_FTechId.Enabled=btnSave.Visible =false;
            }
            BindControl();
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                showInfo();
            }
        }
    }
    private void BindControl()
    {
        var dic = db.Dic.Where(t => t.FParentId == 108).OrderBy(t => t.FOrder).Select(t => new { t.FName, t.FNumber }).ToList();
        t_FTechId.DataSource = dic;
        t_FTechId.DataTextField = "FName";
        t_FTechId.DataValueField = "FNumber";
        t_FTechId.DataBind();
        t_FTechId.Items.Insert(0, new ListItem("--请选择--", ""));
    }
    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
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
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        string fId = EConvert.ToString(ViewState["FID"]);
        CF_Emp_BaseInfo Emp = db.CF_Emp_BaseInfo.Where(t => t.FId == fId).FirstOrDefault();
        if (Emp == null)
        {
            Emp = new CF_Emp_BaseInfo();
            db.CF_Emp_BaseInfo.InsertOnSubmit(Emp);
            Emp.FId = Guid.NewGuid().ToString();
            Emp.FIsDeleted = false;
            Emp.FCreateTime = dTime;
            Emp.FType = 3;//非注册人员
            Emp.FBaseInfoID = CurrentEntUser.EntId;
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
