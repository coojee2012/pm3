using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using ProjectBLL;
using Tools;
using Approve.RuleCenter;
using System.Text;
using System.Data;
using Approve.EntityBase;

public partial class Government_AppEntAction_GoodManageList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
            ShowInfo();
        }
    }






    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        DataTable dt = rc.GetTable("select replace(fdesc,'资质办理','') FDesc, fnumber,FQurl from cf_Sys_SystemName where fplatid=800 order by fnumber");

        dbFSystemId.DataSource = dt;
        dbFSystemId.DataTextField = "FDesc";
        dbFSystemId.DataValueField = "FNumber";
        dbFSystemId.DataBind();
        dbFSystemId.Items.Insert(0, new ListItem("请选择", ""));


        //DataTable dt = rc.GetTable(" select fsystemid from cf_ent_baseinfo group by fsystemid ");
        //dbFSystemId.DataSource = dt;
        //dbFSystemId.DataTextField = "FName";
        //dbFSystemId.DataValueField = "FNumber";
        //dbFSystemId.DataBind();


        if (!string.IsNullOrEmpty(Request.QueryString["sysId"]))
        {
            dbFSystemId.SelectedIndex = dbFSystemId.Items.IndexOf(dbFSystemId.Items.FindByValue(Request.QueryString["sysId"]));
            dbFSystemId.Enabled = false;
        }




    }


    protected void btn_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    private void ShowInfo()
    {
        var v = db.CF_Ent_GoodAction.Where(t => t.FState == 6);
        if (txtFEntName.Text.Trim() != "")
        {
            v = v.Where(t => t.FProjectName.Contains(txtFEntName.Text));
        }
        if (dbFSystemId.SelectedValue != "")
        {
            v = v.Where(t => t.FTxt12 == dbFSystemId.SelectedValue);
        }
        if (txtFDeptIdName.Text.Trim() != "")
        {
            v = v.Where(t => t.FDeptIdname.Contains(txtFDeptIdName.Text));
        }
        if (this.dbFState.SelectedValue != "")
        {
            v = v.Where(t => t.FTxt13 == this.dbFState.SelectedValue);
        }
        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowInfo();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.PageCount * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FTxt13 = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FTxt13"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('GoodInfo.aspx?fid=" + fid + "',800,600);\">" + e.Item.Cells[2].Text + "</a>";
            e.Item.Cells[7].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('GoodInfo.aspx?fid=" + fid + "',800,600);\">" + e.Item.Cells[7].Text + "</a>";

            switch(FTxt13){
                case "1":
                    e.Item.Cells[6].Text = "已发布";
                    break;
                default:
                    e.Item.Cells[6].Text = "未发布";
                    break;
            }
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string FId = e.CommandArgument.ToString();
        pageTool tool = new pageTool(this.Page);
        if (e.CommandName == "Report") // 
        {
            CF_Prj_BadReport report = db.CF_Prj_BadReport.Where(t => t.FId == FId).FirstOrDefault();
            if (report != null)
            {
                report.FState = 1;
                db.SubmitChanges();
                tool.showMessage("操作成功");
            }
        }
        else if (e.CommandName == "Delete") // 
        {

            CF_Prj_BadReport report = db.CF_Prj_BadReport.Where(t => t.FId == FId).FirstOrDefault();
            if (report != null)
            {
                db.CF_Prj_BadReport.DeleteOnSubmit(report);
                db.SubmitChanges();
                tool.showMessage("删除成功");
            }
        }

        ShowInfo();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }


    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool to = new pageTool(this.Page);
        int count = DG_List.Items.Count;
        int x = 0;
        for (int i = 0; i < count; i++)
        {
            CheckBox cb = DG_List.Items[i].Cells[0].Controls[1] as CheckBox;
            if (cb.Checked)
            {
                string fid = DG_List.Items[i].Cells[DG_List.Columns.Count - 1].Text;
                CF_Ent_GoodAction good = db.CF_Ent_GoodAction.Where(t => t.FID == fid).FirstOrDefault();
                good.FTxt13 = "1";
                x++;
            }
        }
        if (x == 0)
        {
            to.showMessage("请选择需要发布的信息！");
            return;
        }
        db.SubmitChanges();
        to.showMessage("发布成功！");
        ShowInfo();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pageTool to = new pageTool(this.Page);
        int count = DG_List.Items.Count;
        int x = 0;
        for (int i = 0; i < count; i++)
        {
            CheckBox cb = DG_List.Items[i].Cells[0].Controls[1] as CheckBox;
            if (cb.Checked)
            {
                string fid = DG_List.Items[i].Cells[DG_List.Columns.Count - 1].Text;
                CF_Ent_GoodAction good = db.CF_Ent_GoodAction.Where(t => t.FID == fid).FirstOrDefault();
                good.FTxt13 = "0";
                x++;
            }
        }
        if (x == 0)
        {
            to.showMessage("请选择撤销发布的信息！");
            return;
        }
        db.SubmitChanges();
        to.showMessage("撤销发布成功！");
        ShowInfo();
    }
}
