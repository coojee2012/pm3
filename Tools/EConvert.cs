using System;

/// <summary>
/// 为了保证用字典创建实体时不至于因字典键值不全而引起转换异常
/// 为了确保实体字段都具有非空(!=null)值而设计本类型转换工具
/// 本转换工具的用途是将字典中的object转换成指定类型的类属性
/// 该类是一个静态工具类，故不允许继承
/// Author:Wengmj,jack
/// Time:2004-11-16
/// </summary>
public sealed class EConvert
{
    /// <summary>
    /// 将对象转化为字符串对象为null返回字符串空
    /// </summary>
    /// <param name="obj">要转化的对象</param>
    /// <returns>返回转换后的字符串</returns>
    public static string ToString(object obj)
    {
        if (obj == null || obj == DBNull.Value) return "";
        return Convert.ToString(obj).Trim();
    }

    /// <summary>
    /// 将对象转化为时间对象，为null返回时间的最小值，以标识当前时间为无效
    /// </summary>
    public static DateTime ToDateTime(object obj)
    {
        try
        {
            if (obj == null || obj == System.DBNull.Value) return System.DateTime.MinValue;
            if (obj.ToString() == "") return System.DateTime.MinValue;

            return Convert.ToDateTime(obj);
        }
        catch
        {
            return System.DateTime.MinValue;

        }

    }
    public static string ToShortDateString(object obj)
    {

        return ToDateTime(obj).ToString("yyyy-MM-dd").Replace("1900-01-01", "").Replace("0001-01-01","");

    }
    /// <summary>
    /// 将对象转化为decimal对象为null返回0
    /// </summary>
    public static decimal ToDecimal(object obj)
    {
        if (obj == null || obj == DBNull.Value) return 0;
        if (obj.ToString() == "") return 0;
        return Convert.ToDecimal(obj);
    }

    /// <summary>
    /// 将对象转化为int对象为null返回0
    /// </summary>
    public static int ToInt(object obj)
    {

        if (obj == null || obj == DBNull.Value) return 0;
        if (obj.ToString() == "") return 0;
        int i = 0;
        try
        {
            i = Convert.ToInt32(obj);
        }
        catch
        {
            i = 0;
        }
        return i;
    }

    public static float ToFloat(object obj)
    {
        if (obj == null || obj == DBNull.Value) return 0;
        if (obj.ToString() == "") return 0;
        return Convert.ToSingle(obj);
    }
    public static double ToDouble(object obj)
    {
        if (obj == null || obj == DBNull.Value) return 0;
        if (obj.ToString() == "") return 0;
        return Convert.ToDouble(obj);
    }
    /// <summary>
    /// 将对象转化为long对象为null返回0
    /// </summary>
    public static long ToLong(object obj)
    {
        if (obj == null || obj == DBNull.Value) return 0;
        if (obj.ToString() == "") return 0;
        return Convert.ToInt64(obj);
    }

    /// <summary>
    /// 将对象转化为bool对象为null返回false
    /// </summary>
    public static bool ToBool(object obj)
    {
        if (obj == null || obj == DBNull.Value) return false;
        if (obj.ToString() == "") return false;
        return Convert.ToBoolean(obj);
    }
}
