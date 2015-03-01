using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;

public partial class KcsjSgt_Statistics_all : System.Web.UI.Page
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


            //勘察文件审查合同备案
            showKCWJSCWT(FPrjId);

            //施工图设计文件审查合同备案
            SGTSJWJSCWT(FPrjId);
        }

    }


    #region 勘察文件审查合同备案

    //勘察文件审查合同备案
    private void showKCWJSCWT(string FPrjId)
    {
        string FBaseinfoId = CurrentEntUser.EntId;
        int fMType = 287;//勘察文件审查合同备案
        var v = (from p in db.CF_Prj_Data
                 join a in db.CF_App_List on p.FAppId equals a.FId
                 where a.FPrjId == FPrjId && a.FToBaseinfoId == FBaseinfoId && p.FType == fMType && a.FManageTypeId == fMType
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
                     //受合同备案企业名称
                     ToEntName = db.CF_Prj_Ent.Where(t => t.FAppId == a.FId && t.FEntType == 145).Select(t => t.FName).FirstOrDefault(),
                     //判断是不是最新的那条
                     isOld = db.CF_App_List.Where(t => t.FPrjId == a.FPrjId && t.FManageTypeId == fMType).Max(t => t.FCreateTime).
                     GetValueOrDefault() > a.FCreateTime
                 }).ToList();

        rep_KCWJSCWT.DataSource = v;
        rep_KCWJSCWT.DataBind();

        //还没有办理
        if (v == null || v.Count == 0) lit_KCWJSCWT.Text = " <div class=\"empty f_l\"> 暂时没有办理或不是本单位办理的 </div>";

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

            string FBaseinfoId = CurrentEntUser.EntId;

            //是否二审。
            int n = EConvert.ToInt(FReportCount);

            //合同备案单位
            string s = "";
            s += "业务：<a title=\"查看合同备案详情\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCWJSCWTSL/ApplyBaseInfo.aspx?FDataID=" + FLinkId + "', 900,700);\">" + FName + "</a>";
            s += " [<tt>" + n + "审</tt>]";
            s += "<br/>合同备案提交时间：<span>[" + FReportDate.ToString("yyyy-MM-dd") + "]</span>";
            s += "<br/>合同备案设计单位：<span>" + ToEntName + "</span>";


            //确认状态
            s += "</br>(1)确认：";
            switch (FState)
            {
                case "0"://未上报
                    s += "<font color='#888888'>未上报</font>";
                    break;
                case "1"://已上报
                    s += "<font color='#888888'>还未确认</font>";
                    break;
                case "2"://被退回
                    s += "<a href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='red'>退回，补充材料</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "6"://已办结
                    s += "<a href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='green'>已确认</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "7"://不予接受
                    s += "<a href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='red'>不予接受</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";

                    break;
            }

            //程序性审查、技术性审查、勘察文件审查备案 结果
            var v = (from a in db.CF_App_List
                     join id in db.CF_App_Idea.Where(t => t.FUserId == null || t.FType == 290) on a.FId equals id.FLinkId into idea
                     from i in idea.DefaultIfEmpty()
                     where a.FBaseinfoId == FBaseinfoId && a.FLinkId == FLinkId && (a.FManageTypeId == 28801 || a.FManageTypeId == 28803 || a.FManageTypeId == 290)
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


    #region 施工图设计文件审查合同备案

    //施工图设计文件审查合同备案
    private void SGTSJWJSCWT(string FPrjId)
    {
        string FBaseinfoId = CurrentEntUser.EntId;
        int fMType = 300;//施工图设计文件审查合同备案
        var v = (from p in db.CF_Prj_Data
                 join a in db.CF_App_List on p.FAppId equals a.FId
                 where a.FToBaseinfoId == FBaseinfoId && a.FPrjId == FPrjId && p.FType == fMType && a.FManageTypeId == fMType
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
                     //受合同备案企业名称
                     ToEntName = db.CF_Prj_Ent.Where(t => t.FAppId == a.FId && t.FEntType == 145).Select(t => t.FName).FirstOrDefault(),
                     //判断是不是最新的那条
                     isOld = db.CF_App_List.Where(t => t.FPrjId == a.FPrjId && t.FManageTypeId == fMType).Max(t => t.FCreateTime).
                     GetValueOrDefault() > a.FCreateTime
                 }).ToList();

        rep_SGTSJWJSCWT.DataSource = v;
        rep_SGTSJWJSCWT.DataBind();

        //还没有办理
        if (v == null || v.Count == 0) lit_SGTSJWJSCWT.Text = " <div class=\"empty f_l\"> 暂时没有办理或不是本单位办理的 </div>";

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

            string FBaseinfoId = CurrentEntUser.EntId;

            //是否二审。
            int n = EConvert.ToInt(FReportCount);

            //合同备案单位
            string s = "";
            s += "业务：<a title=\"查看合同备案详情\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplySGTSJWJSCWTSL/ApplyBaseInfo.aspx?FDataID=" + FLinkId + "', 900,700);\">" + FName + "</a>";
            s += " [<tt>" + n + "审</tt>]";
            s += "<br/>合同备案提交时间：<span>[" + FReportDate.ToString("yyyy-MM-dd") + "]</span>";
            s += "<br/>合同备案设计单位：<span>" + ToEntName + "</span>";


            //确认状态
            s += "<br/>(1)确认：";
            switch (FState)
            {
                case "0"://未上报
                    s += "<font color='#444444'>未上报</font>";
                    break;
                case "1"://已上报
                    s += "<font color='444444'>还未办理</font>";
                    break;
                case "2"://被退回
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplySGTSJWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='red'>被退回</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "6"://已办结
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplySGTSJWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='green'>已确认</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "7"://不予接受
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='red'>不予接受</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
            }


            //程序性审查、技术性审查、勘察文件审查备案 结果
            var v = (from a in db.CF_App_List
                     join id in db.CF_App_Idea.Where(t => t.FUserId == null || t.FType == 305) on a.FId equals id.FLinkId into idea
                     from i in idea.DefaultIfEmpty()
                     where a.FBaseinfoId == FBaseinfoId && a.FLinkId == FLinkId && (a.FManageTypeId == 30101 || a.FManageTypeId == 30103 || a.FManageTypeId == 305)
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

}
