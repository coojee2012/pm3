using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Approve.RuleCenter;
using Approve.EntityBase;
using System.Linq;

public partial class Share_Main_aLeft : System.Web.UI.Page
{
    Share sh = new Share();
    DataTable menu = new DataTable();//菜单
    DataTable menu1 = new DataTable();//系统菜单
    string FMenuList = "";//角色的菜单权限
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            showInfo();
            RegisterStartupScript("", "<script> showMenu();</script>");
            l_title.Text = getTitleName(Request.QueryString["HKINDID"]);
        }
    }

    //显示
    private void showInfo()
    {
        string RoleId = sh.GetSignValue("select FMenuRoleId from CF_Sys_User where fid='" + Session["SH_UserID"] + "'");
        if (!string.IsNullOrEmpty(RoleId) || EConvert.ToString(Session["SH_IsAdmin"]) == "1")
        {
            #region 初始化
            //菜单
            StringBuilder sb = new StringBuilder();
            sb.Append("select FID,fname,fnumber,FAdminUrl,FTarget,FPicName,FSelcePicName,FExpPicName,FType,FLevel,FParent,fisshow,forder,fkindid  ");
            sb.Append("from CF_Sys_tree where 1=1 ");
            if (this.Session["SH_IsAdmin"] != null && this.Session["SH_IsAdmin"].ToString() != "1")
            {
                sb.Append(" and fnumber in ");
                sb.Append(" (select fcolnumber from CF_Sys_RoleRight where froleid='" + RoleId + "' and ftype=1 and fisdeleted=0)");
            }
            sb.Append("order by Forder,FNumber ");
            menu = sh.GetTable(sb.ToString());

            //tree菜单
            sb.Remove(0, sb.Length);
            sb.Append(" select csm.fid,csm.fname,csm.fnumber,csm.fparentid,csm.FSelcePicName,csm.FPicName,");
            sb.Append(" csm.FUrl,csm.FTarget,csm.forder,csm.fcreatetime,csm.flevel,csm.fisdeleted,csm.FIsDis ");
            sb.Append(" from cf_sys_menu csm ");
            sb.Append(" where fisdeleted=0 ");
            menu1 = sh.GetTable(sb.ToString());
            //角色的菜单权限
            FMenuList = sh.GetSignValue("select FMenuList from CF_Sys_MenuRole where FNumber='" + RoleId + "' ");
            #endregion
        }
        else
        {
            Response.Write("没有权限");
            Response.End();
        }


        switch (Request.QueryString["HKINDID"])
        {
            case "lmcd":
                showColInfo(this.TreeView1.Nodes, "");
                break;
            case "xtcd":
                ShowSysColInfo(this.TreeView1.Nodes, "");
                break;
            default:
                LoadNode(this.TreeView1.Nodes, EConvert.ToString(Request.QueryString["HKINDID"]) == "" ? "155001" : EConvert.ToString(Request.QueryString["HKINDID"]), "");
                break;
        }


    }

    /// <summary>
    /// 递归菜单（模块列表）
    /// </summary>
    /// <param name="node">子菜单</param>
    /// <param name="MtID">子菜单上级ID</param>
    private void LoadNode(TreeNodeCollection tree, string fKindId, string FParent)
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(FParent))
            sb.Append("FParent='" + FParent + "'  and FKindId='" + fKindId + "' and fisshow=0 ");
        else
            sb.Append("FLevel=1 and FKindId='" + fKindId + "' and  fisshow=0 ");

        DataRow[] dt = menu.Select(sb.ToString(), "forder,fnumber");
        int i = 0;
        foreach (DataRow item in dt)
        {
            if (!string.IsNullOrEmpty(FMenuList) && FMenuList.Split(',').Contains(item["FNumber"].ToString()) || EConvert.ToString(Session["SH_IsAdmin"]) == "1" || EConvert.ToString(Session["SH_IsAdmin"]) == "0")
            {
                TreeNode node = node = new TreeNode();
                node.Text = "<span class='tree_img_b'>" + item["FName"].ToString() + "</span>";
                node.Value = item["FNumber"].ToString();
                node.NavigateUrl = string.IsNullOrEmpty(item["FAdminUrl"].ToString()) ? "javascript:void(0);" : item["FAdminUrl"].ToString();
                node.Target = item["FTarget"].ToString();
                //node.ImageUrl = item["FPicName"].ToString();
                if (i > 0)
                    node.Expanded = false;
                tree.Add(node);
                LoadNode(node.ChildNodes, fKindId, item["FNumber"].ToString());
                i++;
            }
        }

    }

    /// <summary>
    /// 递归菜单（栏目设置）
    /// </summary>
    /// <param name="node">子菜单</param>
    /// <param name="MtID">子菜单上级ID</param>
    private void showColInfo(TreeNodeCollection tree, string FParent)
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(FParent))
            sb.Append("FParent='" + FParent + "'");
        else
            sb.Append("FLevel=1");

        DataRow[] dt = menu.Select(sb.ToString(), "forder,fnumber");

        foreach (DataRow item in dt)
        {
            TreeNode node = node = new TreeNode();
            node.Text = "&nbsp;&nbsp;" + item["FName"].ToString();
            node.Value = item["FNumber"].ToString();
            node.NavigateUrl = "../SysSet/TreeMain.aspx?fid=" + item["FID"].ToString();
            node.Target = item["FTarget"].ToString();
            tree.Add(node);
            showColInfo(node.ChildNodes, item["FNumber"].ToString());
            node.Expanded = false;
        }
    }
    /// <summary>
    /// 递归菜单（系统菜单设置）
    /// </summary>
    /// <param name="tree"></param>
    /// <param name="FParent"></param>
    private void ShowSysColInfo(TreeNodeCollection TreeNode, string FParentId)
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(FParentId))
            sb.Append("FParentId='" + FParentId + "'");
        else
            sb.Append("FLevel=1");

        DataRow[] dt = menu1.Select(sb.ToString(), "forder,fnumber");
        int i = 0;
        foreach (DataRow item in dt)
        {
            TreeNode node = node = new TreeNode();
            node.Text = "&nbsp;&nbsp;" + item["FName"].ToString();
            node.Value = item["FNumber"].ToString();
            if (item["FIsDis"].ToString() != "1") node.Text = "<font color='Gray'>&nbsp;&nbsp;" + item["FName"].ToString() + "</font>";
            node.NavigateUrl = "../SysSet/MenuAdd.aspx?fid=" + item["FID"].ToString();
            node.Target = "main";
            TreeNode.Add(node);
            ShowSysColInfo(node.ChildNodes, item["FNumber"].ToString());
            if (node.ChildNodes.Count > 0 && i > 0 || !string.IsNullOrEmpty(FParentId))
                node.Expanded = false;
            i++;
        }
    }
    private string getTitleName(string col)
    {
        if (string.IsNullOrEmpty(col))
            col = HKINDID.Value;
        if (col == "lmcd")
        {
            return "栏目菜单";
        }
        else if (col == "xtcd")
        {
            return "系统菜单";
        }
        string str = sh.GetDicName(col);
        return str;
    }

}

