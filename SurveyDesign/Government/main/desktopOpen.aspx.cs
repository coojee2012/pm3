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


public partial class Government_main_desktopOpen : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    public DataTable DT = null;
    private string FNumber = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //ViewState["LookMSG"] = "NOREAD";//只看没读的消息
            //showInfo();
            if (Session["PsID"] != null)
            { Response.Redirect("../expert/Default.aspx"); }

            FNumber = Request["FNumber"];
            if (string.IsNullOrEmpty(FNumber)) FNumber = Session["DFId"].ToString();

            DT = GetData(FNumber);

        }
    }

    private DataTable GetData(string sFRoleId)
    {        
//        string sql = string.Format(@"
//select FUpDeptId,isnull(FName,FUpDeptId) as FName
//  ,(SUM(A&SL)+SUM(B&SL)+SUM(C&SL)+SUM(D&SL)+SUM(E&SL)+SUM(F&SL)+SUM(G&SL)+SUM(H&SL)+SUM(I&SL)) as AA
//  ,(SUM(A&CB)+SUM(B&CB)+SUM(C&CB)+SUM(D&CB)+SUM(E&CB)+SUM(F&CB)+SUM(G&CB)+SUM(H&CB)+SUM(I&CB)) as AB
//  ,SUM(A&SL) as A1,SUM(A&CB) as A2 --选址
//  ,SUM(B&SL) as B1,SUM(B&CB) as B2 --用地
//  ,SUM(C&SL) as C1,SUM(C&CB) as C2 --工程
//  ,SUM(D&SL) as D1,SUM(D&CB) as D2 --招标
//  ,SUM(E&SL) as E1,SUM(E&CB) as E2 --项目报建申报
//  ,SUM(F&SL) as F1,SUM(F&CB) as F2 --质量监督备案
//  ,SUM(G&SL) as G1,SUM(G&CB) as G2 --安全监督备案
//  ,SUM(H&SL) as H1,SUM(H&CB) as H2 --施工许可
//  ,SUM(I&SL) as I1,SUM(I&CB) as I2 --竣工备案
//from (
//	select FManageTypeId,left(FUpDeptId,len({0})+2) as FUpDeptId
//	,Case when FManageTypeId in (4050,4060) then 1 else 0 end as A 
//	,Case when FManageTypeId in (5050,5060) then 1 else 0 end as B
//	,Case when FManageTypeId in (6060,6070) then 1 else 0 end as C
//	,Case when FManageTypeId in (11235,11227,11234,11232,11230) then 1 else 0 end as D
//	,Case FManageTypeId when 8080 then 1 else 0 end as E
//	,Case FManageTypeId when 11221 then 1 else 0 end as F
//	,Case FManageTypeId when 11222 then 1 else 0 end as G
//	,Case when FManageTypeId in (11223,11225,11224) then 1 else 0 end as H
//	,Case when FManageTypeId in (7070,7080) then 1 else 0 end as I
//	,Case when FState in (1,6) then 1 else 0 end as SL
//	,Case when FState in (6) then 1 else 0 end as CB
//	 from cf_app_list
//	 where isnull(FUpDeptId,0)>0 and left(FUpDeptId,len({0}))={0}) YW
//Left Join CF_Sys_ManageDept BM on YW.FUpDeptId=BM.FNumber
//Group By FUpDeptId,FName
//Order by FUpDeptId
//", sFRoleId);
        string sLen = (sFRoleId.Length + 2).ToString();
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

   
}
