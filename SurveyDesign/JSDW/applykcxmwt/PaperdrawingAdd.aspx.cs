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

public partial class PrjManage_ConstructionLicence_ApplyBaseinfo_PaperdrawingAdd : energyEntBasePage
{
    RCenter rq = new RCenter();
    RCenter rc = new RCenter();
    protected override void Page_Load(object sender, EventArgs e)
    {
        this.btnSave.Attributes.Add("OnClick", "if (check()){return true;}else{return false;}");
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            if (Request["fid"] != null && Request["fid"] != "")
            {
                string FID = GetFid();
                if (FID != null && FID != "")
                {
                    Pager1.Visible = true;
                    this.ViewState["FID"] = FID;
                    ShowInfo();
                    ShowOtherInfo();

                }
            }
            if (Request["isOver"] != null && Request["isOver"] == "1")
            {
                btnAdd.Attributes.Add("disabled", "true");
                btnSave.Enabled = false;
                Other_List.Columns[3].Visible = false;
            }
        }
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" fid='" + this.ViewState["FID"] + "' ");
        DataTable dt = rq.GetTable(EntityTypeEnum.EqPrjFile, "*", sb.ToString());
        pageTool tool = new pageTool(this.Page);
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            this.hidden_Fid.Value = dt.Rows[0]["FID"].ToString();

            this.labTitle.Text = rc.GetSignValue("select FRemark from cf_sys_prjlist where fid='" + dt.Rows[0]["FPRJLIstid"].ToString() + "'");
        }
        if (this.Other_List.Items.Count > 0)
        {
            this.t_FCount.Text = this.Other_List.Items.Count.ToString();
        }
        else
        {
            this.t_FCount.Text = "0";
        }
    }

    private void ShowOtherInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select FId,FFileName,FSize,FFilePath from CF_AppPrj_FileOther ");
        sb.Append(" where FPrjFileId='" + this.ViewState["FID"] + "' and FIsDeleted=0");
        sb.Append(" order by FCreateTime desc");
        this.Pager1.className = "RCenter";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 50;
        this.Pager1.controltopage = "Other_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
        if (this.Other_List.Items.Count > 0)
        {
            this.t_FCount.Text = this.Other_List.Items.Count.ToString();
        }
        else
        {
            this.t_FCount.Text = "0";
        }
    }

    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);

        if (this.Other_List.Items.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("fid='" + Request["fid"] + "' ");
            DataTable dt = rc.GetTable(EntityTypeEnum.EsPrjList, "*", sb.ToString());
            SortedList sl = tool.getPageValue();
            SaveOptionEnum so = SaveOptionEnum.Insert;
            if (this.ViewState["FID"] != null && this.ViewState["FID"].ToString() != "")
            {
                so = SaveOptionEnum.Update;
                sl.Add("FID", this.ViewState["FID"]);
            }
            else
            {
                sl.Add("FID", Guid.NewGuid().ToString());
                sl.Add("FAppId", this.Session["FAppId"]);
                sl.Add("FName", dt.Rows[0]["FFileName"].ToString());
                sl.Add("FManageType", dt.Rows[0]["FManageType"].ToString());
                sl.Add("FPrjListId", dt.Rows[0]["FId"].ToString());
                sl.Add("FCreateTime", DateTime.Now);
            }


            if (rq.SaveEBase(EntityTypeEnum.EqPrjFile, sl, "FID", so))
            {
                this.hiddle_IsSaveOk.Value = "1";
                this.ViewState["FID"] = sl["FID"].ToString();
                this.hidden_Fid.Value = sl["FID"].ToString();
                tool.showMessage("保存成功");
            }
            else
            {
                tool.showMessage("保存失败");
            }
        }
        else
        {
            tool.showMessage("请您上传相关文件后再点击保存按钮！");
        }

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (this.Other_List.Items.Count > 0)
        {
            if (this.t_FFileNo.Text == "" || this.t_FFileNo.Text == null)
            {
                tool.showMessage("请填写文件编号！");
                return;
            }
            else
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>window.returnValue=" + this.hiddle_IsSaveOk.Value + ";window.close();</script>");
            }
        }
        else
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>window.returnValue=" + this.hiddle_IsSaveOk.Value + ";window.close();</script>");
        }
    }

    private string GetFid()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("FPrjListId='" + Request["fid"] + "' and FAppId='" + this.Session["FAppId"] + "'");
        string fid = rq.GetSignValue(EntityTypeEnum.EqPrjFile, "fid", sb.ToString());
        return fid;
    }
    protected void Other_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string FFilePath = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string sUrl = ProjectBLL.RBase.GetSysObjectName("FileServerPath") + FFilePath.Replace("../..", "");
            e.Item.Cells[1].Text = "<a href='" + sUrl + "' target='_blank'>" + e.Item.Cells[1].Text + "</a>";

            Button lb = e.Item.Cells[3].Controls[1] as Button;
            lb.Attributes.Add("onclick", "return confirm('确认要删除么?')");

        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowOtherInfo();
    }
    protected void Other_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            if (e.CommandName == "Delete")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" delete from CF_AppPrj_FileOther");
                sb.Append(" where fid='" + fid + "'");
                rq.PExcute(sb.ToString());
                ShowOtherInfo();
            }
        }
    }
}
