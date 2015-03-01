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
using Approve.EntitySys;
using Approve.RuleApp;
using ProjectData;
using System.Linq;
using ProjectBLL;
public partial class JSDW_main_AppDesign : System.Web.UI.Page
{
    RApp ra = new RApp();
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    public int fMType = 294;//初步设计文件审查申报
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowBtnName();
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


    private void ShowBtnName()
    {
        btnPup.Text = "新增" + rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber=" + fMType + "");
    }

    private void ShowInfo()
    {
        var App = db.CF_App_List.Where(t => t.FBaseinfoId == CurrentEntUser.EntId
            && t.FManageTypeId == fMType)
            .OrderByDescending(t => t.FCreateTime).
            Select(t => new
            {
                FId = t.FId,
                t.FPrjId,
                t.FLinkId,
                FPrjName = db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FPrjName).FirstOrDefault(),
                FYear = t.FYear,
                FwriteDate = t.FReportDate,
                FState = t.FState,
                FName = t.FName,

                //判断是不是最新的那条
                isOld = db.CF_App_List.Where(f => t.FPrjId == f.FPrjId && f.FManageTypeId == fMType).Max(f => f.FCreateTime).GetValueOrDefault() > t.FCreateTime
            });
        if (!string.IsNullOrEmpty(ttFPrjName.Text.Trim()))
            App = App.Where(t => t.FPrjName.Contains(ttFPrjName.Text.Trim()));
        if (!string.IsNullOrEmpty(ddlFState.SelectedValue))
            App = App.Where(t => t.FState.ToString() == ddlFState.SelectedValue);
        if (!string.IsNullOrEmpty(drop_FYear.SelectedValue))
            App = App.Where(t => t.FYear.ToString() == drop_FYear.SelectedValue);

        Pager1.RecordCount = App.Count();
        DG_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            int fState = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FState"));
            string FPrjName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjName"));
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            LinkButton lb = (LinkButton)e.Item.FindControl("btnOp");
            LinkButton btnRe = ((LinkButton)e.Item.FindControl("btnRe"));
            string s = "", r = "";

            bool isReApp = false;//需要重新办理的
            switch (fState)
            {
                case 0:
                    s = "<font color='#444444'>未上报</font>";
                    r = "/";
                    lb.Attributes.Add("onclick", "return confirm('确认要删除该业务吗?');");
                    break;
                case 1:
                    s = "<font color='blue'>已上报</font>";
                    r = "<font color='#444444'>还未办理</font>";
                    lb.Text = "撤消上报";
                    lb.CommandName = "Back";
                    lb.Attributes.Add("onclick", "return confirm('确认要撤消上报吗?');");
                    break;
                case 2:
                    s = "<font color='red'>被退回</font>";
                    r = "<font color='red' style='cursor:hand;' onclick=\"showApproveWindow('ShowAppInfo.aspx?fid=" + FID + "',634,500)\">退回</font></a>";
                    lb.Attributes.Add("onclick", "return confirm('确认要删除该业务吗?');");
                    break;
                case 6:
                    s = "<font color='green'>已办结</font>";
                    //办理意见
                    var v = (from t in db.CF_App_Idea
                             where t.FLinkId == FID
                             orderby t.FCreateTime descending
                             select new { t.FResult, t.FResultInt }).FirstOrDefault();
                    if (v != null)
                    {
                        if (v.FResultInt == 1)//同意备案（管理部门审核）
                        {
                            r += "<font color='green'>" + v.FResult + "</font>";
                            lb.Text = "--";
                            lb.CommandName = "";
                            lb.Attributes["onclick"] = "return false;";
                        }
                        else if (v.FResultInt == 3)//不同意（管理部门审核）
                        {
                            r += "<font color='red'>" + v.FResult + "</font>";
                            isReApp = true;
                        }
                    }

                    break;
            }
            //状态
            e.Item.Cells[2].Text = s;

            //办理结果
            e.Item.Cells[4].Text = r;

            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            //需要重新办理合同备案申请的。
            if (isReApp)
            {
                bool isOld = EConvert.ToBool(DataBinder.Eval(e.Item.DataItem, "isOld"));
                if (!isOld)
                {
                    lb.Text = "重办初步文件审查";
                    lb.CommandName = "ReApp";
                    lb.CommandArgument = FPrjId + "," + FPrjName;
                    lb.Attributes["onclick"] = "";
                    lb.Visible = true;

                    btnRe.Text = "重办初步设计合同备案";
                    btnRe.CommandName = "ReAppSJ";
                    btnRe.CommandArgument = FPrjId + "," + FLinkId;
                    btnRe.Attributes["onclick"] = "";
                    btnRe.Visible = true;
                }
                else
                {
                    lb.Text = "--";
                    lb.CommandName = "";
                    lb.Attributes["onclick"] = "return false;";
                }
            }
            //查询项目的变更时间
            var prjBG = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId)
                .Select(t => new { t.FIsBG, t.FBGTime, t.FCount }).FirstOrDefault();
            if (prjBG.FCount > 0)
            {
                ((Literal)e.Item.FindControl("prj_Count")).Text = ("<br/>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")");
            }
            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Count(t => t.FPrjId == FPrjId) > 0;
            if (stop)
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Item.ToolTip = "该项目已被中止，所有业务停止进行。";
                btnRe.Enabled = lb.Enabled = false;
                lb.Attributes["onclick"] = "return false;";
                // btnRe.Visible = false;
            }
            else if (prjBG.FIsBG == 1)//判断该项目是否变更
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Item.ToolTip = "该项目已经做了变更，变更前的业务停止进行。";
                btnRe.Enabled = lb.Enabled = false;
                lb.Attributes["onclick"] = "return false;";
                //lb.Visible = btnRe.Visible = false;
            }
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
                if (fState != 0 && fState != 2)
                    Session["FIsApprove"] = 1;
                else
                    Session["FIsApprove"] = 0;
                Response.Write("<script language='javascript'>parent.parent.document.location='../Appmain/aindex.aspx';</script>");
            }
            if (e.CommandName == "Back")
            {

                //撤销上报
                RQuali rq = new RQuali();
                rq.CancelApply(FId);
                pageTool tool = new pageTool(this.Page);
                ShowInfo();
                tool.showMessage("撤消成功！");
            }
            else if (e.CommandName == "Delete")
            {
                //删除申报信息和企业数据
                StringBuilder sb = new StringBuilder();
                sb.Append("delete cf_App_list where FId='" + FId + "';");
                sb.Append("delete cf_Prj_data where FAppId='" + FId + "';");
                sb.Append("delete cf_Prj_Ent where fappId='" + FId + "';");
                rc.PExcute(sb.ToString());
                pageTool tool = new pageTool(this.Page);
                ShowInfo();
                tool.showMessage("删除成功！");
            }
            else if (e.CommandName == "ReApp") //重新办理
            {
                string[] s = e.CommandArgument.ToString().Split(',');
                if (s.Length == 2)
                {
                    string FPrjId = s[0];
                    string FPrjName = s[1];

                    HPid.Value = FPrjId;
                    txtFPrjName.Text = FPrjName;
                    appTab.Visible = false;
                    applyInfo.Visible = true;
                    ViewState["FMNUMBER"] = fMType;
                    t_FYear.Text = DateTime.Now.Year.ToString();
                    t_FName.Text = t_FYear.Text + "年 " + db.getManageTypeName(fMType);
                }

            }
            else if (e.CommandName == "ReAppSJ") //重办初步设计
            {
                string[] s = e.CommandArgument.ToString().Split(',');
                if (s.Length == 2)
                {
                    string FPrjId = s[0];
                    string FLinkId = s[1];
                    Response.Redirect("AppCBSJWT.aspx?FPrjId=" + FPrjId + "&FLinkId=" + FLinkId);//到勘察合同备案页面重做勘察(2次勘察)
                }
            }
        }
    }

    private void SaveInfo(string fmtnumber)
    {
        pageTool tool = new pageTool(this.Page);
        if (this.Session["FBaseId"] == null)
            return;
        //查询如果已经有了，则不再创建
        if (string.IsNullOrEmpty(HPid.Value))
        {
            tool.showMessage("请先选择项目！");
            return;
        }
        //查询如果有未办结(不包含退回的)，则不可在进行填写
        if (db.CF_App_List.Count(l => l.FPrjId == HPid.Value && l.FManageTypeId == fMType && l.FIsDeleted != true && l.FState <= 1) > 0)
        {
            tool.showMessage("该项目正在办理初步设计文件审查申报业务！");
            return;
        }
        string fPrjDataId = Guid.NewGuid().ToString();
        string fAppId = Guid.NewGuid().ToString();
        CF_App_List lKC = new CF_App_List();//设计企业业务
        lKC.FId = fAppId;
        lKC.FLinkId = fPrjDataId;
        lKC.FBaseinfoId = CurrentEntUser.EntId;
        lKC.FPrjId = HPid.Value;
        lKC.FName = t_FName.Text.Trim();
        lKC.FManageTypeId = fMType;
        lKC.FwriteDate = DateTime.Now;
        lKC.FState = 0;
        lKC.FIsDeleted = false;
        lKC.FYear = EConvert.ToInt(t_FYear.Text.Trim());
        lKC.FMonth = DateTime.Now.Month;
        lKC.FBaseName = CurrentEntUser.EntName;
        lKC.FTime = DateTime.Now;
        lKC.FCreateTime = DateTime.Now;
        lKC.FReportCount = 1;
        db.CF_App_List.InsertOnSubmit(lKC);

        CF_Prj_Data prjData = new CF_Prj_Data();//数据类型
        prjData.FAppId = fAppId;
        prjData.FId = fPrjDataId;
        prjData.FPrjId = HPid.Value;
        prjData.FPrjName = txtFPrjName.Text.Trim();
        prjData.FBaseInfoId = CurrentEntUser.EntId;
        prjData.FType = fMType;
        prjData.FTime = DateTime.Now;
        prjData.FIsDeleted = false;
        prjData.FCreateTime = DateTime.Now;
        db.CF_Prj_Data.InsertOnSubmit(prjData);
        db.SubmitChanges();
        this.Session["FAppId"] = fAppId;
        this.Session["FManageTypeId"] = fMType;
        this.Session["FIsApprove"] = 0;
        this.RegisterStartupScript(new Guid().ToString(), "<script>parent.parent.document.location='../Appmain/aindex.aspx';</script>");
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        this.appTab.Visible = false;
        this.applyInfo.Visible = true;
        this.ViewState["FMNUMBER"] = fMType;
        t_FYear.Text = DateTime.Now.Year.ToString();
        this.t_FName.Text = t_FYear.Text + "年 " + rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + fMType + "'");
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (this.ViewState["FMNUMBER"] == null)
        {
            return;
        }
        pageTool tool = new pageTool(this.Page);
        SaveInfo(this.ViewState["FMNUMBER"].ToString());        
    }

    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        this.appTab.Visible = true;
        this.applyInfo.Visible = false;
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(HPid.Value))
            txtFPrjName.Text = rc.GetSignValue("select FPrjName from cf_Prj_Baseinfo where fid='" + HPid.Value + "'");
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
