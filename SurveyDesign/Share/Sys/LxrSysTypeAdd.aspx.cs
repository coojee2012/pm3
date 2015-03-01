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
using System.Xml.Linq;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using Approve.RuleApp;
using System.Web.UI.Adapters;
using System.Web.UI.MobileControls.Adapters;
using System.Web.UI.Design;
using System.Text;
using System.Data.SqlClient;

public partial class Admin_User_LxrSysTypeAdd : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            ControlBind();
            if (Request["FName"] != null)
            {
                this.t_FLinkName.Text = Request["FName"].ToString();
            }
            if (Request["fid"] != null)
            {
                this.ViewState["FID"] = Request["fid"];

                ShowInfo();
            }
        }
    }

    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FName,FNumber from CF_Sys_SystemName order by forder ");
        DataTable dt = rc.GetTable(sb.ToString());
        t_FSystemId.DataSource = dt;
        t_FSystemId.DataTextField = "FName";
        t_FSystemId.DataValueField = "FNumber";
        t_FSystemId.DataBind();

     

        string fDefaultManageDept = ComFunction.GetDefaultDept(); //获取默认管理部门
        if (fDefaultManageDept == null || fDefaultManageDept == "")
        {
            //如果配置文件中没有设置默认管理部门
            this.Response.Write("<center><font style='font-size:12px;color:red'>系统出错,请在配置文件中设置默认管理部门</font></center>");
            this.Response.End();
        }
        dt = rc.getAllupDeptId(fDefaultManageDept, 0, 3);
        t_FDeptId.DataSource = dt;
        t_FDeptId.DataTextField = "FName";
        t_FDeptId.DataValueField = "Fnumber";
        t_FDeptId.DataBind();



        dt = rc.getDicTbByFNumber("866");
        t_FType.DataSource = dt;
        t_FType.DataTextField = "FName";
        t_FType.DataValueField = "Fnumber";
        t_FType.DataBind();
        t_FType.Items.Insert(0, new ListItem("请选择", ""));
    }

    //显示数据
    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_City_Link  where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            
            t_FUserId.Value = EConvert.ToString(dt.Rows[0]["FUserId"]);
            if (t_FUserId.Value == "")
            {
                btnSelect.Enabled = true;
            }
            else
            {
                btnSelect.Enabled = false;
                ShowUserInfo();
            }

            if (t_FType.SelectedValue == "86602")
            {
                btnSelect.Visible = true;
            }
        }

    }

  
    //显示用户信息(管理部门)
    private void ShowUserInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select u.FID,r.FID rFID,u.FManageDeptId,u.FCompany,u.FLinkMan,u.FTel,r.FRoleId,");
        sb.Append("r.FName,r.FLockLabelNumber,r.FLockNumber,r.FBaseInfoId,r.FState,r.FSystemId,r.FEndTime ");
        sb.Append("from cf_sys_user u,cf_sys_userright r ");
        sb.Append("where u.fid=r.fuserid and isnull(u.fisdeleted,0)=0 and isnull(r.fisdeleted,0)=0 ");
        sb.Append("and u.ftype=1 ");//ftype=1：管理部门用户
        sb.Append("and u.FID=@FID ");
        DataTable dt = rc.GetTable(sb.ToString(), new SqlParameter("@FID", t_FUserId.Value));
        if (dt != null && dt.Rows.Count > 0)
        {
            pageTool tool = new pageTool(this, "tt_");
            tool.fillPageControl(dt.Rows[0]);
            if (t_FLinkName.Text == "")
            {
                t_FLinkName.Text = EConvert.ToString(dt.Rows[0]["FLinkMan"]);
            }
        }
    }


    //保存 
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        sl = tool.getPageValue();
        SaveOptionEnum so = SaveOptionEnum.Insert;

        if (this.ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"]);
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);

        }
        sl.Add("FUserId", t_FUserId.Value);

        if (rc.SaveEBase("CF_City_Link", sl, "FID", so))
        {
            tool.showMessage("保存成功!");
            this.ViewState["FID"] = sl["FID"].ToString();
            HSaveResult.Value = "1";
        }
        else
        {
            tool.showMessage("保存失败！");
        }
    }


    //保存 按钮点击事件
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
    //选择用户返回 刷新事件
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        ShowUserInfo();
    }
 
    //选择用户类型 事件
    protected void t_FType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (t_FType.SelectedValue == "86602")// 管理部门  
        {
            btnSelect.Visible = true;
        }
        else if (t_FType.SelectedValue == "86601")// 技术支持
        {
            btnSelect.Visible = false;
        }
    }
}
