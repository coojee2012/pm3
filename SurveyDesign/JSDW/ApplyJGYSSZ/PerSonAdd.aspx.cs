using Approve.Common;
using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ProjectData;
using System.Data.SqlClient;

public partial class JSDW_ApplyJGYS_PerSonAdd : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            hfId.Value = Id;
            if (!string.IsNullOrEmpty(hfId.Value))
            {
                string sql = string.Format(@"select top 1 * from YW_PERSON where ID='{0}'", hfId.Value);
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                    ddlSex.SelectedValue = row["Sex"].ToString();
                    ddlXL.SelectedValue = row["XL"].ToString();
                    ddlZC.SelectedValue = row["ZC"].ToString();
                    ddlRYLX.SelectedValue = row["RYLX"].ToString();
                    hfCompanyId.Value = row["COMPANYID"].ToString();
                    string RYLX = row["RYLX"].ToString();
                }
            }
            if (!string.IsNullOrEmpty(Id))
                ddlRYLX.Enabled = false;
            EnabledControl();
        }
    }
    private void EnabledControl()
    {
        if (IsShow == "1") //审核页面跳转
        {
            foreach (Control control in this.form1.Controls)
            {
                WebHelper.SetControlEnabled(control);
            }
        }
    }
    private void BindControl()
    {
        //施工人员类型
        var data = db.getDicList(200122);
        ddlRYLX.DataSource = data;
        ddlRYLX.DataTextField = "fname";
        ddlRYLX.DataValueField = "fnumber";
        ddlRYLX.DataBind();
        if (TypeId == "4" || TypeId == "5")//勘察和设计不分类型
        {
            ddlRYLX.Visible = false;
            ddlRYLX.Items.Insert(0, new ListItem() { Text = "", Value = "" });
        }else
        ddlRYLX.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });
      

        var XL = db.getDicList(107);
        ddlXL.DataSource = XL;
        ddlXL.DataTextField = "fname";
        ddlXL.DataValueField = "fnumber";
        ddlXL.DataBind();
        ddlXL.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });

        var ZC = db.getDicList(5080);
        ddlZC.DataSource = ZC;
        ddlZC.DataTextField = "fname";
        ddlZC.DataValueField = "fnumber";
        ddlZC.DataBind();
        ddlZC.Items.Insert(0, new ListItem() { Text = "--请选择--", Value = "" });
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "txt");
        string sql = string.Empty;
        List<SqlParameter> list = new List<SqlParameter>();
        if (string.IsNullOrEmpty(hfId.Value))//新增
        {
            sql = "select top 1 ID from YW_PERSON where COMPANYID=@COMPANYID and SFZH=@SFZH";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter() { ParameterName = "@SFZH", Value = txtSFZH.Text, SqlDbType = SqlDbType.VarChar };
            param[1] = new SqlParameter() { ParameterName = "@COMPANYID", Value = CompanyId, SqlDbType = SqlDbType.VarChar };
            //param[2] = new SqlParameter() { ParameterName = "@ZSBH", Value = txtZSBH.Text, SqlDbType = SqlDbType.VarChar };
            DataTable table = rc.GetTable(sql, param);
            if (table != null && table.Rows.Count > 0)
            {
                tool.showMessage("该人员已添加");
                return;
            }
            string guid = Guid.NewGuid().ToString();
            sql = @"INSERT INTO [dbCenter].[dbo].[YW_PERSON]
                        ([ID]
                        ,[COMPANYID]
                        ,[RYBH]
                        ,[RYLX]
                        ,[RYLXMC]
                        ,[PerSonName]
                        ,[SFZH]
                        ,[Sex]
                        ,[XL]
                        ,[YDDH]
                        ,[ZC]
                        ,[ZW]
                        ,[LXDH]
                        ,[ZY]
                        ,[ZSBH]
                        ,[DJ]
                        ,[ZCBH]
                        ,[ZCZY]
                        ,[ZCRQ])
                        VALUES(@ID,@COMPANYID,@RYBH,@RYLX,@RYLXMC,@PerSonName,@SFZH,@Sex,@XL,@YDDH,@ZC,@ZW,@LXDH,@ZY,@ZSBH,@DJ,@ZCBH,@ZCZY,@ZCRQ)";
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = guid, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@COMPANYID", Value = CompanyId, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@RYBH", Value = Guid.NewGuid().ToString(), SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@RYLX", Value = ddlRYLX.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@RYLXMC", Value = ddlRYLX.SelectedItem.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@PerSonName", Value = txtPerSonName.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SFZH", Value = txtSFZH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@Sex", Value = ddlSex.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XL", Value = ddlXL.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YDDH", Value = txtYDDH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZC", Value = ddlZC.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZW", Value = txtZW.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@LXDH", Value = txtLXDH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZY", Value = txtZY.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZSBH", Value = txtZSBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@DJ", Value = txtDJ.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZCBH", Value = txtZCBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZCZY", Value = txtZCZY.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZCRQ", Value = txtZCRQ.Text, SqlDbType = SqlDbType.VarChar });
            hfId.Value = guid;
            hfCompanyId.Value = CompanyId;
        }
        else { //编辑
            sql = "select top 1 ID from YW_PERSON where COMPANYID=@COMPANYID and SFZH=@SFZH and ID!=@ID";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter() { ParameterName = "@SFZH", Value = txtSFZH.Text, SqlDbType = SqlDbType.VarChar };
            param[1] = new SqlParameter() { ParameterName = "@COMPANYID", Value = hfCompanyId.Value, SqlDbType = SqlDbType.VarChar };
            param[2] = new SqlParameter() { ParameterName = "@ID", Value = hfId.Value, SqlDbType = SqlDbType.VarChar };
            //param[3] = new SqlParameter() { ParameterName = "@ZSBH", Value = txtZSBH.Text, SqlDbType = SqlDbType.VarChar };
            DataTable table = rc.GetTable(sql, param);
            if (table != null && table.Rows.Count > 0)
            {
                tool.showMessage("该人员已添加");
                return;
            }
            sql = @"UPDATE [dbCenter].[dbo].[YW_PERSON]
                           SET [PerSonName] = @PerSonName
                              ,[SFZH] = @SFZH
                              ,[Sex] = @Sex
                              ,[XL] = @XL
                              ,[YDDH] = @YDDH
                              ,[ZC] = @ZC
                              ,[ZW] = @ZW
                              ,[LXDH] = @LXDH
                              ,[ZY] = @ZY
                              ,[ZSBH] = @ZSBH
                              ,[DJ] = @DJ
                              ,[ZCBH] = @ZCBH
                              ,[ZCZY] = @ZCZY
                              ,[ZCRQ] = @ZCRQ
                         WHERE ID=@ID";
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = hfId.Value, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@PerSonName", Value = txtPerSonName.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SFZH", Value = txtSFZH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@Sex", Value = ddlSex.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XL", Value = ddlXL.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YDDH", Value = txtYDDH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZC", Value = ddlZC.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZW", Value = txtZW.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@LXDH", Value = txtLXDH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZY", Value = txtZY.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZSBH", Value = txtZSBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@DJ", Value = txtDJ.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZCBH", Value = txtZCBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZCZY", Value = txtZCZY.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@ZCRQ", Value = txtZCRQ.Text, SqlDbType = SqlDbType.VarChar });
        }
        
        bool success = rc.PExcute(sql,list.ToArray());
        if (success)
            tool.showMessage("操作成功");
        else
            tool.showMessage("操作失败");

    }
    private string CompanyId {
        get {
            return Request.QueryString["companyId"];
        }
    }
    private string Id
    {
        get
        {
            return Request.QueryString["Id"];
        }
    }
    private string JGId {
        get {
            return Request.QueryString["JGId"];
        }
    }
    private string IsShow {
        get { return Request.QueryString["IsShow"]; }
    }
    private string TypeId {
        get {
            return Request.QueryString["typeId"];
        }
    }
}