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
using Approve.EntityBase;
using Approve.RuleCenter;
using Approve.Common;
using System.Text;

public partial class Admin_main_Revert : System.Web.UI.Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            this.btnSave.Attributes.Add("onclick", "if(checkInfo()){return true;}else{return false;}");
            if (!string.IsNullOrEmpty(EConvert.ToString(this.Session["DFName"])))
            {
                t_FRevertPerson.Text = this.Session["DFName"].ToString();
            }
            else
            {
                t_FRevertPerson.Text = "匿名";
            }
            ControlBind();
            this.t_FRevertDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (Request["fid"] != null && Request["fid"] != "")
            {

                this.ViewState["FID"] = Request["fid"];

                ShowInfo();
            }
        }
    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = rc.GetTable(EntityTypeEnum.ElInfo, "", sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
        }
        if (this.t_FRevertDate.Text.Trim() == "")
        {
            this.t_FRevertDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        if (this.t_FRevertPerson.Text.Trim() == "")
        {
            if (!string.IsNullOrEmpty(EConvert.ToString(this.Session["DFName"])))
            {
                t_FRevertPerson.Text = this.Session["DFName"].ToString();
            }
            else
            {
                t_FRevertPerson.Text = "匿名";
            }
        }

    }
    private void ControlBind()
    {
        DataTable dt = rc.getDicTbByFNumber("120");
        this.t_FLinkTypeId.DataSource = dt;
        this.t_FLinkTypeId.DataTextField = "FName";
        this.t_FLinkTypeId.DataValueField = "FNumber";
        this.t_FLinkTypeId.DataBind();
    }
    private void SaveInfo()
    {
        if (this.ViewState["FID"] == null)
        {
            return;
        }
        pageTool tool = new pageTool(this.Page);
        SortedList sl = tool.getPageValue();
        EntityTypeEnum en = EntityTypeEnum.ElInfo;
        string fkey = "FID";
        SaveOptionEnum so = SaveOptionEnum.Update;

        sl.Add("FState", 1);
        sl.Add("FID", this.ViewState["FID"]);
        if (rc.SaveEBase(en, sl, fkey, so))
        {
            tool.showMessageAndRunFunction("回复成功", "window.returnValue='1';window.close();");
        }
        else
        {
            tool.showMessage("回复失败");
        }


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
}
