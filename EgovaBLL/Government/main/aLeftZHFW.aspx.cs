using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Approve.RuleCenter;
using Approve.EntityBase;
using System.Text.RegularExpressions;
using System.Linq;
using ProjectData;
using System.Collections.Generic;

public partial class Government_AppMain_aLeft : System.Web.UI.Page
{
    DataTable menu = new DataTable();//菜单 
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //RegisterStartupScript("", "<script>showMenu();</script>");
            string sKid = Request.QueryString["HKINDID"];
            //if (sKid == "45033" || sKid == "45013")
            //    Response.Redirect("aLeftGZ.aspx?skid=" + sKid);
            //else
            //{
            l_title.Text = getTitleName(sKid);
            showInfo();
            //}
            string fN = Request.QueryString["FN"];
            string vType = Request.QueryString["vType"];
            if (!string.IsNullOrEmpty(fN))
            {
                if (string.IsNullOrEmpty(vType))
                    vType = "-";
                this.RegisterStartupScript("js", "<script>showTag('" + fN + "','" + vType + "');</script>");
            }
        }
    }

    List<string> roleList = new List<string>();
    private void showInfo()
    {
        string DFMenuRoleId = String.IsNullOrEmpty(EConvert.ToString(Session["DFMenuRoleId"])) ? "1" : EConvert.ToString(Session["DFMenuRoleId"]);
        if (!string.IsNullOrEmpty(DFMenuRoleId))
        {
            //角色的菜单权限 

            roleList = DFMenuRoleId.Split(',').ToList();
        }
        else
        {
            Response.Write("没有权限");
            Response.End();
        }
        string FParentId = Request.QueryString["HKINDID"];

        ProjectDB db = new ProjectDB();
        var vMenu = from t in db.Menu
                    where t.FParentId == FParentId && t.FIsDis == 1
                       && roleList.Count(s => t.FRoleId.Split(',').Contains(s)) > 0
                    orderby t.FOrder
                    select new { t.FName, t.FNumber, t.FLevel, t.FID, t.FQUrl, t.FUrl, t.FRoleId, t.FTarget };

        repMenu.DataSource = vMenu;
        repMenu.DataBind();


    }
    protected void repMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater repeater = e.Item.FindControl("repSubMenu") as Repeater;
            if (repeater != null)
            {
                string FNumber = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
                string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
                int FLevel = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FLevel"));

                ProjectDB db = new ProjectDB();
                var v = from t in db.Menu
                        where t.FParentId.ToString() == FNumber && t.FLevel == (FLevel + 1) && t.FIsDis == 1
                         && roleList.Count(s => t.FRoleId.Split(',').Contains(s)) > 0
                        orderby t.FOrder
                        select new { t.FName, t.FNumber, t.FLevel, t.FID, t.FQUrl, t.FUrl, t.FTarget };

                repeater.DataSource = v;
                repeater.DataBind();

            }
        }
    }
    protected void repSubMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater repeater = e.Item.FindControl("repSubMenu2") as Repeater;
            if (repeater != null)
            {
                string FNumber = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
                string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
                int FLevel = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FLevel"));

                ProjectDB db = new ProjectDB();
                var v = from t in db.Menu
                        where t.FParentId.ToString() == FNumber && t.FLevel == (FLevel + 1) && t.FIsDis == 1
                         && roleList.Count(s => t.FRoleId.Split(',').Contains(s)) > 0
                        orderby t.FOrder
                        select new { t.FName, t.FNumber, t.FLevel, t.FID, t.FQUrl, t.FUrl,t.FTarget };

                repeater.DataSource = v;
                repeater.DataBind();

            }
        }
    }


    ////显示
    //private void showInfo()
    //{
    //    string DFMenuRoleId = String.IsNullOrEmpty(EConvert.ToString(Session["DFMenuRoleId"])) ? "1" : EConvert.ToString(Session["DFMenuRoleId"]);
    //    if (!string.IsNullOrEmpty(DFMenuRoleId))
    //    {
    //        #region 初始化
    //        //菜单
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("select fname,fnumber,FUrl,FPicName,FSelcePicName,FTarget,forder,fcreatetime,flevel,fparentid,froleid,FQUrl  ");
    //        sb.Append("from cf_sys_menu ");
    //        sb.Append("where  fisdeleted=0 and fisdis=1 ");
    //        sb.Append("order by flevel,Forder,FNumber ");
    //        menu = rc.GetTable(sb.ToString());

    //        //角色的菜单权限
    //        roleList = DFMenuRoleId.Split(',').ToList();
    //        #endregion
    //    }
    //    else
    //    {
    //        Response.Write("没有权限");
    //        Response.End();
    //    }

    //    //得到级别
    //    ProjectDB db = new ProjectDB();
    //    CF_Sys_Menu m = db.CF_Sys_Menu.Where(t => t.FNumber.ToString() == Request.QueryString["HKINDID"]).FirstOrDefault();
    //    if (m != null)
    //    {
    //        LoadNode(TreeView1.Nodes, Request.QueryString["HKINDID"], m.FLevel.Value + 1);
    //    }

    //    //string FQurl = EConvert.ToString(menu.Select("FNumber='" + Request.QueryString["HKINDID"] + "'").AsEnumerable().Select(t => new { FQUrl = t["FQUrl"] }).FirstOrDefault());
    //    //if (!string.IsNullOrEmpty(FQurl))
    //    //{
    //    //    l_title.Text = "<a id='l_title' href='" + FQurl.Trim() + "' target='main' >" + l_title.Text + "</a>";
    //    //}
    //}

    ///// <summary>
    ///// 递归菜单（模块列表）
    ///// </summary>
    ///// <param name="node">子菜单</param>
    ///// <param name="MtID">子菜单上级ID</param>
    //private void LoadNode(TreeNodeCollection tree, string FParentID, int fLevel)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    if (!string.IsNullOrEmpty(FParentID))
    //        sb.Append("FParentID='" + FParentID + "' and FLevel='" + fLevel + "' ");
    //    else
    //        sb.Append("FLevel=1  ");
    //    DataRow[] dt = menu.Select(sb.ToString(), "forder,fnumber");
    //    bool flag = false;
    //    foreach (DataRow item in dt)
    //    {
    //        string FRoleId = EConvert.ToString(item["FRoleId"]);
    //        foreach (string str in roleList)
    //        {
    //            if (!string.IsNullOrEmpty(FRoleId) && FRoleId.Replace("'", "").Split(',').Contains(str))
    //            {
    //                TreeNode node = node = new TreeNode();
    //                node.Text = "<span class='tree_img_b' num='" + item["fnumber"] + "' url='" + item["furl"] + "'>" + item["FName"].ToString() + "</span>";
    //                node.Value = item["FNumber"].ToString();
    //                node.NavigateUrl = string.IsNullOrEmpty(item["FUrl"].ToString()) ? "javascript:void(0);" : string.Format("javascript:gotoPage('{0}','{1}','{2}')", item["fnumber"], item["fname"], item["FUrl"]);
    //                //node.Target = item["FTarget"].ToString();
    //                //node.ImageUrl = item["FPicName"].ToString();

    //                DataRow[] d = menu.Select("FParentID='" + item["FNumber"] + "' and FLevel='" + (fLevel + 1) + "' ", "");
    //                if (d.Length > 0 || fLevel <= 3)
    //                {
    //                    node.Text = "<span class='tree_img_b2' num='" + item["fnumber"] + "' url='" + item["furl"] + "'>" + item["FName"].ToString() + "</span>";
    //                }

    //                tree.Add(node);
    //                if (!flag)
    //                {
    //                    node.Expanded = true;
    //                    flag = true;
    //                }
    //                else
    //                    node.Expanded = false;
    //                LoadNode(node.ChildNodes, item["FNumber"].ToString(), (fLevel + 1));
    //                break;
    //            }
    //        }
    //    }

    //}


    private string getTitleName(string number)
    {
        string str = "";
        if (!string.IsNullOrEmpty(number))
        {
            str = rc.GetSignValue(EntityTypeEnum.EsMenu, "FName", "FNumber ='" + number + "'");
        }
        return str;
    }



    //-------------------------------------------------
    //判断字符串是否是数字
    public bool IsNumeric(string str)
    {

        bool isNum = true;
        char[] chars = str.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if (!Char.IsNumber(chars[i]))
                isNum = false;
        }
        return isNum;
    }

}
