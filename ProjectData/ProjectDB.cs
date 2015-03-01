using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using System.Collections;
using System.IO;
using Approve.RuleBase;
using System.Data;

namespace ProjectData
{
    public class ProjectDB : ProjectDataContext
    {
        public ProjectDB()
            : base(CreateConnection())
        {

        }
        private static string CreateConnection()
        {
            LicenseTools lt = new LicenseTools();
            string access = lt.GetConnectionString("dbCenter");
            return access;
        }
        public void ClearSystemCache()
        {
            DataCache.RemoveAll();
        }

        //public override void SubmitChanges(System.Data.Linq.ConflictMode failureMode)
        //{
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        this.Log = sw;
        //        try
        //        {
        //            base.SubmitChanges(failureMode);
        //            string s = sw.ToString();
        //            if (s.IndexOf("CF_Sys_Log") == -1 && s != "")
        //            {
        //                string title = "操作数据库的记录";
        //                if (s.IndexOf("\n") > -1)
        //                {
        //                    title += "," + s.Substring(0, s.IndexOf("\n"));
        //                }
        //                DataLog.Write(LogType.Info, LogSort.Operation, title, s);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            string s = sw.ToString();
        //            if (s.IndexOf("CF_Sys_Log") == -1 && s != "")
        //            {
        //                DataLog.Write(LogType.Info, LogSort.Operation, ex.ToString(), ex.StackTrace + "\n" + s);
        //            }
        //            throw;
        //        }
        //        this.Log = null;
        //    }

        //}
        /// <summary>
        /// 缓存中的菜单
        /// </summary>
        public IEnumerable<CF_Sys_Menu> Menu
        {
            set
            {
                DataCache.SetCache("Menu", value);
            }
            get
            {
                IEnumerable<CF_Sys_Menu> menu = DataCache.GetCache("Menu") as IEnumerable<CF_Sys_Menu>;
                if (menu == null)
                {
                    menu = this.CF_Sys_Menu.ToList().AsEnumerable();
                    DataCache.SetCache("Menu", menu);
                }
                return menu;
            }
        }

        /// <summary>
        /// 缓存中的后台菜单
        /// </summary>
        public IEnumerable<CF_Sys_Tree> Tree
        {
            set
            {
                DataCache.SetCache("Tree", value);
            }
            get
            {
                IEnumerable<CF_Sys_Tree> menu = DataCache.GetCache("Tree") as IEnumerable<CF_Sys_Tree>;
                if (menu == null)
                {
                    menu = this.CF_Sys_Tree.ToList().AsEnumerable();
                    DataCache.SetCache("Tree", menu);
                }
                return menu;
            }
        }

        /// <summary>
        /// 缓存中的字典
        /// </summary>
        public IEnumerable<CF_Sys_Dic> Dic
        {
            set
            {
                DataCache.SetCache("Dic", value);
            }
            get
            {
                IEnumerable<CF_Sys_Dic> Sys_Dic = DataCache.GetCache("Dic") as IEnumerable<CF_Sys_Dic>;
                if (Sys_Dic == null)
                {
                    Sys_Dic = this.CF_Sys_Dic.ToList().AsEnumerable();
                    DataCache.SetCache("Dic", Sys_Dic);
                }
                return Sys_Dic;
            }
        }

        /// <summary>
        /// 缓存中的全局变量
        /// </summary>
        public IEnumerable<CF_Sys_Object> Object
        {
            set
            {
                DataCache.SetCache("Object", value);
            }
            get
            {
                IEnumerable<CF_Sys_Object> Sys_Object = DataCache.GetCache("Object") as IEnumerable<CF_Sys_Object>;
                if (Sys_Object == null)
                {
                    Sys_Object = this.CF_Sys_Object.ToList().AsEnumerable();
                    DataCache.SetCache("Object", Sys_Object);
                }
                return Sys_Object;
            }
        }



        /// <summary>
        /// 缓存中的页面功能 CF_Role_Function
        /// </summary>
        public IEnumerable<CF_Sys_Role> Sys_Role
        {
            set
            {
                DataCache.SetCache("Sys_Role", value);
            }
            get
            {
                IEnumerable<CF_Sys_Role> sys_Role = DataCache.GetCache("Sys_Role") as IEnumerable<CF_Sys_Role>;
                if (sys_Role == null)
                {
                    sys_Role = this.CF_Sys_Role.ToList().AsEnumerable();
                    DataCache.SetCache("Sys_Role", sys_Role);
                }
                return sys_Role;
            }
        }



        /// <summary>
        /// 缓存中的管理部门 CF_Sys_ManageDept
        /// </summary>
        public IEnumerable<CF_Sys_ManageDept> ManageDept
        {
            set
            {
                DataCache.SetCache("ManageDept", value);
            }
            get
            {
                IEnumerable<CF_Sys_ManageDept> manageDept = DataCache.GetCache("ManageDept") as IEnumerable<CF_Sys_ManageDept>;
                if (manageDept == null)
                {
                    manageDept = this.CF_Sys_ManageDept.ToList().AsEnumerable();
                    DataCache.SetCache("ManageDept", manageDept);
                }
                return manageDept;
            }
        }
        /// <summary>
        /// 缓存中的管理部门 CF_Sys_Department
        /// </summary>
        public IEnumerable<CF_Sys_Department> Department
        {
            set
            {
                DataCache.SetCache("Department", value);
            }
            get
            {
                IEnumerable<CF_Sys_Department> dpartment = DataCache.GetCache("Department") as IEnumerable<CF_Sys_Department>;
                if (dpartment == null)
                {
                    dpartment = this.CF_Sys_Department.ToList().AsEnumerable();
                    DataCache.SetCache("Department", dpartment);
                }
                return dpartment;
            }
        }

        /// <summary>
        /// 缓存业务类型表 CF_Sys_ManageType
        /// </summary>
        public IEnumerable<CF_Sys_ManageType> ManageType
        {
            set
            {
                DataCache.SetCache("ManageType", value);
            }
            get
            {
                IEnumerable<CF_Sys_ManageType> v = DataCache.GetCache("ManageType") as IEnumerable<CF_Sys_ManageType>;
                if (v == null)
                {
                    v = this.CF_Sys_ManageType.ToList().AsEnumerable();
                    DataCache.SetCache("ManageType", v);
                }
                return v;
            }
        }

        /// <summary>
        /// 缓存业务类型表 CF_Sys_PrjList
        /// </summary>
        public IEnumerable<CF_Sys_PrjList> SysPrjList
        {
            set
            {
                DataCache.SetCache("SysPrjList", value);
            }
            get
            {
                IEnumerable<CF_Sys_PrjList> v = DataCache.GetCache("SysPrjList") as IEnumerable<CF_Sys_PrjList>;
                if (v == null)
                {
                    v = this.CF_Sys_PrjList.ToList().AsEnumerable();
                    DataCache.SetCache("SysPrjList", v);
                }
                return v;
            }
        }

        /// <summary>
        /// 缓存资质级别 CF_Sys_QualiLevel
        /// </summary>
        public IEnumerable<CF_Sys_QualiLevel> SysQualiLevel
        {
            set
            {
                DataCache.SetCache("SysQualiLevel", value);
            }
            get
            {
                IEnumerable<CF_Sys_QualiLevel> v = DataCache.GetCache("SysQualiLevel") as IEnumerable<CF_Sys_QualiLevel>;
                if (v == null)
                {
                    v = this.CF_Sys_QualiLevel.ToList().AsEnumerable();
                    DataCache.SetCache("SysQualiLevel", v);
                }
                return v;
            }
        }


        /// <summary>
        /// 业务系统表 CF_Sys_SystemName
        /// </summary>
        public IEnumerable<CF_Sys_SystemName> SystemName
        {
            set
            {
                DataCache.SetCache("SystemName", value);
            }
            get
            {
                IEnumerable<CF_Sys_SystemName> v = DataCache.GetCache("SystemName") as IEnumerable<CF_Sys_SystemName>;
                if (v == null)
                {
                    v = this.CF_Sys_SystemName.ToList().AsEnumerable();
                    DataCache.SetCache("SystemName", v);
                }
                return v;
            }
        }

        #region 方法

        /// <summary>
        /// 得到系统名称
        /// </summary>
        /// <param name="FNumber">编码</param>
        /// <returns></returns>
        public string getSystemName(object FNumber)
        {
            return SystemName.Where(t => t.FNumber == EConvert.ToInt(FNumber)).Select(t => t.FName).FirstOrDefault();
        }

        /// <summary>
        /// 得到系统变更内容
        /// </summary>
        /// <param name="FNumber">编码</param>
        /// <returns></returns>
        public string getSysObjectContent(object FNumber)
        {
            return Object.Where(t => t.FNumber == EConvert.ToString(FNumber)).Select(t => t.FContent).FirstOrDefault();
        }

        /// <summary>
        /// 资质等级列表
        /// </summary>
        /// <param name="FNumber">类型编码</param>
        /// <returns></returns>
        public IEnumerable<CF_Sys_QualiLevel> getSysQualiLevel(object FNumber)
        {
            return SysQualiLevel.Where(t => t.FSystemId == EConvert.ToInt(FNumber)).OrderBy(t => t.FNumber).ToList();
        }

        /// <summary>
        /// 字典列表
        /// </summary>
        /// <param name="FNumber">字典父编码</param>
        /// <returns></returns>
        public IEnumerable<CF_Sys_Dic> getDicList(object FNumber)
        {
            return Dic.Where(t => t.FParentId == EConvert.ToInt(FNumber)).OrderBy(t => t.FOrder).OrderBy(t => t.FNumber).ToList();
        }

        public string getDicName(object FNumber)
        {
            return Dic.Where(t => t.FNumber == EConvert.ToInt(FNumber)).Select(t => t.FName).FirstOrDefault();
        }


        /// <summary>
        /// 附件名称
        /// </summary>
        /// <param name="FNumber">附件编号</param>
        /// <returns></returns>
        public string getSysFileName(string FPrjFileId)
        {
            return SysPrjList.Where(t => t.FId == FPrjFileId).Select(t => t.FFileName).FirstOrDefault();
        }


        /// <summary>
        /// 得到专项图层使用的标注图片（仅point要素）
        /// </summary>
        /// <param name="FNumber">菜单编号</param>
        /// <returns></returns>
        public string getMapPorintIMG(string FNumber)
        {
            return Menu.Where(m => m.FNumber == FNumber).Select(m => m.FSelcePicName).FirstOrDefault();
        }

        /// <summary>
        /// 通过菜单编号得到类型名称
        /// </summary>
        /// <param name="FNumber">菜单编号</param>
        /// <returns></returns>
        public string getMapTypeName(string FNumber)
        {
            return Menu.Where(m => m.FNumber == FNumber).Select(m => m.FName).FirstOrDefault();
        }

        /// <summary>
        /// 得到地区名
        /// </summary>
        /// <param name="FNumber">地区编号</param>
        /// <returns></returns>
        public string getDeptName(object FNumber)
        {
            return ManageDept.Where(m => m.FNumber == EConvert.ToInt(FNumber)).Select(m => m.FName).FirstOrDefault();
        }

        /// <summary>
        /// 得到地区名
        /// </summary>
        /// <param name="FNumber">地区编号</param>
        /// <returns></returns>
        public string getDeptFullName(object FNumber)
        {
            return ManageDept.Where(m => m.FNumber == EConvert.ToInt(FNumber)).Select(m => m.FFullName).FirstOrDefault();
        }
        /// <summary>
        /// 得到地区编码，传带“区”的会得到市辖区（所有带“区”）的编码列表。
        /// </summary>
        /// <param name="oldNumber"></param>
        /// <returns></returns>
        public List<string> getDeptList(string oldNumber)
        {
            List<string> s = new List<string>();

            var m = ManageDept.Where(t => t.FNumber.ToString() == oldNumber).FirstOrDefault();
            if (m != null)
            {
                string name = m.FName;
                if (name.EndsWith("区") && m.FLevel == 3)
                {
                    var v = from t in ManageDept
                            where t.FName.EndsWith("区") && t.FParentId == m.FParentId
                            select t;
                    foreach (var t in v)
                    {
                        s.Add(t.FNumber.ToString());
                    }
                }
                else
                {
                    s.Add(oldNumber);
                }
            }
            return s;
        }


        /// <summary>
        /// 从业务编号 得到业务名称
        /// </summary>
        /// <param name="FNumber">业务编号</param>
        /// <returns></returns>
        public string getManageTypeName(object FNumber)
        {
            int n = EConvert.ToInt(FNumber);
            return ManageType.Where(t => t.FNumber == n).Select(t => t.FName).FirstOrDefault();
        }


        public DataTable getAllupDeptId(string FProvice, int isHaveTown, int isCity)
        {
            DataTable dtr = new DataTable();
            dtr.Columns.Add(new DataColumn("FNumber"));
            dtr.Columns.Add(new DataColumn("FName"));
            var v = from t in ManageDept where t.FIsDeleted == false select new { t.FNumber, t.FName, t.FOrder, t.FLevel, t.FParentId, t.FIsTown };
            if (FProvice != "")
            {
                v = v.Where(t => t.FNumber.ToString().StartsWith(FProvice));
            }
            if (isCity == 1)
            {
                v = v.Where(t => t.FLevel <= 2);
            }

            for (int i = 0; i < 1; i++)
            {
                var s = v.Where(t => t.FNumber.ToString() == FProvice).Select(t => new { t.FNumber, t.FName }).FirstOrDefault();

                DataRow drc = dtr.NewRow();
                drc["FName"] = s.FName;
                drc["FNumber"] = s.FNumber;
                dtr.Rows.Add(drc);

                var s2 = v.Where(t => t.FParentId.ToString() == FProvice).OrderBy(t => t.FOrder).OrderBy(t => t.FNumber);
                foreach (var t2 in s2)
                {
                    drc = dtr.NewRow();
                    drc["FName"] = t2.FName;
                    drc["FNumber"] = t2.FNumber;
                    dtr.Rows.Add(drc);

                    if (isHaveTown == 1)
                    {
                        var s3 = v.Where(t => t.FParentId == t2.FNumber && t.FIsTown).OrderBy(t => t.FOrder).OrderBy(t => t.FNumber);
                        foreach (var t3 in s3)
                        {
                            drc = dtr.NewRow();
                            drc["FName"] = "　　" + t3.FName;
                            drc["FNumber"] = t3.FNumber;
                            dtr.Rows.Add(drc);
                        }
                    }
                    else
                    {
                        var s3 = v.Where(t => t.FParentId == t2.FNumber && t.FIsTown).OrderBy(t => t.FOrder).OrderBy(t => t.FNumber);
                        foreach (var t3 in s3)
                        {
                            drc = dtr.NewRow();
                            drc["FName"] = "　　" + t3.FName;
                            drc["FNumber"] = t3.FNumber;
                            dtr.Rows.Add(drc);
                        }
                    }
                }
            }
            return dtr;
        }

        #endregion


    }
}
