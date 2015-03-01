using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Approve.Common;
using Approve.RuleCenter;


public partial class Government_AppMain_expertIdear : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["fappid"] != null)
                t_YWBM.Value = Request["fappid"];
            bindInfo();
        }
    }
    public void bindInfo()
    {
        string sql = string.Format(@" select e.*,ps.ExpertName,ps.UnitName,ps.Industry,ps.FirstProfessial,ps.WorkCerName
                            ,ps.UnitTelephone
                            from YW_GF_Expert e
                            left join LINKER_95.dbCenterSC.dbo.CF_Pro_PsExpertInfo ps on ps.psid=e.psid
                            where e.Fappid='" + t_YWBM.Value + "'");
        JustAppInfo_List.DataSource = sh.GetTable(sql);
        JustAppInfo_List.DataBind();
    }
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fappid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "Fappid"));
            string psid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "PsID"));
            string isend = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "isEnd"));
            string result = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "Fresult"));
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
            string sUrl = "../expert/reviewDetail.aspx?FLinkId=" + fappid + "&psid=" + psid + "&isEnd=" + isend;
            if (isend == "1") isend = "通过";
            else if (isend == "-1") isend = "不通过";
            e.Item.Cells[e.Item.Cells.Count - 6].Text = "<a href='javascript:void(0)' onclick=\"javascript:showAddWindow('" + sUrl + "',700,800);\" >" + isend + "</a>";

        }
    }
}