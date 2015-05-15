/*
 * 公用类
 * 一些管理部门和企业都会使用到的方法
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    /// <param name="fprjid">项目编号</param>
    /// <param name="fprjitemid">工程编号</param>
    /// <param name="fentid">所在企业编号</param>
    /// <param name="idcard">人员身份证号</param>
    /// <param name="personname">人员姓名</param>
    /// <returns></returns>
    public bool lockperson(string fappid,string fprjid,string fprjitemid,string fentid,string idcard,string personname)
    {
        try
        {
            EgovaDB db = new EgovaDB();
            TC_PrjItem_Emp_Lock el = new TC_PrjItem_Emp_Lock();
            el.FId = Guid.NewGuid().ToString();
            el.FAppId = fappid;
            el.FPrjId = fprjid;
            el.FPrjItemId = fprjitemid;
            el.FIdCard = idcard;
            el.FHumanName = personname;
            el.IsLock = true;
            el.SelectedCount = 1;
            el.FCreateTime = DateTime.Now;
            el.FTime = DateTime.Now;
            db.TC_PrjItem_Emp_Lock.InsertOnSubmit(el);
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }


}