using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppSGXKZGL_JSDWXX : System.Web.UI.Page
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

        init();
    }
    protected void init()
    {
        BindBaseInfo();
        BindDwInfo();
        BindRyInfo();
    }

    protected void BindBaseInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select b.JSDWDM ,c.FName,dbo.getManageDeptName(a.PrjAddressDept) as PrjAddressDeptName, a.*  from TC_SGXKZ_PrjInfo a ");
        sb.Append(" left join TC_PrjItem_Info b  on a.FPrjItemId = b.FId ");
        sb.Append(" left join CF_Sys_Dic c on a.JSDWXZ = c.FCNumber ");
        sb.Append(" where a.FAppId= '");
        sb.Append(t_fLinkId.Value);
        sb.Append("'");
        string sql = sb.ToString();
        DataTable dt = GetData(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            t_DWMC.Text = EConvert.ToString(dt.Rows[i]["JSDW"]);
            t_Address.Text = EConvert.ToString(dt.Rows[i]["JSDWDZ"]);
            t_DWXZ.Text = EConvert.ToString(dt.Rows[i]["FName"]);
            t_SSD.Text = EConvert.ToString(dt.Rows[i]["PrjAddressDeptName"]);
            t_FRDB.Text = EConvert.ToString(dt.Rows[i]["FDDBR"]);
            t_FRSJH.Text = EConvert.ToString(dt.Rows[i]["FRDH"]);

            t_LXR.Text = EConvert.ToString(dt.Rows[i]["LZR"]);
            t_LXRDH.Text = EConvert.ToString(dt.Rows[i]["LXDH"]);
            t_ZZJGDM.Text = EConvert.ToString(dt.Rows[i]["JSDWDM"]);
            t_YYZZZCH.Text = "";
            t_Email.Text = "";
            t_Memo.Text = "";
            break;
        }
    }

    protected void BindDwInfo()
    {
        
    }

    protected void BindRyInfo()
    {
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