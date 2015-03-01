using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Linq;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using System.Data;
using ProjectData;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class Share_SysSet_SelectMType : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            conBind();
            showInfo();
        }
    }

    private void conBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fid,Fname,fnumber from CF_Sys_SystemName where fisdeleted=0 order by forder");
        DataTable dt = sh.GetTable(sb.ToString());
        this.drop_FSystemId.DataSource = dt;
        this.drop_FSystemId.DataTextField = "FName";
        this.drop_FSystemId.DataValueField = "FNumber";
        this.drop_FSystemId.DataBind();
        this.drop_FSystemId.Items.Insert(0, new ListItem("请选择", ""));
    }

    //条件
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(text_FNumber.Text))
        {
            sb.Append(" and fnumber ='");
            sb.Append(this.text_FNumber.Text + "'");
        }
        if (!string.IsNullOrEmpty(drop_FSystemId.SelectedValue))
        {
            sb.Append(" and fsystemid='");
            sb.Append(this.drop_FSystemId.SelectedValue + "'");
        }
        if (!string.IsNullOrEmpty(text_FMTypeId.Text))
        {
            sb.Append(" and FMTypeId like '%" + text_FMTypeId.Text + "%'");
        }
        return sb.ToString();
    }

    private void showInfo()//显示列表
    {
        pageTool tool = new pageTool(this.Page);
        DataTable dt = sh.GetTable("select FID,FMType from CF_Sys_PrjList where FID=@FID", new SqlParameter("@FID", Request["FID"]));
        if (dt != null && dt.Rows.Count > 0)
        {
            //已选业务
            string FMType = dt.Rows[0]["FMType"].ToString();


            //绑定列表
            StringBuilder sb = new StringBuilder();
            sb.Append("select FId,FName,FNumber,FOperDeptName,FOrder,FMTypeId,FDesc,'" + FMType + "' FMType,");
            sb.Append("(select top 1 fname from CF_Sys_SystemName where fnumber=fsystemid) FSystemName ");
            sb.Append("From CF_Sys_ManageType ");
            sb.Append("Where FIsDeleted=0 ");
            sb.Append(GetCon());
            sb.Append("Order By FOrder,fsystemid,FCreateTime desc ");

            this.Pager1.className = "dbShare";
            this.Pager1.sql = sb.ToString();
            this.Pager1.pagecount = 10;
            this.Pager1.controltopage = "DG_List";
            this.Pager1.controltype = "DataGrid";
            this.Pager1.dataBind();
        }
        else
        {
            tool.showMessageAndRunFunction("链接错误", "window.close();");
        }
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FNumber = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
            string FMType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FMType"));


            //选择和取消
            Literal lit_FMType = (Literal)e.Item.FindControl("lit_FMType");
            Button btnFMType = (Button)e.Item.FindControl("btnFMType");
            if (FMType.Split(',').Count(t => t == FNumber) > 0)
            {
                lit_FMType.Text = "<font color='green'>已选择</font>";
                btnFMType.Text = "取消";
                btnFMType.Attributes.Add("onclick", "return confirm('确定要取消吗？');");
            }
            else
            {
                btnFMType.Text = "选择";
                btnFMType.Attributes.Add("onclick", "return confirm('确定要选择吗？');");
            }

        }
    }

    //绑定列表内控件事件
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "cnFMType")
        {
            pageTool tool = new pageTool(this.Page);
            string FID = Request.QueryString["FID"];
            string FNumber = e.CommandArgument.ToString();//当前操作业务number

            ProjectDB db = new ProjectDB();
            CF_Sys_PrjList p = db.CF_Sys_PrjList.Where(t => t.FId == FID).FirstOrDefault();
            if (p != null)
            {
                Button btnFMType = (Button)e.CommandSource;
                if (btnFMType != null && btnFMType.Text == "选择")
                {
                    if (!string.IsNullOrEmpty(p.FMType))
                        p.FMType += ",";
                    p.FMType += FNumber;
                }
                else
                {
                    List<string> M = p.FMType.Split(',').ToList();
                    M.Remove(FNumber);
                    p.FMType = string.Join(",", M.ToArray());
                }
            }
            p.FType = 2;//查看业务办理情况
            db.SubmitChanges();
            tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
            showInfo();
        }
    }

    //查询按钮
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
