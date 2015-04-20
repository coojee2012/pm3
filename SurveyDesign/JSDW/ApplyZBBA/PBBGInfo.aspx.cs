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

public partial class JSDW_ApplyZBBA_PBBGInfo : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindControl();
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                txtFId.Value = Request.QueryString["fid"];
                ShowTitle();
            }
            else
            {

                EgovaDB dbContext = new EgovaDB();
                var jsdw = dbContext.TC_PBBG_Record
                    .Where(t => t.FAppId == EConvert.ToString(Session["FAppId"]))
                    .FirstOrDefault();
                if (jsdw != null)
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.fillPageControl(jsdw);
                    txtFId.Value = jsdw.FId;
                    ViewState["FID"] = jsdw.FId;
                    ViewState["FAppId"] = jsdw.FAppId;
                    ViewState["BDId"] = jsdw.BDId;
                    ViewState["FPrjId"] = jsdw.FPrjId;
                    txtFId.Value = jsdw.FId;
                    ShowFileBM(txtFId.Value);
                    ShowFileTB(txtFId.Value);
                    ShowFileFB(txtFId.Value);
                    ShowFileHXR(txtFId.Value);
                }
            }
            pageTool tool1 = new pageTool(this.Page);


           
            //是否是审核部门，非0表示审核查看，使用查看
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool1.ExecuteScript("btnEnable();");
                ClientScript.RegisterStartupScript(this.GetType(), "hideBtn", "<script>hideBtn();</script>");
            }
            //如果不是管理部门，则不显示当前人员的证书是否过期
            if (Session["DFRoleId"] == null)
            {
                dg_ListHXR.Columns[6].HeaderStyle.Width = 0;
            }          
        }
    }

    void BindControl()
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
        TC_PBBG_Record qa = dbContext.TC_PBBG_Record.Where(t => t.FId == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
        pageTool tool = new pageTool(this.Page);
        tool.fillPageControl(qa);
        txtFId.Value = qa.FId;
        Session["FAppId"] = qa.FAppId;
        ViewState["FAppId"] = qa.FAppId;
        ViewState["FPrjId"] = qa.FPrjId;
        ViewState["BDId"] = qa.BDId;
        ShowFileBM(txtFId.Value);
        ShowFileTB(txtFId.Value);
        ShowFileFB(txtFId.Value);
        ShowFileHXR(txtFId.Value);
    }

    private void ShowFileBM(string FId)
    {
        EgovaDB dbContext = new EgovaDB();
        var App = dbContext.TC_PBBG_BMQY.Where(t => t.FLinkId == FId).Select(t => new
        {
            t.QYMC,
            t.BMJBR,
            t.LXDH,
            t.BMTime,
            t.FId
        }).ToList();
        PagerBM.RecordCount = App.Count();
        dg_ListBM.DataSource = App.Skip((PagerBM.CurrentPageIndex - 1) * PagerBM.PageSize).Take(PagerBM.PageSize);
        dg_ListBM.DataBind();
        PagerBM.Visible = (PagerBM.RecordCount > PagerBM.PageSize);
    }

    private void ShowFileHXR(string FId)
    {
        dbContext = new EgovaDB();
        var App1 = from t in dbContext.TC_PBBG_ZBHXR
                where t.FLinkId == FId 
                orderby t.FOrder
                select new
        {
            t.HXRMC,
            t.PBJ,
            t.TBJ,
            t.FZRXM,
            t.ZCZSH,
            t.ZC,
            t.ZCZH,
            t.ZCZY,
            t.FId,
            t.RYId,
            t.BDId            
        };
        //记录当前候选人数量，前端判断候选人大于三个则不能继续添加.
        this.hxrcount.Value = App1.Count().ToString();
        PagerHXR.RecordCount = App1.Count();
        dg_ListHXR.DataSource = App1.Skip((PagerHXR.CurrentPageIndex - 1) * PagerHXR.PageSize).Take(PagerHXR.PageSize);
        dg_ListHXR.DataBind();
        PagerHXR.Visible = (PagerHXR.RecordCount > PagerHXR.PageSize);
    }

    private void ShowFileTB(string FId)
    {
        dbContext = new EgovaDB();
        var App2 = dbContext.TC_PBBG_TBQY.Where(t => t.FLinkId == FId).Select(t => new
        {
            t.QYMC,
            t.LXDH,
            t.TBTime,
            t.TBJ,
            t.PBJ,
            t.FId
        }).ToList();
        PagerTB.RecordCount = App2.Count();
        dg_ListTB.DataSource = App2.Skip((PagerTB.CurrentPageIndex - 1) * PagerTB.PageSize).Take(PagerTB.PageSize);
        dg_ListTB.DataBind();
        PagerTB.Visible = (PagerTB.RecordCount > PagerTB.PageSize);
    }

    private void ShowFileFB(string FId)
    {
        dbContext = new EgovaDB();
        var App3 = dbContext.TC_PBBG_FBYY.Where(t => t.FLinkId == FId).Select(t => new
        {
            t.TBR,
            t.FBLY,
            t.Remarks,
            t.FId
        }).ToList();
        PagerFB.RecordCount = App3.Count();
        dg_ListFB.DataSource = App3.Skip((PagerFB.CurrentPageIndex - 1) * PagerFB.PageSize).Take(PagerFB.PageSize);
        dg_ListFB.DataBind();
        PagerFB.Visible = (PagerFB.RecordCount > PagerFB.PageSize);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string fId = txtFId.Value;
        TC_PBBG_Record qa = new TC_PBBG_Record();
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(fId))
        {
            qa = dbContext.TC_PBBG_Record.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {

        }
        qa = tool.getPageValue(qa);
        dbContext.SubmitChanges();
        txtFId.Value = fId;
        //ShowTitle();
        ScriptManager.RegisterStartupScript(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
    }
    protected void btnReload_ClickBM(object sender, EventArgs e)
    {
        ShowFileBM(txtFId.Value);
    }
    protected void btnReload_ClickTB(object sender, EventArgs e)
    {
        ShowFileTB(txtFId.Value);
    }
    protected void btnReload_ClickFB(object sender, EventArgs e)
    {
        ShowFileFB(txtFId.Value);
    }
    protected void btnReload_ClickHXR(object sender, EventArgs e)
    {
        ShowFileHXR(txtFId.Value);
    }
    protected void App_List_ItemDataBoundBM(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.PagerBM.PageSize * (this.PagerBM.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            //e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('BMInfo.aspx?fid=" + fid + "',900,700,btnReload);\">" + e.Item.Cells[2].Text + "</a>";
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('BMInfo.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void App_List_ItemDataBoundTB(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.PagerTB.PageSize * (this.PagerTB.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            //e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('TBInfo.aspx?fid=" + fid + "',900,700,Button3);\">" + e.Item.Cells[2].Text + "</a>";
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('TBInfo.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void App_List_ItemDataBoundFB(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.PagerFB.PageSize * (this.PagerFB.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            //e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('FBInfo.aspx?fid=" + fid + "',900,700,Button9);\">" + e.Item.Cells[2].Text + "</a>";
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('FBInfo.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void App_List_ItemDataBoundHXR(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.PagerHXR.PageSize * (this.PagerHXR.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            //e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('HXRInfo.aspx?fid=" + fid + "',900,700,Button6);\">" + e.Item.Cells[2].Text + "</a>";
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('HXRInfo.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
            //如果是审核部门，则可以看到项目负责人的证书编号是否已经过期
            string  hdryid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "RYId"));
            Label lb = e.Item.FindControl("BDId") as Label;
            if (ispast(hdryid))
            {
               e.Item.Cells[6].Text = "已过期";
               e.Item.Cells[6].ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                e.Item.Cells[6].Text = "未过期";
            }          
        }
    }
    protected void btnDel_ClickBM(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_ListBM, dbContext.TC_PBBG_BMQY, tool_Deleting);
        ShowFileBM(txtFId.Value);
    }
    protected void btnDel_ClickTB(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_ListTB, dbContext.TC_PBBG_TBQY, tool_Deleting);
        ShowFileTB(txtFId.Value);
    }

    //中标选候人
    protected void btnDel_ClickHXR(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_ListHXR, dbContext.TC_PBBG_ZBHXR, tool_Deleting);
        ShowFileHXR(txtFId.Value);
    }
    protected void btnDel_ClickFB(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_ListFB, dbContext.TC_PBBG_FBYY, tool_Deleting);
        ShowFileFB(txtFId.Value);
    }
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
    }
    protected void Pager1_PageChangingBM(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerBM.CurrentPageIndex = e.NewPageIndex;
        ShowFileBM(txtFId.Value);
    }
    protected void Pager1_PageChangingTB(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerTB.CurrentPageIndex = e.NewPageIndex;
        ShowFileTB(txtFId.Value);
    }
    protected void Pager1_PageChangingFB(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerFB.CurrentPageIndex = e.NewPageIndex;
        ShowFileFB(txtFId.Value);
    }
    protected void Pager1_PageChangingHXR(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerHXR.CurrentPageIndex = e.NewPageIndex;
        ShowFileHXR(txtFId.Value);
    }
    protected void dg_ListHXR_ItemCommand(object source, DataGridCommandEventArgs e)
    {
       
    }

    /// <summary>
    /// 根据人员编号判断人员证书是否已经过期
    /// </summary>
    /// <param name="rybh">人员编号</param>
    /// <returns></returns>
    private  bool ispast(string rybh)
    { 
        EgovaDB1 db = new EgovaDB1();
        var v = from a in db.RY_RYJBXX
                join c in db.RY_RYZSXX
                on a.RYBH equals c.RYBH
                where a.RYBH == rybh
                select new
                {   
                    c.ZSYXQJSSJ                   
                };
        if (v != null)
        {
            try
            {
                DateTime jssj = Convert.ToDateTime(v.SingleOrDefault().ZSYXQJSSJ);
                if (jssj < DateTime.Now)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        
        return false;
    }
}