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
using Approve.EntityCenter;
using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.Common;
public partial class Admin_main_EntList : adminBasePage
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
          
            if (Request["fnewsid"] == null || Request["fnewsid"] == "")
            {
                return;
            }
            this.ViewState["FNEWSID"] = Request["fnewsid"];
            ControlBind();
            ShowInfo();
        }
    }
    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fdesc,fnumber from cf_sys_systemname  ");
        sb.Append(" where fisdeleted=0 and  FPlatId = 800 and FNumber<> 127 and FNumber <> 146");
        sb.Append(" order by forder");
        DataTable dt = rc.GetTable(sb.ToString());
        this.dbSystemId.DataSource = dt;
        this.dbSystemId.DataTextField = "FDesc";
        this.dbSystemId.DataValueField = "FNumber";
        this.dbSystemId.DataBind();
        this.dbSystemId.Items.Insert(0, new ListItem("请选择", ""));

        
    }
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtFName.Text != "")
        {
            sb.Append(" and b.fname like '%");
            sb.Append(this.txtFName.Text + "%'");
        }
 

        if (dbSystemId.SelectedValue.Trim() != "")
        {
            sb.Append(" and u.FSystemId='");
            sb.Append(dbSystemId.SelectedValue + "' ");

            if (dbSystemId.SelectedValue == "15501")
            {
                sb.Append(" and r.FSystemId=1554 ");
            }
            else if (dbSystemId.SelectedValue == "155")
            {
                sb.Append(" and r.FSystemId=1553 ");
            }
        }
        else
        {
            sb.Append(" and u.FSystemId ='" + Request.QueryString["sysId"] + "' ");
        }
        return sb.ToString();
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select b.fid,b.fname,b.flinkman,b.ftel, ");
        sb.Append(" (select top 1 fname from cf_sys_systemname where fnumber = b.fsystemid ) fsystemname ");
        sb.Append(" from cf_ent_baseinfo b ");
        sb.Append(" inner join cf_Sys_Userright r on b.FId=r.FBaseInfoId ");
        sb.Append(" inner join cf_Sys_User u on u.FId=r.FUserId ");
        sb.Append(" where b.fisdeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" order by b.fsystemid, b.fcreatetime desc ");

        this.Pager1.className = "RCenter";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "EntInfo_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    protected void Dic_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string fId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fPId = rc.GetSignValue(" select fid from CF_News_RecUnit where fnewsid='" + this.ViewState["FNEWSID"] + "' and FBaseInfoId='" + fId + "' and fisdeleted=0");

            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            if (fPId != null && fPId != "" && fPId != "&nbsp;")
            {
                box.Checked = true;

                e.Item.BackColor = System.Drawing.Color.FromArgb(249, 219, 155);
            }
        }
    }

    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        ArrayList array = new ArrayList();
        StringBuilder sb = new StringBuilder();
       


        
        sb.Append(" begin ");
        int iCount = this.EntInfo_List.Items.Count;
        for (int i = 0; i < iCount; i++)
        {
            string fId = this.EntInfo_List.Items[i].Cells[this.EntInfo_List.Columns.Count - 1].Text;
            sb.Append(" delete from CF_News_RecUnit where fnewsid='" + ViewState["FNEWSID"].ToString() + "'");
            sb.Append(" and FBaseInfoId='" + fId + "' and FIsDeleted=0 ");
            CheckBox box = (CheckBox)this.EntInfo_List.Items[i].Cells[0].Controls[1];
            if (box.Checked == true)
            {
                array.Add(fId);
            }
        }
        sb.Append(" End ");
        rc.PExcute(sb.ToString());


        iCount = array.Count;
        if (iCount == 0)
        {
            return;
        }
        SortedList[] sl = new SortedList[iCount];
        EntityTypeEnum[] en = new EntityTypeEnum[iCount];
        string[] fkey = new string[iCount];
        SaveOptionEnum[] so = new SaveOptionEnum[iCount];

        for (int j = 0; j < iCount; j++)
        {
            sl[j] = new SortedList();
            sl[j].Add("FID", Guid.NewGuid().ToString());
            sl[j].Add("FBaseInfoId", array[j].ToString());
            sl[j].Add("FNewsId", this.ViewState["FNEWSID"].ToString());
            sl[j].Add("FBaseinfoName", rc.GetSignValue(EntityTypeEnum.EbBaseInfo, "FName", "fid='" + array[j].ToString() + "'"));
            sl[j].Add("FIsDeleted", 0);
            sl[j].Add("FCreateTime", DateTime.Now);

            en[j] = EntityTypeEnum.EnRecUnit;
            fkey[j] = "FID";
            so[j] = SaveOptionEnum.Insert;
        }
        if (rc.SaveEBaseM(en, sl, fkey, so))
        {
            tool.showMessage("保存成功");
            ShowInfo();
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
   
}
