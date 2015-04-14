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
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleApp;
using System.Drawing;
using EgovaDAO;

public partial class Government_AppSGXKZGL_ZSCXList : govBasePage
{
    RCenter rc = new RCenter();
    SaveAsBase sab = new SaveAsBase();
    RApp ra = new RApp();
    RAppBacth rap = new RAppBacth();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            ShowInfo();
        }
    }

    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtFPrjItemName.Text.Trim() != "" && this.txtFPrjItemName.Text.Trim() != null)
        {
            sb.Append(" and a.PrjItemName like '%" + this.txtFPrjItemName.Text.Trim() + "%' ");
        }

        if (this.govd_FRegistDeptId.FNumber != null)
        {
            sb.Append(" and dbo.isSuperDept_new(" + this.govd_FRegistDeptId.FNumber + ",a.PrjAddressDept" + ") >0 ");
        }
        else
        {
            sb.Append(" and a.PrjAddressDept <> '' ");
        }
        if (this.txtJSDW.Text.Trim() != "" && this.txtJSDW.Text.Trim() != null)
        {
            sb.Append(" and a.JSDW like '%" + this.txtJSDW.Text.Trim() + "%' ");
        }


        if (this.txtZSBH.Text.Trim() != "" && this.txtZSBH.Text.Trim() != null)
        {
            sb.Append(" and a.SGXKZBH like '%" + this.txtZSBH.Text.Trim() + "%' ");
        }


        if (this.txtFZJG.Text.Trim() != "" && this.txtFZJG.Text.Trim() != null)
        {
            sb.Append(" and a.FZJG like '%" + this.txtFZJG.Text.Trim() + "%' ");
        }


        if (this.txtFZTimeStart.Text.Trim() != "" && this.txtFZTimeStart.Text.Trim() != null)
        {
            sb.Append(" and a.FZTime > '" + this.txtFZTimeStart.Text.Trim() + " 00:00:00' ");
        }

        if (this.txtFZTimeEnd.Text.Trim() != "" && this.txtFZTimeEnd.Text.Trim() != null)
        {
            sb.Append(" and a.FZTime < '" + this.txtFZTimeEnd.Text.Trim() + " 23:59:59' ");
        }

        if (ddlMType.SelectedValue != "-1")
        {
            switch (ddlMType.SelectedValue.Trim())
            {
                case "11223": //初次办理
                    sb.Append(" and ep.FManageTypeId=11223 ");
                    break;
                case "11224": //延期办理
                    sb.Append(" and ep.FManageTypeId=11224 ");
                    break;
                case "11225": //变更办理
                    sb.Append(" and ep.FManageTypeId=11225 ");
                    break;
            }
        }

        if (ddlState.SelectedValue != "-1")
        {
            switch (ddlState.SelectedValue.Trim())
            {
                case "0": //未发布
                    sb.Append(" and ISNULL(b.FPublish,0) = 0 ");
                    break;
                case "1": //已发布
                    sb.Append(" and ISNULL(b.FPublish,0) = 1 ");
                    break;
            }
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

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from ( ");
        sb.Append(" select a.*,b.FId as FFId,");
        sb.Append("  CASE ep.FManageTypeId when 11223 then '初次办理' when 11224 then '延期办理' when  11225 then '变更办理'  else '--' end as YWType, ");
       
        sb.Append(" CASE ISNULL(b.FPublish,0) when 0 then '否' else '是' end as FPublish,  ");
        sb.Append(" CASE ISNULL(b.SGXKZBB,0) when 0 then '否' else '是' end as SGXKZBB");
        sb.Append(" from  TC_SGXKZ_PrjInfo a ");
        sb.Append(" left join TC_SGXKZ_PrjState b on a.FId=b.FId ");
        sb.Append(" left join CF_App_ProcessInstanceBackup ep on ep.FLinkId = a.FAppId ");
     
        sb.Append(" where ISNULL(a.SGXKZBH,'') <> '' ");
        sb.Append(getCondi());
       
        sb.AppendLine(" ) ttt where 1=1 ");





        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();



    }



    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            //int prjAddressDept = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "PrjAddressDept"));
            //EgovaDB db = new EgovaDB();
         
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            string ffid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFId"));
            string FAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));

            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["name"] = fid;
            box.Attributes["fid"] = fid;
            box.Attributes["ffid"] = ffid;
        
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[3].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('../AppTFGGL/SGXKZXX.aspx?FId=" + fid + "&FAppId=" + FAppId + "',900,600);\">" + e.Item.Cells[3].Text + "</a>";
            e.Item.Cells[4].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('JSDWXX.aspx?FId=" + fid + "&FAppId=" + FAppId + "',900,600);\">" + e.Item.Cells[4].Text + "</a>";
        }
    }

    #region 按钮事件
    protected void btnQuery_Click(object sender, EventArgs e)
    {
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
    #endregion
    protected void btnPublish_Click(object sender, EventArgs e)
    {

    }

    protected void btnUnPublish_Click(object sender, EventArgs e)
    {

    }
}