using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;
using Approve.Common;
using Approve.RuleCenter;
using System.Data;

public partial class KC_AppMain_PersonInfoList : System.Web.UI.Page
{
    int fMType = 28002;//见证人员安排-人员安排 
    int fMType_1 = 28004;//见证人员意见
    int fMType_0 = 28003;//见证报告备案

    int fc = 0;
    int fc1 = 0;
    int fc2 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            showInfo();
        }
    }
    ProjectDB db = new ProjectDB();
    void BindControl()
    {
        int iYear = DateTime.Now.Year;
        for (int i = 0; i < 10; i++)
        {
            string year = (iYear - i).ToString();
            ddlFYear.Items.Add(new ListItem(year, year));
        }
        ddlFYear.SelectedValue = iYear.ToString();
    }
    //显示
    private void showInfo()
    {
        int iYear = EConvert.ToInt(ddlFYear.SelectedValue);
        string empId = CurrentEmpUser.EmpId;

        var p = (from l1 in db.CF_App_List//安排
                 join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId//报告
                 join l3 in db.CF_App_List on l1.FLinkId equals l3.FLinkId//意见
                 join m in db.CF_Prj_Emp on l1.FId equals m.FAppId
                 where l1.FState == 6 && l2.FState == 6 && l3.FState == 6
                 && l2.FTime.Value.Year == iYear
                 && l1.FManageTypeId == fMType
                 && l2.FManageTypeId == fMType_0
                 && l3.FManageTypeId == fMType_1
                 && l1.FBaseinfoId == l2.FBaseinfoId
                 && m.FEmpBaseInfo == empId
                 group m by new
                 {
                     l2.FPrjId,
                     l2.FTime.Value.Month
                 } into g
                 select new
                 {
                     g.Key.FPrjId,//工程项目
                     FMonth = g.Key.Month //见证报告备案办结月份 
                 }).ToList();

        //查询见证人员的数据
        var p2 = (from l1 in db.CF_App_List//安排
                  join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId//报告
                  join l3 in db.CF_App_List on l1.FLinkId equals l3.FLinkId//意见
                  join m in db.CF_Prj_Emp on l1.FId equals m.FAppId
                  join i in db.CF_App_Idea on l3.FId equals i.FLinkId//保存的意见
                  where l1.FState == 6 && l2.FState == 6 && l3.FState == 6
                  && m.FType == 2
                  && l2.FTime.Value.Year == iYear
                  && l1.FManageTypeId == fMType
                  && l2.FManageTypeId == fMType_0
                  && l3.FManageTypeId == fMType_1
                  && l1.FBaseinfoId == l2.FBaseinfoId
                  && i.FUserId == empId
                  && m.FEmpBaseInfo == empId
                  group m by new
                  {
                      l2.FPrjId,
                      l2.FTime.Value.Month,
                      i.FResultInt//是否合格  
                  } into g
                  select new
                      {
                          g.Key.FPrjId,//工程项目
                          FMonth = g.Key.Month,//见证报告备案办结月份 
                          FResult = g.Key.FResultInt//是否合格
                      }).ToList();
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[]{
            new DataColumn("FMonth"),
            new DataColumn("FCount"),
            new DataColumn("FCount1"),
            new DataColumn("FCount2")  
        });
        int iMonth = 12;
        if (iYear >= DateTime.Now.Year)
            iMonth = DateTime.Now.Month;
        string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('PersonInfoPrj.aspx?FYear=" + iYear + "&FMonth={0}&FType={1}',700,550);\">{2}</a>";
        string sTemp = string.Empty;
        for (int i = 1; i <= iMonth; i++)
        {
            DataRow dr = dt.NewRow();
            dr["FMonth"] = i.ToString();
            int iCount = p.Where(t => t.FMonth == i).GroupBy(t => t.FPrjId).Count();
            fc += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, i, "0", iCount);
            dr["FCount"] = sTemp;//参与项目数量

            iCount = p2.Where(t => t.FMonth == i && t.FResult == 1).GroupBy(t => t.FPrjId).Count();
            fc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, i, "1", iCount);
            dr["FCount1"] = sTemp;//合格数量 

            iCount = p2.Where(t => t.FMonth == i && t.FResult == 2).GroupBy(t => t.FPrjId).Count();
            fc2 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, i, "1", iCount);
            dr["FCount2"] = sTemp;//不合格数量 
            dt.Rows.Add(dr);
        }
        DG_List.DataSource = dt;
        DG_List.DataBind();
    }

    //列表
    protected void DG_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells[0].ColumnSpan = 2;
            e.Row.Cells[0].Text = "合计：";
            e.Row.Cells[0].CssClass = "t_r t_bg";
            //显示数据
            object[] nums = new object[] { fc, fc1, fc2 };
            string[] types = new string[] { "0", "1", "2" };
            string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('PersonInfoPrj.aspx?FYear=" + ddlFYear.SelectedValue + "&FType={0}',700,550);\">{1}</a>";
            for (int i = 0; i < nums.Length; i++)
            {
                string sTemp = nums[i].ToString();
                if (sTemp != "0")
                    sTemp = string.Format(sUrl, types[i], sTemp);
                e.Row.Cells[i + 1].Text = sTemp;
            }
        }
    }
    protected void ddlFYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        showInfo();
    }
}
