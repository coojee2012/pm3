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

public partial class JSDW_APPLYBHGD_BHProjectItem : System.Web.UI.Page
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

        //结构类型
        DataTable dt = rc.getDicTbByFNumber("509");
        t_ConstrType.DataSource = dt;
        t_ConstrType.DataTextField = "FName";
        t_ConstrType.DataValueField = "FNumber";
        t_ConstrType.DataBind();
        t_ConstrType.Items.Insert(0, new ListItem("--请选择--", ""));

        //工程类别
        dt = rc.getDicTbByFNumber("20001");
        t_PrjItemType.DataSource = dt;
        t_PrjItemType.DataTextField = "FName";
        t_PrjItemType.DataValueField = "FNumber";
        t_PrjItemType.DataBind();


        //币种
        dt = rc.getDicTbByFNumber("112211");
        t_Currency.DataSource = dt;
        t_Currency.DataTextField = "FName";
        t_Currency.DataValueField = "FNumber";
        t_Currency.DataBind();

        //所有制
        dt = rc.getDicTbByFNumber("112212");
        t_JSDWXZ.DataSource = dt;
        t_JSDWXZ.DataTextField = "FName";
        t_JSDWXZ.DataValueField = "FNumber";
        t_JSDWXZ.DataBind();
    }
    //显示
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_BZGD_PrjInfo emp = dbContext.TC_BZGD_PrjInfo.Where(t => t.FAppId == hf_FAppId.Value).FirstOrDefault();
        if (emp != null)
        {
            pageTool tool = new pageTool(this.Page,"t_");
            tool.fillPageControl(emp);
            JSDW_DeptID.fNumber = emp.JSDWAddressDept;
            hf_FId.Value = emp.FId;
        }
        else
        {
            //标化工地项目信息没有，则取TC_BZGD_Record中的相关数据
            TC_BZGD_Record bh = dbContext.TC_BZGD_Record.Where(t => t.FAppId == hf_FAppId.Value).FirstOrDefault();

            v_PrjItem_Info pi = dbContext.v_PrjItem_Info.Where(t => t.FId == bh.FPrjItemId).FirstOrDefault();
            v_prj_Info p = dbContext.v_prj_Info.Where(t => t.FId == bh.FPrjId).FirstOrDefault();
            CF_App_List a = dbContext.CF_App_List.Where(t => t.FPrjId == bh.FPrjId && (t.FManageTypeId == 8080)).FirstOrDefault();

            t_PrjId.Value = bh.FPrjId;
            t_PrjItemId.Value = bh.FPrjItemId;

            PrjGovdeptid.fNumber = p.AddressDept;
            t_JSDW.Text = p.JSDW;
            t_ProjectName.Text = p.ProjectName;
            t_PrjItemName.Text = pi.PrjItemName;
            t_PrjItemType.Text = pi.PrjItemType;
            t_ConstrType.Text = pi.ConstrType;

            t_LXDH.Text = p.Mobile;
            t_JSDWDZ.Text = p.JSDWDZ;
            t_FDDBR.Text = p.JSDWFR;
            t_Address.Text = p.Address;
            t_ConstrScale.Text = p.ConstrScale;
            //业务已经办结且通过，报建时间等于申报时间
            if (a != null)
            {
                if (a.FState != null && a.FState == 6 && !string.IsNullOrEmpty(a.FResult) && a.FResult == "1")
                {
                    t_ProjectTime.Text = a.FReportDate.Value.ToString("yyyy-MM-dd");
                }
            }
            t_ReportTime.Text = DateTime.Now.ToString("yyyy-MM-dd");

            t_StartDate.Text = p.StartDate.ToString();
            t_EndDate.Text = p.EndDate.ToString(); ;
            t_PrjItemId.Value = pi.FId;
            t_Price.Text = pi.Cost.ToString();
        }

    }
    //保存
    private void saveInfo()
    {
        string fId = hf_FId.Value;
        t_JSDWAddressDept.Value = JSDW_DeptID.fNumber;
        t_PrjAddressDept.Value = PrjGovdeptid.fNumber;
        TC_BZGD_PrjInfo Emp = new TC_BZGD_PrjInfo();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_BZGD_PrjInfo.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.PrjItemId = t_PrjItemId.Value;
            Emp.FAppId = hf_FAppId.Value; 
            dbContext.TC_BZGD_PrjInfo.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        hf_FId.Value = fId;
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');", true);
    //     MyPageTool.showMessageAjax("保存成功ii", up_Main);
        //MyPageTool.showMessageAndRunFunctionAjax("保存成功", "window.returnValue='1';", up_Main);
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}