using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.Common;
using System.Text;
using System.Data;
using Approve.RuleCenter;
using Approve.EntityBase;
using System.Collections;

public partial class Share_Sys_BatchAdd : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            if (!string.IsNullOrEmpty(Request.QueryString["FID"]))
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
            }
        }
    }

    //显示基本信息
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Sys_BatchNo where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
        }
    }


    //保存
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);

        #region 验证
        DataTable dt = sh.GetTable("select * from CF_Sys_BatchNo where FName='" + t_FName.Text + "' and fid<>'" + EConvert.ToString(ViewState["FID"]) + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.showMessage("批次名称已存在！");
            t_FName.Focus();
            return;
        }
        #endregion

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
            sl.Add("FIsDeleted", 0);
        }
        if (sh.SaveEBase(EntityTypeEnum.SHsBatchNo, sl, "FID", so))
        {
            tool.showMessage("保存成功");
            HSaveResult.Value = "1";
            ViewState["FID"] = sl["FID"].ToString();
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
}
