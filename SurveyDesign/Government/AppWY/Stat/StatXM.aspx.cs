using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppWY_Stat_StatGS : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    public string statSql = "";
    private StringBuilder sbSql = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showDept();
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
    //绑定数据
    protected void showDept()
    {
        if (Session["DFId"] != null)
        {
            string sql = "select fname,flevel from cf_sys_managedept where fnumber='" + Session["DFId"].ToString() + "'";
            try
            {
                DataTable dt = rc.GetTable(sql);
                if (dt.Rows.Count == 1)
                {
                    t_DeptName.Text = dt.Rows[0]["fname"].ToString();
                    hidCurDeptLevel.Value = dt.Rows[0]["flevel"].ToString();
                }
            }
            catch { }
        }
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

        sbSql.Append("Select isnull(XMCount,0) As XMCount,IsNull(ZZCount,0) AS ZZCount,IsNull(SZCount,0) As SZCount,IsNull(BGCount,0) As BGCount,IsNull(LZFCount,0) As LZFCount,IsNull(GZFCount,0) As GZFCount,IsNull(XJFCount,0) As XJFCount,IsNull(JSFCount,0) As JSFCount,IsNull(ZDMJSum,0) As ZDMJSum,IsNull(JZMJSum,0) As JZMJSum,MD.FName As DeptName From ");
        sbSql.Append("(Select COUNT(FID) XMCount,ShortXMSD,COUNT(case XMZLX when '200010101' then '' end) as ZZCount,COUNT(case XMZLX when '200010110' then '' end) as SZCount,COUNT(case XMZLX when '200010104' then '' end) as BGCount,COUNT(case HsTypeID when '02' then '' end) as LZFCount,COUNT(case HsTypeID when '04' then '' end) as GZFCount,COUNT(case HsTypeID when '03' then '' end) as XJFCount,COUNT(case HsTypeID when '05' then '' end) as JSFCount,SUM(ZDMJ) As ZDMJSum,SUM(JZMJ) As JZMJSum From ");
        sbSql.Append("(Select jb.FID,jb.XMSD,left(jb.XMSD," + iLeftCount + ") ShortXMSD,HsTypeID,XMZLX,JZMJ,ZDMJ From WY_XM_JBXX jb Left Outer Join WY_XM_KZXX on jb.XMBH = WY_XM_KZXX.XMBH Where XMSD Like '" + strDFID + "%') As T1 Group By ShortXMSD) As T2 ");
        sbSql.Append(" Right Outer Join (Select FName,FNumber From Cf_Sys_ManageDept Where FNumber Like '" + strDFID + "%'"+strWhereMa);
        if (Session["DfLevel"].ToString() != "3")
            sbSql.Append(" And FNumber != '" + strDFID + "'");
        sbSql.Append(") MD On MD.FNumber = T2.ShortXMSD");
        //Response.Write(sbSql.ToString());
        DataTable dt = rc.GetTable(sbSql.ToString());
        if (dt.Rows.Count > 0)
        {
            DG_List.DataSource = dt;
            DG_List.DataBind();

        }
    }

    protected void DG_List_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void DG_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string sql = "select sum(XMCount) as XMTotal,sum(ZZCount) as ZZSum,sum(SZCount) as SZSum,sum(BGCount) as BGSum,sum(LZFCount) as LZFSum,sum(GZFCount) as GZFSum,sum(XJFCount) as XJFSum,sum(JSFCount) as JSFSum,sum(JZMJSum) as JZMJTotal,sum(ZDMJSum) as ZDMJTotal from(" + sbSql.ToString() + ") as temp";
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