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
using ProjectData;

public partial class KC_AppMain_FileList : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowInfo();
        }
    }

    static int FManageTypeId;
    static string FAppId;
    static int FIsApprove;

    private void ShowInfo()
    {
        ProjectDB db = new ProjectDB();
        //当前业务类型
        FManageTypeId = EConvert.ToInt(Session["FManageTypeId"]);
        FAppId = EConvert.ToString(Session["FAppId"]);
        FIsApprove = EConvert.ToInt(Session["FIsApprove"]);

        //判断如果是Get过来的FAppId。
        if (!string.IsNullOrEmpty(Request.QueryString["FAppId"]))
        {
            var app = (from t in db.CF_App_List
                       where t.FId == Request.QueryString["FAppId"]
                       select new { t.FId, t.FManageTypeId, t.FState }).FirstOrDefault();
            if (app != null)
            {
                FAppId = app.FId;
                FManageTypeId = app.FManageTypeId.Value;
                if (app.FState > 1 && app.FState != 2)
                {
                    FIsApprove = 1;
                }
            }
        }

        var v = from t in db.CF_Sys_PrjList
                where t.FManageType == FManageTypeId
                orderby t.FOrder
                select new
                {
                    t.FId,
                    t.FFileName,
                    t.FFileAmount,
                    t.FRemark,
                    t.FOrder,
                    AppFile = db.CF_AppPrj_FileOther.Where(f => t.FId == f.FPrjFileId && f.FAppId == FAppId)
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
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            IQueryable<CF_AppPrj_FileOther> AppFile = DataBinder.Eval(e.Item.DataItem, "AppFile") as IQueryable<CF_AppPrj_FileOther>;
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
                ((Literal)e.Item.FindControl("lit_Has")).Text = "<font color='red'>否</font>";
            }

            if (FIsApprove == 0)
            {
                ((Literal)e.Item.FindControl("lit_Has")).Text += "<input type=\"button\" class=\"m_btn_w4\" value=\"上传文件\" onclick='showAddWindow(\"FileUp.aspx?FPrjFileId=" + FID + "\",500,250);' />";
            }
        }
    }
    //二层列表
    protected void rep_File_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (FIsApprove != 0)
            {
                ((LinkButton)e.Item.FindControl("btnDel")).Visible = false;
            }
        }
    }
    //二层列表 事件
    protected void rep_File_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "cnDel")
        {
            string FID = e.CommandArgument.ToString();
            ProjectDB db = new ProjectDB();

            CF_AppPrj_FileOther v = db.CF_AppPrj_FileOther.Where(t => t.FId == FID).FirstOrDefault();
            if (v != null)
            {
                db.CF_AppPrj_FileOther.DeleteOnSubmit(v);
                db.SubmitChanges();

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
