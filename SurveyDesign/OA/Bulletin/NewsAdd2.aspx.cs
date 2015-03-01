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
using Seaskyer.Strings;
using Seaskyer.WebApp.Utility;
using ProjectData;

public partial class OA_Bulletin_NewsAdd2 : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!base.IsPostBack)
        {
            this.t_FValidEnd.Text = DateTime.Now.AddYears(10).ToString("yyyy-MM-dd");
            
            ViewState["fcol"] = Request.QueryString["fcol"];

            if (ViewState["fcol"] == null)
            {
                Response.Clear();
                return;
            }
            SetPostion();
            this.btnSave.Attributes.Add("onclick", "return AutoCheckInfo();");
            if (this.Request["fid"] == null)
            {
                this.t_FPubTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.t_FOrder.Text = "50000";
            }
            else
                ShowInfo();
        }
    }
    void SetPostion()
    {
        switch (ViewState["fcol"].ToString())
        {
            case "600":
                this.Title = lPostion.Text = "企业文件通知";
                break;
            case "800":
                this.Title = lPostion.Text = "企业通知公告";
                break;
            default:
                this.Title = lPostion.Text = "文件通知";
                break;
        }
    }
    void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        this.ViewState["FID"] = Request.QueryString["fid"];
        DataTable dt = rc.GetTable(EntityTypeEnum.EnTitle, "*", "fid='" + ViewState["FID"] + "' ");
        if (dt.Rows.Count < 1)
        {
            tool.showMessageAndEndPage("数据有误，没有最新版本数据！");
            return;
        }
        else
        {
            DataRow dr = dt.Rows[0];
            tool.fillPageControl(dr);
            CBPublish.Checked = dr["fstate"].ToString() == "1";
            string sOperType = dr["FOperType"].ToString();
            Hfnewsid.Value = this.ViewState["FID"].ToString();
            FMain.Value = dr["FMain"].ToString();
        }
    }
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = tool.getPageValue();//news 
        SaveOptionEnum so = SaveOptionEnum.Update;
        string FId = Guid.NewGuid().ToString();
        if (this.ViewState["FID"] == null)
        {
            so = SaveOptionEnum.Insert;
            sl.Add("fcreatetime", DateTime.Now);
            sl.Add("FCount", 0);
            sl.Add("FManageDeptId", Session["DFId"]);
            sl.Add("FOperator", Session["DFUserId"]);
        }
        else
            FId = this.ViewState["FID"].ToString();
        sl.Add("FID", FId);
        sl.Add("Fisdeleted", 0);
        sl.Add("fstate", CBPublish.Checked ? "1" : "0");
        sl.Add("FIsPub", CBPublish.Checked ? "1" : "0");
        sl.Add("FPubOperator", CBPublish.Checked ? Session["DFUserId"] : "");
        sl.Add("FPubManageDeptId", CBPublish.Checked ? Session["DFId"] : "");
        string content = this.FMain.Value.ToString();
        content = content.Replace("'", "''");
        sl.Add("FMain", content);
        sl.Add("FType", ViewState["fcol"]);
        if (rc.SaveEBase(EntityTypeEnum.EnTitle, sl, "FID", so))
        {
            this.ViewState["FID"] = sl["FID"].ToString();
            Hfnewsid.Value = this.ViewState["FID"].ToString();
            tool.showMessageAndRunFunction("保存成功！", "window.returnValue='1';");
        }
        else
        {
            tool.showMessage("保存失败！");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
}
