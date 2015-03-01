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

public partial class KC_Main_ZDTEntList : System.Web.UI.Page
{
    Share sh = new Share();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
            ViewState["entUID"] = CurrentEntUser.UserId;
            ShowInfo();
        }
    }

    private void ControlBind()
    {

        DataTable dt = sh.GetTable("select replace(fdesc,'资质办理','') FDesc, fnumber,FQurl from cf_Sys_SystemName where fplatid=800 and fnumber!=145 order by fnumber");

        t_FSystemId.DataSource = dt;
        t_FSystemId.DataTextField = "FDesc";
        t_FSystemId.DataValueField = "FNumber";
        t_FSystemId.DataBind();
        //查询相应的系统
        string sql = "select replace(replace(FName,'管理信息系统',''),'信息管理系统','')FName,FNumber from cf_Sys_SystemName ";
        DataTable dtOther = sh.GetTable(sql);
        int iIndex = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                iIndex++;
                string fQurl = dt.Rows[i]["FQurl"].ToString();
                if (!string.IsNullOrEmpty(fQurl)
                    && fQurl.Contains(","))
                {
                    DataRow[] drs = dtOther.Select("FNumber in (" + fQurl + ")");
                    if (drs != null && drs.Length > 0)
                    {
                        for (int ii = 0; ii < drs.Length; ii++)
                        {
                            string fNumber = drs[ii]["FNumber"].ToString();
                            string fName = drs[ii]["FName"].ToString();
                            if (fNumber == "100")
                                fName = "选址意见";
                            ListItem item = new ListItem("└└" + fName, "-" + fNumber);
                            item.Attributes.Add("v", "-1");
                            t_FSystemId.Items.Insert(iIndex, item);
                            iIndex++;
                        }
                    }
                }
            }
        }
        t_FSystemId.Items.Insert(0, new ListItem("请选择", ""));

        t_FSystemId.SelectedValue = "100";
        t_FSystemId.Enabled = false;
        //dt = sh.getDicTbByFNumber("6601");
        //t_FState.DataSource = dt;
        //t_FState.DataTextField = "FName";
        //t_FState.DataValueField = "FNumber";
        //t_FState.DataBind();
        //t_FState.Items.Insert(0, new ListItem("请选择", ""));
        //Govdept1.fNumber = ComFunction.GetDefaultCityDept();
        //Govdept1.Dis(1);
    }

    //条件
    private string GetCon()
    {
        //string fDeptNumber = this.Govdept1.FNumber;
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FEntName.Text))
        {
            sb.Append(" and FCompany like '%" + t_FEntName.Text + "%'");
        }
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            sb.Append(" and FName like '%" + t_FName.Text + "%'");
        }

        if (!string.IsNullOrEmpty(t_FLockNumber.Text))
        {
            sb.Append(" and FCANumber like '%" + t_FLockNumber.Text + "%' ");
        }
        if (!string.IsNullOrEmpty(t_FSystemId.SelectedValue))
        {
            string fSysId = t_FSystemId.SelectedValue;
            if (fSysId.StartsWith("-"))
            {
                fSysId = fSysId.Substring(1);
                sb.Append(" and exists (select fid from cf_Sys_Userright where FSystemId='" + fSysId + "' and fUserId=u.FId) ");
            }
            else
                sb.Append(" and FSystemId ='" + fSysId + "' ");
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

    //显示
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select FId,FCompany,fname,FLinkMan,FTel,ftype,FManageDeptId,FCANumber,FSystemId ");
        sb.Append(" From cf_sys_user u ");
        sb.Append(" where FIsDeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" And FType=2 and FSystemId !='145'");
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
            string FManageDeptId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageDeptId"));
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
            string FuSystemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSystemId"));
            string FCompany = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FCompany"));//企业名

            //用户名 
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('AddCAEntUser.aspx?FID=" + FID + "',800,600);\" >" + FName + "</a>";

            //企业名称，加密登陆链接
            DateTime time = DateTime.Now.AddHours(3);
            string key = SecurityEncryption.DesEncrypt(FID + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
            string sUrl = "../../ApproveWeb/main/EntselectSysTem.aspx?key=" + HttpUtility.UrlEncode(key, Encoding.UTF8) + "&fhuid=" + EConvert.ToString(ViewState["entUID"]);
            e.Item.Cells[3].Text = "<a target='_top' href='" + sUrl + "'>" + (string.IsNullOrEmpty(FCompany) ? "暂无填写" : FCompany) + "</a>";//企业名称

            //企业类型
            string s = sh.GetSignValue("select FDesc from CF_Sys_SystemName where FNumber=@FNumber", new SqlParameter("@FNumber", FuSystemId));
            e.Item.Cells[5].Text = string.IsNullOrEmpty(s) ? "" : s.Replace("资质办理", "");

            //主管部门
            e.Item.Cells[6].Text = sh.getDept(FManageDeptId, 1);//主管部门

            #region 系统权限
            StringBuilder sb = new StringBuilder();
            DataTable dt = sh.GetTable("select FLockNumber,FSystemId,FState from CF_Sys_UserRight where fisdeleted=0 and fuserid='" + FID + "'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string FLockNumber = dt.Rows[i]["FLockNumber"].ToString();
                string FSystemId = dt.Rows[i]["FSystemId"].ToString();
                string FState = dt.Rows[i]["FState"].ToString();
                string systemName = sh.GetSignValue("select fname from cf_Sys_SystemName where fnumber='" + FSystemId + "'");
                if (t_FSystemId.SelectedValue == FSystemId)
                    sb.Append("<span style='color:#FC6F6F'>" + systemName + "</span>[" + FLockNumber + "][" + sh.GetDicName(FState) + "]<br/>");
                else
                    sb.Append(systemName + "[" + FLockNumber + "][" + sh.GetDicName(FState) + "]<br/>");
            }
            e.Item.Cells[9].Text = sb.ToString();

            //查询先关权限
            sb.Remove(0, sb.Length);
            sb.Append("select r.fid,n.FName,n.fNumber from cf_Sys_Userright r ");
            sb.Append("inner join cf_Sys_SystemName n on r.FSystemId=n.FNumber ");
            sb.Append("where r.FUserId='" + FID + "' order by fnumber");
            dt = sh.GetTable(sb.ToString());
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
                e.Item.Cells[6].Text = sb.ToString();
            }
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
        sl.Add("CF_Sys_User", "FID");
        sl.Add("CF_Sys_UserRight", "FUserId");
        tool.DelInfoFromGrid(this.DG_List, sl, "dbShare");
        ShowInfo();
    }



    protected void btnSelectEnt_Click(object sender, EventArgs e)
    {
        var result = db.CF_Sys_User.Where(t => t.FType == 2 && (t.FSystemId == "145" || t.FSystemId == "155" || t.FSystemId == "15501"));
        ShareTool st = new ShareTool();
        foreach (var item in result)
        {
            st.DownloadSingleUser(item.FID, EConvert.ToInt(item.FSystemId));
        }
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "alert('下载完成');$('#btnReload').click()", true);
    }
}
