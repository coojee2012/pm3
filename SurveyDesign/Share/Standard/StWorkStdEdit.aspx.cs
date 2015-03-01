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
using System.Reflection;


public partial class Admin_Standard_StWorkStdEdit : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            ControlBind();
            if (Request["fid"] != null && Request["fid"] != "")
            {
                ViewState["FID"] = Request["fid"];
                ShowInfo();
            }

        }
    }
 
    private void ControlBind()
    {
  
       
        ShowDivType();
    }

  

 

    private void ShowDivType()
    {
   
        StringBuilder sb = new StringBuilder();
  
        //sb.Remove(0, sb.Length);
        //sb.Append(" select fid,fcontent from CF_Sys_ProjectType ");
        //sb.Append(" where FStandardTypeId=1 and FQUALIFICATIONID='" + t_FFQUALIFICATIONID.SelectedValue.Trim() + "'");
        //sb.Append(" and  FLEVELGROUPStr like '%" + fLevel + "%'");
        //DataTable dt = sh.GetTable(sb.ToString());
        //t_FTTYPEID.DataSource = dt;
        //t_FTTYPEID.DataValueField = "fid";
        //t_FTTYPEID.DataTextField = "fcontent";
        //t_FTTYPEID.DataBind();
    }


    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" fid='" + ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(EntityTypeEnum.EsAppStand, "", sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            pageTool tool = new pageTool(this.Page);
         
            tool.fillPageControl(dt.Rows[0]);

            sb.Remove(0, sb.Length);
            sb.Append(" select fparentid from cf_sys_dic where fnumber = '" + dt.Rows[0]["FFQUALIFICATIONID"].ToString().Trim() + "'");
            string sParentId = sh.GetSignValue(sb.ToString());
            if (sParentId != null && sParentId != "")
            {
                

             

                ShowDivType();
                t_FTTYPEID.SelectedIndex =
                      t_FTTYPEID.Items.IndexOf(t_FTTYPEID.Items.FindByValue(dt.Rows[0]["FTTYPEID"].ToString().Trim()));
            }
        }
    }

    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = tool.getPageValue();
        SaveOptionEnum so = SaveOptionEnum.Insert;
        if (ViewState["FID"] != null)
        {
            sl.Add("FID", ViewState["FID"].ToString());
            so = SaveOptionEnum.Update;
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FKind", 1);

        }
        sl.Add("FSystemId", Request.QueryString["fsysId"]);
        if (sh.SaveEBase(EntityTypeEnum.EsAppStand, sl, "FID", so))
        {
            tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
            ViewState["FID"] = sl["FID"].ToString();
            HSaveResult.Value = "1";
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }

    
     
}
