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
    int fMType1 = 280;//勘察项目合同备案  
    int fMType4 = 284;//勘察成果移交

    int jMType1 = 28001;//项目见证受理   

    int entType = 15501;//勘察单位
    int fc1 = 0;
    decimal fc1_1 = 0;
    int fc4 = 0;
    decimal fc4_1 = 0;
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
            new DataColumn("F4_1")  
        });
        int iYear = EConvert.ToInt(ddlFYear.SelectedValue);
        int iMonth = 12;
        if (iYear >= DateTime.Now.Year)
            iMonth = DateTime.Now.Month;
        //查询关联的业务数量  
        var v1 = (from t in db.CF_App_List//
                  join l2 in db.CF_App_List on t.FLinkId equals l2.FLinkId//其他单位（见证/勘察单位）的受理
                  join b in db.CF_Prj_Ent on l2.FLinkId equals b.FAppId
                  where ((t.FToBaseinfoId == FBaseinfoID
                  && t.FManageTypeId == fMType1)
                  || (t.FBaseinfoId == FBaseinfoID && t.FManageTypeId == fMType4))
                  && l2.FManageTypeId == jMType1
                  && l2.FAppDate.Value.Year == iYear
                  && t.FState == 6 && l2.FState == 6
                  && b.FBaseInfoId == FBaseinfoID
                  && b.FEntType == entType//当前单位类型
                  group t by new
                  {
                      t.FPrjId,
                      t.FManageTypeId,
                      t.FAppDate.Value.Month,
                      b.FMoney
                  } into g
                  select new
                  {
                      g.Key.FPrjId,
                      FMType = g.Key.FManageTypeId,
                      FMonth = g.Key.Month,
                      g.Key.FMoney
                  }).ToList();
        string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('AppInfoPrj2.aspx?FYear=" + iYear + "&FMType={0}&FMonth={1}&FType={2}',750,550);\">{3}</a>";
        string sTemp = string.Empty;
        for (int i = 1; i <= iMonth; i++)
        {
            DataRow dr = dt.NewRow();
            dr["FMonth"] = i.ToString();
            int iCount = v1.Where(t => t.FMonth == i && t.FMType == fMType1).GroupBy(t => t.FPrjId).Count();
            fc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1, i, "F1", iCount);
            dr["F1"] = sTemp;//受理
            iCount = v1.Where(t => t.FMonth == i && t.FMType == fMType4).GroupBy(t => t.FPrjId).Count();
            fc4 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType4, i, "F4", iCount);
            dr["F4"] = sTemp;//移交
            /*******************************/
            decimal d = EConvert.ToDecimal(v1.Where(t => t.FMonth == i
                && t.FMType == fMType1).Sum(t => t.FMoney));
            dr["F1_1"] = d; fc1_1 += d;  //受理合同额

            d = EConvert.ToDecimal(v1.Where(t => t.FMonth == i
               && t.FMType == fMType4).Sum(t => t.FMoney));
            dr["F4_1"] = d; fc4_1 += d;  //移交合同额
            /*******************************/
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
            object[] nums = new object[] { fc1, fc1_1, fc4, fc4_1 };
            string[] types = new string[] { "F1", "F1_1", "F4", "F4_1" };
            int[] mtypes = new int[] { fMType1, fMType1, fMType4, fMType4 };
            string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('AppInfoPrj2.aspx?FYear=" + ddlFYear.SelectedValue + "&FMType={0}&FType={1}',750,550);\">{2}</a>";
            for (int i = 0; i < nums.Length; i++)
            {
                string sTemp = nums[i].ToString();
                if (!(i == 1 || i == 3) && sTemp != "0")
                    sTemp = string.Format(sUrl, mtypes[i], types[i], sTemp);
                ((Literal)e.Item.FindControl("l_" + i + "")).Text = sTemp;
            }
        }
    }
}
