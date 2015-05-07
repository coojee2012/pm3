using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Tools;
using System.Data;
using EgovaDAO;

public partial class JSDW_ApplyAQJDBA_Lift : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    private RCenter rcXM = new RCenter("XM_BaseInfo");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["fid"]))
            {

            }
            else
            {
                TC_AJBA_QZSB emp = dbContext.TC_AJBA_QZSB.Where(t => t.FId == Convert.ToString(Request.QueryString["fid"])).FirstOrDefault();
                if (emp != null)
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.fillPageControl(emp);
                }
                ViewState["FID"] = Request.QueryString["fid"];
                txtFId.Value = Request.QueryString["fid"];
                bindczry();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["fAppId"]))
            {
                TC_AJBA_Record aj = dbContext.TC_AJBA_Record.Where(t => t.FAppId == Request.QueryString["fAppId"]).FirstOrDefault();
                ViewState["FAppId"] = aj.FAppId;
                ViewState["FPrjItemId"] = aj.FPrjItemId;
            }
            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool1.ExecuteScript("btnEnable();");
            }
        }
    }
    /// <summary>
    /// 绑定操作人员
    /// </summary>
    private void bindczry()
    {
        var czrylist = from t in dbContext.TC_AJBA_QZSB_CZRY.Where(t => t.FLinkID == ViewState["FID"])
                       select new
                       { 
                           t.Name,
                           t.CZZH,
                           t.Trades,
                           t.ID

                       };
        dg_List.DataSource = czrylist;
        dg_List.DataBind();
    }

    //保存
    private void saveInfo()
    {
        string fId = EConvert.ToString(ViewState["FID"]);
        TC_AJBA_QZSB Emp = new TC_AJBA_QZSB();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_AJBA_QZSB.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FPrjItemId = EConvert.ToString(ViewState["FPrjItemId"]);
            Emp.FAppId = EConvert.ToString(ViewState["FAppId"]);
            dbContext.TC_AJBA_QZSB.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        ViewState["FID"] = fId;
        txtFId.Value = fId;

        string sql = @"update GC_JQSBXX set syzt = 1 where sbbabh='" + t_BABH.Text + "'";
        rcXM.PExcute(sql);
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string selSBId = t_SBID.Value;
        string sql = @"select * from GC_JQSBXX where SBID='" + selSBId + "'";
        DataTable dt = rcXM.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            t_SBMC.Text = row["SBMC"].ToString();
            t_BABH.Text = row["SBBABH"].ToString();
            t_SBXH.Text = row["GGXH"].ToString();
            t_CCBH.Text = row["CCBH"].ToString();
        }

    }
    void ShowPrjItemInfo()
    {
        var App = dbContext.TC_AJBA_QZSB_CZRY.Where(t => t.FLinkID == EConvert.ToString(ViewState["FID"]) && t.FAppID == EConvert.ToString(ViewState["FAppId"])).Select(t => new
        {
            t.Name,
            t.Trades,
            t.ID,
            t.CZZH
        }).ToList();
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, dbContext.TC_AJBA_QZSB_CZRY, tool_Deleting);
        ShowPrjItemInfo();
        bindczry();
    }
    //操作人员删除
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        EgovaDB dbContext = new EgovaDB();
        //操作人员
        //var para = dbContext.TC_AJBA_QZSB_CZRY.Where(t => FIdList.ToArray().Contains(t.ID));
        //dbContext.TC_AJBA_QZSB_CZRY.DeleteAllOnSubmit(para);
        foreach (var v in FIdList)
        {
            TC_AJBA_QZSB_CZRY czry = new TC_AJBA_QZSB_CZRY();
            czry = dbContext.TC_AJBA_QZSB_CZRY.Where(t => t.ID == v).FirstOrDefault();
            dbContext.TC_AJBA_QZSB_CZRY.DeleteOnSubmit(czry);
            dbContext.SubmitChanges();
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowPrjItemInfo();
        bindczry();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ID"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('Lift_CZRY.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowPrjItemInfo();
    }
}