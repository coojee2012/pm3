namespace Approve.PersistBase
{
    using Approve.EntityBase;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.EnterpriseServices;
    using System.Text;

    [Transaction(TransactionOption.Supported)]
    public class PBaseUpdate : IPBaseUpdate
    {
        private IBaseTools _baseTools;

        public void AddLog(IConnection Cn, string fid, string title, string add_dt, string strsql, int log_type)
        {
        }

        private void AppendFieldstr(StringBuilder strSql, object obj, bool isLast, bool IsUpdate)
        {
            strSql.Append(obj.ToString());
            if (IsUpdate)
            {
                strSql.Append("=");
            }
            else if (!isLast)
            {
                strSql.Append(",");
            }
        }

        public void AppendValuestr(IConnection Cn, StringBuilder strSql, object obj, bool isLast)
        {
            if ((obj == null) || (obj == DBNull.Value))
            {
                strSql.Append("null");
                if (!isLast)
                {
                    strSql.Append(",");
                }
                return;
            }
            Type type = obj.GetType();
            TypeCode typeCode = Type.GetTypeCode(type);
            if (!(type.IsValueType || (typeCode == TypeCode.String)))
            {
                return;
            }
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    if (!((bool) obj))
                    {
                        strSql.Append("0");
                    }
                    else
                    {
                        strSql.Append("1");
                    }
                    goto Label_019D;

                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    strSql.Append(obj.ToString().Replace("'", "''"));
                    goto Label_019D;

                case TypeCode.DateTime:
                    if (Cn.DatabaseType != DatabaseTypeEnum.Oracle)
                    {
                        strSql.Append("'");
                        break;
                    }
                    strSql.Append("TO_DATE('");
                    break;

                default:
                    strSql.Append("'");
                    strSql.Append(obj.ToString().Replace("'", "''"));
                    strSql.Append("'");
                    goto Label_019D;
            }
            strSql.Append(obj.ToString().Replace("'", "''"));
            if (Cn.DatabaseType == DatabaseTypeEnum.Oracle)
            {
                strSql.Append("','YYYY-MM-DD HH24:MI:SS')");
            }
            else
            {
                strSql.Append("'");
            }
        Label_019D:
            if (!isLast)
            {
                strSql.Append(",");
            }
        }

        public static string BuildCondition(IConnection Cn, IDictionary dict, string keyField)
        {
            return BuildCondition(Cn, dict, keyField, -1);
        }

        public static string BuildCondition(IConnection Cn, IDictionary dict, string keyField, int index)
        {
            StringBuilder builder;
            if (dict == null)
            {
                return "";
            }
            if (dict.Count == 0)
            {
                return "";
            }
            if (keyField == "")
            {
                builder = new StringBuilder();
                if (index > -1)
                {
                    builder.Append("FID=@FID" + index + "FID");
                }
                else
                {
                    builder.Append("FID=@FID");
                }
                return builder.ToString();
            }
            builder = new StringBuilder();
            if (keyField.IndexOf(",") >= 0)
            {
                string[] strArray = keyField.Split(",".ToCharArray());
                foreach (string str in strArray)
                {
                    builder.Append(str);
                    if (Cn.DatabaseType == DatabaseTypeEnum.Oracle)
                    {
                        builder.Append("= ");
                        string str2 = str.ToUpper();
                        if (((str2 == "FVALIDEND") || (str2 == "FVALIDBEGIN")) || (str2 == "FTIME"))
                        {
                            builder.Append("TO_DATE('");
                            builder.Append(dict[str.ToUpper()]);
                            builder.Append("','YYYY-MM-DD HH24:MI:SS')");
                            builder.Append(" And ");
                        }
                        else
                        {
                            builder.Append("'");
                            builder.Append(dict[str.ToUpper()]);
                            builder.Append("'  And ");
                        }
                    }
                    else
                    {
                        builder.Append("= '");
                        builder.Append(dict[str.ToUpper()]);
                        builder.Append("'  And ");
                    }
                }
                builder.Remove(builder.Length - 5, 4);
            }
            else
            {
                builder.Append(keyField);
                builder.Append("= @");
                if (index > -1)
                {
                    builder.Append(keyField.ToUpper() + index + keyField.ToUpper());
                }
                else
                {
                    builder.Append(keyField.ToUpper());
                }
            }
            return builder.ToString();
        }

        public string BuildSqlbyDict(IConnection Cn, IDictionary dict, SqlOptTypeEnum type, string EntityName, string Condition)
        {
            return this.BuildSqlbyDict(Cn, dict, type, EntityName, Condition, -1);
        }

        public string BuildSqlbyDict(IConnection Cn, IDictionary dict, SqlOptTypeEnum type, string EntityName, string Condition, int index)
        {
            if ((dict == null) && (type != SqlOptTypeEnum.Delete))
            {
                return "";
            }
            StringBuilder strSql = new StringBuilder();
            switch (type)
            {
                case SqlOptTypeEnum.Update:
                    strSql.Append(" Update ");
                    strSql.Append(EntityName);
                    strSql.Append(" Set ");
                    strSql.AppendLine(this.GetUpdateFields(dict.Keys, index));
                    strSql.Append(" Where ");
                    strSql.Append(Condition);
                    break;

                case SqlOptTypeEnum.Insert:
                    strSql.Append(" Insert Into ");
                    strSql.Append(EntityName);
                    strSql.Append(" (");
                    foreach (string str in dict.Keys)
                    {
                        this.AppendFieldstr(strSql, str, false, false);
                    }
                    strSql.Remove(strSql.Length - 1, 1);
                    strSql.Append(" ) Values ( ");
                    strSql.AppendLine(this.GetInsertFields(dict.Keys, index));
                    strSql.Append(" ) ");
                    break;

                case SqlOptTypeEnum.Delete:
                    strSql.Append(" Delete from ");
                    strSql.Append(EntityName);
                    strSql.Append(" where ");
                    strSql.Append(Condition);
                    break;

                case SqlOptTypeEnum.Select:
                    strSql.Append(" Select ");
                    foreach (string str2 in dict.Keys)
                    {
                        this.AppendFieldstr(strSql, str2, false, false);
                    }
                    strSql.Append(" From ");
                    strSql.Append(EntityName);
                    strSql.Append(" where ");
                    strSql.Append(Condition);
                    break;
            }
            return strSql.ToString();
        }

        [AutoComplete]
        public virtual bool Del(IConnection Cn, IEBase ent, bool IsFact)
        {
            bool flag2;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                if (ent == null)
                {
                    return false;
                }
                flag2 = this.Del(Cn, ent.GetData(), ent.EntityType, IsFact, "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Del(IConnection Cn, IEBase ent, bool IsFact, SqlTransaction trans)
        {
            bool flag2;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                if (ent == null)
                {
                    return false;
                }
                flag2 = this.Del(Cn, ent.GetData(), ent.EntityType, IsFact, "", trans);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Del(IConnection Cn, IEBase ent, bool IsFact, string keyField)
        {
            bool flag2;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                if (ent == null)
                {
                    return false;
                }
                flag2 = this.Del(Cn, ent.GetData(), ent.EntityType, IsFact, keyField);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact)
        {
            bool flag2;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                flag2 = this.Del(Cn, dict, eType, IsFact, "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Del(IConnection Cn, EntityTypeEnum eType, string condition, bool IsFact, params IDbDataParameter[] parameters)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref Cn, ref isExistCn);
            if (this.GetEntityName(eType) == "")
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            if (IsFact)
            {
                builder.Append(" Delete from ");
                builder.Append(this.GetEntityName(eType));
            }
            else
            {
                builder.Append(" Update ");
                builder.Append(this.GetEntityName(eType));
                if (Cn.DatabaseType == DatabaseTypeEnum.Oracle)
                {
                    builder.Append(" Set FTIME=TO_DATE('");
                    builder.Append(DateTime.Now.ToString());
                    builder.Append("','YYYY-MM-DD HH24:MI:SS')");
                }
                else
                {
                    builder.Append(" Set FTIME='");
                    builder.Append(DateTime.Now.ToString());
                    builder.Append("'");
                }
                builder.Append(",FISDELETED=");
                builder.Append("1");
            }
            if (condition != "")
            {
                builder.Append(" Where ");
                builder.Append(condition);
            }
            try
            {
                Cn.Execute(builder.ToString(), SqlResultEnum.No, new IDbDataParameter[0]);
                flag2 = true;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, builder.ToString(), exception.Message, 1);
                throw exception;
            }
            finally
            {
                DataLog.AddLog(Cn, builder.ToString(), "", 3);
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        public virtual bool Del(IConnection Cn, EntityTypeEnum eType, string condition, bool IsFact, SqlTransaction trans)
        {
            bool flag2;
            bool isExistCn = false;
            PBase.CreateCn(ref Cn, ref isExistCn);
            if (this.GetEntityName(eType) == "")
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            if (IsFact)
            {
                builder.Append(" Delete from ");
                builder.Append(this.GetEntityName(eType));
            }
            else
            {
                builder.Append(" Update ");
                builder.Append(this.GetEntityName(eType));
                if (Cn.DatabaseType == DatabaseTypeEnum.Oracle)
                {
                    builder.Append(" Set FTIME=TO_DATE('");
                    builder.Append(DateTime.Now.ToString());
                    builder.Append("','YYYY-MM-DD HH24:MI:SS')");
                }
                else
                {
                    builder.Append(" Set FTIME='");
                    builder.Append(DateTime.Now.ToString());
                    builder.Append("'");
                }
                builder.Append(",FISDELETED=");
                builder.Append("1");
            }
            if (condition != "")
            {
                builder.Append(" Where ");
                builder.Append(condition);
            }
            try
            {
                Cn.Execute(builder.ToString(), SqlResultEnum.No, trans);
                flag2 = true;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, builder.ToString(), exception.Message, 1);
                throw exception;
            }
            finally
            {
                DataLog.AddLog(Cn, builder.ToString(), "", 3);
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        public virtual bool Del(IConnection Cn, IEBase ent, bool IsFact, string keyField, SqlTransaction trans)
        {
            bool flag2;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                if (ent == null)
                {
                    return false;
                }
                flag2 = this.Del(Cn, ent.GetData(), ent.EntityType, IsFact, keyField, trans);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact, SqlTransaction trans)
        {
            bool flag2;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                flag2 = this.Del(Cn, dict, eType, IsFact, "", trans);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact, string keyField)
        {
            string str;
            bool flag2;
            if (dict == null)
            {
                return false;
            }
            if (dict.Count == 0)
            {
                return false;
            }
            dict["FTIME"] = DateTime.Now;
            dict["FISDELETED"] = true;
            if (IsFact)
            {
                str = this.BuildSqlbyDict(Cn, dict, SqlOptTypeEnum.Delete, this.GetEntityName(eType), BuildCondition(Cn, dict, keyField));
            }
            else
            {
                dict["FTIME"] = DateTime.Now;
                dict["FISDELETED"] = true;
                str = this.BuildSqlbyDict(Cn, dict, SqlOptTypeEnum.Update, this.GetEntityName(eType), BuildCondition(Cn, dict, keyField));
            }
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                Cn.Execute(str, SqlResultEnum.No, new IDbDataParameter[0]);
                flag2 = true;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, str, exception.Message, 1);
                throw exception;
            }
            finally
            {
                DataLog.AddLog(Cn, str, "", 3);
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        public virtual bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact, string keyField, SqlTransaction trans)
        {
            string str;
            bool flag2;
            if (dict == null)
            {
                return false;
            }
            if (dict.Count == 0)
            {
                return false;
            }
            dict["FTIME"] = DateTime.Now;
            dict["FISDELETED"] = true;
            if (IsFact)
            {
                str = this.BuildSqlbyDict(Cn, dict, SqlOptTypeEnum.Delete, this.GetEntityName(eType), BuildCondition(Cn, dict, keyField));
            }
            else
            {
                dict["FTIME"] = DateTime.Now;
                dict["FISDELETED"] = true;
                str = this.BuildSqlbyDict(Cn, dict, SqlOptTypeEnum.Update, this.GetEntityName(eType), BuildCondition(Cn, dict, keyField));
            }
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                Cn.Execute(str, SqlResultEnum.No, trans);
                flag2 = true;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, str, exception.Message, 1);
                throw exception;
            }
            finally
            {
                DataLog.AddLog(Cn, str, "", 3);
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        public object ExecuteSql(IConnection Cn, string strSql, SqlResultEnum rType)
        {
            bool isExistCn = false;
            PBase.CreateCn(ref Cn, ref isExistCn);
            object obj2 = new object();
            try
            {
                obj2 = Cn.Execute(strSql, rType, new IDbDataParameter[0]);
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, strSql, exception.Message, 1);
                throw exception;
            }
            finally
            {
                DataLog.AddLog(Cn, strSql, "", 3);
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return obj2;
        }

        public object ExecuteSql(IConnection Cn, string strSql, SqlResultEnum rType, SqlTransaction trans)
        {
            bool isExistCn = false;
            PBase.CreateCn(ref Cn, ref isExistCn);
            object obj2 = new object();
            try
            {
                obj2 = Cn.Execute(strSql, rType, trans);
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, strSql, exception.Message, 1);
                throw exception;
            }
            finally
            {
                DataLog.AddLog(Cn, strSql, "", 3);
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return obj2;
        }

        public string GetEntityName(EntityTypeEnum eType)
        {
            return this.baseTools.GetName(eType);
        }

        private string GetInsertFields(ICollection fields)
        {
            return this.GetInsertFields(fields, -1);
        }

        private string GetInsertFields(ICollection fields, int index)
        {
            string str = "";
            string str2 = "";
            if (index > -1)
            {
                str2 = str2 + index;
            }
            foreach (string str3 in fields)
            {
                string str5;
                if (index > -1)
                {
                    str5 = str;
                    str = str5 + "@" + str3 + str2 + str3 + ",";
                }
                else
                {
                    str5 = str;
                    str = str5 + "@" + str3 + str2 + ",";
                }
            }
            if (str.Length > 2)
            {
                return str.Substring(0, str.Length - 1);
            }
            Debug.WriteLine("插入的字段不能为空");
            return str;
        }

        public SqlParameter[] GetPrameters(IDictionary dict)
        {
            SqlParameter[] parameterArray = new SqlParameter[dict.Count];
            int num = 0;
            foreach (string str in dict.Keys)
            {
                object obj2 = dict[str];
                if (obj2 == null)
                {
                    obj2 = DBNull.Value;
                }
                parameterArray[num++] = new SqlParameter("@" + str, obj2);
            }
            return parameterArray;
        }

        private string GetUpdateFields(ICollection fields)
        {
            return this.GetUpdateFields(fields, -1);
        }

        private string GetUpdateFields(ICollection fields, int index)
        {
            string str = "";
            string str2 = "";
            if (index > -1)
            {
                str2 = str2 + index;
            }
            foreach (string str3 in fields)
            {
                string str5;
                if (index > -1)
                {
                    str5 = str;
                    str = str5 + str3 + "=@" + str3 + str2 + str3 + ",";
                }
                else
                {
                    str5 = str;
                    str = str5 + str3 + "=@" + str3 + str2 + ",";
                }
            }
            if (str.Length > 3)
            {
                return str.Substring(0, str.Length - 1);
            }
            Debug.WriteLine("更新的字段不能为空");
            return str;
        }

        public bool IsExsist(IConnection Cn, IEBase ent)
        {
            bool flag2;
            bool isExistCn = false;
            StringBuilder builder = new StringBuilder();
            builder.Append(" FId='");
            builder.Append(ent.FId);
            builder.Append(" ' ");
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                flag2 = this.IsExsist(Cn, ent.EntityType, builder.ToString());
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        public bool IsExsist(IConnection Cn, EntityTypeEnum eType, string Condition)
        {
            bool flag2;
            StringBuilder builder = new StringBuilder();
            builder.Append("  Select FID from ");
            builder.Append(this.GetEntityName(eType));
            if (Condition != "")
            {
                builder.Append(" Where ");
                builder.Append(Condition);
            }
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                if (Cn.Execute(builder.ToString(), SqlResultEnum.Have, new IDbDataParameter[0]) == null)
                {
                    return false;
                }
                flag2 = true;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, builder.ToString(), exception.Message, 1);
                throw exception;
            }
            finally
            {
                DataLog.AddLog(Cn, builder.ToString(), "", 3);
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        public bool IsExsist(IConnection Cn, IEBase ent, string keyField)
        {
            bool flag2;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                if (keyField == "")
                {
                    return this.IsExsist(Cn, ent);
                }
                string condition = BuildCondition(Cn, ent.GetData(), keyField);
                flag2 = this.IsExsist(Cn, ent.EntityType, condition);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        public bool PExcute(IConnection Cn, string strSql, SqlResultEnum rType)
        {
            bool isExistCn = false;
            PBase.CreateCn(ref Cn, ref isExistCn);
            SqlTransaction sTrans = ((SqlConnection) Cn.rawConnection).BeginTransaction();
            bool flag2 = true;
            try
            {
                Cn.Execute(strSql, rType, sTrans);
                sTrans.Commit();
            }
            catch (Exception exception)
            {
                sTrans.Rollback();
                DataLog.AddLog(Cn, strSql, exception.Message, 1);
                flag2 = false;
                throw exception;
            }
            finally
            {
                DataLog.AddLog(Cn, strSql, "", 3);
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Save(IConnection Cn, IEBase ent, SaveOptionEnum opt)
        {
            bool flag2;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                flag2 = this.Save(Cn, ent.GetData(), ent.EntityType, "", opt);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Save(IConnection Cn, IEBase ent, SaveOptionEnum opt, SqlTransaction trans)
        {
            bool flag2;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                flag2 = this.Save(Cn, ent.GetData(), ent.EntityType, "", opt, trans);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Save(IConnection Cn, IEBase ent, string keyField, SaveOptionEnum opt)
        {
            bool flag2;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                if (ent == null)
                {
                    return false;
                }
                flag2 = this.Save(Cn, ent.GetData(), ent.EntityType, keyField, opt);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, SaveOptionEnum opt)
        {
            bool flag2;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                flag2 = this.Save(Cn, dict, eType, "", opt);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Save(IConnection Cn, IEBase ent, string keyField, SaveOptionEnum opt, SqlTransaction trans)
        {
            bool flag2;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                if (ent == null)
                {
                    return false;
                }
                flag2 = this.Save(Cn, ent.GetData(), ent.EntityType, keyField, opt, trans);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, SaveOptionEnum opt, SqlTransaction trans)
        {
            bool flag2;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                flag2 = this.Save(Cn, dict, eType, "", opt, trans);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, string keyField, SaveOptionEnum opt)
        {
            bool flag2;
            string strSql = "";
            if (dict == null)
            {
                return false;
            }
            dict["FTIME"] = DateTime.Now;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                string condition = BuildCondition(Cn, dict, keyField);
                if (opt == SaveOptionEnum.Unknown)
                {
                    if (this.IsExsist(Cn, eType, condition))
                    {
                        opt = SaveOptionEnum.Update;
                    }
                    else
                    {
                        opt = SaveOptionEnum.Insert;
                    }
                }
                if (opt == SaveOptionEnum.Insert)
                {
                    strSql = this.BuildSqlbyDict(Cn, dict, SqlOptTypeEnum.Insert, this.GetEntityName(eType), condition);
                }
                else
                {
                    strSql = this.BuildSqlbyDict(Cn, dict, SqlOptTypeEnum.Update, this.GetEntityName(eType), condition);
                }
                Cn.Execute(strSql, SqlResultEnum.No, this.GetPrameters(dict));
                flag2 = true;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, strSql, exception.Message, 1);
                throw exception;
            }
            finally
            {
                DataLog.AddLog(Cn, strSql, "", 3);
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Save(IConnection Cn, IDictionary dict, string sTableName, string keyField, SaveOptionEnum opt)
        {
            bool flag2;
            string strSql = "";
            if (dict == null)
            {
                return false;
            }
            dict["FTIME"] = DateTime.Now;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                string condition = BuildCondition(Cn, dict, keyField);
                if (opt == SaveOptionEnum.Insert)
                {
                    strSql = this.BuildSqlbyDict(Cn, dict, SqlOptTypeEnum.Insert, sTableName, condition);
                }
                else
                {
                    strSql = this.BuildSqlbyDict(Cn, dict, SqlOptTypeEnum.Update, sTableName, condition);
                }
                IDbDataParameter[] prameters = this.GetPrameters(dict);
                Cn.Execute(strSql, SqlResultEnum.No, prameters);
                flag2 = true;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, strSql, exception.Message, 1);
                throw exception;
            }
            finally
            {
                DataLog.AddLog(Cn, strSql, "", 3);
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, string keyField, SaveOptionEnum opt, SqlTransaction trans)
        {
            bool flag2;
            string strSql = "";
            if (dict == null)
            {
                return false;
            }
            dict["FTIME"] = DateTime.Now;
            bool isExistCn = false;
            try
            {
                PBase.CreateCn(ref Cn, ref isExistCn);
                string condition = BuildCondition(Cn, dict, keyField);
                if (opt == SaveOptionEnum.Unknown)
                {
                    if (this.IsExsist(Cn, eType, condition))
                    {
                        opt = SaveOptionEnum.Update;
                    }
                    else
                    {
                        opt = SaveOptionEnum.Insert;
                    }
                }
                if (opt == SaveOptionEnum.Insert)
                {
                    strSql = this.BuildSqlbyDict(Cn, dict, SqlOptTypeEnum.Insert, this.GetEntityName(eType), condition);
                }
                else
                {
                    strSql = this.BuildSqlbyDict(Cn, dict, SqlOptTypeEnum.Update, this.GetEntityName(eType), condition);
                }
                Cn.Execute(strSql, SqlResultEnum.No, trans);
                flag2 = true;
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, strSql, exception.Message, 1);
                throw exception;
            }
            finally
            {
                DataLog.AddLog(Cn, strSql, "", 3);
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
        }

        [AutoComplete]
        public virtual bool SaveM(IConnection Cn, IDictionary[] dict, EntityTypeEnum[] eType, string[] keyField, SaveOptionEnum[] opt)
        {
            bool flag2;
            string str = "";
            StringBuilder builder = new StringBuilder();
            if ((dict == null) || (dict.Length <= 0))
            {
                return false;
            }
            bool isExistCn = false;
            PBase.CreateCn(ref Cn, ref isExistCn);
            SqlConnection rawConnection = (SqlConnection) Cn.rawConnection;
            SqlTransaction transaction = rawConnection.BeginTransaction();
            try
            {
                int num2;
                builder = new StringBuilder();
                int num = 0;
                num += dict.Length;
                for (num2 = 0; num2 < dict.Length; num2++)
                {
                    num += dict[num2].Count;
                }
                SqlCommand command = null;
                command = rawConnection.CreateCommand();
                command.Transaction = transaction;
                int length = dict.Length;
                for (num2 = 0; num2 < length; num2++)
                {
                    if (num > 0x834)
                    {
                        builder.Remove(0, builder.Length);
                        command.Parameters.Clear();
                    }
                    IDictionary dictionary = dict[num2];
                    dictionary["FTIME"] = DateTime.Now;
                    string str2 = keyField[num2].ToString();
                    EntityTypeEnum enum2 = eType[num2];
                    SaveOptionEnum update = opt[num2];
                    string condition = BuildCondition(Cn, dictionary, str2, num2);
                    if (update == SaveOptionEnum.Unknown)
                    {
                        if (this.IsExsist(Cn, enum2, condition))
                        {
                            update = SaveOptionEnum.Update;
                        }
                        else
                        {
                            update = SaveOptionEnum.Insert;
                        }
                    }
                    if (update == SaveOptionEnum.Insert)
                    {
                        str = this.BuildSqlbyDict(Cn, dictionary, SqlOptTypeEnum.Insert, this.GetEntityName(enum2), condition, num2);
                    }
                    else
                    {
                        str = this.BuildSqlbyDict(Cn, dictionary, SqlOptTypeEnum.Update, this.GetEntityName(enum2), condition, num2);
                    }
                    builder.AppendLine(str);
                    foreach (string str4 in dictionary.Keys)
                    {
                        object obj2 = dictionary[str4];
                        if (obj2 == null)
                        {
                            obj2 = DBNull.Value;
                        }
                        command.Parameters.AddWithValue(string.Concat(new object[] { "@", str4, num2, str4 }), obj2);
                    }
                    if (num > 0x834)
                    {
                        command.CommandText = builder.ToString();
                        command.ExecuteNonQuery();
                    }
                }
                if (num <= 0x834)
                {
                    command.CommandText = builder.ToString();
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
                flag2 = true;
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                DataLog.AddLog(Cn, builder.ToString(), exception.Message, 1);
                throw exception;
            }
            finally
            {
                DataLog.AddLog(Cn, builder.ToString(), "", 3);
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return flag2;
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
    }
}

