using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using System.Data.SqlClient;
using System.Threading;
using ProjectData;

public partial class GFEnt_user_AddEntUser : System.Web.UI.Page
{
    Share sh = new Share(); RCenter rc = new RCenter();
    ShareTool st = new ShareTool();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["fid"] != null && !string.IsNullOrEmpty(Request["fid"]))
            { t_Fid.Value = Request["fid"].ToString(); ;showInfo(); }
        }
    }
    public void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from cf_sys_user where fid='" + t_Fid.Value + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            btnSelectEnt.Enabled = false;
            tool.fillPageControl(dt.Rows[0]);
            txtFPassWord.Text = SecurityEncryption.DESDecrypt(dt.Rows[0]["FPassWord"].ToString());
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "";
        if (string.IsNullOrEmpty(t_Fid.Value))
        { tool.showMessage("请选择需要分配改权限的企业"); return; }
        //用户数据  
        int cou = int.Parse(rc.GetSignValue("select count(1) from CF_Sys_Userright where fuserid='" + t_Fid.Value + "' and fsystemid=1123"));
        if (cou <= 0)
        {
            sql += string.Format(@"Insert into CF_Sys_Userright (fid,ftime,fcreatetime
                            ,fuserid,fisdeleted,fsystemid,FBaseinfoID,FName,FPassWord
                            ,FLockLabelNumber,FLockNumber)
                            values (newid(),getdate(),getdate(),'" + t_Fid.Value + "',0,1123,'"
                              + t_FBaseInfoId.Value + "','" + t_FName.Text.Trim() + "','" + Encrypt.MiscClass.encode(txtFPassWord.Text.Trim())
                              + "','" + t_FLockNumber.Text.Trim() + "','" + t_FLockNumber.Text.Trim() + "' ) ;");
        }

        if (rc.PExcute(sql))
        {
            tool.showMessage("分配成功"); HSaveResult.Value = "1";
        }
        else
        {
            tool.showMessage("分配失败");
        }
    }
    protected void btnPass_Click(object sender, EventArgs e)
    {

    }
    //选取未发卡库
    protected void btnSelectEnt_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "select * from LINKER_95.dbCenterSC.dbo.cf_sys_user where FID='" + link_FID.Value + "'";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            t_FName.Text = dt.Rows[0]["FName"].ToString();
            txtFPassWord.Text = Encrypt.MiscClass.decode(dt.Rows[0]["FPassWord"].ToString());
        }
    }
    //选取企业
    protected void btnEnt_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "select * from cf_sys_user where FID='" + t_Fid.Value + "'";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            //t_FLockNumber.Text = dt.Rows[0]["FLockNumber"].ToString();
            t_FCompany.Text = dt.Rows[0]["FName"].ToString();
            t_FLicence.Text = dt.Rows[0]["FLicence"].ToString();
            t_FJuridcialCode.Text = dt.Rows[0]["FJuridcialCode"].ToString();
            t_FLinkMan.Text = dt.Rows[0]["FLinkMan"].ToString();
            t_FTel.Text = dt.Rows[0]["FTel"].ToString();
            t_FAddress.Text = dt.Rows[0]["FAddress"].ToString();
            t_FBaseInfoId.Value = dt.Rows[0]["FBaseInfoId"].ToString();
            t_FCompanyId.Value = dt.Rows[0]["FCompanyId"].ToString();
            txtFPassWord.Text = dt.Rows[0]["FPassWord"].ToString();
            t_FName.Text = dt.Rows[0]["FName"].ToString();
            t_FLockNumber.Text = dt.Rows[0]["FLockNumber"].ToString();
        }
    }
}