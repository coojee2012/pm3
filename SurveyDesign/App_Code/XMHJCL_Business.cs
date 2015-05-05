using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Approve.RuleCenter;
using EgovaDAO;

/// <summary>
/// 项目环节材料业务类
/// </summary>
public class XMHJCL_Business
{

    private static RCenter _rCenter=null;

    private static RCenter GetRCenter()
    {
        return _rCenter ?? (_rCenter = new RCenter("XM_BaseInfo"));
    }

    /// <summary>
    /// 从标准库查询指定项目编号的选址意见书信息
    /// </summary>
    /// <param name="xmbh">项目编号</param>
    /// <returns>选址意见书</returns>
    private DataTable GetXzyjs(string xmbh)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter();

        string sql = @"select  *   from  XM_BaseInfo.dbo.XM_XZYJS where xmbh = '" + xmbh + "'";
        return prjdb.GetTable(sql);

    }


    /// <summary>
    /// 从标准库查询指定项目编号的建设用地规划许可证
    /// </summary>
    /// <param name="xmbh">建设用地规划许可证</param>
    /// <returns>建设用地规划许可证</returns>
    private DataTable GetJsydgh(string xmbh)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter();

        string sql = @"select * from  XM_BaseInfo.dbo.XM_JSYDGH where xmbh = '" + xmbh + "'";
        return prjdb.GetTable(sql);
    }


    public DataTable GataBaseData(string xmbh, 环节材料信息 环节材料Enum)
    {
        if (环节材料Enum.GetHashCode()==环节材料信息.选址意见书.GetHashCode())
        {
            return GetXzyjs(xmbh);
        }else if (环节材料Enum.GetHashCode()==环节材料信息.建设用地规划许可证.GetHashCode())
        {
           return GetJsydgh(xmbh);
        }
        return new DataTable();
    }

    /// <summary>
    /// 环节材料表枚举
    /// </summary>
    public enum 环节材料信息
    {
        选址意见书,
        建设用地规划许可证
    }
}