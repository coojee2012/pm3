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

public partial class barCode_barMake_BarCodeCom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request["FBarCode"].ToString() != "" && Request["FBarCode"].ToString() != null)
            {


                BarCode39Image barcodeimg = new BarCode39Image(Request["FBarCode"].ToString(), 30);
                Bitmap image = barcodeimg.GenBarImage();
                Response.ContentType = "image/gif";
                image.Save(Response.OutputStream, ImageFormat.Gif);
            }
        }
    }
}
