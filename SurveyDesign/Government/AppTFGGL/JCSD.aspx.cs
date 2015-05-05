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

public partial class Government_AppTFGGL_JCSD : System.Web.UI.Page
{
    //RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // t_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            if (Request["idCard"] != null && !string.IsNullOrEmpty(Request["idCard"]))
            {
                t_FIdCard.Value = Request["idCard"];
            }

            showInfo();
        }
    }
    //显示 
    void showInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from ( ");
        sb.Append(" select  a.FId, a.lockType,b.FHumanName,c.PrjItemName,a.FCreateTime,a.FTime,d.FName,c.ProjectName,c.PrjAddressDept,dbo.getManageDeptName(c.PrjAddressDept) as DeptName,c.StartDate,c.EndDate from TC_PrjItem_Emp_Lock a  ");
        sb.Append(" left join  TC_PrjItem_Emp b on a.FIdCard=b.FIdCard and a.FPrjItemId = b.FPrjItemId ");
        sb.Append(" left join TC_SGXKZ_PrjInfo c on a.FAppId = c.FAppId ");
        sb.Append(" left join CF_Sys_Dic d on b.EmpType = d.FNumber ");
        sb.Append(" where a.IsLock=1 and a.FIdCard='"+t_FIdCard.Value+"' ");
        //下面的查询备份表

        sb.AppendLine(" ) ttt where 1=1 ");


        // sb.AppendLine(" order by ttt.FReporttime desc,ttt.FBaseInfoId");


        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "dg_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }

  
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            // e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
           // LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 2].Controls[0] as LinkButton;
            //lb.Text = "解锁";
            //lb.Attributes.Add("onclick", "return confirm('确认要选择该项目吗?');");
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        //  Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    protected void btnJS_Click(object sender, EventArgs e)
    {
        string FId = "";
        string FDeptId = CurrentEntUser.UpDeptId.ToString();
        int RowCount = dg_List.Items.Count;
        IList<string> FIdList = new List<string>();
        string FIds = "";
        for (int i = 0; i < dg_List.Items.Count; i++)
        {
            CheckBox cbx = (CheckBox)dg_List.Items[i].Cells[0].Controls[1];
            if (cbx.Checked)
            {
                FId = dg_List.Items[i].Cells[dg_List.Columns.Count - 1].Text.Trim();
                string dept = dg_List.Items[i].Cells[dg_List.Columns.Count - 2].Text.Trim();
                if (dept.Substring(0, FDeptId.Length) == FDeptId)
                {
                    FIdList.Add(FId);
                    if (string.IsNullOrEmpty(FIds))
                    {
                        FIds = "'" + FId + "'";
                    }
                    else
                    {
                        FIds += ",'" + FId + "'";
                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(up_Main, up_Main.GetType(), "js", "alert('有项目属地不在管辖范围内,不能解锁！');", true);
                    return;
                }
                
            }
        }

        for (int k = 0; k < FIdList.Count(); k++)
        {
            //t_FIdCard.Value
            string newId=Guid.NewGuid().ToString();
            string sql = "UPDATE TC_PrjItem_Emp_Lock SET IsLock=0,FTime=GETDATE() WHERE FId= '"+FIdList[k]+"';";
            sql += " UPDATE TC_PrjItem_Emp_Lock  SET SelectedCount = (select count(1) from TC_PrjItem_Emp_Lock a  where a.IsLock=1 and  a.FIdCard = '" + t_FIdCard.Value + "') WHERE FIdCard='" + t_FIdCard.Value + "';";
            sql += " Insert into TC_PrjItem_Emp_UnLock (FId,FLinkId,FJSR,FJSRID,FJSTime,FJSYY) Values ('";
            sql += newId + "','" + FIdList[k] + "','" + "" + "','" + Session["DFUserId"] + "',GETDATE(),'" + t_JSYY.Text + "');";
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }

        }

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
                string sql = "UPDATE  TC_SGXKZ_TFGTZ SET FAppId = FAppId + '" + fid + ",',PCSL = PCSL + 1 WHERE 1=1 AND FId ='" + t_FIdCard.Value + "'";
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