using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppKJGGL_JGSZ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["FAppId"] != null && !string.IsNullOrEmpty(Request["FAppId"]))
        {
            t_fLinkId.Value = Request["FAppId"].ToString();
        }
        if (Request["FId"] != null && !string.IsNullOrEmpty(Request["FId"]))
        {
            t_fProcessInstanceID.Value = Request["FId"].ToString();
        }
        BindData();
    }

    protected void BindData()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * from TC_SGXKZ_PrjInfo where FAppId= '");
        sb.Append(t_fLinkId.Value);
        sb.Append("'");
        string sql = sb.ToString();
        DataTable dt = GetData(sql);
        for (int i = 0; i < 1; i++)
        {
            t_PrjItemName.Text = EConvert.ToString(dt.Rows[i]["PrjItemName"]);
            t_JSDW.Text = EConvert.ToString(dt.Rows[i]["JSDW"]);
            t_EndDate.Text = EConvert.ToString(dt.Rows[i]["EndDate"]);


        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string sql = "UPDATE TC_SGXKZ_PrjInfo SET SJEndDate = '" + t_SJEndDate.Text + " 23:59:59' WHERE FAppId='" + t_fLinkId.Value + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                ScriptManager.RegisterClientScriptBlock(up_Main, up_Main.GetType(), "js", "alert('保存成功');", true);
            }


        }
        catch (Exception ee)
        {
            ScriptManager.RegisterClientScriptBlock(up_Main, up_Main.GetType(), "js", "alert('保存失败');", true);
        }

    }

    protected DataTable GetData(string sql)
    {
        using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(ds, "ds");
            DataTable dt = ds.Tables[0];

            return dt;
        }
    }
}