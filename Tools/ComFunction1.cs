using System;
using System.Collections;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Web.UI;
using System.Web.Handlers;

public class ComFunction1
{
    static Hashtable hashNumber = new Hashtable();

    public ComFunction1()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //

    }



    //得到webconfig中默认省份
    public static string GetDefaultDept()
    {
        string fDeptNumber = System.Configuration.ConfigurationSettings.AppSettings["DefaultDept"].ToString();
        return fDeptNumber;
    }

    public static string GetValueByName(string sName)
    {
        string sValue = System.Configuration.ConfigurationSettings.AppSettings[sName].ToString();
        return sValue;
    }



    public static string GetSystemConfig(string sName)
    {
        if (hashNumber[sName] != null)
        {
            return hashNumber[sName].ToString();
        }
        string fWebConfigPath = HttpContext.Current.Server.MapPath(GetValueByName("DicConfigPath"));
        if (fWebConfigPath == null || fWebConfigPath == "")
        {
            return "";
        }
        XmlDocument xd = new XmlDocument();
        xd.Load(fWebConfigPath);
        XmlNodeList xl = xd.GetElementsByTagName(sName);
        if (xl == null || xl.Count == 0)
        {
            return "";
        }
        else
        {
            hashNumber.Add(sName, xl[0].InnerText);
        }
        return hashNumber[sName].ToString();
    }

    public static void ClearHashNumber()
    {
        hashNumber.Clear();
    }


    public static string GetColor(int index)
    {
        string[] colors = new string[] { "#0D8ECF", "#04D215", "#B0DE09", "#F8FF01", "#FF9E01", "#FF6600", "#FF1F11", "#E76047", "#D32757", "#551AB9", "#6345EF", "#1A7CDF", "#2097B9", "#228888", "#9FCF92", "#52B920", "#B4DE23", "#A69A2D", "#D56A24", "#E4481B", "#DE261D", "#FC551D", "#A61D00", "#FEC68D", "#B7F36A" };
        return colors[index].ToString();
    }
}
