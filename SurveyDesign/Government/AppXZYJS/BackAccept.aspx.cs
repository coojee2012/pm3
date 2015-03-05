using Approve.Common;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleApp;
using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyXZYJS_AuditMain_BackAccept : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    RCenter rq = new RCenter();
    RApp ra = new RApp();
    ArrayList arrCon = new ArrayList();
    private string sTemp = "";
    private StringBuilder sScript = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HFID1.Value = FLinkId;
            CehckSession();
            ShowInfo();
        }
    }
    private string FLinkId
    {
        get
        {
            return Request.QueryString["FLinkId"];
        }
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
        sb.Append(" select ep.FId,ep.FBaseInfoId,ep.FLinkId,ep.FEntName,ep.FManageTypeId,ep.FListId,ep.FTypeId,ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,");
        sb.Append(" case ep.fState when 1 then '上报审核中' when 2 then '打回企业' when 3 then '打回下级' ");
        sb.Append(" when 5 then '未审核证书' when 6 then '审核完成' end as FStatedesc,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FId FERFId,er.FResult FFResult,er.FAppTime,er.FMeasure");
        sb.Append(" from CF_App_ProcessInstance ep,CF_App_ProcessRecord er");
        sb.Append(" where ep.fId = er.FProcessInstanceID and ep.FSubFLowId=er.FSubFLowId ");
        //sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ep.FManageDeptId = '" + Session["DFId"].ToString() + "' ");
        sb.AppendFormat(" and er.FtypeId={0} and isnull(er.FMeasure,0) in (0,4) ",TypeId); //存子流程类别 1接件 //未接件 
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
        sb.Append(" order by ep.fLinkId, ep.FReportDate desc ");

        DataTable dt = rc.GetTable(sb.ToString());
        DG_List.DataSource = dt;
        DG_List.DataBind();
    }
    private void CehckSession()
    {
        StringBuilder sb = new StringBuilder();
        if (Session["DFSystemId"] == null || Session["DFName"] == null ||
            Session["DFUserId"] == null || Session["DFRoleId"] == null ||
            Session["DFId"] == null || Session["DFLevel"] == null)
        {
            string sDeptNumber = ComFunction.GetDefaultDept();
            sb.Append("<script>");
            sb.Append("alert('Session过期,请重新登录!');");
            switch (sDeptNumber)
            {
                case "21":
                    sb.Append("parent.close();window.open('../../ApproveWeb/lnmain/AppBackLogin.aspx','','');");
                    //sb.Append("parent.frames['left'].src='left123.aspx';");
                    break;
                case "36":
                    sb.Append("parent.close();window.open('../../ApproveWeb/jxmain/AppBackLogin.aspx','','');");
                    break;
            }

            sb.Append("</script>");
            this.Response.Write(sb.ToString());
            this.Response.End();
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
            string fManageTypeId = e.Item.Cells[3].Text;
            string fSubFlowId = e.Item.Cells[9].Text;
            string fListId = e.Item.Cells[4].Text;
            string fTypeId = e.Item.Cells[5].Text;
            string fLevelId = e.Item.Cells[6].Text;

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
                e.Item.Cells[4].Text += fListId;// +"<br>";
            }
            fTypeId = rc.GetDicRemark(fTypeId);
            if (fTypeId != "")
            {
                e.Item.Cells[4].Text += fTypeId;// +"<br>";
            }
            fLevelId = rc.GetSignValue(EntityTypeEnum.EsQualiLevel, "FName", "FNumber=" + fLevelId);
            if (fLevelId != "")
            {
                e.Item.Cells[4].Text += fLevelId;// +"<br>";
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
            string frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + fbid + "'");

            string fMeasure = e.Item.Cells[e.Item.Cells.Count - 3].Text;

            string fisNew = ea.FIsNew.ToString();

            string fIsEnd = rc.GetSignValue(EntityTypeEnum.EaSubFlow, "FIsEnd", "fid='" + ea.FSubFlowId + "'");




            string fEntName = e.Item.Cells[2].Text;
            //查询查询地址
            string sUrl = "";
            sUrl = rc.getSysQurl(ea.FSystemId);
            sUrl += "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid;
            string fDesc = "查询企业审核通过的信息,首次申请的企业没有数据";
            //e.Item.Cells[2].Text = "<a class='link5' href='" + sUrl + "' target='_blank' title='" + fDesc + "'>" + e.Item.Cells[2].Text + "</a>";

            sUrl = rc.getMTypeQurl(ea.FManageTypeId);
            sUrl += "?fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1";
            //e.Item.Cells[3].Text = "<a class='link7' href='" + sUrl + "' target='_blank'>" + e.Item.Cells[3].Text + "</a>";


            sUrl = "AppDetails.aspx?flinkid=" + faid + "&fbid=" + fbid;

            e.Item.Cells[e.Item.Cells.Count - 9].Text = "<a href='" + sUrl + "' class='link5' target='_blank'>相关表格维护</a>";

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

    protected string ReturnString(out string erIds)
    {
        int iCount = this.DG_List.Items.Count;
        ArrayList array = new ArrayList();
        ArrayList array1 = new ArrayList();
        for (int i = 0; i < iCount; i++)
        {
            string pId = DG_List.Items[i].Cells[DG_List.Items[i].Cells.Count - 1].Text;
            string erId = DG_List.Items[i].Cells[DG_List.Items[i].Cells.Count - 4].Text;
            CheckBox cb = (CheckBox)DG_List.Items[i].Cells[0].Controls[1];
            if (cb.Checked == true)
            {
                array.Add(pId);
                array1.Add(erId);
            }
        }
        if (array.Count == 0)
        {
            this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>alert('您所选的已经处理完毕')</script>");
            erIds = string.Empty;
            return "";
        }
        StringBuilder sb = new StringBuilder();
        StringBuilder sb1 = new StringBuilder();
        for (int i = 0; i < array.Count; i++)
        {
            if (i > 0)
            {
                sb.Append(",");
                sb1.Append(",");
            }
            sb.Append(array[i].ToString());
            sb1.Append(array1[i].ToString());
        }
        erIds = sb1.ToString();
        return sb.ToString();
    }

    protected void btnShowInfo_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>window.returnValue='1';window.close();</script>");
    }
    /// <summary>
    /// 新退件打回企业
    /// </summary>
    private void BatchApp()
    {
        ProjectDB db = new ProjectDB();
        if (DG_List.Items.Count <= 0)
        {
            pageTool tool = new pageTool(this.Page);
            tool.showMessage("没有数据，无法处理");
        }
        else
        {
            string backIdea = t_FAppIdea.Text;
            backIdea = backIdea.Length > 200 ? backIdea.Substring(0, 200) : backIdea;
            if (HFID1.Value == "")
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            StringBuilder sbDescription = new StringBuilder();//记录该操作的关键信息



            string[] strs = HFID1.Value.Split(',');
            int iCount = strs.Length;
            sb.Append("select fid,fbaseInfoId from CF_App_list where fid in (");
            for (int i = 0; i < iCount; i++)
            {
                if (i == 0)
                {
                    sb.Append("'" + strs[i].Trim() + "'");
                }
                else
                {
                    sb.Append(",'" + strs[i].Trim() + "'");
                }
            }
            sb.Append(") ");
            DataTable dtFLinkIds = rq.GetTable(sb.ToString());
            if (dtFLinkIds != null && dtFLinkIds.Rows.Count > 0)
            {
                iCount = dtFLinkIds.Rows.Count;
                for (int ii = 0; ii < iCount; ii++)
                {
                    string FLinkId = EConvert.ToString(dtFLinkIds.Rows[ii][0]);
                    string fbaseInfoId = EConvert.ToString(dtFLinkIds.Rows[ii][1]);
                    //查询是否有办结的，如果没有，可以打回到企业
                    sb.Remove(0, sb.Length);
                    sb.Append("select count(*) from (");
                    sb.Append(" select fid from cf_App_ProcessInstanceBackup where fstate=6 and flinkId='" + FLinkId + "'");//已经办结的流程
                    sb.Append(" union ");
                    sb.Append(" select fid from cf_App_ProcessInstance where fstate=6 and flinkId='" + FLinkId + "'");//已经办结的流程
                    sb.Append(")tt");
                    iCount = rc.GetSQLCount(sb.ToString());
                    if (iCount > 0)//如果有办结的流程，不可打回 |继续下一轮
                        continue;
                    //否则
                    sb.Remove(0, sb.Length);
                    sb.Append("select ep.fId,er.FId erFId,'" + Session["DFUserId"] + "' FUserId ");
                    sb.Append(" from cf_App_ProcessInstance ep ");
                    sb.Append(" inner join cf_App_ProcessRecord er on ep.FId=er.FProcessInstanceId ");
                    sb.Append(" where ep.FSubFlowId=er.FSubFlowId ");
                    //sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
                    sb.Append(" and ep.FManageDeptId = '" + Session["DFId"].ToString() + "' ");
                    sb.AppendFormat(" and er.FtypeId={0} and isnull(er.FMeasure,0) in (0,4) ",TypeId); //存子流");
                    sb.Append(" and ep.FState<>6 and ep.FLinkId='" + FLinkId + "'");
                    DataTable dt = rc.GetTable(sb.ToString());
                    if (ra.BatchBack(dt, FLinkId, backIdea))
                    {
                        //记录打回 企业 操作
                        string title = "打回企业操作 操作人(DFUserId)：" + Session["DFUserId"];
                        string description = "关键key:FLinkId" + FLinkId + " backIdea:" + backIdea;
                        DataLog.Write(LogType.Info, LogSort.Operation, title, description);
                    }


                    //保存到CF_App_Idea
                    CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == FLinkId).FirstOrDefault();
                    if (idea == null)
                    {
                        idea = new CF_App_Idea()
                        {
                            FId = Guid.NewGuid().ToString(),
                            FCreateTime = DateTime.Now,
                            FLinkId = FLinkId,
                            FIsdeleted = 0,
                        };
                        db.CF_App_Idea.InsertOnSubmit(idea);
                    }
                    idea.FReportCount = db.CF_App_List.Where(t => t.FId == FLinkId).Select(t => t.FReportCount).FirstOrDefault();
                    idea.FResult = "打回";
                    idea.FResultInt = 2;
                    idea.FContent = t_FAppIdea.Text;
                    idea.FAppTime = DateTime.Now;
                    idea.FUserId = EConvert.ToString(Session["DFUserId"]);
                    idea.FTime = DateTime.Now;
                }
                db.SubmitChanges();
            }
        }
    }


    private void BatchEndApp()
    {
        ProjectDB db = new ProjectDB();
        if (DG_List.Items.Count <= 0)
        {
            pageTool tool = new pageTool(this.Page);
            tool.showMessage("没有数据，无法处理");
        }
        else
        {
            string backIdea = t_FAppIdea.Text;
            backIdea = backIdea.Length > 200 ? backIdea.Substring(0, 200) : backIdea;

            StringBuilder sb = new StringBuilder();
            string erIds = string.Empty;
            string s = ReturnString(out erIds);
            if (s.Trim() == "")
            {
                return;
            }
            string[] strs = s.Trim().Split(',');
            string[] strs1 = erIds.Trim().Split(',');
            if (strs.Length <= 0)
            {
                return;
            }

            int iCount = strs.Length;

            ArrayList arrSl = new ArrayList();
            ArrayList arrEn = new ArrayList();
            ArrayList arrKey = new ArrayList();
            ArrayList arrSo = new ArrayList();
            ArrayList array = new ArrayList();
            ArrayList arrLink = new ArrayList();

            for (int i = 0; i < iCount; i++)
            {
                array.Add(strs[i]);

                SortedList sl = new SortedList();
                sl.Add("FID", strs[i]);
                sl.Add("FSeeState", 3);
                sl.Add("FSeeTime", DateTime.Now);

                arrSl.Add(sl);
                arrEn.Add(EntityTypeEnum.EaProcessInstance);
                arrKey.Add("FID");
                arrSo.Add(SaveOptionEnum.Update);
                if (strs1.Length > i)
                {
                    sl = new SortedList();
                    sl.Add("FID", strs1[i]);
                    sl.Add("FUserId", Session["DFUserId"]);

                    arrSl.Add(sl);
                    arrEn.Add(EntityTypeEnum.EaProcessRecord);
                    arrKey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Update);
                }
                sb.Remove(0, sb.Length);
                sb.Append(" select flinkid from CF_App_ProcessInstance  ");
                sb.Append(" where fid='" + strs[i] + "'");

                string fLinkId = rc.GetSignValue(sb.ToString());
                if (!arrLink.Contains(fLinkId))
                {
                    arrLink.Add(fLinkId);
                }
                //龚成龙【2010-03-16】
                rc.PExcute("update CF_App_ProcessRecord set FAppTime=getdate() where fprocessInstanceId='" + strs[i] + "' and FTypeId=1");//修改退件的时间

            }
            iCount = arrLink.Count;
            for (int j = 0; j < iCount; j++)
            {
                sb.Remove(0, sb.Length);
                sb.Append(" select fid from CF_App_AcceptBook ");
                sb.Append(" where flinkid='" + arrLink[j].ToString() + "'");

                string fId = rc.GetSignValue(sb.ToString());
                if (fId != null && fId != "")
                {
                    SortedList sl = new SortedList();
                    sl.Add("FID", fId);
                    sl.Add("FState", 13);
                    arrSl.Add(sl);
                    arrEn.Add(EntityTypeEnum.EaAcceptBook);
                    arrKey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Update);
                }

                string FLinkId = arrLink[j].ToString();
                //保存到CF_App_Idea
                CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == FLinkId).FirstOrDefault();
                if (idea == null)
                {
                    idea = new CF_App_Idea()
                    {
                        FId = Guid.NewGuid().ToString(),
                        FCreateTime = DateTime.Now,
                        FLinkId = FLinkId,
                        FIsdeleted = 0,
                    };
                    db.CF_App_Idea.InsertOnSubmit(idea);
                }
                idea.FReportCount = db.CF_App_List.Where(t => t.FId == FLinkId).Select(t => t.FReportCount).FirstOrDefault();
                idea.FResult = "打回";
                idea.FResultInt = 2;
                idea.FContent = t_FAppIdea.Text;
                idea.FAppTime = DateTime.Now;
                idea.FUserId = EConvert.ToString(Session["DFUserId"]);
                idea.FTime = DateTime.Now;
            }
            db.SubmitChanges();

            iCount = arrSo.Count;

            SortedList[] sls = new SortedList[iCount];
            EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
            string[] fkeys = new string[iCount];
            SaveOptionEnum[] sos = new SaveOptionEnum[iCount];
            for (int k = 0; k < iCount; k++)
            {
                sls[k] = new SortedList();
                sls[k] = (SortedList)arrSl[k];
                ens[k] = (EntityTypeEnum)arrEn[k];
                fkeys[k] = (string)arrKey[k];
                sos[k] = (SaveOptionEnum)arrSo[k];
            }
            rc.SaveEBaseM(ens, sls, fkeys, sos);
            ra.BatchEnd(array, backIdea, "3");


        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        //ShowInfo();
        BatchApp();
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>alert('操作成功');window.returnValue=1;window.close();</script>");
    }
    protected void btnEnd_Click(object sender, EventArgs e)
    {
        ShowInfo();
        BatchEndApp();
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>alert('操作成功');window.returnValue=1;window.close();</script>");
    }
    private string TypeId {
        get {
            return Request.QueryString["ftype"];
        }
    }
}

