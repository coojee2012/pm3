using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Approve.RuleCenter;
using System.Text;
using Approve.EntityBase; 
/// <summary>
/// webHallBasePage 的摘要说明
/// </summary>
public class webHallBasePage : Page
{

    RCenter rc = new RCenter();
    public webHallBasePage()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public string fBaseInfoid
    {
        get
        {
            if (this.Request.Cookies["fBaseId"] != null)
            {
                return this.Request.Cookies["fBaseId"].Value;
            }
            else
            {
                return null;
            }
        }

    }
    public string fEntName
    {
        get
        {
            if (this.fBaseInfoid != null)
            {
                string fBaseName = rc.GetSignValue(EntityTypeEnum.EbBaseInfo, "FName", "Fid='" + fBaseInfoid + "'");
                if (fBaseName != null)
                {
                    return fBaseName;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

    }
}
