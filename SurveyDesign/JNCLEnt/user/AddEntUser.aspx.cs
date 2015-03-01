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
            t_FMenuRoleId.Text = "省级工法";
            t_FEntType.Text = "省级工法";
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        #region 验证        
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            DataTable dt = sh.GetTable("select fid from CF_Sys_UserRight where FName='" + t_FName.Text + "' and FUserId<>'" + t_Fid.Value + "' and FSystemId='221'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("用户名已存在！");
                t_FName.Focus();
                return;
            }
            dt = sh.GetTable("select fid from CF_Sys_User where FName='" + t_FName.Text + "' and FID<>'" + t_Fid.Value + "' and FSystemId='221' ");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("用户名已存在！");
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
                + "',FLockNumber='" + t_FLockNumber.Text.Trim() + "',FCompany='" + t_FCompany.Text.Trim()
                + "',FLicence='" + t_FLicence.Text.Trim() + "',FJuridcialCode='" + t_FJuridcialCode.Text.Trim()
                + "',FAcceptPerson='" + t_FAcceptPerson.Text.Trim() + "',FLinkMan='" + t_FLinkMan.Text.Trim()
                + "',FTel='" + t_FTel.Text.Trim() + "',FAddress='" + t_FAddress.Text.Trim()
                + "',FBaseInfoId='" + t_FBaseInfoId.Value + "',FManageDeptId='" + t_FManageDeptId.Value
                + "',FSystemId=222,FCompanyId='" + t_FCompanyId.Value + "',FType=2,FIsUserName=1,Fisz=0 where FID='" + t_Fid.Value + "' ;";
        }
        else
        {
            t_Fid.Value = Guid.NewGuid().ToString();
            sql = string.Format(@"insert CF_Sys_User (FID,FIsDeleted,FBaseInfoId,FCreateTime,FType
                                    ,FListType,FLockNumber,FMenuRoleId,FIsUserName
                                    ,FPassWord,FName,FJuridcialCode,FCompany,FLicence,FAcceptPerson
                                    ,FLinkMan,FTel,FAddress,FSystemId,FManageDeptId,FCompanyId,Fisz)
                                values ('" + t_Fid.Value + "',0,'" + t_FBaseInfoId.Value + "',getdate(),2,0,'" + t_FLockNumber.Text.Trim()
                                + "',null,1,'" + SecurityEncryption.DESEncrypt(txtFPassWord.Text.Trim())
                                + "','" + t_FName.Text.Trim() + "','" + t_FJuridcialCode.Text.Trim()
                                + "','" + t_FCompany.Text.Trim() + "','" + t_FLicence.Text.Trim() + "','"
                                + t_FAcceptPerson.Text.Trim() + "','" + t_FLinkMan.Text.Trim() + "','" + t_FTel.Text.Trim()
                                + "','" + t_FAddress.Text.Trim() + "','222','" + t_FManageDeptId.Value + "','" + t_FCompanyId.Value + "',0) ;");

//            sql += string.Format(@" insert LINKER_95.dbCenterSC.dbo.CF_App_PrintIc 
//                                (FId,FAppId,FBaseInfoId,FManageTypeId,FSystemId,FIsICPrint,FICDate,FIsNew,FIsSend,FIsTake
//                                ,FIsAppEnd,FCreateTime,FIsDeleted,PrintContent,FUserID)
//                                values (newid(),null,'" + t_FBaseInfoId.Value
//                                + "',4001,222,0,getdate(),0,0,0,0,getdate(),0,'节能材料','" + t_Fid.Value + "') ;");
        }
        //权限数据
        int cou = int.Parse(rc.GetSignValue("select count(1) from CF_Sys_Userright where fuserid='" + t_Fid.Value
                  + "' and FBaseinfoID='" + t_FBaseInfoId.Value + "' and fsystemid=221"));
        if (cou <= 0)
        {
            sql += string.Format(@"Insert into CF_Sys_Userright (fid,ftime,fcreatetime
                            ,fuserid,fisdeleted,fsystemid,FBaseinfoID,FName,FPassWord
                            ,FLockLabelNumber,FLockNumber)
                            values (newid(),getdate(),getdate(),'" + t_Fid.Value + "',0,222,'"
                              + t_FBaseInfoId.Value + "','" + t_FName.Text.Trim() + "','" + Encrypt.MiscClass.encode(txtFPassWord.Text.Trim())
                              + "','" + t_FLockNumber.Text.Trim() + "','" + t_FLockNumber.Text.Trim() + "' ) ;");
        }
        if (!string.IsNullOrEmpty(link_FID.Value))
        {
            sql += " update LINKER_95.dbCenterSC.dbo.cf_sys_user set FName='" + t_FName.Text.Trim() + "',FPassWord='" + Encrypt.MiscClass.encode(txtFPassWord.Text.Trim())
                   + "',FLockNumber='" + t_FLockNumber.Text.Trim() + "',FCompany='" + t_FCompany.Text.Trim()
                   + "',FLinkMan='" + t_FLinkMan.Text.Trim() + "',FID='" + t_Fid.Value
                   + "',FTel='" + t_FTel.Text.Trim() + "',FAddress='" + t_FAddress.Text.Trim()
                   + "',FBaseInfoId='" + t_FBaseInfoId.Value + "',FManageDeptId='" + t_FManageDeptId.Value
                   + "',FSystemId=222,FCompanyId='" + t_FCompanyId.Value + "',FType=2 where FID='" + link_FID.Value + "' ;";
            sql += string.Format(@"Insert into LINKER_95.dbCenterSC.dbo.CF_Sys_Userright (fid,ftime,fcreatetime
                            ,fuserid,fisdeleted,fsystemid,FName,FPassWord
                            ,FLockNumber)
                            values (newid(),getdate(),getdate(),'" + t_Fid.Value + "',0,222,'"
                                  + t_FName.Text.Trim() + "','" + Encrypt.MiscClass.encode(txtFPassWord.Text.Trim())
                                  + "','" + t_FLockNumber.Text.Trim() + "' ) ;");
        }
        else
        {
            sql += " update LINKER_95.dbCenterSC.dbo.cf_sys_user set FName='" + t_FName.Text.Trim() + "',FPassWord='" + Encrypt.MiscClass.encode(txtFPassWord.Text.Trim())
                + "',FLockNumber='" + t_FLockNumber.Text.Trim() + "',FCompany='" + t_FCompany.Text.Trim()
                + "',FLinkMan='" + t_FLinkMan.Text.Trim()
                + "',FTel='" + t_FTel.Text.Trim() + "',FAddress='" + t_FAddress.Text.Trim()
                + "',FManageDeptId='" + t_FManageDeptId.Value
                + "',FSystemId=222,FCompanyId='" + t_FCompanyId.Value + "',FType=2 where FId='" + t_Fid.Value + "' ;";
        }

        if (rc.PExcute(sql))
        {
            tool.showMessage("保存成功"); HSaveResult.Value = "1";
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