using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Text;
using System.Data;

public partial class JSDW_ApplyJGYS_ProjectFile_ChooseSGTPerson : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
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
        _builder.AppendFormat(@"select a.RYZSXXID,
                                        a.XM,
                                        case a.[XB] when 0 then '男' when 1 then '女' end as XB,
                                        a.ZCZY,
                                        a.ZSJB,
                                        a.ZCZSH,
                                        a.ZSYXQJSSJ,
                                        a.FZSJ,
                                        b.[QYMC]
                              FROM [dbo].[RY_RYZSXX] a left join QY_JBXX b on a.QYBM = b.QYBM WHERE b.QYLXBM in (140,141,145,150)");
        if (!string.IsNullOrEmpty(txtName.Text))
            _builder.AppendFormat(" AND a.XM like '%{0}%'", txtName.Text);

        string sql = string.Format(@"select RYBH from XM_SGTKCSJMX where SGTSCID='{0}'", SGTId);
        DataTable table = rc.GetTable(sql);
        List<string> list = new List<string>();
        if (table != null && table.Rows.Count > 0)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add("'" + table.Rows[i]["RYBH"] + "'");
            }
        }
        if (list.Count > 0)
            _builder.AppendFormat(" and a.RYZSXXID not in({0})", string.Join(",", list.ToArray()));
        _builder.Append(" order by XM");
        this.Pager1.className = "JST_XZSPBaseInfo";
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
    private string SGTId
    {
        get
        {
            return Request.QueryString["SGTId"];
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