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

public partial class WYDW_ApplyKZXX_KZXXInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
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
        if (Session["FAppId"] != null)
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

                //t_ZDMJ.Text = dt.Rows[0]["ZDMJ"].ToString();
                //t_KFDW.Text = dt.Rows[0]["KFDW"].ToString();
                //t_JCND.Text = dt.Rows[0]["JCND"].ToString();
                //t_IsZZXQ.SelectedValue = dt.Rows[0]["IsZZXQ"].ToString();
                //t_JMHS.Text = dt.Rows[0]["JMHS"].ToString();
                //t_FJMHS.Text = dt.Rows[0]["FJMHS"].ToString();
                //t_JZRS.Text = dt.Rows[0]["JZRS"].ToString();
                //t_JGRQ.Text = dt.Rows[0]["JGRQ"].ToString();
                //t_SCJGRQ.Text = dt.Rows[0]["SCJGRQ"].ToString();
                //t_WYYFMJ.Text = dt.Rows[0]["WYYFMJ"].ToString();
                //t_DC_MJ.Text = dt.Rows[0]["DC_MJ"].ToString();
                //t_DCDT_MJ.Text = dt.Rows[0]["DCDT_MJ"].ToString();
                //t_GC_MJ.Text = dt.Rows[0]["GC_MJ"].ToString();
                //t_BS_MJ.Text = dt.Rows[0]["BS_MJ"].ToString();
                //t_BG_MJ.Text = dt.Rows[0]["BG_MJ"].ToString();
                //t_SY_MJ.Text = dt.Rows[0]["SY_MJ"].ToString();
                //t_GY_MJ.Text = dt.Rows[0]["GY_MJ"].ToString();
                //t_QT_MJ.Text = dt.Rows[0]["QT_MJ"].ToString();
                //t_LTCW_MJ.Text = dt.Rows[0]["LTCW_MJ"].ToString();
                //t_LTCW_GS.Text = dt.Rows[0]["LTCW_GS"].ToString();
                //t_DXCW_MJ.Text = dt.Rows[0]["DXCW_MJ"].ToString();
                //t_DXCW_GS.Text = dt.Rows[0]["DXCW_GS"].ToString();
                //t_ZXC_MJ.Text = dt.Rows[0]["ZXC_MJ"].ToString();
                //t_AFXT.SelectedValue = dt.Rows[0]["AFXT"].ToString();
                //t_CRKGS.Text = dt.Rows[0]["CRKGS"].ToString();
                //t_JKXFSGS.Text = dt.Rows[0]["JKXFSGS"].ToString();
                //t_DTGS.Text = dt.Rows[0]["DTGS"].ToString();
                //t_PDFS.Text = dt.Rows[0]["PDFS"].ToString();
                //t_FDJS.Text = dt.Rows[0]["FDJS"].ToString();
                //t_YGGDH.Text = dt.Rows[0]["YGGDH"].ToString();
                //t_DJBPGS.Text = dt.Rows[0]["DJBPGS"].ToString();
                //t_ECGSSXS.Text = dt.Rows[0]["ECGSSXS"].ToString();
                //t_HFCS.Text = dt.Rows[0]["HFCS"].ToString();
                //t_YGGSH.Text = dt.Rows[0]["YGGSH"].ToString();
                //t_MJJKXTTS.Text = dt.Rows[0]["MJJKXTTS"].ToString();
                //t_XFXTTS.Text = dt.Rows[0]["XFXTTS"].ToString();
                //t_TCGLXTTS.Text = dt.Rows[0]["TCGLXTTS"].ToString();
                //t_SJMJ.Text = dt.Rows[0]["SJMJ"].ToString();
            }
        }
        else
        {
            this.RegisterStartupScript(new Guid().ToString(), "<script>alert('数据加载失败！');parent.location.href='../main/Index.aspx';</script>");
        }

    }

    private void readOnly()
    {
        btnSave.Enabled = false;
    }
}