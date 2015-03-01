using System;
using System.Linq;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Web.UI;

public partial class KcsjSgt_ApplyOther_JZJZJNInfo : Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            if (!string.IsNullOrEmpty(Request.QueryString["FId"]))
            {
                ViewState["FID"] = Request.QueryString["FId"];
                showInfo();
            }
        }
    }
    #region 显示

    //绑定默认
    private void BindControl()
    {
        RCenter rc = new RCenter();
        govd_FRegistDeptId.fNumber = ComFunction.GetDefaultDept();
        govd_FRegistDeptId.Dis(3);

        //结构类型
        DataTable dt = rc.getDicTbByFNumber("509");
        t_FStruType.DataSource = dt;
        t_FStruType.DataTextField = "FName";
        t_FStruType.DataValueField = "FNumber";
        t_FStruType.DataBind();
        t_FStruType.Items.Insert(0, new ListItem("--请选择--", ""));

        //建筑使用性质
        dt = rc.getDicTbByFNumber("20003");
        t_FNature.DataSource = dt;
        t_FNature.DataTextField = "FName";
        t_FNature.DataValueField = "FNumber";
        t_FNature.DataBind();
    }

    //显示
    private void showInfo()
    {
        var app = db.CF_Prj_JZJNInfo.Where(t => t.FId == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
        if (app != null)
        {
            pageTool ktool = new pageTool(this.Page, "t_");
            ktool.fillPageControl(app);
        }
    }
    /// <summary>
    /// 显示工程信息和建设单位
    /// </summary>
    private void ShowPrjInfo(string FPrjId)
    {
        CF_Prj_BaseInfo prj = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId).FirstOrDefault();
        if (prj != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(prj);
            govd_FRegistDeptId.fNumber = t_FAddressDept.Value;

            //显示子工程
            //查询子项工程名称
            var prjItemList = db.CF_PrjItem_BaseInfo.Where(t => t.FPrjId == FPrjId).Select(t => t.FPrjItemName).ToList();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < prjItemList.Count; i++)
            {
                if (sb.Length > 0)
                    sb.Append("；");
                sb.Append(prjItemList[i]);
            }
            t_FSubName.Text = sb.ToString();

            var sj = (from t in db.CF_Prj_Ent
                      join a in db.CF_App_List on t.FAppId equals a.FId
                      where a.FPrjId == prj.FId
                      && a.FManageTypeId == 296
                      && t.FEntType == 155 && a.FState == 6
                      select new
                     {
                         t.FName,
                         t.FLevelName,
                         t.FCertiNo
                     }).FirstOrDefault();
            if (sj != null)
            {
                t_SJDW.Text = sj.FName;
                t_FSJLevelName.Text = sj.FLevelName;
                t_FSJCertiNo.Text = sj.FCertiNo;
            }

            //审查机构
            var s = (from t in db.CF_Prj_Ent
                     join a in db.CF_App_List on t.FAppId equals a.FId
                     where a.FManageTypeId == 300 && t.FEntType == 145
                     && t.FPrjId == txtFLinkId.Value && a.FState == 6
                     && t.FBaseInfoId == CurrentEntUser.EntId
                     select new
                     {
                         t.FName,
                         t.FLevelName,
                         t.FCertiNo
                     }).FirstOrDefault();
            if (s != null)
            {
                t_FSCJG.Text = sj.FName;
                t_FSCLevelName.Text = s.FLevelName;
                t_FSCCertiNo.Text = s.FCertiNo;
            }
        }
    }
    #endregion
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
    void SaveInfo()
    {
        pageTool tool = new pageTool(this, "t_");
        string fid = EConvert.ToString(ViewState["FID"]);
        CF_Prj_JZJNInfo data = null;
        if (string.IsNullOrEmpty(fid))
        {
            data = new CF_Prj_JZJNInfo();
            data.FId = Guid.NewGuid().ToString();
            data.FCreateTime = DateTime.Now;
            data.FLinkId = txtFLinkId.Value;
            db.CF_Prj_JZJNInfo.InsertOnSubmit(data);
        }
        else
        {
            data = db.CF_Prj_JZJNInfo.FirstOrDefault(t => t.FId == fid);
        }
        data = tool.getPageValue(data);
        data.FBaseinfoId = CurrentEntUser.EntId;
        data.FTime = DateTime.Now;
        data.FIsDeleted = false;
        data.FPrjType = 2;//居住建筑 
        db.SubmitChanges();
        tool.showMessageAndRunFunction("保存成功！", "window.returnValue='1';");
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
        ShowPrjInfo(txtFLinkId.Value);
    }
}
