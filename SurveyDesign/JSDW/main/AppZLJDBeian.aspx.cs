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
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using Approve.RuleApp;
using ProjectData;
using System.Linq;
using ProjectBLL;
public partial class JSDW_main_AppDesign : System.Web.UI.Page
{
    RApp ra = new RApp();
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    public int fMType = 294;//初步设计文件审查申报
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //ShowBtnName();
            //conBind();
            //ShowInfo();
        }
    }

    //绑定选项
    private void conBind()
    {
        
    }


    private void ShowBtnName()
    {
        btnPup.Text = "新增" + rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber=" + fMType + "");
    }

    private void ShowInfo()
    {
        
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
      
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        
    }

    private void SaveInfo(string fmtnumber)
    {
           }

    protected void btn_Click(object sender, EventArgs e)
    {
           }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        this.appTab.Visible = true;
        this.applyInfo.Visible = false;
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(HPid.Value))
            txtFPrjName.Text = rc.GetSignValue("select FPrjName from cf_Prj_Baseinfo where fid='" + HPid.Value + "'");
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
