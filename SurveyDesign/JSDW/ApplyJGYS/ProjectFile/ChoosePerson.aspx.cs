using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class JSDW_ApplyJGYS_ProjectFile_ChoosePerson : System.Web.UI.Page
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
        _builder.AppendFormat(@"select  RYZSXXID,
                                        XM,
                                        case [XB] when 0 then '男' when 1 then '女' end as XB,
                                        ZCZY,
                                        ZSJB,
                                        ZSYXQJSSJ,
                                        FZSJ,
                                        ZCZSH from [dbo].RY_RYZSXX  WHERE QYBM='{0}'", QYBM);
        if (!string.IsNullOrEmpty(txtName.Text))
            _builder.AppendFormat(" AND XM like '%{0}%'", txtName.Text);

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
            _builder.AppendFormat(" and RYZSXXID not in({0})", string.Join(",", list.ToArray()));
        _builder.Append(" order by XM");
        this.Pager1.className = "JST_XZSPBaseInfo";
        this.Pager1.sql = _builder.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
//    private void Show()
//    {


//        StringBuilder _builder = new StringBuilder();
//        _builder.AppendFormat(@"select [QYBM]
//                              ,[RYBH]
//                              ,[RYLBBM]
//                              ,[RYLBMC]
//                              ,[XM]
//                              ,case [XB] when 0 then '男' when 1 then '女' end as XB
//                              ,[CSRQ]
//                              ,[ZJLXBM]
//                              ,[ZJLXMC]
//                              ,[ZJBH]
//                              ,[ZW]
//                              ,[ZCBM]
//                              ,[ZC]
//                              ,[LXDH]
//                              ,[SJHM]
//                              ,[XLBM]
//                              ,[DZYX]
//                              ,[ZGXL]
//                              ,[SXZY]
//                              ,[BYSJ]
//                              ,[BYXX]
//                              ,[XCSZY]
//                              ,[CYZK]
//                              ,[RYZKBM]
//                              ,[RYZKMC]
//                              ,[SFGMSB]
//                              ,[SBH]
//                              ,[ZPURL]
//                              ,[ZCLXBM]
//                              ,[ZCLXMC]
//                              ,[ZCZYMC]
//                              ,[ZGZSH]
//                              ,[ZCZSH]
//                              ,[ZCDJBM]
//                              ,[ZCDJMC]
//                              ,[ZCZSFZSJ]
//                              ,[ZCZSYXQ]
//                              ,[BZ] from {0}.[dbo].[QY_QYCYRYXX] WHERE RYBH not in(select RYBH from XM_SGTKCSJMX where SGTSCID='{1}')",PreDatabase,SGTId);
//        if (!string.IsNullOrEmpty(QYBM))
//            _builder.AppendFormat(" AND QYBM='{0}'", QYBM);
//        if (!string.IsNullOrEmpty(txtName.Text))
//            _builder.AppendFormat(" AND XM like '%{0}%'", txtName.Text);
//        this.Pager1.className = "dbCenter";
//        this.Pager1.sql = _builder.ToString();
//        this.Pager1.pagecount = 20;
//        this.Pager1.controltopage = "DG_List";
//        this.Pager1.controltype = "DataGrid";
//        this.Pager1.dataBind();
//    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Show();
    }
    private string QYBM {
        get {
            return Request.QueryString["QYBM"];
        }
    }
    private string SGTId {
        get {
            return Request.QueryString["SGTId"];
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string RYBH = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "RYZSXXID"));
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["RYZSXXID"] = RYBH;
        }
    }
}