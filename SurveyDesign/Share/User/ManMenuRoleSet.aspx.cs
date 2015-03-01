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

public partial class Share_User_ManMenuRoleSet : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            if (string.IsNullOrEmpty(Request.QueryString["FUserId"]))
            {
                Response.Write("失败，请重试");
                Response.Clear();
                Response.End();
                return;
            }
            ShowInfo();
        }
    }

    void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select r.fid,r.fmenuRoleId,p.FName,p.FNumber from cf_sys_userright r ");
        sb.Append("inner join CF_Sys_Platform p on r.FSystemId=p.FNumber and p.FIsDeleted=0 ");
        sb.Append("where fuserId=@fuserId order by p.FNumber");
        DataTable dt = sh.GetTable(sb.ToString(), new SqlParameter("@fuserId", Request.QueryString["FUserId"]));
        dg_list.DataSource = dt;
        dg_list.DataBind();
    }
    void SetMenuRoleList(CheckBoxList menuList, string menuIds)
    {
        if (!string.IsNullOrEmpty(menuIds)
            && (menuList != null && menuList is CheckBoxList))
        {
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
        for (int i = 0; i < dg_list.Items.Count; i++)
        {
            SortedList sl = new SortedList();
            string fId = EConvert.ToString(dg_list.DataKeys[i]);
            ////菜单角色
            CheckBoxList cbMenuRoleId = dg_list.Items[i].FindControl("t_FMenuRoleId") as CheckBoxList;
            if (cbMenuRoleId != null)
            {
                if (cbMenuRoleId.SelectedIndex == -1)
                {
                    tool.showMessage("请选择菜单角色");
                    return;
                }
                sl.Add("FID", fId);
                sl.Add("FIsDeleted", 0);
                sl.Add("FCreateTime", DateTime.Now);
                sl.Add("FBaseinfoID", Guid.NewGuid().ToString());//新产生FBaseinfoID(是Share库的企业主FID)
                sl.Add("FDeptFrom", 1);//1：省级用户
                sl.Add("FUserId", Request.QueryString["FUserId"]);//用户ID 
                sl.Add("FMenuRoleId", GetMenuRoleList(cbMenuRoleId));
                arr.Add(sl);
            }
        }
        if (arr.Count > 0)
        {
            int iCount = arr.Count;
            SortedList[] sls = new SortedList[iCount];
            EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
            string[] keys = new string[iCount];
            SaveOptionEnum[] sos = new SaveOptionEnum[iCount];
            for (int i = 0; i < arr.Count; i++)
            {
                sls[i] = arr[i] as SortedList;
                ens[i] = EntityTypeEnum.EsUserRight;
                keys[i] = "FID";
                sos[i] = SaveOptionEnum.Update;
            }
            if (sh.SaveEBaseM(ens, sls, keys, sos))
            {
                tool.showMessage("保存成功");
            }
            else
            {
                tool.showMessage("保存失败");
            }
        }
        else
        {
            tool.showMessage("请选择菜单角色");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void dg_list_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fMenuRoleId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fMenuRoleId"));
            string fPlatId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FNumber"));
            ////菜单角色
            CheckBoxList cbMenuRoleId = e.Item.FindControl("t_FMenuRoleId") as CheckBoxList;
            if (cbMenuRoleId != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" select fname,fnumber from cf_sys_role where fisdeleted=0 ");
                sb.Append(" and ftypeid=2 and FMtypeId=100 and FPlatId='" + fPlatId + "'");
                sb.Append(" order by forder,ftime desc ");
                DataTable dt = sh.GetTable(sb.ToString());
                cbMenuRoleId.DataSource = dt;
                cbMenuRoleId.DataTextField = "FName";
                cbMenuRoleId.DataValueField = "FNumber";
                cbMenuRoleId.DataBind();
                SetMenuRoleList(cbMenuRoleId, fMenuRoleId);
            }
        }
    }
}
