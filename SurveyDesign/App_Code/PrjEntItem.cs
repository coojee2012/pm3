using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Approve.RuleCenter;
using System.Data.SqlClient;
using Approve.EntityBase;
using System.Data;
using System.Collections;

/// <summary>
///PrjEntItem 的摘要说明
/// </summary>
public class PrjEntItem
{
    RCenter rc = new RCenter();
    public PrjEntItem()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }


    public string GetEntNameByFid(string entbaseinfoid)
    {
        string entName = "";
        entName = rc.GetSignValue("select fname from CF_Ent_BaseInfo where fid=@fid", new SqlParameter("fid", entbaseinfoid));
        return entName;
    }

    public string GetEntIdByPrjItemId(string prjItemID,string entType)
    {
        string entId = "";
        SortedList sl = new SortedList();
        sl.Add("fid", prjItemID);
        sl.Add("FEntType", entType);
        entId = rc.GetSignValue("select FBaseInfoId from CF_Prj_Ent where FPrjItemId=@fid and FEntType=@FEntType", rc.ConvertParameters(sl));
        
        return entId;

    }

    public string GetEntNameByPrjItemid(string prjitemid,string entType)
    {
        string entName = "";
        string entId = GetEntIdByPrjItemId(prjitemid,entType);
        entName = GetEntNameByFid(entId);
        return entName;
    }

    //查看已上传批完的招标文件
    public DataTable GetZBWJ(string FPrjItemID,string type)
    {
        

        DataTable dt = rc.GetTable("select top 1 f.FFILEPATH,f.ffilename from CF_Prj_FileOther f,CF_Prj_File pf, CF_PrjItem_BaseInfo p where p.fid = pf.fprocid and pf.fid = f.fprjfileid and p.fprjid ='" + FPrjItemID + "' and p.ftype ="+type );
        
        return dt;
    }

    //解锁未中标的建造师
    public bool PrjLock(string prjitem , string empID )
    {
        string sql = "select fid from CF_Emp_PrjLock where fprjitemid =@prjitem and fempid <> @empid ";

        SortedList sl = new SortedList();
        sl.Add("prjitem", prjitem);
        sl.Add("empid", empID);

        DataTable dt = rc.GetTable(sql, rc.ConvertParameters(sl));
        if (dt != null)
        {
            return rc.PExcute("delete CF_Emp_PrjLock where fid in (" + sql + ")", rc.ConvertParameters(sl));
        }
        return true;
    }
    public static Dictionary<string, string> DicGCLB {
        get {
            string sql = @"select FName,Fnumber,FCnumber from CF_Sys_Dic where FparentId='20001'"; //获取工程类型
            RCenter rc = new RCenter();
            DataTable table = rc.GetTable(sql);
            Dictionary<string, string> dicGCLB = new Dictionary<string, string>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    dicGCLB.Add(row["FCnumber"].ToString(), row["FName"].ToString());
                }
            }
            return dicGCLB;
        }
    }
    public static Dictionary<string, string> DicGCLBBM
    {
        get
        {
            string sql = @"select Fnumber,FCnumber from CF_Sys_Dic where FparentId='20001'"; //获取工程类型
            RCenter rc = new RCenter();
            DataTable table = rc.GetTable(sql);
            Dictionary<string, string> dicGCLB = new Dictionary<string, string>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    dicGCLB.Add(row["FCnumber"].ToString(), row["Fnumber"].ToString());
                }
            }
            return dicGCLB;
        }
    }
}
