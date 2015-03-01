using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using EgovaDAO;

public partial class WYDW_ApplyXMSQ_XMSQInfo : WYPage
{
    EgovaDB db = new EgovaDB();
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
        if (Session["FAppId"] != null)
        {
            string fappid = Session["FAppId"].ToString();
            IQueryable<YW_WY_XM_XMSQ> iqsq = db.YW_WY_XM_XMSQ.Where(t => t.FAppID == fappid);

            if (iqsq.Count() > 0)
            {
                YW_WY_XM_XMSQ sq = iqsq.FirstOrDefault();
                dropLostReason.SelectedValue = sq.SQYY;
                t_Losttime.Text = sq.SQSJ.Value.ToString("yyyy-MM-dd");
                t_BZ.Text = sq.BZ;
            }

        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('数据加载失败！');</script>");
        }
    }
    private void saveData()
    {
        string fappid = "";
        string xmbh = "";

        fappid = Session["FAppId"].ToString();
        YW_WY_XM_JBXX jb = db.YW_WY_XM_JBXX.Where(t => t.FAppID == fappid).FirstOrDefault();

        YW_WY_XM_XMSQ sq = new YW_WY_XM_XMSQ();

        //Label1.Text = strsql;

        try
        {
            IQueryable<YW_WY_XM_XMSQ> iqsq = db.YW_WY_XM_XMSQ.Where(t => t.FAppID == fappid);
            if (iqsq.Count() > 0)
            {
                //db.YW_WY_XM_XMSQSQ.Attach(sq);
                sq = iqsq.FirstOrDefault();
            }

            sq.SQYY = dropLostReason.SelectedValue;
            sq.SQSJ = Convert.ToDateTime(t_Losttime.Text);
            sq.BZ = t_BZ.Text;
            sq.FTime = DateTime.Now;

            if (iqsq.Count() == 0)
            {
                sq.FID = Guid.NewGuid().ToString();
                sq.XMBH = jb.XMBH;
                sq.FAppID = fappid;
                sq.FSystemID = jb.FSystemId;
                sq.FCreateUser = jb.FCreateUser;
                sq.FCreateTime = DateTime.Now;
                sq.FIsDeleted = 0;
                db.YW_WY_XM_XMSQ.InsertOnSubmit(sq);
            }
            db.SubmitChanges();
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('保存成功！');</script>"); showInfo();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('保存发生异常！');</script>");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveData();
    }
    private void readOnly()
    {
        btnSave.Enabled = false;
    }
}