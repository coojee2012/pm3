using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Approve.EntityBase;
using Approve.RuleCenter;

namespace RuleApp
{
    public class UserLogin
    {
        public static void EntLogin(System.Web.UI.Page page, string FLocknumber, string FSystemId, string sys)
        {
            RCenter rc = new RCenter();
            if (string.IsNullOrEmpty(FLocknumber) || FLocknumber == "undefined")
            {
                page.ClientScript.RegisterStartupScript(page.ClientScript.GetType(), "", "<script type='text/javascript'>alert('请插入加密锁，然后点击登陆！')</script>");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select fid,flocknumber,ftype from cf_sys_user where flocknumber ='" + FLocknumber + "' ");

                DataTable dt = rc.GetTable(sb.ToString());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count > 1)
                    {
                        page.ClientScript.RegisterStartupScript(page.ClientScript.GetType(), "", "<script type='text/javascript'>alert('加密锁重复,请和系统管理员联系')</script>");
                    }
                    else
                    {
                        if (EConvert.ToString(dt.Rows[0]["ftype"]) == "1")//如果是管理部门，不用验证企业类型
                        {
                            page.Response.Redirect(string.Format("LockCheck.aspx?flockid={0}", dt.Rows[0]["FId"]));
                        }
                        else
                        {
                            sb.Remove(0, sb.Length);
                            DataTable dtemp = rc.GetTable("select * from cf_sys_userright where FUserId ='" + EConvert.ToString(dt.Rows[0]["FId"]) + "'  and fsystemid='" + FSystemId + "'");
                            if (dtemp.Rows.Count > 0)
                            {
                                page.Response.Redirect(string.Format("LockCheck.aspx?flockid={0}&sys={1}", dt.Rows[0]["FId"], sys));
                            }
                            else
                            {
                                page.ClientScript.RegisterStartupScript(page.ClientScript.GetType(), "", "<script type='text/javascript'>alert('你没有使用该系统的权限,请和系统管理员联系')</script>");
                            }
                        }
                    }
                }
                else
                {
                    sb.Remove(0, sb.Length);
                    DataTable dtemp = rc.GetTable(@"select u.flocknumber from cf_sys_user u  
                                                inner join cf_sys_userright r on u.FId=r.FUserId  where r.flocknumber ='" + FLocknumber + "' and FManageDeptId like '6106%'  and r.fsystemid='" + FSystemId + "'");
                    if (dtemp.Rows.Count > 0)
                    {
                        page.Response.Redirect(string.Format("LockCheck.aspx?flockid={0}&sys={1}", dtemp.Rows[0]["flocknumber"], sys));
                    }
                    else
                    {
                        page.ClientScript.RegisterStartupScript(page.ClientScript.GetType(), "", "<script type='text/javascript'>alert('登录失败，请检查加密锁驱动程序是否安装正确！')</script>");
                    }

                }
            }
        }
    }
}
