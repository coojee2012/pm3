using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ProjectData;
using Tools;
namespace ProjectBLL
{
    public class CurrentDeptUser
    {
        /// <summary>
        /// 管理员FId
        /// </summary>
        public static string DeptUserId
        {
            get
            {
                return EConvert.ToString(HttpContext.Current.Session["DFUserId"]);
            }
            set
            {
                HttpContext.Current.Session["DFUserId"] = value;
            }
        }

        public static string UserName
        {
            get
            {
                ProjectDB projectDB = new ProjectDB();
                string FName = projectDB.CF_Sys_User.Where(u => u.FID == DeptUserId).Select(u => u.FName).FirstOrDefault();
                return FName;
            }
        }

        public static int DeptId
        {
            get
            {
                ProjectDB projectDB = new ProjectDB();
                int DeptId = EConvert.ToInt(projectDB.CF_Sys_User.Where(u => u.FID == DeptUserId).Select(u => u.FManageDeptId).FirstOrDefault());
                return DeptId;
            }
        }
        public static int FDepartmentID
        {
            get
            {
                ProjectDB projectDB = new ProjectDB();
                int FDepartmentID = EConvert.ToInt(projectDB.CF_Sys_User.Where(u => u.FID == DeptUserId).Select(u => u.FDepartmentID).FirstOrDefault());
                return FDepartmentID;
            }
        }
    }
}
