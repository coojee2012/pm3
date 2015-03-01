namespace Approve.PersistBase
{
    using Approve.RuleBase;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Diagnostics;

    public sealed class Connection : IConnection
    {
        private ICommand m_Command;
        private object m_Connection;
        private DatabaseTypeEnum m_DatabaseType;
        private SqlTransaction Trans;

        public Connection()
        {
            this.Trans = null;
            this.GetConfig();
        }

        public Connection(string sDBName)
        {
            this.Trans = null;
            this.GetConfig(sDBName);
        }

        public Connection(string ConnectionString, DatabaseTypeEnum DbType)
        {
            OleDbConnection connection2;
            this.Trans = null;
            switch (DbType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    connection2 = new OleDbConnection();
                    connection2.ConnectionString = ConnectionString;
                    this.m_Connection = connection2;
                    break;

                case DatabaseTypeEnum.SQLServer:
                {
                    SqlConnection connection = new SqlConnection();
                    connection.ConnectionString = ConnectionString;
                    this.m_Connection = connection;
                    break;
                }
                default:
                    connection2 = new OleDbConnection();
                    connection2.ConnectionString = ConnectionString;
                    this.m_Connection = connection2;
                    break;
            }
            this.DatabaseType = DbType;
            this.m_Command = new Command(this);
        }

        public void BeginTransaction()
        {
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                {
                    OleDbConnection connection = (OleDbConnection) this.m_Connection;
                    break;
                }
                case DatabaseTypeEnum.SQLServer:
                    this.Trans = ((SqlConnection) this.m_Connection).BeginTransaction();
                    break;

                default:
                    ((OleDbConnection) this.m_Connection).BeginTransaction();
                    break;
            }
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    ((OleDbConnection) this.m_Connection).BeginTransaction(isolationLevel);
                    break;

                case DatabaseTypeEnum.SQLServer:
                    ((SqlConnection) this.m_Connection).BeginTransaction(isolationLevel);
                    break;

                default:
                    ((OleDbConnection) this.m_Connection).BeginTransaction(isolationLevel);
                    break;
            }
        }

        public void Close()
        {
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    ((OleDbConnection) this.m_Connection).Close();
                    break;

                case DatabaseTypeEnum.SQLServer:
                    ((SqlConnection) this.m_Connection).Close();
                    break;

                default:
                    ((OleDbConnection) this.m_Connection).Close();
                    break;
            }
        }

        public ICommand CreateCommand()
        {
            return new Command(this);
        }

        public void Dispose()
        {
            OleDbConnection connection2;
            this.m_Command.Dispose();
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                    break;

                case DatabaseTypeEnum.SQLServer:
                {
                    SqlConnection connection = (SqlConnection) this.m_Connection;
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                    break;
                }
                case DatabaseTypeEnum.Oledb:
                    connection2 = (OleDbConnection) this.m_Connection;
                    if (connection2.State != ConnectionState.Closed)
                    {
                        connection2.Close();
                    }
                    break;

                default:
                    connection2 = (OleDbConnection) this.m_Connection;
                    if (connection2.State != ConnectionState.Closed)
                    {
                        connection2.Close();
                    }
                    break;
            }
        }

        public object Execute(string strSql, SqlResultEnum resType, params IDbDataParameter[] parameters)
        {
            return this.m_Command.Execute(strSql, resType, parameters);
        }

        public object Execute(string strSql, SqlResultEnum resType, SqlTransaction sTrans)
        {
            System.Diagnostics.Debug.WriteLine(strSql);
            return this.m_Command.Execute(strSql, resType, sTrans);
        }

        private void GetConfig()
        {
            OleDbConnection connection2;
            string str = "";
            this.m_DatabaseType = DatabaseTypeEnum.SQLServer;
            switch (ConfigurationManager.AppSettings["dbType"])
            {
                case "FDC":
                    str = ConfigurationManager.AppSettings["ConFDC"];
                    break;

                case "ZLJC":
                    str = ConfigurationManager.AppSettings["ConZLJC"];
                    break;

                case "KCSJ":
                    str = ConfigurationManager.AppSettings["ConKCSJ"];
                    break;

                case "Person":
                    str = ConfigurationManager.AppSettings["ConPerson"];
                    break;

                case "ZBDL":
                    str = ConfigurationManager.AppSettings["ConZBDL"];
                    break;

                case "JLXT":
                    str = ConfigurationManager.AppSettings["ConJLXT"];
                    break;

                case "THourse":
                    str = ConfigurationManager.AppSettings["ConThourse"];
                    break;

                case "SXWeb":
                    str = ConfigurationManager.AppSettings["ConSXWeb"];
                    break;

                case "Ceeyi":
                    str = ConfigurationManager.AppSettings["ConCeeyi"];
                    break;
            }
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    connection2 = new OleDbConnection();
                    connection2.ConnectionString = str;
                    this.m_Connection = connection2;
                    break;

                case DatabaseTypeEnum.SQLServer:
                {
                    SqlConnection connection = new SqlConnection();
                    connection.ConnectionString = str;
                    this.m_Connection = connection;
                    break;
                }
                default:
                    connection2 = new OleDbConnection();
                    connection2.ConnectionString = str;
                    this.m_Connection = connection2;
                    break;
            }
            this.m_Command = new Command(this);
        }

        private void GetConfig(string sDBName)
        {
            OleDbConnection connection2;
            string connectionString = "";
            this.m_DatabaseType = DatabaseTypeEnum.SQLServer;
            connectionString = new LicenseTools().GetConnectionString(sDBName);
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    connection2 = new OleDbConnection();
                    connection2.ConnectionString = connectionString;
                    this.m_Connection = connection2;
                    break;

                case DatabaseTypeEnum.SQLServer:
                {
                    SqlConnection connection = new SqlConnection();
                    connection.ConnectionString = connectionString;
                    this.m_Connection = connection;
                    break;
                }
                default:
                    connection2 = new OleDbConnection();
                    connection2.ConnectionString = connectionString;
                    this.m_Connection = connection2;
                    break;
            }
            this.m_Command = new Command(this);
        }

        public void Open()
        {
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    ((OleDbConnection) this.m_Connection).Open();
                    break;

                case DatabaseTypeEnum.SQLServer:
                    ((SqlConnection) this.m_Connection).Open();
                    break;

                default:
                    ((OleDbConnection) this.m_Connection).Open();
                    break;
            }
        }

        public void Open(string ConnectionString)
        {
            OleDbConnection connection2;
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    connection2 = (OleDbConnection) this.m_Connection;
                    connection2.ConnectionString = ConnectionString;
                    connection2.Open();
                    break;

                case DatabaseTypeEnum.SQLServer:
                {
                    SqlConnection connection = (SqlConnection) this.m_Connection;
                    connection.ConnectionString = ConnectionString;
                    connection.Open();
                    break;
                }
                default:
                    connection2 = (OleDbConnection) this.m_Connection;
                    connection2.ConnectionString = ConnectionString;
                    connection2.Open();
                    break;
            }
        }

        public void TransCommit()
        {
            if (this.Trans != null)
            {
                this.Trans.Commit();
            }
        }

        public void TransRollBack()
        {
            if (this.Trans != null)
            {
                this.Trans.Rollback();
            }
        }

        public string ConnectionString
        {
            get
            {
                OleDbConnection connection2;
                switch (this.m_DatabaseType)
                {
                    case DatabaseTypeEnum.Oracle:
                    case DatabaseTypeEnum.Oledb:
                        connection2 = (OleDbConnection) this.m_Connection;
                        return connection2.ConnectionString;

                    case DatabaseTypeEnum.SQLServer:
                    {
                        SqlConnection connection = (SqlConnection) this.m_Connection;
                        return connection.ConnectionString;
                    }
                }
                connection2 = (OleDbConnection) this.m_Connection;
                return connection2.ConnectionString;
            }
            set
            {
                OleDbConnection connection2;
                switch (this.m_DatabaseType)
                {
                    case DatabaseTypeEnum.Oracle:
                    case DatabaseTypeEnum.Oledb:
                        connection2 = (OleDbConnection) this.m_Connection;
                        connection2.ConnectionString = value;
                        break;

                    case DatabaseTypeEnum.SQLServer:
                    {
                        SqlConnection connection = (SqlConnection) this.m_Connection;
                        connection.ConnectionString = value;
                        break;
                    }
                    default:
                        connection2 = (OleDbConnection) this.m_Connection;
                        connection2.ConnectionString = value;
                        break;
                }
            }
        }

        public int ConnectionTimeout
        {
            get
            {
                OleDbConnection connection2;
                switch (this.m_DatabaseType)
                {
                    case DatabaseTypeEnum.Oracle:
                    case DatabaseTypeEnum.Oledb:
                        connection2 = (OleDbConnection) this.m_Connection;
                        return connection2.ConnectionTimeout;

                    case DatabaseTypeEnum.SQLServer:
                    {
                        SqlConnection connection = (SqlConnection) this.m_Connection;
                        return connection.ConnectionTimeout;
                    }
                }
                connection2 = (OleDbConnection) this.m_Connection;
                return connection2.ConnectionTimeout;
            }
        }

        public DatabaseTypeEnum DatabaseType
        {
            get
            {
                return this.m_DatabaseType;
            }
            set
            {
                this.m_DatabaseType = value;
            }
        }

        public object rawConnection
        {
            get
            {
                return this.m_Connection;
            }
        }

        public ConnectionState State
        {
            get
            {
                OleDbConnection connection2;
                switch (this.m_DatabaseType)
                {
                    case DatabaseTypeEnum.Oracle:
                    case DatabaseTypeEnum.Oledb:
                        connection2 = (OleDbConnection) this.m_Connection;
                        return connection2.State;

                    case DatabaseTypeEnum.SQLServer:
                    {
                        SqlConnection connection = (SqlConnection) this.m_Connection;
                        return connection.State;
                    }
                }
                connection2 = (OleDbConnection) this.m_Connection;
                return connection2.State;
            }
        }
    }
}

