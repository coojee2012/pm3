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

public partial class OA_Bulletin_AppBull : System.Web.UI.Page
{
    bool temp = true;
    OA oa = new OA();
    string roleID;
    string userId = "C4A53042-55EB-4FA5-AA91-747B36EDC375";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FEmpID"] != null)
        {
            userId = Session["FEmpID"].ToString();
        }
        if (!IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确认通过审核么?');");

            if (userId != null && userId != "")
            {
                DataGreadShow();
            }
        }

    }
    protected void DelRow(int sta)
    {
        pageTool tool = new pageTool(this.Page);
        OA oa = new OA();

        int count = 0;
        for (int i = 0; i < this.BulList.Rows.Count; i++)
        {
            CheckBox cbx = (CheckBox)this.BulList.Rows[i].FindControl("CheckItem");

            if (cbx.Checked)
            {
                string FID = ((Label)BulList.Rows[i].FindControl("Label2")).Text;

                string sqlstr = "update CF_OA_Bulletin set FIsApp = " + sta + " where FID='" + FID + "'";
                count++;
                if (oa.PExcute(sqlstr))
                {

                }

            }
        }
        if (count == 0)
        {
            tool.showMessage("请选择公告！");
        }



    }
    protected void DataGreadShow()
    {
        string sqlstr = "select Forganid from CF_OA_Emp where fid = '" + userId + "'";
        DataTable dt = new DataTable();
        dt = oa.GetTable(sqlstr);
        StringBuilder sb = new StringBuilder();
        if (dt != null && dt.Rows.Count > 0)
        {
            roleID = dt.Rows[0][0].ToString();

            sb.Append("select b.FID,b.Ftitle,t.TypeName,b.FDateOn,u.Fname,(case b.FIsApp when 1 then '未审核' when 2 then '同意发布' when 3 then '审核未通过' end) as app ");
            sb.Append("from CF_OA_Bulletin b,CF_OA_BulType t,CF_OA_Emp u ");
            sb.Append("where b.FBulTypeId=t.FID and u.FID=b.FUserId ");
            //sb.Append("and r.RoleID='" + roleID + "'");
            sb.Append(" and b.FIsDeleted ='false' ");
            sb.Append("and (b.FIsApp = 1 or b.FIsApp=2 or b.FIsApp=3)");
            if (this.txtFTitle.Text != null && this.txtFTitle.Text.Trim() != "")
            {
                sb.Append(" and b.Ftitle like '" + this.txtFTitle.Text.Trim() + "'");
            }
            if (this.textFType.Text != null && this.textFType.Text.Trim() != "")
            {
                sb.Append(" and b.FBulTypeId ='" + this.textFType.Text.Trim() + "'");
            }
            if (this.DropDownList1.Text != null && this.DropDownList1.Text.Trim() != "")
            {
                sb.Append(" and b.FIsApp='" + this.DropDownList1.SelectedValue.ToString() + "'");
            }
            sb.Append(" and '" + DateTime.Now + "' between b.FDateOn and b.FDateOff");
            sb.Append(" order by b.FDateOn desc");
            this.Pager1.controltopage = "BulList";
            this.Pager1.className = "dbOA";
            this.Pager1.sql = sb.ToString();
            this.Pager1.pagecount = 15;
            this.Pager1.controltype = "GridView";
            this.Pager1.dataBind();
        }
        sb.Remove(0, sb.Length);
        sb.Append(@"select bt.FID,bt.TypeName
from CF_OA_BulType bt,CF_OA_Emp u 
where bt.FUserID=u.FID and bt.FIsDeleted = 'false'");
        dt = oa.GetTable(sb.ToString());
        //this.textFType.DataSource = dt;
        //this.textFType.DataTextField = "TypeName";
        //this.textFType.DataValueField = "FID";
        //this.textFType.DataBind();
        this.textFType.Items.Clear();
        this.textFType.Items.Add(new ListItem(" ", " "));
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.textFType.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
            }
        }

    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        DelRow(2);
        DataGreadShow();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        this.txtFTitle.Text = null;

        DataGreadShow();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DataGreadShow();
    }

    protected void BulList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string fid = ((Label)e.Row.FindControl("Label2")).Text;
            e.Row.Cells[1].Text = (e.Row.RowIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            if (temp)
            {
                e.Row.Cells[3].Text = "<a href='#' class='link3' onclick=\"showApproveWindow('LookBull.aspx?fid=" + fid + "',730,500);\">" + e.Row.Cells[3].Text + "</a>";
            }
            string ste = e.Row.Cells[6].Text;
            switch (ste)
            {
                case "审核中":
                    e.Row.Cells[6].Style.Add(HtmlTextWriterStyle.Color, "blue");

                    break;
                case "同意发布":
                    e.Row.Cells[6].Style.Add(HtmlTextWriterStyle.Color, "green");

                    break;
                case "审核未通过":
                    e.Row.Cells[6].Style.Add(HtmlTextWriterStyle.Color, "red");

                    break;
            }
        }

    }
    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        this.temp = false;
        DataGreadShow();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DelRow(3);
        DataGreadShow();
    }
}
