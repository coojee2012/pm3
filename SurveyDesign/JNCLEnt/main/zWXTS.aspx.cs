using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ProjectBLL;
using ProjectData;
using Tools;
using System.Text;
using Approve.RuleCenter;
using Approve.RuleApp;
using System.Web.UI.HtmlControls;

public partial class GFEnt_main_zWXTS : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["LookMSG"] = "NOREAD";//只看没读的消息
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
    //显示
    private void showInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        if (!string.IsNullOrEmpty(FBaseinfoID))
        {
            lit_EntName.Text = CurrentEntUser.EntName;
            lit_TS.Text = "欢迎登录四川省工程建设工法管理信息系统，我们将竭诚为您服务，请及时更新系统信息。";

            //显示系统消息
            showMSG();


            //显示业务
            showAppList();
        }
    }
    #region 显示系统消息
    //显示系统消息
    private void showMSG()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        ProjectDB db = new ProjectDB();

        IQueryable<CF_Sys_Message> v = db.CF_Sys_Message.Where(t => t.FAccept == FBaseinfoID).OrderByDescending(t => t.FIsRead).OrderByDescending(t => t.FSendTime);
        lit_MSGCount.Text = v.Count().ToString();
        lit_MSGNoRead.Text = v.Count(t => t.FIsRead == 0).ToString();
        btnAllRead.Visible = v.Count(t => t.FIsRead == 0) > 0;
        v = (v.Where(t => (EConvert.ToString(ViewState["LookMSG"]) == "NOREAD" ? t.FIsRead == 0 : true)));


        StringBuilder sb = new StringBuilder();
        sb.Append("<marquee onmouseout='this.start()' onmouseover='this.stop()' scrollamount='2' scrolldelay='10' direction=left  ");
        sb.Append("width='98%' height='32px'>");
        foreach (CF_Sys_Message msg in v.Take(10))
        {
            sb.Append("   " + msg.FText);
        }
        sb.Append("</marquee>");

        this.xtxx.Text = sb.ToString();
    }
    //全部标记为已读
    protected void btnAllRead_Click(object sender, EventArgs e)
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        ProjectDB db = new ProjectDB();
        IQueryable<CF_Sys_Message> msg = db.CF_Sys_Message.Where(t => t.FAccept == FBaseinfoID && t.FIsRead == 0);
        foreach (CF_Sys_Message t in msg)
        {
            t.FIsRead = 1;
        }
        db.SubmitChanges();
        showMSG();
    }
    //查看全部
    protected void btnAll_Click(object sender, EventArgs e)
    {
        ViewState["LookMSG"] = "ALL";//查看全部
        showMSG();
    }
    //查看未读
    protected void btnNoRead_Click(object sender, EventArgs e)
    {
        ViewState["LookMSG"] = "NOREAD";//查看未读
        showMSG();
    }
    #endregion

    #region 显示业务
    //显示业务
    private void showAppList()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        string sql = string.Format(@"select l.*,j.GFMC,j.FListName,j.FTypeName
            from CF_App_List l 
            left join YW_GF_JBQK j on l.FID=j.YWBM 
            where l.FBaseinfoId='" + FBaseinfoID + "'and l.FCreateUser = '" + CurrentEntUser.UserId + "' ");
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "App_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();

    }
    //列表
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            ProjectDB db = new ProjectDB();
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FBaseinfoId = CurrentEntUser.EntId;//当前用户BaseinfoId  
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));//业务状态
            string FManageTypeId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageTypeId"));//业务类型
            string FId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            //操作按钮
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
                    btnOp.ToolTip = "删除业务";
                    btnOp.Attributes["onclick"] = "return confirm('确定要删除该业务吗？')";
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
                            e.Item.Cells[8].Text = "";
                        }
                        else
                        {
                            s = "<font color='blue'>已上报</font>";
                            r = "<font color='#444444'>还未办理</font>";
                            btnOp.Text = "撤消上报";
                            btnOp.CommandName = "Back";
                            string sqlt = string.Format(@"select count(1)
                    from YW_CF_DICtime where convert(nvarchar(10),GETDATE(),121) >= convert(nvarchar(10),FStime,121) 
                    and convert(nvarchar(10),GETDATE(),121) <= convert(nvarchar(10),FEtime,121)  ");
                            int cout = int.Parse(rc.GetSignValue(sqlt));
                            if (cout <= 0)
                            { btnOp.Attributes["onclick"] = "alert('工法申报时间已过，不能撤销!');return false;"; }
                            else { btnOp.Attributes["onclick"] = "return confirm('确定要撤消吗？')"; }

                        }
                    }
                    break;
                case "2"://被退回
                    s = "<font color='red'>退回</font>";
                    r = "<font color='red'>退回</font>";
                    btnOp.Text = "撤消";
                    btnOp.CommandName = "Back";
                    btnOp.ToolTip = "撤消";
                    btnOp.Attributes.Add("onclick", "return confirm('确认要撤消上报吗?');");
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
                    btnOp.Text = "--";
                    btnOp.CommandName = "";
                    btnOp.Attributes["onclick"] = "return false;";
                    e.Item.Cells[8].Text = "";
                    break;
            }
            //状态
            e.Item.Cells[6].Text = s;
            //办理结果
            e.Item.Cells[7].Text = r;

        }
    }

    //列表事件
    protected void App_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string FId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
        int fState = EConvert.ToInt(e.Item.Cells[e.Item.Cells.Count - 3].Text);
        string fMType = e.Item.Cells[e.Item.Cells.Count - 2].Text;
        if (e.CommandName == "See") //打开业务
        {
            this.Session["FAppId"] = FId;
            this.Session["FManageTypeId"] = fMType;
            if (fState != 0 && fState != 2)
                Session["FIsApprove"] = 1;
            else
                Session["FIsApprove"] = 0;
            Response.Write("<script language='javascript'>parent.parent.document.location='../AppMain/aIndex.aspx';</script>");
        }
        else if (e.CommandName == "Del") //删除业务
        {
            //删除申报信息和企业数据
            StringBuilder sb = new StringBuilder();
            sb.Append("delete cf_App_list where FId='" + FId + "';");
            sb.Append("delete YW_GF_JBQK where YWBM='" + FId + "';");
            sb.Append("delete YW_GF_GJJS where YWBM='" + FId + "';");
            sb.Append("delete YW_GF_JBQK where YWBM='" + FId + "';");
            sb.Append("delete YW_GF_JS where YWBM='" + FId + "';");
            sb.Append("delete YW_GF_YYGC where YWBM='" + FId + "';");
            sb.Append("delete YW_GF_ZLQK where YWBM='" + FId + "';");
            sb.Append("delete YW_GF_ZYWCDW where YWBM='" + FId + "';");
            sb.Append("delete YW_GF_ZYWCR where YWBM='" + FId + "';");
            sb.Append("delete YW_GF_FileState where FAppid='" + FId + "'");
            sb.Append("delete YW_GF_FileList where FAppid='" + FId + "'");
            rc.PExcute(sb.ToString());
            pageTool tool = new pageTool(this.Page);
            tool.showMessage("删除成功！"); showInfo();
        }
        else if (e.CommandName == "Back") //撤消提交到其他企业的业务
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
    }


    //年度选择事件
    protected void drop_FYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //显示业务
        showAppList();
    }
    #endregion

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }

}