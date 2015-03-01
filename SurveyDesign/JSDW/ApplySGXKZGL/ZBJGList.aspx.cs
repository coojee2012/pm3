using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using EgovaDAO;
using Tools;
using System.Data;

public partial class JSDW_ApplySGXKZGL_ZBJGList : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(EConvert.ToString(Session["FAppId"])))
            {
                ViewState["FAppId"] = EConvert.ToString(Session["FAppId"]);
            }
            ShowTitle();
            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool1.ExecuteScript("btnEnable();");
            }
        }
    }

    private void ShowTitle()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_SGXKZ_PrjInfo qa = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == EConvert.ToString(ViewState["FAppId"])).FirstOrDefault();
        ViewState["FPrjItemId"] = qa.FPrjItemId;
        t_JSDW.Text = qa.JSDW;
        t_ProjectName.Text = qa.ProjectName;
        t_PrjItemName.Text = qa.PrjItemName;
        t_Area.Text = EConvert.ToString(qa.Area);
        t_ConstrScale.Text = qa.ConstrScale;
        TC_Prj_Info p = dbContext.TC_Prj_Info.Where(t => t.FId == qa.PrjId).FirstOrDefault();
        t_ProjectNo.Text = p.ProjectNo;
        TC_SGXKZ_ZBJG sp = dbContext.TC_SGXKZ_ZBJG.Where(t => t.FAppId == EConvert.ToString(ViewState["FAppId"])).FirstOrDefault();
        if (sp != null)
        {
            txtFId.Value = sp.FId;
            //txtKCId.Value = sp.KCId;
            //txtSJId.Value = sp.SJId;
            txtJLId.Value = sp.JLId;
            txtSGId.Value = sp.SGId;
            ShowFile(sp.KCId,"KC");
            ShowFile(sp.KCId, "SJ");
            ShowFile(sp.KCId, "JL");
            ShowFile(sp.KCId, "SG");
        }

        pageTool tool = new pageTool(this.Page, "t_");
        tool.fillPageControl(sp);
    }

    private void ShowFile(string FId, string name)
    {
        EgovaDB dbContext = new EgovaDB();
        var App = dbContext.TC_SGXKZ_File.Where(t => t.FLinkId == FId).Select(t => new
        {
            t.FileName,
            t.FId,
            t.ReportTime
        }).ToList();
        //if (name == "KC")
        //{
        //    PagerKC.RecordCount = App.Count();
        //    dg_ListKC.DataSource = App.Skip((PagerKC.CurrentPageIndex - 1) * PagerKC.PageSize).Take(PagerKC.PageSize);
        //    dg_ListKC.DataBind();
        //    PagerKC.Visible = (PagerKC.RecordCount > PagerKC.PageSize);
        //}
        //else if (name == "SJ")
        //{
        //    PagerSJ.RecordCount = App.Count();
        //    dg_ListSJ.DataSource = App.Skip((PagerSJ.CurrentPageIndex - 1) * PagerSJ.PageSize).Take(PagerSJ.PageSize);
        //    dg_ListSJ.DataBind();
        //    PagerSJ.Visible = (PagerSJ.RecordCount > PagerSJ.PageSize);
        //}
        if (name == "JL")
        {
            PagerJL.RecordCount = App.Count();
            dg_ListJL.DataSource = App.Skip((PagerJL.CurrentPageIndex - 1) * PagerJL.PageSize).Take(PagerJL.PageSize);
            dg_ListJL.DataBind();
            PagerJL.Visible = (PagerJL.RecordCount > PagerJL.PageSize);
        }
        else if (name == "SG")
        {
            PagerSG.RecordCount = App.Count();
            dg_ListSG.DataSource = App.Skip((PagerSG.CurrentPageIndex - 1) * PagerSG.PageSize).Take(PagerSG.PageSize);
            dg_ListSG.DataBind();
            PagerSG.Visible = (PagerSG.RecordCount > PagerSG.PageSize);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string fId = txtFId.Value;
        //string KCId = txtKCId.Value;
        //string SJId = txtSJId.Value;
        string JLId = txtJLId.Value;
        string SGId = txtSGId.Value;
        TC_SGXKZ_ZBJG qa = new TC_SGXKZ_ZBJG();
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(fId))
        {
            qa = dbContext.TC_SGXKZ_ZBJG.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            //KCId = Guid.NewGuid().ToString();
            //SJId = Guid.NewGuid().ToString();
            JLId = Guid.NewGuid().ToString();
            SGId = Guid.NewGuid().ToString();
            qa.FId = fId;
            //qa.KCId = KCId;
            //qa.SJId = SJId;
            qa.JLId = JLId;
            qa.SGId = SGId;
            qa.FprjItemId = EConvert.ToString(ViewState["FPrjItemId"]);
            qa.FAppId = EConvert.ToString(ViewState["FAppId"]);
            dbContext.TC_SGXKZ_ZBJG.InsertOnSubmit(qa);
        }
        qa = tool.getPageValue(qa);
        dbContext.SubmitChanges();
        txtFId.Value = fId;
        //txtKCId.Value = KCId;
        //txtSJId.Value = SJId;
        txtJLId.Value = JLId;
        txtSGId.Value = SGId;
        //  showPrjData();
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    //protected void btnReload_ClickKC(object sender, EventArgs e)
    //{
    //    ShowFile(txtKCId.Value,"KC");
    //}
    //protected void btnReload_ClickSJ(object sender, EventArgs e)
    //{
    //    ShowFile(txtSJId.Value, "SJ");
    //}
    protected void btnReload_ClickJL(object sender, EventArgs e)
    {
        ShowFile(txtJLId.Value, "JL");
    }
    protected void btnReload_ClickSG(object sender, EventArgs e)
    {
        ShowFile(txtSGId.Value, "SG");
    }
    //protected void App_List_ItemDataBoundKC(object sender, DataGridItemEventArgs e)
    //{
    //    if (e.Item.ItemIndex > -1)
    //    {
    //        e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.PagerKC.PageSize * (this.PagerKC.CurrentPageIndex - 1)).ToString();
    //        string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
    //        e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('File.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
    //    }
    //}
    //protected void App_List_ItemDataBoundSJ(object sender, DataGridItemEventArgs e)
    //{
    //    if (e.Item.ItemIndex > -1)
    //    {
    //        e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.PagerSJ.PageSize * (this.PagerSJ.CurrentPageIndex - 1)).ToString();
    //        string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
    //        e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('File.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
    //    }
    //}
    protected void App_List_ItemDataBoundJL(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.PagerJL.PageSize * (this.PagerJL.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('File.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void App_List_ItemDataBoundSG(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.PagerSG.PageSize * (this.PagerSG.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('File.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    //protected void btnDel_ClickKC(object sender, EventArgs e)
    //{
    //    pageTool tool = new pageTool(this.Page);
    //    tool.DelInfoFromGrid(dg_ListKC, dbContext.TC_SGXKZ_File, tool_Deleting);
    //    ShowFile(txtKCId.Value, "KC");
    //}
    //protected void btnDel_ClickSJ(object sender, EventArgs e)
    //{
    //    pageTool tool = new pageTool(this.Page);
    //    tool.DelInfoFromGrid(dg_ListSJ, dbContext.TC_SGXKZ_File, tool_Deleting);
    //    ShowFile(txtSJId.Value, "SJ");
    //}
    protected void btnDel_ClickJL(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_ListJL, dbContext.TC_SGXKZ_File, tool_Deleting);
        ShowFile(txtJLId.Value, "JL");
    }
    protected void btnDel_ClickSG(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_ListSG, dbContext.TC_SGXKZ_File, tool_Deleting);
        ShowFile(txtSGId.Value, "SG");
    }
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        dbContext = new EgovaDB();
        if (dbContext != null)
        {
            var para = dbContext.TC_SGXKZ_File.Where(t => FIdList.ToArray().Contains(t.FId));
            dbContext.TC_SGXKZ_File.DeleteAllOnSubmit(para);
        }
    }
    //protected void Pager_PageChangingKC(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    //{
    //    PagerKC.CurrentPageIndex = e.NewPageIndex;
    //    ShowFile(txtKCId.Value, "KC");
    //}
    //protected void Pager_PageChangingSJ(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    //{
    //    PagerSJ.CurrentPageIndex = e.NewPageIndex;
    //    ShowFile(txtSJId.Value, "SJ");
    //}
    protected void Pager_PageChangingJL(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerJL.CurrentPageIndex = e.NewPageIndex;
        ShowFile(txtJLId.Value, "JL");
    }
    protected void Pager_PageChangingSG(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerSG.CurrentPageIndex = e.NewPageIndex;
        ShowFile(txtSGId.Value, "SG");
    }
}