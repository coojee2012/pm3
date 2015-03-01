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

public partial class WYDW_ApplyCWQK_CWQKInfo : WYPage
{
    RCenter rc = new RCenter();
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckSession();
        if (!IsPostBack)
        {
            binddate();
            showInfo();
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
        }
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
        if (hasCWByFAppId(fappid))
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
            so = SaveOptionEnum.Insert;
        }

        if (rc.SaveEBase("YW_WY_XM_CWQK", sl, "FAppID", so))
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('保存成功！');</script>"); showInfo();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('保存失败！');</script>");
        }

    }

    private void binddate()
    {
        t_ND.Text = DateTime.Now.AddYears(-1).Year.ToString();
    }

    private void showInfo()
    {
        if (Session["FAppId"] != null)
        {
            string sql = "select * from YW_WY_XM_CWQK where FAppID='" + Session["FAppId"].ToString() + "' ";
            DataTable dt = rc.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                t_ND.Text = DateTime.Now.AddYears(-1).ToString("yyyy");
                t_WYFZE.Text = dt.Rows[0]["WYFZE"].ToString();
                t_WYFSFL.Text = dt.Rows[0]["WYFSFL"].ToString();
                t_TCF.Text = dt.Rows[0]["TCF"].ToString();
                t_GGF.Text = dt.Rows[0]["GGF"].ToString();
                t_QT.Text = dt.Rows[0]["QT"].ToString();
                t_YYCB.Text = dt.Rows[0]["YYCB"].ToString();
                t_YYLR.Text = dt.Rows[0]["YYLR"].ToString();
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('数据加载失败！');</script>");
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

    #region [根据给定的FAppId来判断是否已存在一个项目财务情况记录]
    private bool hasCWByFAppId(string fAppId)
    {
        string sql = "select * from YW_WY_XM_CWQK where FAppID='" + fAppId + "' ";
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveData();
    }
    private void readOnly()
    {
        btnSave.Enabled = false;
    }
}