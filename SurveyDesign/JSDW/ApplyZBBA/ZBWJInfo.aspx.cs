﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using EgovaDAO;
using Tools;
using System.Data;

public partial class JSDW_ApplyZBBA_ZBWJInfo : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindControl();
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                txtFId.Value = Request.QueryString["fid"];
                ShowTitle();
            }
            else
            {

                EgovaDB dbContext = new EgovaDB();
                var jsdw = dbContext.TC_ZBWJ_Record
                    .Where(t => t.FAppId == EConvert.ToString(Session["FAppId"]))
                    .FirstOrDefault();
                if (jsdw != null)
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.fillPageControl(jsdw);
                    ViewState["FID"] = jsdw.FId;
                    ViewState["FAppId"] = jsdw.FAppId;
                    txtFId.Value = jsdw.FId;
                    if (jsdw.ZBZZXS == "11221002")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "showTr", "<script>showTr();</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "hideTr", "<script>hideTr();</script>");
                        t_DLJG.Text = dbContext.CF_Ent_BaseInfo.Where(d => d.FId == CurrentEntUser.EntId).Select(d => d.FName).FirstOrDefault();
                    }
                    ShowFile(txtFId.Value);
                    
                }
            }
            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool1.ExecuteScript("btnEnable();");
            }
        }
    }

    void BindControl()
    {
        //发包方式
        DataTable dt = rc.getDicTbByFNumber("112209");
        t_FBFS.DataSource = dt;
        t_FBFS.DataTextField = "FName";
        t_FBFS.DataValueField = "FNumber";
        t_FBFS.DataBind();

        //资格预审方式
        dt = rc.getDicTbByFNumber("112207");
        t_ZGYSFS.DataSource = dt;
        t_ZGYSFS.DataTextField = "FName";
        t_ZGYSFS.DataValueField = "FNumber";
        t_ZGYSFS.DataBind();

        //招标组织形式
        dt = rc.getDicTbByFNumber("112210");
        t_ZBZZXS.DataSource = dt;
        t_ZBZZXS.DataTextField = "FName";
        t_ZBZZXS.DataValueField = "FNumber";
        t_ZBZZXS.DataBind();

    }

    private void ShowTitle()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_ZBWJ_Record qa = dbContext.TC_ZBWJ_Record.Where(t => t.FId == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
        pageTool tool = new pageTool(this.Page);
        tool.fillPageControl(qa);
        txtFId.Value = qa.FId;
        Session["FAppId"] = qa.FAppId;
        ViewState["FAppId"] = qa.FAppId;
        ViewState["FPrjId"] = qa.FPrjId;
        ClientScript.RegisterStartupScript(this.GetType(), "hideTr", "<script>hideTr();</script>");
        ShowFile(txtFId.Value);
    }

    private void ShowFile(string FId)
    {
        EgovaDB dbContext = new EgovaDB();
        var App = dbContext.TC_SGXKZ_File.Where(t => t.FLinkId == FId).Select(t => new
        {
            t.FileName,
            t.FId,
            t.ReportTime
        }).ToList();
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string fId = txtFId.Value;
        TC_ZBWJ_Record qa = new TC_ZBWJ_Record();
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(fId))
        {
            qa = dbContext.TC_ZBWJ_Record.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {

        }
        qa = tool.getPageValue(qa);
        qa.DLJGId = h_selEntId.Value;
        dbContext.SubmitChanges();
        txtFId.Value = fId;
        //  showPrjData();
        ScriptManager.RegisterStartupScript(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowFile(txtFId.Value);
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('File.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, dbContext.TC_SGXKZ_File, tool_Deleting);
        ShowFile(txtFId.Value);
    }
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        dbContext = new EgovaDB();
        if (dbContext != null)
        {
            var para = dbContext.TC_SGXKZ_File.Where(t => FIdList.ToArray().Contains(t.FId));
            dbContext.TC_SGXKZ_File.DeleteAllOnSubmit(para);
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowFile(txtFId.Value);
    }
    protected void t_ZBZZXS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (t_ZBZZXS.SelectedValue == "11221001")
        {
            t_DLJG.Text = dbContext.CF_Ent_BaseInfo.Where(d => d.FId == CurrentEntUser.EntId).Select(d => d.FName).FirstOrDefault();
            ClientScript.RegisterStartupScript(this.GetType(), "hideTr", "<script>hideTr();</script>");
        }
        else
        {
            t_DLJG.Text = "";
            ClientScript.RegisterStartupScript(this.GetType(), "showTr", "<script>showTr();</script>");
        }
    }
    protected void btnAddEnt_Click(object sender, EventArgs e)
    {
        selEnt();
    }
    private void selEnt()
    {
        string selEntId = h_selEntId.Value;
        EgovaDB1 db = new EgovaDB1();
        var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        t_DLJG.Text = v.QYMC;
    }
}