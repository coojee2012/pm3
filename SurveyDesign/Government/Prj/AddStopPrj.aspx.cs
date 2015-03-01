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
using System.Collections;
using Approve.RuleApp;
public partial class Government_Prj_AddStopPrj : Page
{
    ProjectDB db = new ProjectDB();
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
        if (!string.IsNullOrEmpty(Request.QueryString["FID"]))
        {
            ViewState["FID"] = Request.QueryString["FID"];
        }

        t_FAppDate.Text = DateTime.Now.ToShortDateString();
    }

    //显示
    private void showInfo()
    {
        string FID = EConvert.ToString(ViewState["FID"]);
        var v = (from t in db.CF_Prj_Stop
                 where t.FID == FID
                 select new
                 {
                     t.FAppDate,
                     t.FUserId,
                     t.FUserName,
                     t.FRemark,
                     t.FPrjId,
                     t.FTYpe
                 }).FirstOrDefault();
        if (v != null)
        {
            pageTool tool = new pageTool(this.Page);
            tool.fillPageControl(v);

            showPrjInfo();

            btnOp.CommandArgument = "2";
            btnOp.Text = "恢复";
            btnOp.Attributes["onclick"] = "return confirm('确定要恢复该项目吗？'); ";

            btnSel.Visible = false;
        }
        else
        {

            btnOp.CommandArgument = "1";
            btnOp.Text = "终止";
            btnOp.Attributes["onclick"] = "if (confirm('确定要终止该项目吗？')){return checkInfo();}else{return false;}";
        }
    }

    //显示项目信息
    private void showPrjInfo()
    {
        string FPrjId = t_FPrjId.Value;

        var v = (from p in db.CF_Prj_BaseInfo
                 where p.FId == FPrjId
                 select new
                 {
                     p.FPrjName,
                     p.FAddressDept,
                     p.FAllAddress,
                     JSDW = db.CF_Ent_BaseInfo.Where(a => a.FId == p.FBaseinfoId).Select(a => a.FName).FirstOrDefault(),
                 }).FirstOrDefault();

        if (v != null)
        {
            p_FPrjName.Text = v.FPrjName;
            p_FAddress.Text = db.getDeptFullName(v.FAddressDept) + v.FAllAddress;
            p_JSDW.Text = v.JSDW;

        }

    }


    //保存按钮
    protected void btnOp_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        string type = ((Button)sender).CommandArgument.ToString();
        if (type == "1")
        {//终止
            DateTime dTime = DateTime.Now;
            string UserId = EConvert.ToString(Session["DFUserId"]);

            CF_Prj_Stop s = new CF_Prj_Stop();
            db.CF_Prj_Stop.InsertOnSubmit(s);

            s.FID = Guid.NewGuid().ToString();
            s.FIsDeleted = 0;
            s.FCreateTime = dTime;
            s.FTime = dTime;
            s.FTYpe = 2;//表示是管理部门给终止的
            s.FPrjId = t_FPrjId.Value;//要终止的项目
            s.FUserId = UserId;//操作人FID
            s.FUserName = t_FUserName.Text;
            s.FAppDate = EConvert.ToDateTime(t_FAppDate.Text);
            s.FRemark = t_FRemark.Text;

            db.SubmitChanges();

            tool.showMessageAndRunFunction("终止成功", "window.returnValue=1;window.close();");
        }
        else if (type == "2")
        {//恢复
            string FID = EConvert.ToString(ViewState["FID"]);

            CF_Prj_Stop s = db.CF_Prj_Stop.Where(t => t.FID == FID).FirstOrDefault();
            db.CF_Prj_Stop.DeleteOnSubmit(s);
            db.SubmitChanges();

            tool.showMessageAndRunFunction("恢复成功", "window.returnValue=1;window.close();");
        }
    }

    //选择项目按钮 
    protected void btnSel_Click(object sender, EventArgs e)
    {
        showPrjInfo();
    }
}
