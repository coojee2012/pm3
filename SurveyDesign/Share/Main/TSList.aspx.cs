using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Share_Main_TSList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable table = new DataTable();
            table.Columns.Add("TSR",typeof(string));
            table.Columns.Add("TSSJ", typeof(string));
            table.Columns.Add("SZDW", typeof(string));
            table.Columns.Add("BTSQY", typeof(string));
            table.Columns.Add("LXDH", typeof(string));
            table.Columns.Add("Email", typeof(string));
            DataRow row = table.NewRow();
            row["TSR"] = "王成";
            row["TSSJ"] = "2014-11-15";
            row["SZDW"] = "川西南基建工程总公司";
            row["BTSQY"] = "四川彭州瑞信混凝土有限公司";
            row["LXDH"] = "18156940323";
            row["Email"] = "1195830254@qq.com";
            table.Rows.Add(row);
            DataRow row1 = table.NewRow();
            row1["TSR"] = "陈城";
            row1["TSSJ"] = "2014-11-18";
            row1["SZDW"] = "四川春雷建筑劳务有限公司";
            row1["BTSQY"] = "四川省百海置业有限公司";
            row1["LXDH"] = "13570856547";
            row1["Email"] = "12653345643@qq.com";
            table.Rows.Add(row1);
            DataRow row2 = table.NewRow();
            row2["TSR"] = "吴伟";
            row2["TSSJ"] = "2014-11-19";
            row2["SZDW"] = "四川乐源房地产开发有限公司";
            row2["BTSQY"] = "四川盛历建筑劳务有限公司";
            row2["LXDH"] = "13597695065";
            row2["Email"] = "5436544@qq.com";
            table.Rows.Add(row2);
            
            DG_List.DataSource = table;
            DG_List.DataBind();
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
        }
    }
}