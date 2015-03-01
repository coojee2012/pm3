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

public partial class KC_AppMain_AppInfoList : System.Web.UI.Page
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
    int fc4 = 0;
    int fc5 = 0;
    int fc6 = 0;
    int fc7 = 0;
    int fc8 = 0;

    int jc1 = 0;
    int jc4 = 0;
    int jc5 = 0;
    int jc6 = 0;
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
            new DataColumn("F5"),
            new DataColumn("F6"),
            new DataColumn("F7"),
            new DataColumn("F8"),
            new DataColumn("J1"), 
            new DataColumn("J4"), 
            new DataColumn("J5"),
            new DataColumn("J6") 
        });
        int iYear = EConvert.ToInt(ddlFYear.SelectedValue);
        int iMonth = 12;
        if (iYear >= DateTime.Now.Year)
            iMonth = DateTime.Now.Month;
        //查询关联的业务数量 
        var v1 = (from t in db.CF_App_List
                  where ((t.FToBaseinfoId == FBaseinfoID
                  && (t.FManageTypeId == fMType1 || t.FManageTypeId == jMType1))
                  || t.FBaseinfoId == FBaseinfoID)
                  && t.FAppDate.Value.Year == iYear
                  && (t.FState == 6 || t.FState == 3 || t.FState == 7)
                  select new
                  {
                      FMType = t.FManageTypeId,
                      FState =
                      (t.FManageTypeId == fMType6
                      || t.FManageTypeId == jMType6)
                      ? (from i in db.CF_App_Idea
                         join b in db.CF_App_List
                         on i.FLinkId equals b.FId
                         where b.FLinkId == t.FLinkId
                         && b.FManageTypeId == t.FManageTypeId
                         orderby i.FTime
                         select i.FResultInt).FirstOrDefault() : t.FState,
                      FMonth = t.FAppDate.Value.Month
                  }).ToList();

        //告知书|合格证 --- 技术性审查一步  已经打印 的
        var v7_8 = (from t in db.CF_App_List
                    where t.FBaseinfoId == FBaseinfoID
                    && (t.FState == 6 || t.FState == 3)
                    && t.FIsSign == 1//已打印
                    && (t.FManageTypeId == fMType5 || t.FManageTypeId == jMType5)
                    && t.FToBaseinfoId != "" && t.FToBaseinfoId != null
                    && Convert.ToDateTime(t.FToBaseinfoId).Year == iYear
                    select new
                    {
                        t.FPrjId,
                        FMType = t.FManageTypeId,
                        FMonth = Convert.ToDateTime(t.FToBaseinfoId).Month,
                        t.FState
                    }).ToList();
        string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('AppInfoPrj.aspx?FYear=" + iYear + "&FMType={0}&FMonth={1}&FType={2}',750,550);\">{3}</a>";
        string sTemp = string.Empty;
        for (int i = 1; i <= iMonth; i++)
        {
            DataRow dr = dt.NewRow();
            dr["FMonth"] = i.ToString();
            int iCount = v1.Count(t => t.FMonth == i && (t.FMType == fMType1 || t.FMType == jMType1) && t.FState == 6);
            fc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1 + "," + jMType1, i, "F1", iCount);
            dr["F1"] = sTemp;//受理
            iCount = v1.Count(t => t.FMonth == i && (t.FMType == fMType1 || t.FMType == jMType1) && t.FState == 7);
            jc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1 + "," + jMType1, i, "J1", iCount);
            dr["J1"] = sTemp;//不受理
            /*******************************/
            iCount = v1.Count(t => t.FMonth == i && (t.FMType == fMType4 || t.FMType == jMType4) && t.FState == 6);
            fc4 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType4 + "," + jMType4, i, "F4", iCount);
            dr["F4"] = sTemp;//程序性审查 
            iCount = v1.Count(t => t.FMonth == i && (t.FMType == fMType4 || t.FMType == jMType4) && t.FState == 3);
            jc4 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType4 + "," + jMType4, i, "J4", iCount);
            dr["J4"] = sTemp;//程序性审查  
            /*******************************/
            iCount = v1.Count(t => t.FMonth == i && (t.FMType == fMType5 || t.FMType == jMType5) && t.FState == 6);
            fc5 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType5 + "," + jMType5, i, "F5", iCount);
            dr["F5"] = sTemp;//技术性审查  
            iCount = v1.Count(t => t.FMonth == i && (t.FMType == fMType5 || t.FMType == jMType5) && t.FState == 3);
            jc5 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType5 + "," + jMType5, i, "J5", iCount);
            dr["J5"] = sTemp;//技术性审查   
            /*******************************/
            iCount = v1.Count(t => t.FMonth == i && (t.FMType == fMType6 || t.FMType == jMType6) && t.FState == 1);
            fc6 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType6 + "," + jMType6, i, "F6", iCount);
            dr["F6"] = sTemp;//备案  
            iCount = v1.Count(t => t.FMonth == i && (t.FMType == fMType6 || t.FMType == jMType6) && t.FState == 3);
            jc6 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType6 + "," + jMType6, i, "J6", iCount);
            dr["J6"] = sTemp;//备案   
            /*******************************/
            iCount = iCount = v7_8.Count(t => t.FMonth == i && (t.FMType == fMType5 || t.FMType == jMType5) && t.FState == 6);
            fc7 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType5 + "," + jMType5, i, "F7", iCount);
            dr["F7"] = sTemp;//打印合格证   
            /*******************************/
            iCount = v7_8.Count(t => t.FMonth == i && (t.FMType == fMType5 || t.FMType == jMType5) && t.FState == 3);
            fc8 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType5 + "," + jMType5, i, "F8", iCount);
            dr["F8"] = sTemp;//打印告知书   
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
            object[] nums = new object[] { fc1, jc1, fc4, jc4, fc5, jc5, fc6, jc6, fc7, fc8 };
            string[] types = new string[] { "F1", "J1", "F4", "J4", "F5", "J5", "F6", "J6", "F7", "F8" };
            int[] mtypes = new int[] { fMType1, jMType1, fMType4, jMType4, fMType5, jMType5, fMType6, jMType6, fMType5, jMType5 };
            string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('AppInfoPrj.aspx?FYear=" + ddlFYear.SelectedValue + "&FMType={0}&FType={1}',750,550);\">{2}</a>";
            for (int i = 0; i < nums.Length; i++)
            {
                int ii = (i % 2 == 0) ? 1 : -1;
                string sTemp = nums[i].ToString();
                if (sTemp != "0")
                    sTemp = string.Format(sUrl, mtypes[i] + "," + mtypes[i + ii], types[i], sTemp);
                ((Literal)e.Item.FindControl("l_" + i + "")).Text = sTemp;
            }
        }
    }
}
