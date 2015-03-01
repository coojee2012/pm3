using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Text;
using Approve.RuleCenter;
public partial class JSDW_appmain_PrjKCSJSel : System.Web.UI.Page
{
    //---------------------------------------------------------------------
    //本页面主要用于 “勘察文件审查”或“设计文件审查”选择工程。
    //工程来源是从已完成“项目勘察”或“设计文件编制”的业务，已做了审查的不显示，二次审查时是从列表中做的。
    //---------------------------------------------------------------------


    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }
    //显示 
    void showInfo()
    {
        //当前要申请的业务
        string thisFManageTypeId = Request.QueryString["FManageTypeId"];
        //当前企业ID
        string FBaseinfoId = CurrentEntUser.EntId;

        var v = from p in db.CF_Prj_BaseInfo
                join a in db.CF_App_List on p.FId equals a.FPrjId
                where p.FBaseinfoId == FBaseinfoId
                select new { p.FId, p.FPrjName, p.FPrjNo, a.FState, p.FType, a.FManageTypeId, a.FLinkId, a.FBaseName, a.FReportDate };

        if (thisFManageTypeId == "287") //勘察文件审查合同备案
        {
            //已完成“勘察文件成果移交284”和“见证报告备案28003”，但还没做过“勘察文件审查合同备案”的
            v = v.Where(t => t.FManageTypeId == 284 && t.FState == 6//(成果移交完)
                        && db.CF_Prj_Data.Count(b => b.FLinkId == t.FLinkId && b.FType == 287) == 0//(还没做过审查)
                        && (from l in db.CF_App_List //见证受理28001、见证人员安排28002、勘察项目见证28004、见证报告备案28003
                            where l.FLinkId == t.FLinkId && (l.FManageTypeId == 28001 || l.FManageTypeId == 28002 || l.FManageTypeId == 28004 || l.FManageTypeId == 28003)
                            && l.FState != 6
                            select l).Count() == 0//(该见证的见证完)
                        );
        }
        else if (thisFManageTypeId == "300")//施工图设计文件审查合同备案
        {
            //已完成“	施工图设计文件编制成果移交298”，但还没做过“施工图设计文件审查合同备案”的
            v = v.Where(t => t.FManageTypeId == 298 && t.FState == 6 //(成果移交完)
                        && db.CF_Prj_Data.Count(b => b.FLinkId == t.FLinkId && b.FType == 300) == 0);//(还没做过审查)

            dg_List.Columns[3].HeaderText = "设计单位";
        }
        else
        {
            v = v.Where(t => 1 == 2);
        }

        Pager1.RecordCount = v.Count();
        dg_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }

    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FType"));
            if (!string.IsNullOrEmpty(fType))
                fType = db.getDicName(fType);
            e.Item.Cells[2].Text = fType;
            LinkButton lb = e.Item.FindControl("btnLink") as LinkButton;
            lb.Text = "选择";
            lb.Attributes.Add("onclick", "return confirm('确认要选择该项目吗?');");



            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Where(t => t.FPrjId == EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"))).FirstOrDefault();
            if (stop != null)
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;";
                e.Item.ToolTip = "该项目已被中止，所有业务停止进行。";
                lb.Attributes["onclick"] = "alert('该项目已被中止，所有业务停止进行。');return false; ";
            }
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Sel")
        {
            string FLinkId = e.CommandArgument.ToString();
            pageTool tool = new pageTool(this.Page);
            tool.ExecuteScript("window.returnValue='" + FLinkId + "';window.close();");
        }

    }
}
