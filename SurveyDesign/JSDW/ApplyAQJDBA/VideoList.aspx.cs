using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Data;
using EgovaDAO;
using Tools;
using System.Text;
using System.Web.Services;

public partial class JSDW_ApplyAQJDBA_VideoList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    EgovaDB dbContext = new EgovaDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                txtFId.Value = Request.QueryString["fid"];
                showInfo();
                ShowPrjItemInfo();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["fAppId"]))
            {
                TC_AJBA_Record aj = dbContext.TC_AJBA_Record.Where(t => t.FAppId == Request.QueryString["fAppId"]).FirstOrDefault();
                ViewState["FAppId"] = aj.FAppId;
                ViewState["FPrjItemId"] = aj.FPrjItemId;
            }
        }
    }
    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
        TC_AJBA_VideoSupply prj = dbContext.TC_AJBA_VideoSupply.Where(t => t.FId == Convert.ToString(ViewState["FID"])).FirstOrDefault();
        if (prj != null)
        {
            tool.fillPageControl(prj);
            ViewState["FAppId"] = prj.FAppId;
        }
    }
    //保存
    private void saveInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        string fId = EConvert.ToString(ViewState["FID"]);
        TC_AJBA_VideoSupply Emp = new TC_AJBA_VideoSupply();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_AJBA_VideoSupply.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FPrjItemId = EConvert.ToString(ViewState["FPrjItemId"]);
            Emp.FAppId = EConvert.ToString(ViewState["FAppId"]);
            dbContext.TC_AJBA_VideoSupply.InsertOnSubmit(Emp);
        }
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        ViewState["FID"] = fId;
        txtFId.Value = fId;
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }

    void ShowPrjItemInfo()
    {
        var App = dbContext.TC_AJBA_Video.Where(t => t.FLinkId == EConvert.ToString(ViewState["FID"]) && t.FAppId == EConvert.ToString(ViewState["FAppId"])).Select(t => new
        {
            t.VideoCode,
            t.Address,
            IsCallPlaceStr = (t.IsCallPlace == true)?"是":"否",
            t.FId,
            t.EquipTyp
        }).ToList();
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
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
        tool.DelInfoFromGrid(dg_List, dbContext.TC_AJBA_Video, tool_Deleting);
        ShowPrjItemInfo();
    }
    //视频删除
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        EgovaDB dbContext = new EgovaDB();
        //视频
        var para = dbContext.TC_AJBA_Video.Where(t => FIdList.ToArray().Contains(t.FId));
        dbContext.TC_AJBA_Video.DeleteAllOnSubmit(para);
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowPrjItemInfo();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('Video.aspx?fid=" + fid +  "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowPrjItemInfo();
    }
}