using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyJGYS_ProjectFile_ChooseQY : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Show();
        }
    }
    private void Show()
    {
        StringBuilder _builder = new StringBuilder();
        _builder.Append(@"select c.QYZSID,a.QYBM,a.JGDM,a.RegAdrProvinceName+''+a.RegAdrCityName+''+RegAdrCountryName SD,C.ZSBH,A.QYMC,DBO.MergeQYZZXX(C.QYZSID)        ZZMC,a.FRDB,a.LXR,a.LXDH,a.QYXXDZ
                                from dbo.QY_JBXX a 
                                left join dbo.QY_QYZSXX c on a.QYBM = c.QYBM and C.SFYX =1 where 1=1");
        string QYLX = GetQYLX();
        if (!string.IsNullOrEmpty(QYLX))
            _builder.AppendFormat(" AND a.QYLXBM in ({0})", QYLX);
        if (!string.IsNullOrEmpty(txtQYName.Text))
            _builder.AppendFormat(" AND a.QYMC like '%{0}%'", txtQYName.Text);
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
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
        }
    }
    private string GetQYLX()
    {
        if (string.IsNullOrEmpty(TypeId))
            return string.Empty;
        string result = string.Empty;
        if (TypeId == "1" || TypeId == "2" || TypeId == "3") //施工
            result = "101,108";
        else if (TypeId == "4")//勘察单位
            result = "140,141,155";
        else if (TypeId == "5")//设计单位
            result = "155";
        else if (TypeId == "6")//监理单位
            result = "125";
        else if (TypeId == "7")//施工图审查
            result = "145";
        else if (TypeId == "8")
            result = "120,121"; //招投标代理
        return result;
    }
    private string TypeId {
        get {
            return Request.QueryString["typeId"];
        }
    }
}