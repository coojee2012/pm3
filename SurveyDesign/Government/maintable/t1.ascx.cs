using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Approve.RuleCenter;
using Approve.EntityBase;
using System.Data.SqlClient;
using System.Collections;

public partial class Government_maintable_t1 : System.Web.UI.UserControl
{
    RCenter rc = new RCenter();
    public DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        showInfo();
        this.Page.ClientScript.RegisterStartupScript(GetType(), "jj", "<script>showtb('" + hidd_n.Value + "');</script>");
    }

    private void showInfo()
    {

        string FSystemId = Request.QueryString["sysId"];// "150";//系统类型
        string mType = hidd_n.Value;//业务Number
        StringBuilder sb = new StringBuilder();
        /*
        * cf_App_ProcessRecord表 FMeasure 字段说明：
        * 0：刚报上来的
        * 1：已填写意见的
        * 3：已打下回级
        * 4：被上级打回
        * 5：已上报
        */
        sb.Append(" select 0 IsBackUp,ep.FEmpId,ep.FLinkId,ep.FState,er.FID,er.FSubFlowID,ep.FProcessId,er.FMeasure,ep.FEmpId,ep.FLinkId,ep.FManageTypeId,");
        sb.Append(" Convert(varchar(100),er.FReportTime,23) FReportTime,Convert(varchar(100),convert(datetime,er.FTime),23) FTime  ");
        sb.Append(" from cf_App_ProcessInstance ep ");
        sb.Append(" inner join cf_App_ProcessRecord er ");
        sb.Append(" on ep.FId=er.FProcessInstanceId  ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"] + ") ");
        sb.Append(" and ep.FManageDeptId ='" + Session["DFId"] + "' ");
        sb.Append(" and ep.FSystemId='" + FSystemId + "' ");
        sb.Append(" union ");
        sb.Append(" select 1 IsBackUp,ep.FEmpId,ep.FLinkId,ep.FState,er.FID,er.FSubFlowID,ep.FProcessId,er.FMeasure,ep.FEmpId,ep.FLinkId,ep.FManageTypeId, ");
        sb.Append(" Convert(varchar(100),er.FReportTime,23) FReportTime,Convert(varchar(100),convert(datetime,er.FTime),23) FTime  ");
        sb.Append(" from cf_App_ProcessInstanceBackup ep ");
        sb.Append(" inner join cf_App_ProcessRecordBackup er ");
        sb.Append(" on ep.FId=er.FProcessInstanceId  ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"] + ") ");
        sb.Append(" and ep.FManageDeptId ='" + Session["DFId"] + "' ");
        sb.Append(" and ep.FSystemId='" + FSystemId + "' ");
        dt = rc.GetTable(sb.ToString());



        //找出是省级还是市级
        string FLevel = EConvert.ToString(Session["DFLevel"]);


        //找出企业类型下所有业务
        sb.Remove(0, sb.Length);
        sb.Append("select FName,FNumber,FID from CF_Sys_ManageType ");
        sb.Append("where fmtypeid='" + getMtypeId(FSystemId) + "' and isnull(fisdeleted,0)=0 ");
        sb.Append("and FSystemId=@FSystemId ");
        sb.Append("and FNumber not in (186,182)");
        sb.Append("order by FOrder ");
        DataTable dtAppType = rc.GetTable(sb.ToString(), new SqlParameter("@FSystemId", FSystemId));
        rep_AppType.DataSource = dtAppType;
        rep_AppType.DataBind();


        rep_Table.DataSource = dtAppType;
        rep_Table.DataBind();





        sb.Remove(0, sb.Length);
        sb.Append("select count(*) from (");
        sb.Append("select count(*)fcount from cf_App_ProcessInstance ep ");
        sb.Append(" inner join cf_App_ProcessRecord er ");
        sb.Append(" on ep.FId=er.FProcessInstanceId and ep.FState=1 ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"] + ") ");
        sb.Append(" and ep.FManageDeptId like '" + Session["DFId"] + "%' ");
        sb.Append(" and ep.FSystemId='" + FSystemId + "'");
        sb.Append(" and er.FMeasure in (0,1,4)  group by ");
        if (FSystemId == "220")
            sb.Append(" ep.FEmpId ");
        else
            sb.Append(" ep.FLinkId ");
        sb.Append(")t");

        int iCount = rc.GetSQLCount(sb.ToString());

        lit_Count.Text = iCount.ToString();
    }

    private string getMtypeId(string sysId)
    {
        string str = "";
        switch (sysId)
        {
            case "150"://安全
                str = "101";
                break;
            case "175"://质量
                str = "240";
                break;
            case "220"://三类人员
                str = "220";
                break;
            case "135":// 
                str = "180";
                break;
            case "183":// 
                str = "460";
                break;
            default: 
                str = sysId;
                break;
        }
        return str;
    }



    //显示 (隐藏按钮)
    protected void btn_show_Click(object sender, EventArgs e)
    {
        showInfo();
    }


    //业务列表
    protected void rep_Table_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            string FSystemId = Request.QueryString["sysId"]; //系统类型
            string mType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
            string FLevel = EConvert.ToString(Session["DFLevel"]);    //管理部门级别

            Repeater rep_appCount = e.Item.FindControl("rep_appCount") as Repeater;
            if (rep_appCount != null)
            {
                StringBuilder sb = new StringBuilder();


                #region 统计各步骤业务情况

                //找出各业务在当前级别下的流程步骤
                SortedList sl = new SortedList();
                sl.Add("FManageTypeId", mType);
                sl.Add("FLevel", FLevel);
                sb.Append("select s.FID,s.FName stepName,'' DayUpCount,'' DayAppCount,'' WeekUpCount,'' WeekAppCount,'' AppCount,'' NoAppCount ");
                sb.Append("from CF_App_SubFlow s ");
                sb.Append("inner join CF_App_ManageType m on s.fprocessid=m.fprocessid ");
                sb.Append("where m.FManageTypeId=@FManageTypeId and s.FLevel=@FLevel   ");
                sb.Append("and isnull(s.FIsDeleted,0)=0 and isnull(m.FIsDeleted,0)=0  ");
                sb.Append("order by s.FOrder ");
                DataTable dtSubFlowCount = rc.GetTable(sb.ToString(), rc.ConvertParameters(sl));



                //分别统计各个步骤下的业务办理情况
                for (int i = 0; i < dtSubFlowCount.Rows.Count; i++)
                {
                    string subflowID = dtSubFlowCount.Rows[i]["FID"].ToString();
                    DateTime dtime = DateTime.Now;

                    //今日上报
                    dtSubFlowCount.Rows[i]["DayUpCount"] = dt.Select("FManageTypeId='" + mType + "' and FSubFlowID='" + subflowID + "' and FReportTime='" + EConvert.ToShortDateString(dtime) + "'").Count();

                    //今日办理 
                    dtSubFlowCount.Rows[i]["DayAppCount"] = dt.Select("FManageTypeId='" + mType + "' and FSubFlowID='" + subflowID + "' and FTime='" + EConvert.ToShortDateString(dtime) + "' and FMeasure in(3,5)").Count();

                    //本周上报
                    dtSubFlowCount.Rows[i]["WeekUpCount"] = dt.Select("FManageTypeId='" + mType + "' and FSubFlowID='" + subflowID + "' and FReportTime>='" + getThisSunDay() + "'").Count();

                    //本周办理
                    dtSubFlowCount.Rows[i]["WeekAppCount"] = dt.Select("FManageTypeId='" + mType + "' and FSubFlowID='" + subflowID + "' and FTime='" + getThisSunDay() + "' and FMeasure in(3,5)").Count();

                    //总办理
                    dtSubFlowCount.Rows[i]["AppCount"] = dt.Select("FManageTypeId='" + mType + "' and FSubFlowID='" + subflowID + "' and FMeasure in(3,5)").Count();

                    //总未办理 
                    dtSubFlowCount.Rows[i]["NoAppCount"] = dt.Select("FState=1 and FManageTypeId='" + mType + "' and FSubFlowID='" + subflowID + "' and IsBackUp=0 and FMeasure in(0,1,4)").Count();

                }


                rep_appCount.DataSource = dtSubFlowCount;
                rep_appCount.DataBind();

                #endregion

            }
        }
    }


    /// <summary>
    /// 得到当天前最新的最期天
    /// </summary>
    /// <returns></returns>
    private string getThisSunDay()
    {
        string str = "";
        DateTime dtime = DateTime.Now;
        for (int i = 0; i < 7; i++)
        {
            if (dtime.AddDays(-i).DayOfWeek == DayOfWeek.Sunday)
            {
                str = EConvert.ToShortDateString(dtime.AddDays(-i));
            }
        }
        return str;
    }
}
