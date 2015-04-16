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

public partial class JSDW_ApplyZBBA_HXRInfo : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["FID"] = Request.QueryString["fid"];
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                TC_PBBG_ZBHXR pb = dbContext.TC_PBBG_ZBHXR.Where(t => t.FId == Convert.ToString(Request.QueryString["fid"])).FirstOrDefault();
                if (pb != null)
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.fillPageControl(pb, divSetup2);
                }
                string sql = "select Count(*) from TC_PBBG_ZBHXR where FOrder < =" + pb.FOrder;
                int count = dbContext.ExecuteQuery<int>(sql).FirstOrDefault<int>();
                txtOrder.Text = "第" + count + "中标候选人";
            }
            else
            {
                ViewState["FLinkId"] = Request.QueryString["fLinkId"];
                ViewState["FPrjId"] = Request.QueryString["fPrjId"];
                ViewState["FAppId"] = Request.QueryString["fAppId"];
                ViewState["BDId"] = Request.QueryString["BDId"];
            }
            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool1.ExecuteScript("btnEnable();");
            }
        }
    }

    //保存
    private void saveInfo()
    {
        string fId = EConvert.ToString(ViewState["FID"]);
        string oId = fId;
        TC_PBBG_ZBHXR Emp = new TC_PBBG_ZBHXR();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_PBBG_ZBHXR.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FPrjId = EConvert.ToString(ViewState["FPrjId"]);
            Emp.BDId = EConvert.ToString(ViewState["BDId"]);
            Emp.FLinkId = EConvert.ToString(ViewState["FLinkId"]);
            Emp.FAppId = EConvert.ToString(ViewState["FAppId"]);
            Emp.RYId = h_selEmpId.Value;
            dbContext.TC_PBBG_ZBHXR.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        if (string.IsNullOrEmpty(oId))
        {
            EgovaDB db = new EgovaDB();
            string sql = "select ISNULL(max(FOrder),0) from TC_PBBG_ZBHXR";
            int count = db.ExecuteQuery<int>(sql).FirstOrDefault<int>() + 1;
            sql = "select Count(*) from TC_PBBG_ZBHXR where FOrder < =" + count;
            int num = db.ExecuteQuery<int>(sql).FirstOrDefault<int>();
            Emp.FOrder = count;
            Emp.OrderStr = "第" + num + "中标候选人";
        }
        
        dbContext.SubmitChanges();
        txtOrder.Text = Emp.OrderStr;
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
    protected void btnSel_Click(object sender, EventArgs e)
    {
        var result = (from t in dbContext.TC_PBBG_TBQY
                      where t.FId == this.t_TBId.Value
                      select t).SingleOrDefault();
        t_HXRMC.Text = result.QYMC;
        t_TBJ.Text = EConvert.ToString(result.TBJ);
        t_PBJ.Text = EConvert.ToString(result.PBJ);
        t_QYId.Value = result.QYId;
    }

    private void selEmp()
    {
        string selEmpZsId = h_selEmpId.Value;  //人员证书编号
        EgovaDB1 db = new EgovaDB1();
        //var v = db.RY_RYJBXX.Where(t => t.RYZSXXID == selEmpId).FirstOrDefault();
        //if (v != null)
        //{
        //    t_FZRXM.Text = v.XM;
        //}
        var v1 = db.RY_RYZSXX.Where(t => t.RYZSXXID == selEmpZsId).FirstOrDefault();
        if (v1 != null)
        {
            t_ZCZSH.Text = v1.ZCZSH;
            t_FZRXM.Text = v1.XM;
        }
        
    }
    protected void btnAddEmp_Click(object sender, EventArgs e)
    {
        selEmp();
    }
}