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
using System.Data.SqlClient;
using Approve.Common;
using Approve.EntityBase;
using System.Net;
using System.IO;

public partial class Admin_mainother_SmsEdit : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnAdd.Attributes.Add("onclick", "return CheckInfo();");

            showInfo();
            if (string.IsNullOrEmpty(t_FPlanTime.Text))
            {
                t_FPlanTime.Text = DateTime.Now.ToString();
            }
        }
    }

    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_OA_SmsList where FID=@FID");
        DataTable dt = rc.GetTable(sb.ToString(), new SqlParameter("@FID", Request.QueryString["FID"]));
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            ViewState["FID"] = dt.Rows[0]["FID"].ToString();
            if (t_FState.SelectedValue == "0")
            {
                btnAdd.Visible = true;
            }
            else
            { btnAdd.Visible = false; }
        }
    }


    //保存
    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page);

        if (!vilideCode.IsPass(tt_code.Value.Trim().ToLower()))
        {
            tool.showMessage("验证码输入错误！");
            return;
        }

        if (t_FContent.Text.Length > 200)
        {
            tool.showMessage("不能超过200个字");
            return;
        }
        SaveOptionEnum so = SaveOptionEnum.Update;
        SortedList sl = tool.getPageValue();
        if (ViewState["FID"] != null)
        {
            sl.Add("FID", ViewState["FID"]);
        }
        else
        {
            so = SaveOptionEnum.Insert;
            sl.Add("FID", Guid.NewGuid());
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FIsDeleted", 0);


        }

        try
        {
            String url = "http://ipyy.net/WS/BatchSend.aspx?CorpID=YY70026&Pwd=7758258&Mobile=" + t_FMobile.Text + "&Content=" + HttpUtility.UrlEncode(t_FContent.Text, Encoding.GetEncoding("gb2312")) + "&Cell=&SendTime=";
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default);
            string ct = reader.ReadToEnd();

            reader.Close();
            sl.Add("FSubmitTime", DateTime.Now);
            if (ct == "0")
            {
                sl["FState"] = 1;
            }
            else
            {
                sl["FState"] = 2;
            }
        }
        catch
        {
            sl["FState"] = 2;
        }
        if (rc.SaveEBase(EntityTypeEnum.EOASMSList, sl, "FID", so))
        {

            ViewState["FID"] = sl["FID"].ToString();
            tool.showMessageAndRunFunction("发送成功", "window.returnValue=1;");
        }
        else
        {
            tool.showMessage("发送成功");
        }
        btnAdd.Visible = false;
    }



    //保存 按钮
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}
