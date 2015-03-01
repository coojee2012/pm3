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

public partial class SJ_applysjwjbzwt_ApplyBaseInfo : Page
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

    //显示默认
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
        string FLinkId = Request.QueryString["FDataID"];
        //显示data信息
        //这里从合同备案申请业务取
        var v = (from a in db.CF_App_List
                 join d in db.CF_Prj_Data on a.FId equals d.FAppId
                 where d.FId == FLinkId
                 select new
                 {
                     a.FId,
                     a.FManageTypeId,
                     d.FTxt10,
                     d.FTxt11,
                     a.FPrjId,
                     d.FFloat10
                 }).FirstOrDefault();
        if (v != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(v);


            //显示各单位信息
            ShowPrjEnt("e_", v.FId, 100);
            ShowPrjEnt("k_", v.FId, 155);

            //显示工程基本信息
            ShowPrjInfo(v.FPrjId);

            //显示附件
            FAppId = v.FId;
            FManageTypeId = v.FManageTypeId.Value;
            showFileList();

            if (v.FFloat10 == 1)//设计
            {
                t_FFloat10.Checked = true;
                otherSJ.Attributes.Add("style", "display:block");
                mainSJ.Attributes.Add("style", "display:block");
            }
            else
            {
                this.t_FFloat10.Checked = false;
            }
            ShowPrjEnt(v.FId, 15503, repeaterDisplay);//显示设计联合体
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
    // 显示工程信息 
    private void ShowPrjInfo(string FPrjId)
    {
        CF_Prj_BaseInfo prj = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId).FirstOrDefault();
        if (prj != null)
        {
            pageTool tool = new pageTool(this.Page, "p_");
            tool.fillPageControl(prj);
            govd_FRegistDeptId.fNumber = p_FAddressDept.Value;
            //绑定查看按钮
            btnSee.Attributes.Add("onclick", "showAddWindow('../../JSDW/appmain/AddPrjRegist.aspx?fid=" + FPrjId + "',900,700);return false;");
        }
    }

    //显示企业信息
    private void ShowPrjEnt(string tag, string fAppId, int entType)
    {
        var ent = db.CF_Prj_Ent.Where(t => t.FAppId == fAppId && t.FEntType == entType).FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, tag);
            tool.fillPageControl(ent);
        }
    }
    #region 附件
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
    //绑定事件
    protected void repeaterDisplay_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            ((Literal)e.Item.FindControl("lit_NO")).Text = (e.Item.ItemIndex + 1).ToString();
        }
    }
    #endregion
}
