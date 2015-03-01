using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using ProjectBLL;
using Tools;
using Approve.RuleCenter;

public partial class Government_NewAppMain_CertiList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowPostion();
            if (!string.IsNullOrEmpty(Request.QueryString["FManageTypeId"]))
            {
                int FManageTypeId = EConvert.ToInt(Request.QueryString["FManageTypeId"]);
                DG_List.Columns[4].HeaderText = (from m in db.CF_Sys_ManageType
                                                 join s in db.CF_Sys_SystemName on m.FSystemId equals s.FNumber
                                                 where m.FNumber == FManageTypeId
                                                 select s.FName).FirstOrDefault();

                if (FManageTypeId == 294)
                {
                    DG_List.Columns[4].HeaderText = "设计单位";
                }
            }
            ShowInfo();


        }
    }
    private void ShowPostion()
    {
        if (Request["fcol"] != null && Request["fcol"] != "")
        {
            lPostion.Text = rc.GetMenuName(Request["fcol"]);
        }

    }

    protected void btn_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    private void ShowInfo()
    {
        var v = db.CF_Prj_Certi.Where(t => t.FIsValid == 1);
        if (txtFPrjName.Text.Trim() != "")
        {
            v = v.Where(t => t.FName.Contains(txtFPrjName.Text));
        }
        if (txtFEntName.Text.Trim() != "")
        {
            v = v.Where(t => t.FJSEntName.Contains(txtFEntName.Text));
        }
        if (txtFCertiNo.Text.Trim() != "")
        {
            v = v.Where(t => t.FJSEntName.Contains(txtFCertiNo.Text));
        }
        if (!string.IsNullOrEmpty(Request.QueryString["FStartTime"]))
        {
            txtFReportDate.Text = Request.QueryString["FStartTime"];

        }

        if (!string.IsNullOrEmpty(Request.QueryString["FEndTime"]))
        {
            txtFReportDate1.Text = Request.QueryString["FEndTime"];

        }
        if (txtFReportDate.Text.Trim() != "")
        {
            v = v.Where(t => t.FAppDate >= EConvert.ToDateTime(txtFReportDate.Text.Trim()));

        }
        if (txtFReportDate1.Text.Trim() != "")
        {
            v = v.Where(t => t.FAppDate <= EConvert.ToDateTime(txtFReportDate1.Text.Trim()));

        }
        if (string.IsNullOrEmpty(Request.QueryString["DeptId"]))
        {
            v = v.Where(t => t.FCityId.ToString().StartsWith(EConvert.ToString(Session["DFId"])));

        }
        else
        {
            v = v.Where(t => t.FCityId.ToString().StartsWith(Request.QueryString["DeptId"]));

        }
        if (!string.IsNullOrEmpty(Request.QueryString["FManageTypeId"]))
        {
            v = v.Where(t => t.FCertiTypeId == EConvert.ToInt(Request.QueryString["FManageTypeId"]));
        }


        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowInfo();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            int FManageTypeId = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FCertiTypeId"));

            string FBaseInfoId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBaseInfoId"));
            string FEntName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEntName"));
            string FAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));

            string fUrl = rc.getMTypeQurl(FManageTypeId.ToString());
            string sScript = "openWinNew('" + fUrl + "?fbid=" + FBaseInfoId + "&faid=" + FAppId + "&fmid=" + FManageTypeId + "&fly=1')";

            e.Item.Cells[1].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\" >" + e.Item.Cells[1].Text + "</a>";

            string sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=100 ");

            sUrl += "?fbid=" + EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FJSEntId"));

            e.Item.Cells[3].Text = "<a href='#' class='link5' onclick=\"showAddWindow('" + sUrl + "',980,450)\"  >" + e.Item.Cells[3].Text + "</a>";


            if (FManageTypeId == 294)
            {
                FBaseInfoId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSJEntId"));
                FEntName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSJEntName"));
            }
            else
            {

            }
            e.Item.Cells[4].Text = FEntName;
            CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == FBaseInfoId).FirstOrDefault();
            if (ent != null)
            {
                sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=" + ent.FSystemId);

                sUrl += "?fbid=" + FBaseInfoId;

                e.Item.Cells[4].Text = "<a href='#' class='link5' onclick=\"showAddWindow('" + sUrl + "',980,450)\"  >" + e.Item.Cells[4].Text + "</a>";

            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
