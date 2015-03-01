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
            sj_FName.Text = qa.SJDW;
            sj_FRegistAddress.Text = qa.SJDWDZ;
            sj_FLinkMan.Text = qa.SJDWFR;
            sj_FMobile.Text = qa.SJDWDH;
            sj_FLicence.Text = qa.SJDWZS;
            govd_FRegistDeptId.fNumber = pj_AddressDept.Value;
            q_AddressDept.Value = pj_AddressDept.Value;
            string t = prj.PrjItemType;
            tool = new pageTool(this.Page);
            ClientScript.RegisterStartupScript(this.GetType(), "showTr", "<script>showTr();</script>");
        }
        showOtherEnt();
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
        pageTool tool = new pageTool(this.Page,"p_");
        qa = tool.getPageValue(qa);
        tool = new pageTool(this.Page, "pj_");
        qa = tool.getPageValue(qa);
        tool = new pageTool(this.Page, "q_");
        qa = tool.getPageValue(qa);
        qa.Contracts = pj_Contacts.Text;
        db.SubmitChanges();
        showPrjData();
        //ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
    }
    protected void btnSel_sj_Click(object sender, EventArgs e)
    {
        string FID = sj_FBaseInfoId.Value;
        CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == FID).FirstOrDefault();
        CF_Ent_QualiCerti qc = db.CF_Ent_QualiCerti.Where(t => t.FBaseInfoId == FID).FirstOrDefault();
        if (ent != null)
        {
            sj_FName.Text = ent.FName;
            sj_FRegistAddress.Text = ent.FRegistAddress;
            sj_FLinkMan.Text = ent.FLinkMan;
            sj_FLicence.Text = qc.FCertiNo;
            sj_FMobile.Text = ent.FMobile;
            ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr();</script>");
        }
    }
    //添加联合体企业
    protected void btnAddEnt_Click(object sender, EventArgs e)
    {
        string FID = sj_other_FBaseInfoId.Value;
        int FEntType = 1553;
        String FAppID = EConvert.ToString(Session["FAppId"]);
        var v = (from t in db.CF_Ent_BaseInfo
                 where t.FId == FID
                 select new
                 {
                     t.FId,
                     t.FName,
                 }).FirstOrDefault();
        if (v != null)
        {
            TC_Prj_Ent ent = db.TC_Prj_Ent.Where(t => t.FBaseInfoId == v.FId && t.FAppId == FAppID && t.FEntType == FEntType).FirstOrDefault();
            if (ent == null)
            {
                ent = new TC_Prj_Ent();
                db.TC_Prj_Ent.InsertOnSubmit(ent);
                ent.FId = Guid.NewGuid().ToString();
                ent.FPrjId = (string) ViewState["FPrjID"];
                ent.FBaseInfoId = v.FId;
                ent.FEntType = FEntType;
                ent.FAppId = FAppID;
                ent.FName = v.FName;
                ent.FIsDeleted = false;
                ent.FTime = DateTime.Now;
                ent.FCreateTime = DateTime.Now;

                db.SubmitChanges();

                sj_FInt2.Checked = true;//选企业后自动勾上联合体

                showOtherEnt();
            }
        }
    }
    //联合体企业，只有手添的合同显示，自动的合同直接从合同备案业务取
    private void showOtherEnt()
    {
        EgovaDB db = new EgovaDB();
        string FAppId = EConvert.ToString(Session["FAppId"]);
        int FEntType = 1553;
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
                showOtherEnt();
            }
        }
    }
}