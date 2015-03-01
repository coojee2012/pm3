using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Text;

public partial class KcsjSgt_main_Baseinfo1 : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["fbid"]))
        {
            FBaseInfoId = CurrentEntUser.EntId;
            FUserId = CurrentEntUser.UserId;
        }
        else
        {
            FBaseInfoId = Request.QueryString["fbid"];
            FUserId = Request.QueryString["frid"];
            this.ClientScript.RegisterStartupScript(this.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>try{btnEnable();}catch(e){}</script>");
            IsView = "1";
        }
        if (!IsPostBack)
        {

            conBind();

            showInfo();
        }
    }
    private string IsView { get; set; }
    private string FBaseInfoId { get; set; }
    private string FUserId { get; set; }
    private void conBind()
    {
        govd_FRegistDeptId.FNumber = ComFunction.GetDefaultCityDept();
        govd_FRegistDeptId.Dis(1);

        //资质等级
        c_FLevel.DataSource = db.getSysQualiLevel(160);
        c_FLevel.DataTextField = "FName";
        c_FLevel.DataValueField = "FNumber";
        c_FLevel.DataBind();
        c_FLevel.Items.Insert(0, new ListItem("请选择", ""));
    }


    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
        CF_Ent_BaseInfo Ent = db.CF_Ent_BaseInfo.Where(t => t.FId == FBaseInfoId && t.FJuridcialCode != null).FirstOrDefault();
        if (Ent != null)
        {
            tool.fillPageControl(Ent);
            if (Ent.FRegistDeptId.ToString() != "")
                govd_FRegistDeptId.FNumber = Ent.FRegistDeptId.ToString();

            //资质
            var c = db.CF_Ent_QualiCerti.Where(t => t.FBaseInfoId == Ent.FId).Select(t => new { t.FCertiNo, t.FLevel }).FirstOrDefault();
            if (c != null)
            {
                c_FCertiNo.Text = c.FCertiNo;
                c_FLevel.SelectedValue = c.FLevel.ToString();
            }

        }
        else
        {//还没有基本信息表时，从用户中取
            CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == CurrentEntUser.UserId).FirstOrDefault();
            if (user != null)
            {
                t_FName.Text = user.FCompany;
                t_FJuridcialCode.Text = user.FJuridcialCode;
                t_FLinkMan.Text = user.FLinkMan;
                t_FTel.Text = user.FTel;
                govd_FRegistDeptId.fNumber = user.FManageDeptId.ToString();
            }
        }
    }
    //保存
    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        CF_Ent_BaseInfo Ent = db.CF_Ent_BaseInfo.Where(t => t.FId == FBaseInfoId).FirstOrDefault();
        Ent.FTime = dTime;
        Ent.FLinkMan = t_FLinkMan.Text.Trim();
        Ent.FTel = t_FTel.Text.Trim();
        Ent.FEmail = t_FEMail.Text.Trim();
        Ent.FCall = t_FCall.Text.Trim();
        //更新CF_Sys_User表中的FCompany
        CF_Sys_User User = db.CF_Sys_User.Where(t => t.FBaseInfoId == FBaseInfoId).FirstOrDefault();
        if (User != null)
        {
            User.FLinkMan = t_FLinkMan.Text;
            User.FTel = t_FTel.Text;
        }
        db.SubmitChanges();
        tool.showMessage("保存成功");
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}
