using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Text;
using Approve.EntityBase;
using System.Data;
using System.Linq;
using System.Collections;
using System.Data.SqlClient;
using ProjectData;
using Approve.Common;

public partial class Share_User_RegUserList : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            ControlBind();
            ShowInfo();
        }
    }


    //列表绑定
    private void ControlBind()
    {
        ProjectDB db = new ProjectDB();
        IQueryable<CF_Sys_SystemName> sys = db.CF_Sys_SystemName.Where(t => t.FNumber != 100).OrderBy(t => t.FOrder);
     

        t_FSystemId.DataSource = sys;
        t_FSystemId.DataTextField = "FName";
        t_FSystemId.DataValueField = "FNumber";
        t_FSystemId.DataBind();
        t_FSystemId.Items.Insert(0, new ListItem("请选择", ""));
    }

    //条件
    private string GetCon()
    {

        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FEntName.Text))
        {
            sb.Append(" and FCompany like '%" + t_FEntName.Text + "%'");
        }
        if (!string.IsNullOrEmpty(t_FState.SelectedValue))
        {
            sb.Append(" and isnull(FIsApp,0) ='" + t_FState.SelectedValue + "'");

            if (t_FState.SelectedValue == "1" && !string.IsNullOrEmpty(t_FIsApp.SelectedValue))
            {
                sb.Append(" and FIsApp='" + t_FIsApp.SelectedValue + "'");
            }
        }
        if (!string.IsNullOrEmpty(t_FSystemId.SelectedValue))
        {
            sb.Append(" and fid in (select fregid from cf_user_regright where fsystemid='" + t_FSystemId.SelectedValue + "' ) ");
        }
        if (!string.IsNullOrEmpty(t_CreateTime1.Text))
        {
            sb.Append(" and Convert(varchar(100),FCreateTime,23)>='" + t_CreateTime1.Text + "'");
        }
        if (!string.IsNullOrEmpty(t_CreateTime2.Text))
        {
            sb.Append(" and Convert(varchar(100),FCreateTime,23)<='" + t_CreateTime2.Text + "'");
        }

        if (!string.IsNullOrEmpty(t_FType.SelectedValue))
        {
            sb.Append(" and Ftype='" + t_FType.SelectedValue + "'");
        }
        if (!string.IsNullOrEmpty(Request.QueryString["FSystemId"]))
        {
            sb.AppendLine(" and FId in (select FRegId from CF_User_RegRight where FSystemId in (" + Request.QueryString["FSystemId"] + ")) ");
        }
        if (!string.IsNullOrEmpty(EConvert.ToString(Session["DFId"])))
        {
            sb.AppendLine(" and FManageDeptId like  '" + Session["DFId"] + "%'");
        }
        return sb.ToString();
    }
    private void ShowInfo()
    {

        StringBuilder sb = new StringBuilder();
        sb.Append(" select FId,FCompany,FCreateTime,FLinkMan,FTel,ftype,FManageDeptId,FState,FJuridcialCode,FSysList,FIsApp,FRFID ");
        sb.Append(" From CF_User_Reg ");
        sb.Append(" where FIsDeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" And FType in(2,8)  ");
        sb.Append(" Order By  Fcreatetime Desc");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();

    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FRFID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FRFID"));
            string FManageDeptId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageDeptId"));
            string FCompany = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FCompany"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            string FIsApp = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FIsApp"));
            string FType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FType"));

            e.Item.Cells[5].Text = sh.getDept(FManageDeptId, 1);//主管部门

            if (FType == "2")
            {
                e.Item.Cells[3].Text = "新申请";
                e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('RegUserEdit.aspx?FID=" + FID + "',600,600);\" >" + FCompany + "</a>";
                e.Item.Attributes.Add("ondblclick", "showAddWindow('RegUserEdit.aspx?FID=" + FID + "',600,600);");
            }
            else if (FType == "8")
            {
                e.Item.Cells[3].Text = "增开系统";
                e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('RegUserEdit2.aspx?FID=" + FID + "',600,400);\" >" + FCompany + "</a>";
                e.Item.Attributes.Add("ondblclick", "showAddWindow('RegUserEdit2.aspx?FID=" + FID + "',600,400);");
            }


            //申请系统权限列表
            #region

            StringBuilder sb = new StringBuilder();
            sb.Append("select s.FName,r.FState,r.FIsApp,r.FAppUserName,r.FAppDate,s.fNumber  from cf_user_regright r,cf_sys_systemName s ");
            sb.Append("where r.fsystemid=s.fnumber and  FRegId=@FRegId Order by s.Forder ");
            DataTable dt = sh.GetTable(sb.ToString(), new SqlParameter("@FRegId", FID));
            sb.Remove(0, sb.Length);
            int n = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               
                string isapp = dt.Rows[i]["FIsApp"].ToString();
                string state = dt.Rows[i]["FState"].ToString();
                string appUserName = dt.Rows[i]["FAppUserName"].ToString();
                string appDate = ("(" + EConvert.ToShortDateString(dt.Rows[i]["FAppDate"]).Replace("0001-01-01", "") + ")").Replace("()", "");
                string fName = dt.Rows[i]["FName"].ToString();
                string fNumber = dt.Rows[i]["fNumber"].ToString();
                //if (fNumber == "100")
                //    fName = "选址意见管理系统";


                sb.Append(fName);
                if (isapp == "1")
                {
                    if (state == "1")
                    {
                        sb.Append("<span style='color:green'>[已通过]</span>[审核人：" + appUserName + "" + appDate + "]");
                    }
                    else
                    {
                        sb.Append("<span style='color:red'>[未通过]</span>");
                    }
                    n++;
                }
                sb.Append("<br/>");
            }



            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                e.Item.Cells[6].Text = sb.ToString();
            }
            #endregion

            //审核状态
            #region
            string str = "";
            if (n == 0)
            {
                str = "<span style='color:red'>未审核</span>";
            }
            else if (n < dt.Rows.Count)
            {
                str = "<span style='color:blue'>审核中</span>";
            }
            else
            {
                str = "<span style='color:green;'>已审核</span>";
            }
            if (FIsApp == "1")
            {
                str = "<span style='color:green;'>已审核</span>";
            }

            e.Item.Cells[7].Text = str;
            #endregion
        }

    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("isApp('" + t_FState.SelectedValue + "')");
        ShowInfo();

    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        sl.Add("CF_User_Reg", "FID");
        sl.Add("CF_User_RegRight", "FRegId");
        tool.DelInfoFromGrid(this.DG_List, sl, "dbShare");
        ShowInfo();
    }

}
