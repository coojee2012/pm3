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

public partial class Government_AppMain_ProjectQuery : govBasePage
{
    RCenter rc = new RCenter();
    RQuali rq = new RQuali();
    SaveAsBase sab = new SaveAsBase();
    RAppBacth rap = new RAppBacth();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);

      

            showDDType(Request["fsystemid"]);
            ControlBind();
            ShowInfo();
          
            if (Request["fcol"] != null && Request["fcol"] != "")
            {
                lPostion.Text = rc.GetMenuName(Request["fcol"]);
            }

            //如果不是省级用户不显示打印和向省政府发送列
            if (EConvert.ToInt(Session["DFLevel"]) != 1)
            {
                JustAppInfo_List.Columns[17].Visible = false;
            }

            if ((Request.QueryString["ftypeid"] == "10" || Request.QueryString["ftypeid"] == "5") && Session["DFId"].ToString() == ComFunction.GetDefaultDept())
            {
                if (Request["fsystemid"] != null && Request["fsystemid"].ToString() != "100")
                {
                    btnExport.Visible = true;
                }
            }
        }
    }

    private void ShowPostion()
    {
        this.lPostion.Text = rc.GetMenuName(Request["fcol"]);
    }

    private void ControlBind()
    {
        
    }

    string GetMTypeList()
    {
        StringBuilder sb = new StringBuilder();
       
        return sb.ToString();
    }
    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        
        return sb.ToString();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from ( ");
        sb.Append("select ep.FId,er.FId erId,ep.FBaseInfoId,ep.FLinkId,ep.FEntName,ep.FEmpName,ep.FManageTypeId,ep.FListId,er.FTypeId,er.FRoleId,");
        sb.Append(" ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,ep.FManageDeptId,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,Convert(char(10),ep.FPlanTime,20) FAppTime,er.FMeasure,er.FReporttime,");
        sb.Append(" er.fisQuali ");
        sb.Append(" from CF_App_ProcessInstance ep inner join CF_App_ProcessRecord er");
        sb.Append(" on ep.fId = er.FProcessInstanceID ");
        sb.Append(" inner join ( ");
        sb.Append(" select max(er.fid) fid from CF_App_ProcessInstance ep,CF_App_ProcessRecord er");
        sb.Append(" where ep.fId = er.FProcessInstanceID ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"] + ")");
        sb.Append(" and ep.FManageDeptId like '" + Session["DFId"] + "%' ");
   
        sb.Append(getCondi());
        sb.Append(" group by ep.flinkId,er.FMeasure)temp on temp.fid=er.fid where 1=1 ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"] + ")");
        sb.Append(" and ep.FManageDeptId like '" + Session["DFId"] + "%' ");
     
        sb.Append(getCondi());
 
        sb.Append(")tt ");
        ViewState["DFRoleId"] = Session["DFRoleId"];//审批角色 
        sb.Append(" order by tt.FReporttime desc,tt.FBaseInfoId");

        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    private void showDDType(string FSystemId)
    {
       
    }

    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string pId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fLinkId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            string fMeasure = e.Item.Cells[e.Item.Cells.Count - 3].Text;
            string fState = e.Item.Cells[e.Item.Cells.Count - 5].Text;
            string fManageTypeId = e.Item.Cells[3].Text;
            string fManageDeptId = e.Item.Cells[9].Text;
            string fSubFlowId = e.Item.Cells[10].Text;
            string fisQuali = e.Item.Cells[e.Item.Cells.Count - 8].Text;
            string fAppResult = e.Item.Cells[14].Text;

            //应办结时间
            e.Item.Cells[15].Text = "15个工作日内";
            e.Item.Cells[3].Text = rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + fManageTypeId + "'");

            string fListId = e.Item.Cells[4].Text;
            string fTypeId = e.Item.Cells[5].Text;
            string fLevelId = e.Item.Cells[6].Text;
            e.Item.Cells[4].Text = "";

            e.Item.Cells[9].Text = rc.GetSignValue(EntityTypeEnum.EsManageDept, "FName", "Fnumber='" + fManageDeptId + "'");

            switch (fAppResult)
            {
                case "1":
                    e.Item.Cells[14].Text = "同意";
                    break;

                case "3":
                    e.Item.Cells[14].Text = "不同意";
                    break;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("FBaseInfoId,FState,FRoleId,FManageTypeId,fsystemid,FState");
            EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, sb.ToString(), "fid='" + pId + "'");
            if (ea == null)
            {
                EaProcessInstanceBackup backup = (EaProcessInstanceBackup)rc.GetEBase(EntityTypeEnum.EaProcessInstanceBackup, sb.ToString(), "fid='" + pId + "'");
                if (backup == null)
                {

                    return;
                }
                else
                {
                    ea = new EaProcessInstance();
                    ea.FBaseInfoId = backup.FBaseInfoId;
                    ea.FManageTypeId = backup.FManageTypeId;
                    ea.FSystemId = backup.FSystemId;
                }
            }
            string fbid = ea.FBaseInfoId;
            string faid = fLinkId;
            string fmid = ea.FManageTypeId;
            //e.Item.Cells[10].Text = rc.GetSignValue(EntityTypeEnum.EaSubFlow, "FName", "FId='" + fSubFlowId + "'");
            //if (fState == "6")
            //{
            //    e.Item.Cells[10].Text = "已办结";
            //    e.Item.Cells[0].Enabled = false; 
            string FEmpName = Server.HtmlEncode(EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEmpName")));
            string fUrl = "../../GMap/mapApp/main.aspx?ftype=8000101";
            //查询该选择意见书的经纬度
            DataTable dt = rq.GetTable("select fId,fLon,fLat,FAddressDept,fProjectId from CQ_Prj_BaseInfo where fappId='" + faid + "' and fbaseInfoId='" + fbid + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                string fId = dt.Rows[0]["fProjectId"].ToString();

                e.Item.Cells[10].Text = "<a href='#'>查看</a>";
                e.Item.Cells[10].Attributes.Add("onclick", "openWinNew('" + fUrl + "&cityid=" + dt.Rows[0]["FAddressDept"] + "&FLinkId=" + fId + "&pId=" + pId + "&name='+escape('" + FEmpName + "'))");
            }
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string sScript = "showApproveWindow('../AppQualiInfo/ShowAppInfo1.aspx?pid=" + pId + "&lid=" + fLinkId + "',830,500)";
            sScript = "";
            e.Item.Cells[8].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\">查看</a>";
            string frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + fbid + "'");
            string fsystemid = ea.FSystemId;


            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];

            string s = "";

            string fEmpName = e.Item.Cells[2].Text;
            object fentname = DataBinder.Eval(e.Item.DataItem, "FEntName");
            //查询查询地址
            string sUrl = "";
            sUrl = rc.getMTypeQurl(ea.FManageTypeId);
            sUrl += "?fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1";
            e.Item.Cells[3].Text = "<a class='link7' href='" + sUrl + "' target='_blank'>" + e.Item.Cells[3].Text + "</a>";

            //审批地址
            string fAppUrl = rc.getMTypeAurl(fmid);
            fAppUrl += "?lid=" + fLinkId + "&p=" + fMeasure + "&ftypeid=" + Request.QueryString["ftypeid"].ToString();

            if (fMeasure.Trim() == "0" || fMeasure == "3") //未审核
            {
                if (fState.Trim() == "2")
                {
                    sScript = "showApproveWindow1('../webCheck/backIdea.aspx?aid=" + pId + "',780,780)";

                    e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\" title='" + fentname + "'>" + fEmpName + "</a>";
                    e.Item.Cells[14].Text = "打回企业";
                    box.Enabled = false;
                    e.Item.Attributes.Add("onmouseover", "this.title='已经打回企业，不能批量审批'");
                }
                else
                {
                    if (fsystemid == "261" || fsystemid == "262" || fsystemid == "263" || fsystemid == "264" || fsystemid == "267")
                    {
                        e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + fAppUrl + "',980,450)\" title='" + fentname + "'>";
                    }
                    else
                    {
                        e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + fAppUrl + "',980,780)\" title='" + fentname + "'>";
                    }
                    e.Item.Cells[2].Text += fEmpName + "</a>";
                    e.Item.Cells[14].Text = "未填写审批意见，未上报";
                }
            }

            if (fMeasure.Trim() == "1") //已经填写意见
            {
                if (fsystemid == "261" || fsystemid == "262" || fsystemid == "263" || fsystemid == "264" || fsystemid == "267")
                {
                    e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + fAppUrl + "',980,450)\" title='" + fentname + "'>";
                    e.Item.Cells[2].Text += fEmpName + "</a>";
                }
                else
                {
                    e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + fAppUrl + "',980,780)\" title='" + fentname + "'>";
                    e.Item.Cells[2].Text += fEmpName + "</a>";
                }
            }
            if (fMeasure.Trim() == "3") //打回到下级
            {
                sScript = "showApproveWindow1('../webCheck/backPrevIdea.aspx?fMtypeId=" + fManageTypeId + "&flinkId=" + fLinkId + "',780,780)";
                //e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\">" + fEmpName + "</a>";
                e.Item.Cells[14].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\"><font color='#ff0000'>打回到下级</font></a>";
                //box.Enabled = false;
            }
            if (fMeasure.Trim() == "4") //被打回
            {
                if (fState.Trim() != "2")
                {
                    e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + fAppUrl + "',980,780)\">";
                    e.Item.Cells[2].Text += fEmpName + "</a>";
                    string erId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "erId"));
                    sScript = "showApproveWindow1('../webCheck/backPrevIdea.aspx?fMtypeId=" + fManageTypeId + "&flinkId=" + fLinkId + "&erId=" + erId + "',780,780)";
                    e.Item.Cells[14].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\"><font color='#ff0000'>被上级打回</font></a>";
                }
            }
            if (fMeasure.Trim() == "5") //已经上报
            {
                if (ea.FState == 6)
                {
                    sScript = "showApproveWindow('../AppQualiInfo/ShowAppInfo1.aspx?pid=" + pId + "&lid=" + fLinkId + "',830,500)";
                    e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\" title='" + fentname + "'>" + fEmpName + "</a>";
                    e.Item.Attributes.Add("onmouseover", "this.title='已经终审，不能批量审批'");
                }
                else
                {
                    sScript = "showApproveWindow('../AppQualiInfo/ShowAppInfo1.aspx?pid=" + pId + "&lid=" + fLinkId + "',830,500)";
                    e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\" title='" + fentname + "'>" + fEmpName + "</a>";
                    e.Item.Attributes.Add("onmouseover", "this.title='已经审核，不能批量审批'");
                }
                box.Enabled = false;
            }

            if (fState.Trim() == "2")
            {
                sScript = "showApproveWindow1('../webCheck/backIdea.aspx?aid=" + pId + "',780,780)";
                e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\" title='" + fentname + "'>" + fEmpName + "</a>";
                e.Item.Cells[14].Text = "打回企业";
                box.Enabled = false;
                e.Item.Attributes.Add("onmouseover", "this.title='已经打回企业，不能批量审批'");
            }

            if (fisQuali.Trim() == "1")
            {
                //box.Enabled = false;
                e.Item.Attributes.Add("onmouseover", "this.title='需要就位定级，不能批量审批'");
            }

            if (e.Item.Cells[15].Text.IndexOf("不") > -1)
            {
                e.Item.Cells[15].Text = "<font color='#ff0000'>" + e.Item.Cells[15].Text + "</font>";
            }

            //e.Item.Cells[2].Text = s + e.Item.Cells[2].Text;
            string fBaseInfoId = e.Item.Cells[e.Item.Cells.Count - 6].Text;

            sb.Remove(0, sb.Length);
            sb.Append(" select FBatchNoId from CF_App_AppBatchNo ");
            sb.Append(" where FAppId='" + pId + "' and FDFId ='" + this.Session["DFId"].ToString() + "'");
            sb.Append(" and fisdeleted=0 ");

            string fBatchNoId = rc.GetSignValue(sb.ToString());
            if (fBatchNoId != null && fBatchNoId != "&nbsp;")
            {
                sb.Remove(0, sb.Length);
                sb.Append(" fid='" + fBatchNoId + "'");
                e.Item.Cells[e.Item.Cells.Count - 11].Text = rc.GetSignValue(EntityTypeEnum.EaBatchNo, "FTtile", sb.ToString());
            }
            object froleId = DataBinder.Eval(e.Item.DataItem, "FRoleId");

            if (Convert.ToString(ViewState["DFRoleId"]).Trim('\'').IndexOf(froleId.ToString()) < 0)
            {
                e.Item.Cells[2].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEmpName"));
                e.Item.Cells[2].Enabled = false;
                e.Item.Cells[0].Enabled = false;
            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void btnOut_Click(object sender, EventArgs e)
    {
        string sql = this.Pager1.sql.ToString();
        DataTable dt = rc.GetTable(sql);
        if (dt != null)
        {
            this.JustAppInfo_List.DataSource = dt;
            this.JustAppInfo_List.DataBind();
            JustAppInfo_List.Columns[0].Visible = false;
            string fOutTitle = lPostion.Text;
            sab.SaveAsExc(this.JustAppInfo_List, fOutTitle, this.Response, "gb2312", 0);
        }
    }
    public void SaveAsExc(DataGrid DG_List, string Title, System.Web.HttpResponse Response, string tablehtml)
    {
        string sFileName = Title;
        string sHeadTittle = Title;
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "utf-8";  //设置了类型为中文防止乱码的出现  
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(sFileName, System.Text.Encoding.UTF8) + ".xls"); //定义输出文件和文件名 
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文 
        Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。  
        System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        oHtmlTextWriter.Write("<div align=center height=25 valign=middle><font size='6'>" + sHeadTittle + "</font></div><br>");
        oHtmlTextWriter.Write(tablehtml);
        DG_List.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        oHtmlTextWriter.Close();
        oStringWriter.Close();
        Response.End();
    }

  
}
