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

public partial class JSDW_APPLYSGXKZGL_EmpInfo : System.Web.UI.Page
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
       
        //学历
        dt = rc.getDicTbByFNumber("107");
        t_ZGXL.DataSource = dt;
        t_ZGXL.DataTextField = "FName";
        t_ZGXL.DataValueField = "FNumber";
        t_ZGXL.DataBind();

        //职称
        dt = rc.getDicTbByFNumber("5080");
        t_ZC.DataSource = dt;
        t_ZC.DataTextField = "FName";
        t_ZC.DataValueField = "FNumber";
        t_ZC.DataBind();
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
        
        string sql = @" select count(*) from TC_PrjItem_Emp
                            where EmpType='11220201'
                            and FAppId='{0}'";
        sql = string.Format(sql, t_FAppId.Value);
        int count = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql).FirstOrDefault());
        dbContext = new EgovaDB();
        string sql1 = @" select count(*) from TC_PrjItem_Emp
                            where FIdCard='{0}'
                            and FAppId='{1}'";
        sql1 = string.Format(sql1, t_FIdCard.Text, t_FAppId.Value);
        int count1 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql1).FirstOrDefault());
        dbContext = new EgovaDB();
        string sql2 = @" select count(*) from TC_PrjItem_Emp
                            where EmpType='11220209'
                            and FAppId='{0}'";
        sql2 = string.Format(sql2, t_FAppId.Value);
        int count2 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql2).FirstOrDefault());
        if (t_EmpType.SelectedValue == "11220201" && count > 0)
        {
            ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('项目负责人只能添加一位');window.returnValue='1';", true);
        }
        else if (count1 > 0)
        {
            ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('人员不允许重复添加');window.returnValue='1';", true);
        }
        else if (count2 > 0)
        {
            ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('总监理工程师只能添加一位');window.returnValue='1';", true);
        }
        else
        {
            dbContext = new EgovaDB();
            string fId = txtFId.Value;
            TC_PrjItem_Emp Emp = new TC_PrjItem_Emp();
            if (!string.IsNullOrEmpty(fId))
            {
                Emp = dbContext.TC_PrjItem_Emp.Where(t => t.FId == fId).FirstOrDefault();
            }
            else
            {
                fId = Guid.NewGuid().ToString();
                Emp.FId = fId;
                Emp.FEmpId = h_selEmpId.Value;
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
            ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
        }
        
    //     MyPageTool.showMessageAjax("保存成功ii", up_Main);
    //    MyPageTool.showMessageAndRunFunctionAjax("保存成功", "window.returnValue='1';", up_Main);
    }
    public void Alert(string str_Message)
         {
             ClientScriptManager scriptManager =((Page)System.Web.HttpContext.Current.Handler).ClientScript;
            scriptManager.RegisterStartupScript(typeof(string), "", "alert('" + str_Message + "');", true);
         }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    private void selEmp()
    {
        string selEmpId = h_selEmpId.Value;
        EgovaDB1 db = new EgovaDB1();
        var v = db.RY_RYJBXX.Where(t => t.RYBH == selEmpId).FirstOrDefault();
        //t_EmpType.SelectedValue = v.RYLBBM;
        if (v != null)
        {
            t_FHumanName.Text = v.XM;
            t_FIdCard.Text = v.SFZH;
            t_FSex.SelectedValue = v.XB.ToString();
            t_FMobile.Text = v.GRDH;
            t_ZC.SelectedItem.Text = v.ZC??"其他";
            t_ZW.Text = v.ZW;
            t_FTel.Text = v.BGDH;
            t_ZGXL.SelectedItem.Text = "无";
            t_ZY.Text = v.SXZY;
            var v1 = db.RY_RYZSXX.Where(t => t.RYBH == selEmpId).FirstOrDefault();
            if (v1 != null)
            {
                t_ZSBH.Text = string.IsNullOrEmpty(v1.ZCZSBH) ? "" : v1.ZCZSBH;
                t_DJ.Text = v1.ZSJB;
                t_ZCBH.Text = v1.ZCZSH;
                t_ZCZY.Text = v1.ZCZY;
                t_ZCRQ.Text = v1.FZSJ.ToString();
            }
            
        }
    }
    protected void btnAddEmp_Click(object sender, EventArgs e)
    {
        selEmp();
    }


}