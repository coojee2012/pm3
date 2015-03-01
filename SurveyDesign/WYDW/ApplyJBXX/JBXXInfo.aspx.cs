using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Microsoft.SqlServer.Server;
using Tools;

public partial class WYDW_ApplyBaseInfo_Info : WYPage
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckSession();
        checkGD();
        if (!IsPostBack)
        {
            BindControl();
            showInfo();
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveData();
    }
    void BindControl()
    {
        DataTable dt2 = rc.getDicTbByFNumber("2000101");
        t_XMZLX.DataSource = dt2;
        t_XMZLX.DataTextField = "FName";
        t_XMZLX.DataValueField = "FNumber";
        t_XMZLX.DataBind();

        DataTable dt3 = rc.getDicTbByFNumber("20005");
        t_JSXZ.DataSource = dt3;
        t_JSXZ.DataTextField = "FName";
        t_JSXZ.DataValueField = "FNumber";
        t_JSXZ.DataBind();

        DataTable dt4 = rc.getDicTbByFNumber("20009");
        t_JSMS.DataSource = dt4;
        t_JSMS.DataTextField = "FName";
        t_JSMS.DataValueField = "FNumber";
        t_JSMS.DataBind();

    }

    private void showInfo()
    {
        string sql = "select * from YW_WY_XM_JBXX where FAppID='" + (string)Session["FAppId"] + "' ";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            t_ProjectName.Text = dt.Rows[0]["XMMC"].ToString();
            t_JSDW.Text = dt.Rows[0]["JSDW"].ToString();
            if (dt.Rows[0]["JSDW"].ToString() != "")
            {
                t_JSDW.Enabled = false;
            }
            govd_FRegistDeptId.fNumber = dt.Rows[0]["XMSD"].ToString();
            t_Address.Text = dt.Rows[0]["XMDZ"].ToString();
            t_JSDWDM.Text = dt.Rows[0]["JSDWZZJFDM"].ToString();
            if (dt.Rows[0]["JSDWZZJFDM"].ToString() != "")
            {
                t_JSDWDM.Enabled = false;
            }
            t_JSDWDZ.Text = dt.Rows[0]["JSDWDZ"].ToString();
            if (dt.Rows[0]["JSDWDZ"].ToString() != "")
            {
                t_JSDWDZ.Enabled = false;
            }
            t_Contacts.Text = dt.Rows[0]["JSDWFR"].ToString();
            t_Mobile.Text = dt.Rows[0]["JSDWFRDH"].ToString();
            t_JSDWJSFZR.Text = dt.Rows[0]["JSDWJSFZR"].ToString();
            t_JSDWJSFZRZC.Text = dt.Rows[0]["JSDWJSFZRZC"].ToString();
            t_JSDWJSFZRDH.Text = dt.Rows[0]["JSDWJSFZRDH"].ToString();
            //t_XMLX.SelectedValue = dt.Rows[0]["XMLX"].ToString();
            t_XMZLX.SelectedValue = dt.Rows[0]["XMZLX"].ToString();
            t_JSXZ.SelectedValue = dt.Rows[0]["JSXZ"].ToString();
            t_JSMS.SelectedValue = dt.Rows[0]["JSMS"].ToString();
            t_XMZTZ.Text = dt.Rows[0]["XMZTZ"].ToString();
            t_JSGM.Text = dt.Rows[0]["JSGM"].ToString();
            t_JSNR.Text = dt.Rows[0]["JSNR"].ToString();

        }
        else
        {
            this.RegisterStartupScript(new Guid().ToString(), "<script>alert('数据加载失败！');parent.location.href='../main/Index.aspx';</script>");
        }
    }

    private void saveData()
    {
        string fappid = "";
        decimal xmztz = 0;
        Approve.Common.pageTool tool = new Approve.Common.pageTool(this.Page);

        if (govd_FRegistDeptId.fNumber.Length < 6)
        {
            tool.showMessage("项目地区必须精确到区县");
            return;
        }


        if (t_XMZTZ.Text.Trim() != "")
        {
            xmztz = decimal.Parse(t_XMZTZ.Text.Trim());
        }
        fappid = (string)Session["FAppId"];
        string strsql = "Update YW_WY_XM_JBXX set XMSD='" + govd_FRegistDeptId.fNumber + "',XMDZ='" +
                        t_Address.Text.Trim() + "'" +
                        ",JSDW='" + t_JSDW.Text.Trim() + "',JSDWZZJFDM='" + t_JSDWDM.Text.Trim() + "',JSDWDZ='" +
                        t_JSDWDZ.Text.Trim() + "'" +
                        ",JSDWFR='" + t_Contacts.Text.Trim() + "',JSDWFRDH='" + t_Mobile.Text.Trim() +
                        "',JSDWJSFZR='" + t_JSDWJSFZR.Text.Trim() + "'" +
                        ",JSDWJSFZRZC='" + t_JSDWJSFZRZC.Text.Trim() + "',JSDWJSFZRDH='" + t_JSDWJSFZRDH.Text.Trim() +
                        "',XMLX='20001'" +
                        ",XMZLX='" + t_XMZLX.SelectedValue + "',JSXZ='" + t_JSXZ.SelectedValue + "',JSMS='" +
                        t_JSMS.SelectedValue + "',XMZTZ='" + xmztz + "'" +
                        ",JSGM='" + t_JSGM.Text.Trim() + "',JSNR='" + t_JSNR.Text.Trim() + "'" +
                        " where FAppID='" + fappid + "'";

        if (rc.PExcute(strsql))
        { tool.showMessage("保存成功！"); }
        else
        { tool.showMessage("保存失败！"); }

    }

    private void readOnly()
    {
        btnSave.Enabled = false;
    }

    private void checkGD()
    {
        string strsql = "select Fid from WY_XM_JBXX where XMBH='" + (string)Session["XMBH"] + "'";
        if (rc.GetSignValue(strsql) != null)
        {
            notice.InnerHtml = "【该项目基本信息来自归档信息库，无法修改！】";
            btnSave.Enabled = false;
        }
    }
}