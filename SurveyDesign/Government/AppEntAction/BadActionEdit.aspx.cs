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
using Approve.RuleApp;
using Approve.Common;
using Approve.EntityBase;

public partial class Government_AppEntAction_BadActionEdit : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Button1.Attributes.Add("onclick", "showApproveWindow2('SelectEnt.aspx?sysId=" + Request.QueryString["sysId"] + "&stype=action',630,700)");

            this.t_FDTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.t_FBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.t_FEndTime.Text = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");
            this.FWDeptiId.Text = rc.GetSignValue(EntityTypeEnum.EsUser, "fcompany", "FId='" + Session["DFUserId"].ToString() + "'");

            if (!string.IsNullOrEmpty(Request["fbid"]))
            {
                string sBaseId = rc.GetSignValue(EntityTypeEnum.EbBaseInfo, "FId", "Fid='" + Request["fbid"] + "'");
                if (string.IsNullOrEmpty(sBaseId))
                {
                    this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>alert('标准数据库中没有该企业的数据不能添加');this.close();</script>");
                    return;
                }
                this.ViewState["FBID"] = Request["fbid"];
                this.t_FBaseInfoId.Value = Request["fbid"];
                Button1.Visible = false;
                txtFBaseName.Text = rc.GetSignValue(EntityTypeEnum.EbBaseInfo, "FName", "Fid='" + Request["fbid"] + "'");
            }

            if (!string.IsNullOrEmpty(Request["fid"]))
            {
                this.ViewState["FID"] = Request["fid"];
                ShowInfo();
                ShowDgInfo();
                Button1.Visible = false;
            }

            if (isHasAppRight)
            {
                TD1.Visible = true;
                TD2.Visible = true;


                if (ViewState["FID"] != null)
                {
                    string sDeptId = rc.GetSignValue(EntityTypeEnum.EbBadAction, "FDeptId", "FId='" + ViewState["FID"].ToString() + "'");
                    if (!string.IsNullOrEmpty(sDeptId))
                    {
                        if (EConvert.ToInt(Session["DFLevel"]) > EConvert.ToInt(rc.GetSignValue(EntityTypeEnum.EsManageDept, "FLevel", "FNumber='" + sDeptId + "'")))
                        {
                            btnSave.Enabled = false;
                            btnReport.Enabled = false;
                            btnAdd.Disabled = true;
                            btnDel.Enabled = false;
                        }
                    }
                }

            }
            else
            {
                TD1.Visible = false;
                TD2.Visible = false;


                if (ViewState["FID"] != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" select fisapp from CF_App_ActionRecord where flinkid ='" + ViewState["FID"].ToString() + "'");
                    sb.Append(" and FDeptId='" + Session["DFId"].ToString() + "'");
                    string sIsApp = rc.GetSignValue(sb.ToString());
                    if (!string.IsNullOrEmpty(sIsApp))
                    {
                        if (sIsApp == "1")
                        {
                            btnSave.Enabled = false;
                            btnReport.Enabled = false;
                            btnAdd.Disabled = true;
                            btnDel.Enabled = false;
                        }
                    }

                    sb.Remove(0, sb.Length);
                    sb.Append(" select fdeptid,fresult from CF_Ent_BadAction where fid='" + ViewState["FID"].ToString() + "'");
                    DataTable dtApp = rc.GetTable(sb.ToString());

                    if (Session["DFId"].ToString() == dtApp.Rows[0]["fdeptid"].ToString())
                    {
                        if (dtApp.Rows[0]["fresult"].ToString() == "2")
                        {
                            btnSave.Enabled = true;
                            btnReport.Enabled = true;
                            btnAdd.Disabled = false;
                            btnDel.Enabled = true;
                        }
                    }
                    else
                    {
                        btnSave.Enabled = false;
                        btnReport.Enabled = false;
                        btnAdd.Disabled = true;
                        btnDel.Enabled = false;
                    }
                }
            }

            this.btnSave.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            this.btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?')");
        }
    }


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

    private void ShowInfo()
    {
        if (this.ViewState["FID"] == null)
        {
            return;
        }
        HFId.Value = this.ViewState["FID"].ToString();
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Ent_BadAction where fid='" + this.ViewState["FID"].ToString() + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            //this.FWDeptiId.Text = dt.Rows[0]["FWDeptiId"].ToString();
            this.Govdeptid1.FNumber = this.t_FRegionId.Value;
            txtFBaseName.Text = rc.GetSignValue(EntityTypeEnum.EbBaseInfo, "FName", "fid='" + dt.Rows[0]["FBaseInfoId"].ToString() + "'");
        }
    }

    private void ShowDgInfo()
    {
        if (this.ViewState["FID"] == null)
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" select ceba.fid ,csba.fid FACtionCodeId,csba.fnumber,csba.fparentid,csba.fname,ceba.FACtionDesc,ceba.FScore ");
        sb.Append(" from CF_Ent_BadActionCode ceba,CF_Sys_BadActionCode csba ");
        sb.Append(" where ceba.FACtionCodeId=csba.fid ");
        sb.Append(" and ceba.FACtionId='" + this.ViewState["FID"].ToString() + "' ");
        sb.Append(" and ceba.fisdeleted=0 and  csba.fisdeleted=0 order by csba.fparentid,csba.forder,ceba.fcreatetime desc");
        Action_List.DataSource = rc.GetTable(sb.ToString());
        Action_List.DataBind();
    }


    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        if (this.t_FBaseInfoId.Value == "")
        {
            tool.showMessage("请选择对应的企业");
            return;
        }

        t_FRegionId.Value = this.Govdeptid1.FNumber;
        //if (t_FRegionId.Value == "")
        //{
        //    tool.showMessage("请选择所在行政区划");
        //    return;
        //}

        SortedList sl = tool.getPageValue();
        SaveOptionEnum so = SaveOptionEnum.Insert;
        if (this.ViewState["FID"] != null)
        {
            sl.Add("FID", this.ViewState["FID"].ToString());
            so = SaveOptionEnum.Update;
        }
        else
        {
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
            //sl.Add("FWDeptiId", rc.GetSignValue(EntityTypeEnum.EsUser, "fcompany", "FId='" + Session["DFUserId"].ToString() + "'"));
            //sl.Add("FWRoleId", Session["DFRoleId"].ToString());
            sl.Add("FDeptId", Session["DFId"].ToString());
            sl.Add("FResult", 0);
            sl.Add("FState", 0);
            sl.Add("FAppid", Request.QueryString["FAppId"]);
        }

        if (rc.SaveEBase(EntityTypeEnum.EbBadAction, sl, "FID", so))
        {
            tool.showMessage("保存成功");
            this.ViewState["FID"] = sl["FID"].ToString();
            this.hiddle_IsSaveOk.Value = "1";
            HFId.Value = sl["FID"].ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append(" select FDeptId from CF_Ent_BadAction where fid='" + sl["FID"].ToString() + "'");
            string sDeptId = rc.GetSignValue(sb.ToString());


            if (isHasAppRight)
            {
                if (sDeptId == Session["DFId"].ToString())
                {
                    string sId = sl["FID"].ToString();
                    sb.Remove(0, sb.Length);
                    sb.Append(" select fid from CF_App_ActionRecord where FLinkId='" + sId + "'");
                    sb.Append(" and FDeptId='" + Session["DFId"].ToString() + "'");
                    string sAppId = rc.GetSignValue(sb.ToString());
                    so = SaveOptionEnum.Insert;
                    sl = new SortedList();
                    if (!string.IsNullOrEmpty(sAppId))
                    {
                        so = SaveOptionEnum.Update;
                        sl.Add("FID", sAppId);
                        sl.Add("FResult", 6);
                        sl.Add("FDeptId", Session["DFId"].ToString());
                        sl.Add("FIsApp", 1);
                        sl.Add("FReportTime", DateTime.Now);
                    }
                    else
                    {
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FLinkId", sId);
                        sl.Add("FResult", 6);
                        sl.Add("FDeptId", Session["DFId"].ToString());
                        sl.Add("FIsApp", 1);
                        sl.Add("FReportTime", DateTime.Now);
                        sl.Add("FIsDeleted", 0);
                        sl.Add("FCreateTime", DateTime.Now);
                    }
                    rc.SaveEBase(EntityTypeEnum.EaAppActionRecord, sl, "FID", so);
                    rc.PExcute(" update CF_Ent_BadAction set fresult=6,FAppDeptId='" + Session["DFId"].ToString() + "' where fid='" + sId + "'");
                }
            }
            else
            {
                string sId = sl["FID"].ToString();
                sb.Remove(0, sb.Length);
                sb.Append(" select fid from CF_App_ActionRecord where FLinkId='" + sId + "'");
                sb.Append(" and FDeptId='" + Session["DFId"].ToString() + "'");
                string sAppId = rc.GetSignValue(sb.ToString());
                so = SaveOptionEnum.Insert;
                sl = new SortedList();
                if (!string.IsNullOrEmpty(sAppId))
                {
                    so = SaveOptionEnum.Update;
                    sl.Add("FID", sAppId);
                    sl.Add("FResult", 0);
                    sl.Add("FDeptId", Session["DFId"].ToString());
                    sl.Add("FIsApp", 0);
                    sl.Add("FReportTime", DateTime.Now);
                }
                else
                {
                    sl.Add("FID", Guid.NewGuid().ToString());
                    sl.Add("FLinkId", sId);
                    sl.Add("FResult", 0);
                    sl.Add("FDeptId", Session["DFId"].ToString());
                    sl.Add("FIsApp", 0);
                    sl.Add("FReportTime", DateTime.Now);
                    sl.Add("FIsDeleted", 0);
                    sl.Add("FCreateTime", DateTime.Now);
                }
                rc.SaveEBase(EntityTypeEnum.EaAppActionRecord, sl, "FID", so);
                rc.PExcute(" update CF_Ent_BadAction set fresult=1,FAppDeptId='" + Session["DFId"].ToString() + "' where fid='" + sId + "'");
            }
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }

    protected void Action_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            string fPrarentid = e.Item.Cells[e.Item.Cells.Count - 3].Text;
            e.Item.Cells[3].Text = rc.GetSignValue(EntityTypeEnum.EsBadActionCode, "FName", "FNumber='" + fPrarentid + "'");
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1).ToString();
            string sScirpt = "showApproveWindow('ActionDetail.aspx?fid=" + fid + "',500,300);";
            e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"" + sScirpt + "\">" + e.Item.Cells[2].Text + "</a>";
        }
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
        ShowDgInfo();
    }

    protected void btnSave_Click1(object sender, EventArgs e)
    {
        SaveInfo();
        ShowInfo();
    }

    protected void btnShowEntName_Click(object sender, EventArgs e)
    {
        if (this.t_FBaseInfoId.Value != "")
        {
            this.txtFBaseName.Text = rc.GetSignValue(EntityTypeEnum.EbBaseInfo, "FName", "FId='" + this.t_FBaseInfoId.Value + "'");
        }
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        if (ViewState["FID"] == null)
        {
            return;
        }


        pageTool tool = new pageTool(this.Page);
        int iCount = this.Action_List.Items.Count;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < iCount; i++)
        {
            string fid = this.Action_List.Items[i].Cells[this.Action_List.Columns.Count - 1].Text;

            string fScore = this.Action_List.Items[i].Cells[this.Action_List.Columns.Count - 4].Text;

            CheckBox box = (CheckBox)this.Action_List.Items[i].Cells[0].Controls[1];
            if (box.Checked == true)
            {
                sb.Append(" delete from CF_Ent_BadActionCode where fid='" + fid + "' ");
                sb.Append(" update CF_Ent_BadAction set FScore=FScore-" + EConvert.ToFloat(fScore));
                sb.Append(" where fid='" + ViewState["FID"].ToString() + "'");

            }

        }
        if (sb.Length == 0)
        {
            tool.showMessage("请选择");
            return;
        }
        if (rc.PExcute(sb.ToString()))
        {
            ShowInfo();
            ShowDgInfo();
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ViewState["FID"] == null)
        {
            return;
        }

        string sId = ViewState["FID"].ToString();
        StringBuilder sb = new StringBuilder();
        sb.Append(" select FDeptId from CF_Ent_BadAction ");
        sb.Append(" where fid='" + sId + "'");
        string sPutDept = rc.GetSignValue(sb.ToString());

        if (sPutDept == Session["DFId"].ToString())
        {
            sb.Remove(0, sb.Length);
            sb.Append(" delete from CF_App_ActionRecord where FLinkId='" + sId + "'");
            rc.PExcute(sb.ToString());


            SortedList sl = new SortedList();
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FLinkId", sId);
            sl.Add("FDeptId", Session["DFId"].ToString());
            sl.Add("FIsApp", 1);
            sl.Add("FResult", 1);
            sl.Add("FReportTime", DateTime.Now);
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
            rc.SaveEBase(EntityTypeEnum.EaAppActionRecord, sl, "FID", SaveOptionEnum.Insert);

            sb.Remove(0, sb.Length);
            sb.Append(" update CF_Ent_BadAction set FResult=1,FAppDeptId='" + Session["DFId"].ToString() + "' where fid='" + sId + "'");
            rc.PExcute(sb.ToString());
        }

        sb.Remove(0, sb.Length);
        sb.Append(" select FId from CF_App_ActionRecord ");
        sb.Append(" where FDeptId='" + Session["DFId"].ToString() + "'");
        sb.Append(" and FLinkId='" + sId + "'");
        string sAppId = rc.GetSignValue(sb.ToString());
        if (!string.IsNullOrEmpty(sAppId))
        {
            SortedList sl = new SortedList();
            sl.Add("FID", sAppId);
            sl.Add("FResult", 1);
            sl.Add("FIsApp", 1);
            rc.SaveEBase(EntityTypeEnum.EaAppActionRecord, sl, "FID", SaveOptionEnum.Update);
            sb.Remove(0, sb.Length);
            sb.Append(" update CF_Ent_BadAction set FResult=1,FAppDeptId='" + Session["DFId"].ToString() + "' where fid='" + sId + "'");
            rc.PExcute(sb.ToString());

        }


        sb.Remove(0, sb.Length);
        sb.Append(" select FParentId from cf_sys_managedept where fnumber='" + Session["DFId"].ToString() + "'");
        string sUpdeptId = rc.GetSignValue(sb.ToString());
        if (!string.IsNullOrEmpty(sUpdeptId))
        {
            SortedList sl = new SortedList();
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FLinkId", sId);
            sl.Add("FResult", 0);
            sl.Add("FDeptId", sUpdeptId);
            sl.Add("FIsApp", 0);
            sl.Add("FReportTime", DateTime.Now);
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
            rc.SaveEBase(EntityTypeEnum.EaAppActionRecord, sl, "FID", SaveOptionEnum.Insert);

            sb.Remove(0, sb.Length);
            sb.Append(" update CF_Ent_BadAction set FResult=1,FAppDeptId='" + sUpdeptId + "' where fid='" + sId + "'");
            rc.PExcute(sb.ToString());
        }

        btnReport.Enabled = false;
        btnSave.Enabled = false;
        btnAdd.Disabled = true;
        btnDel.Enabled = false;
        Button1.Disabled = true;
    }
}

