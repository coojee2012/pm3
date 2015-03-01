using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Approve.Common;
using System.Text;
using System.Data;
using Approve.EntityBase;
using System.Collections;
using System.Data.SqlClient;

public partial class Share_User_EntTypesSel : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            ShowInfo();
        }
    }

    void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fname,fcnumber fnumber from cf_sys_dic where fisdeleted=0 ");
        sb.Append(" and fparentId=100 order by fnumber");
        DataTable dt = sh.GetTable(sb.ToString());
        t_FEntTypes.DataSource = dt;
        t_FEntTypes.DataTextField = "FName";
        t_FEntTypes.DataValueField = "FNumber";
        t_FEntTypes.DataBind();
        SetMenuRoleList(t_FEntTypes);
    }
    void SetMenuRoleList(CheckBoxList menuList)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["FEntTypes"])
            && (menuList != null && menuList is CheckBoxList))
        {
            string menuIds = Request.QueryString["FEntTypes"];
            string[] menus = menuIds.Split(',');
            for (int i = 0; i < menus.Length; i++)
            {
                ListItem item = menuList.Items.FindByValue(menus[i]);
                if (item != null)
                    item.Selected = true;
            }
        }
    }
    string GetMenuRoleList(CheckBoxList menuList)
    {
        StringBuilder sb = new StringBuilder();
        if (menuList != null && menuList is CheckBoxList)
        {
            for (int i = 0; i < menuList.Items.Count; i++)
            {
                if (menuList.Items[i].Selected)
                {
                    if (sb.Length > 0)
                        sb.Append(",");
                    sb.Append(menuList.Items[i].Value);
                }
            }
        }
        return sb.ToString();
    }
    //保存
    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        ArrayList arr = new ArrayList();
        if (t_FEntTypes.SelectedIndex == -1)
        {
            tool.showMessage("请选择菜单角色");
            return;
        }
        string FMenuRoleId = GetMenuRoleList(t_FEntTypes);
        tool.showMessageAndRunFunction("本页面关闭后，在‘企业用户’页面点击保存按钮，即可保存企业类型信息！", "window.returnValue='" + FMenuRoleId + "';window.close();");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}
