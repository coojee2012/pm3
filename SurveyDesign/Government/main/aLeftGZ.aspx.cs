using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using System.Data;
using Approve.RuleCenter;
using System.Text;

public partial class GMap_Server_aLeftGZ : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    string sCon = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            showInfo();
    }
    //显示
    string GetCon()
    {
        string FNumber = Request.QueryString["sKid"];
        string DFMenuRoleId = String.IsNullOrEmpty(EConvert.ToString(Session["DFMenuRoleId"])) ? "1" : EConvert.ToString(Session["DFMenuRoleId"]);
        StringBuilder sb = new StringBuilder();
        sb.Append("select fnumber ");
        sb.Append("from cf_sys_role ");
        sb.Append("where fnumber in (" + DFMenuRoleId + ")");
        DataTable dtRole = rc.GetTable(sb.ToString());
        string str = "";
        for (int i = 0; i < dtRole.Rows.Count; i++)
        {
            if (!string.IsNullOrEmpty(str))
                str += " or ";
            str += "froleid like '%" + dtRole.Rows[i]["FNumber"].ToString() + "%'";
        }
        return str;
    }

    //private void showInfo()
    //{
    //    string FNumber = Request.QueryString["sKid"];
    //    lTitle.Text = rc.GetMenuName(FNumber);
    //    StringBuilder sb = new StringBuilder();
    //    sb.Remove(0, sb.Length);
    //    sb.Append("select FID,froleid,FNumber,FName,FPicName,FOrder,FUrl,FQurl,FTarget ");
    //    sb.Append("from cf_sys_menu ");
    //    sb.Append("where fparentid = '" + FNumber + "' and flevel=3 and FIsDis=1 and FNumber<>'8050' ");
    //    DataTable dtMenu = rc.GetTable(sb.ToString());
    //    sCon = GetCon();
    //    var v = dtMenu.Select(string.IsNullOrEmpty(sCon) ? "1=2" : sCon).AsEnumerable().Select(t => new { FName = t["FName"], FPicName = t["FPicName"], FOrder = t["FOrder"], FUrl = t["FUrl"], FQurl = t["FQurl"], fnumber = t["fnumber"], FTarget = t["FTarget"] }).OrderBy(t => t.FOrder);
    //    repMenu.DataSource = v;
    //    repMenu.DataBind();

    //}
    //protected void repMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        Repeater repeater = e.Item.FindControl("repSubMenu") as Repeater;
    //        if (repeater != null)
    //        {
    //            string FNumber = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
    //            StringBuilder sb = new StringBuilder();
    //            sb.Remove(0, sb.Length);
    //            sb.Append("select FID,froleid,FNumber,FName,FPicName,FOrder,FUrl,FQurl,FTarget ");
    //            sb.Append("from cf_sys_menu ");
    //            sb.Append("where fparentid = '" + FNumber + "' and flevel=4 and FIsDis=1 and FNumber<>'8050' ");
    //            DataTable dtMenu = rc.GetTable(sb.ToString());
    //            string str = sCon;
    //            var v = dtMenu.Select(string.IsNullOrEmpty(str) ? "1=2" : str).AsEnumerable().Select(t => new { FName = t["FName"], FPicName = t["FPicName"], FOrder = t["FOrder"], FUrl = t["FUrl"], FQurl = t["FQurl"], fnumber = t["fnumber"], FTarget = t["FTarget"] }).OrderBy(t => t.FOrder);
    //            repeater.DataSource = v;
    //            repeater.DataBind();
    //        }
    //    }
    //}
    //protected void repSubMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        Repeater repeater = e.Item.FindControl("repSubMenu2") as Repeater;
    //        if (repeater != null)
    //        {
    //            string FNumber = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
    //            StringBuilder sb = new StringBuilder();
    //            sb.Remove(0, sb.Length);
    //            sb.Append("select FID,froleid,FNumber,FName,FPicName,FOrder,FUrl,FQurl,FTarget ");
    //            sb.Append("from cf_sys_menu ");
    //            sb.Append("where fparentid = '" + FNumber + "' and flevel=5 and FIsDis=1 and FNumber<>'8050' ");
    //            DataTable dtMenu = rc.GetTable(sb.ToString());
    //            string str = sCon;
    //            var v = dtMenu.Select(string.IsNullOrEmpty(str) ? "1=2" : str).AsEnumerable().Select(t => new { FName = t["FName"], FPicName = t["FPicName"], FOrder = t["FOrder"], FUrl = t["FUrl"], FQurl = t["FQurl"], fnumber = t["fnumber"], FTarget = t["FTarget"] }).OrderBy(t => t.FOrder);
    //            repeater.DataSource = v;
    //            repeater.DataBind();
    //        }
    //    }
    //}
    //protected void repSubMenu2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        Repeater repeater = e.Item.FindControl("repSubMenu3") as Repeater;
    //        if (repeater != null)
    //        {
    //            string FNumber = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
    //            StringBuilder sb = new StringBuilder();
    //            sb.Remove(0, sb.Length);
    //            sb.Append("select FID,froleid,FNumber,FName,FPicName,FOrder,FUrl,FQurl,FTarget ");
    //            sb.Append("from cf_sys_menu ");
    //            sb.Append("where fparentid = '" + FNumber + "' and flevel=6 and FIsDis=1 and FNumber<>'8050' ");
    //            DataTable dtMenu = rc.GetTable(sb.ToString());
    //            string str = sCon;
    //            var v = dtMenu.Select(string.IsNullOrEmpty(str) ? "1=2" : str).AsEnumerable().Select(t => new { FName = t["FName"], FPicName = t["FPicName"], FOrder = t["FOrder"], FUrl = t["FUrl"], FQurl = t["FQurl"], fnumber = t["fnumber"], FTarget = t["FTarget"] }).OrderBy(t => t.FOrder);
    //            repeater.DataSource = v;
    //            repeater.DataBind();
    //        }
    //    }
    //}



    //显示



    DataTable menu = new DataTable();//菜单 
    List<string> roleList = new List<string>();
    private void showInfo()
    {
        string FNumber = Request.QueryString["sKid"];
        lTitle.Text = rc.GetMenuName(FNumber);
        string DFMenuRoleId = String.IsNullOrEmpty(EConvert.ToString(Session["DFMenuRoleId"])) ? "1" : EConvert.ToString(Session["DFMenuRoleId"]);
        if (!string.IsNullOrEmpty(DFMenuRoleId))
        {
            #region 初始化
            //菜单
            StringBuilder sb = new StringBuilder();
            sb.Append("select fname,fnumber,FUrl,FPicName,FSelcePicName,FTarget,forder,fcreatetime,flevel,fparentid,froleid,FQUrl  ");
            sb.Append("from cf_sys_menu ");
            sb.Append("where  fisdeleted=0 and fisdis=1 ");
            sb.Append("order by flevel,Forder,FNumber ");
            menu = rc.GetTable(sb.ToString());

            //角色的菜单权限
            roleList = DFMenuRoleId.Split(',').ToList();
            #endregion
        }
        else
        {
            Response.Write("没有权限");
            Response.End();
        }

        //得到级别
        ProjectDB db = new ProjectDB();
        CF_Sys_Menu m = db.CF_Sys_Menu.Where(t => t.FNumber.ToString() == FNumber).FirstOrDefault();
        if (m != null)
        {
            LoadNode(TreeView1.Nodes, Request.QueryString["sKid"], m.FLevel.Value + 1);
        }

        //string FQurl = EConvert.ToString(menu.Select("FNumber='" + Request.QueryString["HKINDID"] + "'").AsEnumerable().Select(t => new { FQUrl = t["FQUrl"] }).FirstOrDefault());
        //if (!string.IsNullOrEmpty(FQurl))
        //{
        //    l_title.Text = "<a id='l_title' href='" + FQurl.Trim() + "' target='main' >" + l_title.Text + "</a>";
        //}
    }

    /// <summary>
    /// 递归菜单（模块列表）
    /// </summary>
    /// <param name="node">子菜单</param>
    /// <param name="MtID">子菜单上级ID</param>
    private void LoadNode(TreeNodeCollection tree, string FParentID, int fLevel)
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(FParentID))
            sb.Append("FParentID='" + FParentID + "' and FLevel='" + fLevel + "' ");
        else
            sb.Append("FLevel=1  ");
        DataRow[] dt = menu.Select(sb.ToString(), "forder,fnumber");
        bool flag = false;
        foreach (DataRow item in dt)
        {
            string FRoleId = EConvert.ToString(item["FRoleId"]);
            foreach (string str in roleList)
            {
                if (!string.IsNullOrEmpty(FRoleId) && FRoleId.Replace("'", "").Split(',').Contains(str))
                {
                    TreeNode node = node = new TreeNode();
                    node.Text = "<span class='tree_img_b' num='" + item["fnumber"] + "' url='" + item["furl"] + "'>" + item["FName"].ToString() + "</span>";
                    node.Value = item["FNumber"].ToString();
                    node.NavigateUrl = string.IsNullOrEmpty(item["FUrl"].ToString()) ? "javascript:void(0);" : string.Format("javascript:gotoPage('{0}','{1}','{2}')", item["fnumber"], item["fname"], item["FUrl"]);
                    //node.Target = item["FTarget"].ToString();
                    //node.ImageUrl = item["FPicName"].ToString();

                    DataRow[] d = menu.Select("FParentID='" + item["FNumber"] + "' and FLevel='" + (fLevel + 1) + "' ", "");
                    if (d.Length > 0 || fLevel < 6)
                    {
                        node.Text = "<span class='tree_img_b2' num='" + item["fnumber"] + "' url='" + item["furl"] + "'>" + item["FName"].ToString() + "</span>";
                    }

                    tree.Add(node);
                    if (!flag)
                    {
                        node.Expanded = true;
                        flag = true;
                    }
                    else
                        node.Expanded = false;
                    LoadNode(node.ChildNodes, item["FNumber"].ToString(), (fLevel + 1));
                    break;
                }
            }
        }

    }

}
