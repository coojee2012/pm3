using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;

public partial class Admin_main_HolidaysAdd : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            if (Request["fid"] != null && Request["fid"] != "")
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
        sb.Append("select FDate from CF_Sys_Holidays where fistrue=1 and fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = new SortedList();
        if (this.ViewState["FID"]!=null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
            sl.Add("FDate", Convert.ToDateTime(t_FDate.Text).ToShortDateString());
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FDate", Convert.ToDateTime(t_FDate.Text).ToShortDateString());
            sl.Add("FIsTrue", 1);
        }
        //不能添加或修改成相同的日期
        if (findDate(t_FDate.Text))
        {
            return;
        }
        if (sh.SaveEBase(EntityTypeEnum.EsHolidays, sl, "FID", so))
        {
            tool.showMessage("保存成功");
            HSaveResult.Value = "1";
        }
        else
        {
            tool.showMessage("保存失败");
        }

    }
    protected bool findDate(string date)
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select count(1) from CF_Sys_Holidays where FDate= '" + Convert.ToDateTime(t_FDate.Text).ToShortDateString() + "'");
        sb.Append(" and fistrue=1");
        int count = Convert.ToInt32(sh.GetSignValue(sb.ToString()));
        if (count > 0)
        {
            tool.showMessage(Convert.ToDateTime(t_FDate.Text).ToShortDateString() + "已经添加过了");
            return true;
        }
        else
        {
            return false;
        }
    }
}
