using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using EgovaDAO;
using Tools;
using EgovaBLL;
using System.Data;

public partial class JSDW_ApplyZBBA_ZGYSJGInfo : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                txtFId.Value = Request.QueryString["fid"];
                ShowTitle();
            }
            else
            {

                EgovaDB dbContext = new EgovaDB();
                var jsdw = dbContext.TC_YSJG_Record
                    .Where(t => t.FAppId == EConvert.ToString(Session["FAppId"]))
                    .FirstOrDefault();
                if (jsdw != null)
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.fillPageControl(jsdw);
                    ViewState["FID"] = jsdw.FId;
                    ViewState["FAppId"] = jsdw.FAppId;
                    ViewState["FPrjId"] = jsdw.FPrjId;
                    ViewState["BDId"] = jsdw.BDId;
                    txtFId.Value = jsdw.FId;
                    ShowFile(txtFId.Value);
                    showEnt(txtFId.Value);
                    //string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //MyPageTool.showMessage(s, this.Page);
                }
            }
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
        TC_YSJG_Record qa = dbContext.TC_YSJG_Record.Where(t => t.FId == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
        pageTool tool = new pageTool(this.Page);
        tool.fillPageControl(qa);
        txtFId.Value = qa.FId;
        Session["FAppId"] = qa.FAppId;
        ViewState["FAppId"] = qa.FAppId;
        ViewState["FPrjId"] = qa.FPrjId;
        ViewState["BDId"] = qa.BDId;
        ShowFile(txtFId.Value);
        showEnt(txtFId.Value);
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

    //显示 
    void showEnt(string FId)
    {
        EgovaDB db = new EgovaDB();
        var App = from b in db.TC_YSJG_QY
                  where b.FLinkId == FId
                  orderby b.QYId
                  select new
                  {
                      b.QYId,
                      b.QYMC,
                      b.QYLX,
                      b.FId
                  };
        AspNetPager1.RecordCount = App.Count();
        DataGrid1.DataSource = App.Skip((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize).Take(AspNetPager1.PageSize);
        DataGrid1.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string fId = txtFId.Value;
        TC_YSJG_Record qa = new TC_YSJG_Record();
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(fId))
        {
            qa = dbContext.TC_YSJG_Record.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {

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
    protected void btnDel1_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(DataGrid1, dbContext.TC_YSJG_QY, tool_Deleting1);
        showEnt(txtFId.Value);
    }
    private void tool_Deleting1(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        
    }
    protected void Pager2_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        showEnt(txtFId.Value);
    }
    protected void btnReload1_Click(object sender, EventArgs e)
    {
        showEnt(txtFId.Value);
    }
    protected void App_List_ItemDataBound1(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            
        }
    }
    protected void btnAddEnt_Click(object sender, EventArgs e)
    {
        seleEnt();
    }
    private void seleEnt()
    {
        string selEntId = h_selEntId.Value;
        EgovaDB1 db = new EgovaDB1();
        if (!string.IsNullOrEmpty(selEntId))
        {
            var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
            var q = dbContext.TC_YSJG_QY.Where(t => t.QYId == selEntId).FirstOrDefault();
            if (q == null)
            {
                TC_YSJG_QY qy = new TC_YSJG_QY();
                qy.FId = Guid.NewGuid().ToString();
                qy.FAppId = EConvert.ToString(ViewState["FAppId"]);
                qy.FPrjId = EConvert.ToString(ViewState["FPrjId"]);
                qy.BDId = EConvert.ToString(ViewState["BDId"]);
                qy.FLinkId = EConvert.ToString(ViewState["FID"]);
                qy.QYId = v.QYBM;
                qy.QYMC = v.QYMC;
                string qylx = "";
                switch (v.QYLXBM)
                {
                    case "101":
                        qylx = "施工企业";
                        break;
                    case "155":
                        qylx = "勘察企业";
                        break;
                    case "125":
                        qylx = "工程监理";
                        break;
                    case "120":
                        qylx = "招标代理";
                        break;
                    case "130":
                        qylx = "房地产开发";
                        break;
                    case "135":
                        qylx = "园林绿化";
                        break;
                    case "196":
                        qylx = "设计施工一体化";
                        break;
                    case "145":
                        qylx = "审图机构";
                        break;
                    case "175":
                        qylx = "检测机构";
                        break;
                    case "187":
                        qylx = "物业服务";
                        break;
                    case "186":
                        qylx = "房地产估价";
                        break;
                    case "202":
                        qylx = "规划编制";
                        break;
                    case "185":
                        qylx = "造价咨询";
                        break;
                }
                qy.QYLX = qylx;
                dbContext.TC_YSJG_QY.InsertOnSubmit(qy);
                dbContext.SubmitChanges();

            }
            showEnt(txtFId.Value);
        }
        
    }
}