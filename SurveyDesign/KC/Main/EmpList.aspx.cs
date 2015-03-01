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
public partial class JSDW_QMain_EmpList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowEmpInfo();
        }
    }
    void ShowEmpInfo()
    {
        string fbid = Request.QueryString["fbid"];
        int ftype = EConvert.ToInt(Request.QueryString["ftype"]);
        if (!string.IsNullOrEmpty(fbid) && ftype != 0)
        {
            IQueryable<CF_Emp_BaseInfo> App = db.CF_Emp_BaseInfo.Where(t => t.FBaseInfoID == fbid && t.FPersonTypeId == ftype).OrderByDescending(t => t.FCreateTime);
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
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
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('AddEmpInfo.aspx?fid=" + fid + "&IsView=1&FBaseInfoId=" + Request.QueryString["fbid"] + "',600,400);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowEmpInfo();
    }
}
