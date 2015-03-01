using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Approve.RuleCenter;
using Approve.EntityBase;
using System.Collections;
using System.Data.SqlClient;

/// <summary>
///SMSList 的摘要说明
/// </summary>
public class SMSList
{
    RCenter rc = new RCenter();
    SaveOptionEnum so = SaveOptionEnum.Insert;
    public SMSList()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    //修改发送短信状态
    public bool UpdateSmsState(string FID, int FState)
    {
        
        StringBuilder sb = new StringBuilder();
        sb.Append("update CF_OA_SMSList set FState = @FState ");
        sb.Append(" where fid=@fid ");
        SqlParameter[] Parameter = new SqlParameter[2];
        Parameter[0] = new SqlParameter("@FState", FState);
        Parameter[1] = new SqlParameter("@fid", FID);
        if (rc.PExcute(sb.ToString(),Parameter))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region
    /// <summary>
    /// 添加短信信息
    /// </summary>
    /// <param name="FCalled">手机号码</param>
    /// <param name="FUserId">用户id</param>
    /// <param name="FContent">短信内容</param>
    /// <returns></returns>
    public bool SaveInfo(string FCalled,string FUserId,string FContent)
    {
        SortedList sl = new SortedList();
        sl.Add("FID",Guid.NewGuid().ToString());
        sl.Add("FCalled",FCalled);
        sl.Add("FUserId",FUserId);
        sl.Add("FContent",FContent);
        sl.Add("FPlanTime",DateTime.Now.AddSeconds(10));
        sl.Add("FSubmitTime",DateTime.Now);
        sl.Add("FState",0);
        sl.Add("FCreateTime", DateTime.Now);
        sl.Add("FIsDeleted",0);
        if (rc.SaveEBase(EntityTypeEnum.EOASMSList, sl, "FID", so))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    

}
