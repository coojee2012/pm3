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
public partial class JSDW_ApplySGXKZGL_TFGList : govBasePage
{
    RCenter rc = new RCenter();
    SaveAsBase sab = new SaveAsBase();
    RApp ra = new RApp();
    RAppBacth rap = new RAppBacth();
    ProjectDB db = new ProjectDB();
    //   Session.Remove("FBaseId");
    //   Session.Remove("FType");
    //   Session.Remove("FUserId");
    //   Session.Remove("FUserRightId");
    //   Session.Remove("FMenuRoleId");
    //   Session.Remove("FRoleId");
    //   Session.Remove("FSystemId");
    //   Session.Remove("FBaseName");
    //   Session.Remove("FBaseinfoId");
    //   Session.Remove("EntUserId");
    //   Session.Remove("fly");
    //   Session.Remove("FManageTypeId");
    //   Session.Remove("FIsApprove");
    //   Session.Remove("FCanMod");
    //   Session.Remove("FAppId");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.Page_Load(sender, e);
            ShowInfo();
        }
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        string jsdwId = Session["EntUserId"].ToString();
        string FBaseinfoID = CurrentEntUser.EntId;
        sb.Append(" select * from ( ");
        sb.Append(" select qa.*,a.FType,a.FTFGRQ,a.FYJSJFGRQ, b.FJSDWID  from TC_SGXKZ_TFG a ");
        sb.Append(" left join TC_SGXKZ_PrjInfo qa on a.FAppId = qa.FAppId");
        sb.Append(" left join TC_Prj_Info b on qa.PrjId=b.FId ");
        sb.Append(" left join CF_App_ProcessInstanceBackup ep on ep.FLinkId = qa.FAppId ");
        sb.Append(" where a.FCLZT=1 ");
        sb.Append(" and b.FJSDWID = '" + FBaseinfoID + "'");
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
            string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[1].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('GCXX.aspx?FId=" + fId + "&FAppId=" + fAppId + "',900,600);\">" + e.Item.Cells[1].Text + "</a>";
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('SGXKZXX.aspx?FId=" + fId + "&FAppId=" + fAppId + "',900,600);\">" + e.Item.Cells[2].Text + "</a>";
            e.Item.Cells[6].Text = "已停工";
            //if (e.Item.Cells[7].Text.Contains("已开工"))
            //{
            //    e.Item.Cells[7].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('JGSZ.aspx?FId=" + fId + "&FAppId=" + fAppId + "',800,400);\">" + e.Item.Cells[7].Text + "</a>";
            //}
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

}