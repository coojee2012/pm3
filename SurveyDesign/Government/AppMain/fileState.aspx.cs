using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Approve.RuleCenter;
using Approve.Common;

public partial class Government_AppMain_fileState : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["fid"] != null)
                t_YWBM.Value = Request["fid"].ToString();
            if (Request["ftype"] != null)
                t_ftype.Value = Request["ftype"].ToString();
            bind();
        }
    }
    public void bind()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = string.Format(@"select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and Ftype='" + t_ftype.Value + "'");
        DataTable dt = sh.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            if (t_Fstate.Items.FindByValue(dt.Rows[0]["Fstate"].ToString()) != null)
                t_Fstate.SelectedValue = t_Fstate.Items.FindByValue(dt.Rows[0]["Fstate"].ToString()).Value;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        string sql = "";
        if (!string.IsNullOrEmpty(t_FID.Value))
        {
            sql = "update YW_GF_FileState set Fstate='" + t_Fstate.SelectedValue + "',Fremark='" + t_Fremark.Text
                + "' where FID='" + t_FID.Value + "'";
        }
        else
        {
            t_FID.Value = Guid.NewGuid().ToString();
            sql = " insert YW_GF_FileState (FID,Fstate,Fremark,FAppid,Ftype) values ('" + t_FID.Value
                + "','" + t_Fstate.SelectedValue + "','" + t_Fremark.Text + "','" + t_YWBM.Value + "','" + t_ftype.Value + "')";
        }
        if (sh.PExcute(sql))
        { tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("保存失败"); }
    }
}