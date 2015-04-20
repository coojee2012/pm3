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

public partial class JSDW_ApplySGXKZGL_HTBA : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                showInfo();
            }
            else
            {
                this.hf_FAppId.Value = EConvert.ToString(Request["FAppId"]);
                showInfo();
            }

            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool.ExecuteScript("btnEnable();");
                //取值不规范，手动屏蔽
                Button1.Visible = false;
                Button2.Visible = false;
                Button3.Visible = false;
            }
        }
    }
    void BindControl()
    {

        //合同类别
        DataTable dt = rc.getDicTbByFNumber("511");
        t_HTLB.DataSource = dt;
        t_HTLB.DataTextField = "FName";
        t_HTLB.DataValueField = "FNumber";
        t_HTLB.DataBind();
    }
    //显示
    private void showInfo()
    {

        TC_SGXKZ_HTBA emp = dbContext.TC_SGXKZ_HTBA.Where(t => t.FId == ViewState["FID"]).FirstOrDefault();
        if (emp != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(emp);
            hf_FId.Value = emp.FId;
        }
        else
        {
            TC_SGXKZ_PrjInfo sp = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == hf_FAppId.Value).FirstOrDefault();
            if (sp != null)
            {
                t_ProjectNo.Text = sp.ProjectNo;
                t_ConstrScale.Text = sp.ConstrScale;
                t_FPrjItemId.Value = sp.PrjItemId;
            }

        }

    }
    //保存
    private void saveInfo()
    {
        var isNew = true;
        string fId = hf_FId.Value;
        TC_SGXKZ_HTBA Emp = new TC_SGXKZ_HTBA();
        if (!string.IsNullOrEmpty(fId))
        {
            isNew = false;
            Emp = dbContext.TC_SGXKZ_HTBA.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FAppId = hf_FAppId.Value;
            Emp.FprjItemId = t_FPrjItemId.Value;
            dbContext.TC_SGXKZ_HTBA.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        if (isNew)
        {
            var count = dbContext.TC_SGXKZ_HTBA.Count(t => t.FAppId == hf_FAppId.Value && t.HTBABH == Emp.HTBABH);
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('已存在该合同');window.returnValue='1';", true);
                return;
            }
        }
        dbContext.SubmitChanges();
        hf_FId.Value = fId;
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void btnAddEntFB_Click(object sender, EventArgs e)
    {
        //发包
        selEnt("FB");
    }
    protected void btnAddEntCB_Click(object sender, EventArgs e)
    {
        //承包
        selEnt("CB");
    }
    protected void btnAddEntLHT_Click(object sender, EventArgs e)
    {
        //联合体
        selEnt("LHT");
    }
    private void selEnt(string type)
    {
        if (type == "FB")
        {
            string selEntId = t_FBDWId.Value;
            EgovaDB1 db = new EgovaDB1();
            var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
            if (v != null)
            {
                t_FBDWMC.Text = v.QYMC;
                t_FBDWZZJGDM.Text = v.JGDM;
            }


        }
        else if (type == "CB")
        {
            string selEntId = t_CBDWId.Value;
            EgovaDB1 db = new EgovaDB1();
            var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
            if (v != null)
            {
                t_CBDWMC.Text = v.QYMC;
                t_CBDWZZJGDM.Text = v.JGDM;
            }

        }
        else if (type == "LHT")
        {
            string selEntId = t_LHTDWId.Value;
            EgovaDB1 db = new EgovaDB1();
            var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
            if (v != null)
            {
                t_LHTCBDWMC.Text = v.QYMC;
                t_LHTCBDWZZJGDM.Text = v.JGDM;
            }

        }

    }
}