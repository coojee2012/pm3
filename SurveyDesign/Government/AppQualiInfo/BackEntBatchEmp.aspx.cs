using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.EntitySys;
using Approve.EntityCenter;
using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.RuleApp;
using Approve.Common;
using System.Linq;
using ProjectData;
using ProjectBLL;
public partial class Government_AppQualiInfo_BackEntBatchEmp : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }
    private void ShowEntInfo()
    {
        string fpids = this.HFID1.Value;
        if (fpids == "")
        {
            return;
        }
        string[] strs = fpids.Trim().Split(',');

        if (strs.Length <= 0)
        {
            return;
        }
        int iCount = strs.Length;
        StringBuilder sb = new StringBuilder();
        sb.Append("select e.FId,e.FName,b.FName FEntName,e.FIdCard,e.FUserName ");
        sb.Append("from cf_Emp_BaseInfo e inner join ");
        sb.Append("cf_Ent_BaseInfo b on e.FBaseInfoId=b.FId ");
        sb.Append("where e.FType=3 and e.FState=1 ");
        sb.Append("and e.FId in ( ");
        for (int i = 0; i < iCount; i++)
        {
            if (i > 0)
                sb.Append(",");
            sb.Append("'" + strs[i].Trim() + "'");
        }
        sb.Append(") ");
        sb.Append("order by e.FState,e.FTime desc");
        DataTable dt = rc.GetTable(sb.ToString());
        this.JustAppInfo_List.DataSource = dt;
        this.JustAppInfo_List.DataBind();
    }
    private void BatchApp()
    {
        StringBuilder sbDescription = new StringBuilder();
        StringBuilder sb = new StringBuilder();
        int iCount = JustAppInfo_List.Items.Count;
        if (iCount == 0)
        {
            pageTool tool = new pageTool(this.Page);
            tool.showMessage("没有数据，无法审核！");
            return;
        }
        SortedList[] sl = new SortedList[iCount];
        EntityTypeEnum[] en = new EntityTypeEnum[iCount];
        SaveOptionEnum[] so = new SaveOptionEnum[iCount];
        string[] key = new string[iCount];
        int colCount = JustAppInfo_List.Columns.Count;
        for (int i = 0; i < iCount; i++)
        {
            string fId = JustAppInfo_List.Items[i].Cells[colCount - 1].Text;
            sl[i] = new SortedList();
            sl[i].Add("FID", fId);
            sl[i].Add("FResume", txtFBackIdea.Text.Trim());
            sl[i].Add("FState", 2);//审核完毕

            en[i] = EntityTypeEnum.EeBaseinfo;
            so[i] = SaveOptionEnum.Update;
            key[i] = "FID";
        }
        rc.SaveEBaseM(en, sl, key, so);
    }
    protected void btnBackEnt_Click(object sender, EventArgs e)
    {
        BatchApp();
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>alert('操作成功');window.returnValue=1;window.close();</script>");
    }
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
        }
    }
    protected void btnShowInfo_Click(object sender, EventArgs e)
    {
        ShowEntInfo();
    }
}
