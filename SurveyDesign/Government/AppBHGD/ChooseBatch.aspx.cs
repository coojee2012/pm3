using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;

public partial class Government_AppBHGD_ChooseBatch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //FLinkId 为 select FAppId from TC_BZGD_Record
            var linkid = EConvert.ToString(Request.QueryString["FLinkId"]);
            LoadData();
        }
    }

    private void LoadData()
    {
        EgovaDB db = new EgovaDB();
        var batch = from a in db.TC_BHGD_Batch
            select a;

        gv_list.DataSource = batch;
        gv_list.DataBind();

    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text =
                (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            //string fAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            //string fPrjItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjItemId"));
            //string fPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));

            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('EntInfo.aspx?fId=" + fId +
                                   ",900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        LoadData();
    }
}