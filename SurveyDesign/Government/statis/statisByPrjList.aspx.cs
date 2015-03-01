using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Text;
using Approve.RuleCenter;
public partial class KC_appmain_statisByPrjList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    int fMType1 = 287;//勘察文件审查合同备案   
    int fMType2 = 28803;//技术性审查(勘察) 
    int fMType3 = 28802;//人员安排(勘察) 
    int fMType4 = 290;//备案 

    int jMType1 = 300;//施工图设计文件审查合同备案   
    int jMType2 = 305;//技术性审查(设计)  
    int jMType3 = 30102;//人员安排(设计) 
    int jMType4 = 305;//备案  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            showInfo();
        }
    }
    void BindControl()
    {
        string cityCode = Request.QueryString["cityCode"];
        if (!string.IsNullOrEmpty(cityCode))
        {
            cityCode = "_" + db.ManageDept.Where(t => t.FNumber.ToString() == cityCode).Select(t => t.FName).FirstOrDefault();
        }
        string fCol = Request.QueryString["col"];
        string fType = Request.QueryString["FType"];
        int fMType = EConvert.ToInt(Request.QueryString["FMType"]);
        string tag = string.Empty;
        if (fCol != "SCLB")
            tag = fMType == fMType1 ? "勘察文件" : "施工图设计文件";
        switch (fCol)
        {
            case "SCLB":
                switch (fType)
                {
                    case "F":
                        fType = "勘察文件审查项目";
                        break;
                    case "F4":
                        fType = "一次性通过勘察文件审查项目";
                        break;
                    case "F5":
                        fType = "勘察文件审查违反工程建设标准强制性条文数";
                        break;

                    case "J":
                        fType = "施工图设计文件审查项目";
                        break;
                    case "J4":
                        fType = "一次性通过施工图设计文件审查项目";
                        break;
                    case "J5":
                        fType = "施工图设计文件审查违反工程建设标准强制性条文数";
                        break;
                }
                break;
            case "GCLB"://工程类别 
                switch (fType)
                {
                    case "F":
                        fType = "审查项目_房屋建筑工程";
                        break;
                    case "F4":
                        fType = "一次性通过审查项目_房屋建筑工程";
                        break;
                    case "F5":
                        fType = "违反工程建设标准强制性条文_房屋建筑工程";
                        break;

                    case "J":
                        fType = "审查项目_市政公用工程";
                        break;
                    case "J4":
                        fType = "一次性通过审查项目_市政公用工程";
                        break;
                    case "J5":
                        fType = "违反工程建设标准强制性条文_市政公用工程";
                        break;
                }
                break;
            case "SJGM":
                switch (fType)
                {
                    case "F":
                        fType = "审查项目_大型";
                        break;
                    case "F4":
                        fType = "一次性通过审查项目_大型";
                        break;
                    case "F5":
                        fType = "违反工程建设标准强制性条文_大型";
                        break;

                    case "J":
                        fType = "审查项目_中型";
                        break;
                    case "J4":
                        fType = "一次性通过审查项目_中型";
                        break;
                    case "J5":
                        fType = "违反工程建设标准强制性条文_中型";
                        break;

                    case "X":
                        fType = "审查项目_小型";
                        break;
                    case "X4":
                        fType = "一次性通过审查项目_中型";
                        break;
                    case "X5":
                        fType = "违反工程建设标准强制性条文_小型";
                        break;
                }
                break;
            case "JSXZ":
                switch (fType)
                {
                    case "F":
                        fType = "审查项目_新建";
                        break;
                    case "F4":
                        fType = "一次性通过审查项目_改建";
                        break;
                    case "F5":
                        fType = "违反工程建设标准强制性条文_扩建";
                        break;

                    case "J":
                        fType = "审查项目_新建";
                        break;
                    case "J4":
                        fType = "一次性通过审查项目_改建";
                        break;
                    case "J5":
                        fType = "违反工程建设标准强制性条文_扩建";
                        break;

                    case "X":
                        fType = "审查项目_新建";
                        break;
                    case "X4":
                        fType = "一次性通过审查项目_改建";
                        break;
                    case "X5":
                        fType = "违反工程建设标准强制性条文_扩建";
                        break;
                }
                break;
        }
        lTitle.Text = tag + fType + cityCode;
    }
    //显示 
    void showInfo()
    {
        string fType = Request.QueryString["FType"];
        string fCol = Request.QueryString["col"];
        int fMType = EConvert.ToInt(Request.QueryString["FMType"]);
        int FYear = EConvert.ToInt(Request.QueryString["FYear"]);
        string cityCode = Request.QueryString["cityCode"];
        int iLen = 0;
        if (!string.IsNullOrEmpty(cityCode))
            iLen = cityCode.Length;
        string sWhere = Request.QueryString["sWhere"];
        string sTag = Request.QueryString["sTag"];
        int syear = 0;
        int squarter = 0;
        int smonth = 0;
        string szdyB = "1900-01-01";
        string szdyE = "9999-12-30";
        switch (sWhere)
        {
            case "year": //按年
                syear = FYear;
                break;
            case "quarter": //按季度 
                syear = FYear;
                squarter = EConvert.ToInt(sTag);
                break;
            case "month": //按月
                syear = FYear;
                smonth = EConvert.ToInt(sTag);
                break;
            case "zdy": //自定义
                string[] zdyTime = sTag.Split("|".ToCharArray(), StringSplitOptions.None);
                if (zdyTime.Length == 2)
                {
                    if (!string.IsNullOrEmpty(zdyTime[0]))
                        szdyB = zdyTime[0];
                    if (!string.IsNullOrEmpty(zdyTime[1]))
                        szdyE = zdyTime[1];
                }
                break;
        }
        string tag = fType.Substring(1);
        if (fCol == "SCLB")//按照审查类别
        {
            int iType = fType.Substring(0, 1) == "F" ? fMType3 : jMType3;
            int iType2 = fType.Substring(0, 1) == "F" ? fMType2 : jMType2;
            int iType3 = fType.Substring(0, 1) == "F" ? fMType4 : jMType4;
            var App = from l1 in db.CF_App_List
                      join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId
                      join l3 in db.CF_App_List on l1.FLinkId equals l3.FLinkId
                      join d in db.CF_Prj_Ent on l2.FLinkId equals d.FAppId
                      join p in db.CF_Prj_BaseInfo on l1.FPrjId equals p.FId
                      where l1.FManageTypeId == fMType && l2.FManageTypeId == iType2
                      && l3.FManageTypeId == iType3 && d.FEntType == 145//审图
                      && l1.FState == 6 && l2.FState == 6 && l3.FState == 6
                      && (db.CF_App_Idea.Where(i => i.FLinkId == l3.FId)
                      .OrderByDescending(i => i.FTime).Select(i => i.FResultInt)
                      .FirstOrDefault() == 1)//并且备案结果同意
                      && l1.FToBaseinfoId == l2.FBaseinfoId
                      && d.FBaseInfoId == l2.FBaseinfoId
                      && (iLen > 0 ? p.FAddressDept.Substring(0, iLen) == cityCode : true)
                      && (syear > 0 ? l3.FAppDate.GetValueOrDefault().Year == syear : true)
                      && (smonth > 0 ? l3.FAppDate.GetValueOrDefault().Month == smonth : true)
                      && (squarter > 0 ? (l3.FAppDate.GetValueOrDefault().Month > 3 * (squarter - 1) && l3.FAppDate.GetValueOrDefault().Month <= 3 * squarter) : true)
                      && (sWhere == "zdy" ? l3.FAppDate >= Convert.ToDateTime(szdyB) : true)
                      && (sWhere == "zdy" ? l3.FAppDate < Convert.ToDateTime(szdyE).AddDays(1) : true)
                      && (tag == "4" ? l1.FReportCount < 2 : true)//一次性
                      && (tag == "5" ? (from n in db.CF_App_List
                                        join n1 in db.CF_App_List
                                        on n.FLinkId equals n1.FLinkId
                                        join m in db.CF_Prj_Emp
                                        on n1.FId equals m.FAppId
                                        join i in db.CF_App_Idea
                                        on n.FId equals i.FLinkId
                                        where n.FLinkId == l2.FLinkId
                                        && n.FManageTypeId == l2.FManageTypeId
                                        && n1.FManageTypeId == iType
                                        && n.FState == 6 && n1.FState == 6
                                        && n.FBaseinfoId == n1.FBaseinfoId
                                        && m.FType > 1
                                        && m.FEmpBaseInfo == i.FUserId
                                        && i.FOrder > 0
                                        select 1).Count() > 0 : true)//违强
                      orderby l3.FAppDate descending
                      select new
                      {
                          FPrjId = p.FId,
                          p.FPrjName,
                          FAddress = db.CF_Sys_ManageDept.Where(t => t.FNumber.ToString() == p.FAddressDept).Select(t => t.FName).FirstOrDefault(),
                          FJSEnt = db.CF_Ent_BaseInfo.Where(t => t.FId == p.FBaseinfoId).Select(t => t.FName).FirstOrDefault(),
                          FSTEnt = db.CF_Ent_BaseInfo.Where(t => t.FId == l3.FBaseinfoId).Select(t => t.FName).FirstOrDefault(),
                          FMoney = d.FMoney
                      };
            if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
                App = App.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
        else
        {
            int iSCType = EConvert.ToInt(Request.QueryString["sSCType"]);//勘察\设计
            int mType2 = iSCType == fMType1 ? fMType2 : jMType2;
            int mType3 = iSCType == fMType1 ? fMType3 : jMType3;
            int mType4 = iSCType == fMType1 ? fMType4 : jMType4;
            int iType = 0;
            if (fCol == "GCLB")//工程类别
            {
                int iType1 = 2000101;//房屋建筑
                int iType2 = 2000102;//市政公用
                iType = fType.Substring(0, 1) == "F" ? iType1 : iType2;
            }
            else if (fCol == "SJGM")//设计规模
            {
                int iType1 = 2000401;//大型
                int iType2 = 2000402;//中型
                int iType3 = 2000403;//小型 
                iType = fType.Substring(0, 1) == "F" ? iType1 : iType2;
                if (fType.Substring(0, 1) == "X")
                    iType = iType3;
            }
            else if (fCol == "JSXZ")//建设性质
            {
                int iType1 = 2000501;//新建
                int iType2 = 2000502;//改建
                int iType3 = 2000503;//扩建
                iType = fType.Substring(0, 1) == "F" ? iType1 : iType2;
                if (fType.Substring(0, 1) == "X")
                    iType = iType3;
            }
            var App = from l1 in db.CF_App_List
                      join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId
                      join l3 in db.CF_App_List on l1.FLinkId equals l3.FLinkId
                      join d in db.CF_Prj_Ent on l2.FLinkId equals d.FAppId
                      join p in db.CF_Prj_BaseInfo on l1.FPrjId equals p.FId
                      where l1.FManageTypeId == iSCType
                            && l2.FManageTypeId == mType2
                            && l3.FManageTypeId == mType4
                            && d.FEntType == 145//审图
                      && l1.FState == 6 && l2.FState == 6 && l3.FState == 6
                      && (db.CF_App_Idea.Where(i => i.FLinkId == l3.FId)
                      .OrderByDescending(i => i.FTime).Select(i => i.FResultInt)
                      .FirstOrDefault() == 1)//并且备案结果同意
                      && (fCol == "GCLB" ? p.FType == iType : true)//类别
                      && (fCol == "SJGM" ? p.FScale == iType : true)//规模
                      && (fCol == "JSXZ" ? p.FKind == iType : true)//性质
                      && l1.FToBaseinfoId == l2.FBaseinfoId
                      && d.FBaseInfoId == l2.FBaseinfoId
                      && (iLen > 0 ? p.FAddressDept.Substring(0, iLen) == cityCode : true)
                      && (syear > 0 ? l3.FAppDate.GetValueOrDefault().Year == syear : true)
                      && (smonth > 0 ? l3.FAppDate.GetValueOrDefault().Month == smonth : true)
                      && (squarter > 0 ? (l3.FAppDate.GetValueOrDefault().Month > 3 * (squarter - 1) && l3.FAppDate.GetValueOrDefault().Month <= 3 * squarter) : true)
                      && (sWhere == "zdy" ? l3.FAppDate >= Convert.ToDateTime(szdyB) : true)
                      && (sWhere == "zdy" ? l3.FAppDate < Convert.ToDateTime(szdyE).AddDays(1) : true)
                      && (tag == "4" ? l1.FReportCount < 2 : true)//一次性
                      && (tag == "5" ? (from n in db.CF_App_List
                                        join n1 in db.CF_App_List
                                        on n.FLinkId equals n1.FLinkId
                                        join m in db.CF_Prj_Emp
                                        on n1.FId equals m.FAppId
                                        join i in db.CF_App_Idea
                                        on n.FId equals i.FLinkId
                                        where n.FLinkId == l2.FLinkId
                                        && n.FManageTypeId == l2.FManageTypeId
                                        && n1.FManageTypeId == mType3
                                        && n.FState == 6 && n1.FState == 6
                                        && n.FBaseinfoId == n1.FBaseinfoId
                                        && m.FType > 1
                                        && m.FEmpBaseInfo == i.FUserId
                                        && i.FOrder > 0
                                        select 1).Count() > 0 : true)//违强
                      orderby l3.FAppDate descending
                      select new
                      {
                          FPrjId = p.FId,
                          p.FPrjName,
                          FAddress = db.CF_Sys_ManageDept.Where(t => t.FNumber.ToString() == p.FAddressDept).Select(t => t.FName).FirstOrDefault(),
                          FJSEnt = db.CF_Ent_BaseInfo.Where(t => t.FId == p.FBaseinfoId).Select(t => t.FName).FirstOrDefault(),
                          FSTEnt = db.CF_Ent_BaseInfo.Where(t => t.FId == l3.FBaseinfoId).Select(t => t.FName).FirstOrDefault(),
                          FMoney = d.FMoney
                      };
            if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
                App = App.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
