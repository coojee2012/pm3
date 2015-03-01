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
public partial class JSDW_appmain_PrjRegistSel : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
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
        IQueryable<CF_Prj_BaseInfo> App = db.CF_Prj_BaseInfo.Where(t => t.FBaseinfoId == CurrentEntUser.EntId && t.FIsBG != 1).OrderByDescending(t => t.FCreateTime);
        //不在已经申请的业务里面
        int iMType = EConvert.ToInt(Request.QueryString["ftype"]);

        //办结的业务要包含该项目
        string strNeedType = Request.QueryString["needfType"];
        if (!string.IsNullOrEmpty(strNeedType))
        {
            string[] types = strNeedType.Split(',');
            foreach (string item in types)
            {
                int iNeedType = EConvert.ToInt(item);
                if (iNeedType > 0)
                {
                    if (iNeedType == 28001)//如果是见证
                    {
                        App = App.Where(t => ((db.CF_App_List
                            .Where(l => l.FManageTypeId == iNeedType
                                && l.FState == 6)
                            .Select(l => l.FPrjId)).Contains(t.FId) ||
                            !(db.CF_App_List
                            .Where(l => l.FManageTypeId == iNeedType)
                            .Select(l => l.FPrjId)).Contains(t.FId)));
                    }
                    else
                    {
                        App = App.Where(t => (db.CF_App_List.Where(l => l.FManageTypeId == iNeedType && l.FState == 6).Select(l => l.FPrjId)).Contains(t.FId));
                    }
                }
            }
        }
        if (iMType == 280)//勘察文件合同备案
            App = App.Where(t => !((db.CF_App_List.Where(l => l.FManageTypeId == iMType && l.FIsDeleted != true).Select(l => l.FPrjId)).Contains(t.FId)));
        else if (iMType > 0)
        {
            App = App.Where(t => !((db.CF_App_List.Where(l => l.FManageTypeId == iMType && l.FPrjId == t.FId).Select(l => l.FPrjId)).Contains(t.FId)));
        }
        //变更记录选项目，必须做过业务，但是没有正在‘办理中’的业务
        if (Request.QueryString["IsBG"] == "1")
        {
            //必须做过业务
            App = App.Where(t => db.CF_App_List.Count(l => l.FPrjId == t.FId) > 0);
            //必须有没有办理中的业务
            //勘察和见证退回或者不予办理
            App = App.Where(t => (from l in db.CF_App_List
                                  join m in db.CF_Sys_ManageType
                                      on l.FManageTypeId equals m.FNumber
                                  where l.FPrjId == t.FId && l.FIsDeleted == false
                                      && ((m.FMTypeId == 193 && l.FState < 3)

                                      || (m.FMTypeId != 193 && l.FState < 2))
                                  select
                                  new { l.FLinkId, l.FManageTypeId, m.FMTypeId })
                                  .Where(ll => (from l in db.CF_App_List
                                                join m in db.CF_Sys_ManageType
                                                on l.FManageTypeId equals m.FNumber
                                                where l.FLinkId == ll.FLinkId
                                                && l.FIsDeleted == false
                                            && m.FMTypeId != 193
                                            && (l.FState == 2 || l.FState == 7
                                            || l.FState == 3)
                                            || ((db.CF_App_List
                                            .Count(il => il.FId == l.FLinkAppId
                                                && il.FIsDeleted == true) > 0))
                                                select 1).Count() <= 0
                                             &&
                                            (from l2 in db.CF_App_List
                                             join d in db.CF_Prj_Data
                                             on l2.FId equals d.FAppId
                                             join l in db.CF_App_List
                                             on d.FPriItemId equals l.FLinkId
                                             join m in db.CF_Sys_ManageType
                                             on l.FManageTypeId equals m.FNumber
                                             where l2.FLinkId == ll.FLinkId
                                             && l.FIsDeleted == false
                                             && m.FMTypeId != 193
                                            && (l.FState == 2 || l.FState == 7
                                            || l.FState == 3)
                                             select 1).Count() <= 0)
                                            .Count() <= 0);
        }
        if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
            App = App.Where(t => t.FPrjName.Contains(t_FName.Text.Trim()));
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FType"));
            if (!string.IsNullOrEmpty(fType))
                fType = rc.GetDicName(fType);
            e.Item.Cells[4].Text = fType;
            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 2].Controls[0] as LinkButton;
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
        if (e.Item.ItemIndex > -1)
        {
            if (e.CommandName == "Sel")
            {
                string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
            }
        }
    }
}
