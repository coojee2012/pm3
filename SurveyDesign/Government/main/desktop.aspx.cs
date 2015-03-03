using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;
using Tools;
using System.Text;
using System.Data;
using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.RuleApp;
using Approve.EntityCenter;
using System.Collections.Specialized;
using System.Collections;
using NJSWebApp;


public partial class Government_main_desktop : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    public DataTable DT = null;
    public DataRow ZJGZ_Row = null;
    public string DFId = string.Empty;
    public string sPieData = string.Empty;
    public string FName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //ViewState["LookMSG"] = "NOREAD";//只看没读的消息
            //showInfo();
            string sDFRoleId = Session["DFRoleId"].ToString();
            string sFID = Session["DFUserId"].ToString();
            string sfroleid = rc.GetSignValue("select froleid from CF_Sys_UserRight where FUserID='" + sFID + "'");
            if (!string.IsNullOrEmpty(sfroleid))
            {
                sfroleid = "," + sfroleid + ",";
                if (sfroleid.IndexOf(",8808,") == -1)
                {
                    Response.Redirect("zgDesktop.aspx");
                    Response.End();
                }
            }
            DFId = Session["DFId"].ToString();
            DT = GetData(DFId);

            FName = GetManageDeptName();

            ZJGZ_Row = GetZJGCData(DFId).Rows[0];
            sPieData = JKCFlowPlatform.Utility.Transform.ToJsonString(getXMData(DFId));
        }
    }

    private string GetManageDeptName()
    {
        string sSql = "select FName from CF_Sys_ManageDept where FNumber='" + DFId + "'";
        return rc.GetSignValue(sSql);
    }

    private DataTable GetData(string sFRoleId)
    {
        string sLen = (sFRoleId.Length+2).ToString();
        string sql = string.Format(@"
select BM.FNumber as FUpDeptId,isnull(FName,BM.FNumber) as FName
  ,(isnull(SUM(A&SL),0)+isnull(SUM(B&SL),0)+isnull(SUM(C&SL),0)+isnull(SUM(D&SL),0)+isnull(SUM(E&SL),0)
  +isnull(SUM(F&SL),0)+isnull(SUM(G&SL),0)+isnull(SUM(H&SL),0)+isnull(SUM(I&SL),0)) as AA
  ,(isnull(SUM(A&CB),0)+isnull(SUM(B&CB),0)+isnull(SUM(C&CB),0)+isnull(SUM(D&CB),0)+isnull(SUM(E&CB),0)
  +isnull(SUM(F&CB),0)+isnull(SUM(G&CB),0)+isnull(SUM(H&CB),0)+isnull(SUM(I&CB),0)) as AB
  ,isnull(SUM(A&SL),0) as A1,isnull(SUM(A&CB),0) as A2 --选址
  ,isnull(SUM(B&SL),0) as B1,isnull(SUM(B&CB),0) as B2 --用地
  ,isnull(SUM(C&SL),0) as C1,isnull(SUM(C&CB),0) as C2 --工程
  ,isnull(SUM(D&SL),0) as D1,isnull(SUM(D&CB),0) as D2 --招标
  ,isnull(SUM(E&SL),0) as E1,isnull(SUM(E&CB),0) as E2 --项目报建申报
  ,isnull(SUM(F&SL),0) as F1,isnull(SUM(F&CB),0) as F2 --质量监督备案
  ,isnull(SUM(G&SL),0) as G1,isnull(SUM(G&CB),0) as G2 --安全监督备案
  ,isnull(SUM(H&SL),0) as H1,isnull(SUM(H&CB),0) as H2 --施工许可
  ,isnull(SUM(I&SL),0) as I1,isnull(SUM(I&CB),0) as I2 --竣工备案
from CF_Sys_ManageDept BM
left join (
	select FManageTypeId,left(FUpDeptId,len({0})+2) as FUpDeptId
	,Case when FManageTypeId in (4050,4060) then 1 else 0 end as A 
	,Case when FManageTypeId in (5050,5060) then 1 else 0 end as B
	,Case when FManageTypeId in (6060,6070) then 1 else 0 end as C
	,Case when FManageTypeId in (11235,11227,11234,11232,11230) then 1 else 0 end as D
	,Case FManageTypeId when 8080 then 1 else 0 end as E
	,Case FManageTypeId when 11221 then 1 else 0 end as F
	,Case FManageTypeId when 11222 then 1 else 0 end as G
	,Case when FManageTypeId in (11223,11225,11224) then 1 else 0 end as H
	,Case when FManageTypeId in (7070,7080) then 1 else 0 end as I
	,Case when FState in (1,6) then 1 else 0 end as SL
	,Case when FState in (6) then 1 else 0 end as CB
	 from cf_app_list
	 where isnull(FUpDeptId,0)>0 and left(FUpDeptId,len({0}))={0}
) YW on YW.FUpDeptId=BM.FNumber
where left(BM.FNumber,len({0}))={0} and len(BM.FNumber) <= {1} and left(BM.FNumber,3)<> 519
Group By BM.FNumber,FName
Order by BM.FNumber
", sFRoleId, sLen);
        DataTable dt = rc.GetTable(sql);
        return dt;
    }

    private DataTable GetZJGCData(string sDFId)
    {
        string sql = string.Format(@"select SUM(NUM) AS AA
  ,SUM(Case when XMLX=1 then NUM else 0 end) A1 
  ,SUM(Case when XMLX=2 then NUM else 0 end) A2
  ,SUM(Case when XMLX=99 then NUM else 0 end) A3
  ,SUM(Case when XMLX=1 then GM else 0 end) B1 
  ,SUM(Case when XMLX=2 then GM else 0 end) B2
  ,SUM(Case when XMLX=99 then GM else 0 end) B3
  ,SUM(Case when XMLX=1 then ZTZ else 0 end) C1 
  ,SUM(Case when XMLX=2 then ZTZ else 0 end) C2
  ,SUM(Case when XMLX=99 then ZTZ else 0 end) C3
from (
select XMLX,Count(*) as NUM,cast(sum(XMZTZ)/10000 as numeric(32,2)) ZTZ 
,cast(sum(case when isnumeric(isnull(JSGM,'0'))=1 then cast(JSGM as numeric(32,2))/10000.0 else 0.0 end) as numeric(38,2)) as GM
from XM_XMJBXX XM,GC_DWGCXX GC
where XM.XMBH=GC.XMBH and XMLX in (1,2,99)
and not Exists(select 1 from XM_JGBAXX where GCBH=GC.DWGCBH)
and not Exists(select 1 from XM_JGBAXX_SZ where GCBH=GC.DWGCBH)
and left(isnull(XMSD,0),len({0}))={0}
group by XMLX) A", sDFId);
        DataSet ds = new DataSet();
        SQLServerDALHelper.ExecuteSQLDataSet(ref ds, "XM_XMJBXX", sql, SqlConType.XMBaseInfo);
        return ds.Tables[0];
    }

    private DataTable getXMData(string sDFId)
    {
        string sSql = string.Format(@"select 
case XMLX when '1' then '房屋建筑' when '2' then '市政' else '其它' end as name
,Count(*) as y
from XM_XMJBXX XM,GC_DWGCXX GC
where XM.XMBH=GC.XMBH and XMLX in (1,2,99)
and left(isnull(XMSD,0),len({0}))={0}
and not Exists(select 1 from XM_JGBAXX where GCBH=GC.DWGCBH)
and not Exists(select 1 from XM_JGBAXX_SZ where GCBH=GC.DWGCBH)
group by XMLX", sDFId);
        DataSet ds = new DataSet();
        SQLServerDALHelper.ExecuteSQLDataSet(ref ds, "XM_XMJBXX", sSql, SqlConType.XMBaseInfo);
        return ds.Tables[0];
    }

}
