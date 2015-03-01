namespace Approve.RuleBase
{
    using Approve.EntityBase;
    using Approve.EntityCenter;
    using Approve.EntitySys;
    using Approve.PersistBase;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    [Guid("2de915e1-df71-3443-9f4d-32259c92ced2"), LicenseProvider(typeof(MyLicenseProvider))]
    public class RBase : MarshalByRefObject, IRBase
    {
        private IBaseTools _baseTools;
        private IConnection Cn_e;
        private IPBase m_PBase;
        protected string pDBName = "";

        public RBase()
        {
            License license = LicenseManager.Validate(typeof(RBase), this);
        }

        public bool ClearException()
        {
            return true;
        }

        public IDbDataParameter[] ConvertParameters(IDictionary dict)
        {
            SqlParameter[] parameterArray = null;
            if (dict != null)
            {
                parameterArray = new SqlParameter[dict.Count];
                int num = 0;
                foreach (string str in dict.Keys)
                {
                    parameterArray[num++] = new SqlParameter("@" + str, dict[str]);
                }
            }
            return parameterArray;
        }

        public int Count(IConnection Cn, EntityTypeEnum eType, string condition, params IDbDataParameter[] parameters)
        {
            return this.Pbase.Count(Cn, eType, condition, parameters);
        }

        public int Count(IConnection Cn, string strMulTblSql, string condition, params IDbDataParameter[] parameters)
        {
            return this.Pbase.Count(Cn, strMulTblSql, condition, parameters);
        }

        public bool Del(IConnection Cn, IEBase ent, bool IsFact)
        {
            return this.Pbase.Del(Cn, ent, IsFact);
        }

        public bool Del(IConnection Cn, IEBase ent, bool IsFact, SqlTransaction trans)
        {
            return this.Pbase.Del(Cn, ent, IsFact, trans);
        }

        public bool Del(IConnection Cn, IEBase ent, bool IsFact, string keyField)
        {
            return this.Pbase.Del(Cn, ent, IsFact, keyField);
        }

        public bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact)
        {
            return this.Pbase.Del(Cn, dict, eType, IsFact);
        }

        public bool Del(string FID, EntityTypeEnum EntityType, ArrayList EFID, bool IsFact)
        {
            bool flag3;
            RBase base2 = new RBase();
            bool isExistCn = false;
            IConnection cn = null;
            PBase.CreateCn(ref cn, ref isExistCn);
            try
            {
                SqlTransaction trans = ((SqlConnection) cn.rawConnection).BeginTransaction();
                for (int i = 0; i < EFID.Count; i++)
                {
                    string condition = "fid='" + EFID[i].ToString() + "' ";
                    if (!this.Del(cn, EntityType, condition, IsFact, trans))
                    {
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                flag3 = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref cn, isExistCn);
            }
            return flag3;
        }

        public bool Del(IConnection Cn, EntityTypeEnum eType, string condition, bool IsFact, params IDbDataParameter[] parameters)
        {
            return this.Pbase.Del(Cn, eType, condition, IsFact, parameters);
        }

        public bool Del(IConnection Cn, EntityTypeEnum eType, string condition, bool IsFact, SqlTransaction trans)
        {
            return this.Pbase.Del(Cn, eType, condition, IsFact, trans);
        }

        public bool Del(IConnection Cn, IEBase ent, bool IsFact, string keyField, SqlTransaction trans)
        {
            return this.Pbase.Del(Cn, ent, IsFact, keyField, trans);
        }

        public bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact, SqlTransaction trans)
        {
            return this.Pbase.Del(Cn, dict, eType, IsFact, trans);
        }

        public bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact, string keyField)
        {
            return this.Pbase.Del(Cn, dict, eType, IsFact, keyField);
        }

        public bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact, string keyField, SqlTransaction trans)
        {
            return this.Pbase.Del(Cn, dict, eType, IsFact, keyField, trans);
        }

        public bool DelCDev(string fid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete from CF_Ent_Device where fid='" + fid + "'");
            builder.Append(" delete from CF_Ent_CheckParameter where FRELATIONID='" + fid + "' ");
            return this.PExcute(builder.ToString());
        }

        public bool DelCEmp(string fid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete from CF_Emp_Certi where fempid='" + fid + "' ");
            builder.Append(" delete from CF_Emp_BaseInfo where fid='" + fid + "' ");
            builder.Append(" delete from CF_Emp_RegistSpecial where fempid='" + fid + "' ");
            builder.Append(" delete from CF_Emp_WorkAchievement where fempid='" + fid + "' ");
            builder.Append(" delete from CF_Emp_WorkExperience where fempid='" + fid + "' ");
            builder.Append(" delete from CF_Emp_RegistCerti where fempid='" + fid + "' ");
            builder.Append(" delete from CF_Emp_Resume where fempid='" + fid + "' ");
            return this.PExcute(builder.ToString());
        }

        public bool DelCPro(string fid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete from CF_Ent_Project where fid='" + fid + "' ");
            builder.Append(" delete from CF_Ent_ProjectOther where fid='" + fid + "' ");
            builder.Append(" delete from CF_Ent_ProjectManager where FProgectId='" + fid + "' ");
            builder.Append(" delete from CF_Ent_ProjectCerti where FProgectId='" + fid + "' ");
            builder.Append(" delete from CF_Ent_ProjBackUp where FProgectId='" + fid + "' ");
            builder.Append(" delete from CF_Ent_CheckParameter where FRELATIONID='" + fid + "' ");
            return this.PExcute(builder.ToString());
        }

        public bool DelDic(string fid)
        {
            StringBuilder builder = new StringBuilder();
            string fAllSubDic = "";
            string fNumber = this.GetSignValue(EntityTypeEnum.EsDicClass, "FNumber", "fid='" + fid + "'");
            switch (fNumber)
            {
                case null:
                case "":
                    fNumber = this.GetSignValue(EntityTypeEnum.EsDic, "FNumber", "fid='" + fid + "'");
                    if ((fNumber == null) || (fNumber == ""))
                    {
                        return false;
                    }
                    break;
            }
            this.GetAllSubDic(fNumber, ref fAllSubDic);
            if (fAllSubDic.Length > 0)
            {
                fAllSubDic = fAllSubDic.Remove(fAllSubDic.Length - 1);
            }
            if ((fAllSubDic != null) && (fAllSubDic != ""))
            {
                fAllSubDic = fAllSubDic.Insert(0, "'" + fid + "',");
            }
            else
            {
                fAllSubDic = fAllSubDic.Insert(0, "'" + fid + "'");
            }
            builder.Append("delete from CF_Sys_Dic where fid in(");
            builder.Append(fAllSubDic);
            builder.Append(")");
            builder.Append(" delete from cf_sys_dicclass where fid='");
            builder.Append(fid);
            builder.Append("'");
            return this.PExcute(builder.ToString());
        }

        public bool DelEBase(EntityTypeEnum Es, ArrayList EFID, bool IsFact)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            StringBuilder builder = new StringBuilder();
            try
            {
                builder.Append(" delete from " + this.GetEntityName(Es) + " where ");
                int count = EFID.Count;
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        builder.Append(" fid='" + EFID[i].ToString() + "' ");
                    }
                    else
                    {
                        builder.Append(" or fid='" + EFID[i].ToString() + "' ");
                    }
                }
                flag2 = this.PExcute(builder.ToString());
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return flag2;
        }

        public bool DelEBase(EntityTypeEnum Es, string condition, bool IsFact)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                flag2 = this.Del(this.Cn_e, Es, condition, IsFact, new IDbDataParameter[0]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return flag2;
        }

        public bool DelEBase(EntityTypeEnum Es, string condition, bool IsFact, params IDbDataParameter[] parameters)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                flag2 = this.Del(this.Cn_e, Es, condition, IsFact, parameters);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return flag2;
        }

        public bool DelEmp(string fid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete from CF_Emp_Certi where fempid='" + fid + "' ");
            builder.Append(" delete from CF_Emp_BaseInfo where fid='" + fid + "' ");
            builder.Append(" delete from CF_Emp_BaseInfo where fempid='" + fid + "' ");
            builder.Append(" delete from CF_Emp_RegistSpecial where fempid='" + fid + "' ");
            builder.Append(" delete from CF_Emp_WorkAchievement where fempid='" + fid + "' ");
            builder.Append(" delete from CF_Emp_WorkExperience where fempid='" + fid + "' ");
            builder.Append(" delete from CF_Emp_RegistCerti where fempid='" + fid + "' ");
            builder.Append(" delete from CF_Emp_Resume where fempid='" + fid + "' ");
            return this.PExcute(builder.ToString());
        }

        public bool DelManageDept(string fid)
        {
            StringBuilder builder = new StringBuilder();
            string fNumber = this.GetSignValue(EntityTypeEnum.EsManageDept, "FNumber", "fid='" + fid + "'");
            switch (fNumber)
            {
                case null:
                case "":
                    return false;
            }
            string fAllSubDept = "";
            this.GetAllSubDept(fNumber, ref fAllSubDept);
            if ((fAllSubDept != null) && (fAllSubDept.Length > 0))
            {
                fAllSubDept = fAllSubDept.Remove(fAllSubDept.Length - 1).Insert(0, "'" + fid + "',");
                builder.Append(" delete from CF_Sys_ManageDept where fid in (");
                builder.Append(fAllSubDept);
                builder.Append(")");
            }
            else
            {
                builder.Append(" delete from CF_Sys_ManageDept where fid ='");
                builder.Append(fid);
                builder.Append("'");
            }
            return this.PExcute(builder.ToString());
        }

        public bool DelMenu(string fNumber)
        {
            string returnValue = "";
            this.GetSubMenu(ref returnValue, fNumber);
            if (returnValue.Length > 0)
            {
                this.PExcute("delete from cf_sys_menu where fnumber in(" + returnValue + ");");
            }
            this.PExcute(" delete from cf_sys_menu where fnumber='" + fNumber + "';");
            return true;
        }

        public bool DelNews(string fid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin ");
            builder.Append(" delete from CF_News_Title where FId='" + fid + "' ");
            builder.Append(" delete from CF_News_Col where FNewsId='" + fid + "' ");
            builder.Append(" delete from CF_News_Content where FNewsId='" + fid + "' ");
            builder.Append(" delete from CF_News_Comment where FNewsId='" + fid + "' ");
            builder.Append(" delete from CF_News_RecUnit where FNewsId='" + fid + "' ");
            builder.Append(" end ");
            return this.PExcute(builder.ToString());
        }

        public bool DelProcess(string fProcessId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin");
            builder.Append(" delete from CF_App_ManageType where FProcessId='" + fProcessId + "' ");
            builder.Append(" delete from CF_App_QualiLevel where FProcessId='" + fProcessId + "' ");
            builder.Append(" delete from CF_App_QualiType where FProcessId='" + fProcessId + "' ");
            builder.Append(" delete from CF_App_SubFlow where FProcessId='" + fProcessId + "' ");
            builder.Append(" delete from CF_App_Process where FId='" + fProcessId + "' ");
            builder.Append(" delete from CF_App_ProcessRecord where FProcessInstanceID in(");
            builder.Append("select fid from CF_App_ProcessInstance where FProcessId='" + fProcessId + "') ");
            builder.Append(" delete from CF_App_ProcessInstance where  FProcessId='" + fProcessId + "' ");
            builder.Append(" end");
            return this.PExcute(builder.ToString());
        }

        public bool DelProPerEmp(string fid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete from CF_Emp_ProPer where fid = '" + fid + "' ");
            builder.Append(" delete from CF_Emp_ProTech where fempid = '" + fid + "' ");
            builder.Append(" delete from CF_Emp_ProCheck where fempid = '" + fid + "' ");
            return this.PExcute(builder.ToString());
        }

        public bool DelQConstruct(string fid)
        {
            string str = "";
            string str2 = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(" select fid, fappid,fempid from CF_Construct_AppDetial where fid='" + fid + "'");
            DataTable table = this.GetTable(builder.ToString());
            if ((table == null) || (table.Rows.Count == 0))
            {
                return true;
            }
            str = table.Rows[0]["fid"].ToString();
            str2 = table.Rows[0]["fempid"].ToString();
            builder.Remove(0, builder.Length);
            if ((str2 != null) && (str2 != ""))
            {
                builder.Append(" delete from CF_Construct_AppDetial where fid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_RegistSpecial where fempid='" + str2 + "'  and fappid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_WorkAchievement where fempid='" + str2 + "' and fappid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_WorkExperience where fempid='" + str2 + "' and fappid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_RegistCerti where fempid='" + str2 + "' and fappid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_Resume where fempid='" + str2 + "' and fappid='" + str + "'");
            }
            return this.PExcute(builder.ToString());
        }

        public bool DelQDev(string fid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select fappid,FDeviceId from CF_AppEnt_Device ");
            builder.Append(" where fid='" + fid + "'");
            DataTable table = this.GetTable(builder.ToString());
            if ((table == null) || (table.Rows.Count == 0))
            {
                return true;
            }
            string str = table.Rows[0]["FDeviceId"].ToString();
            string str2 = table.Rows[0]["fappid"].ToString();
            builder.Remove(0, builder.Length);
            builder.Append(" delete from CF_AppEnt_Device where fid='" + fid + "'");
            builder.Append(" delete from CF_AppEnt_CheckParameter where FRELATIONID='" + str + "' and fappid='" + str2 + "'");
            return this.PExcute(builder.ToString());
        }

        public bool DelQEmp(string fid)
        {
            string str = "";
            string str2 = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(" select fappid,fempid from cf_appemp_baseinfo where fid='" + fid + "'");
            DataTable table = this.GetTable(builder.ToString());
            if ((table == null) || (table.Rows.Count == 0))
            {
                return true;
            }
            str = table.Rows[0]["fappid"].ToString();
            str2 = table.Rows[0]["fempid"].ToString();
            builder.Remove(0, builder.Length);
            if ((str2 != null) && (str2 != ""))
            {
                builder.Append(" delete from CF_AppEmp_BaseInfo where fempid='" + str2 + "' and fappid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_RegistSpecial where fempid='" + str2 + "'  and fappid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_WorkAchievement where fempid='" + str2 + "' and fappid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_WorkExperience where fempid='" + str2 + "' and fappid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_RegistCerti where fempid='" + str2 + "' and fappid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_Resume where fempid='" + str2 + "' and fappid='" + str + "'");
            }
            return this.PExcute(builder.ToString());
        }

        public bool DelQJzs(string fid)
        {
            string str = "";
            string str2 = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(" select fappid,fempid from CF_Construct_AppDetial where fid='" + fid + "'");
            DataTable table = this.GetTable(builder.ToString());
            if ((table == null) || (table.Rows.Count == 0))
            {
                return true;
            }
            str = table.Rows[0]["fappid"].ToString();
            str2 = table.Rows[0]["fempid"].ToString();
            builder.Remove(0, builder.Length);
            if ((str2 != null) && (str2 != ""))
            {
                builder.Append(" delete from CF_Construct_AppDetial where fempid='" + str2 + "' and fappid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_RegistSpecial where fempid='" + str2 + "'  and fappid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_WorkAchievement where fempid='" + str2 + "' and fappid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_WorkExperience where fempid='" + str2 + "' and fappid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_RegistCerti where fempid='" + str2 + "' and fappid='" + str + "'");
                builder.Append(" delete from CF_AppEmp_Resume where fempid='" + str2 + "' and fappid='" + str + "'");
            }
            return this.PExcute(builder.ToString());
        }

        public bool DelQPro(string fid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select fappid,fprojectid from CF_AppEnt_Project where fid = '" + fid + "'");
            DataTable table = this.GetTable(builder.ToString());
            if ((table == null) || (table.Rows.Count == 0))
            {
                return true;
            }
            string str = table.Rows[0]["fprojectid"].ToString();
            string str2 = table.Rows[0]["fappid"].ToString();
            builder.Remove(0, builder.Length);
            builder.Append(" delete from CF_AppEnt_Project where fid='" + fid + "'  and fappid='" + str2 + "'");
            builder.Append(" delete from CF_AppEnt_ProjectOther where fid='" + fid + "' and fappid='" + str2 + "'");
            builder.Append(" delete from CF_AppEnt_ProjectManager where FProgectId='" + str + "' and fappid='" + str2 + "'");
            builder.Append(" delete from CF_AppEnt_ProjectCerti where FProgectId='" + str + "' and fappid='" + str2 + "'");
            builder.Append(" delete from CF_AppEnt_ProjBackUp where FProgectId='" + str + "' and fappid='" + str2 + "'");
            builder.Append(" delete from CF_AppEnt_CheckParameter where FRELATIONID='" + str + "' and fappid='" + str2 + "'");
            return this.PExcute(builder.ToString());
        }

        public bool DelQualiAppCondition(string fid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete from CF_App_QualiCondition where fid='" + fid + "'");
            builder.Append(" delete from CF_App_QualiCondition where FParentId='" + fid + "'");
            return this.PExcute(builder.ToString());
        }

        public bool DelRole(string fid)
        {
            StringBuilder builder = new StringBuilder();
            string fNumber = this.GetSignValue(EntityTypeEnum.EsRole, "FNumber", "fid='" + fid + "'");
            switch (fNumber)
            {
                case null:
                case "":
                    return false;
            }
            string fAllSubRole = "";
            this.GetAllSubRole(fNumber, ref fAllSubRole);
            if ((fAllSubRole != null) && (fAllSubRole.Length > 0))
            {
                fAllSubRole = fAllSubRole.Remove(fAllSubRole.Length - 1).Insert(0, "'" + fid + "',");
                builder.Append(" delete from cf_sys_role where fid in (");
                builder.Append(fAllSubRole);
                builder.Append(")");
            }
            else
            {
                builder.Append(" delete from cf_sys_role where fid ='");
                builder.Append(fid);
                builder.Append("'");
            }
            return this.PExcute(builder.ToString());
        }

        public bool DelStatis(string fid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select fnumber from CF_Sys_Statis where fid = '" + fid + "' ");
            string signValue = this.GetSignValue(builder.ToString());
            builder.Remove(0, builder.Length);
            builder.Append(" delete from CF_Sys_Statis where fid='" + fid + "' ");
            if ((signValue != null) && (signValue != ""))
            {
                builder.Append(" delete from CF_Sys_StatisInfo where fsnumber ='" + signValue + "' ");
            }
            return this.PExcute(builder.ToString());
        }

        public void GetAllSubDept(string fNumber, ref string fAllSubDept)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select fid,fnumber,fparentid from CF_Sys_ManageDept where FParentId='" + fNumber + "'");
            DataTable table = this.GetTable(builder.ToString());
            if ((table != null) && (table.Rows.Count > 0))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    fAllSubDept = fAllSubDept + "'" + table.Rows[i]["fid"].ToString() + "',";
                    this.GetAllSubDept(table.Rows[i]["FNumber"].ToString(), ref fAllSubDept);
                }
            }
        }

        public void GetAllSubDic(string fNumber, ref string fAllSubDic)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select fnumber,fid,fparentid from cf_sys_dic where fparentid='" + fNumber + "'");
            DataTable table = this.GetTable(builder.ToString());
            if ((table != null) && (table.Rows.Count > 0))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    fAllSubDic = fAllSubDic + "'" + table.Rows[i]["fid"].ToString() + "',";
                    this.GetAllSubDic(table.Rows[i]["FNumber"].ToString(), ref fAllSubDic);
                }
            }
        }

        public void GetAllSubRole(string fNumber, ref string fAllSubRole)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select fid,fnumber,fparentid from cf_sys_role where FParentId='" + fNumber + "'");
            DataTable table = this.GetTable(builder.ToString());
            if ((table != null) && (table.Rows.Count > 0))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    fAllSubRole = fAllSubRole + "'" + table.Rows[i]["fid"].ToString() + "',";
                    this.GetAllSubRole(table.Rows[i]["FNumber"].ToString(), ref fAllSubRole);
                }
            }
        }

        public DataTable getAllupDeptId(string FProvice, int isHaveTown, int isCity)
        {
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("fnumber");
            table.Columns.Add(column);
            DataColumn column2 = new DataColumn("fname");
            table.Columns.Add(column2);
            StringBuilder builder = new StringBuilder();
            builder.Append("select fname ,fnumber,fparentid,fistown from cf_sys_ManageDept where fisdeleted=0 ");
            if (FProvice != "")
            {
                builder.Append(" and fnumber like '" + FProvice + "%' ");
            }
            if (isCity == 1)
            {
                builder.Append(" and flevel<=2 ");
            }
            builder.Append(" order by fnumber ");
            DataTable table2 = this.GetTable(builder.ToString());
            int num2 = 0;
            if (FProvice.Length == 2)
            {
                num2 = 1;
            }
            if (FProvice.Length == 4)
            {
                num2 = 1;
            }
            if (FProvice.Length == 6)
            {
                num2 = 1;
            }
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < num2; i++)
            {
                DataRow[] rowArray = table2.Select("fnumber='" + FProvice + "'", "fnumber");
                DataRow row = table.NewRow();
                row["fname"] = rowArray[0]["fname"].ToString();
                row["fnumber"] = rowArray[0]["fnumber"].ToString();
                table.Rows.Add(row);
                rowArray = table2.Select("fparentid='" + FProvice + "'", "fnumber");
                int length = rowArray.Length;
                for (int j = 0; j < length; j++)
                {
                    DataRow[] rowArray2;
                    int num6;
                    int num7;
                    row = table.NewRow();
                    row["fname"] = rowArray[j]["fname"].ToString();
                    row["fnumber"] = rowArray[j]["fnumber"].ToString();
                    table.Rows.Add(row);
                    if (isHaveTown == 1)
                    {
                        rowArray2 = table2.Select("fparentid='" + rowArray[j]["fnumber"].ToString() + "' and fistown=1 ", "fnumber");
                        num6 = rowArray2.Length;
                        num7 = 0;
                        while (num7 < num6)
                        {
                            row = table.NewRow();
                            row["fname"] = "----" + rowArray2[num7]["fname"].ToString();
                            row["fnumber"] = rowArray2[num7]["fnumber"].ToString();
                            table.Rows.Add(row);
                            num7++;
                        }
                    }
                    else
                    {
                        rowArray2 = table2.Select("fparentid='" + rowArray[j]["fnumber"].ToString() + "'", "fnumber");
                        num6 = rowArray2.Length;
                        for (num7 = 0; num7 < num6; num7++)
                        {
                            row = table.NewRow();
                            row["fname"] = "----" + rowArray2[num7]["fname"].ToString();
                            row["fnumber"] = rowArray2[num7]["fnumber"].ToString();
                            table.Rows.Add(row);
                        }
                    }
                }
            }
            return table;
        }

        public string GetColName(string fColNumber)
        {
            StringBuilder builder = new StringBuilder("fnumber='" + fColNumber + "'");
            return this.GetSignValue(EntityTypeEnum.EsTree, "FName", builder.ToString());
        }

        public int GetCount(string sql)
        {
            int num2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                StringBuilder builder = new StringBuilder();
                if (this.Cn_e.DatabaseType == DatabaseTypeEnum.Oracle)
                {
                    builder.Append("select max(rownum) rown from (");
                    builder.Append(sql);
                    builder.Append(")");
                }
                else
                {
                    int index;
                    string str;
                    if (this.Cn_e.DatabaseType == DatabaseTypeEnum.SQLServer)
                    {
                        index = sql.ToUpper().IndexOf("SELECT");
                        str = sql.Substring(index + 6);
                        string str2 = " top 100 percent ";
                        builder.Append("select count(*) from (select ");
                        builder.Append(str2);
                        builder.Append(" ");
                        builder.Append(str);
                        builder.Append(") TT");
                    }
                    else
                    {
                        index = sql.ToUpper().IndexOf("SELECT");
                        str = sql.Substring(index + 6);
                        builder.Append("select count(*) from (select top 100 percent ");
                        builder.Append(str);
                        builder.Append(") TT");
                    }
                }
                try
                {
                    object obj2 = this.Cn_e.Execute(builder.ToString(), SqlResultEnum.Row, new IDbDataParameter[0]);
                    this.Cn_e.Dispose();
                    if (obj2 != null)
                    {
                        return Convert.ToInt32(((DataRow) obj2)[0]);
                    }
                    return -1;
                }
                catch
                {
                    return -1;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return num2;
        }

        public int GetCount(string sql, params IDbDataParameter[] parameters)
        {
            int num2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                StringBuilder builder = new StringBuilder();
                if (this.Cn_e.DatabaseType == DatabaseTypeEnum.Oracle)
                {
                    builder.Append("select max(rownum) rown from (");
                    builder.Append(sql);
                    builder.Append(")");
                }
                else
                {
                    int index;
                    string str;
                    if (this.Cn_e.DatabaseType == DatabaseTypeEnum.SQLServer)
                    {
                        index = sql.ToUpper().IndexOf("SELECT");
                        str = sql.Substring(index + 6);
                        string str2 = " top 100 percent ";
                        builder.Append("select count(*) from (select ");
                        builder.Append(str2);
                        builder.Append(" ");
                        builder.Append(str);
                        builder.Append(") TT");
                    }
                    else
                    {
                        index = sql.ToUpper().IndexOf("SELECT");
                        str = sql.Substring(index + 6);
                        builder.Append("select count(*) from (select top 100 percent ");
                        builder.Append(str);
                        builder.Append(") TT");
                    }
                }
                try
                {
                    object obj2 = this.Cn_e.Execute(builder.ToString(), SqlResultEnum.Row, parameters);
                    this.Cn_e.Dispose();
                    if (obj2 != null)
                    {
                        return Convert.ToInt32(((DataRow) obj2)[0]);
                    }
                    return -1;
                }
                catch
                {
                    return -1;
                }
            }
            catch (Exception exception)
            {
                DataLog.AddLog(this.Cn_e, sql, exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return num2;
        }

        public int GetCount(EntityTypeEnum EntityType, string condition, params IDbDataParameter[] parameters)
        {
            int num;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            StringBuilder builder = new StringBuilder();
            try
            {
                builder.Append("select count(1) from ");
                builder.Append(this.GetEntityName(EntityType) + " ");
                if ((condition != null) || (condition != ""))
                {
                    builder.Append("where " + condition);
                }
                object obj2 = this.Cn_e.Execute(builder.ToString(), SqlResultEnum.Have, parameters);
                if (obj2 != null)
                {
                    return Convert.ToInt32(obj2);
                }
                num = 0;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(this.Cn_e, builder.ToString(), exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return num;
        }

        public int GetCount(string sql, bool BFlag, params IDbDataParameter[] parameters)
        {
            int num;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                object obj2 = this.Cn_e.Execute(sql, SqlResultEnum.Table, parameters);
                if (obj2 != null)
                {
                    DataTable table = (DataTable) obj2;
                    if (table.Rows.Count > 0)
                    {
                        return (Convert.ToInt32(table.Rows[0][0]) + Convert.ToInt32(table.Rows[1][0]));
                    }
                    return 0;
                }
                num = -1;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(this.Cn_e, sql, exception.Message, 1);
                num = -1;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return num;
        }

        public string GetDicName(string fNumber)
        {
            if (fNumber == "&nbsp;")
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            string[] strArray = fNumber.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if (i == 0)
                {
                    builder.Append(this.GetSignValue(EntityTypeEnum.EsDic, "FName", "FNumber='" + strArray[i] + "'"));
                }
                else
                {
                    builder.Append("," + this.GetSignValue(EntityTypeEnum.EsDic, "FName", "FNumber='" + strArray[i] + "'"));
                }
            }
            return builder.ToString();
        }

        public string GetDicName(string fParentId, string fNumber)
        {
            if ((fNumber != "") && (fNumber != "&nbsp;"))
            {
                long num = 0L;
                try
                {
                    num = Convert.ToInt64(fNumber.Trim());
                }
                catch (Exception)
                {
                    return "";
                }
                StringBuilder builder = new StringBuilder();
                if (fParentId.IndexOf(",") != -1)
                {
                    builder.Append("select fname from cf_sys_dic where fparentid in (" + fParentId + ") and fnumber='" + fNumber + "'");
                }
                else
                {
                    builder.Append("select fname from cf_sys_dic where fparentid='" + fParentId + "' and fnumber='" + fNumber + "'");
                }
                string signValue = this.GetSignValue(builder.ToString());
                if (signValue != null)
                {
                    return signValue;
                }
            }
            return "";
        }

        public string getDicNameByFNumber(string nubmer)
        {
            if (nubmer == "")
            {
                return "";
            }
            EsDic dic = (EsDic) this.GetEBase(EntityTypeEnum.EsDic, "", "FNumber='" + nubmer + "'", new IDbDataParameter[0]);
            if (dic != null)
            {
                return dic.FName;
            }
            return nubmer;
        }

        public string GetDicNames(string fnumbers)
        {
            string str = "";
            string[] strArray = fnumbers.Split(new char[] { ',' });
            if (strArray.Length == 0)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("select fname,fnumber from cf_sys_dic where fnumber in (");
            for (int i = 0; i < strArray.Length; i++)
            {
                str = strArray[i];
                if (str == "&nbsp;")
                {
                    str = "";
                }
                if (i == 0)
                {
                    builder.Append("'" + str + "'");
                }
                else
                {
                    builder.Append(",'" + str + "'");
                }
            }
            builder.Append(") ");
            DataTable table = this.GetTable(builder.ToString());
            builder.Remove(0, builder.Length);
            if ((table == null) || (table.Rows.Count == 0))
            {
                return "";
            }
            DataRow[] rowArray = null;
            for (int j = 0; j < strArray.Length; j++)
            {
                str = strArray[j];
                if (str == "&nbsp;")
                {
                    str = 0x7fffffff.ToString();
                }
                rowArray = table.Select("FNumber=" + str);
                if ((rowArray == null) || (rowArray.Length == 0))
                {
                    if (j == 0)
                    {
                        builder.Append("");
                    }
                    else
                    {
                        builder.Append(",");
                    }
                }
                else if (j == 0)
                {
                    builder.Append(rowArray[0]["FName"].ToString());
                }
                else
                {
                    builder.Append("," + rowArray[0]["FName"].ToString());
                }
            }
            return builder.ToString();
        }

        public string GetDicRemark(string fNumber)
        {
            if (fNumber == "&nbsp;")
            {
                return "";
            }
            return this.GetSignValue(EntityTypeEnum.EsDic, "FReMark", "FNumber='" + fNumber + "'");
        }

        public DataTable getDicTbByFNumber(string number)
        {
            DataTable table = new DataTable();
            return this.GetTable(EntityTypeEnum.EsDic, "", "FParentid='" + number + "' and FName<>'全部' order by forder,ftime desc");
        }

        public DataTable getDicTopTbByFNumber(string number, int top)
        {
            DataTable table = new DataTable();
            return this.GetTable(EntityTypeEnum.EsDic, " top " + top + "", "FParent='" + number + "' and FName<>'全部' order by forder");
        }

        public EBase GetEBase(EntityTypeEnum Es, string ReduceOption, string condition, params IDbDataParameter[] parameters)
        {
            EBase base2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                base2 = (EBase) this.GetSingle(this.Cn_e, Es, ReduceOption, condition, parameters);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return base2;
        }

        public string GetEntityName(EntityTypeEnum eType)
        {
            return this.baseTools.GetName(eType);
        }

        public string getEntSystemId(string fBaseInfoId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select fsystemid from cf_ent_baseinfo ");
            builder.Append(" where fid='" + fBaseInfoId + "'");
            string signValue = this.GetSignValue(builder.ToString());
            if ((signValue != null) && (signValue != ""))
            {
                return signValue;
            }
            builder.Remove(0, builder.Length);
            builder.Append("select FSystemId from cf_sys_user where fbaseinfoid = '" + fBaseInfoId + "'");
            signValue = this.GetSignValue(builder.ToString());
            if ((signValue != null) && (signValue != ""))
            {
                return signValue;
            }
            return "";
        }

        public DataTable GetMenu(string fNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select fname,fnumber from cf_sys_menu where fparentid='" + fNumber + "' and fisdeleted=0 order by forder,fcreatetime desc");
            return this.GetTable(builder.ToString());
        }

        public string GetMenuName(string fMenuNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" fnumber='" + fMenuNumber + "'");
            return this.GetSignValue(EntityTypeEnum.EsMenu, "FName", builder.ToString());
        }

        public string getMTypeAurl(string FNumber)
        {
            EsManageType type = (EsManageType) this.GetEBase(EntityTypeEnum.EsManageType, "FAUrl", "FNumber='" + FNumber + "'", new IDbDataParameter[0]);
            if (type == null)
            {
                return "";
            }
            return type.FAUrl;
        }

        public string getMTypeName(string FNumber)
        {
            EsManageType type = (EsManageType) this.GetEBase(EntityTypeEnum.EsManageType, "FName", "FNumber='" + FNumber + "'", new IDbDataParameter[0]);
            if (type == null)
            {
                return "";
            }
            return type.FName;
        }

        public string getMTypeQurl(string FNumber)
        {
            EsManageType type = (EsManageType) this.GetEBase(EntityTypeEnum.EsManageType, "FQUrl", "FNumber='" + FNumber + "'", new IDbDataParameter[0]);
            if (type == null)
            {
                return "";
            }
            return type.FQurl;
        }

        public string GetNewsCol(string fid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select cs.fname from CF_News_Col cn,cf_sys_tree cs ");
            builder.Append(" where cs.fnumber = cn.fcolnumber ");
            builder.Append(" and cn.fnewsid = '" + fid + "' ");
            DataTable table = this.GetTable(builder.ToString());
            builder.Remove(0, builder.Length);
            if ((table == null) || (table.Rows.Count <= 0))
            {
                return "";
            }
            builder.Append("<table cellpadding='0' cellspcing='0' width='98%'  style='border:solid 1px #C0C0C0'>");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if ((i % 2) == 0)
                {
                    builder.Append("<tr>");
                }
                builder.Append("<td style='border:solid 1px #C0C0C0' width='50%'>");
                builder.Append(table.Rows[i]["FName"].ToString());
                builder.Append("</td>");
                if (((i + 1) % 2) == 0)
                {
                    builder.Append("</tr>");
                }
                if ((i == (table.Rows.Count - 1)) && ((i % 2) == 0))
                {
                    builder.Append("<td style='border:solid 1px #C0C0C0' width='50%'></td>");
                    builder.Append("</tr>");
                }
            }
            builder.Append("</table>");
            return builder.ToString();
        }

        public string GetNewsColNumber(string fid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select cs.fnumber from CF_News_Col cn,cf_sys_tree cs ");
            builder.Append(" where cs.fnumber = cn.fcolnumber ");
            builder.Append(" and cn.fnewsid = '" + fid + "' ");
            DataTable table = this.GetTable(builder.ToString());
            builder.Remove(0, builder.Length);
            if ((table == null) || (table.Rows.Count <= 0))
            {
                return "";
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (i == 0)
                {
                    builder.Append(table.Rows[i]["fnumber"].ToString());
                }
                else
                {
                    builder.Append("," + table.Rows[i]["fnumber"].ToString());
                }
            }
            return builder.ToString();
        }

        public DataTable GetPageTable(string sql, int startRow, int pageSize)
        {
            DataTable table;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                table = this.Pbase.GetPageTable(this.Cn_e, sql, startRow, pageSize);
            }
            catch (Exception exception)
            {
                DataLog.AddLog(this.Cn_e, sql, exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return table;
        }

        public DataTable GetPageTable(IConnection Cn, EntityTypeEnum entType, string ReduceOption, string Condition, int PageSize, int currPage)
        {
            return this.Pbase.GetPageTable(Cn, entType, ReduceOption, Condition, PageSize, currPage);
        }

        public DataTable GetPageTable(IConnection Cn, string strSqlQuery, string ReduceOption, string Condition, int PageSize, int currPage)
        {
            return this.Pbase.GetPageTable(Cn, strSqlQuery, ReduceOption, Condition, PageSize, currPage);
        }

        public string GetParentMenuNumber(string fNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select fparentid from cf_sys_menu where fnumber='" + fNumber + "'");
            return this.GetSignValue(builder.ToString());
        }

        public int GetReason()
        {
            int month = DateTime.Now.AddMonths(-3).Month;
            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    return 1;

                case 4:
                case 5:
                case 6:
                    return 2;

                case 7:
                case 8:
                case 9:
                    return 3;

                case 10:
                case 11:
                case 12:
                    return 4;
            }
            return 0;
        }

        public string getRoleNameById(string RoleId)
        {
            EsRole role = (EsRole) this.GetEBase(EntityTypeEnum.EsRole, "FName", "FID='" + RoleId + "'", new IDbDataParameter[0]);
            if (role == null)
            {
                return "";
            }
            return role.FName;
        }

        public string getRoleNameByNumber(string RoleNumber)
        {
            EsRole role = (EsRole) this.GetEBase(EntityTypeEnum.EsRole, "FName", "fnumber='" + RoleNumber + "'", new IDbDataParameter[0]);
            if (role == null)
            {
                return "";
            }
            return role.FName;
        }

        public string getRowCellData(DataRow row, string fColumnName)
        {
            if (row == null)
            {
                return "";
            }
            if (row[fColumnName] == DBNull.Value)
            {
                return "";
            }
            return row[fColumnName].ToString();
        }

        public string GetSignValue(string SQL)
        {
            string str;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                object obj2 = this.Cn_e.Execute(SQL, SqlResultEnum.Have, new IDbDataParameter[0]);
                if ((obj2 != null) && (obj2 != DBNull.Value))
                {
                    return obj2.ToString();
                }
                str = null;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return str;
        }

        public string GetSignValue(string SQL, params IDbDataParameter[] parameters)
        {
            string str;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                object obj2 = this.Cn_e.Execute(SQL, SqlResultEnum.Have, parameters);
                if ((obj2 != null) && (obj2 != DBNull.Value))
                {
                    return obj2.ToString();
                }
                str = null;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(this.Cn_e, SQL, exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return str;
        }

        public string GetSignValue(EntityTypeEnum EntityType, string ReduceOption, string condition)
        {
            string str;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("select " + ReduceOption + " from ");
                builder.Append(this.GetEntityName(EntityType) + " ");
                if ((condition != null) || (condition != ""))
                {
                    builder.Append("where " + condition);
                }
                object obj2 = this.Cn_e.Execute(builder.ToString(), SqlResultEnum.Have, new IDbDataParameter[0]);
                if ((obj2 != null) && (obj2 != DBNull.Value))
                {
                    return obj2.ToString();
                }
                str = null;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return str;
        }

        public string GetSignValue(EntityTypeEnum EntityType, string ReduceOption, string condition, params IDbDataParameter[] parameters)
        {
            string str;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            StringBuilder builder = new StringBuilder();
            try
            {
                builder.Append("select " + ReduceOption + " from ");
                builder.Append(this.GetEntityName(EntityType) + " ");
                if ((condition != null) || (condition != ""))
                {
                    builder.Append("where " + condition);
                }
                object obj2 = this.Cn_e.Execute(builder.ToString(), SqlResultEnum.Have, parameters);
                if ((obj2 != null) && (obj2 != DBNull.Value))
                {
                    return obj2.ToString();
                }
                str = null;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(this.Cn_e, builder.ToString(), exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return str;
        }

        public IEBase GetSingle(IConnection Cn, IEBase ent, params IDbDataParameter[] parameters)
        {
            return this.Pbase.GetSingle(Cn, ent, parameters);
        }

        public IEBase GetSingle(IConnection Cn, EntityTypeEnum eType, string ReduceOption, string condition, params IDbDataParameter[] parameters)
        {
            return this.Pbase.GetSingle(Cn, eType, ReduceOption, condition, parameters);
        }

        public SortedList GetSlFromDr(DataRow row)
        {
            if (row == null)
            {
                return null;
            }
            DataTable table = row.Table;
            int count = table.Columns.Count;
            if (count <= 0)
            {
                return null;
            }
            SortedList list = new SortedList();
            for (int i = 0; i < count; i++)
            {
                list.Add(table.Columns[i].ColumnName.ToUpper().Trim(), row[i].ToString());
            }
            return list;
        }

        public int GetSQLCount(string sql)
        {
            int num;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                object obj2 = this.Cn_e.Execute(sql, SqlResultEnum.Table, new IDbDataParameter[0]);
                if (obj2 != null)
                {
                    DataTable table = (DataTable) obj2;
                    if (table.Rows.Count > 0)
                    {
                        if (table.Rows[0][0] != DBNull.Value)
                        {
                            return Convert.ToInt32(table.Rows[0][0]);
                        }
                        return 0;
                    }
                    return 0;
                }
                num = -1;
            }
            catch
            {
                num = -1;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return num;
        }

        public int GetSQLCount(string sql, params IDbDataParameter[] parameters)
        {
            int num;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                num = EConvert.ToInt(this.Cn_e.Execute(sql, SqlResultEnum.Have, parameters));
            }
            catch (Exception exception)
            {
                DataLog.AddLog(this.Cn_e, sql, exception.Message, 1);
                num = -1;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return num;
        }

        public void GetSubMenu(ref string returnValue, string fNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select fnumber from cf_sys_menu where fparentid='" + fNumber + "' ");
            DataTable table = this.GetTable(builder.ToString());
            if ((table != null) && (table.Rows.Count > 0))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (returnValue.Length == 0)
                    {
                        returnValue = returnValue + "'" + table.Rows[i][0].ToString() + "'";
                    }
                    else
                    {
                        returnValue = returnValue + ",'" + table.Rows[i][0].ToString() + "'";
                    }
                    this.GetSubMenu(ref returnValue, table.Rows[i][0].ToString());
                }
            }
        }

        public string getSubStr(string sOrg, int len)
        {
            if ((sOrg != null) && (sOrg.Length > len))
            {
                return (sOrg.Substring(0, len - 2) + "..");
            }
            return sOrg;
        }

        public string GetSysObjectContent(string fNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select fcontent from CF_Sys_Object where fnumber ='" + fNumber + "'");
            return this.GetSignValue(builder.ToString());
        }

        public string getSysQurl(string FNumber)
        {
            EsSystemName name = (EsSystemName) this.GetEBase(EntityTypeEnum.EsSystemName, "FQUrl", "FNumber='" + FNumber + "'", new IDbDataParameter[0]);
            if (name == null)
            {
                return "";
            }
            return name.FQurl;
        }

        public DataTable GetTable(string strSql)
        {
            DataTable table2;
            DataTable table = new DataTable();
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                table = (DataTable) this.Cn_e.Execute(strSql, SqlResultEnum.Table, new IDbDataParameter[0]);
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return table2;
        }

        public DataTable GetTable(string strSql, params IDbDataParameter[] parameters)
        {
            DataTable table2;
            DataTable table = new DataTable();
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                table = (DataTable) this.Cn_e.Execute(strSql, SqlResultEnum.Table, parameters);
                table2 = table;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(this.Cn_e, strSql, exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return table2;
        }

        public DataTable GetTable(EntityTypeEnum Es, string ReduceOption, string condition)
        {
            DataTable table2;
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (ReduceOption != "")
            {
                builder.Append(ReduceOption);
            }
            else
            {
                builder.Append(" * ");
            }
            builder.Append(" from ");
            builder.Append(this.GetEntityName(Es));
            if (condition != "")
            {
                builder.Append(" where ");
                builder.Append(condition);
            }
            DataTable table = new DataTable();
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                table = (DataTable) this.Cn_e.Execute(builder.ToString(), SqlResultEnum.Table, new IDbDataParameter[0]);
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return table2;
        }

        public DataTable GetTable(string sql, int startrow, int endrow)
        {
            Exception exception;
            DataTable table;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            StringBuilder builder = new StringBuilder();
            if (endrow == 1)
            {
                builder.Append(sql);
            }
            else if ((this.Cn_e.DatabaseType != DatabaseTypeEnum.Oracle) && (this.Cn_e.DatabaseType == DatabaseTypeEnum.SQLServer))
            {
                string str = " top " + ((endrow - 1)).ToString() + " ";
                string str2 = " top " + startrow.ToString() + " ";
                int index = sql.IndexOf("select");
                string str3 = sql.Substring(index + 6);
                builder.Append("select * from (");
                builder.Append("select ");
                builder.Append(str);
                builder.Append(" ");
                builder.Append(str3);
                builder.Append(") TT where TT.fid not in (select fid from (select ");
                builder.Append(str2);
                builder.Append(" ");
                builder.Append(str3);
                builder.Append(") TT1 ) ");
                StringBuilder builder2 = builder;
                try
                {
                    SqlConnection rawConnection = this.Cn_e.rawConnection as SqlConnection;
                    if ((rawConnection != null) && (EConvert.ToFloat(rawConnection.ServerVersion.Substring(0, 2)) >= 9f))
                    {
                        int num3 = new Random().Next(0x3e8, 0x270f);
                        string str4 = "tempid_" + num3;
                        string str5 = str4;
                        int startIndex = str3.LastIndexOf("order by", StringComparison.InvariantCultureIgnoreCase);
                        if (startIndex > -1)
                        {
                            string input = str3.Substring(startIndex, str3.Length - startIndex);
                            Regex regex = new Regex(@"\(", RegexOptions.IgnoreCase);
                            Regex regex2 = new Regex(@"\)", RegexOptions.IgnoreCase);
                            if ((input.Replace(" ", "").IndexOf("orderby", StringComparison.InvariantCultureIgnoreCase) > -1) && (regex.Match(input).Length == regex.Match(input).Length))
                            {
                                str5 = input;
                            }
                        }
                        builder = new StringBuilder();
                        builder.AppendLine(" select * FROM (select  ");
                        if (str5 != str4)
                        {
                            builder.AppendLine("TOP 100 PERCENT ");
                            builder.AppendLine(" ROW_NUMBER() Over(" + str5 + ") as rowNum, ");
                        }
                        else
                        {
                            builder.AppendLine(" ROW_NUMBER() Over(order by " + str5 + ") as rowNum, ");
                        }
                        if (str5 == str4)
                        {
                            builder.AppendLine("* from (select TOP 100 PERCENT  0 tempid_" + num3 + ",");
                        }
                        builder.AppendLine(str3);
                        if (str5 == str4)
                        {
                            builder.AppendLine(" )temp_2");
                        }
                        builder.AppendLine(" ) temp_1");
                        builder.AppendLine(string.Concat(new object[] { " where rowNum> ", startrow, " and rowNum<", endrow }));
                    }
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    Debug.Write(exception.Message);
                    builder = builder2;
                }
            }
            try
            {
                object obj2 = this.Cn_e.Execute(builder.ToString(), SqlResultEnum.Table, new IDbDataParameter[0]);
                this.Cn_e.Dispose();
                if (obj2 != null)
                {
                    return (DataTable) obj2;
                }
                table = null;
            }
            catch (Exception exception2)
            {
                exception = exception2;
                DataLog.AddLog(this.Cn_e, builder.ToString(), exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return table;
        }

        public DataTable GetTable(EntityTypeEnum Es, string ReduceOption, string condition, params IDbDataParameter[] parameters)
        {
            DataTable table2;
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (ReduceOption != "")
            {
                builder.Append(ReduceOption);
            }
            else
            {
                builder.Append(" * ");
            }
            builder.Append(" from ");
            builder.Append(this.GetEntityName(Es));
            if (condition != "")
            {
                builder.Append(" where ");
                builder.Append(condition);
            }
            DataTable table = new DataTable();
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                table = (DataTable) this.Cn_e.Execute(builder.ToString(), SqlResultEnum.Table, parameters);
                table2 = table;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(this.Cn_e, builder.ToString(), exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return table2;
        }

        public DataTable GetTable(IConnection Cn, EntityTypeEnum entType, string ReduceOption, string condition)
        {
            return this.Pbase.GetTable(Cn, entType, ReduceOption, condition);
        }

        public DataTable GetTable(string sql, int startrow, int endrow, params IDbDataParameter[] parameters)
        {
            Exception exception;
            DataTable table;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            StringBuilder builder = new StringBuilder();
            if (endrow == 1)
            {
                builder.Append(sql);
            }
            else if ((this.Cn_e.DatabaseType != DatabaseTypeEnum.Oracle) && (this.Cn_e.DatabaseType == DatabaseTypeEnum.SQLServer))
            {
                string str = " top " + ((endrow - 1)).ToString() + " ";
                string str2 = " top " + startrow.ToString() + " ";
                int index = sql.IndexOf("select");
                string str3 = sql.Substring(index + 6);
                builder.Append("select * from (");
                builder.Append("select ");
                builder.Append(str);
                builder.Append(" ");
                builder.Append(str3);
                builder.Append(") TT where TT.fid not in (select fid from (select ");
                builder.Append(str2);
                builder.Append(" ");
                builder.Append(str3);
                builder.Append(") TT1 ) ");
                StringBuilder builder2 = builder;
                try
                {
                    SqlConnection rawConnection = this.Cn_e.rawConnection as SqlConnection;
                    if ((rawConnection != null) && (EConvert.ToFloat(rawConnection.ServerVersion.Substring(0, 2)) >= 9f))
                    {
                        int num3 = new Random().Next(0x3e8, 0x270f);
                        string str4 = "tempid_" + num3;
                        string str5 = str4;
                        int startIndex = str3.LastIndexOf("order by", StringComparison.InvariantCultureIgnoreCase);
                        if (startIndex > -1)
                        {
                            string input = str3.Substring(startIndex, str3.Length - startIndex);
                            Regex regex = new Regex(@"\(", RegexOptions.IgnoreCase);
                            Regex regex2 = new Regex(@"\)", RegexOptions.IgnoreCase);
                            if ((input.Replace(" ", "").IndexOf("orderby", StringComparison.InvariantCultureIgnoreCase) > -1) && (regex.Match(input).Length == regex.Match(input).Length))
                            {
                                str5 = input;
                            }
                        }
                        builder = new StringBuilder();
                        builder.AppendLine(" select * FROM (select  ");
                        if (str5 != str4)
                        {
                            builder.AppendLine("TOP 100 PERCENT ");
                            builder.AppendLine(" ROW_NUMBER() Over(" + str5 + ") as rowNum, ");
                        }
                        else
                        {
                            builder.AppendLine(" ROW_NUMBER() Over(order by " + str5 + ") as rowNum, ");
                        }
                        if (str5 == str4)
                        {
                            builder.AppendLine("* from (select TOP 100 PERCENT  0 tempid_" + num3 + ",");
                        }
                        builder.AppendLine(str3);
                        if (str5 == str4)
                        {
                            builder.AppendLine(" )temp_2");
                        }
                        builder.AppendLine(" ) temp_1");
                        builder.AppendLine(string.Concat(new object[] { " where rowNum> ", startrow, " and rowNum<", endrow }));
                    }
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    Debug.Write(exception.Message);
                    builder = builder2;
                }
            }
            try
            {
                object obj2 = this.Cn_e.Execute(builder.ToString(), SqlResultEnum.Table, parameters);
                this.Cn_e.Dispose();
                if (obj2 != null)
                {
                    return (DataTable) obj2;
                }
                table = null;
            }
            catch (Exception exception2)
            {
                exception = exception2;
                DataLog.AddLog(this.Cn_e, builder.ToString(), exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return table;
        }

        public EsUser getUserInfoByLockAndPwd(string uid, string pws)
        {
            DataTable table = new DataTable();
            EsUser user = (EsUser) this.GetEBase(EntityTypeEnum.EsUser, "", " FLockNumber='" + uid + "'", new IDbDataParameter[0]);
            if (user != null)
            {
                if (user.FPWD == pws)
                {
                    return user;
                }
                return null;
            }
            return null;
        }

        public int getWorkDay(string dateBegin, string dateEnd)
        {
            string signValue = this.GetSignValue("select top 1 FMemo from CF_Sys_Object where fnumber='_Sys_NoDay'");
            try
            {
                DateTime time = Convert.ToDateTime(dateBegin);
                TimeSpan span = (TimeSpan) (Convert.ToDateTime(dateEnd) - time);
                int totalDays = (int) span.TotalDays;
                int num2 = 0;
                for (int i = 0; i <= totalDays; i++)
                {
                    if (((time.AddDays((double) i).DayOfWeek.ToString() == "Saturday") || (time.AddDays((double) i).DayOfWeek.ToString() == "Sunday")) || (signValue.IndexOf(time.AddDays((double) i).ToString("yyyy-MM-dd")) >= 0))
                    {
                        num2++;
                    }
                }
                return (totalDays - num2);
            }
            catch
            {
                return 0;
            }
        }

        public string getWorkEndDay(string dateBegin, int DayCount)
        {
            string signValue = this.GetSignValue("select top 1 FMemo from CF_Sys_Object where fnumber='_Sys_NoDay'");
            try
            {
                DateTime time = Convert.ToDateTime(dateBegin);
                DateTime time2 = time;
                int num = 0;
                int num2 = 0;
                do
                {
                    num2++;
                    time2 = time.AddDays((double) num2);
                    if (((time2.AddDays((double) num2).DayOfWeek.ToString() != "Saturday") && (time2.AddDays((double) num2).DayOfWeek.ToString() != "Sunday")) && (signValue.IndexOf(time2.ToString("yyyy-MM-dd")) < 0))
                    {
                        num++;
                    }
                }
                while (num < DayCount);
                return time2.ToString("yyyy-MM-dd");
            }
            catch
            {
                return null;
            }
        }

        public string GetYear()
        {
            string strSql = "select getdate() ";
            DataTable table = this.GetTable(strSql);
            if (table != null)
            {
                return table.Rows[0][0].ToString().Substring(0, 4);
            }
            return "";
        }

        public bool isChinese(string str)
        {
            int length = str.Length;
            byte[] bytes = new ASCIIEncoding().GetBytes(str);
            int num2 = 0;
            for (int i = 0; i <= (bytes.Length - 1); i++)
            {
                if (bytes[i] == 0x3f)
                {
                    num2++;
                }
                num2++;
            }
            return (length == num2);
        }

        public bool isDate(string date)
        {
            try
            {
                DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsExist(string SQL)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                object obj2 = this.Cn_e.Execute(SQL, SqlResultEnum.Table, new IDbDataParameter[0]);
                if (obj2 != null)
                {
                    return (((DataTable) obj2).Rows.Count > 0);
                }
                flag2 = false;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(this.Cn_e, SQL, exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return flag2;
        }

        public bool IsExist(EntityTypeEnum EntityType, string condition)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            StringBuilder builder = new StringBuilder();
            try
            {
                builder.Append("select count(1) from ");
                builder.Append(this.GetEntityName(EntityType) + " ");
                if ((condition != null) || (condition != ""))
                {
                    builder.Append("where " + condition);
                }
                object obj2 = this.Cn_e.Execute(builder.ToString(), SqlResultEnum.Have, new IDbDataParameter[0]);
                if (obj2 != null)
                {
                    return (Convert.ToInt32(obj2) > 0);
                }
                flag2 = false;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(this.Cn_e, builder.ToString(), exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return flag2;
        }

        public bool IsExsist(IConnection Cn, IEBase ent)
        {
            return this.Pbase.IsExsist(Cn, ent);
        }

        public bool IsExsist(IConnection Cn, EntityTypeEnum eType, string Condition)
        {
            return this.Pbase.IsExsist(Cn, eType, Condition);
        }

        public bool IsExsist(IConnection Cn, IEBase ent, string keyField)
        {
            return this.Pbase.IsExsist(Cn, ent, keyField);
        }

        public bool isFloat(string num)
        {
            try
            {
                Convert.ToSingle(num);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool PExcute(string SQL)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                this.Cn_e.Execute(SQL, SqlResultEnum.No, new IDbDataParameter[0]);
                flag2 = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return flag2;
        }

        public bool PExcute(string SQL, params IDbDataParameter[] parameters)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                this.Cn_e.Execute(SQL, SqlResultEnum.No, parameters);
                flag2 = true;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(this.Cn_e, SQL, exception.Message, 1);
                throw exception;
            }
            finally
            {
                DataLog.AddLog(this.Cn_e, SQL, "", 3);
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return flag2;
        }

        public bool PExcute(string SQL, bool isTrans)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                this.Pbase.PExcute(this.Cn_e, SQL, SqlResultEnum.No);
                flag2 = true;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(this.Cn_e, SQL, exception.Message, 1);
                throw exception;
            }
            finally
            {
                DataLog.AddLog(this.Cn_e, SQL, "", 3);
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return flag2;
        }

        public bool PubNews(string fid, string fColNumber)
        {
            if ((fColNumber == null) || (fColNumber == ""))
            {
                return false;
            }
            EnTitle title = (EnTitle) this.GetEBase(EntityTypeEnum.EsAppStand, "", "fid='" + fid + "'", new IDbDataParameter[0]);
            if (title == null)
            {
                return false;
            }
            string[] strArray = fColNumber.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int length = strArray.Length;
            SortedList[] dict = new SortedList[length];
            string[] keyField = new string[length];
            SaveOptionEnum[] soe = new SaveOptionEnum[length];
            EntityTypeEnum[] es = new EntityTypeEnum[length];
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                dict[i] = new SortedList();
                string str = this.GetSignValue(EntityTypeEnum.EsProjectType, "FId", "FNewsId='" + fid + "' and fcolnumber='" + strArray[i] + "'");
                if ((str != null) && (str != ""))
                {
                    dict[i].Add("FID", str);
                    dict[i].Add("FState", title.FState);
                    dict[i].Add("FCreateTime", DateTime.Now.ToString());
                    dict[i].Add("FOrder", title.FOrder);
                    soe[i] = SaveOptionEnum.Update;
                }
                else
                {
                    dict[i].Add("FID", Guid.NewGuid().ToString());
                    dict[i].Add("FIsDeleted", 0);
                    dict[i].Add("FState", title.FState);
                    dict[i].Add("FOrder", title.FOrder);
                    dict[i].Add("FPubTime", DateTime.Now.ToString());
                    dict[i].Add("FColor", title.FColor);
                    dict[i].Add("FColNumber", strArray[i]);
                    dict[i].Add("FNewsId", fid);
                    dict[i].Add("FCreateTime", DateTime.Now.ToString());
                    soe[i] = SaveOptionEnum.Insert;
                }
                if (i == 0)
                {
                    builder.Append("'" + strArray[i] + "'");
                }
                else
                {
                    builder.Append(",'" + strArray[i] + "'");
                }
                keyField[i] = "FID";
                es[i] = EntityTypeEnum.EsProjectType;
            }
            if (builder.Length > 0)
            {
                this.PExcute(" delete from cf_news_Col where fnewsid='" + fid + "' and  FColNumber not in(" + builder.ToString() + ")");
            }
            return this.SaveEBaseM(es, dict, keyField, soe);
        }

        public bool Save(IConnection Cn, IEBase ent, SaveOptionEnum opt)
        {
            return this.Pbase.Save(Cn, ent, opt);
        }

        public bool Save(IConnection Cn, IEBase ent, SaveOptionEnum opt, SqlTransaction trans)
        {
            return this.Pbase.Save(Cn, ent, opt, trans);
        }

        public bool Save(IConnection Cn, IEBase ent, string keyField, SaveOptionEnum opt)
        {
            return this.Pbase.Save(Cn, ent, keyField, opt);
        }

        public bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, SaveOptionEnum opt)
        {
            return this.Pbase.Save(Cn, dict, eType, opt);
        }

        public bool Save(IConnection Cn, IEBase ent, string keyField, SaveOptionEnum opt, SqlTransaction trans)
        {
            return this.Pbase.Save(Cn, ent, keyField, opt, trans);
        }

        public bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, SaveOptionEnum opt, SqlTransaction trans)
        {
            return this.Pbase.Save(Cn, dict, eType, opt, trans);
        }

        public bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, string keyField, SaveOptionEnum opt)
        {
            return this.Pbase.Save(Cn, dict, eType, keyField, opt);
        }

        public bool Save(IConnection Cn, IDictionary dict, string sTableName, string keyField, SaveOptionEnum opt)
        {
            return this.Pbase.Save(Cn, dict, sTableName, keyField, opt);
        }

        public bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, string keyField, SaveOptionEnum opt, SqlTransaction trans)
        {
            return this.Pbase.Save(Cn, dict, eType, keyField, opt, trans);
        }

        public bool SaveEBase(EntityTypeEnum Es, EBase eb, string keyField, SaveOptionEnum soe)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                IDictionary data = eb.GetData();
                flag2 = this.Save(this.Cn_e, data, Es, keyField, soe);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return flag2;
        }

        public bool SaveEBase(EntityTypeEnum Es, IDictionary dict, string keyField, SaveOptionEnum soe)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                flag2 = this.Save(this.Cn_e, dict, Es, keyField, soe);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return flag2;
        }

        public bool SaveEBase(string sTableName, IDictionary dict, string keyField, SaveOptionEnum soe)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                flag2 = this.Save(this.Cn_e, dict, sTableName, keyField, soe);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return flag2;
        }

        public bool SaveEBaseM(EntityTypeEnum[] Es, EBase[] eb, string[] keyField, SaveOptionEnum[] soe)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                int length = eb.Length;
                IDictionary[] dict = new IDictionary[length];
                for (int i = 0; i < length; i++)
                {
                    dict[i] = eb[i].GetData();
                }
                flag2 = this.SaveM(this.Cn_e, dict, Es, keyField, soe);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return flag2;
        }

        public bool SaveEBaseM(EntityTypeEnum[] Es, IDictionary[] dict, string[] keyField, SaveOptionEnum[] soe)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref this.Cn_e, ref isExistCn, this.pDBName);
            try
            {
                flag2 = this.SaveM(this.Cn_e, dict, Es, keyField, soe);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref this.Cn_e, isExistCn);
            }
            return flag2;
        }

        public bool SaveException(Exception e, string User, string ErrorCodePostion)
        {
            return true;
        }

        public bool SaveM(IConnection Cn, IDictionary[] dict, EntityTypeEnum[] eType, string[] keyField, SaveOptionEnum[] opt)
        {
            return this.Pbase.SaveM(Cn, dict, eType, keyField, opt);
        }

        public string StrToDate(string date)
        {
            try
            {
                return Convert.ToDateTime(date).ToString("yyyy-MM-dd").Replace("1900-01-01", "");
            }
            catch (Exception)
            {
                return date;
            }
        }

        public string SubStr(string str, int subLength)
        {
            if (str == "&nbsp;")
            {
                return "";
            }
            if ((str == null) || (str == ""))
            {
                return "";
            }
            if (str.Length <= subLength)
            {
                return str;
            }
            return (str.Substring(0, subLength) + "..");
        }

        public string ToDBC(string input)
        {
            char[] chArray = input.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (chArray[i] == '　')
                {
                    chArray[i] = ' ';
                }
                else if ((chArray[i] > 0xff00) && (chArray[i] < 0xff5f))
                {
                    chArray[i] = (char) (chArray[i] - 0xfee0);
                }
            }
            return new string(chArray);
        }

        public string txtStatic(string FId)
        {
            StringBuilder builder = new StringBuilder();
            DataTable table = new DataTable();
            int num = 0;
            builder.Append("select top 1 fcount from CF_News_Title where ");
            builder.Append(" fid='" + FId + "'; ");
            table = this.GetTable(builder.ToString());
            if (table.Rows.Count > 0)
            {
                if (table.Rows[0][0].ToString() != "")
                {
                    num = int.Parse(table.Rows[0][0].ToString());
                }
            }
            else
            {
                num = 0;
            }
            num++;
            builder.Append(" update CF_News_Title set fcount=" + num.ToString() + " where ");
            builder.Append(" fid='" + FId + "'; ");
            this.PExcute(builder.ToString());
            return num.ToString();
        }

        public void webAddStatic()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update CF_Sys_SystemConfig set fvalue=fvalue+1 where fid=1;");
            this.PExcute(builder.ToString());
        }

        public string webSelectStatic()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 fvalue from CF_Sys_SystemConfig where fid=1 ");
            DataTable table = this.GetTable(builder.ToString());
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            return "0";
        }

        public string webStatic()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update CF_Sys_SystemConfig set fvalue=fvalue+1 where fid=1;");
            builder.Append("select top 1 fvalue from CF_Sys_SystemConfig where fid=1 ");
            DataTable table = this.GetTable(builder.ToString());
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            return "0";
        }

        public IBaseTools baseTools
        {
            get
            {
                return this._baseTools;
            }
            set
            {
                this._baseTools = value;
            }
        }

        public string GetMaxDate
        {
            get
            {
                return DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        protected IPBase Pbase
        {
            get
            {
                if (this.m_PBase == null)
                {
                    this.m_PBase = new PBase(this.baseTools);
                }
                return this.m_PBase;
            }
        }
    }
}

