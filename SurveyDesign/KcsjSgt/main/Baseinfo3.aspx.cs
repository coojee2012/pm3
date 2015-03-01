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

        IQueryable<CF_Emp_BaseInfo> App = db.CF_Emp_BaseInfo.Where(t => t.FBaseInfoID == FBaseInfoId && t.FType == 3).OrderByDescending(t => t.FCreateTime);

        if (this.t_FName.Text != null && this.t_FName.Text != "")
        {
            App = App.Where(t => t.FName.Contains(this.t_FName.Text));
        }
        if (this.t_FIdCard.Text != null && this.t_FIdCard.Text != "")
        {
            App = App.Where(t => t.FIdCard == this.t_FIdCard.Text);
        }
        if (!string.IsNullOrEmpty(ddlFState.SelectedValue))
            App = App.Where(t => t.FState == EConvert.ToInt(ddlFState.SelectedValue));
        Pager2.RecordCount = App.Count();
        Emp_List2.DataSource = App.Skip((Pager2.CurrentPageIndex - 1) * Pager2.PageSize).Take(Pager2.PageSize).Select(t => new
        {
            t.FId,
            t.FName,
            t.FIdCard,
            t.FRegistSpecialId,
            t.FState,
            FUserName = t.FUserName == null ? "--" : t.FUserName,
            t.FPassword
        });
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
            string fDesc = "未上报";
            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 2].Controls[0] as LinkButton;
            CheckBox box = e.Item.Cells[0].Controls[1] as CheckBox;
            int fstate = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FState"));
            if (fstate == 1)
            {
                fDesc = "已上报";
                lb.Text = "撤销";
                lb.CommandName = "CX";
                lb.Attributes.Add("onclick", "return confirm('确认要撤销上报吗？');");
                lb.Enabled = true;
                box.Enabled = false;
            }
            else if (fstate == 2)
            {
                fDesc = "<a href='javascript:void(0)' onclick=\"showAddWindow('ShowAppIdea.aspx?fid=" + FID + "',500,400);\"><font color='red'>打回</font></a>";
                lb.Text = "上报";
                lb.Attributes.Add("onclick", "return confirm('确认要上报吗？');");
                lb.Enabled = true;
                box.Enabled = true;
            }
            else if (fstate == 6)
            {
                fDesc = "<font color='green'>已审核</font>";
                lb.Text = "--";
                lb.Enabled = false;
                box.Enabled = false;
            }
            else
            {
                lb.Text = "上报";
                lb.Enabled = true;
                lb.Attributes.Add("onclick", "return confirm('确认要上报吗？');");
                box.Enabled = true;
            }
            e.Item.Cells[7].Text = fDesc;
            //string fPwd = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPassword"));
            //if (!string.IsNullOrEmpty(fPwd))
            //    fPwd = SecurityEncryption.DESDecrypt(fPwd);
            //e.Item.Cells[6].Text = fPwd; 
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
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(Emp_List2, db.CF_Emp_BaseInfo);
        showEmp2();
    }
    protected void Emp_List2_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fState = e.CommandName == "CX" ? "0" : "1";
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            if (!string.IsNullOrEmpty(fid))
            {
                rc.PExcute("update CF_Emp_BaseInfo set FState='" + fState + "' where FId='" + fid + "'");
                pageTool tool = new pageTool(this.Page);
                showEmp2();
                tool.showMessage("操作成功！");
            }
        }
    }
}
