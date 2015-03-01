using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using ProjectBLL;
using Tools;
using Approve.RuleCenter;
using System.Drawing;

public partial class Government_NewAppMain_CertiList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowPostion();
            if (!string.IsNullOrEmpty(Request.QueryString["FManageTypeId"]))
            {
                int FManageTypeId = EConvert.ToInt(Request.QueryString["FManageTypeId"]);
                if (FManageTypeId > 0)
                    DG_List.Columns[4].HeaderText = (from m in db.CF_Sys_ManageType
                                                     join s in db.CF_Sys_SystemName on m.FSystemId equals s.FNumber
                                                     where m.FNumber == FManageTypeId
                                                     select s.FName).FirstOrDefault();

                if (FManageTypeId == 294)
                {
                    DG_List.Columns[4].HeaderText = "设计单位";
                }
                if (FManageTypeId == 294)
                {
                    // DG_List.Columns[5].HeaderText = "批准时间";
                    DG_List.Columns[6].HeaderText = "批文号";
                }
                else if (FManageTypeId == 290 || FManageTypeId == 305)
                {
                    DG_List.Columns[6].HeaderText = "批文号/备案号";
                    DG_List.Columns[7].Visible = true;
                }
            }
            ShowInfo();
        }
    }
    private void ShowPostion()
    {
        if (Request["fcol"] != null && Request["fcol"] != "")
        {
            lPostion.Text = rc.GetMenuName(Request["fcol"]);
        }
        if (!string.IsNullOrEmpty(Request.QueryString["FManageTypeId"])
            && Request.QueryString["FManageTypeId"].IndexOf(',') > -1)
        {
            td_type1.Visible = td_type2.Visible = true;
            td_type0.ColSpan = 1;
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    private void ShowInfo()
    {

        var leftJoin = from p in db.CF_App_ProcessInstanceBackup
                       join c in db.CF_Prj_Certi on p.FLinkId equals c.FAppId
                       into ps
                       from o in ps.DefaultIfEmpty()
                       where p.FManageDeptId.ToString().StartsWith(EConvert.ToString(Session["DFId"])) && o.FId == null
                       select new
                       {
                           FProjectId = p.FEmpId,
                           FCertiTypeId = o.FId == null ? p.FManageTypeId : o.FCertiTypeId,
                           FJSEntId = o.FId == null ? (p.FManageTypeId == 294 ? p.FBaseInfoID : p.FLeadId) : o.FJSEntId,
                           FJSEntName = o.FId == null ? (p.FManageTypeId == 294 ? p.FEntName : p.FLeadName) : o.FJSEntName,
                           FSJEntId = o.FId == null ? (p.FManageTypeId == 294 ? p.FLeadId : "") : o.FSJEntId,
                           FSJEntName = o.FId == null ? (p.FManageTypeId == 294 ? p.FLeadName : "") : o.FSJEntName,
                           FBaseInfoId = o.FId == null ? p.FBaseInfoID : o.FBaseInfoId,
                           FEntName = o.FId == null ? p.FEntName : o.FEntName,
                           FAppId = o.FId == null ? p.FLinkId : o.FAppId,
                           FName = o.FId == null ? p.FEmpName : o.FName,
                           FAddress = o.FId == null ? db.CF_Prj_BaseInfo.Where(t => t.FId == p.FEmpId).Select(t => t.FAllAddress).FirstOrDefault() : o.FAddress,
                           FAppDate = o.FId == null ? p.FSubmitDate : o.FAppDate,
                           FCityId = o.FId == null ? p.FManageDeptId : o.FCityId,
                           FCertiNo = p.FResult == "3" ? "备案不通过" : o.FCertiNo,
                           o.FId,
                           IsPass = p.FResult == "3" ? false : true
                       };


        var rightJoin = from c in db.CF_Prj_Certi
                        join p in db.CF_App_ProcessInstanceBackup
                        on c.FAppId equals p.FLinkId into ps
                        from o in ps.DefaultIfEmpty()
                        where c.FIsValid == 1 &&
                        (c.FCityId.ToString()
                        .StartsWith(EConvert.ToString(Session["DFId"]))
                        || c.FAppDeptId.ToString()
                        .StartsWith(EConvert.ToString(Session["DFId"])))
                        select new
                        {
                            FProjectId = o.FEmpId,
                            c.FCertiTypeId,
                            c.FJSEntId,
                            c.FJSEntName,
                            c.FSJEntId,
                            c.FSJEntName,
                            c.FBaseInfoId,
                            c.FEntName,
                            c.FAppId,
                            c.FName,
                            c.FAddress,
                            c.FAppDate,
                            c.FCityId,
                            FCertiNo = o.FResult == "3" ? "备案不通过" : c.FCertiNo,
                            c.FId,
                            IsPass = o.FResult == "3" ? false : true
                        };

        var v = leftJoin.Union(rightJoin);
        if (txtFPrjName.Text.Trim() != "")
        {
            v = v.Where(t => t.FName.Contains(txtFPrjName.Text));
        }
        if (txtFEntName.Text.Trim() != "")
        {
            v = v.Where(t => t.FJSEntName.Contains(txtFEntName.Text));
        }
        if (txtFCertiNo.Text.Trim() != "")
        {
            v = v.Where(t => t.FJSEntName.Contains(txtFCertiNo.Text));
        }
        if (ddlState.SelectedValue != "")
        {
            if (ddlState.SelectedValue == "1")
            {
                v = v.Where(t => t.IsPass);
            }
            else
            {
                v = v.Where(t => !t.IsPass);
            }
        }
        if (!string.IsNullOrEmpty(Request.QueryString["FStartTime"]))
        {
            txtFReportDate.Text = Request.QueryString["FStartTime"];

        }

        if (!string.IsNullOrEmpty(Request.QueryString["FEndTime"]))
        {
            txtFReportDate1.Text = Request.QueryString["FEndTime"];

        }
        if (txtFReportDate.Text.Trim() != "")
        {
            v = v.Where(t => t.FAppDate >= EConvert.ToDateTime(txtFReportDate.Text.Trim()));

        }
        if (txtFReportDate1.Text.Trim() != "")
        {
            v = v.Where(t => t.FAppDate <= EConvert.ToDateTime(txtFReportDate1.Text.Trim()));

        }
        if (string.IsNullOrEmpty(Request.QueryString["DeptId"]))
        {
            //v = v.Where(t => t.FCityId.ToString().StartsWith(EConvert.ToString(Session["DFId"])));


        }
        else
        {
            v = v.Where(t => t.FCityId.ToString().StartsWith(Request.QueryString["DeptId"]) && t.IsPass);

        }
        if (!string.IsNullOrEmpty(ddlFType.SelectedValue))
        {
            v = v.Where(t => t.FCertiTypeId == EConvert.ToInt(ddlFType.SelectedValue));
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["FManageTypeId"]))
        {
            v = v.Where(t => (Request["FManageTypeId"].Split(',')).Contains(t.FCertiTypeId.ToString()));
        }

        v = v.OrderByDescending(t => t.FAppDate);
        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowInfo();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();

            int FManageTypeId = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FCertiTypeId"));
            string FId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            string FBaseInfoId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBaseInfoId"));
            string FEntName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEntName"));
            string FAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string fUrl = rc.getMTypeQurl(FManageTypeId.ToString());
            string sScript = "openWinNew('" + fUrl + "?fbid=" + FBaseInfoId + "&faid=" + FAppId + "&fmid=" + FManageTypeId + "&fly=1')";

            e.Item.Cells[1].Text = "<a href='#'  onclick=\"" + sScript + "\" >" + e.Item.Cells[1].Text + "</a>";

            //查询该项目是否变更 
            string prjid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FProjectId"));
            if (!string.IsNullOrEmpty(prjid))
            {
                var prjBG = db.CF_Prj_BaseInfo.Where(t => t.FId == prjid)
                    .Select(t => new { t.FBGTime, t.FCount })
                    .FirstOrDefault();
                if (prjBG != null && prjBG.FCount > 0)
                {
                    e.Item.Cells[1].Text += "<br/><font color='#000000;'>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")</font>";
                }
            }
            //e.Item.Cells[2].Text = db.getDeptFullName(DataBinder.Eval(e.Item.DataItem, "FCityId")) + e.Item.Cells[2].Text;

            string sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=100 ");

            sUrl += "?fbid=" + EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FJSEntId"));

            //e.Item.Cells[3].Text = "<a href='#'   onclick=\"showAddWindow('" + sUrl + "',980,450)\"  >" + e.Item.Cells[3].Text + "</a>";
           

            if (db.ManageType.Any(t => t.FNumber == FManageTypeId && t.FIsPrint == 1))
            {
                int? isAutoApp = db.CF_Prj_Certi.Where(t => t.FId == FId).Select(t => t.FIsAutoApp).FirstOrDefault();
                if (isAutoApp.HasValue && isAutoApp == 1)
                {
                    e.Item.Cells[1].Text += "*";
                }
            }
            if (FManageTypeId == 294)
            {
                FBaseInfoId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSJEntId"));
                FEntName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSJEntName"));
            }
            else
            {

            }
            e.Item.Cells[4].Text = FEntName;
            CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == FBaseInfoId).FirstOrDefault();
            if (ent != null)
            {
                sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=" + ent.FSystemId);

                sUrl += "?fbid=" + FBaseInfoId;

                //e.Item.Cells[4].Text = "<a href='#'  onclick=\"showAddWindow('" + sUrl + "',980,450)\"  >" + e.Item.Cells[4].Text + "</a>";

            }
            bool IsPass = EConvert.ToBool(DataBinder.Eval(e.Item.DataItem, "IsPass"));
            if (!IsPass)
            {
                e.Item.CssClass = "m_dg1_i RedTdAndLink";
            }

            if (FManageTypeId == 290 || FManageTypeId == 305)
            {
                string str = "";
                int? FTechnical = db.CF_Prj_Certi.Where(t => t.FId == FId).Select(t => t.FTechnical).FirstOrDefault();
                if (FTechnical.HasValue)
                {
                    if (EConvert.ToInt(FTechnical) == 6)
                    {
                        str = "合格";
                    }
                    else
                    {
                        //技术性审查不合格
                        str = "不合格";
                    }

                }
                else
                {
                    if (FManageTypeId == 290)//勘察文件审查备案
                    {
                        //技术性审查结果
                        var result = (from l1 in db.CF_App_List
                                      join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId
                                      join id in db.CF_App_Idea.Where(t => t.FUserId == null || t.FType == 290) on l2.FId equals id.FLinkId
                                      where l1.FId == FAppId && l2.FManageTypeId == 28803
                                      orderby l2.FCount descending
                                      select id.FResult).FirstOrDefault();
                        str = result;

                    }
                    else if (FManageTypeId == 305)//施工图设计文件备案
                    {
                        //技术性审结果
                        var result = (from l1 in db.CF_App_List
                                      join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId
                                      join id in db.CF_App_Idea.Where(t => t.FUserId == null || t.FType == 305) on l2.FId equals id.FLinkId
                                      where l1.FId == FAppId && l2.FManageTypeId == 30103
                                      orderby l2.FCount descending
                                      select id.FResult).FirstOrDefault();
                        str = result;

                    }

                }
                e.Item.Cells[7].Text = str;

            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnOut_Click(object sender, EventArgs e)
    {

        Pager1.CurrentPageIndex = 1;
        Pager1.PageSize = Pager1.RecordCount;

        string fOutTitle = lPostion.Text;
        Approve.Common.SaveAsBase sab = new Approve.Common.SaveAsBase();
        ShowInfo();
        sab.SaveAsExc(this.DG_List, fOutTitle, this.Response, "gb2312", 0);
    }
}
