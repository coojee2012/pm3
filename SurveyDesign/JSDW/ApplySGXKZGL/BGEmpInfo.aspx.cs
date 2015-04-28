﻿using System;
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

public partial class JSDW_ApplySGXKZGL_EmpInfoForBG : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    RCenter rc2 = new RCenter("Standard_Dic");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            t_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            txtFId.Value = EConvert.ToString(Request["FId"]);
            t_FEntId.Value = EConvert.ToString(Request["entId"]);
            t_qyId.Value = EConvert.ToString(Request["qyId"]);
            //t_FPrjId.Value = EConvert.ToString(Request["FPrjId"]);
            t_FPrjItemId.Value = EConvert.ToString(Request["FPrjItemId"]);

        
         
            //t_FEntId.Value = EConvert.ToString(Request["FEntId"]);
            t_FEntType.Value = EConvert.ToString(Request["FEntType"]);
            //t_FPrjItemId.Value = EConvert.ToString(Request["FPrjItemId"]);

            BindControl();
            showInfo();
        }
    }
    void BindControl()
    {
        //人员类型
        DataTable dt = rc.getDicTbByFNumber("112202");
        t_EmpType.DataSource = dt;
        t_EmpType.DataTextField = "FName";
        t_EmpType.DataValueField = "FNumber";
        t_EmpType.DataBind();

        //学历
        dt = rc.getDicTbByFNumber("107");
        t_ZGXL.DataSource = dt;
        t_ZGXL.DataTextField = "FName";
        t_ZGXL.DataValueField = "FNumber";
        t_ZGXL.DataBind();

        //职称
        dt = rc.getDicTbByFNumber("5080");
        t_ZC.DataSource = dt;
        t_ZC.DataTextField = "FName";
        t_ZC.DataValueField = "FNumber";
        t_ZC.DataBind();
        //证书等级
        dt = rc2.GetTable("select *  from  (select FName,FNumber  from Standard_Dic.dbo.[CF_Dic_Person]  where  FType = 'zcrylx' union select '无' FName,'-1' FNumber) a order by  a.FNumber");
        t_DJ.DataSource = dt;
        t_DJ.DataTextField = "FName";
        t_DJ.DataValueField = "FNumber";
        t_DJ.DataBind();
        t_DJ.Items.FindByValue("-1").Selected = true;

    }
    private string getEmpType(string id)
    {
        switch (id)
        {
            default:
                return "项目负责人";
            case "1":
                return "项目负责人";
            case "2":
                return "项目技术负责人";
            case "3":
                return "安全负责人";
            case "4":
                return "施工员";
            case "5":
                return "质量员";
            case "6":
                return "安全员";
            case "7":
                return "材料员";
            case "8":
                return "预算员";
            case "9":
                return "总监理工程师";
            case "10":
                return "专业监理工程师";
            case "11":
                return "监理员";
            case "12":
                return "其他";
        }
    }
    //显示
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        if (!string.IsNullOrEmpty(txtFId.Value))
        {
            TC_PrjItem_Emp emp = dbContext.TC_PrjItem_Emp.Where(t => t.FId == txtFId.Value).FirstOrDefault();
            if (emp != null)
            {
                pageTool tool = new pageTool(this.Page, "t_");
                tool.fillPageControl(emp);
            }
        }

    }
    //保存
//    private void saveInfo()
//    {
//        string fId = txtFId.Value;
//        string fOldId = fId; 
//        dbContext = new EgovaDB();
//        TC_PrjItem_Emp Emp = new TC_PrjItem_Emp();
//        if (!string.IsNullOrEmpty(fId))
//        {
//            Emp = dbContext.TC_PrjItem_Emp.Where(t => t.FId == fId).FirstOrDefault();
//        }
//        else
//        {
//            string sql = @" select count(*) from TC_PrjItem_Emp
//                            where EmpType='11220201'
//                            and FAppId='{0}' and FEntId = '{1}'";
//            sql = string.Format(sql, t_FAppId.Value, t_qyId.Value);
//            int count = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql).FirstOrDefault());
//            dbContext = new EgovaDB();
//            string sql1 = @" select count(*) from TC_PrjItem_Emp
//                            where FIdCard='{0}'
//                            and FAppId='{1}' and FEntId = '{2}'";
//            sql1 = string.Format(sql1, t_FIdCard.Text, t_FAppId.Value, t_qyId.Value);
//            int count1 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql1).FirstOrDefault());
//            dbContext = new EgovaDB();
//            string sql2 = @" select count(*) from TC_PrjItem_Emp
//                            where EmpType='11220209'
//                            and FAppId='{0}' and FEntId = '{1}'";
//            sql2 = string.Format(sql2, t_FAppId.Value, t_qyId.Value);
//            int count2 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql2).FirstOrDefault());
//            if (t_EmpType.SelectedValue == "11220201" && count > 0)
//            {
//                ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('项目负责人只能添加一位');window.returnValue='1';", true);
//                return;
//            }
//            else if (count1 > 0)
//            {
//                ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('人员不允许重复添加');window.returnValue='1';", true);
//                return;
//            }
//            else if (count2 > 0)
//            {
//                ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('总监理工程师只能添加一位');window.returnValue='1';", true);
//                return;
//            }

//            fId = Guid.NewGuid().ToString();
//            Emp.FId = fId;
//            Emp.FEmpId = h_selEmpId.Value;
//            Emp.FPrjItemId = t_FPrjItemId.Value;
//            Emp.FAppId = t_FAppId.Value;
//            Emp.FTime = DateTime.Now;
//            Emp.FCreateTime = DateTime.Now;
//            Emp.FEntId = t_qyId.Value;
//            Emp.FLinkId = t_FEntId.Value;
//            dbContext.TC_PrjItem_Emp.InsertOnSubmit(Emp);
//        }
//        pageTool tool = new pageTool(this.Page);
//        Emp = tool.getPageValue(Emp);
//        dbContext.SubmitChanges();
//        txtFId.Value = fId;
//        if (string.IsNullOrEmpty(fOldId))
//        {
//            updateRYBG(fId);
//        }
//        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);

//        //     MyPageTool.showMessageAjax("保存成功ii", up_Main);
//        //    MyPageTool.showMessageAndRunFunctionAjax("保存成功", "window.returnValue='1';", up_Main);
//    }

    private void saveInfo()
    {
        var manualVal = t_IsManual.Value;
        dbContext = new EgovaDB();
        string fId = txtFId.Value;
        string zczsbh = t_ZCBH.Text.Trim();

        //判断项目负责人和总监理工程师是否已经存在，如果存在则不能再次添加。
        //如果是新增则直接判断是否已经存在
        if (fId == "")
        {
            string sql = @" select count(*) from TC_PrjItem_Emp  where EmpType='{0}' and FAppId='{1}' and FEntType='{2}'";
            sql = string.Format(sql, t_EmpType.SelectedValue, t_FAppId.Value, t_FEntType.Value);
            int count = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql).FirstOrDefault());

            if (count > 0)
            {
                switch (t_EmpType.SelectedValue)
                {
                    //项目负责人
                    case "11220201":
                        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('项目负责人只能添加一位');window.returnValue='1';", true);
                        return;
                    //总监理工程师
                    case "11220209":
                        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('总监理工程师只能添加一位');window.returnValue='1';", true);
                        return;
                }
            }
        }
        else//如果是修改,则加上身份证号码进行判断
        {
            string sql = @" select count(*) from TC_PrjItem_Emp  where EmpType='{0}' and FAppId='{1}' and FEntType='{2}'  and FIdCard != '{3}'";
            sql = string.Format(sql, t_EmpType.SelectedValue, t_FAppId.Value, t_FEntType.Value, t_FIdCard.Text.Trim());
            int count = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql).FirstOrDefault());

            if (count > 0)
            {
                switch (t_EmpType.SelectedValue)
                {
                    //项目负责人
                    case "11220201":
                        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('项目负责人只能添加一位');window.returnValue='1';", true);
                        return;
                    //总监理工程师
                    case "11220209":
                        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('总监理工程师只能添加一位');window.returnValue='1';", true);
                        return;
                }
            }
        }


        if (manualVal == "1")//手工录入
        {
            var countSql = @" select count(*) from [JST_XZSPBaseInfo].dbo.RY_RYZSXX  where SFZH='{0}'";
            countSql = string.Format(countSql, t_FIdCard.Text);
            int count2 = SConvert.ToInt(dbContext.ExecuteQuery<int>(countSql).FirstOrDefault());
            if (count2 > 0)
            {
                ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('归档库已存在该证书，请采用选择方式添加。');window.returnValue='1';", true);
                return;
            }
            //判断当前的注册编号是否已经存在，如果已经存在则提醒操作人员从库中选择
            var countsql2 = @" select count(*) from [JST_XZSPBaseInfo].dbo.RY_RYZSXX  where  ZCZSH = '{0}'";
            countsql2 = string.Format(countsql2, zczsbh);
            int count3 = SConvert.ToInt(dbContext.ExecuteQuery<int>(countsql2).FirstOrDefault());
            if (count3 > 0)
            {
                ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('注册证书已存在，请采用选择方式添加。');window.returnValue='1';", true);
                return;
            }
        }
        string sql1 = @" select count(*) from TC_PrjItem_Emp  where FIdCard='{0}'  and FAppId='{1}' and FEntType='{2}'";
        sql1 = string.Format(sql1, t_FIdCard.Text, t_FAppId.Value, t_FEntType.Value);
        int count1 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql1).FirstOrDefault());
        //如果存在就修改
        TC_PrjItem_Emp emp;
        pageTool tool;
        if (count1 > 0)
        {
            tool = new pageTool(this.Page);
            if (string.IsNullOrEmpty(fId))
            {
                emp =
                    dbContext.TC_PrjItem_Emp.FirstOrDefault(
                        t =>
                            t.FIdCard == t_FIdCard.Text && t.FAppId == t_FAppId.Value &&
                            t.FEntType == EConvert.ToInt(t_FEntType.Value));

            }
            else
            {
                emp = dbContext.TC_PrjItem_Emp.FirstOrDefault(t => t.FId == fId);
            }
            emp = tool.getPageValue(emp);
            //为添加的人员绑定，人员所在的单位，用来确定人员的类型
            if (emp != null) emp.FEntType = EConvert.ToInt(t_FEntType.Value);
            dbContext.SubmitChanges();
            ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功!');window.returnValue='1';", true);
            return;
        }




        emp = new TC_PrjItem_Emp();
        if (!string.IsNullOrEmpty(fId))
        {
            emp = dbContext.TC_PrjItem_Emp.FirstOrDefault(t => t.FId == fId);
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            emp.FId = fId;
            emp.FEmpId = h_selEmpId.Value;
            emp.FPrjItemId = t_FPrjItemId.Value;
            emp.FAppId = t_FAppId.Value;
            emp.FTime = DateTime.Now;
            emp.FCreateTime = DateTime.Now;
            emp.FEntId = t_FEntId.Value;
            dbContext.TC_PrjItem_Emp.InsertOnSubmit(emp);
        }
        tool = new pageTool(this.Page);
        emp = tool.getPageValue(emp);
        //为添加的人员绑定，人员所在的单位，用来确定人员的类型
        emp.FEntType = EConvert.ToInt(t_FEntType.Value);
        dbContext.SubmitChanges();
        txtFId.Value = fId;
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);

    }
    private void updateRYBG(string FId)
    {
        EgovaDB dbContext = new EgovaDB();
        TC_PrjItem_Emp ent = dbContext.TC_PrjItem_Emp.Where(t => t.FId == FId).FirstOrDefault();
        TC_SGXKZ_RYBGJG sr = new TC_SGXKZ_RYBGJG();

        sr.FId = Guid.NewGuid().ToString();
        sr.FAppId = t_FAppId.Value;
        sr.FPrjItemId = t_FPrjItemId.Value;
        sr.RYLX = getEmpType(t_EmpType.SelectedValue);
        sr.XM = ent.FHumanName;
        sr.ZSBH = ent.ZSBH;
        sr.QYMC = ent.FEntName;
        sr.BGQK = "增加";
        sr.BGTime = DateTime.Now;
        sr.FLinkId = ent.FId;
        dbContext.TC_SGXKZ_RYBGJG.InsertOnSubmit(sr);
        dbContext.SubmitChanges();
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    private void selEmp()
    {
        string selEmpId = h_selEmpId.Value;
        EgovaDB1 db = new EgovaDB1();
        var v = db.RY_RYJBXX.Where(t => t.RYBH == selEmpId).FirstOrDefault();
        //t_EmpType.SelectedValue = v.RYLBBM;
        if (v != null)
        {
            t_FHumanName.Text = v.XM;
            t_FIdCard.Text = v.SFZH;
            t_FSex.SelectedValue = v.XB.ToString();
            t_FMobile.Text = v.GRDH;
            t_ZC.SelectedItem.Text = v.ZC;
            t_ZW.Text = v.ZW;
            t_FTel.Text = v.BGDH;
            t_ZY.Text = v.SXZY;
            var v1 = db.RY_RYZSXX.Where(t => t.RYBH == selEmpId).FirstOrDefault();
            if (v1 != null)
            {
                t_ZSBH.Text = string.IsNullOrEmpty(v1.ZCZSBH) ? v1.ZCZSH : v1.ZCZSBH;
                //t_DJ.Text = v1.ZSJB;
                if (t_DJ.Items.Contains(new ListItem(v1.ZSLX.ToString())))
                {
                    t_DJ.SelectedValue = v1.ZSLX;
                }
                t_ZCBH.Text = v1.ZCZSH;
                t_ZCZY.Text = v1.ZCZY;
                t_ZCRQ.Text = v1.FZSJ.ToString();
            }

        }
    }
    protected void btnAddEmp_Click(object sender, EventArgs e)
    {
        selEmp();
    }
}