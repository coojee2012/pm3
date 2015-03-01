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
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using System.Drawing;
using System.Linq;
using EgovaDAO;

public partial class JSDW_ApplyAQJDBA_FileList : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowInfo();
            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool.ExecuteScript("btnEnable();");
            }
        }
    }

    static string FAppId;

    private void ShowInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        //当前业务类型
        FAppId = EConvert.ToString(Session["FAppId"]);

        var v = from t in dbContext.CF_Sys_PrjList
                orderby t.FId
                where t.FManageType == 11222
                select new
                {
                    t.FId,
                    t.FFileName,
                    FFileCount = t.FFileAmount,
                    AppFile = dbContext.TC_QA_File.Where(f => t.FId == f.FMaterialTypeId && f.FAppId == FAppId)
                };
        Pager1.RecordCount = v.Count();
        rep_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        rep_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页时隐藏分页控件


    }

    //一层列表
    protected void rep_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string FId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            string FFileName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFileName"));
            IQueryable<TC_QA_File> AppFile = DataBinder.Eval(e.Item.DataItem, "AppFile") as IQueryable<TC_QA_File>;
            if (AppFile != null && AppFile.Count() > 0)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = AppFile.Count().ToString();
                ((Literal)e.Item.FindControl("lit_Has")).Text = "<font color='green'>是</font>";

                Repeater rep_File = (Repeater)e.Item.FindControl("rep_File");
                rep_File.DataSource = AppFile;
                rep_File.DataBind();
            }
            else
            {
                ((Literal)e.Item.FindControl("lit_Has")).Text = "<font color='red'>否</font><input type=\"button\" class=\"m_btn_w4\" value=\"上传文件\" onclick='showAddWindow(\"FileUp.aspx?FMaterialTypeId=" + FId + "&&FFileName=" + FFileName + "\",500,250);' />";
            }
        }
    }
    //二层列表 事件
    protected void rep_File_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "cnDel")
        {
            string FID = e.CommandArgument.ToString();
            EgovaDB dbContext = new EgovaDB();

            TC_QA_File v = dbContext.TC_QA_File.Where(t => t.FId == FID).FirstOrDefault();
            if (v != null)
            {
                dbContext.TC_QA_File.DeleteOnSubmit(v);
                dbContext.SubmitChanges();

                pageTool tool = new pageTool(this.Page);
                tool.showMessage("删除成功");
                ShowInfo();
            }
        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowInfo();
    }


    //刷新按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
