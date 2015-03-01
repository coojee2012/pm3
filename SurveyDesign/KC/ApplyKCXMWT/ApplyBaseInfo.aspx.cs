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
public partial class JSDW_appmain_ApplyBaseInfo : Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            showInfo();
        }
    }

    //绑定
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
        string FDataID = Request.QueryString["FDataID"];
        var v = (from t in db.CF_App_List
                 join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                 where d.FId == FDataID && t.FManageTypeId == 280
                 select new
                 {
                     t.FId,
                     t.FManageTypeId,
                     t.FPrjId,
                     t.FLinkId,
                     t.FBaseinfoId,
                     t.FState,
                     d.FTxt10,
                     d.FTxt11,
                     d.FInt3
                 }).FirstOrDefault();
        if (v != null)
        {
            string FLinkId = v.FLinkId;
            string FPrjId = v.FPrjId;
            string FromFBaseinfoId = v.FBaseinfoId;

            t_FTxt10.Text = v.FTxt10;
            t_FTxt11.Text = v.FTxt11;

            ///////////

            if (v.FInt3 == 1)
            {
                this.t_FInt3.Checked = true;
                //显示其他勘察单位信息
                ShowPrjEnt(FLinkId, 15502);
                kcOther.Attributes.Add("style", "display:");
            }
            else if (v.FInt3 == 0)
            {
                this.t_FInt3.Checked = false;
            }
          
            /////////////

            //显示各单位信息
            ShowPrjEnt("e_", FLinkId, 100);
            ShowPrjEnt("k_", FLinkId, 15501);
            ShowPrjEnt("j_", FLinkId, 126);



            //显示工程信息
            ShowPrjInfo(FPrjId);

            ShowEntInfo(FromFBaseinfoId);


            //已提交不能修改
            if (v.FState > 0)
            {
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("btnEnable();");
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
    private void ShowEntInfo(string FBaseinfoId)
    {
        CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == FBaseinfoId).FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, "e_");
            tool.fillPageControl(ent);
            e_FAddress.Text = ent.FEmail;
        }
        e_FBaseInfoId.Value = CurrentEntUser.EntId;
        e_FId.Value = string.Empty;
    }

    //显示相关企业
    private void ShowPrjEnt(string tag, string fAppId, int entType)
    {
        var ent = db.CF_Prj_Ent.Where(t => t.FAppId == fAppId
            && t.FEntType == entType).FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, tag);
            tool.fillPageControl(ent);
        }
    }
    //绑定其他勘察单位信息
    private void ShowPrjEnt(string fAppId, int entType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fname,fcertino,fid from cf_prj_ent where fappid='" + fAppId + "'");
        sb.Append(" and fenttype='" + entType + "'");
        DataTable ent = rc.GetTable(sb.ToString());
        //var ent = db.CF_Prj_Ent.Where(t => t.FAppId == fAppId
        //    && t.FEntType == entType).Select(t => new
        //    {
        //        t.FName,
        //        t.FCertiNo,
        //        t.FId
        //    }).FirstOrDefault();

        if (ent != null)
        {
            this.repeaperDis.DataSource = ent;
            this.repeaperDis.DataBind();
        }
    }
    //绑定事件
    protected void repeaperDis_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            (e.Item.FindControl("lit_NO") as Literal).Text = (e.Item.ItemIndex + 1).ToString();
        }
    }

    #region 附件

    //显示附件
    private void showFileList()
    {
        string FDataID = Request.QueryString["FDataID"];
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
                    AppFile = db.CF_AppPrj_FileOther.Where(f => t.FId == f.FPrjFileId && f.FAppId == FDataID)
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
                ((Literal)e.Item.FindControl("lit_Count")).Text = "0";
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
