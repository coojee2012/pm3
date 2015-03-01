using Approve.EntityBase;
using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tools;

public partial class WYDW_AppMain_ProjectSB : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    public ProjectDB db = new ProjectDB();
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
        //btnZGXM.Text = "新增" + rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber=14401");
        //btnXMSQ.Text = "新增" + rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber=14402");
        for (int i = DateTime.Now.Year; i >= 2013; i--)
        {
            drop_FYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        drop_FYear.Items.Insert(0, new ListItem("--全部--", ""));
    }

    public void showInfo()
    {
        if (Request.QueryString["ManageTypeID"] != null)
        {
            string strManageTypeID = Request.QueryString["ManageTypeID"];

            string strManageType = rc.GetSignValue("Select FName From CF_Sys_ManageType Where FNumber = '" + strManageTypeID + "'");

            lblManageType.Text = strManageType;
            btnAdd.Text = "新增" +strManageType;
            btnAdd.Attributes.Add("onclick", "ShowAppPage('AppAdd.aspx?ManageType=" + strManageTypeID + "',600,140,event);");

            string FBaseinfoID = CurrentEntUser.EntId;
            string sql = "select l.*,j.XMMC,j.XMBH from CF_App_List l left join YW_WY_XM_JBXX j on l.FID=j.FAppID " +
                         "where l.FBaseinfoId='"
                + FBaseinfoID + "'and l.FCreateUser = '" + CurrentEntUser.UserId + "'" + "and FManageTypeId='" + strManageTypeID + "'";

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
        else
            btnAdd.Visible = false;

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        //        pageTool tool = new pageTool(this.Page);
        //        int cou = int.Parse(rc.GetSignValue("select count(1) from CF_App_List where FCreateUser='" + CurrentEntUser.UserId + "'"));
        //        if (cou <= 0)
        //        {
        //            string sqlt = string.Format(@"select top 1 count(1)
        //                from YW_CF_DICtime where convert(nvarchar(10),GETDATE(),121) >= convert(nvarchar(10),FStime,121) 
        //                and convert(nvarchar(10),GETDATE(),121) <= convert(nvarchar(10),FEtime,121)  ");
        //            int cout = int.Parse(rc.GetSignValue(sqlt));
        //            if (cout <= 0)
        //            {

        //                tool.showMessage("未到工法填报时间，请稍后再试！"); return;
        //            }
        //            this.appTab.Visible = false;
        //            this.applyInfo.Visible = true;
        //            this.ViewState["FMNUMBER"] = fMType;
        //            string sqlY = string.Format(@"select top 1 FYear
        //                from YW_CF_DICtime where convert(nvarchar(10),GETDATE(),121) >= convert(nvarchar(10),FStime,121) 
        //                and convert(nvarchar(10),GETDATE(),121) <= convert(nvarchar(10),FEtime,121)  ");
        //            DataTable dt = rc.GetTable(sqlY);
        //            if (dt != null && dt.Rows.Count > 0)
        //            { t_FYear.Text = dt.Rows[0]["FYear"].ToString(); }
        //            this.t_FName.Text = t_FYear.Text + " " + rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + fMType + "'");
        //            string sql = "select FRESON from CF_Sys_User where FID = '" + CurrentEntUser.UserId + "' ";
        //            dt = rc.GetTable(sql);
        //            if (dt != null && dt.Rows.Count > 0)
        //            { txtFPrjName.Text = dt.Rows[0]["FRESON"].ToString(); }
        //        }
        //        else { tool.showMessage("此卡已有工法业务，如还需申报下一个工法，请重新领卡！"); }
    }

    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        this.appTab.Visible = true;
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(fMType.ToString()))
        //{ return; }
        //pageTool tool = new pageTool(this.Page);
        //if (this.Session["FBaseId"] == null)
        //    return;

        //string fPrjDataId = Guid.NewGuid().ToString();
        //string fAppId = Guid.NewGuid().ToString();
        //string FPrjId = Guid.NewGuid().ToString();
        //CF_App_List lKC = new CF_App_List();//设计企业业务
        //lKC.FId = fAppId;
        //lKC.FLinkId = fPrjDataId;
        //lKC.FBaseinfoId = CurrentEntUser.EntId;
        //lKC.FPrjId = FPrjId;
        //lKC.FName = t_FName.Text.Trim();
        //lKC.FManageTypeId = fMType;
        //lKC.FwriteDate = DateTime.Now;
        //lKC.FState = 0;
        //lKC.FIsDeleted = false;
        //lKC.FYear = EConvert.ToInt(t_FYear.Text.Trim());
        //lKC.FMonth = DateTime.Now.Month;
        //lKC.FBaseName = CurrentEntUser.EntName;
        //lKC.FTime = DateTime.Now;
        //lKC.FCreateTime = DateTime.Now;
        //lKC.FReportCount = 1;
        //db.CF_App_List.InsertOnSubmit(lKC);
        //db.SubmitChanges();
        //string sql = "exec JKC_PRO_insertGF '" + fAppId + "','" + CurrentEntUser.EntId + "','"
        //    + txtFPrjName.Text.Trim() + "','" + CurrentEntUser.UserId + "' ";
        //sh.PExcute(sql);
        //this.Session["FAppId"] = fAppId;
        //this.Session["FManageTypeId"] = fMType;
        //this.Session["FIsApprove"] = 0;
        //this.RegisterStartupScript(new Guid().ToString(), "<script>parent.parent.document.location='../AppMain/aIndex.aspx';</script>");
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
                //e.Item.Cells[3].Text = "<font color='green'>已提交</font>";
                //if (FState == "6")
                //    e.Item.Cells[3].Text = "<font color='green'>已确认</font>";
            }
            else
            {
                e.Item.Cells[3].Text = "<font color='#88888'>未提交</font>";
                e.Item.Cells[4].Text = "<font color='#88888'>--</font>";
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
                            btnOp.Text = "撤消上报";
                            btnOp.CommandName = "Back";
                            btnOp.Attributes.Add("onclick", "return confirm('确认要撤消上报吗?');");
                            //e.Item.Cells[7].Text = "";
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
                    e.Item.Cells[7].Text = "";
                    break;
            }
            e.Item.Cells[4].Text = s;
            e.Item.Cells[5].Text = r;
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FId = e.Item.Cells[9].Text;
            int fState = EConvert.ToInt(e.Item.Cells[8].Text);
            string strXMBH = e.Item.Cells[11].Text;
            if (e.CommandName == "See")
            {
                Session["GovLinkID"] = "2";
                this.Session["XMBH"] = strXMBH;
                this.Session["FAppId"] = FId;
                this.Session["FManageTypeId"] = e.Item.Cells[10].Text;
                if (fState != 0 && fState != 2)
                    Session["FIsApprove"] = 1;
                else
                    Session["FIsApprove"] = 0;

                Response.Write("<script language='javascript'>parent.parent.document.location='../AppMain/aIndex.aspx';</script>");

            }
            if (e.CommandName == "Back")
            {
                //撤销上报               
                pageTool tool = new pageTool(this.Page);
                if (!string.IsNullOrEmpty(FId))
                {
                    RQuali rq = new RQuali();
                    rq.CancelApply(FId);
                    tool.showMessage("撤消成功");
                    showInfo();
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
                sb.Append("delete cf_App_list where FId='" + FId + "';");

                sb.Append("delete YW_WY_XM_JBXX where FAppid='" + FId + "'");
                rc.PExcute(sb.ToString(), true);
                pageTool tool = new pageTool(this.Page);
                showInfo();
                tool.showMessage("删除成功！");
            }
        }
    }
    
}