namespace Approve.PersistBase
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Diagnostics;

    public sealed class Command : ICommand
    {
        private object m_Command;
        private IConnection m_Connection;
        private DatabaseTypeEnum m_DatabaseType;

        public Command()
        {
            this.m_Command = new OleDbCommand();
            this.m_DatabaseType = DatabaseTypeEnum.Oledb;
        }

        public Command(IConnection connection)
        {
            OleDbCommand command2;
            this.m_DatabaseType = connection.DatabaseType;
            switch (connection.DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    command2 = new OleDbCommand();
                    command2.Connection = (OleDbConnection) connection.rawConnection;
                    this.m_Command = command2;
                    break;

                case DatabaseTypeEnum.SQLServer:
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = (SqlConnection) connection.rawConnection;
                    this.m_Command = command;
                    break;
                }
                default:
                    command2 = new OleDbCommand();
                    command2.Connection = (OleDbConnection) connection.rawConnection;
                    this.m_Command = command2;
                    break;
            }
            this.m_Connection = connection;
        }

        public Command(string cmdText, IConnection connection)
        {
            this.m_DatabaseType = connection.DatabaseType;
            switch (connection.DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    this.m_Command = new OleDbCommand(cmdText, (OleDbConnection) connection.rawConnection);
                    break;

                case DatabaseTypeEnum.SQLServer:
                    this.m_Command = new SqlCommand(cmdText, (SqlConnection) connection.rawConnection);
                    break;

                default:
                    this.m_Command = new OleDbCommand(cmdText, (OleDbConnection) connection.rawConnection);
                    break;
            }
            this.m_Connection = connection;
        }

        public object AddParameter(string parameterName, Enum DbType)
        {
            OleDbCommand command2;
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.Add(parameterName, (OleDbType) DbType);
                    break;

                case DatabaseTypeEnum.SQLServer:
                {
                    SqlCommand command = (SqlCommand) this.m_Command;
                    command.Parameters.Add(parameterName, (SqlDbType) DbType);
                    break;
                }
                default:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.Add(parameterName, (OleDbType) DbType);
                    break;
            }
            return true;
        }

        public object AddParameter(string parameterName, object pvalue)
        {
            OleDbCommand command2;
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.AddWithValue(parameterName, pvalue);
                    break;

                case DatabaseTypeEnum.SQLServer:
                {
                    SqlCommand command = (SqlCommand) this.m_Command;
                    command.Parameters.AddWithValue(parameterName, pvalue);
                    break;
                }
                default:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.AddWithValue(parameterName, pvalue);
                    break;
            }
            return true;
        }

        public object AddParameter(string parameterName, Enum DbType, int size)
        {
            OleDbCommand command2;
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.Add(parameterName, (OleDbType) DbType, size);
                    break;

                case DatabaseTypeEnum.SQLServer:
                {
                    SqlCommand command = (SqlCommand) this.m_Command;
                    command.Parameters.Add(parameterName, (SqlDbType) DbType, size);
                    break;
                }
                default:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.Add(parameterName, (OleDbType) DbType, size);
                    break;
            }
            return true;
        }

        public object AddParameter(string parameterName, Enum DbType, int size, string sourceColumn)
        {
            OleDbCommand command2;
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.Add(parameterName, (OleDbType) DbType, size, sourceColumn);
                    break;

                case DatabaseTypeEnum.SQLServer:
                {
                    SqlCommand command = (SqlCommand) this.m_Command;
                    command.Parameters.Add(parameterName, (SqlDbType) DbType, size, sourceColumn);
                    break;
                }
                default:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.Add(parameterName, (OleDbType) DbType, size, sourceColumn);
                    break;
            }
            return true;
        }

        public bool ClearParameter()
        {
            OleDbCommand command2;
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.Clear();
                    break;

                case DatabaseTypeEnum.SQLServer:
                {
                    SqlCommand command = (SqlCommand) this.m_Command;
                    command.Parameters.Clear();
                    break;
                }
                default:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.Clear();
                    break;
            }
            return true;
        }

        public bool DelParameter(int pos)
        {
            OleDbCommand command2;
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.RemoveAt(pos);
                    break;

                case DatabaseTypeEnum.SQLServer:
                {
                    SqlCommand command = (SqlCommand) this.m_Command;
                    command.Parameters.RemoveAt(pos);
                    break;
                }
                default:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.RemoveAt(pos);
                    break;
            }
            return true;
        }

        public bool DelParameter(object parameter)
        {
            OleDbCommand command2;
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.Remove(parameter);
                    break;

                case DatabaseTypeEnum.SQLServer:
                {
                    SqlCommand command = (SqlCommand) this.m_Command;
                    command.Parameters.Remove(parameter);
                    break;
                }
                default:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.Remove(parameter);
                    break;
            }
            return true;
        }

        public bool DelParameter(string name)
        {
            OleDbCommand command2;
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.RemoveAt(name);
                    break;

                case DatabaseTypeEnum.SQLServer:
                {
                    SqlCommand command = (SqlCommand) this.m_Command;
                    command.Parameters.RemoveAt(name);
                    break;
                }
                default:
                    command2 = (OleDbCommand) this.m_Command;
                    command2.Parameters.RemoveAt(name);
                    break;
            }
            return true;
        }

        public void Dispose()
        {
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                case DatabaseTypeEnum.Oledb:
                    ((OleDbCommand) this.m_Command).Dispose();
                    break;

                case DatabaseTypeEnum.SQLServer:
                    ((SqlCommand) this.m_Command).Dispose();
                    break;

                default:
                    ((OleDbCommand) this.m_Command).Dispose();
                    break;
            }
        }

        private void DisposeAdapter(IDbDataAdapter adapter)
        {
            if (adapter != null)
            {
                switch (this.m_DatabaseType)
                {
                    case DatabaseTypeEnum.Oracle:
                    case DatabaseTypeEnum.Oledb:
                        ((OleDbDataAdapter) adapter).Dispose();
                        return;

                    case DatabaseTypeEnum.SQLServer:
                        ((SqlDataAdapter) adapter).Dispose();
                        return;
                }
                ((OleDbDataAdapter) adapter).Dispose();
            }
        }

        public object Execute(string strSql, SqlResultEnum resType, params IDbDataParameter[] parameters)
        {
            OleDbCommand command2;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
            }
            Debug.WriteLine(strSql);
            object result = null;
            IDbDataAdapter adapter = null;
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                    if (resType != SqlResultEnum.No)
                    {
                        if (resType == SqlResultEnum.Have)
                        {
                        }
                        break;
                    }
                    break;

                case DatabaseTypeEnum.SQLServer:
                    SqlCommand command;
                    if (resType != SqlResultEnum.No)
                    {
                        if (resType == SqlResultEnum.Have)
                        {
                            command = (SqlCommand) this.m_Command;
                            command.CommandText = strSql;
                            command.CommandTimeout = 300;
                            while (command.Parameters.Count > 0)
                            {
                                command.Parameters.RemoveAt(0);
                            }
                            if (parameters != null)
                            {
                                foreach (SqlParameter parameter in parameters)
                                {
                                    command.Parameters.Add(parameter);
                                }
                            }
                            result = command.ExecuteScalar();
                        }
                        else
                        {
                            command = (SqlCommand) this.m_Command;
                            command.CommandText = strSql;
                            command.CommandTimeout = 300;
                            adapter = new SqlDataAdapter(command);
                            while (adapter.SelectCommand.Parameters.Count > 0)
                            {
                                adapter.SelectCommand.Parameters.RemoveAt(0);
                            }
                            if (parameters != null)
                            {
                                foreach (SqlParameter parameter in parameters)
                                {
                                    adapter.SelectCommand.Parameters.Add(parameter);
                                }
                            }
                            adapter = new SqlDataAdapter(command);
                        }
                        break;
                    }
                    command = (SqlCommand) this.m_Command;
                    command.CommandText = strSql;
                    command.CommandTimeout = 300;
                    while (command.Parameters.Count > 0)
                    {
                        command.Parameters.RemoveAt(0);
                    }
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                    break;

                case DatabaseTypeEnum.Oledb:
                    if (resType != SqlResultEnum.No)
                    {
                        if (resType == SqlResultEnum.Have)
                        {
                            command2 = (OleDbCommand) this.m_Command;
                            command2.CommandText = strSql;
                            command2.CommandTimeout = 300;
                            result = command2.ExecuteScalar();
                        }
                        else
                        {
                            command2 = (OleDbCommand) this.m_Command;
                            command2.CommandText = strSql;
                            command2.CommandTimeout = 300;
                            adapter = new OleDbDataAdapter(command2);
                        }
                        break;
                    }
                    command2 = (OleDbCommand) this.m_Command;
                    command2.CommandText = strSql;
                    command2.CommandTimeout = 300;
                    command2.ExecuteNonQuery();
                    break;

                default:
                    if (resType == SqlResultEnum.No)
                    {
                        command2 = (OleDbCommand) this.m_Command;
                        command2.CommandText = strSql;
                        command2.CommandTimeout = 300;
                        command2.ExecuteNonQuery();
                    }
                    else if (resType == SqlResultEnum.Have)
                    {
                        command2 = (OleDbCommand) this.m_Command;
                        command2.CommandText = strSql;
                        command2.CommandTimeout = 300;
                        result = command2.ExecuteScalar();
                    }
                    else
                    {
                        command2 = (OleDbCommand) this.m_Command;
                        command2.CommandText = strSql;
                        command2.CommandTimeout = 300;
                        adapter = new OleDbDataAdapter(command2);
                    }
                    break;
            }
            if (adapter != null)
            {
                result = this.GetResult(adapter, resType);
            }
            return result;
        }

        public object Execute(string strSql, SqlResultEnum resType, SqlTransaction sTrans)
        {
            object result = null;
            IDbDataAdapter adapter = null;
            OleDbCommand command2;
            switch (this.m_DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                    if (resType != SqlResultEnum.No)
                    {
                        if (resType == SqlResultEnum.Have)
                        {
                        }
                        break;
                    }
                    break;

                case DatabaseTypeEnum.SQLServer:
                    SqlCommand command;
                    if (resType != SqlResultEnum.No)
                    {
                        if (resType == SqlResultEnum.Have)
                        {
                            command = (SqlCommand) this.m_Command;
                            command.CommandText = strSql;
                            command.CommandTimeout = 300;
                            command.Transaction = sTrans;
                            result = command.ExecuteScalar();
                        }
                        else
                        {
                            command = (SqlCommand) this.m_Command;
                            command.CommandText = strSql;
                            command.CommandTimeout = 300;
                            command.Transaction = sTrans;
                            adapter = new SqlDataAdapter(command);
                        }
                        break;
                    }
                    command = (SqlCommand) this.m_Command;
                    command.CommandText = strSql;
                    command.CommandTimeout = 300;
                    command.Transaction = sTrans;
                    command.ExecuteNonQuery();
                    break;

                case DatabaseTypeEnum.Oledb:
                    if (resType != SqlResultEnum.No)
                    {
                        if (resType == SqlResultEnum.Have)
                        {
                            command2 = (OleDbCommand) this.m_Command;
                            command2.CommandText = strSql;
                            command2.CommandTimeout = 300;
                            result = command2.ExecuteScalar();
                        }
                        else
                        {
                            command2 = (OleDbCommand) this.m_Command;
                            command2.CommandText = strSql;
                            command2.CommandTimeout = 300;
                            adapter = new OleDbDataAdapter(command2);
                        }
                        break;
                    }
                    command2 = (OleDbCommand) this.m_Command;
                    command2.CommandText = strSql;
                    command2.CommandTimeout = 300;
                    command2.ExecuteNonQuery();
                    break;

                default:
                    if (resType == SqlResultEnum.No)
                    {
                        command2 = (OleDbCommand) this.m_Command;
                        command2.CommandText = strSql;
                        command2.CommandTimeout = 300;
                        command2.ExecuteNonQuery();
                    }
                    else if (resType == SqlResultEnum.Have)
                    {
                        command2 = (OleDbCommand) this.m_Command;
                        command2.CommandText = strSql;
                        command2.CommandTimeout = 300;
                        result = command2.ExecuteScalar();
                    }
                    else
                    {
                        command2 = (OleDbCommand) this.m_Command;
                        command2.CommandText = strSql;
                        command2.CommandTimeout = 300;
                        adapter = new OleDbDataAdapter(command2);
                    }
                    break;
            }
            if (adapter != null)
            {
                result = this.GetResult(adapter, resType);
            }
            return result;
        }

        private object GetResult(IDbDataAdapter adapter, SqlResultEnum resType)
        {
            object obj3;
            try
            {
                object obj2 = null;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                switch (resType)
                {
                    case SqlResultEnum.Sets:
                        obj2 = dataSet;
                        break;

                    case SqlResultEnum.Table:
                        if (dataSet.Tables.Count > 0)
                        {
                            obj2 = dataSet.Tables[0];
                        }
                        break;

                    case SqlResultEnum.Row:
                        if ((dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0))
                        {
                            obj2 = dataSet.Tables[0].Rows[0];
                        }
                        break;

                    default:
                        obj2 = dataSet;
                        break;
                }
                obj3 = obj2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (adapter != null)
                {
                    this.DisposeAdapter(adapter);
                }
            }
            return obj3;
        }

        public string CommandText
        {
            get
            {
                OleDbCommand command2;
                switch (this.m_DatabaseType)
                {
                    case DatabaseTypeEnum.Oracle:
                    case DatabaseTypeEnum.Oledb:
                        command2 = (OleDbCommand) this.m_Command;
                        return command2.CommandText;

                    case DatabaseTypeEnum.SQLServer:
                    {
                        SqlCommand command = (SqlCommand) this.m_Command;
                        return command.CommandText;
                    }
                }
                command2 = (OleDbCommand) this.m_Command;
                return command2.CommandText;
            }
            set
            {
                OleDbCommand command2;
                switch (this.m_DatabaseType)
                {
                    case DatabaseTypeEnum.Oracle:
                    case DatabaseTypeEnum.Oledb:
                        command2 = (OleDbCommand) this.m_Command;
                        command2.CommandText = value;
                        break;

                    case DatabaseTypeEnum.SQLServer:
                    {
                        SqlCommand command = (SqlCommand) this.m_Command;
                        command.CommandText = value;
                        break;
                    }
                    default:
                        command2 = (OleDbCommand) this.m_Command;
                        command2.CommandText = value;
                        break;
                }
            }
        }

        public int CommandTimeout
        {
            get
            {
                OleDbCommand command2;
                switch (this.m_DatabaseType)
                {
                    case DatabaseTypeEnum.Oracle:
                    case DatabaseTypeEnum.Oledb:
                        command2 = (OleDbCommand) this.m_Command;
                        return command2.CommandTimeout;

                    case DatabaseTypeEnum.SQLServer:
                    {
                        SqlCommand command = (SqlCommand) this.m_Command;
                        return command.CommandTimeout;
                    }
                }
                command2 = (OleDbCommand) this.m_Command;
                return command2.CommandTimeout;
            }
            set
            {
                OleDbCommand command2;
                switch (this.m_DatabaseType)
                {
                    case DatabaseTypeEnum.Oracle:
                    case DatabaseTypeEnum.Oledb:
                        command2 = (OleDbCommand) this.m_Command;
                        command2.CommandTimeout = value;
                        break;

                    case DatabaseTypeEnum.SQLServer:
                    {
                        SqlCommand command = (SqlCommand) this.m_Command;
                        command.CommandTimeout = value;
                        break;
                    }
                    default:
                        command2 = (OleDbCommand) this.m_Command;
                        command2.CommandTimeout = value;
                        break;
                }
            }
        }

        public System.Data.CommandType CommandType
        {
            get
            {
                OleDbCommand command2;
                switch (this.m_DatabaseType)
                {
                    case DatabaseTypeEnum.Oracle:
                    case DatabaseTypeEnum.Oledb:
                        command2 = (OleDbCommand) this.m_Command;
                        return command2.CommandType;

                    case DatabaseTypeEnum.SQLServer:
                    {
                        SqlCommand command = (SqlCommand) this.m_Command;
                        return command.CommandType;
                    }
                }
                command2 = (OleDbCommand) this.m_Command;
                return command2.CommandType;
            }
            set
            {
                OleDbCommand command2;
                switch (this.m_DatabaseType)
                {
                    case DatabaseTypeEnum.Oracle:
                    case DatabaseTypeEnum.Oledb:
                        command2 = (OleDbCommand) this.m_Command;
                        command2.CommandType = value;
                        break;

                    case DatabaseTypeEnum.SQLServer:
                    {
                        SqlCommand command = (SqlCommand) this.m_Command;
                        command.CommandType = value;
                        break;
                    }
                    default:
                        command2 = (OleDbCommand) this.m_Command;
                        command2.CommandType = value;
                        break;
                }
            }
        }

        public IConnection Connection
        {
            get
            {
                return this.m_Connection;
            }
            set
            {
                SqlCommand command;
                OleDbCommand command2;
                if (value.DatabaseType != this.m_DatabaseType)
                {
                    this.Dispose();
                    switch (value.DatabaseType)
                    {
                        case DatabaseTypeEnum.Oracle:
                        case DatabaseTypeEnum.Oledb:
                            command2 = new OleDbCommand();
                            command2.Connection = (OleDbConnection) value.rawConnection;
                            this.m_Command = command2;
                            goto Label_009D;

                        case DatabaseTypeEnum.SQLServer:
                            command = new SqlCommand();
                            command.Connection = (SqlConnection) value.rawConnection;
                            this.m_Command = command;
                            goto Label_009D;
                    }
                    command2 = new OleDbCommand();
                    command2.Connection = (OleDbConnection) value.rawConnection;
                    this.m_Command = command2;
                }
            Label_009D:
                this.m_DatabaseType = value.DatabaseType;
                switch (value.DatabaseType)
                {
                    case DatabaseTypeEnum.Oracle:
                    case DatabaseTypeEnum.Oledb:
                        command2 = (OleDbCommand) this.m_Command;
                        command2.Connection = (OleDbConnection) value.rawConnection;
                        break;

                    case DatabaseTypeEnum.SQLServer:
                        command = (SqlCommand) this.m_Command;
                        command.Connection = (SqlConnection) value.rawConnection;
                        break;

                    default:
                        command2 = (OleDbCommand) this.m_Command;
                        command2.Connection = (OleDbConnection) value.rawConnection;
                        break;
                }
                this.m_Connection = value;
            }
        }

        public object Parameters
        {
            get
            {
                OleDbCommand command2;
                switch (this.m_DatabaseType)
                {
                    case DatabaseTypeEnum.Oracle:
                    case DatabaseTypeEnum.Oledb:
                        command2 = (OleDbCommand) this.m_Command;
                        return command2.Parameters;

                    case DatabaseTypeEnum.SQLServer:
                    {
                        SqlCommand command = (SqlCommand) this.m_Command;
                        return command.Parameters;
                    }
                }
                command2 = (OleDbCommand) this.m_Command;
                return command2.Parameters;
            }
        }

        public object rawCommand
        {
            get
            {
                return this.m_Command;
            }
        }
    }
}

