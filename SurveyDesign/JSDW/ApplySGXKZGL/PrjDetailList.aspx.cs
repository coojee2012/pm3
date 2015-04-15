using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using Approve.RuleCenter;
using Tools;
using System.Data;
using EgovaBLL;

public partial class JSDW_APPLYSGXKZGL_PrjDetailList : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            this.hf_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            lblTitle.InnerText = "建筑工程项目明细表";
            showInfo();
            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool.ExecuteScript("btnEnable();");
            }
        }
    }

    //显示
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        var v = dbContext.TC_SGXKZ_PrjDetail.Where(t => t.FAppId == hf_FAppId.Value);
        dg_List.DataSource = v;
        dg_List.DataBind();
        
    }
    //保存
    private void saveInfo()
    {
        
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);

        tool.DelInfoFromGrid(dg_List, dbContext.TC_SGXKZ_PrjDetail, tool_Deleting);
        showInfo();
    }
    //级联删除人员
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        
    }



    /// <summary>
    /// 删除选择的企业的人员
    /// </summary>
    private IList<string> GetGridCheckIds(DataGrid grid)
    {
        string FId = "";

        int RowCount = grid.Items.Count;
        IList<string> FIdList = new List<string>();
        for (int i = 0; i < grid.Items.Count; i++)
        {
            CheckBox cbx = (CheckBox)grid.Items[i].Cells[0].Controls[1];
            if (cbx.Checked)
            {
                FId = grid.Items[i].Cells[grid.Columns.Count - 1].Text.Trim();

                FIdList.Add(FId);
            }
        }
        return FIdList;
    }


    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string fAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string fPrjItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "PrjItemId"));
            string fPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "PrjId"));

            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('PrjDetailInfo.aspx?fId=" + fId + "&SgxkzInfoID=" + hf_SgxkzId.Value + "&fAppId=" + fAppId + "&fPrjItemId=" + fPrjItemId + "&fprjId=" + fPrjId + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;

    }
}