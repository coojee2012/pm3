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
public partial class Government_AppQualiInfo_BackUpBatchEmp : System.Web.UI.Page
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
        //先查询最大号
        sb.Remove(0, sb.Length);
        sb.Append("select max(FUserName) from cf_Emp_BaseInfo ");
        sb.Append("where len(FUserName)=9 and fUserName like '5%' ");
        sb.Append("and isnumeric(FUserName)=1 ");
        int iMax = EConvert.ToInt(rc.GetSignValue(sb.ToString()));
        if (iMax == 0)
            iMax = 500000000;//以5开头的9位数字
        ViewState["FMaxNo"] = iMax;
        sb.Remove(0, sb.Length);
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
            TextBox box = JustAppInfo_List.Items[i].Cells[colCount - 2].Controls[1] as TextBox;
            string fUserName = box.Text.Trim();
            string fPwd = fUserName.Substring(3, 6);
            fPwd = SecurityEncryption.DESEncrypt(fPwd);
            sl[i] = new SortedList();
            sl[i].Add("FID", fId);
            sl[i].Add("FUserName", fUserName);
            sl[i].Add("FPassWord", fPwd);
            sl[i].Add("FState", 6);//审核完毕

            en[i] = EntityTypeEnum.EeBaseinfo;
            so[i] = SaveOptionEnum.Update;
            key[i] = "FID";
        }
        rc.SaveEBaseM(en, sl, key, so);
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        BatchApp();
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>alert('操作成功');window.returnValue=1;window.close();</script>");
    }
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
            int iNo = EConvert.ToInt(ViewState["FMaxNo"]) + e.Item.ItemIndex + 1;
            TextBox box = e.Item.Cells[e.Item.Cells.Count - 2].Controls[1] as TextBox;
            box.Text = iNo.ToString();
        }
    }
    protected void btnShowInfo_Click(object sender, EventArgs e)
    {
        ShowEntInfo();
    }
}
