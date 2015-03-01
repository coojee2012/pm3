using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleApp;
using ProjectData;
using System.Linq;

public partial class Government_AppMain_SerialNumber : govBasePage
{
    RCenter rc = new RCenter();
    RQuali rq = new RQuali();
    SaveAsBase sab = new SaveAsBase();
    RAppBacth rap = new RAppBacth();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);




            ControlBind();
            ShowInfo();

            if (Request["fcol"] != null && Request["fcol"] != "")
            {
                lPostion.Text = rc.GetMenuName(Request["fcol"]);
            }

        }
    }

    private void ShowPostion()
    {
        this.lPostion.Text = rc.GetMenuName(Request["fcol"]);
    }

    private void ControlBind()
    {

    }


    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();

        return sb.ToString();
    }

    private void ShowInfo()
    {
        //28801
        int FManageTypeId = EConvert.ToInt(Request.QueryString["FManageTypeId"]);
        var v = from t in db.CF_App_List
                join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                where t.FManageTypeId == FManageTypeId && (t.FState == 6)
                orderby t.FReportDate descending
                select new
                {
                    t.FId,
                    t.FPrjId,
                    FPrjName = db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FPrjName).FirstOrDefault(),
                    FAddress = db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FAllAddress).FirstOrDefault(),
                    FJsEnt = "",
                    t.FName,
                    t.FCreateTime,
                    t.FState,
                    t.FManageTypeId,
                    t.FReportDate,
                    t.FUpDeptId,
                    t.FYear,
                    t.FBaseName,
                    t.FAppDate,
                    t.FBaseinfoId,
                    DataId = d.FId,
                    d.FTxt8, // 管理部门分配的业务流水号
                    isOver = (from a in db.CF_App_List
                              where a.FLinkId == t.FLinkId && (a.FManageTypeId == 30103 || a.FManageTypeId == 28803) && a.FState > 1
                              select a).Any()
                };
        if (!string.IsNullOrEmpty(txtFPrjName.Text))
        {
            v = v.Where(t => t.FPrjName.Contains(txtFPrjName.Text));
        }
        if (ddlIsCreate.SelectedValue != "")
        {
            if (ddlIsCreate.SelectedValue == "0")
            {
                v = v.Where(t => t.FTxt8 == null || t.FTxt8 == "");
            }
        }
        if (ddlState.SelectedValue != "")
        {
            if (ddlState.SelectedValue == "0")
            {
                v = v.Where(t => !t.isOver);
            }
            else if (ddlState.SelectedValue == "1")
            {
                v = v.Where(t => t.isOver);
            }
        }

        Pager1.RecordCount = v.Count();
        DG_List.DataKeyField = "DataId";
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页不显示
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            string FId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            string FDataID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "DataId"));
            string FManageTypeId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageTypeId"));
            var Ent = db.CF_Prj_Ent.Where(l => l.FPrjId == FPrjId && l.FEntType == 100).FirstOrDefault();
            if (Ent != null)
            {
                string sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=100 ");

                string sScript = "javascript:showAddWindow('" + sUrl + "?sysid=100&fbid=" + Ent.FBaseInfoId + "&fly=1 ',800,580)";

                e.Item.Cells[4].Text = "<a href=\"" + sScript + "\" >" + Ent.FName + "</a>";
            }
            string sUrl1 = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=145 ");

            string sScript1 = "javascript:showAddWindow('" + sUrl1 + "?sysid=145&fbid=" + EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBaseInfoId")) + "&fly=1 ',800,580)";

            e.Item.Cells[5].Text = "<a href=\"" + sScript1 + "\" >" + e.Item.Cells[5].Text + "</a>";

            sUrl1 = rc.getMTypeQurl(FManageTypeId);
            //sUrl += "?fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1";;

            sScript1 = "javascript:showApproveWindow('" + sUrl1 + "?FDataID=" + FDataID + "',830,500)";


            e.Item.Cells[2].Text = "<a class='link7' href=\"" + sScript1 + "\" >" + e.Item.Cells[2].Text + "</a>";


            bool isOver = EConvert.ToBool(DataBinder.Eval(e.Item.DataItem, "isOver"));
            e.Item.Cells[8].Text = isOver ? "<font color='green'>已审查完</font>" : "<tt>正在审查</tt>";

            //查询该项目是否变更 
            if (!string.IsNullOrEmpty(FPrjId))
            {
                var prjBG = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId)
                    .Select(t => new { t.FBGTime, t.FCount })
                    .FirstOrDefault();
                if (prjBG != null && prjBG.FCount > 0)
                {
                    e.Item.Cells[2].Text += "<br/><font color='#000000;'>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")</font>";
                }
            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    public void DisableControls(DataGrid gv)
    {
        for (int i = 0; i < gv.Items.Count; i++)
        {
            TextBox txt = gv.Items[i].FindControl("txtFTxt8") as TextBox;
            ((TableCell)txt.Parent).Text = txt.Text;
            Label lb = new Label();

        }
    }
    protected void btnOut_Click(object sender, EventArgs e)
    {

        Pager1.CurrentPageIndex = 1;
        Pager1.PageSize = Pager1.RecordCount;
        DG_List.Columns[0].Visible = false;
        string fOutTitle = lPostion.Text;
        Approve.Common.SaveAsBase sab = new Approve.Common.SaveAsBase();
        ShowInfo();
        DisableControls(DG_List);
        sab.SaveAsExc(this.DG_List, fOutTitle, this.Response, "gb2312", 0);
    }


    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowInfo();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this);
        for (int i = 0; i < DG_List.Items.Count; i++)
        {
            TextBox txtFTxt8 = DG_List.Items[i].FindControl("txtFTxt8") as TextBox;
            if (txtFTxt8 != null)
            {
                string FID = DG_List.Items[i].Cells[DG_List.Columns.Count - 1].Text;

                string DataId = EConvert.ToString(DG_List.DataKeys[i]);
                CF_Prj_Data data = (from t in db.CF_App_List
                                    join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                                    where t.FManageTypeId == 28801 && d.FId == DataId
                                    select d).FirstOrDefault();
                if (data != null)
                {
                    //判断这个号是否重复
                    int iCount = (from t in db.CF_App_List
                                  join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                                  where t.FManageTypeId == 28801 && (t.FState == 6 || t.FState == 3) && d.FId != DataId && d.FTxt8 == txtFTxt8.Text
                                  select d).Count();
                    if (iCount > 0)
                    {
                        string FPrjName = (from p in db.CF_Prj_BaseInfo
                                           join t in db.CF_App_List on p.FId equals t.FPrjId
                                           where t.FId == FID
                                           select p.FPrjName).FirstOrDefault();

                        tool.showMessage(FPrjName + ",流水号重复");
                        break;
                    }
                    data.FTxt8 = txtFTxt8.Text;
                }
            }
        }
        db.SubmitChanges();
        tool.showMessage("保存成功");
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        //int max = (from t in db.CF_App_List
        //           join d in db.CF_Prj_Data on t.FId equals d.FAppId
        //           where t.FManageTypeId == 28801 && (d.FTxt8 != null && d.FTxt8 != "")
        //           select d.FTxt8).ToList().Max(t => EConvert.ToInt(t));
        for (int i = DG_List.Items.Count - 1; i >= 0; i--)
        {
            TextBox txtFTxt8 = DG_List.Items[i].FindControl("txtFTxt8") as TextBox;
            if (txtFTxt8 != null && string.IsNullOrEmpty(txtFTxt8.Text))
            {
                // max++;
                //txtFTxt8.Text = (++max).ToString().PadLeft(5, '0');
                RApp ra = new RApp();
                txtFTxt8.Text = ra.GetAutoNO("") + i;
            }
        }
    }
}
