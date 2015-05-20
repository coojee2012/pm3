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

public partial class JSDW_project_EntListSelsql: System.Web.UI.Page
{
    RCenter rc = new RCenter();  
    ShareTool st = new ShareTool();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            showInfo();

        }
    }

    void showInfo()
    {
        string sql = @"select  *  from  V_allent ";
               if(!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
               {
                   sql += "   where QYMC like '%"+this.t_FName.Text.Trim()+"%'";
               }
               this.pager1.className = "RCenter";
               this.pager1.sql = sql.ToString();
               this.pager1.pagecount = 20;
               this.pager1.controltopage = "dg_List";
               this.pager1.controltype = "Repeater";
               this.pager1.dataBind();      
    }
  
      
    protected void btnReload_Click(object sender, EventArgs e)
    {      
            showInfo();
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
                //如果是监理企业，返回的是QY_QYZZXX的主键QYZZID,以解决选择同一个企业不同的资质选择的问题。  modify by psq 20150421
                if (ViewState["qylx"] != null)
                {
                    if (ViewState["qylx"].ToString() == "104")
                    {
                        HiddenField hfzzxxid = e.Item.FindControl("hfqyzzid") as HiddenField;
                        string szzxxid = hfzzxxid.Value;
                        tool.ExecuteScript("window.returnValue='" + szzxxid + "';window.close();");
                    }
                    else
                    {
                        tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
                    }
                }
                else
                {
                    tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
                }               
            }
        }
    }


};
