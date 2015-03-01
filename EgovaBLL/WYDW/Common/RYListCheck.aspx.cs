using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Data;
using System.Text;


public partial class WYDW_Common_RYListCheck : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            conbind();
            showInfo();
        }

    }

    protected void showInfo()
    {
        string strXMBH = ddlXMMC.SelectedValue;

        string FBaseinfoID = CurrentEntUser.EntId;
        string sql = "select t2.fCardID As T2CardID,WY_RY_JBXX.*,j.XMMC  from WY_RY_JBXX  left outer join(Select fcardid from YW_WY_RY_JBXX where FAppID = '"+Session["FAppId"].ToString()+"') As T2 on WY_RY_JBXX.fCardID = T2.fCardID inner join wy_xm_jbxx j on WY_RY_JBXX.xmbh=j.xmbh where j.FBaseinfoId='" + FBaseinfoID + "'and WY_RY_JBXX.fpersonname like '%"+t_FName.Text.Trim()+"%'";

        sql += strXMBH.Trim() == "-1" ? "" : " And j.XMBH = '" + strXMBH + "'";
        DataTable dtResult = rc.GetTable(sql);
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
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {

    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string cardid=e.Item.Cells[e.Item.Cells.Count - 1].Text.Trim();
            if (cardid != null && cardid != "" && cardid != "&nbsp;")
            {
                e.Item.Cells[0].Text = "";
            }
        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        //showInfo();
    }

    protected void btnDoImp_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        if (dg_List.Items.Count > 0)
        {
            sb.Append("insert into yw_wy_ry_jbxx([FID],[XMBH],[fEntName],[fzzjgdm],[fPersonName],[fCardID],[fSex],[fBirthday],[fMZ],[fAddress],[fPosition],[fbysj],[fbyzsh],[fMajor],[flxdz],[fSchool],[fyzbm],[fdzyx],[fbgdh],[fCardType],[fgrdh],[fphoto],[fState],[fTechnical],[fzcqdsj],[fzczsh],[fxl],[fxw],[fxwzsh],[fMemo] ,[fFifteenCardID] ,[fSourceID],[FAppID] ,[FSystemID],[FCreateUser],[FCreateTime],[FTime],[FIsDeleted] ,[fBaseInfoID])");
            sb.Append("select newid(),'"+Session["XMBH"].ToString()+"',[fEntName],[fzzjgdm],[fPersonName],[fCardID],[fSex],[fBirthday],[fMZ],[fAddress],[fPosition],[fbysj],[fbyzsh],[fMajor],[flxdz],[fSchool],[fyzbm],[fdzyx],[fbgdh],[fCardType],[fgrdh],[fphoto],[fState],[fTechnical],[fzcqdsj],[fzczsh],[fxl],[fxw],[fxwzsh],[fMemo] ,[fFifteenCardID],[fSourceID],'" + Session["FAppId"].ToString() + "',[FSystemID],[FCreateUser],[FCreateTime],[FTime],[FIsDeleted],[fBaseInfoID] from wy_ry_jbxx where FID in(");
            for (int i = 0; i < dg_List.Items.Count; i++)
            {
                CheckBox cbx = new CheckBox();
                cbx = (CheckBox)dg_List.Items[i].Cells[0].Controls[1];
                if (cbx.Checked)
                {
                    sb.Append("'" + dg_List.Items[i].Cells[7].Text + "',");
                }

            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");
            //t_FName.Text = sb.ToString();
            if (rc.PExcute(sb.ToString()))
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('导入成功');window.returnValue='ok';window.close();</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>导入失败</script>");
            }
        }

    }

    //绑定下拉框
    protected void conbind()
    {
        string strXMBH = Session["XMBH"].ToString();

        DataTable dtJBXX = (DataTable)Session["JBXX"];
        string sql = "select XMMC,XMBH from wy_xm_jbxx where fbaseinfoid='" + CurrentEntUser.EntId + "'";
        DataTable dt = rc.GetTable(sql);
        ddlXMMC.DataValueField = "XMBH";
        ddlXMMC.DataTextField = "XMMC";
        ddlXMMC.DataSource = dt;
        ddlXMMC.DataBind();

        ListItem item = new ListItem("全部", "-1");
        ddlXMMC.Items.Insert(0,item);

        if (ddlXMMC.Items.FindByValue(strXMBH) != null)
            ddlXMMC.SelectedValue = Session["XMBH"].ToString();
        else
            ddlXMMC.SelectedValue = "-1";
        //t_FName.Text = dt.Rows.Count.ToString();


    }
    protected void ddlXMMC_SelectedIndexChanged(object sender, EventArgs e)
    {

        showInfo();
    }
}