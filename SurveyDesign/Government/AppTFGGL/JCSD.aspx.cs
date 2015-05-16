using Approve.Common;
using Approve.RuleCenter;
using EgovaDAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppTFGGL_JCSD : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // t_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            if (Request["idCard"] != null && !string.IsNullOrEmpty(Request["idCard"]))
            {
                t_FIdCard.Value = Request["idCard"];
            }
            showInfo();
        }
    }
    //显示 
    void showInfo()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(@"select a.*,b.FFullName as DeptName from V_Sgxkz_EmpLock a , CF_Sys_ManageDept b
                     where a.PrjAddressDept = b.FNumber 
                     and  a.IsLock = 1
                    and a.FidCard like '" + t_FIdCard.Value + "%'");

        DataTable dt = rc.GetTable(sb.ToString());

        dg_List.DataSource = dt.DefaultView;
        dg_List.DataBind();

    }

  
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            // e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;

        }
    }
 
    protected void btnJS_Click(object sender, EventArgs e)
    {
        string FId = "";
        string FDeptId = Session["DFId"].ToString();
        int RowCount = dg_List.Items.Count;
        IList<string> FIdList = new List<string>();
        string FIds = "";
        //构造需要解锁的人员清单
        for (int i = 0; i < dg_List.Items.Count; i++)
        {
            CheckBox cbx = (CheckBox)dg_List.Items[i].Cells[0].Controls[1];
            if (cbx.Checked)
            {
                FId = dg_List.Items[i].Cells[dg_List.Columns.Count - 1].Text.Trim();
                string dept = dg_List.Items[i].Cells[dg_List.Columns.Count - 2].Text.Trim();
 
                if (dept == FDeptId)
                {
                    FIdList.Add(FId);
                    if (string.IsNullOrEmpty(FIds))
                    {
                        FIds = "'" + FId + "'";
                    }
                    else
                    {
                        FIds += ",'" + FId + "'";
                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "js", "alert('有项目属地不在管辖范围内,不能解锁！');", true);
                    return;
                }
                
            }
        }
        //
        for (int k = 0; k < FIdList.Count(); k++)
        {
            //直接更改锁定状态
            string newId=Guid.NewGuid().ToString();
            string sql = "UPDATE TC_PrjItem_Emp_Lock SET IsLock=0,FTime=GETDATE() WHERE FId= '"+FIdList[k]+"';";
            rc.PExcute(sql);
           
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "js", "alert('解锁成功！');", true);
        showInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }

}