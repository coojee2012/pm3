using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;

public partial class Government_Prj_PrjList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            conBind();
            showInfo();
        }
    }

    private void conBind()
    { 

        //工程所属区域
        govd_FRegistDeptId.fNumber = EConvert.ToString(Session["DFID"]);
        govd_FRegistDeptId.Dis(EConvert.ToInt(Session["DFLevel"]));
    }

    //显示
    private void showInfo()
    {
        var v = from t in db.CF_Prj_BaseInfo
                join s in db.CF_Prj_Stop on t.FId equals s.FPrjId
                orderby s.FCreateTime descending
                select new
                {
                    s.FID,
                    t.FPrjName, 
                    t.FAddressDept,
                    jsdw = db.CF_Ent_BaseInfo.Where(a => a.FId == t.FBaseinfoId).Select(a => a.FName).FirstOrDefault(),
                    s.FTYpe,
                    s.FRemark,
                    s.FAppDate,
                };

        //工程所属区域
        if (!string.IsNullOrEmpty(govd_FRegistDeptId.FNumber))
        {
            v = v.Where(t => t.FAddressDept.Contains(govd_FRegistDeptId.FNumber));
        }
        //项目终止方式
        if (!string.IsNullOrEmpty(t_FType.SelectedValue))
        {
            v = v.Where(t => t.FTYpe.ToString() == t_FType.SelectedValue);
        }
        //工程名称
        if (!string.IsNullOrEmpty(t_prjName.Text))
        {
            v = v.Where(t => t.FPrjName.Contains(t_prjName.Text));
        }
        //建设单位
        if (!string.IsNullOrEmpty(t_jsBaseName.Text))
        {
            v = v.Where(t => t.jsdw.Contains(t_jsBaseName.Text));
        }

        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }

    //列表
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FType"));


            //地区
            e.Item.Cells[3].Text = db.getDeptName(DataBinder.Eval(e.Item.DataItem, "FAddressDept"));

            e.Item.Cells[6].Text = FType == "1" ? "<tt>建设单位终止</tt>" : "<font color='blue'>主管部门终止</font>";


            Button btnOp = (Button)e.Item.FindControl("btnOp");

            btnOp.Attributes["onclick"] = "return confirm('确定要恢复吗？');";
            btnOp.CommandArgument = FID;
            btnOp.CommandName = "Rest";
            btnOp.Text = "恢复";
        }
    } 
    //列表操作
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Rest")
        {
            string FID = e.CommandArgument.ToString();
            CF_Prj_Stop s = db.CF_Prj_Stop.Where(t => t.FID == FID).FirstOrDefault();
            if (s != null)
            {
                db.CF_Prj_Stop.DeleteOnSubmit(s);
                db.SubmitChanges();

                pageTool Tool = new pageTool(this.Page);
                Tool.showMessage("恢复成功");
                showInfo();
            }
        }
    }

    //查询按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
