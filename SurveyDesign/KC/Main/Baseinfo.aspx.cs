using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Text;
using Approve.RuleCenter;
public partial class JSDW_QMain_Baseinfo : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["fbid"]))
        {
            FBaseInfoId = CurrentEntUser.EntId;
            FUserId = CurrentEntUser.UserId;
        }
        else
        {
            FBaseInfoId = Request.QueryString["fbid"];
            FUserId = Request.QueryString["frid"];
            this.ClientScript.RegisterStartupScript(this.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>try{btnEnable();}catch(e){}</script>");
            IsView = "1";
        }
        if (!IsPostBack)
        {
            if (rc.GetSysObjectContent("_CanModifyBaseInfo") == "1")
            {
                btnAdd.Visible

               = btnMod.Visible = true;

            }
            else
            {
                 btnAdd.Visible
                   = btnMod.Visible
                    = false;
            }
            btnSave.Attributes.Add("onclick", "return CheckInfo();");
            govd_FRegistDeptId.FNumber = ComFunction.GetDefaultCityDept();
            govd_FRegistDeptId.Dis(1);

            showInfo();
            ShowEmpInfo();
            ShowCertiInfo();
        }
    }
    private string IsView { get; set; }
    private string FBaseInfoId { get; set; }
    private string FUserId { get; set; }
    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
        CF_Ent_BaseInfo Ent = db.CF_Ent_BaseInfo.Where(t => t.FId == FBaseInfoId && t.FJuridcialCode != null).FirstOrDefault();
        if (Ent != null)
        {
            tool.fillPageControl(Ent);
            if (Ent.FRegistDeptId.ToString() != "")
                govd_FRegistDeptId.FNumber = Ent.FRegistDeptId.ToString();
        }
        else
        {
            CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == FUserId).FirstOrDefault();
            if (user != null)
            {
                t_FName.Text = user.FCompany;
                t_FJuridcialCode.Text = user.FJuridcialCode;
                t_FLinkMan.Text = user.FLinkMan;
                t_FTel.Text = user.FTel;
                govd_FRegistDeptId.fNumber = user.FManageDeptId.ToString();
            }
        }
    }
    void ShowCertiInfo()
    {
        var App = db.CF_Ent_QualiCerti.Where(t => t.FBaseInfoId == FBaseInfoId).OrderByDescending(t => t.FCreateTime)
             .Select(t => new
             {
                 t.FId,
                 t.FCertiNo,
                 t.FLevelName,
                 FCertiType = db.CF_Sys_Dic.Where(l => l.FNumber == t.FCertiType).Select(l => l.FName).FirstOrDefault(),
                 t.FEndTime
             }); ;

        dgCerti_List.DataSource = App;
        dgCerti_List.DataBind();
    }
    void ShowEmpInfo()
    {
        IQueryable<CF_Emp_BaseInfo> App = db.CF_Emp_BaseInfo.Where(t => t.FBaseInfoID == FBaseInfoId).OrderByDescending(t => t.FCreateTime);
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }
    protected void dgCerti_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('AddCertiInfo.aspx?fid=" + fid + "&IsView=" + IsView + "&FBaseInfoId=" + FBaseInfoId + "',800,520,$('#btnReload1'));\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string fPwd = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPassWord"));
            if (!string.IsNullOrEmpty(fPwd))
                fPwd = SecurityEncryption.DESDecrypt(fPwd);
            e.Item.Cells[7].Text = fPwd;
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('AddEmpInfo.aspx?fid=" + fid + "&IsView=" + IsView + "&FBaseInfoId=" + FBaseInfoId + "',600,400);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowEmpInfo();
    }
    //保存
    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        CF_Ent_BaseInfo Ent = db.CF_Ent_BaseInfo.Where(t => t.FId == FBaseInfoId).FirstOrDefault();
        if (Ent == null)
        {
            Ent = new CF_Ent_BaseInfo();
            Ent.FId = FBaseInfoId;
            Ent.FIsDeleted = false;
            Ent.FCreateTime = dTime;
            Ent.FSystemId = EConvert.ToInt(rc.getEntSystemId(FBaseInfoId));
            db.CF_Ent_BaseInfo.InsertOnSubmit(Ent);
        }
        Ent = tool.getPageValue(Ent);
        Ent.FTime = dTime;
        Ent.FRegistDeptId = EConvert.ToInt(govd_FRegistDeptId.FNumber);
        Ent.FSystemId = 155;
        //更新CF_Sys_User表中的FCompany
        CF_Sys_User User = db.CF_Sys_User.Where(t => t.FBaseInfoId == FBaseInfoId).FirstOrDefault();
        if (User != null)
        {
            User.FCompany = t_FName.Text;
            User.FLinkMan = t_FLinkMan.Text;
            User.FTel = t_FTel.Text;
            User.FJuridcialCode = t_FJuridcialCode.Text;
        }
        db.SubmitChanges();
        tool.showMessage("保存成功");
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, db.CF_Emp_BaseInfo);
        ShowEmpInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowEmpInfo();
    }
    protected void btnDel1_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dgCerti_List, db.CF_Ent_QualiCerti, tool_Deleting);
        ShowCertiInfo();
    }
    private void tool_Deleting(IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        IQueryable<CF_Ent_QualiCertiTrade> UserResult = db.CF_Ent_QualiCertiTrade.Where(u => FIdList.ToArray().Contains(u.FCertiId));
        db.CF_Ent_QualiCertiTrade.DeleteAllOnSubmit(UserResult);
    }
    protected void btnReload1_Click(object sender, EventArgs e)
    {
        ShowCertiInfo();
    }
    protected void btnInput_Click(object sender, EventArgs e)
    {
        ShareTool st = new ShareTool();
        st.UpdateEmp(CurrentEntUser.UserId, CurrentEntUser.EntId);
        pageTool tool = new pageTool(this.Page);
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "alert('导入成功');$('#btnReload').click()", true);
        //tool.showMessage("导入成功。");
        //ShowEmpInfo();
    }
    protected void btnInput0_Click(object sender, EventArgs e)
    {
        ShareTool st = new ShareTool();
        st.UpdateCerti(CurrentEntUser.UserId, CurrentEntUser.EntId);
        pageTool tool = new pageTool(this.Page);
        tool.showMessage("导入成功。");
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "alert('导入成功');$('#btnReload1').click()", true);
        //ShowCertiInfo();
    }
    protected void btnReload3_Click(object sender, EventArgs e)
    {
        showInfo();
        ShowEmpInfo();
        ShowCertiInfo();

    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        ShareTool st = new ShareTool();
        st.DownloadSingleUser(CurrentEntUser.UserId, 15501);
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "$('#btnReload3').click()", true);
    }
}
