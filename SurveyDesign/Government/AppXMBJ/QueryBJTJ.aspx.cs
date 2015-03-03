using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Data;

public partial class Government_AppXMBJ_QueryBJTJ : System.Web.UI.Page
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
		        count(*) BJNumber,
                sum(dbo.IsNumberOrResultNum(YDMJ)) YDMJ,
                sum(dbo.IsNumberOrResultNum(JZMJ)) JZMJ,
                sum(dbo.IsNumberOrResultNum(ZFTZ)) ZFTZ,
                sum(dbo.IsNumberOrResultNum(ZTZ)) ZTZ,
                sum(dbo.IsNumberOrResultNum(ZCTZ)) ZCTZ,
                sum(dbo.IsNumberOrResultNum(DKTZ)) DKTZ,
                sum(dbo.IsNumberOrResultNum(WSTZ)) WSTZ,
                sum(dbo.IsNumberOrResultNum(QTTZ)) QTTZ
            from YW_XMBJ group by XMSD";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                YWItem item = new YWItem();
                item.XMSD = row["XMSD"].ToString();
                item.BJNumber =Convert.ToInt32(row["BJNumber"].ToString());
                item.YDMJ = Convert.ToDouble(row["YDMJ"]);
                item.JZMJ = Convert.ToDouble(row["JZMJ"]);
                item.ZFTZ = Convert.ToDouble(row["ZFTZ"]);
                item.ZTZ = Convert.ToDouble(row["ZTZ"]);
                item.ZCTZ = Convert.ToDouble(row["ZCTZ"]);
                item.DKTZ = Convert.ToDouble(row["DKTZ"]);
                item.WSTZ = Convert.ToDouble(row["WSTZ"]);
                item.QTTZ = Convert.ToDouble(row["QTTZ"]);
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
                    dept.JZMJ = listData.Sum(x => x.JZMJ);
                    dept.ZFTZ = listData.Sum(x => x.ZFTZ);
                    dept.ZTZ = listData.Sum(x => x.ZTZ);
                    dept.ZCTZ = listData.Sum(x => x.ZCTZ);
                    dept.DKTZ = listData.Sum(x => x.DKTZ);
                    dept.WSTZ = listData.Sum(x => x.WSTZ);
                    dept.QTTZ = listData.Sum(x => x.QTTZ);
                    listSource.Add(dept);
                }
                else
                {
                    ManageDept dept = new ManageDept();
                    dept.FNumber = item.FNumber;
                    dept.FName = item.FName;
                    listSource.Add(dept);
                }
            });
            listSource = listSource.OrderBy(x => x.FNumber).ToList();
            ManageDept dept1 = new ManageDept();
            dept1.FName = "合计";
            dept1.YDMJ = listSource.Sum(x => x.YDMJ);
            dept1.JZMJ = listSource.Sum(x => x.JZMJ);
            dept1.ZFTZ = listSource.Sum(x => x.ZFTZ);
            dept1.ZTZ = listSource.Sum(x => x.ZTZ);
            dept1.ZCTZ = listSource.Sum(x => x.ZCTZ);
            dept1.DKTZ = listSource.Sum(x => x.DKTZ);
            dept1.WSTZ = listSource.Sum(x => x.WSTZ);
            dept1.QTTZ = listSource.Sum(x => x.QTTZ);
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
                    e.Item.Cells[1].Text = string.Format("<a href='QueryBJTJ.aspx?deptId={1}' target='_blank'>{0}</a>", e.Item.Cells[1].Text, deptId);
            }
        }
    }
}
public class ManageDept
{
    public string FNumber { get; set; }
    public string FName { get; set; }
    public string XMSD { get; set; }
    public int BJNumber { get; set; }
    public double YDMJ { get; set; } //用地面积
    public double JZMJ { get; set; }//建筑面积
    public double ZFTZ { get; set; }//政府投资
    public double ZTZ { get; set; }//总投资
    public double ZCTZ { get; set; }//自筹投资
    public double DKTZ { get; set; }//贷款投资
    public double WSTZ { get; set; }//外商投资
    public double QTTZ { get; set; }//其它投资
}
public class YWItem
{
    public string XMSD { get; set; }
    public int BJNumber { get; set; }
    public double YDMJ { get; set; } //用地面积
    public double JZMJ { get; set; }//建筑面积
    public double ZFTZ { get; set; }//政府投资
    public double ZTZ { get; set; }//总投资
    public double ZCTZ { get; set; }//自筹投资
    public double DKTZ { get; set; }//贷款投资
    public double WSTZ { get; set; }//外商投资
    public double QTTZ { get; set; }//其它投资
}