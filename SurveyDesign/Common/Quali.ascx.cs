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
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;

public partial class Common_Quali : System.Web.UI.UserControl
{
    RCenter rc = new RCenter();
    private string _sParentNumber = "";
    private static string fDefaultDept = ComFunction.GetDefaultDept();
    public string fNumber
    {
        get
        {

            return this.hNumber.Value;
        }
        set
        {
            SetValue(value);
            this.hNumber.Value = value;
        }
    }

    public string fParentNumber
    {
        get
        { 
            return _sParentNumber;
        }
        set
        {
            _sParentNumber = value;
        }
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.dp2.Attributes.Add("onchange", "XmlPost2(this,document.getElementById('" + this.dp3.ClientID + "'));" + this.ClientID + "getFnumber()");
            this.dp3.Attributes.Add("onchange", "" + this.ClientID + "getFnumber()");
            ShowData();

        }


    }

    private void ShowData()
    { 
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fname,fnumber from cf_sys_dic ");
        sb.Append(" where fparentid='"+fParentNumber+"'");
        sb.Append(" and fisdeleted=0 order by forder ");
        DataTable dt = rc.GetTable(sb.ToString());
        this.dp1.DataSource = dt;
        this.dp1.DataTextField = "FName";
        this.dp1.DataValueField = "FNumber";
        this.dp1.DataBind();
        this.dp1.Items.Insert(0, new ListItem("请选择", ""));
        this.dp1.Attributes.Add("onchange", "XmlPost2(this,document.getElementById('" + this.dp2.ClientID + "'));clearOptions(document.getElementById('" + this.dp3.ClientID + "'));" + this.ClientID + "getFnumber();");


        
    }




    private void SetValue(string fNumber)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sb1 = new StringBuilder();
        sb.Append("Fnumber='" + fNumber + "'");
        string fLevel = rc.GetSignValue(EntityTypeEnum.EsDic, "flevel", sb.ToString());
        if (fLevel == "2")
        {
            this.dp1.SelectedValue = fNumber.Trim();

            sb1.Append("<script>");
            sb1.Append("XmlPost2(document.getElementById('" + this.dp1.ClientID + "'),");
            sb1.Append("document.getElementById('" + this.dp2.ClientID + "'));");
            sb1.Append("" + this.ClientID + "getFnumber();");
            sb1.Append("</script>");
            this.Page.RegisterStartupScript(Guid.NewGuid().ToString(), sb1.ToString());
        }

        if (fLevel == "3")
        { 
            string fPrentNumber = rc.GetSignValue(EntityTypeEnum.EsDic, "fparentid", sb.ToString());
            this.dp1.SelectedValue = fPrentNumber.Trim();

            sb1.Append("<script>");

            sb1.Append("XmlPost2(document.getElementById('" + this.dp1.ClientID + "'),");
            sb1.Append("document.getElementById('" + this.dp2.ClientID + "'));");

            sb1.Append("setValue(document.getElementById('" + this.dp2.ClientID + "'),'" + fNumber.Trim() + "');");

            sb1.Append("XmlPost2(document.getElementById('" + this.dp2.ClientID + "'),");
            sb1.Append("document.getElementById('" + this.dp3.ClientID + "'));");
            sb1.Append("" + this.ClientID + "getFnumber()");
            sb1.Append("</script>");
            this.Page.RegisterStartupScript(Guid.NewGuid().ToString(), sb1.ToString());



        }

        if (fLevel == "4")
        {
            string fPrentNumber = rc.GetSignValue(EntityTypeEnum.EsDic, "fparentid", sb.ToString());
            string fPrentPrentNumber = rc.GetSignValue(EntityTypeEnum.EsDic, "fparentid", "fnumber='" + fPrentNumber.Trim() + "'");
            this.dp1.SelectedValue = fPrentPrentNumber;
            sb1.Append("<script>");
            sb1.Append("XmlPost2(document.getElementById('" + this.dp1.ClientID + "'),");
            sb1.Append("document.getElementById('" + this.dp2.ClientID + "'));");
            sb1.Append("setValue(document.getElementById('" + this.dp2.ClientID + "'),'" + fPrentNumber.Trim() + "');");
            sb1.Append("XmlPost2(document.getElementById('" + this.dp2.ClientID + "'),");
            sb1.Append("document.getElementById('" + this.dp3.ClientID + "'));");
            sb1.Append("setValue(document.getElementById('" + this.dp3.ClientID + "'),'" + fNumber.Trim() + "');");
            sb1.Append("" + this.ClientID + "getFnumber()");
            sb1.Append("</script>");
            this.Page.RegisterStartupScript(Guid.NewGuid().ToString(), sb1.ToString());
        }
    }
}
