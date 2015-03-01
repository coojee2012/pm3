using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
public partial class JSDW_appmain_AddPrjRegist : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                showInfo();
                ShowPrjItemInfo();
                //判断，有该工程的任何业务，则不可再行删除
                if (db.CF_App_List.Count(t => t.FPrjId
                    == Request.QueryString["fid"]) > 0)
                {
                    btnSave.Enabled = btnSave.Visible = false;
                    btnDel.Visible = btnAdd.Visible = false;
                 
                }
                tabSetup1.Visible = false;
                divSetup2.Visible = true;
            }
            else
            {
                tabSetup1.Visible = true;
                divSetup2.Visible = false;
            }
        }
   RegisterStartupScript("jsEn", "<script>btnEnable();</script>");
        this.RegisterStartupScript("js_tr", "<script>showTr();</script>");
    }

    void BindControl()
    {
        string deptId = ComFunction.GetDefaultDept();
        govd_FRegistDeptId.fNumber = deptId;
        govd_FRegistDeptId.Dis(1);
        RCenter rc = new RCenter();
        DataTable dt = rc.getDicTbByFNumber("20001");
        t_FType.DataSource = dt;
        t_FType.DataTextField = "FName";
        t_FType.DataValueField = "FNumber";
        t_FType.DataBind();

        ddlType.DataSource = dt;
        ddlType.DataTextField = "FName";
        ddlType.DataValueField = "FNumber";
        ddlType.DataBind();

        //备案部门
        StringBuilder sb = new StringBuilder();
        sb.Append("select case FLevel when 1 then FFullName when 2 then '-'+FName when 3 then '---'+FName end FName,");
        sb.Append("FNumber from cf_Sys_ManageDept ");
        sb.Append("where fnumber like '" + deptId + "%' ");
        sb.Append("and fname<>'市辖区' ");
        sb.Append("order by left(FNumber,4),flevel");
        dt = rc.GetTable(sb.ToString());
        t_FManageDeptId.DataSource = dt;
        t_FManageDeptId.DataTextField = "FName";
        t_FManageDeptId.DataValueField = "FNumber";
        t_FManageDeptId.DataBind();

        //资金来源
        dt = rc.getDicTbByFNumber("20002");
        t_FFunds.DataSource = dt;
        t_FFunds.DataTextField = "FName";
        t_FFunds.DataValueField = "FNumber";
        t_FFunds.DataBind();
        t_FFunds.Items.Insert(0, new ListItem("--请选择--", ""));

        //建筑使用性质
        dt = rc.getDicTbByFNumber("20003");
        t_FNature.DataSource = dt;
        t_FNature.DataTextField = "FName";
        t_FNature.DataValueField = "FNumber";
        t_FNature.DataBind();

        //设计规模
        dt = rc.getDicTbByFNumber("20004");
        t_FScale.DataSource = dt;
        t_FScale.DataTextField = "FName";
        t_FScale.DataValueField = "FNumber";
        t_FScale.DataBind();
        t_FScale.Items.Insert(0, new ListItem("--请选择--", ""));

        //建设性质
        dt = rc.getDicTbByFNumber("20005");
        t_FKind.DataSource = dt;
        t_FKind.DataTextField = "FName";
        t_FKind.DataValueField = "FNumber";
        t_FKind.DataBind();
        t_FKind.Items.Insert(0, new ListItem("--请选择--", ""));

        //抗震设防分类标准
        dt = rc.getDicTbByFNumber("20006");
        t_FAntiSeismic.DataSource = dt;
        t_FAntiSeismic.DataTextField = "FName";
        t_FAntiSeismic.DataValueField = "FNumber";
        t_FAntiSeismic.DataBind();
        t_FAntiSeismic.Items.Insert(0, new ListItem("--请选择--", ""));

        //市政行业类别
        dt = rc.getDicTbByFNumber("20007");
        t_FSectors.DataSource = dt;
        t_FSectors.DataTextField = "FName";
        t_FSectors.DataValueField = "FNumber";
        t_FSectors.DataBind();

        //结构类型
        dt = rc.getDicTbByFNumber("509");
        t_FStruType.DataSource = dt;
        t_FStruType.DataTextField = "FName";
        t_FStruType.DataValueField = "FNumber";
        t_FStruType.DataBind();
        t_FStruType.Items.Insert(0, new ListItem("--请选择--", ""));
    }
    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
        CF_Prj_BaseInfo emp = db.CF_Prj_BaseInfo.Where(t => t.FId == Convert.ToString(ViewState["FID"])).FirstOrDefault();
        if (emp != null)
        {
            tool.fillPageControl(emp);
            govd_FRegistDeptId.fNumber = t_FAddressDept.Value;
        }
    }
    /// <summary>
    /// 查询工程项目的类型
    /// </summary>
    string GetPrjType()
    {
        //2000101 房屋建筑工程
        //2000102 市政工程
        return db.CF_Prj_BaseInfo.Where(t => t.FId == ViewState["FID"].ToString())
            .Select(t => t.FType).FirstOrDefault().ToString();
    }
    /// <summary>
    /// 显示单体工程信息
    /// </summary>
    void ShowPrjItemInfo()
    {
        string prjType = GetPrjType();
        if (prjType == "2000101")
        {
            dg_List.Columns[3].HeaderText = "工程设计等级";
            var App = db.CF_PrjItem_BaseInfo.Where(t => t.FPrjId == EConvert.ToString(ViewState["FID"])).Select(t => new
            {
                t.FPrjItemName,
                FDJ = t.FLevel,
                FId = t.FId
            }).ToList();
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
        else if (prjType == "2000102")
        {
            dg_List.Columns[3].HeaderText = "工程类别";
            var App = db.CF_PrjItem_BaseInfo.Where(t => t.FPrjId == EConvert.ToString(ViewState["FID"])).Select(t => new
            {
                t.FPrjItemName,
                FDJ = db.CF_Sys_Dic.Where(d => d.FNumber == t.FType).Select(d => d.FName).FirstOrDefault(),
                FId = t.FId
            }).ToList();
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }
    /// <summary>
    /// 先清空未选择的tr项
    /// </summary>
    void ClearNoTr()
    {
        if (t_FType.SelectedValue == "2000102")
        {
            t_FLandArea.Text = string.Empty;
            t_FArea.Text = string.Empty;
            t_FHeight.Text = string.Empty;
            t_FLayers.Text = string.Empty;
            t_FSize.Text = string.Empty;
            t_FGround.Text = string.Empty;
            t_FUnderground.Text = string.Empty;
        }
        else
        {
            t_FSectors.SelectedIndex = -1;
        }
    }
    string GetPrjNo()
    {
        RCenter rc = new RCenter();
        string fNo = string.Empty;
        if (string.IsNullOrEmpty(fNo))
        {
            //先查询地区
            string fDeptId = govd_FRegistDeptId.fNumber;
            fDeptId = fDeptId.PadRight(6, '0');
            string fDate = DateTime.Now.ToString("yyyyMMdd");
            fNo = fDeptId + fDate;
            //查询最大的号码
            StringBuilder sb = new StringBuilder();
            sb.Append("select max(right(FPrjNo,4)) from cf_Prj_Baseinfo ");
            sb.Append("where FPrjNo like '" + fNo + "%' ");
            if (ViewState["FID"] != null)
                sb.Append("and FId!='" + ViewState["FID"] + "' ");
            sb.Append("and isnumeric(right(FPrjNo,4))=1");
            int iNo = EConvert.ToInt(rc.GetSignValue(sb.ToString())) + 1;
            fNo += iNo.ToString().PadLeft(4, '0');
        }
        return fNo;
    }
    //保存
    private void saveInfo()
    {
        ClearNoTr();
        t_FPrjNo.Text = GetPrjNo();
        t_FAddressDept.Value = govd_FRegistDeptId.fNumber;
        govd_FRegistDeptId.fNumber = t_FAddressDept.Value;
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        string fId = EConvert.ToString(ViewState["FID"]);
        CF_Prj_BaseInfo Emp = new CF_Prj_BaseInfo();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = db.CF_Prj_BaseInfo.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FIsDeleted = false;
            Emp.FCreateTime = dTime;
            db.CF_Prj_BaseInfo.InsertOnSubmit(Emp);
        }
        Emp = tool.getPageValue(Emp);
        Emp.FBaseinfoId = CurrentEntUser.EntId;
        Emp.FTime = dTime;
        db.SubmitChanges();
        ViewState["FID"] = fId;
        ShowPrjItemInfo();
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FDJ"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('AddPrjItem.aspx?fid=" + fid + "&fprjId=" + ViewState["FID"] + "',800,450);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, db.CF_PrjItem_BaseInfo);
        ShowPrjItemInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowPrjItemInfo();
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        ViewState["Type"] = ddlType.SelectedValue;
        tabSetup1.Visible = false;
        divSetup2.Visible = true;
        ListItem li = t_FType.Items.FindByValue(EConvert.ToString(ViewState["Type"]));
        if (li != null)
        {
            t_FType.ClearSelection();
            li.Selected = true;

        }
    }
}
