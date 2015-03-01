using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Approve.Common;
using Approve.RuleCenter;

public partial class Government_AppMain_queryGFZS : System.Web.UI.Page
{
    RCenter rc = new RCenter(); SaveAsBase sab = new SaveAsBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindInfo();
        }
    }
    public void bindInfo()
    {
        string sql = string.Format(@"select *,case isEnd when 10 then '未打印' when -1 then '已打印' else '' end dyzt
                        from JKC_V_GFend where isnull(isEnd,0) in (10,-1)");
        if (!string.IsNullOrEmpty(txtFName.Text.Trim()))
        { sql += " and FName like '%" + txtFName.Text.Trim() + "%'"; }
        if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
        { sql += " and GFMC like '%" + txtFPrjName.Text.Trim() + "%'"; }
        if (!string.IsNullOrEmpty(t_FListName.SelectedValue.Trim()))
        {
            sql += " and FListName = '" + t_FListName.SelectedValue.Trim() + "'";

            if (t_FListName.SelectedValue == "其他")
            { sql += " and FTypeName like '%" + t_FTypeName1.Text.Trim() + "%'"; }
            else if (!string.IsNullOrEmpty(t_FTypeName.SelectedValue.Trim()))
            {
                sql += " and FTypeName = '" + t_FTypeName.SelectedValue.Trim() + "'";
            }
        }
        if (!string.IsNullOrEmpty(t_FNu.Text.Trim()))
        { sql += " and FNu like '%" + t_FNu.Text.Trim() + "%' "; }
        if (!string.IsNullOrEmpty(t_Fwh.Text.Trim()))
        { sql += " and Fwh like '%" + t_Fwh.Text.Trim() + "%' "; }
        if (!string.IsNullOrEmpty(t_isEnd.SelectedValue.Trim()))
        { sql += " and isEnd = " + t_isEnd.SelectedValue.Trim() + " "; }
        this.Pager1.sql = sql;
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string isend = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "isEnd"));

            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "YWBM"));
            string url = "gfzsEdit.aspx?fid=" + fid + "&isend=" + isend;
            e.Item.Cells[4].Text = "<a href='javascript:void(0)' onclick=\"javascript:showAddWindow('" + url + "',800,600);\" >" + e.Item.Cells[4].Text + "</a>";
        }
    }

    protected void t_FListName_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindTypeName();
    }
    public void bindTypeName()
    {
        if (!string.IsNullOrEmpty(t_FListName.SelectedValue) && t_FListName.SelectedValue != "--请选择--")
        {
            if (t_FListName.SelectedValue == "房屋建筑工程")
            {
                t_FTypeName.Visible = true; t_FTypeName1.Visible = false;
                this.t_FTypeName.Items.Clear();
                this.t_FTypeName.Items.Insert(0, new ListItem("--请选择--", ""));
                this.t_FTypeName.Items.Insert(1, new ListItem("地基与基础", "地基与基础"));
                this.t_FTypeName.Items.Insert(2, new ListItem("主体结构", "主体结构"));
                this.t_FTypeName.Items.Insert(3, new ListItem("钢结构", "钢结构"));
                this.t_FTypeName.Items.Insert(4, new ListItem("装饰与屋面", "装饰与屋面"));
                this.t_FTypeName.Items.Insert(5, new ListItem("水电与智能", "水电与智能"));
                this.t_FTypeName.Items.Insert(6, new ListItem("其他", "其他"));
            }
            else if (t_FListName.SelectedValue == "土木工程")
            {
                t_FTypeName.Visible = true; t_FTypeName1.Visible = false;
                this.t_FTypeName.Items.Clear();
                this.t_FTypeName.Items.Insert(0, new ListItem("--请选择--", ""));
                this.t_FTypeName.Items.Insert(1, new ListItem("公路", "公路"));
                this.t_FTypeName.Items.Insert(2, new ListItem("铁路", "铁路"));
                this.t_FTypeName.Items.Insert(3, new ListItem("隧道", "隧道"));
                this.t_FTypeName.Items.Insert(4, new ListItem("桥梁", "桥梁"));
                this.t_FTypeName.Items.Insert(5, new ListItem("堤坝与电站", "堤坝与电站"));
                this.t_FTypeName.Items.Insert(6, new ListItem("矿山", "矿山"));
                this.t_FTypeName.Items.Insert(7, new ListItem("其他", "其他"));
            }
            else if (t_FListName.SelectedValue == "工业安装工程")
            {
                t_FTypeName.Visible = true; t_FTypeName1.Visible = false;
                this.t_FTypeName.Items.Clear();
                this.t_FTypeName.Items.Insert(0, new ListItem("--请选择--", ""));
                this.t_FTypeName.Items.Insert(1, new ListItem("工业设备", "工业设备"));
                this.t_FTypeName.Items.Insert(2, new ListItem("工业管道", "工业管道"));
                this.t_FTypeName.Items.Insert(3, new ListItem("电气装置与自动化", "电气装置与自动化"));
                this.t_FTypeName.Items.Insert(4, new ListItem("其他", "其他"));
            }
            else if (t_FListName.SelectedValue == "其他")
            {
                this.t_FTypeName.Visible = false; t_FTypeName1.Visible = true;
                t_FTypeName1.Text = null;
            }
        }
        else { t_FTypeName1.Text = null; t_FTypeName.Items.Clear(); }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bindInfo();
    }
    protected void btnOut_Click(object sender, EventArgs e)
    {
        string sql = this.Pager1.sql.ToString();
        DataTable dt = rc.GetTable(sql);
        if (dt != null)
        {
            this.JustAppInfo_List.DataSource = dt;
            this.JustAppInfo_List.DataBind();
            JustAppInfo_List.Columns[0].Visible = false;
            string fOutTitle = lPostion.Text;
            sab.SaveAsExc(this.JustAppInfo_List, fOutTitle, this.Response);
        }
    }
    protected void JustAppInfo_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FLinkId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            if (e.CommandName == "See")
            {
                this.Session["FAppId"] = FLinkId;
                this.Session["FManageTypeId"] = "4000";
                Session["FIsApprove"] = 1;
                Response.Write("<script language='javascript'>window.open('../../GFEnt/AppMain/aIndex.aspx');</script>");
            }
        }
    }
}