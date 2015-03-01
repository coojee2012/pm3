using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Text;
using Approve.RuleCenter;
using System.Data;
using cn.gov.scjst.zw;
using System.Collections;
using Approve.EntityBase;
public partial class JSDW_appmain_EntListSel : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    ShareTool st = new ShareTool();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["JST"] = rc.GetSysObjectContent("_Sys_Ent_Source");
            BindControl();
            showInfo(false);
        }
    }
    void BindControl()
    {
        int iMType = EConvert.ToInt(Request.QueryString["fsysId"]);
        if (iMType > 0)
        {
            lTitle.Text = db.CF_Sys_SystemName.Where(t => t.FNumber == iMType).Select(t => t.FName).FirstOrDefault();
        }
    }
    string GetSys()
    {
        string sys = Request.QueryString["fsysId"];
        switch (sys)
        {
            case "1554"://勘察
                sys = "1550";
                break;
            case "1553"://设计
                sys = "155";
                break;
            case "1261"://见证
                sys = "126";
                break;
            case "1451"://审图
                sys = "145";
                break;
        }
        return sys;
    }
    void showInfo(bool flag)
    {
        if (EConvert.ToInt(ViewState["JST"]) == 1)//建设厅接口数据
        {
            //查询四川接口数据里面的企业信息 
            //勘察-1550
            //设计-155
            //施工图-145
            DataTable dt = null;
            if (ViewState["DT"] != null && flag)
                dt = ViewState["DT"] as DataTable;
            else
            {
                string rn = string.Empty;
                string sCon = "fsystemId=" + GetSys();
                if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
                    sCon += " and FName like '%" + t_FName.Text.Trim() + "%' ";
                dt = st.GetTABLE(sCon, "企业基本信息（网站）", out rn);
                ViewState["DT"] = dt;
                Pager1.CurrentPageIndex = 1;
            }
            if (dt != null)
            {
                int iCount = dt.Rows.Count;
                DataTable dtNew = new DataTable();
                dtNew.Columns.AddRange(new DataColumn[]{
                new DataColumn("FName"),
                new DataColumn("FJuridcialCode"),
                new DataColumn("FLinkMan"),
                new DataColumn("FId") 
            });
                int iStart = Pager1.PageSize * (Pager1.CurrentPageIndex - 1);
                int iEnd = Pager1.PageSize * Pager1.CurrentPageIndex;
                if (iEnd > iCount)
                    iEnd = iCount;
                for (int i = iStart; i < iEnd; i++)
                {
                    DataRow dr = dt.Rows[i];
                    DataRow drNew = dtNew.NewRow();
                    for (int ii = 0; ii < dtNew.Columns.Count; ii++)
                    {
                        drNew[ii] = dr[dtNew.Columns[ii].ColumnName];
                    }
                    dtNew.Rows.Add(drNew);
                }
                Pager1.RecordCount = iCount;
                dg_List.DataSource = dtNew;
                dg_List.DataBind();
            }
        }
        else
            showInfo123();
    }
    //显示 
    void showInfo123()
    {
        var App = db.CF_Ent_BaseInfo.Where(t => t.FJuridcialCode != "" && t.FJuridcialCode != null).OrderByDescending(t => t.FCreateTime).Select(t => new { t.FName, t.FJuridcialCode, t.FLinkMan, FSystemId = db.CF_Sys_UserRight.Where(r => r.FBaseinfoID == t.FId).Select(r => r.FSystemId), t.FId, FSTAddress = db.CF_Sys_User.Where(u => u.FBaseInfoId == t.FId).Select(u => u.FSTAddress).FirstOrDefault() });
        int iMType = EConvert.ToInt(Request.QueryString["fsysId"]);
        if (iMType > 0)
            App = App.Where(t => t.FSystemId.Contains(iMType));
        if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
            App = App.Where(t => t.FName.Contains(t_FName.Text.Trim()));

        if (Request.QueryString["fadd"] != null
            && Request.QueryString["fadd"] != "")
        {
            string add = Request.QueryString["fadd"].Substring(0, 4);
            App = App.Where(t => t.FSTAddress.Contains(add));
        }

        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 2].Controls[0] as LinkButton;
            lb.Text = "选择";
            lb.Attributes.Add("onclick", "return confirm('确认要选择该企业吗?');");
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo(true);
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo(false);
    }
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            if (e.CommandName == "Sel")
            {
                string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
                pageTool tool = new pageTool(this.Page);
                //调用下载数据到本地的方法
                //然后返回fid
                if (EConvert.ToInt(ViewState["JST"]) == 1)//建设厅接口数据
                {
                    if (DoDownLoad(fid))
                        tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
                    else
                        tool.showMessage("没有找到数据！");
                }
                else
                    tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
            }
        }
    }
    //下载数据到本地数据库
    bool DoDownLoad(string fid)
    {
        DataTable dt = ViewState["DT"] as DataTable;
        if (dt == null)
        {
            JSTJKWebService ws = new JSTJKWebService();
            string rn = string.Empty;
            string sCon = "fid='" + fid + "'";
            dt = st.GetTABLE(sCon, "企业基本信息（网站）", out rn);
        }
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            SortedList sl = new SortedList();
            Approve.EntityBase.SaveOptionEnum so = Approve.EntityBase.SaveOptionEnum.Update;
            sl.Add("FID", fid);
            if (db.CF_Ent_BaseInfo.Count(t => t.FId == fid) <= 0)
            {
                sl.Add("FCreateTime", DateTime.Now);
                so = Approve.EntityBase.SaveOptionEnum.Insert;
            }
            sl.Add("FJuridcialCode", dr["FJuridcialCode"]);
            sl.Add("FName", dr["FName"]);
            sl.Add("FLinkMan", dr["FLinkMan"]);
            sl.Add("FSystemId", Request.QueryString["fsysId"].Substring(0, 3));
            sl.Add("FState", 2);
            sl.Add("FIsDeleted", 0);
            rc.SaveEBase(EntityTypeEnum.EbBaseInfo, sl, "FID", so);

            //证书信息
            //查询证书信息
            JSTJKWebService ws = new JSTJKWebService();
            string rn = string.Empty;
            string sCon = "FBaseInfoId='" + fid + "' and FIsValid=1";
            dt = st.GetTABLE(sCon, "企业证书信息（网站）", out rn);
            if (dt != null && dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
                fid = dr["FId"].ToString();
                sl = new SortedList();
                so = Approve.EntityBase.SaveOptionEnum.Update;
                sl.Add("FID", fid);
                if (db.CF_Ent_QualiCerti
                    .Count(t => t.FId == fid) <= 0)
                {
                    sl.Add("FCreateTime", DateTime.Now);
                    so = Approve.EntityBase.SaveOptionEnum.Insert;
                }
                sl.Add("FEntName", dr["FEntName"]);
                sl.Add("FBaseInfoId", dr["FBaseInfoId"]);
                sl.Add("FCertiNo", dr["FCertiNo"]);
                sl.Add("FLevelName", dr["FLevelName"]);
                sl.Add("FSystemId", Request.QueryString["fsysId"].Substring(0, 3));
                sl.Add("FIsValid", 1);
                sl.Add("FIsDeleted", 0);
                rc.SaveEBase(EntityTypeEnum.EbQualiCerti, sl, "FID", so);
            }
            return true;
        }
        return false;
    }
}
