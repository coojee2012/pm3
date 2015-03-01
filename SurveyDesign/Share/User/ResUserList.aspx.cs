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
public partial class Admin_User_UserList : Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            if (Request["fmatypeid"] != null && Request["fmatypeid"] != "")
            {
                this.ViewState["FMATYPEID"] = Request["fmatypeid"];
            }
            ControlBind();
            ShowInfo();
        }

    }

    private void ControlBind()
    {

    }
    private string GetCon()
    {
        string fDeptNumber = this.Govdept1.FNumber;
        StringBuilder sb = new StringBuilder();
        if (this.text_FName.Text != "")
        {
            sb.Append(" and fname like '");
            sb.Append(this.text_FName.Text + "%' ");
        }
        if (this.text_FEndTime.Text != "")
        {
            sb.Append(" and FEndTime>='");
            sb.Append(this.text_FEndTime.Text + "' ");
        }
        if (this.text_FEndTime1.Text != "")
        {
            sb.Append(" and FEndTime<='");
            sb.Append(this.text_FEndTime1.Text + "' ");
        }
        if (this.text_FLockLabelNumber.Text != "")
        {
            sb.Append(" and FLockLabelNumber like '%");
            sb.Append(this.text_FLockLabelNumber.Text + "%' ");
        }
        if (this.text_FLockNumber.Text != "")
        {
            sb.Append(" and FLockNumber like '%");
            sb.Append(this.text_FLockNumber.Text + "%' ");
        }
        if (fDeptNumber != null && fDeptNumber != "")
        {
            sb.Append(" and FManageDeptId like '");
            sb.Append(fDeptNumber + "%' ");
        }
        return sb.ToString();
    }
    private void ShowInfo()
    {

        StringBuilder sb = new StringBuilder();
        sb.Append(" select FId,FName,FEndTime,FLinkMan,FTel,FLockLabelNumber,FLockNumber,ftype,");
        sb.Append(" FRoleId,");
        sb.Append(" FManageDeptId,");
        sb.Append(" FBatchId ");
        sb.Append(" From cf_sys_user ");
        sb.Append(" where FIsDeleted=0 and ftype='" + Request.QueryString["ftype"] + "'");
        sb.Append(GetCon());
        sb.Append(" Order By  Fcreatetime Desc");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "User_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();

    }

    protected void Dic_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();

            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string ftype = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            string fRoleId = e.Item.Cells[5].Text.Trim();
            string fManageDept = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageDeptId")); ;
            string fBathId = e.Item.Cells[10].Text;
            string flinkMan = e.Item.Cells[8].Text;
            string flinkTel = e.Item.Cells[9].Text;
            StringBuilder sb1 = new StringBuilder();
            sb1.Append("联系人：" + flinkMan.Replace("&nbsp;", "") + "");
            sb1.Append("    联系电话：" + flinkTel.Replace("&nbsp;", "") + "");
            e.Item.Attributes.Add("onmouseover", "this.title='" + sb1.ToString() + "'");

            e.Item.Cells[6].Text = rc.GetSignValue(EntityTypeEnum.EsManageDept, "FName", "FNumber='" + fManageDept + "'");

            string fUrl = "ResUserAdd.aspx?fid=" + fid + "&type=" + Request.QueryString["ftype"];
            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showAddWindow('" + fUrl + "',680,500);\">" + e.Item.Cells[2].Text + "</a>";

            DateTime time = DateTime.Now.AddHours(1);
            string key = SecurityEncryption.DesEncrypt(fid + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
            fUrl = "../Main/LockCheck.aspx?UserID=" + HttpUtility.UrlEncode(key, Encoding.UTF8);
            e.Item.Cells[3].Text = "<a class='link3' target='_blank' href='" + fUrl + "'>" + e.Item.Cells[3].Text + "</a>";
            e.Item.Cells[7].Text = rc.StrToDate(e.Item.Cells[7].Text.Trim());

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
    protected void btnDel_Click1(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(this.User_List, EntityTypeEnum.EsUser, "dbShare");
        ShowInfo();
    }
}
