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
public partial class JZDW_ApplyJZBH_ApplyBaseInfo : EntAppPage
{
    ProjectDB db = new ProjectDB();
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
        govd_FRegistDeptId.fNumber = ComFunction.GetDefaultDept();
        govd_FRegistDeptId.Dis(3);
        

        //备案部门
        DataTable dt = db.getAllupDeptId(ComFunction.GetDefaultDept(), 0, 3);
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
            string FPrjId = v.FPrjId;

            //显示data信息
            var prjData = db.CF_Prj_Data.Where(t => t.FAppId == v.FId).Select(t => new
            {
                t.FTxt1,
                t.FTxt2,
                t.FTxt3,
                t.FTXt4,
                t.FDate1,
                t.FDate2,

            }).FirstOrDefault();
            if (prjData != null)
            {
               
                pageTool tool = new pageTool(this.Page, "t_");
                tool.fillPageControl(prjData);
               
            }

            //显示勘察单位信息
            var ent = (from e in db.CF_Prj_Ent
                       join a in db.CF_App_List on e.FAppId equals a.FLinkId
                       where a.FPrjId == FPrjId && a.FManageTypeId == 280//项目勘察合同备案
                             && a.FState == 6 && e.FEntType == 15501
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
            //从 勘察项目信息备案业务 的人员中取
            var emp = (from t in db.CF_Prj_Emp
                       join a in db.CF_App_List on t.FAppId equals a.FId
                       where a.FManageTypeId == 283 && a.FLinkId == FLinkId && t.FType == 1
                       select new
                       {
                           t.FName,
                           t.FTel,
                           t.FCall,
                           t.FEmail,
                           t.FCertiNo,
                           t.FTechId,
                       }).FirstOrDefault();

            if (emp != null)
            {
                pageTool tool = new pageTool(this.Page, "emp_");
                tool.fillPageControl(emp);
            }

            var ss = db.CF_Prj_Ent.Where(t => t.FAppId == FAppId).Select(t => new
                {
                    t.FName,
                    t.FCertiNo
                }).FirstOrDefault();
            if (ss != null)
            {
                pageTool tool = new pageTool(this.Page, "s_");
                tool.fillPageControl(ss);
            }

            //外业人员情况 (从见证人员安排中取)
            var vv = (from t in db.CF_Prj_Emp
                      join a in db.CF_App_List on t.FAppId equals a.FId
                      where a.FLinkId == FLinkId && t.FType == 2 && a.FState == 6 && a.FManageTypeId == 28002 //见证人员安排
                      select new
                      {
                          t.FId,
                          t.FName,
                          t.FTel,
                          t.FCall,
                          t.FEmail,
                          t.FMajor,
                          t.FFunction,
                          FIdCard = db.CF_Emp_BaseInfo.Where(e => e.FId == t.FEmpBaseInfo).Select(e => e.FIdCard).FirstOrDefault(),
                          FCertiNo = db.CF_Emp_BaseInfo.Where(e => e.FId == t.FEmpBaseInfo).Select(e => e.FCertiNo).FirstOrDefault()
                      }).ToList();

            DG_List.DataSource = vv;
            DG_List.DataBind();
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

    //勘察人员 列表
    protected void DG_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
        }
    }


    //保存
    private void saveInfo()
    {
       
        pageTool tool = new pageTool(this.Page, "t_");
        string FAppId = EConvert.ToString(Session["FAppId"]);
        DateTime dTime = DateTime.Now;

        //基本信息
        CF_Prj_Data data = db.CF_Prj_Data.Where(t => t.FAppId == FAppId).FirstOrDefault();
     
        if (data != null)
        {
            data = tool.getPageValue(data);
        }
        data.FTxt3 = s_FName.Text;
        data.FTXt4 = s_FCertiNo.Text;
        GetEnt("s_", s_FId.Value, 15501, db);//勘察单位信息
        //提交保存
        db.SubmitChanges();
        tool.showMessage("保存成功");

    }

    void GetEnt(string tag, string fid, int entType, ProjectDB pdb)
    {
        string fPrjId = db.CF_App_List.Where(t => t.FId == EConvert.ToString(Session["FAppId"])).Select(t => t.FPrjId).FirstOrDefault();
        CF_Prj_Ent Ent = new CF_Prj_Ent();
        
        if (!string.IsNullOrEmpty(fid))
            Ent = pdb.CF_Prj_Ent.Where(t => t.FId == fid).FirstOrDefault();
        else
        {
            Ent.FCreateTime = DateTime.Now;
            pdb.CF_Prj_Ent.InsertOnSubmit(Ent);
        }
        pageTool tool = new pageTool(this.Page, tag);
        Ent = tool.getPageValue(Ent);
        if (string.IsNullOrEmpty(Ent.FId))
            Ent.FId = Guid.NewGuid().ToString();
        Ent.FEntType = entType;
        Ent.FTime = DateTime.Now;
        Ent.FPrjId = fPrjId;
        Ent.FAppId = EConvert.ToString(Session["FAppId"]);
        Ent.FIsDeleted = false;
    }

    //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        if (btn.CommandName == "KC")//勘察单位
        {
            var App = (from b in db.CF_Ent_BaseInfo
                       join c in db.CF_Ent_QualiCerti
                       on b.FId equals c.FBaseInfoId
                       where c.FId == s_FBaseInfoId_c.Value
                       select new
                       {
                           b.FName,
                           c.FCertiNo,
                           c.FLevelName
                       }).FirstOrDefault();
                      
            if (App != null)
            {
                pageTool tool = new pageTool(this.Page, "s_");
                tool.fillPageControl(App);
            }
        }
    }
}
