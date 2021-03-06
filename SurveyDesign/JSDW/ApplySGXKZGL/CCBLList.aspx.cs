﻿using System;
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
using System.Data.Objects.DataClasses;
using ProjectBLL;
using EgovaDAO;
using EgovaBLL;
public partial class JSDW_APPLYSGXKZGL_CCBLList : System.Web.UI.Page
{
    RApp ra = new RApp();
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    WorkFlowApp wfApp = new WorkFlowApp();
    
    
    public int fMType = 11223;//施工许可证初次办理

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
        EgovaDB dbContext = new EgovaDB();
        string FBaseinfoID = CurrentEntUser.EntId;
        var v = from t in dbContext.CF_App_List
                join a in dbContext.TC_SGXKZ_PrjInfo
                on t.FId equals a.FAppId
                //join b in dbContext.TC_PrjItem_Info
                //on a.FPrjItemId equals b.FId
                where t.FBaseinfoId == FBaseinfoID && t.FManageTypeId == fMType
                orderby t.FReportDate
                select new
                {
                    t.FId,
                    t.FwriteDate,
                    t.FReportDate,
                    t.FCreateTime,
                    t.FState,
                    a.FResult,
                    t.FLinkId,
                    a.PrjAddressDept,
                    PrjAddressDeptName = dbContext.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(a.PrjAddressDept)).Select(d => d.FFullName).FirstOrDefault(),
                    a.Address,
                    a.PrjItemName,
                    a.ProjectName
                };
        if (!string.IsNullOrEmpty(this.txtFPrjItemName.Text.Trim()))
        {
            v = v.Where(t => t.PrjItemName.Contains(this.txtFPrjItemName.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(govd_FRegistDeptId.FNumber))
        {
            if ("51" == govd_FRegistDeptId.FNumber)
            {
            }
            else
            {
                v = v.Where(t => t.PrjAddressDept.Equals(this.govd_FRegistDeptId.FNumber.Trim()));
            }
            
        }
        if (!string.IsNullOrEmpty(this.txtJSDZ.Text.Trim()))
        {
            v = v.Where(t => t.Address.Contains(this.txtJSDZ.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.txtSDate.Text.Trim()))
        {
            v = v.Where(t => t.FReportDate >= DateTime.Parse(this.txtSDate.Text.Trim()+" "+"00:00:00"));
        }
        if (!string.IsNullOrEmpty(this.txtEDate.Text.Trim()))
        {
            v = v.Where(t => t.FReportDate <= DateTime.Parse(this.txtEDate.Text.Trim()+" "+"23:59:59"));
        }
        if (this.ddlFState.SelectedIndex > 0)
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
        EgovaDB dbContext = new EgovaDB();
        if (!WFApp.ValidateNewBiz(t_FPriItemId.Value, fMType))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "js", "alert('条件不符合（可能已经办理了），不能创建初次办理业务！');", true);
            return;
        }
        if (string.IsNullOrEmpty(CurrentEntUser.EntId))
        {
            return;
        }
        lblMessage.Text = "正在创建业务...";
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
        //app.FPrjId = t_FPrjId.Value;
        app.FReportCount = 1;
        dbContext.CF_App_List.InsertOnSubmit(app);

        //通过session中的entid获取到当前登录用户的相关信息，并赋值对应的信息施工许可证中的建设单位信息
        TC_JSDW_USER tju = new TC_JSDW_USER();
        tju = dbContext.TC_JSDW_USER.Where(t =>t.FID == CurrentEntUser.EntId).FirstOrDefault();  
        if (tju != null)
        {

            //添加初次办理信息
            TC_SGXKZ_PrjInfo record = new TC_SGXKZ_PrjInfo
            {
                FId = Guid.NewGuid().ToString(),
                FAppId = FAppId,
                PrjId = t_FPrjId.Value,
                PrjItemId = t_FPriItemId.Value,
                FPrjItemId = t_FPriItemId.Value,
                PrjItemName = t_FPrjItemName.Text,
                ProjectName = t_FPrjName.Value,
                PrjAddressDept = t_AddressDept.Value,
                PrjItemType = t_PrjItemType.Value,
                //JSDWAddressDept = t_JSDWAddressDept.Value,//MODIFY:YTB 从控件取值赋值建设单位所属地
                //新建建设单位id

                jsdwid = CurrentEntUser.EntId,
                //JSDWXZ = tju.DWXZ,//建设单位性质，企业信息表中是汉字，暂时不能对应，让用户自己选择
                JProvince = tju.s1,
                JCity = tju.s2,
                JCounty = tju.s3,
                JSDWAddressDept = tju.SSQYID,
                JSDWDZ = tju.DWDZ,
                FDDBR = tju.FRXM,
                FRDH = tju.FRSJ,
                LZR = tju.LXR,
                LXDH = tju.LXDH,               
                JSFZR = "",
                JSFZRZC = "",
                JSFZRDH = "",
                //
                ReportTime = DateTime.Now,
                //JSDW = t_JSDW.Value
                JSDW = tju.FCompany
            };
            dbContext.TC_SGXKZ_PrjInfo.InsertOnSubmit(record);      
        }
        else
        {
            //添加初次办理信息
            TC_SGXKZ_PrjInfo record = new TC_SGXKZ_PrjInfo
            {
                FId = Guid.NewGuid().ToString(),
                FAppId = FAppId,
                PrjId = t_FPrjId.Value,
                PrjItemId = t_FPriItemId.Value,
                FPrjItemId = t_FPriItemId.Value,
                PrjItemName = t_FPrjItemName.Text,
                ProjectName = t_FPrjName.Value,
                PrjAddressDept = t_AddressDept.Value,
                PrjItemType = t_PrjItemType.Value,
                //JSDWAddressDept = t_JSDWAddressDept.Value,//MODIFY:YTB 从控件取值赋值建设单位所属地
                //新建建设单位id

                jsdwid = CurrentEntUser.EntId,
                //JSDWXZ = tju.DWXZ,//建设单位性质，企业信息表中是汉字，暂时不能对应，让用户自己选择
                //JProvince = tju.s1,
                //JCity = tju.s2,
                //JCounty = tju.s3,
                //JSDWAddressDept = tju.SSQYID,
                //JSDWDZ = tju.DWDZ,
                //FDDBR = tju.FRXM,
                //FRDH = tju.FRSJ,
                //LZR = tju.LXR,
                //LXDH = tju.LXDH,
                JSFZR = "",
                JSFZRZC = "",
                JSFZRDH = "",
                //
                ReportTime = DateTime.Now,
                //JSDW = t_JSDW.Value
                JSDW = ""
            };
            dbContext.TC_SGXKZ_PrjInfo.InsertOnSubmit(record);      
        }
           
       
        //提交修改
        dbContext.SubmitChanges();
        lblMessage.Text = "业务创建成功,即将自动跳转到业务信息登记页面...";
        Session["FAppId"] = FAppId;
        Session["FManageTypeId"] = fMType;
        Session["FIsApprove"] = 0;
        tool.ExecuteScript("top.document.location='../Appmain/aindex.aspx';");
        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "js", "top.document.location='../Appmain/aindex.aspx';", true);
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        this.SaveInfo();
    }
    /// <summary>
    /// 获取企业信息
    /// </summary>
    /// <param name="entId">企业ID</param>
    /// <returns>企业实体</returns>
    private EgovaDAO.CF_Ent_BaseInfo EntInfo(string entId)
    { 
        //MODIFY:YTB 根据企业ID获取企业信息
        EgovaDB egovaDb = new EgovaDB();
        return egovaDb.CF_Ent_BaseInfo.SingleOrDefault(item => item.FId == entId);
    }

    /// <summary>
    /// 获取企业所属地编码
    /// </summary>
    /// <param name="entId">企业ID</param>
    /// <returns>企业所属地，只取前4位</returns>
    private string EntyRegistDeptId(string entId)
    {
        try
        {
            //MODIFY:YTB 获取企业所属地；
            var enty = EntInfo(entId);
            var registDeptId = enty.FRegistDeptId;
            if (registDeptId == null)
                return "";
            if (registDeptId.ToString().Length >= 4)
            {
                return registDeptId.ToString().Substring(0, 4);
            }
            else if (registDeptId.ToString().Length <= 4 && registDeptId.ToString().Length >= 2)
            {
                return registDeptId.ToString().Substring(0, 2);
            }
            else
            {
                return "";
            }
        }
        catch
        {
            return "";
        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowInfo();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void gv_list_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //序号
            Label lbautoid = (Label)e.Row.FindControl("lbautoid");
            lbautoid.Text = (e.Row.RowIndex + 1).ToString();

            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FState"));
            //string FPrjId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPrjId"));
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
                    btnOp.Visible = false;
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
                    saveFResult("1", FID);
                }
                else//不予受理
                {
                    s = "<font color='red'>已办结</font>";
                    n = "<font color='red'>不通过</font>";
                    saveFResult("3", FID);
                }
            }

            e.Row.Cells[4].Text = t;
            e.Row.Cells[5].Text = s;
            //var pr = dbContext.CF_App_ProcessRecord.Where(q => q.FLinkId == FID && q.FMeasure != 0).FirstOrDefault();
            //if (pr != null)
            //{
            //    if (pr.FResult == "1")//通过
            //    {
            //        n = "<font color='green'>通过</font>";
            //        saveFResult("1", FID);
            //    }
            //    else//不通过
            //    {
            //        n = "<font color='red'>不通过</font>";
            //        saveFResult("3", FID);
            //    }
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
            //}
            e.Row.Cells[7].Text = n;
        }
    }
    protected void saveFResult(string result, string fId)
    {
        EgovaDB dbContext = new EgovaDB();
        TC_SGXKZ_PrjInfo r = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == fId).FirstOrDefault();
        r.FResult = result;
        dbContext.SubmitChanges();
    }


    protected void btnSel_Click(object sender, EventArgs e)
    {
        //EgovaDB dbContext = new EgovaDB();
        //var result = (from t in dbContext.TC_PrjItem_Info
        //              where t.FId == this.t_FPriItemId.Value
        //              select t).SingleOrDefault();

        string sql = @"select  a.DWGCMC,a.XMBH,b.JSDW,b.XMMC,b.XMSD,b.XMLX from   xm_baseinfo.dbo.GC_DWGCXX a, xm_baseinfo.dbo.XM_XMJBXX b
                     where a.XMBH = b.XMBH
                     and a.DWGCBH = '"+this.t_FPriItemId.Value+"'";
        DataTable dt = new DataTable();
        dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            t_FPrjItemName.Text = dt.Rows[0]["DWGCMC"].ToString();
            t_FPrjId.Value = dt.Rows[0]["XMBH"].ToString();
            t_FJSDW.Text = dt.Rows[0]["JSDW"].ToString();
            t_FPrjName.Value = dt.Rows[0]["XMMC"].ToString();
            t_AddressDept.Value = dt.Rows[0]["XMSD"].ToString();
            t_PrjItemType.Value = dt.Rows[0]["XMLX"].ToString();
            t_JSDW.Value = CurrentEntUser.EntName.ToString();            
            t_JSDWAddressDept.Value = CurrentEntUser.EntId.ToString();
           
        }
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
            var v1 = db.TC_QA_Record.Where(t => t.FAppId == FId);
            if (v1 != null)
            {
                db.TC_QA_Record.DeleteAllOnSubmit(v1);
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
