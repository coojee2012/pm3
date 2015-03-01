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

public partial class JNCLEnt_mangeInfo_editProduct : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnSave.Attributes.Add("onclick", "if(checkInfo()){return true;}else{return false;}");
            if (Request["fid"] != null && !string.IsNullOrEmpty(Request["fid"]))
            { t_FID.Value = Request["fid"].ToString(); ;showInfo(); }
        }
    }
    public void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "select * from YW_JN_Product where fid='" + t_FID.Value + "' ";
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
        {
            sql += " update YW_JN_Product set editTime=getdate(), MC='" + t_MC.Text + "', BZMC='" + t_BZMC.Text + "', BZH='" + t_BZH.Text
                + "', DJBH='" + t_DJBH.Text + "', DJSJ='" + t_DJSJ.Text + "' where fid ='" + t_FID.Value + "' ";
        }
        else
        {
            t_FID.Value = Guid.NewGuid().ToString();
            sql += " insert YW_JN_Product (FID, fbaseid, ftime, editTime, MC, BZMC, BZH, DJBH, DJSJ) values ('" + t_FID.Value
                + "', '" + CurrentEntUser.EntId + "', getdate(), getdate(), '" + t_MC.Text + "', '" + t_BZMC.Text + "', '" + t_BZH.Text
                + "', '" + t_DJBH.Text + "', '" + t_DJSJ.Text + "') ";
        }
        if (sh.PExcute(sql))
        { tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("保存失败"); }
    }
}