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
using System.Data.SqlClient;

public partial class JNCLEnt_mangeInfo_editEquipmentJC : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.btnSave.Attributes.Add("onclick", "if(checkInfo()){return true;}else{return false;}");
            //if (Session["FAppId"] != null && !string.IsNullOrEmpty(Session["FAppId"].ToString()))
            //{ t_fappid.Value = Session["FAppId"].ToString(); }
            //if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            //{ if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
            //if (Request["fid"] != null && !string.IsNullOrEmpty(Request["fid"]))
            //{ t_FID.Value = Request["fid"].ToString(); ;showInfo(); }
            showInfo();
            EnabledControl();
        }
    }
    public void showInfo()
    {
        if (!string.IsNullOrEmpty(FID))
        {
            pageTool tool = new pageTool(this.Page, "t_");
            string sql = "select * from YW_JN_AppEquipment where fid='" + FID + "' ";
            DataTable dt = sh.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.fillPageControl(dt.Rows[0]);
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = "";
        bool success = false;
        pageTool tool = new pageTool(this.Page, "t_");
        List<SqlParameter> listParam = new List<SqlParameter>();
        if (!string.IsNullOrEmpty(FID))
        {
            //sql += " update YW_JN_AppEquipment set editTime=getdate(), SBMC='" + t_SBMC.Text + "', XH='" + t_XH.Text + "', GL='" + t_GL.Text
            //    + "', NU='" + t_NU.Text + "', DW='" + t_DW.Text + "', SFZY='" + t_SFZY.SelectedValue + "', YZ='" + t_YZ.Text + "', JZ='" + t_JZ.Text
            //    + "', ZT='" + t_ZT.SelectedValue + "' where fid='" + t_FID.Value + "' ";
            sql = @"UPDATE [dbo].[YW_JN_AppEquipment]
                       SET [editTime] = getdate()
                          ,[SBMC] = @SBMC
                          ,[XH] = @XH
                          ,[GL] = @GL
                          ,[NU] = @NU
                          ,[DW] = @DW
                          ,[SFZY] = @SFZY
                          ,[YZ] = @YZ
                          ,[JZ] = @JZ
                          ,[ZT] = @ZT
                     WHERE FID=@FID";
            listParam.Add(new SqlParameter() { ParameterName = "@SBMC", Value = t_SBMC.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@XH", Value = t_XH.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@GL", Value = t_GL.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@NU", Value = t_NU.Text, SqlDbType = SqlDbType.Decimal });
            listParam.Add(new SqlParameter() { ParameterName = "@DW", Value = t_DW.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@SFZY", Value = t_SFZY.SelectedValue, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@YZ", Value = t_YZ.Text, SqlDbType = SqlDbType.Decimal });
            listParam.Add(new SqlParameter() { ParameterName = "@JZ", Value = t_JZ.Text, SqlDbType = SqlDbType.Decimal });
            listParam.Add(new SqlParameter() { ParameterName = "@ZT", Value = t_ZT.SelectedValue, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@FID", Value = FID, SqlDbType = SqlDbType.VarChar });
            success = rc.PExcute(sql, listParam.ToArray());
        }
        else
        {
            //t_FID.Value = Guid.NewGuid().ToString();
            //sql += " insert YW_JN_AppEquipment (FID, Fappid, fbaseid, ftime, editTime, SBMC, XH, GL, NU, DW, SFZY, YZ, JZ, ZT,type) values ('" + t_FID.Value
            //    + "', '" + t_fappid.Value + "', '" + CurrentEntUser.EntId + "', getdate(), getdate(), '" + t_SBMC.Text + "', '" + t_XH.Text
            //    + "', '" + t_GL.Text + "', '" + t_NU.Text + "', '" + t_DW.Text + "', '" + t_SFZY.SelectedValue + "', '" + t_YZ.Text
            //    + "', '" + t_JZ.Text + "','" + t_ZT.SelectedValue + "',0) ";
            sql = @"INSERT INTO [dbo].[YW_JN_AppEquipment]
                               ([FID]
                               ,[YWBM]
                               ,[fbaseid]
                               ,[ftime]
                               ,[editTime]
                               ,[SBMC]
                               ,[XH]
                               ,[GL]
                               ,[NU]
                               ,[DW]
                               ,[SFZY]
                               ,[YZ]
                               ,[JZ]
                               ,[ZT]
                               ,[type])
                         VALUES(NEWID(),@YWBM,@fbaseid,GETDATE(),GETDATE(),@SBMC,@XH,@GL,@NU,@DW,@SFZY,@YZ,@JZ,@ZT,1)";//type 0为生产设备 1位检测设备

            listParam.Add(new SqlParameter() { ParameterName = "@YWBM", Value = YWBM, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@fbaseid", Value = CurrentEntUser.EntId, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@SBMC", Value = t_SBMC.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@XH", Value = t_XH.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@GL", Value = t_GL.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@NU", Value = t_NU.Text, SqlDbType = SqlDbType.Decimal });
            listParam.Add(new SqlParameter() { ParameterName = "@DW", Value = t_DW.Text, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@SFZY", Value = t_SFZY.SelectedValue, SqlDbType = SqlDbType.VarChar });
            listParam.Add(new SqlParameter() { ParameterName = "@YZ", Value = t_YZ.Text, SqlDbType = SqlDbType.Decimal });
            listParam.Add(new SqlParameter() { ParameterName = "@JZ", Value = t_JZ.Text, SqlDbType = SqlDbType.Decimal });
            listParam.Add(new SqlParameter() { ParameterName = "@ZT", Value = t_ZT.SelectedValue, SqlDbType = SqlDbType.VarChar });
            success = rc.PExcute(sql, listParam.ToArray());
        }
        if (success)
            tool.showMessage("保存成功");
        else
            tool.showMessage("保存失败"); 
    }
    private string FID
    {
        get
        {
            return Request.QueryString["FID"];
        }
    }
    private void EnabledControl()
    {
        if (Audit == "1" || FIsApprove == "1") //审核页面跳转
        {
            foreach (Control control in this.form1.Controls)
            {
                WebHelper.SetControlEnabled(control);
            }
        }
    }
    private string YWBM
    {
        get
        {
            return Request.QueryString["YWBM"];
        }
    }
    private string FIsApprove
    {
        get
        {
            string value = Session["FIsApprove"] == null ? "" : Session["FIsApprove"].ToString();
            if (string.IsNullOrEmpty(value))
                return "0";
            return value;
        }
    }
    private string Audit
    {
        get
        {
            return Request.QueryString["audit"];
        }
    }
}