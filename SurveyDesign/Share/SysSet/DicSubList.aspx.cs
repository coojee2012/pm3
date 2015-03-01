using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;

public partial class Share_SysSet_DicSubList : Page
{

    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnDel.Attributes.Add("onclick","return confirm('确认要删除么?');");
            ControlBind();
            if (Request["fparentid"] != null && Request["fparentid"] != "")
            {
                string fparentNumber = sh.GetSignValue(EntityTypeEnum.EsDicClass, "FNumber", "fid='" + Request["fparentid"] + "'");
                if (fparentNumber == null || fparentNumber == "")
                {
                    fparentNumber = sh.GetSignValue(EntityTypeEnum.EsDic, "FNumber", "fid='" + Request["fparentid"] + "'");
                }
                if (fparentNumber == null || fparentNumber == "")
                {
                    return;
                }
                ShowInfo();
                this.btnAdd.Attributes.Add("onclick", "showAddWindow('DicSubAdd.aspx?fparentid=" + fparentNumber + "',378,440,null)");
                
               
            }
        }

    }

    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fid,Fname,fnumber from CF_Sys_SystemName where fisdeleted=0 order by forder");
        DataTable dt = sh.GetTable(sb.ToString());
        this.drop_FSystemId.DataSource = dt;
        this.drop_FSystemId.DataTextField = "FName";
        this.drop_FSystemId.DataValueField = "FNumber";
        this.drop_FSystemId.DataBind();
        this.drop_FSystemId.Items.Insert(0, new ListItem("请选择", ""));
    }
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.text_FName.Text != "")
        {
            sb.Append(" and fname like '%");
            sb.Append(this.text_FName.Text + "%'");
        }
        if (this.text_FNumber.Text != "")
        {
            sb.Append(" and fnumber ='");
            sb.Append(this.text_FNumber.Text + "'");
        }
        if (this.drop_FSystemId.SelectedValue != "")
        {
            sb.Append(" and fsystemid='");
            sb.Append(this.drop_FSystemId.SelectedValue + "'");
        }
        return sb.ToString();
    }
    private void ShowInfo()
    {
        string fparentNumber = sh.GetSignValue(EntityTypeEnum.EsDicClass, "FNumber", "fid='" + Request["fparentid"] + "'");
        if (fparentNumber == null || fparentNumber == "")
        {
            fparentNumber = sh.GetSignValue(EntityTypeEnum.EsDic, "FNumber", "fid='" + Request["fparentid"] + "'");
        }
        if (fparentNumber == null || fparentNumber == "")
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
     
        sb.Append("select FId,FName,FNumber,FCNumber,FSystemId,FParentId,FOrder,FTime,");
        sb.Append("(select top 1 fname from CF_Sys_SystemName where fnumber=fsystemid) FSystemName ");
        //sb.Append("(select top 1 fname from cf_sys_dicclass where fnumber=fparentid) FParentName ");
        sb.Append("From CF_Sys_Dic ");
        sb.Append("Where FIsDeleted=0 ");
        sb.Append(" and fparentid='");
        sb.Append(fparentNumber + "'");
        sb.Append(GetCon());
        sb.Append(" Order By Forder,FTime Desc ");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "Dic_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    private void SaveAllInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        int itemCount = this.Dic_List.Items.Count;
        for (int i = 0; i < itemCount; i++)
        {
            string fid = this.Dic_List.Items[i].Cells[this.Dic_List.Columns.Count - 1].Text;
            TextBox Forder = (TextBox)Dic_List.Items[i].Cells[Dic_List.Columns.Count - 4].Controls[1];
            if (Forder.Text == "")
            {
                this.Dic_List.Items[i].BackColor = System.Drawing.Color.FromArgb(248, 250, 73);
                Forder.Text = "0";
            }
            sb.Append(" update CF_Sys_Dic set forder=" + Forder.Text + " where fid='" + fid + "' ");
        }
        if (sh.PExcute(sb.ToString()))
        {
            tool.showMessage("保存成功");
            ShowInfo();
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }
    protected void Dic_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string fparnetNumber = sh.GetSignValue(EntityTypeEnum.EsDicClass, "FNumber", "fid='" + Request["fparentid"] + "'");
            if (fparnetNumber == null || fparnetNumber == "")
            {
                fparnetNumber = sh.GetSignValue(EntityTypeEnum.EsDic, "FNumber", "fid='" + Request["fparentid"] + "'");
            }
            e.Item.Cells[3].Text = "<a href='#' class='link3' onclick=\"showAddWindow('DicSubAdd.aspx?fid=" + fid + "&fparentid=" + fparnetNumber + "',378,440,null);\">" + e.Item.Cells[3].Text + "</a>";

          
        }
    }
    protected void Dic_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            TextBox Forder = (TextBox)e.Item.Cells[e.Item.Cells.Count - 4].Controls[1];
            if (e.CommandName == "Save")
            {
                pageTool tool = new pageTool(this.Page);
                if (sh.PExcute(" update CF_Sys_Dic set forder=" + Forder.Text + " where fid='" + fid + "' "))
                {
                    tool.showMessage("保存成功");
                    this.Pager1.dataBind();
                }
                else
                {
                    tool.showMessage("保存失败");
                }
            }
            if (e.CommandName == "Add")
            {
                this.Response.Redirect("DicSubList.aspx?fparentid=" + fid);
            }
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        string fparentNumber = sh.GetSignValue(EntityTypeEnum.EsDic,"FParentid","fid='"+Request["fparentid"]+"'");
        if (fparentNumber == null || fparentNumber == "")
        {
            this.Response.Redirect("DicList.aspx"); ;
        }
        string fparentId = sh.GetSignValue(EntityTypeEnum.EsDic, "fid", "fnumber='" + fparentNumber + "'");
        if (fparentId == null || fparentId == "")
        {
            fparentId = sh.GetSignValue(EntityTypeEnum.EsDicClass, "fid", "fnumber='" + fparentNumber + "'");  
        }
        this.Response.Redirect("DicSubList.aspx?fparentid=" + fparentId);
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(this.Dic_List, EntityTypeEnum.EsDic, "dbShare", "DelDic");
        ShowInfo();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveAllInfo();
    }
}

