using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Configuration;
using System.Text;

public partial class JSDW_ApplyJGYS_ProjectFile_ChooseQYSG : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
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
        string JSDWBM = CurrentEntUser.UserId;
        _builder.Append(@"select a.QYBM,a.JGDM,a.RegAdrProvinceName+''+a.RegAdrCityName+''+RegAdrCountryName SD,C.ZSBH,D.ZSBH AXZBH,a.QYMC,b.ZZMC,a.FRDB,a.LXR,a.LXDH,a.QYXXDZ 
                            from QY_JBXX a 
                            left join QY_QYZZXX b on a.QYBM=b.QYBM AND B.SFZX=1 and ISNULL(B.SFZD,0)=0
                            left join QY_QYZSXX c on a.QYBM = c.QYBM and c.ZSLXBM!=150 
                            left join QY_QYZSXX D on a.QYBM = D.QYBM AND D.ZSLXBM=150 AND D.SFYX=1
                            where C.SFYX=1");
        //string QYLX = GetQYLX();
        //if (!string.IsNullOrEmpty(QYLX))
        _builder.Append(" AND a.QYLXBM in (101,180)");
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
    private string TypeId
    {
        get
        {
            return Request.QueryString["typeId"];
        }
    }
}