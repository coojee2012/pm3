namespace Approve.PersistBase
{
    using Approve.EntityBase;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Text;

    public class PBaseQuery : IPBaseQuery
    {
        private IBaseTools _baseTools;

        private string BuildReduceSql(IConnection Cn, EntityTypeEnum eType, string ReduceOption, string condition, int Count)
        {
            string entityName = this.GetEntityName(eType);
            if (entityName == "")
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select ");
            if ((Count > 0) && ((Cn.DatabaseType == DatabaseTypeEnum.SQLServer) || (Cn.DatabaseType == DatabaseTypeEnum.Oledb)))
            {
                builder.Append(" Top ");
                builder.Append(Count.ToString());
                builder.Append(" ");
            }
            if (ReduceOption == "")
            {
                builder.Append(" * ");
            }
            else
            {
                builder.Append(ReduceOption);
            }
            builder.Append(" from ");
            builder.Append(entityName);
            if (condition != "")
            {
                builder.Append(" Where ");
                builder.Append(condition);
            }
            if ((Count > 0) && (Cn.DatabaseType == DatabaseTypeEnum.Oracle))
            {
                if (condition != "")
                {
                    builder.Append(" And rownum<=");
                }
                else
                {
                    builder.Append("  where rownum<=");
                }
                builder.Append(Count.ToString());
            }
            return builder.ToString();
        }

        public virtual int Count(IConnection Cn, EntityTypeEnum eType, string condition, params IDbDataParameter[] parameters)
        {
            bool isExistCn = false;
            PBase.CreateCn(ref Cn, ref isExistCn);
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select Count(*) from ");
            builder.Append(this.GetEntityName(eType));
            if (condition != "")
            {
                builder.Append(" Where ");
                builder.Append(condition);
            }
            object obj2 = null;
            try
            {
                obj2 = Cn.Execute(builder.ToString(), SqlResultEnum.Have, parameters);
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, builder.ToString(), exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public virtual int Count(IConnection Cn, string strMulTblSql, string condition, params IDbDataParameter[] parameters)
        {
            bool isExistCn = false;
            PBase.CreateCn(ref Cn, ref isExistCn);
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select count(*) from ");
            builder.Append("( " + strMulTblSql);
            if (condition != "")
            {
                builder.Append(" where " + condition + " ) aa ");
            }
            else
            {
                builder.Append(" ) aa ");
            }
            object obj2 = null;
            try
            {
                obj2 = Cn.Execute(builder.ToString(), SqlResultEnum.Have, parameters);
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, builder.ToString(), exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
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
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            return obj2;
        }

        public string GetEntityName(EntityTypeEnum eType)
        {
            return this.baseTools.GetName(eType);
        }

        public DataTable GetPageTable(IConnection Cn, string sql, int startRow, int pageSize)
        {
            DataSet dataSet = new DataSet();
            switch (Cn.DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                {
                    OleDbConnection rawConnection = (OleDbConnection) Cn.rawConnection;
                    new OleDbDataAdapter(sql, rawConnection).Fill(dataSet, startRow, pageSize, "new");
                    break;
                }
                case DatabaseTypeEnum.SQLServer:
                {
                    SqlConnection selectConnection = (SqlConnection) Cn.rawConnection;
                    new SqlDataAdapter(sql, selectConnection).Fill(dataSet, startRow, pageSize, "new");
                    break;
                }
                default:
                {
                    OleDbConnection connection3 = (OleDbConnection) Cn.rawConnection;
                    new OleDbDataAdapter(sql, connection3).Fill(dataSet, startRow, pageSize, "new");
                    break;
                }
            }
            return dataSet.Tables[0];
        }

        public virtual DataTable GetPageTable(IConnection Cn, EntityTypeEnum eType, string ReduceOption, string condition, int pageSize, int currPage)
        {
            bool isExistCn = false;
            PBase.CreateCn(ref Cn, ref isExistCn);
            if ((pageSize <= 0) || (currPage <= 0))
            {
                return null;
            }
            int num = pageSize * (currPage - 1);
            int num2 = pageSize * (currPage - 1);
            int num3 = pageSize * currPage;
            string entityName = this.GetEntityName(eType);
            if (entityName == "")
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select ");
            if ((Cn.DatabaseType == DatabaseTypeEnum.SQLServer) || (Cn.DatabaseType == DatabaseTypeEnum.Oledb))
            {
                builder.Append(" Top " + pageSize.ToString());
                if (ReduceOption == "")
                {
                    builder.Append(" * from ");
                }
                else
                {
                    builder.Append(" " + ReduceOption + " from ");
                }
                builder.Append(entityName);
                builder.Append(" Where FId not in ( select top ");
                builder.Append(num.ToString());
                builder.Append(" FId from ");
                builder.Append(entityName);
                if (condition != "")
                {
                    builder.Append(" Where  ");
                    builder.Append(condition);
                    builder.Append(") ");
                }
                else
                {
                    builder.Append(") ");
                }
                if (condition != "")
                {
                    builder.Append(" And ");
                    builder.Append(condition);
                }
            }
            else
            {
                builder.Append(" * from ( ");
                builder.Append(" select rownum rown, a.* from ( ");
                builder.Append(" select * from ");
                builder.Append(entityName);
                if (condition != "")
                {
                    builder.Append(" Where  ");
                    builder.Append(condition);
                }
                builder.Append(" ) a where rownum <= ");
                builder.Append(num3.ToString());
                builder.Append(" ) where rown >= ");
                builder.Append(num2.ToString());
            }
            object obj2 = null;
            try
            {
                obj2 = Cn.Execute(builder.ToString(), SqlResultEnum.Table, new IDbDataParameter[0]);
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, builder.ToString(), exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            if (obj2 == null)
            {
                return null;
            }
            return (DataTable) obj2;
        }

        public virtual DataTable GetPageTable(IConnection Cn, string strSqlQuery, string ReduceOption, string condition, int pageSize, int currPage)
        {
            bool isExistCn = false;
            PBase.CreateCn(ref Cn, ref isExistCn);
            if ((pageSize <= 0) || (currPage <= 0))
            {
                return null;
            }
            int num = pageSize * (currPage - 1);
            int num2 = pageSize * (currPage - 1);
            int num3 = pageSize * currPage;
            StringBuilder builder = new StringBuilder();
            builder.Append(" Select ");
            if ((Cn.DatabaseType == DatabaseTypeEnum.SQLServer) || (Cn.DatabaseType == DatabaseTypeEnum.Oledb))
            {
                builder.Append(" Top " + pageSize.ToString());
                if (ReduceOption == "")
                {
                    builder.Append(" * from ");
                }
                else
                {
                    builder.Append(" " + ReduceOption + " from ");
                }
                builder.Append("( " + strSqlQuery);
                if (condition != "")
                {
                    builder.Append(" where " + condition + " ) aa ");
                }
                else
                {
                    builder.Append(" ) aa ");
                }
                builder.Append(" Where ( FId not in ( select top ");
                builder.Append(num.ToString());
                builder.Append(" aa.FId from ");
                builder.Append("( " + strSqlQuery);
                if (condition != "")
                {
                    builder.Append(" where " + condition + " ) aa )) " + condition.Substring(condition.IndexOf("order")).Replace("a.", "").Replace("b.", "").Replace("c.", "").Replace("d.", "").Replace("e.", "").Replace("f.", ""));
                }
                else
                {
                    builder.Append(" ) aa )) ");
                }
            }
            else
            {
                builder.Append(" * from ( ");
                builder.Append(" select rownum rown, a.* from ( ");
                builder.Append(" select * from ");
                builder.Append(" ( " + strSqlQuery + " ) ");
                if (condition != "")
                {
                    builder.Append(" Where  ");
                    builder.Append(condition.Replace("aa.", ""));
                }
                builder.Append(" ) a where rownum <= ");
                builder.Append(num3.ToString());
                builder.Append(" ) where rown >= ");
                builder.Append(num2.ToString());
            }
            object obj2 = null;
            try
            {
                obj2 = Cn.Execute(builder.ToString(), SqlResultEnum.Table, new IDbDataParameter[0]);
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, builder.ToString(), exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            if (obj2 == null)
            {
                return null;
            }
            return (DataTable) obj2;
        }

        private IEBase GetResEntity(EntityTypeEnum eType, DataRow res, string ReduceOption)
        {
            IEBase base2 = null;
            base2 = this.baseTools.ConstructEntity(eType, res, null, ConstructTypeEnum.Row);
            if (ReduceOption != "")
            {
                base2.IsReduce = true;
                base2.ReduceOption = ReduceOption;
            }
            return base2;
        }

        public virtual IEBase GetSingle(IConnection Cn, IEBase ent, params IDbDataParameter[] parameters)
        {
            if (ent != null)
            {
                bool isExistCn = false;
                PBase.CreateCn(ref Cn, ref isExistCn);
                IDictionary data = ent.GetData();
                if (data == null)
                {
                    return null;
                }
                if (data.Count == 0)
                {
                    return null;
                }
                StringBuilder builder = new StringBuilder();
                builder.Append(" Select * from ");
                builder.Append(this.GetEntityName(ent.EntityType));
                string str = PBaseUpdate.BuildCondition(Cn, data, "");
                if (str != "")
                {
                    builder.Append(" where " + str);
                }
                object obj2 = null;
                try
                {
                    obj2 = Cn.Execute(builder.ToString(), SqlResultEnum.Row, parameters);
                }
                catch (Exception exception)
                {
                    DataLog.AddLog(Cn, builder.ToString(), exception.Message, 1);
                    throw exception;
                }
                finally
                {
                    PBase.DisposeCn(ref Cn, isExistCn);
                }
                if (obj2 != null)
                {
                    return this.GetResEntity(ent.EntityType, (DataRow) obj2, "");
                }
            }
            return null;
        }

        public virtual IEBase GetSingle(IConnection Cn, EntityTypeEnum eType, string ReduceOption, string condition, params IDbDataParameter[] parameters)
        {
            bool isExistCn = false;
            PBase.CreateCn(ref Cn, ref isExistCn);
            string strSql = this.BuildReduceSql(Cn, eType, ReduceOption, condition, 1);
            object obj2 = null;
            try
            {
                obj2 = Cn.Execute(strSql, SqlResultEnum.Row, parameters);
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, strSql, exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            if (obj2 != null)
            {
                return this.GetResEntity(eType, (DataRow) obj2, ReduceOption);
            }
            return null;
        }

        public virtual DataTable GetTable(IConnection Cn, EntityTypeEnum eType, string ReduceOption, string condition)
        {
            bool isExistCn = false;
            PBase.CreateCn(ref Cn, ref isExistCn);
            string sSql = this.BuildReduceSql(Cn, eType, ReduceOption, condition, -1);
            object obj2 = null;
            try
            {
                obj2 = Cn.Execute(sSql.ToString(), SqlResultEnum.Table, new IDbDataParameter[0]);
            }
            catch (Exception exception)
            {
                DataLog.AddLog(Cn, sSql, exception.Message, 1);
                throw exception;
            }
            finally
            {
                PBase.DisposeCn(ref Cn, isExistCn);
            }
            if (obj2 == null)
            {
                return null;
            }
            return (DataTable) obj2;
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

