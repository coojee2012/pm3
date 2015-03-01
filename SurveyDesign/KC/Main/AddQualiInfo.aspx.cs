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
using Approve.EntityCenter;
public partial class Government_EntQualiCerti_AddQualiInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["IsView"]) && Request.QueryString["IsView"] == "1")
        {
            this.ClientScript.RegisterStartupScript(this.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>try{btnEnable();}catch(e){}</script>");

        }
        if (!Page.IsPostBack)
        {
            if (rc.GetSysObjectContent("_CanModifyBaseInfo") == "1")
            {
                btnSave.Visible = true;
            }
            else
            {
                btnSave.Visible = false;
            }
            btnSave.Attributes.Add("onclick", "if(checkInfo()){return true;}else{return false;}");
            ControlBind();
            if (Request["fid"] != null && Request["fid"] != "")
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
            }
        }
    }
    private void ControlBind()
    {
        DataTable dt = null;
        dt = rc.getDicTbByFNumber("134");
        dbTypeId.DataSource = dt;
        dbTypeId.DataTextField = "FName";
        dbTypeId.DataValueField = "FNumber";
        dbTypeId.DataBind();
        dbTypeId.Items.Insert(0, new ListItem("--请选择--", ""));

        StringBuilder sb = new StringBuilder();
        sb.Append(" select fname,fnumber from CF_Sys_QualiLevel where ");
        sb.Append(" fsystemid=190 ");
        sb.Append(" and fisdeleted=0 order by flevel");
        dt = rc.GetTable(sb.ToString());
        t_FLevelId.DataSource = dt;
        t_FLevelId.DataTextField = "FName";
        t_FLevelId.DataValueField = "FNumber";
        t_FLevelId.DataBind();
        t_FLevelId.Items.Insert(0, new ListItem("--请选择--", ""));

        //核准单位 
        sb.Remove(0, sb.Length);
        sb.Append(" fisdeleted=0 and ");
        sb.Append("(fnumber =" + ComFunction.GetDefaultDept() + " ");
        sb.Append(" or fnumber=0) order by fnumber");
        dt = rc.GetTable(EntityTypeEnum.EsManageDept, "FFullName FName,FNumber", sb.ToString());
        t_FAppDeptId.DataSource = dt;
        t_FAppDeptId.DataTextField = "FName";
        t_FAppDeptId.DataValueField = "FNumber";
        t_FAppDeptId.DataBind();
        t_FAppDeptId.Items.Insert(0, new ListItem("--请选择--", ""));
    }

    private void ShowList(string fNumber)
    {
        if (fNumber == "")
            return;
        DataTable dt = rc.getDicTbByFNumber(fNumber);
        this.t_FListId.DataSource = dt;
        this.t_FListId.DataTextField = "FName";
        this.t_FListId.DataValueField = "FNumber";
        this.t_FListId.DataBind();
        this.t_FListId.Items.Insert(0, new ListItem("--请选择--", ""));
    }

    private void ShowTrade(string fNumber)
    {
        if (fNumber == "")
        {
            return;
        }
        if (fNumber.Trim() == "134002003")
        {
            fNumber = "134002002";
        }
        DataTable dt = rc.getDicTbByFNumber(fNumber);
        this.t_FTypeId.DataSource = dt;
        this.t_FTypeId.DataTextField = "FName";
        this.t_FTypeId.DataValueField = "FNumber";
        this.t_FTypeId.DataBind();
        this.t_FTypeId.Items.Insert(0, new ListItem("--请选择--", ""));
    }

    private void ShowSpcal(string fNumber)
    {
        if (fNumber == "")
            return;
        if (t_FListId.SelectedValue.Trim() == "134002002")
            return;

        DataTable dt = rc.getDicTbByFNumber(fNumber);
        cSpcal.DataSource = dt;
        cSpcal.DataTextField = "FName";
        cSpcal.DataValueField = "FNumber";
        cSpcal.DataBind();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        pageTool tool = new pageTool(this.Page);
        sb.Append("FId='" + ViewState["FID"].ToString() + "'");
        DataTable dt = rc.GetTable(EntityTypeEnum.EbQualiCertiTrade, "", sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            sb.Remove(0, sb.Length);
            sb.Append("FNumber='" + dt.Rows[0]["FListId"].ToString().Trim() + "' and fparentid in (134001,134002)");
            string fTypeId = rc.GetSignValue(EntityTypeEnum.EsDic, "FParentId", sb.ToString());
            if (fTypeId != null && fTypeId != "")
            {
                this.dbTypeId.SelectedIndex = this.dbTypeId.Items.IndexOf(this.dbTypeId.Items.FindByValue(fTypeId));
            }

            ShowList(this.dbTypeId.SelectedValue.Trim());
            this.t_FListId.SelectedIndex = this.t_FListId.Items.IndexOf(this.t_FListId.Items.FindByValue(dt.Rows[0]["FListId"].ToString().Trim()));

            ShowTrade(t_FListId.SelectedValue.Trim());
            t_FTypeId.SelectedIndex = t_FTypeId.Items.IndexOf(t_FTypeId.Items.FindByValue(dt.Rows[0]["FTypeId"].ToString().Trim()));

            ShowSpcal(t_FTypeId.SelectedValue.Trim());

            SetSpcalNumber(dt.Rows[0]["FLeadId"].ToString());

            t_FLevelId.SelectedIndex = t_FLevelId.Items.IndexOf(t_FLevelId.Items.FindByValue(dt.Rows[0]["FLevelId"].ToString().Trim()));

        }

    }

    private void ShowAppInfo(string fPId)
    {
        t_FAppTime.Text = DateTime.Now.ToShortDateString();
        EaProcessInstance ep = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "", "fid='" + fPId + "'");
        if (ep == null)
        {
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("FNumber='" + ep.FListId + "' and fparentid in (134001,134002)");
        string fTypeId = rc.GetSignValue(EntityTypeEnum.EsDic, "FParentId", sb.ToString());
        if (fTypeId != null && fTypeId != "")
        {
            this.dbTypeId.SelectedIndex = this.dbTypeId.Items.IndexOf(this.dbTypeId.Items.FindByValue(fTypeId));
        }

        ShowList(this.dbTypeId.SelectedValue.Trim());
        this.t_FListId.SelectedIndex = this.t_FListId.Items.IndexOf(this.t_FListId.Items.FindByValue(ep.FListId.Trim()));

        ShowTrade(this.t_FListId.SelectedValue.Trim());
        this.t_FTypeId.SelectedIndex = this.t_FTypeId.Items.IndexOf(this.t_FTypeId.Items.FindByValue(ep.FTypeId.Trim()));

        ShowSpcal(this.t_FTypeId.SelectedValue.Trim());

        SetSpcalNumber(ep.FLeadId);

        this.t_FLevelId.SelectedIndex = this.t_FLevelId.Items.IndexOf(this.t_FLevelId.Items.FindByValue(ep.FLevelId));
    }

    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        string fSpaclNumber = GetSpcalNumber();
        if (fSpaclNumber != "NoCount" && fSpaclNumber == "")
        {
            tool.showMessage("请选择专业");
            return;
        }
        if (fSpaclNumber == "NoCount")
        {
            fSpaclNumber = "";
        }
        string fSpaclName = GetSpcalName();
        if (fSpaclName == "NoCount")
        {
            fSpaclName = "";
        }
        SaveOptionEnum so = SaveOptionEnum.Insert;

        SortedList sl = tool.getPageValue();
        if (this.ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FBaseInfoId", Request["fbid"]);
            sl.Add("FIsdeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
        }
        sl.Add("FLeadId", fSpaclNumber);
        sl.Add("FLeadName", fSpaclName);
        if (!string.IsNullOrEmpty(t_FTypeId.SelectedValue))
            sl.Add("FTypeName", t_FTypeId.SelectedItem.Text.Trim());
        if (!string.IsNullOrEmpty(t_FListId.SelectedValue))
            sl.Add("FListName", t_FListId.SelectedItem.Text.Trim());
        if (!string.IsNullOrEmpty(t_FLevelId.SelectedValue))
            sl.Add("FLevelName", t_FLevelId.SelectedItem.Text);
        sl.Add("FisBase", 1);
        if (!string.IsNullOrEmpty(Request["fcid"]))
            sl.Add("FCertiId", Request["fcid"]);
        sl.Add("FAppDeptName", t_FAppDeptId.SelectedItem.Text);
        if (rc.SaveEBase(EntityTypeEnum.EbQualiCertiTrade, sl, "FID", so))
        {
            this.ViewState["FID"] = sl["FID"].ToString();
            tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }

    private string GetSpcalNumber()
    {
        StringBuilder sb = new StringBuilder();
        int iCount = this.cSpcal.Items.Count;
        if (iCount == 0)
        {
            return "NoCount";
        }
        else
        {
            for (int i = 0; i < iCount; i++)
            {
                if (this.cSpcal.Items[i].Selected == true)
                {
                    if (i == 0)
                    {
                        sb.Append(this.cSpcal.Items[i].Value.Trim());
                    }
                    else
                    {
                        if (sb.ToString().IndexOf(",") != -1)
                        {
                            if (!sb.ToString().EndsWith(","))
                            {
                                sb.Append("," + this.cSpcal.Items[i].Value.Trim());
                            }
                            else
                            {
                                sb.Append(this.cSpcal.Items[i].Value.Trim());
                            }
                        }
                        else
                        {
                            if (sb.Length > 0)
                                sb.Append("," + cSpcal.Items[i].Value.Trim());
                            else
                                sb.Append(cSpcal.Items[i].Value.Trim() + ",");
                        }
                        if (sb.ToString().EndsWith(","))
                        {
                            sb = sb.Remove(sb.Length - 1, 1);
                        }
                    }
                }
            }
        }
        return sb.ToString();
    }
    private string GetSpcalName()
    {
        StringBuilder sb = new StringBuilder();
        int iCount = this.cSpcal.Items.Count;
        if (iCount == 0)
        {
            return "NoCount";
        }
        else
        {
            for (int i = 0; i < iCount; i++)
            {
                if (this.cSpcal.Items[i].Selected == true)
                {
                    if (i == 0)
                    {
                        sb.Append(this.cSpcal.Items[i].Text.Trim());
                    }
                    else
                    {
                        if (sb.ToString().IndexOf(",") != -1)
                        {
                            if (!sb.ToString().EndsWith(","))
                            {
                                sb.Append("," + this.cSpcal.Items[i].Text.Trim());
                            }
                            else
                            {
                                sb.Append(this.cSpcal.Items[i].Text.Trim());
                            }
                            //sb.Append(this.cSpcal.Items[i].Text.Trim()); 

                        }
                        else
                        {
                            if (sb.Length > 0)
                            {
                                sb.Append("," + this.cSpcal.Items[i].Text.Trim());
                            }
                            else
                            {
                                sb.Append(this.cSpcal.Items[i].Text.Trim() + ",");
                            }
                        }
                    }
                }
            }
            if (sb.ToString().EndsWith(","))
            {
                sb = sb.Remove(sb.Length - 1, 1);
            }
        }
        return sb.ToString();
    }

    private void SetSpcalNumber(string fNumbers)
    {
        if (fNumbers == "")
        {
            return;
        }
        string[] strs = fNumbers.Split(',');
        if (strs != null && strs.Length > 0)
        {
            for (int i = 0; i < strs.Length; i++)
            {
                for (int j = 0; j < cSpcal.Items.Count; j++)
                {
                    if (strs[i].Trim() == cSpcal.Items[j].Value.Trim())
                    {
                        cSpcal.Items[j].Selected = true;
                    }
                }
            }
        }
    }
    protected void t_FListId_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.t_FTypeId.Items.Clear();
        this.cSpcal.Items.Clear();
        if (t_FListId.SelectedValue.Trim() == "134002003")
        {
            ShowTrade("134002002");
        }
        else
        {
            ShowTrade(this.t_FListId.SelectedValue.Trim());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
    protected void dbTypeId_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.t_FListId.Items.Clear();
        this.t_FTypeId.Items.Clear();
        this.cSpcal.Items.Clear();
        ShowList(dbTypeId.SelectedValue.Trim());
    }

    protected void t_FTypeId_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.cSpcal.Items.Clear();

        if (this.t_FListId.SelectedValue.Trim() == "134002002")
        {
            return;
        }
        ShowSpcal(t_FTypeId.SelectedValue.Trim());

    }
}
