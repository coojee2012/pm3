using Approve.Common;
using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyXZYJS_user_EditUser : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["fid"]))
            {
                t_Fid.Value = Request["fid"].ToString();
                showInfo();
            }
        }
    }
    public void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from cf_sys_user where fid='" + t_Fid.Value + "'");
        DataTable dt = rc.GetTable(sb.ToString());
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
        #region 验证
        if (!string.IsNullOrEmpty(t_FCompany.Text))
        {
            DataTable dt = rc.GetTable("select top 1 fid from CF_Sys_User where FRESON='" + t_FRESON.Text.Trim() + "' and FSystemId not in (100,320,330,340)"); //建设单位、选址意见书、用地规划、工程规划
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("此建设单位名称已存在");
                t_FName.Focus();
                return;
            }
        }
        #endregion
        string sql = "";
        //用户数据
        if (string.IsNullOrEmpty(t_FBaseInfoId.Value)) { t_FBaseInfoId.Value = Guid.NewGuid().ToString(); }
        if (!string.IsNullOrEmpty(t_Fid.Value))
        {
            sql = "update CF_Sys_User set FName='" + t_FName.Text.Trim() + "',FPassWord='" + SecurityEncryption.DESEncrypt(txtFPassWord.Text.Trim())
                + "',FLockNumber='" + t_FLockNumber.Text.Trim() + "',FRESON='" + t_FRESON.Text.Trim() + "',FCompany='" + t_FCompany.Text.Trim()
                + "',FLicence='" + t_FLicence.Text.Trim() + "',FJuridcialCode='" + t_FJuridcialCode.Text.Trim()
                + "',FAcceptPerson='" + t_FAcceptPerson.Text.Trim() + "',FLinkMan='" + t_FLinkMan.Text.Trim()
                + "',FTel='" + t_FTel.Text.Trim() + "',FAddress='" + t_FAddress.Text.Trim()
                + "',FBaseInfoId='" + t_FBaseInfoId.Value + "',FManageDeptId='" + t_FManageDeptId.Value
                + "',FSystemId=100,FCompanyId='" + t_FCompanyId.Value + "',FType=2,FIsUserName=1 where FID='" + t_Fid.Value + "' ;";
        }

        if (rc.PExcute(sql))
        {
            tool.showMessageAndRunFunction("保存成功","window.returnValue='1';window.close();");
        }
        else
        {
            tool.showMessage("保存失败");
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
        string sql = "select * from LINKER_95.dbCenterSC.dbo.V_JST_QY where FID='" + ent_FID.Value + "'";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            //t_FLockNumber.Text = dt.Rows[0]["FLockNumber"].ToString();
            t_FCompany.Text = dt.Rows[0]["FName"].ToString();
            t_FLicence.Text = dt.Rows[0]["FLicence"].ToString();
            t_FJuridcialCode.Text = dt.Rows[0]["FJuridcialCode"].ToString();
            //t_FAcceptPerson.Text = dt.Rows[0]["FAcceptPerson"].ToString();
            t_FLinkMan.Text = dt.Rows[0]["FLinkMan"].ToString();
            t_FTel.Text = dt.Rows[0]["FTel"].ToString();
            t_FAddress.Text = dt.Rows[0]["FAddress"].ToString();
            if (dt.Rows[0]["isrc"].ToString() == "1")
            {
                string fid = dt.Rows[0]["FId"].ToString();
                t_FBaseInfoId.Value = fid.Substring(0, fid.Length - 3);
            }
            else { t_FBaseInfoId.Value = dt.Rows[0]["FId"].ToString(); }
            t_FManageDeptId.Value = dt.Rows[0]["FUpDeptId"].ToString();
            t_FCompanyId.Value = Guid.NewGuid().ToString(); //dt.Rows[0]["FCompanyId"].ToString();
        }
    }
}