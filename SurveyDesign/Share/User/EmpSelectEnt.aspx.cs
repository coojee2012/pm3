using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using Approve.RuleCenter;
using ProjectData;
using System.Data;

public partial class Share_User_EmpSelectEnt : System.Web.UI.Page
{
    Share sh = new Share();
    ProjectDB db = new ProjectDB();
    string sSys = "1261,1451,1553,1554";//见证、审图、设计、勘察
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindControl();
            ShowInfo();
        }
    }
    void BindControl()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FDesc FName,FNumber from CF_Sys_SystemName ");
        sb.Append("where FNumber in (" + sSys + ")");
        DataTable dt = sh.GetTable(sb.ToString());
        t_FSystemId.DataSource = dt;
        t_FSystemId.DataTextField = "FName";
        t_FSystemId.DataValueField = "FNumber";
        t_FSystemId.DataBind();
        t_FSystemId.Items.Insert(0, new ListItem("", ""));
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    //显示
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select r.FId,u.FCompany,u.fname,u.FLinkMan,u.FTel,u.ftype,u.FManageDeptId,u.FLockNumber,r.FSystemId,r.FBaseInfoId ");
        sb.Append(" From cf_sys_user u inner join cf_Sys_Userright r ");
        sb.Append(" on u.fid=r.FUserId ");
        sb.Append(" where u.FIsDeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" And u.FType=2 and r.FSystemId in (" + sSys + ") ");
        sb.Append(" Order By u.Fcreatetime Desc,r.FID ");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 15;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();

    }
    private string GetCon()
    {
        string fDeptNumber = this.Govdept1.FNumber;
        Govdept1.fNumber = fDeptNumber;
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FEntName.Text))
        {
            sb.Append(" and u.FCompany like '%" + t_FEntName.Text + "%' ");
        }
        if (!string.IsNullOrEmpty(fDeptNumber))
        {
            sb.Append(" and u.FManageDeptId like '" + fDeptNumber + "%' ");
        }
        if (!string.IsNullOrEmpty(t_FSystemId.SelectedValue))
        {
            sb.Append(" and r.FSystemId='" + t_FSystemId.SelectedValue + "' ");
        }
        return sb.ToString();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            int FManageDeptId = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FManageDeptId"));
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
            string FSystemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSystemId"));
            string FCompany = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FCompany"));//企业名 
            string s = t_FSystemId.Items.FindByValue(FSystemId).Text;
            e.Item.Cells[3].Text = s;

            string fdeptName = db.ManageDept.Where(t => t.FNumber == FManageDeptId).Select(t => t.FName).FirstOrDefault();
            e.Item.Cells[4].Text = fdeptName;
        }
    }
}
