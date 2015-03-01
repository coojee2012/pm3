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

public partial class WYDW_ApplyYWH_YWHCYInfo : WYPage
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

    #region [根据给定的FAppId来判断是否已存在一个业主委员会成员情况记录]
    private bool hasYWHCYByFAppId(string fAppId)
    {
        string sql = "select * from YW_WY_XM_YZWYHCY where FAppID='" + fAppId + "' ";
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


    private void showInfo()
    {
        if (Request.QueryString["fid"] != null)
        {
            string fid = Request.QueryString["fid"].ToString();
            string sql = "select * from YW_WY_XM_YZWYHCY where FID='" + fid + "' ";
            DataTable dt = rc.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                t_XM.Text = dt.Rows[0]["XM"].ToString();
                t_XB.Text = dt.Rows[0]["XB"].ToString();
                t_NL.Text = dt.Rows[0]["NL"].ToString();
                t_SFZH.Text = dt.Rows[0]["SFZH"].ToString();
                t_ZZMM.Text = dt.Rows[0]["ZZMM"].ToString();
                t_YZWYHZW.Text = dt.Rows[0]["YZWYHZW"].ToString();
                t_LXDH.Text = dt.Rows[0]["LXDH"].ToString();
                t_GZDW.Text = dt.Rows[0]["GZDW"].ToString();
                t_JTDZ.Text = dt.Rows[0]["JTDZ"].ToString();
            }
        }
    }

    private void saveData()
    {
        string fappid = "";
        pageTool tool = new pageTool(this.Page);
        SaveOptionEnum so = SaveOptionEnum.Unknown;
        fappid = Session["FAppId"].ToString();
        SortedList sl = tool.getPageValue();
        while (sl.IndexOfValue("") != -1)
        {
            sl.RemoveAt(sl.IndexOfValue(""));
        }
        sl.Remove("FTime");
        //修改情况
        if (Request.QueryString["fid"]!=null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID",Request.QueryString["fid"].ToString());
            sl.Add("FUpdateUser",CurrentEntUser.UserId);
        }
        //添加情况
        else 
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FAppId", fappid);
            sl.Add("XMBH", Session["XMBH"]==null?"":Session["XMBH"].ToString());
            sl.Add("FCreateUser", CurrentEntUser.UserId);
            sl.Add("FUpdateUser", CurrentEntUser.UserId);
            sl.Add("FSystemId", CurrentEntUser.SystemId);
            sl.Add("FIsDeleted", 0);
            so = SaveOptionEnum.Insert;
        }
        //Label1.Text = strsql;
        try
        {
            if (rc.SaveEBase("YW_WY_XM_YZWYHCY", sl, "FID", so))
            {
                ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('保存成功');if(window.opener){window.opener.returnValue='1';}else{window.returnValue='1';}window.close();</script>"); showInfo();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('保存失败');window.returnValue='0';</script>");
            }
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "js", "<script>window.returnValue='-1';</script>");
        }

    }

    private void readOnly()
    {
        btnSave.Enabled = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveData();
    }
}