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
using Approve.EntitySys;

public partial class Admin_main_ProcessAdd : Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            ControlBind();
            if (Request["fid"] != null && Request["fid"] != null)
            {
                this.hidden_fprocessid.Value = Request["fid"];
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
                //ShowQualiLevel(2);
                //ShowManageType(2);
                //ShowQualiType(2);
                ShowOtherInfo();
            }
        }
    }
    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fname,fnumber from CF_Sys_SystemName where fisdeleted=0  order by FOrder,ftime desc");
        DataTable dt = rc.GetTable(sb.ToString());
        this.t_FSystemId.DataSource = dt;
        this.t_FSystemId.DataTextField = "FName";
        this.t_FSystemId.DataValueField = "FNumber";
        this.t_FSystemId.DataBind();

        dt = rc.getDicTbByFNumber("106");
        this.t_FBaseType.DataSource = dt;
        this.t_FBaseType.DataTextField = "FName";
        this.t_FBaseType.DataValueField = "FNumber";
        this.t_FBaseType.DataBind();
    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_App_Process where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            this.Govdept1.FNumber = t_FManageDeptId.Value;
        }
    }
    private void SaveInfo()
    {
        t_FManageDeptId.Value = this.Govdept1.FNumber;
        pageTool tool = new pageTool(this.Page);
        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = tool.getPageValue();
        if (this.ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
        }
        if (rc.SaveEBase(EntityTypeEnum.EaProcess, sl, "FID", so))
        {
            this.hidden_fprocessid.Value = sl["FID"].ToString();
            this.Govdept1.FNumber = t_FManageDeptId.Value;
            tool.showMessage("保存成功");
            this.ViewState["FID"] = sl["FID"].ToString();
            HSaveResult.Value = "1";

        }
        else
        {
            tool.showMessage("保存失败");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        this.ViewState["FID"] = null;
        pageTool tool = new pageTool(this.Page);
        this.hidden_fprocessid.Value = "";
        this.hidden_Value1.Value = "";
        this.hidden_Value2.Value = "";
        this.hidden_Value3.Value = "";
        tool.ExecuteScript("clearPage()");
    }

    private void ShowOtherInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select csq.fname from CF_App_QualiLevel caq,CF_Sys_QualiLevel csq ");
        sb.Append(" where csq.fnumber = caq.FLevelId");
        sb.Append(" and csq.fisdeleted=0 ");
        sb.Append(" and caq.fisdeleted=0 ");
        sb.Append(" and fprocessid='" + this.ViewState["FID"].ToString() + "' ");
        sb.Append(" order by csq.ftime desc ");
        DataTable dt = rc.GetTable(sb.ToString());
        ShowSelectInfo(dt, "hidden_Value1", new string[] { "FName" }, 2);

        sb.Remove(0, sb.Length);
        sb.Append("select csm.fname from CF_App_ManageType cam,CF_Sys_ManageType csm ");
        sb.Append(" where csm.fnumber = cam.FManageTypeId");
        sb.Append(" and csm.fisdeleted=0 ");
        sb.Append(" and cam.fisdeleted=0 ");
        sb.Append(" and fprocessid='" + this.ViewState["FID"].ToString() + "' ");
        sb.Append(" order by  csm.FOrder,csm.FCreateTime desc ");
        dt = rc.GetTable(sb.ToString());
        ShowSelectInfo(dt, "hidden_Value2", new string[] { "FName" }, 3);


        sb.Remove(0, sb.Length);
        sb.Append("select csd.fname,(select top 1 fname from cf_sys_dic where fnumber=csd.fparentid) fpname, ");
        sb.Append("(select top 1 forder from cf_sys_dic where fnumber=csd.fparentid) fporder ");
        sb.Append(" from CF_App_QualiType caq,CF_sys_Dic csd ");
        sb.Append(" where caq.FQualiTypeId = csd.fnumber ");
        sb.Append(" and caq.fisdeleted=0 ");
        sb.Append(" and csd.fisdeleted=0 ");
        sb.Append(" and fprocessid='" + this.ViewState["FID"].ToString() + "' ");
        sb.Append(" order by fporder,csd.forder,csd.ftime desc ");
        dt = rc.GetTable(sb.ToString());
        ShowSelectInfo(dt, "hidden_Value3", new string[] { "fpname", "FName" }, 3);

    }
    private void ShowSelectInfo(DataTable dt, string controlId, string[] colName, int colCount)
    {
        Control con = this.Page.FindControl(controlId);
        HtmlInputHidden tempLabel = (HtmlInputHidden)con;
        if (con == null)
        {
            return;
        }
        if (dt == null || dt.Rows.Count <= 0)
        {
            tempLabel.Value = "<center>暂无内容</center>";
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("<table width='90%' cellspacing='0' cellpadding='0' align='center' class='txt7'>");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i % colCount == 0)
            {
                sb.Append("<tr>");
            }
            sb.Append("<td>");
            for (int j = 0; j < colName.Length; j++)
            {
                if (j == 0)
                {
                    sb.Append(dt.Rows[i][colName[j]].ToString());
                }
                else
                {
                    sb.Append(" | " + dt.Rows[i][colName[j]].ToString());
                }
            }
            sb.Append("</td>");
            if ((i + 1) % colCount == 0)
            {
                sb.Append("</tr>");
            }
        }
        sb.Append("</table>");
        tempLabel.Value = sb.ToString();
    }
}
