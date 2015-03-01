using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Tools;
using SaveOptionEnum = Approve.EntityBase.SaveOptionEnum;

public partial class WYDW_ApplyContract_ContractInfoaspx : WYPage
{
    RCenter rc = new RCenter();
    public string filecount = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckSession();
        getFilecount();
        if (!IsPostBack)
        {
            showInfo();
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveData();
    }

    private void getFilecount()
    {
        string strsql = "select count(*) as FileCount from WY_FileList where FAppId='" + (string)Session["FAppId"] + "' and ftypeid='1001'";
        DataTable dt = rc.GetTable(strsql);
        if (dt != null && dt.Rows.Count > 0)
        {
            filecount = dt.Rows[0]["FileCount"].ToString();
            hidfilecount.Value = filecount;
            UpdateBtn.Value = "上传文件(" + filecount + ")";
        }
    }

    private void saveData()
    {
        pageTool tool = new pageTool(this.Page);

        string sql = "select * from YW_WY_XM_JBXX where FAppID='" + Session["FAppId"].ToString() + "' ";
        DataTable dt3 = rc.GetTable(sql);
        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = new SortedList();
        sl = tool.getPageValue();
        while (sl.IndexOfValue("") != -1)
        {
            sl.RemoveAt(sl.IndexOfValue(""));
        }
        sl.Remove("FTime");
        //string strsql = "select FID from YW_WY_XM_HTBA where FAppId='" + (string)Session["FAppId"] + "'";
        //DataTable dt2 = new DataTable();
        //dt2 = rc.GetTable(strsql);
        if (ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", ViewState["FID"].ToString());
        }
        else
        {
            sl.Add("FID", hidFId.Value);
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FAppId", (string)Session["FAppId"]);
        }
        sl.Add("XMBH", dt3.Rows[0]["XMBH"].ToString());
        sl.Add("FBaseinfoId", CurrentEntUser.EntId);
        sl.Add("FCreateUser", CurrentEntUser.UserId);
        sl.Add("FSystemId", dt3.Rows[0]["FSystemId"].ToString());
        sl.Add("FIsDeleted", 0);
        if (rc.SaveEBase("YW_WY_XM_HTBA", sl, "FID", so))
        {
            ViewState["FID"] = sl["FID"].ToString();
            tool.showMessage("保存成功！");
        }
        else
        {
            tool.showMessage("保存失败！");
        }
    }

    private void showInfo()
    {
        string strsql = "select * from YW_WY_XM_HTBA where FAppId='" + (string)Session["FAppId"] + "'";
        DataTable dt = new DataTable();
        dt = rc.GetTable(strsql);
        if (dt != null && dt.Rows.Count > 0)
        {
            ViewState["FID"] = hidFId.Value = dt.Rows[0]["FID"].ToString();
            t_HTBH.Text = dt.Rows[0]["HTBH"].ToString();
            t_WTDW.Text = dt.Rows[0]["WTDW"].ToString();
            t_XMHQFS.SelectedValue = dt.Rows[0]["XMHQFS"].ToString();
            t_JGRQ.Text = DateTime.Parse(dt.Rows[0]["JGRQ"].ToString()).ToString("yyyy-MM-dd");
            t_HTQDRQ.Text = DateTime.Parse(dt.Rows[0]["HTQDRQ"].ToString()).ToString("yyyy-MM-dd");
            t_HTSXRQ.Text = DateTime.Parse(dt.Rows[0]["HTSXRQ"].ToString()).ToString("yyyy-MM-dd");
            t_HTJZRQ.Text = DateTime.Parse(dt.Rows[0]["HTJZRQ"].ToString()).ToString("yyyy-MM-dd");
        }
        else
        {
            hidFId.Value = Guid.NewGuid().ToString();
        }
    }

    private void readOnly()
    {
        btnSave.Enabled = false;
        hidReadOnly.Value = "1";
    }

}