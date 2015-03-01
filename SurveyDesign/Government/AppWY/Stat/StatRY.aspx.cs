using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppWY_Stat_StatRY : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    public string statSql = "";
    private StringBuilder sbSql = new StringBuilder();
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
        StringBuilder sbWhere = new StringBuilder();
        int iLeftCount = 6;
        string strDFID = Session["DFID"].ToString();
        string strWhereMa = "";

        switch (Session["DFLevel"].ToString())
        {
            case "1":    //省级用户
                iLeftCount = 4;
                strWhereMa = " And len(FNumber)=4";
                break;
            case "2":    //市级用户
                sbWhere.Append("where jb.XMSD like '" + strDFID + "%'");
                break;
            case "3":  //县级用户
                sbWhere.Append("where jb.XMSD = '" + strDFID + "'");
                break;
        }
        sbSql.Append("select isnull(JYCount,0) JYCount,isnull(TJCount,0) TJCount,isnull(JSCount,0) JSCount,isnull(CWCount,0) CWCount,isnull(ZXCount,0) ZXCount,isnull(Total,0) Total,isnull((TecCount-TecCountDel),0) YZCCount,MD.FName DeptName from");
        sbSql.Append("(select count(case fposition when '6001001' then '' when '6002001' then '' end) JYCount,count(case fposition when '6001004' then '' when '6002004' then '' end) TJCount,count(case fposition when '6001003' then '' when '6002003' then '' end) JSCount,count(case fposition when '6001002' then '' when '6002002' then '' end) CWCount,count(case fposition when '6003001' then '' end) ZXCount,count(fPosition) as Total,count(case fTechnical when '-1' then '' when '' then '' end) TecCountDel,count(fTechnical) TecCount,ShortXMSD from");
        sbSql.Append("(select ry.*,xm.xmsd,left(xm.xmsd,"+iLeftCount+") ShortXMSD from wy_ry_jbxx ry left join wy_xm_jbxx xm on ry.xmbh=xm.xmbh where xm.xmsd like '51%') as T1 group by ShortXMSD) as T2 ");
        sbSql.Append(" Right Outer Join (Select FName,FNumber From Cf_Sys_ManageDept Where FNumber Like '" + strDFID + "%'" + strWhereMa);
        //statSql = "select count(case fposition when '6001001' then '' when '6002001' then '' end) JYCount,count(case fposition when '6001004' then '' when '6002004' then '' end) TJCount,count(case fposition when '6001003' then '' when '6002003' then '' end) JSCount,count(case fposition when '6001002' then '' when '6002002' then '' end) CWCount,count(case fposition when '6003001' then '' end) ZXCount,count(fPosition) as Total from wy_ry_jbxx";
        //string sqlYZC = "select count(fTechnical) YZCCount from wy_ry_jbxx where fTechnical is not null and fTechnical<>'-1' and fTechnical<>''";
        if (Session["DfLevel"].ToString() != "3")
            sbSql.Append(" And FNumber != '" + strDFID + "'");
        sbSql.Append(") MD On MD.FNumber = T2.ShortXMSD");
        try
        {
            //Response.Write(sbSql);
            DataTable dtCount = rc.GetTable(sbSql.ToString());
            if (dtCount.Rows.Count > 0)
            {
                DG_List.DataSource = dtCount;
                DG_List.DataBind();

            }

        }
        catch { }
        
    }

    protected void DG_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string sql = "select sum(Total) as TotalSum,sum(YZCCount) as YZCCountSum,sum(JYCount) as JYCountSum,sum(TJCount) as TJCountSum,sum(JSCount) as JSCountSum,sum(CWCount) as CWCountSum,sum(ZXCount) as ZXCountSum from(" + sbSql.ToString() + ") as temp";
        DataTable dt = rc.GetTable(sql);
        if (e.Item.ItemType == ListItemType.Footer && dt.Rows.Count == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                ((Literal)e.Item.FindControl("l_" + i + "")).Text = dt.Rows[0][i].ToString();
            }
        }
    }
}