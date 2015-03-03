using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Data;

public partial class Government_AppJGYS_QueryZSTJ : System.Web.UI.Page
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
        sql = @"select XMSD,
                    sum(case GCYT when '200010101' then 1 else 0 end) JZJZ,
                    sum(case GCYT when '200010102' then 1 else 0 end) JZJZPTGC,
                    sum(case GCYT when '200010103' then 1 else 0 end) GGJZ,
                    sum(case GCYT when '200010104' then 1 else 0 end) BGJZ,
                    sum(case GCYT when '200010105' then 1 else 0 end) LYJZ,
                    sum(case GCYT when '200010106' then 1 else 0 end) JKWWJZ,
                    sum(case GCYT when '200010107' then 1 else 0 end) JTYSL,
                    sum(case GCYT when '200010108' then 1 else 0 end) TXJZ,
                    sum(case GCYT when '200010109' then 1 else 0 end) GGJZPTGC,
                    sum(case GCYT when '200010110' then 1 else 0 end) SZL,
                    sum(case GCYT when '200010111' then 1 else 0 end) NYJZ,
                    sum(case GCYT when '200010112' then 1 else 0 end) NYJZPTGC,
                    sum(case GCYT when '200010103' then 1 else 0 end) GYJZ,
                    sum(case GCYT when '200010114' then 1 else 0 end) GYJZPTGC,
                    sum(case GCYT when '200010115' then 1 else 0 end) QT
                from YW_JGYS group by XMSD";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                YWItem item = new YWItem();
                item.XMSD = row["XMSD"].ToString();
                item.JZJZ = Convert.ToInt32(row["JZJZ"]);
                item.JZJZPTGC = Convert.ToInt32(row["JZJZPTGC"]);
                item.GGJZ = Convert.ToInt32(row["GGJZ"]);
                item.BGJZ = Convert.ToInt32(row["BGJZ"]);
                item.LYJZ = Convert.ToInt32(row["LYJZ"]);
                item.JKWWJZ = Convert.ToInt32(row["JKWWJZ"]);
                item.TXJZ = Convert.ToInt32(row["TXJZ"]);
                item.GGJZPTGC = Convert.ToInt32(row["GGJZPTGC"]);
                item.SZL = Convert.ToInt32(row["SZL"]);
                item.NYJZ = Convert.ToInt32(row["NYJZ"]);
                item.NYJZPTGC = Convert.ToInt32(row["NYJZPTGC"]);
                item.GYJZ = Convert.ToInt32(row["GYJZ"]);
                item.GYJZPTGC = Convert.ToInt32(row["GYJZPTGC"]);
                item.QT = Convert.ToInt32(row["QT"]);
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
                    dept.JZJZ = listData.Sum(x => x.JZJZ);
                    dept.JZJZPTGC = listData.Sum(x => x.JZJZPTGC);
                    dept.GGJZ = listData.Sum(x => x.GGJZ);
                    dept.BGJZ = listData.Sum(x => x.BGJZ);
                    dept.LYJZ = listData.Sum(x => x.LYJZ);
                    dept.JKWWJZ = listData.Sum(x => x.JKWWJZ);
                    dept.TXJZ = listData.Sum(x => x.TXJZ);
                    dept.GGJZPTGC = listData.Sum(x => x.GGJZPTGC);
                    dept.SZL = listData.Sum(x => x.SZL);
                    dept.NYJZ = listData.Sum(x => x.NYJZ);
                    dept.NYJZPTGC = listData.Sum(x => x.NYJZPTGC);
                    dept.GYJZ = listData.Sum(x => x.GYJZ);
                    dept.GYJZPTGC = listData.Sum(x => x.GYJZPTGC);
                    dept.QT = listData.Sum(x => x.QT);
                    listSource.Add(dept);
                }
                else
                {
                    ManageDept dept = new ManageDept();
                    dept.FNumber = item.FNumber;
                    dept.FName = item.FName;
                    //dept.GJ = 0;
                    //dept.KJ = 0;
                    //dept.XJ = 0;
                    //dept.QJ = 0;
                    //dept.GQJ = 0;
                    listSource.Add(dept);
                }
            });
            listSource = listSource.OrderBy(x => x.FNumber).ToList();
            ManageDept dept1 = new ManageDept();
            dept1.FName = "合计";
            dept1.JZJZ = listSource.Sum(x => x.JZJZ);
            dept1.JZJZPTGC = listSource.Sum(x => x.JZJZPTGC);
            dept1.GGJZ = listSource.Sum(x => x.GGJZ);
            dept1.BGJZ = listSource.Sum(x => x.BGJZ);
            dept1.LYJZ = listSource.Sum(x => x.LYJZ);
            dept1.JKWWJZ = listSource.Sum(x => x.JKWWJZ);
            dept1.TXJZ = listSource.Sum(x => x.TXJZ);
            dept1.GGJZPTGC = listSource.Sum(x => x.GGJZPTGC);
            dept1.SZL = listSource.Sum(x => x.SZL);
            dept1.NYJZ = listSource.Sum(x => x.NYJZ);
            dept1.NYJZPTGC = listSource.Sum(x => x.NYJZPTGC);
            dept1.GYJZ = listSource.Sum(x => x.GYJZ);
            dept1.GYJZPTGC = listSource.Sum(x => x.GYJZPTGC);
            dept1.QT = listSource.Sum(x => x.QT);
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
    public int JZJZ { get; set; }//居住建筑
    public int JZJZPTGC { get; set; } //居住建筑配套工程
    public int GGJZ { get; set; }//公共建筑
    public int BGJZ { get; set; }//办公建筑
    public int LYJZ { get; set; }//旅游建筑
    public int JKWWJZ { get; set; }//教科文卫建筑
    public int JTYSL { get; set; }//交通运输类
    public int TXJZ { get; set; }//通信建筑
    public int GGJZPTGC { get; set; }//公共建筑配套工程
    public int SZL { get; set; }//商住楼
    public int NYJZ { get; set; }//农业建筑
    public int NYJZPTGC { get; set; }//农业建筑配套工程
    public int GYJZ { get; set; }//工业建筑
    public int GYJZPTGC { get; set; }//工业建筑配套工程
    public int QT { get; set; }//其它
}
public class YWItem
{
    public string XMSD { get; set; }
    public int JZJZ { get; set; }//居住建筑
    public int JZJZPTGC { get; set; } //居住建筑配套工程
    public int GGJZ { get; set; }//公共建筑
    public int BGJZ { get; set; }//办公建筑
    public int LYJZ { get; set; }//旅游建筑
    public int JKWWJZ { get; set; }//教科文卫建筑
    public int JTYSL { get; set; }//交通运输类
    public int TXJZ { get; set; }//通信建筑
    public int GGJZPTGC { get; set; }//公共建筑配套工程
    public int SZL { get; set; }//商住楼
    public int NYJZ { get; set; }//农业建筑
    public int NYJZPTGC { get; set; }//农业建筑配套工程
    public int GYJZ { get; set; }//工业建筑
    public int GYJZPTGC { get; set; }//工业建筑配套工程
    public int QT { get; set; }//其它
}