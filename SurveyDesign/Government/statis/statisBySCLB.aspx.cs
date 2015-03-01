﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;
using Approve.Common;
using Approve.RuleCenter;
using System.Text;
using System.Data;

public partial class Government_statis_AppInfoList2 : System.Web.UI.Page
{
    int fMType1 = 287;//勘察文件审查合同备案   
    int fMType2 = 28803;//技术性审查(勘察) 
    int fMType3 = 28802;//人员安排(勘察) 
    int fMType4 = 290;//备案 

    int jMType1 = 300;//施工图设计文件审查合同备案   
    int jMType2 = 305;//技术性审查(设计)  
    int jMType3 = 30102;//人员安排(设计) 
    int jMType4 = 305;//备案 

    int fc1 = 0;
    decimal fc1_1 = 0;
    int fc4 = 0;
    int fc5 = 0;

    int jc1 = 0;
    decimal jc1_1 = 0;
    int jc4 = 0;
    int jc5 = 0;
    string stag = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.RegisterStartupScript("js1", "<script>tab();</script>");
        if (!IsPostBack)
        {
            string fcol = db.Menu.Where(t => t.FNumber == Request.QueryString["fcol"]).Select(t => t.FName).FirstOrDefault();
            if (!string.IsNullOrEmpty(fcol))
                lit_Title.Text = fcol;
            BindControl();
            showInfo();
        }
    }
    ProjectDB db = new ProjectDB();
    void BindControl()
    {
        int iYear = DateTime.Now.Year;
        for (int i = 0; i < 10; i++)
        {
            string year = (iYear - i).ToString();
            dr_Year.Items.Add(new ListItem(year, year));
        }
        dr_Year.SelectedValue = iYear.ToString();
    }
    //显示
    private void showInfo()
    {
        btnReturn.Visible = false;
        string fDeptId = EConvert.ToString(ViewState["FCityCode"]);
        if (string.IsNullOrEmpty(fDeptId))
            fDeptId = CurrentDeptUser.DeptId.ToString();
        else
            btnReturn.Visible = true;
        ViewState["FNumber"] = fDeptId;

        StringBuilder sb = new StringBuilder();
        int iLen = EConvert.ToString(ViewState["FNumber"]).Length;
        int iLevel = iLen / 2;
        if (iLen < 6)
            iLevel += 1;
        int strLen = iLen;
        if (iLen < 6)
            strLen += 2;
        sb.Remove(0, sb.Length);
        sb.Append("select FName,FNumber,");
        sb.Append("'' F1,'' F1_1,'' F4,'' F5,");
        sb.Append("'' J1,'' J1_1,'' J4,'' J5 ");
        sb.Append("from cf_Sys_ManageDept ");
        sb.Append("where FNumber like '" + ViewState["FNumber"] + "%'  and FName not like'%市辖区%' ");
        sb.Append("and FNumber like '" + ComFunction.GetDefaultDept() + "%' ");
        sb.Append("and FLevel=" + iLevel + " and FClassNumber=102009 ");
        sb.Append("order by fLevel,FNumber");
        RCenter rc = new RCenter();
        DataTable dt = rc.GetTable(sb.ToString());
        int iYear = EConvert.ToInt(dr_Year.SelectedValue);
        //审查项目数（+审查次数+违反工程建设标准强制性条文数）
        //合同备案
        var v = from t in db.CF_App_List//合同备案
                join t2 in db.CF_App_List on t.FLinkId equals t2.FLinkId//技术性
                join t3 in db.CF_App_List on t.FLinkId equals t3.FLinkId//备案 
                join b in db.CF_Prj_Ent on t.FLinkId equals b.FAppId//金额
                join p in db.CF_Prj_BaseInfo on t.FPrjId equals p.FId
                where t.FState == 6 && t2.FState == 6 && t3.FState == 6
                && (db.CF_App_Idea.Where(i => i.FLinkId == t3.FId)
                    .OrderByDescending(i => i.FTime).Select(i => i.FResultInt)
                    .FirstOrDefault() == 1)//并且备案结果同意
                && b.FBaseInfoId == t2.FBaseinfoId && b.FEntType == 145//施工图
                && ((t.FManageTypeId == fMType1 && t2.FManageTypeId == fMType2
                && t3.FManageTypeId == fMType4)
                || (t.FManageTypeId == jMType1 && t2.FManageTypeId == jMType2
                && t3.FManageTypeId == jMType4))
                group t by new
                {
                    t.FPrjId,
                    FMonth = p.FAddressDept.Substring(0, strLen),
                    t.FLinkId,
                    t3.FAppDate,
                    t.FManageTypeId,
                    t.FReportCount,
                    b.FMoney
                } into g
                select new
                {
                    FMonth = Convert.ToInt32(g.Key.FMonth),
                    g.Key.FAppDate,//时间
                    FMType = g.Key.FManageTypeId,//业务类型
                    FCount = g.Key.FReportCount,//审查次数
                    FWQ = (from n in db.CF_App_List
                           join n1 in db.CF_App_List
                           on n.FLinkId equals n1.FLinkId
                           join m in db.CF_Prj_Emp on n1.FId equals m.FAppId
                           join i in db.CF_App_Idea
                           on n.FId equals i.FLinkId
                           where n.FLinkId == g.Key.FLinkId
                            && ((n.FManageTypeId == fMType2
                            && n1.FManageTypeId == fMType3)
                            || (n.FManageTypeId == jMType2
                            && n1.FManageTypeId == jMType3))
                            && n.FState == 6 && n1.FState == 6
                            && n.FBaseinfoId == n1.FBaseinfoId
                            && m.FType > 1 && m.FEmpBaseInfo == i.FUserId
                            && i.FOrder > 0
                           select 1).Count() > 0,//违反强条数>0
                    g.Key.FMoney//合同备案金额 
                };
        string sWhere = drop_CountWay.SelectedValue;
        string syear = dr_Year.SelectedValue;
        string squarter = dr_Quarter.SelectedValue;
        string smonth = dr_Month.SelectedValue;
        string szdy = txtFBTime.Text.Trim() + "|" + txtFETime.Text.Trim();
        //统计条件 
        if (sWhere == "year") //按年
        {
            if (iYear > 0)
                v = v.Where(t => t.FAppDate.GetValueOrDefault().Year == iYear);
            stag = syear;
        }
        else if (sWhere == "quarter") //按季度
        {
            if (iYear > 0)
                v = v.Where(t => t.FAppDate.GetValueOrDefault().Year == iYear);
            int iQ = EConvert.ToInt(dr_Quarter.SelectedValue) - 1;
            if (iQ >= 0)
                v = v.Where(t => t.FAppDate.GetValueOrDefault().Month > 3 * iQ
                    && t.FAppDate.GetValueOrDefault().Month <= 3 * (iQ + 1));
            stag = squarter;
        }
        else if (sWhere == "month") //按月
        {
            if (iYear > 0)
                v = v.Where(t => t.FAppDate.GetValueOrDefault().Year == iYear);
            int iM = EConvert.ToInt(dr_Month.SelectedValue);
            if (iM > 0)
                v = v.Where(t => t.FAppDate.GetValueOrDefault().Month == iM);
            stag = smonth;
        }
        else if (sWhere == "zdy") //自定义
        {
            if (!string.IsNullOrEmpty(txtFBTime.Text.Trim()))
            {
                v = v.Where(t => t.FAppDate >= Convert.ToDateTime(txtFBTime.Text));
            }
            if (!string.IsNullOrEmpty(txtFETime.Text.Trim()))
            {
                v = v.Where(t => t.FAppDate < Convert.ToDateTime(txtFETime.Text).AddDays(1));
            }
            stag = szdy;
        }
        var v1 = v.ToList();
        string sTemp = string.Empty;
        string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('statisByPrjList.aspx?FYear=" + iYear + "&FMType={0}&FMonth={1}&FType={2}&col=SCLB&sWhere=" + sWhere + "&sTag=" + stag + "&cityCode={4}',700,550);\">{3}</a>";
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            DataRow dr = dt.Rows[j];
            int i = EConvert.ToInt(dr["FNumber"]);
            int iCount = v1.Count(t => t.FMonth == i && t.FMType == fMType1);
            fc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1, i, "F", iCount, i);
            dr["F1"] = sTemp;//审查项目数
            iCount = v1.Count(t => t.FMonth == i && t.FMType == jMType1);
            jc1 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, jMType1, i, "J", iCount, i);
            dr["J1"] = sTemp;//审查项目数 
            /***************************************/
            decimal d = EConvert.ToDecimal(v1.Where(t => t.FMonth == i && t.FMType == fMType1).Sum(t => t.FMoney)); //项目金额 
            fc1_1 += d;
            dr["F1_1"] = d;
            d = EConvert.ToDecimal(v1.Where(t => t.FMonth == i && t.FMType == jMType1).Sum(t => t.FMoney)); //项目金额 
            jc1_1 += d;
            dr["J1_1"] = d;
            /***************************************/
            iCount = v1.Count(t => t.FMonth == i
                && t.FMType == fMType1 && t.FCount < 2);
            fc4 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1, i, "F4", iCount, i);
            dr["F4"] = sTemp;//一次性审查 
            iCount = v1.Count(t => t.FMonth == i
                 && t.FMType == jMType1 && t.FCount < 2);
            jc4 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, jMType1, i, "J4", iCount, i);
            dr["J4"] = sTemp;//一次性审查  
            /***************************************/
            iCount = v1.Count(t => t.FMonth == i && t.FMType == fMType1 && t.FWQ);
            fc5 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, fMType1, i, "F5", iCount, i);
            dr["F5"] = sTemp;//违强  
            iCount = v1.Count(t => t.FMonth == i && t.FMType == jMType1 && t.FWQ);
            jc5 += iCount;
            sTemp = iCount.ToString();
            if (iCount > 0)
                sTemp = string.Format(sUrl, jMType1, i, "J5", iCount, i);
            dr["J5"] = sTemp;//技术性审查   
            /***************************************/
        }
        DG_List.DataSource = dt;
        DG_List.DataBind();
    }
    protected void ddlFYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void DG_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item
             || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string fNumber = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
            if (fNumber.Length == 6)
            {
                LinkButton lb = e.Item.FindControl("ltName") as LinkButton;
                lb.Attributes["href"] = "javascript:void(0)";
            }
        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {
            object[] nums = new object[] { fc1, jc1, fc1_1, jc1_1, fc4, jc4, fc5, jc5 };
            string[] types = new string[] { "F", "J", "F1_1", "J1_1", "F4", "J4", "F5", "J5" };
            int[] mtypes = new int[] { fMType1, jMType1 };
            string sUrl = "<a href='javascript:void(0)' onclick=\"showAddWindow('statisByPrjList.aspx?FYear=" + dr_Year.SelectedValue + "&FMType={0}&FType={1}&col=SCLB&sWhere=" + drop_CountWay.SelectedValue + "&sTag=" + stag + "',700,550);\">{2}</a>";
            for (int i = 0; i < nums.Length; i++)
            {
                string sTemp = nums[i].ToString();
                if (!(i == 2 || i == 3) && sTemp != "0")
                {
                    sTemp = string.Format(sUrl, mtypes[i % 2], types[i], sTemp);
                }
                ((Literal)e.Item.FindControl("l_" + i + "")).Text = sTemp;
            }
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void btnOut_Click(object sender, EventArgs e)
    {
        Approve.Common.SaveAsBase sab = new Approve.Common.SaveAsBase();
        sab.SaveAsExc("审查项目统计-按审查类别统计", this.Response, h_div.Value);
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        ViewState["FCityCode"] = null;
        showInfo();
    }
    protected void DG_List_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item
            || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.CommandName == "Sel")
            {
                string fNumber = e.CommandArgument.ToString();
                ViewState["FCityCode"] = fNumber;
                showInfo();
            }
        }
    }
}
