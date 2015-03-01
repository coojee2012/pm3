using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.Linq;
using System.Linq.Expressions;
using ProjectData;
using Tools;
using ProjectBLL;


public partial class Admin_User_CheckUserPope : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TreeNodeInit();
        }
    }

    /// <summary>
    /// 属性导航初始化
    /// </summary>
    private void TreeNodeInit()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["RightId"]))
        {
            string RightId = Request.QueryString["RightId"];


            ProjectDB db = new ProjectDB();

            List<CF_Sys_Menu> nodes = new List<CF_Sys_Menu>();
            List<CF_Sys_Menu> Menu = LoadNode(nodes, "4000102");

            var result = from m in Menu
                         join p in db.CF_Sys_UserPope.Where(t => t.FUserId == RightId) on m.FID equals p.FPageId
                         into x
                         from t in x.DefaultIfEmpty(new CF_Sys_UserPope { FUserId = "", FPageId = "", FAddBtn = 0, FDelBtn = 0, FPubBtn = 0, FClosePubBtn = 0 })
                         where true
                         orderby m.FOrder
                         select new
                         {
                             m.FID,
                             m.FName,
                             m.FNumber,
                             m.FLevel,
                             t.FUserId,
                             t.FAddBtn,
                             t.FClosePubBtn,
                             t.FDelBtn,
                             t.FPubBtn,
                             t.FPageId
                         };


            PopeTree.DataSource = result;
            PopeTree.DataBind();
 

            //top上面的主菜单
            Menu = db.Menu.Where(m => m.FLevel == 3 && m.FIsDis == 1 && m.FParentId == "40001").ToList();
            var resultMain = from m in Menu
                             join p in db.CF_Sys_UserPope.Where(t => t.FUserId == RightId) on m.FID equals p.FPageId
                             into x
                             from t in x.DefaultIfEmpty(new CF_Sys_UserPope { FUserId = "", FPageId = "", FAddBtn = 0, FDelBtn = 0, FPubBtn = 0, FClosePubBtn = 0 })
                             where true
                             orderby m.FOrder
                             select new
                             {
                                 m.FID,
                                 m.FName,
                                 m.FNumber,
                                 m.FLevel,
                                 t.FUserId,
                                 t.FAddBtn,
                                 t.FClosePubBtn,
                                 t.FDelBtn,
                                 t.FPubBtn,
                                 t.FPageId
                             };


            PopeTreeMain.DataSource = resultMain;
            PopeTreeMain.DataBind();

            DataCache.RemoveCache("Menu");
        }


    }

    /// <summary>
    /// 递归菜单（模块列表）
    /// </summary>
    /// <param name="node">子菜单</param>
    /// <param name="MtID">子菜单上级ID</param>
    private List<CF_Sys_Menu> LoadNode(List<CF_Sys_Menu> nodes, string MtID)
    {
        ProjectDB context = new ProjectDB();
        var result = from m in context.Menu
                     where m.FParentId == MtID && m.FIsDis == 1 && m.FType == 2
                     orderby m.FOrder
                     select m;

        foreach (var item in result)
        {
            string str = "";
            for (int i = 1; i < item.FLevel - 3; i++)
            {
                str += "----";
            }
            item.FName = str + item.FName;
            nodes.Add(item);

            LoadNode(nodes, item.FNumber);
        }
        return nodes;
    }



    protected void PopeTree_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FPageId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPageId"));
            int Level = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FLevel"));
            string FNumber = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
            CheckBox CB_IsPope = (CheckBox)e.Item.FindControl("CB_IsPope");
            CheckBox CB_IsAdd = (CheckBox)e.Item.FindControl("CB_IsAdd");
            CheckBox CB_IsADel = (CheckBox)e.Item.FindControl("CB_IsADel");
            CheckBox CB_IsPub = (CheckBox)e.Item.FindControl("CB_IsPub");
            CheckBox CB_IsClosePub = (CheckBox)e.Item.FindControl("CB_IsClosePub");
            ProjectDB db = new ProjectDB();
            int subCount = db.Menu.Where(t => t.FParentId == FNumber).Count();
            if (subCount > 0)
            {
                //CB_IsPope.Visible = false;
                CB_IsAdd.Visible = false;
                CB_IsADel.Visible = false;
                CB_IsPub.Visible = false;
                CB_IsClosePub.Visible = false;
            }
            if (!string.IsNullOrEmpty(FPageId))
            {
                if (CB_IsPope != null)
                {
                    CB_IsPope.Checked = true;
                }

                int FAddBtn = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FAddBtn"));
                int FDelBtn = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FDelBtn"));
                int FPubBtn = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FPubBtn"));
                int FClosePubBtn = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FClosePubBtn"));

                CB_IsAdd.Checked = FAddBtn == 1;
                CB_IsADel.Checked = FDelBtn == 1;
                CB_IsPub.Checked = FPubBtn == 1;
                CB_IsClosePub.Checked = FClosePubBtn == 1;
            }
        }
    }

    protected void PopeTreeMain_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FPageId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPageId"));
            int Level = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FLevel"));
            string FNumber = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
            CheckBox CB_IsPope = (CheckBox)e.Item.FindControl("CB_IsPope");
            CheckBox CB_IsAdd = (CheckBox)e.Item.FindControl("CB_IsAdd");
            CheckBox CB_IsADel = (CheckBox)e.Item.FindControl("CB_IsADel");
            CheckBox CB_IsPub = (CheckBox)e.Item.FindControl("CB_IsPub");
            CheckBox CB_IsClosePub = (CheckBox)e.Item.FindControl("CB_IsClosePub");

            CB_IsAdd.Visible = false;
            CB_IsADel.Visible = false;
            CB_IsPub.Visible = false;
            CB_IsClosePub.Visible = false;

            if (!string.IsNullOrEmpty(FPageId))
            {
                if (CB_IsPope != null)
                {
                    CB_IsPope.Checked = true;
                }

                int FAddBtn = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FAddBtn"));
                int FDelBtn = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FDelBtn"));
                int FPubBtn = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FPubBtn"));
                int FClosePubBtn = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FClosePubBtn"));

                CB_IsAdd.Checked = FAddBtn == 1;
                CB_IsADel.Checked = FDelBtn == 1;
                CB_IsPub.Checked = FPubBtn == 1;
                CB_IsClosePub.Checked = FClosePubBtn == 1;
            }
        }
    }


    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(Request.QueryString["RightId"]))
        {
            string RightId = Request.QueryString["RightId"];
            ProjectDB db = new ProjectDB();
            IQueryable<CF_Sys_UserPope> DelPope = db.CF_Sys_UserPope.Where(t => t.FUserId == RightId);
            db.CF_Sys_UserPope.DeleteAllOnSubmit(DelPope);

            for (int i = 0; i < PopeTree.Items.Count; i++)
            {
                CheckBox CB_IsPope = (CheckBox)PopeTree.Items[i].FindControl("CB_IsPope");
                if (CB_IsPope.Checked)
                {
                    string FPageId = EConvert.ToString(DataBinder.Eval(PopeTree.Items[i].DataItem, "FID"));
                    Label la = (Label)PopeTree.Items[i].Controls[1];
                    FPageId = la.Text;
                    if (!string.IsNullOrEmpty(FPageId))
                    {
                        CheckBox CB_IsAdd = (CheckBox)PopeTree.Items[i].FindControl("CB_IsAdd");
                        CheckBox CB_IsADel = (CheckBox)PopeTree.Items[i].FindControl("CB_IsADel");
                        CheckBox CB_IsPub = (CheckBox)PopeTree.Items[i].FindControl("CB_IsPub");
                        CheckBox CB_IsClosePub = (CheckBox)PopeTree.Items[i].FindControl("CB_IsClosePub");
                        DateTime dTime = DateTime.Now;
                        CF_Sys_UserPope uPope = new CF_Sys_UserPope();
                        uPope.FId = Guid.NewGuid().ToString();
                        uPope.FPageId = FPageId;
                        uPope.FUserId = RightId;
                        uPope.FTime = dTime;
                        uPope.FCreateTime = dTime;

                        uPope.FAddBtn = CB_IsAdd.Checked ? 1 : 0;
                        uPope.FDelBtn = CB_IsADel.Checked ? 1 : 0;
                        uPope.FPubBtn = CB_IsPub.Checked ? 1 : 0;
                        uPope.FClosePubBtn = CB_IsClosePub.Checked ? 1 : 0;

                        db.CF_Sys_UserPope.InsertOnSubmit(uPope);
                    }
                }
            }

            //Top主菜单
            for (int i = 0; i < PopeTreeMain.Items.Count; i++)
            {
                CheckBox CB_IsPope = (CheckBox)PopeTreeMain.Items[i].FindControl("CB_IsPope");
                if (CB_IsPope.Checked)
                {
                    string FPageId = EConvert.ToString(DataBinder.Eval(PopeTreeMain.Items[i].DataItem, "FID"));
                    Label la = (Label)PopeTreeMain.Items[i].Controls[1];
                    FPageId = la.Text;
                    if (!string.IsNullOrEmpty(FPageId))
                    {
                        CheckBox CB_IsAdd = (CheckBox)PopeTreeMain.Items[i].FindControl("CB_IsAdd");
                        CheckBox CB_IsADel = (CheckBox)PopeTreeMain.Items[i].FindControl("CB_IsADel");
                        CheckBox CB_IsPub = (CheckBox)PopeTreeMain.Items[i].FindControl("CB_IsPub");
                        CheckBox CB_IsClosePub = (CheckBox)PopeTreeMain.Items[i].FindControl("CB_IsClosePub");
                        DateTime dTime = DateTime.Now;

                        CF_Sys_UserPope uPope = new CF_Sys_UserPope();
                        uPope.FId = Guid.NewGuid().ToString();
                        uPope.FPageId = FPageId;
                        uPope.FUserId = RightId;
                        uPope.FTime = dTime;
                        uPope.FCreateTime = dTime;

                        uPope.FAddBtn = CB_IsAdd.Checked ? 1 : 0;
                        uPope.FDelBtn = CB_IsADel.Checked ? 1 : 0;
                        uPope.FPubBtn = CB_IsPub.Checked ? 1 : 0;
                        uPope.FClosePubBtn = CB_IsClosePub.Checked ? 1 : 0;

                        db.CF_Sys_UserPope.InsertOnSubmit(uPope);
                    }
                }
            }

            db.SubmitChanges();
            //saveParentToPope(UserId);
            tool.ExecuteScript("alert(\"保存成功！\");window.returnValue='1';");
            DataCache.RemoveCache("Menu");
        }
    }

    private void saveParentToPope(string UserId)
    {
        ProjectDB db = new ProjectDB();
        List<CF_Sys_Menu> lUp = (from t in db.CF_Sys_UserPope
                                 join t1 in db.CF_Sys_Menu on t.FPageId equals t1.FID
                                 where t.FUserId == UserId
                                 select t1).Distinct().ToList();

        lUp = getParentID(lUp, 1);
        foreach (CF_Sys_Menu item in lUp)
        {
            DateTime dTime = DateTime.Now;
            CF_Sys_UserPope uPope = new CF_Sys_UserPope();
            uPope.FId = Guid.NewGuid().ToString();
            uPope.FPageId = item.FID;
            uPope.FUserId = UserId;
            uPope.FTime = dTime;
            uPope.FCreateTime = dTime;
            db.CF_Sys_UserPope.InsertOnSubmit(uPope);
        }
        db.SubmitChanges();

    }

    private List<CF_Sys_Menu> getParentID(List<CF_Sys_Menu> menu, int n)
    {
        ProjectDB db = new ProjectDB();

        List<CF_Sys_Menu> pList = (from t in menu
                                   join t1 in db.CF_Sys_Menu on t.FParentId equals t1.FNumber
                                   select t1).Distinct().ToList();

        if (pList != null)
        {
            n++;
            if (n > 5)
            {
                return pList.ToList();
            }
            getParentID(pList, n);

            foreach (CF_Sys_Menu item in menu)
            {
                pList.Add(item);
            }
        }
        return pList.Distinct().ToList();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}
