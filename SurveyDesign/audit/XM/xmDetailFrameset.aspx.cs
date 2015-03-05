using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class audit_XM_xmDetailFrameset : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["XMBH"] != null && !string.IsNullOrEmpty(Request["XMBH"]))
            {
                Session["XMBH"] = Request["XMBH"].ToString();
                string sql = "select * from JKC_V_viewDetail where XMBH='" + Session["XMBH"] + "'";
                DataTable dt = rc.GetTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    ShowWindow(row["XMMC"].ToString());
                }
                else {
                    ShowWindow(null);
                }
            }
        }
    }
    private void ShowWindow(string XMMC)
    {
        string projectName = XMMC;// row["XMMC"].ToString();
        

        ltrAQJDimg.Text = "<img src=\"../images/hp_xmjd_dbl.jpg\" alt=\"安全监督备案\" width=\"12\" height=\"12\" />";
        ltrZLJDImg.Text = "<img src=\"../images/hp_xmjd_dbl.jpg\" alt=\"质量监督备案\" width=\"12\" height=\"12\" />";
        string SGXKZurl = "../../TempTable/SGXKZ/A2.html";
        string GCGHurl = "../../TempTable/GCGH/A2.html";
        string YDGHurl = "../../TempTable/YDGH/A2.html";
        string XZYJSurl = "../../TempTable/XZYJS/A2.html";
        string AQJDBASurl = "../../TempTable/AQJDBA/A2.html";
        string JGYSBAurl = "../../TempTable/JGYSBA/A2.html";
        string XMBJurl = "../../TempTable/XMBJ/A2.html";
        string ZTBXXurl = "../../TempTable/ZTBXX/A2.html";
        string ZLJDBAurl = "../../TempTable/ZLJDBA/A2.html";
        if (projectName == "荥经县附城乡中心小学灾后重建工程")
        {
            SGXKZurl = "../../TempTable/SGXKZ/A.html";
            GCGHurl = "../../TempTable/GCGH/A.html";
            YDGHurl = "../../TempTable/YDGH/A.html";
            XZYJSurl = "../../TempTable/XZYJS/A.html";
            AQJDBASurl = "../../TempTable/AQJDBA/A.html";
            JGYSBAurl = "../../TempTable/JGYSBA/A.html";
            XMBJurl = "../../TempTable/XMBJ/A.html";
            ZTBXXurl = "../../TempTable/ZTBXX/A.html";
            ZLJDBAurl = "../../TempTable/ZLJDBA/A.html";
            ltrAQJDimg.Text = " <img src=\"../images/hp_xmjd_ybl.jpg\" alt=\"安全监督备案\" width=\"12\" height=\"12\" />";
            ltrZLJDImg.Text = "<img src=\"../images/hp_xmjd_ybl.jpg\" alt=\"质量监督备案\" width=\"12\" height=\"12\" />";
            ltrZLJDBA.Text = "<a href='#' onclick=\"ShowWindow('" + ZLJDBAurl + "')\">质量监督备案</a>";
            ltrAQJDBA.Text = "<a href='#' onclick=\"ShowWindow('" + AQJDBASurl + "')\">安全监督备案</a>";
        }
        else {
            ltrZLJDBA.Text = "<span style=\"color:#d9e1e4\">质量监督备案</span>";
            ltrAQJDBA.Text = "<span style=\"color:#d9e1e4\">安全监督备案</span>";
        }
        ltrSGXKZ.Text = "<a href='#' onclick=\"ShowWindow('" + SGXKZurl + "')\">施工许可证</a>";
        ltrGCGH.Text = "<a href='#' onclick=\"ShowWindow('" + GCGHurl + "')\">工程规划许可证</a>";
        ltrYDGH.Text = "<a href='#' onclick=\"ShowWindow('" + YDGHurl + "')\">用地规划许可证</a>";
        ltrXZYJS.Text = "<a href='#' onclick=\"ShowWindow('" + XZYJSurl + "')\">选址意见书</a>";
        ltrJGYSBA.Text = "<a href='#' onclick=\"ShowWindow('" + JGYSBAurl + "')\">竣工验收备案</a>";
        ltrXMBJ.Text = "<a href='#' onclick=\"ShowWindow('" + XMBJurl + "')\">项目报建</a>";
        ltrZTBXX.Text = "<a href='#' onclick=\"ShowWindow('" + ZTBXXurl + "')\">招投标备案</a>";
    }
}