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

public partial class JZDW_AppMain_AppInfoList : System.Web.UI.Page
{
    int fMType1 = 28001;//项目见证受理 
    int fMType4 = 28003;//见证报告备案 

    int fc1 = 0;
    int fc4 = 0;

    int jc1 = 0;
    int jc4 = 0;
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
            new DataColumn("F4"), 
            new DataColumn("J1"), 
            new DataColumn("J4")
        });
        int iYear = EConvert.ToInt(ddlFYear.SelectedValue);
        int iMonth = 12;
        if (iYear >= DateTime.Now.Year)
            iMonth = DateTime.Now.Month;
        //查询关联的业务数量 
        var v1 = (from t in db.CF_App_List
                  where ((t.FToBaseinfoId == FBaseinfoID
                  && t.FManageTypeId == fMType1)
                  || t.FBaseinfoId == FBaseinfoID)
                  && t.FAppDate.Value.Year == iYear
                  && (t.FState == 6 || t.FState == 3 || t.FState == 7)
                  select new
                  {
                      FMType = t.FManageTypeId,
                      t.FState,
                      FMonth = t.FAppDate.Value.Month
                  }).ToList();

        string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('AppInfoPrj.aspx?FYear=" + iYear + "&FMType={0}&FMonth={1}&FType={2}',750,550);\">{3}</a>";
        string sTemp = string.Empty;
        for (int i = 1; i <= iMonth; i++)
        {
            DataRow dr = dt.NewRow();
            dr["FMonth"] = i.ToString();
            int iCount = v1.Count(t => t.FMonth == i && t.FMType == fMType1 && t.FState == 6);
            fc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1, i, "F1", iCount);
            dr["F1"] = sTemp;//受理
            iCount = v1.Count(t => t.FMonth == i && t.FMType == fMType1 && t.FState == 7);
            jc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1, i, "J1", iCount);
            dr["J1"] = sTemp;//不受理
            /*******************************/
            iCount = v1.Count(t => t.FMonth == i && t.FMType == fMType4 && t.FState == 6);
            fc4 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType4, i, "F4", iCount);
            dr["F4"] = sTemp;//项目见证报告备案 
            iCount = v1.Count(t => t.FMonth == i && t.FMType == fMType4 && t.FState == 3);
            jc4 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType4, i, "J4", iCount);
            dr["J4"] = sTemp;//项目见证报告备案  
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
            object[] nums = new object[] { fc1, jc1, fc4, jc4 };
            string[] types = new string[] { "F1", "J1", "F4", "J4" };
            int[] mtypes = new int[] { fMType1, fMType1, fMType4, fMType4 };
            string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('AppInfoPrj.aspx?FYear=" + ddlFYear.SelectedValue + "&FMType={0}&FType={1}',750,550);\">{2}</a>";
            for (int i = 0; i < nums.Length; i++)
            {
                string sTemp = nums[i].ToString();
                if (sTemp != "0")
                    sTemp = string.Format(sUrl, mtypes[i], types[i], sTemp);
                ((Literal)e.Item.FindControl("l_" + i + "")).Text = sTemp;
            }
        }
    }
}
