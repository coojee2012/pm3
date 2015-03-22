using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;

public partial class Government_AppBHGD_FZList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadList();
        }
    }

    private void LoadList()
    {
        EgovaDB db = new EgovaDB();
        var list = from b in  db.TC_BZGD_PrjInfo 
                       orderby b.ReportTime
                       select new
                       {
                           b.ProjectName,
                           GCSSD= b.PrjAddressDept,
                           SBDW = b.JSDW,
                           SBRQ = b.ReportTime,
                           BLJL = "",
                           FReportTime = b.ReportTime,
                           BLZT = "",
                           FId= b.FId

                       };

        Pager1.RecordCount = list.Count();
        this.gv_list.DataSource = list.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);

        gv_list.DataBind();

    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        LoadList();
    }
}