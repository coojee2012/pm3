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

public partial class JSDW_ApplySGXKZGL_PrjItemDescForYQ : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hf_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            BindControl();
            showInfo();
            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool.ExecuteScript("btnEnable();");
            }
        }
    }
    void BindControl()
    {

        //结构类型
        DataTable dt = rc.getDicTbByFNumber("509");
        t_ConstrType.DataSource = dt;
        t_ConstrType.DataTextField = "FName";
        t_ConstrType.DataValueField = "FNumber";
        t_ConstrType.DataBind();
        t_ConstrType.Items.Insert(0, new ListItem("--请选择--", ""));

        //工程类别
        dt = rc.getDicTbByFNumber("20001");
        t_PrjItemType.DataSource = dt;
        t_PrjItemType.DataTextField = "FName";
        t_PrjItemType.DataValueField = "FNumber";
        t_PrjItemType.DataBind();

        //币种
        dt = rc.getDicTbByFNumber("112211");
        t_Currency.DataSource = dt;
        t_Currency.DataTextField = "FName";
        t_Currency.DataValueField = "FNumber";
        t_Currency.DataBind();


        //所有制
        dt = rc.getDicTbByFNumber("112212");
        t_JSDWXZ.DataSource = dt;
        t_JSDWXZ.DataTextField = "FName";
        t_JSDWXZ.DataValueField = "FNumber";
        t_JSDWXZ.DataBind();
    }
    //显示
    private void showInfo()
    {
        TC_SGXKZ_YQSQ yq = dbContext.TC_SGXKZ_YQSQ.Where(t => t.FAppId == hf_FAppId.Value).FirstOrDefault();
        if (yq != null)
        {
            pageTool tool1 = new pageTool(this.Page, "p_");
            tool1.fillPageControl(yq);
            TC_SGXKZ_PrjInfo emp = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FId == yq.FPrjInfoId).FirstOrDefault();
            TC_SGXKZ_BGPrjInfo empA = dbContext.TC_SGXKZ_BGPrjInfo.Where(t => t.FPrjItemId == emp.PrjItemId).OrderByDescending(q => q.BGTime).FirstOrDefault();
            if (emp != null)
            {
                pageTool tool = new pageTool(this.Page, "t_");
                tool.fillPageControl(emp);
                //显示变更后的数据
                if (empA != null)
                {
                    JSDW_DeptID.fNumber = empA.JSDWAddressDept;
                    t_PrjItemName.Text = empA.PrjItemName;
                    PrjGovdeptid.fNumber = empA.PrjAddressDept;
                    t_ConstrScale.Text = empA.ConstrScale;
                    t_StartDate.Text = empA.StartDate.HasValue ? empA.StartDate.Value.ToString("yyyy-MM-dd") : "";
                    t_EndDate.Text = empA.EndDate.HasValue ? empA.EndDate.Value.ToString("yyyy-MM-dd") : "";
                }


            }
            hf_FprjItemId.Value = yq.FprjItemId;
            hf_FId.Value = yq.FId;
            ShowPrjItemInfo(yq.FprjItemId);
        }
    }
    //保存
    private void saveInfo()
    {
        string fId = hf_FId.Value;
        t_JSDWAddressDept.Value = JSDW_DeptID.fNumber;
        t_PrjAddressDept.Value = PrjGovdeptid.fNumber;
        TC_SGXKZ_YQSQ Emp = new TC_SGXKZ_YQSQ();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_SGXKZ_YQSQ.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FAppId = hf_FAppId.Value;
            dbContext.TC_SGXKZ_YQSQ.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page, "p_");
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        hf_FId.Value = fId;
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');", true);
        //     MyPageTool.showMessageAjax("保存成功ii", up_Main);
        //    MyPageTool.showMessageAndRunFunctionAjax("保存成功", "window.returnValue='1';", up_Main);
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    /// <summary>
    /// 显示延期信息
    /// </summary>
    void ShowPrjItemInfo(string FprjItemId)
    {
        EgovaDB dbContext = new EgovaDB();
        var App = dbContext.TC_SGXKZ_YQSQ.Where(t => t.FprjItemId == FprjItemId).Select(t => new
        {
            t.YQCS,
            t.YQEndTime,
            t.FId,
            t.FZJG,
            t.FZTime
        }).ToList();
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowPrjItemInfo(hf_FprjItemId.Value);
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowPrjItemInfo(hf_FprjItemId.Value);
    }
}