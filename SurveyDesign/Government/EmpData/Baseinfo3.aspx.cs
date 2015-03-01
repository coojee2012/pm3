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

public partial class KcsjSgt_main_Baseinfo3 : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {

            showEmp2();

        }
    }
    private string IsView { get; set; }
    private string FBaseInfoId { get; set; }
    private string FUserId { get; set; }





    #region 非注册人员

    //显示注册人员
    private void showEmp2()
    {
        string FBaseinfoId = CurrentEntUser.EntId;
        
        IQueryable<CF_Emp_BaseInfo> App = db.CF_Emp_BaseInfo.Where(t =>  t.FType == 3).OrderByDescending(t => t.FCreateTime);

        if (this.t_FName.Text != null && this.t_FName.Text != "")
        {
            App = App.Where(t => t.FName.Contains(this.t_FName.Text));
        }
        if (this.t_FIdCard.Text != null && this.t_FIdCard.Text != "")
        {
            App = App.Where(t => t.FIdCard == this.t_FIdCard.Text);
        }
        Pager2.RecordCount = App.Count();
        Emp_List2.DataSource = App.Skip((Pager2.CurrentPageIndex - 1) * Pager2.PageSize).Take(Pager2.PageSize);
        Emp_List2.DataBind();
        Pager2.Visible = (Pager2.RecordCount > Pager2.PageSize);//不足一页不显示
    }
    //非注册人员列表
    protected void Emp_List2_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager2.PageSize * (this.Pager2.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));

            //姓名
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('AddEmpInfo2.aspx?fid=" + FID + "&IsView=" + IsView + "&FBaseInfoId=" + FBaseInfoId + "',600,400,document.getElementById('btnReload2'));\">" + FName + "</a>";

            //密码
            string fPwd = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPassWord"));
            if (!string.IsNullOrEmpty(fPwd))
                fPwd = SecurityEncryption.DESDecrypt(fPwd);
            e.Item.Cells[6].Text = fPwd;

            string FBaseInfoID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBaseInfoID"));
            e.Item.Cells[3].Text = rc.GetSignValue("select FName from CF_Ent_BaseInfo where fid='" + FBaseInfoID + "'");
        }
    }
    //分页面控件翻页事件
    protected void Pager2_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager2.CurrentPageIndex = e.NewPageIndex;
        showEmp2();
    }
    //删除按钮 


    #endregion




    protected void btnReload3_Click(object sender, EventArgs e)
    {
        
        showEmp2();
    }

}
