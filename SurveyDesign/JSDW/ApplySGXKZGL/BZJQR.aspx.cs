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

public partial class JSDW_APPLYSGXKZGL_BZJQR : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hf_FAppId.Value = EConvert.ToString(Session["FAppId"]);  
            BindControl();
            showInfo();
            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool.ExecuteScript("btnEnable();");
            }
        }
    }
    void BindControl()
    {

        //工程类别
        DataTable dt = rc.getDicTbByFNumber("20001");
        t_PrjItemType.DataSource = dt;
        t_PrjItemType.DataTextField = "FName";
        t_PrjItemType.DataValueField = "FNumber";
        t_PrjItemType.DataBind();

    }
    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        TC_SGXKZ_PrjInfo emp = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == hf_FAppId.Value).FirstOrDefault();
        if (emp != null)
        {           
            tool.fillPageControl(emp);
            hf_FId.Value = emp.FId;
        }
        tool = new pageTool(this.Page, "t1_");
        TC_SGXKZ_BZJQR info1 = dbContext.TC_SGXKZ_BZJQR.Where(t => t.FAppId == hf_FAppId.Value && t.JFXMBM=="1").FirstOrDefault();
        if (info1 != null)
        {
            tool.fillPageControl(info1);
        }
        tool = new pageTool(this.Page, "t2_");
        TC_SGXKZ_BZJQR info2 = dbContext.TC_SGXKZ_BZJQR.Where(t => t.FAppId == hf_FAppId.Value && t.JFXMBM == "2").FirstOrDefault();
        if (info2 != null)
        {
            tool.fillPageControl(info2);
        }
        tool = new pageTool(this.Page, "t3_");
        TC_SGXKZ_BZJQR info3 = dbContext.TC_SGXKZ_BZJQR.Where(t => t.FAppId == hf_FAppId.Value && t.JFXMBM == "3").FirstOrDefault();
        if (info3 != null)
        {
            tool.fillPageControl(info3);
        }
        tool = new pageTool(this.Page, "t4_");
        TC_SGXKZ_BZJQR info4 = dbContext.TC_SGXKZ_BZJQR.Where(t => t.FAppId == hf_FAppId.Value && t.JFXMBM == "4").FirstOrDefault();
        if (info4 != null)
        {
            tool.fillPageControl(info4);
        }
        tool = new pageTool(this.Page, "t5_");
        TC_SGXKZ_BZJQR info5 = dbContext.TC_SGXKZ_BZJQR.Where(t => t.FAppId == hf_FAppId.Value && t.JFXMBM == "5").FirstOrDefault();
        if (info5 != null)
        {
            tool.fillPageControl(info5);
        }
        tool = new pageTool(this.Page, "t6_");
        TC_SGXKZ_BZJQR info6 = dbContext.TC_SGXKZ_BZJQR.Where(t => t.FAppId == hf_FAppId.Value && t.JFXMBM == "6").FirstOrDefault();
        if (info6 != null)
        {
            tool.fillPageControl(info6);
        }
        tool = new pageTool(this.Page, "t7_");
        TC_SGXKZ_BZJQR info7 = dbContext.TC_SGXKZ_BZJQR.Where(t => t.FAppId == hf_FAppId.Value && t.JFXMBM == "7").FirstOrDefault();
        if (info7 != null)
        {
            tool.fillPageControl(info7);
        }
        tool = new pageTool(this.Page, "t8_");
        TC_SGXKZ_BZJQR info8 = dbContext.TC_SGXKZ_BZJQR.Where(t => t.FAppId == hf_FAppId.Value && t.JFXMBM == "8").FirstOrDefault();
        if (info8 != null)
        {
            tool.fillPageControl(info8);
        }
        tool = new pageTool(this.Page, "t9_");
        TC_SGXKZ_BZJQR info9 = dbContext.TC_SGXKZ_BZJQR.Where(t => t.FAppId == hf_FAppId.Value && t.JFXMBM == "9").FirstOrDefault();
        if (info9 != null)
        {
            tool.fillPageControl(info9);
        }
        tool = new pageTool(this.Page, "t10_");
        TC_SGXKZ_BZJQR info10 = dbContext.TC_SGXKZ_BZJQR.Where(t => t.FAppId == hf_FAppId.Value && t.JFXMBM == "10").FirstOrDefault();
        if (info10 != null)
        {
            tool.fillPageControl(info10);
        }
    }
    //保存
    private void saveInfo(string flag)
    {
        string fId;
        HiddenField hf_Id = null;
        switch(flag){
            case "1":
                fId = hf_FId1.Value;
                hf_Id = hf_FId1;
                break;
            case "2":
                fId = hf_FId2.Value;
                hf_Id = hf_FId2;
                break;
            case "3":
                fId = hf_FId3.Value;
                hf_Id = hf_FId3;
                break;
            case "4":
                fId = hf_FId4.Value;
                hf_Id = hf_FId4;
                break;
            case "5":
                fId = hf_FId5.Value;
                hf_Id = hf_FId5;
                break;
            case "6":
                fId = hf_FId6.Value;
                hf_Id = hf_FId6;
                break;
            case "7":
                fId = hf_FId7.Value;
                hf_Id = hf_FId7;
                break;
            case "8":
                fId = hf_FId8.Value;
                hf_Id = hf_FId8;
                break;
            case "9":
                fId = hf_FId9.Value;
                hf_Id = hf_FId9;
                break;
            case "10":
                fId = hf_FId10.Value;
                hf_Id = hf_FId10;
                break;
            default:
                fId = "";
                break;
        };
        TC_SGXKZ_BZJQR Emp = new TC_SGXKZ_BZJQR();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_SGXKZ_BZJQR.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FAppId = hf_FAppId.Value;
            dbContext.TC_SGXKZ_BZJQR.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page,"t"+flag+"_");
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        if (hf_Id != null)
        {
            hf_Id.Value = fId;
        }       
    }
    //保存
    private void saveInfo()
    {
        int i = 1;
        while(i<11){
            saveInfo(i.ToString());
            i++;
        }
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');", true);
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}