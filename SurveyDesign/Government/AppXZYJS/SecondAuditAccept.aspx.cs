using Approve.Common;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleApp;
using Approve.RuleCenter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyXZYJS_AuditMain_SecondAuditAccept : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    SaveAsBase sab = new SaveAsBase();
    RApp ra = new RApp();
    RAppBacth rap = new RAppBacth();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           // string abc = Request["fcol"].ToString();
            //ShowPostion(); 
             ShowInfo();
            // uegovdeptid.fNumber = "-1";
        }
    }

    private void ShowPostion()
    {
        if (Request["fcol"] != null)
        {
            this.lPostion.Text = rc.GetMenuName(Request["fcol"].ToString());

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
        sb.Append(" case ep.FManageTypeId when '4050' then '房建' when '4060' then '市政' end as AuditType,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");

        //sb.Append(" ,er.FIdea,b.FUpDeptName,b.FListName,b.FTypeName ");
        sb.Append(" ,er.FIdea,b.XMSDMC,b.ID,b.YWBM,b.XMMC,b.JSDWMC,b.BH,b.JSDZ");
        sb.Append(" from CF_App_ProcessInstance ep inner join CF_App_ProcessRecord er");
        sb.Append(" on ep.fId = er.FProcessInstanceID ");

        sb.Append(" left join CF_App_List l on ep.FLinkId=l.FId ");
        sb.Append(" left join YW_XZYJS b on l.FId=b.YWBM");

        sb.Append(" inner join ( ");
        sb.Append(" select max(er.fid)fid from CF_App_ProcessInstance ep,CF_App_ProcessRecord er");
        sb.Append(" where ep.fId = er.FProcessInstanceID and ep.fsystemid=1122  and ep.FManageTypeId in('4050','4060')");
        //sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ep.FManageDeptId = '" + Session["DFId"].ToString() + "' ");
        sb.Append(" and er.FtypeId=5 "); //存子流程类别 5复审
        sb.Append(" group by ep.flinkId )temp on er.fid=temp.fid where 1=1 ");
        //sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        //sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
        //sb.Append(" and er.FtypeId=5 "); //存子流程类别 5复审
        sb.Append(Condition());
        sb.AppendLine(" ) ttt where 1=1 ");

        sb.AppendLine(" order by ttt.FReporttime desc,ttt.FBaseInfoId");
        //txtXMMC.Text = sb.ToString();
        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }
    private string Condition()
    {
        StringBuilder _builer = new StringBuilder();
        if (!string.IsNullOrEmpty(txtXMMC.Text))
            _builer.AppendFormat(" AND b.XMMC like '%{0}%'", txtXMMC.Text);
        if (!string.IsNullOrEmpty(txtBH.Text))
            _builer.AppendFormat(" AND b.BH like '%{0}%'", txtBH.Text);
        if (!string.IsNullOrEmpty(txtJSDW.Text))
            _builer.AppendFormat(" AND b.JSDWMC like '%{0}%'", txtJSDW.Text);
        if (!string.IsNullOrEmpty(txtStart.Text))
            _builer.AppendFormat(" AND CONVERT(varchar(100), ep.FReportDate, 23) >= '{0}'", txtStart.Text);
        if (!string.IsNullOrEmpty(txtEnd.Text))
            _builer.AppendFormat(" AND CONVERT(varchar(100), ep.FReportDate, 23) <= '{0}'", txtEnd.Text);
        if (dbSeeState.SelectedValue != "")
        {
            if (dbSeeState.SelectedValue == "0") //未审核
                _builer.Append(" and er.FMeasure=0 ");
            else if (dbSeeState.SelectedValue == "1")//初审已通过fMeasure == "5" && fFResult == "1"
                _builer.Append(" and er.FMeasure=5 and er.FResult=1");
            else //初审未通过
                _builer.Append(" and er.FMeasure=5 and er.FResult in(2,3)");
            //switch (dbSeeState.SelectedValue.Trim())
            //{
            //    case "0": //未审核
            //        _builer.Append(" and er.FMeasure=0 ");
            //        break;
            //    case "1": //准予受理
            //        _builer.Append(" and ((er.FMeasure=5 and er.FResult=1) ");
            //        //  case "3": 不准予受理
            //        _builer.Append(" or (ep.fstate=6 and er.FResult=3) ");
            //        // case "5": 打回企业,重新上报
            //        _builer.Append(" or (ep.fstate=2))");
            //        break;
            //}
        }
        return _builer.ToString();
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
            string Id = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ID"));
            string FManageTypeId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageTypeId"));
            string YWBM = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "YWBM"));
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();

            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["name"] = FLinkId;
            box.Attributes["fSubFlowId"] = fSubFlowId;
            box.Attributes["YJS_ID"] = Id;
            string fColor = "", sUrl = "";
            if (fMeasure == "0")
            {
                if (FManageTypeId == "4050")//房建
                    sUrl = "../../JSDW/ApplyXZYJS/AppMain/index.aspx?YJS_GUID=" + Id + "&fAppId=" + YWBM + "&audit=1";
                else//市政
                    sUrl = "../../JSDW/ApplyXZYJSSZ/AppMain/index.aspx?YJS_GUID=" + Id + "&fAppId=" + YWBM + "&audit=1";
                
                e.Item.Cells[8].Text = "未复审";
                fColor = "#ff9900";
            }
            else if (fMeasure == "5" && fFResult == "1")
            {
                //sUrl = "AcceptInfoGF.aspx?FLinkId=" + FLinkId;
                e.Item.Cells[8].Text = "审批通过";
                box.Enabled = false;
                fColor = "#339933";
            }
            else if (fMeasure == "5" && fFResult == "3")
            {
                e.Item.Cells[8].Text = "复审未通过";
                box.Enabled = false;
                fColor = "#ff0000";
            }
            else if (fState == "6" && fFResult == "3") //流程结束并且不同意
            {

                //sUrl = "AcceptInfoGF.aspx?FLinkId" + FLinkId;
                e.Item.Cells[8].Text = "退回";
                box.Enabled = false;
                fColor = "#ff0000";
            }
            else if (fState == "2")
            {
                //sUrl = "AcceptInfoGF.aspx?FLinkId" + FLinkId;
                e.Item.Cells[8].Text = "打回下级";
                box.Enabled = false;
                fColor = "#b6589d";
            }
            e.Item.Cells[3].Text = "<font color='" + fColor + "'>" + e.Item.Cells[3].Text + "</font>";
            if (!string.IsNullOrEmpty(sUrl))
                e.Item.Cells[3].Text = "<a href='javascript:void(0)' onclick=\"javascript:showAddWindow('" + sUrl + "',1000,600);\" >" + e.Item.Cells[3].Text + "</a>";

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