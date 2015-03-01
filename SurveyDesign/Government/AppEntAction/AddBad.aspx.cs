using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;

using ProjectBLL;
using System.Data;
using System.Text;
using Approve.RuleCenter;
using Approve.EntityBase;
using System.Collections;
using Approve.Common;
public partial class BadBehavior_main_AddBad : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                showInfo();
                ShowBad();
            }
            btnAdd.OnClientClick = "showAddWindow('BadActionEdit.aspx?FAppId=" + ViewState["FID"] + "&sysId=" + Request.QueryString["sysId"] + "&e=0',800,780)";
        }
    }

    //显示
    private void showInfo()
    {
        Tools.pageTool tool = new Tools.pageTool(this.Page);
        CF_Prj_BadReport report = db.CF_Prj_BadReport.Where(t => t.FId == Convert.ToString(ViewState["FID"])).FirstOrDefault();
        if (report != null)
        {
            tool.fillPageControl(report);

        }
        else
        {
            t_FReportTime.Text = EConvert.ToShortDateString(DateTime.Now);
        }
    }

    //保存
    private void saveInfo(int state)
    {
        Tools.pageTool tool = new Tools.pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        string fId = EConvert.ToString(ViewState["FID"]);
        CF_Prj_BadReport report = new CF_Prj_BadReport();
        if (!string.IsNullOrEmpty(fId))
        {
            report = db.CF_Prj_BadReport.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            report.FId = fId;
            report.FIsDeleted = false;
            report.FCreateTime = dTime;

            db.CF_Prj_BadReport.InsertOnSubmit(report);
        }
        report.FState = state;
        report = tool.getPageValue(report);
        report.FDeptUser = EConvert.ToString(Session["DFUserId"]);
        report.FTime = dTime;
        db.SubmitChanges();
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }

    private void ShowBad()
    {

        StringBuilder sb = new StringBuilder();

        sb.Append(" select t2.fid,t2.fbaseinfoid,t1.fsystemid,t1.fname,(select top 1 fdesc from cf_sys_systemname where fnumber = t1.fsystemid) fsystemname,");
        sb.Append(" t1.flinkman,t1.ftel,t2.fdeptidname,t2.fdtime,");
        sb.Append(" (select top 1 fname from cf_sys_managedept where fnumber = t2.FDeptId) FDeptName,t2.FDeptId,");
        sb.Append(" (select count(*) from cf_ent_badaction t4 where t4.FBaseInfoId = t1.FId ) FCount, ");
        sb.Append(" (select top 1 fname from cf_sys_managedept where fnumber = t2.FAppDeptId) FAppDeptId, t2.fstate,t2.fresult,FBuidUnit,FProjectName ");
        sb.Append(" from cf_ent_baseinfo t1,cf_ent_badaction t2 ");
        sb.Append(" where t1.fid = t2.fbaseinfoid ");
        //sb.Append(" and t2.fresult=6 ");
        sb.Append(" and t1.fisdeleted=0 and t2.fisdeleted=0 ");
        sb.Append(" and t2.Fappid='" + ViewState["FID"] + "' ");
        sb.Append(" order by t2.FDeptId,t2.FDTime desc");

        BadAction_List.DataSource = rc.GetTable(sb.ToString());
        BadAction_List.DataBind();

    }

    //保存按钮
    protected void btnSave_Click(object sender, CommandEventArgs e)
    {
        saveInfo(EConvert.ToInt(e.CommandArgument));
        showInfo();
    }
    protected void BadAction_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string sId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string sBaseId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            string sSystemId = e.Item.Cells[e.Item.Cells.Count - 3].Text;
            string sState = e.Item.Cells[e.Item.Cells.Count - 4].Text;
            string sResult = e.Item.Cells[e.Item.Cells.Count - 5].Text;
            string sDeptId = e.Item.Cells[e.Item.Cells.Count - 6].Text;
        

            string sRoleId = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + sBaseId + "'");//登录角色

            CheckBox cb = (CheckBox)e.Item.Cells[0].Controls[1];
            Label lHistory = (Label)e.Item.Cells[e.Item.Cells.Count - 4].FindControl("lHistory");


            StringBuilder sb = new StringBuilder();
            sb.Append(" select FIsApp from CF_App_ActionRecord Where FDeptId='" + Session["DFId"].ToString() + "'");
            sb.Append(" and FLinkId='" + sId + "'");
            string sIsApp = rc.GetSignValue(sb.ToString());



            string sScript = "showApproveWindow('BadActionEdit.aspx?fid=" + sId + "',682,689)";
            e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\">" + e.Item.Cells[2].Text + "</a>";


            //if (sState == "1")
            //{
            //    e.Item.Cells[10].Text += "已发布";
            //}
            //else
            //{
            //    e.Item.Cells[10].Text += "未发布";
            //}



        }
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);

        int iCount = BadAction_List.Items.Count;

        StringBuilder sb = new StringBuilder();


        for (int i = 0; i < iCount; i++)
        {
            CheckBox cb = BadAction_List.Items[i].Cells[0].Controls[1] as CheckBox;

            if (cb.Checked == true)
            {

                string sDeptId = BadAction_List.Items[i].Cells[BadAction_List.Columns.Count - 6].Text;

                string sId = BadAction_List.Items[i].Cells[BadAction_List.Columns.Count - 1].Text;

                sb.Append(" delete  from CF_Ent_BadAction ");
                sb.Append(" where fid='" + sId + "'");

                sb.Append(" delete  from CF_Ent_BadActionCode ");
                sb.Append(" where FACtionId='" + sId + "'");

                sb.Append(" delete  from CF_App_ActionRecord ");
                sb.Append(" where FLinkId='" + sId + "'");
            }

        }

        if (sb.Length > 0)
        {
            if (rc.PExcute(sb.ToString()))
            {
                tool.showMessage("删除成功");
                ShowBad();
            }
            else
            {
                tool.showMessage("删除失败");
            }
        }
        else
        {
            tool.showMessage("请选择要删除的数据");
        }
    }

    protected void btnPub_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);

        int iCount = BadAction_List.Items.Count;

        StringBuilder sb = new StringBuilder();


        ArrayList arrEn = new ArrayList();
        ArrayList arrSl = new ArrayList();
        ArrayList arrSo = new ArrayList();
        ArrayList arrKey = new ArrayList();

        SortedList sl = new SortedList();

        for (int i = 0; i < iCount; i++)
        {
            CheckBox cb = BadAction_List.Items[i].Cells[0].Controls[1] as CheckBox;
            if (cb.Checked == true)
            {
                string sId = BadAction_List.Items[i].Cells[BadAction_List.Columns.Count - 1].Text;
                sb.Remove(0, sb.Length);
                sb.Append(" select FId from CF_App_ActionRecord ");
                sb.Append(" where FDeptId='" + Session["DFId"].ToString() + "'");
                sb.Append(" and FLinkId='" + sId + "'");
                string sAppId = rc.GetSignValue(sb.ToString());
                if (!string.IsNullOrEmpty(sAppId))
                {
                    sl = new SortedList();
                    sl.Add("FID", sAppId);
                    sl.Add("FResult", 1);
                    sl.Add("FIsApp", 1);

                    arrEn.Add(EntityTypeEnum.EaAppActionRecord);
                    arrSl.Add(sl);
                    arrSo.Add(SaveOptionEnum.Update);
                    arrKey.Add("FID");

                }
                else
                {
                    sl = new SortedList();
                    sl.Add("FID", Guid.NewGuid().ToString());
                    sl.Add("FLinkId", sId);
                    sl.Add("FResult", 1);
                    sl.Add("FDeptId", Session["DFId"].ToString());
                    sl.Add("FIsApp", 1);
                    sl.Add("FReportTime", DateTime.Now);
                    sl.Add("FIsDeleted", 0);
                    sl.Add("FCreateTime", DateTime.Now);

                    arrEn.Add(EntityTypeEnum.EaAppActionRecord);
                    arrSl.Add(sl);
                    arrSo.Add(SaveOptionEnum.Insert);
                    arrKey.Add("FID");
                }

                sl = new SortedList();
                sl.Add("FID", sId);
                sl.Add("FResult", 6);
                sl.Add("FState", 1);
                sl.Add("FAppDeptId", Session["DFId"].ToString());

                arrEn.Add(EntityTypeEnum.EbBadAction);
                arrSl.Add(sl);
                arrSo.Add(SaveOptionEnum.Update);
                arrKey.Add("FID");
            }
        }

        if (arrEn.Count == 0)
        {
            tool.showMessage("请选择要发布的数据");
            return;
        }


        EntityTypeEnum[] ens = new EntityTypeEnum[arrEn.Count];
        SortedList[] sls = new SortedList[arrEn.Count];
        SaveOptionEnum[] sos = new SaveOptionEnum[arrEn.Count];
        string[] keys = new string[arrEn.Count];
        for (int i = 0; i < arrEn.Count; i++)
        {

            ens[i] = (EntityTypeEnum)arrEn[i];
            sls[i] = new SortedList();
            sls[i] = (SortedList)arrSl[i];
            sos[i] = (SaveOptionEnum)arrSo[i];
            keys[i] = (string)arrKey[i];
        }

        if (rc.SaveEBaseM(ens, sls, keys, sos))
        {
            tool.showMessage("发布成功");
            ShowBad();

        }
        else
        {
            tool.showMessage("发布失败");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);

        int iCount = BadAction_List.Items.Count;

        StringBuilder sb = new StringBuilder();


        ArrayList arrEn = new ArrayList();
        ArrayList arrSl = new ArrayList();
        ArrayList arrSo = new ArrayList();
        ArrayList arrKey = new ArrayList();

        for (int i = 0; i < iCount; i++)
        {
            CheckBox cb = BadAction_List.Items[i].Cells[0].Controls[1] as CheckBox;
            if (cb.Checked == true)
            {
                string sId = BadAction_List.Items[i].Cells[BadAction_List.Columns.Count - 1].Text;
                sb.Remove(0, sb.Length);
                sb.Append(" select FId from CF_App_ActionRecord ");
                sb.Append(" where FDeptId='" + Session["DFId"].ToString() + "'");
                sb.Append(" and FLinkId='" + sId + "'");
                string sAppId = rc.GetSignValue(sb.ToString());
                if (!string.IsNullOrEmpty(sAppId))
                {
                    SortedList sl = new SortedList();
                    sl.Add("FID", sAppId);
                    sl.Add("FResult", 3);
                    sl.Add("FIsApp", 1);

                    arrEn.Add(EntityTypeEnum.EaAppActionRecord);
                    arrSl.Add(sl);
                    arrSo.Add(SaveOptionEnum.Update);
                    arrKey.Add("FID");

                    sl = new SortedList();
                    sl.Add("FID", sId);
                    sl.Add("FResult", 6);
                    sl.Add("FState", 0);
                    sl.Add("FAppDeptId", Session["DFId"].ToString());

                    arrEn.Add(EntityTypeEnum.EbBadAction);
                    arrSl.Add(sl);
                    arrSo.Add(SaveOptionEnum.Update);
                    arrKey.Add("FID");

                }
            }
        }

        if (arrEn.Count == 0)
        {
            tool.showMessage("请选择要撤销发布的数据");
            return;
        }


        EntityTypeEnum[] ens = new EntityTypeEnum[arrEn.Count];
        SortedList[] sls = new SortedList[arrEn.Count];
        SaveOptionEnum[] sos = new SaveOptionEnum[arrEn.Count];
        string[] keys = new string[arrEn.Count];
        for (int i = 0; i < arrEn.Count; i++)
        {

            ens[i] = (EntityTypeEnum)arrEn[i];
            sls[i] = new SortedList();
            sls[i] = (SortedList)arrSl[i];
            sos[i] = (SaveOptionEnum)arrSo[i];
            keys[i] = (string)arrKey[i];
        }

        if (rc.SaveEBaseM(ens, sls, keys, sos))
        {
            tool.showMessage("撤销成功");
            ShowBad();

        }
        else
        {
            tool.showMessage("撤销失败");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ShowBad();
    }
}
