using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using System.Data.SqlClient;
using ProjectData;


public partial class Share_WebSide_pasword : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        StringBuilder sb = new StringBuilder();
        sb.Append(" begin ");
        string sql = string.Format(@"SELECT A.FPassWord ,A.fid FROM CF_Sys_User A
            ,LINKER_95.JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER B
            where A.FID=B.FID AND A.FPassWord =B.FPassWord 
            AND B.fid in
            (
	            SELECT JS.FID
	            FROM LINKER_95.JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS
	            WHERE FID IN
	            (
	            SELECT B.JSDW
	            FROM 
	            LINKER_95.JKCWFDB_WORK_NJS.DBO.JST_YW_JGYSBA AS BA
	            LEFT JOIN LINKER_95.JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
	            WHERE (BA.YWZT IN (314,315,316,-314,-315,-316,1)   --314待接件,315待初审,316待复审,-314接件未通过,-315初审未通过,-316复审未通过,NULL未上报,1正常办结
		               OR BA.YWZT IS NULL
		               ) 
	              --AND ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
	              AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
	              AND BA.YWBM<>'C8E8ABF5-4B6C-47B1-8BF8-8027EFA32932'
	            )

            )");
        DataTable dt = rc.GetTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string pass = dt.Rows[i]["FPassWord"].ToString();
            pass = SecurityEncryption.DESEncrypt(pass);
            sb.Append(" update cf_sys_user set FPassWord='" + pass + "' where fid='" + dt.Rows[i]["fid"].ToString() + "' ");
        }
        sql = string.Format(@"SELECT A.FPassWord ,A.FuserID,A.FSYSTEMID FROM CF_Sys_UserRight A
            ,LINKER_95.JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER B
            where A.FuserID=B.FID AND A.FPassWord =B.FPassWord AND A.FSystemId=1122 
            AND B.fid in
            (
	            SELECT JS.FID
	            FROM LINKER_95.JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS
	            WHERE FID IN
	            (
	            SELECT B.JSDW
	            FROM 
	            LINKER_95.JKCWFDB_WORK_NJS.DBO.JST_YW_JGYSBA AS BA
	            LEFT JOIN LINKER_95.JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
	            WHERE (BA.YWZT IN (314,315,316,-314,-315,-316,1)   --314待接件,315待初审,316待复审,-314接件未通过,-315初审未通过,-316复审未通过,NULL未上报,1正常办结
		               OR BA.YWZT IS NULL
		               ) 
	              --AND ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
	              AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
	              AND BA.YWBM<>'C8E8ABF5-4B6C-47B1-8BF8-8027EFA32932'
	            )
            )
            ");
        dt.Clear();
        dt = rc.GetTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string pass = dt.Rows[i]["FPassWord"].ToString();
            pass = SecurityEncryption.DESEncrypt(pass);
            sb.Append(" update CF_Sys_UserRight set FPassWord='" + pass + "' where FuserID='" + dt.Rows[i]["FuserID"].ToString() + "' ");
        }
        sb.Append(" end ");
        try
        {
            rc.PExcute(sb.ToString()); tool.showMessage("密码修改成功！");            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}