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
using Approve.EntityBase;
using System.Data.SqlClient;

public partial class jzjn_main_Article : System.Web.UI.Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }

    //显示
    public void showInfo()
    {
        string FID = Request.QueryString["FID"];
        if (string.IsNullOrEmpty(FID) && !string.IsNullOrEmpty(Request.QueryString["FCol"]))
        {//如果传的是FCol则从该栏目中得到最头一篇文章
            FID = rc.GetSignValue("SELECT top 1 n.FID FROM CF_News_Title n,CF_News_Col c where n.fid=c.fnewsid  and c.FColNumber=@FColNumber order by c.FOrder,n.FTime desc", new SqlParameter("@FColNumber", Request.QueryString["FCol"]));
        }
        SortedList sl = new SortedList();
        StringBuilder sb = new StringBuilder();
        string sql = "SELECT n.FName,n.FCount,n.FMain,c.FContent,n.FPubTime FROM CF_News_Title n,CF_News_Content c where n.fid=c.fnewsid  and n.fid=@fid";
        DataTable dt = rc.GetTable(sql, new SqlParameter("@FID", FID));
        if (dt != null && dt.Rows.Count > 0)
        {
            string FCol = rc.GetSignValue("select top 1 FColNumber from CF_News_Col where FNewsId=@FID ", new SqlParameter("@FID", FID));

            //标题
            newstitle.Text = dt.Rows[0]["FName"].ToString();
            //内容
            newsCount.Text = dt.Rows[0]["FContent"].ToString();
            //发布时间
            pubTime.Text = EConvert.ToDateTime(dt.Rows[0]["FPubTime"].ToString()).ToString("yyyy-MM-dd");
            //发布人
            pubPerson.Text = "管理员";

            //浏览次数
            int x = 1;
            if (dt.Rows[0]["FCount"].ToString() != null && dt.Rows[0]["FCount"].ToString() != "")
            {
                x = (int)dt.Rows[0]["FCount"] + 1;
                this.Count.Text = x.ToString();
            }
            else
            {
                this.Count.Text = x.ToString();
            }

            //摘要
            if (dt.Rows[0]["FMain"] != null && dt.Rows[0]["FMain"].ToString() != "")
            {
                string st = "<div class='Articel_Abs'><span style='color: #D14500; font-size: 14px; font-weight: bold; margin-left: 5px;'>摘 要：</span>";
                st += dt.Rows[0]["FMain"].ToString() + "</div>";
                zhaiyao.Text = st;
            }

            //访问次数累计 更新
            rc.PExcute("update CF_News_Title set fcount= " + x + " where FID=@FID", new SqlParameter("@FID", FID));
        }
        else
        {
            Server.Transfer("~/NoPage.aspx");
        }
    }
}
