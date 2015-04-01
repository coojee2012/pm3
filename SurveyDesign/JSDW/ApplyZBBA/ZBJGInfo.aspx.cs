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


public partial class JSDW_ApplyZBBA_ZBJGINFO : System.Web.UI.Page
{
    EgovaDB1 dbcontext1 = new EgovaDB1();
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindContent();
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                txtFId.Value = Request.QueryString["fid"];
                ShowTitle();
            }
            else
            {

                EgovaDB dbContext = new EgovaDB();
                var jsdw = dbContext.TC_ZBJG_Record
                    .Where(t => t.FAppId == EConvert.ToString(Session["FAppId"]))
                    .FirstOrDefault();
                if (jsdw != null)
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.fillPageControl(jsdw);
                    ViewState["FID"] = jsdw.FId;
                    ViewState["FAppId"] = jsdw.FAppId;
                    ViewState["BDId"] = jsdw.BDId;
                    txtFId.Value = jsdw.FId;
                    ShowFile(txtFId.Value);
                }
            }
            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool1.ExecuteScript("btnEnable();");
            }
        }
    }
    private void BindContent()
    {

        //中标结果
        DataTable dt = rc.getDicTbByFNumber("112219");
        t_ZBJG.DataSource = dt;
        t_ZBJG.DataTextField = "FName";
        t_ZBJG.DataValueField = "FNumber";
        t_ZBJG.DataBind();
    }
    private void ShowTitle()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_ZBJG_Record qa = dbContext.TC_ZBJG_Record.Where(t => t.FId == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
        pageTool tool = new pageTool(this.Page);
        tool.fillPageControl(qa);
        txtFId.Value = qa.FId;
        Session["FAppId"] = qa.FAppId;
        ViewState["FAppId"] = qa.FAppId;
        ViewState["FPrjId"] = qa.FPrjId;
        ViewState["BDId"] = qa.BDId;
        ShowFile(txtFId.Value);
    }

    private void ShowFile(string FId)
    {
        EgovaDB dbContext = new EgovaDB();
        var App = dbContext.TC_SGXKZ_File.Where(t => t.FLinkId == FId).Select(t => new
        {
            t.FileName,
            t.FId,
            t.ReportTime
        }).ToList();
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string fId = txtFId.Value;
        TC_ZBJG_Record qa = new TC_ZBJG_Record();
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(fId))
        {
            qa = dbContext.TC_ZBJG_Record.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            qa.FId = Guid.NewGuid().ToString();
        }
        qa = tool.getPageValue(qa);
        dbContext.SubmitChanges();
        txtFId.Value = fId;
        //  showPrjData();
        ScriptManager.RegisterStartupScript(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowFile(txtFId.Value);
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('File.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, dbContext.TC_SGXKZ_File, tool_Deleting);
        ShowFile(txtFId.Value);
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
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowFile(txtFId.Value);
    }


    protected void btnSelhxr_Click(object sender, EventArgs e)
    {
        var result = (from t in dbContext.TC_PBBG_ZBHXR
                      where t.QYId == this.t_QYId.Value
                      select t).SingleOrDefault();
        t_ZHONGBR.Text = result.HXRMC;    
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
        //var result = (from t in dbContext.TC_PBBG_ZBHXR
        //              where t.QYId == this.t_QYId.Value
        //              select t).SingleOrDefault();
        //t_ZHONGBR.Text = result.HXRMC;

        string qybh = this.h_selEntId.Value.ToString();
        var result = (from tb in dbcontext1.QY_JBXX
                      where tb.QYBM == qybh
                      select tb
       ).SingleOrDefault();
        t_ZBDLDW.Text = result.QYMC.ToString();
    }
}