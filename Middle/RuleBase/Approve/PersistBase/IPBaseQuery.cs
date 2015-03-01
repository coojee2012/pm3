namespace Approve.PersistBase
{
    using Approve.EntityBase;
    using System;
    using System.Data;

    public interface IPBaseQuery
    {
        int Count(IConnection Cn, EntityTypeEnum eType, string condition, params IDbDataParameter[] parameters);
        int Count(IConnection Cn, string strMulTblSql, string condition, params IDbDataParameter[] parameters);
        object ExecuteSql(IConnection Cn, string strSql, SqlResultEnum rType);
        string GetEntityName(EntityTypeEnum eType);
        DataTable GetPageTable(IConnection Cn, string sql, int startRow, int pageSize);
        DataTable GetPageTable(IConnection Cn, EntityTypeEnum eType, string ReduceOption, string condition, int pageSize, int currPage);
        DataTable GetPageTable(IConnection Cn, string strSqlQuery, string ReduceOption, string condition, int pageSize, int currPage);
        IEBase GetSingle(IConnection Cn, IEBase ent, params IDbDataParameter[] parameters);
        IEBase GetSingle(IConnection Cn, EntityTypeEnum eType, string ReduceOption, string condition, params IDbDataParameter[] parameters);
        DataTable GetTable(IConnection Cn, EntityTypeEnum eType, string ReduceOption, string condition);
    }
}

