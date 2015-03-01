using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using System.Data.SqlClient;
using ProjectData;

public partial class GFEnt_main_queryGF : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    public int fMType = 4000; ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            conBind();
            showInfo();
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
    public void showInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        string sql = "select l.*,j.GFMC from CF_App_List l left join YW_GF_JBQK j on l.FID=j.YWBM where l.FBaseinfoId='"
            + FBaseinfoID + "' and l.FManageTypeId=4000 ";
        if (!string.IsNullOrEmpty(t_YWFname.Text.Trim()))
        {
            sql += " and l.FName like '%" + t_YWFname.Text.Trim() + "%' ";
        }
        if (!string.IsNullOrEmpty(ddlFState.SelectedValue.Trim()))
        {
            sql += " and l.FState = '" + ddlFState.SelectedValue.Trim() + "' ";
        }
        if (!string.IsNullOrEmpty(drop_FYear.SelectedValue.Trim()))
        {
            sql += " and l.FYear = '" + drop_FYear.SelectedValue.Trim() + "' ";
        }
        sql += " order by l.FCreateTime desc ";

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
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
                e.Item.Cells[3].Text = "<font color='green'>已提交</font>";
                if (FState == "6")
                    e.Item.Cells[3].Text = "<font color='green'>已确认</font>";
            }
            else
            {
                e.Item.Cells[3].Text = "<font color='#88888'>未提交</font>";
                e.Item.Cells[4].Text = "<font color='#88888'>--</font>";
            }

            //状态
            string s = "", r = "";
            switch (FState)
            {
                case "0"://未上报                        
                    s = "<font color='#444444'>未提交</font>";
                    r = "<font color='#444444'>未提交</font>";
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
                        }
                        else if (dt.Rows[0]["FTypeId"].ToString() != "1")
                        {
                            s = "<font color='blue'>已上报</font>";
                            r = "<font color='#444444'>审批中</font>";
                        }
                        else
                        {
                            s = "<font color='blue'>已上报</font>";
                            r = "<font color='#444444'>还未办理</font>";
                        }
                    }
                    break;
                case "2"://被退回
                    s = "<font color='red'>退回</font>";
                    r = "<font color='red'>退回</font>";
                    break;
                case "6"://已办结
                    s = "<font color='green'>已办理</font>";
                    sql = "select FResult from CF_App_ProcessInstanceBackup WHERE FLinkId='" + FId + "' ";
                    dt = rc.GetTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["FResult"].ToString() == "3")
                            r = "<font color='red'>不予受理</font>";
                        else if (dt.Rows[0]["FResult"].ToString() == "1")
                            r = "<font color='green'>同意</font>"; 
                    }
                    e.Item.Cells[8].Text = "";
                    break;
            }
            e.Item.Cells[3].Text = s; e.Item.Cells[5].Text = r;
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            int fState = EConvert.ToInt(e.Item.Cells[e.Item.Cells.Count - 2].Text);
            if (e.CommandName == "See")
            {
                this.Session["FAppId"] = FId;
                this.Session["FManageTypeId"] = fMType;
                Session["FIsApprove"] = 1;
                Response.Write("<script language='javascript'>parent.parent.document.location='../AppMain/aIndex.aspx';</script>");
            }
        }
    }

}