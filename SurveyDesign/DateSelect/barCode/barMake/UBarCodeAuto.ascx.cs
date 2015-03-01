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

public partial class barCode_barMake_UBarCodeAuto : System.Web.UI.UserControl
{
    string _CodeNumber = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (string.IsNullOrEmpty(_CodeNumber))
            {
                this.sBarCode.Text = "草表";
                this.sBarCode.CssClass = "OverName";
            }
            else
            {
                IMG1.Src = "../../barcode/barmake/BarCodeCom.aspx?FBarCode=" + _CodeNumber;
                this.sBarCode.Text = _CodeNumber;
            }

        }
    }

    public string CodeNumber
    {
        get
        {
            return this._CodeNumber;
        }
        set
        {
            _CodeNumber = value;
        }
    }
}
