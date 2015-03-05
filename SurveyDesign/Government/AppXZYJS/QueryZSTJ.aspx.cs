using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Data;

public partial class Government_AppXZYJS_QueryZSTJ : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowInfo(DeptId);
        }
    }
    private void ShowInfo(string deptId)
    {
        if (string.IsNullOrEmpty(deptId))
            deptId = "51";
        string sql = string.Format("select FNumber,FName from CF_Sys_ManageDept where FNumber='{0}' or FParentId='{0}'",deptId);
        DataTable table = rc.GetTable(sql);
        List<YWItem> listItem = new List<YWItem>();
        List<ManageDept> listDept = new List<ManageDept>();
        List<ManageDept> listSource = new List<ManageDept>();
        if (table != null && table.Rows.Count > 0)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                listDept.Add(new ManageDept() { FNumber = row["FNumber"].ToString(), FName = row["FName"].ToString() });
            }
        }
        sql = @"select  XMSD, 
                        sum(dbo.IsNumberOrResultNum(JSGMMJ)) JSGMMJ,
                        sum(dbo.IsNumberOrResultNum(JSMJ)) JSMJ,
                        sum(case SFSW when 1 then 1 else 0 end) SFSW,
                        sum(case ProjectType when 1 then 1 else 0 end) FangJian,
                        sum(case ProjectType when 2 then 1 else 0 end) ShiZheng
              from YW_XZYJS group by XMSD";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                YWItem item = new YWItem();
                item.XMSD = row["XMSD"].ToString();
                item.JSGMMJ = Convert.ToDouble(row["JSGMMJ"]);
                item.JSMJ = Convert.ToDouble(row["JSMJ"]);
                item.SFSW = Convert.ToInt32(row["SFSW"]);
                item.FangJian = Convert.ToInt32(row["FangJian"]);
                item.ShiZheng = Convert.ToInt32(row["ShiZheng"]);
                listItem.Add(item);
            }
        }
        if (listDept.Any())
        {
            listDept.ForEach(item =>
            {
                var listData = listItem.FindAll(x => x.XMSD == item.FNumber);
                if (listData.Count > 0)
                {
                    ManageDept dept = new ManageDept();
                    dept.FNumber = item.FNumber;
                    dept.FName = item.FName;
                    dept.JSGMMJ = listData.Sum(x => x.JSGMMJ);
                    dept.JSMJ = listData.Sum(x => x.JSMJ);
                    dept.SFSW = listData.Sum(x => x.SFSW);
                    dept.FangJian = listData.Sum(x => x.FangJian);
                    dept.ShiZheng = listData.Sum(x => x.ShiZheng);
                    listSource.Add(dept);
                }
                else {
                    ManageDept dept = new ManageDept();
                    dept.FNumber = item.FNumber;
                    dept.FName = item.FName;
                    dept.JSGMMJ = 0.00;
                    dept.JSMJ = 0.00;
                    dept.SFSW = 0;
                    listSource.Add(dept);
                }
            });
            listSource = listSource.OrderBy(x => x.FNumber).ToList();
            ManageDept dept1 = new ManageDept();
            dept1.FName = "合计";
            dept1.JSGMMJ = listSource.Sum(x => x.JSGMMJ);
            dept1.JSMJ = listSource.Sum(x => x.JSMJ);
            dept1.SFSW = listSource.Sum(x => x.SFSW);
            dept1.FangJian = listSource.Sum(x => x.FangJian);
            dept1.ShiZheng = listSource.Sum(x => x.ShiZheng);
            listSource.Add(dept1);
        }
        dgList.DataSource = listSource;
        dgList.DataBind();
    }
    private string DeptId {
        get {
           return Request.QueryString["deptId"];
        }
    }
    protected void dgList_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
            if (string.IsNullOrEmpty(DeptId) && e.Item.ItemIndex > 0)
            {
                string deptId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
                if (!string.IsNullOrEmpty(deptId))
                    e.Item.Cells[1].Text = string.Format("<a href='QueryZSTJ.aspx?deptId={1}' target='_blank'>{0}</a>", e.Item.Cells[1].Text, deptId);
            }
        }
    }
}
public class ManageDept
{
    public string FNumber { get; set; }
    public string FName { get; set; }
    public string XMSD { get; set; }
    public double JSGMMJ { get; set; }
    public double JSMJ { get; set; }
    public int SFSW { get; set; }
    public int FangJian { get; set; }
    public int ShiZheng { get; set; }
}
public class YWItem
{
    public string XMSD { get; set; }
    public double JSGMMJ { get; set; }
    public double JSMJ { get; set; }
    public int SFSW { get; set; }
    public int FangJian { get; set; }
    public int ShiZheng { get; set; }
}