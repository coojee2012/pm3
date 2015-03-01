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
public partial class JSDW_applycbsjwt_ApplyBaseInfo : EntAppPage
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
        string fPrjId = db.CF_App_List.Where(t => t.FId == EConvert.ToString(Session["FAppId"])).Select(t => t.FPrjId).FirstOrDefault();
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
        ShowPrjEnt("k_", fAppId, 155);
        //显示data信息
        var prjData = db.CF_Prj_Data.Where(t => t.FAppId == fAppId).Select(t => new { t.FTxt10, t.FTxt11, t.FFloat10 }).FirstOrDefault();
        if (prjData != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(prjData);
            if (prjData.FFloat10 == 1)//设计
            {
                t_FFloat10.Checked = true;
                otherSJ.Attributes.Add("style", "display:block");
                mainSJ.Attributes.Add("style", "display:block");
            }
            else
            {
                this.t_FFloat10.Checked = false;
            }
        }
        ShowPrjEnt(fAppId, 15503, repeaterDisplay);//显示设计联合体
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
    //显示其他子单位信息
    void ShowPrjEnt(string fAppId, int entType, Repeater rep)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fid,fname from CF_Prj_Ent where fenttype=" + entType);
        sb.Append(" and fappid='" + fAppId + "'");
        RCenter rc = new RCenter();
        DataTable dt = rc.GetTable(sb.ToString());
        rep.DataSource = dt;
        rep.DataBind();
    }
    //保存
    private void saveInfo()
    {
        ProjectDB pdb = new ProjectDB();
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        string fId = EConvert.ToString(Session["FAppId"]);
        CF_Prj_Data Emp = pdb.CF_Prj_Data.Where(t => t.FAppId == fId).FirstOrDefault();
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
        GetEnt("k_", k_FId.Value, 155, null, pdb);//设计单位 
        pdb.SubmitChanges();
        tool.showMessage("保存成功");
    }
    void GetEnt(string tag, string fid, int entType, SortedList list, ProjectDB pdb)
    {
        string fPrjId = db.CF_Prj_Data.Where(t => t.FAppId == EConvert.ToString(Session["FAppId"])).Select(t => t.FPrjId).FirstOrDefault();
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
        if (entType == 15503)//设计
            c = this.otherSJ;
        Ent = tool.getPageValue(Ent, c);
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
        if (btn.CommandName == "SJ")//设计单位
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
    }
    //其他联合体单位列表 新增按钮 
    protected void btnAddKC_Click(object sender, EventArgs e)
    {
        ProjectDB pdb = new ProjectDB();
        pageTool tool = new pageTool(this.Page);
        //根据客户端返回值多表查询得到数据更新或插入cf_prj_ent表中
        string cId = string.Empty;
        string tag = string.Empty;
        string colName = string.Empty;
        int fsysid = EConvert.ToInt(((Button)sender).CommandArgument);
        if (fsysid == 15503)
        {
            cId = sj_FBaseInfoId_c.Value;
            tag = "sj_";
            colName = "FFloat10";
        }
        var App = (from b in pdb.CF_Ent_BaseInfo
                   join c in pdb.CF_Ent_QualiCerti
                   on b.FId equals c.FBaseInfoId
                   where c.FId == cId
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
        GetEnt(tag, string.Empty, fsysid, list, pdb);//其他单位
        isUnite(colName, 1);
        pdb.SubmitChanges();
        showInfo();
    }
    //是否联合体num为1时联合体为0时.
    RCenter rc = new RCenter();
    private void isUnite(string colName, int num)
    {
        pageTool tool = new pageTool(this.Page);
        string fId = EConvert.ToString(Session["FAppId"]);
        CF_Prj_Data Emp = db.CF_Prj_Data.Where(t => t.FAppId == fId).FirstOrDefault();
        if (Emp == null)
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }
        rc.PExcute("update CF_Prj_Data set " + colName + "=" + num + " where fid='" + Emp.FId + "'");
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
            string fid = e.CommandArgument.ToString();
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
            isUnite("FFloat10", 0);
        }
        pdb.SubmitChanges();
        showInfo();
    }
}

