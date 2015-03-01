﻿using System;
using System.Linq;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;

public partial class KcsjSgt_ApplyKCCXXSC_ApplyBaseInfo : EntAppPage
{
    ProjectDB db = new ProjectDB();
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            BindControl();
            showInfo();
        }
    }
    #region 显示

    //绑定默认
    private void BindControl()
    {
        govd_FRegistDeptId.fNumber = ComFunction.GetDefaultDept();
        govd_FRegistDeptId.Dis(3);

    }

    //显示
    private void showInfo()
    {
        //要从 勘察合同备案业务(287) 中查合同备案基本信息，所以从Get中接收业务ID
        string fAppId = Request.QueryString["FAppId"];

        var v = (from a in db.CF_App_List
                 join d in db.CF_Prj_Data on a.FId equals d.FAppId
                 where a.FId == fAppId
                 select new
                 {
                     a.FId,
                     a.FBaseinfoId,
                     d.FTxt10,
                     d.FTxt11,
                     a.FPrjId
                 }).FirstOrDefault();
        if (v != null)
        {
            t_FTxt10.Text = v.FTxt10;
            t_FTxt11.Text = v.FTxt11;

            //显示工程信息
            ShowPrjInfo(v.FPrjId);

            //显示建设单位信息
            ShowEntInfo(v.FBaseinfoId);

            //显示审图机构信息
            var ent = (from t in db.CF_Prj_Ent
                       where t.FAppId == v.FId && t.FEntType == 145
                       select new { t.FId, t.FBaseInfoId, t.FName, t.FLevelName, t.FCertiNo, t.FMoney, t.FPlanDate }).FirstOrDefault();
            if (ent != null)
            {
                pageTool tool = new pageTool(this.Page, "s_");
                tool.fillPageControl(ent);
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
            govd_FRegistDeptId.fNumber = p_FAddressDept.Value;
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
            e_FAddress.Text = ent.FEmail;
        }
    }

    #endregion

    #region 保存

    //保存
    private void saveInfo()
    {
        ProjectDB pdb = new ProjectDB();
        pageTool tool = new pageTool(this.Page, "s_");
        DateTime dTime = DateTime.Now;
        string FAppId = EConvert.ToString(Session["FAppId"]);
        CF_Prj_Data data = pdb.CF_Prj_Data.Where(t => t.FId == FAppId).FirstOrDefault();
        if (data == null)
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }
        data.FBaseInfoId = CurrentEntUser.EntId;
        data.FTime = dTime;
        data.FPrjName = p_FPrjName.Text.Trim();
        data.FTxt10 = t_FTxt10.Text.Trim();
        data.FTxt11 = t_FTxt11.Text.Trim();

        //保存企业
        CF_Prj_Ent ent = pdb.CF_Prj_Ent.Where(t => t.FAppId == FAppId && t.FEntType == 145).FirstOrDefault();
        if (ent == null)
        {
            ent = new CF_Prj_Ent();
            pdb.CF_Prj_Ent.InsertOnSubmit(ent);
            ent.FId = Guid.NewGuid().ToString();
            ent.FIsDeleted = false;
            ent.FEntType = 145;
            ent.FAppId = FAppId;
            ent.FPrjId = data.FPrjId;
        }
        ent = tool.getPageValue(ent);

        pdb.SubmitChanges();
        tool.showMessage("保存成功");
    }

    #endregion

    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        if (btn.CommandName == "SGT")//施工图
        {
            var v = (from b in db.CF_Ent_BaseInfo
                     join c in db.CF_Ent_QualiCerti on b.FId equals c.FBaseInfoId
                     where b.FId == s_FBaseInfoId.Value
                     select new
                     {
                         b.FName,
                         c.FCertiNo,
                         c.FLevelName
                     }).FirstOrDefault();

            if (v != null)
            {
                pageTool tool = new pageTool(this.Page, "s_");
                tool.fillPageControl(v);
            }
        }
    }
}
