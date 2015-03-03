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
using ProjectData;
using System.Linq;

public partial class EvaluateEntApp_main_left1 : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private RQuali rq = new RQuali();
    private string FManageTypeId = "7070";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CreateTree();

            showInfo();
        }
    }


    //显示
    private void showInfo()
    {
        
        ProjectDB db = new ProjectDB();
        var data = (from t in db.Menu
                where t.FManageTypeId == FManageTypeId && t.FIsDis == 1
                && t.FIsDeleted == false && t.FLevel == 3
                orderby t.FOrder, t.FCreateTime ascending
                select new
                {
                    t.FNumber,
                    t.FName,
                    FQUrl = GetUrl(t.FQUrl)
                }).ToList();
        
        var ChildData = db.Menu.Where(t => t.FManageTypeId == FManageTypeId && t.FIsDis == 1
                && t.FIsDeleted == false && t.FLevel == 4).OrderBy(x => x.FOrder).Select(g => new {g.FName,g.FQUrl,g.FParentId }).ToList();
        StringBuilder _builder = new StringBuilder();
        if (data.Count > 0)
        {
            data.ForEach(item =>
            {
                var childList = ChildData.Where(x => x.FParentId == item.FNumber).ToList();
                if (childList.Count < 1)
                {
                    _builder.AppendFormat("<div class=\"l_menu\"><a class=\"l_m_a\" href='{0}' target=\"appMain\"><span>{1}</span></a></div>", item.FQUrl, item.FName);
                }
                else
                {
                    _builder.AppendFormat(" <div class=\"o_menu\" >");
                    _builder.AppendFormat("<a id=\"a12\" class=\"o_m01_1\" style=\"cursor: pointer\"><span>{0}</span></a>", item.FName);
                    _builder.Append("<div class=\"o_smenu\">");
                    childList.ForEach(child =>
                    {
                        _builder.AppendFormat("<a class=\"o_m02_1\" href='{0}' target=\"appMain\"><span>{1}</span></a>", GetUrl(child.FQUrl), child.FName);
                        _builder.AppendFormat("<div class=\"o_smenu2\"></div>");
                    });
                    _builder.Append("</div>");
                    _builder.Append("</div>");
                }
            });
        }
        ltrText.Text = _builder.ToString();
        //rptMenu.DataSource = data;
        //rptMenu.DataBind();
        
    }
    private string GetUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
            return string.Empty;
        string fAppId = Request.QueryString["fAppId"];
        string projectId = Request.QueryString["projectId"];
        string JG_Id = Request.QueryString["JG_Id"];
        
        if (url.Contains("?"))
        {
            if (url.Contains("ReportPrint"))//报表
            {
                url += "&YWBM=" + JG_Id;
            }
            return url;
        }
        string param = url + "?JG_Id=" + JG_Id + "&fAppId=" + fAppId + "&projectId=" + projectId;
        return param;
    }
}
