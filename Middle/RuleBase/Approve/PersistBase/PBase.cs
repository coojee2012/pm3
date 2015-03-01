namespace Approve.PersistBase
{
    using Approve.EntityBase;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;

    public class PBase : IPBase, IPBaseQuery, IPBaseUpdate
    {
        private IBaseTools _baseTools;
        private PBaseQuery m_Query;
        private PBaseUpdate m_Update;

        public PBase()
        {
            this.m_Query = new PBaseQuery();
            this.m_Query.baseTools = this.baseTools;
            this.m_Update = new PBaseUpdate();
            this.m_Update.baseTools = this.baseTools;
        }

        public PBase(IBaseTools b)
        {
            this.m_Query = new PBaseQuery();
            this.m_Query.baseTools = b;
            this.m_Update = new PBaseUpdate();
            this.m_Update.baseTools = b;
        }

        public static IEBase[] ConstructEntityA(EntityTypeEnum eType, int count)
        {
            return null;
        }

        public int Count(IConnection Cn, EntityTypeEnum eType, string condition, params IDbDataParameter[] parameters)
        {
            return this.m_Query.Count(Cn, eType, condition, parameters);
        }

        public int Count(IConnection Cn, string strMulTblSql, string condition, params IDbDataParameter[] parameters)
        {
            return this.m_Query.Count(Cn, strMulTblSql, condition, parameters);
        }

        public static void CreateCn(ref IConnection Cn, ref bool isExistCn)
        {
            Exception exception;
            if (Cn == null)
            {
                Cn = new Connection();
                if (Cn.State != ConnectionState.Open)
                {
                    try
                    {
                        Cn.Open();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        throw exception;
                    }
                }
                isExistCn = true;
            }
            else if (Cn.State != ConnectionState.Open)
            {
                try
                {
                    Cn.Open();
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    throw exception;
                }
                isExistCn = true;
            }
        }

        public static void CreateCn(ref IConnection Cn, ref bool isExistCn, string sDBName)
        {
            Exception exception;
            if (Cn == null)
            {
                Cn = new Connection(sDBName);
                if (Cn.State != ConnectionState.Open)
                {
                    try
                    {
                        Cn.Open();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        throw exception;
                    }
                }
                isExistCn = true;
            }
            else if (Cn.State != ConnectionState.Open)
            {
                try
                {
                    Cn.Open();
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    throw exception;
                }
                isExistCn = true;
            }
        }

        public bool Del(IConnection Cn, IEBase ent, bool IsFact)
        {
            return this.m_Update.Del(Cn, ent, IsFact);
        }

        public bool Del(IConnection Cn, IEBase ent, bool IsFact, SqlTransaction trans)
        {
            return this.m_Update.Del(Cn, ent, IsFact, trans);
        }

        public bool Del(IConnection Cn, IEBase ent, bool IsFact, string keyField)
        {
            return this.m_Update.Del(Cn, ent, IsFact, keyField);
        }

        public bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact)
        {
            return this.m_Update.Del(Cn, dict, eType, IsFact);
        }

        public bool Del(IConnection Cn, EntityTypeEnum eType, string condition, bool IsFact, params IDbDataParameter[] parameters)
        {
            return this.m_Update.Del(Cn, eType, condition, IsFact, parameters);
        }

        public bool Del(IConnection Cn, EntityTypeEnum eType, string condition, bool IsFact, SqlTransaction trans)
        {
            return this.m_Update.Del(Cn, eType, condition, IsFact, trans);
        }

        public bool Del(IConnection Cn, IEBase ent, bool IsFact, string keyField, SqlTransaction trans)
        {
            return this.m_Update.Del(Cn, ent, IsFact, keyField, trans);
        }

        public bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact, SqlTransaction trans)
        {
            return this.m_Update.Del(Cn, dict, eType, IsFact, trans);
        }

        public bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact, string keyField)
        {
            return this.m_Update.Del(Cn, dict, eType, IsFact, keyField);
        }

        public bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact, string keyField, SqlTransaction trans)
        {
            return this.m_Update.Del(Cn, dict, eType, IsFact, keyField, trans);
        }

        public static void DisposeCn(ref IConnection Cn, bool isExistCn)
        {
            if (isExistCn)
            {
                Cn.Dispose();
            }
        }

        public object ExecuteSql(IConnection Cn, string strSql, SqlResultEnum rType)
        {
            bool isExistCn = false;
            CreateCn(ref Cn, ref isExistCn);
            object obj2 = new object();
            try
            {
                obj2 = Cn.Execute(strSql, rType, new IDbDataParameter[0]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                DisposeCn(ref Cn, isExistCn);
            }
            return obj2;
        }

        public object ExecuteSql(IConnection Cn, string strSql, SqlResultEnum rType, SqlTransaction trans)
        {
            bool isExistCn = false;
            CreateCn(ref Cn, ref isExistCn);
            object obj2 = new object();
            try
            {
                obj2 = Cn.Execute(strSql, rType, trans);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                DisposeCn(ref Cn, isExistCn);
            }
            return obj2;
        }

        public string GetEntityName(EntityTypeEnum eType)
        {
            return this.baseTools.GetName(eType);
        }

        public DataTable GetPageTable(IConnection Cn, string sql, int startRow, int pageSize)
        {
            return this.m_Query.GetPageTable(Cn, sql, startRow, pageSize);
        }

        public DataTable GetPageTable(IConnection Cn, EntityTypeEnum entType, string ReduceOption, string Condition, int PageSize, int currPage)
        {
            return this.m_Query.GetPageTable(Cn, entType, ReduceOption, Condition, PageSize, currPage);
        }

        public DataTable GetPageTable(IConnection Cn, string strSqlQuery, string ReduceOption, string condition, int pageSize, int currPage)
        {
            return this.m_Query.GetPageTable(Cn, strSqlQuery, ReduceOption, condition, pageSize, currPage);
        }

        public IEBase GetSingle(IConnection Cn, IEBase ent, params IDbDataParameter[] parameters)
        {
            return this.m_Query.GetSingle(Cn, ent, parameters);
        }

        public IEBase GetSingle(IConnection Cn, EntityTypeEnum eType, string ReduceOption, string condition, params IDbDataParameter[] parameters)
        {
            return this.m_Query.GetSingle(Cn, eType, ReduceOption, condition, parameters);
        }

        public DataTable GetTable(IConnection Cn, EntityTypeEnum entType, string ReduceOption, string condition)
        {
            return this.m_Query.GetTable(Cn, entType, ReduceOption, condition);
        }

        public bool IsExsist(IConnection Cn, IEBase ent)
        {
            return this.m_Update.IsExsist(Cn, ent);
        }

        public bool IsExsist(IConnection Cn, EntityTypeEnum eType, string Condition)
        {
            return this.m_Update.IsExsist(Cn, eType, Condition);
        }

        public bool IsExsist(IConnection Cn, IEBase ent, string keyField)
        {
            return this.m_Update.IsExsist(Cn, ent, keyField);
        }

        public bool PExcute(IConnection Cn, string strSql, SqlResultEnum rType)
        {
            return this.m_Update.PExcute(Cn, strSql, rType);
        }

        public bool Save(IConnection Cn, IEBase ent, SaveOptionEnum opt)
        {
            return this.m_Update.Save(Cn, ent, opt);
        }

        public bool Save(IConnection Cn, IEBase ent, SaveOptionEnum opt, SqlTransaction trans)
        {
            return this.m_Update.Save(Cn, ent, opt, trans);
        }

        public bool Save(IConnection Cn, IEBase ent, string keyField, SaveOptionEnum opt)
        {
            return this.m_Update.Save(Cn, ent, keyField, opt);
        }

        public bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, SaveOptionEnum opt)
        {
            return this.m_Update.Save(Cn, dict, eType, opt);
        }

        public bool Save(IConnection Cn, IEBase ent, string keyField, SaveOptionEnum opt, SqlTransaction trans)
        {
            return this.m_Update.Save(Cn, ent, keyField, opt, trans);
        }

        public bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, SaveOptionEnum opt, SqlTransaction trans)
        {
            return this.m_Update.Save(Cn, dict, eType, opt, trans);
        }

        public bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, string keyField, SaveOptionEnum opt)
        {
            return this.m_Update.Save(Cn, dict, eType, keyField, opt);
        }

        public bool Save(IConnection Cn, IDictionary dict, string sTableName, string keyField, SaveOptionEnum opt)
        {
            return this.m_Update.Save(Cn, dict, sTableName, keyField, opt);
        }

        public bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, string keyField, SaveOptionEnum opt, SqlTransaction trans)
        {
            return this.m_Update.Save(Cn, dict, eType, keyField, opt, trans);
        }

        public bool SaveM(IConnection Cn, IDictionary[] dict, EntityTypeEnum[] eType, string[] keyField, SaveOptionEnum[] opt)
        {
            return this.m_Update.SaveM(Cn, dict, eType, keyField, opt);
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

