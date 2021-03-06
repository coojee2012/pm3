﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using Tools;
using System.Data;
using Approve.RuleCenter;

public partial class JSDW_ApplyAQJDBA_BaseInfo : System.Web.UI.Page
{
    EgovaDB db = new EgovaDB();
    RCenter rc = new RCenter();
    string fAppId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fAppId = EConvert.ToString(Session["FAppId"]);
            BindControl();
            showPrjData();
          //  ShowEntInfo();
          //  showInfo();
            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                btnAddEnt.Visible = false;
                Button1.Visible = false;
                Button2.Visible = false;
                tool.ExecuteScript("btnEnable();");
            }
        }

    }
    void BindControl()
    {
        string deptId = ComFunction.GetDefaultDept();
        govd_FRegistDeptId.fNumber = deptId;
        govd_FRegistDeptId.Dis(1);
        DataTable dt = rc.getDicTbByFNumber("20001");

        //结构类型
        dt = rc.getDicTbByFNumber("509");
        q_ConstrType.DataSource = dt;
        q_ConstrType.DataTextField = "FName";
        q_ConstrType.DataValueField = "FNumber";
        q_ConstrType.DataBind();

    }
    private void showPrjData()
    {
        EgovaDB db = new EgovaDB();
        TC_AJBA_Record qa = db.TC_AJBA_Record.Where(t => t.FAppId == EConvert.ToString(Session["FAppId"])).FirstOrDefault();


        if (qa.RecordTime == null)
        {
            v_PrjItem_Info prj = db.v_PrjItem_Info.Where(t => t.FId == qa.FPrjItemId).FirstOrDefault();
            v_prj_Info prjInfo = db.v_prj_Info.Where(t => t.FId == qa.FPrjId).FirstOrDefault();
            pageTool tool = new pageTool(this.Page, "p_");
            tool.fillPageControl(prj);
            tool = new pageTool(this.Page, "pj_");
            tool.fillPageControl(prjInfo);
            ViewState["FPrjID"] = qa.FPrjId;
        }
        else
        {
            t_SGId.Value = qa.SGId;
            t_jldwid.Value = qa.JLId;
            ViewState["FPrjID"] = qa.FPrjId;
            p_RecordNo.Text = qa.RecordNo;
            pageTool tool = new pageTool(this.Page, "p_");
            tool.fillPageControl(qa);
            tool = new pageTool(this.Page, "pj_");
            tool.fillPageControl(qa);
            tool = new pageTool(this.Page, "q_");
            tool.fillPageControl(qa);
            govd_FRegistDeptId.fNumber = pj_AddressDept.Value;
            q_AddressDept.Value = pj_AddressDept.Value;
            string t = qa.ProjectType;
            tool = new pageTool(this.Page);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TC_AJBA_Record qa = new TC_AJBA_Record();
        qa = db.TC_AJBA_Record.Where(t => t.FAppId == EConvert.ToString(Session["FAppId"])).FirstOrDefault();
        qa.JSDW = p_JSDW.Text;
        pageTool tool = new pageTool(this.Page,"p_");
        qa = tool.getPageValue(qa);
        tool = new pageTool(this.Page, "pj_");
        qa = tool.getPageValue(qa);
        tool = new pageTool(this.Page, "q_");
        qa = tool.getPageValue(qa);
        qa.Contracts = pj_Contacts.Text;
        qa.SGId = t_SGId.Value;
        qa.SGRYId = t_SGRYId.Value;
        //qa.JLId = t_JLId.Value; //此处存放的是监理单位的资质id主键，修改为监理单位id
        qa.JLId = t_jldwid.Value;       
        qa.JLRYId = t_JLRYId.Value;
        if (qa.RecordTime ==null){ qa.RecordTime = System.DateTime.Now; }
        db.SubmitChanges();
      //  showPrjData();
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {

    }
    protected void btnAddEntSG_Click(object sender, EventArgs e)
    {
        selEnt("SG");
    }
    protected void btnAddEntJL_Click(object sender, EventArgs e)
    {
        selEnt("JL");
    }
    protected void btnAddEmpSG_Click(object sender, EventArgs e)
    {
        selEmp("SG");
    }
    protected void btnAddEmpJL_Click(object sender, EventArgs e)
    {
        selEmp("JL");
    }
    private void selEmp(string type)
    {
        if (type == "SG")
        {
            string selEmpId = t_SGRYId.Value;
            EgovaDB1 db = new EgovaDB1();

            var v = db.RY_RYZSXX.Where(t => t.RYZSXXID == selEmpId).FirstOrDefault(); //直接从资质信息中获取
            if (v != null)
            {
                q_SGDWXMJL.Text = v.XM;
                q_SGDWZGZH.Text = v.ZCZSH;
                q_SGDWSFZH.Text = v.SFZH;
                t_SGRYId.Value = v.RYBH;                
            }

            //var v = db.RY_RYJBXX.Where(t => t.RYBH == selEmpId).FirstOrDefault();
            //if (v != null)
            //{
            //    q_SGDWXMJL.Text = v.XM;
                
            //    q_SGDWSFZH.Text = v.SFZH;
            //    var v1 = db.RY_RYZSXX.Where(t => t.RYBH == selEmpId).FirstOrDefault();
            //    if (v1 != null)
            //    {
            //        q_SGDWZGZH.Text = v1.ZCZSH;
            //    }
            //}

        }
        else if (type == "JL")
        {
            string selEmpId = t_JLRYId.Value;
            EgovaDB1 db = new EgovaDB1();

            var v = db.RY_RYZSXX.Where(t => t.RYZSXXID == selEmpId).FirstOrDefault();
            if (v != null)
            {
                q_JLDWXMZJ.Text = v.XM;
                q_JLDWZGZH.Text = v.ZCZSH;
                
            }

            //var v = db.RY_RYJBXX.Where(t => t.RYBH == selEmpId).FirstOrDefault();
            //if (v != null)
            //{
            //    q_JLDWXMZJ.Text = v.XM;
                
            //    var v1 = db.RY_RYZSXX.Where(t => t.RYBH == selEmpId).FirstOrDefault();
            //    if (v1 != null)
            //    {
            //        q_JLDWZGZH.Text = v1.ZCZSH;
            //    }
            //}

        }
    }

    /// <summary>
    /// 选择具有安许证的施工类企业
    /// </summary>
    private void selEntAXZ()
    {
        string selEntId = t_SGId_AXZ.Value;
        EgovaDB1 db = new EgovaDB1();
        var v = db.QY_QYZSXX.Where(t => t.QYBM == selEntId && t.ZSLXBM == "2").FirstOrDefault();
       
        if (v != null)
        {  
                q_SGDWAXZH.Text = v.ZSBH;   
        }
    }


    private void selEnt(string type)
    {
        if (type == "SG")
        {
            string selEntId = t_SGId.Value;
            EgovaDB1 db = new EgovaDB1();
            var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
            if (v != null) {
                var v1 = db.QY_QYZZXX.Where(t => t.QYBM == selEntId && t.SFZX == 1).FirstOrDefault();
                if (v1 != null) {
                    q_SGDWZZZSH.Text = v1.ZSBH;
                    q_SGDWZZDJ.Text = v1.ZZLB+v1.ZZMC+v1.ZZDJ;
                    //q_SGDWZZDJ.Text = v1.ZZDJ;  // 改为显示资质名称及等级tt.ZZLB + tt.ZZMC + tt.ZZDJ
                }
                //var v2 = db.QY_QYZSXX.Where(t => t.QYBM == selEntId && t.ZSLXBM == "150").FirstOrDefault();//安全生产许可证
                //if (v2 != null)
                //{
                //    q_SGDWAXZH.Text = v2.ZSBH;
                //}
            }
            q_SGDW.Text = v.QYMC;
            q_SGDWFR.Text = v.FRDB;
            q_SGDWDH.Text = v.FRDBSJH;
            q_SGDWZZJGDM.Text = v.JGDM;
            //清空之前的人员
            q_SGDWXMJL.Text = "";
            q_SGDWZGZH.Text = "";           
        }
        else if (type == "JL")
        {
            string selEntId = t_JLId.Value;
            EgovaDB1 db = new EgovaDB1();

            var v2 = db.QY_QYZZXX.Where(t => t.QYZZID == selEntId).FirstOrDefault();
            if (v2 != null)
            {
                q_JLDWZZZSH.Text = v2.ZSBH;
                q_JLDWZZDJ.Text = v2.ZZLB + v2.ZZMC + v2.ZZDJ;
                var v = db.QY_JBXX.Where(t => t.QYBM == v2.QYBM).FirstOrDefault();
                if (v != null)
                {
                    q_JLDW.Text = v.QYMC;
                    q_JLDWFR.Text = v.FRDB;
                    q_JLDWDH.Text = v.FRDBSJH;
                    q_JLDWZZJGDM.Text = v.JGDM;
                    t_jldwid.Value = v.QYBM;
                    //清空之前的企业人员
                    q_JLDWZGZH.Text = "";
                    q_JLDWXMZJ.Text = "";
                }
            }

            //var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
            //if (v != null)
            //{
            //    var v1 = db.QY_QYZZXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
            //    if (v1 != null)
            //    {
            //        q_JLDWZZZSH.Text = v1.ZSBH;
            //        q_JLDWZZDJ.Text = v1.ZZLB+v1.ZZMC+v1.ZZDJ;
            //        //q_JLDWZZDJ.Text = v1.ZZDJ;
            //    }
                
            //}
            //q_JLDW.Text = v.QYMC;
            //q_JLDWFR.Text = v.FRDB;
            //q_JLDWDH.Text = v.FRDBSJH;
            //q_JLDWZZJGDM.Text = v.JGDM;
        }
        
    }
    protected void btnAddEntSGzxz_Click(object sender, EventArgs e)
    {
        selEntAXZ();
    }
}