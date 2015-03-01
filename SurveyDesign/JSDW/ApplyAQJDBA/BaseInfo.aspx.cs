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
            govd_FRegistDeptId.fNumber = pj_AddressDept.Value;
            q_AddressDept.Value = pj_AddressDept.Value;
            string t = prj.PrjItemType;
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
        qa.Contracts = pj_Contracts.Text;
        db.SubmitChanges();
      //  showPrjData();
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {

    }
}