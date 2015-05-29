using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using Approve.RuleCenter;
using Tools;
using System.Data;
using EgovaBLL;

public partial class JSDW_ApplySGXKZGL_EntListForBG : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hf_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            this.hf_FEntType.Value = EConvert.ToString(Request["FEntType"]);
            showTitle();
            showInfo();
            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool.ExecuteScript("btnEnable();");
            }
        }
        else
        {
            showInfo();
        }
    }
    //显示
    private void showTitle()
    {
        switch (hf_FEntType.Value)
        {
            case "2":
                lblTitle.InnerText = "施工总承包单位";
                break;
            case "3":
                lblTitle.InnerText = "专业承包单位";
                break;
            case "4":
                lblTitle.InnerText = "劳务分包单位";
                break;
            case "5":
                lblTitle.InnerText = "勘察单位";
                break;
            case "6":
                lblTitle.InnerText = "设计单位";
                break;
            case "7":
                lblTitle.InnerText = "监理单位";
                break;
        }


    }
    private string getEmpType(string id)
    {
        switch (id)
        {
            default:
                return "项目负责人";
            case "1":
                return "项目负责人";
            case "2":
                return "项目技术负责人";
            case "3":
                return "安全负责人";
            case "4":
                return "施工员";
            case "5":
                return "质量员";
            case "6":
                return "安全员";
            case "7":
                return "材料员";
            case "8":
                return "预算员";
            case "9":
                return "总监理工程师";
            case "10":
                return "专业监理工程师";
            case "11":
                return "监理员";
            case "12":
                return "其他";
        }
    }
    //显示
    private void showInfo()
    {
        if (isDel.Value == "1")
        { return; }
        if (hf_FEntType.Value == "2" || hf_FEntType.Value == "3" || hf_FEntType.Value == "4")
        {

        }
        else
        {
            dg_List.Columns[4].HeaderText = "资质项";
        }
        EgovaDB dbContext = new EgovaDB();

        TC_SGXKZ_BGPrjInfo sbg = dbContext.TC_SGXKZ_BGPrjInfo.Where(t => t.FAppId == hf_FAppId.Value).FirstOrDefault();
        t_FPrjItemId.Value = sbg.FPrjItemId;
        //TC_SGXKZ_PrjInfo p = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FId == sbg.FPrjInfoId).FirstOrDefault();
        //var v = dbContext.TC_PrjItem_Ent.Where(t => (t.FAppId == p.FAppId || t.FAppId == hf_FAppId.Value) && t.FEntType.Equals(hf_FEntType.Value));
        //modify by psq 获取当前流程对应类型企业类型即可。ly 不要再次修改。
        var v = dbContext.TC_PrjItem_Ent.Where(t =>  t.FAppId == hf_FAppId.Value && t.FEntType.Equals(hf_FEntType.Value));
        dg_List.DataSource = v;
        dg_List.DataBind();
    }
    //保存
    private void saveInfo()
    {

    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        updateBG();
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, dbContext.TC_PrjItem_Ent, tool_Deleting);
        isDel.Value = "0";
        showInfo();
    }
    //级联删除人员
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        if (dbContext != null)
        {
            //var para = dbContext.TC_PrjItem_Emp.Where(t => FIdList.ToArray().Contains(t.FEntId));            
            //dbContext.TC_PrjItem_Emp.DeleteAllOnSubmit(para);

            //新的删除方法
            IList<string> entlist = GetGridCheckIdsnew(this.dg_List);                      //FIdList.ToArray();
            foreach (string x in entlist)
            {
                string sql = @"select  FId  from  TC_PrjItem_Emp  where  FAppId = '" + hf_FAppId.Value + "'  and  FEntId = '" + x + "'  and FEntType = '" + hf_FEntType.Value + "'";
                DataTable dt = rc.GetTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //先记录日志                       
                        TC_PrjItem_Emp pe = dbContext.TC_PrjItem_Emp.Where(t => t.FId == dt.Rows[i][0].ToString()).FirstOrDefault();
                        TC_SGXKZ_RYBGJG sr = new TC_SGXKZ_RYBGJG();
                        sr.FId = Guid.NewGuid().ToString();
                        sr.FAppId = hf_FAppId.Value;
                        sr.FPrjItemId = t_FPrjItemId.Value;
                        sr.RYLX = getEmpType(pe.EmpType.ToString());
                        sr.XM = pe.FHumanName;
                        sr.ZSBH = pe.ZSBH;
                        sr.QYMC = pe.FEntName;
                        //sr.FLinkId = pe.FEntId;
                        sr.FLinkId = pe.FEmpId;   // by zyd 5.29
                        sr.BGQK = "退出";
                        sr.BGTime = DateTime.Now;
                        dbContext.TC_SGXKZ_RYBGJG.InsertOnSubmit(sr);
                        dbContext.SubmitChanges();
                        //再删除人员
                        string sqldel = "update TC_PrjItem_Emp set needDel = 1 where  fid = '" + dt.Rows[i][0].ToString() + "'";
                        rc.PExcute(sqldel);                                       
                    }
                }
                string sqldel2 = "delete from TC_PrjItem_ent  where  FAppId = '" + hf_FAppId.Value + "' and qyid='" + x + "' and FEntType = '" + hf_FEntType.Value + "'";
                rc.PExcute(sqldel2);
            }
        }
    }

    /// <summary>
    /// 返回企业的编号队列，不是fid
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    private IList<string> GetGridCheckIdsnew(DataGrid grid)
    {
        string FqyId = "";

        int RowCount = grid.Items.Count;
        IList<string> FqyIdList = new List<string>();
        for (int i = 0; i < grid.Items.Count; i++)
        {
            CheckBox cbx = (CheckBox)grid.Items[i].Cells[0].Controls[1];
            if (cbx.Checked)
            {
                FqyId = grid.Items[i].Cells[grid.Columns.Count - 1].Text.Trim();

                FqyIdList.Add(FqyId);
            }
        }
        return FqyIdList;
    }

    protected void updateBG()
    {
        //string FId = "";
        //IList<string> FIdList = new List<string>();
        //for (int i = 0; i < dg_List.Items.Count; i++)
        //{
        //    CheckBox cbx = (CheckBox)dg_List.Items[i].Cells[0].Controls[1];
        //    if (cbx.Checked)
        //    {
        //        FId = dg_List.Items[i].Cells[dg_List.Columns.Count - 1].Text.Trim();
        //        updateYQBG(FId);
        //    }
        //}

        string FId = "";
        IList<string> FIdList = new List<string>();
        for (int i = 0; i < dg_List.Items.Count; i++)
        {
            CheckBox cbx = (CheckBox)dg_List.Items[i].Cells[0].Controls[1];
            if (cbx.Checked)
            {
                //增加了一行名为qyid的列，所以FID向前移动一列
                FId = dg_List.Items[i].Cells[dg_List.Columns.Count - 2].Text.Trim();
                updateYQBG(FId);
            }
        }

    }
    private void updateYQBG(string FId)
    {
        EgovaDB dbContext = new EgovaDB();
        TC_PrjItem_Ent ent = dbContext.TC_PrjItem_Ent.Where(t => t.FId == FId).FirstOrDefault();
        TC_SGXKZ_QYBGJG sq = new TC_SGXKZ_QYBGJG();
        
        sq.FId = Guid.NewGuid().ToString();
        sq.FAppId = this.hf_FAppId.Value;
        sq.FPrjItemId = t_FPrjItemId.Value;
        sq.YQLX = lblTitle.InnerText;
        sq.YQMC = ent.FName;
        sq.BGTime = DateTime.Now;
        sq.FLinkId = ent.QYID;
        sq.BGQK = "退出";
        dbContext.TC_SGXKZ_QYBGJG.InsertOnSubmit(sq);
        dbContext.SubmitChanges();  
       
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string fAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string fPrjItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjItemId"));
            string fPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));

            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('BGEntInfo.aspx?fId=" + fId + "&FEntType=" + hf_FEntType.Value + "&fAppId=" + fAppId + "&fPrjItemId=" + fPrjItemId + "&fprjId=" + fPrjId + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;

    }
}