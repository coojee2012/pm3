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
using System.Drawing;
using System.Linq;
using ProjectBLL;
using ProjectData;
using System.Collections.Generic;

public partial class OA_Talk_TalkList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    OA oa = new OA();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //当前用户
            if (!string.IsNullOrEmpty(CurrentEntUser.EntId))
                ViewState["FBaseinfoId"] = CurrentEntUser.EntId;
            else if (!string.IsNullOrEmpty(CurrentEmpUser.EmpId))
                ViewState["FBaseinfoId"] = CurrentEmpUser.EmpId;

            conBind();
            showInfo();
        }

        ((LinkButton)Page.FindControl("lb" + ViewState["FState"])).Attributes["class"] = "a_a_h";

    }
    private void conBind()
    {
        t_Fproject.DataSource = db.getDicList(9);
        t_Fproject.DataTextField = "FName";
        t_Fproject.DataValueField = "FNumber";
        t_Fproject.DataBind();
        t_Fproject.Items.Insert(0, new ListItem("全部", ""));



        Drop_order1.Items.Clear();
        Drop_order1.Items.Add(new ListItem("按提交时间--降序", "1"));
        Drop_order1.Items.Add(new ListItem("按提交时间--升序", "2"));

        Drop_order.Items.Clear();
        Drop_order.Items.Add(new ListItem("请选择", ""));
        Drop_order.Items.Add(new ListItem("零回复靠前--升序", "3"));
        Drop_order.Items.Add(new ListItem("按回复数--降序", "4"));


        //接收传来的查询条件
        //发起者
        if (!string.IsNullOrEmpty(Request.QueryString["FSubmitPerson"]))
        {
            t_createName.Text = db.CF_Ent_BaseInfo.Where(t => t.FId == Request.QueryString["FSubmitPerson"]).Select(t => t.FName).FirstOrDefault();
            if (string.IsNullOrEmpty(t_createName.Text))
                t_createName.Text = db.CF_Emp_BaseInfo.Where(t => t.FId == Request.QueryString["FSubmitPerson"]).Select(t => t.FName).FirstOrDefault();
        }
        //所属板块
        if (!string.IsNullOrEmpty(Request.QueryString["FProjectID"]))
        {
            t_Fproject.SelectedValue = Request.QueryString["FProjectID"];
        }
        //状态
        ViewState["FState"] = "2";//草稿：1，正在进行：2，已中止：3
        if (!string.IsNullOrEmpty(Request.QueryString["FState"]))
        {
            ViewState["FState"] = Request.QueryString["FState"];
        }
    }

    private void showInfo()
    {
        string FBaseinfoId = EConvert.ToString(ViewState["FBaseinfoId"]);
        pageTool tool = new pageTool(this.Page);

        int FState = EConvert.ToInt(ViewState["FState"]);
        var v = (from t in db.CF_Talk_TalkManage
                 where (t.FTalkState == 1 && t.FSubmitPerson == FBaseinfoId) || t.FTalkState == 2 || t.FTalkState == 3
                 select new
                 {
                     t.FID,
                     t.FProjectID,
                     t.FSubmitPerson,
                     t.FSubmitTime,
                     t.FTime,
                     t.FTalkName,
                     t.FTalkState,
                     t.FKey,
                     FCount = db.CF_Talk_Process.Count(p => p.FTalkID == t.FID),
                     UnLock = db.CF_Talk_Relation.Where(r => r.FTalkId == t.FID && r.FEmpId == FBaseinfoId && r.FKey == t.FKey).Count() > 0,
                     ent = db.CF_Ent_BaseInfo.Where(e => e.FId == t.FSubmitPerson).Select(e => e.FName).FirstOrDefault(),
                     emp = db.CF_Emp_BaseInfo.Where(e => e.FId == t.FSubmitPerson).Select(e => e.FName).FirstOrDefault(),
                 }).ToList();

        lb2.Text = "讨论进行中（" + v.Count(t => t.FTalkState == 2) + "）";
        lb3.Text = "已中止（" + v.Count(t => t.FTalkState == 3) + "）";
        lb1.Text = "草稿箱（" + v.Count(t => t.FTalkState == 1 && t.FSubmitPerson == FBaseinfoId) + "）";

        v = v.Where(t => t.FTalkState.GetValueOrDefault() == FState).ToList();

        #region 查询条件
        if (Drop_order.SelectedValue == "3")
        {
            v = v.OrderBy(t => t.FCount).ToList();
        }
        else if (Drop_order.SelectedValue == "4")
        {
            v = v.OrderByDescending(t => t.FCount).ToList();
        }

        if (Drop_order1.SelectedValue == "1")
        {
            v = v.OrderByDescending(t => t.FSubmitTime).ToList();
        }
        else if (Drop_order1.SelectedValue == "2")
        {
            v = v.OrderBy(t => t.FSubmitTime).ToList();
        }
        if (!string.IsNullOrEmpty(t_Fproject.SelectedValue))
        {
            v = v.Where(t => t.FProjectID == t_Fproject.SelectedValue).ToList();
        }

        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            v = v.Where(t => t.FTalkName.Contains(t_FName.Text)).ToList();
        }
        if (!string.IsNullOrEmpty(t_createName.Text))
        {
            List<string> BaseinfoIdList = db.CF_Ent_BaseInfo.Where(t => t.FName.Contains(t_createName.Text)).Select(t => t.FId).ToList();
            v = v.Where(t => BaseinfoIdList.Contains(t.FSubmitPerson)).ToList();
        }
        if (!string.IsNullOrEmpty(t_appTime1.Text))
        {
            DateTime DTime = EConvert.ToDateTime(t_appTime1.Text);
            v = v.Where(t => t.FSubmitTime >= DTime).ToList();
        }
        if (!string.IsNullOrEmpty(t_appTime2.Text))
        {
            DateTime DTime = EConvert.ToDateTime(t_appTime2.Text);
            v = v.Where(t => t.FSubmitTime <= DTime).ToList();
        }

        #endregion

        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页不显示 
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }



    protected void DG_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
            string FKey = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FKey"));//口令 
            string FState = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FTalkState"));//状态 
            string FCount = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FCount"));
            string FTalkName = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FTalkName")); ;//标题
            string FTime = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FTime"));//修改时间
            string FSubmitTime = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FSubmitTime"));//提交时间 
            string FSubmitPerson = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FSubmitPerson"));
            string FProjectID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FProjectID"));

            //当前用户
            string FBaseinfoId = EConvert.ToString(ViewState["FBaseinfoId"]);

            //是否需要口令
            string l = "";
            string url = "TalkDisp.aspx?FState=" + ViewState["FState"] + "&FID=" + FID + "";
            if (!string.IsNullOrEmpty(FKey))
            {
                if (FSubmitPerson == FBaseinfoId)
                {
                    l = "<img src=\"../../image/unlock.png\" style=\"width:20px;cursor:pointer;\" title=\"已上锁，但您是创建者无需输入口令\" />";
                }
                else
                {
                    bool UnLock = EConvert.ToBool(DataBinder.Eval(e.Row.DataItem, "UnLock"));
                    if (UnLock)
                    { //已解锁
                        l = "<img src=\"../../image/unlock.png\" style=\"width:20px;cursor:pointer;\" title=\"已解锁\" />";
                    }
                    else
                    { //未解锁
                        l = "<img src=\"../../image/lock.png\" style=\"width:20px;cursor:pointer;\" title=\"未解锁或口令已变更，请联系发起者索要口令\" />";
                        url = "javascript:unLock('" + FID + "')";
                    }
                }
            }
            //标题
            pageTool tool = new pageTool(this.Page);
            FTalkName = tool.staticStringbSubstring(FTalkName, 28, "...");
            string s = "<a href=\"" + url + "\" >" + FTalkName + "</a>（" + FCount + "）" + l;
            if (FSubmitPerson == FBaseinfoId)
            {
                s += "<a style=\"color:#333333\" href=\"javascript:showAddWindow('TalkEdit.aspx?FState=" + ViewState["FState"] + "&FID=" + FID + "',700,650);\">[编辑]</a>";

            }
            e.Row.Cells[1].Text = s;

            //板块类型
            e.Row.Cells[2].Text = db.getDicName(FProjectID);

            //提交人
            string userName = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "ent"));
            if (string.IsNullOrEmpty(userName))
                userName = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "emp"));
            e.Row.Cells[3].Text = userName;


            //时间
            DateTime Dcreatetime = Convert.ToDateTime(FTime);
            DateTime Dsubmittime = Convert.ToDateTime(FSubmitTime);
            System.TimeSpan t1 = DateTime.Now.Date - Dcreatetime.Date;
            System.TimeSpan t2 = DateTime.Now.Date - Dsubmittime.Date;
            FTime = oa.getDayName(Dcreatetime);
            FSubmitTime = oa.getDayName(Dsubmittime);
            if (FTime != FSubmitTime && FState != "1")
            {
                FSubmitTime = "<font color='red'>" + FSubmitTime + "</font>";
                e.Row.Cells[4].ToolTip = "于" + FTime.Replace("<br/>", "") + "修改过";
            }
            e.Row.Cells[4].Text = FSubmitTime;




            LinkButton Ok = (LinkButton)e.Row.FindControl("lbuIsOK");
            LinkButton Del = (LinkButton)e.Row.FindControl("lbuDel");

            if (FSubmitPerson == FBaseinfoId)
            {
                if (FState == "1")
                {
                    Ok.Text = "提交";
                    Ok.Attributes.Add("onclick", "return confirm('是否确认提交该讨论话题？')");
                }
                else if (FState == "2")
                {
                    Ok.Text = "中止该话题";
                    Ok.Attributes.Add("onclick", "return confirm('是否确认中止该话题的讨论？')");
                }
                else if (FState == "3")
                {
                    Ok.Text = "继续该话题";
                    Ok.Attributes.Add("onclick", "return confirm('是否确认继续该话题的讨论？')");
                }
                Del.Attributes.Add("onclick", "return confirm('确认要删除吗 ？')");
            }
            else
            {

                e.Row.Cells[7].Text = "<font color='#888888'>--</font>";
            }
        }
    }
    protected void DG_List_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        pageTool tool = new pageTool(this.Page);

        if (e.CommandName == "cnName")
        {
            string FID = e.CommandArgument.ToString();//问题ID
            Response.Redirect("TalkDisp.aspx?fid=" + FID + "&fstate=" + this.ViewState["FState"] + "&#ttt");
        }
        if (e.CommandName == "cnIsOK")//确认解决 或 提交
        {
            string[] str = e.CommandArgument.ToString().Split(',');
            string FID = str[0];
            string state = str[1];

            CF_Talk_TalkManage t = db.CF_Talk_TalkManage.Where(ta => ta.FID == FID).FirstOrDefault();
            if (t != null)
            {
                DateTime dTime = DateTime.Now;
                if (state == "1" || state == "3")
                {
                    t.FTalkState = 2;
                    t.FSubmitTime = dTime;
                    t.FTime = dTime;
                }
                else if (state == "2")
                {
                    t.FTalkState = 3;
                }

                db.SubmitChanges();
                tool.showMessageAndRunFunction("操作成功", "window.returnValue=1;");
                showInfo();
            }
        }
        if (e.CommandName == "cnDel")
        {
            string FID = e.CommandArgument.ToString();//问题ID
            StringBuilder sb = new StringBuilder();
            sb.Append(" delete from CF_talk_talkManage where fid='" + FID + "' ");//问题 
            sb.Append(" delete from CF_talk_Process where ftalkid='" + FID + "'");//处理过程
            if (oa.PExcute(sb.ToString()))
            {
                tool.showMessage("删除成功");
            }
        }
        showInfo();
    }

    //状态查询
    protected void lb_Click(object sender, EventArgs e)
    {
        string FState = ((LinkButton)sender).CommandArgument.ToString();
        ViewState["FState"] = FState;
        showInfo();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
