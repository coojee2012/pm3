using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Approve.Common;
using System.Text;
using System.Data;
using Approve.EntityBase;
using Approve.RuleCenter;
using System.Collections;
using ProjectData;
using RuleAppCode;
using Tools;
using BCACOMLib;
/// <summary>
///Share 的摘要说明
/// </summary>
public class ShareTool
{



    public ShareTool()
    {

        //
        //TODO: 在此处添加构造函数逻辑
        //
    }




    /// <summary>
    /// 从系统类型中匹配对应的菜单角色（针对企业）
    /// </summary>
    /// <param name="sysId"></param>
    /// <returns></returns>
    private string getRoleMemu(string sysId)
    {
        string str = "";
        if (!string.IsNullOrEmpty(sysId))
        {
            Hashtable ht = new Hashtable();
            ht.Add("175", "1655");//质量检测机构
            ht.Add("150", "6601");//安全生产
            ht.Add("220", "1720");//三类人员

            str = ht[sysId].ToString();
        }
        return str;
    }

    /// <summary>
    /// 从统计认证编号得到系统编号
    /// </summary>
    /// <param name="shareId"></param>
    /// <returns></returns>
    private string getSysId(string shareId)
    {
        Share sh = new Share();
        return sh.GetSignValue(EntityTypeEnum.EsSystemName, "FNumber", "FNumber=" + shareId + "");
    }

    /// <summary>
    /// 从统计认证编号得到密钥 
    /// </summary>
    /// <param name="shareId"></param>
    /// <returns></returns>
    public string getSysKey(string shareId)
    {
        Share sh = new Share();
        string str = "";
        if (!string.IsNullOrEmpty(shareId))
            str = sh.GetSignValue(EntityTypeEnum.EsSystemName, "FShareKey", "FNumber=" + shareId + "");
        return str;
    }

    #region 同步用户等基本信息
    /// <summary>
    /// 下载同步数
    /// </summary>
    /// <param name="LoginName">登录名</param>
    /// <param name="EntType">用户类型</param>
    /// <param name="LoginType">用户方式1锁，2用户名，</param>
    /// <returns></returns>
    public bool DownloadFromMarket(string LoginName, int EntType, int LoginType)
    {
        try
        {
            Share sh = new Share();
            if (EConvert.ToString(sh.GetSysObjectContent("_sys_Market")) == "1")//开启同步
            {
                ProjectDB db = new ProjectDB();
                //根据用户名去找用户信息。勘察单位

                cn.gov.scjst.zw.JSTJKWebService ws = new cn.gov.scjst.zw.JSTJKWebService();
                string rn = string.Empty;

                //string sCon = "fsystemId=" + GetSys();

                //    sCon += " and FEntName like '%" + t_FName.Text.Trim() + "%' ";


                //勘察单位、设计单位155,
                int FSystemId = EntType;
                if (EntType == 15501)//下载155的用户
                {
                    FSystemId = 1550;
                }
                string sql = "  Ftype=2 and FSystemId=" + FSystemId + " ";
                SortedList sl = new SortedList();
                sl.Add("FSystemId", FSystemId);

                if (LoginType == 1)
                {
                    sql += " and FLockNumber='" + LoginName + "' ";
                    sl.Add("FLockNumber", LoginName);
                }
                else
                {
                    sql += " and FName='" + LoginName + "' ";
                    sl.Add("FName", LoginName);
                }
                //DataSet ds = ccds.GetDs(SecurityEncryption.DESEncrypt(sql), SerializeFunction.SerializeObject(sl));
                //if (ds != null && ds.Tables.Count > 0)
                //{
                string interfaceUserName = sh.GetSysObjectContent("_InterfaceUserName");
                string interfacePassword = sh.GetSysObjectContent("_InterfacePassword");
                DataTable dt = ws.GetTABLE(interfaceUserName, interfacePassword, sql, 0, "帐号表（网站）", out rn); ;
                if (dt != null && dt.Rows.Count > 0)
                {
                    DownloadOneUser(EntType, db, dt.Rows[0]);
                    return true;
                }
                //}

            }
            else
            {
                DataLog.Write(LogType.Info, LogSort.System, "同步没有关联的数据", "LoginName=" + LoginName + ",EntType=" + EntType + ",LoginType=" + LoginType);
                return false;
            }
            return false;
        }
        catch (Exception ex)
        {
            DataLog.Write(LogType.Error, LogSort.System, "同步时出错", ex.ToString());
            return false;
        }
        return true;
    }

    public void DownloadSingleEnt(string FBaseInfoId)
    {
        ProjectDB db = new ProjectDB();
        var result = db.CF_Sys_User.Where(t => t.FBaseInfoId == FBaseInfoId).Select(t => new { t.FID, t.FSystemId }).FirstOrDefault();
        if (result != null)
        {
            DownloadSingleUser(result.FID, EConvert.ToInt(result.FSystemId));
        }
        db.SubmitChanges();
    }

    public bool DownloadSingleUser(string FUserId, int EntType)
    {
        try
        {
            ProjectDB db = new ProjectDB();
            if (EConvert.ToString(db.getSysObjectContent("_sys_Market")) == "1")//开启同步
            {

                //根据用户名去找用户信息。勘察单位

                string rn = string.Empty;

                //勘察单位、设计单位155,
                int FSystemId = EntType;
                if (EntType == 1550)//厅标准库的勘察编码要转换成15501
                {
                    EntType = 15501;
                }
                string sql = " FId='" + FUserId + "' and FSystemId=" + EntType;

                DataTable dt = GetTABLE(sql, "帐号表（网站）", out rn); ;
                if (dt != null && dt.Rows.Count > 0)
                {
                    DownloadOneUser(EntType, db, dt.Rows[0]);
                }
            }
            else
            {
                DataLog.Write(LogType.Info, LogSort.System, "同步没有关联的数据", "FUserId=" + FUserId + ",EntType=" + EntType);
                return false;
            }
        }
        catch (Exception ex)
        {
            DataLog.Write(LogType.Error, LogSort.System, "同步时出错", ex.ToString());
            return false;
        }
        return true;
    }
    private void DownloadOneUser(int EntType, ProjectDB db, DataRow dr)
    {

        bool isUpdate = false;
        string FBaseInfoId = EConvert.ToString(dr["FBaseInfoId"]);
        string FCompany = EConvert.ToString(dr["FCompany"]);
        string rn = string.Empty;
        string sCon = " FId='" + FBaseInfoId + "'";
        //string sql = " select * from CF_Ent_BaseInfo where FID=@FId ";
        SortedList sl = new SortedList();
        sl.Add("FID", FBaseInfoId);
        DataTable dtEnt = GetTABLE(sCon, "企业基本信息（网站）", out rn);

        string FJuridcialCode = "";

        if (dtEnt != null && dtEnt.Rows.Count > 0)
        {
            FJuridcialCode = EConvert.ToString(dtEnt.Rows[0]["FJuridcialCode"]);
        }
        Tools.pageTool tool = new Tools.pageTool(new System.Web.UI.Page());

        CF_Sys_User user = db.CF_Sys_User.Where(u => u.FBaseInfoId == FBaseInfoId).FirstOrDefault();//验证BaseinfoId
        if (user == null)
        {
            if (!string.IsNullOrEmpty(FJuridcialCode))
            {
                user = db.CF_Sys_User.Where(u => u.FJuridcialCode == FJuridcialCode && u.FSystemId == EntType.ToString()).FirstOrDefault();//验证组织机构代码
                if (user == null)
                {
                    if (!string.IsNullOrEmpty(FCompany))
                    {
                        user = db.CF_Sys_User.Where(u => u.FCompany == FCompany && u.FSystemId == EntType.ToString()).FirstOrDefault();//验证企业名称
                        if (user == null)
                        { }
                        else
                        {
                            isUpdate = true;
                        }
                    }
                }
                else
                {
                    isUpdate = true;
                }
            }
        }
        else
        {
            isUpdate = true;
        }

        //开始同步数据。
        if (isUpdate)
        {
            user.FCompany = EConvert.ToString(dr["FCompany"]);
            user.FTime = DateTime.Now;
        }
        else
        {
            user = new CF_Sys_User()
            {
                FID = EConvert.ToString(dr["FID"]),
                FBaseInfoId = FBaseInfoId,
                FLockLabelNumber = EConvert.ToString(dr["FLockLabelNumber"]),
                FLockNumber = EConvert.ToString(dr["FLockNumber"]),
                FPassWord = SecurityEncryption.DESEncrypt(Share.UncrypString(EConvert.ToString(dr["FPassWord"]))),
                FJuridcialCode = tool.staticStringbSubstring(FJuridcialCode, 10),
                FLinkMan = EConvert.ToString(dr["FLinkMan"]),
                FTel = EConvert.ToString(dr["FTel"]),
                FType = 2,
                FCompany = EConvert.ToString(dr["FCompany"]),
                FCreateTime = DateTime.Now,
                FTime = DateTime.Now,
                FIsDeleted = 0,
                FState = 1,
                FManageDeptId = EConvert.ToInt(dr["FManageDeptId"]),
                FIsUserName = 1,
                FMenuRoleId = db.CF_Sys_Role.Where(t => t.FSystemId == EntType && t.FTypeId == 2).Select(t => t.FNumber).FirstOrDefault().ToString(),
                FName = EConvert.ToString(dr["FName"]),
                FSystemId = EntType.ToString()

            };

            if (EConvert.ToDateTime(dr["FEndTime"]) > new DateTime(1900, 1, 1))
            {
                user.FBeginTime = EConvert.ToDateTime(dr["FBeginTime"]);
            }
            if (EConvert.ToDateTime(dr["FEndTime"]) > new DateTime(1900, 1, 1))
            {
                user.FEndTime = EConvert.ToDateTime(dr["FEndTime"]);
            }

            user.FTime = DateTime.Now;
            db.CF_Sys_User.InsertOnSubmit(user);
        }
        //user.FEmail = EConvert.ToString(dr["FEmail"]);
        //user.FLicence = EConvert.ToString(dr["FLicence"]);
        if (!string.IsNullOrEmpty(EConvert.ToString(dr["FAddress"])))
        {
            user.FAddress = EConvert.ToString(dr["FAddress"]);
        }
        if (user.FManageDeptId.ToString().Length >= 4)
        {
            user.FSTAddress = user.FManageDeptId.ToString().Substring(0, 4);
        }
        else
        {
            user.FSTAddress = "5101,5103,5104,5105,5106,5107,5108,5109,5110,5111,5113,5114,5115,5116,5117,5118,5119,5120,5132,5133,5134";
        }

        string NewBaseInfoId = "";
        //开始分配权限
        string FQurl = db.CF_Sys_SystemName.Where(t => t.FNumber == EntType).Select(t => t.FQUrl).FirstOrDefault();
        //if (EntType == 155)
        //{

        //    //string[] strArray = FQurl.Split(',');
        //    //for (int i = 0; i < strArray.Length; i++)
        //    //{
        //    //    string NewBaseInfoId = FBaseInfoId;

        //    //    //UpdateRight(db, user, dt, EConvert.ToInt(strArray), NewBaseInfoId);
        //    //}
        //    NewBaseInfoId = UpdateRight(db, user, dr, 1553, null);
        //}
        //else if (EntType == 15501)
        //{
        //    NewBaseInfoId = UpdateRight(db, user, dr, 1554, null);
        //}
        //else
        //{
        int TrueSystemId = 0;
        if (EntType == 155)
        {
            TrueSystemId = 1553;
        }
        else if (EntType == 15501)
        {
            TrueSystemId = 1554;
        }
        else if (EntType == 145)
        {
            TrueSystemId = 1451;
        }
        else
        {
            return;
        }
        NewBaseInfoId = UpdateRight(db, user, dr, TrueSystemId, FBaseInfoId);
        //更新CA信息CAMMKH, string CASZZSBH)
        UpdateCA(db, user, EConvert.ToString(dr["CAMMKH"]), EConvert.ToString(dr["CASZZSBH"]));
        //下载企业基本信息
        //}
        if (!string.IsNullOrEmpty(NewBaseInfoId))
        {
            UpdateEnt(db, dtEnt, EntType, FBaseInfoId, NewBaseInfoId);

            UpdateEmp(db, FBaseInfoId, NewBaseInfoId, EntType);
            string FCompanyId = EConvert.ToString(dr["FCompanyId"]);
            if (!string.IsNullOrEmpty(FCompanyId))
            {
                //DataTable dtUser = GetTABLE(" FCompanyId='" + FCompanyId + "' ", "帐号表（网站）", out rn);
                UpdateEmp(db, FCompanyId, NewBaseInfoId, EntType);
            }
        }
        db.SubmitChanges();
    }
    private string UpdateEnt(ProjectDB dc, DataTable dtEnt, int EntType, string FBaseInfoId, string NewFBaseInfoId)
    {

        if (dtEnt != null && dtEnt.Rows.Count > 0)
        {
            string sql = "";
            string rn = string.Empty;
            string sCon = "";

            SortedList sl = new SortedList();

            CF_Ent_BaseInfo ent = dc.CF_Ent_BaseInfo.Where(r => r.FId == NewFBaseInfoId).FirstOrDefault();

            if (ent == null)
            {
                ent = new CF_Ent_BaseInfo()
                {
                    FId = NewFBaseInfoId,
                    FSystemId = EntType,
                    FIsDeleted = false,
                    FCreateTime = DateTime.Now,

                };
                //得到法人
                //sql = " select FName from CF_Ent_Leader where FBaseInfoId=@FBaseInfoId and FPersonTypeId=1 ";

                //sl.Add("FBaseInfoId", EConvert.ToString(dtEnt.Rows[0]["FId"]));

                rn = string.Empty;
                sCon = " FBaseInfoId=@FBaseInfoId and FPersonTypeId=1 ";

                //DataSet ds = ccds.GetDs(SecurityEncryption.DESEncrypt(sql), SerializeFunction.SerializeObject(sl));
                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{

                //    ent.FOTxt5 = EConvert.ToString(ds.Tables[0].Rows[0]["FName"]);//法人
                //}

                dc.CF_Ent_BaseInfo.InsertOnSubmit(ent);




            }
            ent.FEntCode = EConvert.ToString(dtEnt.Rows[0]["FEntCode"]);
            Tools.pageTool tool = new Tools.pageTool(new System.Web.UI.Page());
            ent.FEntCode = tool.staticStringbSubstring(ent.FEntCode, 12);

            ent.FJuridcialCode = EConvert.ToString(dtEnt.Rows[0]["FJuridcialCode"]);

            ent.FJuridcialCode = tool.staticStringbSubstring(ent.FJuridcialCode, 10);

            ent.FName = EConvert.ToString(dtEnt.Rows[0]["FName"]);
            ent.FUpDeptId = EConvert.ToInt(dtEnt.Rows[0]["FUpDeptId"]);
            ent.FLicence = tool.staticStringbSubstring(EConvert.ToString(dtEnt.Rows[0]["FLicence"]), 15);


            if (EConvert.ToString(dtEnt.Rows[0]["FRegistDeptId"]).Length < 4)
            {
                if (ent.FLicence.Length > 6)
                {
                    string TrueDept = ent.FLicence.Substring(0, 6);
                    if (dc.ManageDept.Where(t => t.FNumber.ToString() == TrueDept).Any())
                    {
                        ent.FRegistDeptId = EConvert.ToInt(TrueDept);
                    }
                }
            }
            else
            {
                ent.FRegistDeptId = EConvert.ToInt(dtEnt.Rows[0]["FRegistDeptId"]);
            }
            if (!string.IsNullOrEmpty(EConvert.ToString(dtEnt.Rows[0]["FRegistAddress"])))
            {
                ent.FRegistAddress = EConvert.ToString(dtEnt.Rows[0]["FRegistAddress"]);
            }
            ent.FLinkMan = EConvert.ToString(dtEnt.Rows[0]["FLinkMan"]);
            ent.FMobile = EConvert.ToString(dtEnt.Rows[0]["FMobile"]);
            ent.FTel = EConvert.ToString(dtEnt.Rows[0]["FTel"]);
            ent.FEmail = EConvert.ToString(dtEnt.Rows[0]["FEmail"]);
            ent.FTime = DateTime.Now;
            ent.FState = EConvert.ToInt(dtEnt.Rows[0]["FState"]);
            ent.FOTxt5 = EConvert.ToString(dtEnt.Rows[0]["FDDBR"]);//法人
            ent.FCall = EConvert.ToString(dtEnt.Rows[0]["FFax"]);

            //下载证书

            UpdateCerti(dc, FBaseInfoId, NewFBaseInfoId, EntType);


        }
        return NewFBaseInfoId;
    }
    #region 更新证书
    public void UpdateCerti(string FUserId, string NewFBaseInfoId)
    {
        ProjectDB db = new ProjectDB();


        CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == FUserId).FirstOrDefault();
        if (user != null)
        {
            UpdateCerti(db, user.FBaseInfoId, user.FBaseInfoId, EConvert.ToInt(user.FSystemId));
            db.SubmitChanges();
        }
    }
    private void UpdateCerti(ProjectDB dc, string FBaseInfoId, string NewFBaseInfoId, int EntType)
    {
        string rn = string.Empty;
        string sCon = " FBaseInfoId='" + FBaseInfoId + "' and FIsDeleted=0 and FIsValid=1 ";

        //string sql = " select * from CF_Ent_QualiCerti where FBaseInfoId=@FBaseInfoId and FIsDeleted=0 and FIsValid=1 ";
        //SortedList sl = new SortedList();
        //sl.Add("FBaseInfoId", FBaseInfoId);
        DataTable dtCerti = GetTABLE(sCon, "企业证书信息（网站）", out rn);

        for (int i = 0; dtCerti != null && i < dtCerti.Rows.Count; i++)
        {
            string FCertiId = EConvert.ToString(dtCerti.Rows[i]["FId"]);
            CF_Ent_QualiCerti Certi = dc.CF_Ent_QualiCerti.Where(t => t.FCenterId == FCertiId && t.FBaseInfoId == NewFBaseInfoId).FirstOrDefault();
            if (Certi == null)
            {
                Certi = new CF_Ent_QualiCerti()
                {
                    FId = Guid.NewGuid().ToString(),
                    FBaseInfoId = NewFBaseInfoId,

                    FIsDeleted = false,
                    FCreateTime = DateTime.Now,
                    FSystemId = EntType,

                    FCenterId = FCertiId
                };
                dc.CF_Ent_QualiCerti.InsertOnSubmit(Certi);
            }
            Certi.FCertiType = EConvert.ToInt(dtCerti.Rows[i]["FCertiType"]);
            Certi.FCertiNo = EConvert.ToString(dtCerti.Rows[i]["FCertiNo"]);
            Certi.FAppDeptId = EConvert.ToInt(dtCerti.Rows[i]["FAppDeptId"]);
            Certi.FAppDeptName = EConvert.ToString(dtCerti.Rows[i]["FAppDeptName"]);
            Certi.FLevelName = EConvert.ToString(dtCerti.Rows[i]["FLevelName"]);
            Certi.FLevelId = EConvert.ToString(dtCerti.Rows[i]["FLevelId"]);
            Certi.FLevel = EConvert.ToInt(dtCerti.Rows[i]["FLevelId"]);
            Certi.FContent = EConvert.ToString(dtCerti.Rows[i]["FContent"]);
            Certi.FIsValid = EConvert.ToInt(dtCerti.Rows[i]["FIsValid"]);
            Certi.FIsTemp = EConvert.ToInt(dtCerti.Rows[i]["FIsTemp"]);
            Certi.FEntName = EConvert.ToString(dtCerti.Rows[i]["FEntName"]);
            Certi.FType = EConvert.ToInt(dtCerti.Rows[i]["FType"]);
            Certi.FRemark = EConvert.ToString(dtCerti.Rows[i]["FRemark"]);
            if (EConvert.ToDateTime(dtCerti.Rows[i]["FBeginTime"]) > new DateTime(1900, 1, 1))
            {
                Certi.FBeginTime = EConvert.ToDateTime(dtCerti.Rows[i]["FBeginTime"]);
            }
            if (EConvert.ToDateTime(dtCerti.Rows[i]["FEndTime"]) > new DateTime(1900, 1, 1))
            {
                Certi.FEndTime = EConvert.ToDateTime(dtCerti.Rows[i]["FEndTime"]);
            }
            if (EConvert.ToDateTime(dtCerti.Rows[i]["FAppTime"]) > new DateTime(1900, 1, 1))
            {
                Certi.FAppTime = EConvert.ToDateTime(dtCerti.Rows[i]["FAppTime"]);
            }
            Certi.FTime = DateTime.Now;
            Certi.FIsDeleted = false;
            //下载资质
            rn = string.Empty;
            sCon = " FCertiId='" + FCertiId + "' and FIsDeleted=0 ";

            //sql = " select * from CF_Ent_QualiCertiTrade where FCertiId=@FCertiId and FIsDeleted=0   ";
            //sl = new SortedList();
            //sl.Add("FCertiId", FCertiId);

            DataTable dtTrade = GetTABLE(sCon, "企业资质信息（网站）", out rn);

            for (int j = 0; dtTrade != null && j < dtTrade.Rows.Count; j++)
            {
                string TradeId = EConvert.ToString(dtTrade.Rows[j]["FId"]);
                CF_Ent_QualiCertiTrade Trade = dc.CF_Ent_QualiCertiTrade.Where(t => t.FCenterId == TradeId && t.FBaseInfoId == NewFBaseInfoId).FirstOrDefault();
                if (Trade == null)
                {
                    Trade = new CF_Ent_QualiCertiTrade()
                    {
                        FId = Guid.NewGuid().ToString(),
                        FCertiId = Certi.FId,
                        FBaseInfoId = NewFBaseInfoId,


                        FCreateTime = DateTime.Now,
                        FIsDeleted = false,

                        FCenterId = TradeId
                    };


                    dc.CF_Ent_QualiCertiTrade.InsertOnSubmit(Trade);
                }
                Trade.FListId = EConvert.ToInt(dtTrade.Rows[j]["FListId"]);
                Trade.FListName = EConvert.ToString(dtTrade.Rows[j]["FListName"]);
                Trade.FTypeId = EConvert.ToInt(dtTrade.Rows[j]["FTypeId"]);
                Trade.FTypeName = EConvert.ToString(dtTrade.Rows[j]["FTypeName"]);
                Trade.FLevelId = EConvert.ToInt(dtTrade.Rows[j]["FLevelId"]);
                Trade.FLevelName = EConvert.ToString(dtTrade.Rows[j]["FLevelName"]);
                Trade.FLeadId = EConvert.ToString(dtTrade.Rows[j]["FLeadId"]);
                Trade.FLeadName = EConvert.ToString(dtTrade.Rows[j]["FLeadName"]);
                Trade.FState = EConvert.ToInt(dtTrade.Rows[j]["FState"]);
                Trade.FIsBase = EConvert.ToInt(dtTrade.Rows[j]["FIsBase"]);
                Trade.FAppDeptId = EConvert.ToInt(dtTrade.Rows[j]["FAppDeptId"]);
                Trade.FAppDeptName = EConvert.ToString(dtTrade.Rows[j]["FAppDeptName"]);

                Trade.FIsTemp = EConvert.ToInt(dtTrade.Rows[j]["FIsTemp"]);
                Trade.FContent = EConvert.ToString(dtTrade.Rows[j]["FContent"]);
                Trade.FRemark = EConvert.ToString(dtTrade.Rows[j]["FRemark"]);
                Trade.FOrder = EConvert.ToInt(dtTrade.Rows[j]["FOrder"]);
                if (EConvert.ToDateTime(dtTrade.Rows[j]["FAppTime"]) > new DateTime(1900, 1, 1))
                {
                    Trade.FAppTime = EConvert.ToDateTime(dtTrade.Rows[j]["FAppTime"]);
                }
                Trade.FTime = DateTime.Now;
                if (Trade.FTypeId == 134001)//工程勘察
                {
                    //复制新的;
                    //if (string.IsNullOrEmpty(NewFBaseInfoId))
                    //{

                    //    NewFBaseInfoId = FCertiId;
                    //if (EntType == 15501)
                    //{
                    //    ent.FId = NewFBaseInfoId;
                    //}
                    //    else
                    //    {
                    //        CF_Ent_BaseInfo NewEnt = dc.CF_Ent_BaseInfo.Where(r => r.FId == NewFBaseInfoId).FirstOrDefault();
                    //        if (NewEnt == null)
                    //        {
                    //            NewEnt = new CF_Ent_BaseInfo();

                    //            dc.CF_Ent_BaseInfo.InsertOnSubmit(NewEnt);
                    //        }
                    //        NewEnt = ent.Copy(NewEnt);
                    //        NewEnt.FId = NewFBaseInfoId;


                    //    }

                    //    Certi.FBaseInfoId = NewFBaseInfoId;
                    //}

                }
                else if (Trade.FTypeId == 134002)//工程设计
                {

                }
                //检测勘察设计资质


            }


        }

    }
    #endregion

    private bool DownLoadCaOne(ProjectDB dc, string FID)
    {
        try
        {
            Share sh = new Share();
            if (EConvert.ToString(sh.GetSysObjectContent("_sys_Market")) == "1")//开启同步
            {

                //根据用户名去找用户信息。勘察单位

                cn.gov.scjst.zw.JSTJKWebService ws = new cn.gov.scjst.zw.JSTJKWebService();
                string rn = string.Empty;

                string sql = "  FID='" + FID + "' ";

                string interfaceUserName = sh.GetSysObjectContent("_InterfaceUserName");
                string interfacePassword = sh.GetSysObjectContent("_InterfacePassword");
                DataTable dt = ws.GetTABLE(interfaceUserName, interfacePassword, sql, 0, "帐号表（网站）", out rn); ;
                if (dt != null && dt.Rows.Count > 0)
                {
                    CF_Sys_User user = dc.CF_Sys_User.Where(u => u.FID == FID).FirstOrDefault();
                    if (user != null)
                    {
                        bool result = false;
                        result = UpdateCA(dc, user, EConvert.ToString(dt.Rows[0]["CAMMKH"]), EConvert.ToString(dt.Rows[0]["CASZZSBH"]));
                        dc.SubmitChanges();
                        return result;
                    }

                }
            }
            else
            {
                DataLog.Write(LogType.Info, LogSort.System, "更新CA没有关联的数据", "UserId=" + FID);
                return false;
            }
            return false;
        }
        catch (Exception ex)
        {
            DataLog.Write(LogType.Error, LogSort.System, "更新CA时出错", ex.ToString());
            return false;
        }
        return true;
    }
    /// <summary>
    /// 更新写入CA
    /// </summary>
    /// <param name="dc"></param>
    /// <param name="item"></param>
    /// <param name="CAMMKH">CA卡号</param>
    /// <param name="CASZZSBH">CA编号</param>
    private bool UpdateCA(ProjectDB dc, CF_Sys_User item, string CAMMKH, string CASZZSBH)
    {
        if (string.IsNullOrEmpty(CAMMKH) || string.IsNullOrEmpty(CASZZSBH))
        {
            return false;
        }
        Tools.pageTool tool = new Tools.pageTool(new System.Web.UI.Page());
        CF_Sys_UserCA ca = dc.CF_Sys_UserCA.Where(t => t.FCACardId == CAMMKH).FirstOrDefault();
        if (ca == null)
        {
            ca = new CF_Sys_UserCA()
            {
                FID = Guid.NewGuid().ToString(),

                FCreateTime = DateTime.Now,
                FIsDeleted = 0,
                FListType = 1,
                FUserID = item.FID,
                FCACardId = CAMMKH,
                FTime = DateTime.Now,
                FCANumber = CASZZSBH,
                FJuridcialCode = tool.staticStringbSubstring(item.FJuridcialCode, 10)
            };

            dc.CF_Sys_UserCA.InsertOnSubmit(ca);
            return true;
        }
        else
        {
            if (ca.FUserID == item.FID)
            {
                ca.FCANumber = CASZZSBH;
                ca.FTime = DateTime.Now;
                return true;
            }
            else
            {
                return false;
                //绑定的用户有问题
            }
        }
    }
    private string UpdateRight(ProjectDB dc, CF_Sys_User item, DataRow rowUsers, int FSystemId, string FBaseinfoID)
    {
        if (string.IsNullOrEmpty(FBaseinfoID))
        {
            FBaseinfoID = Guid.NewGuid().ToString();
        }
        CF_Sys_UserRight right = dc.CF_Sys_UserRight.Where(r => r.FUserId == item.FID && r.FSystemId == FSystemId).FirstOrDefault();
        if (right == null)
        {
            right = new CF_Sys_UserRight()
            {
                FId = Guid.NewGuid().ToString(),
                FUserId = item.FID,
                FBaseinfoID = FBaseinfoID,
                FLockLabelNumber = item.FLockLabelNumber,
                FCreateTime = DateTime.Now,
                FName = item.FName,
                FIsDeleted = false,
                FSystemId = FSystemId,
                FIsUserName = 1,
                FPassWord = item.FPassWord
            };
            right.FLockNumber = item.FLockNumber;
            if (EConvert.ToDateTime(item.FBeginTime) > new DateTime(1900, 1, 1))
            {
                right.FBeginTime = EConvert.ToDateTime(item.FBeginTime);
            }
            if (EConvert.ToDateTime(item.FEndTime) > new DateTime(1900, 1, 1))
            {
                right.FEndTime = EConvert.ToDateTime(item.FEndTime);
            }
            string froleId = ComFunction.GetRoleId(FSystemId.ToString());
            if (!string.IsNullOrEmpty(froleId))
            {
                right.FRoleId = froleId;
                right.FMenuRoleId = "1" + froleId;

            }


            dc.CF_Sys_UserRight.InsertOnSubmit(right);

        }

        right.FTime = DateTime.Now;
        if (!right.FState.HasValue)
        {
            right.FState = 660101;
        }
        if (rowUsers != null)
        {
            int FIsFirst = EConvert.ToInt(rowUsers["FIsFirst"]);
            //switch (FIsFirst)
            //{
            //    case 0:
            //        right.FState = 660101;
            //        break;
            //    case 1:
            //        right.FState = 660102;
            //        break;
            //    case 2:
            //        right.FState = 660103;
            //        break;
            //    case 5:
            //        right.FState = 660104;
            //        break;
            //    case 8:
            //        right.FState = 660105;
            //        break;
            //}
            //有效 660201
            //过期 660202
            //失效 660203 
            switch (FIsFirst)
            {
                case 0:
                case 1:
                case 2:
                    right.FState = 660201;
                    break;
                case 5:
                case 8:
                    right.FState = 660203;
                    break;
            }
            if (item.FEndTime.HasValue && DateTime.Now > item.FEndTime)
            {
                right.FState = 660202;
            }
        }
        //新办企业", "0"));
        //有资质无档案企业", "1"));
        //有档案企业", "2"));
        //注销企业", "5"));
        //测试企业", "8"));
        //CF_Sys_UserRight right1 = dc.CF_Sys_UserRight.Where(r => r.FUserId == item.FID && r.FSystemId == 8003).FirstOrDefault();
        //if (right1 == null)
        //{
        //    right1 = new CF_Sys_UserRight()
        //    {
        //        FId = Guid.NewGuid().ToString(),
        //        FUserId = item.FID,
        //        FYBaseinfoID = Guid.NewGuid().ToString(),
        //        FLockLabelNumber = item.FLockLabelNumber,
        //        FCreateTime = DateTime.Now,
        //        FName = item.FLockLabelNumber,
        //        FIsDeleted = 0,
        //        FSystemId = 8003,
        //    };

        //    dc.CF_Sys_UserRight.InsertOnSubmit(right1);
        //}
        //if (!right1.FState.HasValue)
        //{
        //    right1.FState = 660101;
        //}
        //right1.FLockNumber = item.FLockNumber;
        //if (EConvert.ToDateTime(item.FEndTime) > EConvert.ToDateTime(item.FCAEndTime))
        //{
        //    right1.FEndTime = EConvert.ToDateTime(item.FEndTime);
        //}
        //else
        //{
        //    right1.FEndTime = EConvert.ToDateTime(item.FCAEndTime);
        //}
        //right1.FTime = DateTime.Now;
        return right.FBaseinfoID;

    }
    /// <summary>
    /// 人员ID
    /// </summary>
    /// <param name="FEmpId"></param>
    public void DownloadSingleEmp(string FEmpId, int EntType)
    {
        ProjectDB db = new ProjectDB();


        CF_Emp_BaseInfo emp = db.CF_Emp_BaseInfo.Where(t => t.FId == FEmpId).FirstOrDefault();
        if (emp != null)
        {
            UpdateEmp(db, emp.FBaseInfoID, emp.FBaseInfoID, EntType, emp.FCenterId);
            db.SubmitChanges();
        }
    }
    public void UpdateEmp(string FUserId, string NewFBaseInfoId)
    {
        ProjectDB db = new ProjectDB();

        CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == FUserId).FirstOrDefault();
        if (user != null)
        {
            UpdateEmp(db, user.FBaseInfoId, user.FBaseInfoId, EConvert.ToInt(user.FSystemId));
            db.SubmitChanges();
        }
    }
    private void UpdateEmp(ProjectDB dc, string FBaseInfoId, string NewFBaseInfoId, int EntType)
    {
        UpdateEmp(dc, FBaseInfoId, NewFBaseInfoId, EntType, string.Empty);
    }
    private void UpdateEmp(ProjectDB dc, string FBaseInfoId, string NewFBaseInfoId, int EntType, string FCenterId)
    {
        string rn = string.Empty;
        //查已有的人员
        var result = dc.CF_Emp_BaseInfo.Where(r => r.FBaseInfoID == NewFBaseInfoId && r.FType == 2);
        foreach (CF_Emp_BaseInfo e in result)
        {
            //去查一下有没有这个人
            DataTable dtTemp = GetTABLE(" recordId='" + e.FCenterId + "' ", "勘察设计人员（网站）", out rn);
            if (dtTemp != null && dtTemp.Rows.Count == 0)
            {
                dc.CF_Emp_BaseInfo.DeleteOnSubmit(e);
            }
        }

        string sCon = " enterId='" + FBaseInfoId + "' ";

        if (!string.IsNullOrEmpty(FCenterId))
        {
            sCon = " FID='" + FBaseInfoId + "' ";
        }
        //string sql = "select * from CF_Emp_BaseInfo where FBaseInfoId=@FBaseInfoId";

        //SortedList sl = new SortedList();
        //sl.Add("FBaseInfoId", FBaseInfoId);


        //DataTable dtEmp = GetTABLE(sCon, "人员证书", out rn);

        //DataTable dtEmp = ws.GetTABLE("ztbUser", "!@#ztbUser", sCon, 0, "勘察设计人员（网站）", out rn);
        DataTable dtEmp = GetTABLE(sCon, "勘察设计人员（网站）", out rn);

        sCon = " ";
        DataTable dtDicAll = GetTABLE(sCon, "字典表（网站）", out rn);
        for (int i = 0; dtEmp != null && i < dtEmp.Rows.Count; i++)
        {

            string FEmpId = EConvert.ToString(dtEmp.Rows[i]["recordId"]);
            CF_Emp_BaseInfo emp = dc.CF_Emp_BaseInfo.Where(r => r.FCenterId == FEmpId && r.FBaseInfoID == NewFBaseInfoId).FirstOrDefault();
            bool isUpdate = true;
            if (emp == null)
            {
                emp = new CF_Emp_BaseInfo()
                {
                    FId = Guid.NewGuid().ToString(),
                    FBaseInfoID = NewFBaseInfoId,
                    FIdCard = EConvert.ToString(dtEmp.Rows[i]["identityNumber"]),



                    FCreateTime = DateTime.Now,
                    FIsDeleted = false,
                    FCenterId = FEmpId
                };
                if (EConvert.ToDateTime(dtEmp.Rows[i]["fzTimeEnd"]) > new DateTime(1900, 1, 1))
                {
                    emp.FEndTime = EConvert.ToDateTime(dtEmp.Rows[i]["fzTimeEnd"]);
                }
                if (EConvert.ToDateTime(dtEmp.Rows[i]["fzTimeBegin"]) > new DateTime(1900, 1, 1))
                {
                    emp.FStartTime = EConvert.ToDateTime(dtEmp.Rows[i]["fzTimeBegin"]);
                }

                // , 
                isUpdate = false;
            }
            if (string.IsNullOrEmpty(emp.FUserName))
            {
                emp.FUserName = EConvert.ToString(dtEmp.Rows[i]["FSystemCode"]);
                emp.FPassword = SecurityEncryption.DESEncrypt(Share.UncrypString(EConvert.ToString(dtEmp.Rows[i]["FPassWord"])));
            }
            emp.FName = EConvert.ToString(dtEmp.Rows[i]["personName"]);
            emp.FSex = EConvert.ToString(dtEmp.Rows[i]["sex"]);
            //emp.FIDCardTypeId = EConvert.ToInt(dtEmp.Rows[i]["FIDCardTypeId"]);
            //emp.FState = EConvert.ToInt(dtEmp.Rows[i]["FState"]);
            emp.FTime = DateTime.Now;
            //emp.FCertiNo = EConvert.ToString(dtEmp.Rows[i]["certificate"]);
            //if (string.IsNullOrEmpty(emp.FCertiNo))
            //{
            emp.FCertiNo = EConvert.ToString(dtEmp.Rows[i]["registCertificate"]);
            //}
            //emp.FRemark = EConvert.ToString(dtEmp.Rows[i]["FRemark"]);
            //emp.FRegistSpecialId = EConvert.ToString(dtEmp.Rows[i]["workType"]);

            DataTable dtTemp = GetTABLE("recordId='" + FEmpId + "'", "人员证书", out rn);
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                emp.FPrintNo = EConvert.ToString(dtTemp.Rows[0]["yzcertificate"]);
                //emp.FSealNo = EConvert.ToString(dtTemp.Rows[i]["FSealNo"]);
                emp.FRegistSpecialId = EConvert.ToString(dtTemp.Rows[0]["workType"]);
            }

            //emp.FResume = EConvert.ToString(dtEmp.Rows[i]["FResume"]);

            //emp.FOrder = EConvert.ToInt(dtEmp.Rows[i]["FOrder"]);
            //

            emp.FState = 6;
            //执行注册类型
            if (dc.Dic.Where(t => t.FNumber == EConvert.ToInt("123000" + dtEmp.Rows[i]["business"])).Any())
            {
                emp.FType = 2;
            }
            else
            {
                emp.FType = 3;
            }
            emp.FPersonTypeId = EConvert.ToInt("123000" + dtEmp.Rows[i]["business"]);

            //职称

            //dtDic = dtDicAll.Select(sCon);
            emp.FTechId = dc.Dic.Where(t => t.FName == EConvert.ToString(dtEmp.Rows[i]["technical"]) && t.FParentId == 108)
                              .Select(t => t.FNumber).FirstOrDefault();
            if (isUpdate)
            { }
            else
            {
                dc.CF_Emp_BaseInfo.InsertOnSubmit(emp);
            }

        }

    }

    cn.gov.scjst.zw.JSTJKWebService ws = new cn.gov.scjst.zw.JSTJKWebService();
    public bool DownloadAllDeptUser(string FUserId)
    {
        try
        {
            ProjectDB db = new ProjectDB();
            if (EConvert.ToString(db.getSysObjectContent("_sys_Market")) == "1")//开启同步
            {

                string rn = string.Empty;


                string sql = "  Ftype=1  ";
                if (!string.IsNullOrEmpty(FUserId))
                {
                    sql += " and FId='" + FUserId + "'";
                }
                DataTable dt = GetTABLE(sql, "帐号表（网站）", out rn);
                for (int i = 0; dt != null && i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == EConvert.ToString(dt.Rows[i]["FID"])).FirstOrDefault();
                    if (user == null)
                    {

                        user = new CF_Sys_User()
                       {
                           FID = EConvert.ToString(dr["FID"]),

                           FLockLabelNumber = EConvert.ToString(dr["FLockLabelNumber"]),
                           FLockNumber = EConvert.ToString(dr["FLockNumber"]),

                           FPassWord = SecurityEncryption.DESEncrypt(Share.UncrypString(EConvert.ToString(dr["FPassWord"]))),

                           FLinkMan = EConvert.ToString(dr["FLinkMan"]),
                           FTel = EConvert.ToString(dr["FTel"]),
                           FType = 2,
                           FCompany = EConvert.ToString(dr["FCompany"]),
                           FCreateTime = DateTime.Now,
                           FIsDeleted = 0,
                           FState = 1,
                           FManageDeptId = EConvert.ToInt(dr["FManageDeptId"]),
                           FIsUserName = 1,
                           FName = EConvert.ToString(dr["FName"]),
                           FSystemId = "8015"

                       };
                        if (EConvert.ToDateTime(dr["FEndTime"]) > new DateTime(1900, 1, 1))
                        {
                            user.FBeginTime = EConvert.ToDateTime(dr["FBeginTime"]);
                        }
                        if (EConvert.ToDateTime(dr["FEndTime"]) > new DateTime(1900, 1, 1))
                        {
                            user.FEndTime = EConvert.ToDateTime(dr["FEndTime"]);
                        }

                        user.FTime = DateTime.Now;

                        CF_Sys_UserRight right = db.CF_Sys_UserRight.Where(r => r.FUserId == user.FID && r.FSystemId == EConvert.ToInt(user.FSystemId)).FirstOrDefault();
                        if (right == null)
                        {
                            right = new CF_Sys_UserRight()
                            {
                                FId = Guid.NewGuid().ToString(),
                                FUserId = user.FID,

                                FLockLabelNumber = user.FLockLabelNumber,
                                FCreateTime = DateTime.Now,
                                FName = user.FName,
                                FIsDeleted = false,
                                FSystemId = EConvert.ToInt(user.FSystemId),
                                FIsUserName = 1,
                                FPassWord = user.FPassWord,

                            };
                            right.FLockNumber = user.FLockNumber;
                            if (EConvert.ToDateTime(user.FBeginTime) > new DateTime(1900, 1, 1))
                            {
                                right.FBeginTime = EConvert.ToDateTime(user.FBeginTime);
                            }
                            if (EConvert.ToDateTime(user.FEndTime) > new DateTime(1900, 1, 1))
                            {
                                right.FEndTime = EConvert.ToDateTime(user.FEndTime);
                            }
                            //省级管理部门 903
                            //市级管理部门 816
                            //县级管理部门 813 
                            if (user.FManageDeptId.ToString().Length == 2)
                            {
                                right.FRoleId = "903";
                                right.FMenuRoleId = "1999";
                            }
                            else if (user.FManageDeptId.ToString().Length == 4)
                            {
                                right.FRoleId = "816";
                                right.FMenuRoleId = "1998";
                            }
                            else if (user.FManageDeptId.ToString().Length == 6)
                            {
                                right.FRoleId = "813";
                                right.FMenuRoleId = "1997";
                            }


                            //最大权限（县级） 1997
                            //最大权限（市级） 1998
                            //最大权限（省级） 1999 
                            db.CF_Sys_UserRight.InsertOnSubmit(right);

                        }

                        right.FTime = DateTime.Now;
                        db.CF_Sys_User.InsertOnSubmit(user);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            DataLog.Write(LogType.Error, LogSort.System, "同步时出错", ex.ToString());
            return false;
        }
        return true;
    }



    public bool DownloadAllDeptUser()
    {
        try
        {
            ProjectDB db = new ProjectDB();
            if (EConvert.ToString(db.getSysObjectContent("_sys_Market")) == "1")//开启同步
            {

                string rn = string.Empty;

                string FIDs = "";
                //DataTable dt = ws.GetTABLE("ztbUser", "!@#ztbUser", " ftype = 1 ", 0, "帐号表（网站）", out rn);
                DataTable dt = GetTABLE(" ftype = 1 ", "帐号表（网站）", out rn);
                for (int i = 0; dt != null && i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == EConvert.ToString(dt.Rows[i]["FID"])).FirstOrDefault();
                    if (user == null)
                    {
                        if (EConvert.ToString(dr["FID"]) == "")
                        {
                            return false;
                        }
                        user = new CF_Sys_User()
                        {
                            FID = EConvert.ToString(dr["FID"]),

                            FLockLabelNumber = EConvert.ToString(dr["FLockLabelNumber"]),
                            FLockNumber = EConvert.ToString(dr["FLockNumber"]),
                            FName = EConvert.ToString(dr["FName"]),
                            FPassWord = SecurityEncryption.DESEncrypt(Share.UncrypString(EConvert.ToString(dr["FPassWord"]))),

                            FManageDeptId = EConvert.ToInt(dr["FManageDeptId"]),
                            FState = 1,
                            FType = 1,
                            FIsUserName = 1,
                            FLinkMan = EConvert.ToString(dr["FLinkMan"]),
                            FTel = EConvert.ToString(dr["FTel"]),


                            FCreateTime = DateTime.Now,
                            FIsDeleted = 0,
                            FSystemId = "8015"


                        };
                        if (EConvert.ToDateTime(dr["FBeginTime"]) > new DateTime(1900, 1, 1))
                        {
                            user.FBeginTime = EConvert.ToDateTime(dr["FBeginTime"]);
                        }
                        if (EConvert.ToDateTime(dr["FEndTime"]) > new DateTime(1900, 1, 1))
                        {
                            user.FEndTime = EConvert.ToDateTime(dr["FEndTime"]);
                        }

                        user.FTime = DateTime.Now;

                        CF_Sys_UserRight right = db.CF_Sys_UserRight.Where(r => r.FUserId == user.FID && r.FSystemId == EConvert.ToInt(user.FSystemId)).FirstOrDefault();
                        if (right == null)
                        {
                            right = new CF_Sys_UserRight()
                            {
                                FId = Guid.NewGuid().ToString(),
                                FUserId = user.FID,

                                FLockLabelNumber = user.FLockLabelNumber,
                                FCreateTime = DateTime.Now,
                                FName = user.FName,
                                FIsDeleted = false,
                                FSystemId = EConvert.ToInt(user.FSystemId),
                                FIsUserName = 1,
                                FPassWord = user.FPassWord
                            };
                            right.FLockNumber = user.FLockNumber;
                            if (EConvert.ToDateTime(user.FBeginTime) > new DateTime(1900, 1, 1))
                            {
                                right.FBeginTime = EConvert.ToDateTime(user.FBeginTime);
                            }
                            if (EConvert.ToDateTime(user.FEndTime) > new DateTime(1900, 1, 1))
                            {
                                right.FEndTime = EConvert.ToDateTime(user.FEndTime);
                            }
                            //省级管理部门 903
                            //市级管理部门 816
                            //县级管理部门 813 
                            if (user.FManageDeptId.ToString().Length == 2)
                            {
                                right.FRoleId = "903";
                                right.FMenuRoleId = "1999";
                            }
                            else if (user.FManageDeptId.ToString().Length == 4)
                            {
                                right.FRoleId = "816";
                                right.FMenuRoleId = "1998";
                            }
                            else if (user.FManageDeptId.ToString().Length == 6)
                            {
                                right.FRoleId = "813";
                                right.FMenuRoleId = "1997";
                            }
                            else
                            {
                                right.FRoleId = "903";
                                right.FMenuRoleId = "1999";
                            }

                            //最大权限（县级） 1997
                            //最大权限（市级） 1998
                            //最大权限（省级） 1999 
                            db.CF_Sys_UserRight.InsertOnSubmit(right);

                        }
                        else
                        {
                            right.FSystemId = EConvert.ToInt(user.FSystemId);

                        }

                        right.FTime = DateTime.Now;
                        db.CF_Sys_User.InsertOnSubmit(user);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(user.FPassWord))
                        {
                            string FPassword = "";
                            try
                            {
                                //
                                user.FPassWord = SecurityEncryption.DESEncrypt(Share.UncrypString(EConvert.ToString(dr["FPassWord"])));
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                    }

                    //FIDs += "'" + dt.Rows[i]["FID"] + "',";

                }

                db.SubmitChanges();

            }
        }
        catch (Exception ex)
        {
            DataLog.Write(LogType.Error, LogSort.System, "同步时出错", ex.ToString());
            return false;
        }
        return true;
    }

    public bool DownloadJSDWByUserName(string FName)
    {
        RCenter njs = new RCenter("dbNJS");
        StringBuilder sbNJS = new StringBuilder();
        SortedList sl = new SortedList();

        sbNJS.Append(" select * from xm_jsdw_user where FName=@fname  ");
        sl.Add("fname", FName);

        DataTable dtnjs = njs.GetTable(sbNJS.ToString(), njs.ConvertParameters(sl));
        return DownloadOneJSDW(dtnjs);
    }
    public bool DownloadJSDWAll()
    {
        RCenter njs = new RCenter("dbNJS");
        StringBuilder sbNJS = new StringBuilder();
        SortedList sl = new SortedList();

        sbNJS.Append(" select * from xm_jsdw_user where FName is not null ");


        DataTable dtnjs = njs.GetTable(sbNJS.ToString());
        return DownloadOneJSDW(dtnjs);
    }
    public bool DownloadOneJSDW(DataTable dt)
    {
        string strFName = "";
        try
        {

            ProjectDB db = new ProjectDB();
            Tools.pageTool tool = new Tools.pageTool(new System.Web.UI.Page());

            string AppOpen = "1";

            #region 转入User和UserRight
            DateTime d = DateTime.Now;

            for (int i = 0; dt != null && i < dt.Rows.Count; i++)
            {
                string FID = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["FID"]), 36);
                CF_Sys_User user = db.CF_Sys_User.Where(t => t.FName == EConvert.ToString(dt.Rows[i]["FName"])
                    || t.FID == FID).FirstOrDefault();

                strFName = EConvert.ToString(dt.Rows[i]["FName"]) + "," + EConvert.ToString(dt.Rows[i]["FCompany"]);
                if (user == null)
                {
                    user = new CF_Sys_User();


                    //看是否已经加有这个用户（user表）
                    user.FID = FID;
                    user.FBaseInfoId = user.FID;
                    user.FTime = d;
                    user.FCreateTime = d;
                    user.FIsDeleted = 0;
                    user.FState = 1;
                    user.FType = 2;
                    user.FBeginTime = d;
                    user.FEndTime = AppOpen == "1" ? d.AddYears(1) : d.AddYears(1);
                    user.FSystemId = "100";
                    user.FLockNumber = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["FName"]), 20);
                    user.FLockLabelNumber = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["FName"]), 20);
                    user.FName = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["FName"]), 20);
                    user.FPassWord = SecurityEncryption.DESEncrypt(EConvert.ToString(dt.Rows[i]["FPassWord"]));

                    user.FCompany = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["FCompany"]), 100);
                    user.FCompanyId = "";//关联的FId

                    user.FTel = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["LXDH"]), 13);

                    user.FLicence = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["YYZZZCH"]), 15);
                    user.FIsUserName = 1; // 允许用户名登陆
                    db.CF_Sys_User.InsertOnSubmit(user);

                }
                user.FJuridcialCode = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["ZZJGDM"]), 10);
                if (EConvert.ToInt(dt.Rows[i]["SSQYID"]) > 0)
                {
                    user.FManageDeptId = EConvert.ToInt(dt.Rows[i]["SSQYID"]);
                }
                if (string.IsNullOrEmpty(user.FLinkMan))
                {
                    user.FLinkMan = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["LXR"]), 30);
                }
                if (string.IsNullOrEmpty(user.FAddress))
                {
                    user.FAddress = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["DWDZ"]), 200);
                }
                if (string.IsNullOrEmpty(user.FEmail))
                {
                    user.FEmail = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["DZYX"]), 30);
                }
                if (string.IsNullOrEmpty(user.FMobile))
                {
                    user.FMobile = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["FRSJ"]), 13);
                }
                if (string.IsNullOrEmpty(user.FIdCard))
                {
                    user.FIdCard = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["identityNumber"]), 18);
                }
                CF_Sys_UserRight userRight = db.CF_Sys_UserRight.Where(t => t.FUserId == user.FID && t.FSystemId == 1001).FirstOrDefault();
                if (userRight != null)
                { }
                else
                {
                    //插入到userRight 表
                    userRight = new CF_Sys_UserRight();
                    userRight.FId = Guid.NewGuid().ToString();
                    userRight.FUserId = user.FID;
                    userRight.FBaseinfoID = user.FBaseInfoId;
                    userRight.FState = 660201;
                    userRight.FLockLabelNumber = user.FName;
                    userRight.FLockNumber = user.FName;
                    userRight.FName = user.FName;
                    userRight.FPassWord = user.FPassWord;
                    userRight.FDeptFrom = 1;
                    userRight.FBeginTime = d;
                    userRight.FEndTime = AppOpen == "1" ? d.AddYears(1) : d.AddDays(1);
                    userRight.FCreateTime = d;
                    userRight.FTime = d;
                    userRight.FIsDeleted = false;
                    userRight.FSystemId = 1001;
                    userRight.FIsUserName = 1;
                    db.CF_Sys_UserRight.InsertOnSubmit(userRight);
                }
                CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == userRight.FBaseinfoID || t.FId == FID).FirstOrDefault();
                if (ent != null)
                {
                }
                else
                {
                    ent = new CF_Ent_BaseInfo();
                    ent.FId = userRight.FBaseinfoID;
                    ent.FName = tool.staticStringbSubstring(user.FCompany, 60);

                    //ent.FRemark = t_FRemark.Text.Trim();
                    ent.FSystemId = 100;
                    ent.FCreateTime = d;
                    ent.FState = 0;
                    ent.FTime = d;
                    ent.FIsDeleted = false;
                    db.CF_Ent_BaseInfo.InsertOnSubmit(ent);

                }
                ent.FRegistDeptId = user.FManageDeptId;
                ent.FUpDeptId = ent.FRegistDeptId;
                if (!string.IsNullOrEmpty(EConvert.ToString(dt.Rows[i]["DWDZ"])))
                {
                    ent.FRegistAddress = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["DWDZ"]), 150);
                }
                ent.FEntTypeId =
               db.Dic.Where(t => t.FName.Contains(EConvert.ToString(dt.Rows[i]["DWXZ"])) && t.FParentId == 181).
                     Select(t => t.FNumber).FirstOrDefault();
                ent.FJuridcialCode = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["ZZJGDM"]), 10);
                ent.FOTxt5 = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["FRXM"]), 200);
                ent.FLinkMan = tool.staticStringbSubstring(user.FLinkMan, 20);
                if (string.IsNullOrEmpty(ent.FLinkMan))
                {
                    ent.FLinkMan = tool.staticStringbSubstring(ent.FOTxt5, 20);
                }
                if (string.IsNullOrEmpty(ent.FLinkMan))
                {
                    ent.FLinkMan = "联系人";
                }
                if (string.IsNullOrEmpty(ent.FTel))
                {
                    ent.FTel = tool.staticStringbSubstring(user.FTel, 13);
                }
                ent.FMobile = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["FRSJ"]), 13);
                ent.FLicence = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["YYZZZCH"]), 15);
                ent.FIdCard = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["identityNumber"]), 18);
                ent.FEmail = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["DZYX"]), 30);
                if (string.IsNullOrEmpty(ent.FRemark))
                {
                    ent.FRemark = tool.staticStringbSubstring(EConvert.ToString(dt.Rows[i]["MEMO"]), 100);
                }

            #endregion

                db.SubmitChanges();
            }
            DataLog.Write(LogType.Info, LogSort.System, "同步建设单位成功", "同步建设单位成功");
            return true;
        }
        catch (Exception ex)
        {
            DataLog.Write(LogType.Error, LogSort.System, "同步建设单位时出错", strFName + "\n" + ex.ToString());
            return false;
        }
    }


    public DataTable GetTABLE(string sCon, string source, out string rn)
    {
        Share sh = new Share();
        rn = "";
        if (EConvert.ToString(sh.GetSysObjectContent("_sys_SyncType")) == "1")//同步方式
        {
            string tablename = "";
            if (source == "人员证书")
            {
                tablename = "tPersonCaPrint";
            }
            else if (source == "企业基本信息（网站）")
            {
                tablename = "CF_Ent_BaseInfo";
            }
            else if (source == "企业证书信息（网站）")
            {
                tablename = "CF_Ent_QualiCerti";
            }
            else if (source == "企业资质信息（网站）")
            {
                tablename = "CF_Ent_QualiCertiTrade";
            }
            else if (source == "帐号表（网站）")
            {
                tablename = "CF_Sys_User";
            }
            else if (source == "字典表（网站）")
            {
                tablename = "CF_Sys_Dic";
            }
            else if (source == "字典表（网站）")
            {
                tablename = "cf_emp_baseinfo";
            }
            else
            {

            }
            RCenter jst = new RCenter("dbJST");
            return jst.GetTable("select * from " + tablename + " where " + sCon);
        }
        else
        {

            string interfaceUserName = sh.GetSysObjectContent("_InterfaceUserName");
            string interfacePassword = sh.GetSysObjectContent("_InterfacePassword");


            return ws.GetTABLE(interfaceUserName, interfacePassword, sCon, 0, source, out rn);
        }
    }



    public DataTable GetTABLE(string sCon)
    {
        Share sh = new Share();
        if (EConvert.ToString(sh.GetSysObjectContent("_sys_SyncType")) == "1")//同步方式
        {
            RCenter jst = new RCenter("dbJST");
            return jst.GetTable(sCon);
        }
        else
        {
            return null;
        }
    }
    #endregion

    public BCACOMLib.SecurityEngineV2Class InitBCA()
    {
        BCACOMLib.SecurityEngineV2Class bjca;
        if (HttpContext.Current.Application["bjca"] == null)
        {
            HttpContext.Current.Application.Lock();
            try
            {
                bjca = new BCACOMLib.SecurityEngineV2Class();
                bjca.SetWebAppName("SecXV3COM");
                HttpContext.Current.Application["bjca"] = bjca;
            }
            finally
            {
                HttpContext.Current.Application.UnLock();
            }
        }
        //string strRan, strSignedData, strServerCert;

        //bjca = (BCACOMLib.SecurityEngineV2Class)Application["bjca"];
        //strRan = bjca.GenRandom(24);
        //strSignedData = bjca.SignData(strRan);
        //strServerCert = bjca.GetServerCertificate(2);
        //Session["Ran"] = strRan;
        bjca = (BCACOMLib.SecurityEngineV2Class)HttpContext.Current.Application["bjca"];
        return bjca;

    }

    public string ReadCA(string CaCerti)
    {
        BCACOMLib.SecurityEngineV2Class bjca = InitBCA();
        //Response.Write(bjca.GenRandom(24) + "-Test-<br>"); 
        int ret = bjca.ValidateCert(CaCerti);
        string UserCaId = bjca.GetCertInfoByOid(CaCerti, "1.2.86.11.7.1.8");
        if (ret == 0)
        {

        }
        else if (ret == 4)
        {
            return "不是可信CA颁发的数字证书";
        }
        else
        {
            return string.Empty;
        }
        return UserCaId;
    }
    public string ValidateCA(string FName, string CaCerti, int LoginType)
    {

        try
        {
            ProjectDB db = new ProjectDB();
            if (db.getSysObjectContent("_IsVerifyCA") == "1")
            {
                BCACOMLib.SecurityEngineV2Class bjca = InitBCA();
                //Response.Write(bjca.GenRandom(24) + "-Test-<br>"); 
                int ret = bjca.ValidateCert(CaCerti);
                string UserCaId = bjca.GetCertInfoByOid(CaCerti, "1.2.86.11.7.1.8");
                if (ret == 0)//!string.IsNullOrEmpty(UserCaId))//
                {


                    //Response.Write(UserCaId);//登陆修改 通过表ZZJGAndCAInfo判断CA与组织机构是否绑定
                    if (LoginType == 1)
                    {
                        if (db.CF_Sys_User.Where(t => t.FName == FName && t.FCANumber == UserCaId).Any())
                        {
                            return string.Empty;
                        }
                        else
                        {
                            return "验证唯一标识失败";
                        }
                    }
                    else
                    {
                        if (db.CF_Emp_BaseInfo.Where(t => t.FName == FName && t.FCANumber == UserCaId).Any())
                        {
                            return string.Empty;
                        }
                        else
                        {
                            return "验证唯一标识失败";
                        }
                    }
                }
                else
                {
                    return "验证证书无效";
                }
            }
            return string.Empty;
        }
        catch (Exception exp) { return exp.Message; }

    }


    bool isTry = true;
    public string ValidateCA(string FName, string CaCerti, int LoginType, string fuserid)
    {

        try
        {
            ProjectDB db = new ProjectDB();
            if (db.getSysObjectContent("_IsVerifyCA") == "1")
            {
                BCACOMLib.SecurityEngineV2Class bjca = InitBCA();
                //Response.Write(bjca.GenRandom(24) + "-Test-<br>"); 
                int ret = bjca.ValidateCert(CaCerti);
                string UserCaId = bjca.GetCertInfoByOid(CaCerti, "1.2.86.11.7.1.8");
                if (ret == 0)//!string.IsNullOrEmpty(UserCaId))//
                {


                    //Response.Write(UserCaId);//登陆修改 通过表ZZJGAndCAInfo判断CA与组织机构是否绑定
                    if (LoginType == 1)
                    {
                        if (db.CF_Sys_UserCA.Where(t => t.FUserID == fuserid && t.FCANumber == UserCaId).Any())
                        {
                            return string.Empty;
                        }
                        else
                        {
                            if (isTry)
                            {
                                isTry = false;
                                if (DownLoadCaOne(db, fuserid))
                                {
                                    ValidateCA(FName, CaCerti, LoginType, fuserid);
                                }
                                else
                                {
                                    return "没有绑定CA！";

                                }
                            }
                            else
                            {
                                return "验证唯一标识失败!";
                            }

                        }
                    }
                    else
                    {
                        if (db.CF_Emp_BaseInfo.Where(t => t.FName == FName && t.FCANumber == UserCaId).Any())
                        {
                            return string.Empty;
                        }
                        else
                        {
                            return "验证唯一标识失败!!";
                        }
                    }
                }
                else
                {
                    return "验证证书无效!";
                }
            }
            return string.Empty;
        }
        catch (Exception exp) { return exp.Message; }

    }
}
