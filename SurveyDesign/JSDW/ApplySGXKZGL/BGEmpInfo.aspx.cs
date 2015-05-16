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

public partial class JSDW_ApplySGXKZGL_EmpInfoForBG : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    pageTool tool;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            t_FAppId.Value = EConvert.ToString(Session["FAppId"]);//业务编号
            txtFId.Value = EConvert.ToString(Request["FId"]);//项目参与企业的主键
            t_FEntId.Value = EConvert.ToString(Request["entId"]);//项目参与企业的主键，不是企业编号
            t_qyId.Value = EConvert.ToString(Request["qyId"]);//企业编号
            //t_FPrjId.Value = EConvert.ToString(Request["FPrjId"]);
            t_FPrjItemId.Value = EConvert.ToString(Request["FPrjItemId"]);
            t_Enttype.Value = EConvert.ToString(Request["enttype"]);
            BindControl();
            showInfo();
            tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool.ExecuteScript("btnEnable();");
                rglr.Visible = false;
            }
        }        
    }
    void BindControl()
    {

        //人员类型
        //DataTable dt = rc.getDicTbByFNumber("112202");
        //t_EmpType.DataSource = dt;
        //t_EmpType.DataTextField = "FName";
        //t_EmpType.DataValueField = "FNumber";
        //t_EmpType.DataBind();

        //人员类型
        DataTable dt = rc.getDicTbByFNumber("112202");
        DataTable dtNew = dt.Clone();
        DataRow[] drArr;
        drArr = dt.Select();
        switch (t_Enttype.Value)
        {
            //只有施工总承包企业有项目负责人
            //施工总承包单位
            case "2":
                drArr = dt.Select("FNumber in ('11220201','11220202','11220203','11220204','11220205','11220206','11220207','11220208','11220213')");
                break;
            //专业承包单位
            case "3":
                drArr = dt.Select("FNumber in ('11220202','11220203','11220204','11220205','11220206','11220207','11220208','11220213')");
                break;
            //劳动分包
            case "4":
                drArr = dt.Select("FNumber in ('11220202','11220203','11220204','11220205','11220206','11220207','11220208','11220213')");
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
        dt = rc.GetTable("select *  from  (select FName,FNumber  from Standard_Dic.dbo.[CF_Dic_Person]  where  FType = 'zcrylx' union select '无' FName,'-1' FNumber) a order by  a.FNumber");
        t_DJ.DataSource = dt;
        t_DJ.DataTextField = "FName";
        t_DJ.DataValueField = "FNumber";
        t_DJ.DataBind();
        t_DJ.Items.FindByValue("-1").Selected = true;

    }
    private string getEmpType(string id)
    {
        switch (id)
        {
            default:
                return "项目负责人";
            case "1":
                return "项目负责人";
            case "2":
                return "项目技术负责人";
            case "3":
                return "安全负责人";
            case "4":
                return "施工员";
            case "5":
                return "质量员";
            case "6":
                return "安全员";
            case "7":
                return "材料员";
            case "8":
                return "预算员";
            case "9":
                return "总监理工程师";
            case "10":
                return "专业监理工程师";
            case "11":
                return "监理员";
            case "12":
                return "其他";
            case "13": return "建造师";
        }
    }
    //显示
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        if (!string.IsNullOrEmpty(txtFId.Value))
        {
            TC_PrjItem_Emp emp = dbContext.TC_PrjItem_Emp.Where(t => t.FId == txtFId.Value).FirstOrDefault();
            if (emp != null)
            {
                pageTool tool = new pageTool(this.Page, "t_");
                tool.fillPageControl(emp);
            }
        }

    }
    //保存
    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        var manualVal = t_IsManual.Value;
        string fId = txtFId.Value;
        string zczsbh = t_ZCBH.Text.Trim();     
        dbContext = new EgovaDB();
        TC_PrjItem_Emp Emp = new TC_PrjItem_Emp();
        //如果不是新增
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_PrjItem_Emp.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            string sql = @" select count(*) from TC_PrjItem_Emp
                            where EmpType='11220201'
                            and FAppId='{0}' and FEntId = '{1}'";
            sql = string.Format(sql, t_FAppId.Value, t_qyId.Value);
            int count = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql).FirstOrDefault());
            dbContext = new EgovaDB();
            string sql1 = @" select count(*) from TC_PrjItem_Emp
                            where FIdCard='{0}'
                            and FAppId='{1}' and FEntId = '{2}'";
            sql1 = string.Format(sql1, t_FIdCard.Text, t_FAppId.Value, t_qyId.Value);
            int count1 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql1).FirstOrDefault());
            dbContext = new EgovaDB();
            string sql2 = @" select count(*) from TC_PrjItem_Emp
                            where EmpType='11220209'
                            and FAppId='{0}' and FEntId = '{1}'";
            sql2 = string.Format(sql2, t_FAppId.Value, t_qyId.Value);
            int count2 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql2).FirstOrDefault());
            if (t_EmpType.SelectedValue == "11220201" && count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('项目负责人只能添加一位');window.returnValue='1';", true);
                return;
            }
            else if (count1 > 0)
            {
                ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('人员不允许重复添加');window.returnValue='1';", true);
                return;
            }
            else if (count2 > 0)
            {
                ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('总监理工程师只能添加一位');window.returnValue='1';", true);
                return;
            }

            //手工录入
            #region
            if (manualVal == "1")//手工录入
            {
                //如果是人工加的，则new一个ID，仅为保证后续业务的延续性：人员变动记录查重，无他意。  by zyd  
                h_selEmpId.Value = Guid.NewGuid().ToString();

                var countSql = @" select count(*) from [JST_XZSPBaseInfo].dbo.RY_RYZSXX  where SFZH='{0}'";
                countSql = string.Format(countSql, t_FIdCard.Text);
                int count5 = SConvert.ToInt(dbContext.ExecuteQuery<int>(countSql).FirstOrDefault());
                if (count5 > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('归档库已存在该证书，请采用选择方式添加。');window.returnValue='1';", true);
                    return;
                }
                //判断当前的注册编号是否已经存在，如果已经存在则提醒操作人员从库中选择
                var countsql2 = @" select count(*) from [JST_XZSPBaseInfo].dbo.RY_RYZSXX  where  ZCZSH = '{0}'";
                countsql2 = string.Format(countsql2, zczsbh);
                int count3 = SConvert.ToInt(dbContext.ExecuteQuery<int>(countsql2).FirstOrDefault());
                if (count3 > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('注册证书已存在，请采用选择方式添加。');window.returnValue='1';", true);
                    return;
                }
            }

            string sql11 = @" select count(*) from TC_PrjItem_Emp  where FIdCard='{0}'  and FAppId='{1}' and FEntType='{2}'";
            sql11 = string.Format(sql11, t_FIdCard.Text, t_FAppId.Value, t_Enttype.Value);
            int count6 = SConvert.ToInt(dbContext.ExecuteQuery<int>(sql11).FirstOrDefault());
            //如果存在就修改
            TC_PrjItem_Emp emp;
            if (count6 > 0)
            {
                tool = new pageTool(this.Page);
                if (string.IsNullOrEmpty(fId))
                {
                    emp =
                        dbContext.TC_PrjItem_Emp.FirstOrDefault(
                            t =>
                                t.FIdCard == t_FIdCard.Text && t.FAppId == t_FAppId.Value &&
                                t.FEntType == EConvert.ToInt(t_Enttype.Value));

                }
                else
                {
                    emp = dbContext.TC_PrjItem_Emp.FirstOrDefault(t => t.FId == fId);
                }
                emp = tool.getPageValue(emp);
                //为添加的人员绑定，人员所在的单位，用来确定人员的类型
                if (emp != null) emp.FEntType = EConvert.ToInt(t_Enttype.Value);
                dbContext.SubmitChanges();
                ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功!');window.returnValue='1';", true);
                return;
            }
 
            #endregion

            //非人工录入 或人工录入，在库中找不到对应的人。
            fId = Guid.NewGuid().ToString();
            Emp = tool.getPageValue(Emp);
            Emp.FId = fId;
            Emp.FEmpId = h_selEmpId.Value;
            Emp.FPrjItemId = t_FPrjItemId.Value;
            Emp.FAppId = t_FAppId.Value;
            Emp.FTime = DateTime.Now;
            Emp.FCreateTime = DateTime.Now;
            Emp.FEntId = t_qyId.Value;
            Emp.FLinkId = t_FEntId.Value;
            Emp.FEntType = Convert.ToInt16(t_Enttype.Value);
            dbContext.TC_PrjItem_Emp.InsertOnSubmit(Emp);
            dbContext.SubmitChanges();
    
        }
        txtFId.Value = fId;
        updateRYBG(fId);       
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);

        //     MyPageTool.showMessageAjax("保存成功ii", up_Main);
        //    MyPageTool.showMessageAndRunFunctionAjax("保存成功", "window.returnValue='1';", up_Main);
    }
    private void updateRYBG(string FId)
    {
        EgovaDB dbContext = new EgovaDB();
        TC_PrjItem_Emp ent = dbContext.TC_PrjItem_Emp.Where(t => t.FId == FId).FirstOrDefault();
        TC_SGXKZ_RYBGJG sr = new TC_SGXKZ_RYBGJG();

        sr.FId = Guid.NewGuid().ToString();
        sr.FAppId = t_FAppId.Value;
        sr.FPrjItemId = t_FPrjItemId.Value;
        sr.RYLX = getEmpType(t_EmpType.SelectedValue);
        sr.XM = ent.FHumanName;
        sr.ZSBH = ent.ZSBH;
        sr.QYMC = ent.FEntName;
        sr.BGQK = "增加";
        sr.BGTime = DateTime.Now;
        sr.fenttype = EConvert.ToInt(t_Enttype.Value);//企业类型      
        sr.FLinkId = ent.FEmpId;
        dbContext.TC_SGXKZ_RYBGJG.InsertOnSubmit(sr);
        dbContext.SubmitChanges();
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
          if (v != null)
        {
            t_FHumanName.Text = v.XM;
            t_FIdCard.Text = v.SFZH;
            if (v.XB == "女")
            {
                t_FSex.SelectedValue = "1";
            }
            else
            {
                t_FSex.SelectedValue = "0";
            }
            t_FMobile.Text = v.GRDH;
            if (v.ZC != null && v.ZC != "")
            {
                if (t_ZC.SelectedValue.Contains(v.ZC))
                {
                    t_ZC.SelectedValue = v.ZC;
                }
                
            }
           t_FTel.Text = v.BGDH;
            t_ZY.Text = v.SXZY;
            var v1 = db.RY_RYZSXX.Where(t => t.RYBH == selEmpId).FirstOrDefault();
            if (v1 != null)
            {
                t_ZSBH.Text = string.IsNullOrEmpty(v1.ZCZSBH) ? v1.ZCZSH : v1.ZCZSBH;               
                t_ZCBH.Text = v1.ZCZSH;
                t_ZCZY.Text = v1.ZCZY;
                t_ZCRQ.Text = v1.FZSJ.ToString();
                if (t_DJ.Items.Contains(new ListItem(v1.ZSLX.ToString())))
                {
                    t_DJ.SelectedValue = v1.ZSLX.ToString();
                }
            }

        }
    }
    protected void btnAddEmp_Click(object sender, EventArgs e)
    {
        selEmp();
    }
}