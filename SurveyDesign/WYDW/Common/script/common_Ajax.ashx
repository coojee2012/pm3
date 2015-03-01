<%@ WebHandler Language="C#" Class="common_Ajax" %>

using System;
using System.Web;
using Approve.RuleCenter;
using System.Data;
using System.Text;

public class common_Ajax : IHttpHandler {
    public RCenter rc = new RCenter();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string action=context.Request.QueryString["action"];
        switch (action)
        {
            case "GetPositionByPid":
                getPositionByPid(context);
                break;
            case "GetPPosition":
                getPPosition(context);break;
            case "GetAndSetSelPosByPid":
                getAndSetSelPosByPid(context);break;
            case "HasRYByXMBH":
                hasRYByXMBH(context);break;
            default:
                break;
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public void getPositionByPid(HttpContext context)
    {
        object parentId = context.Request.QueryString["parentId"];
        StringBuilder sb = new StringBuilder();
        sb.Append("<option value='-1' selected='selected'>---请选择---</option>");
        if (parentId != null)
        {
            string sql = "select * from cf_sys_dic where fparentid=" + parentId;
            
            try
            {
                DataTable dt=rc.GetTable(sql);
                if(dt.Rows.Count>0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        sb.Append("<option value='" + dr["FNumber"].ToString() + "'>" + dr["FName"].ToString() + "</option>");
                    }
                }
                
            }
            catch { }
            
        }
        context.Response.Write(sb.ToString());
    }

    public void getPPosition(HttpContext context)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<option value='-1' selected='selected'>---请选择---</option>");
        string sql = "select * from CF_Sys_Dic where FParentId=600";
        try
        {
            DataTable dt = rc.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<option value='" + dr["FNumber"].ToString() + "'>" + dr["FName"].ToString() + "</option>");
                }
            }

        }
        catch { }
        context.Response.Write(sb.ToString());
        
    }

    public void getAndSetSelPosByPid(HttpContext context) 
    {
        object parentId = context.Request.QueryString["parentId"];
        object name = context.Request.QueryString["name"];
        object value = context.Request.QueryString["value"];
        StringBuilder sb = new StringBuilder();
        
        
        if (parentId != null&&name!=null&&value!=null)
        {
            string sql = "select * from cf_sys_dic where fparentid=" + parentId.ToString();
            if (name.ToString() == "" && value.ToString() == "")
            {
                sb.Append("<option value='-1' selected='selected'>---请选择---</option>");
            }
            else 
            {
                sb.Append("<option value='-1'>---请选择---</option>");
                try
                {
                    DataTable dt = rc.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (name.ToString() == "value" && dr["FNumber"].ToString() == value.ToString())
                            {
                                sb.Append("<option selected='selected' value='" + dr["FNumber"].ToString() + "'>" + dr["FName"].ToString() + "</option>");
                            }
                            else if (name.ToString() == "text" && dr["FName"].ToString() == value.ToString())
                            {
                                sb.Append("<option selected='selected' value='" + dr["FNumber"].ToString() + "'>" + dr["FName"].ToString() + "</option>");
                            }
                            else
                            {
                                sb.Append("<option name='"+name.ToString()+"____"+value+"' value='" + dr["FNumber"].ToString() + "'>" + dr["FName"].ToString() + "</option>");
                            }
                        }
                    }

                }
                catch { }
            }
            

        }
        context.Response.Write(sb.ToString());
    }

    #region [判断是否在同一个项目中该人员是否存在]
    public void hasRYByXMBH(HttpContext context)
    {
        object cardType = context.Request.QueryString["CardType"];
        object cardId = context.Request.QueryString["CardID"];
        object xmbh = context.Request.QueryString["XMBH"];
        object fid = context.Request.QueryString["FID"];
        string result="no";
        if (cardType != null && cardId != null && xmbh != null&&fid!=null)
        {
            string sql = "select * from yw_wy_ry_jbxx where xmbh='"+xmbh.ToString()+"' and fcardtype='"+cardType+"' and fcardid='"+cardId+"' and fid<>'"+fid+"'";
            try 
            {
                int count = rc.GetCount(sql);
                if (count > 0)
                {
                    result = "yes";
                }
            }
            catch 
            { 
                
            }
        }
        
        context.Response.Write(result);
    }
    #endregion
}