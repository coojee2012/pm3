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
using System.Collections;
using System.Web.UI.HtmlControls;
public partial class JSDW_appmain_ApplyBaseInfo : EntAppPage
{
    ProjectDB db = new ProjectDB();
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        if (!IsPostBack)
        {
            BindControl();
            ShowPrjInfo();
            ShowEntInfo();
            showInfo();
        }
    }
    /// <summary>
    /// 显示工程信息
    /// </summary>
    void ShowPrjInfo()
    {
        string fPrjId = db.CF_Prj_Data.Where(t => t.FId == EConvert.ToString(Session["FAppId"])).Select(t => t.FPrjId).FirstOrDefault();
        CF_Prj_BaseInfo prj = db.CF_Prj_BaseInfo.Where(t => t.FId == fPrjId).FirstOrDefault();
        if (prj != null)
        {
            pageTool tool = new pageTool(this.Page, "p_");
            tool.fillPageControl(prj);
            govd_FRegistDeptId.fNumber = p_FAddressDept.Value;
        }


    }
    /// <summary>
    /// 显示企业信息
    /// </summary>
    void ShowEntInfo()
    {
        CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == CurrentEntUser.EntId).FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, "e_");
            tool.fillPageControl(ent);
            e_FAddress.Text = ent.FEmail;
        }
        e_FBaseInfoId.Value = CurrentEntUser.EntId;
        e_FId.Value = string.Empty;
    }
    void BindControl()
    {
        govd_FRegistDeptId.fNumber = ComFunction.GetDefaultDept();
        govd_FRegistDeptId.Dis(3);
    }
    //显示
    private void showInfo()
    {
        string fAppId = EConvert.ToString(Session["FAppId"]);
        //显示各单位信息
        ShowPrjEnt("e_", fAppId, 100);
        ShowPrjEnt("k_", fAppId, 15501);
        ShowPrjEnt("j_", fAppId, 126);
        //显示data信息
        var prjData = db.CF_Prj_Data.Where(t => t.FId == fAppId).Select(t => new { t.FTxt10, t.FTxt11, t.FInt3 }).FirstOrDefault();
        if (prjData != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(prjData);
            if (prjData.FInt3 == 1)
            {
                this.t_FInt3.Checked = true;
                otherKC.Attributes.Add("style", "display:block");
                mainKC.Attributes.Add("style", "display:block");
            }
            else if (prjData.FInt3 == 0)
            {
                this.t_FInt3.Checked = false;
            }
        }
        ShowPrjEnt(fAppId, 15502);//显示其他勘察单位信息
    }
    void ShowPrjEnt(string tag, string fAppId, int entType)
    {
        var ent = db.CF_Prj_Ent.Where(t => t.FAppId == fAppId
            && t.FEntType == entType).FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, tag);
            tool.fillPageControl(ent);
        }
    }
    //显示其他勘察单位信息
    void ShowPrjEnt(string fAppId, int entType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fid, fname from CF_Prj_Ent where fenttype=" + entType);
        sb.Append(" and fappid='" + fAppId + "'");
        RCenter rc = new RCenter();
        DataTable dt = rc.GetTable(sb.ToString());
        this.repeaterDisplay.DataSource = dt;
        this.repeaterDisplay.DataBind();


    }
    //保存
    private void saveInfo()
    {
        ProjectDB pdb = new ProjectDB();
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        string fId = EConvert.ToString(Session["FAppId"]);
        CF_Prj_Data Emp = pdb.CF_Prj_Data.Where(t => t.FId == fId).FirstOrDefault();
        if (Emp == null)
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }

        Emp.FBaseInfoId = CurrentEntUser.EntId;
        Emp.FTime = dTime;
        Emp.FPrjName = p_FPrjName.Text.Trim();
        Emp.FTxt10 = t_FTxt10.Text.Trim();
        Emp.FTxt11 = t_FTxt11.Text.Trim();
        GetEnt("e_", e_FId.Value, 100, null, pdb);//建设单位
        GetEnt("k_", k_FId.Value, 15501, null, pdb);//勘察单位
        if (!string.IsNullOrEmpty(j_FBaseInfoId.Value))
            GetEnt("j_", j_FId.Value, 126, null, pdb);//见证单位 
        pdb.SubmitChanges();
        tool.showMessage("保存成功");
    }
    void GetEnt(string tag, string fid, int entType, SortedList list, ProjectDB pdb)
    {
        string fPrjId = db.CF_Prj_Data.Where(t => t.FId == EConvert.ToString(Session["FAppId"])).Select(t => t.FPrjId).FirstOrDefault();
        CF_Prj_Ent Ent = new CF_Prj_Ent();
        if (!string.IsNullOrEmpty(fid))
            Ent = pdb.CF_Prj_Ent.Where(t => t.FId == fid).FirstOrDefault();
        else
        {
            Ent.FCreateTime = DateTime.Now;
            pdb.CF_Prj_Ent.InsertOnSubmit(Ent);
        }
        pageTool tool = new pageTool(this.Page, tag);
        Control c = this.form1;
        if (entType == 15502)//勘察
            c = this.otherKC;
        Ent = tool.getPageValue(Ent, c);
        Ent = tool.getPageValue(Ent, otherKC);
        if (string.IsNullOrEmpty(Ent.FId))
            Ent.FId = Guid.NewGuid().ToString();
        if (list != null)
        {
            Ent.FName = list["fname"].ToString();
            Ent.FCertiNo = list["fcertino"].ToString();
            Ent.FLevelName = list["flevelname"].ToString();
        }
        Ent.FEntType = entType;
        Ent.FTime = DateTime.Now;
        Ent.FPrjId = fPrjId;
        Ent.FAppId = EConvert.ToString(Session["FAppId"]);
        Ent.FIsDeleted = false;


        HtmlInputHidden tBox = form1.FindControl(tag + "FId") as HtmlInputHidden;
        if (tBox != null)
        {
            tBox.Value = Ent.FId;
        }
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        if (btn.CommandName == "KC")//勘察
        {
            var App = (from b in db.CF_Ent_BaseInfo
                       join c in db.CF_Ent_QualiCerti
                       on b.FId equals c.FBaseInfoId
                       where c.FId == k_FBaseInfoId_c.Value
                       select new
                       {
                           b.FName,
                           c.FCertiNo,
                           c.FLevelName
                       }).FirstOrDefault();
            if (App != null)
            {
                pageTool tool = new pageTool(this.Page, "k_");
                tool.fillPageControl(App);
            }
        }
        else if (btn.CommandName == "JZ")//见证单位
        {
            var v = db.CF_Ent_BaseInfo.Where(t => t.FId == j_FBaseInfoId.Value).Select(t => new { t.FName }).FirstOrDefault();
            if (v != null)
            {
                j_FName.Text = v.FName;
            }
        }
    }
    //其他勘察单位列表 新增按钮

    protected void btnAddKC_Click(object sender, EventArgs e)
    {
        ProjectDB pdb = new ProjectDB();
        pageTool tool = new pageTool(this.Page);

        //根据客户端返回值多表查询得到数据更新或插入cf_prj_ent表中
        var App = (from b in pdb.CF_Ent_BaseInfo
                   join c in pdb.CF_Ent_QualiCerti
                   on b.FId equals c.FBaseInfoId
                   where c.FId == kc_FBaseInfoId_c.Value
                   select new
                   {
                       b.FName,
                       c.FCertiNo,
                       c.FLevelName
                   }).FirstOrDefault();
        if (App == null)
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }
        SortedList list = new SortedList();
        list.Add("fname", App.FName);
        list.Add("fcertino", App.FCertiNo);
        list.Add("flevelname", App.FLevelName);
        GetEnt("kc_", kc_FId.Value, 15502, list, pdb);//其他勘察单位
        isUnite(pdb, 1);

        pdb.SubmitChanges();
        showInfo();

    }
    //是否联合体num为1时联合体为0时.....
    private void isUnite(ProjectDB pdb, int num)
    {
        pageTool tool = new pageTool(this.Page);
        string fId = EConvert.ToString(Session["FAppId"]);
        CF_Prj_Data Emp = pdb.CF_Prj_Data.Where(t => t.FId == fId).FirstOrDefault();
        if (Emp == null)
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }
        Emp.FInt3 = num;
        Emp.FTime = DateTime.Now;
    }
    //绑定事件
    protected void repeaterDisplay_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            ((Literal)e.Item.FindControl("lit_NO")).Text = (e.Item.ItemIndex + 1).ToString();
        }
    }
    protected void repeaterDisplay_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        ProjectDB pdb = new ProjectDB();
        if (e.CommandName == "Delete")
        {
            string fid = (e.Item.FindControl("btnDel") as LinkButton).CommandArgument;
            var q = from data in pdb.CF_Prj_Ent
                    where data.FId == fid
                    select data;
            foreach (var data in q)
            {
                pdb.CF_Prj_Ent.DeleteOnSubmit(data);
            }

        }
        if (this.repeaterDisplay.Items.Count <= 1)
        {
            isUnite(pdb, 0);
        }
        pdb.SubmitChanges();
        showInfo();
    }
    //清空按钮
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ProjectDB pdb = new ProjectDB();
        string fid = j_FId.Value;
        CF_Prj_Ent q = (from data in pdb.CF_Prj_Ent
                        where data.FId == fid
                        select data).FirstOrDefault();
        if (q != null)
        {
            pdb.CF_Prj_Ent.DeleteOnSubmit(q);
            pdb.SubmitChanges();
        }
        j_FBaseInfoId.Value = j_FId.Value = j_FMoney.Text = j_FName.Text
            = j_FPlanDate.Text = string.Empty;
    }
}
