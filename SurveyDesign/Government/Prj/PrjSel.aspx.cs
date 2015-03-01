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
public partial class JSDW_appmain_PrjRegistSel : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }

    //显示 
    void showInfo()
    {
        //当前管理部门
        string FNumber = EConvert.ToString(Session["DFID"]);

        var v = from p in db.CF_Prj_BaseInfo
                where db.CF_App_List.Count(t => t.FPrjId == p.FId && t.FState == 6) > 0//至少要办过一次业务的吧
                    && p.FManageDeptId.ToString().StartsWith(FNumber) //项目的备案部门必须是该管理部门
                    && db.CF_Prj_Stop.Count(t => t.FPrjId == p.FId) == 0 //目前没有终止的
                orderby p.FCreateTime descending
                select new
                {
                    p.FId,
                    p.FPrjName,
                    p.FManageDeptId,
                    jsdw = db.CF_Ent_BaseInfo.Where(a => a.FId == p.FBaseinfoId).Select(a => a.FName).FirstOrDefault(),
                    p.FPrjNo,
                    p.FType,
                };

        //工程名称
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            v = v.Where(t => t.FPrjName.Contains(t_FName.Text));
        }
        //建设单位
        if (!string.IsNullOrEmpty(t_jsBaseName.Text))
        {
            v = v.Where(t => t.jsdw.Contains(t_jsBaseName.Text));
        }

        Pager1.RecordCount = v.Count();
        dg_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }

    //列表绑定
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            string FType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FType"));

            e.Item.Cells[4].Text = db.getDicName(FType);

            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 2].Controls[0] as LinkButton;
            lb.Text = "选择";
            lb.Attributes.Add("onclick", "return confirm('确认要选择该项目吗?');");

            //查询有无正在办理的业务
            string s = "";
            var v = (from t in db.CF_App_List
                     where t.FPrjId == FId
                     && ((t.FManageTypeId != 28001 || (t.FManageTypeId == 28001 && t.FToBaseinfoId.Trim() != "")) && (t.FState == 0 || t.FState == 1))
                     select new { t.FId, t.FName, t.FState }).ToList();
            if (v != null && v.Count > 0)
            {
                s = "<tt>有</tt>";
            }
            else
            {
                s = "无";
            }
            e.Item.Cells[5].Text = s + "，<a href=\"javascript:showAddWindow('../statis/Prjall.aspx?FID=" + FId + "',900,700);\">查看业务办理情况</a>";
        }
    }

    //列表事件
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

    //查询按钮
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
