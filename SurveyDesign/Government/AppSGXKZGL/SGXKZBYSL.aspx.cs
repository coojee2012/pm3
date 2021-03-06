﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Government_AppSGXKZGL_SGXKZBYSL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["FLinkId"] != null && !string.IsNullOrEmpty(Request["FLinkId"]))
            {
                t_fLinkId.Value = Request["FLinkId"].ToString();
                ffid.Value = getBYSLTZSinfo(t_fLinkId.Value);
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
    private string getBYSLTZSinfo(string fappid)
    {
        string sql = @"select  GuidID  from YW_BYSLTZS where YWBM = '+" + fappid + "' ";
        DataTable dt = GetData(sql);
        if (dt != null&&dt.Rows.Count >0)
        {
            return dt.Rows[0][0].ToString();
        }
        return "";
    }


    protected void BindData()
    {
        ffid.Value = "";
        StringBuilder sb = new StringBuilder();
        sb.Append(" select a.YWLX + a.PrjItemName+'施工许可证' as SLLR,a.FappId,ISNULL(b.GuidID,'') as FFId, a.PrjAddressDept,b.BH,b.LXR,b.LXDH,b.JDDH,ISNULL(b.DJZQX,1) as DJZQX,");
        sb.Append(" (select count(1)+1 from YW_BYSLTZS where DATEDIFF(day,getdate(),SLRQ )=0 ) as SLXH");
        sb.Append(" from V_SGXKZ_YW  a ");
        sb.Append(" left join YW_BYSLTZS b on a.FAppId = b.YWBM ");
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
                ffid.Value = EConvert.ToString(dt.Rows[i]["FFId"]);
            }

            t_RQ.Text = now.ToString("yyyy-MM-dd");
            t_NR.Text = EConvert.ToString(dt.Rows[i]["SLLR"]);
            t_LXR.Text = EConvert.ToString(dt.Rows[i]["LXR"]);
            t_LXDH.Text = EConvert.ToString(dt.Rows[i]["LXDH"]);
            t_JDDH.Text = EConvert.ToString(dt.Rows[i]["JDDH"]);
            ddlMType.SelectedValue = EConvert.ToString(dt.Rows[i]["DJZQX"]);

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

                string sql = "";
                string guid = "";
                if (string.IsNullOrEmpty(ffid.Value))
                {
                    guid = Guid.NewGuid().ToString();
                    ffid.Value = guid;
                    sql = "INSERT INTO YW_BYSLTZS (GuidID,YWBM,BH,LXR,LXDH,JDDH,SLRQ,DJZQX) VALUES  ( '" + guid + "','";
                    sql += t_fLinkId.Value + "','" + t_BH.Text + "','";
                    sql += t_LXR.Text + "','" + t_LXDH.Text + "','" + t_JDDH.Text + "',getdate()," + ddlMType.SelectedValue + ")";
                }
                else
                {
                    guid = ffid.Value;
                    sql = "UPDATE YW_BYSLTZS SET LXR = '" + t_LXR.Text + "',LXDH='";
                    sql += t_LXDH.Text + "',JDDH = '";
                    sql += t_JDDH.Text + "',DJZQX=" + ddlMType.SelectedValue;
                    sql += " WHERE GuidID='" + ffid.Value + "'";

                }
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                ScriptManager.RegisterClientScriptBlock(up_Main, up_Main.GetType(), "js", "$('#ffid').val('" + guid.ToString() + "');alert('保存成功');", true);
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}