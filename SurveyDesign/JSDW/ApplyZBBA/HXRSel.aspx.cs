using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Text;
using Approve.RuleCenter;
using EgovaDAO;

public partial class JSDW_ApplyZBBA_HXRSel : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }
    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_CS.Text.Trim()))
        {
            sb.Append(" and qa.CS = '" + this.t_CS.Text.Trim() + "' ");
        }
        if (!string.IsNullOrEmpty(t_HXRMC.Text.Trim()))
        {
            sb.Append(" and hxr.HXRMC like '%" + this.t_HXRMC.Text.Trim() + "%' ");
        }
        if (!string.IsNullOrEmpty(Request.QueryString["fBDId"]))
        {
            sb.Append(" and hxr.BDId = '" + Request.QueryString["fBDId"].Trim() + "' ");
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

    private void showInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from ( ");
        sb.Append(" select qa.CS,hxr.HXRMC,hxr.QYId");
        sb.Append(" from CF_App_ProcessInstance ep , CF_App_ProcessRecord er, TC_PBBG_Record qa, CF_APP_LIST ap, TC_PBBG_ZBHXR hxr");
        sb.Append(" where ep.fId = er.FProcessInstanceID and hxr.FLinkId = qa.FId ");
        //   sb.Append(" and  er.FtypeId=3  ");
        //  sb.Append(" and ep.FSubFlowId = er.FSubFlowId "); //去掉这行，表示可以查询已经处理了到了下一阶段的业务
        sb.Append(" and ep.flinkId = er.FLinkId  and ep.flinkId = qa.FAppId ");
        sb.Append(" and ep.FLinkId = ap.FId ");
        //sb.Append(" and ep.fState = 6 and er.FResult = 1 ");
        sb.Append(getCondi());
        //下面的查询备份表
        sb.Append(" union all ");
        sb.Append(" select qa.CS,hxr.HXRMC,hxr.QYId ");
        sb.Append(" from CF_App_ProcessInstanceBackup ep , CF_App_ProcessRecordBackup er, TC_PBBG_Record qa, CF_APP_LIST ap, TC_PBBG_ZBHXR hxr");
        sb.Append(" where ep.fId = er.FProcessInstanceID and hxr.FLinkId = qa.FId ");
        //   sb.Append(" and  er.FtypeId=3  ");
        //  sb.Append(" and ep.FSubFlowId = er.FSubFlowId "); //去掉这行，表示可以查询已经处理了到了下一阶段的业务
        sb.Append(" and ep.flinkId = er.FLinkId  and ep.flinkId = qa.FAppId ");
        sb.Append(" and ep.FLinkId = ap.FId ");
        //sb.Append(" and ep.fState = 6 and er.FResult = 1 ");
        sb.Append(getCondi());
        sb.AppendLine(" ) ttt where 1=1 ");

        //if (!string.IsNullOrEmpty(t_FInt3.SelectedValue))
        //{
        //    sb.Append(" and FInt3='" + t_FInt3.SelectedValue + "' ");
        //}

        sb.AppendLine(" order by ttt.QYId ");


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
            string HXRMC = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "HXRMC"));
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 2].Controls[0] as LinkButton;
            lb.Text = "选择";
            lb.Attributes.Add("onclick", "return confirm('确认要选择该标段吗?');");
        }
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
                string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
            }
        }
    }
}