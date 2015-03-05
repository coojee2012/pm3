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

public partial class JSDW_project_ProjectItemInfo : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            if (string.IsNullOrEmpty(Request.QueryString["fprjId"]))
            {
               
            }
            else
            {
                TC_Prj_Info emp = dbContext.TC_Prj_Info.Where(t => t.FId == Convert.ToString(Request.QueryString["fprjId"])).FirstOrDefault();
                if (emp != null)
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.fillPageControl(emp, divSetup2);
                   // govd_FRegistDeptId.fNumber = emp.AddressDept;
                    t_AddressDept.Value = emp.AddressDept;
                    govd_FRegistDeptId.fNumber = emp.AddressDept;
                    t_LegalPerson.Text = emp.JSDWFR;
                    ViewState["FJSDWID"] = emp.FJSDWID;
                    t_PrjItemType.SelectedValue = emp.ProjectType;
                }
                ViewState["FPrjId"] = Request.QueryString["fprjId"];
            }
            
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                showInfo();
            }
        }
    }
    void BindControl()
    {

        //工程类别
        DataTable dt = rc.getDicTbByFNumber("20001");
        t_PrjItemType.DataSource = dt;
        t_PrjItemType.DataTextField = "FName";
        t_PrjItemType.DataValueField = "FNumber";
        t_PrjItemType.DataBind();

        //结构类型
        dt = rc.getDicTbByFNumber("509");
        t_ConstrType.DataSource = dt;
        t_ConstrType.DataTextField = "FName";
        t_ConstrType.DataValueField = "FNumber";
        t_ConstrType.DataBind();
        t_ConstrType.Items.Insert(0, new ListItem("--请选择--", ""));
    }
    //显示
    private void showInfo()
    {

        TC_PrjItem_Info emp = dbContext.TC_PrjItem_Info.Where(t => t.FId == Convert.ToString(ViewState["FID"])).FirstOrDefault();
        if (emp != null)
        {
            pageTool tool = new pageTool(this.Page);
            tool.fillPageControl(emp, divSetup2);
            ViewState["FJSDWID"] = emp.FId;
            ViewState["FPrjId"] = emp.FPrjId;
            govd_FRegistDeptId.fNumber = t_AddressDept.Value;
            
        }
    }
    //private string getAddressDeptName(string fNumber)
    //{
    //    EgovaDB dbContext = new EgovaDB();
    //    var v = dbContext.CF_Sys_ManageDept.Where(t => t.FNumber.Equals(fNumber)).FirstOrDefault();
    //    return EConvert.ToString(v.FFullName);
    //}
    //保存
    private void saveInfo()
    {
        string fId = EConvert.ToString(ViewState["FID"]);
       // t_AddressDept.Value = govd_FRegistDeptId.fNumber;
        TC_PrjItem_Info Emp = new TC_PrjItem_Info();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_PrjItem_Info.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FPrjId = EConvert.ToString(ViewState["FPrjId"]);
            Emp.FJSDWID = EConvert.ToString(ViewState["FJSDWID"]);
            dbContext.TC_PrjItem_Info.InsertOnSubmit(Emp);
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
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        object obj = ViewState["FID"];
        pageTool tool = new pageTool(this.Page);
        if (obj!=null)
        {
            string sql = @"exec SP_GC_TO_BZK @FID";
            rc.PExcute(sql, new System.Data.SqlClient.SqlParameter() { ParameterName = "@FID", Value = obj.ToString(), SqlDbType = SqlDbType.VarChar });
            tool.showMessage("操作成功");
        }
        else
            tool.showMessage("请先保存");
    }
}