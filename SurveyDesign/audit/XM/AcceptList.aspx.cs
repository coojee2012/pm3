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

public partial class Government_AppZLJDBA_AcceptList : govBasePage
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
            base.Page_Load(sender, e); ShowPostion();
            ShowInfo();

        }
    }

    private void ShowPostion()
    {
        if (Request["fcol"] != null)
        {
            //this.lPostion.Text = rc.GetMenuName(Request["fcol"].ToString());
            switch (Request["fcol"].ToString().Trim())
            {
                case "0001":
                    this.lPostion.Text = "选址意见书接件";
                    break;
                case "0002":
                    this.lPostion.Text = "选址意见书初审";
                    break;
                case "0003":
                    this.lPostion.Text = "选址意见书复审";
                    break;
                case "0004":
                    this.lPostion.Text = "选址意见书办理查询";
                    break;
                case "0005":
                    this.lPostion.Text = "用地规划许可接件";
                    break;
                case "0006":
                    this.lPostion.Text = "用地规划许可初审";
                    break;
                case "0007":
                    this.lPostion.Text = "用地规划许可复审";
                    break;
                case "0008":
                    this.lPostion.Text = "用地规划许可办理查询";
                    break;
                case "0009":
                    this.lPostion.Text = "工程规划许可接件";
                    break;
                case "0010":
                    this.lPostion.Text = "工程规划许可初审";
                    break;
                case "0011":
                    this.lPostion.Text = "工程规划许可复审";
                    break;
                case "0012":
                    this.lPostion.Text = "工程规划许可办理查询";
                    break;
                case "0013":
                    this.lPostion.Text = "项目报建接件";
                    break;
                case "0014":
                    this.lPostion.Text = "项目报建初审";
                    break;
                case "0015":
                    this.lPostion.Text = "项目报建复审";
                    break;
                case "0016":
                    this.lPostion.Text = "项目报建办理查询";
                    break;
                case "0017":
                    this.lPostion.Text = "标段划分备案审核";
                    break;
                case "0018":
                    this.lPostion.Text = "预审文件备案审核";
                    break;
                case "0019":
                    this.lPostion.Text = "预审结果备案审核";
                    break;
                case "0020":
                    this.lPostion.Text = "招标文件备案审核";
                    break;
                case "0021":
                    this.lPostion.Text = "评标结果公示、评标报告备案审核";
                    break;
                case "0022":
                    this.lPostion.Text = "中标结果备案审核";
                    break;
                case "0023":
                    this.lPostion.Text = "质监管理接件";
                    break;
                case "0024":
                    this.lPostion.Text = "质监管理初审";
                    break;
                case "0025":
                    this.lPostion.Text = "质监管理复审";
                    break;
                case "0026":
                    this.lPostion.Text = "质监管理备案查询";
                    break;
                case "0027":
                    this.lPostion.Text = "安监管理接件";
                    break;
                case "0028":
                    this.lPostion.Text = "安监管理初审";
                    break;
                case "0029":
                    this.lPostion.Text = "安监管理复审";
                    break;
                case "0030":
                    this.lPostion.Text = "安监管理备案查询";
                    break;
                case "0031":
                    this.lPostion.Text = "施工许可证管理接件";
                    break;
                case "0032":
                    this.lPostion.Text = "施工许可证管理初审";
                    break;
                case "0033":
                    this.lPostion.Text = "施工许可证管理复审";
                    break;
                case "0034":
                    this.lPostion.Text = "施工许可证补证复审办理";
                    break;
                case "0035":
                    this.lPostion.Text = "施工许可证办理查询";
                    break;
                case "0036":
                    this.lPostion.Text = "开工管理";
                    break;
                case "0037":
                    this.lPostion.Text = "竣工管理";
                    break;
                case "0038":
                    this.lPostion.Text = "项目查询";
                    break;
                case "0039":
                    this.lPostion.Text = "停工管理";
                    break;
                case "0040":
                    this.lPostion.Text = "复工管理";
                    break;
                case "0041":
                    this.lPostion.Text = "停复工通告";
                    break;
                case "0042":
                    this.lPostion.Text = "人员锁定管理";
                    break;
                case "0043":
                    this.lPostion.Text = "关键岗位监管";
                    break;
                case "0044":
                    this.lPostion.Text = "塔吊设备监管";
                    break;
                case "0045":
                    this.lPostion.Text = "扬尘噪音检测设备";
                    break;
                case "0046":
                    this.lPostion.Text = "扬尘治理检查";
                    break;
                case "0047":
                    this.lPostion.Text = "扬尘治理自查";
                    break;
                case "0048":
                    this.lPostion.Text = "施工安全评定";
                    break;
                case "0049":
                    this.lPostion.Text = "竣工验收备案接件";
                    break;
                case "0050":
                    this.lPostion.Text = "竣工验收备案初审";
                    break;
                case "0051":
                    this.lPostion.Text = "竣工验收备案复审";
                    break;
                case "0052":
                    this.lPostion.Text = "竣工验收备案查询";
                    break;
                case "0053":
                    this.lPostion.Text = "标化工地接件";
                    break;
                case "0054":
                    this.lPostion.Text = "标化工地预报审核";
                    break;
                case "0055":
                    this.lPostion.Text = "标化工地复查审核";
                    break;
                case "0056":
                    this.lPostion.Text = "标化工地发证审核";
                    break;
                case "0057":
                    this.lPostion.Text = "标化工地批次管理";
                    break;
                case "0058":
                    this.lPostion.Text = "标化工地上会导出";
                    break;
            }
        }
    }

    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fdesc,fnumber from cf_sys_systemname ");
        sb.Append(" where fisdeleted=0 ");
        sb.Append(" and fnumber=1122 ");//施工许可证的权限  
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt == null || dt.Rows.Count == 0)
        {
            Response.Write("<center><font style='font-size:12px' color='red'>对不起,您没有该系统的接见权限!</font></center>");
            Response.End();
            return;
        }
        this.dbSystemId.DataSource = dt;
        this.dbSystemId.DataTextField = "FDesc";
        this.dbSystemId.DataValueField = "FNumber";
        this.dbSystemId.DataBind();
    }

    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtFProjectName.Text.Trim() != "" && this.txtFProjectName.Text.Trim() != null)
        {
            sb.Append(" and qa.ProjectName like '%" + this.txtFProjectName.Text.Trim() + "%' ");
        }
        if (this.txtFPrjItemName.Text.Trim() != "" && this.txtFPrjItemName.Text.Trim() != null)
        {
            sb.Append(" and qa.PrjItemName like '%" + this.txtFPrjItemName.Text.Trim() + "%' ");
        }
        if (this.txtSJDW.Text.Trim() != "" && this.txtSJDW.Text.Trim() != null)
        {
            sb.Append(" and qa.SJDW like '%" + this.txtSJDW.Text.Trim() + "%' ");
        }
        if (this.txtSDate.Text.Trim() != "" && this.txtSDate.Text.Trim() != null)
        {
            sb.Append(" and ep.FReportDate >='" + this.txtSDate.Text.Trim() + "' ");
            //        sb.Append(" and DATEDIFF(day,'2008-12-29','2008-12-30') ");
        }
        if (this.txtEDate.Text.Trim() != "" && this.txtEDate.Text.Trim() != null)
        {
            sb.Append(" and ep.FReportDate  <='" + this.txtEDate.Text.Trim() + "' ");
        }
        //if (dbFBatchNoId.SelectedValue != "")
        //{
        //    sb.Append(" and ep.fid in ");
        //    sb.Append(" (select FAppId from CF_App_AppBatchNo t1 ");
        //    sb.Append(" where t1.fappid= ep.fid ')");
        //}


        if (ddlState.SelectedValue != "-1")
        {
            switch (ddlState.SelectedValue.Trim())
            {
                case "0": //待接件
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
        sb.Append(" select qa.ProjectName,qa.PrjItemName,qa.RecordNo,qa.JSDW,ep.FId,er.FId as FprId,ep.FBaseInfoId,ep.FEntName,ep.FLinkId,ep.FEmpName,ep.FManageTypeId,ep.FListId,ep.FTypeId,ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FBarCode,qa.sjdw,");
        sb.Append(" case ep.fState when 1 then '上报审批中' when 2 then '打回企业' when 3 then '打回下级' ");
        sb.Append(" when 5 then '未审批证书' when 6 then '审批完成' end as FStatedesc,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");
        sb.Append(" from CF_App_ProcessInstance ep , CF_App_ProcessRecord er, TC_QA_Record qa");
        sb.Append(" where ep.fId = er.FProcessInstanceID and  er.FtypeId=1 ");
        //  sb.Append(" and ep.FSubFlowId = er.FSubFlowId "); //去掉这行，表示可以查询已经处理了到了下一阶段的业务
        sb.Append(" and ep.flinkId = er.FLinkId  and ep.flinkId = qa.FAppId ");
  //      sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
        sb.Append(getCondi());
        sb.AppendLine(" ) ttt where 1=1 ");

        //if (!string.IsNullOrEmpty(t_FInt3.SelectedValue))
        //{
        //    sb.Append(" and FInt3='" + t_FInt3.SelectedValue + "' ");
        //}

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
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            string fSubFlowId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSubFlowId"));
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            string ferId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FprId"));
            string fBaseInfoId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBaseInfoId"));
            string fMeasure = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FMeasure"));

            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["name"] = FLinkId;
            box.Attributes["fpid"] = fid;
            box.Attributes["ferid"] = ferId;
            box.Attributes["fSubFlowId"] = fSubFlowId;
            box.Attributes["fBaseInfoId"] = fBaseInfoId;
            box.Attributes["fMeasure"] = fMeasure;
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
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
            string fLinkId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Items[i].Cells.Count - 1].Text;
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
            sScript.Append(" obj.fsystemid= 1122;");
            sScript.Append(" ShowWindow('../../Government/AppZLJDBA/AcceptSeeOneReportInfo.aspx?e=0',900,700,obj);} window.onload=app;</script>");
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
            sScript.Append(" ShowWindow('../../Government/AppZLJDBA/BackSeeOneReportInfo.aspx?e=0',900,700,obj);}window.onload=app;</script>");
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