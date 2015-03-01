using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using Approve.RuleApp;
using ProjectData;
using System.Linq;
using ProjectBLL;
using System.Collections.Generic;
public partial class EvaluateEntApp_main_AppXMKCWT : System.Web.UI.Page
{
    RApp ra = new RApp();
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    public int fMType = 280;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            conBind();
            ShowInfo();
        }
    }

    //绑定选项
    private void conBind()
    {
        btnPup.Text = "新增" + rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber=" + fMType + "");

        for (int i = DateTime.Now.Year; i >= 2013; i--)
        {
            drop_FYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        drop_FYear.Items.Insert(0, new ListItem("--全部--", ""));

        pageTool tool = new pageTool(this.Page);
        //审查不通过时可做二次勘察（从审查业务页面转过来）
        string FPrjId = Request.QueryString["FPrjId"];
        if (!string.IsNullOrEmpty(FPrjId))
        {
            //先查出最后一次勘察，并查出做过几次了。
            var v = (from t in db.CF_App_List
                     join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                     where t.FPrjId == FPrjId && t.FManageTypeId == fMType
                     orderby t.FCreateTime descending
                     select new { t.FId, t.FCount, t.FState, t.FLinkId, d.FPrjName }).FirstOrDefault();
            if (v != null)
            {
                //查出成果移交业务
                var s = db.CF_App_List.Where(t => t.FLinkId == v.FLinkId && t.FManageTypeId == 284).Select(t => t.FState).FirstOrDefault();

                if (s != null && s == 6 && v.FState == 6)
                { //只都为6时才可以做二次勘察

                    appTab.Visible = false;
                    applyInfo.Visible = true;
                    ViewState["FMNUMBER"] = fMType;
                    t_FYear.Text = DateTime.Now.Year.ToString();
                    t_FName.Text = t_FYear.Text + "年 " + db.getManageTypeName(fMType);
                    HPid.Value = FPrjId;
                    txtFPrjName.Text = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId).Select(t => t.FPrjName).FirstOrDefault();
                    t_OldFAppId.Value = v.FId;
                    t_FCount.Value = (v.FCount.GetValueOrDefault() + 1).ToString();
                    t_FPriItemId.Value = Request.QueryString["FLinkId"];//保存审查不合格那条审查业务的CF_Prj_Data.FID
                }
                else
                { //其它情况都是正在办理的

                    //查出来。 
                    ttFPrjName.Text = v.FPrjName;
                    ShowInfo();

                    tool.ExecuteScript("if (confirm('该项目正在做勘察。\\n\\r\\n\\r确定：查询该项目勘察情况\\n\\r取消：返回勘察文件审查合同列表')){}else{history.back();}");
                }
            }
        }
    }

    //显示
    private void ShowInfo()
    {
        var v = (from p in db.CF_Prj_Data
                 join a in db.CF_App_List on p.FId equals a.FLinkId
                 where p.FBaseInfoId == CurrentEntUser.EntId && p.FType == fMType && a.FManageTypeId == fMType
                 orderby p.FCreateTime descending
                 select new
                 {
                     p.FId,
                     DataFID = p.FId,
                     p.FPrjId,
                     a.FLinkId,
                     p.FPrjName,
                     p.FPriItemId,
                     a.FYear,
                     a.FReportDate,
                     a.FAppDate,
                     a.FState,
                     a.FName,
                     a.FCount,
                     FAppId = a.FId,
                     //勘察单位办理情况
                     vKC = (from l in db.CF_App_List //勘察项目信息备案283、勘察人员意见28301、勘察成果移交284
                            where l.FLinkId == a.FLinkId && (l.FManageTypeId == 283 || l.FManageTypeId == 28301 || l.FManageTypeId == 284)
                            select l),
                     //见证单位办理情况
                     vJZ = (from l in db.CF_App_List //见证受理28001、见证人员安排28002、勘察项目见证28004、见证报告备案28003
                            where l.FLinkId == a.FLinkId && (l.FManageTypeId == 28001 || l.FManageTypeId == 28002 || l.FManageTypeId == 28004 || l.FManageTypeId == 28003)
                            select l),
                     ToKCEntName = db.CF_Prj_Ent.Where(t => t.FAppId == p.FId && t.FEntType == 15501).Select(t => t.FName).FirstOrDefault(),
                     ToJZEntName = db.CF_Prj_Ent.Where(t => t.FAppId == p.FId && t.FEntType == 126).Select(t => t.FName).FirstOrDefault(),
                     //判断是不是最新的那条
                     isOld = db.CF_App_List.Where(t => t.FPrjId == a.FPrjId && t.FManageTypeId == fMType).Max(t => t.FCreateTime).GetValueOrDefault() > a.FCreateTime,
                 });
        if (!string.IsNullOrEmpty(ttFPrjName.Text.Trim()))
            v = v.Where(t => t.FPrjName.Contains(ttFPrjName.Text.Trim()));
        if (!string.IsNullOrEmpty(ddlFState.SelectedValue))
        {
            if (ddlFState.SelectedValue == "1")
            {
                v = v.Where(t => t.FState != 0 && t.FState != 6);
            }
            else
            {
                v = v.Where(t => t.FState.ToString() == ddlFState.SelectedValue);
            }
        }
        if (!string.IsNullOrEmpty(drop_FYear.SelectedValue))
            v = v.Where(t => t.FYear.ToString() == drop_FYear.SelectedValue);

        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            ProjectDB db = new ProjectDB();
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));//CF_Prj_Data.ID 
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));//勘察单位状态 
            string FAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            string FPrjName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjName"));
            string FCount = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FCount"));
            DateTime FAppDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FAppDate"));

            string ToKCEntName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ToKCEntName"));
            string ToJZEntName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ToJZEntName"));



            //操作按钮
            LinkButton btnOp = (LinkButton)e.Item.FindControl("btnOp");
            bool re = false;//是否需要重新办理

            #region 勘察单位办理结果
            //合同备案单位
            string s = "勘察单位：<font color='#378BB0'>" + ToKCEntName + "</font>";
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
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KC/applykcxmwt/Report.aspx?FAppId=" + FAppId + "',600,580);\">";
                    s += "<font color='red'>被退回</font> <font color='#666666'>[" + FAppDate.ToString("yyyy-MM-dd") + "]</font>";
                    s += "</a>";
                    break;
                case "6"://已办结
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KC/applykcxmwt/Report.aspx?FAppId=" + FAppId + "',600,580);\">";
                    s += "<font color='green'>已确认</font> <font color='#666666'>[" + FAppDate.ToString("yyyy-MM-dd") + "]</font>";
                    s += "</a>";
                    break;
                case "7"://不予接受
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KC/applykcxmwt/Report.aspx?FAppId=" + FAppId + "',600,580);\">";
                    s += "<font color='red'>不予接受</font> <font color='#666666'>[" + FAppDate.ToString("yyyy-MM-dd") + "]</font>";
                    s += "</a>";
                    break;
            }


            //合同备案情况
            string DataFID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "DataFID"));
            var HTBA = (from dd in db.CF_Prj_Data
                        join tt in db.CF_App_List on dd.FAppId equals tt.FId
                        where dd.FPriItemId == DataFID && tt.FManageTypeId == 411
                        orderby tt.FCreateTime descending
                        select new
                        {
                            tt.FState,
                            //查询备案通过没
                            FResultInt = db.CF_App_Idea.Where(i => i.FLinkId == tt.FId).OrderByDescending(i => i.FCreateTime).Select(i => i.FResultInt).FirstOrDefault(),
                        }).FirstOrDefault();
            //人员安排
            s += "</br>(2)合同备案：";
            if (HTBA != null)
            {
                if (HTBA.FState == 6)
                {
                    if (HTBA.FResultInt == 1)
                        s += "<font color='green'>已办结，已同意</font>";
                    else
                    {
                        s += "<font color='red'>已办结，未同意</font>";

                        re = true;//需要重新办理
                    }
                }
                else
                {
                    s += "<font color='blue'>正在办理</font>";
                }
            }
            else
                s += "<font color='#888888'>未开始</font>";


            IQueryable<CF_App_List> vKC = DataBinder.Eval(e.Item.DataItem, "vKC") as IQueryable<CF_App_List>;

            //勘察项目信息备案283
            s += "</br>(3)勘察项目信息备案：";
            var v283 = vKC.Where(t => t.FManageTypeId == 283).OrderByDescending(t => t.FCreateTime).FirstOrDefault();
            if (v283 != null)
            {
                if (v283.FState == 6)
                {
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:;\">";
                    s += "<font color='green'>已经备案</font>";
                    s += " <font color='#666666'>[" + EConvert.ToDateTime(v283.FAppDate).ToString("yyyy-MM-dd") + "]</font></a>";
                }
                else
                    s += "<font color='blue'>正在备案</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";
            //勘察项目人员意见28301
            s += "</br>(4)勘察人员意见：";
            var v28301 = vKC.Where(t => t.FManageTypeId == 28301).OrderByDescending(t => t.FCreateTime).FirstOrDefault();
            if (v28301 != null)
            {
                if (v28301.FState == 6)
                {
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KC/ApplyRYYJ/Report.aspx?FAppId=" + v28301.FId + "',680,500);\">";
                    s += "<font color='green'>已经完成勘察意见</font>";
                    s += " <font color='#666666'>[" + EConvert.ToDateTime(v28301.FAppDate).ToString("yyyy-MM-dd") + "]</font></a>";
                }
                else
                    s += "<font color='blue'>正在勘察</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";

            //勘察成果移交284
            s += "</br>(5)勘察成果移交：";
            var v284 = vKC.Where(t => t.FManageTypeId == 284).FirstOrDefault();
            if (v284 != null)
            {
                if (v284.FState == 6)
                {
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KC/ApplyKCCGYJ/ApplyBaseInfo.aspx?FAppId=" + FAppId + "',700,680);\">";
                    s += "<font color='green'>已经移交</font>";
                    s += " <font color='#666666'>[" + EConvert.ToDateTime(v284.FAppDate).ToString("yyyy-MM-dd") + "]</font></a>";
                }
                else
                    s += "<font color='blue'>正在移交</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";


            e.Item.Cells[4].Text = s;

            #endregion


            #region 见证单位办理结果

            IQueryable<CF_App_List> vJZ = DataBinder.Eval(e.Item.DataItem, "vJZ") as IQueryable<CF_App_List>;
            string FStateJZ = "0";
            var v28001 = vJZ.Where(t => t.FManageTypeId == 28001).FirstOrDefault();


            //合同备案见证单位
            string r = "";
            if (v28001 != null)
            {
                r = "见证单位：<font color='#378BB0'>" + ToJZEntName + "</font>";
                r += "</br>(1)合同确认：";
                FStateJZ = v28001.FState.GetValueOrDefault().ToString();
                switch (FStateJZ)
                {
                    case "0"://未上报
                        r += "<font color='#888888'>未提交</font>";
                        break;
                    case "1"://已上报
                        r += "<font color='#888888'>还未办理</font>";
                        break;
                    case "2"://被退回 
                        r += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../JZDW/applykcxmwt/Report.aspx?FAppId=" + v28001.FId + "',600,500);\">";
                        r += "<font color='red'>被退回</font> <font color='#666666'>[" + EConvert.ToDateTime(v28001.FAppDate).ToString("yyyy-MM-dd") + "]</font>";
                        r += "</a>";
                        break;
                    case "6"://已办结
                        r += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../JZDW/applykcxmwt/Report.aspx?FAppId=" + v28001.FId + "',600,500);\">";
                        r += "<font color='green'>已确认</font> <font color='#666666'>[" + EConvert.ToDateTime(v28001.FAppDate).ToString("yyyy-MM-dd") + "]</font>";
                        r += "</a>";
                        break;
                    case "7"://不予受理
                        r += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../JZDW/applykcxmwt/Report.aspx?FAppId=" + v28001.FId + "',600,500);\">";
                        s += "<font color='red'>不予接受</font> <font color='#666666'>[" + EConvert.ToDateTime(v28001.FAppDate).ToString("yyyy-MM-dd") + "]</font>";
                        r += "</a>";
                        break;
                }

                //合同备案情况 
                var JZHTBA = (from dd in db.CF_Prj_Data
                              join tt in db.CF_App_List on dd.FAppId equals tt.FId
                              where dd.FPriItemId == v28001.FLinkId && tt.FManageTypeId == 412
                              orderby tt.FCreateTime descending
                              select new
                              {
                                  tt.FState,
                                  //查询备案通过没
                                  FResultInt = db.CF_App_Idea.Where(i => i.FLinkId == tt.FId).OrderByDescending(i => i.FCreateTime).Select(i => i.FResultInt).FirstOrDefault(),
                              }).FirstOrDefault();
                //合同备案
                r += "</br>(2)合同备案：";
                if (JZHTBA != null)
                {
                    if (JZHTBA.FState == 6)
                    {
                        if (JZHTBA.FResultInt == 1)
                            r += "<font color='green'>已办结，已同意</font>";
                        else
                        {
                            r += "<font color='red'>已办结，未同意</font>";

                            re = true;//需要重新办理
                        }
                    }
                    else
                    {
                        r += "<font color='blue'>正在办理</font>";
                    }
                }
                else
                    r += "<font color='#888888'>未开始</font>";

                //见证人员安排28002
                r += "</br>(3)见证人员安排：";
                var v28002 = vJZ.Where(t => t.FManageTypeId == 28002).OrderByDescending(t => t.FCreateTime).FirstOrDefault();
                if (v28002 != null)
                {
                    if (v28002.FState == 6)
                    {
                        r += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../JZDW/applykcxmwt/Staffing.aspx?FAppId=" + v28002.FId + "',680,600);\">";
                        r += "<font color='green'>已经安排</font>";
                        r += " <font color='#666666'>[" + EConvert.ToDateTime(v28002.FAppDate).ToString("yyyy-MM-dd") + "]</font></a>";
                    }
                    else
                        r += "<font color='blue'>正在安排</font>";
                }
                else
                    r += "<font color='#888888'>未开始</font>";

                //勘察项目见证28004
                r += "</br>(4)勘察项目见证：";
                var v28004 = vJZ.Where(t => t.FManageTypeId == 28004).OrderByDescending(t => t.FCreateTime).FirstOrDefault();
                if (v28004 != null)
                {
                    if (v28004.FState == 6)
                    {
                        r += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../JZDW/ApplyXMJZ/Report.aspx?FAppId=" + v28004.FId + "',680,500);\">";
                        r += "<font color='green'>已经完成见证意见</font>";
                        r += " <font color='#666666'>[" + EConvert.ToDateTime(v28004.FAppDate).ToString("yyyy-MM-dd") + "]</font></a>";
                    }
                    else
                        r += "<font color='blue'>正在见证</font>";
                }
                else
                    r += "<font color='#888888'>未开始</font>";

                //见证报告备案28003
                r += "</br>(5)见证报告备案：";
                var v28003 = vJZ.Where(t => t.FManageTypeId == 28003).FirstOrDefault();
                if (v28003 != null)
                {
                    if (v28003.FState == 6)
                    {
                        r += "<a style=\"text-decoration:underline;\" href=\"javascript:;\">";
                        r += "<font color='green'>已经备案</font>";
                        r += " <font color='#666666'>[" + EConvert.ToDateTime(v28003.FAppDate).ToString("yyyy-MM-dd") + "]</font></a>";
                    }
                    else
                        r += "<font color='blue'>正在备案</font>";
                }
                else
                    r += "<font color='#888888'>未开始</font>";

                if (v28001.FState > 0 && string.IsNullOrEmpty(v28001.FToBaseinfoId))
                    r = "<font color='#aaaaaa'>没有合同备案见证单位</font>";
            }
            else
                r = "<font color='#aaaaaa'>没有合同备案见证单位</font>";


            e.Item.Cells[5].Text = r;

            #endregion

            #region 操作

            //操作
            string o = "";
            if (FState == "0" && (FStateJZ == "0" || v28001 == null))
            {//没上报可删除
                btnOp.Text = "删除";
                btnOp.CommandName = "Del";
                btnOp.ToolTip = "删除业务";
                btnOp.Attributes["onclick"] = "return confirm('确定要删除该业务吗？')";

                o = "<font color='#444444'>未提交</font>";
            }
            else if (FState == "1" && (FStateJZ == "1" || v28001 == null))
            {//刚上报还没有人批时可以撤消
                btnOp.Text = "撤消合同";
                btnOp.CommandName = "Back";
                btnOp.ToolTip = "撤消合同";
                btnOp.Attributes["onclick"] = "return confirm('确定要撤消吗？')";

                o = "<font color='blue'>已提交</font>";
            }
            else if (FState == "2" || FStateJZ == "2" || FState == "7" || FStateJZ == "7")
            {//有人退回了

                re = true;//需要重新办理
                o = "<font color='blue'>已提交</font>";
            }
            else
            {
                //其它情况都不允许操作
                btnOp.Text = "--";
                btnOp.CommandName = "";
                btnOp.Attributes["onclick"] = "return false;";

                o = "<font color='blue'>已提交</font>";
                if (FState == "6")
                    o = "<font color='green'>已确认</font>";
            }

            e.Item.Cells[2].Text = o;

            #endregion


            string FPriItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPriItemId"));
            bool isOld = EConvert.ToBool(DataBinder.Eval(e.Item.DataItem, "isOld"));
            if (re)//需要重新办理
                if (!isOld)
                {
                    btnOp.Text = "重新办理";
                    btnOp.CommandName = "ReApp";
                    btnOp.CommandArgument = FPrjId + "," + FPrjName + "," + FCount + "," + FID + "," + FPriItemId;
                    btnOp.Attributes["onclick"] = "";
                }
                else
                {//其它情况都不允许操作
                    btnOp.Text = "--";
                    btnOp.CommandName = "";
                    btnOp.Attributes["onclick"] = "return false;";
                }



            //是否二次。
            int n = EConvert.ToInt(FCount);
            if (n > 1)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = ("(" + n + "次)");

            }
            //查询项目的变更时间
            var prjBG = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId)
                .Select(t => new { t.FIsBG, t.FBGTime, t.FCount }).FirstOrDefault();
            if (prjBG.FCount > 0)
            {
                ((Literal)e.Item.FindControl("prj_Count")).Text = ("<br/>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")");
            }
            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Count(t => t.FPrjId == FPrjId) > 0;
            if (stop)
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Item.ToolTip = "该项目已被中止，所有业务停止进行。";
                btnOp.Enabled = false;
                btnOp.Attributes["onclick"] = "return false;";
            }
            else if (prjBG.FIsBG == 1)//判断该项目是否变更
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Item.ToolTip = "该项目已经做了变更，变更前的业务停止进行。";
                btnOp.Enabled = false;
                btnOp.Attributes["onclick"] = "return false;";
            }
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "See") //打开业务
        {
            string[] s = e.CommandArgument.ToString().Split(',');
            if (s.Length == 2)
            {
                pageTool tool = new pageTool(this);
                string FAppId = s[0];
                string FIsApprove = EConvert.ToInt(s[1]) > 0 ? "1" : "0";

                Session["FAppId"] = FAppId;
                Session["FManageTypeId"] = fMType;
                Session["FIsApprove"] = FIsApprove;

                tool.ExecuteScript("top.document.location='../Appmain/aindex.aspx';");
            }
        }
        else if (e.CommandName == "Del") //删除业务
        {
            string FLinkId = e.CommandArgument.ToString();
            DelApp280(FLinkId);
        }
        else if (e.CommandName == "Back") //撤消提交到其他企业的业务
        {
            string FLinkId = e.CommandArgument.ToString();
            GetBack280(FLinkId);
        }
        else if (e.CommandName == "ReApp") //重新办理
        {
            string[] s = e.CommandArgument.ToString().Split(',');
            if (s.Length == 5)
            {
                string FPrjId = s[0];
                string FPrjName = s[1];
                string FCount = s[2];
                string FOldAppId = s[3];
                string FPriItemId = s[4];//

                HPid.Value = FPrjId;
                txtFPrjName.Text = FPrjName;
                appTab.Visible = false;
                applyInfo.Visible = true;
                ViewState["FMNUMBER"] = fMType;
                t_FYear.Text = DateTime.Now.Year.ToString();
                t_FName.Text = t_FYear.Text + "年 " + db.getManageTypeName(fMType);
                t_OldFAppId.Value = FOldAppId;
                t_FCount.Value = FCount;//是否已是2次
                t_FPriItemId.Value = FPriItemId;//记录之前的2次关联
            }
        }
    }

    /// <summary>
    /// 删除“勘察合同备案业务”专用
    /// </summary>
    /// <param name="FLinkId">CF_Prj_Data.FID</param>
    private void DelApp280(string FLinkId)
    {
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(FLinkId))
        {
            ProjectDB db = new ProjectDB();
            //业务
            IQueryable<CF_App_List> App = db.CF_App_List.Where(t => t.FLinkId == FLinkId);
            if (App != null)
                db.CF_App_List.DeleteAllOnSubmit(App);

            //基本信息
            CF_Prj_Data Data = db.CF_Prj_Data.Where(t => t.FId == FLinkId).FirstOrDefault();
            if (Data != null)
                db.CF_Prj_Data.DeleteOnSubmit(Data);

            //相关企业
            IQueryable<CF_Prj_Ent> Ent = db.CF_Prj_Ent.Where(t => t.FPrjItemId == FLinkId);
            if (Ent != null)
                db.CF_Prj_Ent.DeleteAllOnSubmit(Ent);

            //附件
            IQueryable<CF_AppPrj_FileOther> oFile = db.CF_AppPrj_FileOther.Where(t => t.FAppId == FLinkId);
            if (oFile != null)
                db.CF_AppPrj_FileOther.DeleteAllOnSubmit(oFile);


            //提交删除
            db.SubmitChanges();
            tool.showMessage("删除成功");
            ShowInfo();
        }
        else
        {
            tool.showMessage("删除失败");
        }
    }

    /// <summary>
    ///  “勘察合同备案业务”专用撤消
    /// </summary>
    /// <param name="FLinkId">CF_Prj_Data.FID</param>
    private void GetBack280(string FLinkId)
    {
        pageTool tool = new pageTool(this.Page);
        ProjectDB db = new ProjectDB();
        //业务
        IQueryable<CF_App_List> App = db.CF_App_List.Where(t => t.FLinkId == FLinkId);
        if (App.Count() > 0)
        {
            foreach (CF_App_List t in App)
            {
                t.FState = 0;
            }

            db.SubmitChanges();
            tool.showMessage("撤消成功");
            ShowInfo();
        }
        else
        {
            tool.showMessage("撤消失败");
        }

    }


    private void SaveInfo(string fmtnumber)
    {
        pageTool tool = new pageTool(this.Page);
        if (this.Session["FBaseId"] == null)
            return;
        //勘察项目合同备案--创建两个cf_App_list和一个cf_Prj_Data
        //查询如果已经有了，则不再创建
        if (string.IsNullOrEmpty(HPid.Value))
        {
            tool.showMessage("请先选择项目！");
            return;
        }

        string fPrjDataId = Guid.NewGuid().ToString();

        CF_App_List lKC = new CF_App_List();//勘察企业业务
        lKC.FId = Guid.NewGuid().ToString();
        lKC.FLinkId = fPrjDataId;
        lKC.FBaseinfoId = CurrentEntUser.EntId;
        lKC.FPrjId = HPid.Value;
        lKC.FName = t_FName.Text.Trim();
        lKC.FManageTypeId = fMType;
        lKC.FwriteDate = DateTime.Now;
        lKC.FState = 0;
        lKC.FIsDeleted = false;
        lKC.FYear = EConvert.ToInt(t_FYear.Text.Trim());
        lKC.FMonth = DateTime.Now.Month;
        lKC.FBaseName = CurrentEntUser.EntName;
        lKC.FTime = DateTime.Now;
        lKC.FCreateTime = DateTime.Now;
        lKC.FReportCount = 1;//FReportCount业务上报第几次
        //看是不是二次勘察
        if (!string.IsNullOrEmpty(t_OldFAppId.Value))
            lKC.FCount = EConvert.ToInt(t_FCount.Value);//FCount是几次勘察        
        else
            lKC.FCount = 1;

        db.CF_App_List.InsertOnSubmit(lKC);

        CF_App_List lJZ = new CF_App_List();//见证企业业务
        lJZ.FId = Guid.NewGuid().ToString();
        lJZ.FLinkId = fPrjDataId;
        lJZ.FBaseinfoId = CurrentEntUser.EntId;
        lJZ.FPrjId = HPid.Value;
        lJZ.FName = t_FYear.Text + "年 " + rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='28001'");
        lJZ.FManageTypeId = 28001;
        lJZ.FwriteDate = DateTime.Now;
        lJZ.FState = 0;
        lJZ.FIsDeleted = false;
        lJZ.FYear = EConvert.ToInt(t_FYear.Text.Trim());
        lJZ.FMonth = DateTime.Now.Month;
        lJZ.FBaseName = CurrentEntUser.EntName;
        lJZ.FTime = DateTime.Now;
        lJZ.FCreateTime = DateTime.Now;
        lJZ.FReportCount = 1;//业务上报第几次
        //看是不是二次勘察
        if (!string.IsNullOrEmpty(t_OldFAppId.Value))
            lJZ.FCount = EConvert.ToInt(t_FCount.Value); //是几次勘察
        else
            lJZ.FCount = 1;
        db.CF_App_List.InsertOnSubmit(lJZ);

        CF_Prj_Data prjData = new CF_Prj_Data();
        prjData.FId = fPrjDataId;
        prjData.FPrjId = HPid.Value;
        prjData.FPrjName = txtFPrjName.Text.Trim();
        prjData.FBaseInfoId = CurrentEntUser.EntId;
        prjData.FPriItemId = t_FPriItemId.Value;//保存审查不合格那条审查业务的CF_Prj_Data.FID
        prjData.FType = fMType;
        prjData.FTime = DateTime.Now;
        prjData.FCreateTime = DateTime.Now;
        prjData.FIsDeleted = false;
        db.CF_Prj_Data.InsertOnSubmit(prjData);
        db.SubmitChanges();

        this.Session["FAppId"] = fPrjDataId;
        this.Session["FManageTypeId"] = fMType;
        this.Session["FIsApprove"] = 0;
        this.RegisterStartupScript(new Guid().ToString(), "<script>parent.parent.document.location='../Appmain/aindex.aspx';</script>");
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        this.appTab.Visible = false;
        this.applyInfo.Visible = true;
        this.ViewState["FMNUMBER"] = fMType;
        t_FYear.Text = DateTime.Now.Year.ToString();
        this.t_FName.Text = t_FYear.Text + "年 " + rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + fMType + "'");
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (this.ViewState["FMNUMBER"] == null)
        {
            return;
        }
        pageTool tool = new pageTool(this.Page);
        SaveInfo(this.ViewState["FMNUMBER"].ToString());
    }

    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        //选择时，要还原。以免冲突错误 
        t_OldFAppId.Value = "";
        t_FCount.Value = "";

        this.appTab.Visible = true;
        this.applyInfo.Visible = false;
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(HPid.Value))
            txtFPrjName.Text = rc.GetSignValue("select FPrjName from cf_Prj_Baseinfo where fid='" + HPid.Value + "'");
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
