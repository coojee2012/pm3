using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Approve.Common;
using System.Text;
using Approve.EntityBase;
using System.Data;


public partial class Admin_User_LxrSysType : System.Web.UI.Page
{

    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");

            ShowInfo();
        }
    }
    //显示数据
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select  FID,FLinkName FLinkName,FSystemId,FType ");
        sb.Append("from CF_City_Link  ");
        sb.Append("Where isnull(FIsDeleted,0)=0 ");
        sb.Append(GetCon());
        sb.Append("  order by  FOrder ");
        this.Pager1.className = "RCenter";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 10;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }

    //获取查询条件
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtFName.Text != "")
        {
            sb.Append(" and FLinkName like '");
            sb.Append(this.txtFName.Text + "%'");
        }
        return sb.ToString();
    }

    //搜索的单击事件
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    //单击“联系人”或“修改”，模式显示出新页面，进行修改
    protected void Dic_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FType"));
            string FSystemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSystemId"));

            e.Item.Cells[2].Text = "<a href='#' onclick=\"showAddWindow('LxrSysTypeAdd.aspx?fid=" + FID + "',400,400);\">" + e.Item.Cells[2].Text + "</a>";
            e.Item.Cells[3].Text = rc.GetDicName(FType);


            if (!string.IsNullOrEmpty(FSystemId))
            {
                string str = "";
                DataTable dt = rc.GetTable("select FName from CF_Sys_SystemName where FNumber in(" + FSystemId + ") ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        str += "<br/>";
                    }
                    str += dt.Rows[i]["FName"].ToString();
                }
                e.Item.Cells[4].Text = str;

            }


        }
    }


    //单击“删除”按钮事件
    protected void btnDel_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < DG_List.Items.Count; i++)
        {
            if (((CheckBox)DG_List.Items[i].Cells[0].FindControl("CheckItem")).Checked == true)
            {
                string strFid = DG_List.Items[i].Cells[DG_List.Items[i].Cells.Count - 1].Text;

                sb.Append("delete  CF_City_Link  where Fid = '" + strFid + "'");

            }
        }
        if (sb.ToString() == "")
        {
            pageTool tool = new pageTool();
            tool.showMessage("请选择删除的项");
        }
        else
        {
            rc.PExcute(sb.ToString());
            ShowInfo();
        }

    }

    //单击“刷新”事件
    public void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

}
