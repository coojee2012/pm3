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

public partial class Government_AppMain_HTBAseeAppList : govBasePage
{
    RCenter rc = new RCenter();
    SaveAsBase sab = new SaveAsBase();
    RApp ra = new RApp();
    RAppBacth rap = new RAppBacth();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            ControlBind();
            ShowInfo();
            string abc = Request["fcol"].ToString();
            ShowPostion();

        }
    }

    private void ShowPostion()
    {
        if (Request["fcol"] != null)
        {
            this.lPostion.Text = rc.GetMenuName(Request["fcol"].ToString());

        }
    }

    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        //sb.Append(" select fdesc,fnumber from cf_sys_systemname ");
        //sb.Append(" where fisdeleted=0 ");
        //sb.Append(" and fnumber=260 ");//施工许可证的权限  
        //DataTable dt = rc.GetTable(sb.ToString());
        //if (dt == null || dt.Rows.Count == 0)
        //{
        //    Response.Write("<center><font style='font-size:12px' color='red'>对不起,您没有该系统的接见权限!</font></center>");
        //    Response.End();
        //    return;
        //}
        //this.dbSystemId.DataSource = dt;
        //this.dbSystemId.DataTextField = "FDesc";
        //this.dbSystemId.DataValueField = "FNumber";
        //this.dbSystemId.DataBind(); 

        //ShowBatnNo();


        sb.Remove(0, sb.Length);

        string fDefaultManageDept = Session["DFId"].ToString();
        DataTable dt = rc.getAllupDeptId(fDefaultManageDept, 0, 3);
        DataRow[] row = dt.Select();
        if (row != null && row.Length > 0)
        {
            for (int i = 0; i < row.Length; i++)
            {
                dt.Rows.Remove(row[i]);
            }
        }

        dbFManageDeptId.DataSource = dt;
        dbFManageDeptId.DataTextField = "FName";
        dbFManageDeptId.DataValueField = "FNumber";
        dbFManageDeptId.DataBind();
        dbFManageDeptId.Items.Insert(0, new ListItem("--请选择--", ""));

        dbFManageDeptId.SelectedIndex = dbFManageDeptId.Items.IndexOf(dbFManageDeptId.Items.FindByValue(Session["DFId"].ToString()));
        if (EConvert.ToInt(this.Session["DFLevel"]) > 1)
        {
            dbFManageDeptId.Enabled = false;
        }

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

        //if (dbFBatchNoId.SelectedValue != "")
        //{
        //    sb.Append(" and ep.fid in ");
        //    sb.Append(" (select FAppId from CF_App_AppBatchNo t1 ");
        //    sb.Append(" where t1.fappid= ep.fid ')");
        //}


        if (dbFManageDeptId.SelectedValue.Trim() != "")
        {
            sb.Append(" and ep.FManageDeptId like '" + dbFManageDeptId.SelectedValue.Trim() + "%'");
        }

        if (Request.QueryString["fmanagetypeid"] != null)
        {
            if (Request.QueryString["fmanagetypeid"].IndexOf(",") > -1)
                sb.Append(" and ep.fmanagetypeid in (" + Request.QueryString["fmanagetypeid"] + ") ");
            else
                sb.Append(" and ep.fmanagetypeid='" + Request["fmanagetypeid"] + "'");
        }


        if (dbSeeState.SelectedValue != "")
        {
            switch (dbSeeState.SelectedValue.Trim())
            {
                case "0": //未接件
                    sb.Append(" and er.FMeasure=0 ");
                    break;
                case "1": //准予受理
                    sb.Append(" and (er.FMeasure=5 and er.FResult=1) ");
                    break;
                case "3": //不准予受理
                    sb.Append(" and (ep.fstate=6 and er.FResult=3) ");
                    break;
                case "5": //打回企业,重新上报
                    sb.Append(" and (ep.fstate=2) ");
                    break;
            }
        }

        if (dbSystemId.SelectedValue != "")
        {
            sb.Append(" and ep.fsystemid='" + this.dbSystemId.SelectedValue.Trim() + "'");
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
        sb.Append("select *,(select top 1 d.FInt3 from CF_Prj_Data d where tt.FLinkId=d.FID ) FInt3  from ( ");
        sb.Append(" select ep.FId,ep.FBaseInfoId,ep.FEntName,ep.FLinkId,ep.FEmpName,ep.FManageTypeId,ep.FListId,ep.FTypeId,ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FBarCode,");
        sb.Append(" case ep.fState when 1 then '上报审批中' when 2 then '打回企业' when 3 then '打回下级' ");
        sb.Append(" when 5 then '未审批证书' when 6 then '审批完成' end as FStatedesc,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");
        sb.Append(" from CF_App_ProcessInstance ep inner join CF_App_ProcessRecord er");
        sb.Append(" on ep.fId = er.FProcessInstanceID ");
        sb.Append(" inner join ( ");
        sb.Append(" select max(er.fid)fid from CF_App_ProcessInstance ep,CF_App_ProcessRecord er");
        sb.Append(" where ep.fId = er.FProcessInstanceID ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
        sb.Append(" and er.FtypeId=1 "); //存子流程类别 1接件
        sb.Append(getCondi());
        sb.Append("  ");
        sb.Append(" group by ep.flinkId )temp on er.fid=temp.fid where 1=1 ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
        sb.Append(" and er.FtypeId=1 "); //存子流程类别 1接件
        sb.Append(getCondi());
        sb.AppendLine(")tt ) ttt where 1=1 ");

        if (!string.IsNullOrEmpty(t_FInt3.SelectedValue))
        {
            sb.Append(" and FInt3='" + t_FInt3.SelectedValue + "' ");
        }

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



    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string pId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fLinkId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            string fMeasure = e.Item.Cells[e.Item.Cells.Count - 3].Text;
            string fState = e.Item.Cells[e.Item.Cells.Count - 5].Text;
            string fBarCode = e.Item.Cells[e.Item.Cells.Count - 7].Text;
            string fFResult = e.Item.Cells[e.Item.Cells.Count - 8].Text;
            string fsSeeState = e.Item.Cells[e.Item.Cells.Count - 11].Text;
            string fManageTypeId = e.Item.Cells[4].Text;
            string fSubFlowId = e.Item.Cells[9].Text;
            string fListId = e.Item.Cells[5].Text;
            string fTypeId = e.Item.Cells[6].Text;
            string fLevelId = e.Item.Cells[7].Text;

            //合同类型
            string FInt3 = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FInt3"));
            e.Item.Cells[4].Text = db.getManageTypeName(FInt3);
            e.Item.Cells[4].Text = e.Item.Cells[4].Text.Replace("合同备案", "").Replace("确认", "");


            e.Item.Cells[9].Text = rc.GetSignValue(EntityTypeEnum.EaSubFlow, "FName", "FId='" + fSubFlowId + "'");
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();


            if (fListId == "" || fListId == "&nbsp;")
            {
                fListId = "";
            }
            if (fTypeId == "" || fTypeId == "&nbsp;")
            {
                fTypeId = "";
            }
            if (fLevelId == "" || fLevelId == "&nbsp;")
            {
                fLevelId = "";
            }


            e.Item.Cells[5].Text = "";

            if (fListId != "")
            {
                fListId = rc.GetDicRemark(fListId);
                e.Item.Cells[5].Text += fListId;
            }

            if (fTypeId != "")
            {
                fTypeId = rc.GetDicRemark(fTypeId);
                e.Item.Cells[5].Text += fTypeId;
            }



            if (fLevelId != "")
            {
                fLevelId = rc.GetSignValue(EntityTypeEnum.EsQualiLevel, "FName", "FNumber=" + fLevelId);
                if (string.IsNullOrEmpty(fLevelId))
                {
                    if (!string.IsNullOrEmpty(e.Item.Cells[4].Text))
                    {
                        e.Item.Cells[5].Text += e.Item.Cells[4].Text;
                    }
                }
                else
                {
                    if (fLevelId.Trim() == "所有等级")
                    {
                        if (!string.IsNullOrEmpty(e.Item.Cells[4].Text))
                        {
                            e.Item.Cells[5].Text += e.Item.Cells[4].Text;
                        }
                    }
                    else
                    {
                        e.Item.Cells[5].Text += fLevelId;
                    }
                }
            }



            string linkId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance,
                "FBaseInfoId,FManageTypeId,fsystemid,FResult,FTime", "fid='" + pId + "'");
            if (ea == null)
            {
                return;
            }
            string fbid = ea.FBaseInfoId;
            string faid = linkId.Trim();
            string fmid = ea.FManageTypeId;
            string frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + fbid + "'");

            string s = "";


            string sUrl = "";



            string fColor = "";

            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];

            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["name"] = fLinkId;

            if (fMeasure == "0")
            {
                e.Item.Cells[e.Item.Cells.Count - 12].Text = "未接件";
                e.Item.Cells[e.Item.Cells.Count - 13].Text = "";
                fColor = "#ff9900";
            }

            if (fMeasure == "5" && fFResult == "1")
            {
                sUrl = "../AppDetail/PrintAppSlDetail.aspx?flinkid=" + faid + "&fbid=" + fbid + "&fappno=" + fBarCode;
                e.Item.Cells[e.Item.Cells.Count - 12].Text = "准予受理";
                e.Item.Cells[e.Item.Cells.Count - 13].Text = "<a href='" + sUrl + "' class='link5' target='_blank'>打印受理通知书</a>";
                box.Enabled = false;
                fColor = "#339933";
            }

            EaSubFlow es = (EaSubFlow)rc.GetEBase(EntityTypeEnum.EaSubFlow, "", "fid='" + fSubFlowId + "'");
            if (fState == "6" && ea.FResult == "3") //流程结束并且不同意
            {

                sUrl = "../AppDetail/PrintAppBSlDetail.aspx?flinkid=" + faid + "&fbid=" + fbid + "&fappno=" + fBarCode;
                e.Item.Cells[e.Item.Cells.Count - 12].Text = "不准予受理";
                e.Item.Cells[e.Item.Cells.Count - 13].Text = "<a href='" + sUrl + "' class='link5' target='_blank'>打印不予受理通知单</a>";
                box.Enabled = false;
                fColor = "#ff0000";

                //退件时间取流程实例FTime
                e.Item.Cells[15].Text = ea.FTime.ToShortDateString();

            }
            if (fState == "2")
            {

                sUrl = "../AppDetail/PrintAppBSlDetail.aspx?flinkid=" + faid + "&fbid=" + fbid;
                e.Item.Cells[e.Item.Cells.Count - 12].Text = "打回企业,重新上报";
                e.Item.Cells[e.Item.Cells.Count - 13].Text = "<a href='" + sUrl + "' class='link5' target='_blank'>打印不予受理通知单</a>";
                box.Enabled = false;
                fColor = "#b6589d";

            }
 
            if (Session["DFId"].ToString() != ComFunction.GetDefaultDept() || Session["DFLevel"].ToString() != "1")
                e.Item.Cells[e.Item.Cells.Count - 13].Text = "";

            //string fEntName = e.Item.Cells[2].Text;

            sUrl = rc.getMTypeQurl(ea.FManageTypeId);

            sUrl += "?sysid=" + ea.FSystemId + "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + EConvert.ToString(Session["DFUserId"]);

            object fentname = DataBinder.Eval(e.Item.DataItem, "FEntName");
            e.Item.Cells[2].Text = "<a class='link7' style='text-decoration:none' href='" + sUrl + "' target='_blank' title='" + fentname + "'>" + s + e.Item.Cells[2].Text + "</a>";

            //e.Item.Cells[2].Text = "<a class='link5' href='" + sUrl + "' target='_blank' title='" + fDesc + "'>";
            //e.Item.Cells[2].Text = "<font color='" + fColor + "'>" + e.Item.Cells[2].Text + "</font>";


            if (e.Item.Cells[15].Text == "1900-01-01")
            {
                e.Item.Cells[15].Text = "";
            }



            StringBuilder sb = new StringBuilder();
            sb.Append(" select FBatchNoId from CF_App_AppBatchNo ");
            sb.Append(" where FAppId='" + pId + "' and FDFId ='" + this.Session["DFId"].ToString() + "'");
            sb.Append(" and fisdeleted=0 ");

            string fBatchNoId = rc.GetSignValue(sb.ToString());
            if (fBatchNoId != null && fBatchNoId != "&nbsp;")
            {
                sb.Remove(0, sb.Length);
                sb.Append(" fid='" + fBatchNoId + "'");
                e.Item.Cells[e.Item.Cells.Count - 10].Text = rc.GetSignValue(EntityTypeEnum.EaBatchNo, "FTtile", sb.ToString());
            }


        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void dbSystemId_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ShowBatnNo();
        ShowInfo();
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        int iCount = this.JustAppInfo_List.Items.Count;
        ArrayList array = new ArrayList();

        for (int i = 0; i < iCount; i++)
        {
            string fLinkId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Items[i].Cells.Count - 2].Text;
            CheckBox cb = (CheckBox)JustAppInfo_List.Items[i].Cells[0].Controls[1];
            if (cb.Checked == true)
            {
                if (!array.Contains(fLinkId))
                {
                    array.Add(fLinkId);
                }
            }
        }
        if (array.Count == 0)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择')</script>");
            return;
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
            StringBuilder sScript = new StringBuilder();
            sScript.Append("<script>function app(){var obj = new Object();");
            sScript.Append(" var tmpVal='';");
            sScript.Append(" obj.name='51js';");
            sScript.Append(" tmpVal ='" + sb.ToString() + "';");
            sScript.Append(" obj.id= tmpVal;");
            sScript.Append(" obj.fsystemid= " + dbSystemId.SelectedValue.Trim() + ";");
            sScript.Append(" ShowWindow('AcceptSeeOneReportInfo.aspx?e=0',900,700,obj);} window.onload=app;</script>");
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), sScript.ToString());
        }
    }




    protected void btnBack_Click(object sender, EventArgs e)
    {
        int iCount = this.JustAppInfo_List.Items.Count;
        ArrayList array = new ArrayList();

        for (int i = 0; i < iCount; i++)
        {
            string fLinkId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Items[i].Cells.Count - 2].Text;
            CheckBox cb = (CheckBox)JustAppInfo_List.Items[i].Cells[0].Controls[1];
            if (cb.Checked == true)
            {
                array.Add(fLinkId);
            }
        }
        if (array.Count == 0)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择')</script>");
            return;
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
            StringBuilder sScript = new StringBuilder();
            sScript.Append("<script>function app(){var obj = new Object();");
            sScript.Append("var tmpVal='';");
            sScript.Append("obj.name='51js';");
            sScript.Append(" tmpVal ='" + sb.ToString() + "';");
            sScript.Append(" obj.id= tmpVal;");
            sScript.Append(" ShowWindow('BackSeeOneReportInfo.aspx?e=0',900,700,obj);}window.onload=app;</script>");
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), sScript.ToString());
        }
    }
    protected void btnAddBathNo_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        //if (this.dbFBatchNoId.SelectedValue == "")
        //{
        //    tool.showMessage("请选择要加入的批次");
        //    return;
        //}

        int iCount = JustAppInfo_List.Items.Count;
        ArrayList array = new ArrayList();
        for (int i = 0; i < iCount; i++)
        {
            string fId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Columns.Count - 1].Text;
            string fLinkId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Columns.Count - 2].Text;

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
            tool.showMessage("请选择要加入批次的行");
            return;
        }


        iCount = array.Count;

        StringBuilder sb = new StringBuilder();
        sb.Append(" select fid from CF_App_ProcessInstance ");
        sb.Append(" where flinkid in (");
        for (int i = 0; i < iCount; i++)
        {
            if (i == 0)
            {
                sb.Append("'" + array[i].ToString() + "'");
            }
            else
            {
                sb.Append(",'" + array[i].ToString() + "'");
            }
        }
        sb.Append(")");

        DataTable dt = rc.GetTable(sb.ToString());

        iCount = dt.Rows.Count;
        SortedList[] sls = new SortedList[iCount];
        string[] keys = new string[iCount];
        EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
        SaveOptionEnum[] sos = new SaveOptionEnum[iCount];


        for (int j = 0; j < iCount; j++)
        {
            sls[j] = new SortedList();
            keys[j] = "FID";
            ens[j] = EntityTypeEnum.EaAppBatchNo;

            sb.Remove(0, sb.Length);
            sb.Append(" select fid from CF_App_AppBatchNo ");
            sb.Append(" where FAppId='" + dt.Rows[j]["FID"].ToString() + "'");
            sb.Append(" and FDFId='" + this.Session["DFId"].ToString() + "'");
            sb.Append(" and fisdeleted=0 ");

            string fid = rc.GetSignValue(sb.ToString());
            if (fid == null || fid == "")
            {
                sos[j] = SaveOptionEnum.Insert;
                sls[j].Add("FID", Guid.NewGuid().ToString());
                //sls[j].Add("FBatchNoId", this.dbFBatchNoId.SelectedValue.Trim());
                sls[j].Add("FAppId", dt.Rows[j]["FID"].ToString());
                sls[j].Add("FDFId", this.Session["DFId"].ToString());
                sls[j].Add("FIsDeleted", 0);
                sls[j].Add("FCreateTime", DateTime.Now);
            }
            else
            {
                sos[j] = SaveOptionEnum.Update;
                sls[j].Add("FID", fid);
                //sls[j].Add("FBatchNoId", this.dbFBatchNoId.SelectedValue.Trim());
            }
        }
        if (rc.SaveEBaseM(ens, sls, keys, sos))
        {
            tool.showMessage("加入批次成功");
            ShowInfo();
        }
        else
        {
            tool.showMessage("加入批次失败");
        }
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
}