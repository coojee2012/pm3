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
            //项目id
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
            //工程项目id
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                //判断工程项目是否已经同步到标准库，如果已经同步则不能再保存，并且也不能同步
                if (IsExistProjectItem(Request.QueryString["fid"].ToString()))
                {
                    this.btnSave.Enabled = false;
                    this.btnRefresh.Enabled = false;
                }
                ViewState["FID"] = Request.QueryString["fid"];
                showInfo();
            }         
        }
    }

    /// <summary>
    /// 检测项目是否已经存在在标准库中
    /// </summary>
    /// <param name="fid"></param>
    /// <returns></returns>
    private bool IsExistProjectItem(string fid)
    {
        string sql = @"select  *  from  XM_BaseInfo.[dbo].[GC_DWGCXX]  where [dwgcbh]='" + fid + "'";
        DataTable dt = new DataTable();
        dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
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
        string projitemname = this.t_PrjItemName.Text.Trim();
        if (CheckPrjItemIsExist(projitemname))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "js", "alert('系统中已经有同名工程项目，请从系统中选取对应工程项目！');window.returnValue='1';", true);
            this.t_PrjItemName.Focus();
            return;
        }
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
        //ScriptManager.RegisterStartupScript(up_Main, typeof(UpdatePanel), "js", "$('#btnSave').css('color','#BEBFC3');$('#btnSave').attr('disabled',true);;alert('保存成功');window.returnValue='1';", true);

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
            //数据同步到标准库后不能进行同步、重新同步的操作。
            this.btnSave.Enabled = false;
            this.btnRefresh.Enabled = false;
            tool.showMessage("操作成功");
        }
        else
            tool.showMessage("请先保存");
    }

    /// <summary>
    /// 判断单项工程项目在标准库中是否已经存在，如果已经存在则不允许添加单项，让操作者到标准库中选择
    /// </summary>
    /// <param name="projname">项目名称</param>
    /// <returns></returns>
    private bool CheckPrjItemIsExist(string projitemname)
    {
        string sql = @"select  *  from  XM_BaseInfo.[dbo].[GC_DWGCXX]  where ltrim(rtrim(DWGCMC))='" + projitemname.Trim() + "'";
        DataTable dt = new DataTable();
        dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
     }
}