using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using Tools;

public partial class Government_AppBHGD_BatchList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadList();
            BindDll();
        }
    }

    private void LoadList()
    {
        EgovaDB db = new EgovaDB();
        var dbbatch = from b in db.TC_BHGD_Batch
                      join f in db.TC_BHGD_PrjItemMappingBatch on b.FId equals f.FBatchId
                      //join q in db.TC_BZGD_Record on f.FPrjItemId equals q.FPrjItemId
                      select new
                      {
                          FId = b.FId,
                          FYear = b.FYear,
                          FBatchNumber = b.FBatchNumber,
                          工程名称 = db.TC_BZGD_Record.FirstOrDefault(t => t.FPrjItemId == f.FPrjItemId).ProjectName,
                          申报单位 = "",
                          审批环节 = "未读取"
                      };
        gv_list.DataSource = dbbatch;
        gv_list.DataBind();
    }

    private void BindDll() {
        EgovaDB db = new EgovaDB();
        var yearlist = from q in db.TC_BHGD_Batch
                       group q by q.FYear into p
                       select new { FYear = p.Key};
               
        ddlYear.DataTextField = "FYear";
        ddlYear.DataValueField = "FYear";
        ddlYear.DataSource = yearlist;
        ddlYear.DataBind();

    }

    public void BtnQuery(object sender, EventArgs e)
    {
        LoadList();
    }

    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text =
                (e.Item.ItemIndex + 1 + this.Pager1.PageSize*(this.Pager1.CurrentPageIndex - 1)).ToString();
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
        LoadList();
    }

    public void BtnDel_Click(object sender,EventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(gv_list, dbContext.TC_BHGD_Batch, tool_Deleting);
        LoadList();
    }

    private void tool_Deleting(IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        EgovaDB db = new EgovaDB();
        var mapping = from d in  db.TC_BHGD_PrjItemMappingBatch
                      where FIdList.ToArray().Contains(d.FBatchId)
                      select d;
        db.TC_BHGD_PrjItemMappingBatch.DeleteOnSubmit(mapping.FirstOrDefault());
        db.SubmitChanges();
    } 
}