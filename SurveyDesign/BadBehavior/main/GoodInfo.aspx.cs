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

public partial class BadBehavior_main_GoodInfo : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showBound();
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                showInfo();

            }
            else
            {
                this.t_FProjectName.Text = CurrentEntUser.EntName;
            }
        }
    }


    void showBound()
    {
        var dic = db.Dic.Where(t => t.FParentId == 1121).OrderBy(t => t.FOrder);
        this.t_FTxt1.DataSource = dic;
        t_FTxt1.DataTextField = "FName";
        t_FTxt1.DataValueField = "FNumber";
        t_FTxt1.DataBind();
        dic = db.Dic.Where(t => t.FParentId == 112).OrderBy(t => t.FOrder);
        this.t_FTxt2.DataSource = dic;
        t_FTxt2.DataTextField = "FName";
        t_FTxt2.DataValueField = "FNumber";
        t_FTxt2.DataBind();
    }

    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
        CF_Ent_GoodAction report = db.CF_Ent_GoodAction.Where(t => t.FID == Convert.ToString(ViewState["FID"])).FirstOrDefault();
        if (report != null)
        {
            tool.fillPageControl(report);
            showFileList();
        }

    }
    //显示附件
    private void showFileList()
    {
        pageTool to = new pageTool(this.Page);
        string FAppId = EConvert.ToString(ViewState["FID"]);
        if (FAppId == null || FAppId == "")
        {
            to.showMessage("请先保存基本信息。");
            return;
        }
        //当前业务类型
        int FManageTypeId = 29901;
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


            ((Literal)e.Item.FindControl("lit_Has")).Text += "<input type=\"button\" class=\"m_btn_w4\" value=\"上传文件\" onclick='showAddWindow(\"FileUp.aspx?FAppId=" + EConvert.ToString(ViewState["FID"]) + "&FPrjFileId=" + FID + "\",500,250);' />";

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

                string FAppId = this.ViewState["FID"].ToString();
                showFileList();
            }
        }
    }
    //保存
    private void saveInfo(int state)
    {
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        string fId = EConvert.ToString(ViewState["FID"]);
        CF_Ent_GoodAction report = db.CF_Ent_GoodAction.Where(t => t.FID == fId).FirstOrDefault();
        CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == CurrentEntUser.EntId).FirstOrDefault();
        if (report == null)
        {
            report = new CF_Ent_GoodAction();
            db.CF_Ent_GoodAction.InsertOnSubmit(report);
            report.FID = Guid.NewGuid().ToString();
            report.FIsDeleted = 0;
            report.FCreateTime = dTime;
            report.FBaseInfoId = CurrentEntUser.EntId;
            report.FTxt12 = EConvert.ToString( ent.FSystemId);
        }
        report.FState = state;
        report = tool.getPageValue(report);
        report.FTime = dTime;


        db.SubmitChanges();
        tool.showMessageAndRunFunction((state == 1 ? "保存成功，请添加附件信息。" : "保存成功,请添加附件信息。"), "window.returnValue='1';");
        ViewState["FID"] = report.FID;
    }

    //保存按钮
    protected void btnSave_Click(object sender, CommandEventArgs e)
    {
        saveInfo(EConvert.ToInt(e.CommandArgument));
        showInfo();
    }
    protected void btnShowFile_Click(object sender, EventArgs e)
    {
        showFileList();
    }

}
