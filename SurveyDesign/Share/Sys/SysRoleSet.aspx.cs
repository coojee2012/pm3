using System;
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
using Approve.Common;
using Approve.EntityBase;
using Approve.RuleCenter;

public partial class Admin_main_SysRoleSet : System.Web.UI.Page
{
    StringBuilder sScript = new StringBuilder();
    ArrayList arrParent = new ArrayList();
    bool fLag = false;

    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request["froleid"] == null || Request["froleid"] == "")
            {
                return;
            }
            ControlBind();
            ShowInfo();
        }
    }
    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fnumber,  ");
        sb.Append("  case flevel when 1 then fnumber when 2 then fnumber when 3 then fparent end as FParent,");
        sb.Append(" case flevel when 1 then fname ");
        sb.Append(" when 2 then '--'+fname ");
        sb.Append(" when 3 then '----'+fname ");
        sb.Append(" end as fname ");
        sb.Append(" from cf_sys_tree where fclass=2 and fisshow=0 ");
        sb.Append(" and fisdeleted=0 order by FParent,flevel,forder,fcreatetime desc ");

        DataTable dt = rc.GetTable(sb.ToString());
        this.dbColName.DataSource = dt;
        this.dbColName.DataTextField = "FName";
        this.dbColName.DataValueField = "FNumber";
        this.dbColName.DataBind();
        this.dbColName.Items.Insert(0, new ListItem("请选择","")); 
    }
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.dbColName.SelectedValue.Trim() != "")
        {
            sb.Append(" and st.fnumber=");
            sb.Append(this.dbColName.SelectedValue.Trim() + " ");
        }
        return sb.ToString();
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select st.fid,st.fnumber,sr.fid FSId,st.flevel, "); 
        sb.Append("  case flevel when 1 then fnumber when 2 then fnumber when 3 then fparent end as FParent,");
        sb.Append("  case st.flevel when 1 then st.fname ");  
        sb.Append(" when 2 then '&nbsp;&nbsp;&nbsp;&nbsp;'+st.fname ");
        sb.Append(" when 3 then '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'+st.fname end as fname");
        sb.Append(" from cf_sys_tree st left join CF_Sys_RoleRight sr ");
        sb.Append(" on st.fnumber = sr.FColNumber ");
        sb.Append(" and sr.FRoleId='" + Request["froleid"]+"'");
        sb.Append(" and sr.ftype=1 ");
        sb.Append(" where st.fisdeleted=0 and fisshow=0  "); 
        sb.Append(" and st.fclass=2 "); 
        sb.Append(GetCon());
        sb.Append(" order by st.fnumber,st.flevel,st.forder,st.fcreatetime desc ");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 18; 
        this.Pager1.controltopage = "PubRoleSet_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    protected void PubRoleSet_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fLevel = e.Item.Cells[0].Text;
            string fSId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            e.Item.Cells[2].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            CheckBox box = (CheckBox)e.Item.Cells[1].Controls[1];
            if (fSId != null && fSId != "&nbsp;")
            {
                box.Checked = true;
            }
            if (fLevel.Trim() == "1")
            {
                fLag = true;
                if (arrParent.Count > 0)
                {
                    ((CheckBox)arrParent[0]).Attributes.Add("onclick", "checkBoxSelect(this,'" + sScript.ToString() + "')");
                    arrParent.Clear(); 
                    sScript.Remove(0, sScript.Length);
                }
                arrParent.Add(box);
            }
            if (fLag)
            {
                sScript.Append(box.ClientID + "|");
            }
            
        }
        if (e.Item.ItemIndex == -1)
        {
            if (arrParent.Count > 0)
            {
                ((CheckBox)arrParent[0]).Attributes.Add("onclick", "checkBoxSelect(this,'" + sScript.ToString() + "')"); 
            }
        }
    }
    private void SaveInfo()
    {
        StringBuilder sb = new StringBuilder();
        pageTool tool = new pageTool(this.Page);
        ArrayList array = new ArrayList(); 
        ArrayList array2 = new ArrayList();

        int iCount = this.PubRoleSet_List.Items.Count;
        for (int i = 0; i < iCount; i++)
        { 
            string fNumber = PubRoleSet_List.Items[i].Cells[PubRoleSet_List.Columns.Count - 3].Text;
            CheckBox box1 = (CheckBox)PubRoleSet_List.Items[i].Cells[1].Controls[1]; 
            if (box1.Checked == true)
            {
                array.Add(fNumber); 
            }
              
            sb.Remove(0, sb.Length);
            sb.Append(" select fid from CF_Sys_RoleRight where ");
            sb.Append(" FRoleId='" + Request["froleid"] + "'  and FColNumber='" + fNumber + "'");
            sb.Append(" and ftype=1");
            string fid = rc.GetSignValue(sb.ToString());
            if (fid != null && fid != "")
            {
                array2.Add(fid);
            }
            
        }
       
        try
        {
            iCount = array2.Count;
            if (iCount > 0)
            {
                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_Sys_RoleRight where  fid in (");
                for (int k = 0; k < iCount; k++)
                {
                    if (k == 0)
                    {
                        sb.Append("'" + array2[k].ToString() + "'");
                    }
                    else
                    {
                        sb.Append(",'" + array2[k].ToString() + "'");
                    }
                }
                sb.Append(") ");
                rc.PExcute(sb.ToString());
            }

            iCount = array.Count;
            if (iCount > 0)
            {
                SortedList[] sl = new SortedList[iCount];
                EntityTypeEnum[] en = new EntityTypeEnum[iCount];
                string[] fkey = new string[iCount];
                SaveOptionEnum[] so = new SaveOptionEnum[iCount];

                for (int j = 0; j < iCount; j++)
                {
                    sl[j] = new SortedList();
                    en[j] = EntityTypeEnum.EsRoleRight;
                    fkey[j] = "FID";
                    so[j] = SaveOptionEnum.Insert; 
                     
                    sl[j].Add("FID", Guid.NewGuid().ToString());
                    sl[j].Add("FRoleId", Request["froleid"]);
                    sl[j].Add("FIsDeleted", 0);
                    sl[j].Add("FCreateTime", DateTime.Now);
                    sl[j].Add("FColNumber", array[j].ToString());
                    sl[j].Add("FType", 1); 
                }
                rc.SaveEBaseM(en, sl, fkey, so);
            }
            tool.showMessage("保存成功");
            ShowInfo();
        }
        catch (Exception ex)
        {
            tool.showMessage("保存失败");
        } 
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
