using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ProjectBLL
{
    /// <summary>
    /// SaveAsBase 的摘要说明。
    /// </summary>
    public class SaveAsBase
    {
        public SaveAsBase()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public void SaveAsExc(string HtmlCode, string Title, bool isOutTitle, System.Web.HttpResponse Response)
        {
            string sFileName = Title;

            Response.Clear();
            Response.Buffer = true;
            //			Response.Charset="GB2312";  //设置了类型为中文防止乱码的出现 
            Response.Charset = "utf-8";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(sFileName, System.Text.Encoding.UTF8) + ".xls"); //定义输出文件和文件名 
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文 
            Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。  

            //System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            //System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            ////System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            //oHtmlTextWriter.Write("<div align=center height=25 valign=middle><font size='6'>" + sHeadTittle + "</font></div><br>");
            //DataGrid DG_List = new DataGrid();
            //DG_List.DataSource = dt;
            //DG_List.DataBind();
            //DG_List.ShowHeader = false;
            //DG_List.RenderControl(oHtmlTextWriter);
            if (isOutTitle)
            {
                HtmlCode = HtmlCode.Insert(0, "<div align=center height=25 valign=middle><font size='6'>" + Title + "</font></div><br>");
            }
            Response.Write(HtmlCode);
            //oHtmlTextWriter.Close();
            //oStringWriter.Close();

            Response.End();
            //DG_List.Dispose();
        }

        public void SaveAsExc(DataTable dt, string Title, System.Web.HttpResponse Response)
        {
            string sFileName = Title;
            string sHeadTittle = Title;
            Response.Clear();
            Response.Buffer = true;
            //			Response.Charset="GB2312";  //设置了类型为中文防止乱码的出现 
            Response.Charset = "utf-8";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(sFileName, System.Text.Encoding.UTF8) + ".xls"); //定义输出文件和文件名 
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文 
            Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。  

            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            oHtmlTextWriter.Write("<div align=center height=25 valign=middle><font size='6'>" + sHeadTittle + "</font></div><br>");
            DataGrid DG_List = new DataGrid();
            DG_List.DataSource = dt;
            DG_List.DataBind();
            DG_List.ShowHeader = true;
            DG_List.RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            oHtmlTextWriter.Close();
            oStringWriter.Close();

            Response.End();
            DG_List.Dispose();
        }

        public void SaveAsExc(string HtmlCode, string Title, System.Web.HttpResponse Response)
        {
            string sFileName = Title;
            string sHeadTittle = Title;
            Response.Clear();
            Response.Buffer = true;
            //			Response.Charset="GB2312";  //设置了类型为中文防止乱码的出现 
            Response.Charset = "utf-8";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(sFileName, System.Text.Encoding.UTF8) + ".xls"); //定义输出文件和文件名 
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文 
            Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。  

            //System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            //System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            ////System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            //oHtmlTextWriter.Write("<div align=center height=25 valign=middle><font size='6'>" + sHeadTittle + "</font></div><br>");
            //DataGrid DG_List = new DataGrid();
            //DG_List.DataSource = dt;
            //DG_List.DataBind();
            //DG_List.ShowHeader = false;
            //DG_List.RenderControl(oHtmlTextWriter);
            HtmlCode = HtmlCode.Insert(0, "<div align=center height=25 valign=middle><font size='6'>" + sHeadTittle + "</font></div><br>");
            Response.Write(HtmlCode);
            //oHtmlTextWriter.Close();
            //oStringWriter.Close();

            Response.End();
            //DG_List.Dispose();
        }
        //jia
        public void SaveAsExc(System.Web.UI.Control[] control, string title, System.Web.HttpResponse Response)
        {

            string sHeadTittle = title;
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";  //设置了类型为中文防止乱码的出现   
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(title, System.Text.Encoding.UTF8) + ".xls"); //定义输出文件和文件名 
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文 
            Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。  

            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            oHtmlTextWriter.Write("<div align=center height=25 valign=middle><font size='6'>" + sHeadTittle + "</font></div><br>");
            //			DataGrid DG_List = new DataGrid();
            //			DG_List.DataSource = dt;
            //			DG_List.DataBind();
            for (int i = 0; i < control.Length; i++)
            {
                control[i].RenderControl(oHtmlTextWriter);

            }

            //oStringWriter.Write(oHtmlTextWriter.ToString());
            Response.Write(oStringWriter.ToString());
            oHtmlTextWriter.Close();
            oStringWriter.Close();

            //				DG_List.Dispose();
            //				D_list=null;
            //			this.dataBind();
            Response.End();
        }
        public void SaveAsExc(DataGrid DG_List, string Title, System.Web.HttpResponse Response)
        {
            string sFileName = Title;
            string sHeadTittle = Title;
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";  //设置了类型为中文防止乱码的出现   
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(sFileName, System.Text.Encoding.UTF8) + ".xls"); //定义输出文件和文件名 
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文 
            Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。  

            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            oHtmlTextWriter.Write("<div align=center height=25 valign=middle><font size='6'>" + sHeadTittle + "</font></div><br>");
            //			DataGrid DG_List = new DataGrid();
            //			DG_List.DataSource = dt;
            //			DG_List.DataBind();
            DG_List.RenderControl(oHtmlTextWriter);
            //oStringWriter.Write(oHtmlTextWriter.ToString());
            Response.Write(oStringWriter.ToString());
            oHtmlTextWriter.Close();
            oStringWriter.Close();

            //				DG_List.Dispose();
            //				D_list=null;
            //			this.dataBind();
            Response.End();
        }
        public void SaveAsExc(DataGrid DG_List, string Title, System.Web.HttpResponse Response, string tablehtml)
        {
            string sFileName = Title;
            if (Title.Length >= 17)
            {
                sFileName = Title.Substring(0, 17);
            }
            string sHeadTittle = Title;
            //			DG_List.ShowHeader = false;
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";  //设置了类型为中文防止乱码的出现  
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(sFileName, System.Text.Encoding.UTF8) + ".xls"); //定义输出文件和文件名 
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文 
            Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。  

            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            oHtmlTextWriter.Write("<div align=center height=25 valign=middle><font size='6'>" + sHeadTittle + "</font></div><br>");
            oHtmlTextWriter.Write(tablehtml);
            DG_List.RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            oHtmlTextWriter.Close();
            oStringWriter.Close();
            Response.End();
        }

        public void SaveAsExc(System.Web.UI.WebControls.GridView DG_List, string Title, System.Web.HttpResponse Response, string tablehtml)
        {
            string sFileName = Title;
            if (Title.Length > 17)
                sFileName = Title.Substring(0, 17);

            string sHeadTittle = Title;
            //			DG_List.ShowHeader = false;
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";  //设置了类型为中文防止乱码的出现  
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(sFileName, System.Text.Encoding.UTF8) + ".xls"); //定义输出文件和文件名 
            Response.ContentEncoding = System.Text.Encoding.Default;//设置输出流为简体中文 
            Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。  

            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            oHtmlTextWriter.Write("<div align=center height=25 valign=middle><font size='6'>" + sHeadTittle + "</font></div><br>");
            oHtmlTextWriter.Write(tablehtml);

            DG_List.RenderControl(oHtmlTextWriter);
            Response.Write(Regex.Replace(Regex.Replace(oStringWriter.ToString(), "<a.*?>", ""), "</a>", ""));
            oHtmlTextWriter.Close();
            oStringWriter.Close();
            Response.End();
        }

        public void SaveAsExc(string Title, System.Web.HttpResponse Response, string tablehtml)
        {
            string sFileName = "";
            if (Title.Length >= 17)
            {
                sFileName = Title.Substring(0, 17);
            }
            else
            {
                sFileName = Title;
            }
            string sHeadTittle = Title;
            //			DG_List.ShowHeader = false;
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";  //设置了类型为中文防止乱码的出现  
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(sFileName, System.Text.Encoding.UTF8) + ".xls"); //定义输出文件和文件名 
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文 
            Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。  

            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            oHtmlTextWriter.Write("<div align=center height=25 valign=middle><font size='6'>" + sHeadTittle + "</font></div><br>");
            oHtmlTextWriter.Write(tablehtml);
            Response.Write(Regex.Replace(Regex.Replace(oStringWriter.ToString(), "<a.*?>", ""), "</a>", ""));
            oHtmlTextWriter.Close();
            oStringWriter.Close();

            Response.End();
            Response.Close();
        }


        //jia
        public void SaveAsDoc(System.Web.UI.Control control, string title, System.Web.HttpResponse Response)
        {

            string sHeadTittle = title;
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";  //设置了类型为中文防止乱码的出现   
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(title, System.Text.Encoding.UTF8) + ".xls"); //定义输出文件和文件名 
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文 
            Response.ContentType = "application/ms-word";//设置输出文件类型为excel文件。  

            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            oHtmlTextWriter.Write("<div align=center height=25 valign=middle><font size='6'>" + sHeadTittle + "</font></div><br>");
            //			DataGrid DG_List = new DataGrid();
            //			DG_List.DataSource = dt;
            //			DG_List.DataBind();
            //for (int i = 0; i < control.Length; i++)
            //{
            control.RenderControl(oHtmlTextWriter);

            //}

            //oStringWriter.Write(oHtmlTextWriter.ToString());
            Response.Write(oStringWriter.ToString());
            oHtmlTextWriter.Close();
            oStringWriter.Close();

            //				DG_List.Dispose();
            //				D_list=null;
            //			this.dataBind();
            Response.End();
        }

        public string Print(DataTable ds, DataGrid dg)
        {
            if (ds.Rows.Count > 0)//判断是否有数据
            {
                if (dg.HasControls())//判断dg是否包含有控件
                {
                    DataGrid dg1 = new DataGrid();//创建新的datagrid
                    dg1 = dg;
                    dg1.AllowPaging = false;
                    dg1.PageSize = ds.Rows.Count; //去掉分页
                    StringBuilder htm1 = new StringBuilder();
                    StringBuilder htm2 = new StringBuilder();
                    StringBuilder sb = new StringBuilder();
                    //创建打印页面
                    htm1.Append("<html><head><meta. name=vs_targetSchema content=\"http://schemas.microsoft.com/intellisense/ie5\"><title>打印</title>");
                    htm1.Append("<meta. http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"><!--media=print 这个属性可以在打印时有效--><style.media=print>.Noprint{display:none;}");


                    htm1.Append(".PageNext{page-break-after: always;}</style><style>.tdp{    border-bottom: 1 solid #000000;    border-left:  1 solid #000000;    border-right:  0 solid #ffffff;");


                    htm1.Append("border-top: 0 solid #ffffff;}.tabp{    border-color: #000000 #000000 #000000 #000000;    border-style. solid;    border-top-width: 2px;border-right-width: 2px;    border-bottom-width: 1px;");


                    htm1.Append("  border-left-width: 1px;}.NOPRINT {font-family: \"宋体\";font-size: 9pt;}</style></head><body ><center class=\"Noprint\" >  <p>  <OBJECT  id=WebBrowser  classid=CLSID:8856F961-340A-11D0-A96B-00C04FD705A2  height=0  width=0>");


                    htm1.Append(" </OBJECT>  <input  type=button  value=打印          onclick=document.all.WebBrowser.ExecWB(6,1)>  <input  type=button  value=直接打印  onclick=document.all.WebBrowser.ExecWB(6,6)>");


                    htm1.Append("<input  type=button  value=页面设置  onclick=document.all.WebBrowser.ExecWB(8,1)></p>  <p>    <input  type=button  value=打印预览  onclick=document.all.WebBrowser.ExecWB(7,1)>");


                    htm1.Append(" <br/>    </p>  <hr align=\"center\" width=\"90%\" size=\"1\" noshade></center><table width=\"90%\" border=\"1\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"  class=\"tabp\"><tr>");


                    System.IO.StringWriter tw = new StringWriter();//创建一个字符串输出流
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);//输出html文本的输出流
                    dg1.RenderControl(hw);//将dg1的所有控件信息写入输出流，并保存在tw中
                    htm2.Append("</tr></table></body></html>");
                    return (htm1.ToString() + tw.ToString() + htm2.ToString() + "<center><a href=javascript.:history.back(-1)>关闭 </a></center>");
                }
            }
            return ("<script. language = javascript>alert('表中没有数据!')</script>");
        }







        public string GetFnumber(string fnumber)
        {
            string Fnumber = "";
            fnumber = fnumber.Substring(3, fnumber.Length - 3);
            int index = fnumber.IndexOf('.');
            if (index > -1)
            {
                fnumber = fnumber.Substring(0, index) + fnumber.Substring((index + 1), (fnumber.Length - index - 1));
                Fnumber = fnumber;
            }
            else
                Fnumber = fnumber;
            return Fnumber;
        }
        public bool PrintSaveAs()
        {
            return true;
        }
    }
}