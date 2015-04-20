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
        t_ZGXL.SelectedValue = "107007";//设置默认值为无
        //职称
        dt = rc.getDicTbByFNumber("5080");
        t_ZC.DataSource = dt;
        t_ZC.DataTextField = "FName";
        t_ZC.DataValueField = "FNumber";
        t_ZC.DataBind();
        t_ZC.SelectedValue = "5085";//设置默认值为其他
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
        dbContext = new EgovaDB();
        string fId = txtFId.Value;
        string zczsbh = t_ZCBH.Text.Trim();

        //判断项目负责人和总监理工程师是否已经存在，如果存在则不能再次添加。
        //如果是新增则直接判断是否已经存在
        if (fId == "")
        {
            string sql = @" select count(*) from TC_PrjItem_Emp  where EmpType='{0}' and FAppId='{1}' and FEntType='{2}'";
            sql = string.Format(sql, t_EmpType.SelectedValue, t_FAppId.Value, t_FEntType.Value);
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
        }
        else//如果是修改,则加上身份证号码进行判断
        {
            string sql = @" select count(*) from TC_PrjItem_Emp  where EmpType='{0}' and FAppId='{1}' and FEntType='{2}'  and FIdCard != '{3}'";
            sql = string.Format(sql, t_EmpType.SelectedValue, t_FAppId.Value, t_FEntType.Value,t_FIdCard.Text.Trim());
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
        }


        if (manualVal == "1")//手工录入
        {
            var countSql = @" select count(*) from [JST_XZSPBaseInfo].dbo.RY_RYZSXX  where SFZH='{0}'";
            countSql = string.Format(countSql, t_FIdCard.Text);
            int count2 = SConvert.ToInt(dbContext.ExecuteQuery<int>(countSql).FirstOrDefault());
            if (count2 > 0)
            {
                ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('归档库已存在该证书，请采用选择方式添加。');window.returnValue='1';", true);
                return;
            }
            //判断当前的注册编号是否已经存在，如果已经存在则提醒操作人员从库中选择
            var countsql2 = @" select count(*) from [JST_XZSPBaseInfo].dbo.RY_RYZSXX  where  ZCZSH = '{0}'";  
            countsql2 = string.Format(countsql2,zczsbh);
            int count3 = SConvert.ToInt(dbContext.ExecuteQuery<int>(countsql2).FirstOrDefault());
            if (count3 > 0)
            {
                ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('注册证书已存在，请采用选择方式添加。');window.returnValue='1';", true);
                return;
            }
        }
        string sql1 = @" select count(*) from TC_PrjItem_Emp  where FIdCard='{0}'  and FAppId='{1}' and FEntType='{2}'";
        sql1 = string.Format(sql1, t_FIdCard.Text, t_FAppId.Value, t_FEntType.Value);
        int count1 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql1).FirstOrDefault());
        //如果存在就修改
        TC_PrjItem_Emp emp;
        pageTool tool;
        if (count1 > 0)
        {
            tool = new pageTool(this.Page);
            if (string.IsNullOrEmpty(fId))
            {
                emp =
                    dbContext.TC_PrjItem_Emp.FirstOrDefault(
                        t =>
                            t.FIdCard == t_FIdCard.Text && t.FAppId == t_FAppId.Value &&
                            t.FEntType == EConvert.ToInt(t_FEntType.Value));
               
            }
            else
            {
                emp = dbContext.TC_PrjItem_Emp.FirstOrDefault(t => t.FId == fId);
            }
            emp = tool.getPageValue(emp);
            //为添加的人员绑定，人员所在的单位，用来确定人员的类型
            if (emp != null) emp.FEntType = EConvert.ToInt(t_FEntType.Value);
            dbContext.SubmitChanges();
            ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功!');window.returnValue='1';", true);
            return;
        }

       

        
        emp = new TC_PrjItem_Emp();
        if (!string.IsNullOrEmpty(fId))
        {
            emp = dbContext.TC_PrjItem_Emp.FirstOrDefault(t => t.FId == fId);
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            emp.FId = fId;
            emp.FEmpId = h_selEmpId.Value;
            emp.FPrjItemId = t_FPrjItemId.Value;
            emp.FAppId = t_FAppId.Value;
            emp.FTime = DateTime.Now;
            emp.FCreateTime = DateTime.Now;
            emp.FEntId = t_FEntId.Value;
            dbContext.TC_PrjItem_Emp.InsertOnSubmit(emp);
        }
        tool = new pageTool(this.Page);
        emp = tool.getPageValue(emp);
        //为添加的人员绑定，人员所在的单位，用来确定人员的类型
        emp.FEntType = EConvert.ToInt(t_FEntType.Value);
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
    private void SelEmp()
    {       
        //MODIF:ytb RY_RYZSXX 人员证书信息表，证书信息ID
        string ryzsxxid = h_selEmpId.Value;
        string selEmpId = string.Empty;
        EgovaDB1 db = new EgovaDB1();

        var firstOrDefault = db.RY_RYZSXX.FirstOrDefault(t => t.RYZSXXID == ryzsxxid);   //传回来的值原来是人员编号RYBH ，已改 by zyd
        if (firstOrDefault != null)
            selEmpId = firstOrDefault.RYBH;

        var v = db.RY_RYJBXX.FirstOrDefault(t => t.RYBH == selEmpId);
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
            var v1 = db.RY_RYZSXX.FirstOrDefault(t => t.RYZSXXID == ryzsxxid);
            if (v1 == null) return;
            //t_ZSBH.Text = string.IsNullOrEmpty(v1.ZCZSBH) ? "" : v1.ZCZSBH;
            t_ZSBH.Text = string.IsNullOrEmpty(v1.ZCZSBH) ? "" : v1.ZCZSBH;
            t_DJ.SelectedValue = string.IsNullOrEmpty(v1.ZSJB) ? this.t_DJ.Items.FindByText("其他").Value : (this.t_DJ.Items.FindByValue(v1.ZSJB) == null ? this.t_DJ.Items.FindByText("其他").Value : this.t_DJ.Items.FindByValue(v1.ZSJB).Value);
            t_ZCBH.Text = v1.ZCZSH;
            t_ZCZY.Text = v1.ZCZY;
            t_ZCRQ.Text = v1.FZSJ.ToString();
        }
    }

    /// <summary>
    /// 检查人员是否已经存在
    /// </summary>
    /// <returns></returns>
    private bool isexist()
    {
        string sql1 = @" select count(*) from TC_PrjItem_Emp  where FIdCard='{0}'  and FAppId='{1}' and FEntType='{2}'";
        sql1 = string.Format(sql1, t_FIdCard.Text, t_FAppId.Value, t_FEntType.Value);
        int count1 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql1).FirstOrDefault());
        if (count1 > 0)
            return true;
        else 
            return false;
    }
    protected void btnAddEmp_Click(object sender, EventArgs e)
    {
        SelEmp();
    }


}