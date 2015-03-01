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

public partial class Share_User_RegUserSelect : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
            ShowInfo();
        }
    }

    private void ControlBind()
    {
        DataTable dt = sh.getSystemEntTable(EConvert.ToString(Session["SH_UserId"]));
        t_FSystemId.DataSource = dt;
        t_FSystemId.DataTextField = "FName";
        t_FSystemId.DataValueField = "FNumber";
        t_FSystemId.DataBind();
        t_FSystemId.Items.Insert(0, new ListItem("请选择", ""));

        string FUserId = Request.QueryString["FUserId"];
        t_FCompany.Text = sh.GetSignValue(EntityTypeEnum.EsUser, "FCompany", "FID='" + FUserId + "'");
    }

    //条件
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FCompany.Text.Trim()))
        {
            sb.Append(" and FCompany like '%" + t_FCompany.Text.Trim() + "%'");
        }
        if (!string.IsNullOrEmpty(t_FSystemId.SelectedValue))
        {
            sb.Append(" and FSystemId = '" + t_FSystemId.SelectedValue + "'");
        }
        if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
        {
            sb.Append(" and FName like  '%" + t_FName.Text.Trim() + "%'");
        }
        string SystemEntList = sh.getSystemEntList(EConvert.ToString(Session["SH_UserId"]));
        if (!string.IsNullOrEmpty(SystemEntList))
            sb.Append("and FSystemId in(" + SystemEntList + ")");
        else
            sb.Append("and 1=2 ");

        return sb.ToString();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * from CF_User_Reg where fisdeleted=0 and fstate=0 ");//fstate=0：未发锁的
        sb.Append(GetCon());
        sb.Append(" Order By FCreateTime ");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 10;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }




    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FSystemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSystemId"));
            string SystemName = sh.getSystemName(FSystemId);
            string FUserId = Request.QueryString["FUserId"];

            Button btnSelect = (Button)e.Item.FindControl("btnSelect");
            if (btnSelect != null)
            {
                DataTable dt = sh.GetTable(EntityTypeEnum.EsUserRight, "FID", "FUserId='" + FUserId + "' and FSystemId='" + FSystemId + "'");
                if (dt != null && dt.Rows.Count > 0)
                {
                    btnSelect.Attributes.Add("onclick", "alert('该企业已存在\"" + SystemName + "\"系统权限。'); return false;");
                }
            }

            e.Item.Cells[3].Text = SystemName;
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

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

}
