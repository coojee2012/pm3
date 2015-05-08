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

public partial class JSDW_APPLYSGXKZGL_PrjItemDesc : System.Web.UI.Page
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

        //立项级别
        dt = rc.getDicTbByFNumber("112204");
        t_ProjectLevel.DataSource = dt;
        t_ProjectLevel.DataTextField = "FName";
        t_ProjectLevel.DataValueField = "FNumber";
        t_ProjectLevel.DataBind();

        //建筑性质
        dt = rc.getDicTbByFNumber("20005");
        t_BuildType.DataSource = dt;
        t_BuildType.DataTextField = "FName";
        t_BuildType.DataValueField = "FNumber";
        t_BuildType.DataBind();

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
        TC_SGXKZ_PrjInfo emp = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == hf_FAppId.Value).FirstOrDefault();
        if (emp != null)
        {
            TC_PrjItem_Info pi = dbContext.TC_PrjItem_Info.Where(t => t.FId == emp.FPrjItemId).FirstOrDefault();
            TC_Prj_Info p = dbContext.TC_Prj_Info.Where(t => t.FId == pi.FPrjId).FirstOrDefault();
            CF_App_List a = dbContext.CF_App_List.Where(t => t.FId == hf_FAppId.Value).FirstOrDefault();
            if (p != null)
            {
                //工程用途
                if (p.ProjectType == "2000101")
                {
                    DataTable dt = rc.getDicTbByFNumber("2000101");
                    t_ProjectUse.DataSource = dt;
                    t_ProjectUse.DataTextField = "FName";
                    t_ProjectUse.DataValueField = "FNumber";
                    t_ProjectUse.DataBind();
                }
                else if (p.ProjectType == "2000102")
                {
                    DataTable dt = rc.getDicTbByFNumber("2000102");
                    t_ProjectUse.DataSource = dt;
                    t_ProjectUse.DataTextField = "FName";
                    t_ProjectUse.DataValueField = "FNumber";
                    t_ProjectUse.DataBind();
                }
                else if (p.ProjectType == "2000103")
                {
                    DataTable dt = rc.getDicTbByFNumber("2000102");
                    t_ProjectUse.DataSource = dt;
                    t_ProjectUse.DataTextField = "FName";
                    t_ProjectUse.DataValueField = "FNumber";
                    t_ProjectUse.DataBind();
                    //t_ProjectUse.Items.Clear();
                }

                pageTool tool = new pageTool(this.Page, "t_");
                tool.fillPageControl(emp);
                //从
                JSDW_DeptID.fNumber = emp.JSDWAddressDept;
                PrjGovdeptid.fNumber = p.AddressDept;
                t_PrjItemType.Text = pi.PrjItemType;
                t_ProjectLevel.Text = p.ProjectLevel;
                t_ProjectNumber.Text = p.ProjectNumber;
                t_ProjectUse.Text = p.ProjectUse;
                t_BuildType.Text = p.ConstrType;
                t_ConstrType.Text = pi.ConstrType;
                //
                if (!string.IsNullOrEmpty(emp.LXDH))
                {
                    t_LXDH.Text = emp.LXDH;
                }
                else
                {
                    t_LXDH.Text = p.Mobile;
                }
                if (!string.IsNullOrEmpty(emp.JSDWDZ))
                {
                    t_JSDWDZ.Text = emp.JSDWDZ;
                }
                else
                {
                    t_JSDWDZ.Text = p.JSDWDZ;
                }
                if (!string.IsNullOrEmpty(emp.FDDBR))
                {
                    t_FDDBR.Text = emp.FDDBR;
                }
                else
                {
                    t_FDDBR.Text = p.JSDWFR;
                }
                if (!string.IsNullOrEmpty(emp.Address))
                {
                    t_Address.Text = emp.Address;
                }
                else
                {
                    t_Address.Text = p.Address;
                }


                if (!string.IsNullOrEmpty(emp.ConstrScale))
                {
                    t_ConstrScale.Text = emp.ConstrScale;
                }
                else
                {
                    t_ConstrScale.Text = p.ConstrScale;
                }
                //业务已经办结且通过，报建时间等于申报时间
                if (a.FState != null && a.FState == 6 && !string.IsNullOrEmpty(a.FResult) && a.FResult == "1")
                {
                    t_ProjectTime.Text = emp.ReportTime.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    if (emp.ProjectTime.HasValue)//项目日期有数据才显示
                    {
                        t_ProjectTime.Text = emp.ProjectTime.Value.ToString("yyyy-MM-dd");
                    }
                }
                t_ProjectNo.Value = p.ProjectNo;
                t_PrjItemId.Value = pi.FId;
                t_Price.Text = pi.Cost.ToString();
                t_Area.Text = p.Area.ToString();
                t_Cost.Text = p.Investment.ToString();
                hf_FId.Value = emp.FId;
            }
        }
    }
    //保存
    private void saveInfo()
    {
        string fId = hf_FId.Value;
        t_JSDWAddressDept.Value = JSDW_DeptID.fNumber;
        t_PrjAddressDept.Value = PrjGovdeptid.fNumber;
        TC_SGXKZ_PrjInfo Emp = new TC_SGXKZ_PrjInfo();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FAppId = hf_FAppId.Value;
            dbContext.TC_SGXKZ_PrjInfo.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        hf_FId.Value = fId;
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');", true);
    //     MyPageTool.showMessageAjax("保存成功ii", up_Main);
    //    MyPageTool.showMessageAndRunFunctionAjax("保存成功", "window.returnValue='1';", up_Main);
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}