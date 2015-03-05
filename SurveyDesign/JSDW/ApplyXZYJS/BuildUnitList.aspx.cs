using Approve.Common;
using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyXZYJS_BuildUnitList : System.Web.UI.Page
{
    private Share sh = new Share();
    private ProjectDB db = new ProjectDB();
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //List<Student> list = new List<Student>() { 
            //    new Student(){Name="yujiajun",Path="http://www.google.com"},
            //    new Student(){Name="xueci",Path="http://www.baidu.com"}
            //};
            //rptData.DataSource = list;
            //rptData.DataBind();
            ControlBind();
            Show();
        }
    }
    private void ControlBind()
    {

        DataTable dt = sh.GetTable("select replace(fdesc,'资质办理','') FDesc, fnumber,FQurl from cf_Sys_SystemName where fplatid=800 order by fnumber");
        t_FSystemId.DataSource = dt;
        t_FSystemId.DataTextField = "FDesc";
        t_FSystemId.DataValueField = "FNumber";
        t_FSystemId.DataBind();
        t_FSystemId.Items.Insert(0, new ListItem("请选择", ""));
    }
    private void Show()
    {
        StringBuilder _builder = new StringBuilder();
        _builder.Append("select * from cf_sys_user where FSystemId=100 and  isnull(FIsDeleted,0)=0 And isnull(FType,0)=2");
        _builder.Append(GetCon());
        _builder.Append(" Order By Fcreatetime Desc");
        this.Pager1.className = "dbShare";
        this.Pager1.sql = _builder.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    //条件
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FEntName.Text.Trim()))
        { sb.Append(" and FCompany like '%" + t_FEntName.Text.Trim() + "%'"); }
        if (!string.IsNullOrEmpty(t_FSystemId.SelectedValue))
        {
            //string fSysId = t_FSystemId.SelectedValue;
            //if (fSysId.StartsWith("-"))
            //{
            //    fSysId = fSysId.Substring(1);
            //    sb.Append(" and exists (select fid from cf_Sys_Userright where FSystemId='" + fSysId + "' and fUserId=u.FId) ");
            //}
            //else
            //    sb.Append(" and FSystemId ='" + fSysId + "' ");
            sb.AppendFormat(" and FID in (select FuserId from cf_Sys_Userright where FSystemId={0})",t_FSystemId.SelectedValue);
        }
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            sb.Append(" and FName like '%" + t_FName.Text.Trim() + "%'");
        }
        if (!string.IsNullOrEmpty(t_FState.SelectedValue))
        {
            sb.Append(" and fid in (select FUserId from cf_sys_userright r where r.fuserid=u.FID and FState ='" + t_FState.SelectedValue + "')");
        }
        if (t_FCAState.SelectedValue == "0")
        {
            sb.Append(" and isnull(FCANumber,'')='' ");
        }
        else if (t_FCAState.SelectedValue == "1")
        {
            sb.Append(" and isnull(FCANumber,'')!='' ");
        }
        return sb.ToString();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Show();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        int RowCount = DG_List.Items.Count;
        List<string> listFids = new List<string>();
        //删除数据操作 
        for (int i = 0; i < RowCount; i++)
        {
            CheckBox cbx = (CheckBox)DG_List.Items[i].Cells[0].Controls[1];
            if (cbx.Checked)
            {
                listFids.Add(DG_List.Items[i].Cells[DG_List.Columns.Count - 1].Text.Trim());
            }
        }
        if (listFids.Count < 1)
        {
            tool.showMessage("当前未选择任何项");
            return;
        }
        StringBuilder _builder = new StringBuilder();
        listFids.ForEach(item =>
        {
            _builder.AppendFormat("delete from cf_sys_user where FID='{0}';",item);
            _builder.AppendFormat("delete from cf_sys_userright where fuserid='{0}';", item);
        });
        if (rc.PExcute(_builder.ToString()))
        {
            tool.showMessage("删除成功");
            Show();
        }
        else {
            tool.showMessage("删除失败");
        }
    }
    protected void btnQuery1_Click(object sender, EventArgs e)
    {
        Show();
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

            //e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('user/EditUser.aspx?fid=" + FID + "',800,600);\" >" + FName + "</a>";

            //企业名称，加密登陆链接
            DateTime time = DateTime.Now.AddHours(3);
            string key = SecurityEncryption.DesEncrypt(FID + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
            string sUrl = "../../ApproveWeb/main/EntselectSysTem.aspx?key=" + HttpUtility.UrlEncode(key, Encoding.UTF8);
            e.Item.Cells[4].Text = "<a target='_blank' href='" + sUrl + "'>" + (string.IsNullOrEmpty(FCompany) ? "暂无填写" : FCompany) + "</a>";//企业名称

            //企业类型
            string s = sh.GetSignValue("select FDesc from CF_Sys_SystemName where FNumber=@FNumber", new SqlParameter("@FNumber", FuSystemId));
            e.Item.Cells[6].Text = string.IsNullOrEmpty(s) ? "" : s.Replace("资质办理", "");

            e.Item.Cells[5].Text = db.CF_Sys_UserCA.Where(t => t.FUserID == FID).Select(t => t.FCANumber).FirstOrDefault();

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
                e.Item.Cells[9].Text = sb.ToString();
            }
        }
    }
}