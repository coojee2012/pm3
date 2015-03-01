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
public partial class JSDW_appmain_AddStopPrj : Page
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
        if (!string.IsNullOrEmpty(Request.QueryString["FPrjId"]))
        {
            t_FPrjId.Value = Request.QueryString["FPrjId"];
        }

        t_FAppDate.Text = DateTime.Now.ToShortDateString();
    }

    //显示
    private void showInfo()
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


            var s = (from t in db.CF_Prj_Stop
                     where t.FPrjId == FPrjId
                     select new
                     {
                         t.FAppDate,
                         t.FUserId,
                         t.FUserName,
                         t.FRemark,
                         t.FPrjId,
                         t.FTYpe
                     }).FirstOrDefault();
            if (s != null)
            {
                pageTool tool = new pageTool(this.Page);
                tool.fillPageControl(s);

                if (s.FTYpe == 1)
                {//自已终止的可以恢复
                    btnOp.CommandArgument = "2";
                    btnOp.Text = "恢复";
                    btnOp.Attributes["onclick"] = "return confirm('确定要恢复该项目吗？'); ";
                }
                else
                { //主管部门终止的不能恢复
                    btnOp.Visible = false;
                }
            }
            else
            {
                t_FUserName.Text = CurrentEntUser.EntName;

                btnOp.CommandArgument = "1";
                btnOp.Text = "终止";
                btnOp.Attributes["onclick"] = "return checkInfo();";
            }
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

            CF_Prj_Stop s = new CF_Prj_Stop();
            db.CF_Prj_Stop.InsertOnSubmit(s);

            s.FID = Guid.NewGuid().ToString();
            s.FIsDeleted = 0;
            s.FCreateTime = dTime;
            s.FTime = dTime;
            s.FTYpe = 1; //建设单位自已终止的
            s.FPrjId = t_FPrjId.Value; //要终止的项目
            s.FUserId = CurrentEntUser.EntId; //操作人FID
            s.FUserName = t_FUserName.Text;
            s.FAppDate = EConvert.ToDateTime(t_FAppDate.Text);
            s.FRemark = t_FRemark.Text;

            db.SubmitChanges();
            //ViewState["FID"] = s.FID.ToString();
            tool.showMessageAndRunFunction("终止成功", "window.returnValue=1;window.close();");
        }
        else if (type == "2")
        {//恢复
            //string FID = EConvert.ToString(ViewState["FID"]);

            CF_Prj_Stop s = db.CF_Prj_Stop.Where(t => t.FPrjId == t_FPrjId.Value).FirstOrDefault();
            db.CF_Prj_Stop.DeleteOnSubmit(s);
            db.SubmitChanges();

            tool.showMessageAndRunFunction("恢复成功", "window.returnValue=1;window.close();");
        }
    }

}
