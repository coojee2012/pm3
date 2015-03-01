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
            if (Session["FAppId"] != null && !string.IsNullOrEmpty(Session["FAppId"].ToString()))
            { t_fappid.Value = Session["FAppId"].ToString(); }
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
            if (Request["fid"] != null && !string.IsNullOrEmpty(Request["fid"]))
            { t_FID.Value = Request["fid"].ToString(); ;showInfo(); }
        }
    }
    public void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "select * from YW_JN_AppProduct where fid='" + t_FID.Value + "' ";
        DataTable dt = sh.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            this.FMain.Value = dt.Rows[0]["SM"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = ""; pageTool tool = new pageTool(this.Page, "t_");
        if (!string.IsNullOrEmpty(t_FID.Value))
        {
            sql += " update YW_JN_AppProduct set editTime=getdate(), MC='"+t_MC.Text+"', BZMC='"+t_BZMC.Text+"', BZH='"+t_BZH.Text
                +"', SBJB='"+t_SBJB.Text+"', CPLB='"+t_CPLB.Text+"', CPGG='"+t_CPGG.Text+"', CPXH='"+t_CPXH.Text+"', JSBZ='"+t_JSBZ.Text
                + "', JCJG='" + t_JCJG.Text + "', BGBH='" + t_BGBH.Text + "', JYSJ='" + t_JYSJ.Text + "', SM='" + this.FMain.Value + "' where fid ='" + t_FID.Value + "' ";
        }
        else
        {
            t_FID.Value = Guid.NewGuid().ToString();
            sql +=string.Format(@" insert YW_JN_AppProduct (FID, Fappid, fbaseid, ftime, editTime, MC, BZMC, BZH, DJBH, DJSJ, SBJB
                    , CPLB, CPGG, CPXH, JSBZ, JCJG, BGBH, JYSJ, SM) values ('"+t_FID.Value+"', '"+t_fappid.Value
                    +"', '"+CurrentEntUser.EntId+"', getdate(), getdate(),'"+t_MC.Text+"', '"+t_BZMC.Text+"', '"+t_BZH.Text
                    +"', '"+t_SBJB.Text+"', '"+t_CPLB.Text+"', '"+t_CPGG.Text+"', '"+t_CPXH.Text+"', '"+t_JSBZ.Text
                    + "', '" + t_JCJG.Text + "', '" + t_BGBH.Text + "', '" + t_JYSJ.Text + "', '" 
                    + FMain.Value.ToString().Replace("'", "''") + "') ");
        }
        if (sh.PExcute(sql))
        { tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("保存失败"); }
    }
    public void readOnly()
    {
        btnSave.Enabled = false; t_BZH.Enabled = false;
        t_BZMC.Enabled = false; t_BGBH.Enabled = false;
        t_CPGG.Enabled = false; t_CPLB.Enabled = false;
        t_CPXH.Enabled = false; t_JCJG.Enabled = false;
        t_JSBZ.Enabled = false; t_JYSJ.Enabled = false;
        t_MC.Enabled = false; t_SBJB.Enabled = false;
    }
}