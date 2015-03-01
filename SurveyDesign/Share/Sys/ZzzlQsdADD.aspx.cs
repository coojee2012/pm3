using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Tools;
using ProjectData;

public partial class Share_Sys_ZzzlQsdADD : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnSave.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["FID"]))
            {
                ViewState["FID"] = Request.QueryString["FID"];
            }
            ShowInfo();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        CF_Prj_ZzzlQsd emp = db.CF_Prj_ZzzlQsd.Where(t => t.FId == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
        if (emp != null)
        {
            tool.fillPageControl(emp);
        }

    }
    private void SaveInfo()
    {
        StringBuilder sb = new StringBuilder();
        pageTool tool = new pageTool(this.Page,"t_");
        CF_Prj_ZzzlQsd Emp = new CF_Prj_ZzzlQsd();
        string fId = EConvert.ToString(ViewState["FID"]);
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = db.CF_Prj_ZzzlQsd.Where(t => t.FId == fId).FirstOrDefault();
           
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FisDeleted = 0;
            Emp.FCreateTime = DateTime.Now;
            db.CF_Prj_ZzzlQsd.InsertOnSubmit(Emp);
        }
        Emp = tool.getPageValue(Emp);

        Emp.FTime = DateTime.Now;
        db.SubmitChanges();
        ViewState["FID"] = fId;
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        this.RegisterStartupScript("js", "<script>getPageValue(1);</script>");
    }
}
