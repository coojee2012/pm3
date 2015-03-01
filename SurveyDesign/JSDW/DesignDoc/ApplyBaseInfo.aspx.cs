using System;
using System.Linq;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Web.UI;
using System.Text;

public partial class KcsjSgt_ApplyKCWJBA_ApplyBaseInfo : EntAppPage
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected override void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            showInfo();
            pageTool tool = new pageTool(this.Page);
            tool.ExecuteScript("showTr();btnEnable();");
        }
        base.Page_Load(sender, e);
    }
    #region 显示


    void BindControl()
    {
        string deptId = ComFunction.GetDefaultDept();
        govd_FRegistDeptId.fNumber = deptId;
        govd_FRegistDeptId.Dis(1);
        RCenter rc = new RCenter();
        DataTable dt = rc.getDicTbByFNumber("20001");
        t_FType.DataSource = dt;
        t_FType.DataTextField = "FName";
        t_FType.DataValueField = "FNumber";
        t_FType.DataBind();

        //备案部门
        StringBuilder sb = new StringBuilder();
        sb.Append("select case FLevel when 1 then FFullName when 2 then '-'+FName when 3 then '---'+FName end FName,");
        sb.Append("FNumber from cf_Sys_ManageDept ");
        sb.Append("where fnumber like '" + deptId + "%' ");
        sb.Append("and fname<>'市辖区' ");
        sb.Append("order by left(FNumber,4),flevel");
        dt = rc.GetTable(sb.ToString());
        t_FManageDeptId.DataSource = dt;
        t_FManageDeptId.DataTextField = "FName";
        t_FManageDeptId.DataValueField = "FNumber";
        t_FManageDeptId.DataBind();

        //建筑使用性质
        dt = rc.getDicTbByFNumber("20003");
        t_FNature.DataSource = dt;
        t_FNature.DataTextField = "FName";
        t_FNature.DataValueField = "FNumber";
        t_FNature.DataBind();

        //设计规模
        dt = rc.getDicTbByFNumber("20004");
        t_FScale.DataSource = dt;
        t_FScale.DataTextField = "FName";
        t_FScale.DataValueField = "FNumber";
        t_FScale.DataBind();
        t_FScale.Items.Insert(0, new ListItem("--请选择--", ""));

        //建设性质
        dt = rc.getDicTbByFNumber("20005");
        t_FKind.DataSource = dt;
        t_FKind.DataTextField = "FName";
        t_FKind.DataValueField = "FNumber";
        t_FKind.DataBind();
        t_FKind.Items.Insert(0, new ListItem("--请选择--", ""));

        //抗震设防分类标准
        dt = rc.getDicTbByFNumber("20006");
        t_FAntiSeismic.DataSource = dt;
        t_FAntiSeismic.DataTextField = "FName";
        t_FAntiSeismic.DataValueField = "FNumber";
        t_FAntiSeismic.DataBind();
        t_FAntiSeismic.Items.Insert(0, new ListItem("--请选择--", ""));

        //市政行业类别
        dt = rc.getDicTbByFNumber("20007");
        t_FSectors.DataSource = dt;
        t_FSectors.DataTextField = "FName";
        t_FSectors.DataValueField = "FNumber";
        t_FSectors.DataBind();
    }

    //资金来源
    private void conBindZJLR(int FType)
    {
        int FFunds = 20002;//默认（房屋）
        if (FType == 2000102)//市政
            FFunds = 20009;
        //资金来源 
        t_FFunds.DataSource = db.getDicList(FFunds);
        t_FFunds.DataTextField = "FName";
        t_FFunds.DataValueField = "FNumber";
        t_FFunds.DataBind();
        t_FFunds.Items.Insert(0, new ListItem("--请选择--", ""));
    }


    ////显示
    private void showInfo()
    {
        //要从 勘察合同备案业务(287) 中查合同备案基本信息，所以从Get中接收业务ID
        string FAppId = EConvert.ToString(Session["FAppId"]);
        var app = db.CF_App_List.Where(t => t.FId == FAppId).FirstOrDefault();
        if (app != null)
        {
            //从项目查基本信息
            ShowPrjInfo(app.FPrjId);
            ShowPrjItemInfo(app.FPrjId);

            //勘察单位(15501) 初步设计合同备案(291) 中取
            var k = (from t in db.CF_Prj_Ent
                     join b in db.CF_Ent_BaseInfo on t.FBaseInfoId equals b.FId
                     join a in db.CF_App_List on t.FAppId equals a.FId
                     where a.FPrjId == app.FPrjId && a.FManageTypeId == 291
                     && t.FEntType == 15501 && a.FState == 6
                     select new
                     {
                         a.FId,
                         t.FBaseInfoId,
                         t.FName,
                         t.FLevelName,
                         t.FCertiNo,
                         t.FMoney,
                         t.FPlanDate,
                         t.FAppId,
                         b.FLinkMan,
                         FCall = b.FTel,
                         b.FEmail,
                         b.FTel,
                         LHT = db.CF_Prj_Data.Where(l => l.FAppId == a.FId).Select(l => l.FFloat11).FirstOrDefault()
                     }).FirstOrDefault();
            if (k != null)
            {
                pageTool ktool = new pageTool(this.Page, "k_");
                ktool.fillPageControl(k);
                //查询联合体信息
                if (k.LHT == 1)//勘察
                {
                    t_FFloat11.Checked = true;
                    otherKC.Attributes.Add("style", "display:block");
                    mainKC.Attributes.Add("style", "display:block");
                }
                else
                {
                    this.t_FFloat11.Checked = false;
                }
                ShowPrjEnt(k.FId, 15502, repeaterDisplay_KC);//显示勘察联合体
            }

            ////勘察项目负责人(1)  从勘察项目信息备案(283) 中取
            //var ke = (from t in db.CF_Prj_Emp
            //          join a in db.CF_App_List on t.FAppId equals a.FId
            //          where a.FPrjId == app.FPrjId && a.FManageTypeId == 283
            //          && t.FType == 1 && a.FState == 6
            //          select new
            //          {
            //              t.FName,
            //              t.FTel,
            //              t.FCall,
            //              t.FEmail,
            //              t.FCertiNo,
            //              t.FFunction
            //          }).FirstOrDefault();
            //if (ke != null)
            //{
            //    pageTool ketool = new pageTool(this.Page, "kemp_");
            //    ketool.fillPageControl(ke);
            //}

            //设计单位
            var s = (from t in db.CF_Prj_Ent
                     join a in db.CF_App_List on t.FAppId equals a.FId
                     where a.FPrjId == app.FPrjId
                     && a.FManageTypeId == 291 && t.FEntType == 155
                     && a.FState == 6
                     select new
                     {
                         t.FId,
                         t.FBaseInfoId,
                         t.FName,
                         t.FLevelName,
                         t.FCertiNo,
                         t.FMoney,
                         t.FPlanDate,
                         t.FAppId,
                         LHT = db.CF_Prj_Data.Where(l => l.FAppId == a.FId).Select(l => l.FFloat10).FirstOrDefault()
                     }).FirstOrDefault();
            if (s != null)
            {
                pageTool stool = new pageTool(this.Page, "s_");
                stool.fillPageControl(s);
                //查询联合体信息
                if (s.LHT == 1)//设计
                {
                    t_FFloat10.Checked = true;
                    otherSJ.Attributes.Add("style", "display:block");
                    mainSJ.Attributes.Add("style", "display:block");
                }
                else
                {
                    this.t_FFloat10.Checked = false;
                }
                ShowPrjEnt(s.FAppId, 15503, repeaterDisplay);//显示设计联合体
            }

            var se = (from t in db.CF_Prj_Emp
                      join a in db.CF_App_List on t.FAppId equals a.FId
                      where a.FPrjId == app.FPrjId && a.FManageTypeId == 29201
                      && t.FType == 1 && a.FState == 6
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
        }
    }
    //显示其他子单位信息
    void ShowPrjEnt(string fAppId, int entType, Repeater rep)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fid,fname from CF_Prj_Ent where fenttype=" + entType);
        sb.Append(" and fappid='" + fAppId + "'");
        RCenter rc = new RCenter();
        DataTable dt = rc.GetTable(sb.ToString());
        rep.DataSource = dt;
        rep.DataBind();
    }
    /// <summary>
    /// 显示单体工程信息
    /// </summary>
    void ShowPrjItemInfo(string FPrjId)
    {

        string prjType = t_FType.SelectedValue;
        if (prjType == "2000101")
        {
            dg_List.Columns[3].HeaderText = "工程设计等级";
            var App = db.CF_PrjItem_BaseInfo.Where(t => t.FPrjId == FPrjId).Select(t => new
            {
                t.FPrjItemName,
                FDJ = t.FLevel,
                FId = t.FId,
                t.FPrjId
            }).ToList();
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
        else if (prjType == "2000102")
        {
            dg_List.Columns[3].HeaderText = "工程类别";
            var App = db.CF_PrjItem_BaseInfo.Where(t => t.FPrjId == EConvert.ToString(FPrjId)).Select(t => new
            {
                t.FPrjItemName,
                FDJ = db.CF_Sys_Dic.Where(d => d.FNumber == t.FType).Select(d => d.FName).FirstOrDefault(),
                FId = t.FId,
                t.FPrjId
            }).ToList();
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }
    ///<summary>
    ///显示工程信息和建设单位
    ///</summary>
    private void ShowPrjInfo(string FPrjId)
    {
        CF_Prj_BaseInfo prj = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId).FirstOrDefault();
        if (prj != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(prj);
            govd_FRegistDeptId.fNumber = t_FAddressDept.Value;

            //重新绑定"资金来源"字典
            conBindZJLR(prj.FType.GetValueOrDefault());
            t_FFunds.SelectedValue = prj.FFunds.ToString();

            CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == prj.FBaseinfoId).FirstOrDefault();
            if (ent != null)
            {
                pageTool toole = new pageTool(this.Page, "e_");
                toole.fillPageControl(ent);
            }
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


    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string fprjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            string FType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FDJ"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('../appmain/AddPrjItem.aspx?fid=" + fid + "&fprjId=" + fprjId + " &IsView=1',800,550);\">" + e.Item.Cells[2].Text + "</a>";
            //e.Item.Cells[3].Text = rc.GetDicName("190", FType);
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    //绑定事件
    protected void repeaterDisplay_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            ((Literal)e.Item.FindControl("lit_NO")).Text = (e.Item.ItemIndex + 1).ToString();
        }
    }
}
