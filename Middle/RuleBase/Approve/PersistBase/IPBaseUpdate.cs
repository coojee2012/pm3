namespace Approve.PersistBase
{
    using Approve.EntityBase;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;

    public interface IPBaseUpdate
    {
        bool Del(IConnection Cn, IEBase ent, bool IsFact);
        bool Del(IConnection Cn, IEBase ent, bool IsFact, SqlTransaction trans);
        bool Del(IConnection Cn, IEBase ent, bool IsFact, string keyField);
        bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact);
        bool Del(IConnection Cn, EntityTypeEnum eType, string condition, bool IsFact, params IDbDataParameter[] parameters);
        bool Del(IConnection Cn, EntityTypeEnum eType, string condition, bool IsFact, SqlTransaction trans);
        bool Del(IConnection Cn, IEBase ent, bool IsFact, string keyField, SqlTransaction trans);
        bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact, SqlTransaction trans);
        bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact, string keyField);
        bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact, string keyField, SqlTransaction trans);
        object ExecuteSql(IConnection Cn, string strSql, SqlResultEnum rType);
        object ExecuteSql(IConnection Cn, string strSql, SqlResultEnum rType, SqlTransaction trans);
        string GetEntityName(EntityTypeEnum eType);
        bool IsExsist(IConnection Cn, IEBase ent);
        bool IsExsist(IConnection Cn, EntityTypeEnum eType, string Condition);
        bool IsExsist(IConnection Cn, IEBase ent, string keyField);
        bool PExcute(IConnection Cn, string strSql, SqlResultEnum rType);
        bool Save(IConnection Cn, IEBase ent, SaveOptionEnum opt);
        bool Save(IConnection Cn, IEBase ent, SaveOptionEnum opt, SqlTransaction trans);
        bool Save(IConnection Cn, IEBase ent, string keyField, SaveOptionEnum opt);
        bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, SaveOptionEnum opt);
        bool Save(IConnection Cn, IEBase ent, string keyField, SaveOptionEnum opt, SqlTransaction trans);
        bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, SaveOptionEnum opt, SqlTransaction trans);
        bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, string keyField, SaveOptionEnum opt);
        bool Save(IConnection Cn, IDictionary dict, string sTableName, string keyField, SaveOptionEnum opt);
        bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, string keyField, SaveOptionEnum opt, SqlTransaction trans);
        bool SaveM(IConnection Cn, IDictionary[] dict, EntityTypeEnum[] eType, string[] keyField, SaveOptionEnum[] opt);
    }
}

