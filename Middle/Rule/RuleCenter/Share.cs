using System;
using System.Collections.Generic;
using System.Text;
using Approve.RuleBase;
using Approve.PersistEnterprise;
using System.Data;
using System.Collections;
using Approve.EntityBase;
using System.Data.SqlClient;

namespace Approve.RuleCenter
{
    public class Share : RBase
    {
        private PEnt m_pes;

        public Share()
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
        /// 获得管区划
        /// </summary>
        /// <param name="ID">FNumber或FID</param>
        /// <param name="type">
        /// 1：通过FNumber得到FName
        /// 2：通过FID得到FName
        /// 3：通过FNumber得到FFullName
        /// 4：通过FID得到FFullName
        /// 5：通过FNumber得到FID
        /// </param>
        /// <returns></returns>
        public string getDept(string ID, int type)
        {
            string str = "";
            StringBuilder sb = new StringBuilder();
            switch (type)
            {
                case 1:
                    sb.Append("select FName from CF_Sys_ManageDept where fnumber='" + ID + "'");
                    break;
                case 2:
                    sb.Append("select FName from CF_Sys_ManageDept where FID='" + ID + "'");
                    break;
                case 3:
                    sb.Append("select FFullName from CF_Sys_ManageDept where fnumber='" + ID + "'");
                    break;
                case 4:
                    sb.Append("select FFullName from CF_Sys_ManageDept where FID='" + ID + "'");
                    break;
                case 5:
                    sb.Append("select FID from CF_Sys_ManageDept where fnumber='" + ID + "'");
                    break;
                default:
                    sb.Append("select FName from CF_Sys_ManageDept where fnumber='" + ID + "'");
                    break;
            }
            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                str = this.GetSignValue(sb.ToString());
            }
            return str;
        }

        /// <summary>
        /// 得到系统类型名称
        /// </summary>
        /// <param name="number">CF_Sys_System.FNumber</param>
        /// <returns></returns>
        public string getSystemName(string number)
        {
            string str = "";
            if (!string.IsNullOrEmpty(number))
            {
                string sysName = GetSignValue("select FName from CF_Sys_SystemName where fnumber ='" + number + "'");
                if (!string.IsNullOrEmpty(sysName))
                    str = sysName;
            }
            return str;
        }

        /// <summary>
        /// 得到系统密钥
        /// </summary>
        /// <param name="number">CF_Sys_System.FNumber</param>
        /// <returns></returns>
        public string getSystemLoginURL(string number)
        {
            string str = "";
            if (!string.IsNullOrEmpty(number))
            {
                string FLUrl = GetSignValue("select FLUrl from CF_Sys_SystemName where fnumber ='" + number + "'");
                if (!string.IsNullOrEmpty(FLUrl))
                    str = FLUrl;
            }
            return str;
        }
        /// <summary>
        /// 得到系统密钥
        /// </summary>
        /// <param name="number">CF_Sys_System.FNumber</param>
        /// <returns></returns>
        public string getSystemKey(string number)
        {
            string str = "";
            if (!string.IsNullOrEmpty(number))
            {
                string FKey = GetSignValue("select FShareKey from CF_Sys_SystemName where fnumber ='" + number + "'");
                if (!string.IsNullOrEmpty(FKey))
                    str = FKey;
            }
            return str;
        }


        /// <summary>
        /// 得到指定批次类型的表
        /// </summary>
        /// <param name="number">批次类型ID（CF_Sys_BatchNo.FTypeId）</param>
        /// <returns></returns>
        public DataTable getBatchTable(string number)
        {
            DataTable dt = null;
            if (!string.IsNullOrEmpty(number))
            {
                dt = GetTable("select FID,FName from CF_Sys_BatchNo where FTypeId ='" + number + "'");
            }
            return dt;
        }
        /// <summary>
        /// 得到批次名称
        /// </summary>
        /// <param name="number">批次类型ID（CF_Sys_BatchNo.FTypeId）</param>
        /// <returns></returns>
        public string getBatchName(string FID)
        {
            string str = "";
            if (!string.IsNullOrEmpty(FID))
            {
                str = GetSignValue("select FName from CF_Sys_BatchNo where FID ='" + FID + "'");
            }
            return str;
        }

        /// <summary>
        /// 得到系统用户所能管理的企业系统类型
        /// </summary>
        /// <param name="UserId">用户user.FID</param>
        /// <returns></returns>
        public DataTable getSystemEntTable(string UserId)
        {
            DataTable dt = new DataTable();
            DataTable userDt = GetTable(EntityTypeEnum.EsUser, "FType,FRoleId", "FID=@FID", new SqlParameter("@FID", UserId));
            if (userDt != null && userDt.Rows.Count > 0)
            {
                string RoleId = "";
                if (userDt.Rows[0]["FType"].ToString() == "6")//系统管理员（管理用户的）
                {
                    RoleId = userDt.Rows[0]["FRoleId"].ToString();
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("select FName,FNumber from CF_Sys_System ");
                sb.Append("where fisdeleted=0 and ftype=1 ");//ftype=1：企业系统类型
                if (userDt.Rows[0]["FType"].ToString() == "6")
                {
                    if (!string.IsNullOrEmpty(RoleId))
                        sb.Append("and FNumber in (" + RoleId + ") ");
                    else
                        sb.Append("and 1=2 ");
                }
                sb.Append("order by FOrder,FNumber ");
                dt = GetTable(sb.ToString());

            }

            return dt;
        }

        /// <summary>
        /// 得到系统用户所能管理的企业系统类型
        /// </summary>
        /// <param name="UserId">用户user.FID</param>
        /// <returns></returns>
        public string getSystemEntList(string UserId)
        {
            string str = "";
            DataTable dt = new DataTable();
            DataTable userDt = GetTable(EntityTypeEnum.EsUser, "FType,FRoleId", "FID=@FID", new SqlParameter("@FID", UserId));
            if (userDt != null && userDt.Rows.Count > 0)
            {
                string RoleId = "";
                if (userDt.Rows[0]["FType"].ToString() == "6")//系统管理员（管理用户的）
                {
                    RoleId = userDt.Rows[0]["FRoleId"].ToString();
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("select FName,FNumber from CF_Sys_System ");
                sb.Append("where fisdeleted=0 and ftype=1 ");//ftype=1：企业系统类型
                if (userDt.Rows[0]["FType"].ToString() == "6")
                {
                    if (!string.IsNullOrEmpty(RoleId))
                        sb.Append("and FNumber in (" + RoleId + ") ");
                    else
                        sb.Append("and 1=2 ");
                }
                sb.Append("order by FOrder,FNumber ");
                dt = GetTable(sb.ToString());

            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(str))
                    str += ",";
                str += dt.Rows[i]["FNumber"].ToString();
            }

            return str;
        }

        /// <summary>
        /// 根据当前引擎信息解密字符串
        /// </summary>
        /// <param name="Src">解密的字符串</param>
        /// <returns>返回解密后字符串</returns>
        public static string UncrypString(string Src)
        {
            try
            {
                string Key = "JKCFLOW30";
                int KeyLen, KeyPos, offset, SrcPos, SrcAsc, TmpSrcAsc;
                string dest;
                if ((Src == null) || (Src.Length == 0))
                {
                    return "";
                }
                KeyLen = Key.Length;
                if (KeyLen == 0)
                    Key = "JsDataConvert";
                KeyPos = 0;
                offset = int.Parse(Src.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                SrcPos = 2;
                dest = "";
                while (SrcPos < Src.Length - 1)
                {
                    SrcAsc = int.Parse(Src.Substring(SrcPos, 2), System.Globalization.NumberStyles.HexNumber);
                    TmpSrcAsc = SrcAsc ^ (int)(Key[KeyPos]);
                    if (TmpSrcAsc <= offset)
                        TmpSrcAsc = 255 + TmpSrcAsc - offset;
                    else
                        TmpSrcAsc = TmpSrcAsc - offset;
                    dest = dest + (char)TmpSrcAsc;
                    offset = SrcAsc;
                    SrcPos = SrcPos + 2;
                    if (KeyPos < KeyLen - 1)
                        KeyPos++;
                    else
                        KeyPos = 0;
                }
                return dest;
            }
            catch (Exception ex){
            
            }
            return "";
        }
    }
     
}
