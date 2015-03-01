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
using Approve.RuleCenter;
using System.Text;

public partial class KC_ApplyKCXXBA_SaveIdea : EntAppPage
{
    ProjectDB db = new ProjectDB();
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            showInfo();
        }
    }


    //显示
    private void showInfo()
    {
        string FAppId = EConvert.ToString(Session["FAppId"]);
        pageTool tool = new pageTool(this.Page);
        string FID = Request.QueryString["FID"];

        var v = (from d in db.CF_Prj_Data
                 join a in db.CF_App_List on d.FAppId equals a.FId
                 where a.FId == FAppId
                 select new
                 {
                     d.FId,
                     d.FTxt5,
                     d.FTxt6,
                     d.FDate6,
                     d.FDeptName
                 }).FirstOrDefault();

        if (v != null)
        {
            ViewState["FLinkId"] = v.FId;

            tool.fillPageControl(v);
        }
        if (string.IsNullOrEmpty(t_FTxt5.Text))
        {
            var app = (from t in db.CF_App_List
                       join a in db.CF_App_List on t.FPrjId equals a.FPrjId
                       join e in db.CF_Ent_BaseInfo on t.FToBaseinfoId equals e.FId
                       where a.FId == FAppId && t.FManageTypeId == 28001 && t.FState == 6
                       select e.FName).FirstOrDefault();
            if (!string.IsNullOrEmpty(app))
            {
                t_FTxt5.Text = app;
            }
            else//如果没有见证单位
            {
                tr_tip.Visible = true;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string FAppId = EConvert.ToString(Session["FAppId"]); 
        pageTool tool = new pageTool(this.Page, "t_");
        //保存受理信息
        CF_Prj_Data data = db.CF_Prj_Data.Where(t => t.FAppId == FAppId).FirstOrDefault();
        if (data == null)
        {
            data = new CF_Prj_Data();
            data.FIsDeleted = false;
            data.FCreateTime = DateTime.Now;
        }
        data = tool.getPageValue(data);
        db.SubmitChanges();

        tool.showMessage("保存成功");

    }
}
