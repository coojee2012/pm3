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

public partial class Government_AppTFGGL_TGList : govBasePage
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

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from ( ");
        sb.Append(" select qa.*,ep.FID as FepId,a.FId as FFId,a.FYJSJFGRQ, case a.FCLZT when 0 then '待处理' else '通过' end  as FCLZT ,");
        sb.Append(" case  when a.FTFGRQ <= GETDATE() then '已停工' else '已开工' end as FSGZT  ");
        sb.Append(" from TC_SGXKZ_TFG a ");
        sb.Append(" left join TC_SGXKZ_PrjInfo qa on a.FAppId = qa.FAppId ");
        sb.Append(" left join CF_App_ProcessInstanceBackup ep on ep.FLinkId = qa.FAppId ");
        sb.Append(" where a.FType=0");
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
        if (this.txtSGXKZBH.Text.Trim() != "" && this.txtSGXKZBH.Text.Trim() != null)
        {
            sb.Append(" and qa.SGXKZBH like '%" + this.txtSGXKZBH.Text.Trim() + "%' ");
        }
        if (this.txtBLZT.SelectedValue != null && this.txtBLZT.SelectedValue !="-1")
        {
            sb.Append(" and a.FCLZT =  " + this.txtBLZT.SelectedValue);
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
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFId"));

            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["fId"] = fId;
            box.Attributes["fAppId"] = fAppId;
            box.Attributes["name"] = fAppId;

            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('GCXX.aspx?FId=" + fId + "&FAppId=" + fAppId + "',900,600);\">" + e.Item.Cells[2].Text + "</a>";
            e.Item.Cells[3].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('SGXKZXX.aspx?FId=" + fId + "&FAppId=" + fAppId + "',900,600);\">" + e.Item.Cells[3].Text + "</a>";
            
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}