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
public partial class Government_EntData_JsPrjList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }
    //显示 
    void showInfo()
    {
        IQueryable<CF_Prj_BaseInfo> App = db.CF_Prj_BaseInfo.Where(t => t.FBaseinfoId == Request.QueryString["FBaseinfoId"]).OrderByDescending(t => t.FCreateTime);
        if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
            App = App.Where(t => t.FPrjName.Contains(t_FName.Text.Trim()));
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
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('AddPrjRegist.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
            string fDeptId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAddressDept"));
            if (!string.IsNullOrEmpty(fDeptId))
                fDeptId = rc.getDept(fDeptId, 1);
            e.Item.Cells[4].Text = fDeptId;
            string fType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FType"));
            if (!string.IsNullOrEmpty(fType))
                fType = rc.GetDicName(fType);
            e.Item.Cells[5].Text = fType;
            //查看项目办理情况
            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 2].Controls[0] as LinkButton;
            //判断，有该工程的任何业务，则不可再行删除
            if (db.CF_App_List.Count(t => t.FPrjId == fid) > 0)
            {
                CheckBox box = e.Item.Cells[0].Controls[1] as CheckBox;
                box.Enabled = false;
                e.Item.Cells[0].ToolTip = "该工程已经参与了业务办理，不可修改和删除！";
                lb.Text = "点击查看";
                lb.Attributes.Add("onclick", "javascript:showAddWindow(\"../statis/Prjall.aspx?FID=" + fid + "\",900,700);return false;");
            }
            else
            {
                lb.Text = "无办理情况";
                lb.Enabled = false;
            }
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, db.CF_Prj_BaseInfo);
        showInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
