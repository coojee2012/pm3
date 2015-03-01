namespace Approve.RuleBase
{
    using Approve.EntityBase;
    using Approve.PersistBase;
    using System;
    using System.Collections;
    using System.Data;

    public interface IRBase
    {
        bool ClearException();
        int Count(IConnection Cn, EntityTypeEnum eType, string condition, params IDbDataParameter[] parameters);
        int Count(IConnection Cn, string strMulTblSql, string condition, params IDbDataParameter[] parameters);
        bool Del(IConnection Cn, IEBase ent, bool IsFact);
        bool Del(IConnection Cn, IEBase ent, bool IsFact, string keyField);
        bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact);
        bool Del(IConnection Cn, EntityTypeEnum eType, string condition, bool IsFact, params IDbDataParameter[] parameters);
        bool Del(IConnection Cn, IDictionary dict, EntityTypeEnum eType, bool IsFact, string keyField);
        DataTable GetPageTable(IConnection Cn, EntityTypeEnum eType, string ReduceOption, string condition, int pageSize, int currPage);
        DataTable GetPageTable(IConnection Cn, string strSqlQuery, string ReduceOption, string condition, int pageSize, int currPage);
        IEBase GetSingle(IConnection Cn, IEBase ent, params IDbDataParameter[] parameters);
        IEBase GetSingle(IConnection Cn, EntityTypeEnum eType, string ReduceOption, string condition, params IDbDataParameter[] parameters);
        DataTable GetTable(IConnection Cn, EntityTypeEnum eType, string ReduceOption, string condition);
        bool IsExsist(IConnection Cn, IEBase ent);
        bool IsExsist(IConnection Cn, EntityTypeEnum eType, string Condition);
        bool IsExsist(IConnection Cn, IEBase ent, string keyField);
        bool Save(IConnection Cn, IEBase ent, SaveOptionEnum opt);
        bool Save(IConnection Cn, IEBase ent, string keyField, SaveOptionEnum opt);
        bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, SaveOptionEnum opt);
        bool Save(IConnection Cn, IDictionary dict, EntityTypeEnum eType, string keyField, SaveOptionEnum opt);
        bool Save(IConnection Cn, IDictionary dict, string sTableName, string keyField, SaveOptionEnum opt);
        bool SaveException(Exception e, string User, string ErrorCodePostion);
        bool SaveM(IConnection Cn, IDictionary[] dict, EntityTypeEnum[] eType, string[] keyField, SaveOptionEnum[] opt);
    }
}

