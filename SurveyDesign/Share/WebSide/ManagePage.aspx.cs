using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using Approve.RuleCenter;
using System.Data;
using System.Data.SqlClient;
using Tools;
using ProjectData;

public partial class Share_WebSide_ManagePage : System.Web.UI.Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            showInfo();
        }
    }

    private void showInfo()
    {
        liC_TechSupport.Text = ComFunction.GetValueByName("TechSupport");//技术支持
        liC_webtel.Text = ComFunction.GetValueByName("WebTel");//电话
        liC_Developer.Text = ComFunction.GetValueByName("Developer");//开发单位

        //接收登陆串
        string userID = "";
        string key = Request.QueryString["key"];
        string[] strArray = SecurityEncryption.DesDecrypt(key, "12345678").Split('|');
        if (strArray.Length == 2)
        {
            if (EConvert.ToInt(strArray[1]) > SecurityEncryption.ConvertDateTimeInt(DateTime.Now))
                userID = strArray[0];
        }
        if (!string.IsNullOrEmpty(userID))
        {
            //用户资料显示
            StringBuilder sb = new StringBuilder();
            sb.Append("select FLinkMan from cf_sys_user where fid='" + userID + "'");
            lab_FName.Text = rc.GetSignValue(sb.ToString());

            //用户权限列表
            sb.Remove(0, sb.Length);
            sb.Append("select r.FID,s.FName,FLUrl,FPic,r.FSystemId,r.FEndTime ");
            sb.Append("from cf_sys_systemname s,cf_sys_userright r ");
            sb.Append("where r.fsystemid=s.fnumber and s.fisdeleted=0");
            sb.Append("and r.fUserId=@userID ");
            sb.Append(" order by forder ");

            DataTable dt = rc.GetTable(sb.ToString(), new SqlParameter("@userID", userID));
            sys_List.InnerHtml = getList(dt);//列出该用户的系统权限
        }
        else
        {
            pageTool tool = new pageTool(this.Page);
            tool.showMessageAndGoNewPage("失败", "../../ApproveWeb/main/ManLogin.aspx");
        }
    }

    /// <summary>
    /// 列出该用户的系统权限
    /// </summary>
    /// <param name="dt"></param>
    private string getList(DataTable dt)
    {
        string str = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string FSystemId = dt.Rows[i]["FSystemId"].ToString();
            string rightID = dt.Rows[i]["FID"].ToString();
            string FName = dt.Rows[i]["FName"].ToString();
            string FLUrl = dt.Rows[i]["FLUrl"].ToString();
            string FPic = dt.Rows[i]["FPic"].ToString();
            string FEndTime = dt.Rows[i]["FEndTime"].ToString();

            DateTime time = DateTime.Now.AddHours(1);
            string key = SecurityEncryption.DesEncrypt(rightID + "|" + SecurityEncryption.ConvertDateTimeInt(time), rc.getSystemKey(FSystemId));


            string img = "<img src='" + FPic + "'/>";
            if (string.IsNullOrEmpty(FPic))
            {
                img = FName;
            }

            DateTime dTime = DateTime.Now;
            DateTime endTime = EConvert.ToDateTime(FEndTime);
            if (endTime < dTime) //过期了
            {
                str += "<a href='javascript:alert(\"对不起，该帐户已过期\\n请和管理员联系\");' style='FILTER: gray;'>" + img + "</a>";
            }
            else
            {
                if (FSystemId == "701")
                    str += "<a  href='" + string.Format(FLUrl, rightID) + "'>" + img + "</a>";
                else
                    str += "<a  href='javascript:login(\"" + key + "\",\"" + FSystemId + "\",\"" + FLUrl + "\");'>" + img + "</a>";
            }
        }
        return str;
    }


    /// <summary>
    /// 得到加密串，并保存登陆日志
    /// </summary>
    /// <param name="UserRightId"></param>
    /// <returns></returns>
    private string GetKey(string UserRightId)
    {
        pageTool tool = new pageTool(Page);
        string str = "";
        StringBuilder sb = new StringBuilder();
        sb.Append("select u.FID,u.FName,u.FPassWord,u.FLockLabelNumber,u.FLockNumber,u.FBeginTime,u.FEndTime,");//user表
        sb.Append("u.FType,u.FCreateTime,u.FCompany,u.FFunction,u.FManageDeptId,u.FLinkMan,u.FTel,u.FState, ");//user表
        sb.Append("r.FID rFID,r.FUserId rFUserId,r.FCreateTime rFCreateTime,r.FBaseinfoID rFBaseinfoId,");//userright表
        sb.Append("r.FName rFName,r.FPassWord rFPassWord,r.FLockLabelNumber rFLockLabelNumber,r.FLockNumber rFLockNumber,");//userright表
        sb.Append("r.FBeginTime rFBeginTime,r.FEndTime rFEndTime,r.FSystemId rFSystemId,r.FState rFState,");//userright表
        sb.Append("r.FCanMod rFCanMod,r.FPri rFPri,r.FSelType rFSelType,r.FRoleId rFRoleId,r.FMenuRoleId rFMenuRoleId  ");
        sb.Append("from cf_sys_user u,cf_sys_userright r ");
        sb.Append("where u.fid=r.fuserid and r.fisdeleted=0 and r.FID=@FID ");
        DataTable dt = rc.GetTable(sb.ToString(), new SqlParameter("@FID", UserRightId));
        if (dt != null && dt.Rows.Count > 0)
        {

            sb.Remove(0, sb.Length);
            //user表
            sb.Append(dt.Rows[0]["FID"].ToString() + "@");
            sb.Append(dt.Rows[0]["FName"].ToString() + "@");
            sb.Append(dt.Rows[0]["FPassWord"].ToString() + "@");
            sb.Append(dt.Rows[0]["FLockLabelNumber"].ToString() + "@");
            sb.Append(dt.Rows[0]["FLockNumber"].ToString() + "@");
            sb.Append(dt.Rows[0]["FBeginTime"].ToString() + "@");
            sb.Append(dt.Rows[0]["FEndTime"].ToString() + "@");
            sb.Append(dt.Rows[0]["FType"].ToString() + "@");
            sb.Append(dt.Rows[0]["FCreateTime"].ToString() + "@");
            sb.Append(dt.Rows[0]["FCompany"].ToString() + "@");
            sb.Append(dt.Rows[0]["FFunction"].ToString() + "@");
            sb.Append(dt.Rows[0]["FManageDeptId"].ToString() + "@");
            sb.Append(dt.Rows[0]["FLinkMan"].ToString() + "@");
            sb.Append(dt.Rows[0]["FTel"].ToString() + "@");
            //userright表
            sb.Append(dt.Rows[0]["rFID"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFUserId"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFCreateTime"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFBaseinfoId"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFName"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFPassWord"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFLockLabelNumber"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFLockNumber"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFBeginTime"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFEndTime"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFState"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFCanMod"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFPri"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFSelType"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFRoleId"].ToString() + "@");
            sb.Append(dt.Rows[0]["rFMenuRoleId"].ToString());
            str = sb.ToString();



            //登陆日志
            StringBuilder ss = new StringBuilder();
            ss.Append("系统：" + rc.getSystemName(dt.Rows[0]["rFSystemId"].ToString()));
            ss.Append("\n企业：" + dt.Rows[0]["FCompany"].ToString());
            ss.Append("\nUserrightID：" + UserRightId);
            ss.Append("\n时间：" + DateTime.Now);
            ss.Append("\n事件：通过统一认证选择系统登陆");
            DataLog.Write(LogType.Info, LogSort.Safety, "统一认证选择系统登陆", ss.ToString(), UserRightId);
        }
        return str;
    }


    //登陆按钮
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        string sysId = hidd_sysId.Value;
        string str = hidd_FID.Value;
        if (!string.IsNullOrEmpty(str))
        {
            string rFID = "";
            string[] strArray = SecurityEncryption.DesDecrypt(str, rc.getSystemKey(sysId)).Split('|');
            if (strArray.Length == 2)
            {
                if (EConvert.ToInt(strArray[1]) > SecurityEncryption.ConvertDateTimeInt(DateTime.Now))
                    rFID = strArray[0];
            }

            if (sysId == "401" || sysId == "801")//判断像“建筑市场监管系统”和“收费管理系统”这样只需要一个UserRightId
            {
                str = rFID;
            }
            else
            {
                str = GetKey(rFID);//获得用户信息串
            }

            if (!string.IsNullOrEmpty(str))
            {
                string url = hidd_Url.Value;
                if (!string.IsNullOrEmpty(url))
                {
                    DateTime time = DateTime.Now.AddHours(1);
                    string key = SecurityEncryption.DesEncrypt(str + "|" + SecurityEncryption.ConvertDateTimeInt(time), rc.getSystemKey(sysId));
                    Response.Redirect(url + "?key=" + HttpUtility.UrlEncode(key, Encoding.UTF8) + "&sysId=" + sysId);
                }
                else
                {
                    tool.showMessage("登陆失败，配置登陆地址错误。请和管理员联系。");
                }
            }
            else
            {
                tool.showMessage("登陆失败，请重试或联系管理员");
            }


        }
        else
        {
            tool.showMessage("登陆失败，请重试或联系管理员");
        }
    }

}
