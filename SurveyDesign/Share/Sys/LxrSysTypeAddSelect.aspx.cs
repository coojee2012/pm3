using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Reflection;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using Approve.RuleApp;
using System.Web.UI.Adapters;
//using System.Web.UI.MobileControls.Adapters;
using System.Web.UI.Design;
using System.Text;


public partial class Admin_User_LxrSysTypeAddSelect : System.Web.UI.Page
{
    //实例化中间层的类
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //初始化页面时显示数据
            ShowInfo();
        }
    }
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        //用户名
        if (!string.IsNullOrEmpty(text_FName.Text))
            sb.Append(" and r.FName like '%" + text_FName.Text.Trim() + "%'  ");
        

        //硬件编号
        if (!string.IsNullOrEmpty(text_FLockNumber.Text))
            sb.Append(" and r.FLockNumber like '%" + text_FLockNumber.Text + "%' ");
        //标签编号
        if (!string.IsNullOrEmpty(txtLockLabelNumber.Text))
            sb.Append(" and r.flocklabelnumber like '%" + txtLockLabelNumber.Text + "%' ");
        //管理部门
        if (!string.IsNullOrEmpty(Govdept1.FNumber))
            sb.Append(" and u.FManageDeptId like '" + Govdept1.FNumber + "%'");
     

        return sb.ToString();
    }
    //显示数据
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select u.FID,r.FID rFID,u.FManageDeptId,u.FCompany,u.FLinkMan,u.FTel,r.FRoleId,");
        sb.Append("r.FName,r.FLockLabelNumber,r.FLockNumber,r.FBaseInfoId,r.FState,r.FSystemId,r.FEndTime ");
        sb.Append("from cf_sys_user u,cf_sys_userright r ");
        sb.Append("where u.fid=r.fuserid and isnull(u.fisdeleted,0)=0 and isnull(r.fisdeleted,0)=0 ");
        sb.Append("and u.ftype=1 ");//ftype=1：管理部门用户
        sb.Append(GetCon());
        sb.Append("order by r.fcreatetime desc ");

        this.Pager1.className = "RCenter";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "EntInfo_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }



    
    protected void Dic_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;                 //获取FID的值
            string FName = e.Item.Cells[1].Text;                //获取用户名

            //给“序号”列赋值
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();


        
        }
    }
    //查询按钮
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
