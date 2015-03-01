using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.EntitySys;
using Approve.EntityCenter;
using Approve.RuleApp;

namespace Approve.RuleApp
{
    public class ReportCheck
    {
        public static string Check(System.Web.UI.Page page, string dept, string systemid, string userid, SortedList[] sDeatil)
        {
            RCenter rc = new RCenter();
            RApp ra = new RApp();
            StringBuilder sb = new StringBuilder();
            string fmanagedeptid = dept;
            if (fmanagedeptid.Length > 4)
            {
                fmanagedeptid = fmanagedeptid.Substring(0, 4);
            }
            if (fmanagedeptid == "6106")
            {
                int Count = sDeatil.Length;
                EaProcess ep = null;
                bool[] flag1 = new bool[Count];
                bool flag = true;
                for (int i = 0; i < Count; i++)
                {
                    ep = ra.GetProcess(systemid, sDeatil[i]["FManageTypeId"].ToString(), sDeatil[i]["FLevelId"].ToString(), sDeatil[i]["FTypeId"].ToString(), "61");

                    if (ep == null)
                    {
                        return "noep";
                    }
                    else
                    {
                        //判断流程是否市上结束
                        sb.Remove(0, sb.Length);
                        sb.Append(" select fid from cf_app_subflow where fprocessid='" + ep.FId + "' and FIsEnd=1 and FRoleId=816");
                        DataTable dt = rc.GetTable(sb.ToString());
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            flag1[i] = true;
                        }
                        else
                        {
                            //判断流程是否省上结束
                            sb.Remove(0, sb.Length);
                            sb.Append(" select fid from cf_app_subflow where fprocessid='" + ep.FId + "' and FIsEnd=2 and FRoleId=816 ");
                            string fid1 = rc.GetSignValue(sb.ToString());
                            sb.Remove(0, sb.Length);
                            sb.Append(" select fid from cf_app_subflow where fprocessid='" + ep.FId + "' and FIsEnd=1 and FRoleId=903");
                            string fid2 = rc.GetSignValue(sb.ToString());
                            if (fid1 != null && fid1 != "" && fid2 != null && fid2 != "")
                            {
                                flag1[i] = false;
                            }
                        }
                    }
                }
                for (int i = 0; i < Count; i++)
                {
                    if (flag1[i] == false)
                    {
                        flag = false;
                    }
                }

                if (flag == false)
                {
                    sb.Remove(0, sb.Length);
                    sb.Append(" select * from cf_sys_user where fid='" + userid + "'");
                    DataTable udt = rc.GetTable(sb.ToString());
                    if (udt != null && udt.Rows.Count > 0)
                    {
                        string uflocknumber = udt.Rows[0]["flocknumber"].ToString();
                        if (uflocknumber == null || uflocknumber == "")
                        {
                            string TSWords = System.Configuration.ConfigurationSettings.AppSettings["TSWords"].ToString();
                            return "noright";
                        }
                    }
                    else
                    {
                        return "error";
                    }
                }
            }
            return "";
        }
    }
}
