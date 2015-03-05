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
using ProjectData;
public partial class Share_User_ManUserList : Page
{
    Share sh = new Share();
    RCenter jst = new RCenter("dbJST");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (sh.GetSysObjectContent("_CanModifyBaseInfo") == "1")
            {
                Submit1.Visible = true;
                this.btnDownload.Visible = true;

            }
            else
            {
                Submit1.Visible = true;
            }
            ControlBind();
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
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
        string fDeptNumber = this.Govdept1.FNumber;
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FEntName.Text))
        {
            sb.Append(" and flinkman like '%" + t_FEntName.Text + "%'");
        }
        if (fDeptNumber != null && fDeptNumber != "")
        {
            sb.Append(" and FManageDeptId like '" + fDeptNumber + "%'");
        }
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            sb.Append(" and fname like '%" + t_FName.Text + "%'");
        }
        if (!string.IsNullOrEmpty(t_FLockNumber.Text))
        {
            sb.Append(" and flocknumber = '" + t_FLockNumber.Text + "'");
        }

        return sb.ToString();
    }
    private void ShowInfo()
    {

        StringBuilder sb = new StringBuilder();
        sb.Append(" select FId,FCompany,FLinkMan,FTel,ftype,FManageDeptId,fname,FDepartmentID,flocknumber ");
        sb.Append(" From cf_sys_user u ");
        sb.Append(" where FIsDeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" And FType=1 ");//ftype=1：管理部门用户
        sb.Append(" Order By FManageDeptId,FDepartmentID, ");
        sb.Append(" FCompany,Fcreatetime Desc");

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
            string FCompany = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FCompany"));
            string FDepartmentID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FDepartmentID"));
            string FDepartment = sh.GetSignValue(" select fname from CF_Sys_Department where fnumber='" + FDepartmentID + "'");
            string FBM = sh.GetSignValue(" select fname from CF_Sys_Department where fnumber='" + EConvert.ToInt(FCompany) + "'");

            DateTime time = DateTime.Now.AddHours(3);
            string key = SecurityEncryption.DesEncrypt(FID + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
            string sUrl = "../../ApproveWeb/main/LockCheck.aspx?key=" + HttpUtility.UrlEncode(key, Encoding.UTF8);
            e.Item.Cells[3].Text = "<a target='_blank' href='" + sUrl + "'>" + e.Item.Cells[3].Text + "</a>";
            e.Item.Cells[5].Text = sh.getDept(FManageDeptId, 1) + FDepartment;
            e.Item.Cells[6].Text = FBM;
            e.Item.Attributes.Add("ondblclick", "showAddWindow('ManUserAdd.aspx?FID=" + FID + "',700,500);");

            #region 系统权限
            //StringBuilder sb = new StringBuilder();
            //DataTable dt = sh.GetTable("select * from CF_Sys_UserRight where fisdeleted=0 and fuserid='" + FID + "'");
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    string FLockNumber = dt.Rows[i]["FLockNumber"].ToString();
            //    string FSystemId = dt.Rows[i]["FSystemId"].ToString();
            //    if (t_FSystemId.SelectedValue == FSystemId)
            //        sb.Append("<span style='color:#FC6F6F'>" + sh.getSystemName(FSystemId) + "</span>[" + FLockNumber + "]<br/>");
            //    else
            //        sb.Append(sh.getSystemName(FSystemId) + "[" + FLockNumber + "]<br/>");
            //}
            //e.Item.Cells[6].Text = sb.ToString();

            #endregion
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        //添加删除日志
        CF_Prj_Log log = new CF_Prj_Log();

        log.FType = 3;
        int RowCount = DG_List.Items.Count;
        for (int i = 0; i < RowCount; i++)
        {
            CheckBox cbx = (CheckBox)DG_List.Items[i].Cells[0].Controls[1];
            if (cbx.Checked)
            {
                log.FId = Guid.NewGuid().ToString();
                log.FCreateTime = DateTime.Now;
                log.FUserId = DG_List.Items[i].Cells[DG_List.Columns.Count - 1].Text.Trim();
                log.FUserName = DG_List.Items[i].Cells[DG_List.Columns.Count - 2].Text.Trim();
                log.FLockNumber = DG_List.Items[i].Cells[4].Text.Trim();
                StringBuilder sb = new StringBuilder();
                sb.Append("insert CF_Prj_Log (fid,fcreatetime,fusername,flocknumber,fuserid,fcreateid,fcreatename,fisdeleted,ftype) values('" + log.FId + "','" + log.FCreateTime + "'");
                sb.Append(",'" + log.FUserName + "','" + log.FLockNumber + "','" + log.FUserId + "','" + Session["CreateID"] + "','" + Session["CreateName"] + "'," + 0 + "," + log.FType + ")");
                sh.PExcute(sb.ToString());
                string FId = DG_List.Items[i].Cells[DG_List.Columns.Count - 1].Text.Trim();
                sh.PExcute(" delete cf_sys_user where fid='" + FId + "'");
                sh.PExcute(" delete CF_Sys_UserRight where FUserId='" + FId + "'");

            }
        }


        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(this.DG_List, EntityTypeEnum.EsUser, "dbShare");
        ShowInfo();
    }


    protected void btnSelectEnt_Click(object sender, EventArgs e)
    {
        ShareTool sl = new ShareTool();

        sl.DownloadAllDeptUser();
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "alert('下载完成');$('#btnReload').click()", true);
    }

}
