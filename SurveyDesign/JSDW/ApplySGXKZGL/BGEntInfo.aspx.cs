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

public partial class JSDW_ApplySGXKZGL_EntInfoForBG : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            t_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            t_FPrjItemId.Value = EConvert.ToString(Session["FPrjItemId"]);
            t_FEntType.Value = EConvert.ToString(Request["FEntType"]);
            txtFId.Value = EConvert.ToString(Request["FId"]);
            //t_FPrjId.Value = EConvert.ToString(Request["FPrjId"]);
            //t_FPrjItemId.Value = EConvert.ToString(Request["FPrjItemId"]);
            showTitle();
            showInfo();
            showEmpList();
            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {

                tool.ExecuteScript("btnEnable();");
            }

        }
    }
    //显示
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_PrjItem_Ent ent = null;
        if (t_FEntType.Value == "2")
        {
            ent = dbContext.TC_PrjItem_Ent.Where(t => t.FPrjItemId == t_FPrjItemId.Value && t.FEntType.Equals(t_FEntType.Value)).FirstOrDefault();
            
        }
        else
        {
            ent = dbContext.TC_PrjItem_Ent.Where(t => t.FId == txtFId.Value).FirstOrDefault();
        }

        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(ent);
            txtFId.Value = ent.FId;
        }
        

    }
    //显示
    private void showTitle()
    {
        switch (t_FEntType.Value)
        {
            case "2":
                lblTitle.InnerText = "施工总承包单位";
                break;
            case "3":
                lblTitle.InnerText = "专业承包单位";
                break;
            case "4":
                lblTitle.InnerText = "劳务分包单位";
                break;
            case "5":
                lblTitle.InnerText = "勘察单位";
                break;
            case "6":
                lblTitle.InnerText = "设计单位";
                break;
            case "7":
                lblTitle.InnerText = "监理单位";
                break;
        }


    }
    private string getEmpType(string id)
    {
        switch (id)
        {
            default:
                return "项目经理";
            case "1":
                return "项目经理";
            case "2":
                return "项目技术负责人";
            case "3":
                return "安全负责人";
            case "4":
                return "施工员";
            case "5":
                return "质量员";
            case "6":
                return "专职安全员";
            case "7":
                return "材料员";
            case "8":
                return "预算员";
            case "9":
                return "总监理工程师";
            case "10":
                return "专业监理工程师";
            case "11":
                return "监理员";
            case "12":
                return "其他";
        }
    }
    private void showEmpList()
    {
        if (t_FEntType.Value == "2" || t_FEntType.Value == "3" || t_FEntType.Value == "4")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr1();</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showTr2", "<script>showTr2();</script>");
        }
        EgovaDB dbContext = new EgovaDB();
        var v = dbContext.TC_PrjItem_Emp.Where(t => t.FPrjItemId == t_FPrjItemId.Value && t.FEntId == hf_FId.Value);
        dg_List.DataSource = v;
        dg_List.DataBind();
    }
    //保存
    private void saveInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        string fId = txtFId.Value;
        string fOldId = fId;
        TC_PrjItem_Ent ent = new TC_PrjItem_Ent();
        if (!string.IsNullOrEmpty(fId))
        {
            ent = dbContext.TC_PrjItem_Ent.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            ent.FId = fId;
            ent.FAppId = t_FAppId.Value;
            ent.FPrjItemId = t_FPrjItemId.Value;
            ent.FEntType = EConvert.ToInt(t_FEntType.Value);
            ent.FTime = DateTime.Now;
            ent.FCreateTime = DateTime.Now;
            dbContext.TC_PrjItem_Ent.InsertOnSubmit(ent);
        }
        pageTool tool = new pageTool(this.Page);
        ent = tool.getPageValue(ent);
        dbContext.SubmitChanges();
        hf_FId.Value = fId;
		txtFId.Value = fId;
        if (string.IsNullOrEmpty(fOldId))
        {
            updateYQBG(fId);
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
    }

    private void updateYQBG(string FId)
    {
        EgovaDB dbContext = new EgovaDB();
        TC_PrjItem_Ent ent = dbContext.TC_PrjItem_Ent.Where(t => t.FId == FId).FirstOrDefault();
        TC_SGXKZ_QYBGJG sq = new TC_SGXKZ_QYBGJG();

        sq.FId = Guid.NewGuid().ToString();
        sq.FAppId = this.t_FAppId.Value;
        sq.FPrjItemId = t_FPrjItemId.Value;
        sq.YQLX = lblTitle.InnerText;
        sq.YQMC = ent.FName;
        sq.BGTime = DateTime.Now;
        sq.BGQK = "增加";
        dbContext.TC_SGXKZ_QYBGJG.InsertOnSubmit(sq);
        dbContext.SubmitChanges();
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
        tool.DelInfoFromGrid(dg_List, dbContext.TC_PrjItem_Emp, tool_Deleting);
        showEmpList();
    }

    //删除
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        EgovaDB dbContext = new EgovaDB();
        if (dbContext != null)
        {
            var para = dbContext.TC_PrjItem_Emp.Where(t => FIdList.ToArray().Contains(t.FId));
            foreach (TC_PrjItem_Emp pe in para)
            {
                TC_SGXKZ_RYBGJG sr = new TC_SGXKZ_RYBGJG();
                sr.FId = Guid.NewGuid().ToString();
                sr.FAppId = t_FAppId.Value;
                sr.FPrjItemId = t_FPrjItemId.Value;
                sr.RYLX = getEmpType(pe.EmpType.ToString());
                sr.XM = pe.FHumanName;
                sr.ZSBH = pe.ZSBH;
                sr.QYMC = pe.FEntName;
                sr.BGQK = "退出";
                sr.BGTime = DateTime.Now;
                dbContext.TC_SGXKZ_RYBGJG.InsertOnSubmit(sr);
                dbContext.SubmitChanges();
            }
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showEmpList();
        
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string fAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string fEntId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEntId"));
            string fPrjItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjItemId"));
            string fPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('BGEmpInfo.aspx?FId=" + fId + "&fAppId=" + fAppId + "&fEntId=" + fEntId + "&fPrjItemId=" + fPrjItemId + "&fprjId=" + fPrjId + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;

    }
}