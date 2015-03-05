using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ProjectData;
using System.Data;
using Approve.RuleCenter;
using Approve.Common;

public partial class Government_AppMain_JNCLSBSearch : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private ProjectDB db = new ProjectDB();
    private Approve.Common.SaveAsBase sab = new Approve.Common.SaveAsBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            ShowInfo();
        }
    }
    private void BindControl()
    {
        var CPLB = db.getDicList("2001303");
        ddlCPLB.DataSource = CPLB;
        ddlCPLB.DataValueField = "fnumber";
        ddlCPLB.DataTextField = "fname";
        ddlCPLB.DataBind();
        ddlCPLB.Items.Insert(0, new ListItem() { Selected = true, Text = "--请选择--", Value = "0" });
    }
    private void ShowInfo()
    {
        StringBuilder _builder = new StringBuilder();
        _builder.AppendFormat(@"select b.YWBM, b.SQCPMC,b.CPLBMC,b.BSDJMC,c.QYMC,er.FReporttime, 
                        case er.FtypeId when 1 then '接件' when 10 then '初审' when 15 then '复审' when 5 then '领导审批' end CurrentLocation
                        from CF_App_ProcessInstance ep 
                        inner join CF_App_ProcessRecord er on ep.fId = er.FProcessInstanceID  
                        left join CF_App_List l on ep.FLinkId=l.FId  
                        left join YW_JNCL_PRODUCT b on l.FId=b.YWBM 
                        left join YW_JNCL_QYJBXX c on l.FId=c.YWBM 
                        inner join (  select max(er.fid)fid from CF_App_ProcessInstance ep,CF_App_ProcessRecord er where ep.fId = er.FProcessInstanceID and ep.fsystemid=1122 and ep.FManageTypeId='4001'  and ep.FManageDeptId like '51%'  group by ep.flinkId )temp on er.fid=temp.fid 
                        where b.FType={0} and c.FType={0} and er.FMeasure=0",FType);
        _builder.Append(GetCon());
        _builder.Append(" order by er.FReporttime desc");
        this.Pager1.sql = _builder.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }
    private string GetCon()
    {
        StringBuilder _builder = new StringBuilder();
        if (!string.IsNullOrEmpty(txtQYMC.Text))
            _builder.AppendFormat(" and c.QYMC like '%{0}%'", txtQYMC.Text);
        if (!string.IsNullOrEmpty(txtCPMC.Text))
            _builder.AppendFormat(" and b.SQCPMC like '%{0}%'", txtCPMC.Text);
        if (ddlCPLB.SelectedValue != "0")
            _builder.AppendFormat(" and b.CPLBBM = '{0}'", ddlCPLB.SelectedValue);
        if (!string.IsNullOrEmpty(t_Stime.Text))
            _builder.AppendFormat(" and CONVERT(nvarchar(10),er.FReporttime,121) >= '{0}'", t_Stime.Text);
        if (!string.IsNullOrEmpty(t_Etime.Text))
            _builder.AppendFormat(" and CONVERT(nvarchar(10),er.FReporttime,121) <= '{0}'", t_Etime.Text);
        if (!string.IsNullOrEmpty(ddlCurrentLocation.SelectedValue))
            _builder.AppendFormat(" and er.FtypeId={0}", ddlCurrentLocation.SelectedValue);
        return _builder.ToString();
    }
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string YWBM = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "YWBM"));
            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["YWBM"] = YWBM;
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
        }
    }
    private string FType {
        get {
            return Request.QueryString["FType"];
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
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
            sab.SaveAsExc(this.JustAppInfo_List, "节能材料上报查询", this.Response);
        }
        else {
            pageTool tool = new pageTool(this.Page);
            tool.showMessage("未查询到数据");
        }
    }
}