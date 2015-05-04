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

public partial class JSDW_ApplySGXKZGL_Location : System.Web.UI.Page
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
            //ClientScript.RegisterStartupScript(this.GetType(), "hideTr1", "<script>hideTr1();</script>");
            
            ShowTitle();
            //MODIFY:YTB 判断选址意见书是否存在标准库中。
            bool b=  CheckXMBase();
            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0||b)
            {
                tool1.ExecuteScript("btnEnable();");
            }
        }
    }

    /// <summary>
    /// 验证标准库是否存在该项目的选址意见书
    /// </summary>
    private bool CheckXMBase() {
        //得到项目id
        var appid = ViewState["FAppId"].ToString(); 

        EgovaDB db = new EgovaDB();
       var prjid = string.Empty;
        var prjids = from location in db.TC_SGXKZ_Location
                join prjitem in db.TC_PrjItem_Info on location.FprjItemId equals prjitem.FId
                where location.FAppId == appid
                select new {
                    pid = prjitem.FPrjId
                };
        if (prjids != null&&prjids.Count()>0)
        {
            prjid = prjids.FirstOrDefault().pid;

        }
        //查询标准库，该项目是否存在选址意见书
        if (!string.IsNullOrEmpty(prjid)) {

            XMHJCL_Business business = new XMHJCL_Business();

            DataTable dt = business.GataBaseData(prjid, XMHJCL_Business.环节材料信息.选址意见书);
            //如果标准库中有，就设置为1
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
        }
        return false;
    }


    private void ShowTitle()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_SGXKZ_PrjInfo qa = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == EConvert.ToString(ViewState["FAppId"])).FirstOrDefault();
        ViewState["FPrjItemId"] = qa.FPrjItemId;
        t_JSDW.Text = qa.JSDW;
        t_ProjectName.Text = qa.ProjectName;
        TC_SGXKZ_Location sp = dbContext.TC_SGXKZ_Location.Where(t => t.FAppId == EConvert.ToString(ViewState["FAppId"])).FirstOrDefault();
        if (sp != null)
        {
            txtFId.Value = sp.FId;
            ShowFile(sp.FId);
            //if (!string.IsNullOrEmpty(sp.BL))
            //{
            //    if (sp.BL != "1")
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr1();</script>");
            //    }
            //    else
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "hideTr1", "<script>hideTr1();</script>");
            //    }
            //}
            
        }

        pageTool tool = new pageTool(this.Page, "t_");
        tool.fillPageControl(sp);
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
        TC_SGXKZ_Location qa = new TC_SGXKZ_Location();
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(fId))
        {
            qa = dbContext.TC_SGXKZ_Location.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            qa.FId = fId;
            qa.FprjItemId = EConvert.ToString(ViewState["FPrjItemId"]);
            qa.FAppId = EConvert.ToString(ViewState["FAppId"]);
            dbContext.TC_SGXKZ_Location.InsertOnSubmit(qa);
        }
        qa = tool.getPageValue(qa);
        dbContext.SubmitChanges();
        txtFId.Value = fId;
        ShowTitle();
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
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
}