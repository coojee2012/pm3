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
using Approve.Common;
using Approve.EntityBase;

public partial class Admin_main_newPubTree : adminBasePage
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {


            ShowInfo();
        }
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        string fType = Request["ftype"];
        string fNewsId = Request["fnewsid"];
        if (fType == null)
        {
            return;
        }
        string fColNumber = "";
        if (fNewsId == "")
        {
            if (Request["fcol"] != null && Request["fcol"] != "")
            {
                fColNumber = Request["fcol"];
            }

        }
        else
        {
            fColNumber = rc.GetNewsColNumber(fNewsId);
        }
        DataTable dt = this.GetDataByType(fType);
        DataRow[] rows = dt.Select(" FLevel =1 and fnumber=100", "forder");
        TreeNode node = null;
        for (int i = 0; i < rows.Length; i++)
        {
            node = new TreeNode();
            node.Text = rows[i]["FName"].ToString();
            node.Value = rows[i]["FNumber"].ToString();
            this.Tree.Nodes.Add(node);
            node.Checked = true;
            node.Expanded = true;
            if (IsHas(fColNumber, node.Value))
            {
                node.Checked = true;

            }


            DataRow[] sRows = dt.Select("FLevel=2 and fparent='" + node.Value + "'", "forder");
            TreeNode sNode = null;
            for (int j = 0; j < sRows.Length; j++)
            {
                sNode = new TreeNode();
                sNode.Text = sRows[j]["FName"].ToString();
                sNode.Value = sRows[j]["FNumber"].ToString();
                sNode.Checked = true;
                if (IsHas(fColNumber, sNode.Value))
                {
                    sNode.Checked = true;
                }
                node.Checked = false;
                node.ChildNodes.Add(sNode);
                sNode.Expanded = true;

                DataRow[] sSrows = dt.Select("FLevel=3 and fparent='" + sNode.Value + "'", "forder");
                TreeNode sSNode = null;
                for (int k = 0; k < sSrows.Length; k++)
                {
                    sSNode = new TreeNode();
                    sSNode.Text = sSrows[k]["FName"].ToString();
                    sSNode.Value = sSrows[k]["FNumber"].ToString();
                    sSNode.Checked = true;
                    if (IsHas(fColNumber, sSNode.Value))
                    {
                        sSNode.Checked = true;
                    }
                    sNode.Checked = false;
                    sNode.ChildNodes.Add(sSNode);
                }
            }
        }
    }

    private bool IsHas(string fValue1, string fValue2)
    {
        string[] strs = fValue1.Split(',');
        if (strs == null || strs.Length == 0)
        {
            return false;
        }
        for (int i = 0; i < strs.Length; i++)
        {
            if (strs[i].Trim() == fValue2.Trim())
            {
                return true;
            }
        }
        return false;
    }

    private DataTable GetDataByType(string fType)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" fid in (select fid from CF_Sys_Tree where FLevel=1 and fisdeleted=0 and ftype=" + fType + ") ");

        sb.Append(" or fid in (select tt.fid from CF_Sys_Tree t,CF_Sys_Tree tt where t.fnumber=tt.fparent and ");
        sb.Append(" t.FLevel=1 and t.fisdeleted=0 ");
        sb.Append(" and tt.FLevel=2 and tt.fisdeleted=0 and tt.ftype=" + fType + ") ");
        sb.Append(" or fid in (select t.fid from CF_Sys_Tree t,CF_Sys_Tree tt where t.fnumber=tt.fparent and ");
        sb.Append(" t.FLevel=1 and t.fisdeleted=0 ");
        sb.Append(" and tt.FLevel=2 and tt.fisdeleted=0 and tt.ftype=" + fType + ") ");

        sb.Append(" or fid in (select ttt.fid from CF_Sys_Tree t,CF_Sys_Tree tt,CF_Sys_Tree ttt where t.fnumber=tt.fparent and tt.fnumber=ttt.fparent and ");
        sb.Append(" t.FLevel=1 and t.fisdeleted=0 ");
        sb.Append(" and tt.FLevel=2 and tt.fisdeleted=0 ");
        sb.Append(" and ttt.FLevel=3 and ttt.fisdeleted=0 and ttt.ftype=" + fType + ") ");
        sb.Append(" or fid in (select tt.fid from CF_Sys_Tree t,CF_Sys_Tree tt,CF_Sys_Tree ttt where t.fnumber=tt.fparent and tt.fnumber=ttt.fparent and ");
        sb.Append(" t.FLevel=1 and t.fisdeleted=0 ");
        sb.Append(" and tt.FLevel=2 and tt.fisdeleted=0 ");
        sb.Append(" and ttt.FLevel=3 and ttt.fisdeleted=0 and ttt.ftype=" + fType + ") ");
        sb.Append(" or fid in (select t.fid from CF_Sys_Tree t,CF_Sys_Tree tt,CF_Sys_Tree ttt where t.fnumber=tt.fparent and tt.fnumber=ttt.fparent and ");
        sb.Append(" t.FLevel=1 and t.fisdeleted=0 ");
        sb.Append(" and tt.FLevel=2 and tt.fisdeleted=0 ");
        sb.Append(" and ttt.FLevel=3 and ttt.fisdeleted=0 and ttt.ftype=" + fType + ")  ");
        sb.Append("  ");
        DataTable dt = rc.GetTable(EntityTypeEnum.EsTree, "FName,FNumber,Forder,flevel,FParent", sb.ToString());
        return dt;
    }
    private void SaveInfo()
    {
        StringBuilder sb = new StringBuilder();
        foreach (TreeNode node in this.Tree.Nodes)
        {
            if (node.Checked)
            {
                if (node.Checked == true)
                {
                    sb.Append(node.Value + ",");
                }
            }
            foreach (TreeNode sNode in node.ChildNodes)
            {
                if (sNode.Checked)
                {
                    if (sNode.Checked == true)
                    {
                        sb.Append(sNode.Value + ",");
                    }
                }
                foreach (TreeNode sSNode in sNode.ChildNodes)
                {
                    if (sSNode.Checked)
                    {
                        if (sSNode.Checked)
                        {
                            sb.Append(sSNode.Value + ",");
                        }
                    }
                }
            }
        }
        if (sb.ToString().EndsWith(","))
        {
            sb = sb.Remove(sb.Length - 1, 1);
        }
        if (Request["fnewsid"] != null || Request["fnewsid"] != "")
        {
            rc.PubNews(Request["fnewsid"], sb.ToString());
        }
        selectvalue.Value = sb.ToString();
        string scriptStr = "<script>window.parent.returnValue=\"" + sb.ToString() + "\";close()</script>";
        Response.Write(scriptStr);
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
}
