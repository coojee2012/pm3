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

public partial class OA_Bulletin_SBCLXX : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    RNews rn = new RNews();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnDel1.Attributes.Add("onclick", "return confirm('确认要删除么?')");
            //ControlBind(); 
            ShowPostion();
            ShowInfo();
        }


    }
    private void ShowPostion()
    {
        if (!String.IsNullOrEmpty(Request.QueryString["fcol"]))
        {
            switch (Request.QueryString["fcol"])
            {
                case "600":
                    lPostion.Text = "企业文件通知";
                    break;
                case "800":
                    lPostion.Text = "企业通知公告";
                    break;
                default:
                    lPostion.Text = "文件通知";
                    break;
            }
            ViewState["webType"] = Request.QueryString["fcol"];
        }
    }
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.text_FName.Text != "")
        {
            sb.Append(" and fname like '%");
            sb.Append(this.text_FName.Text + "%' ");
        }
        sb.Append(" and FType='" + ViewState["webType"] + "' ");
        return sb.ToString();

    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        SortedList sl = new SortedList();
        sb.Append("select fid,FName,FCount,FPubTime,FOrder,FState,FCreateTime from CF_News_Title");
        sb.Append(" where 1=1 ");
        sb.Append(GetCon());
        sb.Append(" order by forder,ftime desc");
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

            e.Item.Cells[2].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + e.Item.Cells[2].Text;
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

            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showAddWindow('NewsAdd2.aspx?fid=" + fid + "&fcol=" + ViewState["webType"] + "',900,600);\">" + e.Item.Cells[2].Text + "</a>";
            e.Item.Cells[e.Item.Cells.Count - 4].Text = "<a href='#' class='link3' onclick=\"showApproveWindow('newPubTree.aspx?fnewsid=" + fid + "&ftype=" + Request["ftype"] + "',250,600);\">发布栏目</a>";
            e.Item.Cells[e.Item.Cells.Count - 5].Text = "<a href='#' class='link3' onclick=\"showApproveWindow('newPubTree.aspx?fnewsid=" + fid + "&ftype=" + Request["ftype"] + "',250,600);\">查看评论</a>";
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
            if (rc.PExcute(" update CF_News_Title set fstate=" + fSate + ",forder=" + fOrder + ",ftime=getdate(),FIsPub='" + fSate + "',fpubtime=getdate() where fid='" + fid + "'"))
            {
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
        tool.DelInfoFromGrid(this.News_List, EntityTypeEnum.EnTitle, "RCenter");
        ShowInfo();
    }
}
