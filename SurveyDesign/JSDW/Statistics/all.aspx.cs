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
        btnReturn.Focus();
    }
    private void BindControl()
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
    //显示
    private void showInfo()
    {
        string FPrjId = EConvert.ToString(ViewState["FID"]);
        //查出所有变更集的项目FID
        List<string> prjIDList = db.CF_Prj_BaseInfo.Where(t => t.FLinkId == FPrjId).Select(t => t.FId).ToList();
        prjIDList.Add(FPrjId);//把自已也要加进来


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


            //初步设计合同备案
            showCBSJWT(FPrjId);

            //初步设计文件审查申报
            showCBSJSC(FPrjId);

            //项目勘察合同备案
            showXMKCWT(FPrjId);

            //勘察文件审查合同备案
            showKCWJSCWT(FPrjId);

            //施工图设计文件编制合同备案
            SGTWJBZWT(FPrjId);

            //施工图设计文件审查合同备案
            SGTSJWJSCWT(FPrjId);
        }

    }

    #region 初步设计合同备案

    //初步设计合同备案
    private void showCBSJWT(string FPrjId)
    {
        int fMType = 291;//初步设计合同备案
        var v = (from a in db.CF_App_List
                 join d in db.CF_Prj_Data on a.FLinkId equals d.FId
                 where a.FPrjId == FPrjId && a.FManageTypeId == fMType
                 orderby a.FCreateTime
                 select new
                 {
                     a.FId,
                     a.FName,
                     a.FReportDate,
                     a.FState,
                     a.FLinkId,
                     a.FAppDate,
                     a.FReportCount,
                     ToEntName = db.CF_Prj_Ent.Where(t => t.FAppId == a.FId && t.FEntType == 155).Select(t => t.FName).FirstOrDefault(),
                     //判断是不是最新的那条
                     isOld = db.CF_App_List.Where(t => t.FPrjId == a.FPrjId && t.FManageTypeId == fMType).Max(t => t.FCreateTime).GetValueOrDefault() > a.FCreateTime,
                 }).ToList();

        rep_CBSJWT.DataSource = v;
        rep_CBSJWT.DataBind();

        //还没有办理
        if (v == null || v.Count == 0) lit_CBSJWT.Text = " <div class=\"empty f_l\"> 暂时没有办理 </div>";

    }
    //遍历
    protected void rep_CBSJWT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            string ToEntName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ToEntName"));
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            DateTime FReportDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FReportDate"));//合同备案提交时间
            DateTime FAppDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FAppDate"));//确认时间

            //合同备案单位
            string s = "";
            s += "业务：<a title=\"查看合同详情\" href=\"javascript:showAddWindow('../../SJ/applycbsjwt/ApplyBaseInfo.aspx?FDataID=" + FLinkId + "', 900,700);\">" + FName + "</a>";
            s += "<br/>合同提交时间：" + (EConvert.ToInt(FState) > 1 ? "<span>[" + FReportDate.ToString("yyyy-MM-dd") + "]</span>" : "");
            s += "<br/>合同提交设计单位：<span>" + ToEntName + "</span>";

            //确认状态
            s += "<br/>(1)合同确认：";
            switch (FState)
            {
                case "0"://未上报
                    s += "<font color='#888888'>未提交</font>";
                    break;
                case "1"://已上报
                    s += "<font color='#888888'>还未确认</font>";
                    break;
                case "2"://被退回
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applycbsjwt/Report.aspx?FAppId=" + FID + "',600,400);\"><font color='red'>资料不完整不接受合同</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "6"://已办结
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applycbsjwt/Report.aspx?FAppId=" + FID + "',600,500);\"><font color='green'>资料完整接受合同</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "7"://不予接受
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applycbsjwt/Report.aspx?FAppId=" + FID + "',600,500);\"><font color='red'>不予接受</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
            }

            //人员安排29201、初步设计人员意见29202、成果移交293 
            var v = (from a in db.CF_App_List
                     where a.FLinkId == FLinkId &&
                     (a.FManageTypeId == 29201 || a.FManageTypeId == 29202
                     || a.FManageTypeId == 293)
                     select new
                     {
                         a.FId,
                         a.FState,
                         a.FManageTypeId,
                         a.FAppDate,
                     }).ToList();
            //人员安排
            s += "<br/>(2)人员安排：";
            var v29201 = v.Where(t => t.FManageTypeId == 29201).FirstOrDefault();
            if (v29201 != null)
            {
                if (v29201.FState == 6)
                {
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applycbsjwt/PlanPerson.aspx?FAppId=" + v29201.FId + "',700,600);\">";
                    s += "<font color='green'>已经安排</font>";
                    s += " [" + v29201.FAppDate.Value.ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    s += "<font color='blue'>正在安排</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";

            //人员意见
            s += "</br>(3)人员意见：";
            var v29202 = v.Where(t => t.FManageTypeId == 29202).FirstOrDefault();
            if (v29202 != null)
            {
                if (v29202.FState == 6)
                {
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../SJ/applycbsjryyj/Report.aspx?FAppId=" + v29202.FId + "',700,600);\">";
                    s += "<font color='green'>已经办理</font>";
                    s += " <font color='#666666'>[" + v29202.FAppDate.Value.ToString("yyyy-MM-dd") + "]</font></a>";
                }
                else
                    s += "<font color='blue'>正在办理</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";

            //成果移交
            s += "<br/>(4)成果移交：";
            var v293 = v.Where(t => t.FManageTypeId == 293).FirstOrDefault();
            if (v293 != null)
            {
                if (v293.FState == 6)
                {
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applycbsjcgyj/ApplyBaseInfo.aspx?FAppId=" + v293.FId + "',700,600);\">";
                    s += "<font color='green'>已经移交</font>";
                    s += " [" + v293.FAppDate.Value.ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    s += "<font color='blue'>正在办理</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";

            ((Literal)e.Item.FindControl("lit_Content")).Text = s;
        }
    }

    #endregion


    #region 初步设计文件审查申报

    //初步设计文件审查申报
    private void showCBSJSC(string FPrjId)
    {
        int fMType = 294;//初步设计文件审查申报
        var v = (from a in db.CF_App_List
                 where a.FPrjId == FPrjId && a.FManageTypeId == fMType
                 orderby a.FCreateTime
                 select new
                 {
                     a.FId,
                     a.FReportDate,
                     a.FAppDate,
                     a.FState,
                     a.FName,
                     //判断是不是最新的那条
                     isOld = db.CF_App_List.Where(t => t.FPrjId == a.FPrjId && t.FManageTypeId == fMType).Max(t => t.FCreateTime).GetValueOrDefault() > a.FCreateTime,
                 }).ToList();

        rep_CBSJSC.DataSource = v;
        rep_CBSJSC.DataBind();


        //还没有办理
        if (v == null || v.Count == 0) lit_CBSJSC.Text = " <div class=\"empty f_l\"> 暂时没有办理 </div>";
    }

    protected void rep_CBSJSC_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
            int FState = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FState"));
            DateTime FReportDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FReportDate"));//提交时间
            DateTime FAppDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FAppDate"));//审批时间

            string s = "";
            s += "业务：<a href=\"javascript:\">" + FName + "</a>";
            s += "<br/>合同提交时间：" + (EConvert.ToInt(FState) > 1 ? "<span>[" + FReportDate.ToString("yyyy-MM-dd") + "]</span>" : "");
            s += "<br/>状态：";
            switch (FState)
            {
                case 0:
                    s += "<font color='#444444'>未提交</font>";
                    break;
                case 1:
                    s += "<font color='blue'>已上报</font>[" + FReportDate.ToString("yyyy-MM-dd") + "]";
                    break;
                case 2:
                    s += "<font color='red' style='cursor:hand;' onclick=\"showApproveWindow('ShowAppInfo.aspx?fid=" + FID + "',634,500)\">退回</font>[" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case 6:
                    s += "<font color='green'>已审查</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]";
                    break;
            }
            ((Literal)e.Item.FindControl("lit_Content")).Text = s;
        }
    }

    #endregion


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
                     vKC = (from l in db.CF_App_List //勘察项目信息备案283、勘察人员意见28301、勘察成果移交284
                            where l.FLinkId == a.FLinkId && (l.FManageTypeId == 283 || l.FManageTypeId == 28301 || l.FManageTypeId == 284)
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
            s += "业务：<a title=\"查看合同详情\" href=\"javascript:showAddWindow('../../KC/ApplyKCXMWT/ApplyBaseInfo.aspx?FDataID=" + FLinkId + "', 900,700);\">" + FName + "</a>";
            s += "<br/>合同备案提交时间：" + (EConvert.ToInt(FState) > 1 ? "<span>[" + FReportDate.ToString("yyyy-MM-dd") + "]</span>" : "");
            ((Literal)e.Item.FindControl("lit_Content")).Text = s;


            #region 勘察单位办理结果

            //合同备案单位
            s = "勘察单位：<font color='#378BB0'>" + ToKCEntName + "</font>";
            s += "</br>(1)合同确认：";
            switch (FState)
            {
                case "0"://未上报
                    s += "<font color='#888888'>未提交</font>";
                    break;
                case "1"://已上报
                    s += "<font color='888888'>还未办理</font>";
                    break;
                case "2"://被退回
                    s += "<a href=\"javascript:showAddWindow('../../KC/applykcxmwt/Report.aspx?FAppId=" + FAppId + "',600,580);\">";
                    s += "<font color='red'>资料不完整接受合同</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]";
                    s += "</a>";
                    break;
                case "6"://已办结
                    s += "<a href=\"javascript:showAddWindow('../../KC/applykcxmwt/Report.aspx?FAppId=" + FAppId + "',600,580);\">";
                    s += "<font color='green'>资料完整接受合同</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]";
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
            var v283 = vKC.Where(t => t.FManageTypeId == 283).OrderByDescending(t => t.FCreateTime).FirstOrDefault();
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

            //勘察项目人员意见28301
            s += "</br>(3)勘察人员意见：";
            var v28301 = vKC.Where(t => t.FManageTypeId == 28301).OrderByDescending(t => t.FCreateTime).FirstOrDefault();
            if (v28301 != null)
            {
                if (v28301.FState == 6)
                {
                    s += "<a href=\"javascript:showAddWindow('../../KC/ApplyRYYJ/Report.aspx?FAppId=" + v28301.FId + "',680,500);\">";
                    s += "<font color='green'>已经完成勘察意见</font>";
                    s += " <font color='#666666'>[" + EConvert.ToDateTime(v28301.FAppDate).ToString("yyyy-MM-dd") + "]</font></a>";
                }
                else
                    s += "<font color='blue'>正在勘察</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";

            //勘察成果移交284
            s += "</br>(4)勘察成果移交：";
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
            r += "</br>(1)合同确认：";
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
                        r += "<font color='red'>资料不完整不接受合同</font> [" + EConvert.ToDateTime(v28001.FAppDate).ToString("yyyy-MM-dd") + "]";
                        r += "</a>";
                        break;
                    case "6"://已办结
                        r += "<a href=\"javascript:showAddWindow('../../JZDW/applykcxmwt/Report.aspx?FAppId=" + v28001.FId + "',600,500);\">";
                        r += "<font color='green'>资料完整接受合同</font> [" + EConvert.ToDateTime(v28001.FAppDate).ToString("yyyy-MM-dd") + "]";
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


    #region 勘察文件审查合同备案

    //勘察文件审查合同备案
    private void showKCWJSCWT(string FPrjId)
    {
        int fMType = 287;//勘察文件审查合同备案
        var v = (from p in db.CF_Prj_Data
                 join a in db.CF_App_List on p.FAppId equals a.FId
                 where a.FPrjId == FPrjId && p.FType == fMType && a.FManageTypeId == fMType
                 orderby p.FCreateTime
                 select new
                 {
                     p.FId,
                     p.FPrjName,
                     a.FYear,
                     a.FReportDate,
                     a.FState,
                     a.FName,
                     a.FLinkId,
                     a.FAppDate,
                     a.FPrjId,
                     a.FReportCount,
                     p.FAppId,
                     //企业名称
                     ToEntName = db.CF_Prj_Ent.Where(t => t.FAppId == a.FId && t.FEntType == 145).Select(t => t.FName).FirstOrDefault(),
                     //判断是不是最新的那条
                     isOld = db.CF_App_List.Where(t => t.FPrjId == a.FPrjId && t.FManageTypeId == fMType).Max(t => t.FCreateTime).
                     GetValueOrDefault() > a.FCreateTime
                 }).ToList();

        rep_KCWJSCWT.DataSource = v;
        rep_KCWJSCWT.DataBind();

        //还没有办理
        if (v == null || v.Count == 0) lit_KCWJSCWT.Text = " <div class=\"empty f_l\"> 暂时没有办理 </div>";

    }
    //遍历
    protected void rep_KCWJSCWT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));//CF_Prj_Data.ID 
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            string FPrjName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjName"));
            string FReportCount = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FReportCount"));
            DateTime FReportDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FReportDate"));//提交时间
            DateTime FAppDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FAppDate"));//确认时间
            string FAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string ToEntName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ToEntName"));


            //是否二审。
            int n = EConvert.ToInt(FReportCount);

            //合同备案单位
            string s = "";
            s += "业务：<a title=\"查看合同详情\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCWJSCWTSL/ApplyBaseInfo.aspx?FDataID=" + FLinkId + "', 900,700);\">" + FName + "</a>";
            s += " [<tt>" + n + "审</tt>]";
            s += "<br/>合同提交时间：" + (EConvert.ToInt(FState) > 1 ? "<span>[" + FReportDate.ToString("yyyy-MM-dd") + "]</span>" : "");
            s += "<br/>合同提交设计单位：<span>" + ToEntName + "</span>";


            //确认状态
            s += "</br>(1)合同确认：";
            switch (FState)
            {
                case "0"://未上报
                    s += "<font color='#888888'>未提交</font>";
                    break;
                case "1"://已上报
                    s += "<font color='#888888'>还未确认</font>";
                    break;
                case "2"://被退回
                    s += "<a href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='red'>资料不完整不同意备案</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "6"://已办结
                    s += "<a href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='green'>资料完整同意备案</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "7"://不予接受
                    s += "<a href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='red'>不予接受</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";

                    break;
            }

            //程序性审查、技术性审查、勘察文件审查备案 结果
            var v = (from a in db.CF_App_List
                     join id in db.CF_App_Idea.Where(t => t.FUserId == null || t.FType == 290) on a.FId equals id.FLinkId into idea
                     from i in idea.DefaultIfEmpty()
                     where a.FLinkId == FLinkId && (a.FManageTypeId == 28801 || a.FManageTypeId == 28803 || a.FManageTypeId == 290)
                     select new
                     {
                         a.FId,
                         a.FState,
                         a.FManageTypeId,
                         a.FAppDate,
                         a.FCreateTime,
                         iDeaFID = i == null ? "" : i.FId,
                         FResult = i == null ? "" : i.FResult,
                         FResultInt = i == null ? 0 : i.FResultInt,
                         FAppTime = i == null ? DateTime.Now : i.FAppTime,
                         //技术性审查时，查人员是否有“补正材料”的意见
                         empIdea = (from em in db.CF_Emp_BaseInfo
                                    join id in db.CF_App_Idea on em.FId equals id.FUserId
                                    where id.FLinkId == a.FId && id.FResultInt == 7 && a.FManageTypeId == 28803
                                    select new
                                    {
                                        em.FId,
                                        em.FName,
                                        id.FAppTime
                                    }).FirstOrDefault()
                     }).ToList();
            //程序性审查
            s += "</br>(2)程序性审查：";
            var v28801 = v.Where(t => t.FManageTypeId == 28801).FirstOrDefault();
            if (v28801 != null)
            {
                if (!string.IsNullOrEmpty(v28801.iDeaFID))
                {
                    s += "<a href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCCXXSC/Report.aspx?FAppId=" + v28801.FId + "',700,480);\">";
                    if (v28801.FResultInt == 1)//合格
                    {
                        s += "<font color='green'>" + v28801.FResult + "</font>";
                    }
                    else//不合格
                    {
                        s += "<font color='red'>" + v28801.FResult + "</font>";
                    }
                    s += " [" + v28801.FAppTime.Value.ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    s += "<font color='blue'>正在办理</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";

            //技术性审查
            s += "</br>(3)技术性审查：";
            var v28803 = v.Where(t => t.FManageTypeId == 28803).FirstOrDefault();
            if (v28803 != null)
            {

                if (!string.IsNullOrEmpty(v28803.iDeaFID))
                {
                    s += "<a href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCJSXSC/Report.aspx?FAppId=" + v28803.FId + "',700,680);\">";
                    if (v28803.FResultInt == 6)//合格
                    {
                        s += "<font color='green'>" + v28803.FResult + "</font>";
                    }
                    else//不合格
                    {
                        s += "<font color='red'>" + v28803.FResult + "</font>";
                    }
                    s += " [" + v28803.FAppTime.Value.ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    s += "<font color='blue'>正在办理</font></a>";

                //技术性审查时，查人员是否有“补正材料”的意见
                if (v28803.empIdea != null)
                {
                    s += "</br><tt style='padding-left:90px;'>（审查人“" + v28803.empIdea.FName + "”：补正材料）</tt>";
                }
            }
            else
                s += "<font color='#888888'>未开始</font>";

            //勘察文件审查备案
            s += "</br>(4)勘察文件审查备案：";
            var v290 = v.Where(t => t.FManageTypeId == 290).FirstOrDefault();
            if (v290 != null)
            {
                if (!string.IsNullOrEmpty(v290.iDeaFID))
                {
                    s += "<a href=\"javascript:showAddWindow('ShowAppInfo.aspx?FID=" + v290.FId + "',500,350);\">";
                    if (v290.FResultInt == 1)//同意备案（管理部门审核）
                    {
                        s += "<font color='green'>" + v290.FResult + "</font>";
                    }
                    else//不同意（管理部门审核）
                    {
                        s += "<font color='red'>" + v290.FResult + "</font>";
                    }
                    s += " [" + v290.FAppTime.Value.ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    s += "<font color='blue'>正在办理</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";


            ((Literal)e.Item.FindControl("lit_Content")).Text = s;


        }
    }

    #endregion


    #region 施工图设计文件编制合同备案

    //施工图设计文件编制合同备案
    private void SGTWJBZWT(string FPrjId)
    {
        int fMType = 296;//施工图设计文件编制合同备案
        var v = (from p in db.CF_Prj_Data
                 join a in db.CF_App_List on p.FAppId equals a.FId
                 where a.FPrjId == FPrjId && p.FType == fMType && a.FManageTypeId == fMType
                 orderby p.FCreateTime
                 select new
                 {
                     a.FId,
                     a.FReportDate,
                     a.FState,
                     a.FName,
                     a.FLinkId,
                     a.FAppDate,
                     a.FPrjId,
                     a.FReportCount,
                     p.FAppId,
                     //企业名称
                     ToEntName = db.CF_Prj_Ent.Where(t => t.FAppId == a.FId && t.FEntType == 155).Select(t => t.FName).FirstOrDefault(),
                     //判断是不是最新的那条
                     isOld = db.CF_App_List.Where(t => t.FPrjId == a.FPrjId && t.FManageTypeId == fMType).Max(t => t.FCreateTime).GetValueOrDefault() > a.FCreateTime,

                 }).ToList();

        rep_SGTWJBZWT.DataSource = v;
        rep_SGTWJBZWT.DataBind();

        //还没有办理
        if (v == null || v.Count == 0) lit_SGTWJBZWT.Text = " <div class=\"empty f_l\"> 暂时没有办理 </div>";

    }
    //遍历
    protected void rep_SGTWJBZWT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));//CF_Prj_Data.ID 
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            DateTime FAppDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FAppDate"));//确认时间
            DateTime FReportDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FReportDate"));//提交时间
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            string FAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string ToEntName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ToEntName"));


            //合同备案单位
            string s = "";
            s += "业务：<a title=\"查看合同详情\" href=\"javascript:showAddWindow('../../SJ/applysjwjbzwt/ApplyBaseInfo.aspx?FDataID=" + FLinkId + "', 900,700);\">" + FName + "</a>";
            s += "<br/>合同备案提交时间：" + (EConvert.ToInt(FState) > 1 ? "<span>[" + FReportDate.ToString("yyyy-MM-dd") + "]</span>" : "");
            s += "<br/>合同备案设计单位：<span>" + ToEntName + "</span>";



            //确认状态
            s += "</br>(1)合同确认：";
            switch (FState)
            {
                case "0"://未上报
                    s += "<font color='#888888'>未提交</font>";
                    break;
                case "1"://已上报
                    s += "<font color='#888888'>还未确认</font>";
                    break;
                case "2"://被退回
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applysjwjbzwt/Report.aspx?FAppId=" + FAppId + "',600,500);\"><font color='red'>资料不完整不接受合同</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "6"://已办结
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applysjwjbzwt/Report.aspx?FAppId=" + FAppId + "',600,500);\"><font color='green'>资料完整接受合同</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "7"://不予接受
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applysjwjbzwt/Report.aspx?FAppId=" + FAppId + "',600,500);\"><font color='red'>不予接受</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
            }



            //人员安排29701、人员意见29702、成果移交298
            var v = (from a in db.CF_App_List
                     where a.FLinkId == FLinkId
                     && (a.FManageTypeId == 29701 || a.FManageTypeId == 29702
                     || a.FManageTypeId == 298)
                     select new
                     {
                         a.FId,
                         a.FState,
                         a.FManageTypeId,
                         a.FAppDate,

                     }).ToList();
            //人员安排
            s += "</br>(2)人员安排：";
            var v29701 = v.Where(t => t.FManageTypeId == 29701).FirstOrDefault();
            if (v29701 != null)
            {
                if (v29701.FState == 6)
                {
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applysjwjbzwt/PlanPerson.aspx?FAppId=" + v29701.FId + "',700,680);\">";
                    s += "<font color='green'>已经安排</font>";
                    s += " [" + v29701.FAppDate.Value.ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    s += "<font color='blue'>正在安排</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";
            //人员意见
            s += "</br>(3)人员意见：";
            var v29702 = v.Where(t => t.FManageTypeId == 29702).FirstOrDefault();
            if (v29702 != null)
            {
                if (v29702.FState == 6)
                {
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../SJ/applysjwjbzryyj/Report.aspx?FAppId=" + v29702.FId + "',700,600);\">";
                    s += "<font color='green'>已经办理</font>";
                    s += " <font color='#666666'>[" + v29702.FAppDate.Value.ToString("yyyy-MM-dd") + "]</font></a>";
                }
                else
                    s += "<font color='blue'>正在办理</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";
            //成果移交
            s += "</br>(4)成果移交：";
            var v298 = v.Where(t => t.FManageTypeId == 298).FirstOrDefault();
            if (v298 != null)
            {
                if (v298.FState == 6)
                {
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applysjwjbzcgyj/ApplyBaseInfo.aspx?FAppId=" + v298.FId + "',700,680);\">";
                    s += "<font color='green'>已经移交</font>";
                    s += " [" + v298.FAppDate.Value.ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    s += "<font color='blue'>正在办理</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";



            ((Literal)e.Item.FindControl("lit_Content")).Text = s;


        }
    }

    #endregion


    #region 施工图设计文件审查合同备案

    //施工图设计文件审查合同备案
    private void SGTSJWJSCWT(string FPrjId)
    {
        int fMType = 300;//施工图设计文件审查合同备案
        var v = (from p in db.CF_Prj_Data
                 join a in db.CF_App_List on p.FAppId equals a.FId
                 where a.FPrjId == FPrjId && p.FType == fMType && a.FManageTypeId == fMType
                 orderby p.FCreateTime
                 select new
                 {
                     p.FId,
                     p.FPrjName,
                     a.FYear,
                     a.FReportDate,
                     a.FState,
                     a.FName,
                     a.FLinkId,
                     a.FAppDate,
                     a.FPrjId,
                     a.FReportCount,
                     p.FAppId,
                     //企业名称
                     ToEntName = db.CF_Prj_Ent.Where(t => t.FAppId == a.FId && t.FEntType == 145).Select(t => t.FName).FirstOrDefault(),
                     //判断是不是最新的那条
                     isOld = db.CF_App_List.Where(t => t.FPrjId == a.FPrjId && t.FManageTypeId == fMType).Max(t => t.FCreateTime).
                     GetValueOrDefault() > a.FCreateTime
                 }).ToList();

        rep_SGTSJWJSCWT.DataSource = v;
        rep_SGTSJWJSCWT.DataBind();

        //还没有办理
        if (v == null || v.Count == 0) lit_SGTSJWJSCWT.Text = " <div class=\"empty f_l\"> 暂时没有办理 </div>";

    }
    //遍历
    protected void rep_SGTSJWJSCWT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));//CF_Prj_Data.ID 
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            string FPrjName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjName"));
            string FReportCount = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FReportCount"));
            DateTime FReportDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FReportDate"));//提交时间
            DateTime FAppDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FAppDate"));//确认时间
            string FAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string ToEntName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ToEntName"));


            //是否二审。
            int n = EConvert.ToInt(FReportCount);

            //合同备案单位
            string s = "";
            s += "业务：<a title=\"查看合同详情\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplySGTSJWJSCWTSL/ApplyBaseInfo.aspx?FDataID=" + FLinkId + "', 900,700);\">" + FName + "</a>";
            s += " [<tt>" + n + "审</tt>]";
            s += "<br/>合同提交时间：" + (EConvert.ToInt(FState) > 1 ? "<span>[" + FReportDate.ToString("yyyy-MM-dd") + "]</span>" : "");
            s += "<br/>合同提交设计单位：<span>" + ToEntName + "</span>";


            //确认状态
            s += "</br>(1)合同确认：";
            switch (FState)
            {
                case "0"://未上报
                    s += "<font color='#888888'>未提交</font>";
                    break;
                case "1"://已上报
                    s += "<font color='#888888'>还未办理</font>";
                    break;
                case "2"://被退回
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplySGTSJWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='red'>资料不完整不接受合同</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "6"://已办结
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplySGTSJWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='green'>资料完整接受合同</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "7"://不予接受
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='red'>不予接受</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
            }


            //程序性审查、技术性审查、勘察文件审查备案 结果
            var v = (from a in db.CF_App_List
                     join id in db.CF_App_Idea.Where(t => t.FUserId == null || t.FType == 305) on a.FId equals id.FLinkId into idea
                     from i in idea.DefaultIfEmpty()
                     where a.FLinkId == FLinkId && (a.FManageTypeId == 30101 || a.FManageTypeId == 30103 || a.FManageTypeId == 305)
                     select new
                     {
                         a.FId,
                         a.FState,
                         a.FManageTypeId,
                         a.FAppDate,
                         iDeaFID = i == null ? "" : i.FId,
                         FResult = i == null ? "" : i.FResult,
                         FResultInt = i == null ? 0 : i.FResultInt,
                         FAppTime = i == null ? DateTime.Now : i.FAppTime,
                         //技术性审查时，查人员是否有“补正材料”的意见
                         empIdea = (from em in db.CF_Emp_BaseInfo
                                    join id in db.CF_App_Idea on em.FId equals id.FUserId
                                    where id.FLinkId == a.FId && id.FResultInt == 7 && a.FManageTypeId == 30103
                                    select new
                                    {
                                        em.FId,
                                        em.FName,
                                        id.FAppTime
                                    }).FirstOrDefault()
                     }).ToList();
            //程序性审查
            s += "</br>(2)程序性审查：";
            var v30101 = v.Where(t => t.FManageTypeId == 30101).FirstOrDefault();
            if (v30101 != null)
            {
                if (!string.IsNullOrEmpty(v30101.iDeaFID))
                {
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplySGTSJCXXSC/Report.aspx?FAppId=" + v30101.FId + "',700,480);\">";
                    if (v30101.FResultInt == 1)//合格
                    {
                        s += "<font color='green'>" + v30101.FResult + "</font>";
                    }
                    else//不合格
                    {
                        s += "<font color='red'>" + v30101.FResult + "</font>";
                    }
                    s += " [" + v30101.FAppTime.Value.ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    s += "<font color='blue'>正在办理</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";

            //技术性审查
            s += "</br>(3)技术性审查：";
            var v30103 = v.Where(t => t.FManageTypeId == 30103).FirstOrDefault();
            if (v30103 != null)
            {
                if (!string.IsNullOrEmpty(v30103.iDeaFID))
                {
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplySGTSJJSXSC/Report.aspx?FAppId=" + v30103.FId + "',700,680);\">";
                    if (v30103.FResultInt == 6)//通过
                    {
                        s += "<font color='green'>" + v30103.FResult + "</font>";
                    }
                    else//修改
                    {
                        s += "<font color='red'>" + v30103.FResult + "</font>";
                    }
                    s += " [" + v30103.FAppTime.Value.ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    s += "<font color='blue'>正在办理</font>";

                //技术性审查时，查人员是否有“补正材料”的意见
                if (v30103.empIdea != null)
                {
                    s += "</br><tt style='padding-left:90px;'>（审查人“" + v30103.empIdea.FName + "”：补正材料）</tt>";
                }
            }
            else
                s += "<font color='#888888'>未开始</font>";

            //施工图设计文件备案
            s += "</br>(4)施工图设计文件备案：";
            var v305 = v.Where(t => t.FManageTypeId == 305).FirstOrDefault();
            if (v305 != null)
            {
                if (!string.IsNullOrEmpty(v305.iDeaFID))
                {
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('LookIdea.aspx?FID=" + v305.iDeaFID + "',500,350);\">";
                    if (v305.FResultInt == 1)//同意备案（管理部门审核）
                    {
                        s += "<font color='green'>" + v305.FResult + "</font>";
                    }
                    else//不同意（管理部门审核）
                    {
                        s += "<font color='red'>" + v305.FResult + "</font>";
                    }
                    s += " [" + v305.FAppTime.Value.ToString("yyyy-MM-dd") + "]</a>";
                }
                else
                    s += "<font color='blue'>正在办理</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";


            ((Literal)e.Item.FindControl("lit_Content")).Text = s;


        }
    }

    #endregion

    protected void ddlHis_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["FID"] = ddlHis.SelectedValue;
        showInfo();
    }
}
