using System;
using System.Collections.Generic;
using System.Text;
using Approve.RuleBase;
using Approve.PersistEnterprise;
using System.Data;
using System.Web;
using Approve.EntityBase;
using System.Text.RegularExpressions;
using System.IO;

namespace Approve.RuleCenter
{


    public class OA : RBase
    {
        private PEnt m_pes;


        public OA()
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fNumber"></param>
        /// <returns></returns>
        public string GetParentTreeNumber(string fNumber)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select FParent from cf_sys_tree where fnumber='" + fNumber + "'");
            return this.GetSignValue(sb.ToString());

        }


        #region  部门维护功能

        public bool DelOrg(string fNumber)//删除部门和子部门操作
        {
            string fNumbers = "";
            GEtSubOrg(ref fNumbers, fNumber);
            if (fNumbers.Length > 0)
            {
                PExcute("delete from CF_OA_Organization where fnumber in(" + fNumbers + ");");
            }
            PExcute(" delete from CF_OA_Organization where fnumber='" + fNumber + "';");
            return true;
        }
        public void GEtSubOrg(ref string returnValue, string fNumber)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select fnumber from CF_OA_Organization where fparent='" + fNumber + "'");
            DataTable dt = this.GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (returnValue.Length == 0)
                    {
                        returnValue += "'" + dt.Rows[i][0].ToString() + "'";
                    }
                    else
                    {
                        returnValue += ",'" + dt.Rows[i][0].ToString() + "'";
                    }
                    GetSubMenu(ref returnValue, dt.Rows[i][0].ToString());
                }
            }
        }


        #endregion



        /// <summary>
        /// 从日期得到中文星期几
        /// </summary>
        /// <param name="dTime">日期</param>
        /// <returns></returns>
        public static string getWeekName(DateTime dTime)
        {
            string str = "";
            switch (dTime.DayOfWeek.ToString())
            {
                case "Sunday":
                    str = "星期日";
                    break;
                case "Monday":
                    str = "星期一";
                    break;
                case "Tuesday":
                    str = "星期二";
                    break;
                case "Wednesday":
                    str = "星期三";
                    break;
                case "Thursday":
                    str = "星期四";
                    break;
                case "Friday":
                    str = "星期五";
                    break;
                case "Saturday":
                    str = "星期六";
                    break;
            }

            return str;
        }


        /// <summary>
        /// 得到是该月的第几周格式为　yyyyMMw
        /// </summary>
        /// <param name="dTime">日期</param>
        /// <returns></returns>
        public static string getWeeks(DateTime dTime)
        {
            int week;
            int i = (int)dTime.Date.DayOfWeek;
            if (i == 0)
            {
                i = 7;
            }
            //如果 为上周的剩余天数
            if (dTime.Day <= 7 && dTime.Day < i)
            {
                int year = dTime.Year, month = dTime.Month;
                int beforeYear = 0;
                int beforeMouth = 0;
                if (month <= 1)//如果当前月是一月，那么年份就要减1
                {
                    beforeYear = year - 1;
                    beforeMouth = 12;//上个月
                }
                else
                {
                    beforeYear = year;
                    beforeMouth = month - 1;//上个月
                }
                dTime = EConvert.ToDateTime(beforeYear + "-" + beforeMouth + "-" + DateTime.DaysInMonth(year, beforeMouth));
            }

            //得到当月的第一个周一 
            DateTime firstMonday = EConvert.ToDateTime(dTime.Year + "-" + dTime.Month + "-01");
            if (firstMonday.DayOfWeek != DayOfWeek.Monday)
            {
                i = (int)firstMonday.Date.DayOfWeek;
                if (i == 0)
                {
                    i = 7;
                }
                firstMonday = firstMonday.AddDays(8 - i);
            }
            DateTime FirstofMonth;
            FirstofMonth = Convert.ToDateTime(dTime.Date.Year + "-" + dTime.Date.Month + "-" + 1);


            week = (dTime.Date.Day - firstMonday.Date.Day) / 7 + 1;

            return string.Format("{0:yyyy}", dTime) + string.Format("{0:MM}", dTime) + week.ToString();
        }


        /// <summary>
        /// 从指定时间得到所在周的周一的日期
        /// </summary>
        /// <param name="dTime"></param>
        /// <returns></returns>
        public static DateTime getWeekMonday(DateTime dTime)
        {
            int i = (int)dTime.Date.DayOfWeek;
            if (i == 0)
            {
                i = 7;
            }
            return dTime.AddDays(-(i - 1));
        }


        /// <summary>
        /// 从指定时间得到所在周的周日的日期
        /// </summary>
        /// <param name="dTime"></param>
        /// <returns></returns>
        public static DateTime getWeekSunday(DateTime dTime)
        {
            int i = (int)dTime.Date.DayOfWeek;
            if (i == 0)
            {
                i = 7;
            }
            return dTime.AddDays((7 - i));
        }


        /// <summary>
        /// 从指定时间得到所在月分的第一天日期
        /// </summary>
        /// <param name="dTime"></param>
        /// <returns></returns>
        public static DateTime getMonthFirstDay(DateTime dTime)
        {
            return EConvert.ToDateTime(dTime.ToString("yyyy-MM-") + "01");
        }

        /// <summary>
        /// 从指定时间得到所在月分的最后一天日期
        /// </summary>
        /// <param name="dTime"></param>
        /// <returns></returns>
        public static DateTime getMonthLastDay(DateTime dTime)
        {
            return EConvert.ToDateTime(dTime.ToString("yyyy-MM-") + DateTime.DaysInMonth(dTime.Year, dTime.Month));
        }

        /// <summary>
        /// 得到是该月的第几周
        /// </summary>
        /// <param name="dTime">日期</param>
        /// <returns></returns>
        public static string getWeeksName(DateTime dTime)
        {
            int week;
            int i = (int)dTime.Date.DayOfWeek;
            if (i == 0)
            {
                i = 7;
            }
            //如果 为上周的剩余天数
            if (dTime.Day <= 7 && dTime.Day < i)
            {
                int year = dTime.Year, month = dTime.Month;
                int beforeYear = 0;
                int beforeMouth = 0;
                if (month <= 1)//如果当前月是一月，那么年份就要减1
                {
                    beforeYear = year - 1;
                    beforeMouth = 12;//上个月
                }
                else
                {
                    beforeYear = year;
                    beforeMouth = month - 1;//上个月
                }
                dTime = EConvert.ToDateTime(beforeYear + "-" + beforeMouth + "-" + DateTime.DaysInMonth(year, beforeMouth));
            }

            //得到当月的第一个周一 
            DateTime firstMonday = EConvert.ToDateTime(dTime.Year + "-" + dTime.Month + "-01");
            if (firstMonday.DayOfWeek != DayOfWeek.Monday)
            {
                i = (int)firstMonday.Date.DayOfWeek;
                if (i == 0)
                {
                    i = 7;
                }
                firstMonday = firstMonday.AddDays(8 - i);
            }
            DateTime FirstofMonth;
            FirstofMonth = Convert.ToDateTime(dTime.Date.Year + "-" + dTime.Date.Month + "-" + 1);


            week = (dTime.Date.Day - firstMonday.Date.Day) / 7 + 1;
            string str = "";
            switch (week)
            {
                case 1:
                    str = "第一周";
                    break;
                case 2:
                    str = "第二周";
                    break;
                case 3:
                    str = "第三周";
                    break;
                case 4:
                    str = "第四周";
                    break;
                case 5:
                    str = "第五周";
                    break;
                case 6:
                    str = "第六周";
                    break;
            }
            return dTime.Year.ToString() + "年" + dTime.Month.ToString() + "月 " + str + " ";
        }

        public string getDayName(DateTime dTime)
        {
            string v = dTime.ToString("yyyy-MM-dd");
            System.TimeSpan t1 = DateTime.Now.Date - dTime.Date;
            if (t1.Days == 0)
            {
                v = "今天";
            }
            else if (t1.Days == 1)
            {
                v = "昨天";
            }
            else if (t1.Days == 2)
            {
                v = "前天";
            }

            return v;
        }


        /// <summary>
        /// 得到客户端IP
        /// </summary>
        /// <returns></returns>
        public static string getIpAddr()
        {
            string ip = HttpContext.Current.Request.Headers["x-forwarded-for"];
            if (string.IsNullOrEmpty(ip) || "unknown".Equals(ip, StringComparison.InvariantCultureIgnoreCase))
            {
                ip = HttpContext.Current.Request.Headers["Proxy-Client-IP"];
            }
            if (string.IsNullOrEmpty(ip) || "unknown".Equals(ip, StringComparison.InvariantCultureIgnoreCase))
            {
                ip = HttpContext.Current.Request.Headers["WL-Proxy-Client-IP"];
            }
            if (string.IsNullOrEmpty(ip) || "unknown".Equals(ip, StringComparison.InvariantCultureIgnoreCase))
            {
                ip = HttpContext.Current.Request.UserHostAddress;
            }
            return ip;
        }

        /// <summary>
        /// 正则替换掉发到邮件中内容的无用标记（按钮、链接）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string repl(string str)
        {    ///////去链接
            string pattern = "<a [^>]+>([^>]+)</a>";

            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
            str = reg.Replace(str, "${1}");
            ///////修改图片地址
            reg = new Regex("<(input) [^>]+>", RegexOptions.IgnoreCase);
            str = reg.Replace(str, "");

            str = str.Replace("<A style=\"FONT-SIZE: 12px\" href=\"#ttt\"><B>Top</B></A>", "");
            string VirtualPath = HttpRuntime.AppDomainAppVirtualPath;
            if (VirtualPath == "/")
            {
                VirtualPath = "";
            }
            string path = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + VirtualPath + "/";
            str = str.Replace(VirtualPath + "/upload/", path + "upload/");
            return str;
        }


        /// <summary>
        /// 得到网站地址
        /// </summary>
        /// <returns></returns>
        public string getHttp()
        {
            //整理为网格路径，以适应iss两种网站布署方式。
            string str = "";
            string first = "http://" + HttpContext.Current.Request.Headers["Host"];

            string root = HttpContext.Current.Request.ApplicationPath;
            if (root != "/")
                root += "/";
            str = first + root;
            return str;
        }

        #region 农历


        //天干
        private static string[] TianGan = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };

        //地支
        private static string[] DiZhi = { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };

        //十二生肖
        private static string[] ShengXiao = { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };

        //农历日期
        private static string[] DayName =   {"*","初一","初二","初三","初四","初五",
             "初六","初七","初八","初九","初十",
             "十一","十二","十三","十四","十五",
             "十六","十七","十八","十九","二十",
             "廿一","廿二","廿三","廿四","廿五",      
             "廿六","廿七","廿八","廿九","三十"};

        //农历月份
        private static string[] MonthName = { "*", "正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "腊" };

        //公历月计数天
        private static int[] MonthAdd = { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };

        //农历数据
        private static int[] LunarData =    {2635,333387,1701,1748,267701,694,2391,133423,1175,396438
            ,3402,3749,331177,1453,694,201326,2350,465197,3221,3402
            ,400202,2901,1386,267611,605,2349,137515,2709,464533,1738
            ,2901,330421,1242,2651,199255,1323,529706,3733,1706,398762
            ,2741,1206,267438,2647,1318,204070,3477,461653,1386,2413
            ,330077,1197,2637,268877,3365,531109,2900,2922,398042,2395
            ,1179,267415,2635,661067,1701,1748,398772,2742,2391,330031
            ,1175,1611,200010,3749,527717,1452,2742,332397,2350,3222
            ,268949,3402,3493,133973,1386,464219,605,2349,334123,2709
            ,2890,267946,2773,592565,1210,2651,395863,1323,2707,265877};
        /// <summary>
        /// 获取对应日期的农历
        /// </summary>
        /// <param name="dtDay">公历日期</param>
        /// <returns></returns>
        public static string GetLunarCalendar(DateTime dtDay)
        {
            string sYear = dtDay.Year.ToString();
            string sMonth = dtDay.Month.ToString();
            string sDay = dtDay.Day.ToString();
            int year;
            int month;
            int day;
            try
            {
                year = int.Parse(sYear);
                month = int.Parse(sMonth);
                day = int.Parse(sDay);
            }
            catch
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
                day = DateTime.Now.Day;
            }

            int nTheDate;
            int nIsEnd;
            int k, m, n, nBit, i;
            string calendar = string.Empty;
            //计算到初始时间1921年2月8日的天数：1921-2-8(正月初一)
            nTheDate = (year - 1921) * 365 + (year - 1921) / 4 + day + MonthAdd[month - 1] - 38;
            if ((year % 4 == 0) && (month > 2))
                nTheDate += 1;
            //计算天干，地支，月，日
            nIsEnd = 0;
            m = 0;
            k = 0;
            n = 0;
            while (nIsEnd != 1)
            {
                if (LunarData[m] < 4095)
                    k = 11;
                else
                    k = 12;
                n = k;
                while (n >= 0)
                {
                    //获取LunarData[m]的第n个二进制位的值
                    nBit = LunarData[m];
                    for (i = 1; i < n + 1; i++)
                        nBit = nBit / 2;
                    nBit = nBit % 2;
                    if (nTheDate <= (29 + nBit))
                    {
                        nIsEnd = 1;
                        break;
                    }
                    nTheDate = nTheDate - 29 - nBit;
                    n = n - 1;
                }
                if (nIsEnd == 1)
                    break;
                m = m + 1;
            }
            year = 1921 + m;
            month = k - n + 1;
            day = nTheDate;
            // return year + "-" + month + "-" + day;
            #region 格式化日期显示为三月廿四
            if (k == 12)
            {
                if (month == LunarData[m] / 65536 + 1)
                    month = 1 - month;
                else if (month > LunarData[m] / 65536 + 1)
                    month = month - 1;
            }

            //生肖
            calendar = ShengXiao[(year - 4) % 60 % 12].ToString() + "年 ";
            //天干
            calendar += TianGan[(year - 4) % 60 % 10].ToString();
            //地支
            calendar += DiZhi[(year - 4) % 60 % 12].ToString() + " ";

            //农历月
            if (month < 1)
                calendar += "闰" + MonthName[-1 * month].ToString() + "月";
            else
                calendar += MonthName[month].ToString() + "月";

            //农历日
            calendar += DayName[day].ToString() + "日";

            return calendar;

            #endregion
        }

        /// <summary>
        /// 获取对应日期的农历
        /// </summary>
        /// <param name="dtDay">公历日期</param>
        /// <returns></returns>
        public static string GetLunarCalendar(DateTime dtDay, string type)
        {
            string sYear = dtDay.Year.ToString();
            string sMonth = dtDay.Month.ToString();
            string sDay = dtDay.Day.ToString();
            int year;
            int month;
            int day;
            try
            {
                year = int.Parse(sYear);
                month = int.Parse(sMonth);
                day = int.Parse(sDay);
            }
            catch
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
                day = DateTime.Now.Day;
            }

            int nTheDate;
            int nIsEnd;
            int k, m, n, nBit, i;
            string calendar = string.Empty;
            //计算到初始时间1921年2月8日的天数：1921-2-8(正月初一)
            nTheDate = (year - 1921) * 365 + (year - 1921) / 4 + day + MonthAdd[month - 1] - 38;
            if ((year % 4 == 0) && (month > 2))
                nTheDate += 1;
            //计算天干，地支，月，日
            nIsEnd = 0;
            m = 0;
            k = 0;
            n = 0;
            while (nIsEnd != 1)
            {
                if (LunarData[m] < 4095)
                    k = 11;
                else
                    k = 12;
                n = k;
                while (n >= 0)
                {
                    //获取LunarData[m]的第n个二进制位的值
                    nBit = LunarData[m];
                    for (i = 1; i < n + 1; i++)
                        nBit = nBit / 2;
                    nBit = nBit % 2;
                    if (nTheDate <= (29 + nBit))
                    {
                        nIsEnd = 1;
                        break;
                    }
                    nTheDate = nTheDate - 29 - nBit;
                    n = n - 1;
                }
                if (nIsEnd == 1)
                    break;
                m = m + 1;
            }
            year = 1921 + m;
            month = k - n + 1;
            day = nTheDate;
            // return year + "-" + month + "-" + day;
            #region 格式化日期显示为三月廿四
            if (k == 12)
            {
                if (month == LunarData[m] / 65536 + 1)
                    month = 1 - month;
                else if (month > LunarData[m] / 65536 + 1)
                    month = month - 1;
            }

            switch (type)
            {
                case "sx":
                    //生肖
                    calendar = ShengXiao[(year - 4) % 60 % 12].ToString() + "年 ";
                    break;
                case "tg":
                    //天干
                    calendar += TianGan[(year - 4) % 60 % 10].ToString();
                    break;
                case "dz":
                    //地支
                    calendar += DiZhi[(year - 4) % 60 % 12].ToString() + " ";
                    break;
                case "month":
                    //农历月
                    if (month < 1)
                        calendar += "闰" + MonthName[-1 * month].ToString() + "月";
                    else
                        calendar += MonthName[month].ToString() + "月";
                    break;
                case "day":
                    //农历日
                    if (DayName[day].ToString() == "初一")
                    {
                        if (month < 1)
                            calendar += "闰" + MonthName[-1 * month].ToString() + "月";
                        else
                            calendar += MonthName[month].ToString() + "月";
                    }
                    else
                        calendar += DayName[day].ToString();

                    break;
            }

            return calendar;

            #endregion
        }

        #endregion


        public static string getOAGWurl(string serverPath)
        {
            string str = "";
            string first = "http://" + HttpContext.Current.Request.Headers["Host"];

            string root = HttpContext.Current.Request.ApplicationPath;
            if (root != "/")
                root += "/";
            str = first + root;

            if (File.Exists(HttpContext.Current.Server.MapPath(serverPath)))
            {
                str += serverPath.Replace("~/", "");
            }
            return str;
        }
    }

}
