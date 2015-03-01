using Approve.RuleBase;
using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Tools;
using Approve.EntityBase;

public partial class WYDW_ApplyKZXX_KZXXInfo : WYPage
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveData();
    }

    private void saveData()
    {
        Approve.Common.pageTool tool = new Approve.Common.pageTool(this.Page);
        Approve.EntityBase.SaveOptionEnum so = Approve.EntityBase.SaveOptionEnum.Insert;
        SortedList sl = new SortedList();

        sl = tool.getPageValue();
        if (ViewState["FID"] != null) //如果有数据，修改数据
        {
            so = Approve.EntityBase.SaveOptionEnum.Update;
            sl.Add("FID", ViewState["FID"].ToString());
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FAppID", (string)Session["FAppID"]);
            sl.Add("XMBH", (string)Session["XMBH"]);
            sl.Add("FBaseinfoId", CurrentEntUser.EntId);
            sl.Add("FCreateUser", CurrentEntUser.UserId);
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FSystemId",CurrentEntUser.SystemId);
        }

        sl.Add("MapX", hidMapX.Value);
        sl.Add("MapY", hidMapY.Value);

        while (sl.IndexOfValue("") != -1)
        {
            sl.RemoveAt(sl.IndexOfValue(""));
        }


        if (rc.SaveEBase("YW_WY_XM_KZXX", sl, "FID", so))
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
        string strsql = "select * from YW_WY_XM_KZXX where FAppID='" + (string)Session["FAppId"] + "'";
        DataTable dt = new DataTable();
        dt = rc.GetTable(strsql);
        if (dt != null && dt.Rows.Count > 0)
        {
            Approve.Common.pageTool tool = new Approve.Common.pageTool(this.Page);
            tool.fillPageControl(dt.Rows[0]);
            ViewState["FID"] = dt.Rows[0]["FID"].ToString();
            hidMapX.Value = dt.Rows[0]["MapX"].ToString();
            hidMapY.Value = dt.Rows[0]["MapY"].ToString();
        }
    }

    private void readOnly()
    {
        btnSave.Enabled = false;
    }
}