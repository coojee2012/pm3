using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Text;
using Approve.RuleCenter;
public partial class Government_EntData_SJPrjList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }
    //显示 
    void showInfo()
    {
        IQueryable<CF_Prj_BaseInfo> App = db.CF_Prj_BaseInfo.Where(t => db.CF_App_List.Where(a => a.FBaseinfoId == Request.QueryString["FBaseinfoId"] && a.FPrjId == t.FId).Any()

            ).OrderByDescending(t => t.FCreateTime);
        if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
            App = App.Where(t => t.FPrjName.Contains(t_FName.Text.Trim()));

        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }

    protected void dg_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hfFId = e.Item.FindControl("hfFId") as HiddenField;
            if (hfFId != null)
            {
                string fid = hfFId.Value;
                string FBaseinfoID = Request.QueryString["FBaseinfoId"];
                Literal liEntName = e.Item.FindControl("liEntName") as Literal;
                if (liEntName != null)
                {
                    liEntName.Text = db.CF_Ent_BaseInfo.Where(t => t.FId == liEntName.Text).Select(t => t.FName).FirstOrDefault();
                }
                Literal liAppDate1 = e.Item.FindControl("liAppDate1") as Literal;
                if (liAppDate1 != null)
                {
                    liAppDate1.Text = EConvert.ToShortDateString(db.CF_App_List.Where(t => t.FPrjId == fid && t.FToBaseinfoId == FBaseinfoID && t.FManageTypeId == 291 && t.FState == 6)
       .Select(t => t.FAppDate).FirstOrDefault());
                }


                Literal liSubmitDate1 = e.Item.FindControl("liSubmitDate1") as Literal;
                if (liSubmitDate1 != null)
                {
                    liSubmitDate1.Text = EConvert.ToShortDateString(
            (from a in db.CF_App_List
             join d in db.CF_Prj_Data on a.FLinkId equals d.FId
             where d.FIsDeleted == false && a.FIsDeleted == false
                && a.FBaseinfoId == FBaseinfoID && a.FManageTypeId == 28803 && a.FState == 6 && a.FPrjId == fid
             select a.FAppDate)
                   .FirstOrDefault());
                }


                Literal liAppDate2 = e.Item.FindControl("liAppDate2") as Literal;
                if (liAppDate2 != null)
                {
                    liAppDate2.Text = EConvert.ToShortDateString(db.CF_App_List.Where(t => t.FPrjId == fid && t.FToBaseinfoId == FBaseinfoID && t.FManageTypeId == 296 && t.FState == 6)
       .Select(t => t.FAppDate).FirstOrDefault());
                }




                Literal liSubmitDate2 = e.Item.FindControl("liSubmitDate2") as Literal;
                if (liSubmitDate2 != null)
                {
                    liSubmitDate2.Text = EConvert.ToShortDateString(
            (from a in db.CF_App_List
             join d in db.CF_Prj_Data on a.FLinkId equals d.FId
             where d.FIsDeleted == false && a.FIsDeleted == false
                && a.FBaseinfoId == FBaseinfoID && a.FManageTypeId == 298 && a.FState == 6 && a.FPrjId == fid
             select a.FAppDate)
                   .FirstOrDefault());
                }


            }
        }

    }
}
