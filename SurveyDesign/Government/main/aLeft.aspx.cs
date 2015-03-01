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
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //RegisterStartupScript("", "<script>showMenu();</script>");
            string HKINDID = Request.QueryString["HKINDID"];
            if (!string.IsNullOrEmpty(HKINDID))
            {
                if (HKINDID.StartsWith("45029"))
                {
                    Response.Redirect("aLeftZHFW.aspx?HKINDID=" + HKINDID);
                }
            }
            //l_title.Text = getTitleName(HKINDID);
            showMenu();
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

    #region 显示菜单
    //显示菜单
    private void showMenu()
    {
        string p = Request.QueryString["HKINDID"];
        //var m = from t in db.Menu
        //        where t.FParentId == p && t.FIsDis == 1
        //        orderby t.FOrder
        //        select new { t.FName, t.FNumber, t.FPicName, t.FLevel, t.FTarget, t.FUrl };
        string sql = string.Format(@"select m.FName,m.FNumber, m.FPicName,m.FLevel,m.FTarget,m.FUrl
                                from CF_Sys_Menu m
                                where FIsDis=1 and m.FParentId='" + p
                               + "' and dbo.getRoleid(m.FRoleId,(select FMenuRoleId FROM CF_Sys_UserRight WHERE FUserId ='"
                               + Session["DFUserId"] + "')) = 1 order by m.FOrder ");
        if (Session["PsID"] != null)
        {
            sql = string.Format(@"select m.FName,m.FNumber, m.FPicName,m.FLevel,m.FTarget,m.FUrl
                from CF_Sys_Menu m
                 where FIsDis=1 and m.FParentId='45002' and 
                 dbo.getRoleid(m.FRoleId,(select FMenuRoleId FROM CF_Sys_UserRight 
                 WHERE FUserId ='f3bda2d6-0284-4d1b-b476-18ef6a4c10f0')) = 1
                 and FNumber= '4500232'");
        }
        repMenu.DataSource = rc.GetTable(sql);
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

                //ProjectDB db = new ProjectDB();
                //var v = from t in db.Menu
                //        where t.FParentId.ToString() == FNumber 
                //        && t.FLevel == (FLevel + 1) 
                //        && t.FIsDis == 1
                //        orderby t.FOrder
                //        select new { t.FName, t.FNumber, t.FPicName, t.FLevel, t.FTarget, t.FUrl };
                string sql = string.Format(@"select m.FName,m.FNumber, m.FPicName,m.FLevel,m.FTarget,m.FUrl
                                from CF_Sys_Menu m
                                where FIsDis=1 and m.FParentId='" + FNumber
                               + "' and dbo.getRoleid(m.FRoleId,(select FMenuRoleId FROM CF_Sys_UserRight WHERE FUserId ='"
                               + Session["DFUserId"] + "')) = 1 order by m.FOrder ");

             //   repeater.DataSource = v;
                repeater.DataSource = rc.GetTable(sql);
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
                if (FLevel == 4)
                {
                    ProjectDB db = new ProjectDB();
                    var v = from t in db.Menu
                            where t.FParentId.ToString() == FNumber && t.FLevel == (FLevel + 1) && t.FIsDis == 1
                            orderby t.FOrder
                            select new { t.FName, t.FNumber, t.FPicName, t.FLevel, t.FTarget, t.FUrl };

                    repeater.DataSource = v;
                    repeater.DataBind();
                }
            }
        }
    }
    #endregion




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
