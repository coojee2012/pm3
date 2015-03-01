using Approve.RuleCenter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tools;
using SaveOptionEnum = Approve.EntityBase.SaveOptionEnum;

public partial class WYDW_ApplyCharge_Info : WYPage
{
    RCenter rc = new RCenter();
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckSession();
        if (!IsPostBack)
        {
            showInfo();
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
        }
    }
    private void showInfo()
    {
        string sql = "select * from YW_WY_XM_XMQTXX where FAppID='" + Session["FAppId"].ToString() + "' ";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            t_DC_MJ.Text = dt.Rows[0]["DC_MJ"].ToString();
            t_DC_SF.Text = dt.Rows[0]["DC_SF"].ToString();
            t_DCDT_MJ.Text = dt.Rows[0]["DCDT_MJ"].ToString();
            t_DCDT_SF.Text = dt.Rows[0]["DCDT_SF"].ToString();
            t_GC_MJ.Text = dt.Rows[0]["GC_MJ"].ToString();
            t_GC_SF.Text = dt.Rows[0]["GC_SF"].ToString();
            t_BS_MJ.Text = dt.Rows[0]["BS_MJ"].ToString();
            t_BS_SF.Text = dt.Rows[0]["BS_SF"].ToString();
            t_BG_MJ.Text = dt.Rows[0]["BG_MJ"].ToString();
            t_BG_SF.Text = dt.Rows[0]["BG_SF"].ToString();
            t_SY_MJ.Text = dt.Rows[0]["SY_MJ"].ToString();
            t_SY_SF.Text = dt.Rows[0]["SY_SF"].ToString();
            t_GY_MJ.Text = dt.Rows[0]["GY_MJ"].ToString();
            t_GY_SF.Text = dt.Rows[0]["GY_SF"].ToString();
            t_QT_MJ.Text = dt.Rows[0]["QT_MJ"].ToString();
            t_QT_SF.Text = dt.Rows[0]["QT_SF"].ToString();
            t_LTCW_MJ.Text = dt.Rows[0]["LTCW_MJ"].ToString();
            t_LTCW_SF.Text = dt.Rows[0]["LTCW_SF"].ToString();
            t_LTCW_GS.Text = dt.Rows[0]["LTCW_GS"].ToString();
            t_DXCW_MJ.Text = dt.Rows[0]["DXCW_MJ"].ToString();
            t_DXCW_SF.Text = dt.Rows[0]["DXCW_SF"].ToString();
            t_DXCW_GS.Text = dt.Rows[0]["DXCW_GS"].ToString();
            t_ZXC_MJ.Text = dt.Rows[0]["ZXC_MJ"].ToString();
            t_ZXC_SF.Text = dt.Rows[0]["ZXC_SF"].ToString();
            t_DPC_SF.Text = dt.Rows[0]["DPC_SF"].ToString();
            t_RYPZ_JL.Text = dt.Rows[0]["RYPZ_JL"].ToString();
            t_RYPZ_KF.Text = dt.Rows[0]["RYPZ_KF"].ToString();
            t_RYPZ_XZ.Text = dt.Rows[0]["RYPZ_XZ"].ToString();
            t_RYPZ_AQ.Text = dt.Rows[0]["RYPZ_AQ"].ToString();
            t_RYPZ_WX.Text = dt.Rows[0]["RYPZ_WX"].ToString();
            t_RYPZ_BJ.Text = dt.Rows[0]["RYPZ_BJ"].ToString();
            t_RYPZ_LH.Text = dt.Rows[0]["RYPZ_LH"].ToString();
            t_RYPZ_QT.Text = dt.Rows[0]["RYPZ_QT"].ToString();

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveData();
    }

    private void saveData()
    {
        string fappid = "";
        pageTool tool = new pageTool(this.Page);
        SaveOptionEnum so = SaveOptionEnum.Unknown;

        fappid = Session["FAppId"].ToString();
        string sql = "select * from YW_WY_XM_JBXX where FAppID='" + fappid + "' ";
        DataTable dtXMJBXX = rc.GetTable(sql);
        SortedList sl = tool.getPageValue();
        while (sl.IndexOfValue("") != -1)
        {
            sl.RemoveAt(sl.IndexOfValue(""));
        }
        sl.Remove("FTime");
        //修改情况
        if (hasSFByFAppId(fappid))
        {
            so = SaveOptionEnum.Update;
            sl.Add("FAppID", fappid);
            sl.Add("FUpdateUser", CurrentEntUser.UserId);

        }
        //添加情况
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FAppId", fappid);
            sl.Add("XMBH", dtXMJBXX.Rows[0]["XMBH"].ToString());
            sl.Add("FCreateUser", CurrentEntUser.UserId);
            sl.Add("FUpdateUser", CurrentEntUser.UserId);
            sl.Add("FSystemId", dtXMJBXX.Rows[0]["FSystemId"].ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("XMMC", dtXMJBXX.Rows[0]["XMMC"].ToString());
            so = SaveOptionEnum.Insert;

        }
        //Label1.Text = strsql;
        try
        {
            if (rc.SaveEBase("YW_WY_XM_XMQTXX", sl, "FAppID", so))
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('保存成功！');</script>"); showInfo();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('保存失败！');</script>");
            }
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('保存发生异常！');</script>");
        }
    }

    #region [将字符串转化成数字]
    private decimal stringToDecimal(string str)
    {
        if (str != null && str.ToString() != "")
        {
            try
            {
                return Convert.ToDecimal(str);
            }
            catch
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }
    #endregion

    #region [根据给定的FAppId来判断是否已存在一个项目收费情况记录]
    private bool hasSFByFAppId(string fAppId)
    {
        string sql = "select * from YW_WY_XM_XMQTXX where FAppID='" + fAppId + "' ";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
    private void readOnly()
    {
        btnSave.Enabled = false;
    }
}