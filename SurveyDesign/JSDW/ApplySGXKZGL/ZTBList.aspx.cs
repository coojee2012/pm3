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

public partial class JSDW_ApplySGXKZGL_ZTBList : System.Web.UI.Page
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

            bool isbz = false;
            XMHJCL_Business business = new XMHJCL_Business();
            DataTable dt = business.QueryData(hf_FAppId.Value, XMHJCL_Business.环节材料信息.招投标信息, out isbz);

            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0 || isbz)
            {
                ListItem i = new ListItem("标准库数据", "3");
                t_BL.Items.Add(i);
                tool1.ExecuteScript("btnEnable();");
            }
            ShowInfo(isbz, dt);
        }
        else
        {
            if (t_BL.SelectedItem.Value != "1" && t_BL.SelectedItem.Value != "3")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr1();</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "hideTr1", "<script>hideTr1();</script>");
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        string fId = txtFId.Value;
        TC_SGXKZ_ZBJGBL qa = new TC_SGXKZ_ZBJGBL();
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(fId))
        {
            qa = dbContext.TC_SGXKZ_ZBJGBL.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            TC_SGXKZ_PrjInfo p = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == EConvert.ToString(ViewState["FAppId"])).FirstOrDefault();
            ViewState["FPrjItemId"] = p.FPrjItemId;

            fId = Guid.NewGuid().ToString();
            qa.FId = fId;
            qa.FPrjItemId = EConvert.ToString(p.FPrjItemId);
            qa.FAppId = EConvert.ToString(ViewState["FAppId"]);
            dbContext.TC_SGXKZ_ZBJGBL.InsertOnSubmit(qa);
        }
        qa = tool.getPageValue(qa);
        dbContext.SubmitChanges();
        txtFId.Value = fId;
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    //显示
    private void ShowInfo(bool isbz, DataTable sp)
    {
        if (sp.Rows.Count > 0)
        {
            string strfid = sp.Rows[0]["FId"].ToString();
            string strbl = sp.Rows[0]["bl"].ToString();
            txtFId.Value = strfid;
            dg_List.DataSource = sp.DefaultView;
            dg_List.DataBind();

            if (strbl != "1" && strbl != "3")

            {
                ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr1();</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "hideTr1", "<script>hideTr1();</script>");
            }
            FillPageWithDt tool = new FillPageWithDt();
            tool.fillPageControl(sp, this.Page, "t_");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hideTr1", "<script>hideTr1();</script>");
        }
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, dbContext.TC_SGXKZ_ZBJG, tool_Deleting);
    }
    //级联删除人员
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {

    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        bool isbz = false;
        XMHJCL_Business business = new XMHJCL_Business();
        DataTable dt = business.QueryData(hf_FAppId.Value, XMHJCL_Business.环节材料信息.招投标信息, out isbz);
        ShowInfo(isbz, dt);
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string isbz = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "bl"));

            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('ZBJGList.aspx?fId=" + fId + "&IsBz=" + isbz + "',1300,700);\">" + e.Item.Cells[2].Text + "</a>";
            //e.Item.Cells[2].Text = "<a href='ZBJGList.aspx?fId=" + fId + "&IsBz=" + isbz + "'\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        bool isbz = false;
        XMHJCL_Business business = new XMHJCL_Business();
        DataTable dt = business.QueryData(hf_FAppId.Value, XMHJCL_Business.环节材料信息.招投标信息, out isbz);
        ShowInfo(isbz, dt);
    }
}