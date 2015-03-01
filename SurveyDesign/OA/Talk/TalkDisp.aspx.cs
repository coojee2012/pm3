using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Linq;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using ProjectBLL;
using ProjectData;

public partial class OA_Talk_TalkDisp : System.Web.UI.Page
{
    OA oa = new OA();
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAnswer.Attributes.Add("onclick", "return confirm('确认提交吗?');");
        btn_GetOn.Attributes.Add("onclick", "return confirm('确认要提交此问题吗?');");
        btn_IsOK.Attributes.Add("onclick", "return confirm('是否确认此问题已解决?');");
        if (!Page.IsPostBack)
        {

            if (!string.IsNullOrEmpty(CurrentEntUser.EntId))
                ViewState["FBaseinfoId"] = CurrentEntUser.EntId;
            else if (!string.IsNullOrEmpty(CurrentEmpUser.EmpId))
                ViewState["FBaseinfoId"] = CurrentEmpUser.EmpId;

            if (!string.IsNullOrEmpty(Request.QueryString["FID"]))
            {
                showinfo();
                showContent();
            }
        }

    }
    private void showinfo()
    {
        pageTool tool = new pageTool(this.Page, "la_");
        ProjectDB db = new ProjectDB();
        string FID = Request.QueryString["FID"];

        DataTable dt = oa.GetTable(EntityTypeEnum.ETalkManage, "", "fid='" + Request["FID"] + "'");
        var v = (from t in db.CF_Talk_TalkManage
                 where t.FID == FID
                 select new
                 {
                     t.FID,
                     t.FTalkName,
                     t.FSubmitPerson,
                     t.FProjectID,
                     t.FSubmitTime,
                     t.FTalkState,
                     t.FTalkDescribe,
                     t.FLinkWay,
                     ent = db.CF_Ent_BaseInfo.Where(e => e.FId == t.FSubmitPerson).Select(e => new
                     {
                         e.FName,
                         e.FEmail,
                         e.FLinkMan,
                         e.FTel
                     }).FirstOrDefault(),
                     emp = db.CF_Emp_BaseInfo.Where(e => e.FId == t.FSubmitPerson).Select(e => new
                     {
                         e.FBaseInfoID,
                         e.FName,
                         e.FRegistSpecialId,//从事专业
                         e.FTechId,//职称
                         FEntName = db.CF_Ent_BaseInfo.Where(ent => ent.FId == e.FBaseInfoID).Select(ent => ent.FName).FirstOrDefault(),//所属企业
                     }).FirstOrDefault(),

                 }).FirstOrDefault();
        if (v != null)
        {

            //标题 
            Label_Name.Text = v.FTalkName;
            //提交时间
            Label_AppTime.Text = v.FSubmitTime.GetValueOrDefault().ToString("yyyy-MM-dd");
            // 所属板块
            Link_projectName.Text = db.getDicName(v.FProjectID);
            //联系方式 
            Label_FLinkWay.Text = v.FLinkWay;
            //内容
            Literal_Content.Text = v.FTalkDescribe;

            //创建者信息
            ViewState["FSubmitPerson"] = v.FSubmitPerson;//传说中的楼主
            string s = "";
            if (v.ent != null)
            {//企业
                la_FName.Text = v.ent.FName;
                s += "<tr><td>联系人：</td><td>" + v.ent.FLinkMan + "</td></tr>";
                s += "<tr><td>手机：</td><td>" + v.ent.FTel + "</td></tr>";
                s += "<tr><td>EMail：</td><td>" + v.ent.FEmail + "</td></tr>";
            }
            else if (v.emp != null)
            {//个人  
                la_FName.Text = v.emp.FName;
                s += "<tr><td>所属企业：</td><td>" + v.emp.FEntName + "</td></tr>";
                s += "<tr><td>从事专业：</td><td>" + v.emp.FRegistSpecialId + "</td></tr>";
                s += "<tr><td>职称：</td><td>" + db.getDicName(v.emp.FTechId) + "</td></tr>";
            }
            lit_CreateUserInfo.Text = s;


            if (v.FTalkState == 1)
            {
                Aimg.Src = "../../image/question7.gif";
                if (v.FSubmitPerson == EConvert.ToString(ViewState["FBaseinfoId"]))
                {
                    btn_GetOn.Text = "提交";
                    btn_GetOn.CssClass = "m_btn_w2";
                    btn_GetOn.Visible = true;
                }
            }
            else if (v.FTalkState == 2)
            {
                Aimg.Src = "../../image/question1.gif";
                if (v.FSubmitPerson == EConvert.ToString(ViewState["FBaseinfoId"])) btn_IsOK.Visible = true;
            }
            else if (v.FTalkState == 3)
            {
                Aimg.Src = "../../image/question2.gif";
                if (v.FSubmitPerson == EConvert.ToString(ViewState["FBaseinfoId"]))
                {
                    btn_GetOn.Text = "重新开启讨论";
                    btn_GetOn.CssClass = "m_btn_w6";
                    btn_GetOn.Visible = true;
                }
            }

            ViewState["FProjectID"] = v.FProjectID;
            ViewState["FSubmitPerson"] = v.FSubmitPerson;
        }
    }

    #region 回复

    //回复内容
    private void showContent()
    {
        string FTalkId = Request.QueryString["FID"];
        ProjectDB db = new ProjectDB();
        var v = (from t in db.CF_Talk_Process
                 where t.FTalkID == FTalkId
                 orderby t.FTime descending
                 select new
                 {
                     t.FCreateTime,
                     t.FEmpID,
                     t.FID,
                     t.FContent,
                     ent = db.CF_Ent_BaseInfo.Where(e => e.FId == t.FEmpID).FirstOrDefault(),
                     emp = db.CF_Emp_BaseInfo.Where(e => e.FId == t.FEmpID).FirstOrDefault(),
                 }).ToList();

        Pager1.RecordCount = v.Count();
        dgList.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dgList.DataBind();

        Label_FAnswerCount1.Text = "（" + v.Count + "）";
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showContent();
    }
    //回复列表
    protected void dgList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //用户信息
            Literal Link_AppUserName = (Literal)e.Item.FindControl("Link_AppUserName");
            Literal lit_UserInfo = (Literal)e.Item.FindControl("lit_UserInfo");
            string s = "";
            CF_Ent_BaseInfo ent = DataBinder.Eval(e.Item.DataItem, "ent") as CF_Ent_BaseInfo;
            if (ent != null)
            {//企业
                Link_AppUserName.Text = ent.FName;
                s += "<tr><td>联系人：</td><td>" + ent.FLinkMan + "</td></tr>";
                s += "<tr><td>手机：</td><td>" + ent.FTel + "</td></tr>";
                s += "<tr><td>EMail：</td><td>" + ent.FEmail + "</td></tr>";
            }
            else
            {
                CF_Emp_BaseInfo emp = DataBinder.Eval(e.Item.DataItem, "emp") as CF_Emp_BaseInfo;
                if (emp != null)
                {//个人
                    ProjectDB db = new ProjectDB();
                    string entName = db.CF_Ent_BaseInfo.Where(t => t.FId == emp.FBaseInfoID).Select(t => t.FName).FirstOrDefault();
                    Link_AppUserName.Text = emp.FName;
                    s += "<tr><td>所属企业：</td><td>" + entName + "</td></tr>";
                    s += "<tr><td>从事专业：</td><td>" + emp.FRegistSpecialId + "</td></tr>";
                    s += "<tr><td>职称：</td><td>" + db.getDicName(emp.FTechId) + "</td></tr>";
                }
            }
            lit_UserInfo.Text = s;

            //是否楼主
            string FEmpId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEmpID"));
            string FSubmitPerson = EConvert.ToString(ViewState["FSubmitPerson"]);
            if (FSubmitPerson == FEmpId)
            {
                Link_AppUserName.Text += " <tt>[楼主]</tt>";
            }

            //回复时间


            //楼层
            Literal Label_L = (Literal)e.Item.FindControl("Label_L");
            Label_L.Text = "# " + (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
        }
    }

    //回复
    protected void btnOK_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (this.t_FContent.Value.Length < 5)//小于5个字提示。
        {
            this.t_FContent.Focus();
            tool.showMessage("请仔细填写回复内容");
            return;
        }
        SortedList sl = new SortedList();
        sl.Add("FID", Guid.NewGuid().ToString());
        sl.Add("FtalkID", Request.QueryString["FID"]);
        sl.Add("FEmpID", ViewState["FBaseinfoId"]);
        sl.Add("FContent", t_FContent.Value);
        sl.Add("FCreatetime", DateTime.Now);
        sl.Add("FIsDeleted", 0);
        if (oa.SaveEBase(EntityTypeEnum.ETalkProcess, sl, "FID", SaveOptionEnum.Insert))
        {
            tool.showMessage("您的回复提交成功");
            this.t_FContent.Value = "";
            showContent();
        }
    }

    #endregion

    //中止 按钮
    protected void btn_IsOK_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (oa.PExcute("update CF_Talk_talkManage set ftalkstate='3' where fid='" + Request["FID"] + "'"))
        {
            tool.showMessage("操作成功");
            showinfo();
        }
    }
    //提交 按钮 
    protected void btn_GetOn_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (oa.PExcute("update CF_Talk_talkManage set ftalkstate='2',FSubmitTime=getdate(),FTime=getdate() where fid='" + Request["FID"] + "'"))
        {
            tool.showMessage("提交成功");
            showinfo();
        }
    }


    //点项目名称
    protected void Link_projectName_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("TalkList.aspx?FState=" + Request.QueryString["FState"] + "&FProjectID=" + ViewState["FProjectID"]);
    }
    //点参与人名称
    protected void Link_AppUserName_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("TalkList.aspx?FState=" + Request.QueryString["FState"] + "&FSubmitPerson=" + ViewState["FSubmitPerson"]);
    }
    

    //返回按钮 
    protected void btnGetBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TalkList.aspx?FState=" + Request.QueryString["FState"]);
    }
}
