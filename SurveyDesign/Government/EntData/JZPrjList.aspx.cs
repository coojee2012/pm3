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
public partial class Government_EntData_JZPrjList : System.Web.UI.Page
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
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('AddPrjRegist.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
            string fDeptId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAddressDept"));
            string JsEntId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBaseInfoId"));
            if (!string.IsNullOrEmpty(fDeptId))
                fDeptId = rc.getDept(fDeptId, 1);
            e.Item.Cells[3].Text = fDeptId + e.Item.Cells[3].Text;
            e.Item.Cells[4].Text = db.CF_Ent_BaseInfo.Where(t => t.FId == JsEntId).Select(t => t.FName).FirstOrDefault();
            string FBaseinfoID = Request.QueryString["FBaseinfoId"];
            var app = db.CF_App_List.Where(t => t.FPrjId == fid && t.FToBaseinfoId == FBaseinfoID && t.FManageTypeId == 28001 && t.FState == 6)
                       .Select(t => new { t.FAppDate, FDataID = t.FLinkId }).FirstOrDefault();
            if (app != null)
            {
                e.Item.Cells[6].Text = EConvert.ToShortDateString(app.FAppDate);
                var v = (from t in db.CF_App_List
                         join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                         where d.FId == app.FDataID && t.FManageTypeId == 280
                         select new
                         {
                             t.FId,
                             t.FManageTypeId,
                             t.FPrjId,
                             t.FLinkId,
                             t.FBaseinfoId,
                             d.FTxt10,
                             d.FTxt11
                         }).FirstOrDefault();
                if (v != null)
                {
                    var ent = db.CF_Prj_Ent.Where(t => t.FAppId == v.FLinkId
        && t.FEntType == 15501).FirstOrDefault();
                    if (ent != null)
                    {
                        e.Item.Cells[5].Text = ent.FName;
                    }
                }

            }
            e.Item.Cells[7].Text = EConvert.ToShortDateString(
        (from a in db.CF_App_List
         join d in db.CF_Prj_Data on a.FLinkId equals d.FId
         where d.FIsDeleted == false && a.FIsDeleted == false
            && a.FBaseinfoId == FBaseinfoID && a.FManageTypeId == 28003 && a.FState == 6 && a.FPrjId == fid
         select a.FAppDate)
               .FirstOrDefault());
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, db.CF_Prj_BaseInfo);
        showInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
