using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.VisualBasic;
using ProjectData;
using System.Data;
using System.Diagnostics;
using Tools;
using System.Collections;
namespace ProjectBLL
{
    public class RBase
    {

        /// <summary>
        /// 取业务类型名
        /// </summary>
        /// <param name="fNumber">字典编码</param>
        /// <returns></returns>
        public static string GetAppTypeName(object fNumber)
        {
            if (Information.IsNumeric(fNumber))
            {
                ProjectDB db = new ProjectDB();
                int IntFNumber = EConvert.ToInt(fNumber);
                string FName = db.CF_Sys_ManageType.Where(t => t.FNumber == IntFNumber).Select(t => t.FName).FirstOrDefault();
                return FName;
            }
            else
            {
                Debug.WriteLine("传入的编码不是一个数字");
            }
            return "";



        }



        /// <summary>
        /// 取菜单名
        /// </summary>
        /// <param name="fNumber">菜单编码</param>
        /// <returns></returns>
        public static string GetMenuName(object fNumber)
        {
            ProjectDB db = new ProjectDB();
            string IntFNumber = EConvert.ToString(fNumber);
            string FName = db.Menu.Where(t => t.FNumber == IntFNumber).Select(t => t.FName).FirstOrDefault();
            return FName;
        }
        /// <summary>
        /// 取字典名称
        /// </summary>
        /// <param name="fNumber">字典编码</param>
        /// <returns></returns>
        public static string GetDicName(object fNumber)
        {
            if (Information.IsNumeric(fNumber))
            {
                ProjectDB projectDB = new ProjectDB();
                int IntFNumber = EConvert.ToInt(fNumber);
                string FName = projectDB.Dic.Where(dic => dic.FNumber == IntFNumber).Select(dic => dic.FName).FirstOrDefault();
                return FName;
            }
            else
            {
                Debug.WriteLine("传入的字典编码不是一个数字");
            }
            return "";



        }


        /// <summary>
        /// 取字典名称
        /// </summary>
        /// <param name="fNumber">字典编码</param>
        /// <returns></returns>
        public static string GetDicNameFromC(object fCNumber)
        {
            if (Information.IsNumeric(fCNumber))
            {
                ProjectDB projectDB = new ProjectDB();
                string strFCNumber = EConvert.ToString(fCNumber);
                string FName = projectDB.Dic.Where(dic => dic.FCNumber == strFCNumber).Select(dic => dic.FName).FirstOrDefault();
                return FName;
            }
            else
            {
                Debug.WriteLine("传入的字典编码不是一个数字");
            }
            return "";



        }
        /// <summary>
        /// 取字典名称
        /// </summary>
        /// <param name="fParentId">父级编码</param>
        /// <param name="fNumber">字典编码</param>
        /// <returns></returns>
        public static string GetDicName(object fParentId, object fNumber)
        {
            if (Information.IsNumeric(fNumber) && Information.IsNumeric(fParentId))
            {
                int IntfParentId = EConvert.ToInt(fParentId);
                int IntFNumber = EConvert.ToInt(fNumber);
                ProjectDB projectDB = new ProjectDB();
                string FName = projectDB.Dic.Where(dic => dic.FNumber == IntFNumber && dic.FParentId == IntfParentId).Select(dic => dic.FName).FirstOrDefault();
                return FName;
            }
            return "";
        }

        /// <summary>
        /// 从CF_Sys_QualiLevel得到等级名称
        /// </summary>
        /// <param name="fNumber">CF_Sys_QualiLevel.FNumber</param>
        /// <returns></returns>
        public static string GetLevelName(object fNumber)
        {
            string FContent = "";
            int n = EConvert.ToInt(fNumber);
            ProjectDB db = new ProjectDB();

            FContent = db.CF_Sys_QualiLevel.Where(t => t.FNumber == n).Select(t => t.FName).FirstOrDefault();

            return FContent;
        }

        /// <summary>
        /// 通过变量编码从库中取出变量值
        /// </summary>
        /// <param name="fNumber">系统变更名</param>
        /// <returns></returns>
        public static string GetSysObjectName(object fNumber)
        {
            string FContent = "";
            string str = EConvert.ToString(fNumber);
            ProjectDB db = new ProjectDB();

            FContent = db.Object.Where(t => t.FNumber == str).Select(t => t.FContent).FirstOrDefault();

            return FContent;
        }

        /// <summary>
        /// 通过变量编码从库中取出变量值
        /// </summary>
        /// <param name="fNumber">系统变更名</param>
        /// <returns></returns>
        public static string getAppFZTBUrl(object fNumber)
        {
            int n = EConvert.ToInt(fNumber);
            ProjectDB db = new ProjectDB();

            return db.CF_Sys_SystemName.Where(t => t.FNumber == n).Select(t => t.FZTBUrl).FirstOrDefault();
        }

        public static string GetDeptField(string FId, int ptype)
        {
            string sReturn = "";
            ProjectDB db = new ProjectDB();
            switch (ptype)
            {
                case 1:
                    sReturn = db.ManageDept.Where(t => t.FID == FId).Select(t => t.FName).FirstOrDefault();
                    break;
                case 2:
                    sReturn = db.ManageDept.Where(t => t.FNumber.ToString() == FId).Select(t => t.FName).FirstOrDefault();
                    break;
                case 3:
                    sReturn = db.ManageDept.Where(t => t.FID == FId).Select(t => t.FNumber.ToString()).FirstOrDefault();
                    break;
                case 4:
                    sReturn = db.ManageDept.Where(t => t.FNumber.ToString() == FId).Select(t => t.FID).FirstOrDefault();
                    break;
                case 5:
                    sReturn = db.ManageDept.Where(t => t.FCNumber.ToString() == FId).Select(t => t.FID).FirstOrDefault();
                    break;
            }
            return sReturn;
        }


        /// <summary>
        /// 取政府部门名
        /// </summary>
        /// <param name="fNumber">字典编码</param>
        /// <returns></returns>
        public static string GetDeptName(object fNumber)
        {
            if (Information.IsNumeric(fNumber))
            {
                int IntFNumber = EConvert.ToInt(fNumber);
                ProjectDB db = new ProjectDB();
                string FName = db.ManageDept.Where(t => t.FNumber == IntFNumber).Select(t => t.FName).FirstOrDefault();
                return FName;
            }
            return "";
        }

        /// <summary>
        /// 获取行政部门名
        /// </summary>
        /// <param name="fNumber">行政部门编码</param>
        /// <returns></returns>
        public static string GetDepartmentName(object fNumber)
        {
            if (Information.IsNumeric(fNumber))
            {
                int IntFNumber = EConvert.ToInt(fNumber);
                ProjectDB db = new ProjectDB();
                string FName = db.Department.Where(t => t.FNumber == IntFNumber).Select(t => t.FName).FirstOrDefault();
                return FName;
            }
            return "";
        }


        /// <summary>
        /// 根据用护ID取用户名
        /// </summary>
        /// <param name="FUserId">用户cf_sys_user.FID</param>
        /// <returns></returns>
        public static string GegUserName(string FUserId)
        {
            if (!string.IsNullOrEmpty(FUserId))
            {
                ProjectDB db = new ProjectDB();
                string FName = db.CF_Sys_User.Where(t => t.FID == FUserId).Select(t => t.FName).FirstOrDefault();
                return FName;
            }
            return "";
        }


        //通过编码，取得一个table
        public IEnumerable<CF_Sys_Dic> getDicTbByFNumber(int number)
        {
            ProjectDB db = new ProjectDB();
            IEnumerable<CF_Sys_Dic> dic = null;
            dic = db.Dic.Where(t => t.FParentId == number && t.FName != "全部").OrderBy(t => t.FOrder);
            // dic = this.GetTable(EntityTypeEnum.EsDic, "", "FParentid='" + number + "' and FName<>'全部' order by forder,ftime desc");
            return dic;
        }

        //通过编码，取得字典表前几条
        public IEnumerable<CF_Sys_Dic> getDicTopTbByFNumber(int number, int top)
        {
            ProjectDB db = new ProjectDB();
            IEnumerable<CF_Sys_Dic> dic = null;
            dic = db.Dic.Where(t => t.FParentId == number && t.FName != "全部").Take(top).OrderBy(t => t.FOrder);
            //dt = this.GetTable(EntityTypeEnum.EsDic, " top " + top + "", "FParent='" + number + "' and FName<>'全部' order by forder");
            return dic;
        }


        public IQueryable<CF_Sys_ManageDept> GetCanReportDept(string fBaseInfoId)
        {
            ProjectDB db = new ProjectDB();
            string FUpDeptId = db.CF_Ent_BaseInfo.Where(t => t.FId == fBaseInfoId).Select(t => t.FUpDeptId.ToString()).FirstOrDefault();
            if (string.IsNullOrEmpty(FUpDeptId))
            {
                return null;
            }

            int n = FUpDeptId.Length / 2 - 2;
            string[] str = new string[n];
            for (int i = 0; i < n; i++)
            {
                str[i] = FUpDeptId.Substring(0, (i + 2) * 2);
            }

            return db.CF_Sys_ManageDept.Where(t => str.Contains(t.FNumber.ToString()));
        }

        public IQueryable<CF_Sys_ManageDept> GetCanReportDeptFromDeptNumber(string DeptNumber)
        {
            if (DeptNumber.Length < 4)
                return null;
            ProjectDB db = new ProjectDB();

            int n = DeptNumber.Length / 2 - 2;
            if (DeptNumber.Length == 4)
                n = DeptNumber.Length / 2 - 1;
            string[] str = new string[n];
            for (int i = 0; i < n; i++)
            {
                str[i] = DeptNumber.Substring(0, (i + 2) * 2);
            }

            return db.CF_Sys_ManageDept.Where(t => str.Contains(t.FNumber.ToString()));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetDept()
        {
            string result = System.Web.HttpContext.Current.Request.Url.Host;



            string Dept = ComFunction1.GetDefaultDept();
            ProjectDB db = new ProjectDB();
            CF_DeptColumn DeptC = db.CF_DeptColumn.Where(t => t.FDomain.ToLower() == result.ToLower()).FirstOrDefault();
            if (DeptC != null)
            {
                Dept = EConvert.ToString(DeptC.FManageDeptId);
            }
            return Dept;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetDepartMentID()
        {
            string result = System.Web.HttpContext.Current.Request.Url.Host;

            string DepartMentID = "0";
            ProjectDB db = new ProjectDB();
            CF_DeptColumn DeptC = db.CF_DeptColumn.Where(t => t.FDomain.ToLower() == result.ToLower()).FirstOrDefault();
            if (DeptC != null)
            {
                DepartMentID = EConvert.ToString(DeptC.FDepartMentId);
            }
            return DepartMentID;
        }

    
  
        /// <summary>
        /// 根据证书FID，得到证书编号和等级
        /// </summary>
        /// <param name="certiId">证书FID(CF_Ent_QualiCerti)</param>
        /// <param name="systemId">企业类型systemid</param>
        /// <returns></returns>
        public string getCertiLevel(string certiId, int systemId)
        {
            ProjectDB db = new ProjectDB();
            string str = "&";
            if (!string.IsNullOrEmpty(certiId))
            {
                switch (systemId)
                {
                    case 101:// 施工
                    case 180:// 外来施工
                        var result = (from t in db.CF_Ent_QualiCerti
                                      join t1 in db.CF_Ent_QualiCertiTrade on t.FId equals t1.FCertiId into x
                                      from trade in x.DefaultIfEmpty()
                                      where t.FIsDeleted == false && t.FId == certiId
                                      select new
                                      {
                                          t.FId,
                                          t.FCertiNo,
                                          FLevelName = (db.CF_Sys_QualiLevel.Where(d => d.FNumber == trade.FLevelId).Select(d => d.FName).FirstOrDefault())
                                      }).FirstOrDefault();
                        if (result != null)
                            str = result.FCertiNo + "&" + result.FLevelName;
                        break;
                    case 155:// 勘察设计
                    case 140:// 外来勘察设计
                    case 125:// 监理
                    case 182:// 处来监理
                    case 175:// 质量检测
                    case 145:// 施工图审
                    case 120:// 招标代理 
                    case 185:// 招标代理 
                        result = (from t in db.CF_Ent_QualiCerti
                                  where t.FIsDeleted == false && t.FId == certiId
                                  select new
                                  {
                                      t.FId,
                                      t.FCertiNo,
                                      FLevelName = (db.CF_Sys_QualiLevel.Where(d => d.FNumber == t.FLevel).Select(d => d.FName).FirstOrDefault())
                                  }).FirstOrDefault();
                        if (result != null)
                            str = result.FCertiNo + "&" + result.FLevelName;
                        break;
                }
            }
            return str;
        }


        /// <summary>
        /// 得到用户登陆次数（不包含后台登陆）
        /// </summary>
        /// <param name="UserRightId">userright.FID</param>
        /// <returns></returns>
        public static string getLoginTimes(string UserRightId)
        {
            ProjectDB db = new ProjectDB();
            return db.CF_Sys_Log.Count(t => t.FLogType == 5 && t.FOperator == UserRightId).ToString();
        }

        /// <summary>
        /// 得到最后一次登陆时间（不包含后台登陆）
        /// </summary>
        /// <param name="UserRightId">userright.FID</param>
        /// <returns></returns>
        public static DateTime getLastLoginDate(string UserRightId)
        {
            ProjectDB db = new ProjectDB();
            DateTime time= db.CF_Sys_Log.Where(t => t.FLogType == 5 && t.FOperator == UserRightId).OrderByDescending(t => t.FLogTime).Skip(1).Select(t => t.FLogTime).FirstOrDefault();
            if (time.Date == DateTime.MinValue.Date)
            {
                time = DateTime.Now;
            }
            return time;
        }


 

 
    }
}
