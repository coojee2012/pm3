using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve;
using Approve.RuleCenter;
using Approve.EntityBase;

public partial class ACZJ_main_selfAssessmentList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { bindInfo(); }
    }
    public void bindInfo()
    {
        string sql = string.Format(@"select xm.XMBH,x.DWGCBH,x.DWGCMC,'' XKZBH,xm.JSDW,xm.XMSD,xm.JSXZ,xm.XMDZ
            ,x.DWGCZJ HTJG,null KGRQ,'' ZT
            ,case when exists(select * from XM_JGBAXX xx where xx.GCBH=x.DWGCBH) then '已竣工'
            else case when exists(select * from XM_JGBAXX_SZ sz where sz.GCBH=x.DWGCBH) then '已竣工' else '未竣工' end 
            end isJG,s.JD,s.DJ,s.RQ,s.fid 
            from dbCenter.dbo.YW_selfAssessment s
            left join GC_DWGCXX x on s.GCBH=x.DWGCBH
            left join XM_XMJBXX xm on xm.XMBH=x.XMBH ");
        if (!string.IsNullOrEmpty(t_GCMC.Text.Trim()))
        { sql += " and x.DWGCMC like '%" + t_GCMC.Text.Trim() + "%' "; }
        if (!string.IsNullOrEmpty(t_JSDW.Text.Trim()))
        { sql += " and xm.JSDW like '%" + t_JSDW.Text.Trim() + "%' "; }
        if (!string.IsNullOrEmpty(t_SD.fNumber))
        { sql += " and xm.XMSD like '" + t_SD.fNumber + "%' "; }
        if (!string.IsNullOrEmpty(t_XKZ.Text.Trim()))
        { sql += " and XKZBH like '%" + t_XKZ.Text.Trim() + "%' "; }
        sql += " order by s.RQ desc ";
        this.Pager1.className = "JST_XZSPBaseInfo";
        this.Pager1.sql = sql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bindInfo();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[7].Text = rc.GetSignValue(EntityTypeEnum.EsManageDept, "FName", "Fnumber='" + e.Item.Cells[7].Text + "'");
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fid"));
            if (int.Parse(e.Item.Cells[1].Text) > 0)
                e.Item.Cells[1].Text = "<a href=\"javascript:showAddWindow('selfAssessmentDetail.aspx?fid=" + fid + "',800,600);\" >" + e.Item.Cells[1].Text + "</a>";
        }
    }
}