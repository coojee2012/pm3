using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
public partial class JSDW_QMain_Baseinfo : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
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
            if (db.getSysObjectContent("_CanModifyBaseInfo") == "1")
            {
                btnSave.Visible
                = btnAdd.Visible = true;

            }
            else
            {
                btnSave.Visible
                       = btnAdd.Visible
                    = false;
            }
            btnSave.Attributes.Add("onclick", "return CheckInfo();");
            govd_FRegistDeptId.FNumber = ComFunction.GetDefaultCityDept();
            //govd_FRegistDeptId.isEnbel(1);
            showInfo();
            ShowEmpInfo();
        }
    }
    private string IsView { get; set; }
    private string FBaseInfoId { get; set; }
    private string FUserId { get; set; }
    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
        ProjectDB db = new ProjectDB();
        CF_Ent_BaseInfo Ent = db.CF_Ent_BaseInfo.Where(t => t.FId == FBaseInfoId).FirstOrDefault();
        if (Ent != null)
        {
            tool.fillPageControl(Ent);
            //govd_FUpDeptId.FNumber = Ent.FUpDeptId.ToString();
            if (Ent.FRegistDeptId.ToString() != "")
                govd_FRegistDeptId.FNumber = Ent.FRegistDeptId.ToString();

        }
        else
        {

        }
        CF_Sys_User user = db.CF_Sys_User.Where(t => t.FBaseInfoId == FBaseInfoId).FirstOrDefault();
        if (user != null)
        {
            //t_FName.Text = user.FCompany;
            govd_FRegistDeptId.FNumber = user.FManageDeptId.ToString();

            if (string.IsNullOrEmpty(t_FJuridcialCode.Text))
            {
                t_FJuridcialCode.Text = user.FJuridcialCode;
            }
            if (string.IsNullOrEmpty(t_FLinkMan.Text))
            {
                t_FLinkMan.Text = user.FLinkMan;
            }
            if (string.IsNullOrEmpty(t_FTel.Text))
            {
                t_FTel.Text = user.FTel;
            }
        }

        //CF_Ent_Leader T = db.CF_Ent_Leader.Where(d => d.FPersonTypeId == 7021 && d.FBaseInfoId == CurrentEntUser.EntId).FirstOrDefault();

        //if (T != null)
        //{
        //    tool = new pageTool(this.Page, "n_");
        //    tool.fillPageControl(T);
        //}
    }

    //保存
    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        ProjectDB db = new ProjectDB();
        bool IsInsert = false;
        CF_Ent_BaseInfo Ent = db.CF_Ent_BaseInfo.Where(t => t.FId == CurrentEntUser.EntId).FirstOrDefault();
        if (Ent == null)
        {
            IsInsert = true;
            Ent = new CF_Ent_BaseInfo();
            Ent.FId = CurrentEntUser.EntId;
            Ent.FIsDeleted = false;
            Ent.FCreateTime = dTime;
            Ent.FSystemId = EConvert.ToInt(CurrentEntUser.SystemId);
        }
        Ent = tool.getPageValue(Ent);
        Ent.FTime = dTime;
        //Ent.FUpDeptId = EConvert.ToInt(govd_FUpDeptId.FNumber);
        Ent.FRegistDeptId = EConvert.ToInt(govd_FRegistDeptId.FNumber);

        if (IsInsert)
            db.CF_Ent_BaseInfo.InsertOnSubmit(Ent);

        //更新CF_Sys_User表中的FCompany
        CF_Sys_User User = db.CF_Sys_User.Where(t => t.FBaseInfoId == CurrentEntUser.EntId).FirstOrDefault();
        if (User != null)
        {
            User.FCompany = t_FName.Text;
            User.FLinkMan = t_FLinkMan.Text;
            User.FTel = t_FTel.Text;
            //User.FManageDeptId = EConvert.ToInt(govd_FUpDeptId.FNumber);
            User.FJuridcialCode = t_FJuridcialCode.Text;
        }
        SaveLeader();
        db.SubmitChanges();
        tool.showMessage("保存成功");
    }

    private void SaveLeader()
    {
        ProjectDB qdb = new ProjectDB();


        string BaseId = CurrentEntUser.EntId;
        pageTool tool = new pageTool(this.Page, "n_");





        //DateTime dTime = DateTime.Now;
        //SaveOptionEnum so = SaveOptionEnum.Insert;

        //CF_Ent_Leader T = qdb.CF_Ent_Leader.Where(d => d.FPersonTypeId == 7021 && d.FBaseInfoId == BaseId).FirstOrDefault();

        //if (T == null)
        //{
        //    T = new CF_Ent_Leader();
        //}
        //else
        //{

        //    so = SaveOptionEnum.Update;
        //}
        //if (T != null)
        //{

        //    T = tool.getPageValue(T);



        //    if (so == SaveOptionEnum.Insert)
        //    {
        //        string FId = Guid.NewGuid().ToString();
        //        T.FPersonTypeId = 7021;
        //        T.FBaseInfoId = BaseId;
        //        T.FIsDeleted = false;
        //        T.FTime = dTime;
        //        T.FCreateTime = dTime;
        //        T.FId = FId;

        //        qdb.CF_Ent_Leader.InsertOnSubmit(T);

        //    }
        //}
        //qdb.SubmitChanges();

    }
    void ShowEmpInfo()
    {
        IQueryable<CF_Emp_BaseInfo> App = db.CF_Emp_BaseInfo.Where(t => t.FBaseInfoID == FBaseInfoId).OrderByDescending(t => t.FCreateTime);
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowEmpInfo();
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
    protected void btnInput_Click(object sender, EventArgs e)
    {
        ShareTool st = new ShareTool();
        st.UpdateEmp(CurrentEntUser.UserId, CurrentEntUser.EntId);
        pageTool tool = new pageTool(this.Page);
        tool.showMessage("导入成功。");

    }



    protected void btnReload3_Click(object sender, EventArgs e)
    {
        showInfo();
        ShowEmpInfo();
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        ShareTool st = new ShareTool();
        st.DownloadSingleUser(CurrentEntUser.UserId, 126);
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "$('#btnReload3').click()", true);
    }
}
