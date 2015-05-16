using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using Approve.RuleCenter;
using Tools;
using System.Data;
using EgovaBLL;
using System.Reflection;

public partial class JSDW_ApplySGXKZGL_BGPrjItemDesc : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.h_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            BindControl();
            showInfo();
            ShowBGJG();
            ShowQYBGJG();
            ShowRYBGJG();
            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool.ExecuteScript("btnEnable();");
            }
        }
    }
    void BindControl()
    {

        //结构类型
        DataTable dt = rc.getDicTbByFNumber("509");
        t_ConstrType.DataSource = dt;
        t_ConstrType.DataTextField = "FName";
        t_ConstrType.DataValueField = "FNumber";
        t_ConstrType.DataBind();
        t_ConstrType.Items.Insert(0, new ListItem("--请选择--", ""));
        b_ConstrType.DataSource = dt;
        b_ConstrType.DataTextField = "FName";
        b_ConstrType.DataValueField = "FNumber";
        b_ConstrType.DataBind();
        b_ConstrType.Items.Insert(0, new ListItem("--请选择--", ""));

        //工程类别
        dt = rc.getDicTbByFNumber("20001");
        b_PrjItemType.DataSource = dt;
        b_PrjItemType.DataTextField = "FName";
        b_PrjItemType.DataValueField = "FNumber";
        b_PrjItemType.DataBind();
        b_ConstrType.Items.Insert(0, new ListItem("--请选择--", ""));
        t_PrjItemType.DataSource = dt;
        t_PrjItemType.DataTextField = "FName";
        t_PrjItemType.DataValueField = "FNumber";
        t_PrjItemType.DataBind();
        t_ConstrType.Items.Insert(0, new ListItem("--请选择--", ""));

        //所有制
        dt = rc.getDicTbByFNumber("112212");
        t_JSDWXZ.DataSource = dt;
        t_JSDWXZ.DataTextField = "FName";
        t_JSDWXZ.DataValueField = "FNumber";
        t_JSDWXZ.DataBind();
        b_JSDWXZ.DataSource = dt;
        b_JSDWXZ.DataTextField = "FName";
        b_JSDWXZ.DataValueField = "FNumber";
        b_JSDWXZ.DataBind();

        //币种
        dt = rc.getDicTbByFNumber("112211");
        t_Currency.DataSource = dt;
        t_Currency.DataTextField = "FName";
        t_Currency.DataValueField = "FNumber";
        t_Currency.DataBind();
        b_Currency.DataSource = dt;
        b_Currency.DataTextField = "FName";
        b_Currency.DataValueField = "FNumber";
        b_Currency.DataBind();
    }
    //显示
    private void showInfo()
    {
        TC_SGXKZ_BGPrjInfo sbg = dbContext.TC_SGXKZ_BGPrjInfo.Where(t => t.FAppId == h_FAppId.Value).FirstOrDefault();
        //修改为从flinkid获取，ly不要修改，依赖于从归档库中获取的数据的flinkid,modify by psq 20150501
        //TC_SGXKZ_PrjInfo sp = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FId == sbg.FPrjInfoId).FirstOrDefault();
        TC_SGXKZ_PrjInfo sp = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == sbg.FLinkId).FirstOrDefault();
        if (sp != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            if (null != sbg.JSDW)
            {
                tool.fillPageControl(sbg);
            }
            else
            {
                tool.fillPageControl(sp);
            }

            pageTool tool1 = new pageTool(this.Page, "hf_");
            tool1.fillPageControl(sp);
            pageTool tool2 = new pageTool(this.Page, "b_");
            tool2.fillPageControl(sp);
            b_JSDW_DeptID.fNumber = sp.JSDWAddressDept;
            b_JSDWAddressDept.Value = sp.JSDWAddressDept;
            b_PrjAddressDept.Value = sp.PrjAddressDept;
            b_PrjGovdeptid.fNumber = sp.PrjAddressDept;
            JSDW_DeptID.fNumber = t_JSDWAddressDept.Value;
            PrjGovdeptid.fNumber = t_PrjAddressDept.Value;

            h_FId.Value = sbg.FId;
            h_FPrjInfoId.Value = sbg.FPrjInfoId;
            h_FPrjItemId.Value = sbg.FPrjItemId;
        }
    }
    //保存
    private void saveInfo()
    {
        string fId = h_FId.Value;
        t_JSDWAddressDept.Value = JSDW_DeptID.fNumber;
        t_PrjAddressDept.Value = PrjGovdeptid.fNumber;
        b_JSDWAddressDept.Value = b_JSDW_DeptID.fNumber;
        b_PrjAddressDept.Value = b_PrjGovdeptid.fNumber;
        
        TC_SGXKZ_BGPrjInfo Emp = new TC_SGXKZ_BGPrjInfo();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_SGXKZ_BGPrjInfo.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FAppId = h_FAppId.Value;
            dbContext.TC_SGXKZ_BGPrjInfo.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        //同时向TC_SGXKZ_PrjInfo表中保存相关信息
        TC_SGXKZ_BGPrjInfo sbg = dbContext.TC_SGXKZ_BGPrjInfo.Where(t => t.FAppId == h_FAppId.Value).FirstOrDefault();
        TC_SGXKZ_PrjInfo tsp = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == h_FAppId.Value).FirstOrDefault();
        tsp.FPrjItemId = Emp.FPrjItemId;
        tsp.PrjId = Emp.PrjId;
        tsp.PrjItemName = Emp.PrjItemName;
        tsp.Address = Emp.Address;
        //tsp.Area = Emp.Area;
        //tsp.BuildType = Emp.BuildType;
        tsp.ConstrScale = Emp.ConstrScale;
        tsp.ConstrType = Emp.ConstrType;
        //tsp.Cost = Emp.Cost;
        tsp.Currency = Emp.Currency;
        //tsp.DoScale = Emp.DoScale;
        //tsp.DZZT = Emp.DZZT;
        tsp.EndDate = Emp.EndDate;
        tsp.FDDBR = Emp.FDDBR;
        tsp.FRDH = Emp.FRDH;
        tsp.FResult = Emp.FResult;
        //tsp.FZJG = Emp.FZJG;
        //tsp.FZTime = Emp.FZTime;
        tsp.JCity = Emp.JCity;
        tsp.JCounty = Emp.JCounty;
        tsp.JProvince = Emp.JProvince;
        tsp.JSDW = Emp.JSDW;
        tsp.JSDWAddressDept = Emp.JSDWAddressDept;
        tsp.JSDWDZ = Emp.JSDWDZ;
        //tsp.jsdwid = Emp.jsdwid;
        //tsp.JSDWXZ = Emp.JSDWXZ;
        //tsp.JSFZR = Emp.JSFZR;
        //tsp.JSFZRDH = Emp.JSFZRDH;
        //tsp.JSFZRZC = Emp.JSFZRZC;
        tsp.LXDH = Emp.LXDH;
        tsp.LZR = Emp.LZR;
        tsp.PCity = Emp.PCity;
        tsp.PCounty = Emp.PCounty;
        tsp.PProvince = Emp.PProvince;
        tsp.Price = Emp.Price;
        tsp.PrjAddressDept = Emp.PrjAddressDept;
        tsp.PrjItemType = Emp.PrjItemType;
        //tsp.ProjectFile = Emp.ProjectFile;
        //tsp.ProjectLevel = Emp.ProjectLevel;
        //tsp.ProjectName = Emp.ProjectName;
        //tsp.ProjectNo = Emp.ProjectNo;
        //tsp.ProjectNumber = Emp.ProjectNumber;
        //tsp.ProjectTime = Emp.ProjectTime;
        //tsp.ProjectUse = Emp.ProjectUse;
        //tsp.Remark = Emp.Remark;
        //tsp.ReportTime = Emp.ReportTime;
        //tsp.SGXKZBH = Emp.SGXKZBH;
        //tsp.SJEndDate = Emp.SJEndDate;
        //tsp.SJStartDate = Emp.SJStartDate;
        tsp.StartDate = Emp.StartDate;
        //tsp.upScale = Emp.upScale;
        dbContext.SubmitChanges();

        updateBGJG();
        h_FId.Value = fId;
        //showInfo();
        //System.Threading.Thread.Sleep(2000);
        //显示变更结果
        ScriptManager.RegisterStartupScript(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';$('#Button6').click();", true);

        //     MyPageTool.showMessageAjax("保存成功ii", up_Main);
        //    MyPageTool.showMessageAndRunFunctionAjax("保存成功", "window.returnValue='1';", up_Main);
        //Response.AddHeader("Refresh", "0"); 
    }
    //变更结果更新
    protected void updateBGJG()
    {
        TC_SGXKZ_BGPrjInfo sbg = new TC_SGXKZ_BGPrjInfo();
        //pageTool tool = new pageTool(this.Page, "b_");
        pageTool tool = new pageTool(this.Page, "t_");
        TC_SGXKZ_BGPrjInfo sbg1 = tool.getPageValue(sbg);
        PropertyInfo[] p1 = sbg1.GetType().GetProperties();
        //tool = new pageTool(this.Page, "t_");
        //TC_SGXKZ_BGPrjInfo sbg2 = tool.getPageValue(sbg);
        //System.Web.UI.Control container = this.Page.Form as System.Web.UI.Control;
        //System.Web.UI.Control myform = tool;
        //System.Web.UI.Control myform1 = tool1;
        //System.Web.UI.Control myform2 = tool2;

        //PropertyInfo[] p2 = sbg2.GetType().GetProperties();
        for (int i = 0; i < p1.Count(); i++)
        {
            string Id1 = p1[i].Name;
            string value1 = EConvert.ToString(p1[i].GetValue(sbg1, null));
            //string value2 = EConvert.ToString(p2[i].GetValue(sbg2, null));
            compareValueToSortedList(value1, Id1);
        }

    }
    private bool compareValueToSortedList(string value1, string Id1)
    {
        bool result = true;
        System.Web.UI.HtmlControls.HtmlForm myform1 = (System.Web.UI.HtmlControls.HtmlForm)this.Page.FindControl("Form1");
        //System.Web.UI.Control control1 = myform1.FindControl("t_" + Id1);
        System.Web.UI.Control control1 = myform1.FindControl("b_" + Id1);

        if (control1 != null)
        {
            string value2 = "";
            if (control1 is System.Web.UI.WebControls.TextBox)
            {
                value2 = EConvert.ToString(((System.Web.UI.WebControls.TextBox)control1).Text);
            }
            if (control1 is System.Web.UI.WebControls.DropDownList)
            {
                value2 = EConvert.ToString(((System.Web.UI.WebControls.DropDownList)control1).SelectedValue);
            }

            if (Id1 == "PrjAddressDept")
            {
                value2 = t_PrjAddressDept.Value;
            }
            if (Id1 == "JSDWAddressDept")
            {
                value2 = t_JSDWAddressDept.Value;
            }
            TC_SGXKZ_BGJG Emp = new TC_SGXKZ_BGJG();
            if (Id1 == "StartDate" || Id1 == "EndDate")
            {
                value1 = value1.Substring(0, 10).Replace("-", "/");
                value2 = value2.Substring(0, 10).Replace("-", "/");
            }
            result = value1 == value2;

            if (!result)
            {
                if (Id1 == "PrjAddressDept")
                {
                    value1 = dbContext.ManageDept.Where(t => t.FNumber == EConvert.ToInt(value1)).Select(t => t.FFullName).FirstOrDefault();
                    value2 = dbContext.ManageDept.Where(t => t.FNumber == EConvert.ToInt(value2)).Select(t => t.FFullName).FirstOrDefault();
                }
                if (Id1 == "JSDWAddressDept")
                {
                    value1 = dbContext.ManageDept.Where(t => t.FNumber == EConvert.ToInt(value1)).Select(t => t.FFullName).FirstOrDefault();
                    value2 = dbContext.ManageDept.Where(t => t.FNumber == EConvert.ToInt(t_JSDWAddressDept.Value)).Select(t => t.FFullName).FirstOrDefault();
                }
                if (Id1 == "PrjItemType" || Id1 == "ConstrType")
                {
                    value1 = dbContext.CF_Sys_Dic.Where(d => d.FNumber == EConvert.ToInt(value1)).Select(d => d.FName).FirstOrDefault();
                    value2 = dbContext.CF_Sys_Dic.Where(d => d.FNumber == EConvert.ToInt(value2)).Select(d => d.FName).FirstOrDefault();
                }
                System.Web.UI.HtmlControls.HtmlForm myform = (System.Web.UI.HtmlControls.HtmlForm)this.Page.FindControl("Form1");
                System.Web.UI.Control control = myform.FindControl("n_" + Id1);
                if (control !=null)
                {
                    var para = dbContext.TC_SGXKZ_BGJG.FirstOrDefault(t => t.BGNR == ((System.Web.UI.WebControls.HiddenField)control).Value);
                    if (para != null)
                    {
                        dbContext.TC_SGXKZ_BGJG.DeleteOnSubmit(para);
                    }
                }
                
                
                string fId = Guid.NewGuid().ToString();
                Emp.FId = fId;
                Emp.FAppId = h_FAppId.Value;
                Emp.FPrjInfoId = h_FPrjInfoId.Value;
                Emp.FPrjItemId = h_FPrjItemId.Value;
                if (control != null)
                {
                    Emp.BGNR = ((System.Web.UI.WebControls.HiddenField)control).Value;
                }
                Emp.BeforeBG = value1;
                Emp.AfterBG = value2;
                Emp.BGTime = EConvert.ToShortDateString(DateTime.Now);
                dbContext.TC_SGXKZ_BGJG.InsertOnSubmit(Emp);
                dbContext.SubmitChanges();
            }
        }
        return result;
    }
    /// <summary>
    /// 显示变更信息
    /// </summary>
    void ShowBGJG()
    {
        EgovaDB dbContext = new EgovaDB();
        string sql = @" select count(*) from TC_SGXKZ_RYBGJG
                            where FAppId='{0}' and BGQK='增加'";
        sql = string.Format(sql, h_FAppId.Value);
        int count1 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql).FirstOrDefault());
        sql = @" select count(*) from TC_SGXKZ_RYBGJG
                            where FAppId='{0}' and BGQK='退出'";
        sql = string.Format(sql, h_FAppId.Value);
        int count2 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql).FirstOrDefault());
        lblRY.InnerText = "(增加" + count1 + "个，退出" + count2 + "个)";
        var App = dbContext.TC_SGXKZ_BGJG.Where(t => t.FAppId == h_FAppId.Value).Select(t => new
        {
            t.BeforeBG,
            t.AfterBG,
            t.BGNR,
            t.BGTime,
            t.FId
        }).ToList();
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowBGJG();
    }
    void ShowQYBGJG()
    {
        EgovaDB dbContext = new EgovaDB();
        string sql = @" select count(*) from TC_SGXKZ_QYBGJG 
                            where FAppId='{0}' and BGQK='新增'";
        sql = string.Format(sql, h_FAppId.Value);
        int count1 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql).FirstOrDefault());
        sql = @" select count(*) from TC_SGXKZ_QYBGJG 
                            where FAppId='{0}' and BGQK='退出'";
        sql = string.Format(sql, h_FAppId.Value);
        int count2 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql).FirstOrDefault());
        lblQY.InnerText = "(增加" + count1 + "个，退出" + count2 + "个)";
        var App = dbContext.TC_SGXKZ_QYBGJG.Where(t => t.FAppId == h_FAppId.Value).Select(t => new
        {
            t.YQLX,
            t.YQMC,
            t.BGQK,
            t.BGTime,
            t.FId
        }).ToList();
        PagerQY.RecordCount = App.Count();
        dg_ListQY.DataSource = App.Skip((PagerQY.CurrentPageIndex - 1) * PagerQY.PageSize).Take(PagerQY.PageSize);
        dg_ListQY.DataBind();
        PagerQY.Visible = (PagerQY.RecordCount > PagerQY.PageSize);
    }
    protected void App_List_ItemDataBoundQY(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.PagerQY.PageSize * (this.PagerQY.CurrentPageIndex - 1)).ToString();
        }
    }
    protected void Pager1_PageChangingQY(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerQY.CurrentPageIndex = e.NewPageIndex;
        ShowQYBGJG();
    }
    void ShowRYBGJG()
    {
        EgovaDB dbContext = new EgovaDB();
        var App = dbContext.TC_SGXKZ_RYBGJG.Where(t => t.FAppId == h_FAppId.Value).Select(t => new
        {
            t.RYLX,
            t.XM,
            t.BGQK,
            t.BGTime,
            t.FId
        }).ToList();
        PagerRY.RecordCount = App.Count();
        dg_ListRY.DataSource = App.Skip((PagerRY.CurrentPageIndex - 1) * PagerRY.PageSize).Take(PagerRY.PageSize);
        dg_ListRY.DataBind();
        PagerRY.Visible = (PagerRY.RecordCount > PagerRY.PageSize);
    }
    protected void App_List_ItemDataBoundRY(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.PagerRY.PageSize * (this.PagerRY.CurrentPageIndex - 1)).ToString();
        }
    }
    protected void Pager1_PageChangingRY(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerRY.CurrentPageIndex = e.NewPageIndex;
        ShowRYBGJG();
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowBGJG();
    }
}