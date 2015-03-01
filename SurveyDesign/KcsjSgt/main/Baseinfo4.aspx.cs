using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using ProjectData;
using System.Text;
using System.Data;
using Approve.EntityBase;

public partial class KcsjSgt_main_Baseinfo4 : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["fbid"]))
        {
            FBaseInfoId = CurrentEntUser.EntId;
            FUserId = CurrentEntUser.UserId;
        }
        else
        {
            FBaseInfoId = Request.QueryString["fbid"];
            FUserId = Request.QueryString["frid"];
            this.ClientScript.RegisterStartupScript(this.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>try{btnEnable();}catch(e){}</script>");
            IsView = "1";
        }
        if (!IsPostBack)
        {


            ShowCertiInfo();
        }
    }
    private string IsView { get; set; }
    private string FBaseInfoId { get; set; }
    private string FUserId { get; set; }
    //显示

    void ShowCertiInfo()
    {
        var App = db.CF_Ent_QualiCerti.Where(t => t.FBaseInfoId == FBaseInfoId).OrderByDescending(t => t.FCreateTime)
             .Select(t => new
             {
                 t.FId,
                 t.FCertiNo,
                 t.FLevelName,
                 FCertiType = db.CF_Sys_Dic.Where(l => l.FNumber == t.FCertiType).Select(l => l.FName).FirstOrDefault(),
                 t.FEndTime,
                 t.FAppDeptId,
                 t.FAppTime
             }); ;

        dgCerti_List.DataSource = App;
        dgCerti_List.DataBind();
    }

    protected void dgCerti_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1).ToString();
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('AddCertiInfo.aspx?fid=" + fid + "&IsView=" + IsView + "&FBaseInfoId=" + FBaseInfoId + "',800,520,$('#btnReload1'));\">" + e.Item.Cells[2].Text + "</a>";
            string FAppDeptId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppDeptId"));

            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(FAppDeptId))
            {
                //核准单位 
                sb.Append(" fisdeleted=0 and ");
                sb.Append("fnumber =" + FAppDeptId + " ");
                DataTable dt = rc.GetTable(EntityTypeEnum.EsManageDept, "FFullName", sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    e.Item.Cells[3].Text = EConvert.ToString(dt.Rows[0][0]);
                }
            }
            
        }
    }

}
