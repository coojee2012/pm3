using System;

/// <summary>
/// Ϊ�˱�֤���ֵ䴴��ʵ��ʱ���������ֵ��ֵ��ȫ������ת���쳣
/// Ϊ��ȷ��ʵ���ֶζ����зǿ�(!=null)ֵ����Ʊ�����ת������
/// ��ת�����ߵ���;�ǽ��ֵ��е�objectת����ָ�����͵�������
/// ������һ����̬�����࣬�ʲ�����̳�
/// Author:Wengmj,jack
/// Time:2004-11-16
/// </summary>
public sealed class EConvert
{
    /// <summary>
    /// ������ת��Ϊ�ַ�������Ϊnull�����ַ�����
    /// </summary>
    /// <param name="obj">Ҫת���Ķ���</param>
    /// <returns>����ת������ַ���</returns>
    public static string ToString(object obj)
    {
        if (obj == null || obj == DBNull.Value) return "";
        return Convert.ToString(obj).Trim();
    }

    /// <summary>
    /// ������ת��Ϊʱ�����Ϊnull����ʱ�����Сֵ���Ա�ʶ��ǰʱ��Ϊ��Ч
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
    /// ������ת��Ϊdecimal����Ϊnull����0
    /// </summary>
    public static decimal ToDecimal(object obj)
    {
        if (obj == null || obj == DBNull.Value) return 0;
        if (obj.ToString() == "") return 0;
        return Convert.ToDecimal(obj);
    }

    /// <summary>
    /// ������ת��Ϊint����Ϊnull����0
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
    /// ������ת��Ϊlong����Ϊnull����0
    /// </summary>
    public static long ToLong(object obj)
    {
        if (obj == null || obj == DBNull.Value) return 0;
        if (obj.ToString() == "") return 0;
        return Convert.ToInt64(obj);
    }

    /// <summary>
    /// ������ת��Ϊbool����Ϊnull����false
    /// </summary>
    public static bool ToBool(object obj)
    {
        if (obj == null || obj == DBNull.Value) return false;
        if (obj.ToString() == "") return false;
        return Convert.ToBoolean(obj);
    }
}
