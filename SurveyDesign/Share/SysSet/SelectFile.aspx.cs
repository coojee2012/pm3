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
using Approve.EntitySys;

public partial class Admin_lnMian_SelectFile : System.Web.UI.Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return comfirm('确定删除所选项吗？');");
            showinfoList();
        }
    }
    private void showinfoList()//显示列表
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fname,forder,fid,fnumber  from CF_Sys_dic ");
        sb.Append(" where fparentid='260888' ");
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            sb.Append(" and FName like'%" + t_FName.Text + "%' ");
        }
        sb.Append(" order by forder ");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 10;
        this.Pager1.controltopage = "FileInfo_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    protected void FileInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = Convert.ToString(e.Item.ItemIndex + 1);
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            e.Item.Cells[3].Text = "<a href=\"javascript:window.returnValue='" + FID + "';window.close();\">选择</a>";



        }
    }

    //查询按钮
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showinfoList();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(this.FileInfo_List, EntityTypeEnum.EsDic, "RCenter");
        showinfoList();
    }
}
