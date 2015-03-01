using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ProjectData;
using Tools;
namespace ProjectBLL
{
    public class CurrentEmpUser
    {

        /// <summary>
        /// 人员CF_Emp_Baseinfo.FId
        /// </summary>
        public static string EmpId
        {
            get
            {
                return EConvert.ToString(HttpContext.Current.Session["FEmpID"]);
            }
            set
            {
                HttpContext.Current.Session["FEmpID"] = value;
            }
        }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public static string Name
        {
            get
            {
                ProjectDB db = new ProjectDB();
                string Name = db.CF_Emp_BaseInfo.Where(u => u.FId == EmpId).Select(u => u.FName).FirstOrDefault();
                return Name;
            }
        }

        /// <summary>
        /// 所属企业 FBaseInfoID
        /// </summary>
        public static string EntBaseinfoId
        {
            get
            {
                ProjectDB db = new ProjectDB();
                string EntBaseinfoId = db.CF_Emp_BaseInfo.Where(u => u.FId == EmpId).Select(u => u.FBaseInfoID).FirstOrDefault();
                return EntBaseinfoId;
            }
        }

        /// <summary>
        /// 所属企业名
        /// </summary>
        public static string EntName
        {
            get
            {
                ProjectDB db = new ProjectDB();
                string EntName = (from t in db.CF_Emp_BaseInfo
                                  join b in db.CF_Ent_BaseInfo on t.FBaseInfoID equals b.FId
                                  where t.FId == EmpId
                                  select b.FName).FirstOrDefault();
                return EntName;
            }
        }  
        public static string FSystemId
        {
            get
            {
                ProjectDB db = new ProjectDB();
                string SystemId = (from t in db.CF_Emp_BaseInfo
                                  join b in db.CF_Ent_BaseInfo on t.FBaseInfoID equals b.FId
                                  where t.FId == EmpId
                                  select b.FSystemId.ToString()).FirstOrDefault();
                return SystemId;
            }
        }
    }
}
