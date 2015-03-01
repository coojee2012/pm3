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

public partial class KcsjSgt_main_Baseinfo2 : System.Web.UI.Page
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



            showInfo();
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

            //注册人员
            showEmp();



        }

    }

    #region 注册人员

    //显示注册人员
    private void showEmp()
    {
        string FBaseinfoId = CurrentEntUser.EntId;
        
        IQueryable<CF_Emp_BaseInfo> App = db.CF_Emp_BaseInfo.Where(t => t.FBaseInfoID == FBaseInfoId && t.FType == 2).OrderByDescending(t => t.FCreateTime);

        if (this.t_FName.Text != null && this.t_FName.Text != "")
        {
            App = App.Where(t => t.FName.Contains(this.t_FName.Text));
        }
        if (this.t_FIdCard.Text != null && this.t_FIdCard.Text != "")
        {
            App = App.Where(t => t.FIdCard == this.t_FIdCard.Text);
        }

        Pager1.RecordCount = App.Count();
        Emp_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
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




    //保存按钮






    protected void btnReload3_Click(object sender, EventArgs e)
    {
        showInfo();
        showEmp();

    }

}
