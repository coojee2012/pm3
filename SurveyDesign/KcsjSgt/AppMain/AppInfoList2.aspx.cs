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

public partial class KC_AppMain_AppInfoList2 : System.Web.UI.Page
{
    int fMType1 = 287;//勘察文件审查合同备案   
    int fMType2 = 28803;//技术性审查(勘察) 
    int fMType3 = 28802;//人员安排(勘察)
    int fMType4 = 290;//备案(勘察)

    int jMType1 = 300;//施工图设计文件审查合同备案   
    int jMType2 = 30103;//技术性审查(设计)  
    int jMType3 = 30102;//人员安排(设计) 
    int jMType4 = 305;//备案(设计)

    int fc1 = 0;
    decimal fc1_1 = 0;
    int fc4 = 0;
    int fc5 = 0;

    int jc1 = 0;
    decimal jc1_1 = 0;
    int jc4 = 0;
    int jc5 = 0;
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
            new DataColumn("F4"),
            new DataColumn("F5"), 
            new DataColumn("J1"),
            new DataColumn("J1_1"), 
            new DataColumn("J4"), 
            new DataColumn("J5") 
        });
        int iYear = EConvert.ToInt(ddlFYear.SelectedValue);
        int iMonth = 12;
        if (iYear >= DateTime.Now.Year)
            iMonth = DateTime.Now.Month;
        //审查项目数（+审查次数+违反工程建设标准强制性条文数）
        //合同备案
        var v1 = (from t in db.CF_App_List//合同备案
                  join t2 in db.CF_App_List on t.FLinkId equals t2.FLinkId//技术性
                  join t3 in db.CF_App_List on t.FLinkId equals t3.FLinkId//备案
                  join b in db.CF_Prj_Ent on t.FLinkId equals b.FAppId//金额
                  where t.FState == 6 && t2.FState == 6 && t3.FState == 6
                  && ((t.FManageTypeId == fMType1 && t2.FManageTypeId == fMType2
                  && t3.FManageTypeId == fMType4)
                  || (t.FManageTypeId == jMType1 && t2.FManageTypeId == jMType2
                  && t3.FManageTypeId == jMType4))
                  && (db.CF_App_Idea.Where(i => i.FLinkId == t3.FId)
                  .OrderByDescending(i => i.FTime).Select(i => i.FResultInt)
                  .FirstOrDefault() == 1)//并且备案结果同意
                  && b.FBaseInfoId == FBaseinfoID
                  && t.FToBaseinfoId == FBaseinfoID
                  && t2.FBaseinfoId == FBaseinfoID
                  && t3.FAppDate.Value.Year == iYear
                  group t by new
                  {
                      t.FPrjId,
                      t.FLinkId,
                      t3.FAppDate.Value.Month,
                      t.FManageTypeId,
                      t.FReportCount,
                      b.FMoney
                  } into g
                  select new
                  {
                      FMonth = g.Key.Month,//月份
                      FMType = g.Key.FManageTypeId,//业务类型
                      FCount = g.Key.FReportCount,//审查次数
                      FWQ = (from n in db.CF_App_List
                             join n1 in db.CF_App_List
                             on n.FLinkId equals n1.FLinkId
                             join m in db.CF_Prj_Emp on n1.FId equals m.FAppId
                             join i in db.CF_App_Idea
                             on n.FId equals i.FLinkId
                             where n.FLinkId == g.Key.FLinkId
                              && ((n.FManageTypeId == fMType2
                              && n1.FManageTypeId == fMType3)
                              || (n.FManageTypeId == jMType2
                              && n1.FManageTypeId == jMType3))
                              && n.FState == 6 && n1.FState == 6
                              && n.FBaseinfoId == n1.FBaseinfoId
                              && n.FBaseinfoId == FBaseinfoID
                              && m.FType > 1 && m.FEmpBaseInfo == i.FUserId
                              && i.FOrder > 0
                             select 1).Count() > 0,//违反强条数>0
                      g.Key.FMoney//合同备案金额 
                  }).ToList();
        string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('AppInfoPrj2.aspx?FYear=" + iYear + "&FMType={0}&FMonth={1}&FType={2}',700,550);\">{3}</a>";
        string sTemp = string.Empty;
        for (int i = 1; i <= iMonth; i++)
        {
            DataRow dr = dt.NewRow();
            dr["FMonth"] = i.ToString();
            int iCount = v1.Count(t => t.FMonth == i && t.FMType == fMType1);
            fc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1, i, "F", iCount);
            dr["F1"] = sTemp;//审查项目数
            iCount = v1.Count(t => t.FMonth == i && t.FMType == jMType1);
            jc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, jMType1, i, "J", iCount);
            dr["J1"] = sTemp;//审查项目数 
            /***************************************/
            decimal d = EConvert.ToDecimal(v1.Where(t => t.FMonth == i && t.FMType == fMType1).Sum(t => t.FMoney)); //项目金额 
            fc1_1 += d;
            dr["F1_1"] = d;
            d = EConvert.ToDecimal(v1.Where(t => t.FMonth == i && t.FMType == jMType1).Sum(t => t.FMoney)); //项目金额 
            jc1_1 += d;
            dr["J1_1"] = d;
            /***************************************/
            iCount = v1.Count(t => t.FMonth == i
                && t.FMType == fMType1 && t.FCount < 2);
            fc4 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1, i, "F4", iCount);
            dr["F4"] = sTemp;//一次性审查 
            iCount = v1.Count(t => t.FMonth == i
                 && t.FMType == jMType1 && t.FCount < 2);
            jc4 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, jMType1, i, "J4", iCount);
            dr["J4"] = sTemp;//一次性审查  
            /***************************************/
            iCount = v1.Count(t => t.FMonth == i && t.FMType == fMType1 && t.FWQ);
            fc5 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1, i, "F5", iCount);
            dr["F5"] = sTemp;//违强  
            iCount = v1.Count(t => t.FMonth == i && t.FMType == jMType1 && t.FWQ);
            jc5 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, jMType1, i, "J5", iCount);
            dr["J5"] = sTemp;//技术性审查   
            /***************************************/
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
            object[] nums = new object[] { fc1, jc1, fc1_1, jc1_1, fc4, jc4, fc5, jc5 };
            string[] types = new string[] { "F", "J", "F1_1", "J1_1", "F4", "J4", "F5", "J5" };
            int[] mtypes = new int[] { fMType1, jMType1 };
            string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('AppInfoPrj2.aspx?FYear=" + ddlFYear.SelectedValue + "&FMType={0}&FType={1}',700,550);\">{2}</a>";
            for (int i = 0; i < nums.Length; i++)
            {
                string sTemp = nums[i].ToString();
                if (!(i == 2 || i == 3) && sTemp != "0")
                {
                    sTemp = string.Format(sUrl, mtypes[i % 2], types[i], sTemp);
                }
                ((Literal)e.Item.FindControl("l_" + i + "")).Text = sTemp;
            }
        }
    }
}
