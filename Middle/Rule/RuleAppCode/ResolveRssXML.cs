using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.ComponentModel;
using System.Collections;

namespace Approve.Common
{
    public class ResolveRssXML
    {
        /// <summary>
        /// 获取Head中的rss数据源
        /// </summary>
        /// <param name="url"></param>
        /// <returns>如果存在则返回rss文档的地址，否则返回""</returns>
        public virtual string GetHeadRssSource(string url)
        {
            Uri uri = getHttpURI(url);

            if (uri != null)
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri.AbsoluteUri);
                req.Method = "head";
                req.Timeout = 5000;
                try
                {
                    using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                    {
                        string rss = GetSingleTagValueByAttr(getHtmlSource(res), "link", "type", "application/rss+xml");
                        if (rss != "")
                        {
                            if (!rss.ToLower().StartsWith("http://"))
                            {
                                rss = "http://" + uri.Host + rss;
                                // rss = res.ResponseUri.ToString().Substring(0, res.ResponseUri.ToString().IndexOf(res.ResponseUri.PathAndQuery)) + rss;
                            }
                            return rss;
                        }
                    }
                }
                catch
                {
                }
            }
            return "";
        }
        /// <summary>
        /// 获取本地xml文档
        /// </summary>
        /// <param name="localhosturl">物理地址</param>
        /// <returns>根据物理地址返回一个xml文档，如果文档不存在，则返回null</returns>
        public virtual XElement GetXmlDocByLocalHost(string localhosturl)
        {
            try
            {
                XElement xmlDoc = XElement.Load(localhosturl);
                return xmlDoc;
            }
            catch
            {
            }
            return null;
        }
        /// <summary>
        /// 保存xml文档
        /// </summary>
        /// <param name="ele">要保存的xml文档</param>
        /// <param name="AbsolutePath">物理路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public virtual bool SaveXmlDoc(XElement ele, string AbsolutePath, string fileName)
        {
            if (ele != null)
            {
                try
                {
                    ele.Save(AbsolutePath + fileName, SaveOptions.DisableFormatting);
                    return true;
                }
                catch
                {
                }
            }
            return false;
        }

        /// <summary>
        /// 获取xml文档
        /// </summary>
        /// <param name="url"></param>
        /// <returns>根据url地址返回一个xml文档，如果文档不存在，则返回null</returns>
        public virtual XElement GetXmlDoc(string url)
        {
            Uri uri = getHttpURI(url);

            if (uri != null)
            {
                string extend = Path.GetExtension(uri.AbsoluteUri).ToLower();
                if (extend == ".xml")
                {
                    String userAgent = @"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                    try
                    {
                        HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(uri.AbsoluteUri);
                        myRequest.UserAgent = userAgent;
                        myRequest.Timeout = 5000;
                        HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();

                        Stream rssStream = myResponse.GetResponseStream();
                        XmlDocument rssDoc = new XmlDocument();

                        //rssDoc.Load(rssStream);
                        XmlReader reader = XmlReader.Create(rssStream);
                        XElement xmlDoc = null;

                        xmlDoc = XElement.Load(reader);
                        return xmlDoc;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        ///// <summary>
        ///// 解析rss文档
        ///// </summary>
        ///// <param name="xmlDoc"></param>
        ///// <returns></returns>
        //public virtual Channel ResolveRssXMLToEntity(XElement xmlDoc)
        //{
        //    if (xmlDoc != null)
        //    {
        //        XElement xml = (XElement)(from c in xmlDoc.Descendants("channel") select c).FirstOrDefault();
        //        if (xml != null)
        //        {
        //            Channel channel = new Channel();

        //            channel = ResolveXmlDoc(channel, xml);
        //            //channel = GetEntity(channel, xml);
        //            return channel;
        //        }
        //    }

        //    return null;
        //}

        //private Channel ResolveXmlDoc(Channel source, XElement xmlDoc)
        //{
        //    IEnumerable<XElement> Elements = xmlDoc.Elements();

        //    List<ChannelItem> items = new List<ChannelItem>();
        //    foreach (XElement ele in Elements)
        //    {
        //        if (ele.Name.ToString().ToLower().Trim() == "item")
        //        {
        //            ChannelItem item = new ChannelItem();
        //            item = getElements(item, ele);
        //            items.Add(item);
        //        }
        //        else
        //        {
        //            PropertyInfo propertity = source.GetType().GetProperty(ele.Name.ToString(), BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        //            if (propertity != null)
        //            {
        //                Type type = propertity.PropertyType;

        //                if (ele.Attributes().Count() > 0 || ele.Elements().Count() > 0)//如果该节点有多个属性或者多个元素
        //                {
        //                    object obj = Activator.CreateInstance(type);
        //                    if (ele.Attributes().Count() > 0)
        //                    {
        //                        obj = getAttributes(obj, ele);
        //                        propertity.SetValue(source, obj, null);
        //                    }
        //                    else
        //                    {
        //                        obj = getElements(obj, ele);
        //                        propertity.SetValue(source, obj, null);
        //                    }
        //                }
        //                else
        //                {

        //                    PropertyInfo proper = type.GetProperty("Text", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

        //                    if (proper == null)
        //                    {
        //                        propertity.SetValue(source, ele.Value, null);
        //                    }
        //                    else
        //                    {
        //                        object obj = Activator.CreateInstance(type);
        //                        if (obj != null)
        //                        {
        //                            proper.SetValue(obj, ele.Value, null);

        //                            propertity.SetValue(source, obj, null);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    source.Item = items;
        //    return source;
        //}

        private TSource getAttributes<TSource>(TSource source, XElement xmlDoc)
        {
            IEnumerable<XAttribute> Attributes = xmlDoc.Attributes();

            foreach (XAttribute attribute in Attributes)
            {
                PropertyInfo propertity = source.GetType().GetProperty(attribute.Name.ToString(), BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (propertity != null)
                {
                    Type type = propertity.PropertyType;

                    PropertyInfo proper = type.GetProperty("Text", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                    if (proper == null)
                    {
                        propertity.SetValue(source, attribute.Value, null);
                    }
                    else
                    {
                        object obj = Activator.CreateInstance(type);
                        if (obj != null)
                        {
                            proper.SetValue(obj, xmlDoc.Value, null);

                            propertity.SetValue(source, obj, null);
                        }
                    }
                }
            }
            return source;
        }

        private TSource getElements<TSource>(TSource source, XElement xmlDoc)
        {
            IEnumerable<XElement> Elements = xmlDoc.Elements();
            foreach (XElement element in Elements)
            {
                PropertyInfo propertity = source.GetType().GetProperty(element.Name.ToString(), BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (propertity != null)
                {
                    Type type = propertity.PropertyType;

                    if (element.Attributes().Count() > 0 || element.Elements().Count() > 0)//如果该节点有多个属性或者多个元素
                    {
                        object obj = Activator.CreateInstance(type);
                        if (element.Attributes().Count() > 0)
                        {
                            obj = getAttributes(obj, element);
                            propertity.SetValue(source, obj, null);
                        }
                        else
                        {
                            obj = getElements(obj, element);
                            propertity.SetValue(source, obj, null);
                        }
                    }
                    else
                    {
                        PropertyInfo proper = type.GetProperty("Text", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                        if (proper == null)
                        {
                            propertity.SetValue(source, element.Value, null);
                        }
                        else
                        {
                            object obj = Activator.CreateInstance(type);
                            if (obj != null)
                            {
                                proper.SetValue(obj, element.Value, null);

                                propertity.SetValue(source, obj, null);
                            }
                        }
                    }
                }
            }
            return source;
        }

        /// <summary>
        /// 获取网站中的rss源地址
        /// </summary>
        /// <param name="inputstring">网页的html源码</param>
        /// <param name="tagName">标签Name</param>
        /// <param name="attrname">属性Name</param>
        /// <param name="key">属性的值</param>
        /// <returns>如果存在则返回rss的源地址，如果不存在则返回""</returns>
        private string GetSingleTagValueByAttr(string inputstring, string tagName, string attrname, string key)
        {
            Regex reg = new Regex("<" + tagName + " [^<>]*>", RegexOptions.IgnoreCase);
            MatchCollection matchs = reg.Matches(inputstring);
            string result = string.Empty;
            foreach (Match match in matchs)
            {
                string matchValue = match.Value;
                Regex regValue = new Regex("href=\".*\"", RegexOptions.IgnoreCase);
                if (matchValue.ToLower().IndexOf(attrname.ToLower() + "=\"" + key.ToLower() + "\"") != -1)
                {
                    if (regValue.IsMatch(matchValue))
                    {
                        result = regValue.Match(matchValue).Value;
                        if (!string.IsNullOrEmpty(result))
                        {
                            result = result.ToLower().Replace("href=", "").Replace("\"", "");
                        }
                    }
                    return result;
                }
            }
            return "";
        }
        /// <summary>
        /// 获取HTML页面的源代码
        /// </summary>
        /// <param name="res"></param>
        /// <returns>返回html的源代码</returns>
        private string getHtmlSource(HttpWebResponse res)
        {
            WebClient _client = new WebClient();
            _client.BaseAddress = res.ResponseUri.ToString();
            _client.Headers.Add("Accept", "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*");
            _client.Headers.Add("Accept-Language", "zh-cn");
            _client.Headers.Add("UA-CPU", "x86");
            //_client.Headers.Add("Accept-Encoding","gzip, deflate");
            _client.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
            System.IO.Stream objStream = _client.OpenRead("/");
            System.IO.StreamReader _read = new System.IO.StreamReader(objStream, System.Text.Encoding.Default);
            return _read.ReadToEnd();
        }
        /// <summary>
        /// 获取Http网址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private Uri getHttpURI(string url)
        {
            if (!url.ToLower().StartsWith("http://"))
            {
                url = "http://" + url;
            }
            try
            {
                Uri uri = new Uri(url, UriKind.Absolute);
                return uri;
            }
            catch
            {
                return null;
            }
        }

        public string RSSSet(string mapPath, string fcol, string urlPath)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select t.fid,t.FName,t.FPubTime,t.FState,c.Forder,c.FColNumber,n.FContent from CF_News_Title t,CF_News_Col c,CF_News_Content n where c.FNewsId=t.Fid and n.FNewsId = t.Fid and t.FState =1 and c.FColNumber in (" + fcol + ") ");
            sb.Append(" order by c.Forder,t.fpubtime desc");
            Approve.RuleCenter.RCenter rc = new Approve.RuleCenter.RCenter();
            DataTable dt = rc.GetTable(sb.ToString());

            XmlDocument xml = new XmlDocument();
            String mapFileUrl = mapPath;

            XmlNode RootNode = xml.DocumentElement;
            //加入XML的声明段落
            XmlNode xmlnode = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(xmlnode);
            //加入一个根元素
            XmlElement rootelem = xml.CreateElement("", "rss", "");

            XmlAttribute attrib = xml.CreateAttribute("version");
            attrib.Value = "2.0";
            rootelem.Attributes.Append(attrib);

            attrib = xml.CreateAttribute("xmlns:content");
            attrib.Value = "http://purl.org/rss/1.0/modules/content/";
            rootelem.Attributes.Append(attrib);

            attrib = xml.CreateAttribute("xmlns:wfw");
            attrib.Value = "http://wellformedweb.org/CommentAPI/";
            rootelem.Attributes.Append(attrib);

            attrib = xml.CreateAttribute("xmlns:dc");
            attrib.Value = "http://purl.org/dc/elements/1.1/";
            rootelem.Attributes.Append(attrib);

            attrib = xml.CreateAttribute("xmlns:atom");
            attrib.Value = "http://www.w3.org/2005/Atom";
            rootelem.Attributes.Append(attrib);

            attrib = xml.CreateAttribute("xmlns:sy");
            attrib.Value = "http://purl.org/rss/1.0/modules/syndication/";
            rootelem.Attributes.Append(attrib);

            attrib = xml.CreateAttribute("xmlns:slash");
            attrib.Value = "http://purl.org/rss/1.0/modules/slash/";
            rootelem.Attributes.Append(attrib);

            XmlElement channelElem = xml.CreateElement("channel");
            //        <title>宕机检测多线多地区全智能DNS</title>
            //<atom:link href="http://blog.8gdns.com/index.php/feed/" rel="self" type="application/rss+xml" />
            //<link>http://blog.8gdns.com</link>
            //<description>8GDNS BLOG</description>
            //<lastBuildDate>Sun, 04 Jul 2010 06:48:25 +0000</lastBuildDate>

            //<generator>http://wordpress.org/?v=2.9.2</generator>
            //<language>en</language>
            //<sy:updatePeriod>hourly</sy:updatePeriod>
            //<sy:updateFrequency>1</sy:updateFrequency>
            //        <item>
            XmlElement channelSubElem = xml.CreateElement("title");
            channelSubElem.InnerText = "质量安全_" + rc.GetColName(dt.Rows[0]["FColNumber"].ToString());
            channelElem.AppendChild(channelSubElem);

            channelSubElem = xml.CreateElement("atom", "link", "http://www.w3.org/2005/Atom");
            attrib = xml.CreateAttribute("href");
            attrib.Value = "http://" + urlPath + "/rss.xml";
            channelSubElem.Attributes.Append(attrib);

            attrib = xml.CreateAttribute("rel");
            attrib.Value = "self";
            channelSubElem.Attributes.Append(attrib);

            attrib = xml.CreateAttribute("type");
            attrib.Value = "application/rss+xml";
            channelSubElem.Attributes.Append(attrib);
            channelElem.AppendChild(channelSubElem);

            channelSubElem = xml.CreateElement("link");
            channelSubElem.InnerText = "http://" + urlPath + "/";
            channelElem.AppendChild(channelSubElem);

            channelSubElem = xml.CreateElement("description");
            channelSubElem.InnerText = "";
            channelElem.AppendChild(channelSubElem);

            channelSubElem = xml.CreateElement("lastBuildDate");
            channelSubElem.InnerText = DateTime.Now.ToString();
            channelElem.AppendChild(channelSubElem);

            channelSubElem = xml.CreateElement("generator");
            channelSubElem.InnerText = "vs2008";
            channelElem.AppendChild(channelSubElem);

            channelSubElem = xml.CreateElement("language");
            channelSubElem.InnerText = "zh-CN";
            channelElem.AppendChild(channelSubElem);

            channelSubElem = xml.CreateElement("sy", "updatePeriod", "http://purl.org/rss/1.0/modules/syndication/");
            channelSubElem.InnerText = "hourly";
            channelElem.AppendChild(channelSubElem);

            channelSubElem = xml.CreateElement("sy", "updateFrequency", "http://purl.org/rss/1.0/modules/syndication/");
            channelSubElem.InnerText = "1";
            channelElem.AppendChild(channelSubElem);


            //            <item>
            //        <title>提供两组vip的安全域名ns记录</title>

            //        <link>http://blog.8gdns.com/index.php/2010/07/%e6%8f%90%e4%be%9b%e4%b8%a4%e7%bb%84vip%e7%9a%84%e5%ae%89%e5%85%a8%e5%9f%9f%e5%90%8dns%e8%ae%b0%e5%bd%95/</link>
            //        <comments>http://blog.8gdns.com/index.php/2010/07/%e6%8f%90%e4%be%9b%e4%b8%a4%e7%bb%84vip%e7%9a%84%e5%ae%89%e5%85%a8%e5%9f%9f%e5%90%8dns%e8%ae%b0%e5%bd%95/#comments</comments>
            //        <pubDate>Sun, 04 Jul 2010 06:48:25 +0000</pubDate>
            //        <dc:creator>admin</dc:creator>
            //                <category><![CDATA[Uncategorized]]></category>

            //        <guid isPermaLink="false">http://blog.8gdns.com/?p=57</guid>

            //        <description><![CDATA[ns1.8gdns.in ns2.8gdns.in
            //vip域名可以联系我申请使用
            //]]></description>
            //            <content:encoded><![CDATA[<p>ns1.8gdns.in ns2.8gdns.in</p>
            //<p>vip域名可以联系我申请使用</p>
            //]]></content:encoded>
            //            <wfw:commentRss>http://blog.8gdns.com/index.php/2010/07/%e6%8f%90%e4%be%9b%e4%b8%a4%e7%bb%84vip%e7%9a%84%e5%ae%89%e5%85%a8%e5%9f%9f%e5%90%8dns%e8%ae%b0%e5%bd%95/feed/</wfw:commentRss>
            //        <slash:comments>0</slash:comments>
            //        </item>






            //这里需要查询数据
            //BlogDB db = new BlogDB();
            //var result = "";
            //              join c in db.BlogClass on a.ClassId equals c.ClassId
            //              orderby a.UpdateTime descending
            //              select new { a.ArticleId, a.Title, a.PostTime, a.Remarks, a.Content, c.ClassName }).Take(20);


            foreach (DataRow item in dt.Rows)
            {
                string url = urlPath + "WebSite/main/ViewArticle.aspx?fid=" + item["fid"].ToString() + "&fcol=" + item["FColNumber"].ToString();
                channelSubElem = xml.CreateElement("item");
                XmlElement itemElem = xml.CreateElement("title");
                itemElem.InnerText = item["FName"].ToString();
                channelSubElem.AppendChild(itemElem);

                itemElem = xml.CreateElement("link");
                itemElem.InnerText = url;
                channelSubElem.AppendChild(itemElem);

                itemElem = xml.CreateElement("comments");
                itemElem.InnerText = "";
                channelSubElem.AppendChild(itemElem);

                itemElem = xml.CreateElement("pubDate");
                itemElem.InnerText = item["FPubTime"].ToString();
                channelSubElem.AppendChild(itemElem);

                itemElem = xml.CreateElement("dc", "creator", "http://purl.org/dc/elements/1.1/");
                itemElem.InnerText = "admin";
                channelSubElem.AppendChild(itemElem);

                itemElem = xml.CreateElement("category");
                itemElem.InnerText = rc.GetColName(item["FColNumber"].ToString());
                channelSubElem.AppendChild(itemElem);

                itemElem = xml.CreateElement("guid");
                itemElem.InnerText = url;
                attrib = xml.CreateAttribute("isPermaLink");
                attrib.Value = "false";
                itemElem.Attributes.Append(attrib);
                channelSubElem.AppendChild(itemElem);

                itemElem = xml.CreateElement("description");
                XmlCDataSection CDATA = xml.CreateCDataSection(rc.getSubStr(item["FContent"].ToString(), 2000));
                itemElem.AppendChild(CDATA);
                channelSubElem.AppendChild(itemElem);

                itemElem = xml.CreateElement("content", "encoded", "http://purl.org/rss/1.0/modules/content/");
                CDATA = xml.CreateCDataSection(item["FContent"].ToString());
                itemElem.AppendChild(CDATA);
                channelSubElem.AppendChild(itemElem);

                itemElem = xml.CreateElement("wfw", "commentRss", "http://wellformedweb.org/CommentAPI/");
                itemElem.InnerText = "";
                channelSubElem.AppendChild(itemElem);

                itemElem = xml.CreateElement("slash", "comments", "http://purl.org/rss/1.0/modules/slash/");
                itemElem.InnerText = "0";
                channelSubElem.AppendChild(itemElem);
                channelElem.AppendChild(channelSubElem);
            }

            rootelem.AppendChild(channelElem);
            xml.AppendChild(rootelem);
            try
            {
                xml.Save(mapFileUrl);
            }
            catch (Exception ex)
            {
                return "未发布成功，请查看路径是否正确！";
            }
            return "发布完成";
        }
    }
}
