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
using ProjectData;

public partial class GFEnt_EntAccount : System.Web.UI.Page
{
    Share sh = new Share(); ProjectDB db = new ProjectDB();
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

    }

    //条件
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FEntName.Text.Trim()))
        { sb.Append(" and FCompany like '%" + t_FEntName.Text.Trim() + "%'"); }
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            sb.Append(" and FName like '%" + t_FName.Text.Trim() + "%'");
        }
        return sb.ToString();
    }

    //显示
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"select * from cf_sys_user where fid in (select FUserId from cf_sys_userright where FSystemId=1123) 
                    and isnull(FIsDeleted,0)=0 And isnull(FType,0)=2");
        sb.Append(GetCon());
        sb.Append(" Order By Fcreatetime Desc,FID ");
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
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
            string FCompany = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FCompany"));//企业名
            string FuSystemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSystemId"));

            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('AddEntUser.aspx?fid=" + FID + "',800,600);\" >" + FName + "</a>";

            //企业名称，加密登陆链接
            DateTime time = DateTime.Now.AddHours(3);
            string key = SecurityEncryption.DesEncrypt(FID + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
            string sUrl = "../../ApproveWeb/main/EntselectSysTem.aspx?key=" + HttpUtility.UrlEncode(key, Encoding.UTF8);
            e.Item.Cells[3].Text = "<a target='_blank' href='" + sUrl + "'>" + (string.IsNullOrEmpty(FCompany) ? "暂无填写" : FCompany) + "</a>";//企业名称

            //查询先关权限
            StringBuilder sb = new StringBuilder();
            sb.Remove(0, sb.Length);
            sb.Append("select r.fid,n.FName,n.fNumber from cf_Sys_Userright r ");
            sb.Append("inner join cf_Sys_SystemName n on r.FSystemId=n.FNumber ");
            sb.Append("where r.FUserId='" + FID + "' order by fnumber");
            DataTable dt = sh.GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Remove(0, sb.Length);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string frId = dt.Rows[i]["FId"].ToString();
                    string fName = dt.Rows[i]["FName"].ToString();
                    string fNumber = dt.Rows[i]["fNumber"].ToString();
                    if (fNumber == "100")
                        fName = "选址意见管理信息系统";
                    fName = "[<a href='javascript:void(0)' onclick=\"addRight('" + FID + "','" + frId + "');\">" + fName + "</a>]";
                    if (sb.Length > 0)
                        sb.Append("<br/>");
                    sb.Append(fName);
                }
                e.Item.Cells[7].Text = sb.ToString();
            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append(" begin ");
        for (int i = 0; i < DG_List.Items.Count; i++)
        {
            string id = DG_List.Items[i].Cells[DG_List.Items[i].Cells.Count - 1].Text;
            CheckBox box = (CheckBox)DG_List.Items[i].Cells[0].Controls[1];
            if (box.Checked == true)
                sb.Append("delete cf_sys_userright where FUserId='" + id + "' and FSystemId=1123");
        }
        sb.Append(" end ");
        sh.PExcute(sb.ToString());
        ShowInfo();
    }

}