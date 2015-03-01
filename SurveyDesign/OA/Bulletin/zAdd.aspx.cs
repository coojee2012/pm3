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

public partial class OA_Bulletin_AddBulletin : System.Web.UI.Page
{
    OA oa = new OA();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "return onchick()");
            if (Request.QueryString["fid"] != null && Request.QueryString["fid"].ToString() != "")
            {
                ViewState["FID"] = Request.QueryString["fid"];

                ShowInfo();
            }

        }
    }

    protected void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        DataTable dt = oa.GetTable("select * from CF_OA_Bulletin where FID ='" + this.ViewState["FID"].ToString() + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);

        }
    }


    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        if (this.t_FTitle.Text == null || this.t_FTitle.Text.Trim() == "")
        {
            tool.showMessage("公告标题必须填写！");
            return;
        }
        if (this.t_FContent.Value == null || this.t_FContent.Value.ToString().Trim() == "")
        {
            tool.showMessage("公告内容必须填写！");
            return;
        }
        if (this.t_FContent.Value.ToString().Length > 254)
        {
            tool.showMessage("内容字数超过限制，必须小于255字!");
            return;
        }

        SortedList sl = new SortedList();
        SaveOptionEnum so = SaveOptionEnum.Insert;
        if (this.ViewState["FID"] == null)
        {
            sl.Add("FID", Guid.NewGuid());
            sl.Add("FCreatetime", DateTime.Now);
            sl.Add("FIsDeleted", 0);
        }
        else
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", ViewState["FID"]);
        }
        sl.Add("FTitle", this.t_FTitle.Text);
        sl.Add("FState", t_FState.Checked ? "1" : "0");
        sl.Add("FuserID", Session["FEmpID"]);//发布人
        sl.Add("FContent", this.t_FContent.Value.ToString());


        if (oa.SaveEBase(EntityTypeEnum.EOABulletin, sl, "FID", so))
        {
            ViewState["FID"] = sl["FID"].ToString();
            tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
        }
        else
        {
            tool.showMessage("保存失败");
        }

    }



    //保存按钮 
    protected void btnSave_Click(object sender, EventArgs e)
    {


        SaveInfo();
    }
}
