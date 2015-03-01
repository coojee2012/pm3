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


public partial class Admin_main_NewsList : Page
{
    RCenter rc = new RCenter();
    RNews rn = new RNews();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            this.btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?')");
            this.btnDel1.Attributes.Add("onclick", "return confirm('确认要删除么?')");

            ControlBind();
            ShowInfo();
            ShowPostion();
        }

    }
    private void ShowPostion()
    {
        if (Request["fcol"] != null && Request["fcol"] != "")
        {
            lPostion.Text = "新闻发布>>" + rn.getLMDH(Request["fcol"], ">>", 2);
            this.btnDel.Visible = true;
            Session["webType"] = Request.QueryString["fcol"];
        }
        else
        {
            this.btnDel.Visible = false;

        }
    }
    private void ControlBind()
    {
        string fType = Request["ftype"];
        if (fType == null || fType == "")
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" fid in (select fid from CF_Sys_Tree where  fisdeleted=0 and ftype=" + fType + ") ");

        DataTable dt = rc.GetTable(EntityTypeEnum.EsTree, "FName,FNumber,forder,flevel,fparent", sb.ToString());

        DataRow[] rows = dt.Select("flevel=1", "forder");
        for (int i = 0; i < rows.Length; i++)
        {
            this.drop_FCol.Items.Add(new ListItem(rows[i]["FName"].ToString(), rows[i]["FNumber"].ToString()));
            DataRow[] sRows = dt.Select("flevel=2 and fparent='" + rows[i]["FNumber"].ToString() + "'", "forder");
            for (int j = 0; j < sRows.Length; j++)
            {
                this.drop_FCol.Items.Add(new ListItem("+--" + sRows[j]["FName"].ToString(), sRows[j]["FNumber"].ToString()));
                DataRow[] sSRows = dt.Select("flevel=3 and fparent='" + sRows[j]["FNumber"].ToString() + "'", "forder");
                for (int k = 0; k < sSRows.Length; k++)
                {
                    this.drop_FCol.Items.Add(new ListItem("+----" + sSRows[k]["FName"].ToString(), sSRows[k]["FNumber"].ToString()));

                    DataRow[] sSSRows = dt.Select("flevel=4 and fparent='" + sSRows[k]["FNumber"] + "'", "forder");
                    for (int l = 0; l < sSSRows.Length; l++)
                    {
                        this.drop_FCol.Items.Add(new ListItem("+------" + sSSRows[l]["FName"].ToString(), sSSRows[l]["FNumber"].ToString()));
                    }
                }
            }
        }
        this.drop_FCol.Items.Insert(0, new ListItem("请选择", ""));
    }
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.text_FName.Text != "")
        {
            sb.Append(" and fname like '%");
            sb.Append(this.text_FName.Text + "%' ");
        }
        if (Request["fcol"] != null && Request["fcol"] != "")
        {
            this.drop_FCol.SelectedIndex = this.drop_FCol.Items.IndexOf(this.drop_FCol.Items.FindByValue(Request["fcol"]));

            //this.drop_FCol.Enabled = false;
        }
        if (this.drop_FCol.SelectedValue != "")
        {
            sb.Append(" and fid in (");
            sb.Append("select fnewsid from cf_news_col where FColNumber='" + this.drop_FCol.SelectedValue + "' ) ");
        }   
        return sb.ToString();

    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fid,FName,FCount,FPubTime,FOrder,FState from CF_News_Title where 1=1");
        sb.Append(GetCon());
        sb.Append(" order by forder,fpubtime desc");
        this.Pager1.sql = sb.ToString();
        this.Pager1.className = "RCenter";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "News_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    protected void News_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fState = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            CheckBox IsPub = (CheckBox)e.Item.Cells[e.Item.Cells.Count - 7].Controls[1];
            if (fState == "1")
            {
                IsPub.Checked = true;
            }
            else
            {
                IsPub.Checked = false;
            }
            e.Item.Cells[5].Text = rc.GetNewsCol(fid);

            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('NewsAdd.aspx?fid=" + fid + "',896,856);\">" + e.Item.Cells[2].Text + "</a>";
            e.Item.Cells[e.Item.Cells.Count - 4].Text = "<a href=\"javascript:showAddWindow('newPubTree.aspx?fnewsid=" + fid + "&ftype=" + Request["ftype"] + "',250,600);\">发布栏目</a>";
            e.Item.Cells[e.Item.Cells.Count - 5].Text = "<a href=\"javascript:showAddWindow('newPubTree.aspx?fnewsid=" + fid + "&ftype=" + Request["ftype"] + "',250,600);\">查看评论</a>";
        }
    }
    protected void News_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            pageTool tool = new pageTool(this.Page);
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fSate = "0";
            string fOrder = "0";
            if (e.CommandName == "Save")
            {
                CheckBox IsPub = (CheckBox)e.Item.Cells[e.Item.Cells.Count - 7].Controls[1];
                TextBox Forder = (TextBox)e.Item.Cells[e.Item.Cells.Count - 6].Controls[1];
                if (IsPub.Checked == true)
                {
                    fSate = "1";
                }
                if (Forder.Text != "")
                {
                    fOrder = Forder.Text;
                }
            }
            if (rc.PExcute(" update CF_News_Title set fstate=" + fSate + ",forder=" + fOrder + " where fid='" + fid + "'"))
            {
                if (this.drop_FCol.SelectedValue != "")
                {
                    rc.PExcute(" update CF_News_Col set fstate=" + fSate + ",forder=" + fOrder + " where fnewsid='" + fid + "' and fcolnumber='" + drop_FCol.SelectedValue + "'");
                }
                tool.showMessage("保存成功");
                ShowInfo();
            }
            else
            {
                tool.showMessage("保存失败");
            }
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (Request["fcol"] != null && Request["fcol"] != "")
        {
            StringBuilder sb = new StringBuilder();
            int iCount = this.News_List.Items.Count;
            for (int i = 0; i < iCount; i++)
            {
                CheckBox box = (CheckBox)this.News_List.Items[i].Cells[0].Controls[1];
                string fId = this.News_List.Items[i].Cells[this.News_List.Columns.Count - 1].Text;
                if (box.Checked == true)
                {
                    sb.Append(" delete from cf_news_col ");
                    sb.Append(" where fcolnumber='" + Request["fcol"] + "'");
                    sb.Append(" and fnewsid='" + fId + "'");
                }
            }
            if (sb.Length > 0)
            {
                rc.PExcute(sb.ToString());
            }
        }

        ShowInfo();
    }
    protected void btnDel1_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(this.News_List, EntityTypeEnum.EnTitle, "RCenter", "DelNews");
        ShowInfo();
    }
}
