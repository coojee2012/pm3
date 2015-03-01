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

public partial class OA_DailyMange_zAdd : System.Web.UI.Page
{
    OA oa = new OA();
    DateTime date = DateTime.Now;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //接到的传递的参数 
            if (Request.QueryString["date"] != null)
            {
                date = DateTime.Parse(Request.QueryString["date"]);
            }
            else
            {
                date = DateTime.Now;
            }

            showinfo();

        }
    }
    private void showinfo()
    {
        t_FDate.Text = date.ToString("yyyy-MM-dd");

        pageTool tool = new pageTool(this.Page);

        StringBuilder sb = new StringBuilder();
        sb.Append("select * from  cf_oa_Calendar ");
        sb.Append("where FUserId = '" + Session["FEmpID"] + "' and ");
        sb.Append("(convert(varchar(10),FDate,25)='" + date.ToString("yyyy-MM-dd") + "')");

        DataTable dt = oa.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            ViewState["FID"] = dt.Rows[0]["FID"].ToString();
            tool.fillPageControl(dt.Rows[0]);
        }
    }

    //保存补入信息
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        SaveOptionEnum so = SaveOptionEnum.Update;
        if (ViewState["FID"] == null)
        {
            sl.Add("FID", Guid.NewGuid());
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FIsDeleted", 0);
            sl.Add("FUserId", Session["FEmpID"]);
            so = SaveOptionEnum.Insert;
        }
        else
        {
            sl.Add("FID", ViewState["FID"]);
        }
        sl.Add("FDate", t_FDate.Text);
        sl.Add("FContent", t_FContent.Text);

        if (oa.SaveEBase("cf_oa_Calendar", sl, "FID", so))
        {
            ViewState["FID"] = sl["FID"].ToString();
            tool.showMessageAndRunFunction("保存成功!", "window.returnValue=1;");
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }

}
