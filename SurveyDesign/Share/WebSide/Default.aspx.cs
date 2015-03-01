using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using Approve.RuleCenter;
using System.Data;
using System.Data.SqlClient;
using ProjectData;
using SCJSTWEB;

public partial class Share_WebSide_Default : System.Web.UI.Page
{
    Share rc = new Share();

    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            showInfo();
        }
    }

    //显示
    private void showInfo()
    {



        lit_01.Text = showList("60801", 7, 190, true);
        lit_02.Text = showList("60802", 7, 190, true);
        lit_03.Text = showList("60803", 7, 190, true);


        lit_04.Text = showPrjList(280, 7, 190, true);
        lit_60404.Text = showPrjList(296, 7, 190, true);
        lit_60401.Text = showPrjList(300, 7, 190, true);

        lit_05.Text = showZBXX("60807", 12, 180, false);
    }


    /// <summary>
    /// 得到文章列表
    /// </summary>
    /// <param name="col">栏目编号</param> 
    /// <param name="top">取多少条</param>
    /// <param name="len">文章标题长度（字节）</param>
    /// <param name="isDate">是否显示时间</param>
    private string showZBXX(string col, int top, int w, bool isDate)
    {
        string s = "";
        NewsService rnews = new NewsService();

        int pagecount = 0;

        DataTable dt = rnews.GetNewsList("scjst.gov.cn", "254004", 1, 8, ref  pagecount);
        if (dt != null)
        {


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string page = "http://www.scjst.gov.cn/webSite/main/pageDetail.aspx";


                s += "<li><tt><b></b><a target='_blank' style='width:" + w + "px' ";

                s += "href='" + page + "?fid=" + EConvert.ToString(dt.Rows[i]["FID"]) + "&fcol=254004' target='_Blank' title='" + EConvert.ToString(dt.Rows[i]["FName"]) + "'";

                s += ">";

                s += EConvert.ToString(dt.Rows[i]["FName"]);
                s += "</a>" + (isDate ? "<u>[" + EConvert.ToString(dt.Rows[i]["FPubTime"]) + "]</u>" : "") + "</tt></li>";
            }


        }

        return s;
    }

    /// <summary>
    /// 提取业务办理数据
    /// </summary>
    /// <param name="col"></param>
    /// <param name="top"></param>
    /// <param name="w"></param>
    /// <param name="isDate"></param>
    /// <returns></returns>
    private string showPrjList(int col, int top, int w, bool isDate)
    {
        string s = "";

        var v = (from p in db.CF_Prj_Data
                 join a in db.CF_App_List on p.FId equals a.FLinkId
                 where p.FType == col && a.FManageTypeId == col && a.FIsDeleted == false && a.FState == 6
                 orderby p.FCreateTime descending
                 select new
                 {
                     a.FId,
                     p.FPrjName
                 }).Take(top);


        foreach (var t in v)
        {
            string page = "Article1.aspx";
            if (col == 296)
                page = "Article1.aspx";
            else if (col == 280)
                page = "Article2.aspx";
            else if (col == 300)
                page = "Article3.aspx";

            s += "<li><tt><b></b><a target='_blank' style='width:" + w + "px' ";

            s += "href='" + page + "?fid=" + t.FId + "' target='_Blank' title='" + t.FPrjName + "'";

            s += ">";

            s += rc.SubStr(t.FPrjName, 12);
            s += "</a></li>";
        }

        return s;
    }


    /// <summary>
    /// 得到文章列表
    /// </summary>
    /// <param name="col">栏目编号</param> 
    /// <param name="top">取多少条</param>
    /// <param name="len">文章标题长度（字节）</param>
    /// <param name="isDate">是否显示时间</param>
    private string showList(string col, int top, int w, bool isDate)
    {
        string s = "";

        var v = (from t in db.CF_News_Title
                 join c in db.CF_News_Col on t.FID equals c.FNewsId
                 where (c.FColNumber == col) && c.FState == 1
                 orderby t.FOrder, t.FCreateTime descending
                 select new { t.FName, t.FPubTime, c.FColor, t.FID, c.FColNumber, t.FOperType, t.FWebId }).Take(top);

        foreach (var t in v)
        {
            string page = "Article.aspx";
            string FWebId = t.FWebId;

            s += "<li><tt><b></b><a target='_blank' style='width:" + w + "px' ";
            switch (t.FOperType)
            {
                case 0:
                case 1:
                    s += "href='" + page + "?fid=" + t.FID + "&fcol=" + t.FColNumber + "' target='_Blank' title='" + t.FName + "'";
                    break;
                case 2:
                case 3:
                    if (FWebId.StartsWith("http://")) FWebId = FWebId.Substring(7);
                    s += "href='http://" + FWebId + "' target='_Blank' title='" + t.FName + "'";
                    break;
            }
            s += ">";
            if (!string.IsNullOrEmpty(t.FColor) && t.FColor != "&nbsp;")
            {
                s += "<font color='" + t.FColor + "'>" + t.FName + "</font>";
            }
            else
                s += t.FName;
            s += "</a>" + (isDate ? "<u>[" + t.FPubTime.GetValueOrDefault().ToShortDateString() + "]</u>" : "") + "</tt></li>";
        }

        return s;
    }




}
