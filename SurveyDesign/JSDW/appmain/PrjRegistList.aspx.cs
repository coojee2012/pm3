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
public partial class JSDW_appmain_PrjRegistList : System.Web.UI.Page
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
        var v = from t in db.CF_Prj_BaseInfo
                where t.FBaseinfoId == CurrentEntUser.EntId
                && t.FIsBG != 1//非变更
                orderby t.FCreateTime descending
                select new
                {
                    t.FId,
                    t.FPrjNo,
                    t.FManageDeptId,
                    t.FAddressDept,
                    t.FType,
                    t.FAllMoney,
                    t.FPrjName,
                    t.FLinkId,//各项目间的关联字段
                    overType = db.CF_Prj_Stop.Where(s => s.FPrjId == t.FId).Select(s => s.FTYpe).FirstOrDefault(),
                    //看有没有变更过，
                    BG = (db.CF_Prj_BaseInfo.Count(b => b.FLinkId == t.FLinkId) > 1)

                };
        if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
            v = v.Where(t => t.FPrjName.Contains(t_FName.Text.Trim()));
        Pager1.RecordCount = v.Count();
        dg_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }

    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string fLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('AddPrjRegist.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
            string fDeptId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAddressDept"));
            if (!string.IsNullOrEmpty(fDeptId))
                fDeptId = rc.getDept(fDeptId, 1);
            e.Item.Cells[4].Text = fDeptId;
            string fType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FType"));
            if (!string.IsNullOrEmpty(fType))
                fType = rc.GetDicName(fType);
            e.Item.Cells[5].Text = fType;
            //查看项目办理情况
            LinkButton lb = e.Item.FindControl("btnLink") as LinkButton;
            //判断，有该工程的任何业务，则不可再行删除|或者有历史记录
            if (db.CF_App_List.Count(t => t.FPrjId == fid) > 0)
            {
                CheckBox box = e.Item.Cells[0].Controls[1] as CheckBox;
                box.Enabled = false;
                e.Item.Cells[0].ToolTip = "该工程已经参与了业务办理，不可修改和删除！";
                lb.Text = "点击查看";
                lb.Attributes.Add("onclick", "showAddWindow('../Statistics/all.aspx?FID=" + fid + "',900,700);return false;");
            }
            else if (db.CF_Prj_BaseInfo.Count(t =>
                t.FLinkId == fLinkId && t.FIsBG == 1) > 0)
            {
                CheckBox box = e.Item.Cells[0].Controls[1] as CheckBox;
                box.Enabled = false;
                e.Item.Cells[0].ToolTip = "该工程已经有变更记录，不可删除！";
                lb.Text = "点击查看";
                lb.Attributes.Add("onclick", "showAddWindow('../Statistics/all.aspx?FLinkId=" + fLinkId + "&FId=" + fid + "',900,700);return false;");
            }
            else
            {
                lb.Text = "无办理情况";
                lb.Enabled = false;
            }
            //是否已终止
            Button btnOp = (Button)e.Item.FindControl("btnOp");
            string s = "";
            string overType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "overType"));
            if (!string.IsNullOrEmpty(overType))
            {
                if (overType == "1")
                { //自已终止的
                    s = "<tt>已终止</tt>[自行终止]";

                    btnOp.Attributes["onclick"] = "return confirm('确定要恢复吗？');";
                    btnOp.CommandArgument = fid;
                    btnOp.CommandName = "Rest";
                    btnOp.Text = "恢复";
                }
                else if (overType == "2")
                {//主管部门终止的
                    s = "<tt>已终止</tt>[主管部门终止]";
                    btnOp.Visible = false;
                }
                s = "<a href=\"javascript:showAddWindow('AddStopPrj.aspx?FPrjId=" + fid + "',600,400);\">" + s + "</a>";
            }
            else
            {
                s = "<font color='green'>正常</font>";
                btnOp.Attributes["onclick"] = "showAddWindow('AddStopPrj.aspx?FPrjId=" + fid + "',600,400);return false;";
                btnOp.Text = "终止";
            }

            ((Literal)e.Item.FindControl("lit_FState")).Text = s;
        }

        //看有没有变更过，
        bool BG = EConvert.ToBool(DataBinder.Eval(e.Item.DataItem, "BG"));
        if (BG)
            e.Item.Cells[2].Text = "<img src=\"../../image/tip.gif\" style=\"cursor:pointer\" title=\"该项目有过变更！\"/>" + e.Item.Cells[2].Text;


    }
    //列表操作
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Rest")
        {
            string FPrjId = e.CommandArgument.ToString();
            CF_Prj_Stop s = db.CF_Prj_Stop.Where(t => t.FPrjId == FPrjId).FirstOrDefault();
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

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, db.CF_Prj_BaseInfo, tool_Deleting);
        showInfo();
    }
    //合同备案删除
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        ProjectDB db = context as ProjectDB;
        if (db != null)
        {
            //单体工程
            var pro = db.CF_PrjItem_BaseInfo.Where(t => FIdList.ToArray().Contains(t.FPrjId));
            db.CF_PrjItem_BaseInfo.DeleteAllOnSubmit(pro);
            //参数列表
            var para = db.CF_Prj_Parameter.Where(t => FIdList.ToArray().Contains(t.FPrjId));
            db.CF_Prj_Parameter.DeleteAllOnSubmit(para);
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
