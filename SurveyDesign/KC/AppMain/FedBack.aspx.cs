using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using ProjectBLL;

public partial class KC_AppMain_FedBack : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }

    //显示
    private void showInfo()
    {
        string FBaseinfoId = CurrentEntUser.EntId;
        //参与项目
        var v = (from p in db.CF_Prj_BaseInfo
                 join a in db.CF_App_List on p.FId equals a.FPrjId
                 where (!string.IsNullOrEmpty(t_FPrjName.Text) ? p.FPrjName.Contains(t_FPrjName.Text) : true)
                    && a.FToBaseinfoId == FBaseinfoId && (a.FManageTypeId == 280) && a.FState == 6
                 orderby a.FCreateTime descending
                 select new
                 {
                     p.FId,
                     p.FPrjName,
                     //工程类别
                     p.FType,
                     a.FBaseName,//建设单位
                 }).Distinct().ToList();//排除相同

        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
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
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FType"));

            //工程类别
            e.Item.Cells[2].Text = db.getDicName(FType);
            

            //查询业务办理情况
            string s = "";
            var v = (from a in db.CF_App_List
                     join id in db.CF_App_Idea.Where(t => t.FUserId == null) on a.FId equals id.FLinkId into idea
                     from i in idea.DefaultIfEmpty()
                     where a.FPrjId == FID && a.FState > 0
                        && (a.FManageTypeId == 287 || a.FManageTypeId == 28801 || a.FManageTypeId == 28803 || a.FManageTypeId == 290
                         || a.FManageTypeId == 300 || a.FManageTypeId == 30101 || a.FManageTypeId == 30103 || a.FManageTypeId == 305)
                     select new
                     {
                         a.FId,
                         a.FPrjId,
                         a.FLinkId,
                         a.FState,
                         a.FManageTypeId,
                         a.FAppDate,
                         a.FReportDate,
                         a.FCreateTime,
                         iDeaFID = i == null ? "" : i.FId,
                         FResult = i == null ? "" : i.FResult,
                         FResultInt = i == null ? 0 : i.FResultInt,
                         FAppTime = i == null ? DateTime.Now : i.FAppTime,
                         //技术性审查时，查人员是否有“补正材料”的意见
                     }).ToList();

            if (v != null)
            {
                // 勘察 审查
                var vk = v.Where(a => a.FManageTypeId == 287 || a.FManageTypeId == 28801 || a.FManageTypeId == 28803 || a.FManageTypeId == 290).OrderByDescending(t => t.FCreateTime).FirstOrDefault();
                if (vk != null)
                {
                    s = getStateName(vk.FManageTypeId, vk.FState, vk.iDeaFID, vk.FResultInt, vk.FResult, vk.FAppTime.GetValueOrDefault(), vk.FReportDate.GetValueOrDefault());
                }
                else
                {
                    s = "<font color='#888888'>还未办理</font>";
                }
                e.Item.Cells[4].Text = s;

                //设计 审查
                var vs = v.Where(a => a.FManageTypeId == 300 || a.FManageTypeId == 30101 || a.FManageTypeId == 30103 || a.FManageTypeId == 305).OrderByDescending(t => t.FCreateTime).FirstOrDefault();
                if (vs != null)
                {
                    s = getStateName(vs.FManageTypeId, vs.FState, vs.iDeaFID, vs.FResultInt, vs.FResult, vs.FAppTime.GetValueOrDefault(), vs.FReportDate.GetValueOrDefault());
                }
                else
                {
                    s = "<font color='#888888'>还未办理</font>";
                }
                e.Item.Cells[5].Text = s;
            }
        }
    }

    private string getStateName(int? FManageTypeId, int? FState, string iDeaFID, int? FResultInt, string FResult, DateTime FAppTime, DateTime FReportDate)
    {
        string s = "";
        switch (FManageTypeId)
        {
            case 287:
            case 300:
                s += "合同备案受理：";
                switch (FState)
                {
                    case 0://未上报
                        s += "<font color='#888888'>未上报</font>";
                        break;
                    case 1://已上报
                        s += "<font color='#888888'>还未受理</font>";
                        break;
                    case 2://被退回
                        s += "<font color='red'>退回，补充材料</font> <font color='#666666'>[" + FAppTime.ToString("yyyy-MM-dd") + "]</font>";
                        break;
                    case 6://已办结
                        s += "<font color='green'>已受理</font> <font color='#666666'>[" + FAppTime.ToString("yyyy-MM-dd") + "]</font>";
                        break;
                    case 7://不予受理
                        s += "<font color='red'>不予受理</font> <font color='#666666'>[" + FAppTime.ToString("yyyy-MM-dd") + "]</font>";
                        break;
                }
                break;
            case 28801:
            case 30101:
                s += "程序性审查：";
                if (!string.IsNullOrEmpty(iDeaFID))
                {
                    if (FResultInt == 1)//合格
                    {
                        s += "<font color='green'>" + FResult + "</font>";
                    }
                    else//不合格
                    {
                        s += "<font color='red'>" + FResult + "</font>";
                    }
                    s += " <font color='#666666'>[" + FAppTime.ToString("yyyy-MM-dd") + "]</font>";
                }
                else
                    s += "<font color='blue'>正在办理</font> <font color='#666666'>[" + FReportDate.ToString("yyyy-MM-dd") + "]</font>";
                break;
            case 28803:
            case 30103:
                s += "技术性审查：";
                if (!string.IsNullOrEmpty(iDeaFID))
                {
                    if (FResultInt == 6)//合格
                    {
                        s += "<font color='green'>" + FResult + "</font>";
                    }
                    else//不合格
                    {
                        s += "<font color='red'>" + FResult + "</font>";
                    }
                    s += " <font color='#666666'>[" + FAppTime.ToString("yyyy-MM-dd") + "]</font>";
                }
                else
                    s += "<font color='blue'>正在办理</font> <font color='#666666'>[" + FReportDate.ToString("yyyy-MM-dd") + "]</font>";
                break;
            case 290:
            case 305:
                s += FManageTypeId == 290 ? "勘察文件审查备案：" : "施工图设计文件备案：";
                if (FState == 6)
                {
                    s += "<font color='green'>同意备案</font>";
                    s += " <font color='#666666'>[" + FAppTime.ToString("yyyy-MM-dd") + "]</font>";
                }
                else if (FState == 2)
                {
                    s += "<font color='red'>已打回</font>";
                    s += " <font color='#666666'>[" + FAppTime.ToString("yyyy-MM-dd") + "]</font>";
                }
                else
                    s += "<font color='blue'>正在办理</font> <font color='#666666'>[" + FReportDate.ToString("yyyy-MM-dd") + "]</font>";
                break;
        }
        return s;
    }


    //查询按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
