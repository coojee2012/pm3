
using Approve.Common;
using Approve.RuleApp;
using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplySGXKZGL_EmpClash : govBasePage
{
    RCenter rc = new RCenter();
    SaveAsBase sab = new SaveAsBase();
    RApp ra = new RApp();
    RAppBacth rap = new RAppBacth();
    ProjectDB db = new ProjectDB();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.Page_Load(sender, e);
            ShowInfo();
        }
    }

    private void ShowInfo()
    {
        string fAppid = Session["fAppid"].ToString();
        RCenter rc = new RCenter();
        DataTable dt = rc.GetTable("exec Proc_Cx_EmpClash '"+fAppid + "'");
        empList.DataSource = dt;
        empList.DataBind();
    }


 
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));


            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["fId"] = fId;
            box.Attributes["fAppId"] = fAppId;
            box.Attributes["name"] = fAppId;


            //e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            //e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('GCXX.aspx?FId=" + fId + "&FAppId=" + fAppId + "',900,600);\">" + e.Item.Cells[2].Text + "</a>";
            //e.Item.Cells[3].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('SGXKZXX.aspx?FId=" + fId + "&FAppId=" + fAppId + "',900,600);\">" + e.Item.Cells[3].Text + "</a>";
            //if (e.Item.Cells[7].Text.Contains("已开工"))
            //{
            //    e.Item.Cells[7].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('JGSZ.aspx?FId=" + fId + "&FAppId=" + fAppId + "',800,400);\">" + e.Item.Cells[7].Text + "</a>";
            //}
        }
    }
 
}