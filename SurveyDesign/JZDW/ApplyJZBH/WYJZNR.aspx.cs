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
public partial class JZDW_ApplyJZBH_WYJZNR : EntAppPage
{
    ProjectDB db = new ProjectDB();
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            btnSave.Attributes["onclick"] = "return checkInfo();";
            showInfo();
        }
    }

    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string FAppId = EConvert.ToString(Session["FAppId"]);
        var v = (from t in db.CF_Prj_Data
                 where t.FAppId == FAppId
                 select new
                 {
                     t.FId,
                     t.FPrjId,
                     t.FInt0,
                     t.FInt1,
                     t.FInt2,
                     t.FInt3,
                     t.FInt4,
                     t.FInt5,
                     t.FFloat1,
                     t.FFloat2,
                     t.FFloat3,
                     t.FFloat4,
                     t.FFloat5,
                     t.FFloat6,
                     t.FFloat7,
                     t.FTxt5,
                     t.FTxt6,
                     t.FTxt7,
                     t.FTxt8,
                     t.FTxt9,
                     t.FTxt10,
                     t.FTxt11,
                     t.FTxt13,
                     t.FTxt14,
                     t.FTxt15,
                     t.FTxt16,
                     t.FTxt20,
                     t.FTxt21,
                 }).FirstOrDefault();
        if (v != null)
        {

            tool.fillPageControl(v);
        }
    }



    //保存
    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string FAppId = EConvert.ToString(Session["FAppId"]);
        DateTime dTime = DateTime.Now;

        //基本信息
        CF_Prj_Data data = db.CF_Prj_Data.Where(t => t.FAppId == FAppId).FirstOrDefault();
        if (data != null)
        {
            data = tool.getPageValue(data);
        }


        //提交保存
        db.SubmitChanges();
        tool.showMessage("保存成功");
    }



    //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}
