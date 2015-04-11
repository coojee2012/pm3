using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppTFGGL_FGInfo : System.Web.UI.Page
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
        sb.Append(" select a.*,tf.FID as FIID, isnull(tf.FType,0) as FType,tf.FTFGRQ,tf.FYY,tf.FYJSJFGRQ,tf.FSHR,tf.FSHRQ,tf.FSHDW from TC_SGXKZ_PrjInfo a ");
        sb.Append(" left join TC_SGXKZ_TFG tf on tf.FAppId=a.FAppId ");
        sb.Append("  where ");
        sb.Append("  a.FAppId= '");
        sb.Append(t_fLinkId.Value);
        sb.Append("' and  isnull(tf.FType,0) = 1");
        string sql = sb.ToString();
        DataTable dt = GetData(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            t_PrjItemName.Text = EConvert.ToString(dt.Rows[i]["PrjItemName"]);
            t_JSDW.Text = EConvert.ToString(dt.Rows[i]["JSDW"]);
            t_FZTime.Text = EConvert.ToString(dt.Rows[i]["FZTime"]);
            t_FZJG.Text = EConvert.ToString(dt.Rows[i]["FZJG"]);
            if (string.IsNullOrEmpty(EConvert.ToString(dt.Rows[i]["FTFGRQ"])))
            {
                t_TGDate.Text = "";
            }
            else
            {
                t_TGDate.Text = DateTime.Parse(EConvert.ToString(dt.Rows[i]["FTFGRQ"])).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(EConvert.ToString(dt.Rows[i]["FYJSJFGRQ"])))
            {
                t_CXKGDate.Text = "";
            }
            else
            {
                t_CXKGDate.Text = DateTime.Parse(EConvert.ToString(dt.Rows[i]["FYJSJFGRQ"])).ToString("yyyy-MM-dd");
            }
       

            t_FGYY.Text = EConvert.ToString(dt.Rows[i]["FYY"]);

            t_SHDW.Text = EConvert.ToString(dt.Rows[i]["FSHDW"]);
            t_SHR.Text = EConvert.ToString(dt.Rows[i]["FSHR"]);
            if (string.IsNullOrEmpty(EConvert.ToString(dt.Rows[i]["FSHRQ"])))
            {
                t_SHRQ.Text = "";
            }
            else
            {
                t_SHRQ.Text = DateTime.Parse(EConvert.ToString(dt.Rows[i]["FSHRQ"])).ToString("yyyy-MM-dd");
            }
          

            if (EConvert.ToString(dt.Rows[i]["FType"]) == "0")
            {
                fid.Value = "";
            }
            else
            {
                fid.Value = EConvert.ToString(dt.Rows[i]["FIID"]);
            }



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
                string fid = this.t_fProcessInstanceID.Value.ToString();


                sql = "UPDATE  TC_SGXKZ_TFG SET FSHR = '" + t_SHR.Text + "',FSHRQ = '" + t_SHRQ.Text + " 00:00:00',FSHDW= '" + t_SHDW.Text + "',FCLZT = 2 WHERE FId='" + fid + "'";


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