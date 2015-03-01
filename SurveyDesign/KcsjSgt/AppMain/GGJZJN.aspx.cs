using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;
using Approve.RuleCenter;
using System.Text;

public partial class KcsjSgt_AppMain_GGJZJN : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    public string ReportServer = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportServer = rc.GetSysObjectContent("_ReportServer");
        if (string.IsNullOrEmpty(ReportServer))
        {
            ReportServer = "http://" + Request.Url.Host + ":8075/WebReport/ReportServer?reportlet=";
        }
        if (!IsPostBack)
        {
            showInfo();
        }
    }
    ProjectDB db = new ProjectDB();
    //显示
    private void showInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        var v = from t in db.CF_Prj_JZJNInfo
                where t.FBaseinfoId == FBaseinfoID
                && t.FPrjType == 1//公共建筑
                orderby t.FTime descending
                select new
                {
                    t.FId,
                    t.FLinkId,
                    t.FPrjName,
                    t.FCreateTime,
                    t.JSDW,
                    t.FArea,
                    t.FBackUpNo,
                    t.SJDW
                };
        if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
            v = v.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页不显示
    }

    //列表
    protected void DG_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbautoid = (Label)e.Row.FindControl("lbautoid");
            lbautoid.Text = (e.Row.RowIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
            //打印
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    //刷新按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    //删除 
    protected void btnDel_Click(object sender, EventArgs e)
    {
        Tools.pageTool tool = new Tools.pageTool(this.Page);
        tool.DelInfoFromGrid(DG_List, db.CF_Prj_JZJNInfo);
        showInfo();
    }
}
