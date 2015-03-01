using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;

public partial class JSDW_Statistics_all : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["FID"]))
            {
                ViewState["FID"] = Request.QueryString["FID"];
                BindControl();
                showInfo();
            }
        }
    }
    void BindControl()
    {
        string fid = EConvert.ToString(ViewState["FID"]);
        if (!string.IsNullOrEmpty(fid))
        {
            var hisList = (from p in db.CF_Prj_BaseInfo
                           join h in db.CF_Prj_BaseInfo
                           on p.FLinkId equals h.FLinkId
                           where p.FId == fid
                           orderby h.FCount descending
                           select new { h.FId, h.FPrjName, h.FCount, h.FBGTime })
                          .ToList();
            if (hisList != null && hisList.Count > 1)
            {
                foreach (var v in hisList)
                {
                    string str = v.FPrjName + (v.FCount > 0 ? " 【第" + v.FCount + "次(" + EConvert.ToShortDateString(v.FBGTime) + ")变更】" : " 【首次登记】");
                    ListItem item = new ListItem(str, v.FId);
                    ddlHis.Items.Add(item);
                }
                ddlHis.SelectedValue = Request.QueryString["FID"];
            }
            else
                tr_his.Visible = false;

        }
    }
    protected void ddlHis_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["FID"] = ddlHis.SelectedValue;
        showInfo();
    }
    //显示
    private void showInfo()
    {
        string FPrjId = EConvert.ToString(ViewState["FID"]);
        var prj = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId).Select(t => new
        {
            t.FId,
            t.FPrjName,
            t.FBaseinfoId,
            JSDW = db.CF_Ent_BaseInfo.Where(b => b.FId == t.FBaseinfoId).Select(b => b.FName).FirstOrDefault()
        }).FirstOrDefault();

        if (prj != null)
        {
            lit_PrjName.Text = prj.FPrjName;
            lit_JSDW.Text = prj.JSDW;
 

            //项目勘察合同备案
            showXMKCWT(FPrjId);
 
        }

    }
 

    #region 项目勘察合同备案

    //项目勘察合同备案
    private void showXMKCWT(string FPrjId)
    {
        int fMType = 280;//项目勘察合同备案
        var v = (from p in db.CF_Prj_Data
                 join a in db.CF_App_List on p.FId equals a.FLinkId
                 where a.FPrjId == FPrjId && p.FType == fMType && a.FManageTypeId == fMType
                 orderby p.FCreateTime
                 select new
                 {
                     p.FId,
                     p.FPrjId,
                     a.FLinkId,
                     p.FPrjName,
                     a.FYear,
                     a.FReportDate,
                     a.FAppDate,
                     a.FState,
                     a.FName,
                     FAppId = a.FId,
                     //勘察单位办理情况
                     vKC = (from l in db.CF_App_List //勘察项目信息备案283、勘察成果移交284
                            where l.FLinkId == a.FLinkId && (l.FManageTypeId == 283 || l.FManageTypeId == 284)
                            select l),
                     //见证单位办理情况
                     vJZ = (from l in db.CF_App_List //见证确认28001、见证人员安排28002、勘察项目见证28004、见证报告备案28003
                            where l.FLinkId == a.FLinkId && (l.FManageTypeId == 28001 || l.FManageTypeId == 28002 || l.FManageTypeId == 28004 || l.FManageTypeId == 28003)
                            select l),
                     ToKCEntName = db.CF_Prj_Ent.Where(t => t.FAppId == p.FId && t.FEntType == 15501).Select(t => t.FName).FirstOrDefault(),
                     ToJZEntName = db.CF_Prj_Ent.Where(t => t.FAppId == p.FId && t.FEntType == 126).Select(t => t.FName).FirstOrDefault(),
                     //判断是不是最新的那条
                     isOld = db.CF_App_List.Where(t => t.FPrjId == a.FPrjId && t.FManageTypeId == fMType).Max(t => t.FCreateTime).GetValueOrDefault() > a.FCreateTime,
                 }).ToList();

        rep_KCSMWT.DataSource = v;
        rep_KCSMWT.DataBind();

        //还没有办理
        if (v == null || v.Count == 0) lit_KCSMWT.Text = " <div class=\"empty f_l\"> 暂时没有办理 </div>";

    }
    //遍历
    protected void rep_KCSMWT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            DateTime FReportDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FReportDate"));//合同备案提交时间
            DateTime FAppDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FAppDate"));//确认时间


            string ToKCEntName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ToKCEntName"));
            string ToJZEntName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ToJZEntName"));

            //业务
            string s = "";
            s += "业务：<a title=\"查看合同备案详情\" href=\"javascript:showAddWindow('../../KC/ApplyKCXMWT/ApplyBaseInfo.aspx?FDataID=" + FLinkId + "', 900,700);\">" + FName + "</a>";
            s += "<br/>合同备案提交时间：<span>[" + FReportDate.ToString("yyyy-MM-dd") + "]</span>";
            ((Literal)e.Item.FindControl("lit_Content")).Text = s;


            #region 勘察单位办理结果

            //合同备案单位
            s = "勘察单位：<font color='#378BB0'>" + ToKCEntName + "</font>";
            s += "</br>(1)确认：";
            switch (FState)
            {
                case "0"://未上报
                    s += "<font color='#888888'>未上报</font>";
                    break;
                case "1"://已上报
                    s += "<font color='888888'>还未办理</font>";
                    break;
                case "2"://被退回
                    s += "<a href=\"javascript:showAddWindow('../../KC/applykcxmwt/Report.aspx?FAppId=" + FAppId + "',600,580);\">";
                    s += "<font color='red'>被退回</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]";
                    s += "</a>";
                    break;
                case "6"://已办结
                    s += "<a href=\"javascript:showAddWindow('../../KC/applykcxmwt/Report.aspx?FAppId=" + FAppId + "',600,580);\">";
                    s += "<font color='green'>已确认合同备案</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]";
                    s += "</a>";
                    break;
                case "7"://不予接受
                    s += "<a href=\"javascript:showAddWindow('../../KC/applykcxmwt/Report.aspx?FAppId=" + FAppId + "',600,580);\">";
                    s += "<font color='red'>不予接受</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]";
                    s += "</a>";
                    break;
            }

            IQueryable<CF_App_List> vKC = DataBinder.Eval(e.Item.DataItem, "vKC") as IQueryable<CF_App_List>;

            //勘察项目信息备案283
            s += "</br>(2)勘察项目信息备案：";
            var v283 = vKC.Where(t => t.FManageTypeId == 283).FirstOrDefault();
            if (v283 != null)
            {
                if (v283.FState == 6)
                {
                    s += "<a href=\"javascript:;\">";
                    s += "<font color='green'>已经备案</font>";
                    s += " [" + EConvert.ToDateTime(v283.FAppDate).ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    s += "<font color='blue'>正在备案</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";

            //勘察成果移交284
            s += "</br>(3)勘察成果移交：";
            var v284 = vKC.Where(t => t.FManageTypeId == 284).FirstOrDefault();
            if (v284 != null)
            {
                if (v284.FState == 6)
                {
                    s += "<a href=\"javascript:showAddWindow('../../KC/ApplyKCCGYJ/ApplyBaseInfo.aspx?FAppId=" + FAppId + "',700,680);\">";
                    s += "<font color='green'>已经移交</font>";
                    s += " [" + EConvert.ToDateTime(v284.FAppDate).ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    s += "<font color='blue'>正在移交</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";


            ((Literal)e.Item.FindControl("lit_ContentKC")).Text = s;

            #endregion


            #region 见证单位办理结果

            IQueryable<CF_App_List> vJZ = DataBinder.Eval(e.Item.DataItem, "vJZ") as IQueryable<CF_App_List>;
            string FStateJZ = "0";
            var v28001 = vJZ.Where(t => t.FManageTypeId == 28001).FirstOrDefault();


            //合同备案单位
            string r = "见证单位：<font color='#378BB0'>" + ToJZEntName + "</font>";
            r += "</br>(1)确认：";
            if (v28001 != null)
            {
                FStateJZ = v28001.FState.GetValueOrDefault().ToString();
                switch (FStateJZ)
                {
                    case "0"://未上报
                        r += "<font color='#888888'>未上报</font>";
                        break;
                    case "1"://已上报
                        r += "<font color='#888888'>还未办理</font>";
                        break;
                    case "2"://被退回 
                        r += "<a href=\"javascript:showAddWindow('../../JZDW/applykcxmwt/Report.aspx?FAppId=" + v28001.FId + "',600,500);\">";
                        r += "<font color='red'>被退回</font> [" + EConvert.ToDateTime(v28001.FAppDate).ToString("yyyy-MM-dd") + "]";
                        r += "</a>";
                        break;
                    case "6"://已办结
                        r += "<a href=\"javascript:showAddWindow('../../JZDW/applykcxmwt/Report.aspx?FAppId=" + v28001.FId + "',600,500);\">";
                        r += "<font color='green'>已确认合同备案</font> [" + EConvert.ToDateTime(v28001.FAppDate).ToString("yyyy-MM-dd") + "]";
                        r += "</a>";
                        break;
                    case "7"://不予接受
                        r += "<a href=\"javascript:showAddWindow('../../JZDW/applykcxmwt/Report.aspx?FAppId=" + v28001.FId + "',600,500);\">";
                        s += "<font color='red'>不予接受</font> [" + EConvert.ToDateTime(v28001.FAppDate).ToString("yyyy-MM-dd") + "]";
                        r += "</a>";
                        break;
                }
            }
            //见证人员安排28002
            r += "</br>(2)见证人员安排：";
            var v28002 = vJZ.Where(t => t.FManageTypeId == 28002).FirstOrDefault();
            if (v28002 != null)
            {
                if (v28002.FState == 6)
                {
                    r += "<a href=\"javascript:showAddWindow('../../JZDW/applykcxmwt/Staffing.aspx?FAppId=" + v28002.FId + "',680,600);\">";
                    r += "<font color='green'>已经安排</font>";
                    r += " [" + EConvert.ToDateTime(v28002.FAppDate).ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    r += "<font color='blue'>正在安排</font>";
            }
            else
                r += "<font color='#888888'>未开始</font>";

            //勘察项目见证28004
            r += "</br>(3)勘察项目见证：";
            var v28004 = vJZ.Where(t => t.FManageTypeId == 28004).FirstOrDefault();
            if (v28004 != null)
            {
                if (v28004.FState == 6)
                {
                    r += "<a href=\"javascript:showAddWindow('../../JZDW/ApplyXMJZ/Report.aspx?FAppId=" + v28004.FId + "',680,500);\">";
                    r += "<font color='green'>已经完成见证意见</font>";
                    r += " [" + EConvert.ToDateTime(v28004.FAppDate).ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    r += "<font color='blue'>正在见证</font>";
            }
            else
                r += "<font color='#888888'>未开始</font>";

            //见证报告备案28003
            r += "</br>(4)见证报告备案：";
            var v28003 = vJZ.Where(t => t.FManageTypeId == 28003).FirstOrDefault();
            if (v28003 != null)
            {
                if (v28003.FState == 6)
                {
                    r += "<a href=\"javascript:;\">";
                    r += "<font color='green'>已经备案</font>";
                    r += " [" + EConvert.ToDateTime(v28003.FAppDate).ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    r += "<font color='blue'>正在备案</font>";
            }
            else
                r += "<font color='#888888'>未开始</font>";


            ((Literal)e.Item.FindControl("lit_ContentJZ")).Text = r;

            #endregion

        }
    }

    #endregion

     
}
