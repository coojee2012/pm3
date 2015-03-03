using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Approve.RuleCenter;
using System.Configuration;
using System.Data;

public partial class JSDW_ApplyJGYS_ProjectFile_ChooseManagePerson : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private static string PreDatabase = ConfigurationManager.AppSettings["PerSon"];
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
        _builder.AppendFormat(@"select  a.RYZSXXID,
                                        a.XM,
                                        case a.[XB] when 0 then '男' when 1 then '女' end as XB,
                                        a.ZCZY,
                                        a.ZSJB,
                                        a.ZCZSH,
                                        a.ZSYXQJSSJ,
                                        a.FZSJ,
                                        b.[QYMC] 
                                from [dbo].[RY_RYZSXX] a left join QY_JBXX b on a.QYBM=b.QYBM WHERE 1=1 ");
        if (!string.IsNullOrEmpty(QYBM))
            _builder.AppendFormat(" and a.QYBM='{0}'", QYBM);
        else
            _builder.AppendFormat(" and b.QYLXBM in ({0})", QYLXBM);
        if (!string.IsNullOrEmpty(txtName.Text))
            _builder.AppendFormat(" AND a.XM like '%{0}%'", txtName.Text);
        //string sql = string.Format(@"select RYBH from XM_SGTKCSJMX where SGTSCID='{0}'", SGTId);
        //DataTable table = rc.GetTable(sql);
        //List<string> list = new List<string>();
        //if (table != null && table.Rows.Count > 0)
        //{
        //    for (int i = 0; i < table.Rows.Count; i++)
        //    {
        //        list.Add("'" + table.Rows[i]["RYBH"] + "'");
        //    }
        //}
        //if (list.Count > 0)
        //    _builder.AppendFormat(" and RYZSXXID not in({0})", string.Join(",", list.ToArray()));
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
    private string QYBM
    {
        get
        {
            return Request.QueryString["QYBM"];
        }
    }
    private string QYLXBM {
        get {
            return Request.QueryString["QYLXBM"];
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            //string RYBH = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "RYBH"));
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            //CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            //box.Attributes["id"] = "span" + box.ClientID;
            //box.Attributes["RYBH"] = RYBH;
        }
    }
}