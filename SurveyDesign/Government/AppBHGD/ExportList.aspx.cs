using Approve.Common;
using Approve.RuleCenter;
using EgovaDAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppBHGD_SHDCList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    SaveAsBase sab = new SaveAsBase();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) {
            BindDdl();
            LoadList();
        }
    }

    private void BindDdl() {
        EgovaDB db = new EgovaDB();

        //年份
        var yearlist = from q in db.TC_BHGD_Batch
                       group q by q.FYear into p
                       select new { FYear = p.Key };

        ddlYear.DataTextField = "FYear";
        ddlYear.DataValueField = "FYear";
        ddlYear.DataSource = yearlist;
        ddlYear.DataBind();


        var batchlist = db.TC_BHGD_Batch.DefaultIfEmpty();


        ddl_Batch.DataTextField = "FBatchNumber";
        ddl_Batch.DataValueField = "FYear";
        ddl_Batch.DataSource = batchlist;
        ddl_Batch.DataBind();

        ddl_Batch.Items.Insert(0, new ListItem() { Text = "全部", Value = "", Selected = true });

    }

    private void LoadList()
    {
        EgovaDB db = new EgovaDB();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append(" select  a.FId,a.FYear,a.FBatchNumber,b.FPrjItemId,b.fappid,c.ProjectName,'' SBDW,''SPHJ  from  TC_BHGD_Batch a ,TC_BHGD_PrjItemMappingBatch b,TC_BZGD_Record c where a.FId= b.FBatchId and b.FAppId = c.FAppId");


        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "gv_list";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }

    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            //string fAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            //string fPrjItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjItemId"));
            //string fPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));

            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('EntInfo.aspx?fId=" + fId +
                                   ",900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }

    protected void BtnQuery(object sender, EventArgs e)
    {
        LoadList();
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.curpage = e.NewPageIndex;
        LoadList();
    }


    protected void Onbtn_OutClick(object sender, EventArgs e)
    {

        string sql = this.Pager1.sql.ToString();
        DataTable dt = rc.GetTable(sql);
        if (dt != null)
        {
            this.gv_list.DataSource = dt;
            this.gv_list.DataBind();
            gv_list.Columns[0].Visible = false;
            string fOutTitle = lPostion.Text;
            sab.SaveAsExc(this.gv_list, fOutTitle, this.Response);
        }
    }
}