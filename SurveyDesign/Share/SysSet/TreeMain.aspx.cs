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
using Approve.EntitySys;

public partial class Share_Main_TreeMain : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            this.btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?')");
            ControlBind();
            if (Request["fid"] != null && Request["fid"] != null)
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
            }
        }
    }
    private void ControlBind()
    {
        DataTable dt =  sh.getDicTbByFNumber("155");
        this.t_FKindId.DataSource = dt;
        this.t_FKindId.DataTextField = "FName";
        this.t_FKindId.DataValueField = "FNumber";
        this.t_FKindId.DataBind();
        this.t_FKindId.Items.Insert(0, new ListItem("", ""));




        StringBuilder sb = new StringBuilder();
        sb.Append("select fname ,fnumber from cf_sys_tree where fisdeleted=0 and flevel=1 order by forder,fcreatetime desc");
        this.drop_Parent1.DataSource = sh.GetTable(sb.ToString());
        this.drop_Parent1.DataTextField = "FName";
        this.drop_Parent1.DataValueField = "FNumber";
        this.drop_Parent1.DataBind();
        this.drop_Parent1.Items.Insert(0, new ListItem("", ""));
    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from cf_sys_tree where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            string flevel = dt.Rows[0]["flevel"].ToString();
            if (flevel == "2")
            {
                this.drop_Parent1.SelectedValue = dt.Rows[0]["FParent"].ToString();
            }
            if (flevel == "3")
            {
                string fPrent = dt.Rows[0]["FParent"].ToString();
                string fTParent = sh.GetSignValue(EntityTypeEnum.EsTree, "FParent", "fnumber='" + fPrent + "'");
                this.drop_Parent1.SelectedValue = fTParent;
                dt = sh.GetTable(EntityTypeEnum.EsTree, "FName,FNumber", "FParent='" + fTParent + "'");
                this.drop_Parent2.DataSource = dt;
                this.drop_Parent2.DataTextField = "FName";
                this.drop_Parent2.DataValueField = "FNumber";
                this.drop_Parent2.DataBind();
                this.drop_Parent2.Items.Insert(0, new ListItem("", ""));
                this.drop_Parent2.SelectedValue = fPrent;
            }
            if (flevel == "4")
            {
                string fPrent = dt.Rows[0]["FParent"].ToString();
                string fTParent = sh.GetSignValue(EntityTypeEnum.EsTree, "FParent", "fnumber='" + fPrent + "'");
                dt = sh.GetTable(EntityTypeEnum.EsTree, "FName,FNumber", "FParent='" + fTParent + "'");
                this.drop_Parent3.DataSource = dt;
                this.drop_Parent3.DataTextField = "FName";
                this.drop_Parent3.DataValueField = "FNumber";
                this.drop_Parent3.DataBind();
                this.drop_Parent3.Items.Insert(0, new ListItem("", ""));
                this.drop_Parent3.SelectedValue = fPrent;
                fPrent = fTParent;
                fTParent = sh.GetSignValue(EntityTypeEnum.EsTree, "FParent", "fnumber='" + fPrent + "'");
                this.drop_Parent1.SelectedValue = fTParent;
                dt = sh.GetTable(EntityTypeEnum.EsTree, "FName,FNumber", "FParent='" + fTParent + "'");
                this.drop_Parent2.DataSource = dt;
                this.drop_Parent2.DataTextField = "FName";
                this.drop_Parent2.DataValueField = "FNumber";
                this.drop_Parent2.DataBind();
                this.drop_Parent2.Items.Insert(0, new ListItem("", ""));
                this.drop_Parent2.SelectedValue = fTParent;

            }
        }
    }
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = tool.getPageValue();

        if (t_FNumber.Text != null && t_FNumber.Text != "")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select count(*) from cf_sys_tree where fnumber='" + t_FNumber.Text + "'");
            if (this.ViewState["FID"] != null)
            {
                sb.Append(" and fid<>'" + this.ViewState["FID"] + "'");
            }
            int Count = sh.GetSQLCount(sb.ToString());
            if (Count > 0)
            {
                tool.showMessage("栏目编码重复！");
                return;
            }

        }


        if (this.ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
        }
        if (this.drop_Parent1.SelectedValue != "")
        {
            sl.Add("FParent", this.drop_Parent1.SelectedValue);
        }
        if (this.drop_Parent2.SelectedValue != "")
        {
            sl.Remove("FParent");
            sl.Add("FParent", this.drop_Parent2.SelectedValue);
        }
        if (this.drop_Parent3.SelectedValue != "")
        {
            sl.Remove("FParent");
            sl.Add("FParent", this.drop_Parent3.SelectedValue);
        }
        if (sh.SaveEBase(EntityTypeEnum.EsTree, sl, "FID", so))
        {
            tool.showMessageAndRunFunction("保存成功", "pageReload();");
            this.ViewState["FID"] = sl["FID"].ToString();
        }
        else
        {
            tool.showMessage("保存失败");
            ControlBind();
        }
    }
    private void DelInfo()
    {
        pageTool tool = new pageTool(this.Page);
        if (this.ViewState["FID"] == null)
        {
            tool.showMessage("请选择要删除的栏目");
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" fid='" + this.ViewState["FID"].ToString() + "'");
        if (sh.DelEBase(EntityTypeEnum.EsTree, sb.ToString(), true))
        {
            tool.showMessageAndRunFunction("删除成功", "pageReload();");
        }
        else
        {
            tool.showMessage("删除失败,请重试");
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        this.t_FName.Text = "";
        this.t_FAdminUrl.Text = "";
        this.t_FWebUrl.Text = "";
        this.ViewState["FID"] = null;
        SparentBind();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        DelInfo();
    }
    protected void drop_Parent1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SparentBind();
    }
    private void SparentBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fname,fnumber from cf_sys_tree where fisdeleted=0 and fparent='" + this.drop_Parent1.SelectedValue + "'order by forder,fcreatetime desc");
        this.drop_Parent2.DataSource = sh.GetTable(sb.ToString());
        this.drop_Parent2.DataTextField = "FName";
        this.drop_Parent2.DataValueField = "FNumber";
        this.drop_Parent2.DataBind();
        this.drop_Parent2.Items.Insert(0, new ListItem("", ""));
    }
    private void SparentBind2()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fname,fnumber from cf_sys_tree where fisdeleted=0 and fparent='" + this.drop_Parent2.SelectedValue + "'order by forder,fcreatetime desc");
        this.drop_Parent3.DataSource = sh.GetTable(sb.ToString());
        this.drop_Parent3.DataTextField = "FName";
        this.drop_Parent3.DataValueField = "FNumber";
        this.drop_Parent3.DataBind();
        this.drop_Parent3.Items.Insert(0, new ListItem("", ""));
    }
    protected void t_FLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        SparentBind();
    }
    protected void drop_Parent2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SparentBind2();
    }
}
