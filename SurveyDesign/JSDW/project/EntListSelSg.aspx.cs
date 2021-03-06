﻿/*
 复制了一个entlistsel.aspx页面，专门用于选择施工、专业承包、劳务三类单位
 * 原来的entlist.sel.aspx用于选择勘察、设计、监理单位。
 * date:20150319
 * modify by psq
 */
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
using System.Data;
using Approve.EntityBase;
using System.Collections;
using EgovaDAO;

public partial class JSDW_project_EntListSelSg: System.Web.UI.Page
{
    EgovaDB1 db1 = new EgovaDB1();
    RCenter rc = new RCenter();
    ShareTool st = new ShareTool();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["qylx"]))
            {
                string qylx = EConvert.ToString(Request.QueryString["qylx"]);
                ViewState["qylx"] = qylx;
                if (!string.IsNullOrEmpty(Request.QueryString["axz"]))
                {
                    showInfo(qylx, "axz");
                }
                else
                { showInfo(qylx); }                
            }
            else
            {
                showInfo();
            }
            
            BindControl();
            
        }
    }
    void BindControl()
    {
        int iMType = EConvert.ToInt(Request.QueryString["fsysId"]);
        if (iMType > 0)
        {
         //   lTitle.Text = db.CF_Sys_SystemName.Where(t => t.FNumber == iMType).Select(t => t.FName).FirstOrDefault();
        }
    }
    string GetQYLX()
    {
        string sys = Request.QueryString["fsysId"];


        return sys;
    }

    //显示 
    void showInfo(string qylx,string axz)
    {
        EgovaDB1 db = new EgovaDB1();
        var App = from b in db.QY_JBXX
                  join c in db.QY_QYZSXX
                  on b.QYBM equals c.QYBM
                  where b.QYLXBM == "150" 
                  select new
                  {
                      b.QYBM,
                      b.QYMC,
                      b.QYLXBM,
                      //b.RegAdrProvinceName,
                      RegAdrProvinceName = b.RegAdrProvinceName + "-" + b.RegAdrCityName + "-" + b.RegAdrCountryName,
                      b.QYXXDZ,
                      b.FRDB,
                      b.LXR,
                      b.LXDH,
                      ZSBH = c.ZSBH,
                      ZZMC = c.ZSLXMC,
                      AXBH = c.ZSBH                      
                  };
        if (!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
        {
            App = App.Where(t => t.QYMC.Contains(this.t_FName.Text.Trim()));
        }
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }

    
    //显示 
    void showInfo(string qylx)
    {
        EgovaDB1 db = new EgovaDB1();
        
       
        var App = from b in db.QY_JBXX
                      join c in db.QY_QYZZXX
                      on b.QYBM equals c.QYBM
                      into temp
                      join d in db.QY_QYZSXX
                      on b.QYBM equals d.QYBM
                      into temp1
                      from tt in temp.DefaultIfEmpty()
                      from tt1 in temp1.DefaultIfEmpty()
                      where  tt.SFZX == 1 //&&b.QYLXBM == qylx 

                      select new
                      {
                          b.QYBM,
                          b.QYMC,
                          b.QYLXBM,
                          //b.RegAdrProvinceName,
                          RegAdrProvinceName = b.RegAdrProvinceName + "-" + b.RegAdrCityName + "-" + b.RegAdrCountryName,
                          b.QYXXDZ,
                          b.FRDB,
                          b.LXR,
                          b.LXDH,
                          ZSBH = tt == null ? "" : tt.ZSBH,
                          ZZMC = tt == null ? "" : tt.ZZLB + tt.ZZMC + tt.ZZDJ,
                          //AXBH = tt1==null?"":tt1.ZSBH,
                          AXBH = tt.QY_QYZSXX.ZSLXBM
                          //AXBH = tt1.ZSLXBM == "2" ? tt.QY_QYZSXX.ZSBH : tt1.ZSLXBM//证书类型为2的是安全许可证
                      };
               if (!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
        {
            App = App.Where(t => t.QYMC.Contains(this.t_FName.Text.Trim()));
        }
        //修改企业类型条件(包括入川的对应企业)
        if (qylx == "101")
        {
            App = App.Where(t => t.QYLXBM == "101" || t.QYLXBM =="201");
        }
        else if(qylx == "102")
        {
            App = App.Where(t => t.QYLXBM == "102" || t.QYLXBM == "202");
        }
        else if (qylx == "103")
        {
            App = App.Where(t => t.QYLXBM == "103" || t.QYLXBM == "203");
        }
        else if (qylx == "104")
        {
            App = App.Where(t => t.QYLXBM == "104" || t.QYLXBM == "204");
        }
        else if (qylx == "105")
        {
            App = App.Where(t => t.QYLXBM == "105" || t.QYLXBM == "205");
        }
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }
    //显示 
    void showInfo()
    {
        EgovaDB1 db = new EgovaDB1();
        var App = from b in db.QY_JBXX
                  join c in db.QY_QYZZXX
                  on b.QYBM equals c.QYBM into temp
                  join d in db.QY_QYZSXX
                  on b.QYBM equals d.QYBM
                  into temp1
                  from tt in temp.DefaultIfEmpty()
                  from tt1 in temp1.DefaultIfEmpty()
                  where  tt1.ZSLXBM == "150"
                  
                  select new
                  {
                      b.QYBM,
                      b.QYMC,
                      b.QYLXBM,
                      //b.RegAdrProvinceName,
                      RegAdrProvinceName = b.RegAdrProvinceName + "-" + b.RegAdrCityName + "-" + b.RegAdrCountryName,
                      b.QYXXDZ,
                      b.FRDB,
                      b.LXR,
                      b.LXDH,
                      ZSBH = tt == null ? "" : tt.ZSBH,
                      ZZMC = tt == null ? "" : tt.ZZLB + tt.ZZMC + tt.ZZDJ,
                      //AXBH = tt1 == null ? "" : tt1.ZSBH
                      AXBH = tt.QY_QYZSXX.ZSLXBM == "2" ? tt.QY_QYZSXX.ZSBH : tt.QY_QYZSXX.ZSLXBM//证书类型为2的是安全许可证

                  };
        if (!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
        {
            App = App.Where(t => t.QYMC.Contains(this.t_FName.Text.Trim()));
        }
       
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }
 protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        if (!string.IsNullOrEmpty(EConvert.ToString(ViewState["qylx"])))
        {
            string qylx = EConvert.ToString(ViewState["qylx"]);
            if (!string.IsNullOrEmpty(Request.QueryString["axz"]))
            {
                showInfo(qylx, "axz");
            }
            else
            {
                showInfo(qylx);
            }
        }else {
            showInfo();
        }
        
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(EConvert.ToString(ViewState["qylx"])))
        {           
            string qylx = EConvert.ToString(ViewState["qylx"]);
            if (!string.IsNullOrEmpty(Request.QueryString["axz"]))
            {
                showInfo(qylx, "axz");
            }
            else
            {
                showInfo(qylx);
            }
        }
        else
        {
            showInfo();
        }
    }

    protected void dg_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //MODIFY YTB 新需求，要求取消安许证编号
            //    //重新获取企业的安许证号
            //    HiddenField hfFBaseInfoId = e.Item.FindControl("hfFBaseInfoId") as HiddenField;

            //    string qybm = hfFBaseInfoId.Value;
            //    string axbh = "";
            //    var v  = db1.QY_QYZSXX.Where(t => t.QYBM == qybm && t.ZSLXBM == "2").FirstOrDefault();
            //    if (v != null)
            //    {
            //        axbh = v.ZSBH;
            //        Label lbl = e.Item.FindControl("AXBH") as Label;
            //        lbl.Text = axbh;
            //    }

            LinkButton lb = e.Item.FindControl("btnSelect") as LinkButton;
            lb.Text = "选择";
            lb.Attributes.Add("onclick", "return confirm('确认要选择该企业吗?');");
        }
    }

    protected void dg_List_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
         if (e.Item.ItemIndex > -1)
        {
            if (e.CommandName == "Sel")
            {
                HiddenField hfFBaseInfoId = e.Item.FindControl("hfFBaseInfoId") as HiddenField;
                string fid = hfFBaseInfoId.Value;
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
               // tool.ExecuteScript("window.returnValue='" + fid + "|" + fCertiId + "';window.close();");
            }
        }
    }


}