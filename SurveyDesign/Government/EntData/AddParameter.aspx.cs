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
using Approve.EntityBase;
public partial class JSDW_appmain_Parameter : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["IsView"]) && Request.QueryString["IsView"] == "1")
        {
            this.ClientScript.RegisterStartupScript(this.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>try{btnEnable();}catch(e){}</script>");

        }
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["fprjId"]))
            {
                Response.Clear();
                Response.End();
            }
            else
            {
                //判断，有该工程的任何业务，则不可再行删除
                if (db.CF_App_List.Count(t => t.FPrjId
                    == Request.QueryString["fprjId"]) > 0)
                {
                    btnSave.Enabled = btnSave.Visible = false;
                }
            }
           
            BindControl();
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                showInfo();
            }
        }
    }
    
    void BindControl()
    {

        DataTable dt = rc.getDicTbByFNumber(Request.QueryString["type"]);
        t_FType.DataSource = dt;
        t_FType.DataTextField = "FName";
        t_FType.DataValueField = "FNumber";
        t_FType.DataBind();
        t_FType.Items.Insert(0, new ListItem("--请选择--", ""));

    }
    //显示
    private void showInfo()
    {
        string FPrjItem = EConvert.ToString(ViewState["FID"]);
        var result = db.CF_Prj_Parameter.Where(t => t.FId == Convert.ToString(ViewState["FID"])).FirstOrDefault();

        if (result != null)
        {
            pageTool tool = new pageTool(this.Page);
            tool.fillPageControl(result);
        }
    }
    //保存
    private void saveInfo()
    {
        DateTime dTime = DateTime.Now;
        string fId = EConvert.ToString(ViewState["FID"]);
        CF_Prj_Parameter parameter = db.CF_Prj_Parameter.Where(t => t.FId == fId).FirstOrDefault();
        pageTool tool = new pageTool(this.Page);


        if (parameter == null)
        {
            parameter = new CF_Prj_Parameter();
            fId = Guid.NewGuid().ToString();
            parameter.FId = fId;
            parameter.FCreateTime = dTime;
            parameter.FIsDeleted = 0;
            db.CF_Prj_Parameter.InsertOnSubmit(parameter);
        }
        else
        {


        }

        parameter = tool.getPageValue(parameter);

        parameter.FPrjId = Request.QueryString["fprjId"];
        parameter.FPrjItem = Request.QueryString["FPrjItem"];
        parameter.FTime = dTime;
        db.SubmitChanges();
        ViewState["FID"] = fId;
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }


    protected void t_FType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FNumber=EConvert.ToInt(t_FType.SelectedValue);
       t_FUnit.Text= db.Dic.Where(t => t.FNumber == FNumber).Select(t => t.FRemark).FirstOrDefault();
    }
}
