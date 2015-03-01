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
using ProjectData;
using System.Linq;

public partial class Government_AppMain_SGwaitForAppList : govBasePage
{
    RCenter rc = new RCenter();
    RQuali rq = new RQuali();
    SaveAsBase sab = new SaveAsBase();
    RAppBacth rap = new RAppBacth();
    ProjectDB db = new ProjectDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);


            showDDType(Request["fsystemid"]);

            ControlBind();
            ShowInfo();
            ShowPostion();
        }
    }

    private void ShowPostion()
    {
        if (Request["fcol"] != null && Request["fcol"] != "")
        {
            lPostion.Text = rc.GetMenuName(Request["fcol"]);
        }

    }

    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        string fDefaultManageDept = Session["DFId"].ToString();
        DataTable dt = rc.getAllupDeptId(fDefaultManageDept, 0, 3);
        dbFManageDeptId.DataSource = dt;
        dbFManageDeptId.DataTextField = "FName";
        dbFManageDeptId.DataValueField = "FNumber";
        dbFManageDeptId.DataBind();
        dbFManageDeptId.Items.Insert(0, new ListItem("--请选择--", ""));

        dbFManageDeptId.SelectedIndex = dbFManageDeptId.Items.IndexOf(dbFManageDeptId.Items.FindByValue(Session["DFId"].ToString()));
        if (EConvert.ToInt(this.Session["DFLevel"]) > 2)
        {
            dbFManageDeptId.Enabled = false;
        }
    }

    string GetMTypeList()
    {
        StringBuilder sb = new StringBuilder();
        if (dmType != null && dmType.Items.Count > 0)
        {
            foreach (ListItem item in dmType.Items)
            {
                if (!string.IsNullOrEmpty(item.Value) && item.Value != "0")
                {
                    if (sb.Length > 0)
                        sb.Append(",");
                    sb.Append(item.Value);
                }
            }
        }
        return sb.ToString();
    }
    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();

        if (Request.QueryString["fmanagetypeid"] != null)
        {
            if (Request.QueryString["fmanagetypeid"].IndexOf(",") > -1)
                sb.Append(" and ep.fmanagetypeid in (" + Request.QueryString["fmanagetypeid"] + ") ");
            else
                sb.Append(" and ep.fmanagetypeid='" + Request["fmanagetypeid"] + "'");
        }
        else
        {
            string fMType = GetMTypeList();
            if (string.IsNullOrEmpty(fMType))
                fMType = "-1";
            sb.Append(" and ep.FManageTypeId in (" + fMType + ") ");
        }

        if (this.txtFName.Text != "" && txtFName.Text != null)
        {
            sb.Append(" and ep.FEntName like '%" + txtFName.Text + "%' ");
        }
        if (this.txtFPrjName.Text != "" && txtFPrjName.Text != null)
        {
            sb.Append(" and ep.FEmpName like '%" + txtFPrjName.Text + "%' ");
        }
        //特殊处理
        if (this.dmType.SelectedValue.Trim() != "")
        {
            sb.Append(" and ep.FManageTypeId='" + dmType.SelectedValue + "'");
        }
        if (this.dbFManageDeptId.SelectedValue.Trim() != "")
        {
            sb.Append(" and ep.FManageDeptId like '" + dbFManageDeptId.SelectedValue + "%'");
        }
        if (Request["fsystemid"] != null && Request["fsystemid"] != "")
        {
            sb.Append(" and ep.FSystemId ='" + Request["fsystemid"] + "'");
        }

        if (dbSelfAppState.SelectedValue != "")
        {
            if (dbSelfAppState.SelectedValue == "0")
            {
                sb.Append(" and (er.FMeasure=0 or er.FMeasure=1 or er.FMeasure=3 or er.FMeasure=4) "); //未审核、被打回(4)
                sb.Append(" and ep.fstate<>2 "); //打回企业
            }
            if (dbSelfAppState.SelectedValue == "1")
            {
                sb.Append(" and er.FMeasure=5  "); //已经审批 
            }
            if (dbSelfAppState.SelectedValue == "2") //打回企业
            {
                sb.Append(" and ep.fstate=2   "); // 打回企业
            }
            if (dbSelfAppState.SelectedValue == "3") //打回到下级
            {
                sb.Append(" and er.FMeasure=3   "); // 打回到下级
            }
        }
        return sb.ToString();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("select * from ( ");
        sb.AppendLine("select *,(select top 1 d.FInt3 from CF_Prj_Data d where tt.FLinkId=d.FID ) FInt3 ");
        sb.AppendLine(" from ( ");
        sb.AppendLine("select ep.FId,er.FId erId,ep.FBaseInfoId,ep.FLinkId,ep.FEntName,ep.FEmpName,ep.FManageTypeId,ep.FListId,er.FTypeId,er.FRoleId,");
        sb.AppendLine(" ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,ep.FManageDeptId,");
        sb.AppendLine(" ep.FState,ep.FSeeState,ep.FSeeTime,");
        sb.AppendLine(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime,");
        sb.AppendLine(" er.fisQuali,ep.FLeadName,ep.FLeadId,ep.FEmpId");
        sb.AppendLine(" from CF_App_ProcessInstance ep inner join CF_App_ProcessRecord er");
        sb.AppendLine(" on ep.fId = er.FProcessInstanceID ");
        sb.AppendLine(" inner join ( ");
        sb.AppendLine(" select max(er.fid) fid from CF_App_ProcessInstance ep,CF_App_ProcessRecord er");
        sb.AppendLine(" where ep.fId = er.FProcessInstanceID ");
        sb.AppendLine(" and er.FRoleId in (" + Session["DFRoleId"] + ")");
        sb.AppendLine(" and ep.FManageDeptId like '" + Session["DFId"] + "%' ");
        sb.AppendLine(" and (er.ftypeid=" + Request.QueryString["ftypeid"].ToString() + ")");
        sb.AppendLine(getCondi());
        sb.AppendLine(" group by ep.flinkId,er.FMeasure)temp on temp.fid=er.fid where 1=1 ");
        sb.AppendLine(" and er.FRoleId in (" + Session["DFRoleId"] + ")");
        sb.AppendLine(" and ep.FManageDeptId like '" + Session["DFId"] + "%' ");
        sb.AppendLine(" and (er.ftypeid=" + Request.QueryString["ftypeid"] + ")");
        sb.AppendLine(getCondi());
        if (dbSelfAppState.SelectedValue == "2"
         || string.IsNullOrEmpty(dbSelfAppState.SelectedValue)
         || dbSelfAppState.SelectedValue == "1") //打回企业
        {
            sb.AppendLine(" union ");
            sb.AppendLine(" select ep.FId,er.FId erId,ep.FBaseInfoId,ep.FLinkId,ep.FEntName,ep.FEmpName,ep.FManageTypeId,ep.FListId,er.FTypeId,er.FRoleId,");
            sb.AppendLine(" ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,ep.FManageDeptId,");
            sb.AppendLine(" ep.FState,ep.FSeeState,ep.FSeeTime,");
            sb.AppendLine(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime,");
            sb.AppendLine(" er.fisQuali,ep.FLeadName,ep.FLeadId,ep.FEmpId ");
            sb.AppendLine(" from CF_App_ProcessInstanceBackup ep inner join CF_App_ProcessRecordBackup er");
            sb.AppendLine(" on ep.fId = er.FProcessInstanceID ");
            sb.AppendLine(" inner join ( ");
            sb.AppendLine(" select max(er.fid) fid from CF_App_ProcessInstanceBackup ep,CF_App_ProcessRecordBackup er");
            sb.AppendLine(" where ep.fId = er.FProcessInstanceID ");
            sb.AppendLine(" and er.FRoleId in (" + Session["DFRoleId"] + ")");
            sb.AppendLine(" and ep.FManageDeptId like '" + Session["DFId"] + "%' ");
            sb.AppendLine(" and (er.ftypeid=" + Request.QueryString["ftypeid"] + ")");
            sb.AppendLine(getCondi());
            sb.AppendLine(" group by ep.flinkId,er.FMeasure)temp on temp.fid=er.fid where 1=1 ");
            sb.AppendLine(" and er.FRoleId in (" + Session["DFRoleId"] + ")");
            sb.AppendLine(" and ep.FManageDeptId like '" + Session["DFId"] + "%' ");
            sb.AppendLine(" and (er.ftypeid=" + Request.QueryString["ftypeid"] + ")");
            sb.AppendLine(getCondi());
        }
        sb.AppendLine(")tt ) ttt where 1=1 ");

        if (!string.IsNullOrEmpty(t_FInt3.SelectedValue))
        {
            sb.Append(" and FInt3='" + t_FInt3.SelectedValue + "' ");
        }

        ViewState["DFRoleId"] = Session["DFRoleId"];//审批角色 
        sb.AppendLine(" order by ttt.FReporttime desc,ttt.FBaseInfoId");

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
        if (FSystemId == null || FSystemId == "")
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(" select fname,fnumber from CF_Sys_ManageType ");
        sb.AppendLine(" where FSystemId='" + FSystemId + "' and fmtypeid<>'0'  ");
        if (!string.IsNullOrEmpty(Request.QueryString["FManageTypeId"]))
        {
            sb.AppendLine(" and fnumber=" + EConvert.ToInt(Request.QueryString["FManageTypeId"]));
        }
        sb.AppendLine(" order by forder");
        DataTable dt = rc.GetTable(sb.ToString());
        this.dmType.DataSource = dt;
        this.dmType.DataTextField = "fname";
        this.dmType.DataValueField = "fnumber";
        this.dmType.DataBind();
        this.dmType.Items.Insert(0, new ListItem("全部", ""));
        this.ViewState["FSystemId"] = FSystemId;

    }

    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string pId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fLinkId"));
            string fState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fState"));
            string fMeasure = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fMeasure"));

            string fisQuali = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FisQuali"));
            string fAppResult = e.Item.Cells[e.Item.Cells.Count - 2].Text;

            //合同类型
            string FInt3 = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FInt3"));
            e.Item.Cells[4].Text = db.getManageTypeName(FInt3);
            e.Item.Cells[4].Text = e.Item.Cells[4].Text.Replace("合同备案", "").Replace("确认", "");


            switch (fAppResult)
            {
                case "1":
                    e.Item.Cells[e.Item.Cells.Count - 2].Text = "通过";
                    break;

                case "3":
                    e.Item.Cells[e.Item.Cells.Count - 2].Text = "不通过";
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
            string fManageTypeId = ea.FManageTypeId;
            string FEmpName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEmpName"));


            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string sScript = "showApproveWindow('../AppQualiInfo/ShowAppInfo1.aspx?pid=" + pId + "&lid=" + fLinkId + "',830,500)";
            sScript = "";

            string frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + fbid + "'");
            string fsystemid = ea.FSystemId;


            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];

            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["name"] = pId;
            box.Attributes["fisQuali"] = fisQuali;
            box.Attributes["prjName"] = FEmpName;
            string fEmpName = e.Item.Cells[2].Text;
            object fentname = DataBinder.Eval(e.Item.DataItem, "FEntName");
            //查询查询地址
            string sUrl = "";


            sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber='" + fsystemid + "'"); ;

            sUrl += "?fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1";
            e.Item.Cells[6].Text = "<a class='link7' href='" + sUrl + "' target='_blank'>" + e.Item.Cells[6].Text + "</a>";

            //建设单位
            //得到FLeadId的企业ID的SystemId
            if (fmid == "294")
            {
                sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=155 ");
                JustAppInfo_List.Columns[5].HeaderText = "设计单位";
            }
            else
            {
                sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=100 ");

            }
            string FLeadId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLeadId"));
            if (!string.IsNullOrEmpty(FLeadId))
            {
                sUrl += "?fbid=" + EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLeadId"));
                e.Item.Cells[5].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + sUrl + "',980,450)\"  >" + e.Item.Cells[5].Text + "</a>";
            }

            string DataFID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEmpId"));

            e.Item.Cells[3].Text = db.getDeptFullName(db.CF_Prj_Data.Where(t => t.FId == DataFID).Select(t => t.FInt0).FirstOrDefault());

            //sUrl = rc.getMTypeQurl(ea.FManageTypeId);
            //sUrl += "?fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1";
            //e.Item.Cells[3].Text = "<a class='link7' href='" + sUrl + "' target='_blank'>" + e.Item.Cells[3].Text + "</a>";

            //审批地址
            string fAppUrl = rc.getMTypeAurl(fmid);
            fAppUrl += "?lid=" + fLinkId + "&p=" + fMeasure + "&ftypeid=" + Request.QueryString["ftypeid"].ToString();

            if (fMeasure.Trim() == "0" || fMeasure == "3") //未审核
            {
                e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + fAppUrl + "',980,780)\" >";
                e.Item.Cells[2].Text += fEmpName + "</a>";
                if (fState.Trim() == "2")
                { }
                else
                {
                    e.Item.Cells[e.Item.Cells.Count - 2].Text = "未填写审批意见";
                }
            }

            else if (fMeasure.Trim() == "1") //已经填写意见
            {
                e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + fAppUrl + "',980,780)\"  >"
                      + e.Item.Cells[2].Text + "</a>";
            }
            else if (fMeasure.Trim() == "3") //打回到下级
            {
                sScript = "showApproveWindow1('../webCheck/backPrevIdea.aspx?fMtypeId=" + fManageTypeId + "&flinkId=" + fLinkId + "',780,780)";

                e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\"><font color='#ff0000'>打回到下级</font></a>";
            }
            else if (fMeasure.Trim() == "4") //被打回
            {
                if (fState.Trim() != "2")
                {
                    e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + fAppUrl + "',980,780)\">";
                    e.Item.Cells[2].Text += fEmpName + "</a>";
                    string erId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "erId"));
                    sScript = "showApproveWindow1('../webCheck/backPrevIdea.aspx?fMtypeId=" + fManageTypeId + "&flinkId=" + fLinkId + "&erId=" + erId + "',780,780)";
                    e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\"><font color='#ff0000'>被上级打回</font></a>";
                }
            }
            else if (fMeasure.Trim() == "5") //已经上报
            {
                sScript = "showApproveWindow('../AppQualiInfo/ShowAppInfo1.aspx?pid=" + pId + "&lid=" + fLinkId + "',830,500)";
                e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\" >"
                                  + e.Item.Cells[e.Item.Cells.Count - 2].Text + "</a>";

                string fUrl = rc.getMTypeQurl(ea.FManageTypeId); ;

                sScript = "openWinNew('" + fUrl + "?sysid=" + fsystemid + "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + Session["DFUserId"].ToString() + "')";

                e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\" >";

                e.Item.Cells[2].Text += fEmpName + "</a>";
                box.Enabled = false;
            }

            if (fState.Trim() == "2")
            {
                string fUrl = rc.getMTypeQurl(ea.FManageTypeId); ;

                sScript = "openWinNew('" + fUrl + "?sysid=" + fsystemid + "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + Session["DFUserId"].ToString() + "')";

                e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\" >";

                e.Item.Cells[2].Text += fEmpName + "</a>";

                sScript = "showApproveWindow1('../webCheck/backIdea.aspx?aid=" + pId + "&flinkId=" + fLinkId + "',780,780)";
                e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\" >打回企业</a>";

                box.Enabled = false;
            }
            if (e.Item.Cells[e.Item.Cells.Count - 2].Text.IndexOf("不") > -1)
            {
                e.Item.Cells[e.Item.Cells.Count - 2].Text = "<font color='#ff0000'>" + e.Item.Cells[e.Item.Cells.Count - 2].Text + "</font>";
            }
            //查询该项目是否变更 
            if (!string.IsNullOrEmpty(fLinkId))
            {
                string fPrjId = db.CF_App_List.Where(t => t.FId == fLinkId).Select(t => t.FPrjId).FirstOrDefault();
                if (!string.IsNullOrEmpty(fPrjId))
                {
                    var prjBG = db.CF_Prj_BaseInfo.Where(t => t.FId == fPrjId)
                        .Select(t => new { t.FBGTime, t.FCount })
                        .FirstOrDefault();
                    if (prjBG != null && prjBG.FCount > 0)
                    {
                        e.Item.Cells[2].Text += "<br/><font color='#000000;'>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")</font>";
                    }
                }
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

    private void BatchApp(string pager)
    {
        int iCount = this.JustAppInfo_List.Items.Count;
        ArrayList array = new ArrayList();
        ArrayList array1 = new ArrayList();
        int c = 0;
        for (int i = 0; i < iCount; i++)
        {
            string pId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Items[i].Cells.Count - 1].Text;
            string fisQuali = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Items[i].Cells.Count - 8].Text;
            CheckBox cb = (CheckBox)JustAppInfo_List.Items[i].Cells[0].Controls[1];
            if (cb.Checked == true)
            {
                if (fisQuali != "1")
                {
                    string fManageTypeId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Items[i].Cells.Count - 4].Text;
                    array1.Add(fManageTypeId);
                    array.Add(pId);
                }
                else
                {
                    JustAppInfo_List.Items[i].BackColor = System.Drawing.Color.FromArgb(252, 222, 164);
                    c++;
                }
            }
        }
        if (array.Count == 0)
        {
            string s = "<script>alert('请选择要批量审批的项')</script>";
            if (c > 0)
            {
                s = "<script>alert('您选择的项中要先发放证书')</script>";
            }
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), s);
            return;
        }

        for (int j = 0; j < array.Count - 1; j++)
        {
            if (array1[j].ToString().Trim() != array1[j + 1].ToString().Trim())
            {
                pageTool tool = new pageTool(this.Page);
                tool.showMessage("请选择相同业务类型的数据");
                return;
            }
        }
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < array.Count; i++)
        {
            if (i == 0)
            {
                sb.Append(array[i].ToString());
            }
            else
            {
                sb.Append("," + array[i].ToString());
            }
        }
        if (sb.Length > 0)
        {

            // ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "click", "app('" + pager + "')", true);
            StringBuilder sScript = new StringBuilder();
            sScript.Append("var obj = new Object();");
            sScript.Append("var tmpVal='';");
            sScript.Append("obj.name='51js';");
            sScript.Append(" tmpVal ='" + sb.ToString() + "';");
            sScript.Append(" obj.id= tmpVal;");
            sScript.Append(" ShowWindow('" + pager + "',700,600,obj);");

            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), sScript.ToString());
        }
    }


    protected void btnBatchApp_Click(object sender, EventArgs e)
    {
        BatchApp("../AppQualiInfo/BackUpBatchApp.aspx?e=0&k=3");
    }

    protected void btnBatchBack_Click(object sender, EventArgs e)
    {
        BatchApp("../AppQualiInfo/BackEntBatchApp1.aspx?e=0");
    }


}
