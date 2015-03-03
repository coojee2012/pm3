  using Approve.Common;
using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyJGYS_CommpanyEdit : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private RCenter rcJST = new RCenter("JST_XZSPBaseInfo");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfId.Value = Id;
            ltrText.Text = TypeName;
            hfCompanyId.Value = CompanyId;
            ShowForm();
            ShowData();
            hfTypeId.Value = TypeId;
            if (!string.IsNullOrEmpty(FIsApprove))
            {
                if (FIsApprove == "1")
                {
                    btnSave.Enabled = false;
                    btnAddPerson.Enabled = false;
                    btnChoosePerson.Enabled = false;
                }
            }
            if (TypeId == "4" || TypeId == "5" || TypeId == "6")//勘察单位、设计单位、监理单位
            {
                zxzz.Visible = false;
                ltrZZ.Text = "资质项：";
            }
        }
    }
    private void ShowData()
    {
        string sql = @"select * from YW_PERSON where COMPANYID='" + CompanyId + "'";
        DataTable table = rc.GetTable(sql);
        DG_List.DataSource = table;
        DG_List.DataBind();
    }
    private void ShowForm()
    {
        string sql = @"select top 1 * from YW_COMPANY where ID='" + CompanyId + "'";
        DataTable table = rc.GetTable(sql);
        if (table != null && table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            pageTool tool = new pageTool(this.Page, "txt");
            tool.fillPageControl(row);
            hfQYBM.Value = row["QYBM"].ToString();
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
            if (FIsApprove == "1")
            {
                string perSonId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ID"));
                e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='#' onclick=\"Show('" + perSonId + "')\">查 看</a>";
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string guid = Guid.NewGuid().ToString();
        string sql = @"UPDATE [dbCenter].[dbo].[YW_COMPANY]
                        SET [DWMC] = @DWMC
                            ,[DWDZ] = @DWDZ
                            ,[ZZX] = @ZZX
                            ,[ZZJGDM] = @ZZJGDM
                            ,[ZXZZ] = @ZXZZ
                            ,[FDDBR] = @FDDBR
                            ,[LXR] = @LXR
                            ,[YDDH] = @YDDH
                            ,[LXDH] = @LXDH
                            ,[BZ] = @BZ
                        WHERE ID=@ID";
        List<SqlParameter> list = new List<SqlParameter>();
        list.Add(new SqlParameter() { ParameterName = "@ID", Value = CompanyId, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@DWMC", Value = txtDWMC.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@DWDZ", Value = txtDWDZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZZX", Value = txtZZX.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZZJGDM", Value = txtZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@ZXZZ", Value = txtZXZZ.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@FDDBR", Value = txtFDDBR.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXR", Value = txtLXR.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@YDDH", Value = txtYDDH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@LXDH", Value = txtLXDH.Text, SqlDbType = SqlDbType.VarChar });
        list.Add(new SqlParameter() { ParameterName = "@BZ", Value = txtBZ.Text, SqlDbType = SqlDbType.VarChar });
        pageTool tool = new pageTool(this.Page, "txt");
        bool success = rc.PExcute(sql,list.ToArray());
        if (success)
        {
            ShowData();
            tool.showMessage("操作成功");
        }
        else
            tool.showMessage("操作失败");
    }
    protected void btnChoosePerson_Click(object sender, EventArgs e)
    {
        string perSonId = hfPerSonId.Value;
        if (!string.IsNullOrEmpty(perSonId))
        {
            string[] personItems = perSonId.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            string[] items = personItems[0].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> list = new List<string>();
            for (int i = 0; i < items.Length; i++)
            {
                list.Add("'" + items[i] + "'");
            }
            string sql = string.Format(@"select RYZSXXID,XM,SFZH,XB,ZW,ZCZSBH,ZCZY,RYBH from dbo.RY_RYZSXX where RYZSXXID in ({0})", string.Join(",", list.ToArray()));

            DataTable table = rcJST.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {

                    DataRow row = table.Rows[i];
                    sql = string.Format(@"INSERT INTO [dbo].[YW_PERSON]
                               ([ID]
                               ,[COMPANYID]
                               ,[RYBH]
                               ,[RYLX]
                               ,[RYLXMC]
                               ,[PerSonName]
                               ,[SFZH]
                               ,[Sex]
                               ,[ZW]
                               ,[ZCBH]
                               ,[ZCZY]
                               ,[RYBM])
                         SELECT NEWID(),@COMPANYID,@RYBH,@RYLX,@RYLXMC,@PerSonName,@SFZH,@Sex,@ZW,@ZCBH,@ZCZY,@RYBM");
                    List<SqlParameter> listParam = new List<SqlParameter>();
                    listParam.Add(new SqlParameter() { ParameterName = "@COMPANYID", Value = hfCompanyId.Value, SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@RYBH", Value = row["RYZSXXID"].ToString(), SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@RYLX", Value = personItems[1], SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@RYLXMC", Value = personItems[2], SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@PerSonName", Value = row["XM"].ToString(), SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@SFZH", Value = row["SFZH"].ToString(), SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@Sex", Value = row["XB"].ToString(), SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@ZW", Value = row["ZW"].ToString(), SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@ZCBH", Value = row["ZCZSBH"].ToString(), SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@ZCZY", Value = row["ZCZY"].ToString(), SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@RYBM", Value = row["RYBH"].ToString(), SqlDbType = SqlDbType.VarChar });
                    rc.PExcute(sql, listParam.ToArray());
                }
                ShowData();
            }
        }
    }
    protected void btnAddPerson_Click(object sender, EventArgs e)
    {
        string CompanyId = hfCompanyId.Value;
        if (!string.IsNullOrEmpty(CompanyId))
        {
            ShowData();
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            string perSonId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string sql = string.Empty;
            sql += string.Format("DELETE from YW_PERSON where ID='{0}'", perSonId);
            pageTool tool = new pageTool(this.Page, "txt");
            bool success = rc.PExcute(sql);
            if (success)
            {
                tool.showMessage("删除成功");
                ShowData();
            }
            else
            {
                tool.showMessage("删除失败");
            }
        }
    }
    private string Id
    {
        get
        {
            return Request.QueryString["JG_Id"];
        }
    }
    private string CompanyId
    {
        get
        {
            return Request.QueryString["companyId"];
        }
    }
    private string TypeId
    {
        get
        {
            return Request.QueryString["typeId"];
        }
    }
    private string TypeName
    {
        get
        {
            return Request.QueryString["typeName"];
        }
    }
    private string FIsApprove
    {
        get
        {
            string value = Session["FIsApprove"] == null ? "" : Session["FIsApprove"].ToString();
            if (string.IsNullOrEmpty(value))
                return "0";
            return value;
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ShowData();
    }
}