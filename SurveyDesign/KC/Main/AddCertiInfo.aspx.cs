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
using Approve.Common;
using Approve.RuleCenter;
using Approve.EntityBase;
using System.Text;
using Approve.EntityCenter;
using ProjectBLL;
public partial class Government_EntQualiCerti_AddCertiInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["IsView"]) && Request.QueryString["IsView"] == "1")
        {
            this.ClientScript.RegisterStartupScript(this.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>try{btnEnable();}catch(e){}</script>");

        }
        if (!Page.IsPostBack)
        {
            if (rc.GetSysObjectContent("_CanModifyBaseInfo") == "1")
            {
                btnSave.Visible
                = btnAdd.Visible;

            }
            else
            {
                btnSave.Visible
                         = btnAdd.Visible
                    = false;
            }
            btnSave.Attributes.Add("onclick", "if(checkInfo()){return true;}else{return false;}");
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么？')");

            this.ViewState["FBaseId"] = CurrentEntUser.EntId;
            ControlBind();
            if (Request["fid"] != null && Request["fid"] != "")
            {
                ViewState["FID"] = Request["fid"];
                HFID.Value = Request["fid"];
                ShowInfo();
            }
        }
    }

    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        //核准单位 
        sb.Append(" fisdeleted=0 and ");
        sb.Append("(fnumber =" + ComFunction.GetDefaultDept() + " ");
        sb.Append(" or fnumber=0) order by fnumber");
        DataTable dt = rc.GetTable(EntityTypeEnum.EsManageDept, "FFullName FName,FNumber", sb.ToString());
        t_FAppDeptId.DataSource = dt;
        t_FAppDeptId.DataTextField = "FName";
        t_FAppDeptId.DataValueField = "FNumber";
        t_FAppDeptId.DataBind();
        t_FAppDeptId.Items.Insert(0, new ListItem("--请选择--", ""));

        sb.Remove(0, sb.Length);
        sb.Append(" select fnumber,fname from CF_Sys_QualiLevel ");
        sb.Append(" where fisdeleted=0 ");
        sb.Append(" and fsystemid=190 ");
        sb.Append(" order by flevel");
        dt = rc.GetTable(sb.ToString());
        t_FLevelId.DataSource = dt;
        t_FLevelId.DataTextField = "FName";
        t_FLevelId.DataValueField = "FNumber";
        t_FLevelId.DataBind();
        t_FLevelId.Items.Insert(0, new ListItem("--请选择--", ""));

        dt = rc.getDicTbByFNumber("158");
        t_FCertiType.DataSource = dt;
        t_FCertiType.DataTextField = "FName";
        t_FCertiType.DataValueField = "FNumber";
        t_FCertiType.DataBind();
        t_FCertiType.Items.Insert(0, new ListItem("--请选择--", ""));
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = rc.GetTable(EntityTypeEnum.EbQualiCerti, "", sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            pageTool tool = new pageTool(this.Page);
            tool.fillPageControl(dt.Rows[0]);
            cbFIsTemp.Checked = dt.Rows[0]["FIsTemp"].ToString() == "1";
            ShowQuali();
        }
    }
    void ShowQuali()
    {
        if (ViewState["FID"] != null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select t1.fid,t2.FCertiNo,t1.flistname,t1.ftypename,t1.flevelname,t1.FLeadName,t1.FAppDeptName,");
            sb.Append(" t1.FAppTime ,case FIsBase when 1 then '是' when 0 then '否' end as FIsBase ");
            sb.Append(" from CF_Ent_QualiCertiTrade t1,CF_Ent_QualiCerti t2");
            sb.Append(" where t1.FCertiId = t2.fid ");
            sb.Append(" and t1.fisdeleted=0 and t2.fisdeleted=0 ");
            sb.Append(" and t2.fid='" + ViewState["FID"] + "'");
            sb.Append(" order by t1.FAppTime desc");
            DG_List.DataSource = rc.GetTable(sb.ToString());
            DG_List.DataBind();
        }
    }
    private void SaveInfo()
    {
        EntityTypeEnum en = EntityTypeEnum.EbQualiCerti;
        string fkey = "FID";
        SortedList sl = new SortedList();
        SaveOptionEnum so = SaveOptionEnum.Insert;
        pageTool tool = new pageTool(this.Page);
        sl = tool.getPageValue();

        if (this.ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FIsDeleted", 0);
        }
        sl.Add("FIsValid", 1);
        sl.Add("FIsTemp", cbFIsTemp.Checked ? 1 : 0);
        sl.Add("FBaseInfoId", CurrentEntUser.EntId);
        sl.Add("FEntName", CurrentEntUser.EntName);
        sl.Add("FAppDeptName", t_FAppDeptId.SelectedItem.Text.Trim());
        sl.Add("FLevelName", t_FLevelId.SelectedItem.Text.Trim());
        sl.Add("FSystemId", 155);
        if (rc.SaveEBase(en, sl, fkey, so))
        {
            ViewState["FID"] = sl["FID"].ToString();
            HFID.Value = sl["FID"].ToString();
            tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1).ToString();
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            //如果是查询，则不可修改 
            if (Request.QueryString["isQuery"] != "1")
            {
                //string sScript = "showAddWindow('AddQualiInfo.aspx?fid=" + fid + "&IsView=" + Request.QueryString["IsView"] + "&fbid=" + Request["fbid"] + "',600,500)";
                //e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\">" + e.Item.Cells[2].Text + "</a>";
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(DG_List, EntityTypeEnum.EbQualiCertiTrade, "RCenter");
        ShowInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowQuali();
    }
}
