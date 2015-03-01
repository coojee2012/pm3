using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Approve.Common;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class JNCLEnt_mangeInfo_entJJ : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnSave.Attributes.Add("onclick", "if(checkInfo()){return true;}else{return false;}");
            showInfo();
        }
    }
    public void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "select * from YW_JN_AppEntJJ where fbaseid='" + CurrentEntUser.EntId + "' and Fappid = 'JBQK' ";
        DataTable dt = sh.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = ""; pageTool tool = new pageTool(this.Page, "t_");
        if (!string.IsNullOrEmpty(t_FID.Value))
        { sql += " update YW_JN_AppEntJJ set jianjie='" + t_jianjie.Text + "' where FID='" + t_FID.Value + "'"; }
        else
        {
            t_FID.Value = Guid.NewGuid().ToString();
            sql += " insert YW_JN_AppEntJJ (FID, Fappid, fbaseid, ftime, editTime, jianjie) values ('" + t_FID.Value
                + "','JBQK','" + CurrentEntUser.EntId + "',getdate(),getdate(),'" + t_jianjie.Text + "')";
        }
        if (sh.PExcute(sql))
        { tool.showMessage("保存成功"); showInfo(); }
        else
        { tool.showMessage("保存失败"); }
    }

}