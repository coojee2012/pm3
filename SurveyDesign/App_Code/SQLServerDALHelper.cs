

#define DEBUG
#undef DEBUG

using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace NJSWebApp
{
    public enum SqlConType { Work = 1, Model = 2, Pub = 3, Archive = 4, XMBaseInfo = 5 };

    public class SQLServerDALHelper
    {
        /// <summary>
        ///  用于连接SQLServer数据库的连接字符串(模型库)，存于Web.config中


        /// </summary>
#if (DEBUG)
        private static string _sqlModConnectionString = @"User ID=sa;Initial Catalog=JKCWFDB_MODEL_JFP;Data Source=jkc-008;Password=sa";
#else
        private static string _sqlModConnectionString = ConfigurationManager.AppSettings["ModSQLServerConnectionString"];
#endif


        /// <summary>
        /// 用于连接SQLServer数据库的连接字符串(工作库)，存于Web.config中


        /// </summary>
#if (DEBUG)
        private static string _sqlWokConnectionString = @"User ID=sa;Initial Catalog=JKCWFDB_WORK_JFP;Data Source=jkc-008;Password=sa";
#else
        private static string _sqlWokConnectionString = ConfigurationManager.AppSettings["WokSQLServerConnectionString"];
#endif

        /// <summary>
        /// 用于连接SQLServer数据库的连接字符串(发布库)，存于Web.config中



        /// </summary>
#if (DEBUG)
        private static string _sqlPubConnectionString = @"User ID=sa;Initial Catalog=JKCWFDB_PUB_JFP;Data Source=jkc-008;Password=sa";
#else
        private static string _sqlPubConnectionString = ConfigurationManager.AppSettings["PubSQLServerConnectionString"];
#endif

        /// <summary>
        /// 用于连接SQLServer数据库的连接字符串(归档库)，存于Web.config中



        /// </summary>
#if (DEBUG)
        private static string _sqlArcConnectionString = @"User ID=sa;Initial Catalog=JKCWFDB_ARCHIVE_JFP;Data Source=jkc-008;Password=sa";
#else
        private static string _sqlArcConnectionString = ConfigurationManager.AppSettings["ArcSQLServerConnectionString"];
#endif

        /// </summary>
#if (DEBUG)
        private static string _sqlArcConnectionString = @"User ID=sa;Initial Catalog=JKCWFDB_ARCHIVE_JFP;Data Source=jkc-008;Password=sa";
#else
        private static string _sqlXMBaseInfoConnectionString = ConfigurationManager.AppSettings["XM_BaseInfo"];
#endif



        public static void SetConnctionString(SqlConType conType, string conString)
        {
            switch (conType)
            {
                case SqlConType.Work:
                    _sqlWokConnectionString = conString;
                    break;
                case SqlConType.Model:
                    _sqlModConnectionString = conString;
                    break;
                case SqlConType.Pub:
                    _sqlPubConnectionString = conString;
                    break;
                case SqlConType.Archive:
                    _sqlArcConnectionString = conString;
                    break;
                case SqlConType.XMBaseInfo:
                    _sqlXMBaseInfoConnectionString = conString;
                    break;
            }
        }

        public static string GetDataBaseName(SqlConType conType)
        {
            string _sqlConnectionString = GetConnectionString(conType);
            SqlConnectionStringBuilder strBuilder = new SqlConnectionStringBuilder(_sqlConnectionString);
            return strBuilder.InitialCatalog;
        }

        public static SqlConnection CreateConnection(string connectionstring)
        {
            return new SqlConnection(connectionstring);
        }

        /// <summary>
        /// 执行SQL命令，不返回任何值

        /// </summary>
        /// <param name="sql"></param>
        /// <param name="conType"></param>
        public static void ExecuteSQLNonQurey(string sql, SqlConType conType)
        {
            try
            {
                string _sqlConnectionString = GetConnectionString(conType);
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.CommandTimeout = 0;
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// 执行SQL命令，不返回任何值 链接模型库



        /// </summary>
        /// <param name="sql"></param>
        public static void ExecuteSQLNonQurey(string sql)
        {
            try
            {
                string _sqlConnectionString = _sqlModConnectionString;
                using (SqlConnection connection = new SqlConnection(_sqlModConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// 执行SQL命令，并返回SqlDataReader  链接模型库



        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteSQLReader(string sql)
        {
            try
            {
                SqlConnection connection = new SqlConnection(_sqlWokConnectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader sqlReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                //connection.Close();

                return sqlReader;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 执行SQL命令，并返回SqlDataReader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="conType"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteSQLReader(string sql, SqlConType conType)
        {
            try
            {
                string _sqlConnectionString = GetConnectionString(conType);
                SqlConnection connection = new SqlConnection(_sqlConnectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader sqlReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                //connection.Close();

                return sqlReader;
            }
            catch
            {
                return null;
            }
        }

        public static SqlDataReader ExecuteSQLReader(string sql, SqlConType conType, SqlParameter[] parameters)
        {
            try
            {
                string _sqlConnectionString = GetConnectionString(conType);
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {

                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    SqlDataReader reader = command.ExecuteReader();
                    return reader;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 执行查询返回第一行的第一列 链接模型库

        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object ExecuteSQLScalar(string sql)
        {
            try
            {
                object o = null;
                using (SqlConnection connection = new SqlConnection(_sqlModConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    o = command.ExecuteScalar();
                    connection.Close();
                }
                return o;
            }
            catch {
                return new object();
            }
        }

        /// <summary>
        /// 执行查询返回第一行的第一列



        /// </summary>
        /// <param name="sql"></param>
        /// <param name="conType"></param>
        /// <returns></returns>
        public static object ExecuteSQLScalar(string sql, SqlConType conType)
        {
            try
            {
                string _sqlConnectionString = GetConnectionString(conType);
                object o = null;
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    o = command.ExecuteScalar();
                    connection.Close();
                }
                return o;
            }
            catch
            {
                return new object();
            }
        }


        /// <summary>
        /// 执行存储过程返回第一行的第一列 链接模型库



        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteProcedureScalar(string storedProcedureName, IDataParameter[] parameters)
        {
            object o = null;
            using (SqlConnection connection = new SqlConnection(_sqlModConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                connection.Open();
                o = command.ExecuteScalar();
                connection.Close();
            }
            return o;
        }

        /// <summary>
        /// 执行存储过程返回第一行的第一列        
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="conType"></param>
        /// <returns></returns>
        public static object ExecuteProcedureScalar(string storedProcedureName, IDataParameter[] parameters
            , SqlConType conType)
        {
            try
            {
                string _sqlConnectionString = GetConnectionString(conType);
                object o = null;
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    SqlCommand command = new SqlCommand(storedProcedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    connection.Open();
                    o = command.ExecuteScalar();
                    connection.Close();
                }
                return o;
            }
            catch
            {
                return new object();
            }
        }

        /// <summary>
        /// 执行SQL查询语句并将查询生成的DataTable加入到指定的dataSet
        /// </summary>
        /// <param name="dataSet">已经实例化的dataSet</param>
        /// <param name="tableName"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="conType"></param>
        /// <returns></returns>
        public static void ExecuteSQLDataSet(ref DataSet dataSet, string tableName, string SqlCmd, SqlConType conType)
        {
            ExecuteSQLDataSet(ref dataSet, tableName, SqlCmd, conType, MissingSchemaAction.Add);
        }

        /// <summary>
        /// 执行SQL查询语句并将查询生成的DataTable加入到指定的dataSet
        /// </summary>
        /// <param name="dataSet">已经实例化的dataSet</param>
        /// <param name="tableName"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="conType"></param>
        /// <returns></returns>
        public static void ExecuteSQLDataSet(ref DataSet dataSet, string tableName, string SqlCmd, SqlConType conType, MissingSchemaAction schAction)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString(conType)))
                {
                    SqlDataAdapter da = new SqlDataAdapter(SqlCmd, connection);
                    connection.Open();
                    da.MissingSchemaAction = schAction;
                    da.Fill(dataSet, tableName);
                    connection.Close();
                }
            }
            catch
            {
                dataSet = new DataSet();
            }
        }

        /// <summary>
        ///  执行存储过程，返回数据集 链接模型库



        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet ExecuteProcedureDataSet(string storedProcedureName, IDataParameter[] parameters)
        {
            return ExecuteProcedureDataSet(storedProcedureName, parameters, SqlConType.Model);
        }

        /// <summary>
        ///  执行存储过程，返回数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet ExecuteProcedureDataSet(string storedProcedureName, IDataParameter[] parameters, SqlConType conType)
        {
            return ExecuteProcedureDataSet(storedProcedureName, parameters, conType, MissingSchemaAction.Add);

            //DataSet ds = new DataSet();
            //using (SqlConnection connection = new SqlConnection(GetConnectionString(conType)))
            //{
            //    SqlCommand command = new SqlCommand(storedProcedureName, connection);
            //    command.CommandType = CommandType.StoredProcedure;
            //    if (parameters != null)
            //    {
            //        foreach (SqlParameter parameter in parameters)
            //        {
            //            command.Parameters.Add(parameter);
            //        }
            //    }
            //    SqlDataAdapter da = new SqlDataAdapter(command);
            //    connection.Open();
            //    da.Fill(ds);
            //    connection.Close();
            //}
            //return ds;
        }


        /// <summary>
        ///  执行存储过程，返回数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet ExecuteProcedureDataSet(string storedProcedureName, IDataParameter[] parameters, SqlConType conType, MissingSchemaAction schAction)
        {

            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString(conType)))
                {
                    SqlCommand command = new SqlCommand(storedProcedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.MissingSchemaAction = schAction;
                    connection.Open();
                    da.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch
            {
                return ds;
            }
        }

        /// <summary>
        /// 执行SQL查询语句并将查询生成的DataTable加入到指定的dataSet
        /// </summary>
        /// <param name="dataSet">已经实例化的dataSet</param>
        /// <param name="tableName"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="conType"></param>
        /// <returns></returns>
        public static void ExecuteProcedureDataSet(ref DataSet dataSet, string tableName, string SqlCmd, SqlConType conType)
        {
            ExecuteProcedureDataSet(ref dataSet, tableName, SqlCmd, conType, MissingSchemaAction.AddWithKey);
        }

        /// <summary>
        /// 执行SQL查询语句并将查询生成的DataTable加入到指定的dataSet
        /// </summary>
        /// <param name="dataSet">已经实例化的dataSet</param>
        /// <param name="tableName"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="conType"></param>
        /// <returns></returns>
        public static void ExecuteProcedureDataSet(ref DataSet dataSet, string tableName, string SqlCmd, SqlConType conType, MissingSchemaAction schAction)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString(conType)))
                {
                    SqlDataAdapter da = new SqlDataAdapter(SqlCmd, connection);
                    connection.Open();
                    da.MissingSchemaAction = schAction;
                    da.Fill(dataSet, tableName);
                    connection.Close();
                }
            }
            catch
            {
                dataSet = new DataSet();
            }
        }

        /// <summary>
        /// 执行存储过程，不返回任何值 链接模型库



        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        public static void ExecuteProcedureNonQurey(string storedProcedureName, IDataParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlModConnectionString))
                {
                    SqlCommand command = new SqlCommand(storedProcedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// 执行存储过程，不返回任何值



        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="conType"></param>
        public static void ExecuteProcedureNonQurey(string storedProcedureName, IDataParameter[] parameters, SqlConType conType)
        {
            try
            {
                string _sqlConnectionString = GetConnectionString(conType);
                using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
                {
                    //connection.commandTimeout = 0;
                    SqlCommand command = new SqlCommand(storedProcedureName, connection);
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// 执行存储，并返回SqlDataReader 链接模型库

        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteProcedureReader(string storedProcedureName, IDataParameter[] parameters)
        {
            try
            {
                SqlConnection connection = new SqlConnection(_sqlModConnectionString);
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                connection.Open();
                SqlDataReader sqlReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                return sqlReader;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 执行存储，并返回SqlDataReader 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="conType"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteProcedureReader(string storedProcedureName, IDataParameter[] parameters,SqlConType conType)
        {
            try
            {
                string _sqlConnectionString = GetConnectionString(conType);
                SqlConnection connection = new SqlConnection(_sqlConnectionString);
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                connection.Open();
                SqlDataReader sqlReader = command.ExecuteReader(CommandBehavior.CloseConnection);


                return sqlReader;
            }
            catch
            {
                return null;
            }
        }


        public static string GetConnectionString(SqlConType conType)
        {
            string _sqlConnectionString = string.Empty;
            switch (conType)
            {
                case SqlConType.Work:
                    _sqlConnectionString = _sqlWokConnectionString;
                    break;
                case SqlConType.Model:
                    _sqlConnectionString = _sqlModConnectionString;
                    break;
                case SqlConType.Pub:
                    _sqlConnectionString = _sqlPubConnectionString;
                    break;
                case SqlConType.Archive:
                    _sqlConnectionString = _sqlArcConnectionString;
                    break;
                case SqlConType.XMBaseInfo:
                    _sqlConnectionString = _sqlXMBaseInfoConnectionString;
                    break;
            }
            return _sqlConnectionString;
        }
    }
}
