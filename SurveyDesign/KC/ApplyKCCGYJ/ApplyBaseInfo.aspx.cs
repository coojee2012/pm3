using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
public partial class KC_ApplyKCCGYJ_ApplyBaseInfo : Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }


    //显示
    private void showInfo()
    {
        string FAppId = Request.QueryString["FAppId"];
        string FBaseinfoId = CurrentEntUser.EntId;
        var v = (from t in db.CF_App_List
                 join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                 where t.FId == FAppId
                 select new
                 {
                     t.FId,
                     t.FPrjId,
                     t.FLinkId,
                     t.FBaseinfoId,
                     t.FState,
                     dd = (from dd1 in db.CF_Prj_Data
                           join dd2 in db.CF_App_List
                           on dd1.FAppId equals dd2.FId
                           where dd2.FLinkId == t.FLinkId
                           && dd2.FManageTypeId == 283
                           && dd2.FState == 6
                           && dd2.FPrjId == t.FPrjId
                           select new { dd1.FDate1, dd1.FDate2 })
                          .FirstOrDefault(),//提取勘察信息备案的数据
                     d.FDate3,
                     d.FDate4,
                     d.FDate5,
                     d.FTxt7,
                     d.FTxt9,
                     d.FTxt21,
                     FAppDate = db.CF_App_List.Where(a => t.FPrjId == t.FPrjId && a.FToBaseinfoId == FBaseinfoId && a.FState == 6 && a.FManageTypeId == 280).Select(a => a.FAppDate).FirstOrDefault(),

                     app28001 = db.CF_App_List.Where(a => a.FLinkId == t.FLinkId && a.FManageTypeId == 28001).FirstOrDefault()

                 }).FirstOrDefault();
        if (v != null)
        {

            string FLinkId = v.FLinkId;
            string FPrjId = v.FPrjId;
            t_FAppDate.Text = string.Format("{0:d}", v.FAppDate);


            pageTool tool = new pageTool(this.Page, "d_");
            tool.fillPageControl(v);

            FDataID.Value = v.FLinkId;

            if (v.dd != null)
            {
                d1_FDate1.Text = string.Format("{0:d}", v.dd.FDate1);
                d1_FDate2.Text = string.Format("{0:d}", v.dd.FDate2);
            }

            //显示当前勘察单位信息
            var ent = (from e in db.CF_Prj_Ent
                       join a in db.CF_App_List on e.FAppId equals a.FLinkId
                       where a.FManageTypeId == 280//项目勘察合同备案
                             && a.FState == 6 && e.FEntType == 15501 && e.FBaseInfoId == v.FBaseinfoId
                       select new
                       {
                           e.FName,
                           e.FLevelName,
                           e.FBaseInfoId
                       }).FirstOrDefault();
            pageTool toolent = new pageTool(this.Page, "k_");
            toolent.fillPageControl(ent);

            //显示工程信息
            ShowPrjInfo(FPrjId);

            //显示附件
            showFileList(FAppId);

            //已提交不能修改
            if (v.FState > 1)
            {
                tool.ExecuteScript("btnEnable();");
            }

            //看勘察单位那边有没有退回、不予受理什么的。
            string s = "";
            if (v.app28001 != null)
            {
                if (v.app28001.FState == 2)
                {
                    s += "注意：已被见证单位退回，业务终止！";
                }
                else if (v.app28001.FState == 7)
                {
                    s += "注意：见证单位不予受理，业务终止！";
                }

                if (!string.IsNullOrEmpty(s))
                {
                    tool.ExecuteScript("btnEnable();");
                    lit_TS.Text = "<div class='ts'>" + s + "</div>";
                }
            }


            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Where(st => st.FPrjId == v.FPrjId).FirstOrDefault();
            if (stop != null)
            {
                btnSave.Enabled = false;
                btnSave.ToolTip = "该项目已被中止，所有业务停止进行。";
                btnReport.Enabled = false;
                btnReport.ToolTip = "该项目已被中止，所有业务停止进行。";
            }
        }
    }

    /// <summary>
    /// 显示工程信息
    /// </summary>
    private void ShowPrjInfo(string FPrjId)
    {
        CF_Prj_BaseInfo prj = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId).FirstOrDefault();
        if (prj != null)
        {
            pageTool tool = new pageTool(this.Page, "p_");
            tool.fillPageControl(prj);


            //显示建设单位信息
            ShowEntInfo(prj.FBaseinfoId);
        }
    }
    /// <summary>
    /// 显示建设单位信息
    /// </summary>
    private void ShowEntInfo(string FBaseinfoId)
    {
        CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == FBaseinfoId).FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, "e_");
            tool.fillPageControl(ent);
        }
    }


    #region 附件

    //显示附件
    private void showFileList(string FAppId)
    {
        //当前业务类型
        int FManageTypeId = 284;//勘察成果移交
        ProjectDB db = new ProjectDB();
        var v = from t in db.CF_Sys_PrjList
                join m in db.CF_Sys_ManageType on t.FManageId equals m.FID
                where m.FNumber == FManageTypeId
                orderby t.FOrder
                select new
                {
                    t.FId,
                    t.FFileName,
                    t.FFileAmount,
                    t.FRemark,
                    t.FOrder,
                    FIsMust = t.FIsMust == 1 ? "<font color='red'>是</font>" : "否",
                    AppFile = db.CF_AppPrj_FileOther.Where(f => t.FId == f.FPrjFileId && f.FAppId == FAppId)
                };


        rep_List.DataSource = v;
        rep_List.DataBind();
    }
    //一层列表
    protected void rep_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            IQueryable<CF_AppPrj_FileOther> AppFile = DataBinder.Eval(e.Item.DataItem, "AppFile") as IQueryable<CF_AppPrj_FileOther>;
            if (AppFile != null && AppFile.Count() > 0)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = AppFile.Count().ToString();
                ((Literal)e.Item.FindControl("lit_Has")).Text = "<font color='green'>是</font>";


                Repeater rep_File = (Repeater)e.Item.FindControl("rep_File");
                rep_File.DataSource = AppFile;
                rep_File.DataBind();
            }
            else
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = "0";
                ((Literal)e.Item.FindControl("lit_Has")).Text = "<font color='red'>否</font>";
            }


            ((Literal)e.Item.FindControl("lit_Has")).Text += "<input type=\"button\" class=\"m_btn_w4\" value=\"上传文件\" onclick='showAddWindow(\"../AppMain/FileUp.aspx?FAppId=" + Request.QueryString["FAppId"] + "&FPrjFileId=" + FID + "\",500,250);' />";

        }
    }

    //二层列表 事件
    protected void rep_File_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "cnDel")
        {
            string FID = e.CommandArgument.ToString();
            ProjectDB db = new ProjectDB();

            CF_AppPrj_FileOther v = db.CF_AppPrj_FileOther.Where(t => t.FId == FID).FirstOrDefault();
            if (v != null)
            {
                db.CF_AppPrj_FileOther.DeleteOnSubmit(v);
                db.SubmitChanges();

                pageTool tool = new pageTool(this.Page);
                tool.showMessage("删除成功");

                string FAppId = Request.QueryString["FAppId"];
                showFileList(FAppId);
            }
        }
    }

    //上传合同附件
    protected void btnShowFile_Click(object sender, EventArgs e)
    {
        string FAppId = Request.QueryString["FAppId"];
        showFileList(FAppId);
    }

    #endregion
    /// <summary>
    /// 验证附件是否上传
    /// </summary>
    /// <returns></returns>
    bool IsUploadFile(int? FMTypeId, string FAppId)
    {
        var v = db.CF_Sys_PrjList.Count(t => t.FIsMust == 1
            && t.FManageType == FMTypeId
            && db.CF_AppPrj_FileOther.Count(o => o.FPrjFileId == t.FId
                && o.FAppId == FAppId) < 1) > 0;
        return v;
    }

    //保存
    private void saveInfo(bool IsSubmit)
    {
        pageTool tool = new pageTool(this.Page, "d_");
        string FAppId = EConvert.ToString(Request.QueryString["FAppId"]);
        DateTime dTime = DateTime.Now;
        CF_App_List app = db.CF_App_List.Where(t => t.FId == FAppId).FirstOrDefault();
        if (app != null)
        {
            //验证必需的附件是否上传
            if (IsUploadFile(app.FManageTypeId, FAppId))
            {
                tool.showMessage("“附件上传”菜单中存在未上传的附件(必需上传的)，请先上传！");
                return;
            }
            //基本信息
            CF_Prj_Data data = db.CF_Prj_Data.Where(t => t.FId == app.FLinkId).FirstOrDefault();
            if (data != null)
            {
                data = tool.getPageValue(data);
            }

            if (IsSubmit)
            {
                app.FState = 6;
                app.FReportDate = dTime;
                app.FAppDate = dTime;
            }
        }

        //提交保存
        db.SubmitChanges();
        if (IsSubmit)
        {
            tool.showMessageAndRunFunction("勘察成果移交成功。", "window.returnValue=1;window.close();");
        }
        else
        {
            tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
        }
    }



    //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo(false);
    }

    //提交
    protected void btnReport_Click(object sender, EventArgs e)
    {
        saveInfo(true);
    }

}
