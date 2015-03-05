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


public partial class Government_main_zgDesktop : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    private string sFMenuRoleId = string.Empty;


    public DataRow[] JJRows = null; //接件
    public DataRow[] SCRows = null; //审查
    public DataRow[] SPRows = null; //审批
    public DataTable DT = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string sDFUserId = Session["DFUserId"].ToString();
            sFMenuRoleId = rc.GetSignValue("select FMenuRoleId from cf_sys_userright where fuserid='" + sDFUserId + "'");

            string sDFId = Session["DFId"].ToString();
            DataTable dt = GetData(sDFId);
            JJRows = dt.Select("LX=1");
            SCRows = dt.Select("LX=2");
            SPRows = dt.Select("LX=3");


            DT = GetDataList(sDFId);
        }
    }

    private DataTable GetDataList(string sFManageDeptId)
    {
        string sql = string.Format(@"
select 
  case FTypeId when '1' then '接件数'
               when '2' then '审查数'
			   when '3' then '审批数' end as MC,
   (sum(A1)+sum(A2)+sum(A3)+sum(A4)+sum(A5)+sum(A6)+sum(A7)+sum(A8)+sum(A9)) as AA,
   sum(A1) as A1,sum(A2) as A2,sum(A3) as A3,sum(A4) as A4,sum(A5) as A5
  ,sum(A6) as A6,sum(A7) as A7,sum(A8) as A8,sum(A9) as A9
from (
  select FTypeId,A1,A2,A3,A9,A4,A6,A5,A8,A7 from (
	   select (case er.FTypeId when 1 then 1 when 10 then 2 when 5 then 3 end) as FTypeId
		 ,case when ep.FManageTypeId in (4050,4060) then 1 else 0 end as A1
		 ,case when ep.FManageTypeId in (5050,5060) then 1 else 0 end as A2
		 ,case when ep.FManageTypeId in (6060,6070) then 1 else 0 end as A3
		 ,case when ep.FManageTypeId in (7070,7080) then 1 else 0 end as A9
		 ,case when ep.FManageTypeId in (8080) then 1 else 0 end as A5
		 ,case when ep.FManageTypeId in (11222) then 1 else 0 end as A7
		 ,case when ep.FManageTypeId in (11221) then 1 else 0 end as A6
		 ,case when ep.FManageTypeId in (11223,11224,11225) then 1 else 0 end as A8
		 ,case when ep.FManageTypeId in (11230,11231,11232,11234,11235,11237) then 1 else 0 end as A4
		 ,ep.FManageTypeId
	   from CF_App_ProcessInstance ep,CF_App_ProcessRecord er,CF_App_List l
	   where ep.fId = er.FProcessInstanceID and ep.FLinkId=l.FId  
	  and er.FMeasure=5 
	  and ep.FManageDeptId = '51' 
	  and ep.fsystemid=1122
	  union all
	  select (case er.FTypeId when 1 then 1 when 10 then 2 when 5 then 3 else 3 end) as FTypeId
		 ,case when ep.FManageTypeId in (4050,4060) then 1 else 0 end as A1
		 ,case when ep.FManageTypeId in (5050,5060) then 1 else 0 end as A2
		 ,case when ep.FManageTypeId in (6060,6070) then 1 else 0 end as A3
		 ,case when ep.FManageTypeId in (7070,7080) then 1 else 0 end as A9
		 ,case when ep.FManageTypeId in (8080) then 1 else 0 end as A5
		 ,case when ep.FManageTypeId in (11222) then 1 else 0 end as A7
		 ,case when ep.FManageTypeId in (11221) then 1 else 0 end as A6
		 ,case when ep.FManageTypeId in (11223,11224,11225) then 1 else 0 end as A8
		 ,case when ep.FManageTypeId in (11230,11231,11232,11234,11235,11237) then 1 else 0 end as A4
		 ,ep.FManageTypeId
	   from CF_App_ProcessInstanceBackup ep,CF_App_ProcessRecordBackup er,CF_App_List l
	   where ep.fId = er.FProcessInstanceID and ep.FLinkId=l.FId
	  and er.FMeasure=5
	  and ep.FManageDeptId = '{0}' 
	  and ep.fsystemid=1122
  ) as aa,CF_SYS_DesktopMenu a,cf_sys_menu M
  where A.FNumber=M.FNumber and aa.FTypeId=a.Ftype
     and (a.FManageTypeId like '%''' + cast(aa.FManageTypeId as varchar(50)) + '''%')
	 and dbo.getRoleid(FRoleId,'{1}')=1
  union all
  select 1,0,0,0,0,0,0,0,0,0
  union all
  select 2,0,0,0,0,0,0,0,0,0
  union all
  select 3,0,0,0,0,0,0,0,0,0
  ) A
 group by FTypeId
 order by FTypeId
", sFManageDeptId, sFMenuRoleId);
        DataTable dt = rc.GetTable(sql);
        return dt;
    }


    private DataTable GetData(string sFManageDeptId)
    {       
        string sql = string.Format(@"
select A.FType as LX,M.FUrl as URL,A.FName as MC
 ,(
   select count(1) 
   from CF_App_ProcessInstance ep,CF_App_ProcessRecord er,CF_App_List l
   where ep.fId = er.FProcessInstanceID and ep.FLinkId=l.FId
   and (case er.FTypeId when 1 then 1 when 10 then 2 when 5 then 3 end)=A.FType
  and er.FMeasure=0
  and (A.FManageTypeId like '%''' + cast(ep.FManageTypeId as varchar(50)) + '''%')
  and ep.FManageDeptId = '{0}' 
  and ep.fsystemid=1122 and ep.fstate<>2
  ) as SL
from CF_SYS_DesktopMenu A,cf_sys_menu M
where A.FNumber=M.FNumber 
  and dbo.getRoleid(FRoleId,'{1}')=1
order by (select top 1 FOrder from cf_sys_menu aa where aa.FNumber=M.FParentId),m.FOrder
", sFManageDeptId,sFMenuRoleId);
        DataTable dt = rc.GetTable(sql);
        return dt;
    }

}
