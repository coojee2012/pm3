using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using EgovaDAO;
using System.Data.SqlClient;
using System.Data;

public partial class Government_AppSGXKZGL_SDRYSC : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //base.Page_Load(sender, e);
            h_FAppId.Value = EConvert.ToString(Request["FAppId"]);
            h_FPrjItemId.Value = EConvert.ToString(Request["FPrjItemId"]);
           
            ShowInfo();
        }

    }
    private void ShowInfo()
    {
        using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
        {
            string sql = "SELECT  A.FIdCard,A.FHumanName,A.IsLock,SUM(A.SelectedCount) AS FCount,FAddress=STUFF((select ','+ISNULL([Address],'--') from TC_PrjItem_Info  where fid=A.FPrjItemId for xml path('')),1,1,'') ";
            sql += " FROM TC_PrjItem_Emp_Lock A ";
            sql += " WHERE A.FIdCard IN (SELECT  B.FIdCard FROM  TC_PrjItem_Emp AS B WHERE  B.FAppId='" + h_FAppId.Value + "')";
            sql += " Group BY A.FIdCard,A.FHumanName,A.IsLock,A.FPrjItemId ";

            sql += " union all SELECT  b.FIdCard, B.FHumanName,0 as IsLock ,0 As FCount,(SELECT Address from  TC_PrjItem_Info  where fid=B.FPrjItemId ) as Address";
            sql += " FROM  TC_PrjItem_Emp B WHERE  B.FAppId='" + h_FAppId.Value + "' AND B.FIdCard NOT IN ";
 sql += " (SELECT FIdCard FROM  TC_PrjItem_Emp_Lock )";
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(ds, "ds");
            DataTable dt = ds.Tables[0];
            JustAppInfo_List.DataSource = dt;
            JustAppInfo_List.DataBind();
           // SqlCommand cmd = new SqlCommand(sql, conn);
            //conn.Open();
            //int a = cmd.ExecuteNonQuery();
        }
       
        //this.Pager1.sql = sql ;
        //this.Pager1.controltype = "DataGrid";
        //this.Pager1.controltopage = "JustAppInfo_List";
        //this.Pager1.pagecount = 15;
        //this.Pager1.dataBind();
    }
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        { 
        
        }
    }
}