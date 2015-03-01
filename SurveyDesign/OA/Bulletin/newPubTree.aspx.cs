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
using System.Linq;
using Approve.EntityBase;
using ProjectData;

public partial class Admin_main_newPubTree : govBasePage
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!Page.IsPostBack)
        {
            ShowInfo();
        }
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        string fType = Request.QueryString["fType"];
        DataTable dt = this.GetDataByType(fType);
        string fValue1 = Request.QueryString["cols"];
        bool isHasValue = true;
        string[] strs = fValue1.Split(',');
        if (strs == null || strs.Length == 0)
            isHasValue = false;
        DataRow[] rows = dt.Select();
        Microsoft.Web.UI.WebControls.TreeNode node = null;
        for (int i = 0; i < rows.Length; i++)
        {
            node = new Microsoft.Web.UI.WebControls.TreeNode();
            node.Text = rows[i]["FName"].ToString();
            node.NodeData = rows[i]["FNumber"].ToString();
            this.Tree.Nodes.Add(node);
            node.CheckBox = true;
            node.Expanded = true;
            if (isHasValue && strs.Contains(node.NodeData))
                node.Checked = true;
        }
    }

    private bool IsHas(string fValue2)
    {
        string fValue1 = Request.QueryString["cols"];
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
        sb.Append("fplatId='" + fType + "' order by forder");
        DataTable dt = rc.GetTable(EntityTypeEnum.EsSystemName, "replace(FDesc,'资质办理','') FName,FNumber", sb.ToString());
        return dt;
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < Tree.Nodes.Count; i++)
        {
            if (Tree.Nodes[i].Checked)
            {
                if (sb.Length > 0)
                    sb.Append(",");
                sb.Append(Tree.Nodes[i].NodeData);
            }
        }
        pageTool tool = new pageTool(this.Page);
        if (sb.Length > 0)
            tool.showMessageAndRunFunction("操作成功!", "window.returnValue='" + sb.ToString() + "';window.close();");
        else
            tool.showMessage("请选择项!");
    }
}
