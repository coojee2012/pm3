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
using Approve.EntityBase;
using Approve.RuleCenter;
using Approve.Common;
using System.Text;

public partial class Admin_main_LeavelWordList : System.Web.UI.Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?')");
            ControlBind();
            ShowInfo();
            this.dbState.SelectedValue = "0";
        }
    }
    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fname,fnumber from cf_sys_systemName where fisdeleted=0 order by fnumber ");
        DataTable dt = rc.GetTable(sb.ToString());
        this.dbFSystemId.DataSource = dt;
        this.dbFSystemId.DataTextField = "Fname";
        this.dbFSystemId.DataValueField = "Fnumber";
        this.dbFSystemId.DataBind();
        this.dbFSystemId.Items.Insert(0, new ListItem("请选择", ""));
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select FId,FTitle,FLevelPerson,FRevertPerson,FRevertDate,FOrder,FISpub,");
        sb.Append(" case fstate when 0 then '未回复' when 1 then '已回复' end as FState,");
        sb.Append("FCreateTime from CF_LevelWord_Info where fisdeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" order by forder,fcreatetime desc ");

        this.Pager1.controltopage = "LeavelWord_List";
        this.Pager1.pagecount = 20;
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.dataBind();
    }
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtQueryText.Text != "")
        {
            if (this.rb1.Checked == true)
            {
                sb.Append(" and FLevelPerson like '%");
            }
            if (this.rb2.Checked == true)
            {
                sb.Append(" and FTitle like '%");
            }
            if (this.rb3.Checked == true)
            {
                sb.Append(" and FRevertPerson like '%");
            }
            sb.Append(this.txtQueryText.Text + "%' ");
        }
        if (this.dbFSystemId.SelectedValue != "")
        {
            sb.Append(" and fSystemid=");
            sb.Append(this.dbFSystemId.SelectedValue + " ");
        }
        if (this.dbState.SelectedValue != "")
        {
            sb.Append(" and fstate = ");
            sb.Append(this.dbState.SelectedValue + " ");
        }
        return sb.ToString();
    }
    protected void LeavelWord_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + (this.Pager1.curpage - 1) * this.Pager1.pagecount).ToString();
            e.Item.Cells[4].Text = "<a href='#' class='link1' onclick=\"showAddWindow('Revert.aspx?fid=" + fid + "',677,850);\">" + e.Item.Cells[4].Text + "</a>";

            string fIsPub = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            CheckBox box = (CheckBox)e.Item.Cells[e.Item.Cells.Count - 4].Controls[1];
            if (fIsPub == "1")
            {
                box.Checked = true;
            }
        }
    }
    protected void LeavelWord_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            if (e.CommandName == "Save")
            {
                pageTool tool = new pageTool(this.Page);
                string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
                TextBox box = (TextBox)e.Item.Cells[8].Controls[1];

                string fIsPub = "0";
                CheckBox cbox = (CheckBox)e.Item.Cells[e.Item.Cells.Count - 4].Controls[1];

                if (cbox.Checked == true)
                {
                    fIsPub = "1";
                }

                StringBuilder sb = new StringBuilder();
                if (box.Text == "")
                {
                    box.Text = "0";
                }
                sb.Append("update CF_LevelWord_Info set FOrder =" + box.Text + ",FIsPub=" + fIsPub);
                sb.Append(" where fid='" + fid + "'");
                if (rc.PExcute(sb.ToString()))
                {
                    tool.showMessage("保存成功");
                    ShowInfo();
                }
            }
        }
    }
    protected void btnDel_Click1(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(this.LeavelWord_List, EntityTypeEnum.ElInfo, "dbShare");
        ShowInfo();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int iCount = this.LeavelWord_List.Items.Count;
        SortedList[] sl = new SortedList[iCount];
        EntityTypeEnum[] en = new EntityTypeEnum[iCount];
        string[] FKey = new string[iCount];
        SaveOptionEnum[] so = new SaveOptionEnum[iCount];
        for (int i = 0; i < iCount; i++)
        {
            en[i] = EntityTypeEnum.ElInfo;
            FKey[i] = "FID";
            so[i] = SaveOptionEnum.Update;
            sl[i] = new SortedList();
            string fid = this.LeavelWord_List.Items[i].Cells[this.LeavelWord_List.Columns.Count - 1].Text;
            TextBox box = (TextBox)LeavelWord_List.Items[i].Cells[8].Controls[1];

            string fIsPub = "0";
            CheckBox cbox = (CheckBox)LeavelWord_List.Items[i].Cells[LeavelWord_List.Columns.Count - 4].Controls[1];

            if (cbox.Checked == true)
            {
                fIsPub = "1";
            }



            sl[i].Add("FID", fid);
            sl[i].Add("FOrder", box.Text);
            sl[i].Add("FIsPub", fIsPub);

        }
        pageTool tool = new pageTool(this.Page);
        if (rc.SaveEBaseM(en, sl, FKey, so))
        {
            tool.showMessage("保存成功");
            ShowInfo();
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void LeavelWord_List_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
