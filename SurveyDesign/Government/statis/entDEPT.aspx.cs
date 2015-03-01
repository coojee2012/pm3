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
using System.Configuration;
public partial class Government_statis_entDEPT : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    
    public DataTable dt = new DataTable();
    ProjectDB db = new ProjectDB();
    Dictionary<int, string> ContractType = new Dictionary<int, string>();
    List<CF_Ent_BaseInfo> Contract = new List<CF_Ent_BaseInfo>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ContractType.Add(100, "建设单位");
        ContractType.Add(126, "见证单位");
        ContractType.Add(145, "审图机构");
        ContractType.Add(15501, "勘察企业");
        ContractType.Add(155, "设计企业");
        

        if (!Page.IsPostBack)
        {
            DeptNumber = EConvert.ToString(Session["DFId"]);
            if (!string.IsNullOrEmpty(Request.QueryString["DeptId"]))
            {
                DeptNumber = Request.QueryString["DeptId"];
            }
            
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

        
    
        var result = db.CF_Ent_BaseInfo.Where(t => t.FIsDeleted == false && (t.FRegistDeptId.ToString().StartsWith(DeptNumber)));

       
        //统计条件
        
        var v = result.ToList();
        Contract = v;
        
        DataTable dt = rc.GetTable(sb.ToString(), rc.ConvertParameters(sl));
        

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
                string CityId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
                Literal liCount = e.Item.FindControl("liCount") as Literal;
                if (liCount != null)
                {
                    int FCount = Contract.Where(t => t.FRegistDeptId.ToString().StartsWith(CityId)).Count();
                    liCount.Text = FCount.ToString();
                }
                
            }
        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {
            BindRepContractType(e, "repItem");
            Literal liCount = e.Item.FindControl("liCount") as Literal;
            if (liCount != null)
            {
                int FCount = Contract.Count();
                liCount.Text = FCount.ToString();
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
                        int FCount = Contract.Where(t => t.FRegistDeptId.ToString().StartsWith(CityId) && t.FSystemId == FContractType).Count();
                        liCount.Text = FCount.ToString();
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
                        int FCount = Contract.Where(t => t.FRegistDeptId == FContractType).Count();
                        liCount.Text = FCount.ToString();
                    }
                    
                }
            }
        }
    }
}
