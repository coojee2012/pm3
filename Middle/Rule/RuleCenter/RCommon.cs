using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.RuleBase;
using Approve.PersistBase;


namespace Approve.RuleCenter
{

    public class RCommon
    {
        RCenter rc = new RCenter();
    

        //提取企业资质信息
        public string GetEntQualiInfo(string fBaseInfoId)
        {
            if (fBaseInfoId == null || fBaseInfoId == "")
            {
                return "无资质";
            }

            StringBuilder sb = new StringBuilder();

            DataTable dt = new DataTable();

            int fint1 = 0;

            int fint2 = 0;

            string FRoleId = rc.GetSignValue(EntityTypeEnum.EsUser, "froleid", "fbaseinfoid='" + fBaseInfoId + "'");

            switch (FRoleId)
            {
                case "601":
                case "620":
                case "655":
                    sb.Append(" select count(1)  from  CF_Ent_QualiCertiTrade  where fbaseinfoid='" + fBaseInfoId + "'  and FIsBase=1 and fstate=1");
                    sb.Append(" union all ");
                    sb.Append(" select count(1)  from  CF_Ent_QualiCertiTrade  where fbaseinfoid='" + fBaseInfoId + "'  and fstate=1 ");

                    dt = rc.GetTable(sb.ToString());
                    fint1 = EConvert.ToInt(dt.Rows[0][0].ToString());
                    fint2 = EConvert.ToInt(dt.Rows[1][0].ToString());

                    if (fint1 > 0 && fint2 > 0)
                    {
                        return "有资质" + "<br>有主项资质";
                    }
                    if (fint1 > 0 && fint2 == 0)
                    {
                        return "有主项资质" + "<br>无其他资质";
                    }
                    if (fint1 == 0 && fint2 > 0)
                    {
                        return "有资质" + "<br>无主项资质";
                    }
                    if (fint1 == 0 && fint2 == 0)
                    {
                        return "无资质" + "<br>无主项资质";
                    }
                    break;

                case "610"://招标代理
                case "630"://房地产
                case "635"://园林绿化 
                case "645"://施工图审查企业 
                case "665"://施工图审查企业  
                case "650":
                    sb.Append(" select count(1)  from  CF_Ent_QualiCerti  where fbaseinfoid='" + fBaseInfoId + "'  and FIsValid=1");
                    dt = rc.GetTable(sb.ToString());
                    fint1 = EConvert.ToInt(dt.Rows[0][0].ToString());
                    if (fint1 > 0)
                    {
                        return "有资质";
                    }
                    if (fint1 == 0)
                    {
                        return "无资质";
                    }
                    break;
            }
            return "";
        }
        /// <summary>
        /// 根据开始时间和天数求出结束日期
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dayCount"></param>
        /// <returns></returns>
        public DateTime GetEndTime(DateTime dateStart, int dayCount)
        {
            RCenter rc = new RCenter();
            DateTime dateEnd = dateStart.AddDays(dayCount);//结束时间
            string sql = "select count(*) from CF_Sys_Holidays where FDate>='" + dateStart.ToShortDateString() + "' and  FDate<'" + dateEnd.ToShortDateString() + "'";
            dayCount = rc.GetSQLCount(sql);
            if (dayCount > 0)
            {
                return GetEndTime(dateEnd, dayCount);
            }
            else
            {
                return dateEnd;
            }
        }
    }
}