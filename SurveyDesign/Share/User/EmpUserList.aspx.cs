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
using System.Data.SqlClient;

public partial class Share_User_EmpUserList : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if (sh.GetSysObjectContent("_CanModifyBaseInfo") == "1")
            {
                Submit1.Visible = true;
            }
            else
            {
                Submit1.Visible = false;
            }
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            ControlBind();
            ShowInfo();
        }
    }

    private void ControlBind()
    {
        Govdept1.fNumber = ComFunction.GetDefaultCityDept();
        Govdept1.Dis(1);
    }

    //条件
    private string GetCon()
    {
        //string fDeptNumber = this.Govdept1.FNumber;
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FUserName.Text))
        {
            sb.Append(" and e.FUserName like '%" + t_FUserName.Text + "%'");
        }
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            sb.Append(" and e.FName like '%" + t_FName.Text + "%'");
        }
        if (!string.IsNullOrEmpty(t_FSystemId.SelectedValue))
        {
            sb.Append(" and r.FSystemId ='" + t_FSystemId.SelectedValue + "' ");
        }
        if (!string.IsNullOrEmpty(t_FEntName.Text))
        {
            sb.Append(" and b.FName like '%" + t_FEntName.Text + "%' ");
        }
        if (!string.IsNullOrEmpty(t_FLockNumber.Text.Trim()))
            sb.Append(" and exists (select 1 from cf_Sys_User where FCANumber like '%" + t_FLockNumber.Text.Trim() + "%' and FId=e.FId) ");
        if (t_FCAState.SelectedValue == "0")//未办理
            sb.Append(" and not exists (select 1 from cf_Sys_User where FId=e.FId and isnull(FCANumber,'')!='') ");
        else if (t_FCAState.SelectedValue == "1")//已办理
            sb.Append(" and exists (select 1 from cf_Sys_User where FId=e.FId and isnull(FCANumber,'')!='') ");
        return sb.ToString();
    }

    //显示
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select e.FId,e.FBaseinfoId,e.FName,e.FUserName,e.FPersonTypeId,b.FName FEntName,b.FRegistDeptId,r.FSystemId,e.FCertiNo,");
        sb.Append("(select top 1 FCANumber from cf_Sys_User where FId=e.FId)FCANumber,");
        sb.Append("(select top 1 FTel from cf_Sys_User where FId=e.FId)FTel ");
        sb.Append("From CF_Emp_Baseinfo e inner join CF_Ent_Baseinfo b ");
        sb.Append("on e.FBaseinfoId=b.FID and e.FIsDeleted=0 and b.FIsDeleted=0 ");
        sb.Append("join cf_Sys_Userright r on b.fid=r.FBaseInfoId ");
        sb.Append("where r.FSystemId in (1261,1451,1553,1554) ");
        sb.Append(GetCon());
        sb.Append(" Order By e.Fcreatetime Desc  ");

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
            string FRegistDeptId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FRegistDeptId"));//企业注册地
            string FUserName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FUserName"));
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
            string FSystemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSystemId"));

            //用户名 
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('EmpUserAdd.aspx?FID=" + FID + "',800,600);\" >" + FUserName + "</a>";

            //姓名，加密登陆链接
            DateTime time = DateTime.Now.AddHours(3);
            string key = SecurityEncryption.DesEncrypt(FID + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
            string sUrl = "../../ApproveWeb/main/EmpLockCheck.aspx?admin=1&key=" + HttpUtility.UrlEncode(key, Encoding.UTF8);
            e.Item.Cells[3].Text = "<a target='_blank' href='" + sUrl + "'>" + FName + "</a>";//企业名称

            //人员类型
            string s = t_FSystemId.Items.FindByValue(FSystemId).Text;
            e.Item.Cells[4].Text = s;
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
        sl.Add("CF_Emp_Baseinfo", "FID");
        tool.DelInfoFromGrid(DG_List, sl, "RCenter");
        ShowInfo();
    }



}
