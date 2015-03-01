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

public partial class JSDW_ApplyZBBA_BDHFInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                txtFId.Value = Request.QueryString["fid"];
                showInfo();
                
            }
            else
            {
                
                EgovaDB dbContext = new EgovaDB();
                var jsdw = dbContext.TC_BDHF_Record
                    .Where(t => t.FAppId == EConvert.ToString(Session["FAppId"]))
                    .FirstOrDefault();
                if (jsdw != null)
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.fillPageControl(jsdw);
                    ViewState["FID"] = jsdw.FId;
                    txtFId.Value = jsdw.FId;
                    if (true == jsdw.HFBD)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "showTa", "<script>showTa();</script>");
                        ShowPrjItemInfo();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "hideTa", "<script>hideTa();</script>");
                    }
                    
                }
            }
        }
    }
    void BindControl()
    {
        //招标方式
        DataTable dt = rc.getDicTbByFNumber("112206");
        t_ZBJJFS.DataSource = dt;
        t_ZBJJFS.DataTextField = "FName";
        t_ZBJJFS.DataValueField = "FNumber";
        t_ZBJJFS.DataBind();

        //资格预审方式
        dt = rc.getDicTbByFNumber("112207");
        t_ZGYSFS.DataSource = dt;
        t_ZGYSFS.DataTextField = "FName";
        t_ZGYSFS.DataValueField = "FNumber";
        t_ZGYSFS.DataBind();

        //招标类别
        dt = rc.getDicTbByFNumber("112208");
        t_ZBLB.DataSource = dt;
        t_ZBLB.DataTextField = "FName";
        t_ZBLB.DataValueField = "FNumber";
        t_ZBLB.DataBind();

        //招标范围
        dt = rc.getDicTbByFNumber("112205");
        t_ZBFW.DataSource = dt;
        t_ZBFW.DataTextField = "FName";
        t_ZBFW.DataValueField = "FNumber";
        t_ZBFW.DataBind();

    }
    //显示
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        TC_BDHF_Record prj = dbContext.TC_BDHF_Record.Where(t => t.FId == Convert.ToString(ViewState["FID"])).FirstOrDefault();
        if (prj != null)
        {
            tool.fillPageControl(prj);
            ViewState["HFBD"] = prj.HFBD;
            Session["FAppId"] = prj.FAppId;
            if (true == prj.HFBD)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "showTa", "<script>showTa();</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "hideTa", "<script>hideTa();</script>");
            }
        }
    }
    //保存
    private void saveInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        string fId = EConvert.ToString(ViewState["FID"]);
        TC_BDHF_Record Emp = new TC_BDHF_Record();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_BDHF_Record.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            
        }
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        ViewState["FID"] = fId;
        txtFId.Value = fId;


        //showInfo();
        //tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
        // ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
        //    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "js", "alert('保存成功');window.returnValue='1';", true);
        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
        //tool.showMessage("alert('保存成功');window.returnValue='1';");
    }

    /// <summary>
    /// 显示标段
    /// </summary>
    void ShowPrjItemInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        var App = dbContext.TC_BDHF_BD.Where(t => t.BDHFBAId == EConvert.ToString(ViewState["FID"])).Select(t => new
        {
            t.BDBM,
            t.BDMC,
            t.BDSM,
            t.FId,
            t.ZBBM
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
        tool.DelInfoFromGrid(dg_List, dbContext.TC_BDHF_BD, tool_Deleting);
        ShowPrjItemInfo();
    }
    //单项工程删除
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        
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
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('BDInfo.aspx?fid=" + fid +  "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowPrjItemInfo();
    }
    protected void t_HFBD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (t_HFBD.SelectedValue == "0")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hideTa", "<script>hideTa();</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showTa", "<script>showTa();</script>");
        }
    }
}