using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Approve.Common;
using Approve.RuleCenter;

public partial class Government_AppMain_chooseExpert : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "return confirm('是否确认选择待选框中的专家进行评审？')");
            bindInfo();
        }
    }
    public void bindInfo()
    {
        string sql = string.Format(@" select * from LINKER_95.dbCenterSC.dbo.CF_Pro_PsExpertInfo 
                     where PsID not in (select PsID from YW_GF_Expert where Fappid
                                        in ('" + t_appid.Value.Replace(",", "','") + "')  )");
        if (!string.IsNullOrEmpty(t_ExpertName.Text.Trim()))
        { sql += " and ExpertName like '%" + t_ExpertName.Text.Trim() + "%'"; }
        if (!string.IsNullOrEmpty(t_Industry.Text.Trim()))
        { sql += " and Industry like '%" + t_Industry.Text.Trim() + "%'"; }
        this.Pager1.sql = sql;
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bindInfo();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        string sql = null;
        ArrayList listPsID = new ArrayList(t_psid.Value.Trim().Split(','));
        if (listPsID.Count > 1)
        {
            for (int s = 0; s < listPsID.Count; s++)
            {
                if (!string.IsNullOrEmpty(listPsID[s].ToString()) && listPsID[s].ToString() != "0")
                {
                    sql += string.Format(@"insert YW_GF_Expert (FID,Fappid,PsID,Ftime,userid) values (newid(),'" + t_appid.Value + "','"
                        + listPsID[s].ToString() + "',getdate(),'" + CurrentEntUser.UserId + "');");
                }
            }
        }
        if (sh.PExcute(sql))
        { tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';window.close();"); }
        else
        { tool.showMessage("保存失败"); }
    }
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string psid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "PsID"));

            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();

            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"javascript:showAddWindow('http://app.scjst.gov.cn/MCreditSC/personsys/Expert/province/PsExpert/ExpertApp.aspx?PsID=" + psid + "',800,600);\" >" + e.Item.Cells[2].Text + "</a>";
        }
    }

    protected void JustAppInfo_List_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBox chbIsActive = e.Item.FindControl("CheckItem") as CheckBox;
            chbIsActive.CheckedChanged += new EventHandler(chbIsActive_CheckedChanged);
        }
    }
    private CheckBox GetHeaderCheckBox(DataGrid grd)
    {
        CheckBox chk = null;
        foreach (DataGridItem i in grd.Controls[0].Controls)
        {
            if (i.ItemType == ListItemType.Header)
            {
                chk = (CheckBox)i.FindControl("checkAll");
                break;
            }
        }
        return chk;

    }
    private void chbIsActive_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = this.GetHeaderCheckBox(this.JustAppInfo_List);
        ArrayList listPsID = new ArrayList(t_psid.Value.Trim().Split(','));
        ArrayList list = new ArrayList();
        string[] aName = tbName.Text.Trim().Split('、');
        list = new ArrayList(aName);
        foreach (DataGridItem i in this.JustAppInfo_List.Items)
        {
            CheckBox inChk = (CheckBox)i.FindControl("CheckItem");
            string name = i.Cells[3].Text;
            string psid = i.Cells[i.Cells.Count - 1].Text;
            if (inChk.Checked == true)
            {
                if (!list.Contains(name))
                { list.Add(name); listPsID.Add(psid); }
            }
            else
            {
                if (list.Contains(name))
                { list.Remove(name); listPsID.Remove(psid); }
            }
        }
        tbName.Text = null; t_psid.Value = string.Join(",", (string[])listPsID.ToArray(typeof(string)));
        for (int s = 0; s < list.Count; s++)
        {
            if (!string.IsNullOrEmpty(list[s].ToString()))
                tbName.Text += list[s].ToString() + "、";
        }
        if (!string.IsNullOrEmpty(tbName.Text.Trim()))
        { tbName.Text = tbName.Text.Substring(0, tbName.Text.Length - 1); }
    }


}