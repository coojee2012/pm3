using System;
using System.Web.UI;
using System.Data;
using Approve.EntityBase;
namespace Approve.Design.applyPrint
{
    /// <summary>
    /// BasePrint 的摘要说明。
    /// </summary>
    public class BasePrint
    {
        public BasePrint()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool isDate(string str)
        {
            try
            {
                DateTime dt = DateTime.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void witePageHead(HtmlTextWriter writer)
        {
            writer.Write("<HTML>");
            writer.Write("<HEAD>");
            writer.Write("	<title>baseInfo_detail</title>");
            writer.Write("	<meta content='Microsoft Visual Studio .NET 7.1' name='GENERATOR'>");
            writer.Write("	<meta content='C#' name='CODE_LANGUAGE'>");
            writer.Write("	<meta content='JavaScript' name='vs_defaultClientScript'>");
            writer.Write("	<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>");
            writer.Write("	<!--media=print 这个属性可以在打印时有效-->");
            writer.Write("	<style media='print'>.Noprint { DISPLAY: none }");
            writer.Write("	.PageNext { PAGE-BREAK-AFTER: always }");
            writer.Write("	</style>");
            writer.Write("	<LINK href='../../style/print.css' type='text/css' rel='stylesheet'>");
            writer.Write("	<script language='javascript' src='../script/print.js'></script>");
            writer.Write("	</HEAD>");
            writer.Write("<body onload='SetPrintSettings()'>");
            writer.Write("	<div class='Noprint' style='BACKGROUND-COLOR: #ccccff' align='right'>");
            writer.Write("	   <OBJECT id='WebBrowser' height='0' width='0' classid='CLSID:8856F961-340A-11D0-A96B-00C04FD705A2'");
            writer.Write("	   VIEWASTEXT></OBJECT>");

            writer.Write("	   <OBJECT id='factory' style='DISPLAY: none' codeBase='../script/smsx.cab#Version=6,3,434,26'");
            writer.Write("	   classid='clsid:1663ed61-23eb-11d2-b92f-008048fdd814' viewastext></OBJECT>");
            writer.Write("	   <font size=2>如果打印不了，请下载打印控件，或者把浏览器安全性设置为低。</font>");
            writer.Write("	  <input class='ent_defaultButton' onclick='document.all.WebBrowser.ExecWB(8,1)' type='button'");
            writer.Write("	  value='页面设置'> <input class='ent_defaultButton' onclick='document.all.WebBrowser.ExecWB(7,1)' type='button'");
            writer.Write("	  value='打印预览'> <input class='ent_defaultButton' onclick='document.all.WebBrowser.ExecWB(6,1)' type='button'");
            writer.Write("	  value='打印'>");
            writer.Write("	  </div>");
        }
        public void witePageHead(HtmlTextWriter writer, bool Judge)
        {
            writer.Write("<HTML>");
            writer.Write("<HEAD>");
            writer.Write("	<title>baseInfo_detail</title>");
            writer.Write("	<meta content='Microsoft Visual Studio .NET 7.1' name='GENERATOR'>");
            writer.Write("	<meta content='C#' name='CODE_LANGUAGE'>");
            writer.Write("	<meta content='JavaScript' name='vs_defaultClientScript'>");
            writer.Write("	<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>");
            writer.Write("	<!--media=print 这个属性可以在打印时有效-->");
            writer.Write("	<style media='print'>.Noprint { DISPLAY: none }");
            writer.Write("	.PageNext { PAGE-BREAK-AFTER: always }");
            writer.Write("	</style>");
            writer.Write("	<LINK href='../../style/print.css' type='text/css' rel='stylesheet'>");
            writer.Write("	<script language='javascript' src='../script/print.js'></script>");
            writer.Write("	</HEAD>");
            if (Judge)
                writer.Write("<body onload='SetPrintSettings()'>");
            else
                writer.Write("<body onload='SetPrintSettingsWidth()'>");
            writer.Write("	<div class='Noprint' style='BACKGROUND-COLOR: #ccccff' align='right'>");
            writer.Write("	   <OBJECT id='WebBrowser' height='0' width='0' classid='CLSID:8856F961-340A-11D0-A96B-00C04FD705A2'");
            writer.Write("	   VIEWASTEXT></OBJECT>");

            writer.Write("	   <OBJECT id='factory' style='DISPLAY: none' codeBase='../script/smsx.cab#Version=6,3,434,26'");
            writer.Write("	   classid='clsid:1663ed61-23eb-11d2-b92f-008048fdd814' viewastext></OBJECT>");

            writer.Write("	  <input class='ent_defaultButton' onclick='document.all.WebBrowser.ExecWB(8,1)' type='button'");
            writer.Write("	  value='页面设置'> <input class='ent_defaultButton' onclick='document.all.WebBrowser.ExecWB(7,1)' type='button'");
            writer.Write("	  value='打印预览'> <input class='ent_defaultButton' onclick='document.all.WebBrowser.ExecWB(6,1)' type='button'");
            writer.Write("	  value='打印'>");
            writer.Write("	  </div>");
        }
        public void witePageEnd(HtmlTextWriter writer)
        {
            writer.Write("</body>");
            writer.Write("</HTML>");
        }
        //数据初始化，传入一个以|为分割符的字符串
        public void IniData(ref string[] ArrayData, string Source)
        {
            if (Source.Length > 0)
            {
                ArrayData = Source.Split('|');
                for (int ii = 0; ii < ArrayData.Length; ii++)
                {
                    string Item = "";
                    if (ArrayData[ii] == null)
                        ArrayData.SetValue(Item, ii);
                }
            }
            else
            {
                for (int i = 0; i < ArrayData.Length; i++)
                {
                    ArrayData.SetValue("", i);
                }
            }
        }
        //传入一条数据，当Judge非真时初始化字附串数组
        public void IniData(ref string[] ArrayData, DataRow dr, int RowId)
        {
            int ICount = dr.Table.Columns.Count;
            ICount = ICount + 1;
            for (int i = 0; i < ICount; i++)
            {
                if (i == 0)
                {
                    ArrayData.SetValue(RowId.ToString(), 0);
                }
                else
                {
                    string Item = "";
                    int ii = i - 1;
                    if (dr[ii] != System.DBNull.Value && dr[ii].ToString() != "")
                        Item = dr[ii].ToString().Trim();
                    ArrayData.SetValue(Item, i);
                }
            }
        }
        //表头操作
        public void witeTableNull(HtmlTextWriter writer)
        {
            //
            writer.Write("<TABLE id='TPrint'  cellSpacing='0' borderColorDark='#ffffff' cellPadding='0'");
            writer.Write("width='98%' align='center' borderColorLight='#000000' border='0'  style='border:0px;'  bgcolor=white >");
        }
        public void witeTableNulltemp(HtmlTextWriter writer)
        {
            //
            writer.Write("<TABLE id='TPrint'  cellSpacing='0' borderColorDark='#ffffff' cellPadding='0'");
            writer.Write("width='98%' align='center' borderColorLight='#000000' border='0'  bgcolor=white  style='border:0px;'>");
        }
        //写表名
        public void witeTableName(HtmlTextWriter writer, string sName, int colcount)
        {
            writer.Write("<TR  height='50'>");
            writer.Write("<TD colspan='" + colcount.ToString() + "' align='center' class='ttitle' style='border-top:0px;border-left:0px;border-right:0px'>" + sName + "</TD>");
            writer.Write("</TR>");
        }
        public void witeTableNametemp(HtmlTextWriter writer, string sName, int colcount)
        {
            writer.Write("<TR  height='50'>");
            writer.Write("<TD colspan='" + colcount.ToString() + "' align='center' style='border-top:0px;border-left:0px;border-right:0px' class='ttitle'>" + sName + "</TD>");
            // writer.Write(" style='border-top:0px;border-left:0px;border-right:0px'");
            writer.Write("</TR>");
        }


        //写表第一个标题行
        public void witeTitleRow(HtmlTextWriter writer, string[] ArryStr, params object[] obj)
        {
            int width = 50;
            if (obj != null && obj.Length > 0)
                width = EConvert.ToInt(obj[0]);
            string s = string.Empty;
            if (width > 0)
                s = "width='" + width + "'";
            writer.Write("<TR  height='50'>");
            int len = ArryStr.Length;
            for (int i = 0; i < len; i++)
            {
                if (i == 0)
                    writer.Write("<TD align='center'  class='head' " + s + " nowrap >" + ArryStr[i].ToString() + "</TD>");
                else
                    writer.Write("<TD align='center'  class='head' nowrap>" + ArryStr[i].ToString() + "</TD>");
            }
            writer.Write("</TR>");
        }

        //写表第一个标题行
        public void witeTitleRow(HtmlTextWriter writer, string[] ArryStr)
        {
            writer.Write("<TR  height='50'>");
            int len = ArryStr.Length;
            for (int i = 0; i < len; i++)
            {
                if (i == 0)
                    writer.Write("<TD align='center'  class='head' nowrap  width='50'>" + ArryStr[i].ToString() + "</TD>");
                else
                    writer.Write("<TD align='center'  class='head' nowrap>" + ArryStr[i].ToString() + "</TD>");
            }
            writer.Write("</TR>");
        }
        //写表第一个标题行,没有序号列的
        public void witeTitleRowNOnum(HtmlTextWriter writer, string[] ArryStr)
        {
            writer.Write("<TR  height='50'>");
            int len = ArryStr.Length;
            for (int i = 0; i < len; i++)
            {
                writer.Write("<TD align='center'  class='head' nowrap>" + ArryStr[i].ToString() + "</TD>");
            }
            writer.Write("</TR>");
        }
        //写表第一个标题行
        public void witeTitleRowBold(HtmlTextWriter writer, string[] ArryStr)
        {
            writer.Write("<TR  height='50' style='font-weight:bold;'>");
            int len = ArryStr.Length;
            for (int i = 0; i < len; i++)
            {
                if (i == 0)
                    writer.Write("<TD align='center'  class='head' nowrap  width='50'>" + ArryStr[i].ToString() + "</TD>");
                else
                    writer.Write("<TD align='center'  class='head' nowrap>" + ArryStr[i].ToString() + "</TD>");
            }
            writer.Write("</TR>");
        }

        public void witeTitleRowtemp(HtmlTextWriter writer, string[] ArryStr)
        {
            writer.Write("<TR  height='80'>");
            int len = ArryStr.Length;
            for (int i = 0; i < len; i++)
            {
                if (i == 0)
                    writer.Write("<TD align='center'  class='head' nowrap  width='50'>" + ArryStr[i].ToString() + "</TD>");
                else
                    writer.Write("<TD align='center'  class='head' nowrap>" + ArryStr[i].ToString() + "</TD>");
            }
            writer.Write("</TR>");
        }




        public void witeTitleRowZJZX(HtmlTextWriter writer, string[] ArryStr)
        {
            writer.Write("<TR  height='40'>");
            writer.Write("<td width='6%' rowspan='2' align='center'  class='head' nowrap>" + "序号" + "</td>");
            writer.Write("<td width='10%' rowspan='2' align='center'  class='head' nowrap>" + "姓名" + "</td>");
            writer.Write("<td width='6%' rowspan='2' align='center'  class='head' nowrap>" + "性别" + "</td>");
            writer.Write("<td width='18%' rowspan='2' align='center'  class='head' nowrap>" + "证件号码" + "</td>");
            writer.Write("<td width='13%' rowspan='2' align='center'  class='head' nowrap>" + "档案存档号" + "</td>");
            writer.Write("<td width='18%' rowspan='2' align='center'  class='head' nowrap>" + "人事档案存档单位" + "</td>");
            writer.Write("<td width='19%' colspan='2' align='center'  class='head' nowrap>" + "存档单位" + "</td>");
            writer.Write("<td width='10%' rowspan='2' align='center'  class='head' nowrap>" + "备注" + "</td>");
            writer.Write("</TR>");
            writer.Write("<TR  height='40'>");
            writer.Write("<td width='10%' align='center'  class='head' nowrap>" + "联系电话" + "</td>");
            writer.Write("<td  align='center'  class='head' nowrap>" + "联系人" + "</td>");
            writer.Write("</TR>");

        }

        public void witeTitleRowZJZX2(HtmlTextWriter writer, string[] ArryStr)
        {
            writer.Write("<TR  height='40'>");
            writer.Write("<td width='6%' rowspan='2' align='center'  class='head' nowrap>" + "序号" + "</td>");
            writer.Write("<td width='10%' rowspan='2' align='center'  class='head' nowrap>" + "姓名" + "</td>");
            writer.Write("<td width='6%' rowspan='2' align='center'  class='head' nowrap>" + "性别" + "</td>");
            writer.Write("<td width='18%' rowspan='2' align='center'  class='head' nowrap>" + "证件号码" + "</td>");
            writer.Write("<td width='13%' rowspan='2' align='center'  class='head' nowrap>" + "缴费代码" + "</td>");
            writer.Write("<td width='18%' rowspan='2' align='center'  class='head' nowrap>" + "缴费单位" + "</td>");
            writer.Write("<td width='19%' colspan='2' align='center'  class='head' nowrap>" + "缴费单位" + "</td>");
            writer.Write("<td width='10%' rowspan='2' align='center'  class='head' nowrap>" + "备注" + "</td>");
            writer.Write("</TR>");
            writer.Write("<TR  height='40'>");
            writer.Write("<td width='10%' align='center'  class='head' nowrap>" + "联系人" + "</td>");
            writer.Write("<td  align='center'  class='head' nowrap>" + "联系电话" + "</td>");
            writer.Write("</TR>");

        }



        public void witeTitleRow(HtmlTextWriter writer, string htmlCode)
        {
            writer.Write(htmlCode);
        }
        //添加一个新行
        public void witeTableRowData(HtmlTextWriter writer, string[] ArryStr)
        {
            writer.Write("<TR  height='40'>");
            int len = ArryStr.Length;
            for (int i = 0; i < len; i++)
            {

                writer.Write("<TD align='center'>" + ArryStr[i].ToString() + "</TD>");

            }
            writer.Write("</TR>");
        }



        public void witeTableRowData(HtmlTextWriter writer, string[] ArryStr, string RowHeight)
        {
            if (RowHeight == "") RowHeight = "40";
            int Rheiht = Convert.ToInt32(RowHeight);
            writer.Write("<TR  height='" + Rheiht + "'>");
            int len = ArryStr.Length;
            for (int i = 0; i < len; i++)
            {
                if (i == 0 || i == 2)
                {
                    writer.Write("<TD align='center'>" + ArryStr[i].ToString() + "</TD>");
                }
                else
                {

                    writer.Write("<TD align='center'>" + ArryStr[i].ToString() + "</TD>");
                }
            }
            writer.Write("</TR>");
        }

        public void witeTableRowDataWithCss(HtmlTextWriter writer, string[] ArryStr, string trHeight, string className)
        {
            if (trHeight == "") trHeight = "40";
            writer.Write("<TR  height='" + trHeight + "'>");
            int len = ArryStr.Length;
            for (int i = 0; i < len; i++)
            {
                if (i == 0 || i == 2)
                {
                    writer.Write("<TD align='center'  class='" + className + "'>" + ArryStr[i].ToString() + "</TD>");
                }
                else
                {

                    writer.Write("<TD align='center' class='" + className + "'>" + ArryStr[i].ToString() + "</TD>");
                }
            }
            writer.Write("</TR>");
        }

        public void witeTableRowDataWithCss(HtmlTextWriter writer, string[] ArryStr, string trHeight, string className, params object[] obj)
        {
            if (trHeight == "") trHeight = "40";
            writer.Write("<TR  height='" + trHeight + "'>");
            int rowSpan = 0;
            int rowSpan1 = 0;
            if (obj != null)
                if (obj.Length > 0)
                    rowSpan = EConvert.ToInt(obj[0]);
            if (obj.Length > 1)
                rowSpan1 = EConvert.ToInt(obj[1]);
            int len = ArryStr.Length;

            for (int i = 0; i < len; i++)
            {
                if (i == 0 && rowSpan != 0)
                {
                    if (rowSpan > 0)
                        writer.Write("<TD align='center' rowspan='" + rowSpan + "'  class='" + className + "'>" + ArryStr[i].ToString() + "</TD>");
                }
                else if (i == 1 && rowSpan1 != 0)
                {
                    if (rowSpan1 > 0)
                        writer.Write("<TD align='center' rowspan='" + rowSpan1 + "'  class='" + className + "'>" + ArryStr[i].ToString() + "</TD>");
                }
                else if (i == 0 || i == 2)
                {
                    writer.Write("<TD align='center'  class='" + className + "'>" + ArryStr[i].ToString() + "</TD>");
                }
                else
                {

                    writer.Write("<TD align='center' class='" + className + "'>" + ArryStr[i].ToString() + "</TD>");
                }
            }
            writer.Write("</TR>");
        }

        //添加一个特殊的新行
        public void witeTableRowData(HtmlTextWriter writer, string[] ArryStr, int rowNumber)
        {
            writer.Write("<TR  height='40'>");
            int len = ArryStr.Length;
            for (int i = 0; i < len; i++)
            {
                if (i == rowNumber)
                {
                    writer.Write("<TD align='center' class='listspecial'>" + ArryStr[i].ToString() + "</TD>");
                }
                else
                {
                    writer.Write("<TD align='center'>" + ArryStr[i].ToString() + "</TD>");
                }
            }
            writer.Write("</TR>");
        }
        //添加一个特殊的新行
        public void witeTableRowData(HtmlTextWriter writer, string[] ArryStr, int rowNumber, int colSpan, int rowNumber2)
        {
            writer.Write("<TR  height='40'>");
            //第一列
            writer.Write("<TD align='center'>" + ArryStr[0].ToString() + "</TD>");
            //要显示的列
            writer.Write("<TD align='center' class='listspecial' colspan='" + colSpan + "'>" + ArryStr[rowNumber].ToString() + "</TD>");
            //显示最后一列
            writer.Write("<TD align='center'>" + ArryStr[rowNumber2].ToString() + "</TD>");
            writer.Write("</TR>");
        }

        //添加一个特殊的新行,勘察设计业绩
        public void witeTableRowData(HtmlTextWriter writer, string[] ArryStr, int rowNumber, int colSpan, int rowNumber2, string type)
        {
            if (type == "yj")
            {
                writer.Write("<TR  height='40'>");
                //第一列
                writer.Write("<TD align='center'>" + ArryStr[0].ToString() + "</TD>");
                //要显示的列
                writer.Write("<TD align='center' class='listspecial' colspan='" + colSpan + "'>" + ArryStr[rowNumber].ToString() + "</TD>");

                //要显示的列
                writer.Write("<TD align='center' ' colspan='2'>次行业的项目总数为：</TD>");

                //显示最后一列
                writer.Write("<TD align='center'>" + ArryStr[rowNumber2].ToString() + "</TD>");
                writer.Write("</TR>");
            }
        }

        //添加一个换页说明
        public void witeNewPage(HtmlTextWriter writer, string desc)
        {
            writer.Write("<div style=\"page-break-before: always;\"><!--[if IE 7]><br style=\"height:0; line-height:0\"><![endif]--></div> ");
        }





        //添加表结尾
        public void witeTableEnd(HtmlTextWriter writer)
        {
            writer.Write("</table>");
        }

        //添加表结尾
        public void witeTableEndWithDesc(HtmlTextWriter writer, int colSpan, string sDesc, string sCss)
        {
            writer.Write("<tr><td colspan='" + colSpan + "' class='" + sCss + "'>");
            writer.Write(sDesc);
            writer.Write("</td></tr>");
            writer.Write("</table>");
        }
        //
        public void witeNullRow(HtmlTextWriter writer, int ColumnNumb)
        {
            writer.Write("<TR  height='40'>");
            for (int i = 0; i < ColumnNumb; i++)
            {
                writer.Write("<TD align='center' class='noborderReport' width='25%'></TD>");
            }
            writer.Write("</TR>");
        }

        //添加表结尾
        public void witeTableEnd(HtmlTextWriter writer, int colCount, string fDesc)
        {
            writer.Write("<tr height='30px;'><td colspan='" + colCount + "' style='border:none 0px;'>");
            writer.Write(fDesc);
            writer.Write("</td></tr>");
            writer.Write("</table>");
        }

    }
}
