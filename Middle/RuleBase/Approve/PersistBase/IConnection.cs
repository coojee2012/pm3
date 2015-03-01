namespace Approve.PersistBase
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public interface IConnection
    {
        void BeginTransaction();
        void BeginTransaction(IsolationLevel isolationLevel);
        void Close();
        ICommand CreateCommand();
        void Dispose();
        object Execute(string strSql, SqlResultEnum resType, params IDbDataParameter[] parameters);
        object Execute(string strSql, SqlResultEnum resType, SqlTransaction sTrans);
        void Open();
        void Open(string ConnectionString);
        void TransCommit();
        void TransRollBack();

        string ConnectionString { get; set; }

        int ConnectionTimeout { get; }

        DatabaseTypeEnum DatabaseType { get; set; }

        object rawConnection { get; }

        ConnectionState State { get; }
    }
}

