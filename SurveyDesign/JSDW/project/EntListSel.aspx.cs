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
using System.Data;
using Approve.EntityBase;
using System.Collections;
using EgovaDAO;

public partial class JSDW_project_EntListSel: System.Web.UI.Page
{
    RCenter rc = new RCenter();   
    //RCenter rctest = new RCenter("JST_XZSPBaseInfo");

    ShareTool st = new ShareTool();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["qylx"]))
            {
                //string sql = "select *  from  qy_jbxx";
                //DataTable dt = new DataTable();
                //dt = rctest.GetTable(sql);
                //int rowcount = dt.Rows.Count;

                string qylx = EConvert.ToString(Request.QueryString["qylx"]);
                if (Request.QueryString["iskc"]== null)
                { ViewState["iskc"] = "false";}
                else
                {   string iskc = EConvert.ToString(Request.QueryString["iskc"]);  //是否勘察，控制返回资质ID，否则企业ID
                    ViewState["iskc"] = iskc;
                }

                ViewState["qylx"] = qylx;
                showInfo(qylx);
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
    void showInfo(string qylx)
    {
        EgovaDB1 db = new EgovaDB1();
        //如果是勘察、设计、监理类企业则列出企业所有的资质及等级信息，没有主项的说法
        if ((qylx == "102") || (qylx == "103") || (qylx == "104"))
        {
            //var App = from b in db.QY_JBXX
            //          join c in db.QY_QYZZXX
            //          on b.QYBM equals c.QYBM
            //          into temp
            //          join d in db.QY_QYZSXX
            //          on b.QYBM equals d.QYBM
            //          into temp1
            //          from tt in temp.DefaultIfEmpty()
            //          from tt1 in temp1.DefaultIfEmpty()
            //          where b.QYLXBM == qylx

            //          select new
            //          {
            //              b.QYBM,
            //              b.QYMC,
            //              b.QYLXBM,
            //              //b.RegAdrProvinceName,
            //              RegAdrProvinceName = b.RegAdrProvinceName + "-" + b.RegAdrCityName + "-" + b.RegAdrCountryName,
            //              b.QYXXDZ,
            //              b.FRDB,
            //              b.LXR,
            //              b.LXDH,
            //              //ZSBH = tt == null ? "" : tt.ZSBH,
            //              ZSBH = tt == null ? "" : tt.QY_QYZSXX.ZSBH,
            //              ZZMC = tt == null ? "" : tt.ZZLB + tt.ZZMC + tt.ZZDJ,
            //              AXBH = tt1 == null ? "" : tt1.ZSBH
                  

            var App = from b in db.QY_JBXX
                      join c in db.QY_QYZZXX
                      on b.QYBM equals c.QYBM                     
                      //where b.QYLXBM == qylx

                      select new
                     { 
                          c.QYZZID,//企业资质id
                          b.QYBM,
                          b.QYMC,
                          b.QYLXBM,
                          //b.RegAdrProvinceName,
                          RegAdrProvinceName = b.RegAdrProvinceName + "-" + b.RegAdrCityName + "-" + b.RegAdrCountryName,
                          b.QYXXDZ,
                          b.FRDB,
                          b.LXR,
                          b.LXDH,
                          //ZSBH = tt == null ? "" : tt.ZSBH,
                          //ZSBH = tt == null ? "" : tt.QY_QYZSXX.ZSBH,
                          //ZZMC = tt == null ? "" : tt.ZZLB + tt.ZZMC + tt.ZZDJ,
                          //AXBH = tt1 == null ? "" : tt1.ZSBH
                          ZSBH = c.ZSBH,
                          ZZMC = c.ZZLB+c.ZZMC+c.ZZDJ,
                          AXBH = ""
                      };

            if (!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
            {
                App = App.Where(t => t.QYMC.Contains(this.t_FName.Text.Trim()));
            }
            //修改企业类型条件
            if (qylx == "102")
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
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
        else if(qylx == "105")//代理机构
        {
            var App = from b in db.QY_JBXX
                      join c in db.QY_QYZSXX
                      on b.QYBM equals c.QYBM
                      where b.QYLXBM == "105" || b.QYLXBM == "205"  //205是省外入川代理机构

                      select new
                      {
                          QYZZID="",//企业资质编号为空
                          b.QYBM,
                          b.QYMC,
                          b.QYLXBM,
                          //b.RegAdrProvinceName,
                          RegAdrProvinceName = b.RegAdrProvinceName + "-" + b.RegAdrCityName + "-" + b.RegAdrCountryName,
                          b.QYXXDZ,
                          b.FRDB,
                          b.LXR,
                          b.LXDH,
                          //ZSBH = tt == null ? "" : tt.ZSBH,
                          ZSBH = c.ZSBH,                          
                          ZZMC = c.ZSDJMC,
                          AXBH = ""                       
                      };
            if (!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
            {
                App = App.Where(t => t.QYMC.Contains(this.t_FName.Text.Trim()));
            }
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
        else if (qylx == "109")  //审图机构 目前为145.  modify by psq 20150319 审图机构没有资质信息，只有证书信息。
        {
            var App = from b in db.QY_JBXX
                      join d in db.QY_QYZSXX
                      on b.QYBM equals d.QYBM
                      into temp1
                      from tt1 in temp1.DefaultIfEmpty()
                      where b.QYLXBM == "109" || b.QYLXBM == "209" //209省外入川审图机构

                      select new
                      {
                          QYZZID = "",//企业资质编号为空
                          b.QYBM,
                          b.QYMC,
                          b.QYLXBM,
                          //b.RegAdrProvinceName,
                          RegAdrProvinceName = b.RegAdrProvinceName + "-" + b.RegAdrCityName + "-" + b.RegAdrCountryName,
                          b.QYXXDZ,
                          b.FRDB,
                          b.LXR,
                          b.LXDH,
                          ZSBH = tt1 == null ? "" : tt1.ZSBH,
                          ZZMC = tt1 == null ? "" : tt1.ZSLXMC,
                          AXBH = tt1 == null ? "" : tt1.ZSBH
                      };
            if (!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
            {
                App = App.Where(t => t.QYMC.Contains(this.t_FName.Text.Trim()));
            }
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
        else//101，则需要把省外入川施工类企业202类型的也加入，如果是施工、专业、劳务类企业则列出企业主项资质
        {
            var App = from b in db.QY_JBXX
                      join c in db.QY_QYZZXX
                      on b.QYBM equals c.QYBM
                      into temp
                      join d in db.QY_QYZSXX
                      on b.QYBM equals d.QYBM
                      into temp1
                      from tt in temp.DefaultIfEmpty()
                      from tt1 in temp1.DefaultIfEmpty()
                      where (b.QYLXBM == "101" || b.QYLXBM == "201") && tt.SFZX == 1

                      select new
                      {
                          tt.QYZZID,//企业资质id
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
                          AXBH = tt1 == null ? "" : tt1.ZSBH
                      };
            if (!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
            {
                App = App.Where(t => t.QYMC.Contains(this.t_FName.Text.Trim()));
            }
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }       
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
                  from tt1 in temp1.DefaultIfEmpty().Distinct()
                  where tt.SFZX == 1
                  //where  tt1.ZSLXBM == "150"
                  
                  select new
                  {
                      tt.QYZZID,////企业资质编号为空
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
                      AXBH = tt1 == null ? "" : tt1.ZSBH
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
            showInfo(qylx);
        }else {
            showInfo();
        }
        
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(EConvert.ToString(ViewState["qylx"])))
        {
            string qylx = EConvert.ToString(ViewState["qylx"]);
            showInfo(qylx);
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
                string fid = "";
                if (ViewState["iskc"].ToString().Trim() != "false")
                {
                    HiddenField hfzzxxid = e.Item.FindControl("hfqyzzid") as HiddenField;
                    fid = hfzzxxid.Value;
                }
                else
                {
                    HiddenField hfFBaseInfoId = e.Item.FindControl("hfFBaseInfoId") as HiddenField;
                    fid = hfFBaseInfoId.Value;
                }

                pageTool tool = new pageTool(this.Page);
                //如果是监理企业，返回的是QY_QYZZXX的主键QYZZID,以解决选择同一个企业不同的资质选择的问题。  modify by psq 20150421
                tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
            
               // tool.ExecuteScript("window.returnValue='" + fid + "|" + fCertiId + "';window.close();");
            }
        }
    }


};
