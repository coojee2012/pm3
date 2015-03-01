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

public partial class KcsjSgt_AppMain_AnPaiInfoList : System.Web.UI.Page
{
    int fMType = 28802;//审查人员安排(勘察)  
    int fMType_0 = 28803;//技术性审查

    int fMType2 = 30102;//审查人员安排(设计)
    int fMType_2 = 30103;//技术性审查 
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
    }
    //显示
    private void showInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;

        var p = from l1 in db.CF_App_List
                join m in db.CF_Prj_Emp on l1.FId equals m.FAppId
                where l1.FState == 6
                && (
                (l1.FManageTypeId == fMType &&
                    db.CF_App_List.Count(l2 => l2.FState < 1
                    && l2.FManageTypeId == fMType_0
                    && l2.FLinkId == l1.FLinkId
                    && l2.FBaseinfoId == FBaseinfoID) > 0)//勘察技术性审查未办结
                    ||
                    (l1.FManageTypeId == fMType2 &&
                    db.CF_App_List.Count(l2 => l2.FState < 1
                    && l2.FManageTypeId == fMType_2
                    && l2.FLinkId == l1.FLinkId
                    && l2.FBaseinfoId == FBaseinfoID) > 0)//设计技术性审查未办结
                    )
                && l1.FBaseinfoId == FBaseinfoID
                group m by new
                {
                    m.FEmpBaseInfo,
                    l1.FManageTypeId,
                    l1.FPrjId
                } into g
                select new
                {
                    FEmpBaseInfo = g.Key.FEmpBaseInfo,
                    FMType = g.Key.FManageTypeId
                };

        //查询人员信息列表
        var v = db.CF_Emp_BaseInfo.Where(e => e.FBaseInfoID == FBaseinfoID)
            .OrderBy(e => e.FName)
            .Select(e => new
            {
                e.FId,
                e.FName,
                e.FIdCard,
                e.FRegistSpecialId,
                FCount = p.Count(m => m.FEmpBaseInfo == e.FId
              && m.FMType == fMType),
                FCount2 = p.Count(m => m.FEmpBaseInfo == e.FId
              && m.FMType == fMType2)
            });
        if (!string.IsNullOrEmpty(txtFName.Text.Trim()))
            v = v.Where(t => t.FName.Contains(txtFName.Text.Trim()));
        if (!string.IsNullOrEmpty(txtFIdCard.Text.Trim()))
            v = v.Where(t => t.FIdCard.Contains(txtFIdCard.Text.Trim()));
        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页不显示
    }

    //列表
    protected void DG_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbautoid = (Label)e.Row.FindControl("lbautoid");
            lbautoid.Text = (e.Row.RowIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
            int iCount = EConvert.ToInt(DataBinder.Eval(e.Row.DataItem, "FCount"));
            if (iCount > 0)//如果有参与项目，则可点击查看列表
            {
                string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('AnPaiInfoPrj.aspx?empId=" + FID + "&FType=1',750,550);\">[" + iCount + "]</a>";
                e.Row.Cells[4].Text = sUrl;
            }
            iCount = EConvert.ToInt(DataBinder.Eval(e.Row.DataItem, "FCount2"));
            if (iCount > 0)//如果有参与项目，则可点击查看列表
            {
                string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('AnPaiInfoPrj.aspx?empId=" + FID + "&FType=2',750,550);\">[" + iCount + "]</a>";
                e.Row.Cells[5].Text = sUrl;
            }
        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }

    //刷新按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
