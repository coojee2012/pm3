using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Data;
using EgovaDAO;
using Tools;
using System.Text;
using System.Web.Services;
using System.Configuration;

public partial class JSDW_project_ProjectInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                txtFId.Value = Request.QueryString["fid"];
                //判断项目是否已经同步到标准库，如果已经同步则不能再保存，并且也不能同步
                if (IsExistProject(ViewState["FID"].ToString()))
                {
                    this.btnSave.Enabled = false;
                    this.btnRefresh.Enabled = false;
                }
                showInfo();
                ShowPrjItemInfo();
            }
            else
            {
                //显示建设单位信息
                EgovaDB dbContext = new EgovaDB();
                var jsdw = dbContext.CF_Ent_BaseInfo
                    .Where(t => t.FId == CurrentEntUser.EntId)
                    .Select(t => new
                    {
                        t.FId,
                        t.FName,
                        t.FRegistDeptId,
                        t.FRegistAddress,
                        t.FJuridcialCode,
                        t.FLinkMan,
                        t.FTel,
                        t.FOTxt5
                    }).FirstOrDefault();
                if (jsdw != null)
                {
                    dbContext = new EgovaDB();
                    string fullName = dbContext.ManageDept.Where(t => t.FNumber == jsdw.FRegistDeptId).Select(t => t.FFullName).FirstOrDefault();
                    t_JSDW.Text = jsdw.FName;
                    t_JSDWDZ.Text = jsdw.FRegistAddress;
                    t_JSDWDM.Text = jsdw.FJuridcialCode;
                    t_Contacts.Text = jsdw.FLinkMan;
                    t_JSDWFR.Text = jsdw.FOTxt5;
                    t_Mobile.Text = jsdw.FTel;
                    ViewState["FJSDWID"] = jsdw.FId;
                    if (string.IsNullOrEmpty(t_ProjectNo.Text))
                    {
                        txtProjectNo.Value = GetPrjNo();
                    }
                }
            }
        }
    }
    void BindControl()
    {
        string deptId = ComFunction.GetDefaultDept();
        govd_FRegistDeptId.fNumber = deptId;
        govd_FRegistDeptId.Dis(1);
        //项目类别
        DataTable dt = rc.getDicTbByFNumber("20001");
        t_ProjectType.DataSource = dt;
        t_ProjectType.DataTextField = "FName";
        t_ProjectType.DataValueField = "FNumber";
        t_ProjectType.DataBind();

        //建设性质
        dt = rc.getDicTbByFNumber("20005");
        t_ConstrType.DataSource = dt;
        t_ConstrType.DataTextField = "FName";
        t_ConstrType.DataValueField = "FNumber";
        t_ConstrType.DataBind();

        //立项级别
        dt = rc.getDicTbByFNumber("112204");
        t_ProjectLevel.DataSource = dt;
        t_ProjectLevel.DataTextField = "FName";
        t_ProjectLevel.DataValueField = "FNumber";
        t_ProjectLevel.DataBind();

        //用地性质
        dt = rc.getDicTbByFNumber("500");
        t_LandUse.DataSource = dt;
        t_LandUse.DataTextField = "FName";
        t_LandUse.DataValueField = "FNumber";
        t_LandUse.DataBind();

        //访问大屏
        string url = ConfigurationManager.ConnectionStrings["eGova"].ConnectionString;
        txtUrl.Value = url;
    }
    string GetPrjNo()
    {
        RCenter rc = new RCenter();
        string fNo = string.Empty;
        if (string.IsNullOrEmpty(fNo))
        {
            //先查询地区
            string fDeptId = govd_FRegistDeptId.fNumber;
            fDeptId = fDeptId.PadRight(6, '0');
            string fDate = DateTime.Now.ToString("yyMMdd");
            fNo = fDeptId + fDate;
            //获取项目分类代码
            if (t_ProjectType.SelectedValue == "2000101")
            {
                fNo += "01";
            }
            else if (t_ProjectType.SelectedValue == "2000102")
            {
                fNo += "02";
            }
            else
            {
                fNo += "99";
            }
            //查询最大的号码
            StringBuilder sb = new StringBuilder();
            sb.Append("select ISNULL(max(right(ProjectNo,2)),0) from TC_Prj_Info ");
            sb.Append("where substring(ProjectNo,7,6) = '" + fDate + "'");
            int iNo = EConvert.ToInt(rc.GetSignValue(sb.ToString())) + 1;
            fNo += iNo.ToString().PadLeft(2, '0');
        }
        return fNo;
    }
    //显示
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        TC_Prj_Info prj = dbContext.TC_Prj_Info.Where(t => t.FId == Convert.ToString(ViewState["FID"])).FirstOrDefault();
        if (prj != null)
        {
            tool.fillPageControl(prj);
            govd_FRegistDeptId.fNumber = t_AddressDept.Value;
            t_ProjectType_SelectedIndexChanged();
        }
    }

    /// <summary>
    /// 检测项目是否已经存在在标准库中
    /// </summary>
    /// <param name="fid"></param>
    /// <returns></returns>
    private bool IsExistProject(string fid)
    {
        string sql = @"select  *  from  XM_BaseInfo.[dbo].[XM_XMJBXX]  where [XMBH]='" + fid + "'";
        DataTable dt = new DataTable();
        dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //保存
    private void saveInfo()
    {
        string projname = this.t_ProjectName.Text.Trim();
        if (CheckPrjIsExist(projname))
        {           
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "js", "alert('系统中已经有同名项目，请从系统中选取对应项目！');window.returnValue='1';", true);
            return;
        }
        EgovaDB dbContext = new EgovaDB();
        
        t_AddressDept.Value = govd_FRegistDeptId.fNumber;
        //t_Province.Value = govd_FRegistDeptId.fNumber.Substring(0, 2);
        //t_City.Value = govd_FRegistDeptId.fNumber.Substring(2, 2);
        //t_County.Value = govd_FRegistDeptId.fNumber.Substring(4, 2);
        govd_FRegistDeptId.fNumber = t_AddressDept.Value;
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        string fId = EConvert.ToString(ViewState["FID"]);
        TC_Prj_Info Emp = new TC_Prj_Info();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_Prj_Info.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.JSDW = t_JSDW.Text;
            Emp.JSDWDM = t_JSDWDM.Text;
            Emp.JSDWDZ = t_JSDWDZ.Text;
            Emp.FJSDWID = Convert.ToString(ViewState["FJSDWID"]);
            dbContext.TC_Prj_Info.InsertOnSubmit(Emp);
        }
        Emp = tool.getPageValue(Emp);   
        dbContext.SubmitChanges();
        ViewState["FID"] = fId;
        txtFId.Value = fId;

        //showInfo();
        //tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
       // ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
    //    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "js", "alert('保存成功');window.returnValue='1';", true);
        //ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "$('#btnSave').css('color','#BEBFC3');$('#btnSave').attr('disabled',true);;alert('保存成功');window.returnValue='1';", true);

        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "js", "alert('保存成功');window.returnValue='1';", true);

        //tool.showMessage("alert('保存成功');window.returnValue='1';");
    }
    
    /// <summary>
    /// 显示单体工程信息
    /// </summary>
    void ShowPrjItemInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        dg_List.Columns[3].HeaderText = "工程类别";
        var App = dbContext.TC_PrjItem_Info.Where(t => t.FPrjId == EConvert.ToString(ViewState["FID"])).Select(t => new
        {
            t.PrjItemName,
            FDJ = dbContext.CF_Sys_Dic.Where(d => d.FNumber == Convert.ToInt32(t.PrjItemType)).Select(d => d.FName).FirstOrDefault(),
            FId = t.FId,
            t.FPrjId
        }).ToList();
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
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
        tool.DelInfoFromGrid(dg_List, dbContext.TC_Prj_Info, tool_Deleting);
        ShowPrjItemInfo();
    }
    //单项工程删除
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        EgovaDB dbContext = new EgovaDB();
        //单体工程
        var para = dbContext.TC_PrjItem_Info.Where(t => FIdList.ToArray().Contains(t.FId));
        dbContext.TC_PrjItem_Info.DeleteAllOnSubmit(para);
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowPrjItemInfo();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            //工程编号主键
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            //项目编号id
            string fPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('ProjectItemInfo.aspx?fid=" + fid + "&fprjId=" + fPrjId + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowPrjItemInfo();
    }
    protected void t_ProjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //工程用途
        if (t_ProjectType.SelectedValue == "2000101")
        {
            DataTable dt = rc.getDicTbByFNumber("2000101");
            t_ProjectUse.DataSource = dt;
            t_ProjectUse.DataTextField = "FName";
            t_ProjectUse.DataValueField = "FNumber";
            t_ProjectUse.DataBind();
        } else if (t_ProjectType.SelectedValue == "2000102") {
            DataTable dt = rc.getDicTbByFNumber("2000102");
            t_ProjectUse.DataSource = dt;
            t_ProjectUse.DataTextField = "FName";
            t_ProjectUse.DataValueField = "FNumber";
            t_ProjectUse.DataBind();
        }
        else if (t_ProjectType.SelectedValue == "2000103")
        {
            t_ProjectUse.Items.Clear();
        }
    }

    protected void t_ProjectType_SelectedIndexChanged()
    {
        //工程用途
        if (t_ProjectType.SelectedValue == "2000101")
        {
            DataTable dt = rc.getDicTbByFNumber("2000101");
            t_ProjectUse.DataSource = dt;
            t_ProjectUse.DataTextField = "FName";
            t_ProjectUse.DataValueField = "FNumber";
            t_ProjectUse.DataBind();
        }
        else if (t_ProjectType.SelectedValue == "2000102")
        {
            DataTable dt = rc.getDicTbByFNumber("2000102");
            t_ProjectUse.DataSource = dt;
            t_ProjectUse.DataTextField = "FName";
            t_ProjectUse.DataValueField = "FNumber";
            t_ProjectUse.DataBind();
        }
        else if (t_ProjectType.SelectedValue == "2000103")
        {
            t_ProjectUse.Items.Clear();
        }
    }

    /// <summary>
    /// 确认同步后项目信息不能再进行修改调整
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        object obj = ViewState["FID"];
        if (obj!=null)
        {
            //System.IO.File.AppendAllText("C:\\yujiajun.log", ViewState["FID"].ToString(), Encoding.Default);
            string sql = @"exec SP_GCPRJ_TO_BZK @FID";
            rc.PExcute(sql, new System.Data.SqlClient.SqlParameter() { ParameterName = "@FID", Value = obj.ToString(), SqlDbType = SqlDbType.VarChar });
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作成功')</script>");
            this.btnSave.Enabled = false;
            this.btnRefresh.Enabled = false;
        }
        else
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请先保存')</script>");
    }

    /// <summary>
    /// 判断项目在标准库中是否已经存在，如果已经存在则不允许添加项目，让操作者到标准库中选择
    /// </summary>
    /// <param name="projname">项目名称</param>
    /// <returns></returns>
    private bool CheckPrjIsExist(string projname)
    {
        string sql = @"select  *  from  XM_BaseInfo.[dbo].[XM_XMJBXX]  where ltrim(rtrim(XMMC))='" + projname.Trim() + "'";
        DataTable dt = new DataTable();
        dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }       
    }
}