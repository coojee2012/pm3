using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;

public partial class Share_Help_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "if (checkCount()>0){ return confirm('确定要删除吗？');}else{alert('请选择要删除的项');return false;}");
            conBind();
            showInfo();
        }
    }

    //绑定默认
    private void conBind()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["FLinkNumber"]))
        {
            t_FLinkNumber.Text = Request.QueryString["FLinkNumber"];
            t_FLinkNumber.ReadOnly = true;
        }
    }

    //显示
    private void showInfo()
    {
        ProjectDB db = new ProjectDB();
        IQueryable<CF_Sys_HelpMsg> m = db.CF_Sys_HelpMsg;
        if (!string.IsNullOrEmpty(t_FTitle.Text))
        {
            m = m.Where(t => t.FTitle.Contains(t_FTitle.Text));
        }
        if (!string.IsNullOrEmpty(t_FLinkNumber.Text))
        {
            m = m.Where(t => t.FLinkNumber.Contains(t_FLinkNumber.Text));
        }

        Pager1.RecordCount = m.Count();
        DG_List.DataKeyField = "FID";
        DG_List.DataSource = m.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
    }

    //分面控件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }

    //绑定到行
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FTitle = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FTitle"));
            string FLinkNumber = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkNumber"));
            string FContent = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FContent"));

            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('edit.aspx?FID=" + FID + "',600,600);\">" + FTitle + "</a>";

            ProjectDB db = new ProjectDB();
            string MenuName = db.Menu.Where(t => t.FNumber == FLinkNumber).Select(t => t.FName).FirstOrDefault();
            e.Item.Cells[3].Text += (!string.IsNullOrEmpty(MenuName) ? "[" + MenuName + "]" : "");

            pageTool tool = new pageTool(this.Page);
            //e.Item.Cells[4].Text = tool.staticStringbSubstring(FContent, 30, "...");
        }
    }

    //查询按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    //删除按钮 
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        ProjectDB db = new ProjectDB();
        tool.DelInfoFromGrid(DG_List, db.CF_Sys_HelpMsg);
        showInfo();
    }
}
