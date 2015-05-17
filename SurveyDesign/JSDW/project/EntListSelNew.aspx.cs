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

public partial class JSDW_project_EntListSelNew : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    ShareTool st = new ShareTool();
    /// <summary>
    /// 1- 项目环节[施工图审查信息]:施工图审查机构查询
    /// 2- 筛选招标代理机构 （）
    /// 1- 项目环节[施工图审查信息]:施工图审查机构查询
    /// </summary>
    private static int queryType = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                queryType = EConvert.ToInt(Request.QueryString["type"]);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["qylx"]))
            {
                string qylx = EConvert.ToString(Request.QueryString["qylx"]);
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
        var App = from b in db.QY_JBXX
                  join c in db.QY_QYZZXX
                  on b.QYBM equals c.QYBM
                  into temp
                  join d in db.QY_QYZSXX
                  on b.QYBM equals d.QYBM
                  into temp1
                  from tt in temp.DefaultIfEmpty()
                  from tt1 in temp1.DefaultIfEmpty()
                  //where b.QYLXBM == qylx
                  select new
                  {
                      b.QYBM,
                      b.QYMC,
                      b.QYLXBM,
                      b.RegAdrProvinceName,
                      b.QYXXDZ,
                      b.FRDB,
                      b.LXR,
                      b.LXDH,
                      ZSBH = tt == null ? "" : tt.ZSBH,
                      ZZMC = tt == null ? "" : tt.ZZLB + tt.ZZMC + tt.ZZDJ,
                      AXBH = tt1 == null ? "" : tt1.ZSBH
                  };
        //修改企业类型条件
        if (qylx == "101")
        {
            App = App.Where(t => t.QYLXBM == "101" || t.QYLXBM == "201");
        }
        else if (qylx == "102")
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
       
        if (!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
        {
            App = App.Where(t => t.QYMC.Contains(this.t_FName.Text.Trim()));
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
                  select new
                  {
                      b.QYBM,
                      b.QYMC,
                      b.QYLXBM,
                      b.RegAdrProvinceName,
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
        }
        else
        {
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
                HiddenField hfFBaseInfoId = e.Item.FindControl("hfFBaseInfoId") as HiddenField;
                string fid = hfFBaseInfoId.Value;
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
            }
        }
    }

}