namespace Approve.PersistBase
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public interface ICommand
    {
        object AddParameter(string parameterName, Enum DbType);
        object AddParameter(string parameterName, object pvalue);
        object AddParameter(string parameterName, Enum DbType, int size);
        object AddParameter(string parameterName, Enum DbType, int size, string sourceColumn);
        bool ClearParameter();
        bool DelParameter(int pos);
        bool DelParameter(object parameter);
        bool DelParameter(string Name);
        void Dispose();
        object Execute(string strSql, SqlResultEnum resType, params IDbDataParameter[] parameters);
        object Execute(string strSql, SqlResultEnum resType, SqlTransaction sTrans);

        string CommandText { get; set; }

        int CommandTimeout { get; set; }

        System.Data.CommandType CommandType { get; set; }

        IConnection Connection { get; set; }

        object Parameters { get; }

        object rawCommand { get; }
    }
}

