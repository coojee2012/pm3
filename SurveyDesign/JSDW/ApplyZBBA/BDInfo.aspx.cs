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

public partial class JSDW_ApplyZBBA_BDInfo : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["BDHFBAId"]))
            {

            }
            else
            {
                TC_BDHF_Record emp = dbContext.TC_BDHF_Record.Where(t => t.FId == Convert.ToString(Request.QueryString["BDHFBAId"])).FirstOrDefault();
                if (emp != null)
                {
                    ViewState["FPrjId"] = emp.FPrjId;
                    ViewState["FAppId"] = emp.FAppId;
                    //ViewState["ZBBAMC"] = emp.ZBBAMC;
                }
                ViewState["BDHFBAId"] = Request.QueryString["BDHFBAId"];
                ClientScript.RegisterStartupScript(this.GetType(), "hideTr", "<script>hideTr();</script>");
            }
            BindControl();
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                showInfo();
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
        //string type = EConvert.ToString(ViewState["ZBBAMC"]);
        ////企业资质及等级（专业）
        //if ("11221301" == type)//施工
        //{
        //    //企业资质及等级（序列）
        //    DataTable dt = rc.getDicTbByFNumber("105");
        //    t_LHTQYZZDJXL.DataSource = dt;
        //    t_LHTQYZZDJXL.DataTextField = "FName";
        //    t_LHTQYZZDJXL.DataValueField = "FNumber";
        //    t_LHTQYZZDJXL.DataBind();

        //    t_QYZZDJXL.DataSource = dt;
        //    t_QYZZDJXL.DataTextField = "FName";
        //    t_QYZZDJXL.DataValueField = "FNumber";
        //    t_QYZZDJXL.DataBind();
        //}
        //else if ("11221302" == type)//设计
        //{
        //    //企业资质及等级（序列）
        //    DataTable dt = rc.getDicTbByFNumber("134");
        //    t_LHTQYZZDJXL.DataSource = dt;
        //    t_LHTQYZZDJXL.DataTextField = "FName";
        //    t_LHTQYZZDJXL.DataValueField = "FNumber";
        //    t_LHTQYZZDJXL.DataBind();

        //    t_QYZZDJXL.DataSource = dt;
        //    t_QYZZDJXL.DataTextField = "FName";
        //    t_QYZZDJXL.DataValueField = "FNumber";
        //    t_QYZZDJXL.DataBind();
        //}
        //else if ("11221303" == type)//监理
        //{
        //    //企业资质及等级（序列）
        //    DataTable dt = rc.getDicTbByFNumber("135");
        //    t_LHTQYZZDJXL.DataSource = dt;
        //    t_LHTQYZZDJXL.DataTextField = "FName";
        //    t_LHTQYZZDJXL.DataValueField = "FNumber";
        //    t_LHTQYZZDJXL.DataBind();

        //    t_QYZZDJXL.DataSource = dt;
        //    t_QYZZDJXL.DataTextField = "FName";
        //    t_QYZZDJXL.DataValueField = "FNumber";
        //    t_QYZZDJXL.DataBind();
        //}
        //DataTable dt1 = rc.getDicTbByFNumber("112216");
        //t_LHTQYZZDJ.DataSource = dt;
        //t_LHTQYZZDJ.DataTextField = "FName";
        //t_LHTQYZZDJ.DataValueField = "FNumber";
        //t_LHTQYZZDJ.DataBind();

        //t_QYZZDJ.DataSource = dt;
        //t_QYZZDJ.DataTextField = "FName";
        //t_QYZZDJ.DataValueField = "FNumber";
        //t_QYZZDJ.DataBind();

        ////企业资质及等级（等级）
        //dt = rc.getDicTbByFNumber("112217");
        //t_LHTQYZZDJDJ.DataSource = dt;
        //t_LHTQYZZDJDJ.DataTextField = "FName";
        //t_LHTQYZZDJDJ.DataValueField = "FNumber";
        //t_LHTQYZZDJDJ.DataBind();

        //t_QYZZDJDJ.DataSource = dt;
        //t_QYZZDJDJ.DataTextField = "FName";
        //t_QYZZDJDJ.DataValueField = "FNumber";
        //t_QYZZDJDJ.DataBind();

        //建造师的等级
        DataTable dt = rc.getDicTbByFNumber("112218");
        t_ZBBM.DataSource = dt;
        t_ZBBM.DataTextField = "FName";
        t_ZBBM.DataValueField = "FNumber";
        t_ZBBM.DataBind();
    }
    //显示
    private void showInfo()
    {

        TC_BDHF_BD emp = dbContext.TC_BDHF_BD.Where(t => t.FId == Convert.ToString(ViewState["FID"])).FirstOrDefault();
        if (emp != null)
        {
            pageTool tool = new pageTool(this.Page);
            tool.fillPageControl(emp, divSetup2);
            ViewState["FPrjId"] = emp.FPrjId;
            ViewState["FAppId"] = emp.FAppId;
            if (true == emp.JSLHT)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "showTr", "<script>showTr();</script>");
            }
        }
    }
    //保存
    private void saveInfo()
    {
        string fId = EConvert.ToString(ViewState["FID"]);
        TC_BDHF_BD Emp = new TC_BDHF_BD();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_BDHF_BD.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FPrjId = EConvert.ToString(ViewState["FPrjId"]);
            Emp.FAppId = EConvert.ToString(ViewState["FAppId"]);
            Emp.BDHFBAId = EConvert.ToString(ViewState["BDHFBAId"]);
            dbContext.TC_BDHF_BD.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        ViewState["FID"] = fId;
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
        //     MyPageTool.showMessageAjax("保存成功ii", up_Main);
        //    MyPageTool.showMessageAndRunFunctionAjax("保存成功", "window.returnValue='1';", up_Main);
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void t_JSLHT_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (t_JSLHT.SelectedValue == "0")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hideTr", "<script>hideTr();</script>");
        }
        else if (t_JSLHT.SelectedValue == "1")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showTr", "<script>showTr();</script>");
        }
    }
    //protected void t_LHTQYZZDJXL_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    //protected void t_QYZZDJXL_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string type = t_QYZZDJXL.SelectedValue;
    //    if (!string.IsNullOrEmpty(type))
    //    {
    //        DataTable dt = rc.getDicTbByFNumber(type);
    //        t_QYZZDJ.DataSource = dt;
    //        t_QYZZDJ.DataTextField = "FName";
    //        t_QYZZDJ.DataValueField = "FNumber";
    //        t_QYZZDJ.DataBind();
    //    }
    //}
}