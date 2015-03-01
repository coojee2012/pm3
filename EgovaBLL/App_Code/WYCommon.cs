using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using Approve.EntityBase;
using Approve.RuleCenter;
using Ext.Net;
using System.Text;
using Seaskyer.Strings;

/// <summary>
/// WYCommon 的摘要说明
/// </summary>
public class WYCommon
{
    static RCenter rc = new RCenter();
    static RCenter rc_XMB = new RCenter("XM_BaseInfo"); //项目标准信息库
    static RCenter rc_RYB = new RCenter("SCRYXY"); //项目标准信息库

    public WYCommon()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    #region 物业归档数据
    /// <summary>
    /// 归档物业数据
    /// </summary>
    /// <returns></returns>
    public static bool FileInfo(string fappid, string FManageTypeId)
    {
        string FBaseInfoId = rc.GetSignValue("select FBaseinfoId from CF_App_List where FId='" + fappid + "'");
        string XMBH = rc.GetSignValue("select XMBH from YW_WY_XM_JBXX where FAppID='" + fappid + "'");
        SaveOptionEnum so = SaveOptionEnum.Insert;
        StringBuilder str = new StringBuilder();
        switch (FManageTypeId)
        {
            #region 项目在管申请-14401
            case "14401":
                try
                {
                    str.Append(IntoJBXX(FBaseInfoId, fappid, XMBH));
                    str.Append(";");
                    str.Append(intoBZJBXX(XMBH));
                    str.Append(";");
                    str.Append(IntoKZXX(FBaseInfoId, fappid, XMBH));
                    str.Append(";");
                    str.Append(IntoHTBA(FBaseInfoId, fappid, XMBH));
                    str.Append(";");
                    str.Append(IntoYWHBA(FBaseInfoId, fappid, XMBH));
                    str.Append(";");
                    str.Append(IntoYWHCY(FBaseInfoId, fappid, XMBH));
                    str.Append(";");
                    str.Append(IntoQTXX(FBaseInfoId, fappid, XMBH));
                    str.Append(";");
                    str.Append(IntoRYJBXX(FBaseInfoId, fappid, XMBH));
                    str.Append(";");
                    str.Append(IntoLPB(FBaseInfoId, fappid, XMBH));

                    rc.PExcute(str.ToString(), true);
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
                break;
            #endregion

            #region 项目失去申请-14402
            case "14402": //项目失去申请
                string strsql = "Update WY_XM_JBXX set FBaseInfoID=0 where XMBH='" + XMBH + "'";
                if (!rc.PExcute(strsql)) { return false; }
                break;

            #endregion

            #region 项目变更备案-14403
            case "14403": //项目变更备案
                if (!CheckAndDel("YW_WY_XM_YZWYHBA", fappid, "WY_XM_YZWYHBA", XMBH)) { return false; }
                if (!CheckAndDel("YW_WY_XM_YZWYHCY", fappid, "WY_XM_YZWYHCY", XMBH)) { return false; }
                if (!CheckAndDel("YW_WY_RY_JBXX", fappid, "WY_RY_JBXX", XMBH)) { return false; }
                if (!CheckAndDel("YW_WY_XM_XMQTXX", fappid, "WY_XM_XMQTXX", XMBH)) { return false; }
                //if (!checkAndDel_LPB("YW_WY_XM_LZXX", fappid, "WY_XM_LZXX", XMBH)) { return false; }
                try
                {
                    str.Append(IntoYWHBA(FBaseInfoId, fappid, XMBH));
                    str.Append(";");
                    str.Append(IntoYWHCY(FBaseInfoId, fappid, XMBH));
                    str.Append(";");
                    str.Append(IntoQTXX(FBaseInfoId, fappid, XMBH));
                    str.Append(";");
                    str.Append(IntoRYJBXX(FBaseInfoId, fappid, XMBH));
                    str.Append(";");
                    str.Append(IntoLPB(FBaseInfoId, fappid, XMBH));
                    rc.PExcute(str.ToString(), true);
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
                break;
            #endregion

            #region 项目合同备案-14404
            case "14404": //项目合同备案
                if (!CheckAndDel("YW_WY_XM_HTBA", fappid, "WY_XM_HTBA", XMBH)) { return false; }
                str.Append(IntoHTBA(FBaseInfoId, fappid, XMBH));
                rc.PExcute(str.ToString());
                break;
            #endregion

            #region 项目业委会备案-14405
            case "14405": //项目业委会备案
                if (!CheckAndDel("YW_WY_XM_YZWYHBA", fappid, "WY_XM_YZWYHBA", XMBH)) { return false; }
                if (!CheckAndDel("YW_WY_XM_YZWYHCY", fappid, "WY_XM_YZWYHCY", XMBH)) { return false; }
                str.Append(IntoYWHBA(FBaseInfoId, fappid, XMBH));
                str.Append(";");
                str.Append(IntoYWHCY(FBaseInfoId, fappid, XMBH));
                rc.PExcute(str.ToString(), true);
                break;
            #endregion

            #region 项目财务年报-14406
            case "14406": //项目财务年报
                if (!CheckAndDel("YW_WY_XM_CWQK", fappid, "WY_XM_CWQK", XMBH)) { return false; }
                rc.PExcute(str.ToString());
                break;
            #endregion

            default:
                return false;
                break;
        }
        return true;
    }
    #endregion 物业归档数据

    /// <summary>
    /// 判断是否有值
    /// </summary>
    /// <returns></returns>
    private static bool IsEmpty(string Table, string XMBH, ref string fid)
    {
        string strsql = "select FID from " + Table + " where XMBH='" + XMBH + "'";
        DataTable dt = new DataTable();
        dt = rc.GetTable(strsql);
        if (dt.Rows.Count > 0)
        {
            fid = dt.Rows[0]["FID"].ToString();
            return false;
        }
        return true;
    }
    /// <summary>
    /// 判断是否有值（不同库）
    /// </summary>
    /// <param name="DBname"></param>
    /// <param name="Table"></param>
    /// <param name="XMBH"></param>
    /// <param name="fid"></param>
    /// <returns></returns>
    private static bool IsEmpty(string DBname, string Table, string XMBH, ref string fid)
    {
        string strsql = "use " + DBname + " select XMBH from " + Table + " where XMBH='" + XMBH + "'";
        DataTable dt = new DataTable();
        dt = rc.GetTable(strsql);
        if (dt.Rows.Count > 0)
        {
            fid = dt.Rows[0]["XMBH"].ToString();
            return false;
        }
        return true;
    }
    /// <summary>
    /// 检查并删除
    /// </summary>
    /// <param name="TableName">业务表名</param>
    /// <param name="fappid">业务FID</param>
    /// <param name="FileTBName">归档表名</param>
    /// <param name="XMBH">项目编号</param>
    /// <returns>True/False</returns>
    private static bool CheckAndDel(string TableName, string fappid, string FileTBName, string XMBH)
    {
        string strsql = "select FID from " + TableName + " where FAppID='" + fappid + "'";
        string fid = "";
        fid = rc.GetSignValue(strsql);
        if (!string.IsNullOrEmpty(fid))
        {
            string sql = "delete " + FileTBName + " where XMBH=" + XMBH + "";
            return rc.PExcute(sql);
        }
        return true;
    }
    //检查并删除-楼盘表
    private static bool checkAndDel_LPB(string TableName, string fappid, string FileTBName, string XMBH)
    {
        StringBuilder str = new StringBuilder();
        string strsql = "select BuildId from " + TableName + " where FAppID='" + fappid + "'";
        string fid = "";
        fid = rc.GetSignValue(strsql);
        if (fid != "")
        {
            string strsql2 = "select BuildId from " + FileTBName + " where XMBH='" + XMBH + "'";
            DataTable dt = new DataTable();
            dt = rc.GetTable(strsql2);
            str.Append("delete " + FileTBName + " where XMBH=" + XMBH + "");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str.Append(";Delete WY_XM_FWXX where BuildId='" + dt.Rows[i]["BuildId"] + "'");
                }
            }
            if (rc.PExcute(str.ToString(), true))
                return true;
        }
        return false;
    }

    #region [lyt方法方法]

    #region [人员信息入标准库]
    public static bool OperateRYToBZ(string fAppId)
    {
        string sql = "select * from WY_RY_JBXX where XMBH=(select top 1 XMBH from YW_WY_RY_JBXX where FAppID='" + fAppId + "')";
        DataTable dtRY = rc.GetTable(sql);
        if (dtRY.Rows.Count > 0)
        {
            //try 
            //{
            StringBuilder sb = new StringBuilder();
            SortedList sl = new SortedList();
            SaveOptionEnum so = SaveOptionEnum.Unknown;
            foreach (DataRow dr in dtRY.Rows)
            {
                for (int i = 0; i < dtRY.Columns.Count; i++)
                {
                    sl.Add(dtRY.Columns[i].ColumnName, dr[i].ToString());
                }
                sl.Remove("FID");
                sl.Remove("XMBH");
                sl.Remove("FSystemID");
                sl.Remove("FCreateUser");
                sl.Remove("FTime");
                sl.Remove("FIsDeleted");
                sl.Remove("SourceAppID");
                //添加公共数据
                sl.Add("fLastUpdateTime", DateTime.Now);
                string fid = IsEmptyRYAndReturn_BZ(dr["fPersonName"].ToString(), dr["fCardID"].ToString());
                if (fid == "")
                {
                    so = SaveOptionEnum.Insert;
                    sl.Add("FID", Guid.NewGuid().ToString());
                    //修正数据
                    sl.SetByIndex(sl.IndexOfKey("FCreateTime"), DateTime.Now);
                }
                else
                {
                    sl.Add("FID", fid);
                    so = SaveOptionEnum.Update;
                }
                return rc_RYB.SaveEBase("CF_Empxy_PersonBaseInfo", sl, "FID", so);
            }
            //}
            //catch { }

        }
        return false;
    }
    #endregion

    #region [判断人员标准库中是否存在某条记录如果存在并返回该记录的fid否则返回空]
    /// <summary>
    /// 判断人员标准库中是否存在某条记录如果存在并返回该记录的fid否则返回空
    /// </summary>
    /// <param name="name">人员姓名</param>
    /// <param name="cardId">人员证件号(默认身份证号，其他还未考虑)</param>
    /// <returns></returns>
    public static string IsEmptyRYAndReturn_BZ(string name, string cardId)
    {
        string strsql = "use scryxy select fid from cf_empxy_personbaseinfo where fpersonname='" + name + "' and fcardid='" + cardId + "'";
        DataTable dt = new DataTable();
        dt = rc.GetTable(strsql);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["fid"].ToString();
        }
        return "";
    }
    #endregion

    #endregion

    #region 归档基本信息-NEW

    private static string IntoJBXX(string fbaseinfoid, string fappid, string XMBH)
    {
        string fid = "";
        StringBuilder str = new StringBuilder();
        bool empty = IsEmpty("WY_XM_JBXX", XMBH, ref fid);
        if (empty)
        {
            str.Append("Insert into WY_XM_JBXX(XMBH,XMMC,XMSD,XMDZ,JSDW,JSDWDZ,XMLX,XMZLX,JSXZ,JSMS,XMZTZ,JSGM,JSNR,BH,JSDWZZJFDM,JSDWFR,JSDWFRDH,JSDWJSFZR,JSDWJSFZRZC,JSDWJSFZRDH,MapX,MapY,FUpDeptId,FUpDeptName,FTime,FSystemId,FCreateUser,FCreateTime,FIsDeleted,FBaseInfoID,Fid,StandardStatus,SourceAppID) ");
            str.Append("select XMBH,XMMC,XMSD,XMDZ,JSDW,JSDWDZ,XMLX,XMZLX,JSXZ,JSMS,XMZTZ,JSGM,JSNR,BH,JSDWZZJFDM,JSDWFR,JSDWFRDH,JSDWJSFZR,JSDWJSFZRZC,JSDWJSFZRDH,MapX,MapY,FUpDeptId,FUpDeptName,'" +
                DateTime.Now + "',FSystemId,FCreateUser,FCreateTime,FIsDeleted,'" + fbaseinfoid +
                "',NewID(),StandardStatus,'" + fappid + "' from YW_WY_XM_JBXX where FAppID='" + fappid + "'");
        }
        return str.ToString();
    }

    #endregion

    #region 进入基本信息标准库

    private static string intoBZJBXX(string XMBH)
    {
        string fid = "";
        StringBuilder str = new StringBuilder();
        bool empty = IsEmpty("XM_BaseInfo", "XM_XMJBXX", XMBH, ref fid);
        if (empty)
        {
            str.Append("Insert into XM_BaseInfo.dbo.XM_XMJBXX(XMBH,XMMC,XMSD,XMDZ,JSDW,JSDWDZ,XMLX,XMZLX,JSXZ,JSMS,XMZTZ,JSGM,JSNR,BH,JSDWZZJFDM,JSDWFR,JSDWFRDH,JSDWJSFZR,JSDWJSFZRZC,JSDWJSFZRDH,CreateTime,Ftime) ");
            str.Append("select XMBH,XMMC,XMSD,XMDZ,JSDW,JSDWDZ,XMLX,XMZLX,JSXZ,JSMS,XMZTZ,JSGM,JSNR,BH,JSDWZZJFDM,JSDWFR,JSDWFRDH,JSDWJSFZR,JSDWJSFZRZC,JSDWJSFZRDH,'" + DateTime.Now + "','" + DateTime.Now + "' from dbCenter.dbo.WY_XM_JBXX where XMBH='" + XMBH + "'");
        }
        return str.ToString();
    }

    #endregion

    #region 归档扩展信息-NEW

    private static string IntoKZXX(string fbaseinfoid, string fappid, string XMBH)
    {
        StringBuilder str = new StringBuilder();
        str.Append("Insert into WY_XM_KZXX(FID,XMMC,XMBH,ZDMJ,XMDZ,KFDW,JCND,IsZZXQ,JMHS,FJMHS,JZRS,SCJGRQ,WYYFMJ,DC_MJ,DCDT_MJ,GC_MJ,BS_MJ,BG_MJ,SY_MJ,GY_MJ,QT_MJ,LTCW_MJ,LTCW_GS,DXCW_MJ,DXCW_GS,ZXC_MJ,ZXC_SF,DPC_SF,RYPZ_JL,RYPZ_XZ,RYPZ_KF,RYPZ_AQ,RYPZ_WX,RYPZ_BJ,RYPZ_LH,RYPZ_QT,AFXT,CRKGS,JKXFSGS,DTGS,PDFS,FDJS,YGGDH,DJBPGS,ECGSSXS,HFCS,YGGSH,MJJKXTTS,XFXTTS,TCGLXTTS,SJMJ,HTBH,HTSSRQ,HTJZRQ,GLRYGS,MapX,MapY,JGRQ,FTime,FSystemId,FCreateTime,FBaseInfoID,FCreateUser,FIsDeleted,SourceAppID,HSTypeID) ");
        str.Append("select NewID(),XMMC,'" + XMBH + "',ZDMJ,XMDZ,KFDW,JCND,IsZZXQ,JMHS,FJMHS,JZRS,SCJGRQ,WYYFMJ,DC_MJ,DCDT_MJ,GC_MJ,BS_MJ,BG_MJ,SY_MJ,GY_MJ,QT_MJ,LTCW_MJ,LTCW_GS,DXCW_MJ,DXCW_GS,ZXC_MJ,ZXC_SF,DPC_SF,RYPZ_JL,RYPZ_XZ,RYPZ_KF,RYPZ_AQ,RYPZ_WX,RYPZ_BJ,RYPZ_LH,RYPZ_QT,AFXT,CRKGS,JKXFSGS,DTGS,PDFS,FDJS,YGGDH,DJBPGS,ECGSSXS,HFCS,YGGSH,MJJKXTTS,XFXTTS,TCGLXTTS,SJMJ,HTBH,HTSSRQ,HTJZRQ,GLRYGS,MapX,MapY,JGRQ,'" + DateTime.Now + "',FSystemId,FCreateTime,'" + fbaseinfoid + "',FCreateUser,FIsDeleted,'" + fappid + "',HSTypeID from YW_WY_XM_KZXX where FAppID='" + fappid + "'");
        return str.ToString();
    }

    #endregion

    #region 归档合同备案-NEW
    private static string IntoHTBA(string fbaseinfoid, string fappid, string XMBH)
    {
        StringBuilder str = new StringBuilder();
        str.Append("Insert into WY_XM_HTBA(FID,XMBH,HTBH,XMHQFS,HTSXRQ,HTJZRQ,HTQDRQ,WTDW,JGRQ,FCreateUser,FCreateTime,FTime,ZStatus,FBaseInfoID,FSystemID,FIsDeleted,SourceAppID) ");
        str.Append("select NewID(),'" + XMBH + "',HTBH,XMHQFS,HTSXRQ,HTJZRQ,HTQDRQ,WTDW,JGRQ,FCreateUser,FCreateTime,'" + DateTime.Now + "',ZStatus,'" + fbaseinfoid + "',FSystemID,FIsDeleted,'" + fappid + "' from YW_WY_XM_HTBA where FAppID='" + fappid + "'");
        return str.ToString();
    }

    #endregion

    #region 归档财务情况-NEW

    private static string IntoCWQK(string fbaseinfoid, string fappid)
    {
        StringBuilder str = new StringBuilder();
        str.Append("Insert into WY_XM_CWQK(FID,XMBH,WYFZE,WYFSFL,TCF,GGF,QT,YYCB,YYLR,ND,FCreateID,FCreateUser,FCreateTime,FUpdateUser,FTime,ZStatus,FBaseInfoID,FSystemID,FIsDeleted,SourceAppID) ");
        str.Append("select NewID(),XMBH,WYFZE,WYFSFL,TCF,GGF,QT,YYCB,YYLR,ND,FCreateID,FCreateUser,FCreateTime,FUpdateUser,'" + DateTime.Now + "',ZStatus,'" + fbaseinfoid + "',FSystemID,FIsDeleted,'" + fappid + "' from YW_WY_XM_CWQK where FAppID='" + fappid + "' ");
        return str.ToString();
    }

    #endregion

    #region 归档业主委员会备案-NEW

    private static string IntoYWHBA(string fbaseinfoid, string fappid, string XMBH)
    {
        StringBuilder str = new StringBuilder();
        str.Append("Insert into WY_XM_YZWYHBA(FID,XMBH,YZDHMC,YZDHCLSJ,YZWYHMC,YZWYHCLSJ,YZWYHBGDZ,YCXYZDHYZS,YZDBDHRS,SCYZDBDHZKSJ,SCYZDBDHCXRS,BZ,FBaseInfoID,FSystemID,FCreateUser,FCreateTime,FUpdateUser,FTime,FIsDeleted,SourceAppID) ");
        str.Append("select NewID(),'" + XMBH + "',YZDHMC,YZDHCLSJ,YZWYHMC,YZWYHCLSJ,YZWYHBGDZ,YCXYZDHYZS,YZDBDHRS,SCYZDBDHZKSJ,SCYZDBDHCXRS,BZ,'" + fbaseinfoid + "',FSystemID,FCreateUser,FCreateTime,FUpdateUser,'" + DateTime.Now + "',FIsDeleted,'" + fappid + "' from YW_WY_XM_YZWYHBA where FAppID='" + fappid + "'");
        return str.ToString();
    }

    #endregion

    #region 归档项目其他信息-NEW

    private static string IntoQTXX(string fbaseinfoid, string fappid, string XMBH)
    {
        StringBuilder str = new StringBuilder();
        str.Append("Insert into WY_XM_XMQTXX(FID,XMMC,XMBH,AreaID,XMJLID,XMLX,ZDMJ,JZMA,XMDZ,KFDW,JCND,IsZZXQ,JMHS,FJMHS,JZRS,SCJGRQ,WYYFMJ,DC_MJ,DC_SF,DCDT_MJ,DCDT_SF,GC_MJ,GC_SF,BS_MJ,BS_SF,BG_MJ,BG_SF,SY_MJ,SY_SF,GY_MJ,GY_SF,QT_MJ,QT_SF,LTCW_MJ,LTCW_SF,LTCW_GS,DXCW_MJ,DXCW_SF,DXCW_GS,ZXC_MJ,ZXC_SF,DPC_SF,RYPZ_JL,RYPZ_XZ,RYPZ_KF,RYPZ_AQ,RYPZ_WX,RYPZ_BJ,RYPZ_LH,RYPZ_QT,AFXT,CRKGS,JKXFSGS,DTGS,PDFS,FDJS,YGGDH,DJBPGS,ECGSSXS,HFCS,YGGSH,MJJKXTTS,XFXTTS,TCGLXTTS,SJMJ,HTBH,HTSSRQ,HTJZRQ,GLRYGS,MapX,MapY,Del,FCreateID,FCreateUser,FCreateTime,FUpdateID,FUpdateUser,FTime,ZStatus,FBaseInfoID,FSystemID,FIsDeleted,SourceAppID) ");
        str.Append("select NewID(),XMMC,'" + XMBH + "',AreaID,XMJLID,XMLX,ZDMJ,JZMA,XMDZ,KFDW,JCND,IsZZXQ,JMHS,FJMHS,JZRS,SCJGRQ,WYYFMJ,DC_MJ,DC_SF,DCDT_MJ,DCDT_SF,GC_MJ,GC_SF,BS_MJ,BS_SF,BG_MJ,BG_SF,SY_MJ,SY_SF,GY_MJ,GY_SF,QT_MJ,QT_SF,LTCW_MJ,LTCW_SF,LTCW_GS,DXCW_MJ,DXCW_SF,DXCW_GS,ZXC_MJ,ZXC_SF,DPC_SF,RYPZ_JL,RYPZ_XZ,RYPZ_KF,RYPZ_AQ,RYPZ_WX,RYPZ_BJ,RYPZ_LH,RYPZ_QT,AFXT,CRKGS,JKXFSGS,DTGS,PDFS,FDJS,YGGDH,DJBPGS,ECGSSXS,HFCS,YGGSH,MJJKXTTS,XFXTTS,TCGLXTTS,SJMJ,HTBH,HTSSRQ,HTJZRQ,GLRYGS,MapX,MapY,Del,FCreateID,FCreateUser,FCreateTime,FUpdateID,FUpdateUser,'" + DateTime.Now + "',ZStatus,'" + fbaseinfoid + "',FSystemID,FIsDeleted,'" + fappid + "' from YW_WY_XM_XMQTXX where FAppID='" + fappid + "'");
        return str.ToString();
    }

    #endregion

    #region 归档业委会成员-NEW

    private static string IntoYWHCY(string fbaseinfoid, string fappid, string XMBH)
    {
        StringBuilder str = new StringBuilder();
        str.Append("Insert into WY_XM_YZWYHCY(FID,XMBH,XM,XB,NL,SFZH,ZZMM,YZWYHZW,LXDH,GZDW,JTDZ,FBaseInfoID,FSystemID,FCreateUser,FCreateTime,FUpdateUser,FTime,FIsDeleted,SourceAppID) ");
        str.Append("select NewID(),'" + XMBH + "',XM,XB,NL,SFZH,ZZMM,YZWYHZW,LXDH,GZDW,JTDZ,'" + fbaseinfoid + "',FSystemID,FCreateUser,FCreateTime,FUpdateUser,'" + DateTime.Now + "',FIsDeleted,'" + fappid + "' from YW_WY_XM_YZWYHCY where FAppID='" + fappid + "'");
        return str.ToString();
    }

    #endregion

    #region 归档人员信息-NEW

    private static string IntoRYJBXX(string fbaseinfoid, string fappid, string XMBH)
    {
        StringBuilder str = new StringBuilder();
        str.Append("Insert into WY_RY_JBXX(FID,XMBH,fEntName,fzzjgdm,fPersonName,fCardID,fSex,fBirthday,fMZ,fAddress,fPosition,fbysj,fbyzsh,fMajor,flxdz,fSchool,fyzbm,fdzyx,fbgdh,fCardType,fgrdh,fphoto,fState,fTechnical,fzcqdsj,fzczsh,fxl,fxw,fxwzsh,fMemo,fFifteenCardID,fSourceID,fBaseInfoID,FSystemID,FCreateUser,FCreateTime,FTime,SourceAppID,FIsDeleted) ");
        str.Append("select NewID(),'" + XMBH + "',fEntName,fzzjgdm,fPersonName,fCardID,fSex,fBirthday,fMZ,fAddress,fPosition,fbysj,fbyzsh,fMajor,flxdz,fSchool,fyzbm,fdzyx,fbgdh,fCardType,fgrdh,fphoto,fState,fTechnical,fzcqdsj,fzczsh,fxl,fxw,fxwzsh,fMemo,fFifteenCardID,fSourceID,'" + fbaseinfoid + "',FSystemID,FCreateUser,FCreateTime,'" + DateTime.Now + "','" + fappid + "',FIsDeleted from YW_WY_RY_JBXX where FAppID='" + fappid + "'");
        return str.ToString();
    }

    #endregion

    #region  归档楼盘表数据-NEW

    private static string IntoLPB(string fbaseinfoid, string fappid, string XMBH)
    {
        StringBuilder str = new StringBuilder();
        //归档楼幢信息
        str.Append("Insert into WY_XM_LZXX(BuildId,FBaseInfoID,BuildName,ZTS,ZJZMJ,XMBH,SourceAppID,FTime,FID) ");
        str.Append("Select BuildId,'" + fbaseinfoid + "',BuildName,ZTS,ZJZMJ,'" + XMBH + "','" + fappid + "','" + DateTime.Now + "',FID ");
        str.Append("From YW_WY_XM_LZXX where FAppID='" + fappid + "'and BuildId not in (select BuildId from WY_XM_LZXX)");

        str.Append(";");

        //归档房屋信息
        str.Append("Insert into WY_XM_FWXX(HouseId,BuildId,SourceAppID,ZH,FH,DY,RHC,SH,JZMJ,FTime,FID) ");
        str.Append("Select HouseId,BuildId,'" + fappid + "',ZH,FH,DY,RHC,SH,JZMJ,'" + DateTime.Now + "',FID ");
        str.Append("From YW_WY_XM_FWXX where FAppID='" + fappid + "'and HouseId not in (select HouseId from WY_XM_FWXX)");
        return str.ToString();
    }

    #endregion
}