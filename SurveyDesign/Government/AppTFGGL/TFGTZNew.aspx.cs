using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppTFGGL_TFGTZNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            t_FId.Value = EConvert.ToString(Request["FId"]);
            t_ND.Text = DateTime.Now.ToString("yyyy");
            BindData();
        }
    }

    protected void BindData() {
        if (!string.IsNullOrEmpty(t_FId.Value))
        {
            string sql = "SELECT * FROM TC_SGXKZ_TFGTZ WHERE FId='" + t_FId.Value + "'";
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                DataSet ds = new DataSet();
         
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds, "ds");
                DataTable dt = ds.Tables[0];
                string fapps = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    t_ND.Text = EConvert.ToString(dt.Rows[i]["ND"]);
                    t_TGPC.Text = EConvert.ToString(dt.Rows[i]["TGPC"]);
                    t_TGDate.Text = EConvert.ToString(dt.Rows[i]["TGRQ"]);
                    t_FGDate.Text = EConvert.ToString(dt.Rows[i]["FGRQ"]);
                    t_TGYY.Text = EConvert.ToString(dt.Rows[i]["TGYY"]);
                    fapps = EConvert.ToString(dt.Rows[i]["FAppId"]);
                    break;
                }

                if (!string.IsNullOrEmpty(fapps))
                {
                    BindGCXM(fapps);
                }
               

            }
        }
    }
    protected void BindGCXM(string fapps)
    {
        string[] fappsArarry = fapps.Split(',');
        string newFapps = "";
        for (int i = 0; i < fappsArarry.Count(); i++) {
            if (!string.IsNullOrEmpty(fappsArarry[i]))
            {
                if (string.IsNullOrEmpty(newFapps))
                    newFapps = "'" + fappsArarry[i] + "'";
                else
                {
                    newFapps += ",'" + fappsArarry[i] + "'";
                }
            }
        }
        if (!string.IsNullOrEmpty(newFapps))
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from ( ");
            sb.Append(" 	select * ");
            sb.Append(" ,case when SJStartDate < GETDATE() then '已开工' else '未开工' end as SGZT ");
            sb.Append("  from TC_SGXKZ_PrjInfo ");
            sb.Append(" where where FId IN (  " + newFapps+")");
            //  sb.Append(" and ep.FSubFlowId = er.FSubFlowId "); //去掉这行，表示可以查询已经处理了到了下一阶段的业务
            //sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");    
            //sb.Append(getCondi());
            //下面的查询备份表

            sb.AppendLine(" ) ttt where 1=1 ");


            // sb.AppendLine(" order by ttt.FReporttime desc,ttt.FBaseInfoId");


            this.Pager1.sql = sb.ToString();
            this.Pager1.controltype = "DataGrid";
            this.Pager1.controltopage = "DG_ListYZ";
            this.Pager1.pagecount = 10;
            this.Pager1.dataBind();
        }
        
    }
    /// <summary>
    /// 显示已选择工程项目信息列表
    /// </summary>
    protected void showYZList() {
        //EgovaDB dbContext = new EgovaDB();
        //var v = dbContext.TC_Prj_YZ.Where(t => t.FAppId == t_fLinkId.Value);
        //DG_ListYZ.DataSource = v;
        //DG_ListYZ.DataBind();
    }
    protected void DG_ListYZ_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        //if (e.Item.ItemIndex > -1)
        //{
        //    e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager_YZ.PageSize * (this.Pager_YZ.CurrentPageIndex - 1)).ToString();
        //    string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
        //    string fAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
        //    //string fEntId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEntId"));
        //    //string fPrjItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjItemId"));
        //    //string fPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
        //    e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('YZInfo.aspx?FId=" + fId + "&FAppId=" + fAppId + "',600,450);\">" + e.Item.Cells[2].Text + "</a>";
        //}
    }
    protected void Pager_YZ_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        //Pager_YZ.CurrentPageIndex = e.NewPageIndex;

    }
    protected void btn_del_YZ_Click(object sender, EventArgs e)
    {
        //EgovaDB dbContext = new EgovaDB();
        //pageTool tool = new pageTool(this.Page);
        //tool.DelInfoFromGrid(DG_ListYZ, dbContext.TC_Prj_YZ, tool_Deleting_TKJL);
        //showYZList();

    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showYZList();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string fid = "";
        string sql = "";
        if (string.IsNullOrEmpty(this.t_FId.Value)) { 
        fid=Guid.NewGuid().ToString();
        
        sql = "INSERT INTO TC_SGXKZ_TFGTZ (FId,FAppId,TGPC,ND,TGRQ,FGRQ,TGYY,FBZT,PCSL) VALUES (";
        sql += " '" + fid + "','','" + EConvert.ToString(t_TGPC.Text) + "',YEAR(GETDATE()),'";
        sql += EConvert.ToString(t_TGDate.Text) + "','" + EConvert.ToString(t_FGDate.Text) + "','";
        sql += EConvert.ToString(t_TGYY.Text)+"',0,0)";
        }
        else
        {
            fid = this.t_FId.Value;
            sql = " UPDATE TC_SGXKZ_TFGTZ SET TGPC='" + EConvert.ToString(t_TGPC.Text) + "',TGRQ='";
            sql += EConvert.ToString(t_TGDate.Text) + "', FGRQ='";
            sql += EConvert.ToString(t_FGDate.Text) + "',TGYY='" + EConvert.ToString(t_TGYY.Text) + "'";
            sql += " WHERE FId='" + fid + "'";
        }

        try
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                int a = cmd.ExecuteNonQuery();



            }
            this.t_FId.Value = fid;

            Page.ClientScript.RegisterClientScriptBlock( typeof(Page), "js", "alert('保存成功');", true);
          
        }
        catch (Exception ex) {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "js", "alert('保存失败:"+ex.Message+"');", true);
        }
       

        
    }
}