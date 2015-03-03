using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class JSDW_ApplyXZYJS_ChooseProject : System.Web.UI.Page
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
    private void Show()
    {
        StringBuilder _builder = new StringBuilder();
        //_builder.Append("select a.fid,a.ProjectName,a.JSDW,a.JSDWDZ,a.Contacts,b.FName,a.ConstrScale from TC_Prj_Info a left join CF_Sys_Dic b on a.ProjectType=b.fnumber WHERE 1=1");
        //if (!string.IsNullOrEmpty(CurrentEntUser.EntId))
        //    _builder.AppendFormat(" AND FJSDWID='{0}'", CurrentEntUser.EntId);
        //if (!string.IsNullOrEmpty(txtProjectName.Text))
        //    _builder.AppendFormat(" AND a.ProjectName like '%{0}%'", txtProjectName.Text);
        //if (!string.IsNullOrEmpty(txtUnit.Text))
        //    _builder.AppendFormat(" AND a.JSDW like '%{0}%'", txtUnit.Text);
        //if(!string.IsNullOrEmpty(ddlGCLX.SelectedValue))
        //    _builder.AppendFormat(" AND b.FNumber = '{0}'", ddlGCLX.SelectedValue);

        
        _builder.Append("select a.XMBH,a.XMMC,a.JSDW,a.JSDWDZ,a.JSGM,A.XMLX from XM_XMJBXX a where a.XMMC IS NOT NULL");
        if (!string.IsNullOrEmpty(txtProjectName.Text))
            _builder.AppendFormat(" AND a.XMMC like '%{0}%'", txtProjectName.Text);
        if (!string.IsNullOrEmpty(txtUnit.Text))
            _builder.AppendFormat(" AND a.JSDW like '%{0}%'", txtUnit.Text);
        if (ddlGCLB.SelectedValue != "0")
            _builder.AppendFormat(" AND a.XMLX = {0}", ddlGCLB.SelectedValue);
        this.Pager1.className = "XM_BaseInfo";
        this.Pager1.sql = _builder.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
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
            ddlGCLB.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "0" });
            ddlGCLB.SelectedValue = "1";
        }
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