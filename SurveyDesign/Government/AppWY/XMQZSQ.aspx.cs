using Approve.EntityBase;
using Approve.RuleCenter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppWY_XMQZSQ : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["XMBH"] != null)
        {
            //Approve.Common.pageTool tool = new Approve.Common.pageTool(this.Page);
            string xmbh = Request.QueryString["XMBH"].ToString();
            //SaveOptionEnum so = SaveOptionEnum.Unknown;
            //SortedList sl = new SortedList();
            //sl = tool.getPageValue();
            //while (sl.IndexOfValue("") != -1)
            //{
            //    sl.RemoveAt(sl.IndexOfValue(""));
            //}
            StringBuilder sb = new StringBuilder();
            string InSystemLostRemarks = t_InSystemLostRemarks.Text;
            
                InSystemLostRemarks += " 管理部门名称：" + (Session["DFName"] == null ? "" : Session["DFName"].ToString()) + "，操作人员姓名：" + OperateUser.Text;
            
            try 
            {
                //添加情况
                if (hidHasHistory.Value == "0")
                {
                    sb.Append("insert into wy_xm_jbxx_history([Fid],[XMBH],[XMMC],[XMSD],[XMDZ],[JSDW],[JSDWDZ],[XMLX],[XMZLX],[JSXZ],[JSMS],[XMZTZ],[JSGM],[JSNR],[BH],[JSDWZZJFDM],[JSDWFR],[JSDWFRDH],[JSDWJSFZR],[JSDWJSFZRZC],[JSDWJSFZRDH],[FUpDeptId],[FUpDeptName],[FTime],[FSystemId],[FCreateUser],[FCreateTime],[FIsDeleted],[FBaseInfoID],[StandardStatus],[SourceAppID],[InSystemLostReasonID],[InSystemLostRemarks],[JGQYID])");
                    sb.Append("select newid(),'" + xmbh + "',[XMMC],[XMSD],[XMDZ],[JSDW],[JSDWDZ],[XMLX],[XMZLX],[JSXZ],[JSMS],[XMZTZ],[JSGM],[JSNR],[BH],[JSDWZZJFDM],[JSDWFR],[JSDWFRDH],[JSDWJSFZR],[JSDWJSFZRZC],[JSDWJSFZRDH],[FUpDeptId],[FUpDeptName],'" + DateTime.Now + "',[FSystemId],'" + OperateUser.Text + "','" + DateTime.Now + "',0,[FBaseInfoID],[StandardStatus],[SourceAppID],3,'" + InSystemLostRemarks + "','0' from wy_xm_jbxx where xmbh='" + xmbh + "'");
                    sb.Append("update wy_xm_jbxx set fbaseinfoid='0' where xmbh='" + xmbh + "'");
                    //t_InSystemLostRemarks.Text = sbIns.ToString();
                }
                else if (hidHasHistory.Value == "1")
                {
                    sb.Append("update wy_xm_jbxx_history set fcreateuser='" + OperateUser.Text + "',InSystemLostReasonID=3,InSystemLostRemarks='" + InSystemLostRemarks + "' where xmbh='" + xmbh + "'");

                }
                
                if (sb.ToString() != "")
                {
                    if (rc.PExcute(sb.ToString(),true))
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('保存成功');window.returnValue='ok';window.close();</script>");
                    }
                }
            }
            catch 
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('保存发生异常，请联系管理员！');</script>");
            }
            
            
        }
        
    }
    protected void showInfo()
    {
        string sql = "select Fname from cf_sys_user where fid='" + Session["DFUserId"].ToString() + "'";
        try 
        {
            OperateUser.Text = rc.GetSignValue(sql);
            if (Request.QueryString["XMBH"] != null)
            {
                string xmbh = Request.QueryString["XMBH"].ToString();
                DataTable dt=rc.GetTable("select * from wy_xm_jbxx_history where xmbh='" + xmbh + "'");
                if (dt.Rows.Count> 0)
                {
                    //t_InSystemLostReasonID.SelectedValue = dt.Rows[0]["InSystemLostReasonID"].ToString();
                    t_InSystemLostRemarks.Text = dt.Rows[0]["InSystemLostRemarks"].ToString();
                    hidHasHistory.Value = "1";
                }
                else 
                {
                    hidHasHistory.Value = "0";
                }
            }
        }
        catch { }
    }
    
}