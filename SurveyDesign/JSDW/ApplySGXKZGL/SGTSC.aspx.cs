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

public partial class JSDW_ApplySGXKZGL_SGTSC : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string appid = "";
            if (!string.IsNullOrEmpty(EConvert.ToString(Session["FAppId"])))
            {
                hf_FAppId.Value  = EConvert.ToString(Session["FAppId"]);
                appid = EConvert.ToString(Session["FAppId"]);
            }

            ShowTitle();

            bool isbz = false;
            XMHJCL_Business business = new XMHJCL_Business();
            DataTable dt = business.QueryData(appid, XMHJCL_Business.环节材料信息.施工图审查, out isbz);

            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0 || isbz)
            {
                ListItem i = new ListItem("已办", "3");
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
    void BindControl()
    {
    }
    //显示
    private void ShowTitle()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_SGXKZ_PrjInfo qa = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == EConvert.ToString(hf_FAppId.Value)).FirstOrDefault();
        t_FPrjItemId.Value  = qa.FPrjItemId;
        //t_JSDW.Text = qa.JSDW;
        //t_ProjectName.Text = qa.ProjectName;
    }

    private void ShowInfo(bool isyw, DataTable sp)
    {
        if (sp.Rows.Count > 0)
        {
            string strfid = sp.Rows[0]["FId"].ToString();
            string strbl = sp.Rows[0]["bl"].ToString();
            txtFId.Value = strfid;
            ShowPrjItemInfo();

            if (string.IsNullOrEmpty(strbl))
            { strbl = "1"; }
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

    //保存
    private void saveInfo()
    {
        string fId = txtFId.Value;
        TC_SGXKZ_SGTSC Emp = new TC_SGXKZ_SGTSC();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_SGXKZ_SGTSC.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FAppId = hf_FAppId.Value;
            Emp.FprjItemId = t_FPrjItemId.Value; 
            dbContext.TC_SGXKZ_SGTSC.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        txtFId.Value = fId;
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');", true);
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    /// <summary>
    /// 显示施工图审查人员信息
    /// </summary>
    void ShowPrjItemInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        var App = dbContext.TC_SGXKZ_SGTSCRY.Where(t => t.FSGTSCId == txtFId.Value).Select(t => new
        {
            t.RYXM,
            t.DWMC,
            t.DWZZJGDM,
            t.FId
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
        tool.DelInfoFromGrid(dg_List, dbContext.TC_SGXKZ_SGTSCRY, tool_Deleting);
        ShowPrjItemInfo();
        //showInfo();
    }

    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {

    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowPrjItemInfo();
        //showInfo();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('SGTSCRY.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowPrjItemInfo();
    }
    protected void btnAddEntSC_Click(object sender, EventArgs e)
    {
        selEnt("SC");
    }
    protected void btnAddEntKC_Click(object sender, EventArgs e)
    {
        selEnt("KC");
    }
    protected void btnAddEntSJ_Click(object sender, EventArgs e)
    {
        selEnt("SJ");
    }
    private void selEnt(string type)
    {
        if (type == "SC")
        {
            string selEntId = t_SGTSCJGId.Value;
            EgovaDB1 db = new EgovaDB1();
            var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
            t_SGTSCJGMC.Text = v.QYMC;
            t_SGTSCZZJGDM.Text = v.JGDM;
        }
        else if (type == "KC")
        {
            string selEntId = t_KCDWId.Value;
            EgovaDB1 db = new EgovaDB1();
            var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
            t_KCDWMC.Text = v.QYMC;
            t_KCDWZZJGDM.Text = v.JGDM;
        }
        else if (type == "SJ")
        {
            string selEntId = t_SJDWId.Value;
            EgovaDB1 db = new EgovaDB1();
            var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
            t_SJDWMC.Text = v.QYMC;
            t_SJDWZZJGDM.Text = v.JGDM;
        }
        if (!string.IsNullOrEmpty(t_BL.SelectedValue))
        {
            if (t_BL.SelectedValue != "1")
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
}