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

public partial class Admin_main_QualiAppConditionAdd : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
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
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fnumber, fname from cf_sys_systemname where fisdeleted=0 ");
        sb.Append(" order by forder");
        DataTable dt = sh.GetTable(sb.ToString());
        this.t_FSystemId.DataSource = dt;
        this.t_FSystemId.DataTextField = "FName";
        this.t_FSystemId.DataValueField = "FNumber";
        this.t_FSystemId.DataBind();
        t_FSystemId.Items.Insert(0, new ListItem("请选择", ""));

        //this.t_FQualiLevelId.Items.Insert(0, new ListItem("请先选择所属系统", ""));
        //this.t_FQualiListId.Items.Insert(0, new ListItem("请先选择所属系统", ""));
        //this.t_FQualiTypeId.Items.Insert(0, new ListItem("请先选择所属序列", ""));

    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_App_QualiCondition where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            ShowQualiLevelData(this.t_FSystemId.SelectedValue.Trim());
            this.t_FQualiLevelId.SelectedIndex = t_FQualiLevelId.Items.IndexOf(this.t_FQualiLevelId.Items.FindByValue(dt.Rows[0]["FQualiLevelId"].ToString().Trim()));

            ShowListData(this.t_FSystemId.SelectedValue.Trim());
            this.t_FQualiListId.SelectedIndex = t_FQualiListId.Items.IndexOf(this.t_FQualiListId.Items.FindByValue(dt.Rows[0]["FQualiListId"].ToString().Trim()));


            ShowTypeData(this.t_FQualiListId.SelectedValue.Trim());
            this.t_FQualiTypeId.SelectedIndex = t_FQualiTypeId.Items.IndexOf(this.t_FQualiTypeId.Items.FindByValue(dt.Rows[0]["FQualiTypeId"].ToString().Trim()));


        }
    }
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = tool.getPageValue();
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
        if (sh.SaveEBase(EntityTypeEnum.EaQualiCondition, sl, "FID", so))
        {
            tool.showMessage("保存成功");
            this.ViewState["FID"] = sl["FID"].ToString();
            HSaveResult.Value = "1";
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        this.ViewState["FID"] = null;
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("clearPage()");
    }
    private void ShowQualiLevelData(string fSystemId)
    {
        string sSystemid = "";
        switch (fSystemId)
        {
            case "101": //施工企业
                sSystemid = "100";
                break;
            case "120": //招标代理企业
                sSystemid = "108";
                break;
            case "125": //工程监理企业
                sSystemid = "105";

                break;
            case "130": //房地产企业
                sSystemid = "150";

                break;
            case "135": //园林绿化企业
                sSystemid = "180";

                break;
            case "140": //外来勘察设计企业
                sSystemid = "170";

                break;
            case "145": //施工图审查企业
                sSystemid = "160";

                break;
            case "150": //安全生产许可证管理信息系统
                sSystemid = "100";
                break;
            case "155": //勘察设计企业
                sSystemid = "190";

                break;
            case "160": //监理工程师管理系统
                sSystemid = "190";

                break;
            case "165": //建造师管理系统
                sSystemid = "210";

                break;
        }
        if (sSystemid == "")
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fname,fnumber from CF_Sys_QualiLevel");
        sb.Append(" where fsystemid=" + sSystemid);
        sb.Append(" and fisdeleted=0 order by flevel");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            this.t_FQualiLevelId.Items.Clear();
            this.t_FQualiLevelId.DataSource = dt;
            this.t_FQualiLevelId.DataTextField = "FName";
            this.t_FQualiLevelId.DataValueField = "FNumber";
            this.t_FQualiLevelId.DataBind();
            this.t_FQualiLevelId.Items.Insert(0, new ListItem("请选择", ""));
        }
    }

    private void ShowListData(string fSystemId)
    {

        //this.t_FQualiListId.Items.Clear();
        //this.t_FQualiListId.Items.Insert(0, new ListItem("请选择", ""));
        //t_FQualiListId.SelectedIndex = 0;
        StringBuilder sb = new StringBuilder();


        switch (fSystemId)
        {
            case "101": //施工企业
                sb.Append(" select fname,fnumber from cf_sys_dic");
                sb.Append(" where fparentid=105");
                sb.Append(" and fisdeleted=0 order by forder");

                break;
            case "120": //招标代理企业
                sb.Append(" select fname,fnumber from cf_sys_dic");
                sb.Append(" where fparentid=136");
                sb.Append(" and fisdeleted=0 order by forder");

                break;
            case "125": //工程监理企业
                sb.Append(" select fname,fnumber from cf_sys_dic");
                sb.Append(" where fparentid=135");
                sb.Append(" and fisdeleted=0 order by forder");


                break;
            case "130": //房地产企业
                sb.Append(" select fname,fnumber from cf_sys_dic");
                sb.Append(" where fparentid=137");
                sb.Append(" and fisdeleted=0 order by forder");


                break;
            case "135": //园林绿化企业
                sb.Append(" select fname,fnumber from cf_sys_dic");
                sb.Append(" where fparentid=138");
                sb.Append(" and fisdeleted=0 order by forder");


                break;
            case "140": //外来勘察设计企业
                sb.Append(" select fname,fnumber from cf_sys_dic");
                sb.Append(" where fparentid=139");
                sb.Append(" and fisdeleted=0 order by forder");


                break;
            case "145": //施工图审查企业
                sb.Append(" select fname,fnumber from cf_sys_dic");
                sb.Append(" where fparentid=131");
                sb.Append(" and fisdeleted=0 order by forder");


                break;
            case "150": //安全生产许可证管理信息系统
                sb.Append(" select fname,fnumber from cf_sys_dic");
                sb.Append(" where fparentid=130");
                sb.Append(" and fisdeleted=0 order by forder");

                break;
            case "155": //勘察设计企业  
                sb.Append(" select t.fnumber,");
                sb.Append(" (select top 1 fname from cf_sys_dic where fparentid = 134 and fnumber = t.fparentid)");
                sb.Append(" +' -- '+t.fname fname");
                sb.Append(" from cf_sys_dic t ");
                sb.Append(" where t.fparentid in (134001,134002 )");
                sb.Append(" order by fparentid ,forder");


                break;
            case "160": //监理工程师管理系统
                sb.Append(" select fname,fnumber from cf_sys_dic");
                sb.Append(" where fparentid=132");
                sb.Append(" and fisdeleted=0 order by forder");


                break;
            case "165": //建造师管理系统
                sb.Append(" select fname,fnumber from cf_sys_dic");
                sb.Append(" where fparentid=133");
                sb.Append(" and fisdeleted=0 order by forder");
                break;
        }



        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {



            this.t_FQualiListId.Items.Clear();
            this.t_FQualiListId.DataSource = dt;
            this.t_FQualiListId.DataTextField = "FName";
            this.t_FQualiListId.DataValueField = "FNumber";
            this.t_FQualiListId.DataBind();
            this.t_FQualiListId.Items.Insert(0, new ListItem("请选择", ""));

        }
    }


    private void ShowTypeData(string fParentId)
    {
        string fNumber = "";
        fNumber = fParentId;
        if (fNumber == "")
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fname,fnumber from cf_sys_dic");
        sb.Append(" where fparentid=" + fNumber);
        sb.Append(" and fisdeleted=0 order by flevel");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            this.t_FQualiTypeId.Items.Clear();
            this.t_FQualiTypeId.DataSource = dt;
            this.t_FQualiTypeId.DataTextField = "FName";
            this.t_FQualiTypeId.DataValueField = "FNumber";
            this.t_FQualiTypeId.DataBind();
            this.t_FQualiTypeId.Items.Insert(0, new ListItem("请选择", ""));
        }
    }


    protected void t_FSystemId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.t_FSystemId.SelectedValue.Trim() == "")
        {
            this.t_FQualiLevelId.Items.Clear();
            this.t_FQualiLevelId.Items.Insert(0, new ListItem("请先选择所属系统", ""));

            this.t_FQualiListId.Items.Clear();
            this.t_FQualiListId.Items.Insert(0, new ListItem("请先选择所属系统", ""));

            this.t_FQualiTypeId.Items.Clear();
            this.t_FQualiTypeId.Items.Insert(0, new ListItem("请先选择所属序列", ""));
        }
        else
        {
            ShowQualiLevelData(this.t_FSystemId.SelectedValue.Trim());
            ShowListData(this.t_FSystemId.SelectedValue.Trim());
        }
        //this.t_FQualiTypeId.Items.Clear();
        //this.t_FQualiTypeId.Items.Insert(0, new ListItem("请先选择所属序列", ""));



    }
    protected void dbList_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.t_FQualiTypeId.Items.Clear();
        if (t_FQualiListId.SelectedValue.Trim() != "")
        {
            ShowTypeData(this.t_FQualiListId.SelectedValue.Trim());
        }

    }
}
