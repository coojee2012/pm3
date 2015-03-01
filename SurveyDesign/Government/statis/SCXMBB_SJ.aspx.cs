using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using ProjectBLL;

public partial class Government_statis_SCXMBB_SJ : System.Web.UI.Page
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

    //绑定 
    private void conBind()
    {
        //工程类别
        t_FType.DataSource = db.getDicList("20001");
        t_FType.DataTextField = "FName";
        t_FType.DataValueField = "FNumber";
        t_FType.DataBind();
        t_FType.Items.Insert(0, new ListItem("全部", ""));

        //设计规模
        t_FScale.DataSource = db.getDicList("20004");
        t_FScale.DataTextField = "FName";
        t_FScale.DataValueField = "FNumber";
        t_FScale.DataBind();
        t_FScale.Items.Insert(0, new ListItem("全部", ""));

        //建设性质
        t_FKind.DataSource = db.getDicList("20005");
        t_FKind.DataTextField = "FName";
        t_FKind.DataValueField = "FNumber";
        t_FKind.DataBind();
        t_FKind.Items.Insert(0, new ListItem("全部", ""));

        //按年
        for (int i = DateTime.Now.Year; i >= 2010; i--)
            dr_Year.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));

        //工程所属区域
        govd_FRegistDeptId.fNumber = EConvert.ToString(Session["DFID"]);
        govd_FRegistDeptId.Dis(EConvert.ToInt(Session["DFLevel"]));
    }



    //显示
    private void showInfo()
    {

        //参与项目（查询技术性审查已经通过的。）
        var v = (from t in db.CF_App_List.Where(a => (a.FManageTypeId == 30103) && a.FState == 6)
                 group t by t.FPrjId into g
                 select new
                 {
                     FID = g.Key,
                     //工程信息
                     p = (from p in db.CF_Prj_BaseInfo
                          where p.FId == g.Key
                          select new
                          {
                              JSDW = db.CF_Ent_BaseInfo.Where(b => b.FId == p.FBaseinfoId).Select(b => b.FName).FirstOrDefault(),
                              p.FPrjName,
                              p.FAddressDept,//地区
                              p.FType,//工程类别
                              p.FScale,//设计规模
                              p.FKind,//建设性质
                              p.FArea,//总建筑面积
                              p.FLayers,//总层数
                              p.FGround,//地上
                              p.FUnderground,//地下
                              p.FHeight,//建筑高度
                              p.FAllMoney,//投资总额（万元）
                              p.FStruType,//结构类型                             
                          }).FirstOrDefault(),
                     //技术性审查
                     a = (from a in g
                          where a.FCreateTime == g.Max(g3 => g3.FCreateTime)
                          select new
                          {
                              a.FId,
                              a.FLinkId,
                              a.FBaseName,
                              a.FAppDate,
                              a.FIsSign,
                              //勘察单位、勘察收费（从最后一次勘察合同备案业务取，记注勘察合同备案业务是通过CF_App_List.FLinkId（即CF_Prj_Data.FID）关联副表）
                              //勘察合同备案280 情况
                              kc = (from aa in db.CF_App_List
                                    join b in db.CF_Prj_Ent on aa.FLinkId equals b.FAppId
                                    where aa.FPrjId == a.FPrjId && aa.FManageTypeId == 280 && aa.FState == 6
                                          && b.FEntType == 15501
                                    orderby aa.FCreateTime descending
                                    select new
                                    {
                                        aa.FBaseName,//建设单位
                                        b.FName,
                                        b.FMoney
                                    }).FirstOrDefault(),
                              //设计单位、设计收费（从最后一次施工图设计文件编制合同备案业务取）
                              sj = (from aa in db.CF_App_List
                                    join b in db.CF_Prj_Ent on aa.FId equals b.FAppId
                                    where aa.FPrjId == a.FPrjId && aa.FManageTypeId == 296 && aa.FState == 6
                                          && b.FEntType == 155
                                    orderby aa.FCreateTime descending
                                    select new
                                    {
                                        b.FName,
                                        b.FMoney
                                    }).FirstOrDefault(),
                              //审查合同备案业300
                              sc = (from aa in db.CF_App_List
                                    join b in db.CF_Prj_Ent on aa.FId equals b.FAppId
                                    where aa.FLinkId == a.FLinkId && aa.FManageTypeId == 300 && aa.FState == 6
                                         && b.FEntType == 145
                                    select new
                                    {
                                        aa.FId,
                                        aa.FLinkId,
                                        aa.FReportDate,
                                        aa.FAppDate,//审查受理日期
                                        b.FMoney,//审查收费
                                        aa.FReportCount,//是几审
                                    }).FirstOrDefault(),
                              //备案情况305
                              bak = (from aa in db.CF_App_List
                                     join id in db.CF_App_Idea.Where(t => t.FUserId == null || t.FType == 305)
                                        on new { FID = aa.FId, FReportCount = aa.FReportCount } equals new { FID = id.FLinkId, FReportCount = id.FReportCount } into idea
                                     from i in idea.DefaultIfEmpty()
                                     orderby aa.FCreateTime descending
                                     where aa.FLinkId == a.FLinkId && aa.FManageTypeId == 305
                                     select new
                                     {
                                         iDeaFID = i == null ? "" : i.FId,
                                         FResult = i == null ? "" : i.FResult,
                                         FResultInt = i == null ? 0 : i.FResultInt,
                                         FAppTime = i == null ? DateTime.Now : i.FAppTime,
                                     }).FirstOrDefault()
                          }).FirstOrDefault(),
                 }).ToList();//排除相同


        #region 查询条件

        //工程类别
        if (!string.IsNullOrEmpty(t_FType.SelectedValue))
        {
            v = v.Where(t => t.p.FType.ToString() == t_FType.SelectedValue).ToList();
        }
        //设计规模
        if (!string.IsNullOrEmpty(t_FScale.SelectedValue))
        {
            v = v.Where(t => t.p.FScale.ToString() == t_FScale.SelectedValue).ToList();
        }
        //建设性质
        if (!string.IsNullOrEmpty(t_FKind.SelectedValue))
        {
            v = v.Where(t => t.p.FKind.ToString() == t_FKind.SelectedValue).ToList();
        }
        // 投资总额(万元)
        if (!string.IsNullOrEmpty(t_FAllMoney1.Text))
        {
            v = v.Where(t => t.p.FAllMoney >= EConvert.ToFloat(t_FAllMoney1.Text)).ToList();
        }
        if (!string.IsNullOrEmpty(t_FAllMoney2.Text))
        {
            v = v.Where(t => t.p.FAllMoney <= EConvert.ToFloat(t_FAllMoney2.Text)).ToList();
        }
        //一审通过
        if (!string.IsNullOrEmpty(t_FirstApp.SelectedValue))
        {
            if (t_FirstApp.SelectedValue == "1")//是
                v = v.Where(t => t.a.sc.FReportCount == 1).ToList();
            else if (t_FirstApp.SelectedValue == "2")//否
                v = v.Where(t => t.a.sc.FReportCount > 1).ToList();
        }
        //合格证
        if (!string.IsNullOrEmpty(t_Pint.SelectedValue))
        {
            if (t_Pint.SelectedValue == "1")//已打印
                v = v.Where(t => t.a.FIsSign == 1).ToList();
            else if (t_Pint.SelectedValue == "2")//未打印
                v = v.Where(t => t.a.FIsSign != 1).ToList();
        }
        //备案情况
        if (!string.IsNullOrEmpty(t_Bak.SelectedValue))
        {
            if (t_Bak.SelectedValue == "0")//未办理
                v = v.Where(t => t.a.bak == null).ToList();
            else if (t_Bak.SelectedValue == "1")//正在备案
                v = v.Where(t => t.a.bak.iDeaFID == "").ToList();
            else if (t_Bak.SelectedValue == "2")//备案合格
                v = v.Where(t => t.a.bak.FResultInt == 1).ToList();
            else if (t_Bak.SelectedValue == "3")//备案不合格
                v = v.Where(t => t.a.bak.FResultInt == 3).ToList();
            else if (t_Bak.SelectedValue == "4")//打回
                v = v.Where(t => t.a.bak.FResultInt == 2).ToList();
        }
        //设计单位
        if (!string.IsNullOrEmpty(t_sjBaseName.Text))
        {
            v = v.Where(t => t.a.sj.FName.Contains(t_sjBaseName.Text)).ToList();
        }
        //勘察单位
        if (!string.IsNullOrEmpty(t_kcBaseName.Text))
        {
            v = v.Where(t => t.a.kc.FName.Contains(t_kcBaseName.Text)).ToList();
        }
        //审图机构
        if (!string.IsNullOrEmpty(t_stBaseName.Text))
        {
            v = v.Where(t => t.a.FBaseName.Contains(t_stBaseName.Text)).ToList();
        }
        //工程所属区域
        if (!string.IsNullOrEmpty(govd_FRegistDeptId.FNumber))
        {
            v = v.Where(t => t.p.FAddressDept.Contains(govd_FRegistDeptId.FNumber)).ToList();
        }

        //统计条件
        if (drop_CountWay.SelectedValue == "year") //按年
        {
            if (!string.IsNullOrEmpty(dr_Year.SelectedValue))
                v = v.Where(t => t.a.FAppDate.GetValueOrDefault().Year.ToString() == dr_Year.SelectedValue).ToList();
        }
        else if (drop_CountWay.SelectedValue == "quarter") //按季度
        {
            if (!string.IsNullOrEmpty(dr_Year.SelectedValue))
                v = v.Where(t => t.a.FAppDate.GetValueOrDefault().Year.ToString() == dr_Year.SelectedValue).ToList();
            switch (dr_Quarter.SelectedValue)
            {
                case "1":
                    v = v.Where(t => "1,2,3".Split(',').Contains(t.a.FAppDate.GetValueOrDefault().Month.ToString())).ToList();
                    break;
                case "2":
                    v = v.Where(t => "4,5,6".Split(',').Contains(t.a.FAppDate.GetValueOrDefault().Month.ToString())).ToList();
                    break;
                case "3":
                    v = v.Where(t => "7,8,9".Split(',').Contains(t.a.FAppDate.GetValueOrDefault().Month.ToString())).ToList();
                    break;
                case "4":
                    v = v.Where(t => "10,11,12".Split(',').Contains(t.a.FAppDate.GetValueOrDefault().Month.ToString())).ToList();
                    break;
            }
        }
        else if (drop_CountWay.SelectedValue == "month") //按月
        {
            if (!string.IsNullOrEmpty(dr_Year.SelectedValue))
                v = v.Where(t => t.a.FAppDate.GetValueOrDefault().Year.ToString() == dr_Year.SelectedValue).ToList();
            if (!string.IsNullOrEmpty(dr_Month.SelectedValue))
                v = v.Where(t => t.a.FAppDate.GetValueOrDefault().ToString("yyyyM") == (dr_Year.SelectedValue + dr_Month.SelectedValue)).ToList();
        }
        else if (drop_CountWay.SelectedValue == "auto") //自定义
        {
            if (!string.IsNullOrEmpty(t_Date1.Text))
                v = v.Where(t => t.a.FAppDate >= EConvert.ToDateTime(t_Date1.Text)).ToList();
            if (!string.IsNullOrEmpty(t_Date2.Text))
                v = v.Where(t => t.a.FAppDate <= EConvert.ToDateTime(t_Date2.Text)).ToList();

        }

        #endregion

        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);


        //统计
        #region 统计


        //复制一个作统计数据源
        var c = v.ToList();


        //项目总数
        la_all_Count.Text = c.Count().ToString();
        //投资总额
        la_all_Money.Text = c.Sum(t => t.p.FAllMoney).ToString();
        //一次通过审查项目
        la_all_First.Text = c.Where(t => t.a.sc.FReportCount == 1).Count().ToString();


        //房屋建筑工程
        la_fw_Count.Text = c.Where(t => t.p.FType == 2000101).Count().ToString();
        //投资总额
        la_fw_Money.Text = c.Where(t => t.p.FType == 2000101).Sum(t => t.p.FAllMoney).ToString();
        //一次通过审查项目
        la_fw_First.Text = c.Where(t => t.p.FType == 2000101 && t.a.sc.FReportCount == 1).Count().ToString();
        //总建筑面积
        la_fw_Area.Text = c.Where(t => t.p.FType == 2000101).Sum(t => t.p.FArea).ToString();
        //总层数
        la_fw_Layers.Text = c.Where(t => t.p.FType == 2000101).Sum(t => t.p.FLayers).ToString();
        //地上
        la_fw_Ground.Text = c.Where(t => t.p.FType == 2000101).Sum(t => t.p.FGround).ToString();
        //地下
        la_fw_Underground.Text = c.Where(t => t.p.FType == 2000101).Sum(t => t.p.FUnderground).ToString();
        //总建筑高度
        la_fw_Height.Text = c.Where(t => t.p.FType == 2000101).Sum(t => t.p.FHeight).ToString();



        //市政基础工程
        la_sz_Count.Text = c.Where(t => t.p.FType == 2000102).Count().ToString();
        //投资总额
        la_sz_Money.Text = c.Where(t => t.p.FType == 2000102).Sum(t => t.p.FAllMoney).ToString();
        //一次通过审查项目
        la_sz_First.Text = c.Where(t => t.p.FType == 2000102 && t.a.sc.FReportCount == 1).Count().ToString();

        #endregion

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
            string FType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "p.FType"));

            //地区
            e.Item.Cells[2].Text = db.getDeptName(DataBinder.Eval(e.Item.DataItem, "p.FAddressDept"));
            //工程类别
            e.Item.Cells[3].Text = db.getDicName(FType);
            //设计规模
            e.Item.Cells[4].Text = db.getDicName(DataBinder.Eval(e.Item.DataItem, "p.FScale"));
            //建设性质
            e.Item.Cells[5].Text = db.getDicName(DataBinder.Eval(e.Item.DataItem, "p.FKind"));

            if (FType == "2000101")//房屋建筑工程
            {
                //总建筑面积
                e.Item.Cells[6].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "p.FArea"));
                //地上
                e.Item.Cells[7].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "p.FGround"));
                //地下
                e.Item.Cells[8].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "p.FUnderground"));
                //建筑高度
                e.Item.Cells[9].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "p.FHeight"));
            }
            else
            {
                //总建筑面积
                e.Item.Cells[6].Text = "/";
                //地上
                e.Item.Cells[7].Text = "/";
                //地下
                e.Item.Cells[8].Text = "/";
                //建筑高度
                e.Item.Cells[9].Text = "/";
            }

            //投资总额
            e.Item.Cells[10].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "p.FAllMoney"));
            //结构类型
            e.Item.Cells[11].Text = db.getDicName(DataBinder.Eval(e.Item.DataItem, "p.FStruType"));

            //建设单位
            e.Item.Cells[12].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "a.kc.FBaseName"));

            //勘察单位
            e.Item.Cells[13].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "a.kc.FName"));

            //设计单位
            e.Item.Cells[14].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "a.sj.FName"));

            //审查机构
            e.Item.Cells[15].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "a.FBaseName"));


            //查出最后一次办勘察审查合同备案业务的主表CF_Prj_Data
            var FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "a.FLinkId"));

            //审查人员、（从最后一次人员安排取人员              
            var sc = (from b in db.CF_Prj_Emp
                      join a in db.CF_App_List on b.FAppId equals a.FId
                      where a.FManageTypeId == 30102 && a.FState == 6 && a.FLinkId == FLinkId
                      orderby b.FCreateTime descending
                      select new
                      {
                          b.FName,
                          b.FType,
                          //违反强条数量
                          FOrder = (from aa in db.CF_App_List//（从最后一次技术性审查取）
                                    join ii in db.CF_App_Idea on aa.FId equals ii.FLinkId
                                    where aa.FPrjId == FID && aa.FManageTypeId == 30103 && aa.FState > 0
                                          && ii.FUserId == b.FEmpBaseInfo && aa.FLinkId == FLinkId
                                    select ii.FOrder).FirstOrDefault()
                      }).ToList();
            //审查人员
            e.Item.Cells[16].Text = "<font color='#888888'>负责人：</font>" + sc.Where(t => t.FType == 1).Select(t => t.FName).FirstOrDefault();
            e.Item.Cells[16].Text += " <font color='#888888'>审查人：</font>" + string.Join(",", sc.Where(t => t.FType != 1).Select(t => t.FName).ToArray());

            //违反强条数量
            e.Item.Cells[17].Text = sc.Where(t => t.FType != 1).Sum(t => t.FOrder).ToString();

            //工程勘察收费(万元)
            e.Item.Cells[18].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "a.kc.FMoney"));

            //设计收费(万元)
            e.Item.Cells[19].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "a.sj.FMoney"));

            //审查收费(万元)
            e.Item.Cells[20].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "a.sc.FMoney"));

            //审查受理日期
            e.Item.Cells[21].Text = EConvert.ToShortDateString(DataBinder.Eval(e.Item.DataItem, "a.sc.FAppDate"));

            //审查通过日期
            e.Item.Cells[22].Text = EConvert.ToShortDateString(DataBinder.Eval(e.Item.DataItem, "a.FAppDate"));


            //审查历时(天)
            e.Item.Cells[23].Text = ((EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "a.FAppDate")) - EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "a.sc.FReportDate"))).Days + 1).ToString();

            //是否一审通过
            e.Item.Cells[24].Text = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "a.sc.FReportCount")) > 1 ? "<tt>否</tt>" : "<font color='green'>是</font>";

            //合格证
            e.Item.Cells[25].Text = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "a.FIsSign")) == 1 ? "<font color='green'>已打印</font>" : "<tt>未打印</tt>";

            //备案情况
            string s = "";
            object bak = DataBinder.Eval(e.Item.DataItem, "a.bak");
            if (bak != null)
            {
                string iDeaFID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "a.bak.iDeaFID"));
                if (!string.IsNullOrEmpty(iDeaFID))
                {
                    string FResultInt = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "a.bak.FResultInt"));
                    if (FResultInt == "1" || FResultInt == "6")
                    {
                        s = "<font color='green'>备案通过</font>";
                    }
                    else if (FResultInt == "3")
                    {
                        s = "<font color='red'>备案不通过</font>";
                    }
                    else if (FResultInt == "2")
                    {
                        s = "<font color='red'>打回</font>";
                    }
                }
                else
                    s = "<font color='#888888'>正在备案</font>";
            }
            else
            {
                s = "<font color='#888888'>未办理</font>";
            }
            //备案情况
            e.Item.Cells[26].Text = s;
        }
    }

    //查询按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }

    //导出按钮
    protected void btnOut_Click(object sender, EventArgs e)
    {
        string title = lit_Title.Text;
        SaveAsBase sab = new ProjectBLL.SaveAsBase();
        sab.SaveAsExc(DG_List, title, Response);
    }

    //统计按钮 
    protected void btnCount_Click(object sender, EventArgs e)
    {
        showInfo();
    }

}
