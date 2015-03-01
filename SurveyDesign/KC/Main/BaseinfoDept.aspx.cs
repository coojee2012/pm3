using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Text;
using System.Data;
using Approve.RuleCenter;
using System.Collections;
using Approve.EntityBase;
public partial class JSDW_QMain_BaseinfoDept : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["fbid"]))
        {
            FBaseInfoId = CurrentEntUser.EntId;
            FUserId = CurrentEntUser.UserId;
        }
        else
        {
            FBaseInfoId = Request.QueryString["fbid"];
            FUserId = Request.QueryString["frid"];
            this.ClientScript.RegisterStartupScript(this.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>try{btnEnable();}catch(e){}</script>");
            IsView = "1";
        }
        if (!IsPostBack)
        {
            govd_FRegistDeptId.FNumber = ComFunction.GetDefaultCityDept();
            govd_FRegistDeptId.Dis(1);
            showInfo();
            ShowEmpInfo();
            ShowCertiInfo();
            showPrj();
        }
    }
    private string IsView { get; set; }
    private string FBaseInfoId { get; set; }
    private string FUserId { get; set; }
    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
        CF_Ent_BaseInfo Ent = db.CF_Ent_BaseInfo.Where(t => t.FId == FBaseInfoId && t.FJuridcialCode != null).FirstOrDefault();
        if (Ent != null)
        {
            tool.fillPageControl(Ent);
            if (Ent.FRegistDeptId.ToString() != "")
                govd_FRegistDeptId.FNumber = Ent.FRegistDeptId.ToString();
        }
        else
        {
            CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == FUserId).FirstOrDefault();
            if (user != null)
            {
                t_FName.Text = user.FCompany;
                t_FJuridcialCode.Text = user.FJuridcialCode;
                t_FLinkMan.Text = user.FLinkMan;
                t_FTel.Text = user.FTel;
                govd_FRegistDeptId.fNumber = user.FManageDeptId.ToString();
            }
        }
    }
    void ShowCertiInfo()
    {
        var App = db.CF_Ent_QualiCerti.Where(t => t.FBaseInfoId == FBaseInfoId).OrderByDescending(t => t.FCreateTime)
             .Select(t => new
             {
                 t.FId,
                 t.FCertiNo,
                 t.FLevelName,
                 t.FAppDeptId,
                 t.FAppTime,
                 FCertiType = db.CF_Sys_Dic.Where(l => l.FNumber == t.FCertiType).Select(l => l.FName).FirstOrDefault(),
                 t.FEndTime
             }); ;

        dgCerti_List.DataSource = App;
        dgCerti_List.DataBind();
    }
    void ShowEmpInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select t1.FName,t2.FCount,t1.FNumber ");
        sb.Append("from cf_Sys_Dic t1 left join (");
        sb.Append("select FPersonTypeId FType,count(*) FCount ");
        sb.Append("from CF_Emp_BaseInfo ");
        sb.Append("where fBaseInfoId='" + FBaseInfoId + "' ");
        sb.Append("group by FPersonTypeId) t2 on t1.FNumber=t2.FType ");
        sb.Append("and isnull(t2.FType,'0')!='0' ");
        sb.Append("where t1.FParentId='123' order by t1.FOrder,t1.FNumber");
        DataTable dt = rc.GetTable(sb.ToString());
        sb.Remove(0, sb.Length);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append("<tr>");
            for (int ii = 0; ii < 2; ii++)
            {
                if (ii > 0)
                {
                    if (!(i < dt.Rows.Count - 1))
                        break;
                    else
                        i++;
                }
                sb.Append("<td class='t_c t_bg'>" + dt.Rows[i][0] + "</td>");
                int iCount = EConvert.ToInt(dt.Rows[i][1]);
                if (iCount > 0)
                    sb.Append("<td class='t_c'><b><a href=\"javascript:showAddWindow('EmpList.aspx?fbid=" + FBaseInfoId + "&fType=" + dt.Rows[i][2] + "',700,400);\">" + iCount + "</a></b></td>");
                else
                    sb.Append("<td class='t_c'>0</td>");
            }
            sb.Append("</tr>");
        }
        litEmp.Text = sb.ToString();
    }
    protected void dgCerti_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1).ToString();
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('AddCertiInfo.aspx?fid=" + fid + "&IsView=" + IsView + "&FBaseInfoId=" + FBaseInfoId + "',800,520,$('#btnReload1'));\">" + e.Item.Cells[2].Text + "</a>";
            string FAppDeptId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppDeptId"));
            StringBuilder sb = new StringBuilder();
            //核准单位 
            sb.Append(" fisdeleted=0 and ");
            sb.Append("fnumber =" + FAppDeptId + " ");
            DataTable dt = rc.GetTable(EntityTypeEnum.EsManageDept, "FFullName", sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                e.Item.Cells[3].Text = EConvert.ToString(dt.Rows[0][0]);
            }
        }
    }



    private void showPrj()
    {

        if (FBaseInfoId != null && FBaseInfoId != "")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select t.FName,tt.FProjectAddress,tt.FConUnit,t.FBeginDate,t.FEndDate from CF_Ent_Project t,CF_Ent_ProjectOther tt where t.FBaseInfoId='" + FBaseInfoId + "' and t.fid=tt.fid ");


            this.Pager1.className = "dbJST";
            this.Pager1.sql = sb.ToString();
            this.Pager1.pagecount = 10;
            this.Pager1.controltopage = "dg_List";
            this.Pager1.controltype = "DataGrid";
            this.Pager1.dataBind();
        }
    }


    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));



        }
    }


    private void showBAD()
    {
        if (FBaseInfoId != null && FBaseInfoId != "")
        {
            StringBuilder sb = new StringBuilder();
            SortedList sl = new SortedList();
            sb.Append("select * from (select ID,CASE WHEN DQC IS NOT NULL THEN DQC ELSE CASE WHEN DQB IS NOT NULL THEN DQB ELSE DQA END END as SD,QYMC,ZRZTLB,ZRZTLBID,CFRQ,FS,RDDW From QY_BLXW_XXB) tt  ");
            sb.Append(" where 1=1 and ZRZTLBID in (145,140,155) and  ZRZTLBID=@fbaseinfoid");
            sl.Add("fbaseinfoid", FBaseInfoId);
            sb.Append(" Order By CFRQ Desc ");

            this.Pager2.className = "dbJST";
            this.Pager2.sql = sb.ToString();
            this.Pager2.pagecount = 10;
            this.Pager2.controltopage = "dg_List1";
            this.Pager2.controltype = "DataGrid";
            this.Pager2.dataBind();
        }
    }

    protected void App_List1_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ID"));
            e.Item.Cells[4].Text = e.Item.Cells[4].Text + "分";

            e.Item.Cells[1].Text = "<a href=\"javascript:showAddWindow('EntBadAction.aspx?fid=" + FID + "',900,700);\">" + e.Item.Cells[1].Text + " </a>";

        }
    }

}
