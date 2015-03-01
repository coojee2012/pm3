using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;

public partial class Admin_main_ProcessList : Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            ControlBind();

            if (Request["fmatypeid"] != null && Request["fmatypeid"] != "")
            {
                this.ViewState["FMATYPEID"] = Request["fmatypeid"];
            }
            ShowInfo();
        }
    }

    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fnumber,fname from cf_sys_systemName where fisdeleted=0 order by forder,ftime desc");
        this.drop_FSystem.DataSource = rc.GetTable(sb.ToString());
        this.drop_FSystem.DataTextField = "FName";
        this.drop_FSystem.DataValueField = "Fnumber";
        this.drop_FSystem.DataBind();
        this.drop_FSystem.Items.Insert(0, new ListItem("请选择", ""));
    }
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.text_FName.Text != "")
        {
            sb.Append(" and fname like '");
            sb.Append(this.text_FName.Text + "%'");
        }
        if (this.text_FNumber.Text != "")
        {
            sb.Append(" and FNumber ='");
            sb.Append(this.text_FNumber.Text + "'");
        }
        if (this.drop_FSystem.SelectedValue != "")
        {
            sb.Append(" and FSystemId='");
            sb.Append(this.drop_FSystem.SelectedValue + "' ");
        }
        return sb.ToString();
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        //sb.Append("select FId,FName,FFullName,FDefineDay,FNumber, ");
        //sb.Append("(select top 1 fname from CF_Sys_ManageDept where fnumber=FManageDeptId) FManageDeptName,");
        //sb.Append("(select top 1 fname from CF_Sys_SystemName where fnumber=FSystemId) FSystemName ");
        //sb.Append("From CF_App_Process ");
        //sb.Append("Where FIsDeleted=0  and FNumber<>'155' "); //安全生产许可证流程
        //sb.Append(GetCon());
        //sb.Append("Order By fnumber ");

        sb.Append("select cap.FId,cap.FName,cap.FFullName,cap.FDefineDay,cap.FNumber,");
        sb.Append(" (select top 1 fname from CF_Sys_ManageDept where fnumber=cap.FManageDeptId) FManageDeptName,");
        sb.Append(" (select top 1 fname from CF_Sys_SystemName where fnumber=cap.FSystemId) FSystemName");
        sb.Append(" From CF_App_Process cap ");
        sb.Append(" Where cap.FIsDeleted=0 ");
        sb.Append(GetCon());
        if (ViewState["FMATYPEID"] != null)
        {
            sb.Append(" and cap.fid in(");
            sb.Append("select FProcessId from  CF_App_ManageType pa,CF_Sys_ManageType p where");
            sb.Append(" pa.FManageTypeId=p.fnumber and p.fmtypeid=" + ViewState["FMATYPEID"].ToString() + "");
            sb.Append(") ");
        }
        sb.Append(" Order By cap.fnumber ");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 50;
        this.Pager1.controltopage = "Process_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }

    protected void Process_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showAddWindow('ProcessAdd.aspx?fid=" + fid + "&fmatypeid=" + EConvert.ToString(ViewState["FMATYPEID"]) + "',900,750);return false;\">" + e.Item.Cells[2].Text + "</a>";
            e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='#' class='link3' onclick=\"showAddWindow('SubFlowList.aspx?fprocessid=" + fid + "',850,750);return false;\">维护子流程</a>";


            StringBuilder sb = new StringBuilder();
            sb.Append("select csq.fname from CF_App_QualiLevel caq,CF_Sys_QualiLevel csq ");
            sb.Append(" where csq.fnumber = caq.FLevelId");
            sb.Append(" and csq.fisdeleted=0 ");
            sb.Append(" and caq.fisdeleted=0 ");
            sb.Append(" and fprocessid='" + fid + "' ");
            sb.Append(" order by csq.ftime desc ");
            DataTable dt = rc.GetTable(sb.ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                e.Item.Cells[e.Item.Cells.Count - 5].Text = "暂无内容";
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i != 0)
                    {
                        e.Item.Cells[e.Item.Cells.Count - 5].Text += "<br>" + dt.Rows[i]["FName"].ToString();
                    }
                    else
                    {
                        e.Item.Cells[e.Item.Cells.Count - 5].Text += dt.Rows[i]["FName"].ToString();
                    }
                }
            }

            sb.Remove(0, sb.Length);
            sb.Append("select csm.fname from CF_App_ManageType cam,CF_Sys_ManageType csm ");
            sb.Append(" where csm.fnumber = cam.FManageTypeId");
            sb.Append(" and csm.fisdeleted=0 ");
            sb.Append(" and cam.fisdeleted=0 ");
            sb.Append(" and fprocessid='" + fid + "' ");
            sb.Append(" order by csm.forder,csm.fcreatetime desc ");
            dt = rc.GetTable(sb.ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                e.Item.Cells[e.Item.Cells.Count - 4].Text = "暂无内容";
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i != 0)
                    {
                        e.Item.Cells[e.Item.Cells.Count - 4].Text += "<br>" + dt.Rows[i]["FName"].ToString();
                    }
                    else
                    {
                        e.Item.Cells[e.Item.Cells.Count - 4].Text += dt.Rows[i]["FName"].ToString();
                    }
                }
            }

            sb.Remove(0, sb.Length);
            sb.Append("select csd.fname,(select top 1 fname from cf_sys_dic where fnumber=csd.fparentid) fpname, ");
            sb.Append("(select top 1 forder from cf_sys_dic where fnumber=csd.fparentid) fporder ");
            sb.Append(" from CF_App_QualiType caq,CF_sys_Dic csd ");
            sb.Append(" where caq.FQualiTypeId = csd.fnumber ");
            sb.Append(" and caq.fisdeleted=0 ");
            sb.Append(" and csd.fisdeleted=0 ");
            sb.Append(" and fprocessid='" + fid + "' ");
            sb.Append(" order by fporder,csd.forder,csd.ftime desc ");
            dt = rc.GetTable(sb.ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                e.Item.Cells[e.Item.Cells.Count - 3].Text = "暂无内容";
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i != 0)
                    {
                        e.Item.Cells[e.Item.Cells.Count - 3].Text += "<br>" + dt.Rows[i]["fpname"].ToString() + " | " + dt.Rows[i]["FName"].ToString();
                    }
                    else
                    {
                        e.Item.Cells[e.Item.Cells.Count - 3].Text += dt.Rows[i]["fpname"].ToString() + " | " + dt.Rows[i]["FName"].ToString();
                    }
                }
            }

        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(this.Process_List, EntityTypeEnum.EaProcess, "RCenter", "DelProcess");
        ShowInfo();
    }


}
