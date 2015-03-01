using System;
using System.Collections.Generic;
using System.Text;
using Approve.RuleBase;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.RuleCenter
{
    public class DataEXC
    {
        string type = "Ent";
        public DataEXC()
        {

        }

        /// <summary>
        /// 人员做业务调用
        /// </summary>
        /// <param name="dbType">数字就行</param>
        public DataEXC(int dbType)
        {
            type = "Emp";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DBName"></param>
        /// <returns></returns>
        private RBase DB(string DBName)
        {
            switch (DBName)
            {
                case "RCenter":
                    return new RCenter();
             
                case "dbOA"://oa库
                    return new OA();
                case "dbShare"://share库
                    return new Share();
                default:
                    return new RCenter();
            }

        }

        #region 上报
        /// <summary>
        /// 企业上报业务时，各存一遍centerbackup和qualibackup
        /// </summary>
        /// <param name="appID">当前上报的业务ID</param>
        /// <param name="FLinkId">当前企业FBaseinfoId（或人员的FEmpId）</param>
        /// <returns></returns>
        public bool dEXC_Report(string appID, string FLinkId)
        {
            bool rv = true;
            if (!string.IsNullOrEmpty(appID))
            {
                string appType = string.Empty;
                if (type == "Emp")
                {
                    appType = DB("RQuali").GetSignValue(EntityTypeEnum.EqEeDetail, "FManageTypeId", "FID='" + appID + "'");
                }
                else
                    appType = DB("RQuali").GetSignValue(EntityTypeEnum.EqList, "FManageTypeId", "FID='" + appID + "'");
                if (!string.IsNullOrEmpty(appType))
                {
                    if (ExcQuali_Report(appID, appType))//备份Quali库
                    {
                        //得到Center库当前信息的FAppId
                        string CenterAppId = "";
                        if (type == "Ent")
                            CenterAppId = DB("RCenter").GetSignValue(EntityTypeEnum.EbBaseInfo, "FAppId", "FID='" + FLinkId + "'");
                        else
                            CenterAppId = DB("RCenter").GetSignValue(EntityTypeEnum.EePersonBaseInfo, "FAppId", "FID='" + FLinkId + "'");

                        if (!string.IsNullOrEmpty(CenterAppId))
                        {
                            if (ExcCenter_Report(CenterAppId))//备份Center库
                                rv = false;
                        }
                        else
                            rv = false;
                    }
                    else
                        rv = false;
                }
            }
            return rv;
        }


        /// <summary>
        /// 将Quali库的当前业务信息备份到QualiBackup
        /// </summary>
        /// <param name="appID">当前上报的业务ID</param>
        /// <param name="appType">当前业务类型</param>
        /// <returns></returns>
        private bool ExcQuali_Report(string appID, string appType)
        {
            bool rv = true;
            EntityTypeEnum[] enTable = getTabelListFromAppType(appType);
            int n = 0;
            string fAppListId = appID;
            if (type == "Emp")//如果是人员的话
            {
                fAppListId = DB("RQuali").GetSignValue(EntityTypeEnum.EqEeDetail, "FAppId", "FID='" + appID + "'");
            }
            foreach (EntityTypeEnum tableName in enTable)
            {
                DataTable dt = new DataTable();
                if (tableName == EntityTypeEnum.EqList)
                {
                    if (DB("RQualiBackUp").GetSQLCount("select count(*) from cf_App_list where fid='" + fAppListId + "'") <= 0)
                        dt = DB("RQuali").GetTable(tableName, "FID", "FID ='" + fAppListId + "'");
                    else
                        continue;
                }
                else if (tableName == EntityTypeEnum.EqEeDetail)
                    dt = DB("RQuali").GetTable(tableName, "FID", "FID ='" + appID + "'");
                else
                    dt = DB("RQuali").GetTable(tableName, "FID", "FAppId ='" + appID + "'");
                n += dt.Rows.Count;
            }
            if (n > 0)
            {
                SortedList[] sl = new SortedList[n];
                EntityTypeEnum[] en = new EntityTypeEnum[n];
                string[] fKey = new string[n];
                SaveOptionEnum[] so = new SaveOptionEnum[n];
                n = 0;
                foreach (EntityTypeEnum tableName in enTable)
                {
                    DataTable dt = new DataTable();
                    if (tableName == EntityTypeEnum.EqList)
                    {
                        if (DB("RQualiBackUp").GetSQLCount("select count(*) from cf_App_list where fid='" + fAppListId + "'") <= 0)//如果已经导入过了，就不导入了
                            dt = DB("RQuali").GetTable(tableName, "*", "FID ='" + fAppListId + "'");
                        else
                            continue;
                    }
                    else if (tableName == EntityTypeEnum.EqEeDetail)
                        dt = DB("RQuali").GetTable(tableName, "*", "FID ='" + appID + "'");
                    else
                        dt = DB("RQuali").GetTable(tableName, "*", "FAppId ='" + appID + "'");
                    CopyFiled_Quali(dt, tableName, ref n, ref sl, ref en, ref fKey, ref so);
                }
                if (!DB("RQualiBackUp").SaveEBaseM(en, sl, fKey, so))
                {
                    rv = false;
                }
            }
            return rv;
        }


        /// <summary>
        /// 将Center库的当前业务信息备份到CenterBackup
        /// </summary>
        /// <param name="appID">当前上报的业务ID</param>
        /// <param name="appType">当前业务类型</param>
        /// <returns></returns>
        public bool ExcCenter_Report(string CenterAppId)
        {
            bool rv = true;
            EntityTypeEnum[] enTable = getTabelListEnt();
            int n = 0;
            foreach (EntityTypeEnum tableName in enTable)
            {
                DataTable dt = DB("RCenter").GetTable(tableName, "FID", "FAppId ='" + CenterAppId + "'");
                n += dt.Rows.Count;
            }
            if (n > 0)
            {
                SortedList[] sl = new SortedList[n];
                EntityTypeEnum[] en = new EntityTypeEnum[n];
                string[] fKey = new string[n];
                SaveOptionEnum[] so = new SaveOptionEnum[n];
                n = 0;
                foreach (EntityTypeEnum tableName in enTable)
                {
                    DataTable dt = DB("RCenter").GetTable(tableName, "*", "FAppId ='" + CenterAppId + "'");
                    CopyFiled_Center(dt, tableName, ref n, ref sl, ref en, ref fKey, ref so);
                }
                if (!DB("RCenterBackUp").SaveEBaseM(en, sl, fKey, so))
                {
                    rv = false;
                }
            }
            return rv;
        }


        /// <summary>
        /// 将指定datatable存入sl[]  （Quali库）
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName"></param>
        /// <param name="n"></param>
        /// <param name="sl"></param>
        /// <param name="en"></param>
        /// <param name="fKey"></param>
        /// <param name="so"></param>
        private void CopyFiled_Quali(DataTable dt, EntityTypeEnum tableName, ref int n, ref SortedList[] sl, ref EntityTypeEnum[] en, ref string[] fKey, ref SaveOptionEnum[] so)
        {
            for (int i = 0; i < dt.Rows.Count; i++)//遍历表记录
            {
                sl[n] = new SortedList();
                en[n] = tableName;
                fKey[n] = "FID";
                so[n] = SaveOptionEnum.Insert;
                for (int j = 0; j < dt.Columns.Count; j++)//遍历字段
                {
                    if (dt.Columns[j].ColumnName.ToLower() != "ftime")
                        sl[n].Add(dt.Columns[j].ColumnName, dt.Rows[i][j]);
                }
                n++;
            }
        }

        /// <summary>
        /// 将指定datatable存入sl[]  （Center库）
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName"></param>
        /// <param name="n"></param>
        /// <param name="sl"></param>
        /// <param name="en"></param>
        /// <param name="fKey"></param>
        /// <param name="so"></param>
        private void CopyFiled_Center(DataTable dt, EntityTypeEnum tableName, ref int n, ref SortedList[] sl, ref EntityTypeEnum[] en, ref string[] fKey, ref SaveOptionEnum[] so)
        {
            for (int i = 0; i < dt.Rows.Count; i++)//遍历表记录
            {
                sl[n] = new SortedList();
                en[n] = tableName;
                fKey[n] = "KFID";
                so[n] = SaveOptionEnum.Insert;
                for (int j = 0; j < dt.Columns.Count; j++)//遍历字段
                {
                    if (dt.Columns[j].ColumnName.ToLower() != "ftime")
                        sl[n].Add(dt.Columns[j].ColumnName, dt.Rows[i][j]);
                }
                sl[n].Add("KFID", Guid.NewGuid().ToString());
                n++;
            }
        }

        #endregion



        #region 撤消，打回

        /// <summary>
        /// 撤消上报或打回，删除centerbackup和qualibackup
        /// </summary>
        /// <param name="appID">当前上报的业务ID</param>
        /// <param name="FLinkId">当前企业FBaseinfoId（或人员的FEmpId）</param>
        /// <returns></returns>
        public bool dEXC_GetBack(string appID, string FLinkId)
        {
            bool rv = true;
            if (!string.IsNullOrEmpty(appID))
            {
                string appType = DB("RQuali").GetSignValue(EntityTypeEnum.EqList, "FManageTypeId", "FID='" + appID + "'");
                if (!string.IsNullOrEmpty(appType))
                {
                    if (type == "Emp")//如果是人员，得找到申请信息的FId,然后删除
                    {
                        appID = DB("RQuali").GetSignValue(EntityTypeEnum.EqEeDetail, "FId", "FEmpId='" + FLinkId + "' and FAppId='" + appID + "'");
                    }
                    if (ExcQuali_GetBack(appID, appType))//删除QualiBakUp库
                    {
                        //得到Center库当前信息的FAppId
                        string CenterAppId = "";
                        if (type == "Ent")
                            CenterAppId = DB("RCenter").GetSignValue(EntityTypeEnum.EbBaseInfo, "FAppId", "FID='" + FLinkId + "'");
                        else
                            CenterAppId = DB("RCenter").GetSignValue(EntityTypeEnum.EePersonBaseInfo, "FAppId", "FID='" + FLinkId + "'");

                        if (ExcCenter_GetBack(CenterAppId))//删除CenterBakUp库
                            rv = false;
                    }
                    else
                        rv = false;
                }
            }
            else
                rv = false;

            return rv;
        }

        /// <summary>
        /// 打回，删除相关信息（Quali）
        /// </summary>
        /// <param name="appID">当前上报的业务ID</param>
        /// <param name="appType">当前业务类型</param>
        /// <returns></returns>
        private bool ExcQuali_GetBack(string appID, string appType)
        {
            bool rv = true;
            EntityTypeEnum[] enTable = getTabelListFromAppType(appType);
            StringBuilder sb = new StringBuilder();
            foreach (EntityTypeEnum tableName in enTable)
            {
                BaseTools bt = new BaseTools();
                if (tableName == EntityTypeEnum.EqList)
                {
                    if (type == "Ent")//如果是企业 
                        sb.Append("delete from " + bt.GetName(tableName) + " where FId ='" + appID + "'  ");
                    else
                        continue;
                }
                else if (tableName == EntityTypeEnum.EqEeDetail)
                    sb.Append("delete from " + bt.GetName(tableName) + " where FId ='" + appID + "'  ");
                else
                    sb.Append("delete from " + bt.GetName(tableName) + " where FAppId ='" + appID + "'  ");
            }
            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                if (!DB("RQualiBackUp").PExcute(sb.ToString()))
                    rv = false;
            }
            return rv;
        }

        /// <summary>
        /// 打回，删除相关信息（Center）
        /// </summary>
        /// <param name="appID">当前上报的业务ID</param>
        /// <param name="appType">当前业务类型</param>
        /// <returns></returns>
        private bool ExcCenter_GetBack(string CenterAppId)
        {
            bool rv = true;
            EntityTypeEnum[] enTable = getTabelListEnt();
            StringBuilder sb = new StringBuilder();
            foreach (EntityTypeEnum tableName in enTable)
            {
                BaseTools bt = new BaseTools();
                sb.Append("delete from " + bt.GetName(tableName) + " where FAppId='" + CenterAppId + "' ");
            }
            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                if (!DB("RCenterBackUp").PExcute(sb.ToString()))
                    rv = false;
            }
            return rv;
        }
        #endregion



        #region  办结
        /// <summary>
        /// 企业业务办结时，更新center库FAppId、删除Quali库信息
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="LinkID">当前企业FBaseinfoId（或人员的FEmpId）</param>
        /// <returns></returns>
        public bool dEXC_End(string appID, string LinkID)
        {
            bool rv = true;
            if (!string.IsNullOrEmpty(appID))
            {
                if (type == "Emp")//如果是人员
                {
                    appID = DB("RQuali").GetSignValue(EntityTypeEnum.EqEeDetail, "FId", "FEmpId='" +
LinkID + "' and FAppId='" + appID + "'");
                }
                if (dEXC_EndCenter(appID, LinkID))//更新center库FAppID
                {
                    //得到业务类型编号
                    string appType = DB("RQuali").GetSignValue(EntityTypeEnum.EqList, "FManageTypeId", "FID='" + appID + "'");

                    if (dEXC_EndQuali(appID, appType)) //删除Quali库                
                        rv = false;
                }
                else
                    rv = false;
            }
            return rv;
        }

        /// <summary>
        /// 办结，更新center库FAppID
        /// </summary>
        /// <param name="appID">企业：CF_App_List.FID；人员：CF_Person_AppDetial.FID</param>
        /// <param name="FBaseinfoId"></param>
        /// <returns></returns>
        public bool dEXC_EndCenter(string appID, string LinkId)
        {
            bool rv = true;
            //得到Center库当前信息的旧FAppId
            string OldAppId = "";
            if (type == "Emp")//如果是人员
                OldAppId = DB("RQuali").GetSignValue(EntityTypeEnum.EqEeDetail, "FOldAppId", "FID='" + appID + "'");
            else
                OldAppId = DB("RQuali").GetSignValue(EntityTypeEnum.EqList, "FOldAppId", "FID='" + appID + "'");

            if (!string.IsNullOrEmpty(OldAppId))
            {
                EntityTypeEnum[] enTable = getTabelListEnt();
                StringBuilder sb = new StringBuilder();
                foreach (EntityTypeEnum tableName in enTable)
                {
                    BaseTools bt = new BaseTools();
                    sb.Append("update " + bt.GetName(tableName) + " set FAppId='" + appID + "' where FAppId='" + OldAppId + "' ");
                }
                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    if (!DB("RCenter").PExcute(sb.ToString()))//更新center库FAppID
                        rv = false;
                }
            }
            return rv;
        }
        /// <summary>
        /// 三类人员企业名称变更办结，更新center库FAppID
        /// </summary>
        /// <param name="appID">企业：CF_App_List.FID；人员：CF_Person_AppDetial.FID</param>
        /// <param name="FBaseinfoId"></param>
        /// <returns></returns>
        public bool dEXC_EndCenterEmp(string appID, string OldAppId)
        {
            bool rv = true;
            if (!string.IsNullOrEmpty(OldAppId))
            {
                EntityTypeEnum[] enTable = getTabelListEnt();
                StringBuilder sb = new StringBuilder();
                foreach (EntityTypeEnum tableName in enTable)
                {
                    BaseTools bt = new BaseTools();
                    sb.Append("update " + bt.GetName(tableName) + " set FAppId='" + appID + "' where FAppId='" + OldAppId + "' ");
                }
                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    if (!DB("RCenter").PExcute(sb.ToString()))//更新center库FAppID
                        rv = false;
                }
            }
            return rv;
        }
        /// <summary>
        /// 办结，删除Quali库
        /// </summary>
        /// <param name="appID">当前上报的业务ID</param>
        /// <param name="appType">当前业务类型</param>
        /// <returns></returns>
        public bool dEXC_EndQuali(string appID, string appType)
        {
            bool rv = true;
            EntityTypeEnum[] enTable = getTabelListFromAppType(appType);
            StringBuilder sb = new StringBuilder();
            foreach (EntityTypeEnum tableName in enTable)
            {
                BaseTools bt = new BaseTools();
                if (tableName == EntityTypeEnum.EqList)
                {
                    if (type == "Ent")//如果是企业，就把业务类型设置为办结状态
                    {
                        sb.Append("update " + bt.GetName(tableName) + " set FState=6 where FId ='" + appID + "'  ");
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (tableName == EntityTypeEnum.EqEeDetail)
                    sb.Append("delete from " + bt.GetName(tableName) + " where FId ='" + appID + "'  ");
                else
                    sb.Append("delete from " + bt.GetName(tableName) + " where FAppId ='" + appID + "'  ");
            }
            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                if (!DB("RQuali").PExcute(sb.ToString()))
                    rv = false;
            }
            return rv;
        }

        #endregion


        /// <summary>
        /// 从业务类型(appType)中得到该业务在Quali库所用到的表
        /// </summary>
        /// <param name="appType">业务类型</param>
        /// <returns></returns>
        private EntityTypeEnum[] getTabelListFromAppType(string appType)
        {
            switch (appType)
            {
                //case "":   在此加各业务所用表
                //break;
                default:
                    if (type == "Ent")
                    {
                        EntityTypeEnum[] en = new EntityTypeEnum[17];
                        en[0] = EntityTypeEnum.EqDetail;//申请资质表
                        en[1] = EntityTypeEnum.EqEbChangeRecord;//变更用表
                        en[2] = EntityTypeEnum.EqList; //业务表   
                        en[3] = EntityTypeEnum.EqEeBaseinfo;   //人员基本信息      
                        en[4] = EntityTypeEnum.EqEeResume;    //人员简历
                        en[5] = EntityTypeEnum.EqEbBaseInfo;    //企业基本信息
                        en[6] = EntityTypeEnum.EqEbBaseInfoChange; //企业变更信息
                        en[7] = EntityTypeEnum.EqEbBaseInfoOther; //企业其它基本信息
                        en[8] = EntityTypeEnum.EqEbDevice;     //机械设备
                        en[9] = EntityTypeEnum.EqEbFinance;
                        en[10] = EntityTypeEnum.EqEbLeader;    //主要负责人
                        en[11] = EntityTypeEnum.EqEbProject;    //工程业绩表
                        en[12] = EntityTypeEnum.EqEbProjectOther; //工程业绩其它信息表
                        en[13] = EntityTypeEnum.EqEbQualiCerti;  //资质证书
                        en[14] = EntityTypeEnum.EqEbQualiCertiTrade; //资质序列
                        en[15] = EntityTypeEnum.EqSafetyCerti; //安全生产许可证
                        en[16] = EntityTypeEnum.EqPubText;        //长字符内容信息
                        return en;
                    }
                    else
                    {
                        EntityTypeEnum[] en = new EntityTypeEnum[7];
                        en[0] = EntityTypeEnum.EqList;//业务表
                        en[1] = EntityTypeEnum.EqEeDetail;// 人员基本信息
                        en[2] = EntityTypeEnum.EqEeContinueStudy; //教育情况   
                        en[3] = EntityTypeEnum.EqEeOtherInfo;   //人员其它基本信息      
                        en[4] = EntityTypeEnum.EqEeResume;    //人员简历
                        en[5] = EntityTypeEnum.EqPubText;        //长字符内容信息
                        en[6] = EntityTypeEnum.EqPersonChangeDetial;//企业名称变更的人员记录
                        return en;
                    }

            }

        }


        /// <summary>
        /// Center 企业 要备份的表
        /// </summary>
        /// <returns></returns>
        private EntityTypeEnum[] getTabelListEnt()
        {
            if (type == "Ent")
            {
                EntityTypeEnum[] en = new EntityTypeEnum[16];
                en[0] = EntityTypeEnum.EbBaseInfo;    //企业基本信息
                en[1] = EntityTypeEnum.EbBaseInfoOther; //企业其它基本信息
                en[2] = EntityTypeEnum.EbQualiCerti;  //资质证书
                en[3] = EntityTypeEnum.EbQualiCertiTrade;//资质序列
                en[4] = EntityTypeEnum.EbSafetyCerti; //安全生产许可证
                en[5] = EntityTypeEnum.EeBaseinfo;   //人员基本信息      
                en[6] = EntityTypeEnum.EeResume;     //人员简历
                en[7] = EntityTypeEnum.EbDevice;     //机械设备
                en[8] = EntityTypeEnum.EbFinance;
                en[9] = EntityTypeEnum.EbLeader;    //主要负责人
                en[10] = EntityTypeEnum.EbProject;    //工程业绩表
                en[11] = EntityTypeEnum.EbProjectOther; //工程业绩其它信息表
                en[12] = EntityTypeEnum.EPText;        //长字符内容信息
                en[13] = EntityTypeEnum.EbGoodAction;  //良好行为
                en[14] = EntityTypeEnum.EbBadAction;  //不良行为
                en[15] = EntityTypeEnum.EaAppActionRecord;  //行为记分记录

                return en;
            }
            else
            {
                EntityTypeEnum[] en = new EntityTypeEnum[4];
                en[0] = EntityTypeEnum.EePersonBaseInfo;// 人员基本信息
                en[1] = EntityTypeEnum.EeContinueStudy; //教育情况   
                en[2] = EntityTypeEnum.EeResume;    //人员简历
                en[3] = EntityTypeEnum.EPText;        //长字符内容信息
                return en;
            }
        }
    }
}
