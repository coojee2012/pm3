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

public partial class OA_Bulletin_AddBullType : System.Web.UI.Page
{
    bool temp = false;
    OA oa = new OA();
    string userId = "";
    string mag = "公告类型添加成功！";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.QueryString["fid"] != null)
            {
                this.ViewState["FID"] = Request.QueryString["fid"].ToString();

                ShowInfo();
            }

        }
        this.OFFDuty.Attributes.Add("onclick", "return onchick()");
        if (Session["FEmpID"] != null)
        {
            userId = Session["FEmpID"].ToString();

        }
    }

    protected void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        DataTable dt = oa.GetTable("select * from CF_OA_BulType where FID ='" + this.ViewState["FID"].ToString() + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);

        }
        this.OFFDuty.Text = "修改公告类型";
        
    }

    protected void OFFDuty_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }



    private void SaveInfo()
    {
        
        pageTool tool = new pageTool(this.Page);
        SaveOptionEnum so = SaveOptionEnum.Insert;
        string FID="";
        if (this.t_TypeName.Text == null || this.t_TypeName.Text.Trim() == "")
        {
            tool.showMessage("请填写类型名称！");
            return;
        }
        if (Session["FEmpID"] == null)
        {
            tool.showMessage("会话过期！");
            return;
        }
        if (this.ViewState["FID"] == null)
            FID = Guid.NewGuid().ToString();
        else
        {
            FID = this.ViewState["FID"].ToString();
            so = SaveOptionEnum.Update;
            mag = "更新公告类型成功！";
            this.OFFDuty.Text = "修改公告类型";
        }


        SortedList sl = new SortedList();
        sl.Add("FID", FID);
        sl.Add("TypeName", this.t_TypeName.Text);
        sl.Add("FuserID", userId);

        sl.Add("FIsDeleted", false);

        sl.Add("FCratetime", DateTime.Now);

        if (oa.SaveEBase(EntityTypeEnum.EOABulType, sl, "FID", so))
        {

            temp = true;
        }
        if (temp)
        {
            tool.showMessage(mag);
            this.ViewState["FID"] = FID;
        }
        else
        {
            tool.showMessage("添加失败");
        }

    }


}
