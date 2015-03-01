using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using Approve.RuleCenter;
using System.Data.SqlClient;
using System.Text;

/// <summary>
///DownLoadInfaceWS 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class DownLoadInfaceWS : System.Web.Services.WebService
{
    Share sh = new Share();
    public DownLoadInfaceWS()
    {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }
    string GetSystemsByPlatId(string fplatId)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fnumber from cf_Sys_SystemName where fplatId=@fplatId");
        DataTable dt = sh.GetTable(sb.ToString(), new SqlParameter("@fplatId", fplatId));
        sb.Remove(0, sb.Length);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (sb.Length > 0)
                    sb.Append(",");
                sb.Append(dt.Rows[i]["fnumber"].ToString());
            }
        }
        return sb.ToString();
    }
    /// <summary>
    /// 根据管理部门的权限rightID和系统类型下载用户和权限信息
    /// </summary>
    /// <param name="right"></param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownDeptMentOne(string key, string userID, string fPlamtType)
    {
        if (!string.IsNullOrEmpty(userID))
        {
            string fsystemIds = GetSystemsByPlatId(fPlamtType);
            if (string.IsNullOrEmpty(fsystemIds))
                return null;
            DataTable dt = sh.GetTable(@"select top 1 u.FID uFID,u.FLockNumber uFLockNumber, 
            u.FLockLabelNumber uFLockLabelNumber, 
            u.FName uFName, u.FPassWord uFPassWord, 
            u.FManageDeptId uFManageDeptId,
            u.FBaseInfoId uFBaseInfoId,u.FType uFType,
            u.FBeginTime uFBeginTime,u.FEndTime uFEndTime,
            u.FCompany uFCompany,u.FFunction uFFunction, 
            u.FLinkMan uFLinkMan, u.FTel uFTel,
            u.FState uFState,u.FIsDeleted uFIsDeleted, 
            u.FDepartmentID uFDepartmentID,
            r.FId rFId, r.FBaseInfoId rFBaseInfoId,
            r.FLockNumber rFLockNumber, r.FLockLabelNumber rFLockLabelNumber, 
            r.FName rFName, r.FPassWord rFPassWord,r.FState rFState, 
            r.FBeginTime rFBeginTime, r.FEndTime rFEndTime,r.FSystemId rFSystemId,
            r.FUserId rFUserId,r.FRoleId rFRoleId,r.FMenuRoleId rFMenuRoleId,
            r.FRegistPostCode rFRegistPostCode, r.FCreateUser rFCreateUser,
            r.FAppTypeId rFAppTypeId,r.FDeptFrom rFDeptFrom 
            from cf_Sys_User u inner join CF_Sys_Userright r on u.FId=r.FUserId 
            where u.FID=@userID and r.FSystemId in (@FSystemId)",
            new SqlParameter[] { new SqlParameter("@userID", userID), new SqlParameter("@FSystemId", fsystemIds) });
            if (dt != null && dt.Rows.Count > 0)
            {
                DataSet ds = dt.DataSet;
                return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
            }
        }
        return null;
    }
    /// <summary>
    /// 根据管理部门系统类型下载用户和权限信息
    /// </summary>
    /// <param name="right"></param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownDeptMentAll(string key, string fPlamtType)
    {
        if (!string.IsNullOrEmpty(fPlamtType))
        {
            string fsystemIds = GetSystemsByPlatId(fPlamtType);
            if (string.IsNullOrEmpty(fsystemIds))
                return null;
            DataTable dt = sh.GetTable(@"select u.FID uFID,u.FLockNumber uFLockNumber, 
            u.FLockLabelNumber uFLockLabelNumber, 
            u.FName uFName, u.FPassWord uFPassWord, 
            u.FManageDeptId uFManageDeptId,
            u.FBaseInfoId uFBaseInfoId,u.FType uFType,
            u.FBeginTime uFBeginTime,u.FEndTime uFEndTime,
            u.FCompany uFCompany,u.FFunction uFFunction, 
            u.FLinkMan uFLinkMan, u.FTel uFTel,
            u.FState uFState,u.FIsDeleted uFIsDeleted, 
            u.FDepartmentID uFDepartmentID,
            r.FId rFId, r.FBaseInfoId rFBaseInfoId,
            r.FLockNumber rFLockNumber, r.FLockLabelNumber rFLockLabelNumber, 
            r.FName rFName, r.FPassWord rFPassWord,r.FState rFState, 
            r.FBeginTime rFBeginTime, r.FEndTime rFEndTime,r.FSystemId rFSystemId,
            r.FUserId rFUserId,r.FRoleId rFRoleId,r.FMenuRoleId rFMenuRoleId,
            r.FRegistPostCode rFRegistPostCode, r.FCreateUser rFCreateUser,
            r.FAppTypeId rFAppTypeId,r.FDeptFrom rFDeptFrom 
            from cf_Sys_User u inner join CF_Sys_Userright r on u.FId=r.FUserId where r.FId 
            in (select max(r1.fId) from cf_Sys_User u1 inner join cf_Sys_Userright r1 on r1.fuserId=u1.fid 
            where r1.FSystemId in (@FSystemId) group by u1.fid)", new SqlParameter("@FSystemId", fsystemIds));
            if (dt != null && dt.Rows.Count > 0)
            {
                DataSet ds = dt.DataSet;
                return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
            }
        }
        return null;
    }
    /// <summary>
    /// 下载字典父级编码
    /// </summary>
    /// <param name="right"></param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownDicClassInfo(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"select FID, FName, FTime, FIsDeleted, FSystemId, FNumber, FCNumber, FOrder ");
        sb.Append(" from cf_Sys_DicClass ");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataSet ds = dt.DataSet;
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        }
        return null;
    }
    /// <summary>
    /// 下载字典编码
    /// </summary>
    /// <param name="right"></param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownDicInfo(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"select FID, FName, FNumber, FCNumber, FParentId,");
        sb.Append(" FClassId, FOrder, FLevel, FIsDeleted,");
        sb.Append(" FSystemId, FRemark, FCertiNo, FTime ");
        sb.Append(" from cf_Sys_dic ");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataSet ds = dt.DataSet;
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        }
        return null;
    }
    /// <summary>
    /// 下载角色编码
    /// </summary>
    /// <param name="right"></param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownSysRoleInfo(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"select FId, FIsDeleted, FCreateTime, FTime,
        FName, FNumber, FParentId, FOrder,");
        if (fPlamtType == "100")//如果是基础数据平台
        {
            sb.Append(" 2 FTypeId,");//审核和菜单角色
            sb.Append(" case FTypeId when 1 then 2 when 2 then 1 end FMTypeId ");
        }
        else
            sb.Append("FMTypeId, FTypeId");
        sb.Append(" where FPlatId=@fPlamtType");
        DataTable dt = sh.GetTable(sb.ToString(), new SqlParameter("@fPlamtType", fPlamtType));
        if (dt != null && dt.Rows.Count > 0)
        {
            DataSet ds = dt.DataSet;
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        }
        return null;
    }
    /// <summary>
    /// 下载系统类型编码
    /// </summary>
    /// <param name="right"></param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownSystemNameInfo(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"select FID, FTime, FIsDeleted, FName, FNumber, FShareKey,");
        sb.Append("FOrder, FDesc, FType, FLUrl, FQUrl, FState from cf_Sys_SystemName ");
        sb.Append("where FPlatId=@fPlamtType");
        DataTable dt = sh.GetTable(sb.ToString(), new SqlParameter("@fPlamtType", fPlamtType));
        if (dt != null && dt.Rows.Count > 0)
        {
            DataSet ds = dt.DataSet;
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        }
        return null;
    }
    /// <summary>
    /// 下载业务类型编码
    /// </summary>
    /// <param name="right"></param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownSysManageTypeInfo(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"select FID, FTIME, FISDELETED, FCreateTime, FName, FNumber, FSystemId,");
        sb.Append("FOperDeptName, FLawUrl, FTableUrl, FTypeId, FDesc, FOrder, fday, FMTypeId,");
        sb.Append("FQUrl, FAUrl, FPublicDay, FIsPrint from CF_Sys_ManageType ");
        sb.Append("where 1=1 ");
        if (fPlamtType == "200")
            sb.Append(" and 1=1");
        else
            sb.Append(" and 1=2");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataSet ds = dt.DataSet;
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        }
        return null;
    }
    /// <summary>
    /// 下载资质等级编码
    /// </summary>
    /// <param name="right"></param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownSysQualiLevelInfo(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"select FID, FName, FNumber, FSystemName, FLevel, FDescrible, FTime,");
        sb.Append("FIsDeleted, FSystemId from CF_Sys_QualiLevel ");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataSet ds = dt.DataSet;
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        }
        return null;
    }
    /// <summary>
    /// 下载流程设置
    /// </summary>
    /// <param name="right"></param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownAppProcessInfo(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        DataSet ds = new DataSet();
        sb.Append(@"select FID, FTIME, FIsDeleted, FCreateTime, FManageDeptId,");
        sb.Append("FBaseType, FName, FFullName, FDefineDay, FSystemId, FNumber from cf_App_Process");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataTable dtClone = dt.Copy();
            dtClone.TableName = "cf_app_process";
            ds.Tables.Add(dtClone);
        }
        sb.Remove(0, sb.Length);
        sb.Append(@"select FId, FTime, FCreateTime, FIsDeleted, FProcessId,");
        sb.Append("FManageTypeId from cf_App_ManageType ");
        dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataTable dtClone = dt.Copy();
            dtClone.TableName = "cf_app_managetype";
            ds.Tables.Add(dtClone);
        }
        sb.Remove(0, sb.Length);
        sb.Append(@"select FId, FTime, FCreateTime, FIsDeleted, FQualiTypeId,");
        sb.Append("FProcessId from CF_App_QualiType ");
        dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            dt.TableName = "cf_app_qualitype";
            ds.Tables.Add(dt);
        }
        sb.Remove(0, sb.Length);
        sb.Append(@"select FId, FTime, FCreateTime, FIsDeleted, FLevelId,");
        sb.Append("FProcessId from cf_App_QualiLevel");
        dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataTable dtClone = dt.Copy();
            dtClone.TableName = "cf_app_qualilevel";
            ds.Tables.Add(dtClone);
        }
        sb.Remove(0, sb.Length);
        sb.Append(@"select FID, FTime, FIsDeleted, FCreateTime, FName, FProcessId,");
        sb.Append("FOrder, FLevel, FIsSend, FSendDeptId, FIsEnd, FIsAppEnd, FRoleId,");
        sb.Append("FDefineDay, FDesc, FTypeId, FIsQuali, FIsPrint from cf_App_SubFlow");
        dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataTable dtClone = dt.Copy();
            dtClone.TableName = "cf_app_subflow";
            ds.Tables.Add(dtClone);
        }
        if (ds != null && ds.Tables.Count > 0)
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        return null;
    }
    /// <summary>
    /// 下载节假日编码
    /// </summary>
    /// <param name="right"></param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownSysHolidayInfo(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"select FId, FDate, FIsTrue, FIsDeleted, FTime, FCreateTime from CF_Sys_Holidays");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataSet ds = dt.DataSet;
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        }
        return null;
    }
    /// <summary>
    /// 下载打回意见编码
    /// </summary>
    /// <param name="right"></param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownAppBackIdeaInfo(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"select FId,SystemId, FUserId, FContent, FTime,");
        sb.Append("FIsdeleted, FCreateTime, FType, FOrder from cf_App_BackIdea ");
        sb.Append("where FPlatId=@FPlatId");
        DataTable dt = sh.GetTable(sb.ToString(), new SqlParameter("@fPlamtType", fPlamtType));
        if (dt != null && dt.Rows.Count > 0)
        {
            DataSet ds = dt.DataSet;
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        }
        return null;
    }
    /// <summary>
    /// 下载栏目菜单编码
    /// </summary>
    /// <param name="right"></param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownSysTreeInfo(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"select FID, FName, FNumber, FParent, FLevel, FOrder, FAdminUrl,");
        sb.Append(" FWebUrl, FDisType, FTarget, FType, FCreateTime, FTime, FIsDeleted,");
        sb.Append(" FLinkMan, FClass, FPicName, FSelcePicName, FExpPicName, FIsShow,");
        sb.Append(" FWebListUrl, FKindId from cf_Sys_Tree ");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataSet ds = dt.DataSet;
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        }
        return null;
    }
    /// <summary>
    /// 下载系统菜单编码
    /// </summary>
    /// <param name="key">密码锁</param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownSysMenuInfo(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"select FID, FNumber, FParentId, FUrl, FName, FLevel-1 FLevel,");
        sb.Append(" FSelcePicName, FPicName, FOrder, FRoleId, FManageTypeId, FTarget,");
        sb.Append(" FQUrl, FIsDis, FSystemId, FTime, FCreateTime, FIsDeleted, FIsShow,");
        sb.Append(" FType from cf_Sys_Menu where 1=1 ");
        if (fPlamtType == "200")//行政审核系统 
            sb.Append(" and (FNumber like '400%' or FNumber like '403%' or FNumber like '453%')");
        else
            sb.Append(" and 1=2 ");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataSet ds = dt.DataSet;
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        }
        return null;
    }
    /// <summary>
    /// 下载政府部门编码
    /// </summary>
    /// <param name="right"></param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownSysDeptInfo(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"select FID, FName, FNumber, FLevel, FClassNumber, FParentId, FFullName,");
        sb.Append("FCNumber, FIsTown, FTime, FCreateTime, FIsDeleted from cf_Sys_ManageDept ");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataSet ds = dt.DataSet;
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        }
        return null;
    }
    /// <summary>
    /// 下载行政部门编码
    /// </summary>
    /// <param name="right"></param>
    /// <param name="?"></param>
    /// <returns>加密后的数据集</returns>
    [WebMethod]
    public byte[] DownSysDepartmentInfo(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"select FID,FName,FNumber,FLevel,FClassNumber,FParentId,FFullName,");
        sb.Append("FCNumber,FIsDeleted,FSystemId,FIsTown,FCreateTime,FTime from CF_Sys_Department");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataSet ds = dt.DataSet;
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        }
        return null;
    }


    /// <summary>
    /// 下载系统菜单
    /// </summary>
    /// <param name="FSystemId">系统类型</param>
    /// <returns></returns>
    [WebMethod]
    public byte[] DownMenu(string key, string FSystemId)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FID, FNumber, FParentId, FUrl, FName, FLevel, FSelcePicName,");
        sb.Append("FPicName, FOrder, FRoleId, FManageTypeId, FTarget, FQUrl, FIsDis,");
        sb.Append("FSystemId, FTime, FCreateTime, FIsDeleted, FIsShow, FType ");
        sb.Append("from CF_Sys_Menu where FSystemId=@FSystemId ");
        DataTable dt = sh.GetTable(sb.ToString(), new SqlParameter("@FSystemId", FSystemId));
        if (dt != null && dt.Rows.Count > 0)
        {
            DataSet ds = dt.DataSet;
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        }
        return null;
    }


    /// <summary>
    /// 下载菜单角色
    /// </summary>
    /// <param name="FPlatId">系统类型</param>
    /// <param name="FType">角色类型（菜单角色、审核角色... ）</param>
    /// <returns></returns>
    [WebMethod]
    public byte[] DownMenuRole(string key, string FPlatId, string FTypeId)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FId, FIsDeleted, FCreateTime, FTime, FName, FNumber, FParentId, FOrder, FMTypeId, FTypeId, FPlatId, FSystemId ");
        sb.Append("from CF_Sys_Role where FPlatId=@FPlatId and FTypeId=@FTypeId ");
        SortedList sl = new SortedList();
        sl.Add("FPlatId", FPlatId);
        sl.Add("FTypeId", FTypeId);
        DataTable dt = sh.GetTable(sb.ToString(), sh.ConvertParameters(sl));
        if (dt != null && dt.Rows.Count > 0)
        {
            DataSet ds = dt.DataSet;
            return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
        }
        return null;
    }
    /// <summary>
    /// 下载用户
    /// </summary>
    /// <param name="FSystemId">系统类型</param>
    /// <returns></returns>
    [WebMethod]
    public byte[] DownUser(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        DataSet ds = new DataSet();
        sb.Append("select FID, FBaseInfoId, FTime, FIsDeleted, FCreateTime, FLockNumber, FLockLabelNumber,");
        sb.Append("FName, FPassword, FManageDeptId, FState, FType, FBeginTime, FEndTime,");
        sb.Append("FRoleId, FCompany, FFunction, FLinkMan, FTel, FBatchId,FCanMod,");
        sb.Append("FMenuRoleId,FJuridcialCode,FIsUserName,FOrg,FPope,FDepartmentId,FSystemId ");
        sb.Append("from CF_Sys_User u where 1=1 ");
        if (fPlamtType == "200")//行政审核 
            sb.Append(" and (ftype=2 and fsystemId<>260) or (ftype=2 and fsystemId=260 and exists (select fid from cf_Sys_Userright where fuserId=u.FId and fsystemId=101)) "); //所有的企业和有行政审核权限的管理部门
        else if (fPlamtType == "260")//有施工许可权限的企业 
            sb.Append(" and ftype=2 and exists (select fid from cf_Sys_Userright where fuserId=u.FId and fsystemId=260)");
        else
            sb.Append(" and 1=2");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataTable dtClone = dt.Copy();
            dtClone.TableName = "cf_sys_user";
            ds.Tables.Add(dtClone);
        }
        sb.Remove(0, sb.Length);
        sb.Append("select fid,FBaseInfoId,FName,FPassWord,FLockLabelNumber,FLockNumber,");
        sb.Append("FUserId,FRegistPostCode,FDeptFrom,FBeginTime,FEndTime,FSystemId,");
        sb.Append("FState,FTime,FCreateTime,FIsDeleted,FIsUserName,FIsPub,FCanMod,");
        sb.Append("(select top 1 fnumber from cf_sys_role where ftypeId=1 and fsystemId=r.fsystemId)FRoleId,(select top 1 fnumber from cf_sys_role where ftypeId=2 and fsystemId=r.fsystemId)FMenuRoleId ");
        sb.Append("from CF_Sys_Userright r where 1=1 ");
        if (fPlamtType == "200")//行政审核 
            sb.Append(" and fsystemId<>'260' ");
        else if (fPlamtType == "260")//施工许可 
            sb.Append(" and fsystemId='260' ");

        sb.Append("and fuserId in ( ");
        sb.Append("select fid from CF_Sys_User u where 1=1 ");
        if (fPlamtType == "200" || fPlamtType == "260")//行政审核、施工许可
            sb.Append(" and ftype=2 ");
        sb.Append(" )");
        dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataTable dtClone = dt.Copy();
            dtClone.TableName = "cf_sys_userright";
            ds.Tables.Add(dtClone);
        }
        //主管部门
        sb.Remove(0, sb.Length);
        sb.Append("select u.FManageDeptId,u.FType,u.FCompany,u.FFunction,u.FLinkMan,u.FTel,u.FBatchId,");
        sb.Append(" r.fid,r.FName,r.FPassWord,r.FLockLabelNumber,r.FLockNumber,r.FBeginTime,r.FEndTime,");
        sb.Append(" r.FSystemId,r.FState,r.FTime,r.FCreateTime,r.FIsDeleted,");
        sb.Append(" r.FIsUserName,r.FIsPub,r.FCanMod,r.FRoleId,r.FMenuRoleId ");
        sb.Append(" from CF_Sys_User u inner join cf_Sys_Userright r on u.FId=r.FUserId ");
        if (fPlamtType == "200")//行政审核 
            sb.Append(" and r.FSystemId=701 ");
        dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataTable dtClone = dt.Copy();
            dtClone.TableName = "cf_sys_user_manager";
            ds.Tables.Add(dtClone);
        }
        return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
    }
    /// <summary>
    /// 下载标准
    /// </summary>
    /// <param name="fPlamtType">平台</param>
    /// <returns></returns>
    [WebMethod]
    public byte[] DownStand(string key, string fPlamtType)
    {
        StringBuilder sb = new StringBuilder();
        DataSet ds = new DataSet();
        sb.Append("select FId,FSystemId,FQUALIFICATIONID,FCONTENT,FNUMBER,FLEVEL,FISLEAF,FISDELETED,FLEVELGROUP,FStandardTypeId,FOrder,FLevelGroupStr from CF_Sys_ProjectType u where 1=1 ");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataTable dtClone = dt.Copy();
            dtClone.TableName = "cf_sys_projecttype";
            ds.Tables.Add(dtClone);
        }
        sb.Remove(0, sb.Length);
        sb.Append("select fid,ffqualificationid,flevelid,fttypeid,fnumber,flevel,fisleaf,frelation,");
        sb.Append("ftargetunit,ftargetvalue,fneedcount,foption,fcondition,");
        sb.Append("fisdeleted,fname,fkind,fbz,fsystemid ");
        sb.Append(" from cf_sys_appstand where 1=1 ");
        dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataTable dtClone = dt.Copy();
            dtClone.TableName = "cf_sys_appstand";
            ds.Tables.Add(dtClone);
        }
        sb.Remove(0, sb.Length);
        sb.Append("select fid,FName,foperation,FOrAnd,FOrder,ftarget,FUnit,fdescription,ffqualificationid,");
        sb.Append("fstandandardtypeid,flevelid,fisdeleted,ftypeid,FLevel,FBZ,fsystemid ");
        sb.Append(" from CF_Sys_CheckPara where 1=1 ");
        dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataTable dtClone = dt.Copy();
            dtClone.TableName = "cf_sys_checkpara";
            ds.Tables.Add(dtClone);
        }
        return KCDataFormatter.GetBinaryFormatDataCompress(key, ds);
    }
}

