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

public partial class Share_Sys_SysNameAdd : System.Web.UI.Page
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
                hidden_Fid.Value = ViewState["FID"].ToString();
                ShowInfo();
            }
        }
    }

    //显示基本信息
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Sys_System where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            img_url.Src = dt.Rows[0]["FPic"].ToString() ;
        }
    }


    //保存
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);

        #region 验证
        DataTable dt = sh.GetTable("select * from CF_Sys_System where fnumber='" + t_FNumber.Text + "' and fid<>'" + EConvert.ToString(ViewState["FID"]) + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.showMessage("系统编号已存在！");
            t_FNumber.Focus();
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
        if (sh.SaveEBase("CF_Sys_System", sl, "FID", so))
        {
            tool.showMessage("保存成功");
            HSaveResult.Value = "1";
            ViewState["FID"] = sl["FID"].ToString();
            hidden_Fid.Value = sl["FID"].ToString();
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        img_url.Src = t_FPic.Value;
        SaveInfo();
    }
}
