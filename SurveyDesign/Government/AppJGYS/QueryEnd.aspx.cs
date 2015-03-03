using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class JSDW_ApplyJGYS_AuditMain_QueryEnd : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindInfo();
        }
    }
    public void bindInfo()
    {
        StringBuilder _builder = new StringBuilder();
        _builder.Append(@"select j.ID,j.YWBM,pb.FManageTypeId,pb.FReportDate,pb.FTime,pb.FEmpName,pb.FEntName,case pb.FManageTypeId when '7070' then '房建' when '7080' then '市政' end as AuditType, case pb.FResult when 1 then '通过' when 3 then '不通过' else pb.FResult end idear
                                                        from CF_App_ProcessInstanceBackup pb
                                                        left join YW_JGYS j on pb.FLinkId=j.YWBM where 1=1");
        if (string.IsNullOrEmpty(ddlAuditType.SelectedValue))
            _builder.Append(" and pb.FManageTypeId in ('7070','7080')");
        else
            _builder.AppendFormat(" and pb.FManageTypeId = '{0}'",ddlAuditType.SelectedValue);

        if (!string.IsNullOrEmpty(txtFEmpName.Text))
            _builder.AppendFormat(" and pb.FEmpName like '%{0}%'",txtFEmpName.Text);
        if (!string.IsNullOrEmpty(txtFEntName.Text))
            _builder.AppendFormat(" and pb.FEntName like '%{0}%'",txtFEntName.Text);
        _builder.Append(" order by pb.FCreateTime desc");
        this.Pager1.sql = _builder.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bindInfo();
    }
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string YWBM = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "YWBM"));
            string Id = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ID"));
            string FManageTypeId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageTypeId"));
            string url = "";
            if (FManageTypeId == "7070")//房建
                url = "../../JSDW/ApplyJGYS/AppMain/index.aspx?JG_Id=" + Id + "&fAppId=" + YWBM + "&audit=1";
            else//市政
                url = "../../JSDW/ApplyJGYSSZ/AppMain/index.aspx?JG_Id=" + Id + "&fAppId=" + YWBM + "&audit=1";
            e.Item.Cells[1].Text = "<a href='javascript:void(0)' onclick=\"javascript:showAddWindow('" + url + "',1000,600);\" >" + e.Item.Cells[1].Text + "</a>";
        }
    }
}