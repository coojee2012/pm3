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


public partial class Government_main_Default : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["LookMSG"] = "NOREAD";//只看没读的消息
            showInfo();
            if (Session["PsID"] != null)
            { Response.Redirect("../expert/Default.aspx"); }
        }
    }

    //显示
    private void showInfo()
    {

        showAppList();


    }


    #region 显示业务

    //显示业务
    private void showAppList()
    {
        string DFMenuRoleId = String.IsNullOrEmpty(EConvert.ToString(Session["DFMenuRoleId"])) ? "1" : EConvert.ToString(Session["DFMenuRoleId"]);

        List<string> roleList = DFMenuRoleId.Split(',').ToList();
        var result1 =
      db.Menu.Where(t => t.FIsDis == 1 && t.FParentId == "45002").Union
      (
      from t in db.Menu
      join t1 in db.Menu on t.FNumber equals t1.FParentId
      where t1.FIsDis == 1 && t1.FIsDis == 1 && t.FParentId == "45002"
      select t1)
      .Union(
          from t in db.Menu
          join t1 in db.Menu on t.FNumber equals t1.FParentId
          join t2 in db.Menu on t1.FNumber equals t2.FParentId
          where t1.FIsDis == 1 && t1.FIsDis == 1 && t.FParentId == "45002"
          select t2
      )
      ;
        var result = from t in result1
                     where t.FIsDeleted == false && t.FIsDis == 1 && (t.FUrl.Contains("SGwaitForAppList.aspx") || t.FUrl.Contains("waitForAppList.aspx"))
                           && roleList.Any(x => t.FRoleId.Split(',').Contains(x))
                     group t by t.FUrl.ToLower() into g
                     select new { FName = g.Max(m => m.FName), FNumber = g.Max(m => m.FNumber), FUrl = g.Key };


        List<int> SystemIdList = new List<int>();
        List<int> TypeIdList = new List<int>();
        List<int> ManageTypeIdList = new List<int>();

        List<int> alSystemId = new List<int>();
        var AddressList = new[] { new { Name = "", Count = 0, FNumber = "", FUrl = "", FSystemId = 0, FTypeId = 0, FManageTypeId = 0 } }.Where(t => false).ToList();
        foreach (var item in result)
        {
            Uri baseUri = Request.Url;
            Uri absoluteUri = new Uri(baseUri, item.FUrl);
            NameValueCollection col = HttpUtility.ParseQueryString(absoluteUri.Query);


            //EConvert.ToInt(col["fsystemid"])
            //EConvert.ToInt(col["ftypeid"]) 

            if (!string.IsNullOrEmpty(col["fsystemid"]))
            {
                if (!SystemIdList.Contains(EConvert.ToInt(col["fsystemid"])))
                {
                    SystemIdList.Add(EConvert.ToInt(col["fsystemid"]));
                }
            }
            if (!string.IsNullOrEmpty(col["ftypeid"]))
            {
                if (!TypeIdList.Contains(EConvert.ToInt(col["ftypeid"])))
                {
                    TypeIdList.Add(EConvert.ToInt(col["ftypeid"]));
                }
            }
            if (!string.IsNullOrEmpty(col["FManageTypeId".ToLower()]))
            {
                string[] strManange = col["FManageTypeId".ToLower()].Split(',');
                for (int i = 0; i < strManange.Length; i++)
                {
                    if (!ManageTypeIdList.Contains(EConvert.ToInt(strManange[i])))
                    {
                        ManageTypeIdList.Add(EConvert.ToInt(strManange[i]));

                        AddressList.Add(
                        new
                        {
                            Name = item.FName,
                            Count = 0,
                            FNumber = item.FNumber,
                            FUrl = item.FUrl,
                            FSystemId = EConvert.ToInt(col["fsystemid"]),
                            FTypeId = EConvert.ToInt(col["ftypeid"]),
                            FManageTypeId = EConvert.ToInt(strManange[i])
                        }
                           );
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(col["fsystemid"]))
                {
                    alSystemId.Add(EConvert.ToInt(col["fsystemid"]));
                }
            }
            AddressList.Add(
             new
             {
                 Name = item.FName,
                 Count = 0,
                 FNumber = item.FNumber,
                 FUrl = item.FUrl,
                 FSystemId = EConvert.ToInt(col["fsystemid"]),
                 FTypeId = EConvert.ToInt(col["ftypeid"]),
                 FManageTypeId = EConvert.ToInt(col["FManageTypeId"])
             }
                );
        }
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("select * from ( ");
        sb.AppendLine("select ep.FId,er.FId erId,ep.FBaseInfoId,ep.FLinkId,ep.FEntName,ep.FEmpName,ep.FManageTypeId,ep.FListId,er.FTypeId,er.FRoleId,");
        sb.AppendLine(" ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,ep.FManageDeptId,");
        sb.AppendLine(" ep.FState,ep.FSeeState,ep.FSeeTime,");
        sb.AppendLine(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime,");
        sb.AppendLine(" er.fisQuali,ep.FLeadName,ep.FLeadId,ep.FEmpId,ep.FSystemId ");
        sb.AppendLine(" from CF_App_ProcessInstance ep inner join CF_App_ProcessRecord er");
        sb.AppendLine(" on ep.fId = er.FProcessInstanceID ");
        sb.AppendLine(" inner join ( ");
        sb.AppendLine(" select max(er.fid) fid from CF_App_ProcessInstance ep,CF_App_ProcessRecord er");
        sb.AppendLine(" where ep.fId = er.FProcessInstanceID ");
        sb.AppendLine(" and er.FRoleId in (" + Session["DFRoleId"] + ")");
        sb.AppendLine(" and ep.FManageDeptId like '" + Session["DFId"] + "%' ");

        if (SystemIdList.Count > 0)
        {
            sb.Append(" and (ep.FSystemId  in (" + string.Join(",", SystemIdList.Select(t => t.ToString()).ToArray()) + ")");
            foreach (var item in SystemIdList)
            {
                sb.Append(" or ep.FSystemId =" + item + " ");
            }
            foreach (var item in AddressList.Where(t => t.FSystemId == 0).GroupBy(t => t.FManageTypeId))
            {
                sb.Append(" or ep.FManageTypeId =" + item.Key + " ");
            }

            sb.AppendLine(" )");
        }
        else
        {
            if (ManageTypeIdList.Count > 0)
            {
                sb.Append(" or ep.FManageTypeId  in (" + string.Join(",", ManageTypeIdList.Select(t => t.ToString()).ToArray()) + ") )");
            }
        }

        if (TypeIdList.Count > 0)
        {
            sb.Append(" and er.ftypeid  in (" + string.Join(",", TypeIdList.Select(t => t.ToString()).ToArray()) + ")");
        }
        else
        {
            sb.Append(" and er.ftypeid  in (0)");
        }
        sb.AppendLine(" group by ep.flinkId,er.FMeasure)temp on temp.fid=er.fid where 1=1 ");
        sb.AppendLine(" and er.FRoleId in (" + Session["DFRoleId"] + ")");
        sb.AppendLine(" and ep.FManageDeptId like '" + Session["DFId"] + "%' ");

        sb.Append(" and (er.FMeasure=0 or er.FMeasure=1 or er.FMeasure=3 or er.FMeasure=4) "); //未审核、被打回(4)
        sb.AppendLine(" and ep.fstate<>2 ");

        sb.AppendLine(")tt ");
        ViewState["DFRoleId"] = Session["DFRoleId"];//审批角色 
        sb.AppendLine(" order by tt.fMeasure, tt.FReporttime desc,tt.FBaseInfoId");

        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();



        //查到有权限业务

        DataTable dt = rc.GetTable(sb.ToString());
        liCount.Text = dt.Rows.Count.ToString();
        var resultManage = from m in db.ManageType
                           join m1 in db.ManageType.GroupBy(t => t.FName).Select(g => g.Max(t => t.FID)) on m.FID equals m1
                           where alSystemId.Contains(m.FSystemId.Value) || ManageTypeIdList.Contains(m.FNumber.Value)
                           orderby m.FOrder
                           select new
                           {
                               m.FName,
                               m.FNumber,
                               Url =
                                   AddressList.Where(t => (t.FSystemId == m.FSystemId && t.FManageTypeId == 0) || t.FManageTypeId == m.FNumber).Select(t => t.FUrl).FirstOrDefault(),
                               Count = dt.Select(" FManageTypeId in (" + string.Join(",", db.ManageType.Where(t => t.FName == m.FName).Select(t => t.FNumber.ToString()).ToArray()) + ") ").Length
                           };

        repManageType.DataSource = resultManage;
        repManageType.DataBind();
    }


    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string pId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fLinkId"));
            string fState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fState"));
            string fMeasure = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fMeasure"));

            string fisQuali = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FisQuali"));
            string fAppResult = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFResult")); ;


            int ftypeid = 5;

            switch (fAppResult)
            {
                case "1":
                    e.Item.Cells[e.Item.Cells.Count - 3].Text = "通过";
                    break;

                case "3":
                    e.Item.Cells[e.Item.Cells.Count - 3].Text = "不通过";
                    break;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("FBaseInfoId,FState,FRoleId,FManageTypeId,fsystemid,FState");
            EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, sb.ToString(), "fid='" + pId + "'");
            if (ea == null)
            {
                EaProcessInstanceBackup backup = (EaProcessInstanceBackup)rc.GetEBase(EntityTypeEnum.EaProcessInstanceBackup, sb.ToString(), "fid='" + pId + "'");
                if (backup == null)
                {

                    return;
                }
                else
                {
                    ea = new EaProcessInstance();
                    ea.FBaseInfoId = backup.FBaseInfoId;
                    ea.FManageTypeId = backup.FManageTypeId;
                    ea.FSystemId = backup.FSystemId;
                }
            }
            string fbid = ea.FBaseInfoId;
            string faid = fLinkId;
            string fmid = ea.FManageTypeId;
            string fManageTypeId = ea.FManageTypeId;
            string FEmpName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEmpName"));


            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string sScript = "showApproveWindow('../AppQualiInfo/ShowAppInfo1.aspx?pid=" + pId + "&lid=" + fLinkId + "',830,500)";
            sScript = "";

            string frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + fbid + "'");
            string fsystemid = ea.FSystemId;




            object fentname = DataBinder.Eval(e.Item.DataItem, "FEntName");
            //查询查询地址
            string sUrl = "";

            //建设单位
            string FLeadName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLeadName"));
            sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=100 ");
            //得到FLeadId的企业ID的SystemId
            if (fmid == "294")
            {

                FLeadName = fentname.ToString();
                sUrl += "?fbid=" + fbid;
                e.Item.Cells[3].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + sUrl + "',980,450)\"  >" + fentname + "</a>";

            }
            else
            {
                string fLeadId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLeadId"));
                if (string.IsNullOrEmpty(fLeadId))//如果建设单位ID是空
                {
                    string fPrjId = db.CF_App_List.Where(t => t.FId == fLinkId).Select(t => t.FPrjId).FirstOrDefault();
                    if (!string.IsNullOrEmpty(fPrjId))
                    {
                        fLeadId = db.CF_Prj_BaseInfo.Where(t => t.FId == fPrjId).Select(t => t.FBaseinfoId).FirstOrDefault();
                    }
                }
                if (!string.IsNullOrEmpty(fLeadId))//如果建设单位ID是空
                {
                    sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=100 ");
                    sUrl += "?fbid=" + fLeadId;
                    e.Item.Cells[3].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + sUrl + "',980,450)\"  >" + FLeadName + "</a>";
                }

            }

            string fUrl = rc.getMTypeQurl(ea.FManageTypeId); ;
            sScript = "openWinNew('" + fUrl + "?sysid=" + fsystemid + "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + Session["DFUserId"].ToString() + "')";

            e.Item.Cells[1].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\" >";

            e.Item.Cells[1].Text += FEmpName + "</a>";


            e.Item.Cells[2].Text = rc.getMTypeName(fManageTypeId);
            //审批地址
            string fAppUrl = rc.getMTypeAurl(fmid);
            fAppUrl += "?lid=" + fLinkId + "&p=" + fMeasure + "&ftypeid=" + ftypeid;

            if (fMeasure.Trim() == "0" || fMeasure == "3") //未审核
            {
                e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + fAppUrl + "',980,780)\" >";
                e.Item.Cells[e.Item.Cells.Count - 2].Text += "办理</a>";
                if (fState.Trim() == "2")
                {

                }
                else
                {

                    e.Item.Cells[e.Item.Cells.Count - 3].Text = "未审批";
                }
            }

            else if (fMeasure.Trim() == "1") //已经填写意见
            {

                e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + fAppUrl + "',980,780)\"  >"
                      + "办理</a>";
            }
            if (e.Item.Cells[e.Item.Cells.Count - 3].Text.IndexOf("不") > -1)
            {
                e.Item.Cells[e.Item.Cells.Count - 3].Text = "<font color='#ff0000'>" + e.Item.Cells[e.Item.Cells.Count - 3].Text + "</font>";
            }
            //查询该项目是否变更 
            if (!string.IsNullOrEmpty(fLinkId))
            {
                string fPrjId = db.CF_App_List.Where(t => t.FId == fLinkId).Select(t => t.FPrjId).FirstOrDefault();
                if (!string.IsNullOrEmpty(fPrjId))
                {
                    var prjBG = db.CF_Prj_BaseInfo.Where(t => t.FId == fPrjId)
                        .Select(t => new { t.FBGTime, t.FCount })
                        .FirstOrDefault();
                    if (prjBG != null && prjBG.FCount > 0)
                    {
                        e.Item.Cells[1].Text += "<br/><font color='#000000;'>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")</font>";
                    }
                }
            }
        }
    }
    #endregion



    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
