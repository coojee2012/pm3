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
using Approve.RuleCenter;
public partial class JSDW_appmain_EmpListSel : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //如果是多选
            if (Request.QueryString["ckAll"] == "1")
            {
                btnOk.Visible = dg_List.Columns[0].Visible = true;
                dg_List.Columns[dg_List.Columns.Count - 2].Visible = false;
            }
            BindControl();
            showInfo();
        }
    }
    void BindControl()
    {
        int iMType = EConvert.ToInt(Request.QueryString["fsysId"]);
        if (iMType > 0)
        {
            lTitle.Text = db.CF_Sys_SystemName.Where(t => t.FNumber == iMType).Select(t => t.FName).FirstOrDefault();
        }
    }
    //显示 
    void showInfo()
    {
        var App = db.CF_Emp_BaseInfo.Where(t => t.FBaseInfoID == CurrentEntUser.EntId).OrderByDescending(t => t.FCreateTime).Select(t => new { t.FId, t.FName, t.FRegistSpecialId, t.FIdCard,t.FCertiNo });
        string fAppId = Request.QueryString["FAppId"];
        if (!string.IsNullOrEmpty(fAppId))
        {
            int iMType = EConvert.ToInt(Request.QueryString["fsysId"]);
            if (iMType > 0)
                App = App.Where(t => !db.CF_Prj_Emp.Where(l => l.FType == iMType && l.FAppId == fAppId && !l.FIsDeleted.GetValueOrDefault()).Select(l => l.FEmpBaseInfo).Contains(t.FId));
        }
        if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
            App = App.Where(t => t.FName.Contains(t_FName.Text.Trim()));
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 2].Controls[0] as LinkButton;
            lb.Text = "选择";
            lb.Attributes.Add("onclick", "return confirm('确认要选择该人员吗?');");
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            if (e.CommandName == "Sel")
            {
                string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
            }
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        int iCount = dg_List.Columns.Count;
        int ii = 0;

        //选择的人员类型
        int FType = EConvert.ToInt(Request.QueryString["fsysid"]);
        //业务FID
        string FAppId = Request.QueryString["FAppId"];
        //查出所有人（变前、变后的）
        IQueryable<CF_Prj_Emp> allEmp = db.CF_Prj_Emp.Where(t => t.FAppId == FAppId && t.FType == FType);

        for (int i = 0; i < dg_List.Items.Count; i++)
        {
            CheckBox box = dg_List.Items[i].Cells[0].Controls[1] as CheckBox;
            if (box.Checked)
            {
                string fEmpId = dg_List.Items[i].Cells[iCount - 1].Text;
                string fName = dg_List.Items[i].Cells[2].Text;
                string FZY = dg_List.Items[i].Cells[4].Text;

                //查出首次时有没有选过这个人
                CF_Prj_Emp emp = allEmp.Where(t => t.FEmpBaseInfo == fEmpId).FirstOrDefault();
                if (emp != null)
                { //选过
                    emp.FIsDeleted = false;//恢复
                }
                else
                {
                    emp = new CF_Prj_Emp();
                    emp.FId = Guid.NewGuid().ToString();
                    emp.FName = fName;
                    emp.FIsDeleted = false;
                    emp.FCreateTime = DateTime.Now;
                    emp.FTime = DateTime.Now;
                    emp.FEmpBaseInfo = fEmpId;
                    emp.FAppId = FAppId;
                    emp.FMajor = FZY;
                    emp.FType = FType;
                    emp.FDataFrom = (Request.QueryString["c"] == "1" ? 1 : 0);//是变更时选的人
                    db.CF_Prj_Emp.InsertOnSubmit(emp);
                }
                ii++;
            }
        }
        pageTool tool = new pageTool(this.Page);
        if (ii > 0)
        {
            db.SubmitChanges();
            showInfo();
            tool.showMessageAndRunFunction("操作成功！", "window.returnValue='1';window.close();");
        }
        else
        {
            tool.showMessage("请选择人员！");
        }
    }
}
