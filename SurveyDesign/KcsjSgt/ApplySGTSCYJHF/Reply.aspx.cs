using System;
using System.Linq;
using System.Web.UI;
using ProjectData;
using Tools;

public partial class KcsjSgt_ApplySGTSCYJHF_Reply : Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (!IsPostBack)
        {
            btnSave.Attributes["onclick"] = "return checkInfo();";
            BindControl();
            showInfo();
        }
    }

    //绑定
    private void BindControl()
    {

    }

    //显示
    private void showInfo()
    {
        string FID = Request.QueryString["FID"];


        pageTool tool = new pageTool(this.Page);
        var v = (from t in db.CF_Prj_Reply
                 where t.FID == FID
                 select t).FirstOrDefault();
        if (v != null)
        {
            ViewState["FID"] = v.FID;
            tool.fillPageControl(v);

            btnSave.Visible = v.FState != 2;
        }
    }


    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="isReport">是否提交</param>
    private void saveInfo(bool isReport)
    {
        string FBaseinfoId = CurrentEntUser.EntId;
        pageTool tool = new pageTool(this.Page);
        CF_Prj_Reply r = db.CF_Prj_Reply.Where(t => t.FID == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
        if (r != null)
        {
            r = tool.getPageValue(r);
            r.FTxt = t_FTxt.Text;
            r.FAppDate = EConvert.ToDateTime(t_FAppDate.Text);
            r.FState = isReport ? 2 : 0;//1:已提交；0:暂存；2:已确认
            db.SubmitChanges();
            tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
        }
    }

    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo(true);
        btnSave.Visible = false;
    }
}
