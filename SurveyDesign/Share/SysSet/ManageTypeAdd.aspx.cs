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
public partial class Admin_main_ManageTypeAdd : Page
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
        sb.Append("select fid,Fname,fnumber from CF_Sys_SystemName where fisdeleted=0 order by forder ");
        DataTable dt = sh.GetTable(sb.ToString());
        this.t_FSystemId.DataSource = dt;
        this.t_FSystemId.DataTextField = "FName";
        this.t_FSystemId.DataValueField = "FNumber";
        this.t_FSystemId.DataBind();
        t_FSystemId.Items.Insert(0, new ListItem("所有系统", "0"));

        dt = sh.getDicTbByFNumber("104");
        this.t_FTypeId.DataSource = dt;
        this.t_FTypeId.DataTextField = "FName";
        this.t_FTypeId.DataValueField = "FNumber";
        this.t_FTypeId.DataBind();
    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Sys_ManageType where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            this.FMain.Value = sh.GetSignValue(EntityTypeEnum.EPText, "FContent", "FLinkId='" + this.ViewState["FID"] + "'");
        }
    }
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = tool.getPageValue();

        #region 验证
        StringBuilder sb = new StringBuilder();
        sb.Append(" select count(*) from CF_Sys_ManageType where fnumber='" + t_FNumber.Text + "' and fid<>'" + EConvert.ToString(ViewState["FID"]) + "'");
        if (sh.GetSQLCount(sb.ToString()) > 0)
        {
            tool.showMessage("编码重复");
            t_FNumber.Focus();
            return;
        }
        sb.Remove(0, sb.Length);

        #endregion

        if (this.ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
        }
        if (sh.SaveEBase(EntityTypeEnum.EsManageType, sl, "FID", so))
        {
            this.ViewState["FID"] = sl["FID"].ToString();
            SaveIntroduce();
            tool.showMessageAndRunFunction("保存成功","window.returnValue=1;"); 
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }
    private void SaveIntroduce()
    {
        string fid = sh.GetSignValue(EntityTypeEnum.EPText, "fid", "FLinkId='" + this.ViewState["FID"].ToString() + "'");
        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = new SortedList();
        if (fid != null && fid != "")
        {
            sl.Add("FID", fid);
            so = SaveOptionEnum.Update;
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsdeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
        }
        sl.Add("FLinkId", this.ViewState["FID"].ToString());
        sl.Add("FContent", this.FMain.Value);
        sh.SaveEBase(EntityTypeEnum.EPText, sl, "FID", so);
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

    //
    protected void btnTry_Click(object sender, EventArgs e)
    {

    }
}
