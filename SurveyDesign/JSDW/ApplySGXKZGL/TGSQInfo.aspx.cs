using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplySGXKZGL_TGSQInfo : System.Web.UI.Page
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
        sb.Append(" select a.*,tf.FID as FIID,tf.FTFGRQ,tf.FYY,tf.FYJSJFGRQ from TC_SGXKZ_PrjInfo a ");
        sb.Append(" left join TC_SGXKZ_TFG tf on tf.FAppId=a.FAppId ");
        sb.Append("  where isnull(tf.FType,0) = 0 and isnull(tf.FCLZT,0) = 0  and ");
        sb.Append("  a.FAppId= '");
        sb.Append(t_fLinkId.Value);
        sb.Append("'");
        string sql = sb.ToString();
        DataTable dt = GetData(sql);
        for (int i = 0; i < 1; i++)
        {
            t_PrjItemName.Text = EConvert.ToString(dt.Rows[i]["PrjItemName"]);
            t_JSDW.Text = EConvert.ToString(dt.Rows[i]["JSDW"]);
            t_FZTime.Text = EConvert.ToString(dt.Rows[i]["FZTime"]);
            t_FZJG.Text = EConvert.ToString(dt.Rows[i]["FZJG"]);
            t_TGDate.Text = EConvert.ToString(dt.Rows[i]["FTFGRQ"]);
            t_YJFGDate.Text = EConvert.ToString(dt.Rows[i]["FYJSJFGRQ"]);

            t_TGYYA.Text = EConvert.ToString(dt.Rows[i]["FYY"]);
            fid.Value = EConvert.ToString(dt.Rows[i]["FIID"]);
         


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
                string sql = "SELECT 1 AS ONE;";
                    string fid=this.fid.Value.ToString();
                    if (string.IsNullOrEmpty(fid))
                    {
                        fid = Guid.NewGuid().ToString() ;
                        sql = " INSERT INTO TC_SGXKZ_TFG (FId,FTFGRQ,FYJSJFGRQ,FYY,FAppId,FType,FCLZT) VALUES ('" + fid + "',";
                        sql += "'"+t_TGDate.Text+" 00:00:00',";
                        sql += " '" + t_YJFGDate.Text + " 00:00:00',";
                        sql += " '" + t_TGYYA.Text + "','"+t_fLinkId.Value+"',0,0)";
                       
                    }
                    else
                    {

                        sql = "UPDATE  TC_SGXKZ_TFG SET FTFGRQ = '" + t_TGDate.Text + " 00:00:00',FYJSJFGRQ" + t_YJFGDate.Text + " 00:00:00',FYY" +t_TGYYA.Text+ " WHERE FId='" + fid + "'";
                    }
                 
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                this.fid.Value = fid;
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