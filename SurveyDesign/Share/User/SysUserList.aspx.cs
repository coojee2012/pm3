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
using Approve.RuleBase;
public partial class Share_User_ManUserList : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            ShowInfo();
        }
    }

    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("select * from CF_Sys_MenuRole ");
        sb.Append("where FType=1 and fisdeleted=0  order by fnumber ");
        DataTable dt = sh.GetTable(sb.ToString());

        t_FMenuRoleId.DataSource = dt;
        t_FMenuRoleId.DataTextField = "FName";
        t_FMenuRoleId.DataValueField = "FNumber";
        t_FMenuRoleId.DataBind();
        t_FMenuRoleId.Items.Insert(0, new ListItem("请选择", ""));

        Govdept1.fNumber = ComFunction.GetDefaultCityDept();
        Govdept1.Dis(1);
    }


    //条件
    private string GetCon()
    {
        string fDeptNumber = this.Govdept1.FNumber;
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FEntName.Text))
        {
            sb.Append(" and fName like '%" + t_FEntName.Text + "%'");
        }
        if (fDeptNumber != null && fDeptNumber != "")
        {
            sb.Append(" and FManageDeptId like '" + fDeptNumber + "%'");
        }
        if (!string.IsNullOrEmpty(t_FLockNumber.Text))
        {
            sb.Append(" and FLockNumber like '%" + t_FLockNumber.Text + "%'");
        }
        if (!string.IsNullOrEmpty(t_FMenuRoleId.SelectedValue))
        {
            sb.Append(" and FMenuRoleId ='" + t_FMenuRoleId.SelectedValue + "'");
        }

        return sb.ToString();
    }
    private void ShowInfo()
    {

        StringBuilder sb = new StringBuilder();
        sb.Append(" select FId,FCompany,FName,FLockNumber,FMenuRoleId,FTel,ftype,FManageDeptId ");
        sb.Append(" From cf_sys_user ");
        sb.Append(" where FIsDeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" And FType=6  ");//ftype=6：系统管理员用户
        sb.Append(" Order By  Fcreatetime Desc");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();

    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FManageDeptId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageDeptId"));
            string FMenuRoleId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FMenuRoleId"));

            //角色
            e.Item.Cells[4].Text = sh.GetSignValue("select FName from CF_Sys_MenuRole where FNumber='" + FMenuRoleId + "'");
            //主管部门
            e.Item.Cells[5].Text = sh.getDept(FManageDeptId, 1);

            e.Item.Attributes.Add("ondblclick", "showAddWindow('SysUserAdd.aspx?FID=" + FID + "&fmatypeid=" + Request.QueryString["fmatypeid"] + "' ,700,500);");
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        sl.Add("CF_Sys_User", "FID");

        tool.DelInfoFromGrid(this.DG_List, sl, "dbShare");

        ShowInfo();
    }




}
