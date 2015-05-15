using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppSGXKZGL_SGXKZZYSL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            issave.Value = "0";
            if (Request["FLinkId"] != null && !string.IsNullOrEmpty(Request["FLinkId"]))
            {
                t_fLinkId.Value = Request["FLinkId"].ToString();
                t_ffid.Value = getSLTZSinfo(t_fLinkId.Value);
            }
            if (Request["FId"] != null && !string.IsNullOrEmpty(Request["FId"]))
            {
                t_fProcessInstanceID.Value = Request["FId"].ToString();
            }

            BindData();
        }
        
    }

    /// <summary>
    /// 获取填写的受理通知书主键，并赋值给隐藏字段ffid
    /// </summary>
    /// <param name="fappid"></param>
    /// <returns></returns>
    private string getSLTZSinfo(string fappid)
    {
        string sql = @"select  GuidID  from YW_SLTZS where YWBM = '+" + fappid + "' ";
        DataTable dt = GetData(sql);
        if (dt != null&& dt.Rows.Count >0)
        {
            return dt.Rows[0][0].ToString();
        }
        return "";
    }

    protected void BindData()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select a.YWLX + a.PrjItemName+'施工许可证' as SLLR,a.FappId,ISNULL(b.GuidID,'') as FFId, a.PrjAddressDept,b.BH,b.LXR,b.LXDH,b.JDDH,");
        sb.Append(" (select count(1)+1 from YW_SLTZS where DATEDIFF(day,getdate(),SLRQ )=0 ) as SLXH");
        sb.Append(" from V_SGXKZ_YW  a ");
        sb.Append(" left join YW_SLTZS b on a.FAppId = b.YWBM ");
        sb.Append(" where FAppId= '");
        sb.Append(t_fLinkId.Value);
        sb.Append("'");
        string sql = sb.ToString();
        DataTable dt = GetData(sql);
        DateTime now = DateTime.Now;
        string today = now.ToString("yyMMdd");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string slxh = EConvert.ToString(dt.Rows[i]["SLXH"]);
            if (slxh.Length == 1)
                slxh = "000" + slxh;
            if (slxh.Length == 2)
                slxh = "00" + slxh;
            if (slxh.Length == 3)
                slxh = "0" + slxh;

            if (string.IsNullOrEmpty(EConvert.ToString(dt.Rows[i]["FFId"])))
            {
                t_BH.Text = EConvert.ToString(dt.Rows[i]["PrjAddressDept"]) + today + slxh;
            }
            else
            {
                t_BH.Text = EConvert.ToString(dt.Rows[i]["BH"]);
                t_ffid.Value = dt.Rows[i]["FFId"].ToString();
                issave.Value = "1";
            }
        
            t_RQ.Text = now.ToString("yyyy-MM-dd");
            t_NR.Text = EConvert.ToString(dt.Rows[i]["SLLR"]);
            t_LXR.Text = EConvert.ToString(dt.Rows[i]["LXR"]);
            t_LXDH.Text = EConvert.ToString(dt.Rows[i]["LXDH"]);
            t_JDDH.Text = EConvert.ToString(dt.Rows[i]["JDDH"]);
            break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            string guid = "";
            string sql = "";

            if (string.IsNullOrEmpty(t_ffid.Value))
            {
                guid = Guid.NewGuid().ToString();

                sql = "INSERT INTO YW_SLTZS (GuidID,YWBM,BH,LXR,LXDH,JDDH,SLRQ) VALUES  ( '" + guid + "','";
                sql += t_fLinkId.Value + "','" + t_BH.Text + "','";
                sql += t_LXR.Text + "','" + t_LXDH.Text + "','" + t_JDDH.Text + "',getdate())";

            }
            else
            {
                guid = t_ffid.Value;
                sql = "UPDATE YW_SLTZS SET LXR = '" + t_LXR.Text + "',LXDH='";
                sql += t_LXDH.Text + "',JDDH = '";
                sql += t_JDDH.Text + "'";
                sql += " WHERE GuidID='" + t_ffid.Value + "'";

            }
        using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        ScriptManager.RegisterClientScriptBlock(up_Main, up_Main.GetType(), "js", "$('#issave').val(1);$('#t_ffid').val('" + guid.ToString()+ "');alert('保存成功');", true);

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