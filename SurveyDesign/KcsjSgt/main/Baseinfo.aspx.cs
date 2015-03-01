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
            conBind();

            showInfo();
        }
    }
    private string IsView { get; set; }
    private string FBaseInfoId { get; set; }
    private string FUserId { get; set; }
    private void conBind()
    {
        govd_FRegistDeptId.FNumber = ComFunction.GetDefaultCityDept();
        govd_FRegistDeptId.Dis(1);

        //资质等级
        c_FLevel.DataSource = db.getSysQualiLevel(160);
        c_FLevel.DataTextField = "FName";
        c_FLevel.DataValueField = "FNumber";
        c_FLevel.DataBind();
        c_FLevel.Items.Insert(0, new ListItem("请选择", ""));
    }


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

            //资质
            var c = db.CF_Ent_QualiCerti.Where(t => t.FBaseInfoId == Ent.FId).Select(t => new { t.FCertiNo, t.FLevel }).FirstOrDefault();
            if (c != null)
            {
                c_FCertiNo.Text = c.FCertiNo;
                c_FLevel.SelectedValue = c.FLevel.ToString();
            }
            //注册人员
            showEmp();

            //非注册人员
            showEmp2();
        }
        else
        {//还没有基本信息表时，从用户中取
            CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == CurrentEntUser.UserId).FirstOrDefault();
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

    #region 注册人员

    //显示注册人员
    private void showEmp()
    {
        string FBaseinfoId = CurrentEntUser.EntId;
        var v = from t in db.CF_Emp_BaseInfo
                where t.FBaseInfoID == FBaseinfoId && t.FType == 2
                orderby t.FCreateTime descending
                select t;

        Pager1.RecordCount = v.Count();
        Emp_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        Emp_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页不显示
    }
    //注册人员列表
    protected void Emp_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));

            //姓名
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('AddEmpInfo.aspx?fid=" + FID + "&IsView=" + IsView + "&FBaseInfoId=" + FBaseInfoId + "',600,400);\">" + FName + "</a>";

            //密码
            string fPwd = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPassWord"));
            if (!string.IsNullOrEmpty(fPwd))
                fPwd = SecurityEncryption.DESDecrypt(fPwd);
            e.Item.Cells[7].Text = fPwd;

        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showEmp();
    }
    //删除按钮 
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(Emp_List, db.CF_Emp_BaseInfo);
        showEmp();
    }
    //刷新
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showEmp();
    }

    #endregion

    #region 非注册人员

    //显示注册人员
    private void showEmp2()
    {
        string FBaseinfoId = CurrentEntUser.EntId;
        var v = from t in db.CF_Emp_BaseInfo
                where t.FBaseInfoID == FBaseinfoId && t.FType == 3
                orderby t.FCreateTime descending
                select t;

        Pager2.RecordCount = v.Count();
        Emp_List2.DataSource = v.Skip((Pager2.CurrentPageIndex - 1) * Pager2.PageSize).Take(Pager2.PageSize);
        Emp_List2.DataBind();
        Pager2.Visible = (Pager2.RecordCount > Pager2.PageSize);//不足一页不显示
    }
    //非注册人员列表
    protected void Emp_List2_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));

            //姓名
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('AddEmpInfo2.aspx?fid=" + FID + "&IsView=" + IsView + "&FBaseInfoId=" + FBaseInfoId + "',600,400,document.getElementById('btnReload2'));\">" + FName + "</a>";

            //密码
            string fPwd = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPassWord"));
            if (!string.IsNullOrEmpty(fPwd))
                fPwd = SecurityEncryption.DESDecrypt(fPwd);
            e.Item.Cells[6].Text = fPwd;

        }
    }
    //分页面控件翻页事件
    protected void Pager2_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager2.CurrentPageIndex = e.NewPageIndex;
        showEmp2();
    }
    //删除按钮 
    protected void btnDel2_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(Emp_List2, db.CF_Emp_BaseInfo);
        showEmp2();
    }
    //刷新
    protected void btnReload2_Click(object sender, EventArgs e)
    {
        showEmp2();
    }

    #endregion


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
        //更新CF_Sys_User表中的FCompany
        CF_Sys_User User = db.CF_Sys_User.Where(t => t.FBaseInfoId == CurrentEntUser.UserId).FirstOrDefault();
        if (User != null)
        {
            User.FCompany = t_FName.Text;
            User.FLinkMan = t_FLinkMan.Text;
            User.FTel = t_FTel.Text;
            User.FJuridcialCode = t_FJuridcialCode.Text;
        }

        //保存资质信息
        CF_Ent_QualiCerti c = db.CF_Ent_QualiCerti.Where(t => t.FBaseInfoId == Ent.FId).FirstOrDefault();
        if (c == null)
        {
            c = new CF_Ent_QualiCerti();
            db.CF_Ent_QualiCerti.InsertOnSubmit(c);
            c.FId = Guid.NewGuid().ToString();
            c.FBaseInfoId = Ent.FId;
            c.FCreateTime = dTime;
            c.FTime = dTime;
            c.FIsDeleted = false;
        }
        c.FLevel = EConvert.ToInt(c_FLevel.SelectedValue);
        c.FLevelName = c_FLevel.SelectedItem.Text;
        c.FCertiNo = c_FCertiNo.Text;

        db.SubmitChanges();
        tool.showMessage("保存成功");
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
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
        showEmp();
        showEmp2();
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        ShareTool st = new ShareTool();
        st.DownloadSingleUser(CurrentEntUser.UserId, 145);
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "$('#btnReload3').click()", true);
    }
}
