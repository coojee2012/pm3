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
using Approve.EntityBase;
public partial class JSDW_appmain_AddPrjRegist : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["IsView"]) && Request.QueryString["IsView"] == "1")
        {
            this.ClientScript.RegisterStartupScript(this.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>try{btnEnable();}catch(e){}</script>");
            DG_List.Columns[DG_List.Columns.Count - 2].Visible = false;
        }
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["fprjId"]))
            {
                Response.Clear();
                Response.End();
            }
            else
            {
                //判断，有该工程的任何业务，则不可再行删除
                if (db.CF_App_List.Count(t => t.FPrjId
                    == Request.QueryString["fprjId"]) > 0)
                {
                    int iCount = DG_List.Columns.Count;
                    DG_List.Columns[iCount - 2].Visible = DG_List.Columns[iCount - 3].Visible = btnAdd.Visible = btnSave.Enabled = btnSave.Visible = false;
                    RegisterStartupScript("jsEn", "<script>btnEnable();</script>");
                }
            }
            string prjType = GetPrjType();
            if (!string.IsNullOrEmpty(prjType))
                ViewState["FPrjType"] = prjType;
            else
            {
                Response.Clear();
                Response.End();
            }
            BindControl();
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                showInfo();
            }
            ShowParameter();
        }
    }
    /// <summary>
    /// 查询工程项目的类型
    /// </summary>
    string GetPrjType()
    {
        //2000101 房屋建筑工程
        //2000102 市政工程
        return db.CF_Prj_BaseInfo.Where(t => t.FId == Request.QueryString["fprjId"])
            .Select(t => t.FType).FirstOrDefault().ToString();
    }
    void BindControl()
    {
        div_t1.Visible = false;
        div_t2.Visible = false;
        string fType = EConvert.ToString(ViewState["FPrjType"]);
        if (fType == "2000101")
        {
            lTitle.Text = "房屋建筑";
            div_t1.Visible = true;
        }
        else if (fType == "2000102")
        {
            lTitle.Text = "市政";
            div_t2.Visible = true;
        }
        DataTable dt = rc.getDicTbByFNumber("20007");
        p_FType.DataSource = dt;
        p_FType.DataTextField = "FName";
        p_FType.DataValueField = "FNumber";
        p_FType.DataBind();
        p_FType.Items.Insert(0, new ListItem("--请选择--", ""));

        //建筑使用性质
        dt = rc.getDicTbByFNumber("20003");
        t_FNature.DataSource = dt;
        t_FNature.DataTextField = "FName";
        t_FNature.DataValueField = "FNumber";
        t_FNature.DataBind();

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

        CF_PrjItem_BaseInfo emp = db.CF_PrjItem_BaseInfo.Where(t => t.FId == Convert.ToString(ViewState["FID"])).FirstOrDefault();
        if (emp != null)
        {
            if (div_t1.Visible)
            {
                pageTool tool = new pageTool(this.Page);
                tool.fillPageControl(emp, div_t1);
            }
            else
            {
                pageTool tool = new pageTool(this.Page, "p_");
                tool.fillPageControl(emp, div_t2);
            }


        }
    }
    //保存
    private void saveInfo()
    {
        DateTime dTime = DateTime.Now;
        string fId = EConvert.ToString(ViewState["FID"]);
        CF_PrjItem_BaseInfo Emp = new CF_PrjItem_BaseInfo();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = db.CF_PrjItem_BaseInfo.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FCreateTime = dTime;
            db.CF_PrjItem_BaseInfo.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        if (div_t1.Visible)
        {
            tool = new pageTool(this.Page, "t_");
            Emp = tool.getPageValue(Emp, div_t1);
        }
        else if (div_t2.Visible)
        {
            tool = new pageTool(this.Page, "p_");
            Emp = tool.getPageValue(Emp, div_t2);
        }
        Emp.FPrjId = Request.QueryString["fprjId"];
        Emp.FIsDeleted = false;
        Emp.FTime = dTime;
        db.SubmitChanges();
        ViewState["FID"] = fId;
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }


    private void ShowParameter()
    {
        string FPrjItem = EConvert.ToString(ViewState["FID"]);
        var result = db.CF_Prj_Parameter.Where(t => t.FPrjItem == FPrjItem);
        DG_List.DataKeyField = "FID";
        DG_List.DataSource = result;
        DG_List.DataBind();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FType"));
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
            e.Item.Cells[1].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('AddParameter.aspx?fid=" + fid + "&fprjId=" + Request.QueryString["fprjId"] + "&FPrjItem=" +
                ViewState["FID"] + "&IsView=" + Request.QueryString["IsView"] + "&type=" + p_FType.SelectedValue + "',300, 250);\">" + rc.GetDicName(FType) + "</a>";

        }
    }
    protected void DG_List_EditCommand(object source, DataGridCommandEventArgs e)
    {
        DG_List.EditItemIndex = e.Item.ItemIndex;
        ShowParameter();
    }
    protected void DG_List_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        string FId = DG_List.DataKeys[e.Item.ItemIndex].ToString();
        var result = db.CF_Prj_Parameter.Where(t => t.FId == FId).FirstOrDefault();
        if (result != null)
        {
            TextBox txtFValue = e.Item.FindControl("txtFValue") as TextBox;
            if (txtFValue != null)
            {
                result.FValue = txtFValue.Text;
                db.SubmitChanges();
            }
        }
        DG_List.EditItemIndex = -1;
        ShowParameter();
    }
    protected void DG_List_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        DG_List.EditItemIndex = -1;
        ShowParameter();
    }
    protected void btnSave1_Click(object sender, EventArgs e)
    {
        ShowParameter();
    }
    protected void DG_List_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        string FId = DG_List.DataKeys[e.Item.ItemIndex].ToString();
        var result = db.CF_Prj_Parameter.Where(t => t.FId == FId).FirstOrDefault();
        if (result != null)
        {
            db.CF_Prj_Parameter.DeleteOnSubmit(result);

            db.SubmitChanges();

        }
        ShowParameter();
    }
}
