using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
public partial class OA_Bulletin_LookBull : System.Web.UI.Page
{
    bool temp = false;
    OA oa = new OA();
    string userId = "";
    SaveOptionEnum so = SaveOptionEnum.Insert;
    string mag = "公告发布成功！";
    string FID = Guid.NewGuid().ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!IsPostBack)
        {
            
            
            if (Request.QueryString["fid"] != null)
            {
                FID = Request.QueryString["fid"].ToString();

                ShowInfo();
            }

        }
        if (Session["FEmpID"] != null)
        {
            userId = Session["FEmpID"].ToString();

        }
        if (Request.QueryString["fid"] != null)
        {
            FID = Request.QueryString["fid"].ToString();
            so = SaveOptionEnum.Update;
        }

    }

    protected void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        DataTable dt = oa.GetTable("select * from CF_OA_Bulletin where FID ='" + FID + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            
        }
        dt = oa.GetTable("select typename from CF_OA_BulType where FID='"+this.t_FBulTypeId.Text+"'");
        if (dt != null && dt.Rows.Count > 0)
        {
            this.t_FBulTypeId.Text = dt.Rows[0][0].ToString();
        }
        string MsgTo = "";
        dt = oa.GetTable("select RoleID from CF_OA_BullReal where BullID='" + FID + "' and fisdeleted = 'false'");
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 0)
                {
                    MsgTo += ",";
                }
                MsgTo += "'" + dt.Rows[i][0].ToString() + "'";
            }
        }
        this.presonFID.Value = MsgTo;

        ShowName();


    }



   
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowName();
    }

    private void ShowName()
    {
        this.presonFID.Value = this.presonFID.Value.ToString().Replace("^", "'");
        string str = "";
        StringBuilder sb = new StringBuilder();
        if (this.presonFID.Value.Length > 8)
        {
            sb.Append("select fname from CF_OA_Organization where Fnumber in(" + this.presonFID.Value + ")");
            DataTable dt = oa.GetTable(sb.ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (str.Length > 0)
                {
                    str += ",";
                }
                str += dt.Rows[i]["fname"].ToString();
            }
            this.presonList.Text = str;
        }
        this.presonList.Text = str;

        this.presonFID.Value = this.presonFID.Value.ToString().Replace("'", "^");
    }
}
