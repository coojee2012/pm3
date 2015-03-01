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
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using Approve.EntityCenter;
using Approve.RuleApp;

/// <summary>
/// EntReportDet 的摘要说明
/// </summary>
public class EntReportDet
{
    static RCenter rc = new RCenter();
    public static bool setDept(string dept,string systemid,string userid)
    {
        if ((dept != null && dept != "") && (systemid != null && systemid != "") && (userid != null && userid != ""))
        {
            if (dept.Length >= 4)
            {
                //得到字符串的前4位字符
                string tempStr = dept.Substring(0,4);
                if (tempStr == "6106")
                {
                    //在cf_sys_userright表中查找是否已经在《延安市》购买身份认证锁
                    string YASQL = "select fbegintime,fendtime,flocknumber from cf_sys_userright ";
                    YASQL += " where fsystemid='"+systemid+"' and fuserid='"+userid+"'";
                    DataTable YAdt = rc.GetTable(YASQL.ToString());
                    if (YAdt != null && YAdt.Rows.Count > 0)
                    {
                        //如何没有在《延安市》购买“身份认证锁”则不可以上报数据
                        if ((rc.StrToDate(YAdt.Rows[0]["fbegintime"].ToString()) == rc.StrToDate(YAdt.Rows[0]["fendtime"].ToString())) || 
                             YAdt.Rows[0]["flocknumber"]=="" || YAdt.Rows[0]["flocknumber"]==null)
                        {
                            return false;
                        }
                    }

              
                }
            }
        }
        return true;
    }
}
