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
public partial class Admin_main_BadActionTypeAdd : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            if (Request["fid"] != null && Request["fid"] != null)
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
            }
        }
    }

    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Sys_BadActionCode where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);

        }
    }
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = tool.getPageValue();
        string foldNumber = "";
        if (this.ViewState["FID"] != null)
        {
            foldNumber = sh.GetSignValue(EntityTypeEnum.EsBadActionCode, "FNumber", "FID='" + ViewState["FID"].ToString() + "'");
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
        }
        if (sh.SaveEBase(EntityTypeEnum.EsBadActionCode, sl, "FID", so))
        {
            this.ViewState["FID"] = sl["FID"].ToString();
            string fnewNumber = sl["FNUMBER"].ToString();
            if (fnewNumber != foldNumber && foldNumber != "")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select FId from CF_Sys_BadActionCode where fparentid='" + foldNumber + "'");
                DataTable dt = sh.GetTable(sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    int rowCount = dt.Rows.Count;
                    SortedList[] sls = new SortedList[rowCount];
                    EntityTypeEnum[] ens = new EntityTypeEnum[rowCount];
                    string[] fkeys = new string[rowCount];
                    SaveOptionEnum[] sos = new SaveOptionEnum[rowCount];
                    for (int i = 0; i < rowCount; i++)
                    {
                        ens[i] = EntityTypeEnum.EsBadActionCode;
                        sls[i] = new SortedList();
                        sls[i].Add("FID", dt.Rows[i]["FID"].ToString());
                        sls[i].Add("FParentId", fnewNumber);
                        fkeys[i] = "FID";
                        sos[i] = SaveOptionEnum.Update;
                    }
                    sh.SaveEBaseM(ens, sls, fkeys, sos);
                }
            }
            tool.showMessage("保存成功");
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
    protected void btnNew_Click(object sender, EventArgs e)
    {
        this.ViewState["FID"] = null;
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("clearPage()");
    }

}
