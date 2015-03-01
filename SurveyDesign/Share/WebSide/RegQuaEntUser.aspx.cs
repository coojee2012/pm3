using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Approve.EntityBase;
using System.Collections;
using System.Data;
using Approve.Common;

public partial class Share_WebSide_RegEntUser : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //保存
    private void SaveInfo()
    {
        string FSystemId = Request.QueryString["FSystemId"];
        pageTool tool = new pageTool(this.Page);
        #region 验证
        t_FManageDeptId.Value = this.Govdept1.FNumber;//主管部门number
        if (t_FManageDeptId.Value == "")
        {
            Govdept1.FNumber = ComFunction.GetDefaultDept();
            tool.showMessage("请选择主管部门");
            return;
        }
        if (!string.IsNullOrEmpty(t_FCompany.Text))
        {
            DataTable dt = sh.GetTable("select fid from CF_User_Reg where FCompany='" + t_FCompany.Text + "' and FSystemId='" + FSystemId + "' and fid<>'" + EConvert.ToString(ViewState["FID"]) + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("企业名称已存在！");
                t_FCompany.Focus();
                return;
            }
        }
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            DataTable dt = sh.GetTable("select fid from CF_User_Reg where FName='" + t_FName.Text + "' and FSystemId='" + FSystemId + "' and fid<>'" + EConvert.ToString(ViewState["FID"]) + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.showMessage("用户名已存在！");
                t_FName.Focus();
                return;
            }
        }
        #endregion
        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = tool.getPageValue();

        sl.Add("FID", Guid.NewGuid().ToString());
        sl.Add("FIsDeleted", 0);
        sl.Add("FBaseInfoId", Guid.NewGuid().ToString());
        sl.Add("FRFID", Guid.NewGuid().ToString());//userRight.FID
        sl.Add("FCreateTime", DateTime.Now);
        sl.Add("FType", 2);//企业用户
        sl.Add("FEndTime", DateTime.Now.AddYears(10));
        sl.Add("FState", 0);

        if (sh.SaveEBase("CF_User_Reg", sl, "FID", so))
        {
            Response.Redirect("RegEntUserSuccess.aspx?regID=" + sl["FID"].ToString());
        }
        else
        {
            tool.showMessage("注册失败");
        }
    }

    protected void btnREG_Click(object sender, EventArgs e)
    {
        string fDeptNumber = this.Govdept1.FNumber;
        SaveInfo();
        this.Govdept1.FNumber = fDeptNumber;
    }


}
