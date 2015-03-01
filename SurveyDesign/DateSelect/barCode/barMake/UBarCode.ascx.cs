using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Approve.RuleCenter;
using Approve.EntityCenter;
using Approve.EntityBase;

public partial class barCode_barMake_UBarCode : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string sWhere = "flinkid='" + EConvert.ToString(Session["FAppId"]) + "'";
            if (!string.IsNullOrEmpty(Request.QueryString["FlinkId"]))
            {
                IMG1.Src = string.Format("barcode.aspx?FlinkId={0}&FEmpId={1}", Request.QueryString["FlinkId"], Request.QueryString["FEmpId"]);
                sWhere = " flinkid='" + Request.QueryString["FlinkId"] + "' and FEmpId='" + Request.QueryString["FEmpId"] + "'";
            }
            RCenter rule = new RCenter();
            EaProcessInstance ep = (EaProcessInstance)rule.GetEBase(EntityTypeEnum.EaProcessInstance, "", sWhere);

            string FBarCode = "";
            int FState = 0;

            EaProcessInstanceBackup epbacup = null;
            if (ep == null)
            {
                epbacup = rule.GetEBase(EntityTypeEnum.EaProcessInstanceBackup, "", sWhere) as EaProcessInstanceBackup;
                if (epbacup != null)
                {
                    FBarCode = epbacup.FBarCode;

                    FState = epbacup.FState;
                }
            }
            else
            {
                FBarCode = ep.FBarCode;
                FState = ep.FState;
            }
            if (ep != null || epbacup != null)
            {

                if (FState == 0 || FState == 2)
                {
                    this.sBarCode.Text = "草表";

                    this.sBarCode.CssClass = "OverName";
                }
                else
                {
                    this.sBarCode.Text = FBarCode;
                }
            }
        }
    }
}
