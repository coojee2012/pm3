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
            hf_bhgdAppid.Value = linkid;
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
    protected void gv_list_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Choose") {
            //得到选择的批次ID
            var id = e.CommandArgument.ToString();

            //得到标化工地的appid
            var hfappid = hf_bhgdAppid.Value;
            //根据appid 找到单项工程id
            var prjItemdid = GetPrjItemIdByAppid(hfappid);
            //如果有单项工程id 就保存 单项工程与批次的绑定。
            if (!string.IsNullOrEmpty(prjItemdid))
            {
                SavePrjItemMappingBatch(id, prjItemdid,hfappid);
            }
            else {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "", "alert('未找到单项工程');", true);
            }            
        }
    }

    /// <summary>
    /// 根据appid 找到单项工程ID
    /// </summary>
    /// <param name="appid"></param>
    /// <returns></returns>
    private string GetPrjItemIdByAppid(string appid) {
        EgovaDB db = new EgovaDB();
        var prjItem = from item in db.v_PrjItem_Info
                      join hzgd in db.TC_BZGD_Record
                      on item.FId equals hzgd.FPrjItemId
                      where hzgd.FAppId == appid

                      select item;


        return prjItem.FirstOrDefault() != null ? prjItem.FirstOrDefault().FId : string.Empty;
    }
    /// <summary>
    /// 保存项目与批次关系
    /// </summary>
    protected void SavePrjItemMappingBatch(string batchId,string prjItemid,string appid) {
        //是否已经存在对应关系，如果存在给予提示。
        if (IsHaveMappint(prjItemid))
        {
            UpdatePrjItemMappingBatch(batchId, prjItemid);
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "js", "alert('修改批次成功!');", true);
        }
        else { 
            //保存映射关系
            EgovaDB db = new EgovaDB();

            TC_BHGD_PrjItemMappingBatch mapping = new TC_BHGD_PrjItemMappingBatch(){
                 FId = Guid.NewGuid().ToString(),
                 FBatchId = batchId,
                 FPrjItemId = prjItemid,
                 FAppId= appid
            };

            db.TC_BHGD_PrjItemMappingBatch.InsertOnSubmit(mapping);
            db.SubmitChanges();
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "js", "alert('保存成功!');window.returnValue='1';window.close();", true);
        }

    }

    private void UpdatePrjItemMappingBatch(string batchId, string prjItemid) {

        EgovaDB db = new EgovaDB();

        var mapping = db.TC_BHGD_PrjItemMappingBatch.FirstOrDefault(item => item.FPrjItemId == prjItemid);
        mapping.FBatchId = batchId;
        db.SubmitChanges();
    }
    /// <summary>
    /// 是否存在对应关系
    /// </summary>
    /// <param name="prjItemid">单项工程</param>
    /// <returns>true:存在，false:不存在</returns>
    protected bool IsHaveMappint(string prjItemid) {
        EgovaDB db = new EgovaDB();


        var mapping = from d in db.TC_BHGD_PrjItemMappingBatch
                      where d.FPrjItemId == prjItemid
                      select d;

        return mapping.Count() > 0 ? true : false;
    }
}