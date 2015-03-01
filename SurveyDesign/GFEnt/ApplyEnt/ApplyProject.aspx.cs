using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using System.Drawing;
using System.Linq;
using ProjectData;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class GFEnt_ApplyEnt_ApplyProject : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            t_FID.Value = Session["FAppId"].ToString(); ShowInfo();
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
        }
    }
    private void ShowInfo()
    {
        string sql = "select * from YW_GF_YYGC where YWBM='" + Session["FAppId"] + "'";
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();

        sql = string.Format(@"select count(1) from YW_GF_FileList l 
                     inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId
                     where l.FAppid='" + t_FID.Value + "' and l.FType=1002");
        string cou = sh.GetSignValue(sql);
        btnUP.Attributes.Add("value", "文件上传(" + cou + ")");
    }

    //刷新按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "GCID"));
        string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "GCMC"));

        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('addProject.aspx?fid=" + FID + "',800,600);\" >" + FName + "</a>";
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
        }
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        sl.Add("YW_GF_YYGC", "GCID");
        tool.DelInfoFromGrid(this.DG_List, sl, "dbShare"); ShowInfo();
    }

    public void readOnly()
    {
        Submit1.Attributes.Add("disabled", "disabled"); btnDel.Enabled = false;

    }

}