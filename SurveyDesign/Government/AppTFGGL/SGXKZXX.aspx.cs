using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Government_AppTFGGL_SGXKZXX : System.Web.UI.Page
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
        sb.Append(" select * from TC_SGXKZ_PrjInfo where FAppId= '");
        sb.Append(t_fLinkId.Value);
        sb.Append("'");
        string sql = sb.ToString();
        DataTable dt = GetData(sql);
        for (int i = 0; i < 1; i++)
        {
            t_SGXKZBH.Text = EConvert.ToString(dt.Rows[i]["SGXKZBH"]);
            t_Price.Text = EConvert.ToString(dt.Rows[i]["Price"]);
            t_FZTime.Text = EConvert.ToString(dt.Rows[i]["FZTime"]);
            t_FZJG.Text = EConvert.ToString(dt.Rows[i]["FZJG"]);
            t_PrjItemName.Text = EConvert.ToString(dt.Rows[i]["PrjItemName"]);
            t_JSDW.Text = EConvert.ToString(dt.Rows[i]["JSDW"]);
            t_Address.Text = EConvert.ToString(dt.Rows[i]["Address"]);
            t_Area.Text = EConvert.ToString(dt.Rows[i]["Area"]);
            t_Height.Text = "";
            t_JSFZR.Text = EConvert.ToString(dt.Rows[i]["JSFZR"]);
            t_StartDate.Text = EConvert.ToString(dt.Rows[i]["StartDate"]);
            t_EndDate.Text = EConvert.ToString(dt.Rows[i]["EndDate"]);

        }
    }

    protected void BindDwInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * from TC_PrjItem_Ent where FAppId = '");
        sb.Append(t_fLinkId.Value);
        sb.Append("'");
        string sql = sb.ToString();
        DataTable dt = GetData(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //施工总承包商单位
            if (EConvert.ToString(dt.Rows[i]["FEntType"]) == "2")
            {
                if (String.IsNullOrEmpty(t_SGDW.Text))
                    t_SGDW.Text = EConvert.ToString(dt.Rows[i]["FName"]);
                else
                    t_SGDW.Text += "," + EConvert.ToString(dt.Rows[i]["FName"]);
            }
            //专业承包单位
            else if (EConvert.ToString(dt.Rows[i]["FEntType"]) == "3")
            {

                if (String.IsNullOrEmpty(t_ZYFBDW.Text))
                    t_ZYFBDW.Text = EConvert.ToString(dt.Rows[i]["FName"]);
                else
                    t_ZYFBDW.Text += "," + EConvert.ToString(dt.Rows[i]["FName"]);


            }
            //劳务分包单位
            else if (EConvert.ToString(dt.Rows[i]["FEntType"]) == "4")
            {
                if (String.IsNullOrEmpty(t_LWFBDW.Text))
                    t_LWFBDW.Text = EConvert.ToString(dt.Rows[i]["FName"]);
                else
                    t_LWFBDW.Text += "," + EConvert.ToString(dt.Rows[i]["FName"]);


            }
            //勘察单位
            else if (EConvert.ToString(dt.Rows[i]["FEntType"]) == "5")
            {
                if (String.IsNullOrEmpty(t_KCDW.Text))
                    t_KCDW.Text = EConvert.ToString(dt.Rows[i]["FName"]);
                else
                    t_KCDW.Text += "," + EConvert.ToString(dt.Rows[i]["FName"]);

            }
            //设计单位
            else if (EConvert.ToString(dt.Rows[i]["FEntType"]) == "6")
            {
                if (String.IsNullOrEmpty(t_SJDW.Text))
                    t_SJDW.Text = EConvert.ToString(dt.Rows[i]["FName"]);
                else
                    t_SJDW.Text += "," + EConvert.ToString(dt.Rows[i]["FName"]);

            }
            //监理单位
            else if (EConvert.ToString(dt.Rows[i]["FEntType"]) == "7")
            {
                if (String.IsNullOrEmpty(t_JLDW.Text))
                    t_JLDW.Text = EConvert.ToString(dt.Rows[i]["FName"]);
                else
                    t_JLDW.Text += "," + EConvert.ToString(dt.Rows[i]["FName"]);


            }
            //
            else
            {

            }

        }
    }

    protected void BindRyInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * from TC_PrjItem_Emp where FAppId = '");
        sb.Append(t_fLinkId.Value);
        sb.Append("'");
        string sql = sb.ToString();
        DataTable dt = GetData(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //select * from  [dbo].[CF_Sys_Dic] where FNumber like '112202%' 查看码表
            //监理工程师
            if (EConvert.ToString(dt.Rows[i]["EmpType"]) == "11220209")
            {
                if (String.IsNullOrEmpty(t_JLGCS.Text))
                    t_JLGCS.Text = EConvert.ToString(dt.Rows[i]["FHumanName"]);
                else
                    t_JLGCS.Text += "," + EConvert.ToString(dt.Rows[i]["FHumanName"]);
            }
            //项目负责人
            else if (EConvert.ToString(dt.Rows[i]["EmpType"]) == "11220201")
            {
                if (String.IsNullOrEmpty(t_XMFZR.Text))
                    t_XMFZR.Text = EConvert.ToString(dt.Rows[i]["FHumanName"]);
                else
                    t_XMFZR.Text += "," + EConvert.ToString(dt.Rows[i]["FHumanName"]);
                t_ZYZG.Text = EConvert.ToString(dt.Rows[i]["ZC"]);
            }
            //安全负责人
            else if (EConvert.ToString(dt.Rows[i]["EmpType"]) == "11220203")
            {
                if (String.IsNullOrEmpty(t_AQFZR.Text))

                    t_AQFZR.Text = EConvert.ToString(dt.Rows[i]["FHumanName"]);
                else
                    t_AQFZR.Text += "," + EConvert.ToString(dt.Rows[i]["FHumanName"]);
            }

            //
            else
            {

            }

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