using System.Text;
using System.Collections;
using System.Data;
using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using Approve.RuleBase;
using Approve.EntityCenter;
using Approve.PersistBase;
using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.EntitySys;
 
using System.Xml;
namespace Approve.RuleApp
{
    public class RAppBacth : RBase
    {
        RCenter rc = new RCenter();
        public RAppBacth()
        {
            this.pDBName = "dbCenter";
            BaseTools baseTools = new BaseTools();
            this.baseTools = baseTools;
        }

        //gzxz
        public DataTable getBacthList(string FSystemId, string FDeptId, string fManageTypeId)
        {
            string fManageName = rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber=" + fManageTypeId);
            if (fManageName == null || fManageName == "")
            {
                return null;
            }

            DataTable dt = null;

            StringBuilder sb = new StringBuilder();

            //sb.Append(" select fid ");
            //sb.Append(" from CF_App_BatchNo ");
            //sb.Append(" where FSystemID ='" + FSystemId + "'");
            //sb.Append(" and FDFId='" + FDeptId + "'");
            //sb.Append(" and FManageTypeId=" + fManageTypeId);
            //sb.Append(" and fisdeleted=0 ");
            //sb.Append(" and fstate<>1 ");
            //DataTable dt = this.GetTable(sb.ToString());

            //int iCount = dt.Rows.Count;
            ////对所有未板结批次进行检查处理
            //for (int i = 0; i < iCount; i++)
            //{
            //    //提取该批次中已经板结，同时必须该批次已经包含的有业务的批次
            //    string FAppNoId = dt.Rows[i]["fid"].ToString();
            //    sb.Remove(0, sb.Length);
            //    sb.Append("select count(*) from ");
            //    sb.Append(" CF_App_BatchNo pn,CF_App_ProcessPublic pp ");
            //    sb.Append(" where pn.fid=pp.fbatchnoid   ");
            //    sb.Append(" and pn.fid='" + FAppNoId + "'"); 
            //    int itemp = this.GetSQLCount(sb.ToString());
            //    if (itemp > 0)
            //    {
            //        //如果该批次已经全部完成，则把该批次设置为板结
            //        this.PExcute("update CF_App_BatchNo set fstate=1 where fid='" + FAppNoId + "' ");
            //    }
            //}

            //重新检查批次如果不够三条，则添加新批次
            sb.Remove(0, sb.Length);
            sb.Append(" select count(1) ");
            sb.Append(" from CF_App_BatchNo ");
            sb.Append(" where FSystemID ='" + FSystemId + "'");
            sb.Append(" and FDFId='" + FDeptId + "'");
            sb.Append(" and FManageTypeId=" + fManageTypeId);
            sb.Append(" and fisdeleted=0 ");
            sb.Append(" and fstate<>1 ");
            int iCount = this.GetSQLCount(sb.ToString());

            //每次保证有三个批次没有板结
            iCount = 3 - iCount;
            if (iCount > 0)
            {
                sb.Remove(0, sb.Length);
                sb.Append(" select max(FNumber) from CF_App_BatchNo ");
                sb.Append(" where FDFId='" + FDeptId + "'");
                sb.Append(" and FSystemID='" + FSystemId + "'");
                sb.Append(" and FManageTypeId=" + fManageTypeId);
                sb.Append(" and fnumber like '" + DateTime.Now.Year.ToString() + fManageTypeId + "%'");
                string fNo = this.GetSignValue(sb.ToString());
                if (fNo != null)
                {
                    fNo = (EConvert.ToInt(fNo)).ToString();
                }
                else
                {
                    fNo = DateTime.Now.Year.ToString() + fManageTypeId + "000";
                }

                SortedList sl = new SortedList();
                for (int i = 0; i < iCount; i++)
                {
                    sl.Clear();
                    sl.Add("FID", Guid.NewGuid().ToString());
                    fNo = (EConvert.ToInt(fNo) + 1).ToString();
                    string FName = DateTime.Now.Year.ToString() + "年" + fManageName + "第" + EConvert.ToInt(fNo.Substring(7, 3)) + "批";
                    sl.Add("FIsDeleted", 0);
                    sl.Add("FCreateTime", DateTime.Now);
                    sl.Add("FDFId", FDeptId);
                    sl.Add("FNumber", fNo);
                    sl.Add("FManageTypeId", fManageTypeId);
                    sl.Add("FTtile", FName);
                    sl.Add("FYear", DateTime.Now.Year);
                    sl.Add("FSystemID", FSystemId);
                    sl.Add("fstate", 0);
                    this.SaveEBase(EntityTypeEnum.EaBatchNo, sl, "FID", SaveOptionEnum.Insert);
                }
            }

            sb.Remove(0, sb.Length);
            sb.Append(" select fid,FTtile ");
            sb.Append(" from CF_App_BatchNo ");
            sb.Append(" where FSystemID ='" + FSystemId + "'");
            sb.Append(" and FDFId='" + FDeptId + "'");
            sb.Append(" and FManageTypeId=" + fManageTypeId);
            sb.Append(" and fisdeleted=0 ");
            sb.Append(" and fstate<>1 order by fnumber desc ");
            dt = this.GetTable(sb.ToString());
            return dt;
        }

        public DataTable getBacthList(string FSystemId, string FDeptId, string fManageTypeId, int iState)
        {
           string fManageName = rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber=" + fManageTypeId);
            if (fManageName == null || fManageName == "")
            {
                return null;
            }

            DataTable dt = null;

            StringBuilder sb = new StringBuilder();

            sb.Append(" select fid ");
            sb.Append(" from CF_App_BatchNo ");
            sb.Append(" where FSystemID ='" + FSystemId + "'");
            sb.Append(" and FDFId='" + FDeptId + "'");
            sb.Append(" and FManageTypeId=" + fManageTypeId);
            sb.Append(" and fisdeleted=0 ");
            sb.Append(" and fstate<>1 ");
            dt = this.GetTable(sb.ToString());

            int iCount = dt.Rows.Count;
            //对所有未板结批次进行检查处理
            for (int i = 0; i < iCount; i++)
            {
                //提取该批次中已经板结，同时必须该批次已经包含的有业务的批次


                string FAppNoId = dt.Rows[i]["fid"].ToString();
                //sb.Remove(0, sb.Length);
                //sb.Append("select count(*) from ");
                //sb.Append(" CF_App_BatchNo pn,CF_App_ProcessPublic pp ");
                //sb.Append(" where pn.fid=pp.fbatchnoid   ");
                //sb.Append(" and pn.fid='" + FAppNoId + "'");

                sb.Remove(0, sb.Length);
                sb.Append("select count(*) from cf_app_appbatchno pn,cf_app_processinstance pp ");
                sb.Append(" where  pn.fappid=pp.fid and pp.fstate=6 ");
                sb.Append(" and pn.FBatchNoId='" + FAppNoId + "'");

                int itemp = this.GetSQLCount(sb.ToString());
                if (itemp > 0)
                {
                    //如果该批次已经全部完成，则把该批次设置为板结
                    this.PExcute("update CF_App_BatchNo set fstate=1 where fid='" + FAppNoId + "' ");
                }
            }

            //重新检查批次如果不够三条，则添加新批次
            sb.Remove(0, sb.Length);
            sb.Append(" select count(1) ");
            sb.Append(" from CF_App_BatchNo ");
            sb.Append(" where FSystemID ='" + FSystemId + "'");
            sb.Append(" and FDFId='" + FDeptId + "'");
            sb.Append(" and FManageTypeId=" + fManageTypeId);
            sb.Append(" and fisdeleted=0 ");
            sb.Append(" and fstate<>1 ");
            iCount = this.GetSQLCount(sb.ToString());

            //每次保证有三个批次没有板结
            iCount = 3 - iCount;
            if (iCount > 0)
            {
                sb.Remove(0, sb.Length);
                sb.Append(" select max(FNumber) from CF_App_BatchNo ");
                sb.Append(" where FDFId='" + FDeptId + "'");
                sb.Append(" and FSystemID='" + FSystemId + "'");
                sb.Append(" and FManageTypeId=" + fManageTypeId);
                sb.Append(" and fnumber like '" + DateTime.Now.Year.ToString() + fManageTypeId + "%'");
                string fNo = this.GetSignValue(sb.ToString());
                if (fNo != null)
                {
                    fNo = (EConvert.ToInt(fNo)).ToString();
                }
                else
                {
                    fNo = DateTime.Now.Year.ToString() + fManageTypeId + "000";
                }

                SortedList sl = new SortedList();
                for (int i = 0; i < iCount; i++)
                {
                    sl.Clear();
                    sl.Add("FID", Guid.NewGuid().ToString());
                    fNo = (EConvert.ToInt(fNo) + 1).ToString();
                    string FName = DateTime.Now.Year.ToString() + "年" + fManageName + "第" + EConvert.ToInt(fNo.Substring(7, 3)) + "批";
                    sl.Add("FIsDeleted", 0);
                    sl.Add("FCreateTime", DateTime.Now);
                    sl.Add("FDFId", FDeptId);
                    sl.Add("FNumber", fNo);
                    sl.Add("FManageTypeId", fManageTypeId);
                    sl.Add("FTtile", FName);
                    sl.Add("FYear", DateTime.Now.Year);
                    sl.Add("FSystemID", FSystemId);
                    sl.Add("fstate", 0);
                    this.SaveEBase(EntityTypeEnum.EaBatchNo, sl, "FID", SaveOptionEnum.Insert);
                }
            }

            sb.Remove(0, sb.Length);
            sb.Append(" select fid,FTtile ");
            sb.Append(" from CF_App_BatchNo ");
            sb.Append(" where FSystemID ='" + FSystemId + "'");
            sb.Append(" and FDFId='" + FDeptId + "'");
            sb.Append(" and FManageTypeId=" + fManageTypeId);
            sb.Append(" and fisdeleted=0 ");
            if (iState != int.MaxValue)
            {
                sb.Append(" and fstate=" + iState);
            }
            sb.Append("  order by fnumber desc ");
            dt = this.GetTable(sb.ToString());
            return dt;
        }
        public DataTable getBacthList(string FSystemId, string FDeptId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" select fid ");
            sb.Append(" from CF_App_BatchNo ");
            sb.Append(" where FSystemID ='" + FSystemId + "'");
            sb.Append(" and FDFId='" + FDeptId + "'");
            sb.Append(" and fisdeleted=0 ");
            sb.Append(" and fstate<>1 ");
            DataTable dt = this.GetTable(sb.ToString());

            int iCount = dt.Rows.Count;
            //对所有未板结批次进行检查处理
            for (int i = 0; i < iCount; i++)
            {
                //提取该批次中已经板结，同时必须该批次已经包含的有业务的批次
                string FAppNoId = dt.Rows[i]["fid"].ToString();
                sb.Remove(0, sb.Length);
                sb.Append("select count(*) from ");
                sb.Append(" CF_App_BatchNo pn,CF_App_AppBatchNo apn,CF_App_ProcessInstance p ");
                sb.Append(" where pn.fid=apn.fbatchnoid and p.fid=apn.fappid ");
                sb.Append(" and pn.fid='" + FAppNoId + "'");
                sb.Append(" and EXISTS ");
                sb.Append("(select * from CF_App_AppBatchNo where ");
                sb.Append(" apn.fbatchnoid='" + FAppNoId + "') ");
                int itemp = this.GetSQLCount(sb.ToString());
                if (itemp < 0)
                {
                    //如果该批次已经全部完成，则把该批次设置为板结
                    this.PExcute("update CF_App_BatchNo set fstate=1 where fid='" + FAppNoId + "' ");
                }
            }

            //重新检查批次如果不够三条，则添加新批次
            sb.Remove(0, sb.Length);
            sb.Append(" select count(1) ");
            sb.Append(" from CF_App_BatchNo ");
            sb.Append(" where FSystemID ='" + FSystemId + "'");
            sb.Append(" and FDFId='" + FDeptId + "'");
            sb.Append(" and fisdeleted=0 ");
            sb.Append(" and fstate<>1 ");
            iCount = this.GetSQLCount(sb.ToString());
            
            //每次保证有三个批次没有板结
            iCount = 3 - iCount;
            if (iCount > 0)
            {
                sb.Remove(0, sb.Length);
                sb.Append(" select max(FNumber) from CF_App_BatchNo ");
                sb.Append(" where FDFId='" + FDeptId + "'");
                sb.Append(" and FSystemID='" + FSystemId + "'");
                sb.Append(" and fnumber like '" + DateTime.Now.Year.ToString() + "%'");
                string fNo = this.GetSignValue(sb.ToString());
                if (fNo != null)
                {
                    fNo = (EConvert.ToInt(fNo) + 1).ToString();
                }
                else
                {
                    fNo = DateTime.Now.Year.ToString() + "001";
                }

                SortedList sl = new SortedList();
                for (int i = 0; i < iCount; i++)
                {
                    sl.Clear();
                    sl.Add("FID", Guid.NewGuid().ToString());
                    fNo = (EConvert.ToInt(fNo) + 1).ToString();
                    string FName = DateTime.Now.Year.ToString() + "年第" + EConvert.ToInt(fNo.Substring(4, 3)) + "批";
                    sl.Add("FIsDeleted", 0);
                    sl.Add("FCreateTime", DateTime.Now);
                    sl.Add("FDFId", FDeptId);
                    sl.Add("FNumber", fNo);
                    sl.Add("FTtile", FName);
                    sl.Add("FYear", DateTime.Now.Year);
                    sl.Add("FSystemID", FSystemId);
                    sl.Add("fstate", 0);
                    this.SaveEBase(EntityTypeEnum.EaBatchNo, sl, "FID", SaveOptionEnum.Insert);
                }
            }

            sb.Remove(0, sb.Length);
            sb.Append(" select fid,FTtile ");
            sb.Append(" from CF_App_BatchNo ");
            sb.Append(" where FSystemID ='" + FSystemId + "'");
            sb.Append(" and FDFId='" + FDeptId + "'");
            sb.Append(" and fisdeleted=0 ");
            sb.Append(" and fstate<>1 order by fnumber desc ");
            dt = this.GetTable(sb.ToString());
            return dt;
        }
    }
}
