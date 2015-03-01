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
using Approve.EntityBase;
using System.Collections;
public partial class JSDW_appmain_EntListSelCerti : System.Web.UI.Page
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
                JSTJKWebService ws = new JSTJKWebService();
                string rn = string.Empty;
                string sCon = "fsystemId=" + GetSys();
                string fnotBid = Request.QueryString["notBid"];
                string fAppId = EConvert.ToString(Session["FAppId"]);
                if (!string.IsNullOrEmpty(fnotBid))//过滤不需要的企业 
                {
                    sCon += " and FBaseInfoId!='" + fnotBid + "' ";
                    var bidList = db.CF_Prj_Ent.Where(ee => ee.FAppId == fAppId
                        && (ee.FEntType == 15502 || ee.FEntType == 15503))
                        .Select(ee => ee.FBaseInfoId).ToList();
                    StringBuilder sbIds = new StringBuilder();
                    foreach (var item in bidList)
                    {
                        if (sbIds.Length > 0)
                            sbIds.Append(",");
                        sbIds.Append("'" + item + "'");
                    }
                    if (sbIds.Length > 0)
                        sCon += " and FBaseInfoId not in (" + sbIds + ") ";
                }
                else
                {
                    var bidList = db.CF_Prj_Ent.Where(ee => ee.FAppId == fAppId).Select(ee => ee.FBaseInfoId).ToList();
                    StringBuilder sbIds = new StringBuilder();
                    foreach (var item in bidList)
                    {
                        if (sbIds.Length > 0)
                            sbIds.Append(",");
                        sbIds.Append("'" + item + "'");
                    }
                    if (sbIds.Length > 0)
                        sCon += " and FBaseInfoId not in (" + sbIds + ") ";
                }

                if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
                    sCon += " and FEntName like '%" + t_FName.Text.Trim() + "%' ";
                dt = st.GetTABLE(sCon, "企业证书信息（网站）", out rn);
                ViewState["DT"] = dt;
                Pager1.CurrentPageIndex = 1;
            }
            if (dt != null)
            {
                int iCount = dt.Rows.Count;
                DataTable dtNew = new DataTable();
                dtNew.Columns.AddRange(new DataColumn[]{
                new DataColumn("FName"),
                new DataColumn("FCertiNo"),
                new DataColumn("FLevelName"),
                new DataColumn("FCertiId"),
                new DataColumn("FId") 
            });
                //int iStart = Pager1.PageSize * (Pager1.CurrentPageIndex - 1);
                //int iEnd = Pager1.PageSize * Pager1.CurrentPageIndex;
                //if (iEnd > iCount)
                //    iEnd = iCount;
                //for (int i = iStart; i < iEnd; i++)
                //{
                //    DataRow dr = dt.Rows[i];
                //    DataRow drNew = dtNew.NewRow();
                //    drNew["FName"] = dr["FEntName"];
                //    drNew["FCertiNo"] = dr["FCertiNo"];
                //    drNew["FLevelName"] = dr["FLevelName"];
                //    drNew["FCertiId"] = dr["FId"];
                //    drNew["FId"] = dr[""];
                //    dtNew.Rows.Add(drNew);
                //}
                //Pager1.RecordCount = iCount;
                //dg_List.DataSource = dtNew;
                if (dt.Rows.Count > 0)
                {
                    var result = dt.AsEnumerable().GroupBy(t => EConvert.ToString( t["FBaseInfoId"])).
                        Select(t => new { FId = t.Key, FName = t.Max(g =>EConvert.ToString(g["FEntName"])), count = t.Count() });
                    Pager1.RecordCount = result.Count();

                    dg_List.DataSource = result.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);

                    dg_List.DataBind();
                }
  
            }
        }
        else
            showInfo123();
    }
    //显示 
    void showInfo123()
    {
        var App = from b in db.CF_Ent_BaseInfo
                  join c in db.CF_Ent_QualiCerti
                  on b.FId equals c.FBaseInfoId
                  where b.FJuridcialCode != null && b.FJuridcialCode != ""
                  orderby b.FId
                  select new
                  {
                      b.FId,
                      fCertiId = c.FId,
                      b.FName,
                      c.FCertiNo,
                      c.FLevelName,
                      FSystemId = db.CF_Sys_UserRight.Where(r => r.FBaseinfoID == b.FId).Select(r => r.FSystemId)
                  };
        int iMType = EConvert.ToInt(Request.QueryString["fsysId"]);
        if (iMType > 0)
            App = App.Where(t => t.FSystemId.Contains(iMType));
        string fnotBid = Request.QueryString["notBid"];
        string fAppId = EConvert.ToString(Session["FAppId"]);
        if (!string.IsNullOrEmpty(fnotBid))//过滤不需要的企业
        {
            App = App.Where(t => t.FId != fnotBid);
            App = App.Where(t => !(db.CF_Prj_Ent
                .Where(ee => ee.FAppId == fAppId && (ee.FEntType == 15502
                    || ee.FEntType == 15503)).Select(ee => ee.FBaseInfoId)
                    .Contains(t.FId)));
        }
        else //排除已经被选择的企业
            App = App.Where(t => !(db.CF_Prj_Ent.Where(ee => ee.FAppId == fAppId).Select(ee => ee.FBaseInfoId).Contains(t.FId)));
        if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
            App = App.Where(t => t.FName.Contains(t_FName.Text.Trim()));
   

        //合并
        var result=App.GroupBy(t=>t.FId).Select(t=>new{FId=t.Key,FName=t.Max(g=>g.FName),count=t.Count()});
        Pager1.RecordCount = result.Count();
 
        dg_List.DataSource = result.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        
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
            dt = st.GetTABLE(sCon, "企业证书信息（网站）", out rn);
        }
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            SortedList sl = new SortedList();
            //企业信息
            Approve.EntityBase.SaveOptionEnum so = Approve.EntityBase.SaveOptionEnum.Update;
            sl.Add("FID", dr["FBaseInfoId"]);
            if (db.CF_Ent_BaseInfo
                .Count(t => t.FId == dr["FBaseInfoId"].ToString()) <= 0)
            {
                sl.Add("FCreateTime", DateTime.Now);
                so = Approve.EntityBase.SaveOptionEnum.Insert;
            }
            sl.Add("FName", dr["FEntName"]);
            sl.Add("FSystemId", Request.QueryString["fsysId"].Substring(0, 3));
            sl.Add("FState", 2);
            sl.Add("FIsDeleted", 0);
            rc.SaveEBase(EntityTypeEnum.EbBaseInfo, sl, "FID", so);
            //证书信息
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
            return rc.SaveEBase(EntityTypeEnum.EbQualiCerti, sl, "FID", so);
        }
        return false;
    }
    protected void dg_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater subList = e.Item.FindControl("subList") as Repeater;
            if (subList != null)
            {
                HiddenField hfFID = e.Item.FindControl("hfFID") as HiddenField;
                HiddenField hfCount = e.Item.FindControl("hfCount") as HiddenField;
                if (EConvert.ToInt(ViewState["JST"]) == 1)//建设厅接口数据
                {
                    DataTable dt = ViewState["DT"] as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        subList.DataSource = dt.AsEnumerable().Where(t => EConvert.ToString(t["FBaseInfoId"]) == hfFID.Value)
                            .Select(t => new
                                {
                                    index = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)),
                                    count = EConvert.ToInt(hfCount.Value),
                                    FId = t["FId"],
                                    fCertiId = t["FId"],
                                    FName = t["FEntName"],
                                    FCertiNo = t["FCertiNo"],
                                    FLevelName = t["FLevelName"]
                                });

                        subList.DataBind();
                    }
                }
                else
                {
                    var App = from b in db.CF_Ent_BaseInfo
                              join c in db.CF_Ent_QualiCerti
                              on b.FId equals c.FBaseInfoId
                              where b.FId == hfFID.Value
                              orderby b.FId
                              select new
                              {
                                  index = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)),
                                  count = EConvert.ToInt(hfCount.Value),
                                  b.FId,
                                  fCertiId = c.FId,
                                  b.FName,
                                  c.FCertiNo,
                                  c.FLevelName,

                              };
                    subList.DataSource = App;
                    subList.DataBind();
                }
            }
            //e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
        
        }
    }

    protected void subList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
         if (e.Item.ItemIndex > -1)
        {
            if (e.CommandName == "Sel")
            {
                HiddenField hfFBaseInfoId = e.Item.FindControl("hfFBaseInfoId") as HiddenField;
                HiddenField hfCertiId = e.Item.FindControl("hfCertiId") as HiddenField;
                string fid = hfFBaseInfoId.Value;
                string fCertiId = hfCertiId.Value;
                pageTool tool = new pageTool(this.Page);
                //调用下载数据到本地的方法
                //然后返回fid
                //然后返回fid
                if (EConvert.ToInt(ViewState["JST"]) == 1)//建设厅接口数据
                {
                    if (DoDownLoad(fCertiId))
                        tool.ExecuteScript("window.returnValue='" + fid + "|" + fCertiId + "';window.close();");
                    else
                        tool.showMessage("没有找到数据！");
                }
                else
                    tool.ExecuteScript("window.returnValue='" + fid + "|" + fCertiId + "';window.close();");
            }
        }
    }
    protected void subList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lb = e.Item.FindControl("btnSelect") as LinkButton;
        lb.Text = "选择";
        lb.Attributes.Add("onclick", "return confirm('确认要选择该企业吗?');");
    }
}