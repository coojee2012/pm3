using System;
using System.Web.UI;
using Approve.EntityBase;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Approve.RuleCenter;
using System.Collections;

public partial class Government_main_main : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    public string tablePhoto = "";
    public string mustView = "1";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hidd_sysId.Value = Request.QueryString["sysId"];
            showInfo();
        }
    }

    //显示自定义小板块
    private void showInfo()
    {
        string sysId = Request.QueryString["sysId"];
        string FUserRightId = EConvert.ToString(Session["DFUserRightId"]);
        if (!string.IsNullOrEmpty(FUserRightId))
        {
            SortedList sl = new SortedList();
            sl.Add("FUserRightId", FUserRightId);
            sl.Add("sysId", sysId);
            DataTable dt = rc.GetTable("select FID,FMyTable,FPic from CF_User where FLinkId=@FUserRightId and FType=@sysId ", rc.ConvertParameters(sl));
            if (dt != null && dt.Rows.Count > 0)
            {
                string[] mytable = dt.Rows[0]["FMyTable"].ToString().Split(':');
                if (mytable.Length > 0 && mytable[0]!="")
                {

                    //左则
                    string lefttable = mytable[0];
                    foreach (string str in lefttable.Split(','))
                    {

                        if (File.Exists(Server.MapPath("../maintable/t" + str + ".ascx")))//先判断模块控件是否存在
                            plhLeft.Controls.Add(LoadControl("../maintable/t" + str + ".ascx"));
                    }

                    //右则
                    //string righttable = mytable[1];
                    //foreach (string str in righttable.Split(','))
                    //{
                    //    if (File.Exists(Server.MapPath("../maintable/t" + str + ".ascx")))//先判断模块控件是否存在
                    //        plhRight.Controls.Add(LoadControl("../maintable/t" + str + ".ascx"));
                    //}

                    //自定义背景图片
                    tablePhoto = dt.Rows[0]["FPic"].ToString();
                }
                else
                    Response.Redirect("../maintable/dragconfig.aspx?sysId=" + hidd_sysId.Value);
            }
            else
            {
                Response.Redirect("../maintable/dragconfig.aspx?sysId=" + hidd_sysId.Value);
            }
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "s", "<script>_upc();</script>", true);
    }
}
