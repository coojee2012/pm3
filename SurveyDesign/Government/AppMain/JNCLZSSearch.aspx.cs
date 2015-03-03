using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Approve.RuleCenter;
using System.Data;
using Approve.Common;

public partial class Government_AppMain_JNCLZSSearch : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private Approve.Common.SaveAsBase sab = new Approve.Common.SaveAsBase();
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
        _builder.AppendFormat(@"select j.YWBM,j.SQCPMC,j.CPLBMC,j.BSDJMC,c.QYMC,pb.FReportDate,pb.FTime from CF_App_ProcessInstanceBackup pb
        left join YW_JNCL_PRODUCT j on pb.FLinkId=j.YWBM
        left join YW_JNCL_QYJBXX c on pb.FLinkId=c.YWBM
        where pb.FManageTypeId ='4001' and j.FType={0} and c.FType={0}", FType);

        if (!string.IsNullOrEmpty(txtQYMC.Text))
            _builder.AppendFormat(" and c.QYMC like '%{0}%'", txtQYMC.Text);
        if (!string.IsNullOrEmpty(txtCPMC.Text))
            _builder.AppendFormat(" and j.SQCPMC like '%{0}%'", txtCPMC.Text);
        //if (ddlCPLB.SelectedValue != "0")
        //    _builder.AppendFormat(" and j.CPLBBM='{0}'", ddlCPLB.SelectedValue);
        //if (ddlAuditResult.SelectedValue != "0")
        //    _builder.AppendFormat(" and pb.FResult={0}", ddlAuditResult.SelectedValue);
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
            string YWBM = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "YWBM"));
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            //string YWBM = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "YWBM"));
            //string Id = ""; //EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ID"));
            //string FManageTypeId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageTypeId"));
            //string url = "";
            //if (FManageTypeId == "6060")//房建
            //    url = "../../JSDW/ApplyGCGH/AppMain/index.aspx?GC_Id=" + Id + "&fAppId=" + YWBM + "&audit=1";
            //else//市政
            //    url = "../../JSDW/ApplyGCGHSZ/AppMain/index.aspx?GC_Id=" + Id + "&fAppId=" + YWBM + "&audit=1";
            //e.Item.Cells[1].Text = "<a href='javascript:void(0)' onclick=\"javascript:showAddWindow('" + url + "',1000,600);\" >" + e.Item.Cells[1].Text + "</a>";
        }
    }
    private string FType
    {
        get
        {
            return Request.QueryString["FType"];
        }
    }
    protected void btnOut_Click(object sender, EventArgs e)
    {
        string sql = this.Pager1.sql.ToString();
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            this.JustAppInfo_List.DataSource = dt;
            this.JustAppInfo_List.DataBind();
            JustAppInfo_List.Columns[0].Visible = false;
            sab.SaveAsExc(this.JustAppInfo_List, "节能材料证书查询", this.Response);
        }
        else
        {
            pageTool tool = new pageTool(this.Page);
            tool.showMessage("未查询到数据");
        }
    }
}
