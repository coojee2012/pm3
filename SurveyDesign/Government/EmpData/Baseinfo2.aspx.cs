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

public partial class KcsjSgt_main_Baseinfo2 : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            showBound();
            showEmp();
        }
    }
    private string IsView { get; set; }
    private string FBaseInfoId { get; set; }
    private string FUserId { get; set; }

    void showBound()
    {
        
        var dic = db.Dic.Where(t => t.FParentId == 123).OrderBy(t => t.FOrder).Select(t => new { t.FName, t.FNumber }).ToList();
        t_FPersonTypeId.DataSource = dic;
        t_FPersonTypeId.DataTextField = "FName";
        t_FPersonTypeId.DataValueField = "FNumber";
        t_FPersonTypeId.DataBind();
        t_FPersonTypeId.Items.Insert(0, new ListItem("--请选择--", ""));
        if (Request.QueryString["fType"]!=null&&Request.QueryString["fType"] != "")
        {
            this.t_FPersonTypeId.SelectedValue = Request.QueryString["fType"];
            this.t_FPersonTypeId.Enabled = false;
        }
    }

  

    #region 注册人员

    //显示注册人员
    private void showEmp()
    {
        

        string FBaseinfoId = CurrentEntUser.EntId;

        IQueryable<CF_Emp_BaseInfo> App = db.CF_Emp_BaseInfo.Where(t => t.FType == 2).OrderByDescending(t => t.FCreateTime);

        if (this.t_FName.Text != null && this.t_FName.Text != "")
        {
            App = App.Where(t => t.FName.Contains(this.t_FName.Text));
        }
        if (this.t_FIdCard.Text != null && this.t_FIdCard.Text != "")
        {
            App = App.Where(t => t.FIdCard == this.t_FIdCard.Text);
        }
        if (this.t_FPersonTypeId.SelectedValue != "")
        {
            App = App.Where(t => t.FPersonTypeId == EConvert.ToInt(this.t_FPersonTypeId.SelectedValue));
        }
        if (this.t_FEnt.Text != null && this.t_FEnt.Text != "")
        {
            IQueryable<CF_Ent_BaseInfo> ents = db.CF_Ent_BaseInfo.Where(t => t.FName.Contains(this.t_FEnt.Text));
            App = App.Where(t => ents.Select(e => e.FId).Contains(t.FBaseInfoID));
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
            string FBaseInfoID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBaseInfoID"));
            //姓名
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('AddEmpInfo.aspx?fid=" + FID + "&IsView=" + IsView + "&FBaseInfoId=" + FBaseInfoId + "',600,400);\">" + FName + "</a>";
            
            e.Item.Cells[3].Text = rc.GetSignValue("select FName from CF_Ent_BaseInfo where fid='" + FBaseInfoID + "'");


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

        showEmp();

    }

}
