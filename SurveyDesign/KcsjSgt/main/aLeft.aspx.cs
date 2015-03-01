using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;


public partial class KcsjSgt_main_aLeft : System.Web.UI.Page
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

 

    }



    public string GetRan()
    {
        string str = "";
        Random ran = new Random();
        str = ran.Next().ToString();
        return str;
    }
}
