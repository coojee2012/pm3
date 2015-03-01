using Approve.RuleCenter;
using EgovaDAO;
using ProjectData;
using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tools;

public partial class WYDW_Common_ItemList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }

    void showInfo()
    {
        StringBuilder sb = new StringBuilder();
        object fType = Request.QueryString["FType"];
        if (fType != null)
        {
            //本企业正在进行的业务
            //用项目编号进行判断没有问题，如果项目名称一样，项目编号肯定一样。用项目编号进行判断没有问题。
            //除非在企业第一次提前项目后，标准库从其他渠道写入了相同名称的项目
            string strSqlAppXMBHList = "select j.XMBH from CF_App_List l left join YW_WY_XM_JBXX j on l.FID=j.FAppID Where l.FBaseinfoId='" + CurrentEntUser.EntId + "' and l.FState!=6";

        
            if (fType.ToString() == "14401")  
            {
                sb.Append("Select XMMC,XMDZ,XMBH from XM_BaseInfo.dbo.XM_XMJBXX where XMMC like '%" + t_FName.Text.Trim() + "%' And XMBH Not In(Select XMBH From WY_XM_JBXX Where FBaseInfoID = '" + CurrentEntUser.EntId + "') And XMBH Not In(" + strSqlAppXMBHList + ") order by ftime desc");
            }
            else
            {
                //本企业在管项目，并且未在业务办理中
                sb.Append("Select XMMC,XMDZ,XMBH from WY_XM_JBXX where XMMC like '%" + t_FName.Text.Trim() + "%' And FBaseInfoID = '" + CurrentEntUser.EntId + "' ");
                sb.Append(" And  XMBH Not In(" + strSqlAppXMBHList + ") order by XMBH desc");
            }


            DataTable dtResult = rc.GetTable(sb.ToString());
            DataTable dt = dtResult.Clone();
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                int count = dtResult.Rows.Count;
                int index = (Pager1.CurrentPageIndex - 1) * Pager1.PageSize;

                int max = (index + Pager1.PageSize) > count ? count : (index + Pager1.PageSize);
                for (int i = index; i < max; i++)
                {
                    dt.Rows.Add(dtResult.Rows[i].ItemArray);
                }
                Pager1.RecordCount = count;
                dg_List.DataSource = dt;
                dg_List.DataBind();
            }
            else
            {
                Pager1.RecordCount = 0;
                dg_List.DataSource = null;
                dg_List.DataBind();
            }
        }


    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 2].Controls[0] as LinkButton;
            lb.Text = "选择";
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            if (e.CommandName == "Sel")
            {
                string xmbh = e.Item.Cells[e.Item.Cells.Count - 1].Text;
                string xmmc = e.Item.Cells[2].Text;
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("if(window.opener){window.opener.returnValue=['" + xmbh + "','" + xmmc + "'];}else{window.returnValue=['" + xmbh + "','" + xmmc + "'];}window.close();");
            }
        }
    }
}