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

public partial class Share_Sys_CardNoEdit : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
            btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            if (!string.IsNullOrEmpty(Request["fid"]))
            {
                ViewState["FID"] = Request["fid"];
                ShowInfo();
            }
        }
    }

    private void ControlBind()
    {
        //批次
        StringBuilder sb = new StringBuilder();
        sb.Append("select FId,FNo FName ");
        sb.Append("From CF_Sys_UserLockInfo ");
        sb.Append("Where FIsDeleted=0 ");
        sb.Append("Order By FDate Desc");
        DataTable dt = sh.GetTable(sb.ToString());
        t_FBatchId.DataSource = dt;
        t_FBatchId.DataTextField = "FName";
        t_FBatchId.DataValueField = "FID";
        t_FBatchId.DataBind();
        t_FBatchId.Items.Insert(0, new ListItem("请选择", ""));
    }

    private void ShowInfo()
    {
        pageTool toool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append(" fid='" + ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(EntityTypeEnum.SHsLock, "", sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            toool.fillPageControl(dt.Rows[0]);
            if (dt.Rows[0]["FState"].ToString() == "1")
            {
                btnAdd.Enabled = false;
                btnAdd.ToolTip = "已分配无法修改";
            }
        }
    }

    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);

        //判断加密锁是否重复
        StringBuilder sb = new StringBuilder();
        sb.Append(" select count(*) from CF_Sys_Lock  ");
        sb.Append(" where FLockNumber='" + t_FLockNumber.Text + "'");
        if (ViewState["FID"] != null)
        {
            sb.Append(" and fid<>'" + ViewState["FID"].ToString() + "'");
        }

        int iCount = sh.GetSQLCount(sb.ToString());
        if (iCount > 0)
        {
            tool.showMessage("加密锁硬件编号重复，请仔细核对！");
            t_FLockNumber.Focus();
            return;
        }

        SortedList sl = tool.getPageValue();
        SaveOptionEnum so = SaveOptionEnum.Insert;
        if (ViewState["FID"] != null)
        {
            sl.Add("FID", ViewState["FID"].ToString());
            so = SaveOptionEnum.Update;
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
        }

        if (sh.SaveEBase(EntityTypeEnum.SHsLock, sl, "FID", so))
        {
            HSaveResult.Value = "1";
            tool.showMessage("保存成功");
            ViewState["FID"] = sl["FID"].ToString();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        this.ViewState["FID"] = null;
        btnAdd.Enabled = true;
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("clearPage()");
    }
}
