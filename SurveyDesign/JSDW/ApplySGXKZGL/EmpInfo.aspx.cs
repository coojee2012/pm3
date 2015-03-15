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

public partial class JSDW_APPLYSGXKZGL_EmpInfo : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            t_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            txtFId.Value = EConvert.ToString(Request["FId"]);
            t_FEntId.Value = EConvert.ToString(Request["FEntId"]);
            t_FEntType.Value = EConvert.ToString(Request["t_FEntType"]);
            t_FPrjItemId.Value = EConvert.ToString(Request["FPrjItemId"]);
            BindControl();
            showInfo();
        }
    }
    void BindControl()
    {
        //人员类型
        DataTable dt = rc.getDicTbByFNumber("112202");
        DataTable dtNew = dt.Clone();
        DataRow[] drArr;
        drArr = dt.Select();
        switch (t_FEntType.Value)
        {
            //设计单位
            case "2":
                drArr = dt.Select("FNumber in ('11220201','11220202','11220203','11220204','11220205','11220206','11220207','11220208')");
                break;
            //专业承包单位
            case "3":
                drArr = dt.Select("FNumber in ('11220201','11220202','11220203','11220204','11220205','11220206','11220207','11220208')");
                break;
            //劳动分包
            case "4":
                drArr = dt.Select("FNumber in ('11220201','11220202','11220203','11220204','11220205','11220206','11220207','11220208')");
                break;
            //勘察单位
            case "5":
                drArr = dt.Select("FNumber in ('11220201','11220202','11220212')");
                break;
            //设计单位
            case "6":
                drArr = dt.Select("FNumber in ('11220201','11220202','11220212')");
                break;
            //监理单位
            case "7":
                drArr = dt.Select("FNumber in ('11220209','11220210','11220211')");
                break;
        }
        for (int i = 0; i < drArr.Length; i++)
        {
            dtNew.ImportRow(drArr[i]);
        }
        t_EmpType.DataSource = dtNew;
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
        dt = rc.getDicTbByFNumber("920");
        t_DJ.DataSource = dt;
        t_DJ.DataTextField = "FName";
        t_DJ.DataValueField = "FNumber";
        t_DJ.DataBind();
    }
    //显示
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_PrjItem_Emp emp = dbContext.TC_PrjItem_Emp.Where(t => t.FId == txtFId.Value).FirstOrDefault();
        if (emp != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(emp);
        }
    }
    //保存
    private void saveInfo()
    {
        var manualVal = t_IsManual.Value;
        if (manualVal == "1")//手工录入
        {
            var countSQL = @" select count(*) from [JST_XZSPBaseInfo].dbo.RY_RYZSXX  where SFZH='{0}'";
            countSQL = string.Format(countSQL, t_FIdCard.Text);
            int count2 = SConvert.ToInt(dbContext.ExecuteQuery<int>(countSQL).FirstOrDefault());
            if (count2 > 0)
            {
                ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('归档库已存在该证书，请采用选择方式添加。');window.returnValue='1';", true);
                return;
            }
        }
        string sql1 = @" select count(*) from TC_PrjItem_Emp  where FIdCard='{0}'";
        sql1 = string.Format(sql1, t_FIdCard.Text);
        int count1 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql1).FirstOrDefault());
        if (count1 > 0)
        {
            ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('人员不允许重复添加');window.returnValue='1';", true);
            return;
        }

        string sql = @" select count(*) from TC_PrjItem_Emp  where EmpType='{0}' and FAppId='{1}'";
        sql = string.Format(sql, t_EmpType.SelectedValue, t_FAppId.Value);
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

        dbContext = new EgovaDB();
        string fId = txtFId.Value;
        TC_PrjItem_Emp Emp = new TC_PrjItem_Emp();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_PrjItem_Emp.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FEmpId = h_selEmpId.Value;
            Emp.FPrjItemId = t_FPrjItemId.Value;
            Emp.FAppId = t_FAppId.Value;
            Emp.FTime = DateTime.Now;
            Emp.FCreateTime = DateTime.Now;
            Emp.FEntId = t_FEntId.Value;
            dbContext.TC_PrjItem_Emp.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        txtFId.Value = fId;
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);

    }
    public void Alert(string str_Message)
    {
        ClientScriptManager scriptManager = ((Page)System.Web.HttpContext.Current.Handler).ClientScript;
        scriptManager.RegisterStartupScript(typeof(string), "", "alert('" + str_Message + "');", true);
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
            t_ZC.SelectedValue = ((v.ZC == null) ? this.t_ZC.Items.FindByText("其他").Value : (this.t_ZC.Items.FindByText(v.ZC) == null ? this.t_ZC.Items.FindByText("其他").Value : this.t_ZC.Items.FindByText(v.ZC).Value));
            t_ZW.Text = v.ZW;
            t_FTel.Text = v.BGDH;
            t_ZGXL.SelectedValue = this.t_ZGXL.Items.FindByText("无").Value;
            t_ZY.Text = v.SXZY;
            var v1 = db.RY_RYZSXX.Where(t => t.RYBH == selEmpId).FirstOrDefault();
            if (v1 != null)
            {
                t_ZSBH.Text = string.IsNullOrEmpty(v1.ZCZSBH) ? "" : v1.ZCZSBH;

                t_DJ.SelectedValue = string.IsNullOrEmpty(v1.ZSJB) ? this.t_DJ.Items.FindByText("其他").Value : (this.t_DJ.Items.FindByValue(v1.ZSJB) == null ? this.t_DJ.Items.FindByText("其他").Value : this.t_DJ.Items.FindByValue(v1.ZSJB).Value);
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