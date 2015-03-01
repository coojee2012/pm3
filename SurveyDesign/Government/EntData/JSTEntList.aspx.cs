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
using Approve.EntityBase;
using Approve.RuleCenter;
using Approve.Common;
using Approve.PersistBase;
using ProjectData;
using System.Linq;

public partial class Government_EntData_JSTEntList : System.Web.UI.Page
{
    RQuali rq = new RQuali();
    RCenter rc = new RCenter();
    SaveAsBase sab = new SaveAsBase();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (EConvert.ToInt(Request.QueryString["enttype"]) == 145)
            {
            }
            else
            {
                Ent_List.Columns[6].Visible = false;
            }
            ShowTitle();
            ControlBind();
            ShowInfo();
        }
    }
    private void ShowTitle()
    {
        string fColName = rc.GetMenuName(Request["fcol"]);
        if (!string.IsNullOrEmpty(fColName))
        {
            this.lTitle.Text = fColName;
        }
    }

    private void ControlBind()
    {
        string fDeptNumber = ComFunction.GetDefaultDept();
        StringBuilder sb = new StringBuilder();
        DataTable dt = rc.getAllupDeptId(Session["DFId"].ToString(), 0, 0);
        dbMangeDept.DataSource = dt;
        dbMangeDept.DataTextField = "FName";
        dbMangeDept.DataValueField = "FNumber";
        dbMangeDept.DataBind();
        dbMangeDept.Items.Insert(0, new ListItem("--请选择--", ""));
    }

    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        string fmdept = dbMangeDept.SelectedValue.Trim();
        if (fmdept != null && fmdept != "")
        {
            sb.Append(" and e.FRegistDeptId like '" + fmdept + "%' ");
        }

        if (this.txtFName.Text != "")
        {
            sb.Append(" and e.fname like '%" + txtFName.Text + "%' ");
        }
        if (!string.IsNullOrEmpty(Request.QueryString["enttype"]))
        {
            if (Request.QueryString["enttype"] == "15501")
            {
                sb.Append(" and e.FSystemId='1550' ");
            }
            else
            {
                sb.Append(" and e.FSystemId='" + Request.QueryString["enttype"] + "' ");
            }
        }

        return sb.ToString();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        //sb.Append(" select t1.fid,t1.FState,t1.FRegistAddress,FCompany  fname,");
        //sb.Append(" t1.flinkman,t1.ftel,t1.FRegistDeptId,");
        //sb.Append(" t1.FLicence,u.FSystemId,t1.FMobile,t1.FJuridcialCode ");
        //sb.Append(" from cf_Sys_User u ");
        //sb.Append(" inner join cf_Sys_Userright r on u.FId=r.FUserId ");
        //sb.Append(" left join cf_ent_baseinfo t1 on t1.FId=r.FBaseInfoId ");
        //sb.Append(" where u.ftype=2 ");
        //sb.Append(" and t1.fstate<>8 ");

        sb.Append("select e.* from cf_ent_baseinfo e,cf_sys_user u where e.fid=u.fbaseinfoid and u.ftype=2 and e.fstate<> 8 and u.fisdeleted = 0  and e.fisdeleted = 0");
        sb.Append(GetCon());
        sb.Append(" order by e.FSystemId,e.FName ");
        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "Ent_List";
        this.Pager1.className = "dbJST";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }
    protected void Ent_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fUpDeptId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FRegistDeptId"));
            string fsystemid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fsystemid"));

            if (!string.IsNullOrEmpty(fUpDeptId) && fUpDeptId.Trim() != "0")
                e.Item.Cells[3].Text = rc.GetSignValue(EntityTypeEnum.EsManageDept, "FName", "fnumber='" + fUpDeptId + "'");

            var c = db.CF_Ent_QualiCerti.Where(t => t.FBaseInfoId == fid).Select(t => new { t.FCertiNo, t.FLevel }).FirstOrDefault();
            if (c != null)
            {
                e.Item.Cells[6].Text = db.SysQualiLevel.Where(t => t.FNumber == c.FLevel).Select(t => t.FName).FirstOrDefault();
            }

            //string sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber='" + fsystemid + "'"); ;
            string sUrl = "";
            string sScript = "javascript:openWinNew('" + sUrl + "?sysid=" + fsystemid + "&fbid=" + fid + "&fly=1&fuid=" + Session["DFUserId"].ToString() + "')";

            //e.Item.Cells[2].Text = "<a class='link7' href=\"" + sScript + "\" >" + e.Item.Cells[2].Text + "</a>";
            if (fsystemid == "100")
            {
                sUrl = "JsPrjList.aspx";

            }
            else if (fsystemid == "15501")
            {
                sUrl = "KCPrjList.aspx";
            }
            else if (fsystemid == "155")
            {
                sUrl = "SJPrjList.aspx";
            }
            else if (fsystemid == "126")
            {
                sUrl = "JZPrjList.aspx";
            }
            else if (fsystemid == "145")
            {
                sUrl = "SgtPrjList.aspx";
            }
            e.Item.Cells[Ent_List.Columns.Count - 2].Text = "<a class='link7' href=\"javascript:showAddWindow('" + sUrl + "?FBaseinfoId=" + fid + "',800,450);\" >查看</a>";
        }
    }
    string GetBaseIdBySecret(string fbid)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(fbid);
        sb.Append("|");
        sb.Append(SecurityEncryption.ConvertDateTimeInt(DateTime.Now.AddHours(1)));
        return SecurityEncryption.DesEncrypt(sb.ToString(), "12345678");
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnOut_Click(object sender, EventArgs e)
    {
        string sql = Pager1.sql;
        DataTable dt = rc.GetTable(sql);
        Ent_List.DataSource = dt;
        Ent_List.DataBind();
        Ent_List.Columns[0].Visible = false;
        Ent_List.Columns[Ent_List.Columns.Count - 2].Visible = false;
        sab.SaveAsExc(Ent_List, "企业信息", this.Response, "gb2312", 0);
    }
    protected void Ent_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {

    }
}
