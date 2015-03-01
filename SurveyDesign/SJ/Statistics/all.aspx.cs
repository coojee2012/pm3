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


            //初步设计合同备案
            showCBSJWT(FPrjId);

            //施工图设计文件编制合同备案
            SGTWJBZWT(FPrjId);
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
            s += "业务：<a title=\"查看合同备案详情\" href=\"javascript:showAddWindow('../../SJ/applycbsjwt/ApplyBaseInfo.aspx?FDataID=" + FLinkId + "', 900,700);\">" + FName + "</a>";
            s += "<br/>合同备案提交时间：<span>[" + FReportDate.ToString("yyyy-MM-dd") + "]</span>";
            s += "<br/>合同备案设计单位：<span>" + ToEntName + "</span>";

            //确认状态
            s += "<br/>(1)确认：";
            switch (FState)
            {
                case "0"://未上报
                    s += "<font color='#888888'>未提交</font>";
                    break;
                case "1"://已上报
                    s += "<font color='#888888'>还未确认</font>";
                    break;
                case "2"://被退回
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applycbsjwt/Report.aspx?FAppId=" + FID + "',600,400);\"><font color='red'>被退回</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "6"://已办结
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applycbsjwt/Report.aspx?FAppId=" + FID + "',600,500);\"><font color='green'>已确认</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "7"://不予接受
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applycbsjwt/Report.aspx?FAppId=" + FID + "',600,500);\"><font color='red'>不予接受</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
            }

            //人员安排29201、成果移交293 
            var v = (from a in db.CF_App_List
                     where a.FLinkId == FLinkId && (a.FManageTypeId == 29201 || a.FManageTypeId == 293)
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

            //成果移交
            s += "<br/>(3)成果移交：";
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
                     //受合同备案企业名称
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
            s += "业务：<a title=\"查看合同备案详情\" href=\"javascript:showAddWindow('../../SJ/applycbsjwt/ApplyBaseInfo.aspx?FDataID=" + FLinkId + "', 900,700);\">" + FName + "</a>";
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
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applysjwjbzwt/Report.aspx?FAppId=" + FAppId + "',600,500);\"><font color='red'>被退回</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "6"://已办结
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applysjwjbzwt/Report.aspx?FAppId=" + FAppId + "',600,500);\"><font color='green'>已确认</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
                case "7"://不予接受
                    s += "<a href=\"javascript:showAddWindow('../../SJ/applysjwjbzwt/Report.aspx?FAppId=" + FAppId + "',600,500);\"><font color='red'>不予接受</font> [" + FAppDate.ToString("yyyy-MM-dd") + "]</a>";
                    break;
            }



            //人员安排29701、成果移交298
            var v = (from a in db.CF_App_List
                     where a.FLinkId == FLinkId && (a.FManageTypeId == 29701 || a.FManageTypeId == 298)
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

            //成果移交
            s += "</br>(3)成果移交：";
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

 

}
