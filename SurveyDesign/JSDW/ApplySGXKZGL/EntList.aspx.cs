﻿using System;
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

public partial class JSDW_APPLYSGXKZGL_EntList : System.Web.UI.Page
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
    //显示
    private void showInfo()
    {
        if (hf_FEntType.Value == "2" || hf_FEntType.Value == "3" || hf_FEntType.Value == "4")
        {
            
        }
        else
        {
            dg_List.Columns[4].HeaderText = "资质项";
        }
        EgovaDB dbContext = new EgovaDB();
        var v = dbContext.TC_PrjItem_Ent.Where(t => t.FAppId == hf_FAppId.Value && t.FEntType.Equals(hf_FEntType.Value));
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
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        //先删除选择的企业的相关人员
        //DeleteChildren(dg_List);
        tool.DelInfoFromGrid(dg_List, dbContext.TC_PrjItem_Ent, tool_Deleting);
        showInfo();
    }

    //级联删除人员
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {       
        if (dbContext != null)
        {
            IList<string> entlist = GetGridCheckIdsnew(this.dg_List);                      //FIdList.ToArray();
            
            foreach(string x in entlist)
            {  
                
                string sql = @"select  FId  from  TC_PrjItem_Emp  where  FAppId = '"+hf_FAppId.Value+"'  and  FEntId = '"+x+"'  and FEntType = '"+hf_FEntType.Value+"'";
                DataTable dt = rc.GetTable(sql);
                if(dt != null && dt.Rows.Count > 0)
                {
                    for(int i=0;i<dt.Rows.Count;i++)
                    {
                        string sqldel = "delete  from  TC_PrjItem_Emp where  fid = '" + dt.Rows[i][0].ToString() + "'";
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


    /// <summary>
    /// 删除选择的企业的人员
    /// </summary>
    private IList<string> GetGridCheckIds(DataGrid grid)
    {
        string FId = "";

        int RowCount = grid.Items.Count;
        IList<string> FIdList = new List<string>();
        for (int i = 0; i < grid.Items.Count; i++)
        {
            CheckBox cbx = (CheckBox)grid.Items[i].Cells[0].Controls[1];
            if (cbx.Checked)
            {
                FId = grid.Items[i].Cells[grid.Columns.Count - 2].Text.Trim();

                FIdList.Add(FId);
            }
        }
        return FIdList;
    }

    private void DeleteChildren(DataGrid grid)
    {
        IList<string> listid = GetGridCheckIds(grid);
        var para = dbContext.TC_PrjItem_Ent.Where(t => listid.ToArray().Contains(t.FId) && t.FAppId == hf_FAppId.Value && t.FEntType.Equals(hf_FEntType.Value));

        foreach (var emp_temp in para)
        {
            var emp = dbContext.TC_PrjItem_Emp.Where(t => t.FAppId == emp_temp.FAppId && t.FPrjItemId == emp_temp.FPrjItemId && t.FEntId == emp_temp.QYID);

            dbContext.TC_PrjItem_Emp.DeleteAllOnSubmit(emp);
        }
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

            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('EntInfo.aspx?fId=" + fId + "&FEntType=" + hf_FEntType.Value + "&fAppId=" + fAppId + "&fPrjItemId=" + fPrjItemId + "&fprjId=" + fPrjId + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;

    }
}