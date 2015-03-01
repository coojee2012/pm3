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
using System.Drawing;
using System.Linq;
using EgovaDAO;
using EgovaBLL;
using Approve.RuleCenter;

public partial class Government_AppAQJDBA_Stat : govBasePage
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            showStatInfo();

        }
    }
   
    protected void showStatInfo()
    {
        EgovaDB db = new EgovaDB();
        var t1 = from a in db.TC_QA_Record
                 join c in db.CF_App_List
                 on a.FAppId equals c.FId
                 join b in db.CF_Sys_ManageDept
                 on a.AddressDept equals b.FNumber.ToString()
                 where c.FState == 6 
                 select new {
                     c.FAppDate,
                     a.AddressDept,
                     AddressDeptName = b.FName,
                     a.PrjItemType,
                     SZJCSS = (a.PrjItemType.Equals("2000102")?1:0),
                     FWJZGC = (a.PrjItemType.Equals("2000101") ? 1 : 0),
                     Other  = (a.PrjItemType.Equals("2000103") ? 1 : 0),
                     HJ = 1
                 };
        if (!string.IsNullOrEmpty(this.txtSDate.Text.Trim()))
        {
            t1 = t1.Where(t => t.FAppDate >=DateTime.Parse( this.txtSDate.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.txtEDate.Text.Trim()))
        {
            t1 = t1.Where(t => t.FAppDate < DateTime.Parse(this.txtEDate.Text.Trim()));
        }
        var v = from a in t1
                      group a by new {
                          a.AddressDept,
                          a.AddressDeptName
                          }into g
                      select new { g.Key.AddressDept,
                          g.Key.AddressDeptName,
                          SZJCSS = g.Sum(t=>t.SZJCSS),
                          FWJZGC = g.Sum(t => t.FWJZGC),
                          Other = g.Sum(t => t.Other),
                          HJ = g.Sum(t => t.HJ),
                      };
        

        Pager1.RecordCount = v.Count();
        this.gv_stat.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        gv_stat.DataBind();

    }
    protected void showDetailInfo()
    {
        EgovaDB db = new EgovaDB();
        var v = from a in db.TC_QA_Record
                 join c in db.CF_App_List
                 on a.FAppId equals c.FId
                 join b in db.CF_Sys_ManageDept
                 on a.AddressDept equals b.FNumber.ToString()
                 where c.FState == 6
                 select new
                 {
                     a.FAppId,
                     c.FAppDate,
                     a.AddressDept,
                     AddressDeptName = b.FName,
                     a.PrjItemType,
                     a.PrjItemName,
                     a.ProjectName,
                     a.JSDW,
                     a.RecordNo
                 };
        if (!string.IsNullOrEmpty(this.txtPrjItemName.Text.Trim()))
        {
            v = v.Where(t => t.PrjItemName.Contains(this.txtPrjItemName.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
        {
            v = v.Where(t => t.ProjectName.Contains(this.txtProjectName.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.txtJSDW.Text.Trim()))
        {
            v = v.Where(t => t.JSDW.Contains(this.txtJSDW.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.txtRecordNo.Text.Trim()))
        {
            v = v.Where(t => t.RecordNo.Contains(this.txtRecordNo.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.p_PrjItemType.Text.Trim()))
        {
            v = v.Where(t => t.PrjItemType.Equals(this.p_PrjItemType.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.govd_FRegistDeptId.FNumber))
        {
            v = v.Where(t => t.AddressDept.Equals(this.govd_FRegistDeptId.FNumber));
        }
        if (!string.IsNullOrEmpty(this.txtSDate1.Text.Trim()))
        {
            v = v.Where(t => t.FAppDate >= DateTime.Parse(this.txtSDate1.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.txtEDate1.Text.Trim()))
        {
            v = v.Where(t => t.FAppDate < DateTime.Parse(this.txtEDate1.Text.Trim()));
        }
        Pager2.RecordCount = v.Count();
        this.gv_detail.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        gv_detail.DataBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.showStatInfo();
    }
    protected void btnQuery1_Click(object sender, EventArgs e)
    {
        this.showDetailInfo();
    }
    protected void gv_stat_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gv_stat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "See1")
        {
            string sAddressDept = SConvert.ToString(e.CommandArgument);
            this.govd_FRegistDeptId.FNumber = sAddressDept;
            this.p_PrjItemType.SelectedValue = "2000102";
            this.DetailPanel.Visible = true;
            this.StatPanel.Visible = false;

        }
        if (e.CommandName == "See2")
        {
            string sAddressDept = SConvert.ToString(e.CommandArgument);
            this.govd_FRegistDeptId.FNumber = sAddressDept;
            this.p_PrjItemType.SelectedValue = "2000101";
            this.DetailPanel.Visible = true;
            this.StatPanel.Visible = false;

        }
        if (e.CommandName == "See3")
        {
            string sAddressDept = SConvert.ToString(e.CommandArgument);
            this.govd_FRegistDeptId.FNumber = sAddressDept;
            this.p_PrjItemType.SelectedValue = "2000103";
            this.DetailPanel.Visible = true;
            this.StatPanel.Visible = false;

        }
        if (e.CommandName == "SeeAll")
        {
            string sAddressDept = SConvert.ToString(e.CommandArgument);
            this.govd_FRegistDeptId.FNumber = sAddressDept;
        //    this.p_PrjItemType.SelectedValue = "2000102";
            this.DetailPanel.Visible = true;
            this.StatPanel.Visible = false;

        }
    }
    protected void gv_stat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //序号
            Label lbautoid = (Label)e.Row.FindControl("lbautoid");
            lbautoid.Text = (e.Row.RowIndex + 1).ToString();
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.StatPanel.Visible = true;
        this.DetailPanel.Visible = false;
    }
    protected void gv_detail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //序号
            Label lbautoid = (Label)e.Row.FindControl("lbautoid");
            lbautoid.Text = (e.Row.RowIndex + 1).ToString();
           
        }

    }
    protected void gv_detail_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gv_detail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "See")
        {
            string fAppId = SConvert.ToString(e.CommandArgument);
            EgovaDB db = new EgovaDB();
            string faid = fAppId;
            var v = (from t in db.CF_App_ProcessInstance
                     where t.FLinkId == faid
                     select t).FirstOrDefault();
            string fbid = v.FBaseInfoID;
            string fmid = SConvert.ToString(v.FManageTypeId);
            var v1 = (from t in db.CF_Sys_User
                      where t.FBaseInfoId == fbid
                      select t).FirstOrDefault();
            string frid = v1.FMenuRoleId;
            string fsid = SConvert.ToString(v.FSystemId);
            var v2 = (from t in db.CF_Sys_ManageType
                      where t.FSystemId == SConvert.ToInt(fsid)
                      select t).FirstOrDefault();
            string fUrl = v2.FQUrl; ;
            string js = "openWinNew('" + fUrl + "?sysid=" + fsid + "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + Session["DFUserId"].ToString() + "')";
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "jsKey", js, true);

        }
    }
    protected void Pager2_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager2.CurrentPageIndex = e.NewPageIndex;
        this.showDetailInfo();
    }
}