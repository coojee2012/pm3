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

public partial class WYDW_ApplyYWH_YWHCYInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string type = Request.QueryString["e"].ToString();
            //修改
            if (type == "2")
            {
                showInfo();
            }
            
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
        if (Session["FAppId"] != null)
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
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('数据加载失败！');</script>");
        }
    }

    private void saveData(string type)
    {
        string fappid = "";
        string fid="";
        pageTool tool = new pageTool(this.Page);
        SaveOptionEnum so = SaveOptionEnum.Unknown;
        if (Session["FAppId"] != null&&Request.QueryString["fid"]!=null)
        {
            fappid = Session["FAppId"].ToString();
            fid = Request.QueryString["fid"].ToString();
            SortedList sl = tool.getPageValue();
            while (sl.IndexOfValue("") != -1)
            {
                sl.RemoveAt(sl.IndexOfValue(""));
            }
            sl.Remove("FTime");
            //修改情况
            if (type=="2")
            {
                so = SaveOptionEnum.Update;
                sl.Add("FID",fid);
                sl.Add("FUpdateUser",CurrentEntUser.UserId);
            }
            //添加情况
            else if(type=="1")
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
                    ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('保存成功');window.close();window.returnValue='1';", true); showInfo();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('保存失败');window.returnValue='0';", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "window.returnValue='-1';", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('保存出错');window.returnValue='-1';", true);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        t_SFZH.Text = "保存点击";
    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        string type = Request.QueryString["e"].ToString();
        saveData(type);
    }
}