using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;
using System.Collections;
using System.Web;
using System.IO;

public class RPhoto
{
    private static readonly string sConStr = ConfigurationManager.AppSettings["dbPhoto"];
    /// <summary>
    /// 获取第一行第一列数据
    /// </summary>
    /// <param name="SQL"></param>
    /// <returns></returns>
    public static object GetSignValue(string SQL)
    {
        SqlConnection Cn = new SqlConnection(sConStr);
        SqlCommand cmd = new SqlCommand(SQL, Cn);
        try
        {
            Cn.Open();
            object obj = cmd.ExecuteScalar();//获取单一数据
            return obj;
        }
        catch
        {
            return null;
        }
        finally
        {
            Cn.Close();
        }
    }
    /// <summary>
    /// 获取数据表
    /// </summary>
    /// <param name="SQL"></param>
    /// <returns></returns>
    public static DataTable GetTable(string SQL)
    {
        SqlConnection Cn = new SqlConnection(sConStr);
        SqlCommand cmd = new SqlCommand(SQL, Cn);
        try
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 执行数据库的操作（增、删、改）[已sql为参数]
    /// </summary>
    /// <param name="SQL"></param>
    /// <returns></returns>
    public static bool ExcuteSQL(string SQL, params SqlParameter[] param)
    {
        SqlConnection Cn = new SqlConnection(sConStr);
        SqlCommand Cmd = new SqlCommand();
        Cmd.Connection = Cn;
        Cmd.CommandText = SQL;
        try
        {
            if (param.Length > 0)
                Cmd.Parameters.AddRange(param);
            Cn.Open();
            int Result = Cmd.ExecuteNonQuery();
            return Result > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            Cn.Close();
        }
    }
    /// <summary>
    /// 返回文件的二进制流
    /// </summary>
    /// <param name="upFile"></param>
    /// <returns></returns>
    public static byte[] GetBuffer(HttpPostedFile upFile)
    {
        byte[] buffer = new byte[upFile.ContentLength];
        Stream stream = upFile.InputStream;
        stream.Read(buffer, 0, buffer.Length);
        stream.Flush();
        stream.Close();
        return buffer;
    }
}