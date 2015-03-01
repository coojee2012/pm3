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
    

        //��ȡ��ҵ������Ϣ
        public string GetEntQualiInfo(string fBaseInfoId)
        {
            if (fBaseInfoId == null || fBaseInfoId == "")
            {
                return "������";
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
                        return "������" + "<br>����������";
                    }
                    if (fint1 > 0 && fint2 == 0)
                    {
                        return "����������" + "<br>����������";
                    }
                    if (fint1 == 0 && fint2 > 0)
                    {
                        return "������" + "<br>����������";
                    }
                    if (fint1 == 0 && fint2 == 0)
                    {
                        return "������" + "<br>����������";
                    }
                    break;

                case "610"://�б����
                case "630"://���ز�
                case "635"://԰���̻� 
                case "645"://ʩ��ͼ�����ҵ 
                case "665"://ʩ��ͼ�����ҵ  
                case "650":
                    sb.Append(" select count(1)  from  CF_Ent_QualiCerti  where fbaseinfoid='" + fBaseInfoId + "'  and FIsValid=1");
                    dt = rc.GetTable(sb.ToString());
                    fint1 = EConvert.ToInt(dt.Rows[0][0].ToString());
                    if (fint1 > 0)
                    {
                        return "������";
                    }
                    if (fint1 == 0)
                    {
                        return "������";
                    }
                    break;
            }
            return "";
        }
        /// <summary>
        /// ���ݿ�ʼʱ������������������
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dayCount"></param>
        /// <returns></returns>
        public DateTime GetEndTime(DateTime dateStart, int dayCount)
        {
            RCenter rc = new RCenter();
            DateTime dateEnd = dateStart.AddDays(dayCount);//����ʱ��
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