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
    /// <summary>
    /// 环节材料表枚举
    /// </summary>
    public enum 环节材料信息
    {
        选址意见书,
        建设用地规划许可证,
        建设工程规划许可证,
        招投标信息,
        合同备案,
        施工图审查,
        质量安全监督手续,
        资金保函及证明
    }
    private static RCenter _rCenter=null;

    private static RCenter GetRCenter(string dbName)
    {
        return _rCenter ?? (_rCenter = new RCenter(dbName));
    }

    /// <summary>
    /// 验证数据存放情况：
    /// 1、验证当前是否有数据，有就取，没有就继续判断。
    /// 2、验证标准库是否存在该项目的选址意见书，如有，就加载，没有就执行三。
    /// 3、标准库没有，就充许补填。
    /// </summary>
    public DataTable QueryData(string appid ,环节材料信息 环节材料Enum,out bool isyw)
    {
        EgovaDB db = new EgovaDB();
        var prjid = string.Empty;

        //得到项目编号
        var prjids = from location in db.TC_SGXKZ_PrjInfo
                     join prjitem in db.TC_PrjItem_Info on location.FPrjItemId equals prjitem.FId
                     where location.FAppId == appid
                     select new
                     {
                         pid = prjitem.FPrjId
                     };
        if (prjids != null && prjids.Count() > 0)
        {
            prjid = prjids.FirstOrDefault().pid;

        }
       return GetaData(appid,prjid,环节材料Enum,out isyw);
    }

    private DataTable GetaData(string appid, string xmbh, 环节材料信息 环节材料Enum,out bool isbz)
    {
        DataTable dt = null;
        isbz = false;
        switch (环节材料Enum)
        {
            case 环节材料信息.选址意见书:
                dt = GetXzyjs_yw(appid); //判断业务库中是否存在
                if (dt == null || dt.Rows.Count == 0)
                {
                    dt = GetXzyjs_bz(xmbh);   //判断标准库中是否存在
                    if (dt.Rows.Count > 0)
                    { isbz = true; }
                    else
                    { isbz = false; }
                }
                else
                { isbz = false; }
                break;
            case 环节材料信息.建设用地规划许可证:
                dt = GetJsydgh_yw(appid); //判断业务库中是否存在
                if (dt == null || dt.Rows.Count == 0)
                {
                    dt = GetJsydgh_bz(xmbh);  //判断标准库中是否存在
                    if (dt.Rows.Count > 0)
                    { isbz = true; }
                    else
                    { isbz = false; }
                }
                else 
                { isbz = false; }
                break;
            case 环节材料信息.建设工程规划许可证:
                dt = GetJsgcgh_yw(appid); //判断业务库中是否存在
                if (dt == null || dt.Rows.Count == 0)
                {
                    dt = GetJsgcgh_bz(xmbh);  //判断标准库中是否存在
                    if (dt.Rows.Count > 0)
                    { isbz = true; }
                    else
                    { isbz = false; }
                }
                else
                { isbz = false; }
                break;
            case 环节材料信息.招投标信息:
                dt = GetZBjg_yw(appid); //判断业务库中是否存在
                if (dt == null || dt.Rows.Count == 0)
                {
                    dt = GetZBjg_bz(xmbh);  //判断标准库中是否存在
                    if (dt.Rows.Count > 0)
                    { isbz = true; }
                    else
                    { isbz = false; }
                }
                else
                { isbz = false; }
                break;
            case 环节材料信息.施工图审查:
                dt = GetTs_yw(appid); //判断业务库中是否存在
                if (dt == null || dt.Rows.Count == 0)
                {
                    dt = GetTs_bz(xmbh);  //判断标准库中是否存在
                    if (dt.Rows.Count > 0)
                    { isbz = true; }
                    else
                    { isbz = false; }
                }
                else
                { isbz = false; }
                break;
            case 环节材料信息.合同备案:
                dt = GetJsgcgh_yw(appid); //判断业务库中是否存在
                if (dt == null || dt.Rows.Count == 0)
                {
                    dt = GetJsgcgh_bz(xmbh);  //判断标准库中是否存在
                    if (dt.Rows.Count > 0)
                    { isbz = true; }
                    else
                    { isbz = false; }
                }
                else
                { isbz = false; }
                break;
            case 环节材料信息.质量安全监督手续:
                dt = GetZlJd_yw(appid); //判断业务库中是否存在
                if (dt == null || dt.Rows.Count == 0)
                {
                    dt = GetZlJd_bz(xmbh);  //判断标准库中是否存在
                    if (dt.Rows.Count > 0)
                    { isbz = true; }
                    else
                    { isbz = false; }
                }
                else
                { isbz = false; }
                break;
        }

        return dt;
    }

    #region 选址意见书信息
    /// <summary>
    /// 从业务库 TC_SGXKZ_Location 中查询指定项目编号的选址意见书信息
    /// </summary>
    /// <param name="xmbh">项目编号</param>
    /// <returns>选址意见书</returns>
    private DataTable GetXzyjs_yw(string appid)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("dbCenter");

        string sql = @"select  *   from  TC_SGXKZ_Location where FAppId = '" + appid + "'";
        return prjdb.GetTable(sql);

    }

    /// <summary>
    /// 从标准库查询指定项目编号的选址意见书信息
    /// </summary>
    /// <param name="xmbh">项目编号</param>
    /// <returns>选址意见书</returns>
    private DataTable GetXzyjs_bz(string xmbh)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("XM_BaseInfo");

        string sql = @"select  [xzid] as fid ,[xmbh] as fprjitemid,[xmmc] as projectname,[xmdz] as locationaddress,[jsdw] as jsdw,[nydmj] as area,[njsgm] as scale,
            [xmjsyj] as projectbasis,[zsbh] as xzyjszsbh,[hfrq] as createtime,[fzjg] as hfjg,[createtime],qtsx as ydpzsx,'3' as bl  from  XM_BaseInfo.dbo.XM_XZYJS where xmbh = '" + xmbh + "'";
        return prjdb.GetTable(sql);

    }

    #endregion

    #region 建设用地规划许可证
    /// <summary>
    /// 从业务库查询指定项目编号的建设用地规划许可证
    /// </summary>
    /// <param name="xmbh">建设用地规划许可证</param>
    /// <returns>建设用地规划许可证</returns>
    private DataTable GetJsydgh_yw(string appid)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("dbCenter");

        string sql = @"select * from TC_SGXKZ_JSYDGHXKZ where  FAppId = '" + appid + "'";
        return prjdb.GetTable(sql);
    }

    /// <summary>
    /// 从标准库查询指定项目编号的建设用地规划许可证
    /// </summary>
    /// <param name="xmbh">建设用地规划许可证</param>
    /// <returns>建设用地规划许可证</returns>
    private DataTable GetJsydgh_bz(string xmbh)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("XM_BaseInfo");

        string sql = @"SELECT [JSID] as  fid,[XMBH] as  fprjitemid,[XMMC] as  projectname,[JSDW] as  jsdw,[YDWZ] as  address,[YDDW]  ,[YDXZ] as  ydxz,[YDMJ] as  constrscale,[JSGM]  ,[FTJFJMC] as others ,[HFRQ] as CreateTime,[FZJG] as  hfjg,[BH] as  ydghxkzbh,
[CreateTime]  as greatetime,[Ftime],'3' as bl from   XM_BaseInfo.dbo.XM_JSYDGH where xmbh = '" + xmbh + "'";
        sql = sql + @" union SELECT [JSID] as  fid,[XMBH] as  fprjitemid,[XMMC] as  projectname,[JSDW] as  jsdw,[YDWZ] as  address,[YDDW]  ,[YDXZ] as  ydxz,[YDMJ] as  constrscale,[JSGM]  ,[FTJFJMC] as others ,[HFRQ] as CreateTime,[FZJG] as  hfjg,[BH] as  ydghxkzbh,
[CreateTime]  as greatetime,[Ftime],'3' as bl from   XM_BaseInfo.dbo.XM_JSYDGH_SZ where xmbh = '" + xmbh + "'";
        return prjdb.GetTable(sql);
    }
    #endregion

    #region 建设工程规划许可证
    /// <summary>
    /// 从业务查询指定项目编号的建设工程规划许可证
    /// </summary>
    /// <param name="xmbh">建设用地规划许可证</param>
    /// <returns>建设工程规划许可证</returns>
    private DataTable GetJsgcgh_yw(string appid)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("dbCenter");

        string sql = @"select * from TC_SGXKZ_JSGCGHXKZ where  FAppId = '" + appid + "'";
        return prjdb.GetTable(sql);
    }

    /// <summary>
    /// 从标准库查询指定项目编号的建设工程规划许可证
    /// </summary>
    /// <param name="xmbh">建设工程规划许可证</param>
    /// <returns>建设用地规划许可证</returns>
    private DataTable GetJsgcgh_bz(string xmbh)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("XM_BaseInfo");

        string sql = @"SELECT jsid as FId ,xmbh as FprjItemId ,xmmc ProjectName ,JSDW ,xmdz Address ,JZZMJ Area ,JSGM as ConstrScale ,CSGD Span ,FTJFJMC Others ,
                              hfrq CreateTime ,fzjg HFJG ,ZSBH GCGHXKZBH ,'3' BL from  XM_BaseInfo.dbo.XM_JSGCGH where xmbh = '" + xmbh + "'";
        sql = sql + @" union SELECT jsid as FId ,xmbh as FprjItemId ,xmmc ProjectName ,JSDW ,xmdz Address ,'' Area ,JSGM as ConstrScale ,'' Span ,FTJFJMC Others ,
                             hfrq CreateTime ,fzjg HFJG ,ZSBH GCGHXKZBH ,'3' BL from   XM_BaseInfo.dbo.XM_JSGCGH_SZ where xmbh = '" + xmbh + "'"; 
        return prjdb.GetTable(sql);
    }

    #endregion

    #region 招投标信息
    /// <summary>
    /// 从业务查询指定项目编号招投标信息
    /// </summary>
    /// <param name="xmbh">招投标信息</param>
    /// <returns>招投标信息</returns>
    private DataTable GetZBjg_yw(string appid)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("dbCenter");

        string sql = @"select a.fid as pid,a.bl,a.yl,b.fid,b.FAppId,b.FprjItemId,b.PrjItemName,b.ProjectName,b.ProjectNo,b.JLZBLX,JLZBDW,ZBTZSBH,Area 
                       from TC_SGXKZ_ZBJGBL a, TC_SGXKZ_ZBJG b where a.FAppId = b.FAppId and a.FPrjItemId = b.FprjItemId and a.FAppId = '" + appid + "'";
        return prjdb.GetTable(sql);
    }

    /// <summary>
    /// 从标准库查询指定项目编号的招投标信息
    /// </summary>
    /// <param name="xmbh">招投标信息</param>
    /// <returns>招投标信息</returns>
    private DataTable GetZBjg_bz(string xmbh)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("XM_BaseInfo");
        string sql = @"select '3' as bl,zbid as fid,xmbh as FprjItemId,isnull(xmmc,'') + ''  + isnull(bdmc,'') as PrjItemName,bh as ProjectNo,
                               '' as JLZBLX,zbqymc as JLZBDW,zbtzsbh as ZBTZSBH, zbmj as Area from  XM_BaseInfo.dbo.[XM_ZBJGXX] where xmbh = '" + xmbh + "'";
        return prjdb.GetTable(sql);
    }
    #endregion

    #region 开放出来的招投标信息
    /// <summary>
    /// 从业务查询指定项目编号招投标信息
    /// </summary>
    /// <param name="xmbh">招投标信息</param>
    /// <returns>招投标信息</returns>
    public DataTable GetZBjg_yw_p(string fid)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("dbCenter");

        string sql = @"select * from TC_SGXKZ_ZBJG where  fid = '" + fid + "'";
        return prjdb.GetTable(sql);
    }

    /// <summary>
    /// 从标准库查询指定项目编号的招投标信息
    /// </summary>
    /// <param name="xmbh">招投标信息</param>
    /// <returns>招投标信息</returns>
    public DataTable GetZBjg_bz_p(string fid)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("XM_BaseInfo");

        string sql = @"select '3' as bl,zbid as fid,xmbh as FprjItemId,isnull(xmmc,'') as ProjectName, isnull(bdmc,'') as PrjItemName,bh as ProjectNo,JSDW, ZBQYZZJGDM as JLZBDWZZJGDM,
                               ZBQYZZJDJ as JLZBQYZZDJ,ZBQYZZZSH as JLZBQYZZZSH,'11220801' as JLZBLX,zbqymc as JLZBDW,zbtzsbh as ZBTZSBH, zbmj as Area,ZBJG as JLZBJ,ZBTZSFBSJ as JLZBRQ,
                               BASJ as JLHTBATime from  XM_BaseInfo.dbo.[XM_ZBJGXX]
                        where ZBID = '" + fid + "'";
        return prjdb.GetTable(sql);
    }
    #endregion

    #region 合同备案
    /// <summary>
    /// 从业务查询指定项目编号合同备案
    /// </summary>
    /// <param name="xmbh">合同备案</param>
    /// <returns>合同备案</returns>
    private DataTable GetHt_yw(string appid)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("dbCenter");

        string sql = @"select * from TC_SGXKZ_HTBA where  FAppId = '" + appid + "'";
        return prjdb.GetTable(sql);
    }

    /// <summary>
    /// 从标准库查询指定项目编号的合同备案
    /// </summary>
    /// <param name="xmbh">合同备案</param>
    /// <returns>合同备案</returns>
    private DataTable GetHt_bz(string xmbh)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("XM_BaseInfo");

        string sql = @"select * from  XM_BaseInfo.dbo.[XM_HTBAXX] where xmbh = '" + xmbh + "'";
        return prjdb.GetTable(sql);
    }
    #endregion

    #region 开放出来的合同备案
    /// <summary>
    /// 从业务查询指定项目编号合同备案
    /// </summary>
    /// <param name="xmbh">合同备案</param>
    /// <returns>合同备案</returns>
    public DataTable GetHt_yw_p(string fid)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("dbCenter");

        string sql = @"select * from TC_SGXKZ_HTBA where  FAppId = '" + fid + "'";
        return prjdb.GetTable(sql);
    }

    /// <summary>
    /// 从标准库查询指定项目编号的合同备案
    /// </summary>
    /// <param name="xmbh">合同备案</param>
    /// <returns>合同备案</returns>
    public DataTable GetHt_bz_p(string xmbh)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("XM_BaseInfo");

        string sql = @"select * from  XM_BaseInfo.dbo.[XM_HTBAXX] where xmbh = '" + xmbh + "'";
        return prjdb.GetTable(sql);
    }
    #endregion

    #region 图审信息
    /// <summary>
    /// 从业务查询指定项目编号的建设工程规划许可证
    /// </summary>
    /// <param name="xmbh">建设用地规划许可证</param>
    /// <returns>建设工程规划许可证</returns>
    private DataTable GetTs_yw(string appid)
    {
        RCenter prjdb = GetRCenter("dbCenter");

        string sql = @"select * from TC_SGXKZ_SGTSC where  FAppId = '" + appid + "'";
        return prjdb.GetTable(sql);
    }

    /// <summary>
    /// 从标准库查询指定项目编号的建设工程规划许可证
    /// </summary>
    /// <param name="xmbh">建设工程规划许可证</param>
    /// <returns>建设用地规划许可证</returns>
    private DataTable GetTs_bz(string xmbh)
    {
        //从标准库中读取项目信息
        RCenter prjdb = GetRCenter("XM_BaseInfo");

        string sql = @"SELECT stid as [FId] ,xmbh as [FprjItemId] ,STJGZSBH  as [SGTSCHGSBH] ,bh    as [ProjectNo] ,stjg  as [SGTSCJGMC] ,CensorCorpCode as [SGTSCZZJGDM] ,
                       FZRQ  as [SCWCRQ] ,JZZGD   as [ConstrScale] ,kcdw as  [KCDWMC] ,KCDWZSBH as  [KCDWZZJGDM] ,sjdw as [SJDWMC] ,sjdwzsbh as [SJDWZZJGDM] ,OneCensorIsPass as [YCSCSFTG] ,OneCensorWfqtCount as [YCSCWFTS] ,OneCensorWfqtContent as [YCSCWFTM] ,'3' as [BL] from  XM_BaseInfo.dbo.XM_SGTSCXX where xmbh = '" + xmbh + "'";
        sql = sql + @" union SELECT stid as [FId] ,xmbh as [FprjItemId] ,STJGZSBH  as [SGTSCHGSBH] ,bh    as [ProjectNo] ,stjg  as [SGTSCJGMC] ,CensorCorpCode as [SGTSCZZJGDM] ,
                      FZRQ    as [SCWCRQ] ,''  as [ConstrScale] ,kcdw as  [KCDWMC] ,KCDWZSBH as  [KCDWZZJGDM] ,sjdw as [SJDWMC] ,sjdwzsbh as [SJDWZZJGDM] ,OneCensorIsPass as [YCSCSFTG] ,OneCensorWfqtCount as [YCSCWFTS] ,OneCensorWfqtContent as [YCSCWFTM] ,'3' as [BL]   from   XM_BaseInfo.dbo.XM_SGTSCSZXX where xmbh = '" + xmbh + "'";
        return prjdb.GetTable(sql);        return prjdb.GetTable(sql);
    }
    #endregion

    #region 质量安全监督手续
    /// <returns>质量安全监督手续</returns>
    private DataTable GetZlJd_yw(string appid)
    {
        RCenter prjdb = GetRCenter("dbCenter");

        string sql = @"select * from TC_SGXKZ_JDSX where  FAppId = '" + appid + "'";
        return prjdb.GetTable(sql);
    }

    /// <returns>质量安全监督手续</returns>
    private DataTable GetZlJd_bz(string xmbh)
    {
        //从标准库中读取项目信息
        RCenter projdb = GetRCenter("XM_BaseInfo");

        string sql = @" dbo.Proc_ZLAQ '" + xmbh + "'";
        return projdb.GetTable(sql);
    }
    #endregion
}