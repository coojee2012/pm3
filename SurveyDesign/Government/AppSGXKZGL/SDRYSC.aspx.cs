using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using System.Data.SqlClient;
using System.Data;
using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleApp;
using EgovaDAO;
using EgovaBLL;
using ProjectBLL;
using Tools;
using System.Data.SqlClient;

public partial class Government_AppSGXKZGL_SDRYSC : System.Web.UI.Page
{
    RCenter rc = new RCenter();
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
            //string sql = "SELECT  A.FIdCard,A.FHumanName,A.IsLock,SUM(A.SelectedCount) AS FCount,FAddress=STUFF((select ','+ISNULL([Address],'--') from TC_PrjItem_Info  where fid=A.FPrjItemId for xml path('')),1,1,'') ";
            //sql += " FROM TC_PrjItem_Emp_Lock A ";
            //sql += " WHERE A.FIdCard IN (SELECT  B.FIdCard FROM  TC_PrjItem_Emp AS B WHERE  B.FAppId='" + h_FAppId.Value + "')";
            //sql += " Group BY A.FIdCard,A.FHumanName,A.IsLock,A.FPrjItemId ";

            //sql += " union all SELECT  b.FIdCard, B.FHumanName,0 as IsLock ,0 As FCount,(SELECT Address from  TC_PrjItem_Info  where fid=B.FPrjItemId ) as Address";
            //sql += " FROM  TC_PrjItem_Emp B WHERE  B.FAppId='" + h_FAppId.Value + "' AND B.FIdCard NOT IN ";
            //sql += " (SELECT FIdCard FROM  TC_PrjItem_Emp_Lock )";

//            string sql = @"  SELECT  A.FIdCard,A.FHumanName,count(1) AS FCount
//                        ,FAddress=STUFF((select ','+ISNULL([Address],'--') from TC_PrjItem_Info 
//                            where fid=A.FPrjItemId for xml path('')),1,1,'')  
//                            FROM TC_PrjItem_Emp A  
//                            WHERE  a.FAppId = '" + h_FAppId.Value + "'  and a.FEntType in ('2','3','4','7')"
//                            +" Group BY A.FIdCard,A.FHumanName,A.FPrjItemId  ";
            string sql = @"SELECT  A.FIdCard,A.FHumanName,0  FCount,[dbo].[getManageDeptName](B.PrjAddressDept)  FAddress,dbo.getemptypename(A.EmpType,'112202') EmpType 
                            FROM TC_PrjItem_Emp A,TC_SGXKZ_PrjInfo B
							WHERE A.FAppId = B.FAppId							
                            AND  a.FAppId = '" +h_FAppId.Value+"'"+
							" and a.FEntType in ('2','3','4','7')   Group BY A.FIdCard,A.FHumanName,A.FPrjItemId,B.PrjAddressDept,A.EmpType   ";  //排除勘察、设计的人员，此类人员不用锁定

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
            string fidcard = e.Item.Cells[4].Text;
            string sql = @"select  count(1)  from  TC_PrjItem_Emp_Lock  where ltrim(rtrim(FIdCard)) = '"+fidcard.Trim()+"' and IsLock = 1";
            DataTable dt = rc.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                Label lbl_count = e.Item.FindControl("lbl_sdcount") as Label;
                lbl_count.Text = dt.Rows.Count.ToString();
            }
        }
    }
}