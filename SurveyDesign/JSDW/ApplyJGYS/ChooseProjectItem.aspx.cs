using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class JSDW_ApplyJGYS_ChooseProjectItem : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            Show();
        }
    }
    private void BindControl()
    {
        string sql = @"select FName,FCnumber from CF_Sys_Dic where FparentId='20001'";
        DataTable table = rc.GetTable(sql);
        if (table != null && table.Rows.Count > 0)
        {
            ddlGCLB.DataSource = table;
            ddlGCLB.DataValueField = "FCnumber";
            ddlGCLB.DataTextField = "FName";
            ddlGCLB.DataBind();
            ddlGCLB.Items.Insert(0,new ListItem() { Text = "--请选择--", Value = "0" });
            ddlGCLB.SelectedValue = "1";
        }
    }
    private void Show()
    {
        StringBuilder _builder = new StringBuilder();
//        _builder.Append(@"select b.FID,b.PrjItemName,b.PrjItemType,b.[Address],b.ProjectName,c.FName from TC_Prj_Info a inner join TC_PrjItem_Info b 
//on a.FId=b.FPrjId left join (select FNumber,FName from CF_Sys_Dic where FParentId='20001') c on b.PrjItemType=c.FNumber where 1=1");
//        if (!string.IsNullOrEmpty(CurrentEntUser.EntId))
//            _builder.AppendFormat(" AND b.FJSDWID='{0}'", CurrentEntUser.EntId);
//        if (!string.IsNullOrEmpty(txtProjectName.Text))
//            _builder.AppendFormat(" AND b.ProjectName like '%{0}%'", txtProjectName.Text);
//        if (!string.IsNullOrEmpty(txtItemName.Text))
//            _builder.AppendFormat(" AND b.PrjItemName like '%{0}%'", txtItemName.Text);
//        _builder.Append("  order by c.FName");
        _builder.Append(@"select A.XMBH,A.DWGCBH,B.XMMC,A.DWGCMC,B.XMLX,B.XMDZ  from GC_DWGCXX A LEFT JOIN XM_XMJBXX B ON A.XMBH=B.XMBH WHERE XMMC IS NOT NULL");
        if (!string.IsNullOrEmpty(txtProjectName.Text))
            _builder.AppendFormat(" AND B.XMMC LIKE '%{0}%'", txtProjectName.Text);
        if (!string.IsNullOrEmpty(txtItemName.Text))
            _builder.AppendFormat(" AND A.DWGCMC LIKE '%{0}%'", txtItemName.Text);
        if(ddlGCLB.SelectedValue!="0")
            _builder.AppendFormat(" AND B.XMLX = {0}", ddlGCLB.SelectedValue);
        this.Pager1.className = "XM_BaseInfo";
        this.Pager1.sql = _builder.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Show();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string XMLX = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "XMLX"));
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string XMLXMC = "其它";
            if (PrjEntItem.DicGCLB.ContainsKey(XMLX))
                XMLXMC = PrjEntItem.DicGCLB[XMLX];
            e.Item.Cells[4].Text = XMLXMC;
        }
    }
}