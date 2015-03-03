using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using Approve.RuleCenter;
using Tools;
using System.Data;
using EgovaBLL;

public partial class JSDW_ApplySGXKZGL_SGTSCRY : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                txtFId.Value = Request.QueryString["fid"];
                showInfo();
            }
            else
            {
                ViewState["FAppId"] = EConvert.ToString(Session["FAppId"]);
                ViewState["FPrjItemId"] = Request.QueryString["fPrjItemId"];
                ViewState["FSGTSCId"] = Request.QueryString["fSGTSCId"];
                TC_SGXKZ_SGTSC Emp = dbContext.TC_SGXKZ_SGTSC.Where(t => t.FId == Request.QueryString["fSGTSCId"]).FirstOrDefault();
                ViewState["SC"] = Emp.SGTSCJGId;
                ViewState["KC"] = Emp.KCDWId;
                ViewState["SJ"] = Emp.SJDWId;
            }
            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool.ExecuteScript("btnEnable();");
            }
        }
    }
    void BindControl()
    {

        //承担角色
        DataTable dt = rc.getDicTbByFNumber("112220");
        t_CDJS.DataSource = dt;
        t_CDJS.DataTextField = "FName";
        t_CDJS.DataValueField = "FNumber";
        t_CDJS.DataBind();

        //证件类型
        dt = rc.getDicTbByFNumber("112203");
        t_ZJLX.DataSource = dt;
        t_ZJLX.DataTextField = "FName";
        t_ZJLX.DataValueField = "FNumber";
        t_ZJLX.DataBind();

        //注册类型及等级
        dt = rc.getDicTbByFNumber("112221");
        t_ZCLXJDJ.DataSource = dt;
        t_ZCLXJDJ.DataTextField = "FName";
        t_ZCLXJDJ.DataValueField = "FNumber";
        t_ZCLXJDJ.DataBind();
    }
    //显示
    private void showInfo()
    {

        TC_SGXKZ_SGTSCRY emp = dbContext.TC_SGXKZ_SGTSCRY.Where(t => t.FId == txtFId.Value).FirstOrDefault();
        if (emp != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(emp);
            TC_SGXKZ_SGTSC Emp = dbContext.TC_SGXKZ_SGTSC.Where(t => t.FId == emp.FSGTSCId).FirstOrDefault();
            ViewState["SC"] = Emp.SGTSCJGId;
            ViewState["KC"] = Emp.KCDWId;
            ViewState["SJ"] = Emp.SJDWId;
        }

    }
    //保存
    private void saveInfo()
    {
        string fId = txtFId.Value;
        TC_SGXKZ_SGTSCRY Emp = new TC_SGXKZ_SGTSCRY();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_SGXKZ_SGTSCRY.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FAppId = EConvert.ToString(ViewState["FAppId"]);
            Emp.FprjItemId = EConvert.ToString(ViewState["FPrjItemId"]);
            Emp.FSGTSCId = EConvert.ToString(ViewState["FSGTSCId"]);
            dbContext.TC_SGXKZ_SGTSCRY.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        txtFId.Value = fId;
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
        //     MyPageTool.showMessageAjax("保存成功ii", up_Main);
        //    MyPageTool.showMessageAndRunFunctionAjax("保存成功", "window.returnValue='1';", up_Main);
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    private void selEmp()
    {
        string selEmpId = t_RYId.Value;
        EgovaDB1 db = new EgovaDB1();
        var v = db.RY_RYJBXX.Where(t => t.RYBH == selEmpId).FirstOrDefault();
        if (v != null)
        {
            var v1 = db.QY_JBXX.Where(t => t.QYBM == v.QYBM).FirstOrDefault();
            t_RYXM.Text = v.XM;
            t_ZJHM.Text = v.SFZH;
            if (v1 != null)
            {
                t_DWMC.Text = v1.QYMC;
                t_DWZZJGDM.Text = v1.JGDM;
            }
        }
        


    }
    protected void btnAddEmp_Click(object sender, EventArgs e)
    {
        selEmp();
    }
}