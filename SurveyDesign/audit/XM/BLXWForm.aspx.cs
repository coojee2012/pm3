using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Approve.RuleCenter;

public partial class Share_Main_Default2 : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucProjectPlace.fNumber = "5119";
        }
    }
    //选取企业
    protected void btnEnt_Click(object sender, EventArgs e)
    {
        string sql = "select * from LINKER_95.dbCenterSC.dbo.V_JST_QY where FID='" + ent_FID.Value + "'";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            //t_FLockNumber.Text = dt.Rows[0]["FLockNumber"].ToString();
            TextBox7.Text = dt.Rows[0]["FName"].ToString();         
        }
    }
}