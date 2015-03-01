using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;

public partial class KcsjSgt_ApplyKCJSXSC_EmpReport : Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Attributes["onclick"] = "return checkInfo();";

            BindControl();
            showInfo();
        }
    }

    //绑定默认
    private void BindControl()
    { 

        //问题类型 
        t_FRemark.DataSource = db.getDicList(499);
        t_FRemark.DataTextField = "FName";
        t_FRemark.DataValueField = "FNumber";
        t_FRemark.DataBind();
        t_FRemark.Items.Insert(0, new ListItem("请选择", ""));
    }
    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string FEmpBaseinfoId = Request.QueryString["FEmpBaseinfoId"];
        string FAppId = Request.QueryString["FAppId"];
        string FId = Request.QueryString["FId"];

        var v = (from t in db.CF_Prj_Text
                 where t.FId == FId
                 select new
                 {
                     t.FId,
                     t.FTxt1,
                     t.FTxt2,
                     t.FRemark,
                     t.FContent

                 }).FirstOrDefault();

        if (v != null)
        {
            ViewState["FID"] = v.FId;
            tool.fillPageControl(v);
        }

    }

    //保存
    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;

        string FEmpBaseinfoId = Request.QueryString["FEmpBaseinfoId"];
        string FAppId = Request.QueryString["FAppId"];

        if (string.IsNullOrEmpty(FEmpBaseinfoId) || string.IsNullOrEmpty(FAppId))
        {
            tool.showMessageAndRunFunction("保存失败，参数不正确", "window.close();");
            return;
        }


        CF_Prj_Text T = db.CF_Prj_Text.Where(t => t.FId == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
        if (T == null)
        {
            T = new CF_Prj_Text();
            db.CF_Prj_Text.InsertOnSubmit(T);
            T.FId = Guid.NewGuid().ToString();
            T.FIsDeleted = false;
            T.FCreateTime = dTime;
            T.FAppId = FAppId;
            T.FUserId = FEmpBaseinfoId;
        }
        T.FTime = dTime; 
        T.FTxt2 = t_FTxt2.Text;
        T.FRemark = t_FRemark.Text;
        T.FContent = t_FContent.Text;


        db.SubmitChanges();
        tool.showMessageAndRunFunction("操作成功！", "window.returnValue=1;");
        ViewState["FID"] = T.FId;
    }


    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}
