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
public partial class OA_Bulletin_BullType : System.Web.UI.Page
{

    string userId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FEmpID"] != null)
        {
            userId = Session["FEmpID"].ToString();
        }
        if (!IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            
            if (userId != null && userId != "")
            {
                DataGreadShow();
            }
        }


    }
    protected void DelRow()
    {
        pageTool tool = new pageTool(this.Page);
        OA oa = new OA();

        int count = 0;
        for (int i = 0; i < this.LogList.Rows.Count; i++)
        {
            CheckBox cbx = (CheckBox)this.LogList.Rows[i].FindControl("CheckItem");

            if (cbx.Checked)
            {
                string FID = ((Label)LogList.Rows[i].FindControl("Label2")).Text;
                //for (int x = 0; x < LogList.Rows[i].Cells.Count; x++)
                //{
                //    FID = this.LogList.Rows[i].Cells[LogList.Rows[i].Cells.Count-1];
                //}
                string sqlstr = "update CF_OA_BulType set FIsDeleted = 'true' where FID='" + FID + "'";
                if (oa.PExcute(sqlstr))
                {
                    count++;
                }

            }
        }
        if (count == 0)
        {
            tool.showMessage("请选择要删除的数据！");
        }
        else
        {
            tool.showMessage("成功删除了" + count + "项数据！");
        }


    }
    protected void DataGreadShow()
    {

        StringBuilder sb = new StringBuilder();
        sb.Append("select u.fname,bt.FID,bt.TypeName,bt.FCratetime ");
        sb.Append("from CF_OA_BulType bt,CF_OA_Emp u ");
        sb.Append("where bt.FUserID=u.FID");
        sb.Append(" and bt.FIsDeleted = 'false'");
        if (this.txtFTitle.Text != null && this.txtFTitle.Text.Trim() != "")
        {
            sb.Append(" and bt.TypeName like '%"+this.txtFTitle.Text+"%'");
        }
        sb.Append(" order by bt.FCratetime");
        this.Pager1.controltopage = "LogList";
        this.Pager1.className = "dbOA";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 15;
        this.Pager1.controltype = "GridView";
        this.Pager1.dataBind();

    }
    protected void LogList_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        int temp = (e.Row.RowIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1));

        if (temp > 0)
        {
            
            e.Row.Cells[1].Text = (e.Row.RowIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            
        }


    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        DelRow();
        DataGreadShow();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        DataGreadShow();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DataGreadShow();
    }

}
