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

public partial class JSDW_ApplySGXKZGL_ZBJGList : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    DataTable dt = null;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            BindControl();

            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["fid"] = Request.QueryString["fid"];
                ViewState["IsBz"] = Request.QueryString["IsBz"];
                ShowInfo(ViewState["IsBz"].ToString(), ViewState["fid"].ToString());
            }
            else
            {
                ViewState["IsBz"] = "1";
                ViewState["FAppId"] = EConvert.ToString(Request["FAppId"]);
                ShowTitle();
            }
            //ClientScript.RegisterStartupScript(this.GetType(), "hideTr1", "<script>hideTr1();</script>");
            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0 || ViewState["IsBz"].ToString() =="3")
            {
                tool1.ExecuteScript("btnEnable();");
                //屏蔽新增按钮
                btnAddEnt.Visible = false;
                Button1.Visible = false;
                Button2.Visible = false;
                Button4.Visible = false;
            }

            ShowFile(t_JLId.Value, "JL");
        }
    }

    void BindControl()
    {
        //招标类型
        DataTable dt = rc.getDicTbByFNumber("112208");
        t_JLZBLX.DataSource = dt;
        t_JLZBLX.DataTextField = "FName";
        t_JLZBLX.DataValueField = "FNumber";
        t_JLZBLX.DataBind();
        //t_SGZBLX.DataSource = dt;
        //t_SGZBLX.DataTextField = "FName";
        //t_SGZBLX.DataValueField = "FNumber";
        //t_SGZBLX.DataBind();

        //招标方式
        dt = rc.getDicTbByFNumber("112206");
        t_JLZBFS.DataSource = dt;
        t_JLZBFS.DataTextField = "FName";
        t_JLZBFS.DataValueField = "FNumber";
        t_JLZBFS.DataBind();
        //t_SGZBFS.DataSource = dt;
        //t_SGZBFS.DataTextField = "FName";
        //t_SGZBFS.DataValueField = "FNumber";
        //t_SGZBFS.DataBind();

        //证件类型
        dt = rc.getDicTbByFNumber("112203");
        t_JLGCSZJLX.DataSource = dt;
        t_JLGCSZJLX.DataTextField = "FName";
        t_JLGCSZJLX.DataValueField = "FNumber";
        t_JLGCSZJLX.DataBind();
        //t_SGXMJLZJLX.DataSource = dt;
        //t_SGXMJLZJLX.DataTextField = "FName";
        //t_SGXMJLZJLX.DataValueField = "FNumber";
        //t_SGXMJLZJLX.DataBind();
    }




    private void ShowTitle()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_SGXKZ_PrjInfo qa = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == EConvert.ToString(ViewState["FAppId"])).FirstOrDefault();
        ViewState["FPrjItemId"] = qa.FPrjItemId;
        t_JSDW.Text = qa.JSDW;
        t_ProjectName.Text = qa.ProjectName;
        t_PrjItemName.Text = qa.PrjItemName;
        t_Area.Text = EConvert.ToString(qa.Area);
        t_ConstrScale.Text = qa.ConstrScale;
        TC_Prj_Info p = dbContext.TC_Prj_Info.Where(t => t.FId == qa.PrjId).FirstOrDefault();
        t_ProjectNo.Text = p.ProjectNo;
        //加载附件列表
        //ShowFile(t_JLId.Value, "JL");

        //pageTool tool = new pageTool(this.Page, "t_");
        //tool.fillPageControl(sp);
    }


    //根据传入参数，取到指定的数据，并返回值。
    private void ShowInfo(string isbz, string fid)
    {
        DataTable sp = null;
        XMHJCL_Business business = new XMHJCL_Business();
        t_JLId.Value = fid;
        if (isbz == "3")
        {
            sp = business.GetZBjg_bz_p(fid);  //标准库数据
            ViewState["FAppId"] = "1";
            ViewState["FPrjItemId"] = "1";
        }
        else
        {
            sp = business.GetZBjg_yw_p(fid);  //业务数据库
            if (sp.Rows.Count > 0)
            {
                ViewState["FAppId"] = sp.Rows[0]["FAppId"].ToString();
                ViewState["FPrjItemId"] = sp.Rows[0]["FprjItemId"].ToString();
            }
        }
        if (sp.Rows.Count > 0)
        {
            string strbl = sp.Rows[0]["bl"].ToString();
            txtFId.Value = fid;
            ShowFile(fid, "JL");
    
            FillPageWithDt tool = new FillPageWithDt();
            tool.fillPageControl(sp, this.Page, "t_");
            ClientScript.RegisterStartupScript(this.GetType(), "hideTr1", "<script>hideTr1();</script>");
        }
    }


    private void ShowFile(string FId, string name)
    {
        EgovaDB dbContext = new EgovaDB();
        var App = dbContext.TC_SGXKZ_File.Where(t => t.FLinkId == FId).Select(t => new
        {
            t.FileName,
            t.FId,
            t.ReportTime
        }).ToList();
        //if (name == "KC")
        //{
        //    PagerKC.RecordCount = App.Count();
        //    dg_ListKC.DataSource = App.Skip((PagerKC.CurrentPageIndex - 1) * PagerKC.PageSize).Take(PagerKC.PageSize);
        //    dg_ListKC.DataBind();
        //    PagerKC.Visible = (PagerKC.RecordCount > PagerKC.PageSize);
        //}
        //else if (name == "SJ")
        //{
        //    PagerSJ.RecordCount = App.Count();
        //    dg_ListSJ.DataSource = App.Skip((PagerSJ.CurrentPageIndex - 1) * PagerSJ.PageSize).Take(PagerSJ.PageSize);
        //    dg_ListSJ.DataBind();
        //    PagerSJ.Visible = (PagerSJ.RecordCount > PagerSJ.PageSize);
        //}
        if (name == "JL")
        {
            PagerJL.RecordCount = App.Count();
            dg_ListJL.DataSource = App.Skip((PagerJL.CurrentPageIndex - 1) * PagerJL.PageSize).Take(PagerJL.PageSize);
            dg_ListJL.DataBind();
            PagerJL.Visible = (PagerJL.RecordCount > PagerJL.PageSize);
        }
        //else if (name == "SG")
        //{
        //    PagerSG.RecordCount = App.Count();
        //    dg_ListSG.DataSource = App.Skip((PagerSG.CurrentPageIndex - 1) * PagerSG.PageSize).Take(PagerSG.PageSize);
        //    dg_ListSG.DataBind();
        //    PagerSG.Visible = (PagerSG.RecordCount > PagerSG.PageSize);
        //}
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        var isNew = true;
        //string fId = txtFId.Value;
        string fId = "";
        if (ViewState["fid"] != null)
        {
            fId = ViewState["fid"].ToString();
        }
        TC_SGXKZ_ZBJG qa = new TC_SGXKZ_ZBJG();
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(fId))
        {
            qa = dbContext.TC_SGXKZ_ZBJG.Where(t => t.FId == fId).FirstOrDefault();
            isNew = false;
        }
        else
        {

            fId = Guid.NewGuid().ToString();
            ViewState["fid"] = fId;
            txtFId.Value = fId;
            qa.FId = fId;
            qa.FprjItemId = EConvert.ToString(ViewState["FPrjItemId"]);
            qa.FAppId = EConvert.ToString(ViewState["FAppId"]);
            if (this.t_JLZBLX.SelectedValue == "11220801")
            {
                qa.SGId = this.t_JLId.Value;
            }
            else if (this.t_JLZBLX.SelectedValue == "11220802")
            {
                qa.JLId = this.t_JLId.Value;
            }
            else if (this.t_JLZBLX.SelectedValue == "11220803")
            {
                qa.SJId = this.t_JLId.Value;
            }          
            dbContext.TC_SGXKZ_ZBJG.InsertOnSubmit(qa);
            isNew = true;
        }
        qa = tool.getPageValue(qa);
        if (qa.JLId != null && qa.JLId.Length > 36)//这里不知道是什么原因 这个长度超出了。暂时这样处理。
        {
            qa.JLId = qa.JLId.Substring(0, 36);
        }
        if (qa.SGId != null && qa.SGId.Length > 36)//这里不知道是什么原因 这个长度超出了。暂时这样处理。
        {
            qa.SGId = qa.SGId.Substring(0, 36);
        }
        if (isNew)
        {
            var count1 = dbContext.TC_SGXKZ_ZBJG.Count(t => t.FAppId == EConvert.ToString(ViewState["FAppId"]) && t.JLId == qa.JLId);
            if (count1 > 0)
            {
                ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('请不要重复添加该中标单位');window.returnValue='1';", true);
                return;
            }
        }
        dbContext.SubmitChanges();
        
       
        ShowFile(t_JLId.Value, "JL");
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
    }
    //protected void btnReload_ClickKC(object sender, EventArgs e)
    //{
    //    ShowFile(txtKCId.Value,"KC");
    //}
    //protected void btnReload_ClickSJ(object sender, EventArgs e)
    //{
    //    ShowFile(txtSJId.Value, "SJ");
    //}
    protected void btnReload_ClickJL(object sender, EventArgs e)
    {
        ShowFile(t_JLId.Value, "JL");
    }
    //protected void btnReload_ClickSG(object sender, EventArgs e)
    //{
    //    ShowFile(txtSGId.Value, "SG");
    //}
    //protected void App_List_ItemDataBoundKC(object sender, DataGridItemEventArgs e)
    //{
    //    if (e.Item.ItemIndex > -1)
    //    {
    //        e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.PagerKC.PageSize * (this.PagerKC.CurrentPageIndex - 1)).ToString();
    //        string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
    //        e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('File.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
    //    }
    //}
    //protected void App_List_ItemDataBoundSJ(object sender, DataGridItemEventArgs e)
    //{
    //    if (e.Item.ItemIndex > -1)
    //    {
    //        e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.PagerSJ.PageSize * (this.PagerSJ.CurrentPageIndex - 1)).ToString();
    //        string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
    //        e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('File.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
    //    }
    //}
    protected void App_List_ItemDataBoundJL(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.PagerJL.PageSize * (this.PagerJL.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('File.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    //protected void App_List_ItemDataBoundSG(object sender, DataGridItemEventArgs e)
    //{
    //    if (e.Item.ItemIndex > -1)
    //    {
    //        e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.PagerSG.PageSize * (this.PagerSG.CurrentPageIndex - 1)).ToString();
    //        string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
    //        e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('File.aspx?fid=" + fid + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
    //    }
    //}
    //protected void btnDel_ClickKC(object sender, EventArgs e)
    //{
    //    pageTool tool = new pageTool(this.Page);
    //    tool.DelInfoFromGrid(dg_ListKC, dbContext.TC_SGXKZ_File, tool_Deleting);
    //    ShowFile(txtKCId.Value, "KC");
    //}
    //protected void btnDel_ClickSJ(object sender, EventArgs e)
    //{
    //    pageTool tool = new pageTool(this.Page);
    //    tool.DelInfoFromGrid(dg_ListSJ, dbContext.TC_SGXKZ_File, tool_Deleting);
    //    ShowFile(txtSJId.Value, "SJ");
    //}
    protected void btnDel_ClickJL(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_ListJL, dbContext.TC_SGXKZ_File, tool_Deleting);
        ShowFile(t_JLId.Value, "JL");
    }
    //protected void btnDel_ClickSG(object sender, EventArgs e)
    //{
    //    pageTool tool = new pageTool(this.Page);
    //    tool.DelInfoFromGrid(dg_ListSG, dbContext.TC_SGXKZ_File, tool_Deleting);
    //    ShowFile(txtSGId.Value, "SG");
    //}
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        dbContext = new EgovaDB();
        if (dbContext != null)
        {
            var para = dbContext.TC_SGXKZ_File.Where(t => FIdList.ToArray().Contains(t.FId));
            dbContext.TC_SGXKZ_File.DeleteAllOnSubmit(para);
        }
    }
    //protected void Pager_PageChangingKC(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    //{
    //    PagerKC.CurrentPageIndex = e.NewPageIndex;
    //    ShowFile(txtKCId.Value, "KC");
    //}
    //protected void Pager_PageChangingSJ(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    //{
    //    PagerSJ.CurrentPageIndex = e.NewPageIndex;
    //    ShowFile(txtSJId.Value, "SJ");
    //}
    protected void Pager_PageChangingJL(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        PagerJL.CurrentPageIndex = e.NewPageIndex;
        ShowFile(t_JLId.Value, "JL");
    }
    //protected void Pager_PageChangingSG(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    //{
    //    PagerSG.CurrentPageIndex = e.NewPageIndex;
    //    ShowFile(txtSGId.Value, "SG");
    //}
    protected void btnAddEntSC_Click(object sender, EventArgs e)
    {       

        //中标单位
        selEnt("SC");
    }
    protected void btnAddEntSG_Click(object sender, EventArgs e)
    {
        //代理单位
        selEnt("SG");
    }
    protected void btnAddEntSJ_Click(object sender, EventArgs e)
    {
        //中标单位人员
        selEnt("SJ");
    }
    private void selEnt(string type)
    {


        if (type == "SC")
        {

            string selEntId = t_JLId.Value;
            if (t_SGIdold.Value != t_JLId.Value)
            {
                t_JLGCS.Text = "";//清空对应人员信息
                t_JLGCSZJHM.Text = "";
            }

            t_SGIdold.Value = t_JLId.Value;
            EgovaDB1 db = new EgovaDB1();
            var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
            string strEntType = t_JLZBLX.SelectedValue;
            if (strEntType == "101")
            { 
            }
            if (v != null)
            {
                t_JLZBDW.Text = v.QYMC;
                t_JLZBDWZZJGDM.Text = v.JGDM;
                var v1 = db.QY_QYZZXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
                if (v1 != null)
                {
                    t_JLZBQYZZDJ.Text = v1.ZZLB + v1.ZZMC + v1.ZZDJ;
                    t_JLZBQYZZZSH.Text = v1.ZSBH;
                }
            }



        }
        else if (type == "SG")
        {
            //string selEntId = t_JLId.Value;

            string selEntId = t_SGId.Value;
            EgovaDB1 db = new EgovaDB1();
            var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
            if (v != null)
            {
                t_ZBDLDWMC.Text = v.QYMC;
                t_ZBDLDWZZJGDM.Text = v.JGDM;
            }
           
        }
        else if (type == "SJ")
        {
            string selEmpId = t_SJId.Value;
            EgovaDB1 db = new EgovaDB1();
            //var v = db.RY_RYJBXX.Where(t => t.RYBH == selEmpId).FirstOrDefault();
            var v = db.RY_RYZSXX.Where(t => t.RYZSXXID == selEmpId).FirstOrDefault();//从资质信息表中直接获取，selEmpId是资质信息中的编号
            if (v != null)
            {
                t_JLGCS.Text = v.XM;
                t_JLGCSZJHM.Text = v.SFZH;
            }

        }
        if (!string.IsNullOrEmpty(t_BL.SelectedValue))
        {
            if (t_BL.SelectedValue != "1")
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
    }
    protected void btn1_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.showMessage("alert('保存成功');");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ShowFile(t_JLId.Value, "JL");
    }
}