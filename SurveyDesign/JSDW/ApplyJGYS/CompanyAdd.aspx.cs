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

public partial class JSDW_ApplyJGYS_CompanyAdd : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private RCenter rcJST = new RCenter("JST_XZSPBaseInfo");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfTypeId.Value = TypeId;
            hfId.Value = Id;
            ltrText.Text = TypeName;
            if (TypeId == "4" || TypeId == "5" || TypeId == "6")//勘察单位、设计单位、监理单位
            {
                zxzz.Visible = false;
                ltrZZ.Text = "资质项：";
                ShowQYType.Value = "1";
            }
        }
    }
    
    private void ShowData(string companyId)
    {
        string sql = @"select * from YW_PERSON where COMPANYID='" + companyId + "'";
        DataTable table = rc.GetTable(sql);
        DG_List.DataSource = table;
        DG_List.DataBind();
    }
    private void ShowForm()
    {
        string sql = @"select top 1 * from YW_COMPANY where JGYS_ID='" + Id + "'";
        DataTable table = rc.GetTable(sql);
        if (table != null && table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            pageTool tool = new pageTool(this.Page, "txt");
            tool.fillPageControl(row);
            hfCompanyId.Value = row["ID"].ToString();
            hfQYBM.Value = row["QYBM"].ToString();
            ShowData(row["ID"].ToString());
        }
    }
    private string Id
    {
        get
        {
            return Request.QueryString["JG_Id"];
        }
    }
    private string TypeId {
        get {
            return Request.QueryString["typeId"];
        }
    }
    private string TypeName {
        get {
            return Request.QueryString["typeName"];
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string guid = Guid.NewGuid().ToString();
        string sql = string.Empty;
        List<SqlParameter> list = new List<SqlParameter>();
        if (string.IsNullOrEmpty(hfCompanyId.Value))//新增
        {
            sql = @"INSERT INTO [dbCenter].[dbo].[YW_COMPANY]
                            ([ID]
                            ,[JGYS_ID]
                            ,[TypeId]
                            ,[QYBM]
                            ,[DWMC]
                            ,[DWDZ]
                            ,[ZZX]
                            ,[ZZJGDM]
                            ,[ZXZZ]
                            ,[FDDBR]
                            ,[LXR]
                            ,[YDDH]
                            ,[LXDH]
                            ,[BZ])
                        VALUES(@ID,@JGYS_ID,@TypeId,@QYBM,@DWMC,@DWDZ,@ZZX,@ZZJGDM,@ZXZZ,@FDDBR,@LXR,@YDDH,@LXDH,@BZ)";
            hfCompanyId.Value = guid;
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = guid, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JGYS_ID", Value = Id, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@TypeId", Value = TypeId, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@QYBM", Value = hfQYBM.Value, SqlDbType = SqlDbType.VarChar });
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
            hfSourceQYBM.Value = hfQYBM.Value;
        }
        else {
            sql = @"UPDATE [dbCenter].[dbo].[YW_COMPANY]
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
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = hfCompanyId.Value, SqlDbType = SqlDbType.VarChar });
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
        }
        pageTool tool = new pageTool(this.Page, "txt");
        bool success = rc.PExcute(sql,list.ToArray());
        if (success)
        {
            ShowData(hfCompanyId.Value);
            tool.showMessage("保存成功");
        }
        else
            tool.showMessage("保存失败");
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
                ShowData(hfCompanyId.Value);
            }
        }
    }
    //protected void btnAddPerson_Click(object sender, EventArgs e)
    //{
    //    string CompanyId = hfCompanyId.Value;
    //    if (!string.IsNullOrEmpty(CompanyId))
    //    {
    //        ShowData(hfCompanyId.Value);
    //    }
    //}
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
                ShowData(hfCompanyId.Value);
            }
            else
            {
                tool.showMessage("删除失败");
            }
        }
    }
    protected void btnChoose_Click(object sender, EventArgs e)
    {
        string sql = string.Format("delete from YW_PERSON where COMPANYID='{0}'", hfCompanyId.Value);
        if (hfQYBM.Value != hfSourceQYBM.Value)
        {
            rc.PExcute(sql);//删除原有人员
            ShowData(hfCompanyId.Value);
        }
        txtZZX.Text = string.Empty;
        txtZXZZ.Text = string.Empty;
        sql = string.Format(@"select top 1* from [dbo].[QY_JBXX] where QYBM='{0}'",  hfQYBM.Value);
        DataTable table = rcJST.GetTable(sql);
        if (table != null && table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            txtDWMC.Text = row["QYMC"].ToString();
            txtDWDZ.Text = row["QYXXDZ"].ToString();
            txtFDDBR.Text = row["FRDB"].ToString();
            txtLXR.Text = row["LXR"].ToString();
            txtYDDH.Text = row["FRDBSJH"].ToString();
            txtLXDH.Text = row["LXDH"].ToString();
            txtZZJGDM.Text = row["JGDM"].ToString();
            if (TypeId == "4" || TypeId == "5" || TypeId == "6")//勘察单位、设计单位、监理单位 无主项
            {
                sql = string.Format("SELECT ZZMC,ZZLB,ZZDJ FROM JST_XZSPBaseInfo.[dbo].QY_QYZZXX WHERE  FCertiId='{0}' AND ZZMC IS NOT NULL", hfQYZSID.Value);
                DataTable dt = rcJST.GetTable(sql);
                List<string> list = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row1 = dt.Rows[i];
                    string value = row1["ZZMC"].ToString() + row1["ZZLB"].ToString() + row1["ZZDJ"].ToString();
                    if (!string.IsNullOrEmpty(value))
                        list.Add(value);
                }
                txtZZX.Text = string.Join(",", list.ToArray());
            }
            else
            {
                sql = string.Format("select ZZMC,SFZX,ZZLB,ZZDJ from [dbo].QY_QYZZXX where QYBM='{0}' and ZZMC IS NOT NULL", hfQYBM.Value);
                DataTable dt = rcJST.GetTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] rows = dt.Select("SFZX=1");
                    if (rows != null && rows.Length > 0)
                    {
                        txtZZX.Text = rows[0]["ZZMC"].ToString() + rows[0]["ZZLB"].ToString() + rows[0]["ZZDJ"].ToString();
                    }
                    DataRow[] ZRows = dt.Select("SFZX=0");
                    if (ZRows != null && ZRows.Length > 0)
                    {
                        List<string> list = new List<string>();
                        for (int i = 0; i < ZRows.Length; i++)
                        {
                            DataRow row1 = ZRows[i];
                            string value = row1["ZZMC"].ToString() + row1["ZZLB"].ToString() + row1["ZZDJ"].ToString();
                            if (!string.IsNullOrEmpty(value))
                                list.Add(value);
                        }
                        txtZXZZ.Text = string.Join(",", list.ToArray());
                    }
                }
            }
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ShowData(hfCompanyId.Value);
    }
    protected void btnAddPerson_Click(object sender, EventArgs e)
    {
        ShowData(hfCompanyId.Value);
    }
}