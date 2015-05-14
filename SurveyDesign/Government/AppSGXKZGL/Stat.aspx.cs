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
    //string FType = Session["FType"].ToString();
    //string FSystemId = Session["DFSystemId"].ToString();
    //string DFName = Session["DFName"].ToString();
    //string DFUserId = Session["DFUserId"].ToString();
    //string DFUserRightId = Session["DFUserRightId"].ToString();
    //string DFRoleId = Session["DFRoleId"].ToString();
    //string DFMenuRoleId = Session["DFMenuRoleId"].ToString();
    //string DFId = Session["DFId"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            lbTitle.Text = "施工许可证办理统计情况（截止统计日期" + DateTime.Now.GetDateTimeFormats('D')[0].ToString()+"）";
            showStatInfo();

        }
    }
   
    protected void showStatInfo()
    {
        EgovaDB db = new EgovaDB();
        string DFId = Session["DFId"].ToString();
        string DFId2= EConvert.ToString(Request.QueryString["PrjAddressDept"]);
        var v = from t in db.V_SGXKZ_TJ.Where(t=>t.SD !=null && t.SD != "")
                     select t;
        if (DFId.Length == 2 && string.IsNullOrEmpty(DFId2))
            v = v.Where(t => t.PrjAddressDept.Length == 4);
        else if (DFId.Length == 4 && string.IsNullOrEmpty(DFId2))
        {
            v = v.Where(t => t.PrjAddressDept.Contains(DFId) );
        }
        else if (!string.IsNullOrEmpty(DFId2))
        {
            //v = v.Where(t => t.PrjAddressDept.Contains(DFId2) && t.PrjAddressDept != DFId2);
            v = v.Where(t => t.PrjAddressDept.Contains(DFId2) && t.PrjAddressDept != DFId);
        }

        if (DFId2.Length == 6)
        {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('已经是最低区域');", true);
        }
        else
        {
            Pager1.RecordCount = v.Count();

            this.rep_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            rep_List.DataBind();
        }

       

    }
    protected void showDetailInfo()
    {
        EgovaDB db = new EgovaDB();
        var v = from a in db.V_SGXKZ_YW
                join c in db.CF_App_List
                on a.FAppId equals c.FId
                join b in db.CF_Sys_ManageDept
                on a.PrjAddressDept equals b.FNumber.ToString()
                select new
                {
                    a.FAppId,
                    c.FAppDate,
                    a.PrjAddressDept,
                    AddressDeptName = b.FName,
                    a.PrjItemType,
                    a.PrjItemName,
                    a.ProjectName,
                    a.JSDW,
                    a.YWLX,
                    c.FManageTypeId,
                    c.FState,
                    a.Address
                };
        if (!string.IsNullOrEmpty(this.txtPrjItemName.Text.Trim()))
        {
            v = v.Where(t => t.PrjItemName.Contains(this.txtPrjItemName.Text.Trim()));
        }

        if (!string.IsNullOrEmpty(this.txtJSDW.Text.Trim()))
        {
            v = v.Where(t => t.JSDW.Contains(this.txtJSDW.Text.Trim()));
        }
        if (this.ddlMType.SelectedValue != "-1")
        {
            v = v.Where(t => t.FManageTypeId.Equals(this.ddlMType.SelectedValue.Trim()));
        }
        if (this.ddlState.SelectedValue.Trim() != "-1")
        {
            v = v.Where(t => t.FState.Equals(this.ddlState.SelectedValue.Trim()));
        }
        if (!string.IsNullOrEmpty(this.govd_FRegistDeptId.FNumber))
        {
            v = v.Where(t => t.PrjAddressDept.Equals(this.govd_FRegistDeptId.FNumber));
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

  
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showStatInfo();
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
    protected void rep_List_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string[] arg;
        switch (e.CommandName)
        {
            case "CCBL_WSB":
            case "CCBL_YSB":
            case "CCBL_YSH":
                arg = e.CommandArgument.ToString().Split('|');//取得参数
                ddlMType.SelectedValue = "11223";
                ddlState.SelectedValue = arg[0];
                this.govd_FRegistDeptId.FNumber = arg[1];
                this.showDetailInfo();
                this.DetailPanel.Visible = true;
                this.StatPanel.Visible = false;
                break;
            case "YQBL_WSB":
            case "YQBL_YSB":
            case "YQBL_YSH":
                arg = e.CommandArgument.ToString().Split('|');//取得参数
                ddlMType.SelectedValue = "11224";
                ddlState.SelectedValue = arg[0];
                this.govd_FRegistDeptId.FNumber = arg[1];
                this.showDetailInfo();
                this.DetailPanel.Visible = true;
                this.StatPanel.Visible = false;
                break;
            case "BGBL_WSB":
            case "BGBL_YSB":
            case "BGBL_YSH":
                arg = e.CommandArgument.ToString().Split('|');//取得参数
                ddlMType.SelectedValue = "11225";
                ddlState.SelectedValue = arg[0];
                this.govd_FRegistDeptId.FNumber = arg[1];
                this.showDetailInfo();
                this.DetailPanel.Visible = true;
                this.StatPanel.Visible = false;
                break;
        }
    }
}