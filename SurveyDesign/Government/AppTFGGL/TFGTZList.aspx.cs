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

public partial class Government_AppTFGGL_TFGTZList : govBasePage
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
        sb.Append(" select * from ( ");
        sb.Append(" select *,case FBZT WHEN 0 THEN '未发布' ELSE '已发布' end as FBZTz ");
 
        sb.Append(" from TC_SGXKZ_TFGTZ ");
      
        sb.Append(" where 1 = 1");
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
        if (this.txtND.Text.Trim() != "" && this.txtND.Text.Trim() != null)
        {
            sb.Append(" and ND = " + this.txtND.Text.Trim() + " ");
        }
        if (this.txtTGPC.Text.Trim() != "" && this.txtTGPC.Text.Trim() != null)
        {
            sb.Append(" and TGPC like '%" + this.txtTGPC.Text.Trim() + "%' ");
        }
        if (this.txtFBZT.SelectedValue != null && this.txtFBZT.SelectedValue != "-1")
        {
            sb.Append(" and FBZT =  " + this.txtFBZT.SelectedValue);
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
            string fbzt = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBZT"));
            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["fId"] = fId;
            box.Attributes["fbzt"] = fbzt;
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