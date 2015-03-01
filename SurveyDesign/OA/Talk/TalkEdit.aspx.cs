using System;
using System.Data;
using System.Collections;
using System.Web;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Seaskyer.Strings;
using Seaskyer.WebApp.Utility;
using System.Linq;
using ProjectBLL;
using ProjectData;

public partial class OA_Talk_TalkEdit : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    OA oa = new OA();
    protected void Page_Load(object sender, EventArgs e)
    {

        pageTool tool = new pageTool(this.Page);
        if (!IsPostBack)
        {
            conBind();
            if (!string.IsNullOrEmpty(Request.QueryString["FID"]))
            {
                ViewState["FID"] = Request.QueryString["FID"];
                t_BB.Text = "修改讨论";
                showInfo();
            }
        }
    }
    //绑定默认项
    private void conBind()
    {
        t_Fproject.DataSource = db.getDicList(9);
        t_Fproject.DataTextField = "FName";
        t_Fproject.DataValueField = "FNumber";
        t_Fproject.DataBind();

        //当前用户
        if (!string.IsNullOrEmpty(CurrentEntUser.EntId))
            ViewState["FBaseinfoId"] = CurrentEntUser.EntId;
        else if (!string.IsNullOrEmpty(CurrentEmpUser.EmpId))
            ViewState["FBaseinfoId"] = CurrentEmpUser.EmpId;
    }

    //显示
    private void showInfo()
    {
        string FID = EConvert.ToString(ViewState["FID"]);
        var v = (from t in db.CF_Talk_TalkManage
                 where t.FID == FID
                 select new
                 {
                     t.FID,
                     t.FProjectID,
                     t.FSubmitPerson,
                     t.FSubmitTime,
                     t.FCreateTime,
                     t.FTalkName,
                     t.FTalkState,
                     t.FKey,
                     t.FLinkWay,
                     t.FTalkDescribe // 内容 
                 }).FirstOrDefault();
        if (v != null)
        {
            t_FTalkName.Text = v.FTalkName;
            t_FContent.Value = v.FTalkDescribe;
            t_FKey.Text = v.FKey;
            t_Fproject.SelectedValue = v.FProjectID;
            t_FLinkWay.Text = v.FLinkWay;

            //控制提交按钮
            showBtn(v.FTalkState.ToString());
        }
    }


    //控制提交按钮
    private void showBtn(string FState)
    {
        switch (FState)
        {
            case "1":
                btnOK.Visible = true;
                btnOK.Text = "提交";
                btnOK.CssClass = "m_btn_w2";
                btnOK.Attributes["onclick"] = "confirm('确定提交到讨论区吗？');";
                break;
            case "2":
                btnOK.Visible = false;
                break;
            case "3":
                btnOK.Visible = true;
                btnOK.Text = "继续该话题";
                btnOK.CssClass = "m_btn_w6";
                btnOK.Attributes["onclick"] = "confirm('确定继续讨论该话题吗？');";
                break;
            default:
                btnOK.Visible = false;
                break;
        }

    }



    #region 保存

    //保存
    private void saveinfo()
    {
        pageTool tool = new pageTool(this.Page);
        string FBaseinfoId = EConvert.ToString(ViewState["FBaseinfoId"]);

        DateTime dTime = DateTime.Now;
        string FID = EConvert.ToString(ViewState["FID"]);
        CF_Talk_TalkManage t = db.CF_Talk_TalkManage.Where(ta => ta.FID == FID).FirstOrDefault();
        if (t == null)
        {
            t = new CF_Talk_TalkManage();
            db.CF_Talk_TalkManage.InsertOnSubmit(t);
            t.FID = Guid.NewGuid().ToString();
            t.FTalkState = 1;//初始状态在“草稿箱”
            t.FSubmitPerson = FBaseinfoId;
            t.FCreateTime = dTime;
            t.FSubmitTime = dTime;
        }
        t.FTime = dTime;
        t.FTalkName = t_FTalkName.Text;//标题
        t.FProjectID = t_Fproject.SelectedValue;//项目
        t.FTalkDescribe = t_FContent.Value;//内容 
        t.FKey = t_FKey.Text;//口令 
        t.FLinkWay = t_FLinkWay.Text;//联系方式 

        db.SubmitChanges();
        ViewState["FID"] = t.FID;
        tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
        showBtn(t.FTalkState.ToString());

    }

    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveinfo();
    }

    //提交按钮 
    protected void btnOK_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        string FID = EConvert.ToString(ViewState["FID"]);
        CF_Talk_TalkManage t = db.CF_Talk_TalkManage.Where(ta => ta.FID == FID).FirstOrDefault();
        if (t != null)
        {
            DateTime dTime = DateTime.Now;
            t.FTalkState = 2;
            t.FSubmitTime = dTime;
            t.FTime = dTime;

            db.SubmitChanges();
            tool.showMessageAndRunFunction("操作成功", "window.returnValue=1;");
            showBtn(t.FTalkState.ToString());
        }

        
    }

    #endregion

}
