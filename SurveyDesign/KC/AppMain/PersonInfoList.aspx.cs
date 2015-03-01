﻿using System;
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

public partial class KC_AppMain_PersonInfoList : System.Web.UI.Page
{
    int fMType = 283;//	勘察项目信息备案-人员安排 
    int fMType_0 = 284;//勘察成果移交
    RCenter rc = new RCenter();
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
        ddlFYear.Items.Insert(0, new ListItem("-全部-", ""));

        for (int i = 1; i < 13; i++)
        {
            string month = i.ToString();
            ddlFMonth.Items.Add(new ListItem(month, month));
        }
        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
        ddlFMonth.Items.Insert(0, new ListItem("-全部-", ""));
    }
    //显示
    private void showInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;

        string[] empids = db.CF_Prj_Emp.Select(t => t.FEmpBaseInfo).ToArray();
        IQueryable<CF_Emp_BaseInfo> emps = db.CF_Emp_BaseInfo.Where(t => t.FBaseInfoID == FBaseinfoID && empids.Contains(t.FId));
        Pager1.RecordCount = emps.Count();
        DG_List.DataSource = emps.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
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
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from cf_app_list where fid in (");
            sb.Append("select fappid from CF_Prj_Emp where FEmpBaseInfo='" + FID + "' and (ftype=1 or ftype =2)   ");
            sb.Append(" group by fappid)");
            DataTable dtx = rc.GetTable(sb.ToString());
            if (dtx != null)
            {
                if (dtx.Rows.Count > 0)//如果有参与项目，则可点击查看列表
                {
                    string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('PersonInfoPrj.aspx?empId=" + FID + "',750,550);\">[" + dtx.Rows.Count + "]</a>";
                    e.Row.Cells[4].Text = sUrl;
                }
            }

            
            //string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
            //int iCount = EConvert.ToInt(DataBinder.Eval(e.Row.DataItem, "FCount"));
            //if (iCount > 0)//如果有参与项目，则可点击查看列表
            //{
            //    string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('PersonInfoPrj.aspx?empId=" + FID + "',750,550);\">[" + iCount + "]</a>";
            //    e.Row.Cells[4].Text = sUrl;
            //}
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
