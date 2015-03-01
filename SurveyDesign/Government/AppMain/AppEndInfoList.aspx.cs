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
using Approve.Common;
using Approve.EntityCenter;
using Approve.RuleCenter;
using System.Text;
using Approve.EntityBase;
using Approve.RuleApp;
using System.Linq;
using ProjectData;

public partial class Government_AppMain_AppEndInfoList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    RAppBacth rap = new RAppBacth();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request["fcol"] != null && Request["fcol"] != "")
            {
                lPostion.Text = rc.GetMenuName(Request["fcol"]);
            }
            CotrolBind();
            ShowInfo();
        }
    }

    private void CotrolBind()
    {
        StringBuilder sb = new StringBuilder();


        showDDType(Request["fsystemid"]);


        //DataTable dt = rc.getAllupDeptId(Session["DFId"].ToString(), 0, 0);
        //ddlDept.DataSource = dt;
        //ddlDept.DataTextField = "FName";
        //ddlDept.DataValueField = "FNumber";
        //ddlDept.DataBind();

        if (Request.QueryString["b"] == "1")
            btnReturn.Visible = true;
    }

    private void showDDType(string FSystemId)
    {
        ProjectDB db = new ProjectDB();
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(" select fname,fnumber from CF_Sys_ManageType ");
        sb.AppendLine(" where  fmtypeid<>'0'");

        sb.AppendLine("   order by forder ");
        var varManageType = db.ManageType;
        if (string.IsNullOrEmpty(FSystemId))
        {
            varManageType = varManageType.Where(t => t.FMTypeId == 193);

        }
        else
        {
            varManageType = varManageType.Where(t => t.FSystemId == EConvert.ToInt(FSystemId));

        }
        var result = from m in varManageType
                     join m1 in db.ManageType.GroupBy(t => t.FName).Select(g => g.Max(t => t.FID)) on m.FID equals m1
                     where m.FMTypeId != 0
                     orderby m.FOrder
                     select new
                     {
                         m.FName,
                         FNumber = string.Join(",", db.ManageType.Where(t => t.FName == m.FName).Select(t => t.FNumber.ToString()).ToArray())
                     };


        //DataTable dt = rc.GetTable(sb.ToString());
        this.dbFManageTypeId.DataSource = result;
        this.dbFManageTypeId.DataTextField = "fname";
        this.dbFManageTypeId.DataValueField = "fnumber";
        this.dbFManageTypeId.DataBind();
        this.dbFManageTypeId.Items.Insert(0, new ListItem("全部", ""));

        if (!string.IsNullOrEmpty(Request.QueryString["FManageTypeId"]))
        {
            ListItem li = dbFManageTypeId.Items.FindByValue(Request.QueryString["FManageTypeId"]);
            if (li != null)
            {
                dbFManageTypeId.ClearSelection();
                li.Selected = true;
            }
        }

    }

    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (txtFEntName.Text.Trim() != "")
        {
            sb.Append(" and t1.fentname like '%" + txtFEntName.Text.Trim() + "%'");
        }

        if (dbFManageTypeId.SelectedValue.Trim() != "")
        {
            sb.Append(" and t1.FManageTypeId in(" + dbFManageTypeId.SelectedValue + ") ");
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
            sb.Append(" and t1.FTime >= '" + txtFReportDate.Text.Trim() + "'");
        }
        if (txtFReportDate1.Text.Trim() != "")
        {
            sb.Append(" and t1.FTime <= '" + txtFReportDate1.Text.Trim() + "'");
        }

        sb.Append(" and t1.FManageDeptId like '" + Session["DFId"] + "%' ");
        sb.Append(" and t1.fid in ");
        sb.Append(" (select FProcessInstanceID from CF_App_ProcessRecordBackUp t2");
        sb.Append(" where t2.FProcessInstanceID = t1.fid ");
        //if (EConvert.ToInt(Session["DFLevel"]) > 1)
        sb.Append(" and t2.froleid in ('" + Session["DFRoleId"] + "') ");
        sb.Append(" ) ");

        //if (!string.IsNullOrEmpty(ddlDept.SelectedValue))
        //    sb.Append(" and t1.FManageDeptId like '" + ddlDept.SelectedValue + "%' ");
        return sb.ToString();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select t1.fid,t1.flinkid,t1.fBaseInfoId,");
        sb.Append(" t1.fentname,t1.flistid,");
        sb.Append(" t1.ftypeid,t1.flevelid,t1.FManageTypeId, ");
        sb.Append(" t1.FResult,t1.FTime FReportDate,t1.fstate,t1.FSubFlowId,t1.FEmpName,t1.FEmpId,t1.FLeadId,t1.FLeadName,t1.fsystemid ");
        sb.Append(" from CF_App_ProcessInstanceBackUp t1 ");
        sb.Append(" where t1.fisdeleted=0 ");
        sb.Append(" and t1.fstate = 6 ");
        sb.Append(GetCon());
        sb.Append(" order by t1.FReportDate desc ");


        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "DG_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string fId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fLinkId"));
            string fManageTypeId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fManageTypeId"));
            string fState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fState"));
            string fBaseInfoId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fBaseInfoId"));
            string fLevelId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fLevelId"));
            string fTypeId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fTypeId"));
            string fListId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fListId"));

            string fResult = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fResult"));
            string fsystemid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fsystemid"));

            StringBuilder sb = new StringBuilder();
            e.Item.Cells[4].Text = rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + fManageTypeId + "'");




            string sColor = "";
            switch (fState)
            {


                case "6":


                    if (fResult == "1")
                    {
                        e.Item.Cells[6].Text += "通过";
                    }
                    if (fResult == "3")
                    {
                        e.Item.Cells[6].Text += "不通过";
                    }
                    if (fResult == "6")
                    {
                        e.Item.Cells[7].Text += "待定";
                    }
                    break;
            }
            string frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + fBaseInfoId + "'");
            string sUrl = rc.getMTypeQurl(fManageTypeId);

            sUrl += "?e=0&fbid=" + fBaseInfoId + "&faid=" + fLinkId + "&frid=" + frid + "&fmid=" + fManageTypeId + "&sysid=" + fsystemid;


            string prjName = e.Item.Cells[1].Text;
            e.Item.Cells[1].Text = "<a class='link7' href='" + sUrl + "' target='_blank'>";
            e.Item.Cells[1].Text += prjName + "</a>";

            string fBaseName = e.Item.Cells[3].Text;
            sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=100 ");
            if (fManageTypeId == "294")
            {
                fBaseName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEntName"));
                sUrl += "?fbid=" + fBaseInfoId;

            }
            else
            {

                sUrl += "?fbid=" + EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLeadId"));
            }


            e.Item.Cells[3].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + sUrl + "&fly=1',980,450)\"  >" + fBaseName + "</a>";

            string prjid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEmpId"));
            e.Item.Cells[2].Text = db.CF_Prj_BaseInfo.Where(t => t.FId == prjid).Select(t => t.FAllAddress).FirstOrDefault();

            string sAppContent = e.Item.Cells[6].Text;
            sUrl = "../AppQualiInfo/ShowAppList.aspx";
            sUrl += "?e=0&pid=" + fId;
            e.Item.Cells[6].Text = "<a class='link7' href='" + sUrl + "' target='_blank'>";
            e.Item.Cells[6].Text += "" + sAppContent + "</a>";

            //查询该项目是否变更 
            if (!string.IsNullOrEmpty(fLinkId))
            {
                string fPrjId = db.CF_App_List.Where(t => t.FId == fLinkId).Select(t => t.FPrjId).FirstOrDefault();
                if (!string.IsNullOrEmpty(fPrjId))
                {
                    var prjBG = db.CF_Prj_BaseInfo.Where(t => t.FId == fPrjId)
                        .Select(t => new { t.FBGTime, t.FCount })
                        .FirstOrDefault();
                    if (prjBG != null && prjBG.FCount > 0)
                    {
                        e.Item.Cells[1].Text += "<br/><font color='#000000;'>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")</font>";
                    }
                }
            }

            string DataFID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEmpId"));
            //合同备案业务
            if ("411,412,413,414,421,422,423,424".Split(',').Contains(fManageTypeId))
            {
                e.Item.Cells[2].Text = db.CF_Prj_Data.Where(t => t.FId == DataFID).Select(t => t.FDeptName).FirstOrDefault();
            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }


    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../statis/Business.aspx");
    }
}
