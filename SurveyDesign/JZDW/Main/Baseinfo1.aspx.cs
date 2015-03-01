using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;

public partial class JZDW_Main_Baseinfo1 : System.Web.UI.Page
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
            
            showInfo();
            
        }
    }
    private string IsView { get; set; }
    private string FBaseInfoId { get; set; }
    private string FUserId { get; set; }
    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
        ProjectDB db = new ProjectDB();
        CF_Ent_BaseInfo Ent = db.CF_Ent_BaseInfo.Where(t => t.FId == FBaseInfoId).FirstOrDefault();
        if (Ent != null)
        {
            tool.fillPageControl(Ent);
            //govd_FUpDeptId.FNumber = Ent.FUpDeptId.ToString();
            if (Ent.FRegistDeptId.ToString() != "")
                govd_FRegistDeptId.FNumber = Ent.FRegistDeptId.ToString();

        }
        else
        {

        }
        CF_Sys_User user = db.CF_Sys_User.Where(t => t.FBaseInfoId == FBaseInfoId).FirstOrDefault();
        if (user != null)
        {
            //t_FName.Text = user.FCompany;
            govd_FRegistDeptId.FNumber = user.FManageDeptId.ToString();

            if (string.IsNullOrEmpty(t_FJuridcialCode.Text))
            {
                t_FJuridcialCode.Text = user.FJuridcialCode;
            }
            if (string.IsNullOrEmpty(t_FLinkMan.Text))
            {
                t_FLinkMan.Text = user.FLinkMan;
            }
            if (string.IsNullOrEmpty(t_FTel.Text))
            {
                t_FTel.Text = user.FTel;
            }
        }

        //CF_Ent_Leader T = db.CF_Ent_Leader.Where(d => d.FPersonTypeId == 7021 && d.FBaseInfoId == CurrentEntUser.EntId).FirstOrDefault();

        //if (T != null)
        //{
        //    tool = new pageTool(this.Page, "n_");
        //    tool.fillPageControl(T);
        //}
    }


}
