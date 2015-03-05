using Approve.Common;
using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class JSDW_ApplyJGYS_ProjectFile_ZTBList : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfId.Value = Id;
            ShowInfo();
            EnabledControl();
        }
    }
    private void EnabledControl()
    {
        if (FIsApprove == "1" || Audit == "1") //审核页面跳转
        {
            foreach (Control control in this.form1.Controls)
            {
                WebHelper.SetControlEnabled(control);
            }
        }
    }
    private void ShowInfo()
    {
        string sql = @"select * from (select ID DWID,ZBLXMC UnitType,ZBTZSBH,ZTBID from XM_JLDWZTB) a left join XM_ZTB b
                        on a.ZTBID=b.ID where b.YWBM='" + Id + "'";
        sql += " order by CreateTime desc ";
        this.Pager1.className = "dbCenter";
        this.Pager1.sql = sql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();

        sql = string.Format("select top 1* from XM_JGYS_TRANS WHERE YWBM='{0}' and TypeId=3;", Id);
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            hfIsExists.Value = "1";
            DataRow row = dt.Rows[0];
            ddlIsTrans.SelectedValue = row["IsTrans"].ToString();
            txtLY.Text = row["LY"].ToString();
        }
        else
            hfIsExists.Value = "0";
    }
    private string Id
    {
        get
        {
            return Request.QueryString["JG_Id"];
        }
    }
    private string FIsApprove
    {
        get
        {
            string value = Session["FIsApprove"] == null ? "" : Session["FIsApprove"].ToString();
            if (string.IsNullOrEmpty(value))
                return "0";
            return value;
        }
    }
    private string Audit
    {
        get
        {
            return Request.QueryString["audit"];
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            if (FIsApprove == "1" || Audit == "1")
            {
                string perSonId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "DWID"));
                string Id = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ID"));
                e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='#' ZTBID='" + Id + "' onclick=\"Show('" + perSonId + "',this)\">查 看</a>";
            }
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            pageTool tool = new pageTool(this.Page, "txt");
            string ZTBId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string sql = string.Empty;// string.Format(@"delete from XM_ZTB where Id='{0}';", ZTBId);
            sql += string.Format(@"delete from XM_JLDWZTB where ID='{0}'",ZTBId);
            //sql += string.Format(@"delete from XM_SGDWZTB where ID='{0}'",ZTBId);
            bool success = rc.PExcute(sql,true);
            if (success)
            {
                tool.showMessage("删除成功");
                ShowInfo();
            }
            else
                tool.showMessage("删除失败");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"INSERT INTO [dbo].[XM_JGYS_TRANS]
                       ([ID]
                       ,[YWBM]
                       ,[TypeId]
                       ,[IsTrans]
                       ,[LY])
                        values(NEWID(),'{0}','{1}','{2}','{3}');", Id, 3, ddlIsTrans.SelectedValue, txtLY.Text.Replace("'",string.Empty));
        if (hfIsExists.Value == "1") //编辑
        {
            sql = string.Format(@"UPDATE [dbo].[XM_JGYS_TRANS]
                                       SET [IsTrans] = {0}
                                          ,[LY] = '{1}'
                                     WHERE YWBM='{2}' and TypeId=3;", ddlIsTrans.SelectedValue, txtLY.Text.Replace("'", string.Empty), Id);
        }
        bool success = rc.PExcute(sql);
        pageTool tool = new pageTool(this.Page);
        if (success)
        {
            tool.showMessage("保存成功");
        }
        else
            tool.showMessage("保存失败");
        
    }
}