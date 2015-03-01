using System;
using System.Linq;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Web.UI;

public partial class KcsjSgt_ApplyKCWJBA_ApplyBaseInfo : EntAppPage
{
    ProjectDB db = new ProjectDB();
    protected override void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            showInfo();
        }
        base.Page_Load(sender, e);
        this.RegisterStartupScript("jssz", "<script>showSZ();</script>");
    }
    #region 显示

    //绑定默认
    private void BindControl()
    {
        RCenter rc = new RCenter();
        govd_FRegistDeptId.fNumber = ComFunction.GetDefaultDept();
        govd_FRegistDeptId.Dis(3);

        //建设性质
        DataTable dt = rc.getDicTbByFNumber("20005");
        p_FKind.DataSource = dt;
        p_FKind.DataTextField = "FName";
        p_FKind.DataValueField = "FNumber";
        p_FKind.DataBind();
        p_FKind.Items.Insert(0, new ListItem("--请选择--", ""));

        //建筑使用性质 
        dt = rc.getDicTbByFNumber("20003");
        p_FNature.DataSource = dt;
        p_FNature.DataTextField = "FName";
        p_FNature.DataValueField = "FNumber";
        p_FNature.DataBind();

        //市政行业类别
        dt = rc.getDicTbByFNumber("20008");
        p_FSectors.DataSource = dt;
        p_FSectors.DataTextField = "FName";
        p_FSectors.DataValueField = "FNumber";
        p_FSectors.DataBind();

        //结构类型
        dt = rc.getDicTbByFNumber("509");
        p_FStruType.DataSource = dt;
        p_FStruType.DataTextField = "FName";
        p_FStruType.DataValueField = "FNumber";
        p_FStruType.DataBind();
        p_FStruType.Items.Insert(0, new ListItem("--请选择--", ""));

        //工程等级
        dt = rc.getDicTbByFNumber("20010");
        p_FLevel.DataSource = dt;
        p_FLevel.DataTextField = "FName";
        p_FLevel.DataValueField = "FNumber";
        p_FLevel.DataBind();
        p_FLevel.Items.Insert(0, new ListItem("--请选择--", ""));
    }

    //显示
    private void showInfo()
    {
        //要从 设计合同备案业务(287) 中查合同备案基本信息，所以从Get中接收业务ID
        string FAppId = EConvert.ToString(Session["FAppId"]);
        var app = db.CF_App_List.Where(t => t.FId == FAppId).FirstOrDefault();
        if (app != null)
        {
            //从项目查基本信息
            ShowPrjInfo(app.FPrjId);

            //勘察单位(15501) 从勘察信息备案(280) 中取
            var k = (from t in db.CF_Prj_Ent
                     join a in db.CF_App_List on t.FAppId equals a.FLinkId
                     where a.FPrjId == app.FPrjId && a.FManageTypeId == 280 && t.FEntType == 15501 && a.FState == 6
                     orderby t.FTime descending
                     select new
                     {
                         t.FId,
                         t.FBaseInfoId,
                         t.FName,
                         t.FLevelName,
                         t.FCertiNo,
                         t.FMoney,
                         t.FPlanDate,
                         t.FAppId
                     }).FirstOrDefault();
            if (k != null)
            {
                pageTool ktool = new pageTool(this.Page, "k_");
                ktool.fillPageControl(k);
            }

            //设计单位(155)
            var sj = (from t in db.CF_Prj_Ent
                      join a in db.CF_App_List on t.FAppId equals a.FId
                      join p in db.CF_Prj_Data on a.FLinkId equals p.FId
                      where a.FPrjId == app.FPrjId && a.FManageTypeId == 296 && t.FEntType == 155 && a.FState == 6
                           && p.FId == db.CF_Prj_Data.Where(d => d.FId == app.FLinkId).Select(d => d.FLinkId).FirstOrDefault()
                      select new
                      {
                          t.FId,
                          t.FBaseInfoId,
                          t.FName,
                          t.FLevelName,
                          t.FCertiNo,
                          t.FMoney,
                          t.FPlanDate,
                          t.FAppId
                      }).FirstOrDefault();
            if (sj != null)
            {
                pageTool ktool = new pageTool(this.Page, "sj_");
                ktool.fillPageControl(sj);
            }

            //审图机构(145) 从勘察文件审查合同备案(300) 中取
            var s = (from t in db.CF_Prj_Ent
                     join a in db.CF_App_List on t.FAppId equals a.FId
                     where a.FLinkId == app.FLinkId && a.FManageTypeId == 300 && t.FEntType == 145 && a.FState == 6
                     select new
                     {
                         t.FId,
                         t.FBaseInfoId,
                         t.FName,
                         t.FLevelName,
                         t.FCertiNo,
                         t.FMoney,
                         t.FPlanDate,
                         t.FAppId
                     }).FirstOrDefault();
            if (s != null)
            {
                pageTool stool = new pageTool(this.Page, "s_");
                stool.fillPageControl(s);
            }

            //审图机构项目负责人(1)  勘察文件的审查人员安排(30102) 中取
            var se = (from t in db.CF_Prj_Emp
                      join a in db.CF_App_List on t.FAppId equals a.FId
                      where a.FLinkId == app.FLinkId && a.FManageTypeId == 30102 && t.FType == 1 && a.FState == 6
                      select new
                      {
                          t.FName,
                          t.FTel,
                          t.FCall,
                          t.FEmail,
                          t.FCertiNo,
                          t.FFunction
                      }).FirstOrDefault();
            if (se != null)
            {
                pageTool setool = new pageTool(this.Page, "semp_");
                setool.fillPageControl(se);
            }

            //查询送审日期(建设单位提交施工图文件审查的日期)
            var ssDate = db.CF_App_List.Where(t => t.FManageTypeId == 300 &&
               t.FId == app.FLinkId).Select(t => t.FReportDate).FirstOrDefault();
            txtSSDate.Text = EConvert.ToShortDateString(ssDate);

            //技术性审查完成日期\综合结论\处理意见
            var scDate = (from l in db.CF_App_List
                          join a in db.CF_App_Idea
                          on l.FId equals a.FLinkId
                          where l.FManageTypeId == 30103
                          && l.FLinkId == app.FLinkId
                          && a.FUserId == null
                          select new
                          {
                              l.FReportDate,
                              a.FResult,
                              a.FContent
                          }).FirstOrDefault();
            if (scDate != null)
            {
                txtFSCDate.Text = EConvert.ToShortDateString(scDate.FReportDate);
                txtFResult.Text = scDate.FResult;
                txtFContent.Text = scDate.FContent;
            }
            //显示审查编号
            var prjData = db.CF_Prj_Data.Where(t => t.FAppId == FAppId)
                .Select(t => new
                {
                    t.FTxt1,
                    t.FTxt2,
                    t.FId,
                    t.FInt1,
                    t.FDate1,
                    t.FDate2,
                    t.FTxt16,
                    t.FTxt17
                }).FirstOrDefault();
            if (prjData != null)
            {
                pageTool tool = new pageTool(this, "t_");
                tool.fillPageControl(prjData);
            }
            showEmpYJ(app.FLinkId);
        }
    }
    //显示人员意见
    private void showEmpYJ(string fLinkId)
    {
        //得从上一步“审查人员安排”中取安排的人员列表 
        var v = from a in db.CF_App_List
                join oa in db.CF_App_List on a.FLinkId equals oa.FLinkId
                join e in db.CF_Prj_Emp on oa.FId equals e.FAppId
                join id in db.CF_App_Idea on new { FID = a.FId, FEmpBaseInfo = e.FEmpBaseInfo } equals new { FID = id.FLinkId, FEmpBaseInfo = id.FUserId } into idea
                from i in idea.DefaultIfEmpty()
                where a.FManageTypeId == 30103 && oa.FManageTypeId == 30102
                && a.FLinkId == fLinkId
                && oa.FState == 6 && (a.FState == 3 || a.FState == 6)
                && (e.FType == 2 || e.FType == 3)//注册人员、非注册人员
                select new
                {
                    e.FId,
                    e.FAppId,
                    e.FName,//审查人 
                    e.FMajor,//专业 
                    FOrder = i == null ? "" : i.FOrder.ToString(),//
                    FResult = i == null ? "" : i.FResult,//审查结论
                    FContent = i == null ? "" : i.FContent,//审查意见
                    FCount = db.CF_App_List.Where(li => li.FLinkId == fLinkId && li.FManageTypeId == 300).Select(li => li.FReportCount).FirstOrDefault(),
                    e.FAppMajor,//审查专业
                    e.FTxt3,//违反工程建设强制性标准编号及条文编号 
                    e.FTxt4//审核人
                };

        rep_Emp.DataSource = v;
        rep_Emp.DataBind();
    }
    protected void rep_File_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FResult = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FResult"));
            if (string.IsNullOrEmpty(FResult))
            {
                //e.Row.Cells[6].Text = "";
            }
        }
    }
    /// <summary>
    /// 显示工程信息和建设单位
    /// </summary>
    private void ShowPrjInfo(string FPrjId)
    {
        CF_Prj_BaseInfo prj = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId).FirstOrDefault();
        if (prj != null)
        {
            pageTool tool = new pageTool(this.Page, "p_");
            tool.fillPageControl(prj);
            govd_FRegistDeptId.fNumber = p_FAddressDept.Value;

            CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == prj.FBaseinfoId).FirstOrDefault();
            if (ent != null)
            {
                pageTool toole = new pageTool(this.Page, "e_");
                toole.fillPageControl(ent);
            }
            //查询子项工程名称
            var prjItemList = db.CF_PrjItem_BaseInfo.Where(t => t.FPrjId == FPrjId).Select(t => t.FPrjItemName).ToList();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < prjItemList.Count; i++)
            {
                if (sb.Length > 0)
                    sb.Append("\r\n");
                sb.Append(prjItemList[i]);
            }
            txtFPrjItemList.Text = sb.ToString();
        }
    }


    //项目负责人
    private void showEmp(string FAppId, int FType)
    {
        var emp = (from t in db.CF_Prj_Emp
                   where t.FAppId == FAppId && t.FType == FType
                   select new
                   {
                       t.FName,
                       t.FTel,
                       t.FCall,
                       t.FEmail,
                       t.FCertiNo,
                       t.FFunction
                   }).FirstOrDefault();

        if (emp != null)
        {
            pageTool tool = new pageTool(this.Page, "emp_");
            tool.fillPageControl(emp);
        }
    }
    #endregion
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
    void SaveInfo()
    {
        pageTool tool = new pageTool(this, "t_");
        string FAppId = EConvert.ToString(Session["FAppId"]);
        CF_Prj_Data data = db.CF_Prj_Data.FirstOrDefault(t => t.FAppId == FAppId);
        if (data == null)
        {
            data = new CF_Prj_Data();
            data.FId = Guid.NewGuid().ToString();
            data.FCreateTime = DateTime.Now;
            db.CF_Prj_Data.InsertOnSubmit(data);
        }
        data = tool.getPageValue(data);
        data.FAppId = FAppId;
        data.FPrjId = p_FId.Value;
        data.FPrjName = p_FPrjName.Text.Trim();
        data.FTime = DateTime.Now;
        data.FIsDeleted = false;
        db.SubmitChanges();
        tool.showMessage("保存成功！");
    }
}
