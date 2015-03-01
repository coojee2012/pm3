using System;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Approve.PersistEnterprise;
using Approve.PersistBase;
using Approve.RuleBase;
using System.EnterpriseServices;
using Approve.EntityCenter;
using Approve.EntityBase;
 
using Approve.EntitySys;
namespace Approve.RuleCenter
{
    //RNews
    public class RNews : RBase
    {

        private PEnt m_pes;

        public RNews()
        {
            m_pes = null;
            this.pDBName = "dbCenter";
            BaseTools baseTools = new BaseTools();
            this.baseTools = baseTools;
        }

        private PEnt pes
        {
            get
            {
                if (m_pes == null)
                    m_pes = new PEnt();
                return m_pes;
            }
        }


        #region 新闻部分专用

        //跑马灯
        public string ShowMarNews(string FClassId, int top, string flag, string style, string width, string height, string pDetail)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select top " + top.ToString() + " n.FName,n.fid,n.FCreateTime,n.FFileNote,");
            sb.Append("n.FWebId from CF_News_Title n, CF_News_Col c where c.FNewsId = n.Fid and ");
            sb.Append("c.FColNumber ='" + FClassId);
            sb.Append("' and c.fisdeleted=0 and c.Fstate=1 order by c.forder,c.fpubtime desc");

            DataTable dt = this.GetTable(sb.ToString());
            sb.Remove(0, sb.Length);
            sb.Append("<marquee onmouseout='this.start()' onmouseover='this.stop()' scrollamount='2' scrolldelay='70' ");
            sb.Append("width='" + width + "' height='" + height + "'>");
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("\n<a href='" + pDetail + "?fid=" + dt.Rows[i]["FId"].ToString());
                    sb.Append("&type=" + FClassId + "' class='" + style + "' target=_blank title=" + dt.Rows[i]["FName"].ToString().Trim() + " >&nbsp;&nbsp;");

                    sb.Append(flag + dt.Rows[i]["FName"].ToString().Trim());
                    sb.Append("</a>");
                }
                return sb.ToString();
            }
            else
            {
                sb.Append("<center>暂无任何信息</center>");
            }
            sb.Append("</marquee>");
            return sb.ToString();
        }

        //图片
        public string ShowMarPic(string FClassId, int top, string acss, string pcss, string mwidth, string mheight, string twidth, string theight, string pwidth, string pheight, string pDetail)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select top " + top.ToString() + " n.FName,n.fid,n.FCreateTime,n.FFileNote,n.FPicUrl,");
            sb.Append("n.FWebId from CF_News_Title n, CF_News_Col c where c.FNewsId = n.Fid and ");
            sb.Append("c.FColNumber ='" + FClassId);
            sb.Append("' and c.fisdeleted=0 and c.Fstate=1 order by c.forder,c.fpubtime desc");

            DataTable dt = this.GetTable(sb.ToString());
            sb.Remove(0, sb.Length);
            sb.Append("<marquee onmouseout='this.start()' onmouseover='this.stop()' scrollamount='2' scrolldelay='70' ");
            sb.Append("width='" + mwidth + "' height='" + mheight + "'>");
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Append("<table  cellpadding=5 cellspacing=0 border=0>");
                sb.Append("<tr>");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("<td width='" + twidth + "' height='" + theight + "' valign=middle>");
                    sb.Append("<a href='" + pDetail + "?fid=" + dt.Rows[i]["FId"].ToString());
                    sb.Append("&type=" + FClassId + "' class='" + acss + "' target=_blank title=" + dt.Rows[i]["FName"].ToString().Trim() + " >");
                    sb.Append("<img src='" + dt.Rows[i]["FPicUrl"].ToString() + "' class='" + pcss + "' width='" + pwidth + "' height='" + pheight + "'>");
                    sb.Append("</a>");
                    sb.Append("</td>");
                }
                sb.Append("</td></tr></table>");
                return sb.ToString();
            }
            else
            {
                sb.Append("<center>暂无任何信息</center>");
            }
            sb.Append("</marquee>");
            return sb.ToString();
        }

        //提取新闻列表
        //colNumber:栏目编号；iTop：提取多少条；colCount：多少列；tablewidth：表格宽度；flag：标志；lheight：行高；sub：字符数量；isdate：是否显示日期；style：样式标；isline：是否显示下线；url：明细页面地址；fwidth：标识列宽度；nwidth：新闻列款对；dwidth：日期列宽度；
        public string ShowIndexNews(string colNumber, int iTop, int colCount, string tablewidth, string flag, int lheight, int sub, int isdate, string style, int isline, string url, string target, string fwidth, string nwidth, string dwidth)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select top " + iTop.ToString() + " n.fid,n.fname,n.FOperType,n.FTypeId,n.ffilenote,n.fwebid,n.FType,n.FPubTime,");
            sb.Append(" c.FColNumber,c.FState,n.FColor,c.FPic ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            if (colNumber.IndexOf('%') > 0)
            {
                sb.Append("c.FColNumber like '" + colNumber + "' ");
            }
            else
            {
                sb.Append(" c.FColNumber='" + colNumber + "'");
            }


            sb.Append(" and  n.FValidEnd>='" + DateTime.Now.ToString() + "' ");
            sb.Append(" and n.fstate=1 ");
            sb.Append(" and c.fisdeleted=0 and c.Fstate=1 order by c.forder,n.fpubtime desc");


            DataTable dt = this.GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Remove(0, sb.Length);

                sb.Append("<table cellSpacing='0' cellPadding='0' width='");
                if (tablewidth != "")
                    sb.Append(tablewidth);
                else
                    sb.Append("100%");
                sb.Append("'  align='center' border='0' >");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string fisdirect = dr["FOperType"].ToString();
                    string titlecolor = dr["FColor"].ToString();
                    string title = dr["fName"].ToString();


                    if (i % colCount == 0)
                    {
                        sb.Append("<tr height='" + lheight + "' ");
                        if (i != dt.Rows.Count - 1)
                        {
                            switch (isline)
                            {
                                case 1:
                                    sb.Append(" class=tdLine ");
                                    break;
                            }
                        }
                        sb.Append(" >");
                    }

                    if (flag != "")
                    {
                        sb.Append("<td align='center' style='padding-left:2px;padding-right:0px;'");
                        if (fwidth != "0")
                        {
                            sb.Append(" width='" + fwidth + "' ");
                        }
                        sb.Append(">");
                        if (flag.Length > 1 && flag.Substring(0, 1) != "<")
                        {
                            sb.Append("<img align=absmiddle src='" + flag + "' border=0 />&nbsp;");

                        }
                        else
                        {
                            sb.Append("<font class='" + style + "'>" + flag + "</font>");
                        }
                        sb.Append("</td>");
                    }

                    sb.Append(" <td   align='left' ");
                    if (nwidth != "0")
                    {
                        sb.Append(" width='" + nwidth + "' ");
                    }
                    sb.Append(">");


                    switch (fisdirect)
                    {
                        case "0":
                            sb.Append("<a href='" + url + "?fid=");
                            sb.Append(dt.Rows[i]["FId"].ToString() + "&fcol=" + dr["FColNumber"].ToString() + "' class='" + style + "' target='" + target + "' title='" + title + "' >");
                            break;
                        case "2":
                            sb.Append("<a href='" + dr["ffilenote"].ToString() + "' class='" + style + "' target='" + target + "' title='" + title + "' >");
                            break;
                        case "1":
                            sb.Append("<a href='" + dr["fwebid"].ToString() + "' class='" + style + "' target='" + target + "' title='" + title + "' >");
                            break;
                        default:
                            sb.Append("<a href='" + url + "?fid=");
                            sb.Append(dt.Rows[i]["FId"].ToString() + "&fcol=" + dr["FColNumber"].ToString() + "' class='" + style + "' target='" + target + "' title='" + title + "' >");
                            break;

                    }

                    string opetype = dr["FType"].ToString();
                    int subb = sub;
                    subb -= 2;
                    string title1 = "";
                    switch (opetype)
                    {
                        case "1":
                            title1 = "<img border=0 src=../../webSiteNews/images/new.gif />";
                            break;
                        case "2":
                            title1 = "<img border=0 src=../../webSiteNews/images/hot.gif />";
                            break;
                        default:
                            subb += 2;
                            break;
                    }

                    //if (title.Length > subb)
                    //{
                    //    title = title.Substring(0, subb) + "..";
                    //}
                    title = this.getSubStr(title, sub);

                    title += title1;

                    //if (type == 1)
                    //{
                    //    if (title.Length > 15)
                    //        title = title.Substring(0, 15) + "..";
                    //    title = "<center><span class=BigTitle>" + title + "</span></center>";
                    //}
                    if (titlecolor != "")
                    {
                        title = "<font color='" + titlecolor + "' >" + title + "</font>";
                    }

                    sb.Append(title);
                    sb.Append("</a>");

                    sb.Append("</td>");
                    string st = "";
                    switch (isdate)
                    {
                        case 1:
                            st = Convert.ToDateTime(dt.Rows[i]["FPubTime"].ToString()).ToString("MM-dd");
                            if (dwidth != "0")
                            {
                                sb.Append("<td width='" + dwidth + "' class='" + style + "'>" + st + "</td>");
                            }
                            else
                            {
                                sb.Append("<td width=30  class='" + style + "'>" + st + "</td>");
                            }
                            break;
                        case 2:
                            st = Convert.ToDateTime(dt.Rows[i]["FPubTime"].ToString()).ToString("yyy-MM-dd");
                            if (dwidth != "0")
                            {
                                sb.Append("<td width='" + dwidth + "' class='" + style + "'>[" + st + "]</td>");
                            }
                            else
                            {
                                sb.Append("<td width=80 class=text0>[" + st + "]</td>");
                            }
                            break;
                    }

                    if ((i + 1) % colCount == 0)
                    {
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");
            }
            else
            {
                sb.Remove(0, sb.Length);
                sb.Append("<center>暂无数据</center>");
            }
            return sb.ToString();

        }


        public string ShowIndexNewsByCount(string colNumber, int iTop, int colCount, string tablewidth, string flag, int lheight, int sub, int isdate, string style, int isline, string url, string fwidth, string nwidth, string dwidth)
        {
            StringBuilder sb = new StringBuilder();


            sb.Append("select top " + iTop.ToString() + " n.fid,n.fname,n.FOperType,n.FTypeId,n.ffilenote,n.fwebid,n.FType,n.FPubTime,");
            sb.Append(" c.FColNumber,c.FState,c.FColor,c.FPic ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            if (colNumber.IndexOf('%') > 0)
            {
                sb.Append("c.FColNumber like '" + colNumber + "' ");
            }
            else
            {
                sb.Append(" c.FColNumber='" + colNumber + "'");
            }

            sb.Append(" and c.fisdeleted=0 and c.fstate=1 ");
            sb.Append(" order by n.fcount desc,c.fpubtime desc");

            DataTable dt = this.GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Remove(0, sb.Length);

                sb.Append("<table cellSpacing='0' cellPadding='0' width='");
                if (tablewidth != "")
                    sb.Append(tablewidth);
                else
                    sb.Append("100%");
                sb.Append("'  align='center' border='0' >");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string fisdirect = dr["FOperType"].ToString();
                    string titlecolor = dr["FColor"].ToString();
                    string title = dr["fName"].ToString();
                    string FTypeId = dr["FTypeId"].ToString();

                    if (i % colCount == 0)
                    {
                        sb.Append("<tr height='" + lheight + "' ");
                        switch (isline)
                        {
                            case 1:
                                sb.Append(" class=dianline ");
                                break;

                        }
                        sb.Append(" >");
                    }

                    if (flag != "")
                    {
                        sb.Append("<td align='center' style='padding-left:2px;padding-right:5px;'");
                        if (fwidth != "0")
                        {
                            sb.Append(" width='" + fwidth + "' ");
                        }
                        sb.Append(">");
                        if (flag.Length > 1 && flag.Substring(0, 1) != "<")
                        {

                            sb.Append("<img align=baseline src='" + flag + Convert.ToString(i + 1) + ".gif' border=0 />&nbsp;");

                        }
                        else
                        {
                            sb.Append("<font class=text0 >" + flag + "</font>");
                        }
                        sb.Append("</td>");
                    }

                    sb.Append(" <td   align='left' ");
                    if (nwidth != "0")
                    {
                        sb.Append(" width='" + nwidth + "' ");
                    }
                    sb.Append(">");
                    if (Convert.ToInt32(FTypeId) < 3)
                    {

                        switch (fisdirect)
                        {
                            case "1":
                                sb.Append("<a href='" + url + "?fid=");
                                sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + dr["FColNumber"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            case "2":
                                sb.Append("<a href='" + dr["ffilenote"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            case "3":
                                sb.Append("<a href='" + dr["fwebid"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            default:
                                sb.Append("<a href='" + url + "?fid=");
                                sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + dr["FColNumber"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                        }
                    }
                    else
                    {
                        sb.Append("<a class=" + style + " target=_blank href=../../webSiteDwon/main/ShowDetailInfo.aspx?fid=");
                        sb.Append(dr["FId"].ToString());
                        sb.Append("&type=" + dr["FColNumber"].ToString());
                        sb.Append(" >");
                    }
                    string opetype = dr["FType"].ToString();
                    int subb = sub;
                    subb -= 2;
                    string title1 = "";
                    switch (opetype)
                    {
                        case "1":
                            title1 = "<img border=0 src=../../webSiteNews/images/new.gif />";
                            break;
                        case "2":
                            title1 = "<img border=0 src=../../webSiteNews/images/hot.gif />";
                            break;
                        default:
                            subb += 2;
                            break;
                    }


                    title = this.getSubStr(title, sub);

                    title += title1;


                    if (titlecolor != "")
                    {
                        title = "<font color='" + titlecolor + "' >" + title + "</font>";
                    }

                    sb.Append(title);
                    sb.Append("</a>");

                    sb.Append("</td>");
                    string st = "";
                    switch (isdate)
                    {
                        case 1:
                            st = Convert.ToDateTime(dt.Rows[i]["FPubTime"].ToString()).ToString("MM-dd");
                            if (dwidth != "0")
                            {
                                sb.Append("<td width='" + dwidth + "' class='" + style + "'>" + st + "</td>");
                            }
                            else
                            {
                                sb.Append("<td width=30  class='" + style + "'>" + st + "</td>");
                            }
                            break;
                        case 2:
                            st = Convert.ToDateTime(dt.Rows[i]["FPubTime"].ToString()).ToString("yyy-MM-dd");
                            if (dwidth != "0")
                            {
                                sb.Append("<td width='" + dwidth + "' class='" + style + "'>[" + st + "]</td>");
                            }
                            else
                            {
                                sb.Append("<td width=80 class=text0>[" + st + "]</td>");
                            }
                            break;
                    }

                    if ((i + 1) % colCount == 0)
                    {
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");
            }
            else
            {
                sb.Remove(0, sb.Length);
                sb.Append("<center>暂无数据</center>");
            }
            return sb.ToString();

        }


        //新加
        //colNumber:栏目编号；iTop：提取多少条；colCount：多少列；tablewidth：表格宽度；flag：标志；lheight：行高；sub：字符数量；isdate：是否显示日期；style：样式标；isline：是否显示下线；url：明细页面地址；fwidth：标识列宽度；nwidth：新闻列款对；dwidth：日期列宽度；
        public string ShowIndexNews(string colNumber, int iTop, int iTop1, int colCount, string tablewidth, string flag, int lheight, int sub, int isdate, string style, int isline, string url, string fwidth, string nwidth, string dwidth)
        {
            StringBuilder sb = new StringBuilder();
            //flag显示的为行前的标志,例*或·  sub为取的标题长度  isdate为是否显示日期1为显示,0为不显示
            //string fclassid = "";
            //if (colNumber.IndexOf('%') > 0)
            //    fclassid = colNumber.Substring(0, colNumber.Length - 1);
            //else
            //    fclassid = colNumber;

            //sb.Remove(0, sb.Length);

            sb.Append("select top " + iTop.ToString() + " n.fid,n.fname,n.FOperType,n.FTypeId,n.ffilenote,n.fwebid,n.FType,n.FPubTime,");
            sb.Append(" c.FColNumber,c.FState,c.FColor,c.FPic ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            if (colNumber.IndexOf('%') > 0)
            {
                sb.Append("c.FColNumber like '" + colNumber + "' ");
            }
            else
            {
                sb.Append(" c.FColNumber='" + colNumber + "'");
            }


            sb.Append(" and n.fid not in(");

            sb.Append("select top " + iTop1.ToString() + " n.fid ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            if (colNumber.IndexOf('%') > 0)
            {
                sb.Append("c.FColNumber like '" + colNumber + "' ");
            }
            else
            {
                sb.Append(" c.FColNumber='" + colNumber + "'");
            }
            sb.Append(" and c.fisdeleted=0 and c.fstate=1 ");
            sb.Append(" order by c.forder,c.fpubtime desc");
            sb.Append(") ");

            //string fid = getLMFid(fclassid1);
            //sb.Append("(c.FColNumber='" + fclassid1 + "'");
            //sb.Append(" or c.FColNumber in (select t.FNumber from CF_Sys_Tree t where t.FParentId='" + fid + "'))");
            sb.Append(" and c.fisdeleted=0 and c.fstate=1 ");
            sb.Append(" order by c.forder,c.fpubtime desc");

            DataTable dt = this.GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Remove(0, sb.Length);

                sb.Append("<table cellSpacing='0' cellPadding='0' width='");
                if (tablewidth != "")
                    sb.Append(tablewidth);
                else
                    sb.Append("100%");
                sb.Append("'  align='center' border='0' >");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string fisdirect = dr["FOperType"].ToString();
                    string titlecolor = dr["FColor"].ToString();
                    string title = dr["fName"].ToString();
                    string FTypeId = dr["FTypeId"].ToString();

                    if (i % colCount == 0)
                    {
                        sb.Append("<tr height='" + lheight + "' ");
                        switch (isline)
                        {
                            case 1:
                                sb.Append(" class=dianline ");
                                break;

                        }
                        sb.Append(" >");
                    }

                    if (flag != "")
                    {
                        sb.Append("<td align='center' style='padding-left:2px;padding-right:5px;'");
                        if (fwidth != "0")
                        {
                            sb.Append(" width='" + fwidth + "' ");
                        }
                        sb.Append(">");
                        if (flag.Length > 1 && flag.Substring(0, 1) != "<")
                        {
                            sb.Append("<img align=absmiddle src='" + flag + "' border=0 />&nbsp;");

                        }
                        else
                        {
                            sb.Append("<font class=text0 >" + flag + "</font>");
                        }
                        sb.Append("</td>");
                    }

                    sb.Append(" <td   align='left' ");
                    if (nwidth != "0")
                    {
                        sb.Append(" width='" + nwidth + "' ");
                    }
                    sb.Append(">");
                    if (Convert.ToInt32(FTypeId) < 3)
                    {

                        switch (fisdirect)
                        {
                            case "1":
                                sb.Append("<a href='" + url + "?fid=");
                                sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + dr["FColNumber"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            case "2":
                                sb.Append("<a href='" + dr["ffilenote"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            case "3":
                                sb.Append("<a href='" + dr["fwebid"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            default:
                                sb.Append("<a href='" + url + "?fid=");
                                sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + dr["FColNumber"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                        }
                    }
                    else
                    {
                        sb.Append("<a class=" + style + " target=_blank href=../../webSiteDwon/main/ShowDetailInfo.aspx?fid=");
                        sb.Append(dr["FId"].ToString());
                        sb.Append("&type=" + dr["FColNumber"].ToString());
                        sb.Append(" >");
                    }
                    string opetype = dr["FType"].ToString();
                    int subb = sub;
                    subb -= 2;
                    string title1 = "";
                    switch (opetype)
                    {
                        case "1":
                            title1 = "<img border=0 src=../../webSiteNews/images/new.gif />";
                            break;
                        case "2":
                            title1 = "<img border=0 src=../../webSiteNews/images/hot.gif />";
                            break;
                        default:
                            subb += 2;
                            break;
                    }

                    //if (title.Length > subb)
                    //{
                    //    title = title.Substring(0, subb) + "..";
                    //}
                    title = this.getSubStr(title, sub);

                    title += title1;

                    //if (type == 1)
                    //{
                    //    if (title.Length > 15)
                    //        title = title.Substring(0, 15) + "..";
                    //    title = "<center><span class=BigTitle>" + title + "</span></center>";
                    //}
                    if (titlecolor != "")
                    {
                        title = "<font color='" + titlecolor + "' >" + title + "</font>";
                    }

                    sb.Append(title);
                    sb.Append("</a>");

                    sb.Append("</td>");
                    string st = "";
                    switch (isdate)
                    {
                        case 1:
                            st = Convert.ToDateTime(dt.Rows[i]["FPubTime"].ToString()).ToString("MM-dd");
                            if (dwidth != "0")
                            {
                                sb.Append("<td width='" + dwidth + "' class='" + style + "'>" + st + "</td>");
                            }
                            else
                            {
                                sb.Append("<td width=30  class='" + style + "'>" + st + "</td>");
                            }
                            break;
                        case 2:
                            st = Convert.ToDateTime(dt.Rows[i]["FPubTime"].ToString()).ToString("yyy-MM-dd");
                            if (dwidth != "0")
                            {
                                sb.Append("<td width='" + dwidth + "' class='" + style + "'>[" + st + "]</td>");
                            }
                            else
                            {
                                sb.Append("<td width=80 class=text0>[" + st + "]</td>");
                            }
                            break;
                    }

                    if ((i + 1) % colCount == 0)
                    {
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");
            }
            else
            {
                sb.Remove(0, sb.Length);
                sb.Append("<center>暂无数据</center>");
            }
            return sb.ToString();

        }


        public string ShowIndexNews(DataTable dtt, int col, string tablewidth, string flag, int height, int sub, int isdate, int top, string style, int type, int isline, string openurl)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = dtt;
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Remove(0, sb.Length);

                if (dt.Rows.Count > top)
                {
                    int dtCount = dt.Rows.Count - top;
                    for (int j = 0; j < dtCount; j++)
                    {
                        dt.Rows.RemoveAt(dt.Rows.Count - 1);
                    }
                }

                sb.Append("<table cellSpacing='0' cellPadding='0' width='");
                if (tablewidth != "")
                    sb.Append(tablewidth);
                else
                    sb.Append("100%");
                sb.Append("'  align='center' border='0' >");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string fisdirect = dr["FOperType"].ToString();
                    string titlecolor = dr["FColor"].ToString();
                    string title = dr["fName"].ToString();
                    string FTypeId = dr["FTypeId"].ToString();

                    if (i % col == 0)
                    {
                        sb.Append("<tr valign='bottom'");
                        switch (isline)
                        {
                            case 1:
                                sb.Append(" class=dianline ");
                                break;

                        }
                        sb.Append(" >");
                    }
                    sb.Append("<td align='center' style='padding-left:2px;padding-right:0px;' >");
                    if (flag.Length > 1 && flag.Substring(0, 1) != "<")
                    {
                        sb.Append("<img align=absmiddle src='" + flag + "' border=0 />&nbsp;");

                    }
                    else
                    {
                        sb.Append("<font class=text0 >" + flag + "</font>");
                    }
                    sb.Append("</td><td  height=" + height + " align='left' >");
                    if (Convert.ToInt32(FTypeId) < 3)
                    {

                        switch (fisdirect)
                        {
                            case "1":
                                sb.Append("<a href='" + openurl + "?fid=");
                                sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + dr["FColNumber"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            case "2":
                                sb.Append("<a href='" + dr["ffilenote"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            case "3":
                                sb.Append("<a href='" + dr["fwebid"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            default:
                                sb.Append("<a href='" + openurl + "?fid=");
                                sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + dr["FColNumber"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                        }
                    }
                    else
                    {
                        sb.Append("<a class=" + style + " target=_blank href=../../webSiteDwon/main/ShowDetailInfo.aspx?fid=");
                        sb.Append(dr["FId"].ToString());
                        sb.Append("&type=" + dr["FColNumber"].ToString());
                        sb.Append(" >");
                    }
                    string opetype = dr["FType"].ToString();
                    int subb = sub;
                    subb -= 2;
                    string title1 = "";
                    switch (opetype)
                    {
                        case "1":
                            title1 = "<img border=0 src=../../webSiteNews/images/new.gif />";
                            break;
                        case "2":
                            title1 = "<img border=0 src=../../webSiteNews/images/hot.gif />";
                            break;
                        default:
                            subb += 2;
                            break;
                    }

                    if (title.Length > subb)
                    {
                        title = title.Substring(0, subb) + "..";
                    }
                    title += title1;

                    if (type == 1)
                    {
                        if (title.Length > 15)
                            title = title.Substring(0, 15) + "..";
                        title = "<center><span class=BigTitle>" + title + "</span></center>";
                        type = 0;
                    }
                    if (titlecolor != "")
                    {
                        title = "<font color='" + titlecolor + "' >" + title + "</font>";
                    }

                    sb.Append(title);
                    sb.Append("</a>");

                    sb.Append("</td>");


                    string st = "";


                    if (isdate == 1)
                    {

                        try
                        {
                            st = Convert.ToDateTime(dt.Rows[i]["fpubtime"].ToString()).ToString("MM-dd");
                            sb.Append("<td width=30 valign='middle' class=text0>" + st + "</td>");

                        }
                        catch (Exception ex)
                        {
                            sb.Append("<td width=30 valign='middle' class=text0></td>");

                        }
                    }

                    if (isdate == 2)
                    {

                        try
                        {
                            st = Convert.ToDateTime(dt.Rows[i]["fpubtime"].ToString()).ToString("yyyy-MM-dd");
                            sb.Append("<td width=80  class=text0>[" + st + "]</td>");

                        }
                        catch (Exception ex)
                        {
                            sb.Append("<td width=80  class=text0></td>");

                        }
                    }


                    if ((i + 1) % col == 0)
                    {
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");
            }
            else
            {
                sb.Remove(0, sb.Length);
                sb.Append("<center>暂无数据</center>");
            }
            return sb.ToString();

        }



        //修改关，首页政务和商务区别
        public string ShowIndexNewsKind(string colNumber, int iTop, int col, string tablewidth, string flag, int lheight, int sub, int isdate, string style, int type, int isline, string openUrl)
        {
            StringBuilder sb = new StringBuilder();
            //flag显示的为行前的标志,例*或·  sub为取的标题长度  isdate为是否显示日期1为显示,0为不显示

            sb.Append("select top " + iTop.ToString() + " n.fid,n.fname,n.FOperType,n.FTypeId,n.ffilenote,n.fwebid,n.FType,n.FPubTime,n.FClass,");
            sb.Append(" c.FColNumber,c.FState,c.FColor,c.FPic ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            if (colNumber.IndexOf('%') > 0)
            {
                sb.Append("c.FColNumber like '" + colNumber + "' ");
            }
            else
            {
                sb.Append(" c.FColNumber='" + colNumber + "'");
            }
            //string fid = getLMFid(fclassid1);
            //sb.Append("(c.FColNumber='" + fclassid1 + "'");
            //sb.Append(" or c.FColNumber in (select t.FNumber from CF_Sys_Tree t where t.FParentId='" + fid + "'))");
            sb.Append(" and c.fisdeleted=0 and c.fstate=1 ");
            sb.Append(" order by c.forder,c.fpubtime desc");

            DataTable dt = this.GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Remove(0, sb.Length);

                sb.Append("<table cellSpacing='0' cellPadding='0' width='");
                if (tablewidth != "0")
                {
                    sb.Append(tablewidth);
                }
                else
                {
                    sb.Append("100%");
                }
                sb.Append("'  align='center' border='0' >");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string fisdirect = dr["FOperType"].ToString();
                    string titlecolor = dr["FColor"].ToString();
                    string title = dr["fName"].ToString();
                    string fTypeId = dr["FTypeId"].ToString();
                    string fid = dr["FID"].ToString();
                    string fClass = dr["FClass"].ToString();
                    string fcol = dr["FColNumber"].ToString();
                    string fpubtime = dr["FPubTime"].ToString();

                    if (i % col == 0)
                    {
                        sb.Append("<tr valign='bottom'");
                        switch (isline)
                        {
                            case 1:
                                sb.Append(" class=dianline ");
                                break;

                        }
                        sb.Append(" >");
                    }
                    sb.Append("<td align='center' style='padding-left:2px;padding-right:5px;' >");


                    if (fClass == "政务")
                    {
                        sb.Append("<font color='#FB3800'>政务|</font>");
                    }
                    else
                    {
                        sb.Append("<font color='#98AE27'>" + fClass + "|</font>");
                    }

                    sb.Append("</td><td  height=" + lheight + " align='left' >");
                    if (Convert.ToInt32(fTypeId) < 3)
                    {

                        switch (fisdirect)
                        {
                            case "1":
                                sb.Append("<a href='" + openUrl + "?fid=");
                                sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fcol + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            case "2":
                                sb.Append("<a href='" + dr["ffilenote"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            case "3":
                                sb.Append("<a href='" + dr["fwebid"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            default:
                                sb.Append("<a href='" + openUrl + "?fid=");
                                sb.Append(fid + "&type=" + fcol + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                        }
                    }
                    else
                    {
                        sb.Append("<a class=" + style + " target=_blank href=" + openUrl + "?fid=");
                        sb.Append(dr["FId"].ToString());
                        sb.Append("&type=" + fcol);
                        sb.Append(" >");
                    }
                    string opetype = dr["FType"].ToString();
                    int subb = sub;
                    subb -= 2;
                    string title1 = "";
                    switch (opetype)
                    {
                        case "1":
                            title1 = "<img border=0 src=../../webSiteNews/images/new.gif />";
                            break;
                        case "2":
                            title1 = "<img border=0 src=../../webSiteNews/images/hot.gif />";
                            break;
                        default:
                            subb += 2;
                            break;
                    }

                    title = this.getSubStr(title, subb);

                    title += title1;

                    if (type == 1)
                    {
                        if (title.Length > 15)
                            title = title.Substring(0, 15) + "..";
                        title = "<center><span class=BigTitle>" + title + "</span></center>";
                        type = 0;
                    }
                    if (titlecolor != "")
                    {
                        title = "<font color='" + titlecolor + "' >" + title + "</font>";
                    }

                    sb.Append(title);
                    sb.Append("</a>");

                    sb.Append("</td>");
                    if (isdate == 1)
                    {

                        try
                        {
                            string st = Convert.ToDateTime(fpubtime).ToString("MM-dd");
                            sb.Append("<td width=30 valign='top' class=text0>" + st + "</td>");

                        }
                        catch (Exception ex)
                        {
                            sb.Append("<td width=30 valign='top' class=text0></td>");

                        }
                    }
                    if ((i + 1) % col == 0)
                    {
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");
            }
            else
            {
                sb.Remove(0, sb.Length);
                sb.Append("<center>暂无数据</center>");
            }
            return sb.ToString();
        }



        // 首页显示的时政要闻
        public string ShowIndexYWNews(string colNumber, string pcolNumber, int iTop, int pTop, int col, string tablewidth, string flag, int lheight, int sub, int isdate, string style, int type, int isline, string openUrl, string showpic, string picCss, string picwidth, string picheight, string ptableHeight)
        {
            StringBuilder sb = new StringBuilder();
            //flag显示的为行前的标志,例*或·  sub为取的标题长度  isdate为是否显示日期1为显示,0为不显示


            sb.Append("select top " + iTop.ToString() + " n.fid,n.fname,n.FOperType,n.FTypeId,n.ffilenote,n.fwebid,n.FType,n.FPubTime,n.FClass,n.FPicUrl,");
            sb.Append(" c.FColNumber,c.FState,c.FColor,c.FPic ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            if (colNumber.IndexOf('%') > 0)
            {
                sb.Append("c.FColNumber like '" + colNumber + "' ");
            }
            else
            {
                sb.Append(" c.FColNumber='" + colNumber + "'");
            }

            sb.Append(" and c.fisdeleted=0 and c.fstate=1 ");
            sb.Append(" order by c.forder,c.fpubtime desc");

            DataTable dt = this.GetTable(sb.ToString());


            sb.Remove(0, sb.Length);

            sb.Append("select top " + pTop.ToString() + " n.fid,n.fname,n.FOperType,n.FTypeId,n.ffilenote,n.fwebid,n.FType,n.FPubTime,n.FClass,n.FPicUrl,");
            sb.Append(" c.FColNumber,c.FState,c.FColor,c.FPic ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            if (pcolNumber.IndexOf('%') > 0)
            {
                sb.Append("c.FColNumber like '" + pcolNumber + "' ");
            }
            else
            {
                sb.Append(" c.FColNumber='" + pcolNumber + "'");
            }

            sb.Append(" and c.fisdeleted=0 and c.fstate=1 ");
            sb.Append(" order by c.forder,n.fpubtime desc");


            DataTable pdt = this.GetTable(sb.ToString());



            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Remove(0, sb.Length);

                if (showpic == "showpic" && pdt != null && pdt.Rows.Count > 0)
                {


                    sb.Append("<table cellSpacing='0' cellPadding='0' width='");
                    if (tablewidth != "0")
                    {
                        sb.Append(tablewidth);
                    }
                    else
                    {
                        sb.Append("100%");
                    }


                    sb.Append("' align='center' border='0' ");
                    if (ptableHeight != "")
                    {
                        sb.Append("height='" + ptableHeight + "'px ");
                    }
                    sb.Append(">");

                    sb.Append("<tr>");

                    for (int i = 0; i < pdt.Rows.Count; i++)
                    {
                        DataRow dr = pdt.Rows[i];
                        string strurl = dr["FPicUrl"].ToString();
                        string fcol = dr["FColNumber"].ToString();
                        string title = dr["fName"].ToString();

                        sb.Append("<td style= width:139px; height:104px'>");
                        sb.Append("<a href='" + openUrl + "?fid=");
                        sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fcol + "' class='" + style + "' target=_blank title='" + title + "' >");
                        sb.Append("<img border='0' width='" + picwidth + "px' height='" + picheight + "px' style='margin-top:6px'  src='");
                        sb.Append(strurl);
                        sb.Append("' ");
                        if (picCss != "")
                        {
                            sb.Append(" class='" + picCss + "' ");
                        }
                        sb.Append("/>");
                        sb.Append("</a>");
                        sb.Append("</td>");

                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }


                sb.Append("<table cellSpacing='0' cellPadding='0' width='");
                if (tablewidth != "0")
                {
                    sb.Append(tablewidth);
                }
                else
                {
                    sb.Append("100%");
                }
                sb.Append("' align='center' border='0' >");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string fisdirect = dr["FOperType"].ToString();
                    string titlecolor = dr["FColor"].ToString();
                    string title = dr["fName"].ToString();
                    string fTypeId = dr["FTypeId"].ToString();
                    string fid = dr["FID"].ToString();
                    string fClass = dr["FClass"].ToString();
                    string fcol = dr["FColNumber"].ToString();
                    string fpubtime = dr["FPubTime"].ToString();

                    if (i % col == 0)
                    {
                        sb.Append("<tr valign='bottom'");
                        switch (isline)
                        {
                            case 1:
                                sb.Append(" class='dianline'");
                                break;
                        }
                        sb.Append(" >");
                    }
                    sb.Append("<td align='left' height='" + lheight + "' >");

                    if (Convert.ToInt32(fTypeId) < 3)
                    {

                        switch (fisdirect)
                        {
                            case "0":
                                sb.Append("<a href='" + openUrl + "?fid=");
                                sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fcol + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            case "2":
                                sb.Append("<a href='" + dr["ffilenote"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            case "3":
                                sb.Append("<a href='" + dr["fwebid"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            default:
                                sb.Append("<a href='" + openUrl + "?fid=");
                                sb.Append(fid + "&type=" + fcol + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                        }
                    }
                    else
                    {
                        sb.Append("<a class=" + style + " target=_blank href=" + openUrl + "?fid=");
                        sb.Append(dr["FId"].ToString());
                        sb.Append("&type=" + fcol);
                        sb.Append(" >");
                    }
                    string opetype = dr["FType"].ToString();
                    int subb = sub;
                    subb -= 2;
                    string title1 = "";
                    switch (opetype)
                    {
                        case "1":
                            title1 = "<img border=0 src=../../webSiteNews/images/new.gif />";
                            break;
                        case "2":
                            title1 = "<img border=0 src=../../webSiteNews/images/hot.gif />";
                            break;
                        default:
                            subb += 2;
                            break;
                    }

                    title = this.getSubStr(title, subb);

                    title += title1;

                    if (type == 1)
                    {
                        if (title.Length > 15)
                            title = title.Substring(0, 15) + "..";
                        title = "<center><span class=BigTitle>" + title + "</span></center>";
                        type = 0;
                    }
                    if (titlecolor != "")
                    {
                        title = "<font color='" + titlecolor + "' >" + title + "</font>";
                    }
                    sb.Append("<font color=red>·</font>");
                    sb.Append(title);
                    sb.Append("</a>");

                    sb.Append("</td>");

                    if ((i + 1) % col == 0)
                    {
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");

            }
            else
            {
                sb.Remove(0, sb.Length);
                sb.Append("<center>暂无数据</center>");
            }
            return sb.ToString();

        }



        //评论
        public bool setPl(string fid, string name, string content)
        {
            SortedList sl = new SortedList();
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FNewsId", fid);
            sl.Add("FUserName", name);
            sl.Add("FContent", content);
            sl.Add("FPubTime", DateTime.Now.ToString());
            sl.Add("FCreateTime", DateTime.Now.ToString());
            sl.Add("FOrder", 5000);
            sl.Add("FState", 1);
            sl.Add("FIsDeleted", 0);
            if (this.SaveEBase(EntityTypeEnum.EnComment, sl, "FID", SaveOptionEnum.Insert))
                return true;
            else
                return false;

        }





        public string ShowLinkPic(string fcol, string tablewidth, string piccss, int picwidth, int picheight, int padleft, int top, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Remove(0, sb.Length);
            sb.Append("select top " + top + " n.*,c.Forder,c.FColor,n.FOperType,n.FTypeId,c.FPic,c.FColNumber ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            if (fcol.IndexOf("%") >= 0)
            {
                sb.Append(" c.FColNumber like '");
                sb.Append(fcol);
                sb.Append("'");
            }
            else
            {
                sb.Append(" c.FColNumber='");
                sb.Append(fcol);
                sb.Append("'");
            }
            sb.Append(" and n.fisdeleted=0 and c.fstate=1 ");
            sb.Append(" order by c.forder,c.fpubtime desc");



            DataTable dt = new DataTable();
            dt = this.GetTable(sb.ToString());
            sb.Remove(0, sb.Length);
            sb.Append("<table style='margin-top:5px;' cellpadding=0 cellspacing=0 ");
            if (tablewidth != "")
            {
                sb.Append("width=" + tablewidth);

            }
            sb.Append(">");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataRow dr = dt.Rows[i];
                    string FTypeId = dr["FTypeId"].ToString();
                    string fisdirect = dr["FOperType"].ToString();


                    sb.Append("<td style='padding-left:" + padleft.ToString() + "px;text-align:center'>");



                    if (Convert.ToInt32(FTypeId) < 3)
                    {
                        switch (fisdirect)
                        {
                            case "0":
                                sb.Append("<a href='" + url + "?fid=");
                                sb.Append(dr["FId"].ToString() + "&type=" + fcol + "' target=_blank >");
                                break;
                            case "1":
                                sb.Append("<a href='" + dr["ffilenote"].ToString() + "'  target=_blank >");
                                break;
                            case "2":
                                sb.Append("<a href='" + dr["FWebId"].ToString() + "' target=_blank>");
                                break;
                            default:
                                sb.Append("<a href='" + url + "?fid=");
                                sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fcol + "'  target=_blank >");
                                break;
                        }
                    }
                    else
                    {
                        sb.Append("<a  target=_blank href=ShowDetailInfo.aspx?fid=");
                        sb.Append(dr["FId"].ToString());
                        sb.Append("&type=" + dr["FColNumber"].ToString());
                        sb.Append(" >");
                    }



                    if (dr["FPicUrl"].ToString().Length > 6)
                    {

                        sb.Append("<img width=" + picwidth + "px height=" + picheight + "px border=0 src=" + dr["FPicUrl"].ToString() + " ");
                    }
                    else
                    {
                        sb.Append("<img width=" + picwidth + "px height=" + picheight + "px border=0 src='../images/Nopic.gif'" + " ");
                    }
                    if (piccss != "")
                    {
                        sb.Append(" class='" + piccss + "'");
                    }
                    sb.Append("/>");
                    sb.Append("</a>");

                    sb.Append("</td>");
                }
            }
            sb.Append("</tr></table>");

            return sb.ToString();

        }




        public string ShowPicLink(string fcol, string tablewidth, string tableheight, int border, string tablecss, int picwidth, int picheight, string divcss, int sub, string css, int top, string piccss, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select top " + top.ToString() + " n.fid,n.FPicUrl,n.fname,n.FWebID,");
            sb.Append("c.FColNumber,c.FState,c.FPic,c.Fisdeleted,c.Forder from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");

            if (fcol.IndexOf("%") != -1)
            {
                sb.Append("  c.fcolnumber like '");
                sb.Append(fcol + "");
            }
            else
            {
                sb.Append(" c.FColNumber='");
                sb.Append(fcol);
            }
            sb.Append("' and c.fisdeleted=0 and c.fstate=1");
            sb.Append(" order by c.forder,c.fpubtime desc");
            DataTable dt = dt = this.GetTable(sb.ToString());
            if (dt.Rows.Count == 0)
                return "<center>暂无数据</center>";
            sb.Remove(0, sb.Length);
            sb.Append("<table width=" + tablewidth + " height=" + tableheight + " class=" + tablecss + " align=center cellpadding=0 cellspacing=0 border=" + border + " >");
            sb.Append("<tr>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                sb.Append("<td align=center >");
                string fwebid = dr["FWebID"].ToString();
                string fid = dr["fid"].ToString();
                string fcolnumber = dr["fcolnumber"].ToString();
                string furl = "<a target=_blank class=text01 href='" + url + "?fid=" + fid + "&type=" + fcolnumber + "'>";
                if (url != "")
                {

                }
                else
                {
                    furl = "<a target=_blank class=text01 href='";

                    if (fwebid == "")
                        fwebid = "//";
                    furl += fwebid + "' >";
                }
                sb.Append(furl);
                if (dr["FPicUrl"].ToString().Length > 5)
                {
                    sb.Append("<img border=0  height=" + picheight + " width=" + picwidth + " src=" + dr["FPicUrl"].ToString() + " ");
                }
                else
                {
                    sb.Append("<img border=0  height=" + picheight + " width=" + picwidth + " src='../images/Nopic.gif'" + " ");
                }
                if (piccss != "")
                {
                    sb.Append(" class='" + piccss + "' ");
                }
                sb.Append("/>");
                sb.Append("</a>");



                sb.Append("<div class=" + divcss + " >");
                sb.Append(furl);
                string fname = dr["FName"].ToString();
                fname = this.getSubStr(fname, sub);
                sb.Append(fname);
                sb.Append("</a>");
                sb.Append("</div>");
                sb.Append("</td>");
            }
            sb.Append("</tr>");
            sb.Append("</table>");

            return sb.ToString();
        }




        public string ShowFlashLink(string fcol, int top1, int top2)
        {
            StringBuilder sb = new StringBuilder();
            string orderstr = " order by c.forder,n.fpubtime desc ";
            sb.Append("select  n.fid");
            sb.Append(" from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            sb.Append("c.FColNumber='");
            sb.Append(fcol);
            sb.Append("' and c.fisdeleted=0 and c.fstate=1 ");

            sb.Append(orderstr);

            DataTable dt = this.GetPageTable(sb.ToString(), top1, top2);
            sb.Remove(0, sb.Length);
            if (dt == null || dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return this.ShowData(dt.Rows[0]["FID"].ToString());

            }

        }

        /// <summary>
        /// 根据类型查询广告
        /// </summary>
        /// <param name="fcol"></param>
        /// <param name="top1"></param>
        /// <param name="top2"></param>
        /// <returns></returns>
        public DataTable ShowFlashLinkUrl(string fcol, int top1, int top2)
        {
            StringBuilder sb = new StringBuilder();
            string orderstr = " order by c.forder,n.fpubtime desc ";
            sb.Append("select  n.fid,n.Fwebid,n.fpicurl ");
            sb.Append(" from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            sb.Append("c.FColNumber='");
            sb.Append(fcol);
            sb.Append("' and c.fisdeleted=0 and c.fstate=1 ");

            sb.Append(orderstr);

            DataTable dt = this.GetPageTable(sb.ToString(), top1, top2);
            sb.Remove(0, sb.Length);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dt;

            }
        }




        public string ShowTitle(string fid)
        {
            DataTable dt = this.GetTable(EntityTypeEnum.EnTitle, "", "fid='" + fid + "' ");
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["FName"].ToString();
            }
            return "";
        }

        public string ShowData(string FId)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = this.GetTable(EntityTypeEnum.EnContent, "", "FNewsId='" + FId + "' ");
            if (dt.Rows.Count > 0)
            {
                sb.Append("<div id='jscontent' class='text-newspop'>" + dt.Rows[0]["FContent"].ToString() + "</div>");
            }
            else
            {
                sb.Append("<center>没有数据</center>");

            }
            return sb.ToString();
        }

        public string getWZDJ(string FId) //获取文章点击数
        {
            StringBuilder sb = new StringBuilder();
            EnTitle news = (EnTitle)this.GetEBase(EntityTypeEnum.EnTitle, "", "FID='" + FId + "'");
            if (news == null)
            {
                return "";
            }
            else
            {
                //sb.Append("<div style='height:30px;vertical-align:middle;line-height:30px' align=right>");
                sb.Append("<script>function doZoom(size){ document.getElementById('jscontent').style.fontSize=size+'px'}</script>");
                sb.Append("<table width=100%><tr><td height=35 valign=middle align=center'>");
                sb.Append("http://www.shannaxijs.gov.cn&nbsp;&nbsp;");
                sb.Append("</td>");
                sb.Append("<td  valign=middle align=center'>");
                sb.Append(news.FPubTime.ToShortDateString());
                sb.Append("</td>");
                sb.Append("<td  valign=middle align=center'>");
                sb.Append("&nbsp;&nbsp;文章点击数为：");
                sb.Append(this.txtStatic(FId));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("&nbsp;&nbsp;&nbsp;字号：&nbsp;<a class=text1 href='javascript:doZoom(16)'>[大]</a>&nbsp;");
                sb.Append("<a class=text1 href='javascript:doZoom(14)'>[中]</a>&nbsp;");
                sb.Append("<a class=text1 href='javascript:doZoom(12)'>[小]</a>");
                sb.Append("</td></tr></table>");
                //sb.Append("</div>");

            }
            return sb.ToString();
        }


        public string getWZDJ(string FId, string message) //获取文章点击数
        {
            StringBuilder sb = new StringBuilder();
            EnTitle news = (EnTitle)this.GetEBase(EntityTypeEnum.EnTitle, "", "FID='" + FId + "'");
            if (news == null)
            {
                return "";
            }
            else
            {
                //sb.Append("<div style='height:30px;vertical-align:middle;line-height:30px' align=right>");
                sb.Append("<script>function doZoom(size){ document.getElementById('jscontent').style.fontSize=size+'px'}</script>");
                sb.Append("<table width=100%><tr><td height=35 valign=middle align=center'>");
                sb.Append(message);
                sb.Append("</td>");
                sb.Append("<td  valign=middle align=center'>");
                sb.Append(news.FPubTime.ToShortDateString());
                sb.Append("</td>");
                sb.Append("<td  valign=middle align=center'>");
                sb.Append("&nbsp;&nbsp;点击数为：");
                sb.Append(this.txtStatic(FId));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("&nbsp;&nbsp;&nbsp;字号：&nbsp;<a class=text1 href='javascript:doZoom(16)'>[大]</a>&nbsp;");
                sb.Append("<a class=text1 href='javascript:doZoom(14)'>[中]</a>&nbsp;");
                sb.Append("<a class=text1 href='javascript:doZoom(12)'>[小]</a>");
                sb.Append("</td></tr></table>");
                //sb.Append("</div>");

            }
            return sb.ToString();
        }

        public string GetNewsFPubDepart(string fid)
        {
            StringBuilder sb = new StringBuilder();
            EnTitle news = (EnTitle)this.GetEBase(EntityTypeEnum.EnTitle, "FPubDepart", "FID='" + fid + "'");
            if (news == null)
            {
                return "";
            }
            return news.FPubDepart;
        }

        public string getNewsPL(string fid, string fcol)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            DataTable dt = this.GetTable(EntityTypeEnum.EnComment, "", "fnewsid='" + fid + "' and fisdeleted =0 and fstate=1");
            if (dt != null)
                count = dt.Rows.Count;
            else
                count = 0;
            sb.Append("<a href=./CommentList.aspx?fid=" + fid + "&type=" + fcol + " target=_blank class=text0><b>【评论(" + count.ToString() + ")】</b></a>");
            return sb.ToString();
        }

        public string getKey(string fid)
        {
            EnTitle news;
            string key = "";
            news = (EnTitle)this.GetEBase(EntityTypeEnum.EnTitle, "", " fid='" + fid + "'");
            if (news != null)
            {
                key = news.FKey;
            }
            return key;
        }

        //提取标题
        public Approve.EntityCenter.EData GetNewsData(string FId, int type)
        {
            EData edata = new EData();

            EnTitle est = (EnTitle)this.GetEBase(EntityTypeEnum.EnTitle, "", "Fid='" + FId + "'");
            if (est != null)
            {
                edata.FTxt1 = est.FName;
                edata.FTxt2 = est.FPubTime.ToString("yyyy-MM-dd");
            }
            else
            {
                return null;
            }

            if (type == 2)
            {
                EnContent econtent = (EnContent)this.GetEBase(EntityTypeEnum.EnContent, "", "FNewsId='" + FId + "'");
                if (econtent != null)
                {
                    edata.FTxt3 = econtent.FContent;
                }
            }
            return edata;
        }

        //提取数据
        public Approve.EntityCenter.EData getNewsFirstData(string fcol)
        {
            EData edata = new EData();

            StringBuilder sb = new StringBuilder();
            //sb.Append("select top 1  n.*,c.FColNumber,c.FState,c.FColor,c.FPic,c.Fisdeleted,c.Forder,c.FColor,n.FOperType ");
            sb.Append("select n.fid,n.fname,n.fmain,n.fpubtime,fpicurl,n.fcount ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            sb.Append("c.FColNumber='" + fcol + "'");
            sb.Append(" order by c.FOrder,c.fpubtime desc ");
            DataTable dt = this.GetTable(sb.ToString());
            if (dt.Rows.Count > 0)
            {
                edata.FTxt1 = dt.Rows[0]["FID"].ToString();
                edata.FTxt2 = dt.Rows[0]["Fname"].ToString();
                edata.FTxt3 = dt.Rows[0]["fmain"].ToString();
                edata.FTxt4 = dt.Rows[0]["FPubTime"].ToString();
                edata.FTxt5 = dt.Rows[0]["FPicUrl"].ToString();
                if (dt.Rows[0]["fcount"].ToString() != null && dt.Rows[0]["fcount"].ToString() != "")
                {
                    edata.FInt1 = int.Parse(dt.Rows[0]["fcount"].ToString());
                }
                else
                {
                    edata.FInt1 = 0;
                }
            }
            else
            {
                return null;
            }
            return edata;
        }

        public string GetNewsPicAndDetail(string FCol, string twidth, string theight, string t1width, string pwidth, string pheight, string pcss, string t2width, string url, string t2css, int sub)
        {
            StringBuilder sb = new StringBuilder();
            EData edata = this.getNewsFirstData(FCol);
            if (edata != null)
            {
                sb.Append("<table cellSpacing='0' cellPadding='0' width='");
                if (twidth != "")
                    sb.Append(twidth);
                else
                    sb.Append("100%");
                sb.Append("' ");

                sb.Append(" height='");
                if (theight != "")
                    sb.Append(theight);
                else
                    sb.Append("100%");
                sb.Append("' ");
                sb.Append("'  align='center' border='0' >");
                sb.Append("<tr>");
                sb.Append("<td align=center valign=middle>");
                sb.Append("<a href='" + url + "?fid=");
                sb.Append(edata.FTxt1 + "&type=" + FCol + "' target=_blank title='" + edata.FTxt2 + "' >");
                sb.Append("<img src='" + edata.FTxt5 + "' target=_blank class='" + pcss + "'");
                sb.Append(" width='" + pwidth + "' height='" + pheight + "' >");
                sb.Append("</a>");
                sb.Append("</td>");

                sb.Append("<td width='" + t2width + "' style='padding-left:2px'>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;<a href='" + url + "?fid=");
                sb.Append(edata.FTxt1 + "&type=" + FCol + "' class='" + t2css + "' target=_blank title='" + edata.FTxt2 + "' >");
                sb.Append(this.getSubStr(edata.FTxt3, sub));
                sb.Append("</a>");
                sb.Append("</td>");

                sb.Append("</tr>");
                sb.Append("</table>");
                return sb.ToString();
            }
            return sb.ToString();
        }

        /// <summary>
        /// 得到某种分类信息的头条(标题加简介的格式)
        /// </summary>
        /// <param name="fcol"></param>
        /// <param name="tablewidth"></param>
        /// <param name="url"></param>
        /// <param name="tsub"></param>
        /// <param name="dsub"></param>
        /// <param name="tcss"></param>
        /// <param name="dcss"></param>
        /// <returns></returns>
        public string getFirstNewsTable(string fcol, string tablewidth, string url, int tsub, int dsub, string tcss, string dcss)
        {
            EData edata = this.getNewsFirstData(fcol);
            if (edata == null)
            {
                return "暂无内容";

            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<table cellSpacing='0' cellPadding='0' width='");
            if (tablewidth != "")
                sb.Append(tablewidth);
            else
                sb.Append("100%");
            sb.Append("'  align='center' border='0' >");
            sb.Append("<tr>");
            sb.Append("<td align=center >");
            sb.Append("<a href='" + url + "?fid=");
            sb.Append(edata.FTxt1 + "&type=" + fcol + "' class='" + tcss + "' target=_blank title='" + edata.FTxt2 + "' >");
            sb.Append(this.getSubStr(edata.FTxt2, tsub));
            sb.Append("</a>");
            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td align=left >");
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;<a href='" + url + "?fid=");
            sb.Append(edata.FTxt1 + "&type=" + fcol + "' class='" + dcss + "' target=_blank title='" + edata.FTxt2 + "' >");
            sb.Append(this.getSubStr(edata.FTxt3, dsub));
            sb.Append("</a>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            return sb.ToString();
        }



        public string getFirstNewsTableWithPic(string fcol, string tablewidth, string picwidth, string picheight, string url, int tsub, int dsub, string tcss, string dcss, string pcss, int type)
        {

            EData edata = this.getNewsFirstData(fcol);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();


            if (edata == null)
            {
                return "<center>暂无内容</center>";

            }

            sb1.Append("<td  rowspan=2 valign='middle'>");


            sb1.Append("<a href='" + url + "?fid=");
            sb1.Append(edata.FTxt1 + "&type=" + fcol + "'");
            sb1.Append(" target=_blank >");
            if (edata.FTxt5.Length > 6)
            {
                sb1.Append("<img src='" + edata.FTxt5 + "' border=0 ");
            }
            else
            {
                sb1.Append("<img src='../images/Nopic.gif' border=0 ");
            }
            if (picwidth != "")
            {
                sb1.Append(" width=" + picwidth);
            }
            if (picheight != "")
            {
                sb1.Append(" height=" + picheight);
            }
            if (pcss != "")
            {
                sb1.Append(" class='" + pcss + "' ");
            }
            sb1.Append("/>");
            sb1.Append("</a>");


            sb.Append("<table cellSpacing='0' cellPadding='0'  width='");
            if (tablewidth != "")
                sb.Append(tablewidth);
            else
                sb.Append("100%");
            sb.Append("'  align='center' border='0' >");
            sb.Append("<tr>");
            if (type == 1)
            {
                sb.Append(sb1);
            }


            sb.Append("<td align=center");
            if (tsub != 0)
            {
                sb.Append(">");
                sb.Append("<a href='" + url + "?fid=");
                sb.Append(edata.FTxt1 + "&type=" + fcol + "' class='" + tcss + "' target=_blank title='" + edata.FTxt2 + "' >");
                sb.Append(this.getSubStr(edata.FTxt2, tsub));
                sb.Append("</a>");

            }
            else
            {
                sb.Append(" style='display:none'>");
            }
            sb.Append("</td>");

            if (type == 2)
            {
                sb.Append(sb1);
            }



            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td align=left >");
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;<a href='" + url + "?fid=");
            sb.Append(edata.FTxt1 + "&type=" + fcol + "' class='" + dcss + "' target=_blank title='" + edata.FTxt2 + "' >");
            sb.Append(this.getSubStr(edata.FTxt3, dsub));
            sb.Append("</a>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            return sb.ToString();
        }


        //连接
        public string ShowLinkFriendb(string fcol, string tablewidth, int col, int pheight, int pwidth, int top)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select top " + top.ToString() + " n.FWebId,n.FId,n.FPicUrl,");
            sb.Append("c.FColNumber,c.FState,c.FPic,c.Fisdeleted,c.Forder from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            sb.Append("c.FColNumber='");
            sb.Append(fcol);
            sb.Append("' and c.fisdeleted=0 and c.fstate=1");
            sb.Append(" order by c.forder,c.fpubtime desc");

            DataTable dt = this.GetTable(sb.ToString());
            sb.Remove(0, sb.Length);
            if (dt == null || dt.Rows.Count <= 0)
            {
                sb.Append("<center>暂无内容</center>");
                return sb.ToString();
            }
            sb.Append("<table  height=10 align=center  cellpadding=0 cellspacing=0 border=0");
            if (tablewidth != "")
            {
                sb.Append(" width=" + tablewidth);
            }
            sb.Append(">");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (i % col == 0)
                {
                    sb.Append("<tr>");
                }

                sb.Append("<td align=center vAlign=middle width=" + (100 / col) + "%  height=" + (pheight + 8) + ">");
                sb.Append("<a class=text01 href='");
                string fwebid = dr["FWebId"].ToString();
                if (fwebid == "")
                    sb.Append("//");
                else
                    sb.Append(dt.Rows[i]["FWebId"].ToString());

                sb.Append("' target=_blank ><img src=");
                sb.Append(dt.Rows[i]["FPicUrl"].ToString());
                sb.Append(" border=0  height=" + pheight.ToString() + " width=" + pwidth.ToString() + "></a>");
                sb.Append("</td>");
                if ((i + 1) % col == 0) sb.Append("</tr>");
                if (i + 1 == dt.Rows.Count)
                {
                    for (int j = i % col; j < col; j++)
                    {
                        sb.Append("<td></td>");
                    }
                    sb.Append("</tr>");

                }
            }
            sb.Append("</table>");
            return sb.ToString();
        }


        public string ShowPicAndMain(string fcol, string tablewidth, int width, int height, string piccss, string css, int sub, string url)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            sb.Append("select top 1 n.*,c.FColNumber,c.FState,c.FColor,c.FPic,c.Fisdeleted,c.Forder,c.FColor,n.FOperType ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            sb.Append("c.FColNumber='" + fcol + "'");
            sb.Append(" and c.fisdeleted=0 and c.fstate=1 ");
            sb.Append(" order by c.forder,c.fpubtime desc");

            DataTable dt = this.GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Remove(0, sb.Length);

                sb.Append("<table cellSpacing='0' cellPadding='0' width='");
                if (tablewidth != "")
                    sb.Append(tablewidth);
                else
                    sb.Append("100%");
                sb.Append("'  align='center' border='0' >");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string fisdirect = dr["FOperType"].ToString();
                    string titlecolor = dr["FColor"].ToString();
                    string title = dr["FName"].ToString();
                    string FMain = dr["FMain"].ToString();

                    string FTypeId = dr["FTypeId"].ToString();
                    if (Convert.ToInt32(FTypeId) < 3)
                    {
                        switch (fisdirect)
                        {
                            case "1":
                                sb1.Append("<a href='" + url + "?fid=");
                                sb1.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fcol + "' class='" + css + "' target=_blank title='" + title + "' >");
                                break;
                            case "3":
                                sb1.Append("<a href='" + dr["ffilenote"].ToString() + "' class='" + css + "' target=_blank title='" + title + "' >");
                                break;
                            case "2":
                                sb1.Append("<a href='" + dr["FWebId"].ToString() + "' class='" + css + "' target=_blank title='" + title + "' >");
                                break;
                            default:
                                sb1.Append("<a href='" + url + "?fid=");
                                sb1.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fcol + "' class='" + css + "' target=_blank title='" + title + "' >");
                                break;
                        }
                    }
                    else
                    {
                        sb1.Append("<a class=" + css + " target=_blank href=ShowDetailInfo.aspx?fid=");
                        sb1.Append(dr["FId"].ToString());
                        sb1.Append("&type=" + dr["FColNumber"].ToString());
                        sb1.Append(" >");
                    }


                    sb.Append("<tr><td align=left style='padding:1px 1px 1px 1px;' >");

                    sb.Append(sb1.ToString());

                    sb.Append("<img border=0 src='" + dr["FPicUrl"].ToString());
                    sb.Append("' width=" + width + " height=" + height + " class=" + piccss + " />");

                    sb.Append("</a>");
                    sb.Append(sb1.ToString());


                    string txt = dr["FMain"].ToString();
                    if (txt.Length > sub)
                        txt = txt.Substring(0, sub) + "..";
                    sb.Append(txt);

                    sb.Append("</a>");

                    sb.Append("</td>");
                    sb.Append("</tr>");

                }
                sb.Append("</table>");
            }
            else
            {
                sb.Remove(0, sb.Length);
                sb.Append("<center>暂无数据</center>");
            }
            return sb.ToString();
        }

        public string ShowNewsPicAndList(string colNumber1, string colNumber2, int iTop, string FKind, string tablewidth, string t1width, string pwidth, string pheight, string pcss, string t2width, string flag, int lheight, int sub, int isdate, string t2css, int isline, string url, string fwidth, string nwidth, string dwidth)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select top 1 ");
            sb.Append(" n.fid,n.fname,n.FPicUrl,");
            sb.Append(" c.FColNumber from CF_News_Title n,CF_News_Col c where ");
            sb.Append(" n.fid=c.FNewsID and n.fisdeleted=0 ");
            sb.Append(" and c.fstate=1 and c.fisdeleted=0 ");

            if (colNumber2.IndexOf('%') > 0)
            {
                sb.Append(" and c.FColNumber like '" + colNumber1 + "' ");
            }
            else
            {
                sb.Append(" and c.FColNumber='" + colNumber1 + "'");
            }
            sb.Append(" order by c.forder,c.fpubtime desc");

            DataTable dt = this.GetTable(sb.ToString());
            sb.Remove(0, sb.Length);
            sb.Append("<table cellSpacing='0' cellPadding='0' width='");
            if (tablewidth != "")
                sb.Append(tablewidth);
            else
                sb.Append("100%");
            sb.Append("'  align='center' border='0' >");
            sb.Append("<tr>");
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Append("<td width='" + t1width + "' align=center>");
                DataRow dr = dt.Rows[0];
                sb.Append("<a href='" + url + "?fid=");
                sb.Append(dr["FId"].ToString() + "&type=" + dr["FColNumber"].ToString() + "'  target=_blank title='" + dr["fname"].ToString() + "' >");
                sb.Append("<img src='" + dr["FPicUrl"].ToString() + "' border=0 width='" + pwidth + "' height='" + pheight + "' class='" + pcss + "'>");
                sb.Append("</a>");
                sb.Append("</td>");
            }

            sb.Append("<td>");
            sb.Append(this.ShowIndexNews(colNumber2, iTop, 1, t2width, flag, lheight, sub, isdate, t2css, isline, url, "_blank", fwidth, nwidth, dwidth));
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            return sb.ToString();
        }


        public string ShowNewsPic(string fcol, string tablewidth, int width, int height, string piccss, string css, string url)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            RNews Re = new RNews();
            sb.Remove(0, sb.Length);

            sb.Append("select top 1 n.fid,n.fopertype,n.ffilenote,n.fname,n.FPicUrl,n.fmain,n.FTypeId,c.FColNumber,c.FState,c.FColor,c.FPic,c.Fisdeleted,c.Forder,c.FColor,n.FOperType ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            sb.Append("c.FColNumber='" + fcol + "'");
            sb.Append(" and c.fisdeleted=0 and c.fstate=1 ");
            sb.Append(" order by c.forder,c.fpubtime desc");

            DataTable dt = Re.GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Remove(0, sb.Length);

                sb.Append("<table cellSpacing='0' cellPadding='0' width='");
                if (tablewidth != "")
                    sb.Append(tablewidth);
                else
                    sb.Append("100%");
                sb.Append("'  align='center' border='0' >");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string fisdirect = dr["FOperType"].ToString();
                    string titlecolor = dr["FColor"].ToString();
                    string title = dr["FName"].ToString();
                    string FMain = dr["FMain"].ToString();

                    string FTypeId = dr["FTypeId"].ToString();
                    if (Convert.ToInt32(FTypeId) < 3)
                    {
                        switch (fisdirect)
                        {
                            case "0":
                                sb1.Append("<a href='" + url + "?fid=");
                                sb1.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fcol + "' class='" + css + "' target=_blank title='" + title + "' >");
                                break;
                            case "1":
                                sb1.Append("<a href='" + dr["ffilenote"].ToString() + "' class='" + css + "' target=_blank title='" + title + "' >");
                                break;
                            case "2":
                                sb1.Append("<a href='" + dr["FWebId"].ToString() + "' class='" + css + "' target=_blank title='" + title + "' >");
                                break;
                            default:
                                sb1.Append("<a href=''" + url + "?fid=");
                                sb1.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fcol + "' class='" + css + "' target=_blank title='" + title + "' >");
                                break;
                        }
                    }
                    else
                    {
                        sb1.Append("<a class=" + css + " target=_blank href=ShowDetailInfo.aspx?fid=");
                        sb1.Append(dr["FId"].ToString());
                        sb1.Append("&type=" + dr["FColNumber"].ToString());
                        sb1.Append(" >");
                    }


                    sb.Append("<tr><td align=center style='padding:1px 1px 1px 1px;' >");
                    sb.Append(sb1.ToString());

                    sb.Append("<img border=0 src=" + dr["FPicUrl"].ToString());
                    sb.Append(" width=" + width + " height=" + height + " class=" + piccss + " />");


                    sb.Append("</a>");

                    sb.Append("</td>");
                    sb.Append("</tr>");

                }
                sb.Append("</table>");
            }
            else
            {
                sb.Remove(0, sb.Length);
                sb.Append("<center>暂无数据</center>");
            }
            return sb.ToString();

        }



        //王几所写方法
        public DataTable getNewsTable(string fcol, int srow, int erow)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            sb.Append("select  n.fid,n.fname,n.ftype,n.ftypeid,n.forigin,n.fopertype,n.fwebid,n.ffilenote,n.fcount,");
            sb.Append("n.fmain,n.fkey,n.fpubdepart,n.fauthor,n.fsize,n.fregion,");
            sb.Append("n.fpubperson,n.fvideoid,n.forder,n.fpubtime,n.fpicurl,n.fclass ,");
            sb.Append("c.FColNumber,c.FState,c.FPic,c.FColor ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid  ");
            //sb.Append("c.FColNumber='" + fcol + "'");
            //string fid = "";
            //dt = this.GetTable(EntityTypeEnum.EsTree, "", "FNumber='" + fcol + "'");
            //if (dt.Rows.Count > 0)
            //{
            //    fid = dt.Rows[0]["FID"].ToString();
            //    sb.Append(" ( c.FColNumber in (select t.FNumber from CF_Sys_Tree t where t.FParentid='" + fid + "')");

            //    sb.Append(" or  c.FColNumber='" + fcol + "' )");

            //    sb.Append(" and (c.FColNumber='" + fcol + "' ");

            //    sb.Append(" or c.FColNumber in (select t.FNumber from CF_Sys_Tree t where t.FParentid='" + fid + "')");
            //    sb.Append(" and c.fid in (select top 1 cc.fid from CF_News_Col cc where ");
            //    sb.Append(" cc.fnewsid=n.fid  ");
            //    sb.Append(" and cc.FColNumber in (select t.FNumber from CF_Sys_Tree t where t.FParentid='" + fid + "')))");

            //}
            sb.Append(" and c.fcolnumber='");
            sb.Append(fcol + "' ");



            sb.Append(" order by c.forder,c.fpubtime desc");




            dt = this.GetPageTable(sb.ToString(), srow, erow);
            //dt = this.GetTable(sb.ToString());
            return dt;
        }

        public DataTable getNewsTable(string fcol, int top)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();



            sb.Append("select  top " + top + " n.fid,n.fname,n.ftype,n.ftypeid,n.forigin,n.fopertype,n.fwebid,n.ffilenote,n.fcount,");
            sb.Append("n.fmain,n.fkey,n.fpubdepart,n.fauthor,n.fsize,n.fregion,");
            sb.Append("n.fpubperson,n.fvideoid,n.forder,n.fpubtime,n.fpicurl,n.fclass ,");
            sb.Append("c.FColNumber,c.FState,c.FPic,c.FColor ");



            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            //string fid = "";
            //dt = this.GetTable(EntityTypeEnum.EsTree, "", "FNumber='" + fcol + "'");
            //if (dt.Rows.Count > 0)
            //{
            //    fid = dt.Rows[0]["FID"].ToString();
            //    sb.Append(" ( c.FColNumber in (select t.FNumber from CF_Sys_Tree t where t.FParentid='" + fid + "')");

            //    sb.Append(" or  c.FColNumber='" + fcol + "' )");

            //    sb.Append(" and (c.FColNumber='" + fcol + "' ");

            //    sb.Append(" or c.FColNumber in (select t.FNumber from CF_Sys_Tree t where t.FParentid='" + fid + "')");
            //    sb.Append(" and c.fid in (select top 1 cc.fid from CF_News_Col cc where ");
            //    sb.Append(" cc.fnewsid=n.fid  ");
            //    sb.Append(" and cc.FColNumber in (select t.FNumber from CF_Sys_Tree t where t.FParentid='" + fid + "')))");
            //}

            sb.Append("  c.fcolnumber='");
            sb.Append(fcol + "' ");
            sb.Append(" and n.fisdeleted=0 and c.fstate=1 ");
            sb.Append(" order by c.forder,c.fpubtime desc");

            dt = this.GetTable(sb.ToString());
            return dt;
        }

        public string ShowIndexNews(string fclassid1, string flag, string tablewidth, int height, int sub, int isdate, int top, string style, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select top " + top.ToString() + " n.fid,n.fname,n.FOperType,n.FTypeId,n.ffilenote,n.fwebid,n.FType,n.FPubTime,");
            sb.Append(" c.FColNumber,c.FState,c.FColor,c.FPic ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            if (fclassid1.IndexOf('%') > 0)
            {
                sb.Append("c.FColNumber like '" + fclassid1 + "' ");
            }
            else
            {
                sb.Append(" c.FColNumber='" + fclassid1 + "'");
            }
            sb.Append(" and c.fisdeleted=0 and c.fstate=1 ");
            sb.Append(" order by c.forder,c.fpubtime desc");

            string fclassid = "";
            if (fclassid1.IndexOf('%') > 0)
                fclassid = fclassid1.Substring(0, fclassid1.Length - 1);
            else
                fclassid = fclassid1;

            DataTable dt = this.GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Remove(0, sb.Length);

                sb.Append("<table cellSpacing='0' cellPadding='0'   align='center' border='0' ");
                if (tablewidth != "")
                {
                    sb.Append(" width=" + tablewidth);
                }
                else
                {
                    sb.Append(" width=100% ");
                }
                sb.Append(">");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string fisdirect = dr["FOperType"].ToString();
                    string titlecolor = dr["FColor"].ToString();
                    string title = dr["fName"].ToString();
                    sb.Append("<tr align='top'><td align='left' width=2% >");
                    if (flag.Length > 1 && flag.Substring(0, 1) != "<")
                    {
                        sb.Append("&nbsp;<img src='" + flag + "' border=0 />&nbsp;");

                    }
                    else
                    {
                        sb.Append("<font class=text0>&nbsp;" + flag + "</font>&nbsp;");
                    }
                    sb.Append("</td><td height=" + height + "  align='left' >");
                    sb.Append("<a href='" + url + "?fid=");
                    sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fclassid + "' class='" + style + "' target=_blank title='" + title + "' >");

                    //if (EConvert.ToInt(fisdirect) <3)
                    //{
                    //    switch (fisdirect)
                    //    {
                    //        case "1":
                    //            sb.Append("<a href='" + url + "?fid=");
                    //            sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fclassid + "' class='" + style + "' target=_blank title='" + title + "' >");
                    //            break;
                    //        case "2":
                    //            sb.Append("<a href='" + dr["ffilenote"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                    //            break;
                    //        case "3":
                    //            sb.Append("<a href='" + dr["fwebid"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                    //            break;
                    //        default:
                    //            sb.Append("<a href='../../webSiteNews/Main/ShowDetailNews.aspx?fid=");
                    //            sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fclassid + "' class='" + style + "' target=_blank title='" + title + "' >");
                    //            break;
                    //    }
                    //}

                    //else
                    //{
                    //    sb.Append("<a class=" + style + " target=_blank href=ShowDetailInfo.aspx?fid=");
                    //    sb.Append(dr["FId"].ToString());
                    //    sb.Append("&type=" + dr["FColNumber"].ToString());
                    //    sb.Append(" >");

                    //}



                    string opetype = dr["FType"].ToString();
                    int subb = sub;
                    subb -= 2;
                    string title1 = "";
                    switch (opetype)
                    {
                        case "1":
                            title1 = "<img border=0 src=../../webSiteNews/images/new.gif />";
                            break;
                        case "2":
                            title1 = "<img border=0 src=../../webSiteNews/images/hot.gif />";
                            break;
                        default:
                            subb += 2;
                            break;
                    }

                    if (title.Length > subb)
                    {
                        title = title.Substring(0, subb) + "..";
                    }
                    title += title1;




                    if (titlecolor != "")
                    {
                        title = "<font color='" + titlecolor + "' >" + title + "</font>";
                    }
                    sb.Append(title);
                    sb.Append("</a>");

                    sb.Append("</td>");
                    switch (isdate)
                    {
                        case 1:
                            sb.Append("<td class=" + style + " width=50valign='top' class=text0>" + Convert.ToDateTime(dt.Rows[i]["FCreateTime"].ToString()).ToString("MM-dd") + "</td>");
                            break;
                        case 2:
                            sb.Append("<td class=" + style + " width=100 valign='top' class=text0>" + Convert.ToDateTime(dt.Rows[i]["FCreateTime"].ToString()).ToString("yyyy-MM-dd") + "</td>");
                            break;
                        case 3:
                            sb.Append("<td class=" + style + " width=60 valign='top' class=text0>" + Convert.ToDateTime(dt.Rows[i]["FCreateTime"].ToString()).ToString("MM月dd日") + "</td>");
                            break;
                    }

                    sb.Append("</tr>");

                }
                sb.Append("</table>");
            }
            else
            {
                sb.Remove(0, sb.Length);
                sb.Append("<center>暂无数据</center>");
            }
            return sb.ToString();

        }

        public string ShowEnNews1(string fcol, int width, int sub, int type)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            //flag显示的为行前的标志,例*或·  sub为取的标题长度  isdate为是否显示日期1为显示,0为不显示

            sb.Append("select top 1 ");
            sb.Append(" n.FName,n.FMain,n.FTypeId,n.ffilenote,n.FWebId,n.fid,n.FPicUrl,");
            sb.Append(" c.FColNumber,c.FState,c.FColor,c.FPic,c.Fisdeleted,c.Forder,c.FColor,n.FOperType ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");

            sb.Append("c.FColNumber='" + fcol + "'");
            sb.Append(" and c.fisdeleted=0 and c.fstate=1 ");
            sb.Append(" order by c.forder,c.fpubtime desc");

            DataTable dt = this.GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Remove(0, sb.Length);

                sb.Append("<table cellSpacing='0' cellPadding='0' width='100%'  align='center' border='0' >");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string fisdirect = dr["FOperType"].ToString();
                    string titlecolor = dr["FColor"].ToString();
                    string title = dr["FName"].ToString();
                    string FMain = dr["FMain"].ToString();

                    string FTypeId = dr["FTypeId"].ToString();
                    if (Convert.ToInt32(FTypeId) < 3)
                    {
                        switch (fisdirect)
                        {
                            case "1":
                                sb1.Append("<a href='../../webSiteNews/Main/ShowDetailNews.aspx?fid=");
                                sb1.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fcol + "' class='text01' target=_blank title='" + title + "' >");
                                break;
                            case "3":
                                sb1.Append("<a href='" + dr["ffilenote"].ToString() + "' class='text01' target=_blank title='" + title + "' >");
                                break;
                            case "2":
                                sb1.Append("<a href='" + dr["FWebId"].ToString() + "' class='text01' target=_blank title='" + title + "' >");
                                break;
                            default:
                                sb1.Append("<a href='../../webSiteNews/Main/ShowDetailNews.aspx?fid=");
                                sb1.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fcol + "' class='text01' target=_blank title='" + title + "' >");
                                break;
                        }
                    }
                    else
                    {
                        sb1.Append("<a class=text0 target=_blank href=ShowDetailInfo.aspx?fid=");
                        sb1.Append(dr["FId"].ToString());
                        sb1.Append("&type=" + dr["FColNumber"].ToString());
                        sb1.Append(" >");
                    }


                    sb.Append("<tr align='top'><td width=" + width + " align=center >");
                    sb.Append(sb1.ToString());
                    switch (type)
                    {
                        case 1:
                            sb.Append("<img border=0 src=" + dr["FPicUrl"].ToString());
                            sb.Append(" width=" + Convert.ToString(width - 10) + " height='70'/>");
                            sb.Append("</a>");
                            sb.Append("</td><td valign=top >");
                            type = 0;
                            break;
                        case 2:
                            //sb.Append("<img style='margin-bottom:10px;' border=0 src=" + dr["FPicUrl"].ToString());
                            // sb.Append(" width=" + Convert.ToString(width - 20) + " />");
                            sb.Append(getVideoPlay(dr["FPicUrl"].ToString(), width - 50, 180));

                            sb.Append("<br>");
                            sb.Append(title);
                            sb.Append("</a>");
                            sb.Append("</td><td valign=top >");
                            type = 0;
                            break;
                        case 3:
                            sb.Append("<img style='margin-bottom:10px;' border=0 src=" + dr["FPicUrl"].ToString());
                            sb.Append(" width=" + Convert.ToString(width - 20) + " />");
                            sb.Append("</a>");
                            sb.Append("</td></tr><tr><td valign=top >");
                            type = 0;
                            break;
                        case 4:
                            sb.Append("<img style='margin-bottom:10px;' border=0 src=" + dr["FPicUrl"].ToString());
                            sb.Append(" width=" + Convert.ToString(width - 20) + " />");
                            sb.Append("</a>");
                            sb.Append("</td></tr><tr><td valign=top >");
                            FMain = title;
                            break;
                        case 5:
                            sb.Append("<img border=1 style='border:#000 1px solid; padding:7px;margin-bottom:1px;margin-right:9px;' src=" + dr["FPicUrl"].ToString());
                            sb.Append(" width=" + Convert.ToString(width - 10) + " />");
                            sb.Append("</a>");
                            sb.Append("</td><td valign=top align=left >");
                            if (title.Length > 10)
                                title = title.Substring(0, 10) + "..";
                            sb.Append("<a class=newsTitle target=_blank href=../../webSiteNews/Main/ShowDetailNews.aspx?fid=");
                            sb.Append(dr["FId"].ToString());
                            sb.Append("&type=" + dr["FColNumber"].ToString());
                            sb.Append(" >");
                            sb.Append(title);
                            sb.Append("</a>");
                            sb.Append("<br>");
                            type = 0;
                            break;

                        default:
                            FMain = dr["FName"].ToString();
                            break;
                    }



                    sb.Append(sb1.ToString());

                    if (FMain.Length > sub)
                    {
                        FMain = FMain.Substring(0, sub) + "..";
                    }

                    FMain = "<img border=0 height=\"1\" src=\"../../webSiteEnterprise/images/sp.gif\" width=\"16\" />" + FMain;
                    //if (titlecolor != "")
                    //{
                    //    FMain = "<font color='" + titlecolor + "' >" + FMain + "</font>";
                    //}
                    sb.Append(FMain);
                    sb.Append("</a>");

                    sb.Append("</td>");
                    sb.Append("</tr>");

                }
                sb.Append("</table>");
            }
            else
            {
                sb.Remove(0, sb.Length);
                sb.Append("<center>暂无数据</center>");
            }
            return sb.ToString();

        }
        public string ShowEnNews2(string fcol, int width, int sub, int type)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            //flag显示的为行前的标志,例*或·  sub为取的标题长度  isdate为是否显示日期1为显示,0为不显示

            sb.Append("select top 1 ");
            sb.Append(" n.FName,n.FMain,n.FTypeId,n.ffilenote,n.FWebId,n.fid,n.FPicUrl,");
            sb.Append(" c.FColNumber,c.FState,c.FColor,c.FPic,c.Fisdeleted,c.Forder,c.FColor,n.FOperType ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");

            sb.Append("c.FColNumber='" + fcol + "'");
            sb.Append(" and c.fisdeleted=0 and c.fstate=1 ");
            sb.Append(" order by c.forder,c.fpubtime desc");

            DataTable dt = this.GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Remove(0, sb.Length);

                sb.Append("<table cellSpacing='0' cellPadding='0' width='100%'  align='center' border='0' >");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string fisdirect = dr["FOperType"].ToString();
                    string titlecolor = dr["FColor"].ToString();
                    string title = dr["FName"].ToString();
                    string FMain = dr["FMain"].ToString();

                    string FTypeId = dr["FTypeId"].ToString();
                    if (Convert.ToInt32(FTypeId) < 3)
                    {
                        switch (fisdirect)
                        {
                            case "1":
                                sb1.Append("<a href='../../webSiteNews/Main/ShowDetailNews.aspx?fid=");
                                sb1.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fcol + "' class='text0' target=_blank title='" + title + "' >");
                                break;
                            case "3":
                                sb1.Append("<a href='" + dr["ffilenote"].ToString() + "' class='text01' target=_blank title='" + title + "' >");
                                break;
                            case "2":
                                sb1.Append("<a href='" + dr["FWebId"].ToString() + "' class='text01' target=_blank title='" + title + "' >");
                                break;
                            default:
                                sb1.Append("<a href='../../webSiteNews/Main/ShowDetailNews.aspx?fid=");
                                sb1.Append(dt.Rows[i]["FId"].ToString() + "&type=" + fcol + "' class='text01' target=_blank title='" + title + "' >");
                                break;
                        }
                    }
                    else
                    {
                        sb1.Append("<a class=text0 target=_blank href=ShowDetailInfo.aspx?fid=");
                        sb1.Append(dr["FId"].ToString());
                        sb1.Append("&type=" + dr["FColNumber"].ToString());
                        sb1.Append(" >");
                    }


                    sb.Append("<tr align='top'><td width=" + width + " align=center >");
                    sb.Append(sb1.ToString());
                    switch (type)
                    {
                        case 1:
                            sb.Append("<img border=0 src='../images/bg555.jpg' ");
                            sb.Append(" width=" + Convert.ToString(width - 12) + " height='66'/>");
                            sb.Append("</a>");
                            sb.Append("</td><td valign=top >");
                            type = 0;
                            break;
                        case 2:
                            //sb.Append("<img style='margin-bottom:10px;' border=0 src=" + dr["FPicUrl"].ToString());
                            // sb.Append(" width=" + Convert.ToString(width - 20) + " />");
                            sb.Append(getVideoPlay(dr["FPicUrl"].ToString(), width - 50, 180));

                            sb.Append("<br>");
                            sb.Append(title);
                            sb.Append("</a>");
                            sb.Append("</td><td valign=top >");
                            type = 0;
                            break;
                        case 3:
                            sb.Append("<img style='margin-bottom:10px;' border=0 src=" + dr["FPicUrl"].ToString());
                            sb.Append(" width=" + Convert.ToString(width - 20) + " />");
                            sb.Append("</a>");
                            sb.Append("</td></tr><tr><td valign=top >");
                            type = 0;
                            break;
                        case 4:
                            sb.Append("<img style='margin-bottom:10px;' border=0 src=" + dr["FPicUrl"].ToString());
                            sb.Append(" width=" + Convert.ToString(width - 20) + " />");
                            sb.Append("</a>");
                            sb.Append("</td></tr><tr><td valign=top>");
                            FMain = title;
                            break;
                        case 5:
                            sb.Append("<img border=1 style='border:#000 1px solid; padding:7px;margin-bottom:1px;margin-right:9px;' src=" + dr["FPicUrl"].ToString());
                            sb.Append(" width=" + Convert.ToString(width - 10) + " />");
                            sb.Append("</a>");
                            sb.Append("</td><td valign=top align=left >");
                            if (title.Length > 10)
                                title = title.Substring(0, 10) + "..";
                            sb.Append("<a class=newsTitle target=_blank href=../../webSiteNews/Main/ShowDetailNews.aspx?fid=");
                            sb.Append(dr["FId"].ToString());
                            sb.Append("&type=" + dr["FColNumber"].ToString());
                            sb.Append(" >");
                            sb.Append(title);
                            sb.Append("</a>");
                            sb.Append("<br>");
                            type = 0;
                            break;

                        default:
                            FMain = dr["FName"].ToString();
                            break;
                    }



                    sb.Append(sb1.ToString());

                    if (FMain.Length > sub)
                    {
                        FMain = FMain.Substring(0, sub) + "..";
                    }

                    FMain = "<img border=0 height=\"1\" src=\"../../webSiteEnterprise/images/sp.gif\" width=\"16\" />" + FMain;
                    //if (titlecolor != "")
                    //{
                    //    FMain = "<font color='" + titlecolor + "' >" + FMain + "</font>";
                    //}
                    sb.Append(FMain);
                    sb.Append("</a>");

                    sb.Append("</td>");
                    sb.Append("</tr>");

                }
                sb.Append("</table>");
            }
            else
            {
                sb.Remove(0, sb.Length);
                sb.Append("<center>暂无数据</center>");
            }
            return sb.ToString();

        }

        public string getVideoPlay()
        {
            StringBuilder sb = new StringBuilder();
            string url = "";
            sb.Append("select top 1* from CF_Pub_Video n,CF_Pub_Col c where n.fid=c.FVideoId and n.FIsdeleted=0 and c.FState=1 and fistj=1 ");
            sb.Append("and c.FColNumber='106001' order by c.forder,c.fpubtime desc ");
            DataTable dt = this.GetTable(sb.ToString());
            sb.Remove(0, sb.Length);
            //DataTable dt = Re.GetTable(EntityTypeEnum.EpVideo, "top 1*", "fistj=1 and fisdeleted=0 and fstate=1 order by forder,fcreatetime desc");
            if (dt.Rows.Count > 0)
            {
                url = dt.Rows[0]["FWebUrl"].ToString();
            }
            sb.Append("<object id='player' classid='CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6' style='width: 340px; height: 330px'>");
            sb.Append("<param NAME='AutoStart' VALUE='-1'>");
            sb.Append("<param NAME='Balance' VALUE='0'>");
            sb.Append("<param name='enabled' value='-1'>");
            sb.Append("<param NAME='EnableContextMenu' VALUE='-1'>");
            sb.Append("<param NAME='url' value='" + url + "'>");
            sb.Append("<param NAME='PlayCount' VALUE='1'>");
            sb.Append("<param name='rate' value='1'>");
            sb.Append("<param name='currentPosition' value='0'>");
            sb.Append("<param name='currentMarker' value='0'>");
            sb.Append("<param name='defaultFrame' value=''>");
            sb.Append("<param name='invokeURLs' value='0'>");
            sb.Append("<param name='baseURL' value=''>");
            sb.Append("<param name='stretchToFit' value='0'>");
            sb.Append("<param name='volume' value='50'>");
            sb.Append("<param name='mute' value='0'>");
            sb.Append("<param name='uiMode' value='Full'>");
            sb.Append("<param name='windowlessVideo' value='0'>");
            sb.Append("<param name='fullScreen' value='0'>");
            sb.Append("<param name='enableErrorDialogs' value='-1'>");
            sb.Append("<param name='SAMIStyle' value>");
            sb.Append("<param name='SAMILang' value>");
            sb.Append("<param name='SAMIFilename' value>");
            sb.Append("</object>");
            sb.Append("<div style='height:30px; width:85%;vertical-align:middle; text-align:left;margin-top:5px'>简介：");
            if (dt.Rows[0]["FDesc"].ToString().Length > 48)
                sb.Append(dt.Rows[0]["FDesc"].ToString().Substring(0, 48) + "..");
            else
                sb.Append(dt.Rows[0]["FDesc"].ToString());
            sb.Append("</div>");
            return sb.ToString();

        }

        public string getVideoPlay(string url, int height, int width)
        {
            StringBuilder sb = new StringBuilder();
            string sss = url.Substring(url.Length - 3, 3);
            sss = sss.ToLower();
            if (sss != "jpg" && sss != "jpeg" && sss != "gif")
            {
                sb.Append("<div style='background-color:#ccc;position:relative;clear:both;left: 0px; top: 0px; width:100px; height: 30px'><div>");
                sb.Append("<object id=\"WindowsMediaPlayer\" classid='CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6' height=" + height + " width=" + width + " >");
                sb.Append("<param NAME='AutoStart' VALUE='0'>");
                sb.Append("<param NAME='Balance' VALUE='0'>");
                sb.Append("<param name='enabled' value='-1'>");
                sb.Append("<param NAME='EnableContextMenu' VALUE='-1'>");
                sb.Append("<param NAME='url' value='" + url + "'>");
                sb.Append("<param NAME='PlayCount' VALUE='1'>");
                sb.Append("<param name='rate' value='1'>");
                sb.Append("<param name='currentPosition' value='0'>");
                sb.Append("<param name='currentMarker' value='0'>");
                sb.Append("<param name='defaultFrame' value=''>");
                sb.Append("<param name='invokeURLs' value='0'>");
                sb.Append("<param name='baseURL' value=''>");
                sb.Append("<param name='stretchToFit' value='0'>");
                sb.Append("<param name='volume' value='50'>");
                sb.Append("<param name='mute' value='0'>");
                sb.Append("<param name='uiMode' value='none'>");
                sb.Append("<param name='windowlessVideo' value='0'>");
                sb.Append("<param name='fullScreen' value='0'>");
                sb.Append("<param name='enableErrorDialogs' value='-1'>");
                sb.Append("<param name='SAMIStyle' value>");
                sb.Append("<param name='SAMILang' value>");
                sb.Append("<param name='SAMIFilename' value>");
                sb.Append("</object>");
                //sb.Append("</div><div style='float:left;height:1px;color:white;position: relative;z-index: 5000;left:"+(width/2-10)+"px;bottom:" + (height / 2 + 10) + "px;'>");
                sb.Append("</div>");

                sb.Append("<div id=btplay style='float:center;clear:both;color:white;position:absolute;left:0;bottom:0;'>");
                sb.Append("<img src='../../wetSiteHr/images/MediaPlay.gif' width=" + width + " height=" + height + " style='cursor:hand;' onclick='play()' />");
                sb.Append("</div>");

                if (url == "")
                {
                    sb.Append("<div style='float:center;clear:both;color:white;position:absolute;left:0px;bottom:0px'>");
                    sb.Append("<img src='../../wetSiteHr/images/novideo.jpg' width=" + width + " height=" + height + " />");
                    //sb.Append("无视频内容");
                    sb.Append("</div>");
                }
                sb.Append("</div>");
                sb.Append("<script>");
                sb.Append("function play()");
                sb.Append("{");
                sb.Append("btplay.style.display='none'; document.getElementById('WindowsMediaPlayer').controls.play(); ");
                sb.Append("}");
                sb.Append("</script>");
            }
            else
            {
                sb.Append("<img src='" + url + "' width=" + width + " height=" + height + " />");
            }
            return sb.ToString();
        }

        //连接
        public string ShowLinkFriendb(string fcol, string tablewidth, int col, int pheight, int pwidth, int top, string pcss)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select top " + top.ToString() + " n.FWebId,n.FId,n.FPicUrl,");
            sb.Append("c.FColNumber,c.FState,c.FPic,c.Fisdeleted,c.Forder from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            sb.Append("c.FColNumber='");
            sb.Append(fcol);
            sb.Append("' and c.fisdeleted=0 and c.fstate=1");
            sb.Append(" order by c.forder,c.fpubtime desc");

            DataTable dt = this.GetTable(sb.ToString());
            sb.Remove(0, sb.Length);
            if (dt == null || dt.Rows.Count <= 0)
            {
                sb.Append("<center>暂无内容</center>");
                return sb.ToString();
            }
            sb.Append("<table  height=10 align=center  cellpadding=0 cellspacing=0 border=0");
            if (tablewidth != "")
            {
                sb.Append(" width=" + tablewidth);
            }
            sb.Append(">");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (i % col == 0)
                {
                    sb.Append("<tr>");
                }

                sb.Append("<td align=center vAlign=middle width=" + (100 / col) + "%  height=" + (pheight + 8) + ">");
                sb.Append("<a class=text01 href='");
                string fwebid = dr["FWebId"].ToString();
                if (fwebid == "")
                    sb.Append("//");
                else
                    sb.Append(dt.Rows[i]["FWebId"].ToString());

                sb.Append("' target=_blank ><img src=");
                sb.Append(dt.Rows[i]["FPicUrl"].ToString());
                sb.Append(" border=0  height=" + pheight.ToString() + " width=" + pwidth.ToString());
                if (pcss != "")
                {
                    sb.Append(" class='" + pcss + "' ");
                }
                sb.Append("></a>");
                sb.Append("</td>");
                if ((i + 1) % col == 0) sb.Append("</tr>");
                if (i + 1 == dt.Rows.Count)
                {
                    for (int j = i % col; j < col; j++)
                    {
                        sb.Append("<td></td>");
                    }
                    sb.Append("</tr>");

                }
            }
            sb.Append("</table>");
            return sb.ToString();
        }





        public string ShowIndexNewsByCountAndTime(string colNumber, int iTop, int colCount, string tablewidth, string flag, int lheight, int sub, int isdate, string style, int isline, string url, string fwidth, string nwidth, string dwidth, int days)
        {
            StringBuilder sb = new StringBuilder();
            //flag显示的为行前的标志,例*或·  sub为取的标题长度  isdate为是否显示日期1为显示,0为不显示
            //string fclassid = "";
            //if (colNumber.IndexOf('%') > 0)
            //    fclassid = colNumber.Substring(0, colNumber.Length - 1);
            //else
            //    fclassid = colNumber;

            //sb.Remove(0, sb.Length);

            sb.Append("select top " + iTop.ToString() + " n.fid,n.fname,n.FOperType,n.FTypeId,n.ffilenote,n.fwebid,n.FType,n.FPubTime,");
            sb.Append(" c.FColNumber,c.FState,c.FColor,c.FPic ");
            sb.Append("from CF_News_Title n,CF_News_Col c ");
            sb.Append("where n.fid=c.fnewsid and ");
            if (colNumber.IndexOf('%') > 0)
            {
                sb.Append("c.FColNumber like '" + colNumber + "' ");
            }
            else
            {
                sb.Append(" c.FColNumber='" + colNumber + "'");
            }
            if (days != 0)
            {
                sb.Append(" and n.fpubtime<='" + DateTime.Now.ToString() + "'");
                sb.Append(" and n.fpubtime>='" + DateTime.Now.AddDays(-days) + "' ");
            }
            //string fid = getLMFid(fclassid1);
            //sb.Append("(c.FColNumber='" + fclassid1 + "'");
            //sb.Append(" or c.FColNumber in (select t.FNumber from CF_Sys_Tree t where t.FParentId='" + fid + "'))");
            sb.Append(" and c.fisdeleted=0 and c.fstate=1 ");
            sb.Append(" order by n.fcount desc,c.fpubtime desc");

            DataTable dt = this.GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Remove(0, sb.Length);

                sb.Append("<table cellSpacing='0' cellPadding='0' width='");
                if (tablewidth != "")
                    sb.Append(tablewidth);
                else
                    sb.Append("100%");
                sb.Append("'  align='center' border='0' >");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string fisdirect = dr["FOperType"].ToString();
                    string titlecolor = dr["FColor"].ToString();
                    string title = dr["fName"].ToString();
                    string FTypeId = dr["FTypeId"].ToString();

                    if (i % colCount == 0)
                    {
                        sb.Append("<tr height='" + lheight + "' ");
                        switch (isline)
                        {
                            case 1:
                                sb.Append(" class=dianline ");
                                break;

                        }
                        sb.Append(" >");
                    }

                    if (flag != "")
                    {
                        sb.Append("<td align='center' style='padding-left:2px;padding-right:5px;'");
                        if (fwidth != "0")
                        {
                            sb.Append(" width='" + fwidth + "' ");
                        }
                        sb.Append(">");
                        if (flag.Length > 1 && flag.Substring(0, 1) != "<")
                        {

                            sb.Append("<img align=baseline src='" + flag + Convert.ToString(i + 1) + ".gif' border=0 />&nbsp;");

                        }
                        else
                        {
                            sb.Append("<font class=text0 >" + flag + "</font>");
                        }
                        sb.Append("</td>");
                    }

                    sb.Append(" <td   align='left' ");
                    if (nwidth != "0")
                    {
                        sb.Append(" width='" + nwidth + "' ");
                    }
                    sb.Append(">");
                    if (Convert.ToInt32(FTypeId) < 3)
                    {

                        switch (fisdirect)
                        {
                            case "1":
                                sb.Append("<a href='" + url + "?fid=");
                                sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + dr["FColNumber"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            case "2":
                                sb.Append("<a href='" + dr["ffilenote"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            case "3":
                                sb.Append("<a href='" + dr["fwebid"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                            default:
                                sb.Append("<a href='" + url + "?fid=");
                                sb.Append(dt.Rows[i]["FId"].ToString() + "&type=" + dr["FColNumber"].ToString() + "' class='" + style + "' target=_blank title='" + title + "' >");
                                break;
                        }
                    }
                    else
                    {
                        sb.Append("<a class=" + style + " target=_blank href=../../webSiteDwon/main/ShowDetailInfo.aspx?fid=");
                        sb.Append(dr["FId"].ToString());
                        sb.Append("&type=" + dr["FColNumber"].ToString());
                        sb.Append(" >");
                    }
                    string opetype = dr["FType"].ToString();
                    int subb = sub;
                    subb -= 2;
                    string title1 = "";
                    switch (opetype)
                    {
                        case "1":
                            title1 = "<img border=0 src=../../webSiteNews/images/new.gif />";
                            break;
                        case "2":
                            title1 = "<img border=0 src=../../webSiteNews/images/hot.gif />";
                            break;
                        default:
                            subb += 2;
                            break;
                    }


                    title = this.getSubStr(title, sub);

                    title += title1;


                    if (titlecolor != "")
                    {
                        title = "<font color='" + titlecolor + "' >" + title + "</font>";
                    }

                    sb.Append(title);
                    sb.Append("</a>");

                    sb.Append("</td>");
                    string st = "";
                    switch (isdate)
                    {
                        case 1:
                            st = Convert.ToDateTime(dt.Rows[i]["FPubTime"].ToString()).ToString("MM-dd");
                            if (dwidth != "0")
                            {
                                sb.Append("<td width='" + dwidth + "' class='" + style + "'>" + st + "</td>");
                            }
                            else
                            {
                                sb.Append("<td width=30  class='" + style + "'>" + st + "</td>");
                            }
                            break;
                        case 2:
                            st = Convert.ToDateTime(dt.Rows[i]["FPubTime"].ToString()).ToString("yyy-MM-dd");
                            if (dwidth != "0")
                            {
                                sb.Append("<td width='" + dwidth + "' class='" + style + "'>[" + st + "]</td>");
                            }
                            else
                            {
                                sb.Append("<td width=80 class=text0>[" + st + "]</td>");
                            }
                            break;
                    }

                    if ((i + 1) % colCount == 0)
                    {
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");
            }
            else
            {
                sb.Remove(0, sb.Length);
                sb.Append("<center>暂无数据</center>");
            }
            return sb.ToString();

        }



        public string getLMDH(string FNewsNo)
        {
            EsTree eSt;
            string title = "";
            string pid = "";
            int flevel = 1;

            eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, "FParent,FName,FLevel,FWebUrl", " Fnumber ='" + FNewsNo + "' ");

            if (eSt != null)
            {
                pid = eSt.FParent;
                title = eSt.FName;
                flevel = eSt.FLevel;
            }
            eSt = null;
            if (flevel == 3)
            {
                eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, " FParent,FName,FLevel,FWebUrl ", " Fnumber ='" + pid + "'");

                if (eSt != null)
                {
                    pid = eSt.FParent;
                    title = eSt.FName + " > " + title;
                    flevel = eSt.FLevel;
                }
            }
            eSt = null;
            if (flevel == 2)
            {
                eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, " FParent,FName,FLevel,FWebUrl ", " Fnumber ='" + pid + "'");

                if (eSt != null)
                {
                    pid = eSt.FParent;
                    title = eSt.FName + " > " + title;
                    flevel = eSt.FLevel;
                }
            }


            return title;
        }



        public string getLMDH(string fColNo, string flag, int fLevelId)
        {
            EsTree eSt;
            string title = "";
            string pid = "";
            int flevel = 1;

            eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, "FParent,FName,FLevel,FWebUrl", " Fnumber ='" + fColNo + "'");

            if (eSt != null)
            {
                pid = eSt.FParent;
                title = eSt.FName;
                flevel = eSt.FLevel;
            }
            if (flevel == fLevelId)
            {
                return title;
            }
            eSt = null;
            if (flevel == 3)
            {
                eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, " FParent,FName,FLevel,FWebUrl ", " Fnumber ='" + pid + "'");

                if (eSt != null)
                {
                    pid = eSt.FParent;
                    //title =  "<a href='" + listPage + FNewsNo + "' class='" + linkcss + "'>" + eSt.FName + "</a>" +flag + title;


                    title = eSt.FName + flag + title;

                    flevel = eSt.FLevel;
                }

                //eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, " FParentId,FName,FLevel ", " fid ='" + pid + "'");

                //if (eSt != null)
                //{
                //    pid = eSt.FParentId;
                //    title = eSt.FName + flag + title;

                //}

            }
            if (flevel == fLevelId)
            {
                return title;
            }
            eSt = null;
            if (flevel == 2)
            {
                eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, " FParent,FName,FLevel,FWebUrl ", " Fnumber ='" + pid + "'");

                if (eSt != null)
                {
                    //pid = eSt.FParentId;
                    //title =   eSt.FName+flag + title;

                    title = eSt.FName + flag + title;


                }
            }


            //title = "当前位置:" + title;
            return title;
        }



        public string getLMDH(string FNewsNo, string flag, string linkcss)
        {
            EsTree eSt;
            string title = "";
            string pid = "";
            int flevel = 1;

            eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, "FParent,FName,FLevel,FWebUrl", " Fnumber ='" + FNewsNo + "'");

            if (eSt != null)
            {
                pid = eSt.FParent;
                //title = "<a href='"+listPage+FNewsNo+"' class='"+linkcss+"'>"+eSt.FName+"</a>";
                if (eSt.FWebUrl == "0") eSt.FWebUrl = "#";
                title = "<a href='" + eSt.FWebUrl + "' class='" + linkcss + "'>" + eSt.FName + "</a>";
                flevel = eSt.FLevel;
            }
            eSt = null;
            if (flevel == 3)
            {
                eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, " FParent,FName,FLevel,FWebUrl ", " Fnumber ='" + pid + "'");

                if (eSt != null)
                {
                    pid = eSt.FParent;
                    //title =  "<a href='" + listPage + FNewsNo + "' class='" + linkcss + "'>" + eSt.FName + "</a>" +flag + title;

                    if (eSt.FWebUrl == "0") eSt.FWebUrl = "#";
                    title = "<a href='" + eSt.FWebUrl + "' class='" + linkcss + "'>" + eSt.FName + "</a>" + flag + title;

                    flevel = eSt.FLevel;
                }

                //eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, " FParentId,FName,FLevel ", " fid ='" + pid + "'");

                //if (eSt != null)
                //{
                //    pid = eSt.FParentId;
                //    title = eSt.FName + flag + title;

                //}

            }
            eSt = null;
            if (flevel == 2)
            {
                eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, " FParent,FName,FLevel,FWebUrl ", " Fnumber ='" + pid + "'");

                if (eSt != null)
                {
                    //pid = eSt.FParentId;
                    //title =   eSt.FName+flag + title;
                    if (eSt.FWebUrl == "0") eSt.FWebUrl = "#";
                    title = "<a href='" + eSt.FWebUrl + "' class='" + linkcss + "'>" + eSt.FName + "</a>" + flag + title;


                }
            }


            //title = "当前位置:" + title;
            return title;
        }


        public string getLMDH(string FNewsNo, string flag, string linkcss, int fLevelId)
        {
            EsTree eSt;
            string title = "";
            string pid = "";
            int flevel = 1;

            eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, "FParent,FName,FLevel,FWebUrl", " Fnumber ='" + FNewsNo + "'");

            if (eSt != null)
            {
                pid = eSt.FParent;
                //title = "<a href='"+listPage+FNewsNo+"' class='"+linkcss+"'>"+eSt.FName+"</a>";
                if (eSt.FWebUrl == "0") eSt.FWebUrl = "#";
                title = "<a href='" + eSt.FWebUrl + "' class='" + linkcss + "'>" + eSt.FName + "</a>";
                flevel = eSt.FLevel;
            }
            if (flevel == fLevelId)
            {
                return title;
            }
            eSt = null;
            if (flevel == 3)
            {
                eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, " FParent,FName,FLevel,FWebUrl ", " Fnumber ='" + pid + "'");

                if (eSt != null)
                {
                    pid = eSt.FParent;
                    //title =  "<a href='" + listPage + FNewsNo + "' class='" + linkcss + "'>" + eSt.FName + "</a>" +flag + title;

                    if (eSt.FWebUrl == "0") eSt.FWebUrl = "#";
                    title = "<a href='" + eSt.FWebUrl + "' class='" + linkcss + "'>" + eSt.FName + "</a>" + flag + title;

                    flevel = eSt.FLevel;
                }

                //eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, " FParentId,FName,FLevel ", " fid ='" + pid + "'");

                //if (eSt != null)
                //{
                //    pid = eSt.FParentId;
                //    title = eSt.FName + flag + title;

                //}

            }
            if (flevel == fLevelId)
            {
                return title;
            }
            eSt = null;
            if (flevel == 2)
            {
                eSt = (EsTree)this.GetEBase(EntityTypeEnum.EsTree, " FParent,FName,FLevel,FWebUrl ", " Fnumber ='" + pid + "'");

                if (eSt != null)
                {
                    //pid = eSt.FParentId;
                    //title =   eSt.FName+flag + title;
                    if (eSt.FWebUrl == "0") eSt.FWebUrl = "#";
                    title = "<a href='" + eSt.FWebUrl + "' class='" + linkcss + "'>" + eSt.FName + "</a>" + flag + title;


                }
            }


            //title = "当前位置:" + title;
            return title;
        }


        public string ShowLeftMenu(string fParentCol)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select fname,fnumber,FWebListUrl ");
            sb.Append(" from cf_sys_tree where ");
            sb.Append(" fisdeleted=0 and FIsShow=0 ");
            sb.Append(" and FParent='" + fParentCol + "'");
            sb.Append(" order by forder,FCreateTime desc");
            DataTable dt = this.GetTable(sb.ToString());
            if (dt == null || dt.Rows.Count == 0)
            {
                return "暂无子栏目";
            }
            sb.Remove(0, sb.Length);
            int iCount = dt.Rows.Count;
            for (int i = 0; i < iCount; i++)
            {
                sb.Append("<div class='leftMenu" + (i + 1).ToString() + "'>");
                sb.Append("<a href='" + dt.Rows[i]["FWebListUrl"].ToString() + "' class='zwLInk7'>");
                sb.Append(dt.Rows[i]["FName"].ToString());
                sb.Append("</a>");
                sb.Append("</div>");
            }
            return sb.ToString();
        }

        #endregion

    }
}