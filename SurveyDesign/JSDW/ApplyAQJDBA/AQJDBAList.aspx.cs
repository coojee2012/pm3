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
using EgovaDAO;
using EgovaBLL;
public partial class JSDW_APPLYZLJDBN_AQJDBAList : System.Web.UI.Page
{
    RApp ra = new RApp();
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    EgovaDB dbContext = new EgovaDB();
    
    public int fMType = 11222;

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
        t_FYear.Text = DateTime.Now.Year.ToString();
        t_FName.Text = t_FYear.Text + "年 " + db.getManageTypeName(fMType);
    }

    private void ShowInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        var v = from t in dbContext.CF_App_List
                join a in dbContext.TC_AJBA_Record
                on t.FId equals a.FAppId
                where t.FBaseinfoId == FBaseinfoID && t.FManageTypeId == fMType
                      && t.FIsDeleted == false
                orderby t.FReportDate
                select new
                {
                    t.FId,
                    t.FwriteDate,
                    t.FReportDate,
                    t.FState,
                    a.FPrjId,
                    t.FLinkId,
                    a.RecordNo,
                    a.PrjItemName,
                    a.ProjectName,
                    a.FResult
                };
        if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
        {
            v = v.Where(t => t.ProjectName.Contains(this.txtProjectName.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.txtProjectItemName.Text.Trim()))
        {
            v = v.Where(t => t.PrjItemName.Contains(this.txtProjectItemName.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.txtSDate.Text.Trim()))
        {
            v = v.Where(t => t.FReportDate>=DateTime.Parse( this.txtSDate.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.txtEDate.Text.Trim()))
        {
            v = v.Where(t => t.FReportDate <= DateTime.Parse(this.txtEDate.Text.Trim()));
        }
        if (this.ddlFState.SelectedIndex>0)
        {
            v = v.Where(t => t.FState.Equals(ddlFState.SelectedValue));
        }
        if (this.ddlFResult.SelectedIndex > 0)
        {
            v = v.Where(t => t.FResult.Equals(ddlFResult.SelectedValue));
        }

        Pager1.RecordCount = v.Count();
        this.gv_list.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        gv_list.DataBind();

    }


    protected void btn_Click(object sender, EventArgs e)
    {
        appTab.Visible = false;
        applyInfo.Visible = true;
    }
    //取消
    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        appTab.Visible = true;
        applyInfo.Visible = false;
    }

    //创建业务
    private void SaveInfo()
    {
        
        pageTool tool = new pageTool(this.Page);
        if (!WFApp.ValidateNewBiz(t_FPriItemId.Value,fMType))
        {
            tool.showMessage("同一个工程不能创建两条备案信息！");
            return;
        }
        if (string.IsNullOrEmpty(CurrentEntUser.EntId))
            return;
        //添加业务
        DateTime dTime = DateTime.Now;
        string FAppId = Guid.NewGuid().ToString();
        EgovaDAO.CF_App_List app = new EgovaDAO.CF_App_List();//业务
        app.FId = FAppId;
        app.FLinkId = t_FPriItemId.Value;
        app.FManageTypeId = fMType;
        app.FwriteDate = dTime;
        app.FState = 0;
        app.FIsDeleted = false;
        app.FName = t_FName.Text.Trim();//业务名
        app.FYear = EConvert.ToInt(t_FYear.Text.Trim());//年份
        app.FMonth = DateTime.Now.Month;//月份
        app.FBaseName = CurrentEntUser.EntName;//单位名
        app.FBaseinfoId = CurrentEntUser.EntId;//单位ID
        app.FTime = dTime;
        app.FCreateTime = dTime;
        app.FReportCount = 1;
        dbContext.CF_App_List.InsertOnSubmit(app);
        //添加备案信息
        TC_AJBA_Record record = new TC_AJBA_Record();
        string FRecordId = Guid.NewGuid().ToString();
        record.FId = FRecordId;
        record.FAppId = FAppId;
        record.FPrjId = t_FPrjId.Value;
        record.FPrjItemId = t_FPriItemId.Value;
        record.PrjItemName = t_FPrjItemName.Text;
        record.ProjectName = t_FProjectName.Value;
        record.RecordTime = dTime;
        //从项目信息中获取建设单位
        var re = (from t in dbContext.v_PrjItem_Info
                      where t.FId == this.t_FPriItemId.Value
                      select t).SingleOrDefault();
        if (re != null)
        {
            record.FJSDWID = re.FJSDWID;
        }


         //生成备案号
        //record.RecordNo = getBANumber();
        string ProjectType;
        var result = (from t in dbContext.v_prj_Info
                      where t.FId == t_FPrjId.Value
                           select t).FirstOrDefault();   
        if(result.ProjectType == "2000101")
        {
            ProjectType = "01";
        }
        else if(result.ProjectType == "2000102")
        {
            ProjectType = "02";
        }
        else
        {
            ProjectType = "99";
        }
        record.RecordNo = getBANumber(result.AddressDept, ProjectType,"AX");
        //
        dbContext.TC_AJBA_Record.InsertOnSubmit(record);
        //提交修改
        dbContext.SubmitChanges();

        Session["FAppId"] = FAppId;
        Session["FManageTypeId"] = fMType;
        Session["FIsApprove"] = 0;
        tool.ExecuteScript("top.document.location='../Appmain/aindex.aspx';");
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        this.SaveInfo();
    }


    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void gv_list_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //序号
            Label lbautoid = (Label)e.Row.FindControl("lbautoid");
            lbautoid.Text = (e.Row.RowIndex + 1).ToString();

            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FState"));
            string FPriItemId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FLinkId"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPrjId"));
            DateTime FReportDate = EConvert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "FReportDate"));

            //本业务办理状态
            string t = "", s = "", n = "";
            LinkButton btnOp = (LinkButton)e.Row.FindControl("btnOp");
            LinkButton btnDel = (LinkButton)e.Row.FindControl("btnDel");
            LinkButton btnBack = (LinkButton)e.Row.FindControl("btnBack");
            btnBack.Attributes["onclick"] = "return confirm('确定要撤消上报吗？');";
            btnDel.Attributes["onclick"] = "return confirm('确定要删除吗？');";
            switch (FState)
            {
                case "0"://未上报 
                    t = "<font color='#888888'>--</font>";
                    s = "<font color='#888888'>未上报</font>";
                    btnOp.Visible = true;
                    btnDel.Visible = true;
                    btnBack.Visible = false;
                    break;
                case "1"://已上报 
                    t = FReportDate.ToShortDateString();
                    s = "<font color='blue'>已上报</font>";
                    btnOp.Text = "--";
                    
                    btnDel.Visible = false;
                    if (WFApp.ValidateCanCancelApply(FID))
                    {
                        btnBack.Visible = true;
                    }
                    else
                    {
                        btnBack.Visible = false;
                    }
                    break;
                case "2"://被退回 
                    btnOp.Attributes["onclick"] = "return false;";
                    btnOp.Text = "--";
                    btnDel.Visible = false;
                    btnBack.Visible = false;
                    t = FReportDate.ToShortDateString();
                    s = "<a href=\"javascript:showApproveWindow('../main/JGLookIdea.aspx?FAppId=" + FID + "',600,400);\"><font color='red'>退回</font></a>";
                    break;
                case "6"://办结
                    btnOp.Attributes["onclick"] = "return false;";
                    btnOp.Text = "--";
                    btnDel.Visible = false;
                    btnBack.Visible = false;
                    t = FReportDate.ToShortDateString();
                    
                    break;
                default:
                    btnOp.Attributes["onclick"] = "return false;";
                    btnOp.Text = "--";
                    btnDel.Visible = false;
                    btnBack.Visible = false;
                    break;
            }
            
            //查询办结备案通过没
            var v = (from i in dbContext.CF_App_ProcessRecordBackup
                        where i.FLinkId == FID
                        select new
                        {
                            i.FID,
                            i.FResult,
                        }).FirstOrDefault();
            if (v != null)
            {
                if (v.FResult == "1")//正常办结
                {
                    s = "<font color='green'>已办结</font>";
                    n = "<font color='green'>通过</font>";
                    //saveFResult("1", FID);
                }
                else//不予受理
                {
                    s = "<font color='red'>已办结</font>";
                    n = "<font color='red'>不通过</font>";
                    //saveFResult("3", FID);
                }
            }
          //  e.Row.Cells[6].Text = n;
            e.Row.Cells[3].Text = t;
            e.Row.Cells[4].Text = s;
            var pr = dbContext.CF_App_ProcessRecord.Where(q => q.FLinkId == FID);
           
            if (pr != null)
            {
                foreach(var p in pr)
                {
                    if (p.FTypeId == 5) //复审
                    {
                        if (p.FMeasure == 5) //已经办理
                        {
                            if (p.FResult == "1")
                            {
                                n = "<font color='green'>复审通过</font>";
                                break;
                            }
                            else
                            {
                                n = "<font color='green'>复审中</font>";
                                break;
                            }
                        }
                        else
                        {
                            n = "<font color='green'>复审中</font>";
                            break;
                        }
                    }
                    else if (p.FTypeId == 10) //初审
                    {
                        if (p.FMeasure == 5)  //已经办理
                        {
                            if (p.FResult == "1")
                            {
                                n = "<font color='green'>初审通过</font>";
                                break;
                            }
                            else
                            {
                                n = "<font color='green'>初审中</font>";
                                break;
                            }
                        }
                        else
                        {
                            n = "<font color='green'>初审中</font>";
                            break;
                        }
                    }
                    else if (p.FTypeId == 10) //接件
                    {
                        if (p.FMeasure == 5)  //已经办理
                        {
                            if (p.FResult == "1")
                            {
                                n = "<font color='green'>接件成功</font>";
                                break;
                            }
                            else
                            {
                                n = "<font color='green'>待接件</font>";
                                break;
                            }
                        }
                        else
                        {
                            n = "<font color='green'>待接件</font>";
                            break;
                        }
                    }
                    else //未上报
                    {
                        n = "";
                    }
                }

                /*
                if (pr.FResult == "1")//通过 屏蔽以前的方法，modify by psq 20150416
                {
                    n = "<font color='green'>通过</font>";
                    saveFResult("1", FID);
                }
                else//不通过
                {
                    n = "<font color='red'>不通过</font>";
                    saveFResult("3", FID);
                }
                 * */   

                //if (pr.FRoleDesc.Contains("接件"))
                //{
                //    if (pr.FResult == "1")//通过
                //    {
                //        n += "，<font color='green'>同意接件</font>";
                //    }
                //    else//不通过
                //    {
                //        n += "，<font color='red'>不同意接件</font>";
                //    }
                //} else if (pr.FRoleDesc.Contains("初审"))
                //{
                //    if (pr.FResult == "1")//通过
                //    {
                //        n += "，<font color='green'>初审通过</font>";
                //    }
                //    else//不通过
                //    {
                //        n += "，<font color='red'>初审不通过</font>";
                //    }
                //} else if (pr.FRoleDesc.Contains("复审"))
                //{
                //    if (pr.FResult == "1")//通过
                //    {
                //        n += "，<font color='green'>复审通过</font>";
                //    }
                //    else//不通过
                //    {
                //        n += "，<font color='red'>复审不通过</font>";
                //    }
                //}
            }
            e.Row.Cells[6].Text = n;
        }
    }

    protected void saveFResult(string result, string fId)
    {
        TC_AJBA_Record r = dbContext.TC_AJBA_Record.Where(t => t.FAppId == fId).FirstOrDefault();
        r.FResult = result;
        dbContext.SubmitChanges();
    }

    protected void btnSel_Click(object sender, EventArgs e)
    {
        var result = (from t in dbContext.v_PrjItem_Info
                      where t.FId == this.t_FPriItemId.Value
                      select t).SingleOrDefault();
        t_FPrjItemName.Text = result.PrjItemName;
        t_FPrjId.Value = result.FPrjId;
        t_FProjectName.Value = result.ProjectName;
        t_JSDW.Value = result.JSDW;
        
        
    }
    private string getBANumber()
    {

        string recordNo = "AX" + string.Format("{0:yyyyMMdd}", DateTime.Now);
        var result = (from t in dbContext.TC_AJBA_Record
                      where t.RecordNo.Contains(recordNo)
                      select t).Count();
        return recordNo + (result + 1);

    }
    /// <summary>
    /// 生成备案号
    /// 
    /// </summary>
    /// <param name="prjitemarea">项目属地</param>
    /// <param name="prjitemtype">项目类型</param>
    /// <param name="bussinesstype">业务节点类型</param>
    /// <returns>备案号</returns>
    private string getBANumber(string prjitemarea,string prjitemtype,string bussinesstype)
    {
      
        int todayno;
        int todayxlh;
        string recordnewno = "";
        string stodayno,stodayxlh;
        
        //当天日期
        string datatoday = string.Format("{0:yyMMdd}", DateTime.Now);
        //获取当天最大的值
        var result = (from t in dbContext.TC_AJBA_Record
                      where t.RecordNo.Substring(6, 6) == datatoday
                        && t.Area.Equals(prjitemarea)
                      orderby t.RecordTime descending
                      select t).FirstOrDefault();
        if (result != null)
        {
            todayno = Convert.ToInt32(result.RecordNo.Substring(12, 2)) + 1;
            todayxlh = Convert.ToInt32(result.RecordNo.Substring(20, 3)) + 1;
        }
        else
        {
            todayno = 1;
            todayxlh = 1;
        }
        //如果项目编号小于10
        if (todayno < 10)
        {
            stodayno = "0" + todayno.ToString();
        }
        else
        {
            stodayno = todayno.ToString();
        }
        //如果序列号小于10或1000
        if (todayxlh < 10)
        {
            stodayxlh = "00" + todayxlh.ToString();
        }
        else if (todayxlh < 100)
        {
            stodayxlh = "0" + todayxlh.ToString();
        }
        else
        {
            stodayxlh = todayxlh.ToString();
        }

        //生成编号
        recordnewno = prjitemarea + string.Format("{0:yyMMdd}", DateTime.Now) + prjitemtype + stodayno + "-" + bussinesstype + "-" + stodayxlh;
        return recordnewno;

    }

    protected void gv_list_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "See")
        {
            string[] str = Convert.ToString(e.CommandArgument).Split('@'); 
            string FId = str[0];
            int fState = Convert.ToInt32(str[1]);
            this.Session["FAppId"] = FId;
            this.Session["FManageTypeId"] = fMType;
            if (fState != 0 && fState != 2)
            {
                Session["FIsApprove"] = 1;//表单不可编辑
            }
            else
            {
                Session["FIsApprove"] = 0;//表单可编辑
            }
            //Response.Redirect("../Appmain/aindex.aspx");
            Response.Write("<script language='javascript'>top.document.location='../Appmain/aindex.aspx';</script>");

        }
        if (e.CommandName == "Del")
        {
            pageTool tool = new pageTool(this.Page);
            EgovaDB db = new EgovaDB();
            string FId = Convert.ToString(e.CommandArgument);
            var v = db.CF_App_List.Where(t => t.FId == FId);
            if (v != null)
            {
                db.CF_App_List.DeleteAllOnSubmit(v);
                
             //   this.ShowInfo();
            }
            var v1 = db.TC_AJBA_Record.Where(t => t.FAppId == FId);
            if(v1!=null){
                db.TC_AJBA_Record.DeleteAllOnSubmit(v1);
            }
            db.SubmitChanges();
            tool.showMessage("删除成功");
            this.ShowInfo();
        }
        if (e.CommandName == "Op")
        {
            string FId = Convert.ToString(e.CommandArgument);
            this.Session["FAppId"] = FId;
            this.Session["FManageTypeId"] = fMType;
            //if (fState != 0 && fState != 2)
            //    Session["FIsApprove"] = 1;
            //else
            //    Session["FIsApprove"] = 0;
            Response.Write("<script language='javascript'>top.document.location='../Appmain/aindex.aspx';</script>");
            
        }
        if (e.CommandName == "Back")
        {
            string FAPPID = e.CommandArgument.ToString();
            pageTool tool = new pageTool(this.Page);
            if (!string.IsNullOrEmpty(FAPPID))
            {
                //RQuali rq = new RQuali();
                //rq.CancelApply(FAPPID);
                if (WFApp.ValidateCanCancelApply(FAPPID))
                {
                    WFApp.CancelApply(FAPPID);
                    tool.showMessage("撤消成功");
                    this.ShowInfo();
                }
                
            }
            else
            {
                tool.showMessage("撤消失败");
            }

        }
    }
    protected void gv_list_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //把row的index添加到CommandArgument中
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton LinkButton1 = (LinkButton)e.Row.FindControl("btnItemSee");
            LinkButton1.CommandArgument = e.Row.RowIndex.ToString();
            
        }
        e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;//guid列的隐藏
    }
}
