using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;
using System.Text;


public partial class Admin_main_aLeft : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            showInfo();
        }
    }
    private void showInfo()
    {
        ProjectDB db = new ProjectDB();
        CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == CurrentEntUser.EntId).FirstOrDefault();
        //    if (ent != null)
        //        lefttable5.HRef = "../../Enterprise/main/index.aspx?fid=" + ent.FId + "&SysId=" + EConvert.ToString(ent.FSystemId.Value);
        if (CurrentEntUser.FHUid != null&&CurrentEntUser.FHUid!="")
        {
            string FID = CurrentEntUser.FHUid;
            DateTime time = DateTime.Now.AddHours(3);
            string key = SecurityEncryption.DesEncrypt(FID + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
            string sUrl = "../../ApproveWeb/main/EntselectSysTem.aspx?key=" + HttpUtility.UrlEncode(key, Encoding.UTF8);

            this.YDW.Text = "<div class='o_menu'><a id='a155' class='o_m01_1' href='"+sUrl+"' target='_top'><span>返回原单位</span></a></div>";
            this.dv1.Visible = false;
            this.dv2.Visible = false;
            this.dv3.Visible = false;
            this.dv4.Visible = false;
            this.dv5.Visible = false;
            this.dv6.Visible = false;
            this.dv7.Visible = false;
        }

        
    }



    public string GetRan()
    {
        string str = "";
        Random ran = new Random();
        str = ran.Next().ToString();
        return str;
    }
}
