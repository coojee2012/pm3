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
using Approve.EntityBase;
using Approve.Common;

public partial class Admin_mainother_SmsList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确定删除所选项吗？');");
            showInfo();
        }
    }

    //条件
    private string getCon()
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FState.SelectedValue))
        {
            sb.Append(" and  FState ='" + t_FState.SelectedValue + "' ");
        }
        if (!string.IsNullOrEmpty(t_FSubmitTime1.Text))
        {
            sb.Append(" and FSubmitTime>='" + t_FSubmitTime1.Text + "' ");
        }
        if (!string.IsNullOrEmpty(t_FSubmitTime2.Text))
        {
            sb.Append(" and FSubmitTime<='" + t_FSubmitTime2.Text + "' ");
        }

        return sb.ToString();
    }


    //显示
    private void showInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_OA_SmsList where FIsDeleted=0 ");
        sb.Append(getCon());
        sb.Append("order by FSubmitTime desc ");

        this.Pager1.className = "RCenter";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "Dic_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();

    }

    //列表
    protected void Dic_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            string FContent = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FContent"));

            //内容
            e.Item.Cells[3].Text = "<a href=\"javascript:showApproveWindow('SmsEdit.aspx?FID=" + FID + "',600,350);\">" + FContent + "</a>";


            //状态
            string str = "";
            switch (FState)
            {
                case "0":
                    str = "未发送";
                    break;
                case "1":
                    str = "<font color='green'>已发送</font>";
                    break;
                case "2":
                    str = "<font color='red'>发送失败</font>";
                    break;
                default:
                    str = "未发送";
                    break;
            }
            e.Item.Cells[4].Text = str;
        }
    }


    #region 按钮

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        sl.Add("CF_OA_SmsList", "FID");
        tool.DelInfoFromGrid(Dic_List, sl, "");
        showInfo();
    }

    #endregion

}
