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

public partial class JSDW_ApplyAQJDBA_Participat : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            if (string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
               
            }
            else
            {
                TC_AJBA_CJDW emp = dbContext.TC_AJBA_CJDW.Where(t => t.FId == Convert.ToString(Request.QueryString["fid"])).FirstOrDefault();
                if (emp != null)
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.fillPageControl(emp);
                }
                ViewState["FID"] = Request.QueryString["fid"];
            }
            
            if (!string.IsNullOrEmpty(Request.QueryString["fAppId"]))
            {
                TC_AJBA_Record aj = dbContext.TC_AJBA_Record.Where(t => t.FAppId == Request.QueryString["fAppId"]).FirstOrDefault();
                ViewState["FAppId"] = aj.FAppId;
                ViewState["FPrjItemId"] = aj.FPrjItemId;
            }
            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool1.ExecuteScript("btnEnable();");
            }
        }
    }
    void BindControl()
    {
        //参建角色
        DataTable dt = rc.getDicTbByFNumber("112201");
        t_CJJS.DataSource = dt;
        t_CJJS.DataTextField = "FName";
        t_CJJS.DataValueField = "FNumber";
        t_CJJS.DataBind();
        
    }
    //保存
    private void saveInfo()
    {
        string fId = EConvert.ToString(ViewState["FID"]);
        TC_AJBA_CJDW Emp = new TC_AJBA_CJDW();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_AJBA_CJDW.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FPrjItemId = EConvert.ToString(ViewState["FPrjItemId"]);
            Emp.FAppId = EConvert.ToString(ViewState["FAppId"]);
            dbContext.TC_AJBA_CJDW.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        ViewState["FID"] = fId;
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void btnAddEnt_Click(object sender, EventArgs e)
    {
        selEnt();
    }
    private void selEnt()
    {
        string selEntId = t_QYId.Value;
        EgovaDB1 db = new EgovaDB1();
        var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v != null)
        {
            if (t_CJJS.Text == "11220102")
            {
                var v1 = db.QY_QYZZXX.Where(t => t.QYBM == selEntId && t.SFZX==1).FirstOrDefault();
                if (v1 != null)
                {
                    t_ZZZS.Text = v1.ZSBH;
                    t_ZZDJ.Text = v1.ZZMC+v1.ZZDJ;
                }
            }
            else
            {
                var v1 = db.QY_QYZZXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
                if (v1 != null)
                {
                    t_ZZZS.Text = v1.ZSBH;
                    t_ZZDJ.Text = v1.ZZMC + v1.ZZDJ;
                }
            }
        }
        t_QYMC.Text = v.QYMC;
        t_QYDZ.Text = v.QYXXDZ;
        t_YYZZH.Text = v.YEZZZCH;
        t_ZZJGDM.Text = v.JGDM;
        t_QYFDDB.Text = v.FRDB;
    }
}