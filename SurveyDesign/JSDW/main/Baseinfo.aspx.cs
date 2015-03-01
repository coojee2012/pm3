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
public partial class JSDW_QMain_Baseinfo : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ClientScript.RegisterStartupScript(this.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>showDiv();</script>");
        if (string.IsNullOrEmpty(Request.QueryString["fbid"]))
        {
            FBaseInfoId = CurrentEntUser.EntId;
            FUserId = CurrentEntUser.UserId;
        }
        else
        {
            this.btnSave.Enabled = false;
            FBaseInfoId = Request.QueryString["fbid"];
            FUserId = Request.QueryString["frid"];
            this.ClientScript.RegisterStartupScript(this.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>try{btnEnable();}catch(e){}</script>");
            IsView = "1";
        }
        if (!IsPostBack)
        {
            if (db.getSysObjectContent("_CanModifyBaseInfo") == "1")
            {

                btnSave.Visible  = true;

            }
            else
            {
                //btnSave.Visible   = false;
            }
            btnSave.Attributes.Add("onclick", "return CheckInfo();");
            govd_FRegistDeptId.FNumber = ComFunction.GetDefaultCityDept();
            govd_FRegistDeptId.Dis(1);
            BindCtrol();
            showInfo();
            ShowEmpInfo();

            if (Request.QueryString["fly"] == "1")
            {
                btnSave.Visible = false;
            }
        }
    }
    void BindCtrol()
    {
        //查询建设单位性质
        var dic = db.Dic.Where(t => t.FParentId == 181).OrderBy(t => t.FOrder);
        t_FEntTypeId.DataSource = dic;
        t_FEntTypeId.DataTextField = "FName";
        t_FEntTypeId.DataValueField = "FNumber";
        t_FEntTypeId.DataBind();
    }
    private string IsView { get; set; }
    private string FBaseInfoId { get; set; }
    private string FUserId { get; set; }
    //显示
    private void showInfo()
    {

        pageTool tool = new pageTool(this.Page);
        CF_Ent_BaseInfo Ent = db.CF_Ent_BaseInfo.Where(t => t.FId == FBaseInfoId && t.FLinkMan != null).FirstOrDefault();
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
    void ShowEmpInfo()
    {
        IQueryable<CF_Emp_BaseInfo> App = db.CF_Emp_BaseInfo.Where(t => t.FBaseInfoID == CurrentEntUser.EntId).OrderByDescending(t => t.FCreateTime);
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
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
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('AddEmpInfo.aspx?fid=" + fid + "',600,400);\">" + e.Item.Cells[2].Text + "</a>";
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
        CF_Ent_BaseInfo Ent = db.CF_Ent_BaseInfo.Where(t => t.FId == CurrentEntUser.EntId).FirstOrDefault();
        if (Ent == null)
        {
            Ent = new CF_Ent_BaseInfo();
            Ent.FId = CurrentEntUser.EntId;
            Ent.FIsDeleted = false;
            Ent.FCreateTime = dTime;
            Ent.FSystemId = EConvert.ToInt(CurrentEntUser.SystemId);
            db.CF_Ent_BaseInfo.InsertOnSubmit(Ent);
        }
        Ent = tool.getPageValue(Ent);
        Ent.FTime = dTime;
        Ent.FRegistDeptId = EConvert.ToInt(govd_FRegistDeptId.FNumber);
        Ent.FSystemId = 100;
        //更新CF_Sys_User表中的FCompany
        CF_Sys_User User = db.CF_Sys_User.Where(t => t.FBaseInfoId == CurrentEntUser.UserId).FirstOrDefault();
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
    protected void btnReload3_Click(object sender, EventArgs e)
    {
        showInfo();
  
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        //ShareTool st = new ShareTool();
        //st.DownloadSingleUser(CurrentEntUser.UserId, 100);
        //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "$('#btnReload3').click()", true);
    }
   
}
