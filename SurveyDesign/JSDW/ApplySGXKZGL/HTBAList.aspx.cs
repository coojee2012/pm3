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

public partial class JSDW_ApplySGXKZGL_HTBAList : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(EConvert.ToString(Session["FAppId"])))
            {
                ViewState["FAppId"] = EConvert.ToString(Session["FAppId"]);
                this.hf_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            }
            
            ClientScript.RegisterStartupScript(this.GetType(), "hideTr1", "<script>hideTr1();</script>");
            ShowTitle();
            showInfo();
            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool.ExecuteScript("btnEnable();");
            }
        }
    }

    private void ShowTitle()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_SGXKZ_PrjInfo p = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == EConvert.ToString(ViewState["FAppId"])).FirstOrDefault();
        ViewState["FPrjItemId"] = p.FPrjItemId;

        TC_SGXKZ_HTBABL qa = dbContext.TC_SGXKZ_HTBABL.Where(t => t.FAppId == EConvert.ToString(ViewState["FAppId"])).FirstOrDefault();
        if (qa != null)
        {
            txtFId.Value = qa.FId;
            if (!string.IsNullOrEmpty(qa.BL))
            {
                if (qa.BL != "1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr1();</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "hideTr1", "<script>hideTr1();</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "hideTr1", "<script>hideTr1();</script>");
            }
        }

        pageTool tool = new pageTool(this.Page, "t_");
        tool.fillPageControl(qa);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        string fId = txtFId.Value;
        TC_SGXKZ_HTBABL qa = new TC_SGXKZ_HTBABL();
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(fId))
        {
            qa = dbContext.TC_SGXKZ_HTBABL.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            qa.FId = fId;
            qa.FPrjItemId = EConvert.ToString(ViewState["FPrjItemId"]);
            qa.FAppId = EConvert.ToString(ViewState["FAppId"]);
            dbContext.TC_SGXKZ_HTBABL.InsertOnSubmit(qa);
        }
        qa = tool.getPageValue(qa);
        dbContext.SubmitChanges();
        txtFId.Value = fId;
        ShowTitle();
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    //显示
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        var v = 
        from t in dbContext.TC_SGXKZ_HTBA
                where t.FAppId == hf_FAppId.Value 
                orderby t.FId
                select new
                {
                    t.HTBABH,
                    t.HTJE,
                    HTLBStr = dbContext.CF_Sys_Dic.Where(d => d.FNumber == Convert.ToInt32(t.HTLB)).Select(d => d.FName).FirstOrDefault(),
                    t.FBDWMC,
                    t.FId,
                    t.FAppId
                };
        dg_List.DataSource = v;
        dg_List.DataBind();
        ShowTitle();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, dbContext.TC_SGXKZ_HTBA, tool_Deleting);
        showInfo();
    }
    //级联删除人员
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        
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

            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('HTBA.aspx?fId=" + fId + "',1100,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
}