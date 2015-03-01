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
    int fMType = 28802;//审查人员安排(勘察) 
    int fMType_0 = 28803;//技术性审查

    int fMType2 = 30102;//审查人员安排(设计)
    int fMType_2 = 30103;//技术性审查 

    int fc = 0;
    int fc1 = 0;
    int fc2 = 0;

    int jc = 0;
    int jc1 = 0;
    int jc2 = 0;
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
                 join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId//技术 
                 join m in db.CF_Prj_Emp on l1.FId equals m.FAppId
                 where l1.FState == 6 && l2.FState > 1//技术性审查给过状态了
                 && ((l1.FManageTypeId == fMType && l2.FManageTypeId == fMType_0)
                 || (l1.FManageTypeId == fMType2 && l2.FManageTypeId == fMType_2))
                 && l2.FAppDate.GetValueOrDefault().Year == iYear
                 && l1.FBaseinfoId == l2.FBaseinfoId
                 && m.FEmpBaseInfo == empId
                 group m by new
                 {
                     l2.FManageTypeId,
                     FAppId = l1.FId,
                     FLinkId = l2.FId,
                     FState = l2.FState,
                     l2.FAppDate.GetValueOrDefault().Month
                 } into g
                 select new
                 {
                     FMType = g.Key.FManageTypeId,//业务类型 
                     FResult = db.CF_Prj_Emp.Where(t => t.FEmpBaseInfo == empId && t.FAppId == g.Key.FAppId).Select(t => t.FType).FirstOrDefault() == 1 ? g.Key.FState : db.CF_App_Idea.Where(t => t.FUserId == empId && t.FLinkId == g.Key.FLinkId).Select(t => t.FResultInt).FirstOrDefault(),//查询人员状态
                     FMonth = g.Key.Month //技术性审查办结月份 
                 }).ToList();

        ////查询审查人员的数据
        //var p2 = (from l1 in db.CF_App_List//安排
        //   join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId//技术 
        //   join m in db.CF_Prj_Emp on l1.FId equals m.FAppId
        //   join i in db.CF_App_Idea on l2.FId equals i.FLinkId//保存的意见
        //   where l1.FState == 6 && l2.FState > 1//技术性审查给过状态了 
        //   && (m.FType == 2 || m.FType == 3)
        //   && l2.FAppDate.GetValueOrDefault().Year == iYear
        //   && ((l1.FManageTypeId == fMType && l2.FManageTypeId == fMType_0)
        //   || (l1.FManageTypeId == fMType2 && l2.FManageTypeId == fMType_2))
        //   && l1.FBaseinfoId == l2.FBaseinfoId
        //   && i.FUserId == empId
        //   && m.FEmpBaseInfo == empId
        //   group m by new
        //   {
        //       l2.FManageTypeId,
        //       l2.FLinkId,
        //       l2.FAppDate.Value.Month,
        //       i.FResultInt//是否合格  
        //   } into g
        //   select new
        //       {
        //           FMType = g.Key.FManageTypeId,//业务类型
        //           g.Key.FLinkId,//业务
        //           FMonth = g.Key.Month,//技术性审查办结月份 
        //           FResult = g.Key.FResultInt//是否合格
        //       }).ToList();
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[]{
            new DataColumn("FMonth"),
            new DataColumn("F"),
            new DataColumn("F1"),
            new DataColumn("F2"),
            new DataColumn("J"),
            new DataColumn("J1"),
            new DataColumn("J2")  
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
            int iCount = p.Count(t => t.FMonth == i && t.FMType == fMType_0);
            fc += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, i, "F0", iCount);
            dr["F"] = sTemp;//参与项目数量
            iCount = p.Count(t => t.FMonth == i && t.FMType == fMType_2);
            jc += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, i, "J0", iCount);
            dr["J"] = sTemp;//参与项目数量
            /****************************************/
            iCount = p.Count(t => t.FMonth == i && t.FResult == 6 && t.FMType == fMType_0);
            fc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, i, "F6", iCount);
            dr["F1"] = sTemp;//合格数量 
            iCount = p.Count(t => t.FMonth == i && t.FResult == 6 && t.FMType == fMType_2);
            jc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, i, "J6", iCount);
            dr["J1"] = sTemp;//合格数量 
            /****************************************/
            iCount = p.Count(t => t.FMonth == i && t.FResult == 3 && t.FMType == fMType_0);
            fc2 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, i, "F3", iCount);
            dr["F2"] = sTemp;//不合格数量 
            iCount = p.Count(t => t.FMonth == i && t.FResult == 3 && t.FMType == fMType_2);
            jc2 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, i, "J3", iCount);
            dr["J2"] = sTemp;//合格数量 
            dt.Rows.Add(dr);
        }
        DG_List.DataSource = dt;
        DG_List.DataBind();
    }
    protected void ddlFYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void DG_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            object[] nums = new object[] { fc, jc, fc1, jc1, fc2, jc2 };
            string[] types = new string[] { "F0", "J0", "F6", "J6", "F3", "J3" };
            string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('PersonInfoPrj.aspx?FYear=" + ddlFYear.SelectedValue + "&FType={0}',700,550);\">{1}</a>";
            for (int i = 0; i < nums.Length; i++)
            {
                string sTemp = nums[i].ToString();
                if (sTemp != "0")
                    sTemp = string.Format(sUrl, types[i], sTemp);
                ((Literal)e.Item.FindControl("l_" + i + "")).Text = sTemp;
            }
        }
    }
}
