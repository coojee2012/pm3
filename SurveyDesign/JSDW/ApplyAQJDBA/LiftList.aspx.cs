using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using EgovaDAO;
using Tools;

public partial class JSDW_ApplyAQJDBA_LiftList : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    private RCenter rcXM = new RCenter("XM_BaseInfo");
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
        var v = from t in dbContext.TC_AJBA_QZSB
                orderby t.SBXH
                select new
                {
                    t.FAppId,
                    t.SBMC,
                    t.BABH,
                    t.SBXH,
                    t.SYDW,
                    t.AZDW,
                    t.FSBId,
                    t.FId
                };
        if (!string.IsNullOrEmpty(EConvert.ToString(Session["FAppId"])))
        {
            v = v.Where(t => t.FAppId.Contains(EConvert.ToString(Session["FAppId"])));
        }
        if (!string.IsNullOrEmpty(this.txtSBMC.Text.Trim()))
        {
            v = v.Where(t => t.SBMC.Contains(this.txtSBMC.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.txtBABH.Text.Trim()))
        {
            v = v.Where(t => t.BABH.Contains(this.txtBABH.Text.Trim()));
        }
        Pager1.RecordCount = v.Count();
        this.dg_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowTitle();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('Lift.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, dbContext.TC_AJBA_QZSB, tool_Deleting);

        ShowTitle();
    }
    //设备删除
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        if (context != null)
        {
            EgovaDB dbContext = (EgovaDB)context;
            var para = dbContext.TC_AJBA_QZSB.Where(t => FIdList.ToArray().Contains(t.FId));
            var paras = dbContext.TC_AJBA_QZSB.Where(t => FIdList.ToArray().Contains(t.FId)).Select(t=>t.BABH).ToList();
            foreach (string s in paras)
            {
                string sql = @"update GC_JQSBXX set syzt = 0 where sbbabh = '" + s + "'";
                rcXM.PExcute(sql);
            }
            dbContext.TC_AJBA_QZSB.DeleteAllOnSubmit(para);

        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowTitle();
    }
}