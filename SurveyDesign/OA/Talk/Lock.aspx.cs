using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;
using Tools;

public partial class OA_Talk_Lock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //当前用户
            if (!string.IsNullOrEmpty(CurrentEntUser.EntId))
                ViewState["FBaseinfoId"] = CurrentEntUser.EntId;
            else if (!string.IsNullOrEmpty(CurrentEmpUser.EmpId))
                ViewState["FBaseinfoId"] = CurrentEmpUser.EmpId;
            showInfo();
        }
    }

    private void showInfo()
    {
        ProjectDB db = new ProjectDB();
        string FState = Request.QueryString["FState"];
        string FID = Request.QueryString["FID"];
        var v = (from t in db.CF_Talk_TalkManage
                 where t.FID == FID
                 select new
                 {
                     t.FID,
                     t.FTalkName,
                     t.FLinkWay,
                     t.FKey,
                     ent = db.CF_Ent_BaseInfo.Where(e => e.FId == t.FSubmitPerson).Select(e => new
                     {
                         e.FName,
                     }).FirstOrDefault(),
                     emp = db.CF_Emp_BaseInfo.Where(e => e.FId == t.FSubmitPerson).Select(e => new
                     {
                         e.FName,
                     }).FirstOrDefault(),

                 }).FirstOrDefault();

        if (v != null)
        {
            ViewState["FKey"] = v.FKey;
            t_FTalkName.Text = v.FTalkName;
            t_FLinkWay.Text = v.FLinkWay;
            string s = "";
            if (v.ent != null)
            {//企业
                s = v.ent.FName;
            }
            else if (v.emp != null)
            {//个人  
                s = v.emp.FName;
            }
            t_userName.Text = s;
        }
    }


    //解锁按钮
    protected void btnOK_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        string FID = Request.QueryString["FID"];
        string FBaseinfoId = EConvert.ToString(ViewState["FBaseinfoId"]);
        string FKey = EConvert.ToString(ViewState["FKey"]);
        if (t_FKey.Text == FKey)
        {//解锁成功
            ProjectDB db = new ProjectDB();
            CF_Talk_Relation r = db.CF_Talk_Relation.Where(t => t.FTalkId == FID && t.FEmpId == FBaseinfoId).FirstOrDefault();
            if (r == null)
            {
                r = new CF_Talk_Relation();
                db.CF_Talk_Relation.InsertOnSubmit(r);
                r.FID = Guid.NewGuid().ToString();
                r.FIsDeleted = 0;
                r.FCreateTime = DateTime.Now;
                r.FTalkId = FID;
                r.FEmpId = FBaseinfoId;
            }
            r.FKey = FKey;
            r.FTime = DateTime.Now;
            db.SubmitChanges();

            tool.showMessageAndRunFunction("解锁成功！", "window.returnValue=1;window.close();");
        }
        else
        {//解锁失败
            tool.showMessage("解锁失败！");
        }
    }
}
