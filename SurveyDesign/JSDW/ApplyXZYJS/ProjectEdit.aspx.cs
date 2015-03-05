using Approve.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Approve.EntityBase;
using Approve.RuleCenter;
public partial class JSDW_ApplyXZYJS_ProjectEdit : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pageTool tool = new pageTool(this.Page, "txt");
            string XMBM = Request.QueryString["XMBM"];
            if (!string.IsNullOrEmpty(XMBM))
            {
                string sql = @"SELECT TOP 1 XMBM,XMBH,JSDW,JSDWDZ,LXR,LXDH,XMMC,XMSD,SDA,SDB,SDC,JGLX,XMDZ,LXWH,LXSJ,XMSFSW,JZZMJ,ZTZ,JSYJ,XMNR FROM XM_XMInfo WHERE XMBM='"+XMBM+"'";
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    tool.fillPageControl(row);
                    ddlXMSFSW.SelectedValue = string.IsNullOrEmpty(row["XMSFSW"].ToString()) ? "-1" : row["XMSFSW"].ToString();
                    ucProjectPlace.fNumber = row["XMSD"].ToString();
                  //  e.Item.Cells[3].Text = rc.GetSignValue(EntityTypeEnum.EsManageDept, "FName", "Fnumber='" + e.Item.Cells[3].Text + "'");
                }
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string XMBM = Request.QueryString["XMBM"];
        string province = ucProjectPlace.GetProvinceNumber() == "" ? "-1" : ucProjectPlace.GetProvinceNumber();
        string city = ucProjectPlace.GetCityNumber() == "" ? "-1" : ucProjectPlace.GetCityNumber();
        string country = ucProjectPlace.GetCountryNumber() == "" ? "-1" : ucProjectPlace.GetCountryNumber();
        string jzzmj = txtJZZMJ.Text == "" ? "0" : txtJZZMJ.Text;
        string sql = @"Update XM_XMInfo set XMBH='" + txtXMBH.Text + 
            "',JSDW='" + txtJSDW.Text + 
            "',JSDWDZ='" + txtJSDWDZ.Text + 
            "',LXR='" + txtLXR.Text + 
            "',LXDH='" + txtLXDH.Text + 
            "',XMMC='" + txtXMMC.Text + 
            "',XMSD='" + ucProjectPlace.fNumber +
            "',SDA=" + province + 
            ",SDB=" + city + 
            ",SDC=" + country + 
            ",JGLX='" + txtJGLX.Text + 
            "',XMDZ='" + txtXMDZ.Text + 
            "',LXWH='" + txtLXWH.Text + 
            "',LXSJ='" + txtLXSJ.Text + 
            "',XMSFSW=" + ddlXMSFSW.SelectedValue +
            ",JZZMJ='" + jzzmj + 
            "',ZTZ='" + txtZTZ.Text + 
            "',JSYJ='" + txtJSYJ.Text +
            "',XMNR='" + txtXMNR.Text + 
            "' WHERE XMBM='" + XMBM + "'";
        bool success = rc.PExcute(sql);
        if (success)
            tool.showMessageAndRunFunction("编辑成功", "window.close();window.returnValue='1';");
        else
            tool.showMessage("保存失败");
    }
}