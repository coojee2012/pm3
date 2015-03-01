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
using Approve.RuleCenter;
using System.Text;
using Approve.EntityBase;

public partial class Government_AppEntAction_SelectEnt : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
            ShowInfo();
        }
    }
    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" FIsdeleted=0  and fnumber in (100,126,145,15501,155) order by forder desc ");
        DataTable dt = rc.GetTable(EntityTypeEnum.EsSystemName, "FName,FNumber", sb.ToString());
        dbFSystemId.DataSource = dt;
        dbFSystemId.DataTextField = "FName";
        dbFSystemId.DataValueField = "FNumber";
        dbFSystemId.DataBind();
        dbFSystemId.Items.Insert(0, new ListItem("请选择", ""));

        if (!string.IsNullOrEmpty(Request.QueryString["sysId"]))
        {
            dbFSystemId.SelectedIndex = dbFSystemId.Items.IndexOf(dbFSystemId.Items.FindByValue(Request.QueryString["sysId"]));
            dbFSystemId.Enabled = false;
        }


        sb.Remove(0, sb.Length);

        sb.Append(" select FNumber,");
        sb.Append(" case flevel when 1 then fnumber when 2 then fnumber when 3 then fparentid end as FoderNumber ,");
        sb.Append(" case flevel when 1 then FName when 2 then '--'+FName when 3 then '----'+FName end as FName ");
        sb.Append(" from cf_sys_managedept");
        sb.Append(" where fnumber like '" + Session["DFId"] + "%'  and flevel>=1");
        sb.Append(" order by  FoderNumber,flevel");

        dbMangeDept.DataSource = rc.GetTable(sb.ToString());
        dbMangeDept.DataTextField = "FName";
        dbMangeDept.DataValueField = "FNumber";
        dbMangeDept.DataBind();

        if (!string.IsNullOrEmpty(Request.QueryString["stype"]))
        {
            if (Request.QueryString["stype"] == "action") //行为
            {
                if (Session["DFLevel"] != null)
                {
                    int ilevel = int.Parse(Session["DFLevel"].ToString());
                    if (ilevel > 1)
                    {
                        dbMangeDept.SelectedIndex = dbMangeDept.Items.IndexOf(dbMangeDept.Items.FindByValue(Session["DFId"].ToString().Trim()));
                        dbMangeDept.Enabled = false;
                    }
                    else
                        dbMangeDept.Items.Insert(0, new ListItem("--请选择--", ""));
                }
            }
        }
        else
        {
            if (Session["DFLevel"] != null)
            {
                int ilevel = int.Parse(Session["DFLevel"].ToString());
                if (ilevel > 1)
                {
                    dbMangeDept.SelectedIndex = dbMangeDept.Items.IndexOf(dbMangeDept.Items.FindByValue(Session["DFId"].ToString().Trim()));
                    dbMangeDept.Enabled = false;
                }
                else
                    dbMangeDept.Items.Insert(0, new ListItem("--请选择--", ""));
            }
        }

    }

    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtFName.Text != "")
        {
            sb.Append(" and b.fname like '%");
            sb.Append(this.txtFName.Text + "%' ");
        }

        if (dbMangeDept.SelectedValue.Trim() != "")
        {
            sb.Append(" and b.FUpDeptId");
            sb.Append("   like '");
            sb.Append(this.dbMangeDept.SelectedValue.Trim() + "%' ");
        }

        if (dbFSystemId.SelectedValue.Trim() != "")
        {
            sb.Append(" and u.FSystemId='");
            sb.Append(dbFSystemId.SelectedValue + "' ");

            if (dbFSystemId.SelectedValue == "15501")
            {
                sb.Append(" and r.FSystemId=1554 ");
            }
            else if (dbFSystemId.SelectedValue == "155")
            {
                sb.Append(" and r.FSystemId=1553 ");
            }
        }
        else
        {
            sb.Append(" and u.FSystemId ='" + Request.QueryString["sysId"] + "' ");
        }

        string sType = this.Request.QueryString["stype"];
        if (sType == "safety") //安全生产许可证
        {
            sb.Append(" and  fid not in (select fbaseinfoid from CF_Ent_SafetyCerti where fisdeleted=0 )");
        }

        if (sType == "quali") //资质
        {
            if (this.dbFSystemId.SelectedValue.Trim() != "")
            {
                sb.Append(" and  fid not in (select b.fid from CF_Ent_QualiCerti c,cf_ent_baseinfo b ");
                sb.Append(" where c.fisdeleted=0 and b.fisdeleted=0  and b.fsystemid=" + dbFSystemId.SelectedValue);
                sb.Append(" and b.fid=c.fbaseinfoid and c.FIsValid=1 )");
            }
        }


        return sb.ToString();
    }

    private void ShowInfo()
    {
        string fMangetdept = Session["DFId"].ToString();
        StringBuilder sb = new StringBuilder();

        sb.Append(" select b.fid,b.fname,b.flinkman,b.ftel from cf_ent_baseinfo b   ");
        sb.Append(" inner join cf_Sys_Userright r on b.FId=r.FBaseInfoId ");
        sb.Append(" inner join cf_Sys_User u on u.FId=r.FUserId ");
        sb.Append(" where b.fisdeleted=0 ");
        sb.Append(GetCon());
        sb.Append("order by b.FId,b.FCreateTime Desc");


        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "Ent_List";
        this.Pager1.className = "RCenter";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();

    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();

    }

    protected void Ent_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fBaseInfoId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + (this.Pager1.curpage - 1) * Pager1.pagecount).ToString();
        }
    }

    protected void Ent_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.TabIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            if (e.CommandName == "Ok")
            {
                this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>window.returnValue='" + fid + "';window.close();</script>");
            }
        }
    }
}

