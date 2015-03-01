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

public partial class Government_statis_EntBadActionList : System.Web.UI.Page
{
    RCenter rc = new RCenter("dbJST");
    Share sh = new Share();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();

            ViewState["entUID"] = CurrentEntUser.UserId;
            govd_FRegistDeptId.fNumber = "51";

            ViewState["entID"] = CurrentEntUser.EntId;
            if (CurrentEntUser.EntId != null)
                ShowInfo();
        }
    }

    private void ControlBind()
    {

        //DataTable dt = sh.GetTable("select replace(fdesc,'资质办理','') FDesc, fnumber,FQurl from cf_Sys_SystemName where fplatid=800  order by fnumber");

        //t_FSystemId.DataSource = dt;
        //t_FSystemId.DataTextField = "FDesc";
        //t_FSystemId.DataValueField = "FNumber";
        //t_FSystemId.DataBind();
        //t_FSystemId.Items.Insert(0, new ListItem("请选择", ""));



    }

    //条件
    private string GetCon(ref SortedList sl)
    {
        //string fDeptNumber = this.Govdept1.FNumber;
        StringBuilder sb = new StringBuilder();


        if (!string.IsNullOrEmpty(time.Text))
        {
            sb.Append(" and CFRQ = @time ");
            sl.Add("time", EConvert.ToDateTime(time.Text));
        }

        if (t_FSystemId.SelectedValue != "")
        {
            string fSysId = t_FSystemId.SelectedValue;

            sb.Append(" and ZRZTLBID ='" + fSysId + "' ");
        }
        string fnum = this.govd_FRegistDeptId.fNumber;
        if (fnum != "")
        {
            sb.Append(" and SD like @gcdz ");
            sl.Add("gcdz", fnum + "%");
        }

        if (!string.IsNullOrEmpty(time1.Text))
        {
            sb.Append(" and CFRQ >= @time1 ");
            sl.Add("time1", EConvert.ToDateTime(time1.Text));
        }

        if (!string.IsNullOrEmpty(time2.Text))
        {
            sb.Append(" and CFRQ <= @time2 ");
            sl.Add("time2", EConvert.ToDateTime(time2.Text));
        }

        return sb.ToString();
    }

    //显示
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sb1 = new StringBuilder();
        SortedList sl = new SortedList();
        SortedList sl1 = new SortedList();
        sb.Append("select * from (select ID,CASE WHEN DQC IS NOT NULL THEN DQC ELSE CASE WHEN DQB IS NOT NULL THEN DQB ELSE DQA END END as SD,QYMC,ZRZTLB,ZRZTLBID,CFRQ,FS,RDDW From QY_BLXW_XXB where ZRZTLBID='" + CurrentEntUser.EntId + "') tt  ");
        sb.Append(" where 1=1 and ZRZTLBID in (145,140,155) ");
        sb.Append(GetCon(ref sl));
        sb.Append(" Order By CFRQ Desc ");


        sb1.Append(" select QYID from (select ID,QYID,CASE WHEN DQC IS NOT NULL THEN DQC ELSE CASE WHEN DQB IS NOT NULL THEN DQB ELSE DQA END END as SD,QYMC,ZRZTLB,ZRZTLBID,CFRQ,FS,RDDW From QY_BLXW_XXB) tt where 1=1 and ZRZTLBID in (145,140,155) ");
        sb1.Append(GetCon(ref sl1));
        sb1.Append(" group by QYID ");

        DataTable dt1 = rc.GetTable(sb.ToString(), rc.ConvertParameters(sl));
        DataTable dt2 = rc.GetTable(sb1.ToString(), rc.ConvertParameters(sl1));
        int i1 = 0;
        int i2 = 0;
        if (dt1 != null && dt1.Rows.Count > 0)
        {
            i1 = dt1.Rows.Count;
        }
        if (dt2 != null && dt2.Rows.Count > 0)
        {
            i2 = dt2.Rows.Count;
        }

        this.li_MSG.Text = "处罚企业数" + i2 + ",处罚次数" + i1 + "。";

        this.Pager1.className = "dbJST";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.Parameters = sl;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();

    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ID"));
            e.Item.Cells[4].Text = e.Item.Cells[4].Text + "分";

            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('EntBadAction.aspx?fid=" + FID + "',900,700);\">" + e.Item.Cells[2].Text + " </a>";
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }





}
