using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class GFEnt_EntInfo : Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowEntInfo(); showInfo();
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
        }
    }
    public void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        t_YWBM.Value = Session["FAppId"].ToString();
        string sql = "select * from YW_GF_JBQK where YWBM='" + Session["FAppId"] + "'";
        DataTable dt = sh.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);

            t_FUpDeptName.fNumber = dt.Rows[0]["FUpDeptName"].ToString();

            if (t_FListName.Items.FindByValue(dt.Rows[0]["FListName"].ToString()) != null)
                t_FListName.SelectedValue = t_FListName.Items.FindByValue(dt.Rows[0]["FListName"].ToString()).Value;
            if (dt.Rows[0]["FListName"].ToString() == "其他")
            {
                t_FTypeName1.Visible = true; t_FTypeName.Visible = false;
                t_FTypeName1.Text = dt.Rows[0]["FTypeName"].ToString();
            }
            else
            {
                bindTypeName();
                t_FTypeName.Visible = true; t_FTypeName1.Visible = false;
                if (t_FTypeName.Items.FindByValue(dt.Rows[0]["FTypeName"].ToString()) != null)
                    t_FTypeName.SelectedValue = t_FTypeName.Items.FindByValue(dt.Rows[0]["FTypeName"].ToString()).Value;
            }

            if (t_FUpgradeTF.Value == "1")
            {
                rTrue.Checked = false; rFalse.Checked = true;
                trtrue.Attributes.Add("style", "display: none;");
                trfalse.Attributes.Add("style", "display: block;");
            }
            else if (t_FUpgradeTF.Value == "0")
            {
                rTrue.Checked = true; rFalse.Checked = false;
                trfalse.Attributes.Add("style", "display: none;");
                trtrue.Attributes.Add("style", "display: block;");
            }
        }
        pageTool tools = new pageTool(this.Page, "ts_");
        sql = "select * from YW_GF_JS where YWBM='" + Session["FAppId"] + "'";
        dt = sh.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            tools.fillPageControl(dt.Rows[0]);
        }
        sql = string.Format(@"select count(1) from YW_GF_FileList l 
                     inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1000");
        string cou = sh.GetSignValue(sql);
        btnUP.Attributes.Add("value", "文件上传(" + cou + ")");
    }

    public void ShowEntInfo()
    {
        string sql = "select * from CF_Sys_User where FBaseInfoId='" + CurrentEntUser.EntId + "' ";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            t_FName.Text = dt.Rows[0]["FCompany"].ToString();
            t_Linkman.Text = dt.Rows[0]["FLinkMan"].ToString();
            t_LinkmanMobile.Text = dt.Rows[0]["FTel"].ToString();
            t_FBaseInfoId.Value = CurrentEntUser.EntId;
            t_FSystemId.Value = dt.Rows[0]["FSystemId"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "", FTypeName = "";
        if (t_FListName.SelectedValue == "其他") { FTypeName = t_FTypeName1.Text.Trim(); }
        else { FTypeName = t_FTypeName.SelectedValue; }
        if (rTrue.Checked == true)
            t_FUpgradeTF.Value = "0";
        else if (rFalse.Checked == true)
            t_FUpgradeTF.Value = "1";
        if (!string.IsNullOrEmpty(t_FID.Value))
        {
            sql += "update YW_GF_JBQK set GFMC='" + t_GFMC.Text.Trim() + "',FName='" + t_FName.Text.Trim()
                + "',Linkman='" + t_Linkman.Text.Trim() + "',LinkmanMobile='" + t_LinkmanMobile.Text.Trim()
                + "',FListName='" + t_FListName.SelectedValue + "',FTypeName='" + FTypeName
                + "',FUpgradeTF='" + t_FUpgradeTF.Value + "',FOldGFMC='" + t_FOldGFMC.Text.Trim()
                + "',FOldName='" + t_FOldName.Text.Trim() + "',FOldGJJGFPZWH='" + t_FOldGJJGFPZWH.Text.Trim()
                + "',FOldGFBH='" + t_FOldGFBH.Text.Trim() + "',FTime=getdate(),FUpDeptName='" + t_FUpDeptName.fNumber
                + "' where FID='" + t_FID.Value + "' ;";
        }
        else
        {
            t_FID.Value = Guid.NewGuid().ToString();
            sql += string.Format(@"insert YW_GF_JBQK (GFMC,FName,Linkman,LinkmanMobile,FListName,
                    FTypeName,FUpgradeTF,FOldGFMC,FOldName,FOldGJJGFPZWH,FOldGFBH,FID,YWBM
                    ,FBaseInfoId,FSystemId,FCreateTime,FIsDeleted,FTime,FUpDeptName)
                    values 
                    ('" + t_GFMC.Text.Trim() + "','" + t_FName.Text.Trim() + "','" + t_Linkman.Text.Trim()
                        + "','" + t_LinkmanMobile.Text.Trim() + "','" + t_FListName.SelectedValue + "','"
                        + FTypeName + "','" + t_FUpgradeTF.Value + "','" + t_FOldGFMC.Text.Trim()
                        + "','" + t_FOldName.Text.Trim() + "','" + t_FOldGJJGFPZWH.Text.Trim()
                        + "','" + t_FOldGFBH.Text.Trim() + "','" + t_FID.Value + "','" + t_YWBM.Value
                        + "','" + t_FBaseInfoId.Value + "','" + t_FSystemId.Value + "',getdate(),0,getdate(),'" + t_FUpDeptName.fNumber + "') ;");
        }
        if (!string.IsNullOrEmpty(ts_FID.Value))
        {
            sql += " update YW_GF_JS set NRJS='" + ts_NRJS.Text + "' where FID='" + ts_FID.Value + "'";
        }
        else
        {
            sql += string.Format(@" insert YW_GF_JS (FID,NRJS,YWBM,FBaseInfoId,FSystemId,FCreateTime,FIsDeleted,FTime) 
                        values 
                        (newid(),'" + ts_NRJS.Text + "','" + t_YWBM.Value + "','" + t_FBaseInfoId.Value + "','"
                        + t_FSystemId.Value + "',getdate(),0,getdate() )");
        }
        if (sh.PExcute(sql))
        { tool.showMessage("保存成功"); showInfo(); }
        else
        { tool.showMessage("保存失败"); }

    }
    protected void t_FListName_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindTypeName();
    }
    public void bindTypeName()
    {
        if (!string.IsNullOrEmpty(t_FListName.SelectedValue) && t_FListName.SelectedValue != "--请选择--")
        {
            if (t_FListName.SelectedValue == "房屋建筑工程")
            {
                t_FTypeName.Visible = true; t_FTypeName1.Visible = false;
                this.t_FTypeName.Items.Clear();
                this.t_FTypeName.Items.Insert(0, new ListItem("--请选择--", ""));
                this.t_FTypeName.Items.Insert(1, new ListItem("地基与基础", "地基与基础"));
                this.t_FTypeName.Items.Insert(2, new ListItem("主体结构", "主体结构"));
                this.t_FTypeName.Items.Insert(3, new ListItem("钢结构", "钢结构"));
                this.t_FTypeName.Items.Insert(4, new ListItem("装饰与屋面", "装饰与屋面"));
                this.t_FTypeName.Items.Insert(5, new ListItem("水电与智能", "水电与智能"));
                this.t_FTypeName.Items.Insert(6, new ListItem("其他", "其他"));
            }
            else if (t_FListName.SelectedValue == "土木工程")
            {
                t_FTypeName.Visible = true; t_FTypeName1.Visible = false;
                this.t_FTypeName.Items.Clear();
                this.t_FTypeName.Items.Insert(0, new ListItem("--请选择--", ""));
                this.t_FTypeName.Items.Insert(1, new ListItem("公路", "公路"));
                this.t_FTypeName.Items.Insert(2, new ListItem("铁路", "铁路"));
                this.t_FTypeName.Items.Insert(3, new ListItem("隧道", "隧道"));
                this.t_FTypeName.Items.Insert(4, new ListItem("桥梁", "桥梁"));
                this.t_FTypeName.Items.Insert(5, new ListItem("堤坝与电站", "堤坝与电站"));
                this.t_FTypeName.Items.Insert(6, new ListItem("矿山", "矿山"));
                this.t_FTypeName.Items.Insert(7, new ListItem("其他", "其他"));
            }
            else if (t_FListName.SelectedValue == "工业安装工程")
            {
                t_FTypeName.Visible = true; t_FTypeName1.Visible = false;
                this.t_FTypeName.Items.Clear();
                this.t_FTypeName.Items.Insert(0, new ListItem("--请选择--", ""));
                this.t_FTypeName.Items.Insert(1, new ListItem("工业设备", "工业设备"));
                this.t_FTypeName.Items.Insert(2, new ListItem("工业管道", "工业管道"));
                this.t_FTypeName.Items.Insert(3, new ListItem("电气装置与自动化", "电气装置与自动化"));
                this.t_FTypeName.Items.Insert(4, new ListItem("其他", "其他"));
            }
            else if (t_FListName.SelectedValue == "其他")
            {
                this.t_FTypeName.Visible = false; t_FTypeName1.Visible = true;
                t_FTypeName1.Text = null;
            }
        }
    }
    public void readOnly()
    {
        btnSave.Enabled = false; t_GFMC.Enabled = false;
        t_Linkman.Enabled = false; t_LinkmanMobile.Enabled = false;
        t_FListName.Enabled = false; t_FTypeName.Enabled = false;
        t_FTypeName1.Enabled = false; rTrue.Attributes.Add("disabled", "disabled");
        rFalse.Attributes.Add("disabled", "disabled"); t_FOldGFMC.Enabled = false;
        t_FOldName.Enabled = false; t_FOldGJJGFPZWH.Enabled = false;
        t_FOldGFBH.Enabled = false; ts_NRJS.Enabled = false;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}