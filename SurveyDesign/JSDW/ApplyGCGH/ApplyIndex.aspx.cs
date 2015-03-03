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

public partial class JSDW_ApplyYDGH_ApplyIndex : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            conBind();
            ShowInfo();

        }
    }
    //绑定选项
    private void conBind()
    {
        for (int i = DateTime.Now.Year; i >= 2013; i--)
        {
            drop_FYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        drop_FYear.Items.Insert(0, new ListItem("--全部--", ""));
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    private void ShowInfo()
    {
        string sql = @"select A.*,B.GCGHMC,B.ID,B.ProjectType,case B.ProjectType when 1 then '房屋建筑' when 2 then '市政基础' else '其它' end as TypeName FROM CF_App_List A,YW_GCGH B WHERE A.FID = B.YWBM AND B.FBaseInfoId='" + CurrentEntUser.EntId + "'";
        if (!string.IsNullOrEmpty(t_YWFname.Text.Trim()))
        {
            sql += " and A.FName like '%" + t_YWFname.Text.Trim() + "%' ";
        }
        if (!string.IsNullOrEmpty(ddlFState.SelectedValue.Trim()))
        {
            sql += " and A.FState = '" + ddlFState.SelectedValue.Trim() + "' ";
        }
        if (!string.IsNullOrEmpty(drop_FYear.SelectedValue.Trim()))
        {
            sql += " and A.FYear = '" + drop_FYear.SelectedValue.Trim() + "' ";
        }
        sql += " order by A.FCreateTime desc ";
        this.Pager1.className = "dbCenter";
        this.Pager1.sql = sql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            string FId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            //提交状态
            if (FState != "0")
            {
                e.Item.Cells[4].Text = "<font color='green'>已提交</font>";
                if (FState == "6")
                    e.Item.Cells[4].Text = "<font color='green'>已确认</font>";
            }
            else
            {
                e.Item.Cells[4].Text = "<font color='#88888'>未提交</font>";
                e.Item.Cells[5].Text = "<font color='#88888'>--</font>";
            }
            LinkButton btnOp = (LinkButton)e.Item.FindControl("btnOp");

            //状态
            string s = "", r = "";
            switch (FState)
            {
                case "0"://未上报                        
                    s = "<font color='#444444'>未提交</font>";
                    r = "<font color='#444444'>未提交</font>";
                    btnOp.Text = "删除";
                    btnOp.CommandName = "Del";
                    btnOp.Attributes.Add("onclick", "return confirm('确认要删除该业务吗?');");
                    break;
                case "1"://已上报
                    string sql = "select top 1 FMeasure,FTypeId from CF_App_ProcessRecord WHERE FLinkId='" + FId + "' order by FReportTime desc ";
                    DataTable dt = rc.GetTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["FMeasure"].ToString() == "4" || dt.Rows[0]["FMeasure"].ToString() == "3")
                        {
                            s = "<font color='red'>打回</font>";
                            r = "<font color='red'>退件</font>";
                            btnOp.Text = "撤消";
                            btnOp.CommandName = "Back";
                            btnOp.ToolTip = "撤消";
                            btnOp.Attributes["onclick"] = "return confirm('确定要撤消吗？')";
                        }
                        else if (dt.Rows[0]["FTypeId"].ToString() != "1")
                        {
                            s = "<font color='blue'>已上报</font>";
                            r = "<font color='#444444'>审批中</font>";
                            btnOp.Text = "--";
                            btnOp.CommandName = "";
                            btnOp.Attributes["onclick"] = "return false;";
                        }
                        else
                        {
                            s = "<font color='blue'>已上报</font>";
                            r = "<font color='#444444'>还未办理</font>";
                            btnOp.Text = "撤消上报";
                            btnOp.CommandName = "Back";
                            btnOp.Attributes.Add("onclick", "return confirm('确认要撤消上报吗?');");
                        }
                    }
                    break;
                case "2"://被退回
                    s = "<font color='red'>退回</font>";
                    r = "<font color='red'>退回</font>";
                    btnOp.Text = "撤消";
                    btnOp.CommandName = "Back";
                    btnOp.ToolTip = "撤消";
                    btnOp.Attributes["onclick"] = "return confirm('确定要撤消吗？')";
                    break;
                case "6"://已办结
                    sql = "select FResult from CF_App_ProcessInstanceBackup WHERE FLinkId='" + FId + "' ";
                    dt = rc.GetTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["FResult"].ToString() == "3")
                            r = "<font color='red'>不予受理</font>";
                        else if (dt.Rows[0]["FResult"].ToString() == "1")
                            r = "<font color='green'>同意</font>";
                    }
                    s = "<font color='green'>已办理</font>";
                    btnOp.Text = "--";
                    btnOp.CommandName = "";
                    btnOp.Attributes["onclick"] = "return false;";
                    break;
            }
            e.Item.Cells[4].Text = s; e.Item.Cells[6].Text = r;
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            if (e.CommandName == "See")
            {
                string fAppId = FId;
                string GC_Id = e.Item.Cells[e.Item.Cells.Count - 1].Text;
                // this.Session["YJS_fAppId"] = FId;
                //this.Session["FManageTypeId"] = fMType;
                int fState = EConvert.ToInt(e.CommandArgument);
                if (fState != 0)
                    Session["FIsApprove"] = 1;
                else
                    Session["FIsApprove"] = 0;
                //  string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
                // Session["YJS_GUID"] = e.Item.Cells[e.Item.Cells.Count - 1].Text;
                var param = "?GC_Id=" + GC_Id + "&fAppId=" + fAppId;
                string projectType = e.Item.Cells[e.Item.Cells.Count - 3].Text;
                if (projectType == "1")
                    Response.Write("<script language='javascript'>parent.parent.document.location='AppMain/aIndex.aspx" + param + "';</script>");
                else if (projectType == "2")
                    Response.Write("<script language='javascript'>parent.parent.document.location='../ApplyGCGHSZ/AppMain/aIndex.aspx" + param + "';</script>");
                //Response.Write("<script language='javascript'>parent.parent.document.location='AppMain/aIndex.aspx" + param + "';</script>");
            }
            if (e.CommandName == "Back")
            {
                //撤销上报               
                pageTool tool = new pageTool(this.Page);
                if (!string.IsNullOrEmpty(FId))
                {
                    RQuali rq = new RQuali();
                    //rq.CancelApply(FId);
                    bool success = rq.CancelApply(FId);
                    if (success)
                    {
                        string sql = string.Format("delete from YW_FILE_REMARK where YWBM='{0}'", FId);//删除附件备注
                        sql += "update CF_App_List set FResult='' where Fid='" + FId + "';";
                        rc.PExcute(sql);
                    }
                    tool.showMessage("撤消成功");
                    ShowInfo();
                }
                else
                {
                    tool.showMessage("撤消失败");
                }
            }
            else if (e.CommandName == "Del")
            {
                //删除申报信息和企业数据
                StringBuilder sb = new StringBuilder();
                sb.Append("delete from cf_App_list where FId='" + FId + "';");
                sb.Append("delete from YW_GCGH where YWBM='" + FId + "';");
                sb.Append("delete from YW_FILE_REMARK where YWBM='" + FId + "';");
                sb.Append("delete from YW_FILE_DETAIL where YWBM='" + FId + "';");
                //sb.Append("delete YW_GF_GJJS where YWBM='" + FId + "';");
                //sb.Append("delete YW_GF_JBQK where YWBM='" + FId + "';");
                //sb.Append("delete YW_GF_JS where YWBM='" + FId + "';");
                //sb.Append("delete YW_GF_YYGC where YWBM='" + FId + "';");
                //sb.Append("delete YW_GF_ZLQK where YWBM='" + FId + "';");
                //sb.Append("delete YW_GF_ZYWCDW where YWBM='" + FId + "';");
                //sb.Append("delete YW_GF_ZYWCR where YWBM='" + FId + "';");
                //sb.Append("delete YW_GF_FileState where FAppid='" + FId + "'");
                //sb.Append("delete YW_GF_FileList where FAppid='" + FId + "'");
                rc.PExcute(sb.ToString());
                pageTool tool = new pageTool(this.Page);
                ShowInfo();
                tool.showMessage("删除成功！");
            }
        }
    }
}