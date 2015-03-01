using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppWY_Stat_StatRY : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    public string statSql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }

    }
    protected void btnStat_Click(object sender, EventArgs e)
    {

    }
    protected void btnOut_Click(object sender, EventArgs e)
    {

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {

    }
    //显示数据
    protected void showInfo()
    {
        statSql = "select count(case fposition when '6001001' then '' when '6002001' then '' end) JYCount,count(case fposition when '6001004' then '' when '6002004' then '' end) TJCount,count(case fposition when '6001003' then '' when '6002003' then '' end) JSCount,count(case fposition when '6001002' then '' when '6002002' then '' end) CWCount,count(case fposition when '6003001' then '' end) ZXCount,count(fPosition) as Total from yw_wy_ry_jbxx";
        string sqlYZC = "select count(fTechnical) YZCCount from yw_wy_ry_jbxx where fTechnical is not null and fTechnical<>'-1' and fTechnical<>''";
        
        try
        {
            string YZCStr="";
            DataTable dtCount = rc.GetTable(statSql);
            YZCStr = rc.GetSignValue(sqlYZC);
            if (dtCount.Rows.Count == 1&&YZCStr.Trim()!="")
            {
                dtCount.Columns.Add("YZCCount");
                dtCount.Rows[0]["YZCCount"] = YZCStr;
                DG_List.DataSource = dtCount;
                DG_List.DataBind();

            }

        }
        catch { }
        
    }
    protected void DG_List_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void DG_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
        //if (e.Item.ItemType == ListItemType.ItemTemplate && dt.Rows.Count == 1)
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        ((Literal)e.Item.FindControl("l_" + i + "")).Text = dt.Rows[0][i].ToString();
        //    }
        //}
        
        //for (int i = 0; i < DG_List.Items.Count; i++)
        //{
        //    cbx = (CheckBox)DG_List.Items[i].Cells[0].Controls[1];
        //    if (cbx.Checked)
        //    {
        //        sb.Append("'" + dg_List.Items[i].Cells[7].Text + "',");
        //    }

        //}
    }
}