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
public partial class KC_ApplyKCXXBA_ApplyBaseInfo : EntAppPage
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            btnSave.Attributes["onclick"] = "return checkInfo();";
            BindControl();
            showInfo();
        }
    }

    //绑定
    private void BindControl()
    {
        //工程地点
        string deptId = ComFunction.GetDefaultDept();
        govd_FRegistDeptId.fNumber = deptId;
        govd_FRegistDeptId.Dis(3);

        //备案部门
        StringBuilder sb = new StringBuilder();
        sb.Append("select case FLevel when 1 then FFullName when 2 then '-'+FName when 3 then '---'+FName end FName,");
        sb.Append("FNumber from cf_Sys_ManageDept ");
        sb.Append("where fnumber like '" + deptId + "%' ");
        sb.Append("and fname<>'市辖区' ");
        sb.Append("order by left(FNumber,4),flevel");
        DataTable dt = rc.GetTable(sb.ToString());
        p_FManageDeptId.DataSource = dt;
        p_FManageDeptId.DataTextField = "FName";
        p_FManageDeptId.DataValueField = "FNumber";
        p_FManageDeptId.DataBind();

        //人员职称
        emp_FTechId.DataSource = db.getDicList(108);
        emp_FTechId.DataTextField = "FName";
        emp_FTechId.DataValueField = "FNumber";
        emp_FTechId.DataBind();
        emp_FTechId.Items.Insert(0, new ListItem("请选择", ""));
    }

    //显示
    private void showInfo()
    {
        string FAppId = EConvert.ToString(Session["FAppId"]);
        var v = (from t in db.CF_App_List
                 where t.FId == FAppId
                 select new
                 {
                     t.FId,
                     t.FPrjId,
                     t.FLinkId,
                     t.FBaseinfoId
                 }).FirstOrDefault();
        if (v != null)
        {
            string FLinkId = v.FLinkId;
            hidd_FLinkId.Value = FLinkId;
            string FPrjId = v.FPrjId;

            //显示data信息
            var prjData = db.CF_Prj_Data.Where(t => t.FAppId == v.FId).Select(t => new { t.FDate1, t.FDate2 }).FirstOrDefault();
            if (prjData != null)
            {
                pageTool tool = new pageTool(this.Page, "t_");
                tool.fillPageControl(prjData);
            }

            //显示当前勘察单位信息
            var ent = (from e in db.CF_Prj_Ent
                       join a in db.CF_App_List on e.FAppId equals a.FLinkId
                       where a.FPrjId == FPrjId && a.FManageTypeId == 280//项目勘察合同备案
                             && a.FState == 6 && e.FEntType == 15501 && e.FBaseInfoId == v.FBaseinfoId
                       select new
                       {
                           e.FName,
                           e.FLevelName,
                           e.FBaseInfoId
                       }).FirstOrDefault();
            pageTool toolent = new pageTool(this.Page, "k_");
            toolent.fillPageControl(ent);



            //显示工程信息
            ShowPrjInfo(FPrjId);

            //勘察项目负责人
            showEmp(FAppId, 1);

            //勘察人员
            showEmpList(FAppId, 2);

        }
    }

    /// <summary>
    /// 显示工程信息
    /// </summary>
    private void ShowPrjInfo(string FPrjId)
    {
        CF_Prj_BaseInfo prj = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId).FirstOrDefault();
        if (prj != null)
        {
            pageTool tool = new pageTool(this.Page, "p_");
            tool.fillPageControl(prj);
            govd_FRegistDeptId.fNumber = prj.FAddressDept;


            //显示建设单位信息
            ShowEntInfo(prj.FBaseinfoId);
        }
    }
    /// <summary>
    /// 显示建设单位信息
    /// </summary>
    private void ShowEntInfo(string FBaseinfoId)
    {
        CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == FBaseinfoId).FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, "e_");
            tool.fillPageControl(ent);
        }
    }

    //勘察项目负责人
    private void showEmp(string FAppId, int FType)
    {
        var emp = (from t in db.CF_Prj_Emp
                   where t.FAppId == FAppId && t.FType == FType
                   select new
                   {
                       t.FName,
                       t.FEmpBaseInfo,
                       t.FTel,
                       t.FCall,
                       t.FEmail,
                       t.FCertiNo,
                       t.FTechId
                   }).FirstOrDefault();

        if (emp != null)
        {
            pageTool tool = new pageTool(this.Page, "emp_");
            tool.fillPageControl(emp);
        }
    }

    //勘察人员
    private void showEmpList(string FAppId, int FType)
    {
        var v = (from t in db.CF_Prj_Emp
                 where t.FAppId == FAppId && t.FType == FType
                 select new
                 {
                     t.FId,
                     t.FName,
                     t.FTel,
                     t.FCall,
                     t.FEmail,
                     t.FMajor,
                     t.FFunction
                 }).ToList();

        DG_List.DataSource = v;
        DG_List.DataBind();
    }
    //勘察人员 列表
    protected void DG_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbautoid = (Label)e.Row.FindControl("lbautoid");
            lbautoid.Text = (e.Row.RowIndex + 1).ToString();

            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
        }
    }
    //保存
    protected void DG_List_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cnSaveEmp")
        {
            pageTool tool = new pageTool(this.Page);
            string FID = e.CommandArgument.ToString();
            CF_Prj_Emp emp = db.CF_Prj_Emp.Where(t => t.FId == FID).FirstOrDefault();
            if (emp != null)
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                //获取当前行中的某一列中的TextBox控件
                string FFunction = ((TextBox)gvr.FindControl("t_FFunction")).Text;
                emp.FFunction = FFunction;

                db.SubmitChanges();
                tool.showMessage("保存成功");
            }
        }
    }
    //删除 勘察人员
    protected void btnDel_Click(object sender, EventArgs e)
    {
        string FAppId = EConvert.ToString(Session["FAppId"]);
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(DG_List, db.CF_Prj_Emp);

        //勘察人员
        showEmpList(FAppId, 2);
    }

    //选择勘察项目负责人
    protected void btnSelectEmp_Click(object sender, EventArgs e)
    {
        var v = (from t in db.CF_Emp_BaseInfo
                 where t.FId == emp_FEmpBaseInfo.Value
                 select new
                 {
                     t.FName,
                     t.FCertiNo,
                     t.FTechId
                 }).FirstOrDefault();

        if (v != null)
        {
            emp_FName.Text = v.FName;
            emp_FCertiNo.Text = v.FCertiNo;
            //emp_FTechId.SelectedValue = v.FTechId.ToString();
            emp_FTechId.SelectedIndex = emp_FTechId.Items.IndexOf(emp_FTechId.Items.FindByValue(v.FTechId.ToString()));
        }
    }


    //选择勘察员
    protected void btnSelEmpList_Click(object sender, EventArgs e)
    {
        string FAppId = EConvert.ToString(Session["FAppId"]);
        //勘察人员
        showEmpList(FAppId, 2);
    }

    //保存
    private void saveInfo()
    {
        if (this.DG_List.Rows.Count < 1)
        {
            RegisterStartupScript("key", "<script>alert('请安排勘察人员')</script>");
            return;
        }
        string FAppId = EConvert.ToString(Session["FAppId"]);
        DateTime dTime = DateTime.Now;

        //基本信息
        CF_Prj_Data data = db.CF_Prj_Data.Where(t => t.FAppId == FAppId).FirstOrDefault();
        if (data != null)
        {
            pageTool tooldata = new pageTool(this.Page, "t_");
            data = tooldata.getPageValue(data);
        }

        // 勘察项目负责人
        pageTool tool = new pageTool(this.Page, "emp_");
        CF_Prj_Emp emp = db.CF_Prj_Emp.Where(t => t.FAppId == FAppId && t.FType == 1).FirstOrDefault();
        if (emp == null)
        {
            emp = new CF_Prj_Emp();
            db.CF_Prj_Emp.InsertOnSubmit(emp);
            emp.FId = Guid.NewGuid().ToString();
            emp.FIsDeleted = false;
            emp.FTime = dTime;
            emp.FCreateTime = dTime;
            emp.FType = 1;
            emp.FAppId = FAppId;
            emp.FPrjId = p_FId.Value;
        }
        emp = tool.getPageValue(emp);

        //勘察人员
        for (int i = 0; i < DG_List.Rows.Count; i++)
        {
            string FID = EConvert.ToString(DG_List.DataKeys[i]["FId"]);
            string FFunction = ((TextBox)DG_List.Rows[i].FindControl("t_FFunction")).Text;

            CF_Prj_Emp v = db.CF_Prj_Emp.Where(t => t.FId == FID).FirstOrDefault();
            if (v != null)
            {
                v.FFunction = FFunction;
            }
        }
        //提交保存
        db.SubmitChanges();
        tool.showMessage("保存成功");
    }



    //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}
