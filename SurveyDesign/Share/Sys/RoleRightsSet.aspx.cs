using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.Common;

public partial class Share_Sys_RoleRightsSet : System.Web.UI.Page
{
    Share sh = new Share();
    DataTable menu = new DataTable();//菜单
    string FMenuList = "";//角色的菜单权限
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            showInfo();
        }
    }
    //显示
    private void showInfo()
    {
        string RoleId = Request.QueryString["RoleId"];
        if (!string.IsNullOrEmpty(RoleId))
        {

            #region 初始化
            //菜单
            StringBuilder sb = new StringBuilder();
            sb.Append("select FName,FNumber,FLevel,FParent ");
            sb.Append("from CF_Sys_tree  ");
            sb.Append("order by Forder,FNumber ");
            menu = sh.GetTable(sb.ToString());

            //角色的菜单权限
            FMenuList = sh.GetSignValue("select FMenuList from CF_Sys_MenuRole where fid='" + RoleId + "' ");
            #endregion

            DataRow[] dr = menu.Select("1=2");
            LoadNode(ref dr, "");
            PopeTreeMain.DataSource = dr.AsEnumerable().Select(t => new { FName = t["FName"], FNumber = t["FNumber"], FLevel = t["FLevel"], FParent = t["FParent"] });
            PopeTreeMain.DataBind();
        }
    }

    /// <summary>
    /// 递归菜单（模块列表）
    /// </summary>
    /// <param name="node">子菜单</param>
    /// <param name="MtID">子菜单上级ID</param>
    private void LoadNode(ref DataRow[] nodes, string FParent)
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(FParent))
            sb.Append("FParent='" + FParent + "'");
        else
            sb.Append("FLevel=1");
        DataRow[] dt = menu.Select(sb.ToString());
        foreach (DataRow item in dt)
        {
            string str = "";
            for (int i = 1; i < EConvert.ToInt(item["FLevel"]); i++)
            {
                str += "----";
            }
            item["FName"] = str + item["FName"];

            DataRow[] ddd = new DataRow[nodes.Length + 1];
            Array.Copy(nodes, ddd, nodes.Length);
            ddd[nodes.Length] = item;
            nodes = ddd;
            LoadNode(ref nodes, item["FNumber"].ToString());
        }
    }


    //保存
    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        string RoleId = Request.QueryString["RoleId"];
        if (!string.IsNullOrEmpty(RoleId))
        {
            string str = "";
            for (int i = 0; i < PopeTreeMain.Items.Count; i++)
            {
                CheckBox CB_IsPope = (CheckBox)PopeTreeMain.Items[i].FindControl("CB_IsPope");
                if (CB_IsPope.Checked)
                {
                    Label la = (Label)PopeTreeMain.Items[i].FindControl("la_FNumber");
                    if (!string.IsNullOrEmpty(str))
                        str += ",";
                    str += la.Text;
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("update CF_Sys_MenuRole ");
            sb.Append("set FMenuList='" + str + "' ");
            sb.Append("where fid='" + RoleId + "'");
            if (sh.PExcute(sb.ToString()))
                tool.showMessage("保存成功");
            else
                tool.showMessage("保存失败");
        }
        else
            tool.showMessage("保存失败");

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void PopeTreeMain_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FNumber = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
            CheckBox CB_IsPope = (CheckBox)e.Item.FindControl("CB_IsPope");
            if (CB_IsPope != null)
            {
                if (!string.IsNullOrEmpty(FMenuList) && FMenuList.Split(',').Contains(FNumber))
                {
                    CB_IsPope.Checked = true;
                }
            }

        }
    }
}
