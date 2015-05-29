/*
 * 公用类
 * 一些管理部门和企业都会使用到的方法
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Approve.RuleCenter;
using EgovaDAO;

/// <summary>
/// Common 的摘要说明
/// </summary>
public class Common
{

	public Common()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 通过人员身份证号码解锁人员
    /// 业务描述：1、开竣工管理中工程竣工时需要对相关人员进行解锁操作，如果该人员在其他项目中还存在锁定的情况，
    ///             则将改人员锁定到其他任一未竣工项目中。
    ///           2、
    /// </summary>
    /// <param name="personidcard">身份证号码</param>
    /// <param name="fprjitemid">工程id</param>
    /// <param name="fappid">业务流程编号</param>
    /// <returns></returns>
    public bool UnlockPerson(string fappid,string fprjitemid,string personidcard)
    {
        EgovaDB db = new EgovaDB();
        var v = db.TC_PrjItem_Emp_Lock.Where(t => t.FAppId == fappid && t.FPrjItemId == fprjitemid && t.FIdCard.Contains(personidcard)).FirstOrDefault();
        if (v != null)
        {
            v.IsLock = false;
            db.SubmitChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 锁定人员
    /// </summary>
    /// <param name="fappid">业务流程编号</param>
    /// <returns></returns>
    public bool lockperson(string fappid,out string errMsg)
    {
        try
        {
            RCenter db = new RCenter();
            string strsql = @" insert into TC_PrjItem_Emp_Lock(fid,FPrjId,FPrjItemId,FAppId,FEntId,FIdCard,IsLock,FHumanName,FEntName,FCreateTime,FTime,lockType,Fempid)
                                    select fid,FPrjId,FPrjItemId,FAppId,FEntId,FIdCard,1,FHumanName,FEntName,getdate(),getdate(),0,Fempid
	                                  from TC_PrjItem_Emp a
	                                 where FEntType in ('2','3','4','7') 
                                       and not exists(select 1 from TC_PrjItem_Emp_Lock b where a.FAppId = b.FAppId and b.FIdCard = b.FIdCard)
                                       and isnull(needDel,0) <> 1
                                       and FAppId ='" + fappid + "'";

            db.PExcute(strsql);
            errMsg = "";
            return true;
        }
        catch (Exception e)
        {
            errMsg = e.Message;
            return false;
        }
    }


}