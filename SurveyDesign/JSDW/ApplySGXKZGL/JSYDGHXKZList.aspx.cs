using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using EgovaDAO;
using Tools;
using System.Data;
using ProjectData;

public partial class JSDW_ApplySGXKZGL_JSYDGHXKZList : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(EConvert.ToString(Session["FAppId"])))
            {
                ViewState["FAppId"] = EConvert.ToString(Session["FAppId"]);
            }
            ClientScript.RegisterStartupScript(this.GetType(), "hideTr1", "<script>hideTr1();</script>");
            ShowTitle();
            BindControl();
            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool1.ExecuteScript("btnEnable();");
            }
        }
    }
    void BindControl()
    {
        //用地性质
        DataTable dt = rc.getDicTbByFNumber("500");
        t_YDXZ.DataSource = dt;
        t_YDXZ.DataTextField = "FName";
        t_YDXZ.DataValueField = "FNumber";
        t_YDXZ.DataBind();
    }
    private void ShowTitle()
    {
        EgovaDB dbContext = new EgovaDB();       
         TC_SGXKZ_PrjInfo qa = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == EConvert.ToString(ViewState["FAppId"])).FirstOrDefault();
         ViewState["FPrjItemId"] = qa.FPrjItemId;
         string prjid = qa.PrjId;
        TC_SGXKZ_JSYDGHXKZ sp = dbContext.TC_SGXKZ_JSYDGHXKZ.Where(t => t.FAppId == EConvert.ToString(ViewState["FAppId"])).FirstOrDefault();
        if (sp != null)
        {
            txtFId.Value = sp.FId;
            ShowFile(sp.FId);
            if (!string.IsNullOrEmpty(sp.BL))
            {
                if (sp.BL != "1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr1();</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "hideTr1", "<script>hideTr1();</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "hideTr1", "<script>hideTr1();</script>");
            }


            t_YDXZ.SelectedValue = sp.YDXZ;
            //MODIFY:YTB修改bug 刚开始打开表单，办理选项下拉值为“补填”，但填写完数据，点击保存按钮后，办理选项字段的下拉值就自动变为了“已办”；没有哪个需求要求保存后修改办理选项内容。
            //t_BL.SelectedItem.Text = "已办";
            //t_BL.SelectedItem.Value = "3";
            //t_BL.Enabled = false;
            //从标准库中读取项目信息
            RCenter prjdb = new RCenter("XM_BaseInfo");
            string sql = @"select   JSDW,XMMC,XMDZ,JSGM   from  XM_BaseInfo.dbo.XM_XMJBXX   where xmbh = '" + prjid + "'";
            DataTable dt = prjdb.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                t_JSDW.Text = dt.Rows[0][0].ToString();
                t_ProjectName.Text = dt.Rows[0][1].ToString();
                t_Address.Text = dt.Rows[0][2].ToString();
                t_ConstrScale.Text = dt.Rows[0][3].ToString();
            }
        }
        else  //如果是未办理的信息则从业务库中读取信息
        {           
            t_JSDW.Text = qa.JSDW;
            t_ProjectName.Text = qa.ProjectName;
            t_Address.Text = qa.Address;
            t_ConstrScale.Text = EConvert.ToString(qa.ConstrScale);
        }

        pageTool tool = new pageTool(this.Page, "t_");
        tool.fillPageControl(sp);
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
        TC_SGXKZ_JSYDGHXKZ qa = new TC_SGXKZ_JSYDGHXKZ();
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(fId))
        {
            qa = dbContext.TC_SGXKZ_JSYDGHXKZ.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            qa.FId = fId;
            qa.FprjItemId = EConvert.ToString(ViewState["FPrjItemId"]);
            qa.FAppId = EConvert.ToString(ViewState["FAppId"]);
            dbContext.TC_SGXKZ_JSYDGHXKZ.InsertOnSubmit(qa);
        }
        qa = tool.getPageValue(qa);
        dbContext.SubmitChanges();
        txtFId.Value = fId;
        ShowTitle();
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
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
}