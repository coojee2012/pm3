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
using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.Common;
using System.Text;
public partial class Government_AppEntAction_BadManageList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
            ShowInfo();
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么？')");


           

        }
    }

    private string ShowCountInfo()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("where FIsDeleted=0 and FBaseInfoId is not null  ");
        builder.Append("and fbaseinfoid in(select fid from cf_ent_baseinfo   where 1=1");
        if (txtFName.Text.Trim() != "")
        {
            builder.Append(" and FName like '%" + txtFName.Text.Trim() + "%'");
        }
        if (dbFSystemId.SelectedValue != "")
        {
            builder.Append(" and FSystemId='" + dbFSystemId.SelectedValue + "'");
        }
        builder.Append(")");

        StringBuilder sb = new StringBuilder();

        if (dbFDeptId.SelectedValue != "")
        {
            sb.Append(" and FDeptId like '" + dbFDeptId.SelectedValue + "%'");
        }

        if (txtFDeptIdName.Text.Trim() != "")
        {
            sb.Append(" and FDeptIdName like '%" + txtFDeptIdName.Text + "%'");
        }
        if (dbFState.SelectedValue != "")
        {
            sb.Append(" and FState ='" + dbFState.SelectedValue + "'");
        }

      
        return "";
    }


    //判断是否有权限
    public bool isHasAppRight
    {
        get
        {
            if (ViewState["isHasAppRight"] == null)
            {
                //string sIsApp = rc.GetSignValue(EntityTypeEnum.EsUser, "FPri", "FId='" + Session["DFUserId"].ToString() + "'");
                //if (!string.IsNullOrEmpty(sIsApp) && sIsApp == "1")
                //{
                    ViewState["isHasAppRight"] = true;
                //}
                //else
                //{
                //    ViewState["isHasAppRight"] = false;
                //}
            }
            return (bool)ViewState["isHasAppRight"];
        }
    }


    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" FIsdeleted=0 and ftype=1 order by forder desc ");
        DataTable dt = rc.GetTable(EntityTypeEnum.EsSystemName, "FName,FNumber", sb.ToString());
        dbFSystemId.DataSource = dt;
        dbFSystemId.DataTextField = "FName";
        dbFSystemId.DataValueField = "FNumber";
        dbFSystemId.DataBind();
        dbFSystemId.Items.Insert(0, new ListItem("请选择", ""));

        if (!string.IsNullOrEmpty(Request.QueryString["sysId"]))
        {
            dbFSystemId.SelectedIndex = dbFSystemId.Items.IndexOf(dbFSystemId.Items.FindByValue(Request.QueryString["sysId"]));
            dbFSystemId.Enabled = false;
        }


        sb.Remove(0, sb.Length);
        sb.Append(" select FNumber,");
        sb.Append(" case flevel when 1 then fnumber when 2 then fnumber when 3 then fparentid end as FoderNumber ,");
        sb.Append(" case flevel when 1 then FName when 2 then '--'+FName when 3 then '----'+FName end as FName ");
        sb.Append(" from cf_sys_managedept");
        sb.Append(" where fnumber like '" + Session["DFId"].ToString() + "%' and FClassNumber='102009' and fisdeleted=0  and fnumber<>2116");
        sb.Append(" order by fodernumber,flevel ");
        dt = rc.GetTable(sb.ToString());
        dbFDeptId.DataSource = dt;
        dbFDeptId.DataTextField = "FName";
        dbFDeptId.DataValueField = "FNumber";
        dbFDeptId.DataBind();
        dbFDeptId.Items.Insert(0, new ListItem("--请选择--", ""));

    }

    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (txtFName.Text != "")
        {
            sb.Append(" and t1.fname like '%" + txtFName.Text + "%'");
        }
        if (dbFSystemId.SelectedValue != "")
        {
            sb.Append(" and t1.fsystemid = '" + dbFSystemId.SelectedValue + "'");
        }
        if (txtFDeptIdName.Text != "")
        {
            sb.Append(" and t2.FDeptIdName like '%" + txtFDeptIdName.Text + "%'");
        }
        if (dbFState.SelectedValue != "")
        {
            sb.Append(" and t2.FState = '" + dbFState.SelectedValue + "'");
        }
        if (dbFDeptId.SelectedValue != "")
        {
            sb.Append(" and t2.FDeptId like '" + dbFDeptId.SelectedValue + "%'");
        }
        else
        {
            sb.Append(" and t2.FDeptId like '" + Session["DFId"].ToString() + "%'");
        }
        return sb.ToString();
    }

    private void ShowInfo()
    {
        //Label1.Text = ShowCountInfo();


        StringBuilder sb = new StringBuilder();

        sb.Append(" select t2.fid,t2.fbaseinfoid,t1.fsystemid,t1.fname,(select top 1 fdesc from cf_sys_systemname where fnumber = t1.fsystemid) fsystemname,");
        sb.Append(" t1.flinkman,t1.ftel,t2.fdeptidname,t2.fdtime,");
        sb.Append(" (select top 1 fname from cf_sys_managedept where fnumber = t2.FDeptId) FDeptName,t2.FDeptId,");
        sb.Append(" (select count(*) from cf_ent_badaction t4 where t4.FBaseInfoId = t1.FId ) FCount, ");
        sb.Append(" (select top 1 fname from cf_sys_managedept where fnumber = t2.FAppDeptId) FAppDeptId, t2.fstate,t2.fresult ");
        sb.Append(" from cf_ent_baseinfo t1,cf_ent_badaction t2 ");
        sb.Append(" where t1.fid = t2.fbaseinfoid ");
        //sb.Append(" and t2.fresult=6 ");
        sb.Append(" and t1.fisdeleted=0 and t2.fisdeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" order by t2.FDeptId,t2.FDTime desc");

        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "BadAction_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }


    protected void BadAction_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string sId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string sBaseId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            string sSystemId = e.Item.Cells[e.Item.Cells.Count - 3].Text;
            string sState = e.Item.Cells[e.Item.Cells.Count - 4].Text;
            string sResult = e.Item.Cells[e.Item.Cells.Count - 5].Text;
            string sDeptId = e.Item.Cells[e.Item.Cells.Count - 6].Text;
            string sAppDeptId = e.Item.Cells[e.Item.Cells.Count - 9].Text;

            string sRoleId = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + sBaseId + "'");//登录角色

            CheckBox cb = (CheckBox)e.Item.Cells[0].Controls[1];
            Label lHistory = (Label)e.Item.Cells[e.Item.Cells.Count - 4].FindControl("lHistory");


            StringBuilder sb = new StringBuilder();
            sb.Append(" select FIsApp from CF_App_ActionRecord Where FDeptId='" + Session["DFId"].ToString() + "'");
            sb.Append(" and FLinkId='" + sId + "'");
            string sIsApp = rc.GetSignValue(sb.ToString());
            if (!isHasAppRight)
            {
                if (!string.IsNullOrEmpty(sIsApp) && sIsApp == "1")
                {
                    cb.Enabled = false;
                }
            }


            string sScript = "showApproveWindow1('BadActionEdit.aspx?fid=" + sId + "',682,689)";
            e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\">" + e.Item.Cells[2].Text + "</a>";


            if (sState == "1")
            {
                e.Item.Cells[10].Text += "已发布";
            }
            else
            {
                e.Item.Cells[10].Text += "未发布";
            }


            string sUrl = rc.getSysQurl(sSystemId);
            sUrl += "&fbid=" + sBaseId + "&frid=" + getRId(sSystemId);
            lHistory.Text = "<a href='" + sUrl + "' target='_blank'>查看信用档案</a>";
        }
    }
    private string getRId(string FSystemId)
    {
        string str = "";
        switch (FSystemId)
        {
            case "150":
                str = "6601";
                break;
            case "175":
                str = "1655";
                break;

        }

        return str;
    }



    protected void btnQuery_Click1(object sender, EventArgs e)
    {
        ShowInfo();
    }


    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }


    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);

        int iCount = BadAction_List.Items.Count;

        StringBuilder sb = new StringBuilder();


        for (int i = 0; i < iCount; i++)
        {
            CheckBox cb = BadAction_List.Items[i].Cells[0].Controls[1] as CheckBox;

            if (cb.Checked == true)
            {

                string sDeptId = BadAction_List.Items[i].Cells[BadAction_List.Columns.Count - 6].Text;
                if (!isHasAppRight)
                {
                    if (sDeptId != Session["DFId"].ToString())
                    {
                        BadAction_List.Items[i].BackColor = System.Drawing.Color.Red;
                        continue;
                    }
                }
                string sId = BadAction_List.Items[i].Cells[BadAction_List.Columns.Count - 1].Text;

                sb.Append(" delete  from CF_Ent_BadAction ");
                sb.Append(" where fid='" + sId + "'");

                sb.Append(" delete  from CF_Ent_BadActionCode ");
                sb.Append(" where FACtionId='" + sId + "'");

                sb.Append(" delete  from CF_App_ActionRecord ");
                sb.Append(" where FLinkId='" + sId + "'");
            }

        }

        if (sb.Length > 0)
        {
            if (rc.PExcute(sb.ToString()))
            {
                tool.showMessage("删除成功");
                ShowInfo();
            }
            else
            {
                tool.showMessage("删除失败");
            }
        }
        else
        {
            tool.showMessage("请选择要删除的数据");
        }
    }


    protected void btnPub_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);

        int iCount = BadAction_List.Items.Count;

        StringBuilder sb = new StringBuilder();


        ArrayList arrEn = new ArrayList();
        ArrayList arrSl = new ArrayList();
        ArrayList arrSo = new ArrayList();
        ArrayList arrKey = new ArrayList();

        SortedList sl = new SortedList();

        for (int i = 0; i < iCount; i++)
        {
            CheckBox cb = BadAction_List.Items[i].Cells[0].Controls[1] as CheckBox;
            if (cb.Checked == true)
            {
                string sId = BadAction_List.Items[i].Cells[BadAction_List.Columns.Count - 1].Text;
                sb.Remove(0, sb.Length);
                sb.Append(" select FId from CF_App_ActionRecord ");
                sb.Append(" where FDeptId='" + Session["DFId"].ToString() + "'");
                sb.Append(" and FLinkId='" + sId + "'");
                string sAppId = rc.GetSignValue(sb.ToString());
                if (!string.IsNullOrEmpty(sAppId))
                {
                    sl = new SortedList();
                    sl.Add("FID", sAppId);
                    sl.Add("FResult", 1);
                    sl.Add("FIsApp", 1);

                    arrEn.Add(EntityTypeEnum.EaAppActionRecord);
                    arrSl.Add(sl);
                    arrSo.Add(SaveOptionEnum.Update);
                    arrKey.Add("FID");

                }
                else
                {
                    sl = new SortedList();
                    sl.Add("FID", Guid.NewGuid().ToString());
                    sl.Add("FLinkId", sId);
                    sl.Add("FResult", 1);
                    sl.Add("FDeptId", Session["DFId"].ToString());
                    sl.Add("FIsApp", 1);
                    sl.Add("FReportTime", DateTime.Now);
                    sl.Add("FIsDeleted", 0);
                    sl.Add("FCreateTime", DateTime.Now);

                    arrEn.Add(EntityTypeEnum.EaAppActionRecord);
                    arrSl.Add(sl);
                    arrSo.Add(SaveOptionEnum.Insert);
                    arrKey.Add("FID");
                }

                sl = new SortedList();
                sl.Add("FID", sId);
                sl.Add("FResult", 6);
                sl.Add("FState", 1);
                sl.Add("FAppDeptId", Session["DFId"].ToString());

                arrEn.Add(EntityTypeEnum.EbBadAction);
                arrSl.Add(sl);
                arrSo.Add(SaveOptionEnum.Update);
                arrKey.Add("FID");
            }
        }

        if (arrEn.Count == 0)
        {
            tool.showMessage("请选择要发布的数据");
            return;
        }


        EntityTypeEnum[] ens = new EntityTypeEnum[arrEn.Count];
        SortedList[] sls = new SortedList[arrEn.Count];
        SaveOptionEnum[] sos = new SaveOptionEnum[arrEn.Count];
        string[] keys = new string[arrEn.Count];
        for (int i = 0; i < arrEn.Count; i++)
        {

            ens[i] = (EntityTypeEnum)arrEn[i];
            sls[i] = new SortedList();
            sls[i] = (SortedList)arrSl[i];
            sos[i] = (SaveOptionEnum)arrSo[i];
            keys[i] = (string)arrKey[i];
        }

        if (rc.SaveEBaseM(ens, sls, keys, sos))
        {
            tool.showMessage("发布成功");
            ShowInfo();

        }
        else
        {
            tool.showMessage("发布失败");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);

        int iCount = BadAction_List.Items.Count;

        StringBuilder sb = new StringBuilder();


        ArrayList arrEn = new ArrayList();
        ArrayList arrSl = new ArrayList();
        ArrayList arrSo = new ArrayList();
        ArrayList arrKey = new ArrayList();

        for (int i = 0; i < iCount; i++)
        {
            CheckBox cb = BadAction_List.Items[i].Cells[0].Controls[1] as CheckBox;
            if (cb.Checked == true)
            {
                string sId = BadAction_List.Items[i].Cells[BadAction_List.Columns.Count - 1].Text;
                sb.Remove(0, sb.Length);
                sb.Append(" select FId from CF_App_ActionRecord ");
                sb.Append(" where FDeptId='" + Session["DFId"].ToString() + "'");
                sb.Append(" and FLinkId='" + sId + "'");
                string sAppId = rc.GetSignValue(sb.ToString());
                if (!string.IsNullOrEmpty(sAppId))
                {
                    SortedList sl = new SortedList();
                    sl.Add("FID", sAppId);
                    sl.Add("FResult", 3);
                    sl.Add("FIsApp", 1);

                    arrEn.Add(EntityTypeEnum.EaAppActionRecord);
                    arrSl.Add(sl);
                    arrSo.Add(SaveOptionEnum.Update);
                    arrKey.Add("FID");

                    sl = new SortedList();
                    sl.Add("FID", sId);
                    sl.Add("FResult", 6);
                    sl.Add("FState", 0);
                    sl.Add("FAppDeptId", Session["DFId"].ToString());

                    arrEn.Add(EntityTypeEnum.EbBadAction);
                    arrSl.Add(sl);
                    arrSo.Add(SaveOptionEnum.Update);
                    arrKey.Add("FID");

                }
            }
        }

        if (arrEn.Count == 0)
        {
            tool.showMessage("请选择要撤销发布的数据");
            return;
        }


        EntityTypeEnum[] ens = new EntityTypeEnum[arrEn.Count];
        SortedList[] sls = new SortedList[arrEn.Count];
        SaveOptionEnum[] sos = new SaveOptionEnum[arrEn.Count];
        string[] keys = new string[arrEn.Count];
        for (int i = 0; i < arrEn.Count; i++)
        {

            ens[i] = (EntityTypeEnum)arrEn[i];
            sls[i] = new SortedList();
            sls[i] = (SortedList)arrSl[i];
            sos[i] = (SaveOptionEnum)arrSo[i];
            keys[i] = (string)arrKey[i];
        }

        if (rc.SaveEBaseM(ens, sls, keys, sos))
        {
            tool.showMessage("撤销成功");
            ShowInfo();

        }
        else
        {
            tool.showMessage("撤销失败");
        }
    }
}
