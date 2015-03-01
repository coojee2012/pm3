using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.EntityBase;
using Approve.RuleCenter;
using Approve.PersistBase;
using ProjectData;
using System.Linq;
using EgovaDAO;
using Tools;

public partial class JSDW_ApplyAQJDBA_ParticipatList : System.Web.UI.Page
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
            BindControl();
            ShowTitle();
            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool.ExecuteScript("btnEnable();");
            }
        }
    }
    private void ShowTitle()
    {
        var v = from t in dbContext.TC_AJBA_CJDW
                orderby t.CJJS
                select new
                {
                    t.FAppId,
                    t.QYMC,
                    t.CJJS,
                    CJJSStr = dbContext.CF_Sys_Dic.Where(d => d.FNumber == Convert.ToInt32(t.CJJS)).Select(d => d.FName).FirstOrDefault(),
                    t.ZZJGDM,
                    t.ZZDJ,
                    t.ZZZS,
                    t.FId,
                    t.QYID
                };
        if (!string.IsNullOrEmpty(EConvert.ToString(Session["FAppId"])))
        {
            v = v.Where(t => t.FAppId.Contains(EConvert.ToString(Session["FAppId"])));
        }
        AspNetPager1.RecordCount = v.Count();
        this.Ent_List.DataSource = v.Skip((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize).Take(AspNetPager1.PageSize);
        Ent_List.DataBind();
    }

    private void BindControl()
    {
    }

    protected void Ent_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.AspNetPager1.PageSize * (this.AspNetPager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('Participat.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Ent_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {

    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(Ent_List, dbContext.TC_AJBA_CJDW, tool_Deleting);
        ShowTitle();
    }
    // 参建单位删除
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        if (dbContext != null)
        {
            var para = dbContext.TC_AJBA_CJDW.Where(t => FIdList.ToArray().Contains(t.FId));
            dbContext.TC_AJBA_CJDW.DeleteAllOnSubmit(para);
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowTitle();
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        ShowTitle();
    }
}
