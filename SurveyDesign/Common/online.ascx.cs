using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Approve.RuleCenter;
using System.Collections;
using Approve.EntityBase;
using System.Data.SqlClient;
using ProjectBLL;


public partial class Common_online : System.Web.UI.UserControl
{
    RCenter rc = new RCenter();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();

        }
    }

    //显示
    private void showInfo()
    {
        string FID = EConvert.ToString(Session["FUserId"]);
        string UserRightID = EConvert.ToString(CurrentEntUser.UserRightID);

        // 给在线人员控件属性赋值
        string systemId = rc.GetSignValue("select fsystemid from cf_sys_userright where fid=@fid", new SqlParameter("@fid", UserRightID));
        DataTable user = rc.GetTable("select FManageTypeId,FCompany from cf_sys_user where fid=@fid", new SqlParameter("@fid", FID));
        if (user != null && user.Rows.Count > 0)
        {

            ViewState["_SystemID"] = systemId;
            ViewState["_DeptId"] = user.Rows[0]["FManageTypeId"].ToString();
            ViewState["_RightFID"] = UserRightID;
            ViewState["_EntName"] = user.Rows[0]["FCompany"].ToString();

        }
        showREP();
    }

    //分组绑定
    private void showREP()
    {
        DataTable repDT = rc.getDicTbByFNumber("866");
        rep.DataSource = repDT;
        rep.DataBind();
    }


    //显示可服务的人员
    private string showServers(string type, ref int n, ref int m)
    {
        StringBuilder sb = new StringBuilder();
        if (EConvert.ToString(ViewState["_RightFID"]) != EConvert.ToString(CurrentEntUser.UserRightID))
        {
            showInfo();
        }
        if (!string.IsNullOrEmpty(EConvert.ToString(ViewState["_SystemID"])))
        {
            SortedList sl = new SortedList();
            sl.Add("FSystemId", "%" + EConvert.ToString(ViewState["_SystemID"]) + "%");
            sl.Add("FDeptId", EConvert.ToString(ViewState["_DeptId"]) + "%");
            sl.Add("FType", type);

            sb.Append("select top 5 FId,FLinkName,FLoginKey ");
            sb.Append("from CF_City_Link ");
            sb.Append("where  isnull(FisDeleted,0)=0 ");
            sb.Append("and FSystemId like @FSystemId and FDeptId like @FDeptId  and FType=@FType ");
            sb.Append("order by FOrder ");
            DataTable dt = rc.GetTable(sb.ToString(), rc.ConvertParameters(sl));


            // 在线服务连接
            string OnLineServersURL = rc.GetSysObjectContent("_sys_OnlineServers_URL");//在线客服连接地址
            string PRJID = rc.GetSysObjectContent("_sys_OnlineServers_PRJID");//在线客服项目ID
            string FID = EConvert.ToString(ViewState["_RightFID"]);//企业userright.fid
            string EntName = EConvert.ToString(ViewState["_EntName"]);//企业名

            sb.Remove(0, sb.Length);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string LoginKey = dt.Rows[i]["FLoginKey"].ToString();
                string LinkName = dt.Rows[i]["FLinkName"].ToString();

                string key = SecurityEncryption.DesEncrypt(PRJID + "|" + SecurityEncryption.ConvertDateTimeInt(DateTime.Now) + "|" + FID + "|" + LoginKey, "23456789");
                key = key.Replace("+", "%20");

                string img = OnLineServersURL + "?SID=" + Server.HtmlEncode(key) + "&online=1";

                sb.Append("<div class='onli_emp'>");
                sb.Append("<span><img src='" + img + "'/></span>");
                sb.Append("<a href=\"javascript:openChat('" + OnLineServersURL + "','" + key + "','" + FID + "','" + EntName + "');\">");
                sb.Append(LinkName);
                sb.Append("</a>");
                sb.Append("</div>");

            }
            //if (string.IsNullOrEmpty(sb.ToString()))
            //{
            //    string key = SecurityEncryption.DesEncrypt(PRJID + "|" + SecurityEncryption.ConvertDateTimeInt(DateTime.Now) + "|" + FID, "23456789");
            //    key = key.Replace("+", "%20");

            //    string img = OnLineServersURL + "?SID=" + key + "&online=1";

            //    sb.Append("<div class='onli_emp'>");
            //    sb.Append("<span><img src='" + img + "'/></span>");
            //    sb.Append("<a href=\"javascript:openChat('" + OnLineServersURL + "','" + key + "','" + FID + "','" + EntName + "');\">客服</a>");
            //    sb.Append("</div>");
            //}

        }

        return sb.ToString();
    }

    //分组repeat绑定事件
    protected void rep_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            int n = 0;//总人数
            int m = 0;//在线人数
            string FType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));//分组number

            Literal lit_online = (Literal)e.Item.FindControl("lit_online");
            lit_online.Text = showServers(FType, ref n, ref m);

            // Literal lit_count = (Literal)e.Item.FindControl("lit_count");
            // lit_count.Text = "[" + m.ToString() + "/" + n.ToString() + "]";

        }
    }


    //刷新每个分组的人
    protected void Timer2_Tick(object sender, EventArgs e)
    {
        for (int i = 0; i < rep.Items.Count; i++)
        {
            int n = 0;//总人数
            int m = 0;//在线人数
            Literal lit_online = (Literal)rep.Items[i].FindControl("lit_online");
            HiddenField lit_Type = (HiddenField)rep.Items[i].FindControl("lit_Type");
            string FType = lit_Type.Value;

            lit_online.Text = showServers(FType, ref n, ref m);
        }

    }
}
