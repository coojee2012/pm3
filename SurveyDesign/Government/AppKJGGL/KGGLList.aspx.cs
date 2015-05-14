using Approve.Common;
using Approve.RuleApp;
using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppKJGGL_KGGLList : govBasePage
{
    RCenter rc = new RCenter();
    SaveAsBase sab = new SaveAsBase();
    RApp ra = new RApp();
    RAppBacth rap = new RAppBacth();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        ShowInfo();
    }

    private void ShowInfo() {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from ( ");
        sb.Append(" select qa.*,ep.FID as FepId,dbo.getManageDeptName(qa.PrjAddressDept) as PrjAddressDeptName ,case qa.SJStartDate when qa.SJStartDate then '已开工' else '未开工' end as KGState from TC_SGXKZ_PrjInfo qa ");
        sb.Append(" left join CF_App_ProcessInstanceBackup ep on ep.FLinkId = qa.FAppId ");
        sb.Append(" left join CF_App_ProcessRecordBackup er on ep.fId = er.FProcessInstanceID ");
        sb.Append(" left join CF_APP_LIST ap on ep.FLinkId = ap.FId ");
        sb.Append(" where ep.FState = 6 and er.FTypeId=5 and er.FResult=1 ");
        sb.Append(getCondi());
        sb.Append(" ) as ttt where 1=1");

        //sb.AppendLine(" order by ttt.FReporttime desc,ttt.FBaseInfoId");


        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }


    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtFPrjItemName.Text.Trim() != "" && this.txtFPrjItemName.Text.Trim() != null)
        {
            sb.Append(" and qa.PrjItemName like '%" + this.txtFPrjItemName.Text.Trim() + "%' ");
        }

        if (this.govd_FRegistDeptId.FNumber != null)
        {
            sb.Append(" and qa.PrjAddressDept = '" + this.govd_FRegistDeptId.FNumber + "'");
        }
        else
        {
            sb.Append(" and qa.PrjAddressDept <> '' ");
        }
        if (this.txtJSDW.Text.Trim() != "" && this.txtJSDW.Text.Trim() != null)
        {
            sb.Append(" and qa.JSDW like '%" + this.txtJSDW.Text.Trim() + "%' ");
        }

        if (this.txtSGXKZBH.Text.Trim() != "" && this.txtSGXKZBH.Text.Trim() != null)
        {
            sb.Append(" and qa.SGXKZBH like '%" + this.txtSGXKZBH.Text.Trim() + "%' ");
        }

        if (this.txtHTJGB.Text.Trim() != "" && this.txtHTJGB.Text.Trim() != null)
        {
            sb.Append(" and convert(decimal(38,6),ISNULL(qa.Price,'0')) >= " + this.txtHTJGB.Text.Trim());
        }
        if (this.txtHTJGE.Text.Trim() != "" && this.txtHTJGE.Text.Trim() != null)
        {
            sb.Append(" and convert(decimal(38,6),ISNULL(qa.Price,'0')) <= " + this.txtHTJGE.Text.Trim());
        }

        if (this.txtKGSDate.Text.Trim() != "" && this.txtKGSDate.Text.Trim() != null)
        {
            sb.Append(" and qa.StartDate >='" + this.txtKGSDate.Text.Trim() + " " + "00:00:00' ");
        }
        if (this.txtKGEDate.Text.Trim() != "" && this.txtKGEDate.Text.Trim() != null)
        {
            sb.Append(" and qa.StartDate  <='" + this.txtKGEDate.Text.Trim() + " " + "23:59:59' ");
        }


        

        if (sb.Length > 0)
        {
            return sb.ToString();
        }
        else
        {
            return "";
        }
    }
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e) {
        if (e.Item.ItemIndex > -1)
        {
            string fAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FepId"));
   

            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[1].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('GCXX.aspx?FId=" + fId + "&FAppId=" + fAppId + "',900,600);\">" + e.Item.Cells[1].Text + "</a>";
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('SGXKZXX.aspx?FId=" + fId + "&FAppId=" + fAppId + "',900,600);\">" + e.Item.Cells[2].Text + "</a>";
            if (e.Item.Cells[7].Text == "未开工")
            {
                e.Item.Cells[7].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('KGSZ.aspx?FId=" + fId + "&FAppId=" + fAppId + "',800,400);\">" + e.Item.Cells[7].Text + "</a>";
            }
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e) {
        ShowInfo();
    }



    protected void btnOut_Click(object sender, EventArgs e)
    {
        string sql = this.Pager1.sql.ToString();
        DataTable dt = rc.GetTable(sql);
        if (dt != null)
        {
            this.JustAppInfo_List.DataSource = dt;
            this.JustAppInfo_List.DataBind();
            JustAppInfo_List.Columns[0].Visible = false;
            string fOutTitle = lPostion.Text;
            sab.SaveAsExc(this.JustAppInfo_List, fOutTitle, this.Response);
        }
    }
}