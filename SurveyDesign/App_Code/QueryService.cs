using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Text;
using Approve.RuleBase;
 


/// <summary>
/// QueryService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class QueryService : System.Web.Services.WebService
{

    public QueryService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DataSet GetDs(string SQL)
    {
        DataSet ds = new DataSet();
        RBase Re = new RBase();
        DataTable dt = Re.GetTable(SQL);
        ds.Tables.Add(dt.Copy());
        return ds;
    }
    [WebMethod]
    public bool Excute(string SQL)
    {
        RBase Re = new RBase();
        return Re.PExcute(SQL);
    }

}

