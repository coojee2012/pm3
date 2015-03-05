using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Approve.EntityBase;
using System.Collections;
using System.Data;
using Approve.Common;
using Approve.RuleCenter;
using Approve.RuleApp;
using ProjectData;

public partial class Government_AppMain_JNCLTwoAuditList : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Approve.Common.SaveAsBase sab = new Approve.Common.SaveAsBase();
    RApp ra = new RApp(); Share sh = new Share();
    RAppBacth rap = new RAppBacth();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // ShowPostion(); 
            //bindBatch(); 
            BindControl();
            ShowInfo();
        }
    }
    private void ShowPostion()
    {
        if (Request["fcol"] != null)
        {
            //this.lPostion.Text = rc.GetMenuName(Request["fcol"].ToString());

        }
    }
    private void BindControl()
    {
        var CPLB = db.getDicList("2001303");
        ddlCPLB.DataSource = CPLB;
        ddlCPLB.DataValueField = "fnumber";
        ddlCPLB.DataTextField = "fname";
        ddlCPLB.DataBind();
        ddlCPLB.Items.Insert(0, new ListItem() { Selected = true, Text = "--请选择--", Value = "0" });
    }
    public void bindBatch()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * from CF_App_BatchNo ");
        sb.Append(" where FSystemID ='1122'");
        sb.Append(" and FDFId='" + this.Session["DFId"].ToString() + "'");
        sb.Append(" and fisdeleted=0 ");
        sb.Append(" and fstate<>1 ");
        //ddBatch.DataSource = rc.GetTable(sb.ToString());
        //ddBatch.DataValueField = "FID";
        //ddBatch.DataTextField = "FTtile";
        //ddBatch.DataBind();
        //ddBatch.Items.Insert(0, new ListItem("--请选择--", ""));
    }

    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtFName.Text.Trim() != "" && this.txtFName.Text.Trim() != null)
        {
            sb.Append(" and c.QYMC like '%" + this.txtFName.Text.Trim() + "%' ");
        }
        if (!string.IsNullOrEmpty(txtCPMC.Text))
            sb.AppendFormat(" and b.SQCPMC like '%{0}%'", txtCPMC.Text);
        if (ddlCPLB.SelectedValue != "0")
            sb.AppendFormat(" and b.CPLBMC ='{0}'", ddlCPLB.SelectedValue);
        //if (Request.QueryString["fmanagetypeid"] != null)
        //{
        //    if (Request.QueryString["fmanagetypeid"].IndexOf(",") > -1)
        //        sb.Append(" and ep.fmanagetypeid in (" + Request.QueryString["fmanagetypeid"] + ") ");
        //    else
        //        sb.Append(" and ep.fmanagetypeid='" + Request["fmanagetypeid"] + "'");
        //}        
        if (dbSeeState.SelectedValue != "")
        {
            switch (dbSeeState.SelectedValue.Trim())
            {
                case "0": //未审核
                    sb.Append(" and er.FMeasure=0 ");
                    break;
                case "1": //准予受理
                    sb.Append(" and ( (er.FMeasure=5 and er.FResult=1) ");
                    //  case "3": 不准予受理
                    sb.Append(" or (ep.fstate=6 and er.FResult=3) ");
                    // case "5": 打回企业,重新上报
                    sb.Append(" or ep.fstate=2 )");
                    break;
            }
        }
        //if (!string.IsNullOrEmpty(t_Stime.Text))
        //{ sb.Append(" and CONVERT(nvarchar(10),er.FReporttime,121) >= '" + t_Stime.Text + "'"); }
        //if (!string.IsNullOrEmpty(t_Stime.Text))
        //{ sb.Append(" and CONVERT(nvarchar(10),er.FReporttime,121) <= '" + t_Etime.Text + "'"); }
        //if (!string.IsNullOrEmpty(ddBatch.SelectedValue))
        //{
        //    sb.Append(" and ep.FLinkId in (select FAppId from CF_App_AppBatchNo where FBatchNoId='" + ddBatch.SelectedValue + "')");
        //}
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

        sb.Append(" ,er.FIdea,l.FName ywName,b.SQCPMC,b.CPLBMC,b.BSDJMC,c.QYMC,b.YWBM ");

        sb.Append(" from CF_App_ProcessInstance ep inner join CF_App_ProcessRecord er");
        sb.Append(" on ep.fId = er.FProcessInstanceID ");

        sb.Append(" left join CF_App_List l on ep.FLinkId=l.FId ");
        sb.Append(" left join YW_JNCL_PRODUCT b on l.FId=b.YWBM");
        sb.Append(" left join YW_JNCL_QYJBXX c on l.FId=c.YWBM");
        sb.Append(" inner join ( ");
        sb.Append(" select max(er.fid)fid from CF_App_ProcessInstance ep,CF_App_ProcessRecord er");
        sb.Append(" where ep.fId = er.FProcessInstanceID and ep.fsystemid=1122 and ep.FManageTypeId='4001' ");
        //sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
        sb.Append(" and er.FtypeId=15 "); //存子流程类别5复审
        sb.AppendFormat(" group by ep.flinkId )temp on er.fid=temp.fid where b.FType={0} and c.FType={0} ",FType);
        //sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        //sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
        //sb.Append(" and er.FtypeId=5 "); //存子流程类别 5复审
        sb.Append(getCondi());
        sb.AppendLine(" ) ttt where 1=1 ");

        sb.AppendLine(" order by ttt.FReporttime desc,ttt.FBaseInfoId");
        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
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
            //  string fOutTitle = lPostion.Text;
            // sab.SaveAsExc(this.JustAppInfo_List, fOutTitle, this.Response);
        }
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
            string YWBM = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "YWBM"));
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();

            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["name"] = FLinkId;
            box.Attributes["fSubFlowId"] = fSubFlowId;
            box.Attributes["YWBM"] = YWBM;
            string fColor = "", sUrl = "";
            if (fMeasure == "0")
            {
                sUrl = "AcceptInfoJNCL.aspx?ftype=10&FLinkId=" + FLinkId + "&fSubFlowId=" + fSubFlowId;
                e.Item.Cells[7].Text = "未复审";
                fColor = "#ff9900";
            }
            if (fMeasure == "5" && fFResult == "1")
            {
                //sUrl = "AcceptInfoGF.aspx?FLinkId=" + FLinkId;
                e.Item.Cells[7].Text = "审批通过";
                box.Enabled = false;
                fColor = "#339933";
            }
            if (fState == "6" && fFResult == "3") //流程结束并且不同意
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
            e.Item.Cells[2].Text = "<font color='" + fColor + "'>" + e.Item.Cells[3].Text + "</font>";
            if (!string.IsNullOrEmpty(sUrl))
                e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"javascript:showAddWindow('" + sUrl + "',800,600);\" >" + e.Item.Cells[2].Text + "</a>";


            //            string sql = string.Format(@" select FBatchNoId from CF_App_AppBatchNo 
            //                            where FAppId='" + FLinkId + "' and FDFId ='" + this.Session["DFId"].ToString()
            //                            + "' and fisdeleted=0 ");
            //            string fBatchNoId = rc.GetSignValue(sql);
            //            if (fBatchNoId != null && fBatchNoId != "&nbsp;")
            //            {
            //                e.Item.Cells[7].Text = rc.GetSignValue(EntityTypeEnum.EaBatchNo, "FTtile", "FID='" + fBatchNoId + "'");
            //            }           
        }
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
                this.Session["FManageTypeId"] = "4001";
                Session["FIsApprove"] = 1;
                Response.Write("<script language='javascript'>window.open('../../JNCLEnt/AppMain/aIndex.aspx');</script>");
            }
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
        int cou = int.Parse(rc.GetSignValue("select count(1) from CF_App_BatchNo where FId='"
            + ddBatch.SelectedValue + "' and isnull(Fstime,'')='' and isnull(Fetime,'')='' "));
        if (cou <= 0)
        {
            tool.showMessage("您选择的批次已经设置了公示日期,不能再加入");
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
        if (sh.PExcute(sql.ToString()))
        {
            tool.showMessage("加入批次成功");
            ShowInfo();
        }
        else
        {
            tool.showMessage("加入批次失败");
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
                        + "' and ftypeId='10' and froleId in(" + Session["DFRoleId"] + ") order by FReportCount desc");
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

    private string FType
    {
        get
        {
            return Request.QueryString["FType"];
        }
    }

}