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

public partial class Government_EntData_SCEntList : System.Web.UI.Page
{
    RQuali rq = new RQuali();
    RCenter rc = new RCenter();
    string interfaceUserName = "";//接口用户名
    string interfacePassword = "";//接口密码
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
        interfaceUserName = rc.GetSysObjectContent("_InterfaceUserName");
        interfacePassword = rc.GetSysObjectContent("_InterfacePassword");
        StringBuilder sb = new StringBuilder("1=1 ");
        string rn = "";
        if (this.txtFName.Text != null && this.txtFName.Text != "")
        {
            sb.Append(" and FName like '%" + this.txtFName.Text + "%'");
        }
        if (Request.QueryString["enttype"] != null && Request.QueryString["enttype"] != "")
        {
            sb.Append(" and FSystemId = '" + Request.QueryString["enttype"] + "'");
        }

        if (this.dbMangeDept.SelectedValue != "")
        {
            sb.Append(" and  FUpDeptId like '" + this.dbMangeDept.SelectedValue + "%' ");
        }

        cn.gov.scjst.zw.JSTJKWebService jst = new cn.gov.scjst.zw.JSTJKWebService();
        DataTable dt = jst.GetTABLE(interfaceUserName, interfacePassword, sb.ToString(), 0, "勘察设计企业信息表（网站）", out rn);

        if (dt != null && dt.Rows.Count > 0)
        {






            //DataRow[] rows = dt.Select(" 1=1 " + sb.ToString());

            var v = from r in dt.AsEnumerable()
                    select new
                    {
                        FId = r["FId"],
                        FUpDeptId = r["FUpDeptId"],
                        FSystemId = r["FSystemId"],
                        FAddress = r["FAddress"],
                        FJuridcialCode = r["FJuridcialCode"],
                        FLinkMan = r["FLinkMan"],
                        FMobile = r["FMobile"],
                        FName = r["FName"],
                        SYFS = dt.Columns.Contains("SYFS")?(r["SYFS"] as int?):default(int?)
                    };//default(int?),
            // "勘察设计企业信息表（网站） 才有信用的分数，但这个表里数据不全.r.co r["SYFS"]
            Pager1.RecordCount = v.Count();
            Ent_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            Ent_List.DataBind();
            Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页不显示
        }
    }
    protected void Ent_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();

            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fUpDeptId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FUpDeptId"));
            string fsystemid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSystemId"));

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
            else if (fsystemid == "1550")
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
            if (sUrl != null && sUrl.Length > 1)
                e.Item.Cells[Ent_List.Columns.Count - 2].Text = "<a class='link7' href=\"javascript:showAddWindow('" + sUrl + "?FBaseinfoId=" + fid + "',800,450);\" >查看</a>";
            int? SYFS = DataBinder.Eval(e.Item.DataItem, "SYFS") as int?;
            if (SYFS==null ||  (SYFS != null && !SYFS.HasValue))
            {
                //查分数
                cn.gov.scjst.zw.JSTJKWebService jst = new cn.gov.scjst.zw.JSTJKWebService();
                string rn = "";
                DataTable dt = jst.GetTABLE(interfaceUserName, interfacePassword, "FID='" + fid + "'", 0, "勘察设计企业信息表（网站）", out rn);
                if (dt != null && dt.Rows.Count > 0)
                {
                    e.Item.Cells[Ent_List.Columns.Count - 3].Text = EConvert.ToString(dt.Rows[0]["SYFS"]);
                }
                else
                {
                    e.Item.Cells[Ent_List.Columns.Count - 3].Text = "100";
                }
            }
            else
            {
                e.Item.Cells[Ent_List.Columns.Count - 3].Text = "100";
            }
            string syfsUrl = "http://web.scjst.gov.cn/webSite/project_appinfo/cxEntBadDetail_JKC.aspx";
            e.Item.Cells[Ent_List.Columns.Count - 3].Text = "<a class='link7' href=\"javascript:showAddWindow('" + syfsUrl + "?FId=" + fid + "',800,450);\" >" + e.Item.Cells[Ent_List.Columns.Count - 3].Text + "</a>";
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
        string rn = "";
        cn.gov.scjst.zw.JSTJKWebService jst = new cn.gov.scjst.zw.JSTJKWebService();
        DataTable dt = dt = jst.GetTABLE(interfaceUserName, interfacePassword, "", 0, "勘察设计企业信息表（网站）", out rn);

        Ent_List.DataSource = dt;
        Ent_List.DataBind();
        Ent_List.Columns[0].Visible = false;
        Ent_List.Columns[Ent_List.Columns.Count - 2].Visible = false;
        sab.SaveAsExc(Ent_List, "企业信息", this.Response, "gb2312", 0);
    }
    protected void Ent_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {

    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowInfo();
    }

}
