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

public partial class Government_AppTFGGL_RWSDList : govBasePage
{
    RCenter rc = new RCenter();
    SaveAsBase sab = new SaveAsBase();
    RApp ra = new RApp();
    RAppBacth rap = new RAppBacth();
    ProjectDB db = new ProjectDB();
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
        sb.Append("select * from ( ");
        sb.Append(" select a.FId,a.FAppId,a.IsLock,a.SelectedCount,case b.FSex when 1 then '男' else '女' end as FSex,");
        sb.Append(" b.ZW,b.FIdCard,b.FHumanName,b.FEntName,b.ZSBH,b.ZCZY,b.EmpType,d.FName, ");
        sb.Append(" c.PrjItemName,c.ProjectName,dbo.getManageDeptName(c.PrjAddressDept) as DeptName,c.StartDate,c.EndDate   ");
  
        sb.Append(" from TC_PrjItem_Emp_Lock a ");
        sb.Append(" left join TC_PrjItem_Emp b on a.FIdCard=b.FIdCard ");
        sb.Append(" left join TC_SGXKZ_PrjInfo c on c.FAppId = a.FAppId ");
        sb.Append(" left join CF_Sys_Dic d on b.EmpType = d.FNumber ");
        sb.Append(" where not exists (select 1 from TC_PrjItem_Emp_Lock where FIdCard = a.FIdCard  and FCreateTime > a.FCreateTime) ");
        sb.Append(" and a.IsLock = 1 ");
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
        if (this.txtXM.Text.Trim() != "" && this.txtXM.Text.Trim() != null)
        {
            sb.Append(" and b.FHumanName like '%" + this.txtXM.Text.Trim() + "%' ");
        }
        if (this.txtIDCard.Text.Trim() != "" && this.txtIDCard.Text.Trim() != null)
        {
            sb.Append(" and b.FIdCard like '%" + this.txtIDCard.Text.Trim() + "%' ");
        }
        if (this.txtSGXKZBH.Text.Trim() != "" && this.txtSGXKZBH.Text.Trim() != null)
        {
            sb.Append(" and b.ZSBH like '%" + this.txtSGXKZBH.Text.Trim() + "%' ");
        }
        if (this.txtQYMC.Text.Trim() != "" && this.txtQYMC.Text.Trim() != null)
        {
            sb.Append(" and b.FEntName like '%" + this.txtQYMC.Text.Trim() + "%' ");
        }
        if (this.txtFBZT.SelectedValue != null && this.txtFBZT.SelectedValue != "-1")
        {
            //sb.Append(" and b.ZSBH =  " + this.txtSGXKZBH.SelectedValue);
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
            string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "Fid"));
            string idCard = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FIdCard"));

            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["fId"] = fId;
            box.Attributes["fAppId"] = fAppId;
            box.Attributes["name"] = fAppId;

            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[13].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('JCSD.aspx?idCard=" + idCard + "&FAppId=" + fAppId + "',1024,600);\">" + e.Item.Cells[13].Text + "</a>";
          //  e.Item.Cells[3].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('SGXKZXX.aspx?FId=" + fId + "&FAppId=" + fAppId + "',900,600);\">" + e.Item.Cells[3].Text + "</a>";

        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}