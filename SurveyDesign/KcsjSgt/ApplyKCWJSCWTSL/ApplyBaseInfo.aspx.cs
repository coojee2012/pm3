using System;
using System.Linq;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Web.UI;

public partial class JSDW_ApplyKCWJSCWT_ApplyBaseInfo : Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            showInfo();
        }
    }
    #region 显示

    //绑定默认
    private void BindControl()
    {
        govd_FRegistDeptId.fNumber = ComFunction.GetDefaultDept();
        govd_FRegistDeptId.Dis(3);

    }

    string FAppId;
    int FManageTypeId;

    //显示
    private void showInfo()
    {
        var v = (from a in db.CF_App_List
                 join d in db.CF_Prj_Data on a.FId equals d.FAppId
                 where d.FId == Request.QueryString["FDataId"]
                 select new
                 {
                     a.FId,
                     a.FManageTypeId,
                     a.FBaseinfoId,
                     d.FTxt10,
                     d.FTxt11,
                     a.FPrjId
                 }).FirstOrDefault();
        if (v != null)
        {
            t_FTxt10.Text = v.FTxt10;
            t_FTxt11.Text = v.FTxt11;

            //显示工程信息
            ShowPrjInfo(v.FPrjId);

            //显示建设单位信息
            ShowEntInfo(v.FBaseinfoId);

            //显示审图机构信息
            var ent = (from t in db.CF_Prj_Ent
                       where t.FAppId == v.FId && t.FEntType == 145
                       select new { t.FId, t.FBaseInfoId, t.FName, t.FLevelName, t.FCertiNo, t.FMoney, t.FPlanDate }).FirstOrDefault();
            if (ent != null)
            {
                pageTool tool = new pageTool(this.Page, "s_");
                tool.fillPageControl(ent);
            }


            //显示附件
            FAppId = v.FId;
            FManageTypeId = v.FManageTypeId.Value;
            showFileList();
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
            govd_FRegistDeptId.fNumber = p_FAddressDept.Value;
        }
    }
    /// <summary>
    /// 显示建设单位信息
    /// </summary>
    private void ShowEntInfo(string fBId)
    {
        CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == fBId).FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, "e_");
            tool.fillPageControl(ent);
            e_FAddress.Text = ent.FEmail;
        }
    }

    #endregion

    #region 附件

    //显示附件
    private void showFileList()
    {
        var v = from t in db.CF_Sys_PrjList
                where t.FManageType == FManageTypeId
                orderby t.FOrder
                select new
                {
                    t.FId,
                    t.FFileName,
                    t.FFileAmount,
                    t.FRemark,
                    t.FOrder,
                    FIsMust = t.FIsMust == 1 ? "<font color='red'>是</font>" : "否",
                    AppFile = db.CF_AppPrj_FileOther.Where(f => t.FId == f.FPrjFileId && f.FAppId == FAppId)
                };

        Pager1.RecordCount = v.Count();
        rep_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        rep_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页时隐藏分页控件
    }

    //一层列表
    protected void rep_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            IQueryable<CF_AppPrj_FileOther> AppFile = DataBinder.Eval(e.Item.DataItem, "AppFile") as IQueryable<CF_AppPrj_FileOther>;
            if (AppFile != null && AppFile.Count() > 0)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = AppFile.Count().ToString();
                ((Literal)e.Item.FindControl("lit_Has")).Text = "<font color='green'>是</font>";


                Repeater rep_File = (Repeater)e.Item.FindControl("rep_File");
                rep_File.DataSource = AppFile;
                rep_File.DataBind();
            }
            else
            {
                ((Literal)e.Item.FindControl("lit_Has")).Text = "<font color='red'>否</font>";
            }
        }
    }


    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showFileList();

    }

    #endregion
}
