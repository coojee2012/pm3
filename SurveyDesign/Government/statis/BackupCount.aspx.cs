using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Approve.RuleCenter;
using Approve.EntityBase;
using System.Data.SqlClient;
using System.Collections;
using ProjectData;

public partial class Government_statis_BackupCount : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    public DataTable dt = new DataTable();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DeptNumber = EConvert.ToString(Session["DFId"]);
            if (!string.IsNullOrEmpty(Request.QueryString["DeptId"]))
            {
                DeptNumber = Request.QueryString["DeptId"];
            }
            conBind();
            showInfo();
            ShowPostion();

        }
    }
    public string DeptNumber
    {
        get { return EConvert.ToString(ViewState["DeptNumber"]); }
        set { ViewState["DeptNumber"] = value; }
    }
    private void ShowPostion()
    {
        this.lPostion.Text = rc.GetMenuName(Request["fcol"]);
    }
    //绑定下拉框
    private void conBind()
    {
        //按年
        for (int i = DateTime.Now.Year; i >= 2010; i--)
            dr_Year.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));
    }

    private string getCondi()
    {

        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" and er.FRoleId in (" + Session["DFRoleId"] + ")");
        sb.AppendLine(" and ep.FManageDeptId like '" + Session["DFId"] + "%' ");
        sb.Append(" and ep.fstate = 6 ");
        return sb.ToString();
    }

    //显示
    private void showInfo()
    {
        SortedList sl = new SortedList();

        StringBuilder sb = new StringBuilder();
        sb.Append("select FName,FNumber,FLevel,FId ");
        sb.Append("from CF_Sys_ManageDept where FName not like'%市辖区%' ");
        int FManageTypeId = EConvert.ToInt(Request.QueryString["FManageTypeId"]);
        if (DeptNumber.Length == 2)
        {


            sb.Append("  and (fnumber like '" + DeptNumber + "%' and flevel=2)");

        }
        else if (DeptNumber.Length == 4)
        {


            sb.Append(" and  (fnumber like '" + DeptNumber + "%' and flevel=3)");

        }
        else
        {
            sl.Add("FParentId", DeptNumber);
            sb.Append(" and  FNumber=@FParentId ");
        }
        sb.Append(" order by FNumber ");

        var result = db.CF_Prj_Certi.Where(t => t.FIsValid == 1 && t.FCityId.ToString().StartsWith(DeptNumber)
            && t.FCertiTypeId == FManageTypeId);

        if (!string.IsNullOrEmpty(Request.QueryString["CountWay"]))
        {
            ListItem li = drop_CountWay.Items.FindByValue(Request.QueryString["CountWay"]);
            if (li != null)
            {
                drop_CountWay.ClearSelection();
                li.Selected = true;
            }
        }
        //统计条件
        if (drop_CountWay.SelectedValue == "year") //按年
        {
            if (!string.IsNullOrEmpty(dr_Year.SelectedValue))
            {
                DateTime StartTime = new DateTime(EConvert.ToInt(dr_Year.SelectedValue), 1, 1);
                DateTime EndTime = new DateTime(EConvert.ToInt(dr_Year.SelectedValue) + 1, 1, 1);
                txtStartTime.Text = EConvert.ToShortDateString(StartTime);
                txtEndTime.Text = EConvert.ToShortDateString(EndTime.AddDays(-1));

            }
        }
        else if (drop_CountWay.SelectedValue == "quarter") //按季度
        {
            if (!string.IsNullOrEmpty(dr_Year.SelectedValue))
            {
                DateTime StartTime = new DateTime(EConvert.ToInt(dr_Year.SelectedValue), 1, 1);
                DateTime EndTime = new DateTime(EConvert.ToInt(dr_Year.SelectedValue) + 1, 1, 1);
                switch (dr_Quarter.SelectedValue)
                {
                    case "1":

                        EndTime = new DateTime(EConvert.ToInt(dr_Year.SelectedValue), 4, 1);
                        break;
                    case "2":
                        StartTime = new DateTime(EConvert.ToInt(dr_Year.SelectedValue), 4, 1);
                        EndTime = new DateTime(EConvert.ToInt(dr_Year.SelectedValue), 7, 1);

                        break;
                    case "3":
                        StartTime = new DateTime(EConvert.ToInt(dr_Year.SelectedValue), 7, 1);
                        EndTime = new DateTime(EConvert.ToInt(dr_Year.SelectedValue), 10, 1);

                        break;
                    case "4":
                        StartTime = new DateTime(EConvert.ToInt(dr_Year.SelectedValue), 10, 1);
                        break;
                }
                txtStartTime.Text = EConvert.ToShortDateString(StartTime);
                txtEndTime.Text = EConvert.ToShortDateString(EndTime.AddDays(-1));
            }
        }
        else if (drop_CountWay.SelectedValue == "month") //按月
        {
            if (!string.IsNullOrEmpty(dr_Month.SelectedValue))
            {
                DateTime StartTime = new DateTime(EConvert.ToInt(dr_Year.SelectedValue), EConvert.ToInt(dr_Month.SelectedValue), 1);
                DateTime EndTime = new DateTime(EConvert.ToInt(dr_Year.SelectedValue), EConvert.ToInt(dr_Month.SelectedValue), 1).AddMonths(1);
                txtStartTime.Text = EConvert.ToShortDateString(StartTime);
                txtEndTime.Text = EConvert.ToShortDateString(EndTime.AddDays(-1));
            }

        }
        else
        {
            if (!string.IsNullOrEmpty(Request.QueryString["FStartTime"]))
            {
                txtStartTime.Text = Request.QueryString["FStartTime"];

            }

            if (!string.IsNullOrEmpty(Request.QueryString["FEndTime"]))
            {
                txtEndTime.Text = Request.QueryString["FEndTime"];

            }
        }
        if (!string.IsNullOrEmpty(txtStartTime.Text))
        {
            result = result.Where(t => t.FAppDate >= EConvert.ToDateTime(txtStartTime.Text));
        }
        if (!string.IsNullOrEmpty(txtEndTime.Text))
        {
            result = result.Where(t => t.FAppDate <= EConvert.ToDateTime(txtEndTime.Text));
        }
        var v = result.ToList();
        DataTable dt = rc.GetTable(sb.ToString(), rc.ConvertParameters(sl));
        dt.Columns.Add("FCount");
        dt.Columns.Add("Money");
        dt.Columns.Add("JzMoney");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int FCount = v.Where(t => t.FCityId.ToString().StartsWith(EConvert.ToString(dt.Rows[i]["FNumber"]))).Count();
            dt.Rows[i]["FCount"] = FCount;

            double Money = EConvert.ToDouble(v.Where(t => t.FCityId.ToString().StartsWith(EConvert.ToString(dt.Rows[i]["FNumber"]))).Sum(t => t.FMoney));
            dt.Rows[i]["Money"] = Money;
            if (FManageTypeId == 283)
            {
                Money = EConvert.ToDouble(v.Where(t => t.FCityId.ToString().StartsWith(EConvert.ToString(dt.Rows[i]["FNumber"]))).Sum(t => t.FJZMoney));
                dt.Rows[i]["JzMoney"] = Money;//见证合同金额（万元）
            }

        }
        if (FManageTypeId == 283)
        {
            DG_List.Columns[3].HeaderText = "勘察合同金额（万元）";
            DG_List.Columns[4].HeaderText = "见证合同金额（万元）";
            DG_List.Columns[4].Visible = true;
        }
        else
        {
            DG_List.Columns[4].Visible = false;
        }


        if (DeptNumber.Length > 2 && DeptNumber != EConvert.ToString(Session["DFId"]))
        {
            btnReturn.Visible = true;
        }
        else
        {
            btnReturn.Visible = false;

        }

        switch (Request.QueryString["type"])
        {
            case "1":
                break;
            case "2":
                getPieXml(dt);
                break;
            default:
                DG_List.DataSource = dt;
                DG_List.DataBind();
                break;
        }
    }
    //默认颜色
    public string[] c = new string[] { "#29B7FC", "#FE920C", "#96C42D", "#FE4B60", "#DE4945", "#BE65FD", "#FCFA51", "#95CC28", "#DDF1FA" };
    private void getPieXml(DataTable dt)
    {
        DateTime dTime = SecurityEncryption.GetTime(Request.QueryString["dTime"]);
        DateTime StartTime = new DateTime(dTime.Year, dTime.Month, dTime.Day);
        DateTime EndTime = StartTime.AddDays(1);
        RCenter rc = new RCenter();



        StringBuilder sb = new StringBuilder();
        sb.Append("<chart caption='合同金额按地区统计' pieRadius='120' ");
        sb.Append(" enableSmartLabels='1' skipOverlapLabels='1' ");
        sb.Append("animation='1' showValues='0' decimals='2' startingAngle='50' enableRotation='1' ");
        sb.Append("bgColor='FAFAFA' showBorder='0'  baseFontSize='12'>");
        //换行数据显示样式
        decimal count = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            count = dt.AsEnumerable().Sum(t => EConvert.ToDecimal(t["Money"]));
        }
        int j = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {

      

            var value = EConvert.ToDecimal(dt.Rows[i]["Money"]);
            if (value != null)
            {
                decimal rage = 0;
                if (count > 0)
                {
                    rage = Math.Round((value / count) * 100, 2);
                }
                sb.Append("<set " + (i == 0 ? "isSliced='1'" : "") + " color='" + c[(j >= c.Length ? (new Random(j)).Next(0, c.Length) : j)] + "' name='" + dt.Rows[i]["FName"] + "' value='" + value + "'hoverText='" + dt.Rows[i]["FName"] + "\n" + value+ "万元\n" + rage + "%' />");
                
            }



            j++;
        }
        sb.Append("</chart>");
        Response.ContentType = "text/xml";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.Clear();
        Response.Write(sb.ToString());
        Response.End();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {

            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
            string FNumber = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
            if (FNumber.Length > 4)
            {

                e.Item.Cells[1].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
            }

            e.Item.Cells[2].Text = "<a href='../NewAppMain/CertiList.aspx?FManageTypeId=" + Request.QueryString["FManageTypeId"] + "&DeptId=" + FNumber
                + "&FStartTime=" + txtStartTime.Text + "&FEndTime=" + txtEndTime.Text + "'>"
                + e.Item.Cells[2].Text + "</a>";

        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }

    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            DeptNumber = EConvert.ToString(e.CommandArgument);
            showInfo();
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {

        DeptNumber = EConvert.ToString(Session["DFId"]);
        if (!string.IsNullOrEmpty(Request.QueryString["DeptId"]))
        {
            DeptNumber = Request.QueryString["DeptId"];
        }

        showInfo();
    }
}
