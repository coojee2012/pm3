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
using System.Drawing.Imaging;
using System.Drawing;
using Approve.RuleCenter;
using Approve.EntityCenter;
using Approve.EntityBase;
public partial class barCode_barMake_BarCode : System.Web.UI.Page
{
    RCenter rule = new RCenter();
    private Graphics g;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string FBarCode = "";
            int FState = 0;
            string sWhere = "flinkid='" +  EConvert.ToString(Session["FAppId"]) + "'";
            if (!string.IsNullOrEmpty(Request.QueryString["FlinkId"]))
            {
                sWhere = "flinkid='" + Request["FlinkId"] + "'  and Fempid='" + Request.QueryString["fempid"] + "'";
            }

            EaProcessInstance ep = (EaProcessInstance)rule.GetEBase(EntityTypeEnum.EaProcessInstance, "", sWhere);
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

                    BarCode39Image barcodeimg = new BarCode39Image(FBarCode, 30);
                    Bitmap image = barcodeimg.GenBarImageBlank();
                    Response.ContentType = "image/gif";
                    image.Save(Response.OutputStream, ImageFormat.Gif);
                }
                else
                {
                    BarCode39Image barcodeimg = new BarCode39Image(FBarCode, 30);
                    Bitmap image = barcodeimg.GenBarImage();
                    Response.ContentType = "image/gif";
                    image.Save(Response.OutputStream, ImageFormat.Gif);
                    //						this.sBarCode.Text=ep.FBarCode;
                }
            }
            else
            {

                Bitmap image = new Bitmap(100, 20);
                Graphics g = System.Drawing.Graphics.FromImage(image);
                Font f = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(Color.Black);
                g.Clear(Color.White);
                g.DrawString("草表", f, b, 3, 3);
                image.Save(Response.OutputStream, ImageFormat.Gif);
            }


        }



    }
}
