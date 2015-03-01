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
public partial class KC_ApplyHTBA_ApplyBaseInfo : EntAppPage
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            btnSave.Attributes["onclick"] = "return checkInfo();";
            BindControl();
            showInfo();
        }
    }

    //绑定
    private void BindControl()
    {
        //工程地点
        string deptId = ComFunction.GetDefaultDept();
        govd_FRegistDeptId.fNumber = deptId;
        govd_FRegistDeptId.Dis(3);

        string FManageTypeId = EConvert.ToString(Session["FManageTypeId"]);
        if (FManageTypeId == "422")
        {
            govd_FRegistDeptId.RemoveDefaultDept = true;
        }


        //备案部门
        StringBuilder sb = new StringBuilder();
        sb.Append("select case FLevel when 1 then FFullName when 2 then '-'+FName when 3 then '---'+FName end FName,");
        sb.Append("FNumber from cf_Sys_ManageDept ");
        sb.Append("where fnumber like '" + deptId + "%' ");
        sb.Append("and fname<>'市辖区' ");
        sb.Append("order by left(FNumber,4),flevel");
        DataTable dt = rc.GetTable(sb.ToString());
        t_FInt1.DataSource = dt;
        t_FInt1.DataTextField = "FName";
        t_FInt1.DataValueField = "FNumber";
        t_FInt1.DataBind();
    }

    //显示
    private void showInfo()
    {
        string FAppId = EConvert.ToString(Session["FAppId"]);
        var v = (from t in db.CF_App_List
                 join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                 where t.FId == FAppId
                 select new
                 {
                     t.FId,
                     t.FPrjId,
                     t.FLinkId,
                     t.FBaseinfoId,
                     d.FPriItemId,
                     d.FPrjName,
                     d.FInt0,
                     d.FDeptName,
                     d.FTxt1,
                     d.FTxt2,
                     d.FTxt3,
                     d.FTXt4,
                     d.FTxt5,
                     d.FTxt6,
                     d.FTxt7,
                     d.FTxt8,
                     d.FTxt16,
                     d.FTxt18,
                     d.FInt1,
                     d.FInt2,
                     d.FInt3,
                     d.FFloat1,
                     d.FDate1,
                 }).FirstOrDefault();
        if (v != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(v);
            govd_FRegistDeptId.fNumber = v.FInt0.ToString();

            // 承包人（管理人）企业地址： 
            if (string.IsNullOrEmpty(t_FTxt16.Text))
            {
                t_FTxt16.Text = db.CF_Ent_BaseInfo.Where(t => t.FId == v.FBaseinfoId).Select(t => t.FRegistAddress).FirstOrDefault();
            }


            //判断是不中从合同备案业务自动创建的合同备案
            if (!string.IsNullOrEmpty(v.FPriItemId))
            {//是
                //工程基本信息
                ShowPrjInfo(v.FPrjId);


                //合同备案业务
                var WT = (from t in db.CF_Prj_Data
                          join a in db.CF_App_List on t.FId equals a.FLinkId
                          where t.FId == v.FPriItemId && a.FManageTypeId == 280
                          select new { a.FId, a.FLinkId, t.FInt3 }).FirstOrDefault();
                if (WT != null)
                {
                    //取出企业列表
                    var ent = (from t in db.CF_Prj_Ent
                               where t.FAppId == WT.FLinkId && (t.FEntType == 126)
                               select new
                               {
                                   t.FName,
                                   t.FMoney,
                                   t.FEntType
                               }).ToList();

                    //合同金
                    t_FFloat1.Text = ent.Where(t => t.FEntType == 126).Select(t => t.FMoney.ToString()).FirstOrDefault();
                }
            }
            else
            {//否  让灰色控件可填
                for (int i = 0; i < Form.Controls.Count; i++)
                {
                    govd_FRegistDeptId.Dis(0);

                    TextBox tb = Form.Controls[i] as TextBox;
                    if (tb != null && tb.ReadOnly)
                        tb.ReadOnly = false;

                    DropDownList dr = Form.Controls[i] as DropDownList;
                    if (dr != null && !dr.Enabled)
                        dr.Enabled = true;

                    CheckBox cb = Form.Controls[i] as CheckBox;
                    if (cb != null && !cb.Enabled)
                        cb.Enabled = true;
                }
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
            govd_FRegistDeptId.fNumber = prj.FAddressDept;
            t_FTxt2.Text = prj.FPrjNo;
            t_FDeptName.Text = prj.FAllAddress;
            t_FInt1.SelectedValue = prj.FManageDeptId.ToString();
            t_FTxt5.Text = prj.FArea.ToString() + "平方米";
        }
    }

    //保存
    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        string FAppId = EConvert.ToString(Session["FAppId"]);
        DateTime dTime = DateTime.Now;

        //基本信息
        CF_Prj_Data data = db.CF_Prj_Data.Where(t => t.FId == FAppId).FirstOrDefault();
        if (data != null)
        {
            pageTool tooldata = new pageTool(this.Page, "t_");
            data = tooldata.getPageValue(data);
            data.FInt0 = EConvert.ToInt(govd_FRegistDeptId.fNumber);
            data.FTime = dTime;
            //提交保存
            db.SubmitChanges();
            tool.showMessage("保存成功");
        }
    }


    //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}
