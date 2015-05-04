using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class JSDW_ApplySGXKZGL_FGSQInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
    }

    protected void BindData()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select a.*,tf.FID as FIID, isnull(tf.FType,0) as FType,tf.FTFGRQ,tf.FYY,tf.FYJSJFGRQ from TC_SGXKZ_PrjInfo a ");
        sb.Append(" left join TC_SGXKZ_TFG tf on tf.FAppId=a.FAppId ");
        sb.Append("  where ");
        sb.Append("  a.FAppId= '");
        sb.Append(t_fLinkId.Value);
        sb.Append("' order by isnull(tf.FType,0) desc ");
        string sql = sb.ToString();
        DataTable dt = GetData(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            t_PrjItemName.Text = EConvert.ToString(dt.Rows[i]["PrjItemName"]);
            t_JSDW.Text = EConvert.ToString(dt.Rows[i]["JSDW"]);
            t_FZTime.Text = EConvert.ToString(dt.Rows[i]["FZTime"]);
            t_FZJG.Text = EConvert.ToString(dt.Rows[i]["FZJG"]);
            if (EConvert.ToString(dt.Rows[i]["FType"]) == "0")
            {
                fid.Value = "";
            }
            else
            {
                fid.Value = EConvert.ToString(dt.Rows[i]["FIID"]);
            }
            if (!string.IsNullOrEmpty(fid.Value))
            {
                t_TGDate.Text = DateTime.Parse(EConvert.ToString(dt.Rows[i]["FTFGRQ"])).ToString("yyyy-MM-dd");
                t_CXKGDate.Text = DateTime.Parse(EConvert.ToString(dt.Rows[i]["FYJSJFGRQ"])).ToString("yyyy-MM-dd");
            }

            t_FGYY.Text = EConvert.ToString(dt.Rows[i]["FYY"]);
           
            


            break;
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
                string fid = this.fid.Value.ToString();
                if (string.IsNullOrEmpty(fid))
                {
                    fid = Guid.NewGuid().ToString();
                    sql = " INSERT INTO TC_SGXKZ_TFG (FId,FTFGRQ,FYJSJFGRQ,FYY,FAppId,FType,FCLZT) VALUES ('" + fid + "',";
                    sql += "'" + t_TGDate.Text + " 00:00:00',";
                    sql += " '" + t_CXKGDate.Text + " 00:00:00',";
                    sql += " '" + t_FGYY.Text + "','" + t_fLinkId.Value + "',1,0)";

                }
                else
                {

                    sql = "UPDATE  TC_SGXKZ_TFG SET FTFGRQ = '" + t_TGDate.Text + " 00:00:00',FYJSJFGRQ = '" + t_CXKGDate.Text + " 00:00:00',FYY= '" + t_FGYY.Text + "' WHERE FId='" + fid + "'";
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

    protected void btnShangBao_Click(object sender, EventArgs e)
    {
        try
        {

            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string sql = "SELECT 1 AS ONE;";
                string fid = this.fid.Value.ToString();
                sql = "UPDATE  TC_SGXKZ_TFG SET FCLZT = 1 WHERE FId='" + fid + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                ScriptManager.RegisterClientScriptBlock(up_Main, up_Main.GetType(), "js", "alert('上报成功');", true);
            }


        }
        catch (Exception ee)
        {
            ScriptManager.RegisterClientScriptBlock(up_Main, up_Main.GetType(), "js", "alert('上报失败');", true);
        }
    }
}