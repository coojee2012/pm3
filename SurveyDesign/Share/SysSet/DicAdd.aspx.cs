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
public partial class Share_SysSet_DicAdd : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            ControlBind();
            if (Request["fid"] != null && Request["fid"] != null)
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
            }
        }
    }
    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fid,Fname,fnumber from CF_Sys_SystemName where fisdeleted=0 order by forder");
        DataTable dt = sh.GetTable(sb.ToString());
        this.t_FSystemId.DataSource = dt;
        this.t_FSystemId.DataTextField = "FName";
        this.t_FSystemId.DataValueField = "FNumber";
        this.t_FSystemId.DataBind();
        this.t_FSystemId.Items.Insert(0, new ListItem("所有", "0"));
    }
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from cf_sys_dicclass where fid='" + this.ViewState["FID"].ToString() + "'");
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
        string fnewNumber = "";
        DataTable dtt = new DataTable();
        dtt = sh.GetTable(EntityTypeEnum.EsDicClass, "FID", "fnumber='" + this.t_FNumber.Text + "' and fid<>'" + this.ViewState["FID"] + "'");
        if (dtt != null && dtt.Rows.Count > 0)
        {
            tool.showMessage("字典编码重复");
            this.t_FNumber.Focus();
            return;
        }

        if (this.ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
            foldNumber = sh.GetSignValue(EntityTypeEnum.EsDicClass, "FNumber", "FId='" + this.ViewState["FID"].ToString() + "'");
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
        }
        if (sh.SaveEBase(EntityTypeEnum.EsDicClass, sl, "FID", so))
        {
            fnewNumber = sl["FNUMBER"].ToString();
            if (foldNumber != fnewNumber && foldNumber != "")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" select fid from CF_Sys_Dic where FParentId='" + foldNumber + "' ");
                DataTable dt = sh.GetTable(sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    int rowCount = dt.Rows.Count;
                    SortedList[] sls = new SortedList[rowCount];
                    EntityTypeEnum[] ens = new EntityTypeEnum[rowCount];
                    SaveOptionEnum[] sos = new SaveOptionEnum[rowCount];
                    string[] fkeys = new string[rowCount];

                    for (int i = 0; i < rowCount; i++)
                    {
                        sls[i] = new SortedList();
                        sls[i].Add("FID", dt.Rows[i]["FID"].ToString());
                        sls[i].Add("FParentId", fnewNumber);

                        ens[i] = EntityTypeEnum.EsDic;
                        fkeys[i] = "FID";
                        sos[i] = SaveOptionEnum.Update;
                    }
                    sh.SaveEBaseM(ens, sls, fkeys, sos);
                }
            }
            tool.showMessageAndRunFunction("保存成功","window.returnValue='1';");
            HSaveResult.Value = "1";
            this.ViewState["FID"] = sl["FID"].ToString();
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
