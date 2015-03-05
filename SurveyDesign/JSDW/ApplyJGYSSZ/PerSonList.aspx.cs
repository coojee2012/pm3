using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class JSDW_ApplyJGYS_PerSonList : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private ProjectDB db = new ProjectDB();
    private RCenter rcJST = new RCenter("JST_XZSPBaseInfo");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfTypeId.Value = TypeId;
            if (TypeId == "4" || TypeId == "5")
            {
                ddlRYLX.Items.Insert(0, new ListItem() { Text = "", Value = "", Selected = true });
                ddlRYLX.Visible = false;
            }
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
                                        ZSYXQJSSJ,
                                        FZSJ,
                                        ZCZSH from [dbo].RY_RYZSXX  WHERE QYBM='{0}'", QYBM);
        if (!string.IsNullOrEmpty(txtPersonName.Text))
            _builder.AppendFormat(" AND XM like '%{0}%'", txtPersonName.Text);
        if (!string.IsNullOrEmpty(ddlRYLX.SelectedValue))
            _builder.AppendFormat(" AND ZSJB in ({0})", ddlRYLX.SelectedValue.Split('_')[0]);
        string sql = string.Format(@"select RYBH from YW_PERSON where COMPANYID='{0}'",Id);
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
            _builder.AppendFormat(" and RYZSXXID not in({0})", string.Join(",", list.ToArray()));
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
        get {
            return Request.QueryString["QYBM"];
        }
    }
    private string Id {
        get {
            return Request.QueryString["companyId"];
        }
    }
    private string TypeId {
        get {
            return Request.QueryString["typeId"];
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