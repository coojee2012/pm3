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
using System.Text;
using System.Data;

public partial class KC_AppMain_AppSJGMList : System.Web.UI.Page
{
    int fMType1 = 287;//勘察文件审查合同备案  
    int fMType4 = 28801;//程序性审查(勘察) 
    int fMType5 = 28803;//技术性审查(勘察)
    int fMType6 = 290;//勘察文件审查备案

    int jMType1 = 300;//施工图设计文件审查合同备案  
    int jMType4 = 30101;//程序性审查(设计)
    int jMType5 = 30103;//技术性审查(设计) 
    int jMType6 = 305;//施工图设计文件备案

    int fc1 = 0;
    int fc1_1 = 0;
    decimal fc1_2 = 0;
    int fc4 = 0;
    int fc5 = 0;
    int fc6 = 0;
    int fc7 = 0;
    int fc8 = 0;

    int jc1 = 0;
    int jc1_1 = 0;
    decimal jc1_2 = 0;
    int jc4 = 0;
    int jc5 = 0;
    int jc6 = 0;
    int jc7 = 0;
    int jc8 = 0;

    int xc1 = 0;
    int xc1_1 = 0;
    decimal xc1_2 = 0;
    int xc4 = 0;
    int xc5 = 0;
    int xc6 = 0;
    int xc7 = 0;
    int xc8 = 0;
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
        string FBaseinfoID = CurrentEntUser.EntId;
        StringBuilder sb = new StringBuilder();
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[]{
            new DataColumn("FMonth"),
            new DataColumn("F1"),
            new DataColumn("F1_1"),
            new DataColumn("F1_2"), 
            new DataColumn("F4"),
            new DataColumn("F5"),
            new DataColumn("F6"),
            new DataColumn("F7"),
            new DataColumn("F8"),
            new DataColumn("J1"),
            new DataColumn("J1_1"),
            new DataColumn("J1_2"),
            new DataColumn("J4"), 
            new DataColumn("J5"),
            new DataColumn("J6"),
            new DataColumn("J7"),
            new DataColumn("J8"),
            new DataColumn("X1"),
            new DataColumn("X1_1"),
            new DataColumn("X1_2"),
            new DataColumn("X4"), 
            new DataColumn("X5"),
            new DataColumn("X6"),
            new DataColumn("X7"),
            new DataColumn("X8") 
        });
        int iYear = EConvert.ToInt(ddlFYear.SelectedValue);
        int iMonth = 12;
        if (iYear >= DateTime.Now.Year)
            iMonth = DateTime.Now.Month;
        //查询关联的业务数量
        //合同备案
        var v1 = (from t in db.CF_App_List
                  join p in db.CF_Prj_BaseInfo on t.FPrjId equals p.FId
                  where t.FToBaseinfoId == FBaseinfoID
                  && (t.FManageTypeId == fMType1 || t.FManageTypeId == jMType1)
                  && t.FReportDate.Value.Year == iYear
                  && t.FState > 0
                  group t by new
                  {
                      t.FPrjId,
                      p.FScale,
                      t.FReportDate.Value.Month
                  } into g
                  select new
                  {
                      PType = g.Key.FScale,
                      FMonth = g.Key.Month
                  }).ToList();
        //合同备案_受理
        var v1_1 = (from t in db.CF_App_List
                    join d in db.CF_Prj_Ent on t.FId equals d.FAppId
                    join p in db.CF_Prj_BaseInfo on t.FPrjId equals p.FId
                    where t.FToBaseinfoId == FBaseinfoID
                    && d.FBaseInfoId == FBaseinfoID
                    && (t.FManageTypeId == fMType1 || t.FManageTypeId == jMType1)
                    && t.FState == 6
                    && t.FAppDate.Value.Year == iYear
                    select new
                    {
                        t.FPrjId,
                        FMonth = t.FAppDate.Value.Month,
                        PType = p.FScale,
                        FMoney = d.FMoney
                    }).ToList();
        //其他业务
        var v4_5 = (from t in db.CF_App_List
                    join p in db.CF_Prj_BaseInfo on t.FPrjId equals p.FId
                    where t.FBaseinfoId == FBaseinfoID
                    && t.FState == 6
                    && (t.FManageTypeId == fMType4 || t.FManageTypeId == jMType4
                    || t.FManageTypeId == fMType5 || t.FManageTypeId == jMType5
                    || t.FManageTypeId == fMType6 || t.FManageTypeId == jMType6)
                    && t.FAppDate.Value.Year == iYear
                    select new
                    {
                        t.FPrjId,
                        FMonth = t.FAppDate.Value.Month,
                        PType = p.FScale,
                        FMType = t.FManageTypeId
                    }).ToList();

        //告知书|合格证 --- 技术性审查一步  已经打印 的
        var v7_8 = (from t in db.CF_App_List
                    join p in db.CF_Prj_BaseInfo on t.FPrjId equals p.FId
                    where t.FBaseinfoId == FBaseinfoID
                    && (t.FState == 6 || t.FState == 3)
                    && t.FIsSign == 1//已打印
                    && (t.FManageTypeId == fMType5 || t.FManageTypeId == jMType5)
                    && t.FAppDate.Value.Year == iYear
                    select new
                    {
                        t.FPrjId,
                        PType = p.FScale,
                        FMonth = t.FAppDate.Value.Month,
                        t.FState
                    }).ToList();
        string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('AppSJGMPrj.aspx?FYear=" + iYear + "&FMType={0}&FMonth={1}&FType={2}',700,550);\">{3}</a>";
        string sTemp = string.Empty;
        int iType1 = 2000401;//大型
        int iType2 = 2000402;//中型
        int iType3 = 2000403;//小型
        for (int i = 1; i <= iMonth; i++)
        {
            DataRow dr = dt.NewRow();
            dr["FMonth"] = i.ToString();
            int iCount = v1.Count(t => t.FMonth == i
                && t.PType == iType1);
            fc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1 + "," + jMType1, i, "F1", iCount);
            dr["F1"] = sTemp;//合同备案项目数量
            iCount = v1.Count(t => t.FMonth == i
                && t.PType == iType2);
            jc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1 + "," + jMType1, i, "J1", iCount);
            dr["J1"] = sTemp;//合同备案项目数量
            iCount = v1.Count(t => t.FMonth == i
                && t.PType == iType3);
            xc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1 + "," + jMType1, i, "X1", iCount);
            dr["X1"] = sTemp;//合同备案项目数量
            ////////////////////////////////////////// 
            iCount = v1_1.Where(t => t.FMonth == i
                && t.PType == iType1).GroupBy(t => t.FPrjId).Count();
            fc1_1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1 + "," + jMType1, i, "F1_1", iCount);
            dr["F1_1"] = sTemp;//受理项目数量 
            decimal d = EConvert.ToDecimal(v1_1.Where(t => t.FMonth == i
                && t.PType == iType1).Sum(t => t.FMoney)); //受理项目金额 
            dr["F1_2"] = d; fc1_2 += d;
            iCount = v1_1.Where(t => t.FMonth == i
                && t.PType == iType2).GroupBy(t => t.FPrjId).Count();
            jc1_1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1 + "," + jMType1, i, "J1_1", iCount);
            dr["J1_1"] = sTemp;//受理项目数量 
            d = EConvert.ToDecimal(v1_1.Where(t => t.FMonth == i
                && t.PType == iType2).Sum(t => t.FMoney)); //受理项目金额 
            dr["J1_2"] = d; jc1_2 += d;
            iCount = v1_1.Where(t => t.FMonth == i
              && t.PType == iType3).GroupBy(t => t.FPrjId).Count();
            xc1_1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1 + "," + jMType1, i, "X1_1", iCount);
            dr["X1_1"] = sTemp;//受理项目数量 
            d = EConvert.ToDecimal(v1_1.Where(t => t.FMonth == i
                && t.PType == iType3).Sum(t => t.FMoney)); //受理项目金额  
            dr["X1_2"] = d; xc1_2 += d;
            //////////////////////////////////////////
            iCount = v4_5.Where(t => t.FMonth == i
               && (t.FMType == fMType4 || t.FMType == jMType4)
               && t.PType == iType1).GroupBy(t => t.FPrjId).Count();
            fc4 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType4 + "," + jMType4, i, "F4", iCount);
            dr["F4"] = sTemp;//程序性审查 
            iCount = v4_5.Where(t => t.FMonth == i
                && (t.FMType == fMType4 || t.FMType == jMType4)
                && t.PType == iType2).GroupBy(t => t.FPrjId).Count();
            jc4 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType4 + "," + jMType4, i, "J4", iCount);
            dr["J4"] = sTemp;//程序性审查  
            iCount = v4_5.Where(t => t.FMonth == i
              && (t.FMType == fMType4 || t.FMType == jMType4)
              && t.PType == iType3).GroupBy(t => t.FPrjId).Count();
            xc4 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType4 + "," + jMType4, i, "X4", iCount);
            dr["X4"] = sTemp;//程序性审查  
            //////////////////////////////////////////
            iCount = v4_5.Where(t => t.FMonth == i
                && (t.FMType == fMType5 || t.FMType == jMType5)
                && t.PType == iType1).GroupBy(t => t.FPrjId).Count();
            fc5 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType5 + "," + jMType5, i, "F5", iCount);
            dr["F5"] = sTemp;//技术性审查  
            iCount = v4_5.Where(t => t.FMonth == i
                && (t.FMType == fMType5 || t.FMType == jMType5)
                && t.PType == iType2).GroupBy(t => t.FPrjId).Count();
            jc5 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType5 + "," + jMType5, i, "J5", iCount);
            dr["J5"] = sTemp;//技术性审查   
            iCount = v4_5.Where(t => t.FMonth == i
             && (t.FMType == fMType5 || t.FMType == jMType5)
             && t.PType == iType3).GroupBy(t => t.FPrjId).Count();
            xc5 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType5 + "," + jMType5, i, "X5", iCount);
            dr["X5"] = sTemp;//技术性审查   
            //////////////////////////////////////////
            iCount = v4_5.Where(t => t.FMonth == i
                && (t.FMType == fMType6 || t.FMType == jMType6)
                && t.PType == iType1).GroupBy(t => t.FPrjId).Count();
            fc6 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType6 + "," + jMType6, i, "F6", iCount);
            dr["F6"] = sTemp;//备案  
            iCount = v4_5.Where(t => t.FMonth == i
                && (t.FMType == fMType6 || t.FMType == jMType6)
                && t.PType == iType2).GroupBy(t => t.FPrjId).Count();
            jc6 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType6 + "," + jMType6, i, "J6", iCount);
            dr["J6"] = sTemp;//备案   
            iCount = v4_5.Where(t => t.FMonth == i
               && (t.FMType == fMType6 || t.FMType == jMType6)
               && t.PType == iType3).GroupBy(t => t.FPrjId).Count();
            xc6 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType6 + "," + jMType6, i, "X6", iCount);
            dr["X6"] = sTemp;//备案   
            //////////////////////////////////////////
            iCount = v7_8.Where(t => t.FMonth == i
                && t.FState == 6 && t.PType == iType1)
                .GroupBy(t => t.FPrjId).Count();
            fc7 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType5 + "," + jMType5, i, "F7", iCount);
            dr["F7"] = sTemp;//打印合格证  
            iCount = v7_8.Where(t => t.FMonth == i
                && t.FState == 6 && t.PType == iType2)
                .GroupBy(t => t.FPrjId).Count();
            jc7 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType5 + "," + jMType5, i, "J7", iCount);
            dr["J7"] = sTemp;//打印合格证    
            iCount = v7_8.Where(t => t.FMonth == i
               && t.FState == 6 && t.PType == iType3)
               .GroupBy(t => t.FPrjId).Count();
            xc7 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType5 + "," + jMType5, i, "X7", iCount);
            dr["X7"] = sTemp;//打印合格证    
            //////////////////////////////////////////
            iCount = v7_8.Where(t => t.FMonth == i
                && t.FState == 3 && t.PType == iType1)
                .GroupBy(t => t.FPrjId).Count();
            fc8 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType5 + "," + jMType5, i, "F8", iCount);
            dr["F8"] = sTemp;//打印告知书 
            iCount = v7_8.Where(t => t.FMonth == i
                && t.FState == 3 && t.PType == iType2)
                .GroupBy(t => t.FPrjId).Count();
            jc8 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType5 + "," + jMType5, i, "J8", iCount);
            dr["J8"] = sTemp;//打印告知书    
            iCount = v7_8.Where(t => t.FMonth == i
                && t.FState == 3 && t.PType == iType3)
                .GroupBy(t => t.FPrjId).Count();
            xc8 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType5 + "," + jMType5, i, "X8", iCount);
            dr["X8"] = sTemp;//打印告知书    
            dt.Rows.Add(dr);
        }
        DG_List.DataSource = dt;
        DG_List.DataBind();
    }

    //列表
    protected void DG_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void ddlFYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void DG_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            object[] nums = new object[] { fc1, jc1, xc1, fc1_1, jc1_1, xc1_1, fc1_2, jc1_2, xc1_2, fc4, jc4, xc4, fc5, jc5, xc5, fc6, jc6, xc6, fc7, jc7, xc7, fc8, jc8, xc8 };
            string[] types = new string[] { "F1", "J1", "X1", "F1_1", "J1_1", "X1_1", "F1_2", "J1_2", "X1_2", "F4", "J4", "X4", "F5", "J5", "X5", "F6", "J6", "X6", "F7", "J7", "X7", "F8", "J8", "X8" };
            string[] mtypes = new string[] { fMType1 + "," + jMType1, fMType1 + "," + jMType1, fMType1 + "," + jMType1, fMType4 + "," + jMType4, fMType5 + "," + jMType5, fMType6 + "," + jMType6, fMType5 + "," + jMType5, fMType5 + "," + jMType5 };
            string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('AppSJGMPrj.aspx?FYear=" + ddlFYear.SelectedValue + "&FMType={0}&FType={1}',700,550);\">{2}</a>";
            for (int i = 0; i < nums.Length; i++)
            {
                string sTemp = nums[i].ToString();
                if (!(i == 6 || i == 7 || i == 8) && sTemp != "0")
                {
                    sTemp = string.Format(sUrl, mtypes[i / 3], types[i], sTemp);
                }
                ((Literal)e.Item.FindControl("l_" + i + "")).Text = sTemp;
            }
        }
    }
}

