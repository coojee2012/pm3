using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Approve.Common;

public partial class JSDW_ApplyXZYJS_ProjectAdd : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
         
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        StringBuilder _builder = new StringBuilder();
        _builder.Append("INSERT INTO XM_XMInfo (XMBM,XMBH,JSDW,JSDWDZ,LXR,LXDH,XMMC,XMSD,SDA,SDB,SDC,JGLX,XMDZ,LXWH,LXSJ,XMSFSW,JZZMJ,ZTZ,JSYJ,XMNR) VALUES");
        _builder.AppendFormat("(newid(),'{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},{8},{9},'{10}','{11}','{12}','{13}',{14},'{15}','{16}','{17}','{18}')", txtProjectNumber.Text, txtBuildUnitName.Text, txtBuildUnitAddress.Text, txtName.Text, txtPhone.Text, txtProjectName.Text, ucProjectPlace.fNumber, string.IsNullOrEmpty(ucProjectPlace.GetProvinceNumber()) ? "-1" : ucProjectPlace.GetProvinceNumber(), string.IsNullOrEmpty(ucProjectPlace.GetCityNumber()) ? "-1" : ucProjectPlace.GetCityNumber(), string.IsNullOrEmpty(ucProjectPlace.GetCountryNumber()) ? "-1" : ucProjectPlace.GetCountryNumber(), txtProjectType.Text, txtAddress.Text, txtLXWH.Text, txtLXSJ.Text, ddlOuter.SelectedValue, string.IsNullOrEmpty(txtArea.Text) ? "0" : txtArea.Text, txtMoney.Text, txtYJ.Text, txtContent.Text);
        bool success = rc.PExcute(_builder.ToString());
        if (success)
            tool.showMessageAndRunFunction("保存成功", "window.close();window.returnValue='1';");
        else
            tool.showMessage("保存失败");
    }
}