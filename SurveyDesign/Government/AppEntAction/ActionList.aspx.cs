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
using Approve.RuleCenter;
using Approve.RuleApp;
using Approve.Common;
using Approve.EntityBase;

public partial class Government_AppEntAction_ActionList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (string.IsNullOrEmpty(Request["fbid"]))
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
        sb.Append(" select fsystemid from cf_ent_baseinfo where fid='" + Request["fbid"] + "'");
        string sSystemId = rc.GetSignValue(sb.ToString());
        if (string.IsNullOrEmpty(sSystemId))
        {
            return;
        }

        sb.Remove(0, sb.Length);
        sb.Append("select fname,fnumber from CF_Sys_BadActionCode where fparentid is null ");
        sb.Append(" order by forder,fcreatetime desc ");
        DataTable dt = rc.GetTable(sb.ToString());
        dbFUnitTypeId.DataSource = dt;
        dbFUnitTypeId.DataTextField = "FName";
        dbFUnitTypeId.DataValueField = "FNumber";
        dbFUnitTypeId.DataBind();


        string str = "";
        switch (sSystemId)
        {
            case "150":
                str = "P1";
                break;
            case "175":
                str = "H1";
                break;
        }

        if (!string.IsNullOrEmpty(str))
        {
            dbFUnitTypeId.SelectedIndex = dbFUnitTypeId.Items.IndexOf(dbFUnitTypeId.Items.FindByValue(str));
            dbFUnitTypeId.Enabled = false;
        }

        dbFUnitTypeId.Items.Insert(0, new ListItem("--请选择--", ""));
        dbFActionTypeId.Items.Insert(0, new ListItem("--请选择--", ""));

    }

    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        string sSystemId = rc.GetSignValue("select fsystemid from cf_ent_baseinfo where fid='" + Request["fbid"] + "'");
        if (string.IsNullOrEmpty(sSystemId))
        {
            sb.Append(" and t1.in ( " + rc.GetBadByEntType(sSystemId) + ")");
        }


        if (this.dbFUnitTypeId.SelectedValue != "")
        {
            sb.Append(" and t1.fparentid='");
            sb.Append(this.dbFUnitTypeId.SelectedValue + "' ");
        }

        if (this.dbFActionTypeId.SelectedValue != "")
        {
            sb.Append(" and t2.fparentid='");
            sb.Append(this.dbFActionTypeId.SelectedValue + "' ");
        }

        if (!string.IsNullOrEmpty(Request["fid"]))
        {
            sb.Append(" and t2.fid not in ");
            sb.Append(" (select FACtionCodeId from CF_Ent_BadActionCode where FACtionId='" + Request["fid"] + "')");
        }
        return sb.ToString();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select t2.fid,t2.fnumber,t2.fdesc,t2.fscore,t1.fname  FPName");
        sb.Append(" from CF_Sys_BadActionCode t1, CF_Sys_BadActionCode t2 ");
        sb.Append(" where t1.fnumber = t2.fparentid ");
        sb.Append(" and t1.fisdeleted=0 and t2.fisdeleted=0 ");
        sb.Append(" and t1.fparentid is not null ");
        sb.Append(GetCon());
        sb.Append(" order by t2.fparentid,t2.forder,t2.fcreatetime desc ");



        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "Action_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }



    protected void dbFUnitTypeId_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.dbFActionTypeId.Items.Clear();

        StringBuilder sb = new StringBuilder();
        sb.Append(" select t1.fname,t1.fnumber ");
        sb.Append(" from CF_Sys_BadActionCode t1 where t1.fparentid = '" + this.dbFUnitTypeId.SelectedValue + "' ");
        sb.Append(" order by t1.forder,t1.fcreatetime desc ");
        DataTable dt = rc.GetTable(sb.ToString());
        this.dbFActionTypeId.DataSource = dt;
        this.dbFActionTypeId.DataTextField = "FName";
        this.dbFActionTypeId.DataValueField = "FNumber";
        this.dbFActionTypeId.DataBind();
        this.dbFActionTypeId.Items.Insert(0, new ListItem("--请选择--", ""));
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request["fid"]))
        {
            return;
        }

        StringBuilder sb = new StringBuilder();
        float fTotleScore = 0.0f;
        pageTool tool = new pageTool(this.Page);
        int iCount = this.Action_List.Items.Count;

        ArrayList array1 = new ArrayList();
        ArrayList array2 = new ArrayList();
        ArrayList array3 = new ArrayList();
        ArrayList array4 = new ArrayList();

        for (int i = 0; i < iCount; i++)
        {
            string fid = this.Action_List.Items[i].Cells[this.Action_List.Columns.Count - 1].Text;
            string fScore = this.Action_List.Items[i].Cells[this.Action_List.Columns.Count - 2].Text;
            string fDesc = this.Action_List.Items[i].Cells[this.Action_List.Columns.Count - 3].Text;
            string fCodeId = this.Action_List.Items[i].Cells[this.Action_List.Columns.Count - 4].Text;

            CheckBox box = (CheckBox)this.Action_List.Items[i].Cells[0].Controls[1];
            if (box.Checked == true)
            {
                array1.Add(fid);
                array2.Add(fDesc);
                array3.Add(rc.GetSignValue(EntityTypeEnum.EsBadActionCode, "FNumber", "Fid='" + fid + "'"));
                array4.Add(fScore);

            }
        }
        if (array1.Count == 0)
        {
            tool.showMessage("请选择");
            return;
        }

        int sCount = array1.Count;
        SortedList[] sl = new SortedList[sCount];
        EntityTypeEnum[] en = new EntityTypeEnum[sCount];
        string[] fkey = new string[sCount];
        SaveOptionEnum[] so = new SaveOptionEnum[sCount];
        for (int j = 0; j < sCount; j++)
        {
            en[j] = EntityTypeEnum.EbBadActionCode;
            fkey[j] = "FID";
            so[j] = SaveOptionEnum.Insert;
            sl[j] = new SortedList();
            sl[j].Add("FID", Guid.NewGuid().ToString());
            sl[j].Add("FACtionId", Request["fid"]);
            sl[j].Add("FACtionDesc", array2[j].ToString());
            sl[j].Add("FActionCode", array3[j].ToString());
            sl[j].Add("FACtionCodeId", array1[j].ToString());
            sl[j].Add("FScore", array4[j].ToString());
            sl[j].Add("FIsDeleted", 0);
            sl[j].Add("FCreateTime", DateTime.Now);

            fTotleScore += EConvert.ToFloat(array4[j].ToString());
        }

        if (rc.SaveEBaseM(en, sl, fkey, so))
        {
            sb.Append(" update CF_Ent_BadAction set FScore=FScore+" + fTotleScore.ToString());
            sb.Append(" where fid='" + Request["fid"] + "'");
            rc.PExcute(sb.ToString());

            hiddle_IsSaveOk.Value = "1";
            string sScript = "if(document.all.hiddle_IsSaveOk.value!=null&&document.all.hiddle_IsSaveOk.value=='1'){window.returnValue=1;window.close();}";
            sScript += "else{window.returnValue=0;window.close();}";
            tool.showMessageAndRunFunction("选择成功", sScript);
        }
        else
        {
            tool.showMessage("选择失败");
        }
    }

    protected void Action_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string sScript = "showApproveWindow('ActionDetail.aspx?fid=" + fid + "',500,400)";
            e.Item.Cells[2].Text = "<a href='#' onclick=\"" + sScript + "\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
}
