using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using System.Collections;
using System.IO;
using Approve.RuleBase;

namespace ProjectData
{
    public class CenterDB : ProjectDataContext
    {
        public CenterDB()
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
    }
}
