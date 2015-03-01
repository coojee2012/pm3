using Approve.RuleBase;
using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Tools;
using Approve.EntityBase;

public partial class WYDW_ApplyYWH_YWHinfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showBAInfo();
            showCYInfo();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveData();
    }
    private void saveData()
    {
        Approve.Common.pageTool tool = new Approve.Common.pageTool(this.Page);
        Approve.EntityBase.SaveOptionEnum so = Approve.EntityBase.SaveOptionEnum.Insert;
        SortedList sl = new SortedList();
        sl = tool.getPageValue();
        if (hasYWHByFAppId(Session["FAppID"].ToString()))
        {
            so = Approve.EntityBase.SaveOptionEnum.Update;
            sl.Add("FID", ViewState["FID"].ToString());
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FAppID",Session["FAppid"].ToString());
        }

        if (rc.SaveEBase("YW_WY_XM_YZWYHBA", sl, "FID", so))
        {
            ViewState["FID"] = sl["FID"].ToString();
            tool.showMessage("保存成功！");
        }
        else
        {
            tool.showMessage("保存失败！");
        }
    }
    //显示业委会备案信息
    private void showBAInfo()
    {
        if (Session["FAppId"] != null)
        {
            string sql = "select * from YW_WY_XM_YZWYHBA where FAppID='" + Session["FAppId"].ToString() + "' ";
            DataTable dt = rc.GetTable(sql);

            if (dt.Rows.Count > 0)
            {
                Approve.Common.pageTool tool = new Approve.Common.pageTool(this.Page);
                tool.fillPageControl(dt.Rows[0]);
                ViewState["FID"] = dt.Rows[0]["FID"].ToString();
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('数据加载失败！');</script>");
        }
    }

    //显示业委会成员信息
    private void showCYInfo()
    {
        if (Session["FAppId"] != null)
        {
            string sql = "select * from YW_WY_XM_YZWYHCY where FAppID='" + Session["FAppId"].ToString() + "' order by FCreateTime desc";

            DataTable dtResult = rc.GetTable(sql);
            DataTable dt = dtResult.Clone();
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                int count = dtResult.Rows.Count;
                int index = (Pager1.CurrentPageIndex - 1) * Pager1.PageSize;

                int max = (index + Pager1.PageSize) > count ? count : (index + Pager1.PageSize);
                for (int i = index; i < max; i++)
                {
                    dt.Rows.Add(dtResult.Rows[i].ItemArray);
                }
                Pager1.RecordCount = count;
                
            }
            ywhcy_List.DataSource = dt;
            ywhcy_List.DataBind();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('数据加载失败！');</script>");
        }
    }
    #region [将字符串转化成decimal]
    private decimal stringToDecimal(string str)
    {
        if (str != null && str.ToString() != "")
        {
            try
            {
                return Convert.ToDecimal(str);
            }
            catch
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }
    #endregion

    #region [将字符串转化成int]
    private decimal stringToInt(string str)
    {
        if (str != null && str.ToString() != "")
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }
    #endregion

    #region [根据给定的FAppId来判断是否已存在一个业委会备案情况记录]
    private bool hasYWHByFAppId(string fAppId)
    {
        string sql = "select * from YW_WY_XM_YZWYHBA where FAppID='" + fAppId + "' ";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showCYInfo();
    }
    protected void YWHCY_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('YWHCYInfo.aspx?e=2&fid=" + fid + "','720','350');\">" + e.Item.Cells[2].Text + "</a>";
            //性别:0表示女，1表示男
            if (e.Item.Cells[e.Item.Cells.Count - 1].Text.Trim() == "0")
            {
                e.Item.Cells[3].Text = "女";
            }
            else if (e.Item.Cells[e.Item.Cells.Count - 1].Text.Trim() == "1")
            {
                e.Item.Cells[3].Text = "男";
            }
        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showCYInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        string sql = getSelectedFids(true, "YW_WY_XM_YZWYHCY").ToString();
        //Label1.Text = sql;
        try
        {
            if (rc.PExcute(sql, true))
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('删除成功!')</script>"); 
                showCYInfo(); 
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('删除失败!')</script>");
            }
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('删除发生异常!')</script>");
        }
    }

    //获取批量删除时选择的FID或者批量删除的sql语句
    /// <summary>
    /// 获取批量删除时选择的FID或者批量删除的sql语句
    /// </summary>
    /// <param name="IsSql">是否获取到的是SQL语句</param>
    /// <param name="tableName">如果获取到的是SQL语句则表示的要删的表名，否则传空</param>
    /// <returns></returns>
    private object getSelectedFids(bool IsSql, string tableName)
    {
        string FId = "";
        int ColumnsCount = ywhcy_List.Columns.Count;
        int RowCount = ywhcy_List.Items.Count;
        List<string> FIdList = new List<string>();
        StringBuilder sqlStr = new StringBuilder();
        for (int i = 0; i < RowCount; i++)
        {
            CheckBox cbx = (CheckBox)ywhcy_List.Items[i].Cells[0].Controls[1];
            if (cbx.Checked)
            {
                FId = ywhcy_List.Items[i].Cells[ColumnsCount - 1].Text.Trim();
                sqlStr.Append("delete from " + tableName + " where FID='" + FId + "'");
                FIdList.Add(FId);
            }
        }
        if (IsSql)
        {
            return sqlStr.ToString();
        }
        else
        {
            return FIdList;
        }
    }
}