using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;

public partial class JZDW_Main_Baseinfo3 : System.Web.UI.Page
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
            
           
            ShowEmpInfo();
        }
    }
    private string IsView { get; set; }
    private string FBaseInfoId { get; set; }
    private string FUserId { get; set; }
    //显示

    void ShowEmpInfo()
    {
        IQueryable<CF_Emp_BaseInfo> App = db.CF_Emp_BaseInfo.Where(t => t.FBaseInfoID == FBaseInfoId).OrderByDescending(t => t.FCreateTime);

        if (this.t_FName.Text != null && this.t_FName.Text != "")
        {
            App = App.Where(t => t.FName.Contains(this.t_FName.Text));
        }
        if (this.t_FIdCard.Text != null && this.t_FIdCard.Text != "")
        {
            App = App.Where(t => t.FIdCard == this.t_FIdCard.Text);
        }

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




    protected void btnsel_Click(object sender, EventArgs e)
    {
        ShowEmpInfo();
    }
}
