using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Approve.RuleCenter;

public partial class audit_XM_ipVideo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    public string XMMC, YWBM, CameraID, Caption, htmlSPSBinfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["XMMC"] != null && !string.IsNullOrEmpty(Request["XMMC"]))
            { XMMC = Request["XMMC"].ToString().Trim(); }
            if (Request["YWBM"] != null && !string.IsNullOrEmpty(Request["YWBM"]))
            { YWBM = Request["YWBM"].ToString().Trim(); }
            //if (Request["CameraID"] != null && !string.IsNullOrEmpty(Request["CameraID"]))
            //{ CameraID = Request["CameraID"].ToString().Trim(); }
            //if (Request["Caption"] != null && !string.IsNullOrEmpty(Request["Caption"]))
            //{ Caption = Request["Caption"].ToString().Trim(); }
            htmlSPSBinfo = "<table border='0'  cellspacing='0' cellpadding='0' style='width:100%;'>";

            htmlSPSBinfo += "<tr style='height:30px'>";
            htmlSPSBinfo += "<td>&nbsp;&nbsp;<img src='../Images/C1.png' />&nbsp;&nbsp;<a href='#' onclick=\"startvideo('27')\" >";
            htmlSPSBinfo += "后门</a></td>";
            htmlSPSBinfo += "<td>&nbsp;&nbsp;<img src='../Images/C1.png' />&nbsp;&nbsp;<a href='#' onclick=\"startvideo('18')\" >";
            htmlSPSBinfo += "生活区</a></td>";
            htmlSPSBinfo += "</tr>";

            htmlSPSBinfo += "<tr style='height:30px'>";
            htmlSPSBinfo += "<td>&nbsp;&nbsp;<img src='../Images/C1.png' />&nbsp;&nbsp;<a href='#' onclick=\"startvideo('82')\" >";
            htmlSPSBinfo += "大门</a></td>";
            htmlSPSBinfo += "<td>&nbsp;&nbsp;<img src='../Images/C1.png' />&nbsp;&nbsp;<a href='#' onclick=\"startvideo('78')\" >";
            htmlSPSBinfo += "二号塔吊</a></td>";
            htmlSPSBinfo += "</tr>";

            htmlSPSBinfo += "<tr style='height:30px'>";
            htmlSPSBinfo += "<td>&nbsp;&nbsp;<img src='../Images/C1.png' />&nbsp;&nbsp;<a href='#' onclick=\"startvideo('81')\" >";
            htmlSPSBinfo += "5号塔吊</a></td>";
            htmlSPSBinfo += "<td>&nbsp;&nbsp;<img src='../Images/C1.png' />&nbsp;&nbsp;<a href='#' onclick=\"startvideo('78')\" >";
            htmlSPSBinfo += "3号塔吊</a></td>";
            htmlSPSBinfo += "</tr>";

            htmlSPSBinfo += "</table>";
        }
    }
    //动态获取项目下有几个视频
    public string getVideo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select Caption,LX,LinkID,SBID from GC_SPSBinfo where 1=1 ");
        sb.Append(" and YWBM = '" + YWBM + "' ");
        DataTable dt = rc.GetTable(sb.ToString());
        int i, iCount;
        iCount = dt.Rows.Count;
        sb.Remove(0, sb.Length);
        if (iCount > 0)
        {
            for (i = 0; i < iCount; )
            {
                string Caption, LinkID, Captions, LinkIDs;
                Caption = dt.Rows[i]["Caption"].ToString();
                LinkID = dt.Rows[i]["LinkID"].ToString();
                if (i + 1 < iCount)
                {
                    Captions = dt.Rows[i + 1]["Caption"].ToString();
                    LinkIDs = dt.Rows[i + 1]["LinkID"].ToString();
                }
                else
                {
                    Captions = "";
                    LinkIDs = "";
                }
                sb.Append("<tr style='height:30px'><td>");
                sb.Append("&nbsp;&nbsp;<img src='images/C1.png' />&nbsp;&nbsp;<a href='#' onclick=\"startvideo('" + LinkID + "')\" >" + Caption + "</a>");
                sb.Append("</td>");
                if (Captions != "" && LinkIDs != "")
                {
                    sb.Append("<td>");
                    sb.Append("<img src='images/C1.png'/>&nbsp;&nbsp;<a href='#' onclick=\"startvideo('" + LinkIDs + "')\" >" + Captions + "</a>");
                    sb.Append("</td>");
                }
                else
                {
                    sb.Append("<td>");
                    sb.Append("&nbsp;");
                    sb.Append("</td>");
                }
                i += 2;
            }
            if (sb.Length > 0)
            {
                htmlSPSBinfo = "<table border='0'  cellspacing='0' cellpadding='0' style='width:100%;'>" + sb.ToString() + "</table>";
            }
            else htmlSPSBinfo = "";
        }
        else htmlSPSBinfo = "";
        return htmlSPSBinfo;
    }

}