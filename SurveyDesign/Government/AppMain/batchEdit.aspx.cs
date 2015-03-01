using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleApp;
using System.Drawing;
using ProjectData;

public partial class Government_AppMain_batchEdit : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request["fsystemid"] == null || Request["fsystemid"] == "")
            {
                return;
            }

            t_FYear.Text = DateTime.Now.Year.ToString();
            btnSave.Attributes.Add("onclick", "if( checkInfo()){return true;}else{return false;}");
            ControlBind();
            if (Request["fid"] != null && Request["fid"] != "")
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
            }
            else
            {
                CreateNo();
            }
        }
    }
    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fname,fnumber from cf_sys_systemname where 1=1 ");
        //sb.Append(" where fnumber in ");
        //sb.Append(" (select fsystemid from cf_sys_userright ");
        //sb.Append(" where fuserid='" + this.Session["DFUserId"].ToString() + "' ");
        //sb.Append(" and fisdeleted=0)");
        sb.Append(" and fisdeleted=0 ");
        sb.Append(" order by forder ");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt == null || dt.Rows.Count == 0)
        {
            return;
        }
        this.t_FSystemID.DataSource = dt;
        this.t_FSystemID.DataTextField = "FName";
        this.t_FSystemID.DataValueField = "FNumber";
        this.t_FSystemID.DataBind();
        t_FSystemID.Items.Insert(0, new ListItem("--请选择--", ""));

        t_FSystemID.SelectedIndex = t_FSystemID.Items.IndexOf(t_FSystemID.Items.FindByValue(Request["fsystemid"]));
        t_FSystemID.Enabled = false;
    }

    private void CreateNo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select max(FNumber) from CF_App_BatchNo ");
        sb.Append(" where FDFId='" + this.Session["DFId"].ToString() + "'");
        sb.Append(" and FSystemID='" + Request["fsystemid"] + "'");
        sb.Append(" and fnumber like '" + DateTime.Now.Year.ToString() + "%'");

        string fNo = rc.GetSignValue(sb.ToString());
        if (fNo != null)
        {
            fNo = (EConvert.ToInt(fNo) + 1).ToString();
        }
        else
        {
            fNo = DateTime.Now.Year.ToString() + "001";
        }

        t_FNumber.Text = fNo;
        t_FTtile.Text = DateTime.Now.Year.ToString() + "年 第" + EConvert.ToInt(fNo.Substring(4, 3)) + "批";
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * from CF_App_BatchNo ");
        sb.Append(" where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            pageTool tool = new pageTool(this.Page);
            tool.fillPageControl(dt.Rows[0]);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "";
        if (!string.IsNullOrEmpty(t_FID.Value))
        {
            sql = "update CF_App_BatchNo set FNumber='" + t_FNumber.Text.Trim() + "',FTtile='" + t_FTtile.Text.Trim()
                + "',FYear='" + t_FYear.Text.Trim() + "',FSystemID='" + t_FSystemID.SelectedValue + "',FState='" + t_FState.SelectedValue.Trim()
                + "' where FID='" + t_FID.Value + "'";
        }
        else
        {
            t_FID.Value = Guid.NewGuid().ToString();
            sql = string.Format(@" insert CF_App_BatchNo (FID,FIsDeleted,FCreateTime,FNumber,FTtile
                        ,FYear,FSystemID,FState,FDFId)
                        values ('" + t_FID.Value + "',0,getdate(),'" + t_FNumber.Text.Trim() + "','" + t_FTtile.Text.Trim()
                                  + "','" + t_FYear.Text.Trim() + "','" + t_FSystemID.SelectedValue.Trim() + "','" + t_FState.SelectedValue.Trim()
                                  + "','" + Session["DFId"].ToString() + "')");
        }
        if (sh.PExcute(sql))
        { tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("保存失败"); }
    }
}