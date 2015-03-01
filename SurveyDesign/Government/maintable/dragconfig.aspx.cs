using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Approve.EntityBase;
using Approve.RuleCenter;
using System.Data.SqlClient;

public partial class Government_maintable_dragconfig : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SavePosition();
        }
    }

    /// <summary>
    /// 保存位置
    /// </summary>
    private void SavePosition()
    {
        string sysId = Request.QueryString["sysId"];
        string mytable = defultTable(sysId);
        string FUserRightId = EConvert.ToString(Session["DFUserRightId"]);
        SortedList ss = new SortedList();
        ss.Add("FLinkId", FUserRightId);
        ss.Add("sysId", sysId);
        string FID = rc.GetSignValue("select FID from cf_user where FLinkId=@FLinkId and FType=@sysId ", rc.ConvertParameters(ss));
        SaveOptionEnum so = SaveOptionEnum.Update;
        SortedList sl = new SortedList();
        if (string.IsNullOrEmpty(FID))
        {
            so = SaveOptionEnum.Insert;
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FLinkId", FUserRightId);
            sl.Add("FType", sysId);
        }
        else
        {
            sl.Add("FID", FID);
        }

        sl.Add("FMyTable", mytable);//保存位置
        rc.SaveEBase("CF_User", sl, "FID", so);
        Response.Redirect("../main/main.aspx?sysId=" + sysId);


    }

    private string defultTable(string sysId)
    {
        string str = "";
        switch (sysId)
        {
            case "150"://安全生产
                str = "1,2,3,:";
                break;

            case "175"://质量检测
                str = "1,2,4,:";
                break;

            case "220"://三类人员
                str = "1,2,5,:";
                break;
            case "135"://园林绿化
                str = "1,2:";
                break;
            case "183"://园林绿化
                str = "1:";
                break;
            default:
                str = "1";
                break;
        }
        return str;
    }
}
