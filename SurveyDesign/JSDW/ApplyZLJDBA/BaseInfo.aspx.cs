using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using Tools;
using System.Data;
using Approve.RuleCenter;
using System.Text;

public partial class JSDW_ApplyZLJDBA_BaseInfo : System.Web.UI.Page
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
            if (EConvert.ToInt(Session["FIsApprove"])!=0)
            {
                tool.ExecuteScript("btnEnable();");
            }
        }

    }
     
    void BindControl()
    {
        string deptId = ComFunction.GetDefaultDept();
        govd_FRegistDeptId.fNumber = deptId;
        govd_FRegistDeptId.Dis(1);
        //工程性质
        DataTable dt = rc.getDicTbByFNumber("20001");
        p_PrjItemType.DataSource = dt;
        p_PrjItemType.DataTextField = "FName";
        p_PrjItemType.DataValueField = "FNumber";
        p_PrjItemType.DataBind();

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
        TC_QA_Record qa = db.TC_QA_Record.Where(t => t.FAppId.Equals(EConvert.ToString(Session["FAppId"]))).FirstOrDefault();
        TC_PrjItem_Info prj = db.TC_PrjItem_Info.Where(t => t.FId == qa.FPrjItemId).FirstOrDefault();
        TC_Prj_Info prjInfo = db.TC_Prj_Info.Where(t => t.FId == qa.FPrjId).FirstOrDefault();
        if (prj != null)
        {
            ViewState["FPrjID"] = prj.FPrjId;
            p_RecordNo.Text = qa.RecordNo;
            pageTool tool = new pageTool(this.Page, "p_");
            tool.fillPageControl(prj);
            tool = new pageTool(this.Page, "pj_");
            tool.fillPageControl(prjInfo);
            tool = new pageTool(this.Page, "q_");
            tool.fillPageControl(qa);
            q_SGDWoldId.Value = qa.SGDWId; 
            sj_FName.Text = qa.SJDW;
            sj_FRegistAddress.Text = qa.SJDWDZ;
            sj_FLinkMan.Text = qa.SJDWFR;
            sj_FMobile.Text = qa.SJDWDH;
            sj_FLicence.Text = qa.SJDWZS;
            if (qa.RegisterTime.HasValue)
            { 
            pj_ProjectTime.Text = qa.RegisterTime.Value.ToString("yyyy-M-d");
            }
            govd_FRegistDeptId.fNumber = pj_AddressDept.Value;
            q_AddressDept.Value = pj_AddressDept.Value;
            if (string.IsNullOrEmpty(p_RecordNo.Text))
            {
                p_RecordNo.Text = GetPrjNo(prjInfo.ProjectNo);
            }
            string t = prj.PrjItemType;
            tool = new pageTool(this.Page);
            ClientScript.RegisterStartupScript(this.GetType(), "showTr", "<script>showTr();</script>");
        }
        showOtherEnt(6);
    }

    string GetPrjNo(string projectNo)
    {
        RCenter rc = new RCenter();
        string fNo = string.Empty;
        if (string.IsNullOrEmpty(fNo))
        {
            ////查询最大的号码
            //StringBuilder sb = new StringBuilder();
            //sb.Append("select ISNULL(max(right(ProjectNo,2)),0) from TC_Prj_Info ");
            //sb.Append("where substring(ProjectNo,7,6) = '" + fDate + "'");
            //int iNo = EConvert.ToInt(rc.GetSignValue(sb.ToString())) + 1;
            fNo = projectNo + "-ZX-001";         
        }
        return fNo;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        TC_QA_Record qa = new TC_QA_Record();
        qa = db.TC_QA_Record.Where(t => t.FAppId == EConvert.ToString(Session["FAppId"])).FirstOrDefault();
        qa.JSDW = p_JSDW.Text;
        qa.SJDW = sj_FName.Text;
        qa.SJDWDZ = sj_FRegistAddress.Text;
        qa.SJDWFR = sj_FLinkMan.Text;
        qa.SJDWDH = sj_FMobile.Text;
        qa.SJDWZS = sj_FLicence.Text;
        qa.SJDWId = sj_FBaseInfoId.Value;
        qa.RegisterTime = Convert.ToDateTime(pj_ProjectTime.Text);
        pageTool tool = new pageTool(this.Page,"p_");
        qa = tool.getPageValue(qa);
        tool = new pageTool(this.Page, "pj_");
        qa = tool.getPageValue(qa);
        tool = new pageTool(this.Page, "q_");
        qa = tool.getPageValue(qa);
        //qa.SGDW = "";

        qa.Contracts = pj_Contacts.Text;
        qa.AddressDept = govd_FRegistDeptId.fNumber;
        db.SubmitChanges();
        showPrjData();
        pageTool tool1 = new pageTool(this.Page);
        //ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
        tool1.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
    }

    //人员选择:建造师
    protected void btnSel_JCS_Click(object sender, EventArgs e)
    {

        string selEmpid = sj_ZCJCS.Value; //注册建造师

        EgovaDB1 db1 = new EgovaDB1();
        //RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYBH == selEmpid).FirstOrDefault();
        RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYZSXXID == selEmpid).FirstOrDefault();
        if (v != null)
        {
            q_JZS.Text = v.XM;;
        }

    }

    //人员选择：结构师
    protected void btnSel_JGS_Click(object sender, EventArgs e)
    {

        string selEmpid = sj_ZCJGS.Value; //注册结构师

        EgovaDB1 db1 = new EgovaDB1();

        //RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYBH == selEmpid).FirstOrDefault();
        RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYZSXXID == selEmpid).FirstOrDefault();
        if (v != null)
        {
            q_JGS.Text = v.XM; ;
        }

    }

    //人员选择：岩土工程师
    protected void btnSel_YTG_Click(object sender, EventArgs e)
    {

        string selEmpid = kc_YTGCS.Value; //岩土工程师

        EgovaDB1 db1 = new EgovaDB1();
        //RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYBH == selEmpid).FirstOrDefault();
        RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYZSXXID == selEmpid).FirstOrDefault();
        if (v != null)
        {
            q_YTGCS.Text = v.XM;
            q_CCDWZS.Text = v.ZCZSBH;
        }

    }
    //人员选择：项目经理
    protected void btnSel_XMJ_Click(object sender, EventArgs e)
    {

        string selEmpid = sj_XMJL.Value; //项目经理

        EgovaDB1 db1 = new EgovaDB1();
        //RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYBH == selEmpid).FirstOrDefault();
        RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYZSXXID == selEmpid).FirstOrDefault();
        if (v != null)
        {
            q_XMJL.Text = v.XM; 
        }

    }

    //人员选择：项目总监
    protected void btnSel_XMZ_Click(object sender, EventArgs e)
    {

        string selEmpid = jl_XMZJ.Value; //项目总监

        EgovaDB1 db1 = new EgovaDB1();
        //RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYBH == selEmpid).FirstOrDefault();
        RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYZSXXID == selEmpid).FirstOrDefault();
        if (v != null)
        {
            q_XMZJ.Text = v.XM; ;
        }

    }

    protected void btnSel_sj_Click(object sender, EventArgs e)
    {
        string selEntId = sj_FBaseInfoId.Value;
        EgovaDB1 db1 = new EgovaDB1();
        QY_JBXX v = db1.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v != null)
        {
            sj_FName.Text = v.QYMC;
            sj_FRegistAddress.Text = v.QYXXDZ;
            sj_FLinkMan.Text = v.FRDB;
            sj_FMobile.Text = v.LXDH;
        }
        var v1 = db1.QY_QYZSXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v1 != null)
            sj_FLicence.Text = v1.ZSBH;
        //变更单位时清空项目设计注册建筑师和项目设计注册结构师
        q_JZS.Text = "";
        q_JGS.Text = "";
        ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr();</script>");
    }
    protected void btnSel_kc_Click(object sender, EventArgs e)
    {
        string selEntId = q_KCDWId.Value;
        EgovaDB1 db1 = new EgovaDB1();
        QY_JBXX v = db1.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v != null)
        {
            q_CCDW.Text = v.QYMC;
            q_CCDWDZ.Text = v.QYXXDZ;
            q_CCDWFR.Text = v.FRDB;
            q_CCDWDH.Text = v.LXDH;
        }
        var v1 = db1.QY_QYZSXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v1 != null)
            q_CCDWZS.Text = v1.ZSBH;
        //重新选择单位时清空注册岩土工程师信息
        q_YTGCS.Text = "";
        ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr();</script>");
    }
    protected void btnSel_sg_Click(object sender, EventArgs e)
    {
        string selEntId = q_SGDWId.Value;
        EgovaDB1 db1 = new EgovaDB1();
        QY_JBXX v = db1.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v != null)
        {
            q_SGDW.Text = v.QYMC;
            q_SGDWDZ.Text = v.QYXXDZ;
            q_SGDWFR.Text = v.FRDB;
            q_SGDWDH.Text = v.LXDH;
        }
        var v1 = db1.QY_QYZSXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v1 != null)
            q_SGDWZS.Text = v1.ZSBH;
        //更改单位的同时删除以前的项目经理
        q_XMJL.Text = "";
        ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr();</script>");
    }
    protected void btnSel_jl_Click(object sender, EventArgs e)
    {
        string selEntId = q_JLDWId.Value;
        EgovaDB1 db1 = new EgovaDB1();
        QY_JBXX v = db1.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v != null)
        {
            q_JLDW.Text = v.QYMC;
            q_JLDWDZ.Text = v.QYXXDZ;
            q_JLDWFR.Text = v.FRDB;
            q_JLDWDH.Text = v.LXDH;
        }
        var v1 = db1.QY_QYZSXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v1 != null)
            q_JLZS.Text = v1.ZSBH;
        //变更单位时清空项目总监
        q_XMZJ.Text = "";
        ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr();</script>");
    }
    //添加联合体企业
    protected void btnAddEnt_sj_Click(object sender, EventArgs e)
    {
        string FID = sj_other_FBaseInfoId.Value;
        var FEntType = 6;//设计
        EgovaDB1 db1 = new EgovaDB1();
        String FAppID = EConvert.ToString(Session["FAppId"]);
        var v = (from t in db1.QY_JBXX
                 where t.QYBM == FID
                 select new
                 {
                     t.QYBM,
                     t.QYMC,
                 }).FirstOrDefault();
        if (v != null)
        {
            TC_Prj_Ent ent = db.TC_Prj_Ent.Where(t => t.FBaseInfoId == v.QYBM && t.FAppId == FAppID && t.FEntType == FEntType).FirstOrDefault();
            if (ent == null)
            {
                ent = new TC_Prj_Ent();
                db.TC_Prj_Ent.InsertOnSubmit(ent);
                ent.FId = Guid.NewGuid().ToString();
                ent.FPrjId = (string) ViewState["FPrjID"];
                ent.FBaseInfoId = v.QYBM;
                ent.FEntType = FEntType;
                ent.FAppId = FAppID;
                ent.FName = v.QYMC;
                ent.FIsDeleted = false;
                ent.FTime = DateTime.Now;
                ent.FCreateTime = DateTime.Now;

                db.SubmitChanges();

                sj_FInt2.Checked = true;//选企业后自动勾上联合体

                showOtherEnt(6);
            }
        }
    }
    //联合体企业，只有手添的合同显示，自动的合同直接从合同备案业务取
    private void showOtherEnt(int type)
    {
        EgovaDB db = new EgovaDB();
        string FAppId = EConvert.ToString(Session["FAppId"]);
        int FEntType = type;
        var v = from t in db.TC_Prj_Ent
                where t.FAppId == FAppId && t.FEntType == FEntType
                select new { t.FName, t.FId, };
        TC_Prj_Ent ent = db.TC_Prj_Ent.Where(t => t.FAppId == FAppId && t.FEntType == FEntType).FirstOrDefault();
        if (ent == null)
        {
            sj_FInt2.Checked = false;
        }
        else
        {
            sj_FInt2.Checked = true;
        }
        DG_List.DataSource = v;
        DG_List.DataBind();
        ClientScript.RegisterStartupScript(this.GetType(), "showTr2", "<script>showTr();</script>");
    }
    //列表事件
    protected void DG_List_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            string FID = e.CommandArgument.ToString();
            TC_Prj_Ent ent = db.TC_Prj_Ent.Where(t => t.FId == FID).FirstOrDefault();
            if (ent != null)
            {
                pageTool tool = new pageTool(this.Page);
                db.TC_Prj_Ent.DeleteOnSubmit(ent);
                db.SubmitChanges();
                tool.showMessage("删除成功");
                showOtherEnt(6);
            }
        }
    }
    //protected void Button4_Click(object sender, EventArgs e)
    //{
    //    this.q_XMJL.Text = "";
    //}
}