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

public partial class JSDW_ApplyBHGD_BHGDInfo : System.Web.UI.Page
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
            showData();
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
        b_ProjectType.DataSource = dt;
        b_ProjectType.DataTextField = "FName";
        b_ProjectType.DataValueField = "FNumber";
        b_ProjectType.DataBind();

        //结构类型
        dt = rc.getDicTbByFNumber("509");
        b_ConstrType.DataSource = dt;
        b_ConstrType.DataTextField = "FName";
        b_ConstrType.DataValueField = "FNumber";
        b_ConstrType.DataBind();

    }
    private void showData()
    {
        string sgdwid = null,jldwid = null ,jsdwid = null;
        EgovaDB db = new EgovaDB();
        //
        TC_BZGD_Record bh = db.TC_BZGD_Record.Where(t => t.FAppId.Equals(EConvert.ToString(Session["FAppId"]))).FirstOrDefault();

        if (bh != null)
        {
            ViewState["FPrjID"] = bh.FPrjId;
            pageTool tool = new pageTool(this.Page, "b_");
            tool.fillPageControl(bh);
            ClientScript.RegisterStartupScript(this.GetType(), "showTr", "<script>showTr();</script>");

            var app = db.CF_App_List.Where(t => t.FId == fAppId)
                      .Select(t => new { t.FName, t.FYear, t.FPrjId, t.FState, t.FUpDeptId }).FirstOrDefault();
            if (app != null)
            {
                if (app.FState == 1 || app.FState == 6)
                {
                    Session["FIsApprove"] = 1;
                }
            }

            sgdwid = bh.SGDWId;
            jldwid = bh.JLDWId;
            jsdwid = bh.JSDWID;
        }

        //找到施工单位
        if (string.IsNullOrEmpty(sgdwid))
        {
            TC_PrjItem_Ent sgdw = db.TC_PrjItem_Ent.Where(t => t.FPrjId == bh.FPrjId && (t.FEntType == 2)).FirstOrDefault();
            if (sgdw != null)
            {
                sgdwid = sgdw.QYID;
                b_SGDW.Text = sgdw.FName;
            }
        }
        b_SGDWId.Value = sgdwid;

        //监理单位
        if (string.IsNullOrEmpty(jldwid))
        {
            TC_PrjItem_Ent jldw = db.TC_PrjItem_Ent.Where(t => t.FPrjId == bh.FPrjId && (t.FEntType == 7)).FirstOrDefault();
            if (jldw != null)
            {
                jldwid = jldw.QYID;
                b_JLDW.Text = jldw.FName;
                b_JLDWMC.Text = jldw.FName;
            }
        }
        b_JLDWId.Value = jldwid;

        //建设单位
        if (string.IsNullOrEmpty(jsdwid))
        {
            TC_Prj_Info jsdw = db.TC_Prj_Info.Where(t => t.FId == bh.FPrjId).FirstOrDefault();
            if (jsdw != null)
            {
                jsdwid = jsdw.FJSDWID;
                b_JSDWMC.Text = jsdw.JSDW;
                b_JSDW.Text = jsdw.JSDW;
                b_Address.Text = jsdw.Address;
            }
        }
        b_JSDWID.Value = jsdwid;

        EgovaDB1 db1 = new EgovaDB1();

        //找到施工单位资质信息
        if (string.IsNullOrEmpty(bh.SGDWZS))
        {
            QY_QYZZXX qyzz = db1.QY_QYZZXX.Where(t => t.QYBM == sgdwid).FirstOrDefault();
            if (qyzz != null)
            {
                b_SGDWZS.Text = qyzz.ZSBH;
            }
        }

        //找到施工单位证书信息
        if (string.IsNullOrEmpty(bh.SGDWAQSCXKZ))
        {
            QY_QYZSXX qyzs = db1.QY_QYZSXX.Where(t => t.QYBM == sgdwid && t.ZSLXBM == "150" ).FirstOrDefault();
            if (qyzs != null)
            {
                b_SGDWAQSCXKZ.Text = qyzs.ZSBH;
            }
        }

        //报送单位信息  SBDWID b_SBDWMC
        if (string.IsNullOrEmpty(bh.SBDWMC))
        {
            b_SBDWMC.Text = Session["FBaseName"].ToString();
            b_SBSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");

        }
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
        TC_BZGD_Record bh = db.TC_BZGD_Record.Where(t => t.FAppId == EConvert.ToString(Session["FAppId"])).FirstOrDefault();
        bh.JSDW = b_JSDW.Text;
        pageTool tool = new pageTool(this.Page,"b_");
        bh = tool.getPageValue(bh);

        db.SubmitChanges();
        //showData();
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
    }

    //人员选择:建造师
    //protected void btnSel_JCS_Click(object sender, EventArgs e)
    //{

    //    string selEmpid = sj_ZCJCS.Value; //注册建造师

    //    EgovaDB1 db1 = new EgovaDB1();
    //    RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYBH == selEmpid).FirstOrDefault();
    //    if (v != null)
    //    {
    //        q_JZS.Text = v.XM;;
    //    }

    //}

    //人员选择：结构师
    //protected void btnSel_JGS_Click(object sender, EventArgs e)
    //{

    //    string selEmpid = sj_ZCJGS.Value; //注册结构师

    //    EgovaDB1 db1 = new EgovaDB1();
    //    RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYBH == selEmpid).FirstOrDefault();
    //    if (v != null)
    //    {
    //        b_JGS.Text = v.XM; ;
    //    }

    //}

    //人员选择：岩土工程师
    //protected void btnSel_YTG_Click(object sender, EventArgs e)
    //{

    //    string selEmpid = kc_YTGCS.Value; //岩土工程师

    //    EgovaDB1 db1 = new EgovaDB1();
    //    RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYBH == selEmpid).FirstOrDefault();
    //    if (v != null)
    //    {
    //        b_YTGCS.Text = v.XM;
    //        b_CCDWZS.Text = v.ZCZSBH;
    //    }

    //}
    //人员选择：项目经理
    protected void btnSel_XMJ_Click(object sender, EventArgs e)
    {

        //string selEmpid = sj_XMJL.Value; //项目经理

        //EgovaDB1 db1 = new EgovaDB1();
        //RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYBH == selEmpid).FirstOrDefault();
        //if (v != null)
        //{
        //    b_XMJL.Text = v.XM; ;
        //}

    }

    //人员选择：项目总监
    protected void btnSel_XMZ_Click(object sender, EventArgs e)
    {

        //string selEmpid = jl_XMZJ.Value; //项目总监

        //EgovaDB1 db1 = new EgovaDB1();
        //RY_RYZSXX v = db1.RY_RYZSXX.Where(t => t.RYBH == selEmpid).FirstOrDefault();
        //if (v != null)
        //{
        //    b_XMZJ.Text = v.XM; ;
        //}

    }

    //protected void btnSel_sj_Click(object sender, EventArgs e)
    //{
    //    string selEntId = sj_FBaseInfoId.Value;
    //    EgovaDB1 db1 = new EgovaDB1();
    //    QY_JBXX v = db1.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
    //    if (v != null)
    //    {
    //        sj_FName.Text = v.QYMC;
    //        sj_FRegistAddress.Text = v.QYXXDZ;
    //        sj_FLinkMan.Text = v.FRDB;
    //        sj_FMobile.Text = v.LXDH;
    //    }
    //    var v1 = db1.QY_QYZSXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
    //    if (v1 != null)
    //        sj_FLicence.Text = v1.ZSBH;
    //    ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr();</script>");
    //}
    //protected void btnSel_kc_Click(object sender, EventArgs e)
    //{
    //    string selEntId = b_KCDWId.Value;
    //    EgovaDB1 db1 = new EgovaDB1();
    //    QY_JBXX v = db1.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
    //    if (v != null)
    //    {
    //        b_CCDW.Text = v.QYMC;
    //        b_CCDWDZ.Text = v.QYXXDZ;
    //        b_CCDWFR.Text = v.FRDB;
    //        b_CCDWDH.Text = v.LXDH;
    //    }
    //    var v1 = db1.QY_QYZSXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
    //    if (v1 != null)
    //        b_CCDWZS.Text = v1.ZSBH;
    //    ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr();</script>");
    //}
    protected void btnSel_sg_Click(object sender, EventArgs e)
    {
        string selEntId = b_SGDWId.Value;
        EgovaDB1 db1 = new EgovaDB1();
        QY_JBXX v = db1.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v != null)
        {
            b_SGDW.Text = v.QYMC;
            //b_SGDWDZ.Text = v.QYXXDZ;
            //b_SGDWFR.Text = v.FRDB;
            //b_SGDWDH.Text = v.LXDH;
        }
        var v1 = db1.QY_QYZSXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v1 != null)
            b_SGDWZS.Text = v1.ZSBH;
        ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr();</script>");
    }
    protected void btnSel_jl_Click(object sender, EventArgs e)
    {
        string selEntId = b_JLDWId.Value;
        EgovaDB1 db1 = new EgovaDB1();
        QY_JBXX v = db1.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v != null)
        {
            b_JLDW.Text = v.QYMC;
            //b_JLDWDZ.Text = v.QYXXDZ;
            //b_JLDWFR.Text = v.FRDB;
            //b_JLDWDH.Text = v.LXDH;
        }
        var v1 = db1.QY_QYZSXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v1 != null)
            //b_JLZS.Text = v1.ZSBH;
        ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr();</script>");
    }
    //添加联合体企业
    //protected void btnAddEnt_sj_Click(object sender, EventArgs e)
    //{
    //    string FID =  sj_other_FBaseInfoId.Value;
    //    var FEntType = 6;//设计
    //    EgovaDB1 db1 = new EgovaDB1();
    //    String FAppID = EConvert.ToString(Session["FAppId"]);
    //    var v = (from t in db1.QY_JBXX
    //             where t.QYBM == FID
    //             select new
    //             {
    //                 t.QYBM,
    //                 t.QYMC,
    //             }).FirstOrDefault();
    //    if (v != null)
    //    {
    //        TC_Prj_Ent ent = db.TC_Prj_Ent.Where(t => t.FBaseInfoId == v.QYBM && t.FAppId == FAppID && t.FEntType == FEntType).FirstOrDefault();
    //        if (ent == null)
    //        {
    //            ent = new TC_Prj_Ent();
    //            db.TC_Prj_Ent.InsertOnSubmit(ent);
    //            ent.FId = Guid.NewGuid().ToString();
    //            ent.FPrjId = (string) ViewState["FPrjID"];
    //            ent.FBaseInfoId = v.QYBM;
    //            ent.FEntType = FEntType;
    //            ent.FAppId = FAppID;
    //            ent.FName = v.QYMC;
    //            ent.FIsDeleted = false;
    //            ent.FTime = DateTime.Now;
    //            ent.FCreateTime = DateTime.Now;

    //            db.SubmitChanges();

    //            //sj_FInt2.Checked = true;//选企业后自动勾上联合体

    //            showOtherEnt(6);
    //        }
    //    }
    //}
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
        //if (ent == null)
        //{
        //    sj_FInt2.Checked = false;
        //}
        //else
        //{
        //    sj_FInt2.Checked = true;
        //}
        //DG_List.DataSource = v;
        //DG_List.DataBind();
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
    protected void b_SJAQWMSGGDJH_TextChanged(object sender, EventArgs e)
    {

    }
}