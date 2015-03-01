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

public partial class Government_statis_ContractCount : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    public DataTable dt = new DataTable();
    ProjectDB db = new ProjectDB();
    Dictionary<int, string> ContractType = new Dictionary<int, string>();
    List<CF_Prj_Certi> Contract = new List<CF_Prj_Certi>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ContractType.Add(280, "项目勘察");
        ContractType.Add(28001, "项目勘察见证");
        ContractType.Add(291, "初步设计");
        ContractType.Add(296, "施工图设计文件编制");
        ContractType.Add(287, "勘察文件审查");
        ContractType.Add(300, "施工图设计文件审查");

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


            sb.Append(" and (fnumber like '" + DeptNumber + "%' and flevel=2)");

        }
        else if (DeptNumber.Length == 4)
        {


            sb.Append(" and (fnumber like '" + DeptNumber + "%' and flevel=3)");

        }
        else
        {
            sl.Add("FParentId", DeptNumber);
            sb.Append(" and  FNumber=@FParentId ");
        }
        sb.Append(" order by FNumber ");


        var result = db.CF_Prj_Certi.Where(t => t.FIsValid == 1 && (t.FCityId.ToString().StartsWith(DeptNumber)
            && (t.FCertiTypeId == 411 || t.FCertiTypeId == 412 || t.FCertiTypeId == 413 || t.FCertiTypeId == 414 )
          ||  t.FCertiTypeId == 421 || t.FCertiTypeId == 422 || t.FCertiTypeId == 423 || t.FCertiTypeId == 424
            ));

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
        Contract = v;
        //ViewState["Contract"] = v;
        DataTable dt = rc.GetTable(sb.ToString(), rc.ConvertParameters(sl));
        //dt.Columns.Add("FCount");
        //dt.Columns.Add("Money");
        //dt.Columns.Add("JzMoney");
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    int FCount = v.Where(t => t.FCityId.ToString().StartsWith(EConvert.ToString(dt.Rows[i]["FNumber"]))).Count();
        //    dt.Rows[i]["FCount"] = FCount;

        //    double Money = EConvert.ToDouble(v.Where(t => t.FCityId.ToString().StartsWith(EConvert.ToString(dt.Rows[i]["FNumber"]))).Sum(t => t.FMoney));
        //    dt.Rows[i]["Money"] = Money;
        //    if (FManageTypeId == 283)
        //    {
        //        Money = EConvert.ToDouble(v.Where(t => t.FCityId.ToString().StartsWith(EConvert.ToString(dt.Rows[i]["FNumber"]))).Sum(t => t.FJZMoney));
        //        dt.Rows[i]["JzMoney"] = Money;//见证合同金额（万元）
        //    }

        //}
        //if (FManageTypeId == 283)
        //{
        //    DG_List.Columns[3].HeaderText = "勘察合同金额（万元）";
        //    DG_List.Columns[4].HeaderText = "见证合同金额（万元）";
        //    DG_List.Columns[4].Visible = true;
        //}
        //else
        //{
        //    DG_List.Columns[4].Visible = false;
        //}


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

        repHeader1.DataSource = ContractType;
        repHeader1.DataBind();

        repHeader2.DataSource = ContractType;
        repHeader2.DataBind();
        repItem.DataSource = ContractType;
        repItem.DataBind();
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
                sb.Append("<set " + (i == 0 ? "isSliced='1'" : "") + " color='" + c[(j >= c.Length ? (new Random(j)).Next(0, c.Length) : j)] + "' name='" + dt.Rows[i]["FName"] + "' value='" + value + "'hoverText='" + dt.Rows[i]["FName"] + "\n" + value + "万元\n" + rage + "%' />");

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
   
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
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
    protected void DG_List_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            DeptNumber = EConvert.ToString(e.CommandArgument);
            showInfo();
        }
    }
    protected void DG_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            BindRepContractType(e, "repHeader1");
            BindRepContractType(e, "repHeader2");
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            BindRepContractType(e, "repItem");

            if (Contract != null)
            {
                string CityId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem,"FNumber"));
                Literal liCount = e.Item.FindControl("liCount") as Literal;
                if (liCount != null)
                {
                    int FCount = Contract.Where(t => t.FCityId.ToString().StartsWith(CityId) 
                        && (t.FCertiTypeId == 411 || t.FCertiTypeId == 412 || t.FCertiTypeId == 413 || t.FCertiTypeId == 414)).Count();
                    liCount.Text = FCount.ToString();
                }
                Literal liMoney = e.Item.FindControl("liMoney") as Literal;
                if (liMoney != null)
                {
                    double? FMoney = Contract.Where(t => t.FCityId.ToString().StartsWith(CityId) 
                         && (t.FCertiTypeId == 411 || t.FCertiTypeId == 412 || t.FCertiTypeId == 413 || t.FCertiTypeId == 414)
                        ).Sum(t => t.FMoney);
                    liMoney.Text = FMoney.ToString();
                }
            }
        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {
            BindRepContractType(e, "repItem");
            Literal liCount = e.Item.FindControl("liCount") as Literal;
            if (liCount != null)
            {
                int FCount = Contract.Where(t =>(t.FCertiTypeId == 411 || t.FCertiTypeId == 412 || t.FCertiTypeId == 413 || t.FCertiTypeId == 414)).Count();
                liCount.Text = FCount.ToString();
            }
            Literal liMoney = e.Item.FindControl("liMoney") as Literal;
            if (liMoney != null)
            {
                double? FMoney = Contract.Where(t => (t.FCertiTypeId == 411 || t.FCertiTypeId == 412 || t.FCertiTypeId == 413 || t.FCertiTypeId == 414)
                    ).Sum(t => t.FMoney);
                liMoney.Text = FMoney.ToString();
            }
        }
    }
    private void BindRepContractType(RepeaterItemEventArgs e, string ControlName)
    {
        Repeater rep = e.Item.FindControl(ControlName) as Repeater;
        if (rep != null)
        {
            rep.DataMember = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
            rep.DataSource = ContractType;
            rep.DataBind();
        }
    }
    protected void repItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            if (Contract != null)
            {
                Repeater rep = ((Repeater)sender);
                Literal liCount = e.Item.FindControl("liCount") as Literal;
                string CityId = EConvert.ToString(rep.DataMember);
                HiddenField hfKey = e.Item.FindControl("hfKey") as HiddenField;
                if (hfKey != null)
                {
                    int FContractType = EConvert.ToInt(hfKey.Value);
                    if (liCount != null)
                    {
                        int FCount = Contract.Where(t => t.FCityId.ToString().StartsWith(CityId) && t.FContractType == FContractType  
                            && (t.FCertiTypeId == 411 || t.FCertiTypeId == 412 || t.FCertiTypeId == 413 || t.FCertiTypeId == 414 )).Count();
                        liCount.Text = FCount.ToString();
                    }
                    Literal liMoney = e.Item.FindControl("liMoney") as Literal;
                    if (liMoney != null)
                    {
                        double? FMoney = Contract.Where(t => t.FCityId.ToString().StartsWith(CityId) && t.FContractType == FContractType
                             && (t.FCertiTypeId == 411 || t.FCertiTypeId == 412 || t.FCertiTypeId == 413 || t.FCertiTypeId == 414 )
                            ).Sum(t => t.FMoney);
                        liMoney.Text = FMoney.ToString();
                    }
                }
            }
        }
    }
    protected void repItem1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            if (Contract != null)
            {
                Repeater rep = ((Repeater)sender);
                Literal liCount = e.Item.FindControl("liCount") as Literal;
                HiddenField hfKey = e.Item.FindControl("hfKey") as HiddenField;
                if (hfKey != null)
                {
                    int FContractType = EConvert.ToInt(hfKey.Value);
                    if (liCount != null)
                    {
                        int FCount = Contract.Where(t =>  t.FContractType == FContractType
                            && (t.FCertiTypeId == 421 || t.FCertiTypeId == 422 || t.FCertiTypeId == 423 || t.FCertiTypeId == 424)).Count();
                        liCount.Text = FCount.ToString();
                    }
                    Literal liMoney = e.Item.FindControl("liMoney") as Literal;
                    if (liMoney != null)
                    {
                        double? FMoney = Contract.Where(t => t.FContractType == FContractType
                             && (t.FCertiTypeId == 421 || t.FCertiTypeId == 422 || t.FCertiTypeId == 423 || t.FCertiTypeId == 424)
                            ).Sum(t => t.FMoney);
                        liMoney.Text = FMoney.ToString();
                    }
                }
            }
        }
    }
}
