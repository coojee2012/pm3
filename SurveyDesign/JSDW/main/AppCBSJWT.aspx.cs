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
public partial class EvaluateEntApp_main_AppCBSJWT : System.Web.UI.Page
{
    RApp ra = new RApp();
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    public int fMType = 291;//初步设计合同备案
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            conBind();
            ShowInfo();
        }
    }

    //绑定选项
    private void conBind()
    {
        btnPup.Text = "新增" + rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber=" + fMType + "");
        for (int i = DateTime.Now.Year; i >= 2013; i--)
        {
            drop_FYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        drop_FYear.Items.Insert(0, new ListItem("--全部--", ""));



        pageTool tool = new pageTool(this.Page);
        //审查不通过时可做二次勘察（从审查业务页面转过来）
        string FPrjId = Request.QueryString["FPrjId"];
        if (!string.IsNullOrEmpty(FPrjId))
        {
            //先查出最后一次勘察，并查出做过几次了。
            var v = (from t in db.CF_App_List
                     join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                     where t.FPrjId == FPrjId && t.FManageTypeId == fMType
                     orderby t.FCreateTime descending
                     select new { t.FId, t.FCount, t.FState, t.FLinkId, d.FPrjName }).FirstOrDefault();
            if (v != null)
            {
                //查出成果移交业务
                var s = db.CF_App_List.Where(t => t.FLinkId == v.FLinkId && t.FManageTypeId == fMType).Select(t => t.FState).FirstOrDefault();

                if (s != null && s == 6 && v.FState == 6)
                { //只都为6时才可以做二次初步设计
                    appTab.Visible = false;
                    applyInfo.Visible = true;
                    ViewState["FMNUMBER"] = fMType;
                    t_FYear.Text = DateTime.Now.Year.ToString();
                    t_FName.Text = t_FYear.Text + "年 " + db.getManageTypeName(fMType);
                    t_FPrjId.Value = FPrjId;
                    txtFPrjName.Text = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId).Select(t => t.FPrjName).FirstOrDefault();
                    t_OldFAppId.Value = v.FId;
                    t_FCount.Value = (v.FCount.GetValueOrDefault() + 1).ToString();
                    t_FPriItemId.Value = Request.QueryString["FLinkId"];//保存审查不合格那条审查业务的CF_Prj_Data.FID
                }
                else
                { //其它情况都是正在办理的

                    //查出来。 
                    ttFPrjName.Text = v.FPrjName;
                    ShowInfo();

                    tool.ExecuteScript("if (confirm('该项目正在做勘察。\\n\\r\\n\\r确定：查询该项目勘察情况\\n\\r取消：返回勘察文件审查合同列表')){}else{history.back();}");
                }
            }
        }
    }

    private void ShowInfo()
    {
        var v = (from a in db.CF_App_List
                 join d in db.CF_Prj_Data on a.FLinkId equals d.FId
                 where a.FBaseinfoId == CurrentEntUser.EntId && a.FManageTypeId == fMType
                 orderby a.FCreateTime descending
                 select new
                 {
                     a.FId,
                     DataFID = d.FId,
                     d.FPrjName,
                     d.FPriItemId,
                     a.FYear,
                     a.FCount,
                     a.FReportDate,
                     a.FState,
                     a.FName,
                     a.FLinkId,
                     a.FAppDate,
                     a.FPrjId,
                     a.FReportCount,
                     ToEntName = db.CF_Prj_Ent.Where(t => t.FAppId == a.FId && t.FEntType == 155).Select(t => t.FName).FirstOrDefault(),
                     //判断是不是最新的那条
                     isOld = db.CF_App_List.Where(t => t.FPrjId == a.FPrjId && t.FManageTypeId == fMType).Max(t => t.FCreateTime).GetValueOrDefault() > a.FCreateTime,

                 });
        if (!string.IsNullOrEmpty(ttFPrjName.Text.Trim()))
            v = v.Where(t => t.FPrjName.Contains(ttFPrjName.Text.Trim()));
        if (!string.IsNullOrEmpty(ddlFState.SelectedValue))
        {
            if (ddlFState.SelectedValue == "1")
            {
                v = v.Where(t => t.FState != 0 && t.FState != 6);
            }
            else
            {
                v = v.Where(t => t.FState.ToString() == ddlFState.SelectedValue);
            }

        }
        if (!string.IsNullOrEmpty(drop_FYear.SelectedValue))
            v = v.Where(t => t.FYear.ToString() == drop_FYear.SelectedValue);
        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();

            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            DateTime FAppDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FAppDate"));//受理时间
            string FCount = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FCount"));
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            string FPrjName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjName"));
            string FId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            string ToEntName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ToEntName"));
            string FPriItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPriItemId"));
            
            //操作按钮
            LinkButton btnOp = (LinkButton)e.Item.FindControl("btnOp");
            bool re = false;//是否需要重新办理

            //提交状态
            if (FState != "0")
            {
                e.Item.Cells[2].Text = "<font color='green'>已提交</font>";
                if (FState == "6")
                    e.Item.Cells[2].Text = "<font color='green'>已确认</font>";
            }
            else
            {
                e.Item.Cells[2].Text = "<font color='#88888'>未提交</font>";
                e.Item.Cells[3].Text = "<font color='#88888'>--</font>";
            }

            //合同备案单位
            string s = "合同提交单位：<font color='#378BB0'>" + ToEntName + "</font>";


            //受理状态
            s += "</br>(1)合同确认：";
            switch (FState)
            {
                case "0"://未上报
                    s += "<font color='#888888'>未提交</font>";
                    btnOp.Text = "删除";
                    btnOp.CommandName = "Del";
                    btnOp.Attributes.Add("onclick", "return confirm('确认要删除该业务吗?');");
                    break;
                case "1"://已上报
                    s += "<font color='#888888'>还未办理</font>";
                    btnOp.Text = "撤消合同";
                    btnOp.CommandName = "Back";
                    btnOp.Attributes.Add("onclick", "return confirm('确认要撤消合同吗?');");
                    break;
                case "2"://被退回
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../SJ/applycbsjwt/Report.aspx?FAppId=" + FId + "',600,400);\"><font color='red'>被退回</font> <font color='#666666'>[" + FAppDate.ToString("yyyy-MM-dd") + "]</font></a>";

                    btnOp.Text = "--";
                    btnOp.CommandName = "";
                    btnOp.Attributes.Add("onclick", "return false;");
                    break;
                case "6"://已办结
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../SJ/applycbsjwt/Report.aspx?FAppId=" + FId + "',600,500);\"><font color='green'>已确认</font> <font color='#666666'>[" + FAppDate.ToString("yyyy-MM-dd") + "]</font></a>";

                    btnOp.Text = "--";
                    btnOp.CommandName = "";
                    btnOp.Attributes.Add("onclick", "return false;");
                    break;
                case "7"://不予受理
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../SJ/applycbsjwt/Report.aspx?FAppId=" + FId + "',600,500);\"><font color='red'>不予接受</font> <font color='#666666'>[" + FAppDate.ToString("yyyy-MM-dd") + "]</font></a>";
                    re = true;//需要重新办理
                    break;
            }


            //合同备案同况
            string DataFID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "DataFID"));
            var HTBA = (from dd in db.CF_Prj_Data
                        join tt in db.CF_App_List on dd.FAppId equals tt.FId
                        where dd.FPriItemId == DataFID && tt.FManageTypeId == 414
                        orderby tt.FCreateTime descending
                        select new
                        {
                            tt.FState,
                            //查询备案通过没
                            FResultInt = db.CF_App_Idea.Where(i => i.FLinkId == tt.FId).OrderByDescending(i => i.FCreateTime).Select(i => i.FResultInt).FirstOrDefault(),
                        }).FirstOrDefault();
            string c = "<img title=\"合同备案未完成，无法安排\" src=\"../../image/info.jpg\" style=\"cursor:pointer;\"/>";
            //人员安排
            s += "</br>(2)合同备案：";
            if (HTBA != null)
            {
                if (HTBA.FState == 6)
                {
                    if (HTBA.FResultInt == 1)
                        s += "<font color='green'>已办结，已同意</font>";
                    else
                    {
                        s += "<font color='red'>已办结，未同意</font>";

                        re = true;//需要重新办理
                    }
                }
                else
                {
                    s += "<font color='blue'>正在办理</font>";
                }
            }
            else
                s += "<font color='#888888'>未开始</font>";

            bool isOld = EConvert.ToBool(DataBinder.Eval(e.Item.DataItem, "isOld"));
            if (re)//是否需要重新办理
                if (!isOld)
                {
                    btnOp.Text = "重新办理";
                    btnOp.CommandName = "ReApp";
                    btnOp.CommandArgument = FPrjId + "," + FPrjName + "," + FCount + "," + FId + "," + FPriItemId;
                    btnOp.Attributes["onclick"] = "";
                }
                else
                {
                    btnOp.Text = "--";
                    btnOp.CommandName = "";
                    btnOp.Attributes["onclick"] = "return false;";
                }

            //人员安排29201、初步设计人员意见29202、成果移交293 
            var v = (from a in db.CF_App_List
                     where a.FLinkId == FLinkId &&
                     (a.FManageTypeId == 29201 || a.FManageTypeId == 29202
                     || a.FManageTypeId == 293)
                     orderby a.FCreateTime
                     select new
                     {
                         a.FId,
                         a.FState,
                         a.FManageTypeId,
                         a.FAppDate,
                         a.FCreateTime,
                     }).ToList();
            //人员安排
            s += "</br>(3)人员安排：";
            var v29201 = v.Where(t => t.FManageTypeId == 29201).OrderByDescending(t => t.FCreateTime).FirstOrDefault();
            if (v29201 != null)
            {
                if (v29201.FState == 6)
                {
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../SJ/applycbsjwt/PlanPerson.aspx?FAppId=" + v29201.FId + "',700,600);\">";
                    s += "<font color='green'>已经安排</font>";
                    s += " <font color='#666666'>[" + v29201.FAppDate.Value.ToString("yyyy-MM-dd") + "]</font></a>";
                }
                else
                    s += "<font color='blue'>正在安排</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";

            //人员意见
            s += "</br>(4)人员意见：";
            var v29202 = v.Where(t => t.FManageTypeId == 29202).FirstOrDefault();
            if (v29202 != null)
            {
                if (v29202.FState == 6)
                {
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../SJ/applycbsjryyj/Report.aspx?FAppId=" + v29202.FId + "',700,600);\">";
                    s += "<font color='green'>已经办理</font>";
                    s += " <font color='#666666'>[" + v29202.FAppDate.Value.ToString("yyyy-MM-dd") + "]</font></a>";
                }
                else
                    s += "<font color='blue'>正在办理</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";

            //成果移交
            s += "</br>(5)成果移交：";



            var v293 = v.Where(t => t.FManageTypeId == 293).FirstOrDefault();
            if (v293 != null)
            {
                if (v293.FState == 6)
                {
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../SJ/applycbsjcgyj/ApplyBaseInfo.aspx?FAppId=" + v293.FId + "',700,600);\">";
                    s += "<font color='green'>已经移交</font>";
                    s += " <font color='#666666'>[" + v293.FAppDate.Value.ToString("yyyy-MM-dd") + "]</font></a>";
                }
                else
                    s += "<font color='blue'>正在办理</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";



            //办理结果
            e.Item.Cells[4].Text = s;


            //是否二次。
            int n = EConvert.ToInt(FCount);
            if (n > 1)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = ("(" + n + "次)");
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
                btnOp.Enabled = false;
                btnOp.Attributes["onclick"] = "return false;";
            }
            else if (prjBG.FIsBG == 1)//判断该项目是否变更
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Item.ToolTip = "该项目已经做了变更，变更前的业务停止进行。";
                btnOp.Enabled = false;
                btnOp.Attributes["onclick"] = "return false;";
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
                string FAppId = e.CommandArgument.ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("update cf_App_list set fstate=0,");
                sb.Append("FReportDate='' ");
                sb.Append("where FId='" + FAppId + "';");
                rc.PExcute(sb.ToString());
                pageTool tool = new pageTool(this.Page);
                ShowInfo();
                tool.showMessage("撤销成功！");
            }
            else if (e.CommandName == "Del")
            {
                //删除申报信息和企业数据
                StringBuilder sb = new StringBuilder();
                sb.Append("delete cf_App_list where FId='" + FId + "';");
                sb.Append("delete cf_Prj_data where FAppId='" + FId + "';");
                sb.Append("delete cf_Prj_Ent where FAppId='" + FId + "';");
                sb.Append("delete CF_AppPrj_FileOther where FAppId='" + FId + "';");
                rc.PExcute(sb.ToString());
                pageTool tool = new pageTool(this.Page);
                ShowInfo();
                tool.showMessage("删除成功！");
            }
            else if (e.CommandName == "ReApp") //重新办理
            {
                string[] s = e.CommandArgument.ToString().Split(',');
                if (s.Length == 5)
                {
                    string FPrjId = s[0];
                    string FPrjName = s[1];
                    string FCount = s[2];
                    string FOldAppId = s[3];
                    string FPriItemId = s[4];//

                    t_FPrjId.Value = FPrjId;
                    txtFPrjName.Text = FPrjName;
                    appTab.Visible = false;
                    applyInfo.Visible = true;
                    ViewState["FMNUMBER"] = fMType;
                    t_FYear.Text = DateTime.Now.Year.ToString();
                    t_FName.Text = t_FYear.Text + "年 " + db.getManageTypeName(fMType);
                    t_OldFAppId.Value = FOldAppId;
                    t_FCount.Value = FCount;//是否已是2次
                    t_FPriItemId.Value = FPriItemId;//记录之前的2次关联
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
        if (string.IsNullOrEmpty(t_FPrjId.Value))
        {
            tool.showMessage("请先选择项目！");
            return;
        }
        //查询如果有未办结(不包含退回的)，则不可在进行填写
        if (db.CF_App_List.Count(l => l.FPrjId == t_FPrjId.Value && l.FManageTypeId == fMType && l.FIsDeleted != true && (l.FState != 7 && l.FState != 6)) > 0)
        {
            tool.showMessage("该项目正在办理初步设计合同备案业务！");
            return;
        }
        string fPrjDataId = Guid.NewGuid().ToString();
        string fAppId = Guid.NewGuid().ToString();
        CF_App_List lKC = new CF_App_List();//设计企业业务
        lKC.FId = fAppId;
        lKC.FLinkId = fPrjDataId;
        lKC.FBaseinfoId = CurrentEntUser.EntId;
        lKC.FPrjId = t_FPrjId.Value;
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
        //看是不是二次初步设计
        if (!string.IsNullOrEmpty(t_OldFAppId.Value))
            lKC.FCount = EConvert.ToInt(t_FCount.Value); //是几次
        else
            lKC.FCount = 1;
        db.CF_App_List.InsertOnSubmit(lKC);

        CF_Prj_Data prjData = new CF_Prj_Data();//数据类型
        prjData.FAppId = fAppId;
        prjData.FId = fPrjDataId;
        prjData.FPrjId = t_FPrjId.Value;
        prjData.FPrjName = txtFPrjName.Text.Trim();
        prjData.FBaseInfoId = CurrentEntUser.EntId;
        prjData.FPriItemId = t_FPriItemId.Value;//保存审查不合格那条审查业务的CF_Prj_Data.FID
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
        if (!string.IsNullOrEmpty(t_FPrjId.Value))
            txtFPrjName.Text = rc.GetSignValue("select FPrjName from cf_Prj_Baseinfo where fid='" + t_FPrjId.Value + "'");
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
