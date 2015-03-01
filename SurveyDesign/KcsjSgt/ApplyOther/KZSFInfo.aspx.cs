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

public partial class KcsjSgt_ApplyOther_KZSFInfo : Page
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

        //设防类别
        DataTable dt = rc.getDicTbByFNumber("20006");
        t_FAntiSeismic.DataSource = dt;
        t_FAntiSeismic.DataTextField = "FName";
        t_FAntiSeismic.DataValueField = "FNumber";
        t_FAntiSeismic.DataBind();
        t_FAntiSeismic.Items.Insert(0, new ListItem("--请选择--", ""));

        //结构类型
        dt = rc.getDicTbByFNumber("509");
        t_FStruType.DataSource = dt;
        t_FStruType.DataTextField = "FName";
        t_FStruType.DataValueField = "FNumber";
        t_FStruType.DataBind();
        t_FStruType.Items.Insert(0, new ListItem("--请选择--", ""));
    }

    //显示
    private void showInfo()
    {
        var app = db.CF_Prj_KZSFInfo.Where(t => t.FId == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
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
            t_FRemark.Text = string.Empty;

            var sj = (from t in db.CF_Prj_Ent
                      join a in db.CF_App_List on t.FAppId equals a.FId
                      where a.FPrjId == prj.FId
                      && a.FManageTypeId == 296
                      && t.FEntType == 155 && a.FState == 6
                      select t.FName).FirstOrDefault();
            t_SJDW.Text = sj;
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
        CF_Prj_KZSFInfo data = null;
        if (string.IsNullOrEmpty(fid))
        {
            data = new CF_Prj_KZSFInfo();
            data.FId = Guid.NewGuid().ToString();
            data.FCreateTime = DateTime.Now;
            data.FLinkId = txtFLinkId.Value;
            db.CF_Prj_KZSFInfo.InsertOnSubmit(data);
        }
        else
        {
            data = db.CF_Prj_KZSFInfo.FirstOrDefault(t => t.FId == fid);
        }
        data = tool.getPageValue(data);
        data.FBaseinfoId = CurrentEntUser.EntId;
        data.FTime = DateTime.Now;
        data.FIsDeleted = false;
        db.SubmitChanges();
        tool.showMessageAndRunFunction("保存成功！", "window.returnValue='1';");
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
        ShowPrjInfo(txtFLinkId.Value);
    }
}
