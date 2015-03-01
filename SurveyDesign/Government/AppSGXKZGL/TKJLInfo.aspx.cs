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

public partial class JSDW_APPSGXKZGL_YZInfo : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            h_FAppId.Value = EConvert.ToString(Request["FAppId"]);
            t_FId.Value = EConvert.ToString(Request["FId"]);
            t_FEntId.Value = EConvert.ToString(Request["FEntId"]);
            t_FPrjId.Value = EConvert.ToString(Request["FPrjId"]);
            t_FPrjItemId.Value = EConvert.ToString(Request["FPrjItemId"]);
            showInfo();
        }
    }
    void BindControl()
    {

       
    }
    //显示
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_Prj_XCTKJL emp = dbContext.TC_Prj_XCTKJL.Where(t => t.FId == t_FId.Value).FirstOrDefault();
        if (emp != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(emp);
        }
    }
    //保存
    private void saveInfo()
    {
        string fId = t_FId.Value;
        TC_Prj_XCTKJL Emp = new TC_Prj_XCTKJL();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_Prj_XCTKJL.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FAppId = h_FAppId.Value;
            t_FId.Value = fId;
            dbContext.TC_Prj_XCTKJL.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        t_FId.Value = fId;
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
    //     MyPageTool.showMessageAjax("保存成功ii", up_Main);
    //    MyPageTool.showMessageAndRunFunctionAjax("保存成功", "window.returnValue='1';", up_Main);
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}