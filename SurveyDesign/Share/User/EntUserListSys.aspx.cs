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
using System.Linq;

public partial class Share_User_EntUserListSys : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lbPosition.Text = sh.GetSignValue("select fname from cf_Sys_Tree where fnumber='" + Request.QueryString["fcol"] + "'");
            ControlBind();
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            ShowInfo();
        }
    }

    private void ControlBind()
    {
        DataTable dt = sh.getDicTbByFNumber("6601");
        t_FState.DataSource = dt;
        t_FState.DataTextField = "FName";
        t_FState.DataValueField = "FNumber";
        t_FState.DataBind();
        t_FState.Items.Insert(0, new ListItem("请选择", ""));
    }

    //条件
    private string GetCon()
    {
        string fDeptNumber = this.Govdept1.FNumber;
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FEntName.Text))
        {
            sb.Append(" and u.FCompany like '%" + t_FEntName.Text + "%'");
        }
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            sb.Append(" and r.FName like '%" + t_FName.Text + "%'");
        }
        if (fDeptNumber != null && fDeptNumber != "")
        {
            sb.Append(" and u.FManageDeptId like '" + fDeptNumber + "%'");
        }
        if (!string.IsNullOrEmpty(t_FLockNumber.Text))
        {
            sb.Append(" and r.flocknumber like '%" + t_FLockNumber.Text + "%' ");
        }
        if (!string.IsNullOrEmpty(t_FState.SelectedValue))
        {
            sb.Append(" and r.FState ='" + t_FState.SelectedValue + "'");
        }

        return sb.ToString();
    }

    //显示
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select u.FId FUserId,r.FId,u.FCompany,r.fname,u.FLinkMan,u.FTel,");
        sb.Append("u.ftype,u.FManageDeptId,r.FLockNumber,r.FState ");
        sb.Append(" From cf_sys_user u inner join cf_sys_Userright r on u.fid=r.fuserId ");
        sb.Append(" where u.FIsDeleted=0 and r.FSystemId='" + Request.QueryString["fsystemid"] + "'");
        sb.Append(GetCon());
        sb.Append(" And u.FType=2  ");
        sb.Append(" Order By r.Fcreatetime Desc,r.FID ");

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
            string FUserID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FUserId"));
            string FManageDeptId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageDeptId"));
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
            string FCompany = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FCompany"));
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('EntUserRightAdd.aspx?FUserId=" + FUserID + "&FID=" + FID + "',700,500);\" >" + FName + "</a>";
            string sUrl = "../../ApproveWeb/main/EntselectSysTem.aspx?userID=" + FUserID + "&rightId=" + FID;
            e.Item.Cells[3].Text = "<a target='_blank' href='" + sUrl + "'>" + (string.IsNullOrEmpty(FCompany) ? "暂无填写" : FCompany) + "</a>";//企业名称

            e.Item.Cells[5].Text = sh.getDept(FManageDeptId, 1);//主管部门

            #region 系统权限
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            e.Item.Cells[8].Text = sh.GetDicName(FState);

            #endregion
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
        sl.Add("CF_Sys_UserRight", "FID");
        tool.DelInfoFromGrid(this.DG_List, sl, "dbShare");
        ShowInfo();
    }



}
