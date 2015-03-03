using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Data;

public partial class Government_AppYDGH_QueryZSTJ : System.Web.UI.Page
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
        string sql = string.Format("select FNumber,FName from CF_Sys_ManageDept where FNumber='{0}' or FParentId='{0}'", deptId);
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
        sql = @"select XMSD, sum(dbo.IsNumberOrResultNum(YDMJ)) YDMJ,
                    sum(case JZXZ when '30501' then 1 else 0 end) GJ,
                    sum(case JZXZ when '30502' then 1 else 0 end) KJ,
                    sum(case JZXZ when '30503' then 1 else 0 end) XJ,
                    sum(case JZXZ when '30504' then 1 else 0 end) HF,
                    sum(case JZXZ when '30505' then 1 else 0 end) QJ,
                    sum(case JZXZ when '30506' then 1 else 0 end) GQJ
              from YW_YDGH group by XMSD";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                YWItem item = new YWItem();
                item.XMSD = row["XMSD"].ToString();
                item.YDMJ = Convert.ToDouble(row["YDMJ"]);
                item.GJ = Convert.ToInt32(row["GJ"]);
                item.KJ = Convert.ToInt32(row["KJ"]);
                item.XJ = Convert.ToInt32(row["XJ"]);
                item.HF = Convert.ToInt32(row["HF"]);
                item.QJ = Convert.ToInt32(row["QJ"]);
                item.GQJ = Convert.ToInt32(row["GQJ"]);
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
                    dept.YDMJ = listData.Sum(x => x.YDMJ);
                    dept.GJ = listData.Sum(x => x.GJ);
                    dept.KJ = listData.Sum(x => x.KJ);
                    dept.XJ = listData.Sum(x => x.XJ);
                    dept.HF = listData.Sum(x => x.HF);
                    dept.QJ = listData.Sum(x => x.QJ);
                    dept.GQJ = listData.Sum(x => x.GQJ);
                    listSource.Add(dept);
                }
                else
                {
                    ManageDept dept = new ManageDept();
                    dept.FNumber = item.FNumber;
                    dept.FName = item.FName;
                    dept.YDMJ = 0.00;
                    dept.GJ = 0;
                    dept.KJ = 0;
                    dept.XJ = 0;
                    dept.HF = 0;
                    dept.QJ = 0;
                    dept.GQJ = 0;
                    listSource.Add(dept);
                }
            });
            listSource = listSource.OrderBy(x => x.FNumber).ToList();
            ManageDept dept1 = new ManageDept();
            dept1.FName = "合计";
            dept1.YDMJ = listSource.Sum(x => x.YDMJ);
            dept1.GJ = listSource.Sum(x => x.GJ);
            dept1.KJ = listSource.Sum(x => x.KJ);
            dept1.XJ = listSource.Sum(x => x.XJ);
            dept1.HF = listSource.Sum(x => x.HF);
            dept1.QJ = listSource.Sum(x => x.QJ);
            dept1.GQJ = listSource.Sum(x => x.GQJ);
            listSource.Add(dept1);
        }
        dgList.DataSource = listSource;
        dgList.DataBind();
    }
    private string DeptId
    {
        get
        {
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
    public double YDMJ { get; set; }
    public int GJ { get; set; } //改建
    public int KJ { get; set; }//扩建
    public int XJ { get; set; }//新建
    public int HF { get; set; }//恢复
    public int QJ { get; set; }//迁建
    public int GQJ { get; set; }//改迁建
}
public class YWItem
{
    public string XMSD { get; set; }
    public double YDMJ { get; set; }
    public int GJ { get; set; } //改建
    public int KJ { get; set; }//扩建
    public int XJ { get; set; }//新建
    public int HF { get; set; }//恢复
    public int QJ { get; set; }//迁建
    public int GQJ { get; set; }//改迁建
}