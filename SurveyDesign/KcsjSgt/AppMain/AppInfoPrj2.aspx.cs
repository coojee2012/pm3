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
public partial class KC_appmain_AppInfoPrj2 : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    int fMType1 = 287;//勘察文件审查合同备案   
    int fMType2 = 28803;//技术性审查(勘察) 
    int fMType3 = 28802;//人员安排(勘察) 
    int fMType4 = 290;//备案(勘察) 

    int jMType1 = 300;//施工图设计文件审查合同备案   
    int jMType2 = 30103;//技术性审查(设计)  
    int jMType3 = 30102;//人员安排(设计) 
    int jMType4 = 305;//备案(设计)

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
        string fType = Request.QueryString["FType"];
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
        lTitle.Text = fType;
    }
    //显示 
    void showInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        string fType = Request.QueryString["FType"];
        int fMType = EConvert.ToInt(Request.QueryString["FMType"]);
        int FYear = EConvert.ToInt(Request.QueryString["FYear"]);
        int FMonth = EConvert.ToInt(Request.QueryString["FMonth"]);
        string tag = fType.Substring(1);
        int iType = fType.Substring(0, 1) == "F" ? fMType3 : jMType3;
        int iType2 = fType.Substring(0, 1) == "F" ? fMType2 : jMType2;
        int iType3 = fType.Substring(0, 1) == "F" ? fMType4 : jMType4;
        var App = from l1 in db.CF_App_List
                  join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId
                  join l3 in db.CF_App_List on l1.FLinkId equals l3.FLinkId//备案
                  join d in db.CF_Prj_Ent on l2.FLinkId equals d.FAppId
                  join p in db.CF_Prj_BaseInfo on l1.FPrjId equals p.FId
                  where l1.FManageTypeId == fMType && l2.FManageTypeId == iType2
                  && l3.FManageTypeId == iType3
                  && (l1.FToBaseinfoId == FBaseinfoID
                  && l2.FBaseinfoId == FBaseinfoID) && d.FBaseInfoId == FBaseinfoID
                  && l1.FState == 6 && l2.FState == 6 && l3.FState == 6
                  && (db.CF_App_Idea.Where(i => i.FLinkId == l3.FId)
                  .OrderByDescending(i => i.FTime).Select(i => i.FResultInt)
                  .FirstOrDefault() == 1)//并且备案结果同意
                  && l3.FAppDate.Value.Year == FYear
                  && (FMonth > 0 ? l3.FAppDate.Value.Month == FMonth : true)
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
                                    && n.FBaseinfoId == FBaseinfoID
                                    && m.FType > 1 && m.FEmpBaseInfo == i.FUserId
                                    && i.FOrder > 0
                                    select 1).Count() > 0 : true)//违强
                  orderby l3.FAppDate descending
                  select new
                  {
                      FLinkId = l1.FLinkId,
                      FType = 2,
                      FUrl = fType.Substring(0, 1),
                      FPrjId = p.FId,
                      p.FPrjName,
                      FAddress = db.CF_Sys_ManageDept.Where(t => t.FNumber.ToString() == p.FAddressDept).Select(t => t.FName).FirstOrDefault(),
                      FJSEnt = db.CF_Ent_BaseInfo.Where(t => t.FId == p.FBaseinfoId).Select(t => t.FName).FirstOrDefault(),
                      FMoney = d.FMoney
                  };
        if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
            App = App.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
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
