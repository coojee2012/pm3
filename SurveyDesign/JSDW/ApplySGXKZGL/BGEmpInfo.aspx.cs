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

public partial class JSDW_ApplySGXKZGL_EmpInfoForBG : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            t_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            txtFId.Value = EConvert.ToString(Request["FId"]);
            t_FEntId.Value = EConvert.ToString(Request["FEntId"]);
            //t_FPrjId.Value = EConvert.ToString(Request["FPrjId"]);
            t_FPrjItemId.Value = EConvert.ToString(Request["FPrjItemId"]);
            BindControl();
            showInfo();
        }
    }
    void BindControl()
    {
        //人员类型
        DataTable dt = rc.getDicTbByFNumber("112202");
        t_EmpType.DataSource = dt;
        t_EmpType.DataTextField = "FName";
        t_EmpType.DataValueField = "FNumber";
        t_EmpType.DataBind();
       
    }
    private string getEmpType(string id)
    {
        switch (id)
        {
            default:
                return "项目经理";
            case "1":
                return "项目经理";
            case "2":
                return "项目技术负责人";
            case "3":
                return "安全负责人";
            case "4":
                return "施工员";
            case "5":
                return "质量员";
            case "6":
                return "专职安全员";
            case "7":
                return "材料员";
            case "8":
                return "预算员";
            case "9":
                return "总监理工程师";
            case "10":
                return "专业监理工程师";
            case "11":
                return "监理员";
            case "12":
                return "其他";
        }
    }
    //显示
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_PrjItem_Emp emp = dbContext.TC_PrjItem_Emp.Where(t => t.FId == txtFId.Value).FirstOrDefault();
        if (emp != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(emp);
        }
    }
    //保存
    private void saveInfo()
    {
        string fId = txtFId.Value;
        string fOldId = fId;
        TC_PrjItem_Emp Emp = new TC_PrjItem_Emp();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_PrjItem_Emp.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FPrjItemId = t_FPrjItemId.Value;
            Emp.FAppId = t_FAppId.Value;
            Emp.FTime = DateTime.Now;
            Emp.FCreateTime = DateTime.Now;
            Emp.FEntId = t_FEntId.Value;
            dbContext.TC_PrjItem_Emp.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        txtFId.Value = fId;
        if (string.IsNullOrEmpty(fOldId))
        {
            updateRYBG(fId);
        }
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
        //     MyPageTool.showMessageAjax("保存成功ii", up_Main);
        //    MyPageTool.showMessageAndRunFunctionAjax("保存成功", "window.returnValue='1';", up_Main);
    }
    private void updateRYBG(string FId)
    {
        EgovaDB dbContext = new EgovaDB();
        TC_PrjItem_Emp ent = dbContext.TC_PrjItem_Emp.Where(t => t.FId == FId).FirstOrDefault();
        TC_SGXKZ_RYBGJG sr = new TC_SGXKZ_RYBGJG();

        sr.FId = Guid.NewGuid().ToString();
        sr.FAppId = t_FAppId.Value;
        sr.FPrjItemId = t_FPrjItemId.Value;
        sr.RYLX = getEmpType(sr.RYLX.ToString());
        sr.XM = ent.FHumanName;
        sr.ZSBH = ent.ZSBH;
        sr.QYMC = ent.FEntName;
        sr.BGQK = "增加";
        sr.BGTime = DateTime.Now;
        dbContext.TC_SGXKZ_RYBGJG.InsertOnSubmit(sr);
        dbContext.SubmitChanges();
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}