using Approve.Common;
using EgovaDAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppTFGGL_XMList : System.Web.UI.Page
{
    //RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // t_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            if (Request["t_FAppId"] != null && !string.IsNullOrEmpty(Request["t_FAppId"]))
            {
                t_FAppId.Value = Request["t_FAppId"];
            }

            showInfo();
        }
    }
    //显示 
    void showInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from ( ");
        sb.Append(" 	select *,dbo.getManageDeptName(PrjAddressDept) as PrjAddressDeptName");
         sb.Append(" ,case when SJStartDate < GETDATE() then '已开工' else '未开工' end as SGZT ");
        sb.Append("  from TC_SGXKZ_PrjInfo");
        sb.Append(" where SGXKZBH IS NOT NULL ");
        //  sb.Append(" and ep.FSubFlowId = er.FSubFlowId "); //去掉这行，表示可以查询已经处理了到了下一阶段的业务
        //sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");    
        sb.Append(getCondi());
        //下面的查询备份表
       
        sb.AppendLine(" ) ttt where 1=1 ");


       // sb.AppendLine(" order by ttt.FReporttime desc,ttt.FBaseInfoId");


        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "dg_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }

    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        if (this.t_FGCName.Text.Trim() != "" && this.t_FGCName.Text.Trim() != null)
        {
            sb.Append(" and PrjItemName like '%" + this.t_FGCName.Text.Trim() + "%' ");
        }

        if (this.govd_FRegistDeptId.FNumber != null)
        {
            sb.Append(" and dbo.isSuperDept_new(" + this.govd_FRegistDeptId.FNumber + ",PrjAddressDept" + ") >0 ");
        }
        else
        {
            sb.Append(" and PrjAddressDept <> '' ");
        }
       

        

        


        if (sb.Length > 0)
        {
            return sb.ToString();
        }
        else
        {
            return "";
        }
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
           // e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 2].Controls[0] as LinkButton;
            lb.Text = "选择";
            lb.Attributes.Add("onclick", "return confirm('确认要选择该项目吗?');");
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
      //  Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            if (e.CommandName == "Sel")
            {          
                string fid = e.Item.Cells[10].Text;
                string FHumanName = e.Item.Cells[2].Text;
                pageTool tool = new pageTool(this.Page);
               // tool.ExecuteScript("window.returnValue='" + fid + "@" + FHumanName + "';window.close();");
                tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
                string sql = "UPDATE  TC_SGXKZ_TFGTZ SET FAppId = FAppId + '" + fid + ",',PCSL = PCSL + 1 WHERE 1=1 AND FId ='" + t_FAppId.Value + "'";
                sql += " AND FAppId NOT LIKE '%" + fid + "%'";

                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
                {
                  
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand(sql, conn);

                   cmd.ExecuteNonQuery();



                }
            }
        }
    }
}