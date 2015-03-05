using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Approve.RuleCenter;
using Approve.Common;
using System.Configuration;
using System.Data.SqlClient;
public partial class JSDW_ApplyJGYS_ProjectFile_SGTSCXXForm : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private RCenter rcJST = new RCenter("JST_XZSPBaseInfo");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql = string.Format("select top 1* from XM_JGYS_TRANS where YWBM='{0}' and TypeId=5", Id);
            DataTable table = rc.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row1 = table.Rows[0];
                txtLY.Text = row1["LY"].ToString();
                ddlIsTrans.SelectedValue = row1["IsTrans"].ToString();
            }
            sql = string.Format("select top 1 * from XM_SGTSCXX where YWBM='{0}'", Id);
            table.Clear();
            table = rc.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                pageTool tool = new pageTool(this.Page, "txt");
                tool.fillPageControl(row);
                hfId.Value = row["ID"].ToString();
                hfSGTId.Value = row["SGTSCJGBM"].ToString();
                hfKCDWId.Value = row["KCDWBM"].ToString();
                hfSJDWId.Value = row["SJDWBM"].ToString();
                hfSrouce.Value = row["IsSource"].ToString();
                ddlSFTG.SelectedValue = row["YCSCSFTG"].ToString();
                ShowPerSon(row["ID"].ToString());
            }
            else
            {
                sql = string.Format("select top 1 XMBH,JSGM from YW_JGYS where ID='{0}'", Id);
                DataTable dt = rc.GetTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                }
            }
            EnabledControl();
        }
    }
    private void EnabledControl()
    {
        if (FIsApprove == "1" || Audit == "1") //审核页面跳转
        {
            foreach (Control control in this.form1.Controls)
            {
                WebHelper.SetControlEnabled(control);
            }
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
            if (FIsApprove == "1" || Audit == "1") //审核页面跳转
            {
                string perSonId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ID"));
                e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='#' onclick=\"Show('" + perSonId + "')\">查 看</a>";
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = string.Empty;
        string XMId = string.Empty;
        List<SqlParameter> list = new List<SqlParameter>();
        if (string.IsNullOrEmpty(hfId.Value))//新增
        {
            XMId = Guid.NewGuid().ToString();
            sql = @"INSERT INTO [dbCenter].[dbo].[XM_SGTSCXX]
                        ([ID]
                        ,[YWBM]
                        ,[SGTSCXXBM]
                        ,[SGTSCHGSBH]
                        ,[XMBH]
                        ,[SGTSCJGMC]
                        ,[SGTSCJGZZJGDM]
                        ,[SCWCRQ]
                        ,[JSGM]
                        ,[KCDWMC]
                        ,[KCDWZZJGDM]
                        ,[SJDWMC]
                        ,[SJDWZZJGDM]
                        ,[YCSCSFTG]
                        ,[YCSCWFQTS]
                        ,[YCSCWFNum]
                        ,SGTSCJGBM
                        ,KCDWBM
                        ,SJDWBM)
                    VALUES(@ID,@YWBM,@SGTSCXXBM,@SGTSCHGSBH,@XMBH,@SGTSCJGMC,@SGTSCJGZZJGDM,@SCWCRQ,@JSGM,@KCDWMC,@KCDWZZJGDM,@SJDWMC,@SJDWZZJGDM,@YCSCSFTG,@YCSCWFQTS,@YCSCWFNum,@SGTSCJGBM,@KCDWBM,@SJDWBM);";
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = XMId, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YWBM", Value = Id, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SGTSCXXBM", Value = Guid.NewGuid().ToString(), SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SGTSCHGSBH", Value = txtSGTSCHGSBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMBH", Value = txtXMBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SGTSCJGMC", Value = txtSGTSCJGMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SGTSCJGZZJGDM", Value = txtSGTSCJGZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SCWCRQ", Value = txtSCWCRQ.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSGM", Value = txtJSGM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@KCDWMC", Value = txtKCDWMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@KCDWZZJGDM", Value = txtKCDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SJDWMC", Value = txtSJDWMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SJDWZZJGDM", Value = txtSJDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YCSCSFTG", Value = ddlSFTG.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YCSCWFQTS", Value = txtYCSCWFQTS.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YCSCWFNum", Value = txtYCSCWFNum.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SGTSCJGBM", Value = hfSGTId.Value, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@KCDWBM", Value = hfKCDWId.Value, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SJDWBM", Value = hfSJDWId.Value, SqlDbType = SqlDbType.VarChar });

            sql += string.Format(@"INSERT INTO [dbo].[XM_JGYS_TRANS]
                       ([ID]
                       ,[YWBM]
                       ,[TypeId]
                       ,[IsTrans]
                       ,[LY])
                        values(NEWID(),'{0}','{1}','{2}','{3}');", Id, 5, ddlIsTrans.SelectedValue, txtLY.Text.Replace("'", string.Empty));
        }
        else
        {
            sql = @"UPDATE [dbCenter].[dbo].[XM_SGTSCXX]
                        SET [SGTSCHGSBH] = @SGTSCHGSBH
                            ,[XMBH] = @XMBH
                            ,[SGTSCJGMC] = @SGTSCJGMC
                            ,[SGTSCJGZZJGDM] = @SGTSCJGZZJGDM
                            ,[SCWCRQ] = @SCWCRQ
                            ,[JSGM] = @JSGM
                            ,[KCDWMC] = @KCDWMC
                            ,[KCDWZZJGDM] = @KCDWZZJGDM
                            ,[SJDWMC] = @SJDWMC
                            ,[SJDWZZJGDM] = @SJDWZZJGDM
                            ,[YCSCSFTG] = @YCSCSFTG
                            ,[YCSCWFQTS] = @YCSCWFQTS
                            ,[YCSCWFNum] = @YCSCWFNum
                            ,[SGTSCJGBM] = @SGTSCJGBM
                            ,[KCDWBM] = @KCDWBM
                            ,[SJDWBM] = @SJDWBM
                        WHERE YWBM=@YWBM;";
            XMId = hfId.Value;
            list.Add(new SqlParameter() { ParameterName = "@YWBM", Value = Id, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SGTSCHGSBH", Value = txtSGTSCHGSBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMBH", Value = txtXMBH.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SGTSCJGMC", Value = txtSGTSCJGMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SGTSCJGZZJGDM", Value = txtSGTSCJGZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SCWCRQ", Value = txtSCWCRQ.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@JSGM", Value = txtJSGM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@KCDWMC", Value = txtKCDWMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@KCDWZZJGDM", Value = txtKCDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SJDWMC", Value = txtSJDWMC.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SJDWZZJGDM", Value = txtSJDWZZJGDM.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YCSCSFTG", Value = ddlSFTG.SelectedValue, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YCSCWFQTS", Value = txtYCSCWFQTS.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YCSCWFNum", Value = txtYCSCWFNum.Text, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SGTSCJGBM", Value = hfSGTId.Value, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@KCDWBM", Value = hfKCDWId.Value, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@SJDWBM", Value = hfSJDWId.Value, SqlDbType = SqlDbType.VarChar });

            sql += string.Format(@"UPDATE [dbo].[XM_JGYS_TRANS]
                                       SET [IsTrans] = {0}
                                          ,[LY] = '{1}'
                                     WHERE YWBM='{2}' and TypeId=5;", ddlIsTrans.SelectedValue, txtLY.Text.Replace("'", string.Empty), Id);
        }
        pageTool tool = new pageTool(this.Page, "txt");
        bool success = rc.PExcute(sql, list.ToArray());
        if (success)
        {
            hfId.Value = XMId;
            ShowPerSon(hfId.Value);
            tool.showMessage("保存成功");
        }
        else
            tool.showMessage("保存失败");
    }
    private void ShowPerSon(string STGId)
    {
        string sql = string.Format(@"select * from XM_SGTKCSJMX where SGTSCID='{0}'",STGId);
        DataTable table = rc.GetTable(sql);
        DG_List.DataSource = table;
        DG_List.DataBind();
    }
    private string Id
    {
        get
        {
            return Request.QueryString["JG_Id"];
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
    private string Audit
    {
        get
        {
            return Request.QueryString["audit"];
        }
    }
    protected void btnChoosePerson_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hfPerSonId.Value))
        {
            string[] items = hfPerSonId.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> list = new List<string>();
            for (int i = 0; i < items.Length; i++)
			{
                list.Add("'" + items[i] + "'");
			}
           // string sql = string.Format(@"INSERT INTO [dbCenter].[dbo].[XM_SGTKCSJMX]
           // select NEWID(),'{1}',a.QYBM,b.QYMC,getdate(),b.JGDM,a.XM,a.ZJLXBM,a.ZJLXMC,a.ZJBH,a.ZCDJBM,a.ZCDJMC,'',a.RYBH from {2}.[dbo].[RY_RYZSXX] a left join [JST_XZSPBaseInfo].[dbo].QY_JBXX b on a.QYBM = b.QYBM where a.RYZSXXID in ({0})", string.Join(",", list.ToArray()), hfId.Value, PrePerSon);
            string sql = string.Format("select b.QYBM,b.QYMC,b.JGDM,a.RYZSXXID,a.XM,a.SFZH from [dbo].[RY_RYZSXX] a left join [dbo].QY_JBXX b on a.QYBM = b.QYBM where a.RYZSXXID in ({0})", string.Join(",", list.ToArray()));
            DataTable table = rcJST.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    sql = @"INSERT INTO [dbo].[XM_SGTKCSJMX]
                                   ([ID]
                                   ,[SGTSCID]
                                   ,[SSDWID]
                                   ,[SSDWMC]
                                   ,[SSDWZZJGDM]
                                   ,[RYBH]
                                   ,[RYXM]
                                   ,[ZJHM])values(NEWID(),@SGTSCID,@SSDWID,@SSDWMC,@SSDWZZJGDM,@RYBH,@RYXM,@ZJHM)";
                    List<SqlParameter> listParam = new List<SqlParameter>();
                    listParam.Add(new SqlParameter() { ParameterName = "@SGTSCID", Value = hfId.Value, SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@SSDWID", Value = row["QYBM"].ToString(), SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@SSDWMC", Value = row["QYMC"].ToString(), SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@SSDWZZJGDM", Value = row["JGDM"].ToString(), SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@RYBH", Value = row["RYZSXXID"].ToString(), SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@RYXM", Value = row["XM"].ToString(), SqlDbType = SqlDbType.VarChar });
                    listParam.Add(new SqlParameter() { ParameterName = "@ZJHM", Value = row["SFZH"].ToString(), SqlDbType = SqlDbType.VarChar });
                    rc.PExcute(sql, listParam.ToArray());
                }
                ShowPerSon(hfId.Value);
            }
            //if (success)
                
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            pageTool tool = new pageTool(this.Page, "txt");
            string Id = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string sql = string.Format(@"delete from XM_SGTKCSJMX where Id='{0}'",Id);
            bool success = rc.PExcute(sql);
            if (success)
            {
                tool.showMessage("删除成功");
                ShowPerSon(hfId.Value);
            }
            else
                tool.showMessage("删除失败");
        }
    }
    protected void btnRefreash_Click(object sender, EventArgs e)
    {
        ShowPerSon(hfId.Value);
    }
    protected void btnSearchSGT_Click(object sender, EventArgs e)
    {
        string QYBM = hfSGTId.Value;
        if (!string.IsNullOrEmpty(QYBM))
        {
            string sql = string.Format("select top 1* from QY_JBXX where QYBM='{0}'", QYBM);
            DataTable dt = rcJST.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtSGTSCJGMC.Text = row["QYMC"].ToString();
                txtSGTSCJGZZJGDM.Text = row["JGDM"].ToString();
            }
        }
    }
    protected void btnKC_Click(object sender, EventArgs e)
    {
        string QYBM = hfKCDWId.Value;
        if (!string.IsNullOrEmpty(QYBM))
        {
            string sql = string.Format("select top 1* from QY_JBXX where QYBM='{0}'", QYBM);
            DataTable dt = rcJST.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtKCDWMC.Text = row["QYMC"].ToString();
                txtKCDWZZJGDM.Text = row["JGDM"].ToString();
            }
        }
    }
    protected void btnSJDW_Click(object sender, EventArgs e)
    {
        string QYBM = hfSJDWId.Value;
        if (!string.IsNullOrEmpty(QYBM))
        {
            string sql = string.Format("select top 1* from QY_JBXX where QYBM='{0}'", QYBM);
            DataTable dt = rcJST.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtSJDWMC.Text = row["QYMC"].ToString();
                txtSJDWZZJGDM.Text = row["JGDM"].ToString();
            }
        }
    }
}