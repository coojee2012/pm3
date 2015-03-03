using Approve.Common;
using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyXZYJS_ChooseBuildUnit : System.Web.UI.Page
{
    private Share sh = new Share();
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
        _builder.Append("select XMBM,XMBH,XMMC,JSDW,JSDWDZ,JSGM from XM_XMInfo WHERE 1=1");
        if (!string.IsNullOrEmpty(JSDWBM))
            _builder.AppendFormat(" AND JSDWBM='{0}'", JSDWBM);
        if (!string.IsNullOrEmpty(txtProjectName.Text))
            _builder.AppendFormat(" AND XMMC like '%{0}%'", txtProjectName.Text);
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
}