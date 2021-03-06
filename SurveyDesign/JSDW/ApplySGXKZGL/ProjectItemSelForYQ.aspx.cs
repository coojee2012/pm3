﻿using System;
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
using EgovaDAO;

public partial class JSDW_ApplySGXKZGL_ProjectItemSelForYQ : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    EgovaDB dbContext = new EgovaDB();
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
        //var App = from t in dbContext.TC_PrjItem_Info
        //        join a in dbContext.TC_SGXKZ_PrjInfo
        //        on t.FId equals a.FPrjItemId
        //        join b in dbContext.CF_App_List
        //        on a.FAppId equals b.FId
        //        where t.FJSDWID == CurrentEntUser.EntId && b.FState == 6
        //        orderby t.FId
        //        select new
        //        {
        //            t.PrjItemName,
        //            t.PrjItemType,
        //            t.FId
        //        };
        //if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
        //    App = App.Where(t => t.PrjItemName.Contains(t_FName.Text.Trim()));
        //以前是从业务库中选取项目，现在改为从归档库中获取项目信息  modify by psq 20150429,需要在TC_SGXKZ_PrjInfo和GD_TC_SGXKZ_PrjInfo表中增加jsdwid，用于识别是否是自己建立的业务。
        var App = from t in dbContext.GD_TC_SGXKZ_PrjInfo                                 
                  join b in dbContext.CF_App_List
                  on t.FAppId equals b.FId
                  where b.FBaseinfoId == CurrentEntUser.EntId && b.FState == 6     
                  orderby t.FId
                  select new
                  {
                      t.PrjItemName,
                      t.PrjItemType,
                      t.FId,
                      t.jsdwid,
                      t.FPrjItemId
                  };
        if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
            App = App.Where(t => t.PrjItemName.Contains(t_FName.Text.Trim()));

        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            try
            {
                String PrjItemType = e.Item.Cells[3].Text;
                e.Item.Cells[3].Text = dbContext.CF_Sys_Dic.Where(d => d.FNumber == Convert.ToInt32(PrjItemType)).Select(d => d.FName).FirstOrDefault();
            }
            catch
            { }
            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 3].Controls[0] as LinkButton;
            lb.Text = "选择";
            lb.Attributes.Add("onclick", "return confirm('确认要选择该项目吗?');");
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