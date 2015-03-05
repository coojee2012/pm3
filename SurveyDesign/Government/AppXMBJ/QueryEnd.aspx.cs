using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Text;

public partial class Government_AppXMBJ_QueryEnd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindInfo();
            uegovdeptid.fNumber = "-1";
        }
    }
    public void bindInfo()
    {
        StringBuilder _builder = new StringBuilder();
        _builder.Append(@"select j.ID,j.YWBM,pb.FTime,j.BH,j.XMMC,j.XMSDMC,j.JSDW,j.CreateTime, case pb.FResult when 1 then '通过' when 3 then '不通过' else pb.FResult end idear from CF_App_ProcessInstanceBackup pb left join YW_XMBJ j on pb.FLinkId=j.YWBM where pb.FManageTypeId ='8080'");
        if (!string.IsNullOrEmpty(txtXMMC.Text))
            _builder.AppendFormat(" AND j.XMMC like '%{0}%'", txtXMMC.Text);
        if (!string.IsNullOrEmpty(txtXMBH.Text))
            _builder.AppendFormat(" AND j.BH like '%{0}%'", txtXMBH.Text);
        if (!string.IsNullOrEmpty(txtJSDW.Text))
            _builder.AppendFormat(" AND j.JSDW like '%{0}%'", txtJSDW.Text);
        if (!string.IsNullOrEmpty(uegovdeptid.fNumber) && uegovdeptid.fNumber != "-1")
            _builder.AppendFormat(" AND j.XMSD like '{0}%'", uegovdeptid.fNumber);
        if (!string.IsNullOrEmpty(ddlSeeState.SelectedValue))
            _builder.AppendFormat(" AND pb.FResult = '{0}'", ddlSeeState.SelectedValue);
        if (!string.IsNullOrEmpty(txtBJRQStart.Text))
            _builder.AppendFormat(" AND CONVERT(varchar(100), pb.FTime, 23) >= '{0}'", txtBJRQStart.Text);
        if (!string.IsNullOrEmpty(txtBJRQEnd.Text))
            _builder.AppendFormat(" AND CONVERT(varchar(100), pb.FTime, 23) <= '{0}'", txtBJRQEnd.Text);
        _builder.Append(" order by pb.FCreateTime desc");
        //txtXMMC.Text = _builder.ToString();
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
            string url = "../../JSDW/ApplyXMBJ/AppMain/index.aspx?XM_Id=" + Id + "&fAppId=" + YWBM + "&audit=1";
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"javascript:showAddWindow('" + url + "',1100,600);\" >" + e.Item.Cells[2].Text + "</a>";
        }
    }
}