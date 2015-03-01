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
using System.Drawing;
using ProjectData;
public partial class Government_AppMain_GFTwoAuditList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    SaveAsBase sab = new SaveAsBase();
    RApp ra = new RApp();
    RAppBacth rap = new RAppBacth();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string abc = Request["fcol"].ToString();
            ShowPostion(); bindBatch(); ShowInfo();
        }
    }

    private void ShowPostion()
    {
        if (Request["fcol"] != null)
        {
            this.lPostion.Text = rc.GetMenuName(Request["fcol"].ToString());

        }
    }

    public void bindBatch()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * from CF_App_BatchNo ");
        sb.Append(" where FSystemID ='220'");
        sb.Append(" and FDFId='" + this.Session["DFId"].ToString() + "'");
        sb.Append(" and fisdeleted=0 ");
        sb.Append(" and isnull(fstate,0)<>1 ");
        ddBatch.DataSource = rc.GetTable(sb.ToString());
        ddBatch.DataValueField = "FID";
        ddBatch.DataTextField = "FTtile";
        ddBatch.DataBind();
        ddBatch.Items.Insert(0, new ListItem("--请选择--", ""));

        sb.Remove(0, sb.Length - 1);
        sb.Append(" select * from CF_App_BatchNo ");
        sb.Append(" where FSystemID ='220'");
        sb.Append(" and FDFId='" + this.Session["DFId"].ToString() + "'");
        sb.Append(" and fisdeleted=0 ");
        sb.Append(" and isnull(fstate,0)=0 ");
        ddBatchTime.DataSource = rc.GetTable(sb.ToString());
        ddBatchTime.DataValueField = "FID";
        ddBatchTime.DataTextField = "FTtile";
        ddBatchTime.DataBind();
        ddBatchTime.Items.Insert(0, new ListItem("--请选择--", ""));
    }

    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtFName.Text.Trim() != "" && this.txtFName.Text.Trim() != null)
        {
            sb.Append(" and ep.FEntName like '%" + this.txtFName.Text.Trim() + "%' ");
        }
        if (this.txtFPrjName.Text.Trim() != "" && this.txtFPrjName.Text.Trim() != null)
        {
            sb.Append(" and ep.FEmpName like '%" + this.txtFPrjName.Text.Trim() + "%' ");
        }

        if (Request.QueryString["fmanagetypeid"] != null)
        {
            if (Request.QueryString["fmanagetypeid"].IndexOf(",") > -1)
                sb.Append(" and ep.fmanagetypeid in (" + Request.QueryString["fmanagetypeid"] + ") ");
            else
                sb.Append(" and ep.fmanagetypeid='" + Request["fmanagetypeid"] + "'");
        }

        if (!string.IsNullOrEmpty(t_FListName.SelectedValue.Trim()))
        {
            sb.Append(" and b.FListName = '" + t_FListName.SelectedValue.Trim() + "'");

            if (t_FListName.SelectedValue == "其他")
            { sb.Append(" and b.FTypeName like '%" + t_FTypeName1.Text.Trim() + "%'"); }
            else if (!string.IsNullOrEmpty(t_FTypeName.SelectedValue.Trim()))
            {
                sb.Append(" and b.FTypeName = '" + t_FTypeName.SelectedValue.Trim() + "'");
            }
        }
        if (!string.IsNullOrEmpty(t_FUpDeptName.fNumber))
        {
            if (t_FUpDeptName.fNumber != "51")
                sb.Append(" and b.FUpDeptName='" + t_FUpDeptName.fNumber + "'");
        }
        if (!string.IsNullOrEmpty(ddBatch.SelectedValue))
        {
            sb.Append(" and ep.FLinkId in (select FAppId from CF_App_AppBatchNo where FBatchNoId='" + ddBatch.SelectedValue + "')");
        }
        if (dbSeeState.SelectedValue != "")
        {
            switch (dbSeeState.SelectedValue.Trim())
            {
                case "0": //未审核
                    sb.Append(" and er.FMeasure=0 ");
                    break;
                case "1": //准予受理
                    sb.Append(" and （ (er.FMeasure=5 and er.FResult=1) ");
                    //  case "3": 不准予受理
                    sb.Append(" or (ep.fstate=6 and er.FResult=3) ");
                    // case "5": 打回企业,重新上报
                    sb.Append(" or (ep.fstate=2) ）");
                    break;
            }
        }

        if (sb.Length > 0)
        {
            return sb.ToString();
        }
        else
        {
            return "";
        }
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from ( ");
        sb.Append(" select ep.FId,ep.FBaseInfoId,ep.FEntName,ep.FLinkId,ep.FEmpName,ep.FManageTypeId,ep.FListId,ep.FTypeId,ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FBarCode,");
        sb.Append(" case ep.fState when 1 then '审批中' when 2 then '打回企业' when 3 then '打回下级' ");
        sb.Append(" when 5 then '未审批' when 6 then '审批完成' end as FStatedesc,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");

        sb.Append(" ,er.FIdea,b.FUpDeptName,b.FListName,b.FTypeName ");

        sb.Append(" from CF_App_ProcessInstance ep inner join CF_App_ProcessRecord er");
        sb.Append(" on ep.fId = er.FProcessInstanceID ");

        sb.Append(" left join CF_App_List l on ep.FLinkId=l.FId ");
        sb.Append(" left join YW_GF_JBQK b on l.FId=b.YWBM");

        sb.Append(" inner join ( ");
        sb.Append(" select max(er.fid)fid from CF_App_ProcessInstance ep,CF_App_ProcessRecord er");
        sb.Append(" where ep.fId = er.FProcessInstanceID and ep.fsystemid=220 ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
        sb.Append(" and er.FtypeId=15 "); //存子流程类别 15复审
        sb.Append(" group by ep.flinkId )temp on er.fid=temp.fid where 1=1 ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
        sb.Append(" and er.FtypeId=15 "); //存子流程类别 15复审
        sb.Append(getCondi());
        sb.AppendLine(" ) ttt where 1=1 ");

        sb.AppendLine(" order by ttt.FReporttime desc,ttt.FBaseInfoId");

        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }

    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            string fMeasure = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FMeasure"));//e.Item.Cells[e.Item.Cells.Count - 3].Text;
            string fFResult = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFResult"));//e.Item.Cells[e.Item.Cells.Count - 8].Text;
            string fState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));//e.Item.Cells[e.Item.Cells.Count - 5].Text;
            string fSubFlowId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSubFlowId"));//e.Item.Cells[9].Text;
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));

            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();

            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["name"] = FLinkId;
            box.Attributes["fSubFlowId"] = fSubFlowId;

            string fColor = "", sUrl = "";
            if (fMeasure == "0")
            {
                sUrl = "AcceptInfoGF.aspx?ftype=15&FLinkId=" + FLinkId + "&fSubFlowId=" + fSubFlowId;
                e.Item.Cells[7].Text = "未审核";
                fColor = "#ff9900";
            }
            if (fMeasure == "5" && fFResult == "1")
            {
                //sUrl = "AcceptInfoGF.aspx?FLinkId=" + FLinkId;
                e.Item.Cells[7].Text = "审批通过";
                box.Enabled = false;
                fColor = "#339933";
            }
            EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance,
                "FBaseInfoId,FManageTypeId,fsystemid,FResult,FTime", "fid='" + fid + "'");
            EaSubFlow es = (EaSubFlow)rc.GetEBase(EntityTypeEnum.EaSubFlow, "", "fid='" + fSubFlowId + "'");
            if (fState == "6" && ea.FResult == "3") //流程结束并且不同意
            {

                //sUrl = "AcceptInfoGF.aspx?FLinkId" + FLinkId;
                e.Item.Cells[7].Text = "退回";
                box.Enabled = false;
                fColor = "#ff0000";
            }
            if (fState == "2")
            {
                //sUrl = "AcceptInfoGF.aspx?FLinkId" + FLinkId;
                e.Item.Cells[7].Text = "打回下级";
                box.Enabled = false;
                fColor = "#b6589d";
            }
            e.Item.Cells[2].Text = "<font color='" + fColor + "'>" + e.Item.Cells[2].Text + "</font>";
            if (!string.IsNullOrEmpty(sUrl))
                e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"javascript:showAddWindow('" + sUrl + "',800,600);\" >" + e.Item.Cells[2].Text + "</a>";

            string sql = string.Format(@" select FBatchNoId from CF_App_AppBatchNo 
                            where FAppId='" + FLinkId + "' and FDFId ='" + this.Session["DFId"].ToString()
                            + "' and fisdeleted=0 ");
            string fBatchNoId = rc.GetSignValue(sql);
            if (fBatchNoId != null && fBatchNoId != "&nbsp;")
            {
                e.Item.Cells[11].Text = rc.GetSignValue(EntityTypeEnum.EaBatchNo, "FTtile", "FID='" + fBatchNoId + "'");
            }
            sql = string.Format(@"select CONVERT(nvarchar(10),fbegindate,121) stime,
                            CONVERT(nvarchar(10),FEndDate,121) etime from CF_App_ProcessPublic 
                            where FBatchNoId='" + fBatchNoId + "' and FAppId='" + FLinkId + "'");
            DataTable dt = rc.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                e.Item.Cells[9].Text = dt.Rows[0]["stime"].ToString();
                e.Item.Cells[10].Text = dt.Rows[0]["etime"].ToString();
            }
            //专家评审
            sql = "select count(1) from YW_GF_Expert where Fappid='" + FLinkId + "' and isnull(isEnd,0)<>0 ";
            string cou = rc.GetSignValue(sql);
            e.Item.Cells[12].Text = "<a href='javascript:void(0)' onclick=\"javascript:showAddWindow('expertIdear.aspx?fappid=" + FLinkId
                + "',800,600);\" >查看(" + cou + ")</a>";
        }
    }
    protected void t_FListName_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindTypeName();
    }
    public void bindTypeName()
    {
        if (!string.IsNullOrEmpty(t_FListName.SelectedValue) && t_FListName.SelectedValue != "--请选择--")
        {
            if (t_FListName.SelectedValue == "房屋建筑工程")
            {
                t_FTypeName.Visible = true; t_FTypeName1.Visible = false;
                this.t_FTypeName.Items.Clear();
                this.t_FTypeName.Items.Insert(0, new ListItem("--请选择--", ""));
                this.t_FTypeName.Items.Insert(1, new ListItem("地基与基础", "地基与基础"));
                this.t_FTypeName.Items.Insert(2, new ListItem("主体结构", "主体结构"));
                this.t_FTypeName.Items.Insert(3, new ListItem("钢结构", "钢结构"));
                this.t_FTypeName.Items.Insert(4, new ListItem("装饰与屋面", "装饰与屋面"));
                this.t_FTypeName.Items.Insert(5, new ListItem("水电与智能", "水电与智能"));
                this.t_FTypeName.Items.Insert(6, new ListItem("其他", "其他"));
            }
            else if (t_FListName.SelectedValue == "土木工程")
            {
                t_FTypeName.Visible = true; t_FTypeName1.Visible = false;
                this.t_FTypeName.Items.Clear();
                this.t_FTypeName.Items.Insert(0, new ListItem("--请选择--", ""));
                this.t_FTypeName.Items.Insert(1, new ListItem("公路", "公路"));
                this.t_FTypeName.Items.Insert(2, new ListItem("铁路", "铁路"));
                this.t_FTypeName.Items.Insert(3, new ListItem("隧道", "隧道"));
                this.t_FTypeName.Items.Insert(4, new ListItem("桥梁", "桥梁"));
                this.t_FTypeName.Items.Insert(5, new ListItem("堤坝与电站", "堤坝与电站"));
                this.t_FTypeName.Items.Insert(6, new ListItem("矿山", "矿山"));
                this.t_FTypeName.Items.Insert(7, new ListItem("其他", "其他"));
            }
            else if (t_FListName.SelectedValue == "工业安装工程")
            {
                t_FTypeName.Visible = true; t_FTypeName1.Visible = false;
                this.t_FTypeName.Items.Clear();
                this.t_FTypeName.Items.Insert(0, new ListItem("--请选择--", ""));
                this.t_FTypeName.Items.Insert(1, new ListItem("工业设备", "工业设备"));
                this.t_FTypeName.Items.Insert(2, new ListItem("工业管道", "工业管道"));
                this.t_FTypeName.Items.Insert(3, new ListItem("电气装置与自动化", "电气装置与自动化"));
                this.t_FTypeName.Items.Insert(4, new ListItem("其他", "其他"));
            }
            else if (t_FListName.SelectedValue == "其他")
            {
                this.t_FTypeName.Visible = false; t_FTypeName1.Visible = true;
                t_FTypeName1.Text = null;
            }
        }
        else { t_FTypeName1.Text = null; t_FTypeName.Items.Clear(); }
    }

    protected void JustAppInfo_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FLinkId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            int fState = EConvert.ToInt(e.Item.Cells[e.Item.Cells.Count - 2].Text);
            if (e.CommandName == "See")
            {
                this.Session["FAppId"] = FLinkId;
                this.Session["FManageTypeId"] = "4000";
                Session["FIsApprove"] = 1;
                Response.Write("<script language='javascript'>window.open('../../GFEnt/AppMain/aIndex.aspx');</script>");
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
            sab.SaveAsExc(this.JustAppInfo_List, fOutTitle, this.Response);
        }
    }
    protected void btnAddPc_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (this.ddBatch.SelectedValue == null || string.IsNullOrEmpty(ddBatch.SelectedValue) || ddBatch.SelectedValue == "0")
        {
            tool.showMessage("请选择要加入的批次");
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.Append(" fid='" + ddBatch.SelectedValue.Trim() + "'");
        string sBatchNoState = rc.GetSignValue(EntityTypeEnum.EaBatchNo, "FState", sb.ToString());
        if (sBatchNoState == "1")
        {
            tool.showMessage("您选择的批次已经办结,不能再加入");
            return;
        }
        int cou = int.Parse(rc.GetSignValue("select count(1) from CF_App_BatchNo where FId='"
            + ddBatch.SelectedValue + "' and isnull(Fstime,'')='' and isnull(Fetime,'')='' "));
        if (cou <= 0)
        {
            tool.showMessage("您选择的批次已经设置了公示日期,不能再加入");
            return;
        }

        ArrayList array = new ArrayList();
        for (int i = 0; i < JustAppInfo_List.Items.Count; i++)
        {
            string fLinkId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Columns.Count - 1].Text;
            CheckBox box = (CheckBox)JustAppInfo_List.Items[i].Cells[0].Controls[1];
            if (box.Checked == true)
            {
                if (!array.Contains(fLinkId))
                {
                    array.Add(fLinkId);
                }
            }
        }
        if (array.Count == 0)
        {
            tool.showMessage("请选择要加入批次的数据");
            return;
        }

        StringBuilder sql = new StringBuilder();
        sql.Append(" begin ");
        for (int i = 0; i < array.Count; i++)
        {
            sb.Remove(0, sb.Length);
            sb.Append(" select fid from CF_App_AppBatchNo ");
            sb.Append(" where FAppId='" + array[i].ToString() + "'");
            sb.Append(" and FDFId='" + this.Session["DFId"].ToString() + "'");
            sb.Append(" and fisdeleted=0 ");
            string fid = rc.GetSignValue(sb.ToString());
            if (!string.IsNullOrEmpty(fid))
            {
                sql.Append(" update CF_App_AppBatchNo set FBatchNoId='" + ddBatch.SelectedValue + "' where FID='" + fid + "'");
            }
            else
            {
                sql.Append(@" insert CF_App_AppBatchNo (FID,FBatchNoId,FAppId,FDFId,FIsDeleted,FCreateTime)
                            values (newid(),'" + ddBatch.SelectedValue + "','" + array[i].ToString()
                         + "','" + this.Session["DFId"].ToString() + "',0,getdate() )");
            }
        }
        sql.Append(" end ");
        if (rc.PExcute(sql.ToString()))
        {
            tool.showMessage("加入批次成功");
            ShowInfo();
        }
        else
        {
            tool.showMessage("加入批次失败");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        tStime.Text = null; tEtime.Text = null;
        ddBatchTime.SelectedIndex = 0;
    }

    protected void btnPCTime_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (string.IsNullOrEmpty(tStime.Text) || string.IsNullOrEmpty(tEtime.Text) || string.IsNullOrEmpty(ddBatchTime.SelectedValue))
        { tool.showMessage("请选择批次和时间进行设定！"); return; }
        string sql = " exec JKC_PRO_BatchTime '" + tStime.Text.Trim() + "','" + tEtime.Text + "','" + ddBatchTime.SelectedValue + "' ";
        if (rc.PExcute(sql))
        {
            tool.showMessage("批次时间设置成功");
            bindBatch(); ShowInfo();
        }
        else
        {
            tool.showMessage("批次时间设置失败");
        }
    }

    protected void btnBackNext_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        ArrayList array = new ArrayList(); ArrayList arrayAppid = new ArrayList();
        for (int i = 0; i < JustAppInfo_List.Items.Count; i++)
        {
            string ProcessInstanceID = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Items[i].Cells.Count - 2].Text;
            string FAppid = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Items[i].Cells.Count - 1].Text;
            CheckBox cb = (CheckBox)JustAppInfo_List.Items[i].Cells[0].Controls[1];
            if (cb.Checked == true)
            { array.Add(FAppid); }
        }
        if (array.Count == 0)
        {
            tool.showMessage("请选择要打回的业务");
            return;
        }
        try
        {
            for (int i = 0; i < array.Count; i++)
            {
                BackToPre(array[i].ToString());
            }
            tool.showMessage("打回成功"); ShowInfo();
        }
        catch (Exception ex) { tool.showMessage("打回失败"); }
    }
    #region 打回到上级
    /// <summary>
    /// 打回到上级
    /// </summary>
    protected void BackToPre(string appid)
    {
        string FLinkId = appid;
        int fReportCount = EConvert.ToInt(rc.GetSignValue("select isnull(max(FReportCount),0) from cf_App_ProcessRecord where flinkId='" + FLinkId + "'"));//最大步骤
        fReportCount++;
        DataTable dt = rc.GetTable(EntityTypeEnum.EaProcessInstance, "FID", "flinkid='" + FLinkId + "' AND fstate=1");
        int iCount = dt.Rows.Count;
        if (iCount > 0)
        {
            ArrayList arrEn = new ArrayList();
            ArrayList arrSl = new ArrayList();
            ArrayList arrKey = new ArrayList();
            ArrayList arrSo = new ArrayList();
            for (int i = 0; i < iCount; i++)
            {
                string fProcessInstanceId = dt.Rows[i]["FID"].ToString();
                SortedList sl = new SortedList();
                SortedList sl1 = new SortedList();
                SortedList sl2 = new SortedList();
                DataTable dtTemp = rc.GetTable("select * from cf_App_ProcessRecord where FProcessinstanceId='" + fProcessInstanceId + "' ");
                string fSubFlowId = string.Empty;
                string fRoleId = string.Empty;
                if (dtTemp != null && dtTemp.Rows.Count > 0)//如果有这一步
                {
                    sl1.Clear();
                    DataRow drTemp = dtTemp.Rows[0];
                    sl1.Add("FID", System.Guid.NewGuid().ToString());
                    sl1.Add("FProcessInstanceID", fProcessInstanceId);
                    sl1.Add("FLinkId", drTemp["FLinkId"]);
                    sl1.Add("FRoleDesc", drTemp["FRoleDesc"]);
                    sl1.Add("FMeasure", 4);//被打回状态
                    sl1.Add("FReportTime", DateTime.Now);
                    sl1.Add("FRoleId", drTemp["FRoleId"]);
                    sl1.Add("FSubFlowId", drTemp["FSubFlowId"]);
                    fRoleId = drTemp["FRoleId"].ToString();//角色
                    fSubFlowId = drTemp["FSubFlowId"].ToString();//步骤
                    sl1.Add("FIsQuali", drTemp["FIsQuali"]);
                    sl1.Add("FIsPrint", drTemp["FIsPrint"]);
                    sl1.Add("FManageDeptId", drTemp["FManageDeptId"]);
                    sl1.Add("FDefineDay", drTemp["FDefineDay"]);
                    sl1.Add("FTypeId", drTemp["FTypeId"]);
                    sl1.Add("FOrder", drTemp["FOrder"]);
                    sl1.Add("FLevel", drTemp["FLevel"]);
                    sl1.Add("FIsDeleted", 0);
                    sl1.Add("FReportCount", fReportCount);//审核步骤
                    arrEn.Add(EntityTypeEnum.EaProcessRecord);
                    arrSl.Add(sl1);
                    arrKey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);//新插入一条 

                    string fnewId = rc.GetSignValue("select fid from cf_App_ProcessRecord where fprocessInstanceId='" + fProcessInstanceId
                        + "' and ftypeId='15' and froleId in(" + Session["DFRoleId"] + ") order by FReportCount desc");
                    if (!string.IsNullOrEmpty(fnewId))
                    {
                        sl2.Clear();
                        sl2.Add("FID", fnewId);
                        sl2.Add("FMeasure", 3);//标识为打回到上一步状态
                        sl2.Add("FAppPerson", rc.GetSignValue("select FLinkMan from cf_sys_user where fid='" + Session["DFUserId"] + "' "));
                        sl2.Add("FIdea", "打回");
                        arrEn.Add(EntityTypeEnum.EaProcessRecord);
                        arrSl.Add(sl2);
                        arrKey.Add("FID");
                        arrSo.Add(SaveOptionEnum.Update);
                    }

                }
                sl.Clear();
                sl.Add("FID", fProcessInstanceId);
                if (!string.IsNullOrEmpty(fSubFlowId))
                    sl.Add("FSubFlowId", fSubFlowId);
                if (!string.IsNullOrEmpty(fRoleId))
                    sl.Add("FRoleId", fRoleId);
                arrEn.Add(EntityTypeEnum.EaProcessInstance);
                arrSl.Add(sl);
                arrKey.Add("FID");
                arrSo.Add(SaveOptionEnum.Update);
            }
            StringBuilder sb = new StringBuilder();
            iCount = arrSo.Count;
            EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
            SaveOptionEnum[] sos = new SaveOptionEnum[iCount];
            string[] fkeys = new string[iCount];
            SortedList[] sls = new SortedList[iCount];

            for (int t = 0; t < iCount; t++)
            {
                ens[t] = (EntityTypeEnum)arrEn[t];
                sos[t] = (SaveOptionEnum)arrSo[t];
                fkeys[t] = (string)arrKey[t];

                sls[t] = new SortedList();
                sls[t] = (SortedList)arrSl[t];
            }
            rc.SaveEBaseM(ens, sls, fkeys, sos);
        }
    }
    #endregion

}