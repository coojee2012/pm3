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
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            showInfo();
        }
    }

    //绑定
    private void BindControl()
    {
        string FAppId = Request.QueryString["FAppId"];
        d_FDate3.Text = DateTime.Now.ToShortDateString();
    }

    //显示
    private void showInfo()
    {
        string FAppId = Request.QueryString["FAppId"];
        var v = (from a in db.CF_App_List
                 join d in db.CF_Prj_Data on a.FLinkId equals d.FId
                 where a.FId == FAppId
                 select new
                 {
                     a.FId,
                     a.FPrjId,
                     a.FLinkId,
                     a.FState,
                     FAppDate = db.CF_App_List.Where(t => a.FLinkId == t.FLinkId && t.FState == 6 && t.FManageTypeId == 291).Select(t => t.FAppDate).FirstOrDefault(),
                     d.FDate3,
                     d.FDate4,
                     d.FDate5,
                     d.FTxt1,
                     d.FTxt15,
                     d.FTxt21

                 }).FirstOrDefault();
        if (v != null)
        {
            string FLinkId = v.FLinkId;
            string FPrjId = v.FPrjId;
            t_FAppDate.Text = string.Format("{0:d}", v.FAppDate);

            pageTool tool = new pageTool(this.Page, "d_");
            tool.fillPageControl(v);

            hidd_FDataID.Value = v.FLinkId;

            //计划开始时间和计划结束时间
            var date = (from a in db.CF_App_List
                        join e in db.CF_Prj_Emp on a.FId equals e.FAppId
                        where e.FType == 1 && a.FManageTypeId == 29201 && a.FState == 6 && a.FLinkId == v.FLinkId
                        select new { e.FBeginTime, e.FEndTime }).FirstOrDefault();
            if (date != null)
            {
                d1_FDate1.Text = string.Format("{0:d}", date.FBeginTime);
                d1_FDate2.Text = string.Format("{0:d}", date.FEndTime);
            }


            //显示工程信息
            ShowPrjInfo(FPrjId);

            //显示附件
            showFileList(FAppId);

            //显示人员信息
            ShowEmpInfo();

            //提交后不能修改。
            if (v.FState > 0)
            {
                tool.ExecuteScript("btnEnable();");
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
        int FManageTypeId = 293;
        ProjectDB db = new ProjectDB();
        var v = from t in db.CF_Sys_PrjList
                where t.FManageType == FManageTypeId
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

            //保存负责人
            int iCount = dg_List.Items.Count;
            for (int i = 0; i < iCount; i++)
            {
                string fid = dg_List.Items[i].Cells[dg_List.Columns.Count - 1].Text;
                TextBox box = dg_List.Items[i].Cells[dg_List.Columns.Count - 4].Controls[1] as TextBox;
                if (box != null)
                {
                    string sql = "update cf_Prj_SJEmp set FSpecialName='" + box.Text.Trim() + "' where fid='" + fid + "'";
                    rc.PExcute(sql);
                }
            }
        }

        //提交保存
        db.SubmitChanges();
        if (IsSubmit)
        {
            tool.showMessageAndRunFunction("初步设计成果移交成功。", "window.returnValue=1;window.close();");
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
    //显示人员列表
    private void ShowEmpInfo()
    {
        string fAppId = Request.QueryString["FAppId"];
        var v = (from t in db.CF_Prj_SJEmp
                 where t.FAppId == fAppId
                 orderby t.FCreateTime descending
                 select new
                 {
                     t.FId,
                     t.FIsDeleted,
                     t.FSpecialName,
                     t.FEmpName,
                     t.FBaseinfoId,
                     t.FCertiNo
                 }).ToList();

        Pager1.RecordCount = v.Count();
        dg_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataKeyField = "FBaseInfoId";
        dg_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }
    //人员列表
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 2].Controls[0] as LinkButton;
            if (lb != null)
            {
                lb.Text = "保存";
                lb.Attributes.Add("onclick", "return doSave(this);");
            }
        }
    }
    //保存
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            if (e.CommandName == "Save")
            {
                string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
                TextBox box = e.Item.Cells[e.Item.Cells.Count - 4].Controls[1] as TextBox;
                if (box != null)
                {
                    string sql = "update cf_Prj_SJEmp set FSpecialName='" + box.Text.Trim() + "' where fid='" + fid + "'";
                    rc.PExcute(sql);
                    pageTool tool = new pageTool(this.Page);
                    tool.showMessage("保存成功！");
                    ShowEmpInfo();
                }
            }
        }
    }
    //删除人员
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        //保存选择的人员列表
        for (int i = 0; i < dg_List.Items.Count; i++)
        {
            string FID = dg_List.Items[i].Cells[dg_List.Columns.Count - 1].Text;
            //已选中的
            if (((CheckBox)dg_List.Items[i].FindControl("CheckItem")).Checked)
            {
                CF_Prj_SJEmp v = db.CF_Prj_SJEmp.Where(t => t.FId == FID).FirstOrDefault();
                if (v != null)
                {
                    db.CF_Prj_SJEmp.DeleteOnSubmit(v);
                }
            }
        }
        db.SubmitChanges();
        tool.showMessageAndRunFunction("删除成功！", "window.returnValue=1;");
        ShowEmpInfo();
    }
    //刷新人员列表
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowEmpInfo();
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowEmpInfo();
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        if (btn.CommandName == "ZC")//注册人员
        {
            ShowEmpInfo();
        }
    }
}
