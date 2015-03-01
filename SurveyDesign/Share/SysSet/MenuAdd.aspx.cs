using System;
using System.Linq;
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
using ProjectData;
public partial class Admin_main_MenuAdd : Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            this.btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?')");
            ControlBind();
            if (Request["fid"] != null && Request["fid"] != null)
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
            }
            else
            {
                ShowColLevel("1");
            }
        }
    }

    private void showManageType(string FSystemID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fid,Fname,fnumber from CF_Sys_ManageType where fisdeleted=0 and FSystemId='" + FSystemID + "' order by forder,fcreatetime desc");
        DataTable dt = rc.GetTable(sb.ToString());
        this.t_FManageTypeId.DataSource = dt;
        this.t_FManageTypeId.DataTextField = "FName";
        this.t_FManageTypeId.DataValueField = "FNumber";
        this.t_FManageTypeId.DataBind();
        this.t_FManageTypeId.Items.Insert(0, new ListItem("管理部门使用业务", "1"));
    }
    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        //sb.Append("select fid,Fname,fnumber from CF_Sys_ManageType where fisdeleted=0 order by forder,fcreatetime desc");
        //DataTable dt = rc.GetTable(sb.ToString());
        //this.t_FManageTypeId.DataSource = dt;
        //this.t_FManageTypeId.DataTextField = "FName";
        //this.t_FManageTypeId.DataValueField = "FNumber";
        //this.t_FManageTypeId.DataBind();
        //this.t_FSystemId.Items.Insert(0, new ListItem("管理部门使用业务", "1"));

        sb.Remove(0, sb.Length);
        sb.Append("select fid,fname,fnumber from CF_Sys_SystemName where fisdeleted=0 order by forder");
        DataTable dt = rc.GetTable(sb.ToString());
        this.t_FSystemId.DataSource = dt;
        this.t_FSystemId.DataTextField = "FName";
        this.t_FSystemId.DataValueField = "Fnumber";
        this.t_FSystemId.DataBind();
        t_FSystemId.Items.Insert(0, new ListItem("所有", "0"));

        //sb.Remove(0, sb.Length);
        //sb.Append("select fid,fname,fnumber from cf_sys_role where fisdeleted=0 order by forder, ftime desc");
        //dt = rc.GetTable(sb.ToString());
        //this.t_FRoleId.DataSource = dt;
        //this.t_FRoleId.DataTextField = "FName";
        //this.t_FRoleId.DataValueField = "Fnumber";
        //this.t_FRoleId.DataBind();

        sb.Remove(0, sb.Length);
        sb.Append("select fid,Fname,fnumber from cf_sys_role where fisdeleted=0  and ftypeid=2 order by forder,ftime desc");
        dt = rc.GetTable(sb.ToString());
        this.t_FRoleId.DataSource = dt;
        this.t_FRoleId.DataTextField = "FName";
        this.t_FRoleId.DataValueField = "FNumber";
        this.t_FRoleId.DataBind();

        sb.Remove(0, sb.Length);
        sb.Append("select fname,fnumber from cf_sys_menu where flevel=1 and fisdeleted=0 order by forder,fcreatetime desc");
        dt = rc.GetTable(sb.ToString());
        this.t_FParentId1.DataSource = dt;
        this.t_FParentId1.DataTextField = "FName";
        this.t_FParentId1.DataValueField = "FNumber";
        this.t_FParentId1.Items.Insert(0, new ListItem("请选择", ""));
        this.t_FParentId1.DataBind();
    }



    private void ColDataBind(DropDownList drop, int fLevel, string fParent)
    {
        drop.Items.Clear();
        if (fParent == null || fParent == "")
        {
            drop.Items.Insert(0, new ListItem("请选择", ""));
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("FLevel=" + fLevel + " and FParentId='" + fParent + "' and FIsDeleted=0 order by forder,fcreatetime desc");
        DataTable dt = rc.GetTable(EntityTypeEnum.EsMenu, "FNumber,FName", sb.ToString());
        drop.DataSource = dt;
        drop.DataTextField = "FName";
        drop.DataValueField = "FNumber";
        drop.DataBind();
        drop.Items.Insert(0, new ListItem("请选择", ""));
    }

    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from cf_sys_menu where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            //帮助信息
            ProjectDB db = new ProjectDB();
            string FNumber = dt.Rows[0]["FNumber"].ToString().Trim();
            CF_Sys_HelpMsg v = db.CF_Sys_HelpMsg.Where(t => t.FLinkNumber == FNumber).FirstOrDefault();
            if (v != null)
            {
                a_Help.Text = "<a href=\"javascript:showAddWindow('../Help/edit.aspx?FID=" + v.FID + "',600,600);\" class=\"a_2\">修改帮助信息</a>";
            }
            else
            {
                a_Help.Text = "<a href=\"javascript:showAddWindow('../Help/edit.aspx?FLinkNumber=" + FNumber + "',600,600);\" class=\"a_1\">添加帮助信息</a>";
            }


            t_FSystemId.SelectedIndex = t_FSystemId.Items.IndexOf(t_FSystemId.Items.FindByValue(dt.Rows[0]["FSystemId"].ToString()));
            this.showManageType(t_FSystemId.SelectedValue);
            t_FManageTypeId.SelectedIndex = t_FManageTypeId.Items.IndexOf(t_FManageTypeId.Items.FindByValue(dt.Rows[0]["FManageTypeId"].ToString()));
            ShowMenuRole();

            tool.fillPageControl(dt.Rows[0]);
            ShowColLevel(dt.Rows[0]["flevel"].ToString());
            string fColNumber = dt.Rows[0]["FNumber"].ToString().Trim();

            string fParentNumber1 = "";
            string fParentNumber2 = "";
            string fParentNumber3 = "";
            string fParentNumber4 = "";
            string fParentNumber5 = "";
            string fParentNumber6 = "";


            switch (dt.Rows[0]["flevel"].ToString().Trim())
            {
                case "2":
                    fParentNumber1 = rc.GetParentMenuNumber(fColNumber);
                    this.t_FParentId1.SelectedValue = fParentNumber1;
                    break;

                case "3":
                    fParentNumber1 = rc.GetParentMenuNumber(fColNumber);
                    fParentNumber2 = rc.GetParentMenuNumber(fParentNumber1);
                    this.t_FParentId1.SelectedValue = fParentNumber2;
                    ColDataBind(this.t_FParentId2, 2, this.t_FParentId1.SelectedValue.Trim());
                    this.t_FParentId2.SelectedValue = fParentNumber1;
                    break;

                case "4":
                    fParentNumber1 = rc.GetParentMenuNumber(fColNumber);
                    fParentNumber2 = rc.GetParentMenuNumber(fParentNumber1);
                    fParentNumber3 = rc.GetParentMenuNumber(fParentNumber2);

                    this.t_FParentId1.SelectedValue = fParentNumber3;
                    ColDataBind(this.t_FParentId2, 2, this.t_FParentId1.SelectedValue.Trim());
                    this.t_FParentId2.SelectedValue = fParentNumber2;

                    ColDataBind(this.t_FParentId3, 3, this.t_FParentId2.SelectedValue.Trim());
                    this.t_FParentId3.SelectedValue = fParentNumber1;
                    break;

                case "5":
                    fParentNumber1 = rc.GetParentMenuNumber(fColNumber);
                    fParentNumber2 = rc.GetParentMenuNumber(fParentNumber1);
                    fParentNumber3 = rc.GetParentMenuNumber(fParentNumber2);
                    fParentNumber4 = rc.GetParentMenuNumber(fParentNumber3);

                    this.t_FParentId1.SelectedValue = fParentNumber4;
                    ColDataBind(this.t_FParentId2, 2, this.t_FParentId1.SelectedValue.Trim());
                    this.t_FParentId2.SelectedValue = fParentNumber3;

                    ColDataBind(this.t_FParentId3, 3, this.t_FParentId2.SelectedValue.Trim());
                    this.t_FParentId3.SelectedValue = fParentNumber2;

                    ColDataBind(this.t_FParentId4, 4, this.t_FParentId3.SelectedValue.Trim());
                    this.t_FParentId4.SelectedValue = fParentNumber1;
                    break;

                case "6":
                    fParentNumber1 = rc.GetParentMenuNumber(fColNumber);
                    fParentNumber2 = rc.GetParentMenuNumber(fParentNumber1);
                    fParentNumber3 = rc.GetParentMenuNumber(fParentNumber2);
                    fParentNumber4 = rc.GetParentMenuNumber(fParentNumber3);
                    fParentNumber5 = rc.GetParentMenuNumber(fParentNumber4);

                    this.t_FParentId1.SelectedValue = fParentNumber5;

                    ColDataBind(this.t_FParentId2, 2, this.t_FParentId1.SelectedValue.Trim());
                    this.t_FParentId2.SelectedValue = fParentNumber4;

                    ColDataBind(this.t_FParentId3, 3, this.t_FParentId2.SelectedValue.Trim());
                    this.t_FParentId3.SelectedValue = fParentNumber3;

                    ColDataBind(this.t_FParentId4, 4, this.t_FParentId3.SelectedValue.Trim());
                    this.t_FParentId4.SelectedValue = fParentNumber2;

                    ColDataBind(this.t_FParentId5, 5, this.t_FParentId4.SelectedValue.Trim());
                    this.t_FParentId5.SelectedValue = fParentNumber1;
                    break;

                case "7":
                    fParentNumber1 = rc.GetParentMenuNumber(fColNumber);
                    fParentNumber2 = rc.GetParentMenuNumber(fParentNumber1);
                    fParentNumber3 = rc.GetParentMenuNumber(fParentNumber2);
                    fParentNumber4 = rc.GetParentMenuNumber(fParentNumber3);
                    fParentNumber5 = rc.GetParentMenuNumber(fParentNumber4);
                    fParentNumber6 = rc.GetParentMenuNumber(fParentNumber5);

                    this.t_FParentId1.SelectedValue = fParentNumber6;

                    ColDataBind(this.t_FParentId2, 2, this.t_FParentId1.SelectedValue.Trim());
                    this.t_FParentId2.SelectedValue = fParentNumber5;

                    ColDataBind(this.t_FParentId3, 3, this.t_FParentId2.SelectedValue.Trim());
                    this.t_FParentId3.SelectedValue = fParentNumber4;

                    ColDataBind(this.t_FParentId4, 4, this.t_FParentId3.SelectedValue.Trim());
                    this.t_FParentId4.SelectedValue = fParentNumber3;

                    ColDataBind(this.t_FParentId5, 5, this.t_FParentId4.SelectedValue.Trim());
                    this.t_FParentId5.SelectedValue = fParentNumber2;

                    ColDataBind(this.t_FParentId6, 6, this.t_FParentId5.SelectedValue.Trim());
                    this.t_FParentId6.SelectedValue = fParentNumber1;
                    break;

                default:
                    break;
            }

            //判断是不是地图菜单，地图菜单得加配置项            

            string m = "4500703,";
            if (m.Split(',').Count(t => dt.Rows[0]["FNumber"].ToString().Contains(t)) > 0)
            {
                div_Map.Visible = true;
            }

        }
    }
    private void ShowColLevel(string fLevel)
    {
        int colLevel = EConvert.ToInt(fLevel.Trim());
        for (int i = 1; i < colLevel; i++)
        {
            this.Page.FindControl("isShowTr" + i).Visible = true;
        }
        for (int i = colLevel; i <= 6; i++)
        {
            this.Page.FindControl("isShowTr" + i).Visible = false;
        }
    }
    private string GetParentNumber()
    {
        //if (this.isShowTr6.Visible == true)
        //{
        //    return this.t_FParentId6.SelectedValue.Trim();
        //}
        //if (this.isShowTr5.Visible == true)
        //{
        //    return this.t_FParentId5.SelectedValue.Trim();
        //}
        //if (this.isShowTr4.Visible == true)
        //{
        //    return this.t_FParentId4.SelectedValue.Trim();
        //}
        //if (this.isShowTr3.Visible == true)
        //{
        //    return this.t_FParentId3.SelectedValue.Trim();
        //}
        //if (this.isShowTr2.Visible == true)
        //{
        //    return this.t_FParentId2.SelectedValue.Trim();
        //}
        //if (this.isShowTr1.Visible == true)
        //{
        //    return this.t_FParentId1.SelectedValue.Trim();
        //}
        for (int i = 6; i >= 1; i--)
        {
            if (this.Page.FindControl("isShowTr" + i).Visible)
            {
                DropDownList ddlParent = Page.FindControl("t_FParentId" + i) as DropDownList;
                if (ddlParent != null)
                    return ddlParent.SelectedValue.Trim();
            }
        }
        return "";
    }



    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = tool.getPageValue();

        string sONumber = "";

        string sql = "select count(*) from cf_sys_menu where fnumber='" + this.t_FNumber.Text.Trim() + "' ";

        if (this.ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
            sONumber = rc.GetSignValue("select fnumber from cf_sys_menu where fid='" + this.ViewState["FID"].ToString() + "'");

            sql += " and fid<>'" + this.ViewState["FID"].ToString() + "'";
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
        }

        int iCount = rc.GetSQLCount(sql);
        if (iCount >= 1)
        {
            tool.showMessage("编码重复");
            return;
        }

        sl.Add("FParentId", GetParentNumber());
        sl.Add("FMapPic", t_FMapPic.Text);
        sl.Add("FMapZoom", t_FMapZoom.Text);
        if (rc.SaveEBase(EntityTypeEnum.EsMenu, sl, "FID", so))
        {
            ViewState["Number"] = t_FNumber.Text.Trim();

            if (sONumber != "")
            {
                rc.PExcute("update CF_Sys_Menu set FParentId='" + this.t_FNumber.Text.Trim() + "' where FParentId='" + sONumber + "'");
            }
            if (cbIsReLoad.Checked == true)
            {
                tool.showMessageAndRunFunction("保存成功", "setLeftMenu('" + this.t_FNumber.Text.Trim() + "');");
            }
            else
            {
                tool.showMessage("保存成功");
            }
            this.ViewState["FID"] = sl["FID"].ToString();
        }
        else
        {
            tool.showMessage("保存失败");
        }
        Tools.DataCache.RemoveCache("Menu");
    }
    private void DelInfo()
    {
        pageTool tool = new pageTool(this.Page);
        if (this.ViewState["FID"] == null)
        {
            tool.showMessage("请选择要删除的栏目");
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" fid='" + this.ViewState["FID"].ToString() + "'");
        string fNumber = rc.GetSignValue(EntityTypeEnum.EsMenu, "FNumber", sb.ToString());
        if (fNumber == null || fNumber == "")
        {
            tool.showMessage("要删除的栏目不存在");
            return;
        }
        if (rc.DelMenu(fNumber))
        {
            //parent.frames["left"].document.location.reload();
            //tool.showMessageAndRunFunction("删除成功", "pageReload();");
            if (this.cbIsReLoad.Checked == true)
            {
                tool.showMessageAndRunFunction("删除成功", "setLeftMenu(" + "" + ");");
            }
            else
            {
                tool.showMessage("删除成功");
            }
        }
        else
        {
            tool.showMessage("删除失败,请重试");
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        this.t_FName.Text = "";
        this.t_FUrl.Text = "";
        this.t_FQUrl.Text = "";

        if (this.ViewState["FID"] != null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" fid='" + this.ViewState["FID"].ToString() + "'");
            DataTable dt = rc.GetTable(EntityTypeEnum.EsMenu, "FNumber,FLevel", "FId='" + this.ViewState["FID"] + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                int colLevel = EConvert.ToInt(dt.Rows[0]["FLevel"].ToString());
                if (colLevel > 1)
                {
                    string fNumber = dt.Rows[0]["FNumber"].ToString().Trim();
                    if (colLevel < 7)
                    {
                        ColDataBind((DropDownList)this.Page.FindControl("t_FParentId" + (colLevel)), colLevel, ((DropDownList)this.Page.FindControl("t_FParentId" + (colLevel - 1))).SelectedValue.Trim());
                        ((DropDownList)this.Page.FindControl("t_FParentId" + (colLevel))).SelectedValue = fNumber;
                    }
                }
            }
        }

        this.ViewState["FID"] = null;
    }
    protected void t_FLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowColLevel(t_FLevel.SelectedValue);
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        DelInfo();
    }
    protected void t_FParentId1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ColDataBind(this.t_FParentId2, 2, this.t_FParentId1.SelectedValue.Trim());
    }
    protected void t_FParentId2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ColDataBind(this.t_FParentId3, 3, this.t_FParentId2.SelectedValue.Trim());
    }

    protected void t_FParentId3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ColDataBind(this.t_FParentId4, 4, this.t_FParentId3.SelectedValue.Trim());
    }
    protected void t_FParentId4_SelectedIndexChanged(object sender, EventArgs e)
    {
        ColDataBind(this.t_FParentId5, 5, this.t_FParentId4.SelectedValue.Trim());
    }
    protected void t_FParentId5_SelectedIndexChanged(object sender, EventArgs e)
    {
        ColDataBind(this.t_FParentId6, 6, this.t_FParentId5.SelectedValue.Trim());
    }
    protected void t_FSystemId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.t_FSystemId.SelectedValue != "")
        {
            this.showManageType(this.t_FSystemId.SelectedValue);
            ShowMenuRole();
        }
    }
    protected void t_FManageTypeId_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowMenuRole();
    }
    void ShowMenuRole()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fid,Fname,fnumber from cf_sys_role where fisdeleted=0 ");
        if (!string.IsNullOrEmpty(t_FSystemId.SelectedValue) && t_FSystemId.SelectedValue != "0")
            sb.Append(" and FSystemId='" + t_FSystemId.SelectedValue + "' ");
        if (t_FManageTypeId.SelectedIndex == 0)
            sb.Append(" and fMTypeId=100 ");
        else
            sb.Append(" and fMTypeId=110 ");
        sb.Append("and ftypeid=2 order by forder,ftime desc");
        DataTable dt = rc.GetTable(sb.ToString());
        this.t_FRoleId.DataSource = dt;
        this.t_FRoleId.DataTextField = "FName";
        this.t_FRoleId.DataValueField = "FNumber";
        this.t_FRoleId.DataBind();
    }
}
