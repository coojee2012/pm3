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
using Approve.Common;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.EntityQuali;
using Approve.RuleApp;
using Approve.RuleCenter;
using System.Text;
using Approve.PersistBase;
using ProjectBLL;

public partial class Government_AppMain_seeOneReportInfo : govBasePage
{
    RCenter rc = new RCenter();
    RQuali rq = new RQuali();
    RApp ra = new RApp();
    ArrayList arrCon = new ArrayList();
    private string sTemp = "";
    private StringBuilder sScript = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
    }

    private void ShowInfo()
    {
        if (HFID1.Value == "")
        {
            return;
        }
        string[] strs = HFID1.Value.Split(',');
        if (strs == null || strs.Length <= 0)
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" select ep.FId,ep.FBaseInfoId,ep.FLinkId,ep.FEntName,ep.FManageTypeId,");
        sb.Append(" ep.FListId,ep.FTypeId,ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FSystemId,");
        sb.Append(" case ep.fState when 1 then '上报审核中' when 2 then '打回企业' when 3 then '打回下级' ");
        sb.Append(" when 5 then '未审核证书' when 6 then '审核完成' end as FStatedesc,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.fid frid,er.FReporttime");
        sb.Append(" from CF_App_ProcessInstance ep,CF_App_ProcessRecord er");
        sb.Append(" where ep.fId = er.FProcessInstanceID ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
        sb.Append(" and er.FtypeId=1 and isnull(er.FMeasure,0) in (0,4) "); //存子流程类别 1受理 //未受理
        if (EConvert.ToString(Request.QueryString["type"]) == "p")
        {
            sb.Append(" and ep.Fid in (");
        }
        else
        {
            sb.Append(" and ep.flinkid in (");
        }
        for (int i = 0; i < strs.Length; i++)
        {
            if (i == 0)
            {
                sb.Append("'" + strs[i] + "'");
            }
            else
            {
                sb.Append(",'" + strs[i] + "'");
            }
        }


        sb.Append(" ) ");
        sb.Append(" order by ep.fLinkId, er.FReporttime desc ");

        DataTable dt = rc.GetTable(sb.ToString());
        DG_List.DataSource = dt;
        DG_List.DataBind();

        if (HIsPsotBack.Value == "0")
        {
            //ControlBind(); 
            txtFSeeTime.Text = DateTime.Now.ToShortDateString();

            HIsPsotBack.Value = "1";
        }
        object fuserId = Session["DFUserId"];
        sb.Remove(0, sb.Length);
        sb.Append(" select FLinkMan,fcompany,FFunction,FTel,FDepartmentID ");
        sb.Append(" from cf_sys_user where fid='" + fuserId + "'");
        dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            t_FAppPerson.Text = dt.Rows[0]["FLinkMan"].ToString();
            t_FAppPersonJob.Text = dt.Rows[0]["FFunction"].ToString();
            t_FAppPersonUnit.Text = RBase.GetDepartmentName(dt.Rows[0]["FDepartmentID"].ToString()) + RBase.GetDepartmentName(dt.Rows[0]["FCompany"].ToString());
        }

    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1).ToString();
            string pId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fsSeeState = e.Item.Cells[e.Item.Cells.Count - 8].Text;
            string fLinkId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            string fMeasure = e.Item.Cells[e.Item.Cells.Count - 3].Text;
            string fManageTypeId = e.Item.Cells[3].Text;
            string fSubFlowId = e.Item.Cells[9].Text;
            string fListId = e.Item.Cells[4].Text;
            string fTypeId = e.Item.Cells[5].Text;
            string fLevelId = e.Item.Cells[6].Text;
            string fsystemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSystemId"));
            e.Item.Cells[3].Text = rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + fManageTypeId + "'");
            e.Item.Cells[9].Text = rc.GetSignValue(EntityTypeEnum.EaSubFlow, "FName", "FId='" + fSubFlowId + "'");

            if (fListId == "" || fListId == "&nbsp;")
            {
                fListId = "''";
            }
            if (fTypeId == "" || fTypeId == "&nbsp;")
            {
                fTypeId = "''";
            }
            if (fLevelId == "" || fLevelId == "&nbsp;")
            {
                fLevelId = "''";
            }


            e.Item.Cells[4].Text = "";
            fListId = rc.GetDicRemark(fListId);
            if (fListId != "")
            {
                e.Item.Cells[4].Text += fListId; //+ "<br>";
            }
            fTypeId = rc.GetDicRemark(fTypeId);
            if (fTypeId != "")
            {
                e.Item.Cells[4].Text += fTypeId; //+ "<br>";
            }

            if (fLevelId != "")
            {
                fLevelId = rc.GetSignValue(EntityTypeEnum.EsQualiLevel, "FName", "FNumber=" + fLevelId);
                if (string.IsNullOrEmpty(fLevelId))
                {
                    if (!string.IsNullOrEmpty(e.Item.Cells[3].Text))
                    {
                        e.Item.Cells[4].Text += e.Item.Cells[3].Text;
                    }
                }
                else
                {
                    if (fLevelId.Trim() == "所有等级")
                    {
                        if (!string.IsNullOrEmpty(e.Item.Cells[3].Text))
                        {
                            e.Item.Cells[4].Text += e.Item.Cells[3].Text;
                        }
                    }
                    else
                    {
                        e.Item.Cells[4].Text += fLevelId;
                    }
                }
            }



            string linkId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FBaseInfoId,FManageTypeId,fsystemid", "fid='" + pId + "'");
            if (ea == null)
            {
                return;
            }
            string fbid = ea.FBaseInfoId;
            string faid = linkId;
            string fmid = ea.FManageTypeId;
            string fEntName = e.Item.Cells[2].Text;
            //查询查询地址
            string sUrl = "";
            //sUrl = rc.getSysQurl(ea.FSystemId);
            //sUrl += "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fuid=" + Session["DFUserId"].ToString();
            //string fDesc = "查询企业审核通过的信息,首次申请的企业没有数据";
            //e.Item.Cells[2].Text = "<a class='link5' href='" + sUrl + "' target='_blank' title='" + fDesc + "'>" + e.Item.Cells[2].Text + "</a>";
            sUrl = rc.GetSignValue("select FQurl from cf_Sys_SystemName where fnumber='8814'");
            if (!string.IsNullOrEmpty(sUrl))
            {
                sUrl += "?sysid=" + fsystemId + "&FBaseId=" + fbid + "&faid=" + faid + "&fmid=" + fManageTypeId + "&fly=1";
                //e.Item.Cells[2].Text = "<a class='link7' href='" + sUrl + "' target='_blank'>" + e.Item.Cells[2].Text + "</a>";
            }
            //sUrl = rc.getMTypeQurl(ea.FManageTypeId);
            //sUrl += "?fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + Session["DFUserId"].ToString();
            //e.Item.Cells[2].Text = e.Item.Cells[2].Text;


            sUrl = "AppDetails.aspx?flinkid=" + faid + "&fbid=" + fbid;

            e.Item.Cells[e.Item.Cells.Count - 10].Text = "<a href='" + sUrl + "' class='link5' target='_blank'>相关表格维护</a>";
            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Checked = false;
            string FMeasure = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FMeasure"));
            if (FMeasure == "0")
            {
                box.Checked = true;
                box.ToolTip = "未接件";
            }
            if (FMeasure == "4")
            {
                box.Checked = true;
                box.ToolTip = "被打回";
            }
            if (FMeasure == "5" && EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFResult")) == "1")
            {
                box.ToolTip = "准予受理";
                box.Checked = false;
            }
            if (EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState")) == "6" && EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFResult")) == "3")
            {
                box.ToolTip = "不准予受理";
                box.Checked = false;
            }
            if (EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState")) == "2")
            {
                box.ToolTip = "打回企业,重新上报";
                box.Checked = false;
            }

            if (e.Item.Cells[15].Text == "1900-01-01")
            {
                e.Item.Cells[15].Text = "";
            }
        }
    }

    private void SaveAllInfo()
    {

        int iCount = this.DG_List.Items.Count;
        ArrayList aProcessRecord = new ArrayList();

        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();

        //sb.Append(" fid='" + dbFBatchNoId.SelectedValue.Trim() + "'");
        //string sBatchNoState = rc.GetSignValue(EntityTypeEnum.EaBatchNo, "FState", sb.ToString());
        //if (sBatchNoState == "1")
        //{
        //    tool.showMessage("您选择的批次已经办结,不能再加入");
        //    return;
        //}

        sb.Remove(0, sb.Length);

        sb.Append(" select FLinkMan,fcompany,FFunction,FTel from cf_sys_user");
        sb.Append(" where fid='" + Session["DFUSerId"].ToString() + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt == null || dt.Rows.Count == 0)
        {
            return;
        }

        string fLegalDay = "0";
        string fConsentEndTime = "1900-01-01";

        ArrayList linkList = new ArrayList();
        ArrayList legalDayList = new ArrayList();
        ArrayList consentEndTimeList = new ArrayList();


        ArrayList arrEn = new ArrayList();
        ArrayList arrSl = new ArrayList();
        ArrayList arrFKey = new ArrayList();
        ArrayList arrSo = new ArrayList();

        bool isAddBath = false;
        //if (this.dbFBatchNoId.SelectedValue == "")
        //{
        //    isAddBath = false;
        //}


        // 上报电子监察
        string[] R_FManageTypeId = new string[iCount];
        string[] R_FBaseInfoId = new string[iCount];
        string[] R_FAppId = new string[iCount];
        string[] R_FProcessinstanceId = new string[iCount];
        string[] R_Matter = new string[iCount];

        string R_FTime = DateTime.Now.ToString();
        string BZ = "";
        string[] R_XMName = new string[iCount];
        string R_FManagerId = Session["DFUSerId"].ToString();
        string R_SFJS = "0";
        string R_Cause = "";
        string R_bType = "0";

        //记录接见
        StringBuilder sbDescription = new StringBuilder();


        string sTemp = "";
        for (int i = 0; i < iCount; i++)
        {
            string pId = DG_List.Items[i].Cells[DG_List.Columns.Count - 1].Text;
            string linkId = DG_List.Items[i].Cells[DG_List.Columns.Count - 2].Text;
            string fRId = DG_List.Items[i].Cells[DG_List.Columns.Count - 3].Text;
            string fbaseinfoid = DG_List.Items[i].Cells[DG_List.Columns.Count - 6].Text;
            string fmanagetypeid = DG_List.Items[i].Cells[DG_List.Columns.Count - 4].Text;

            CheckBox box = (CheckBox)DG_List.Items[i].Cells[0].Controls[1];

            if (box.Checked == true)
            {
                SortedList sl = new SortedList();
                DateTime fPlanTime = EConvert.ToDateTime(rc.GetSignValue(EntityTypeEnum.EaProcessInstance, "FPlanTime", "FId='" + pId + "'"));
                if (fPlanTime == null || fPlanTime == DateTime.MinValue)//如果为空，求出计划时间
                {
                    sb.Remove(0, sb.Length);
                    string fManageTypeId = DG_List.Items[i].Cells[DG_List.Columns.Count - 5].Text;
                    sb.Append("select isnull(FDay,0) from cf_sys_ManageType where fnumber='" + fManageTypeId + "'");
                    fLegalDay = rc.GetSignValue(sb.ToString());
                    fConsentEndTime = new RCommon().GetEndTime(DateTime.Now, EConvert.ToInt(fLegalDay)).ToString("yyyy-MM-dd");
                    sl.Add("FPlanTime", fConsentEndTime);
                }
                sl.Add("FID", pId);
                sl.Add("FSeeState", 1);
                if (txtFSeeTime.Text == "")
                {
                    sl.Add("FSeeTime", DateTime.Now);
                }
                else
                {
                    sl.Add("FSeeTime", txtFSeeTime.Text);
                }

                arrSl.Add(sl);
                arrSo.Add(SaveOptionEnum.Update);
                arrFKey.Add("FID");
                arrEn.Add(EntityTypeEnum.EaProcessInstance);

                if (isAddBath) //需要加入批次
                {
                    sl = new SortedList();
                    sb.Remove(0, sb.Length);
                    sb.Append(" select fid from CF_App_AppBatchNo ");
                    sb.Append(" where FAppId ='" + pId + "' and FDFId='" + Session["DFId"].ToString() + "'");

                    string fBathId = rc.GetSignValue(sb.ToString());
                    if (fBathId == null || fBathId == "")
                    {
                        arrSo.Add(SaveOptionEnum.Insert);
                        sl.Add("FID", Guid.NewGuid().ToString());
                        //sl.Add("FBatchNoId", dbFBatchNoId.SelectedValue);
                        sl.Add("FAppId", pId);
                        sl.Add("FDFId", Session["DFId"].ToString());
                        sl.Add("FIsDeleted", 0);
                        sl.Add("FCreateTime", DateTime.Now);
                    }
                    else
                    {
                        arrSo.Add(SaveOptionEnum.Update);
                        sl.Add("FID", fBathId);
                        //sl.Add("FBatchNoId", dbFBatchNoId.SelectedValue);
                        sl.Add("FDFId", Session["DFId"].ToString());
                    }
                    arrSl.Add(sl);
                    arrFKey.Add("FID");
                    arrEn.Add(EntityTypeEnum.EaAppBatchNo);
                }

                sl = new SortedList();
                EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FRoleId,FLinkId,FReportDate", "fid='" + pId + "'");
                sl.Add("FID", fRId);
                sl.Add("FProcessInstanceId", pId);
                sl.Add("FAppPerson", string.IsNullOrEmpty(t_FAppPerson.Text.Trim()) ? dt.Rows[0]["FLinkMan"].ToString() : t_FAppPerson.Text);
                if (txtFSeeTime.Text == "")
                {
                    sl.Add("FAppTime", DateTime.Now);
                }
                else
                {
                    sl.Add("FAppTime", txtFSeeTime.Text);
                }
                sl.Add("FCompany", string.IsNullOrEmpty(t_FAppPersonUnit.Text.Trim()) ? dt.Rows[0]["fcompany"].ToString() : t_FAppPersonUnit.Text.Trim());
                sl.Add("FFunction", string.IsNullOrEmpty(t_FAppPersonJob.Text.Trim()) ? dt.Rows[0]["FFunction"].ToString() : t_FAppPersonJob.Text.Trim());
                sl.Add("FIdea", "同意接件");
                sl.Add("FLevel", EConvert.ToInt(Session["DFLevel"].ToString()));
                sl.Add("FLinkId", ea.FLinkId);
                sl.Add("FMeasure", 5);//dResult.SelectedValue);
                sl.Add("FResult", "1");
                sl.Add("FUserId", Session["DFUserId"].ToString());

                DateTime fReportTime = ea.FReportDate;
                DateTime nowTime = DateTime.Now;
                TimeSpan spanTime = nowTime - fReportTime;
                sl.Add("FWaiteTime", spanTime.Days);
                aProcessRecord.Add(sl);

                //上报电子监察

                int j = 0;
                R_FAppId[j] = pId;
                R_FBaseInfoId[j] = fbaseinfoid;
                R_FProcessinstanceId[j] = pId;
                R_FManageTypeId[j] = fmanagetypeid;
                sb.Remove(0, sb.Length);

                sb.Append(" select ep.fentname 企业名称,");
                sb.Append(" (select top 1 fname from cf_sys_managetype where fnumber = ep.fmanagetypeid) 业务类型,");
                sb.Append(" (select top 1 fremark from cf_sys_dic where fnumber = ep.flistid)+");
                sb.Append(" (select top 1 fremark from cf_sys_dic where fnumber = ep.ftypeid)+");
                sb.Append(" (select top 1 fname from cf_sys_qualilevel where fnumber = ep.flevelid) 申报内容");

                sb.Append(" from CF_App_ProcessInstance ep where ep.fid='" + pId + "'");

                DataTable dt1 = rc.GetTable(sb.ToString());
                if (dt1.Rows.Count > 0)
                {
                    R_Matter[j] = dt.Rows[0][2].ToString();
                    R_XMName[j] = dt.Rows[0][1].ToString();
                }
                else
                {
                    R_Matter[j] = "";
                    R_XMName[j] = "";
                }

                j++;

                if (sTemp != linkId)
                {
                    linkList.Add(linkId);
                    legalDayList.Add(fLegalDay);
                    consentEndTimeList.Add(fConsentEndTime);
                    sTemp = linkId;
                }

                //记录接见相关信息
                sbDescription.AppendFormat(" ProcessId:{0}", pId);
                sbDescription.AppendFormat(" ,FLinkId:{0}", linkId);
                sbDescription.AppendFormat(" ,FManageTypeId:{0}", fmanagetypeid);
                sbDescription.AppendFormat(" ,FBaseInfoId:{0}", fbaseinfoid);
            }
        }
        if (arrSl.Count > 0)
        {
            iCount = arrSl.Count;

            EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
            string[] fkeys = new string[iCount];
            SaveOptionEnum[] sos = new SaveOptionEnum[iCount];
            SortedList[] sls = new SortedList[iCount];

            for (int i = 0; i < iCount; i++)
            {
                ens[i] = (EntityTypeEnum)arrEn[i];
                fkeys[i] = (string)arrFKey[i];
                sos[i] = (SaveOptionEnum)arrSo[i];
                sls[i] = new SortedList();
                sls[i] = (SortedList)arrSl[i];
            }

            rc.SaveEBaseM(ens, sls, fkeys, sos);

            iCount = aProcessRecord.Count;
            if (iCount > 0)
            {
                SortedList[] sl = new SortedList[iCount];
                for (int i = 0; i < iCount; i++)
                {
                    sl[i] = new SortedList();
                    sl[i] = (SortedList)aProcessRecord[i];
                }

                ra.BatchAppKCSJ(sl);
            }

            //如果登录用户是省级用户更新申请表(县、市不更新)
            if (EConvert.ToInt(Session["DFLevel"]) == 1)
            {
                if (linkList.Count > 0)
                {
                    SortedList[] sDetail = new SortedList[linkList.Count];
                    for (int i = 0; i < linkList.Count; i++)
                    {
                        sDetail[i] = new SortedList();
                        sDetail[i].Add("FLinkId", linkList[i].ToString());
                        sDetail[i].Add("FAcceptState", 1);
                        sDetail[i].Add("FAcceptUnit", "省建设厅");
                        sDetail[i].Add("FAcceptWindow", dt.Rows[0]["fcompany"].ToString());
                        sDetail[i].Add("FAcceptPerson", dt.Rows[0]["FLinkMan"].ToString());
                        sDetail[i].Add("FAcceptPersonTel", dt.Rows[0]["FTel"].ToString());
                        sDetail[i].Add("FAppCount", iCount);


                        if (txtFSeeTime.Text == "")
                        {
                            sDetail[i].Add("FAcceptTime", DateTime.Now);
                        }
                        else
                        {
                            sDetail[i].Add("FAcceptTime", txtFSeeTime.Text);
                        }
                        sDetail[i].Add("FLegalDay", legalDayList[i]);
                        sDetail[i].Add("FConsentEndTime", consentEndTimeList[i]);
                        sDetail[i].Add("FConsentDay", legalDayList[i]);

                    }
                    ra.SeeAppDetail(sDetail);
                }
            }

            //subProvinceCenter scp = new subProvinceCenter();
            //scp.setPrinceCenter(linkList, Session["DFUserId"].ToString());
             
            tool.showMessageAndRunFunction("接件成功", "window.returnValue='1';");
            btnAccept.Enabled = false;
            btnSave.Disabled = true;

            //记录接见操作
            string title = "接件操作，操作人(DFUserId):" + EConvert.ToString(Session["DFUserId"]);
            string description = title + " =》相关数据" + sbDescription.ToString();
            DataLog.Write(LogType.Info, LogSort.Operation, title, description);
            ShowInfo();
        }
        else
        {
            tool.showMessage("您所选的已经处理完毕");
            ShowInfo();
        }
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        ShowInfo();
        SaveAllInfo();
        ShowInfo();
    }

    protected void btnShowInfo_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>window.returnValue='1';window.close();</script>");
    }


}
