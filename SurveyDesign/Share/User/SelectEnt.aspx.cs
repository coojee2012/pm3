using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using Approve.EntityBase;
using ProjectData;

public partial class Share_User_SelectEnt : System.Web.UI.Page
{
    Share sh = new Share();
    ShareTool st = new ShareTool();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

           // ShowInfo();
        }
    }



    //条件
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FCompany.Text.Trim()))
        {
            sb.Append(" and FCompany like '%" + t_FCompany.Text.Trim() + "%'");
        }
        //if (!string.IsNullOrEmpty(t_FSystemId.SelectedValue))
        //{
        //    sb.Append(" and FSystemId = '" + t_FSystemId.SelectedValue + "'");
        //}
        //if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
        //{
        //    sb.Append(" and FName like  '%" + t_FName.Text.Trim() + "%'");
        //}


        return sb.ToString();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();

        string rn = string.Empty;
        string  FSystemId = EConvert.ToInt(Request.QueryString["EntType"]).ToString();
        if (FSystemId == "15501")//下载155的用户
        {
            FSystemId = "1550,141";//省外入川勘察141
        }
        else if (FSystemId == "155")//设计单位
        {
            FSystemId = "155,140";//省外入川设计140
        }
        //省外入川设计施工一体化129
        string sql = "  Ftype=2 and FSystemId in (" + FSystemId + ") " + GetCon();
        string FIds = string.Join(" ", db.CF_Sys_User.Where(t => t.FType == 2).Select(t => " and FID<>'" + t.FID + "' ").ToArray());
        if (!string.IsNullOrEmpty(FIds))
        {
            // sql += FIds;
        }
        DataTable dt = ViewState["dt"] as DataTable;
        if (dt == null)
        {
            dt = st.GetTABLE(sql, "帐号表（网站）", out rn);
            ViewState["dt"] = dt;
        }
        if (dt != null && dt.Rows.Count > 0)
        {
            var App = dt.AsEnumerable();
            if (!string.IsNullOrEmpty(t_FCompany.Text.Trim()))
            {
                App = App.Where(t => EConvert.ToString(t["FCompany"]).Contains(t_FCompany.Text.Trim()));

            }
            if (App.Count() > 0)
            {
                Pager1.RecordCount = App.Count();
                DG_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize).CopyToDataTable();
                DG_List.DataBind();
            }
            else
            {
                Pager1.RecordCount = 0;
                DG_List.DataSource = App;
                DG_List.DataBind();
            }
        }

    }




    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            Button lb = e.Item.FindControl("btnSelect") as Button;
            if (lb != null)
            {
                lb.Text = "选择";
                lb.Attributes.Add("onclick", "return confirm('确认要选择该企业吗?');");
            }
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "cnSelect")
        {
            string fId = e.CommandArgument.ToString();


            this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>returnValue='" + fId + "';window.close();</script>");
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowInfo();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        //ViewState["dt"] = null;
        ShowInfo();
    }

}
