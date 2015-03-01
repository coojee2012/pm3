using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using Tools;
using Approve.RuleCenter;
using System.Data;

public partial class WYDW_AppMain_AppPage : System.Web.UI.Page
{
    EgovaDB db = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            setPage();
        }
        else
        {
            if (hidIsSelected.Value == "1")
            {
                selTips.ForeColor = System.Drawing.Color.Green;
                selTips.Text = "已选择";
            }
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {      
        pageTool tool = new pageTool(this.Page);

        try
        {
            string FPrjId = Guid.NewGuid().ToString();
            if (hidIsSelected.Value == "1")
            {
                string sql = "use XM_BaseInfo select * from XM_XMJBXX where XMBH='" + hidXMBH + "'";
                DataTable dtBZ = rc.GetTable(sql);
                if (dtBZ != null && dtBZ.Rows.Count > 0)
                {
                    FPrjId = dtBZ.Rows[0]["XMBH"].ToString();
                }
            }
            string fPrjDataId = Guid.NewGuid().ToString();
            string fAppId = Guid.NewGuid().ToString();
            CF_App_List lKC = new CF_App_List(); //设计企业业务
            lKC.FId = fAppId; //业务GUID
            lKC.FLinkId = fPrjDataId; //项目GUID
            lKC.FBaseinfoId = CurrentEntUser.EntId;
            lKC.FPrjId = FPrjId; //项目编号
            lKC.FName = t_FName.Text.Trim();
            lKC.FManageTypeId = Convert.ToInt32(Request.QueryString["ManageType"]);
            lKC.FwriteDate = DateTime.Now;
            lKC.FState = 0;
            lKC.FIsDeleted = false;
            lKC.FYear = EConvert.ToInt(t_FYear.Text.Trim());
            lKC.FMonth = DateTime.Now.Month;
            lKC.FBaseName = CurrentEntUser.EntName;
            lKC.FTime = DateTime.Now;
            lKC.FCreateTime = DateTime.Now;
            lKC.FReportCount = 1;
            lKC.FCreateUser = CurrentEntUser.UserId;
            db.CF_App_List.InsertOnSubmit(lKC);
            db.SubmitChanges();

            YW_WY_XM_JBXX xmjbxx = new YW_WY_XM_JBXX();

            xmjbxx.Fid = fPrjDataId;
            xmjbxx.FAppID = fAppId;
            xmjbxx.XMBH = FPrjId;
            xmjbxx.XMMC = t_XMMC.Text;
            xmjbxx.FTime = DateTime.Now;
            xmjbxx.FSystemId = 144;
            xmjbxx.FCreateTime = DateTime.Now;
            xmjbxx.FIsDeleted = 0;
            xmjbxx.StandardStatus = hidIsSelected.Value == "0" ? Convert.ToInt16(1) : Convert.ToInt16(2);
            db.YW_WY_XM_JBXX.InsertOnSubmit(xmjbxx);
            db.SubmitChanges();
            this.Session["FIsApprove"] = 0;
            this.RegisterStartupScript(new Guid().ToString(), "<script>window.returnValue = 'ok';window.close();</script>");
        }
        catch
        {

        }
    }

    private void setPage()
    {
        try
        {
            if( Request.QueryString["ManageType"] != null)
            {
                string strManageType = Request.QueryString["ManageType"];
                hidfMType.Value = strManageType;

                t_FName.Text = rc.GetSignValue("Select FName From CF_Sys_ManageType Where FNumber = '" + strManageType + "'");
            }
         
        }
        catch
        {
            applyInfo.Visible = false;
            pageError.Visible = true;
        }

        this.t_FYear.Text = DateTime.Now.Year.ToString();
    }
    protected void Btn_Search_Click(object sender, EventArgs e)
    {

    }
}