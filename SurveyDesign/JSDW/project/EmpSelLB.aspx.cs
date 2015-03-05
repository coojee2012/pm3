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

public partial class JSDW_project_EmpSelLB : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    ShareTool st = new ShareTool();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string ZW = EConvert.ToString(Request.QueryString["ZW"]);
            ViewState["RYLBBM"] = ZW;
            BindControl();
            showInfo(ZW);
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
    void showInfo(string ZW)
    {
        EgovaDB1 db = new EgovaDB1();
        var v = from a in db.QY_QYCYRYXX
                where a.ZW == ZW
                select new
                {
                    a.QYBM,
                    a.RYLBBM,
                    a.RYBH,
                    a.XM,
                    a.ZJBH,
                    a.XB,
                    a.ZCZSH,
                    a.ZCZYMC,
                    a.ZCZSYXQ,
                    a.ZCZSFZSJ
                };
        if (!string.IsNullOrEmpty(this.txtIDCard.Text.Trim()))
        {
            v = v.Where(t => t.ZJBH.Contains(this.txtIDCard.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
        {
            v = v.Where(t => t.XM.Contains(this.t_FName.Text.Trim()));
        }
        Pager1.RecordCount = v.Count();
        dg_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        string qybm = EConvert.ToString(ViewState["qybm"]);
        showInfo(qybm);
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        string qybm = EConvert.ToString(ViewState["qybm"]);
        showInfo(qybm);
    }
    protected void dg_List_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            if (e.CommandName == "Sel")
            {
                HiddenField hfEmpId = e.Item.FindControl("hfEmpId") as HiddenField;
                string fid = hfEmpId.Value;
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
            }
        }

    }

    protected void dg_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        EgovaDB db = new EgovaDB();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblLock = e.Item.Controls[0].FindControl("lblLock") as Label;
            HiddenField hLock = e.Item.Controls[0].FindControl("h_lock") as HiddenField;
            string idCard = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ZJBH"));
            var v = db.TC_PrjItem_Emp_Lock.Where(t => t.FIdCard == idCard).FirstOrDefault();
            if (v != null)
            {
                lblLock.Text = EConvert.ToBool(v.IsLock) ? "锁定" : "";
                hLock.Value = "1";
            }
            else
            {
                lblLock.Text = "";
                hLock.Value = "0";
            }
            LinkButton lb = e.Item.FindControl("btnSelect") as LinkButton;
            lb.Text = "选择";
            lb.Attributes.Add("onclick", "selEmp();");
        }
    }

    //protected void subList_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //     if (e.Item.ItemIndex > -1)
    //    {
    //        if (e.CommandName == "Sel")
    //        {
    //            HiddenField hfEmpId = e.Item.FindControl("hfEmpId") as HiddenField;
    //            string fid = hfEmpId.Value;
    //            pageTool tool = new pageTool(this.Page);
    //            tool.ExecuteScript("window.returnValue='" + fid  + "';window.close();");
    //        }
    //    }
    //}
    //protected void subList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    LinkButton lb = e.Item.FindControl("btnSelect") as LinkButton;
    //    lb.Text = "选择";
    //    lb.Attributes.Add("onclick", "selEmp();");
    //}
}