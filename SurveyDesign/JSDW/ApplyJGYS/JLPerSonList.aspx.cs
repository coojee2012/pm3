using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using ProjectData;
using System.Configuration;
using System.Text;

public partial class JSDW_ApplyJGYS_JLPerSonList : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private ProjectDB db = new ProjectDB();
    private static readonly string PrePerSon = ConfigurationManager.AppSettings["PerSon"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Show();
        }
    }
    private void Show()
    {
        StringBuilder _builder = new StringBuilder();
        _builder.AppendFormat(@"select  RYZSXXID,
                                        XM,
                                        case [XB] when 0 then '男' when 1 then '女' end as XB,
                                        ZCZY,
                                        ZSJB,
                                        ZCZSH from {0}.[dbo].RY_RYZSXX  WHERE QYBM='{1}'", PrePerSon, QYBM);
        if (!string.IsNullOrEmpty(txtPersonName.Text))
            _builder.AppendFormat(" AND XM like '%{0}%'", txtPersonName.Text);
        if (!string.IsNullOrEmpty(ddlRYLX.SelectedValue))
            _builder.AppendFormat(" AND ZSJB in ({0})", ddlRYLX.SelectedValue);
        _builder.AppendFormat(" and RYZSXXID not in(select RYBH from YW_PERSON where COMPANYID='{0}')", Id);
        _builder.Append(" order by XM");
        this.Pager1.className = "dbCenter";
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
    private string QYBM
    {
        get
        {
            return Request.QueryString["QYBM"];
        }
    }
    private string Id
    {
        get
        {
            return Request.QueryString["companyId"];
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string RYZSXXID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "RYZSXXID"));
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["RYZSXXID"] = RYZSXXID;
        }
    }
}