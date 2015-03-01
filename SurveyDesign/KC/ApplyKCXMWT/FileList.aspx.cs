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
using System.Drawing;

public partial class PrjManage_ConstructionLicence_ApplyBaseinfo_Paperdrawing : energyEntBasePage
{
    RCenter rc = new RCenter();
    RCenter rq = new RCenter();
    string order = "";
    int colorIndex = 0; 
    int count = 1;
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!Page.IsPostBack)
        {
            InSertOrUpDate();
            ShowInfo();
        }
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Remove(0, sb.Length);
        sb.Append(" select FId,FFileName,FFileAmount,FRemark,Forder from CF_Sys_PrjList");
        sb.Append(" where FManageType='" + this.Session["FManageTypeId"] + "'");
        sb.Append(" order by forder ");

        this.Pager1.className = "RCenter";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "File_list";
        this.Pager1.controltype = "GridView";
        this.Pager1.dataBind();
    }

    protected void File_list_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string fid = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FId"));

            HtmlInputButton upload = e.Row.Cells[e.Row.Cells.Count - 3].FindControl("btnUpLoad") as HtmlInputButton;
            if (Session["FIsApprove"] != null && Session["FIsApprove"].ToString() == "1")
            {
                upload.Disabled = true;
                upload.Attributes.Add("onclick", "showApproveWindow('PaperdrawingAdd.aspx?fid=" + fid + "&isOver=1',700,500);");
            }
            else
            {
                upload.Attributes.Add("onclick", "showApproveWindow('PaperdrawingAdd.aspx?fid=" + fid + "',700,500);location.reload();");
            }
            Label IsUpload = e.Row.Cells[e.Row.Cells.Count - 3].FindControl("IsUpload") as Label;

            TextBox FFileNo = e.Row.Cells[3].Controls[1] as TextBox;
            TextBox FCount = e.Row.Cells[4].Controls[1] as TextBox;

            string fPrjFileId = "";
            StringBuilder sb = new StringBuilder();
            sb.Append("FPrjListId='" + fid + "' and FAppId='" + this.Session["FAppId"] + "'");
            DataTable dt = rq.GetTable(EntityTypeEnum.EqPrjFile, "*", sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                fPrjFileId = dt.Rows[0]["fid"].ToString();
                FFileNo.Text = dt.Rows[0]["FFileNo"].ToString();
                sb.Remove(0, sb.Length);
                string f_count = rq.GetSignValue("select count(fid) from cf_appprj_fileother where fprjfileid='" + fPrjFileId + "'");
                FCount.Text = f_count;
            }
            sb.Remove(0, sb.Length);
            sb.Append("FPrjFileId='" + fPrjFileId + "' and FIsDeleted=0");
            DataTable dt1 = rq.GetTable(EntityTypeEnum.EqPrjFileOther, "", sb.ToString());
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                IsUpload.Text = "<font color='green'>是</font>";
                upload.Value = "查看文件";
                upload.Disabled = false;
            }
            else
            {
                IsUpload.Text = "<font color='red'>否</font>";
            }



            string forder = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FOrder"));
            if (order != forder)
            {
                if (colorIndex == 0)
                    colorIndex = 1;
                else
                    colorIndex = 0;
                if (e.Row.RowIndex > 0)
                    File_list.Rows[e.Row.RowIndex - count].Cells[0].RowSpan = count;
                count = 1;
            }
            else
            {
                count++;
                if (e.Row.RowIndex > 0)
                    e.Row.Cells[0].Visible = false;
            } 
            order = forder; 
        }
    }

    private void SaveInfo()
    {
        ArrayList arrEn = new ArrayList();
        ArrayList arrSo = new ArrayList();
        ArrayList arrKey = new ArrayList();
        ArrayList arrSl = new ArrayList();

        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        int itemCount = this.File_list.Rows.Count;
        for (int i = 0; i < itemCount; i++)
        {
            string fid = this.File_list.Rows[i].Cells[this.File_list.Columns.Count - 1].Text;
            sb.Remove(0, sb.Length);
            sb.Append(" select fid from cf_appprj_file");
            sb.Append(" where FAppId='" + this.Session["FAppId"] + "' and FPrjListId='" + fid + "'");
            string fFileId = rq.GetSignValue(sb.ToString());

            TextBox FFileNo = (TextBox)File_list.Rows[i].Cells[3].Controls[1];
            TextBox FCount = (TextBox)File_list.Rows[i].Cells[4].Controls[1];

            if (fFileId != null && fFileId != "")
            {
                SortedList sl = new SortedList();
                sl.Add("FID", fFileId);
                sl.Add("FFileNo", FFileNo.Text.Trim());
                sl.Add("FCount", FCount.Text.Trim());
                sl.Add("FName", this.File_list.Rows[i].Cells[1].Text);


                arrEn.Add(EntityTypeEnum.EqPrjFile);
                arrSo.Add(SaveOptionEnum.Update);
                arrKey.Add("FID");
                arrSl.Add(sl);
            }

        }

        int iCount = arrSl.Count;
        if (iCount <= 0)
        {
            return;
        }

        SortedList[] sls = new SortedList[iCount];
        string[] fkeys = new string[iCount];
        SaveOptionEnum[] sos = new SaveOptionEnum[iCount];
        EntityTypeEnum[] ens = new EntityTypeEnum[iCount];

        for (int i = 0; i < iCount; i++)
        {
            sls[i] = (SortedList)arrSl[i];
            fkeys[i] = (string)arrKey[i];
            sos[i] = (SaveOptionEnum)arrSo[i];
            ens[i] = (EntityTypeEnum)arrEn[i];
        }

        if (rq.SaveEBaseM(ens, sls, fkeys, sos))
        {
            tool.showMessage("保存成功");
        }
        else
        {
            tool.showMessage("保存失败");
        }

    }

    private void InSertOrUpDate()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select FId,FFileName,FFileAmount,FRemark,FManageType from CF_Sys_PrjList");
        sb.Append(" where FManageType='" + this.Session["FManageTypeId"] + "'");
        DataTable dt = rc.GetTable(sb.ToString());

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Remove(0, sb.Length);
            sb.Append(" select fid from cf_appprj_file ");
            sb.Append(" where FAppId='" + this.Session["FAppId"] + "' ");
            sb.Append(" and FPrjListId='" + dt.Rows[i]["fid"].ToString() + "'");
            string fPrjFileId = rq.GetSignValue(sb.ToString());
            SortedList sl = new SortedList();
            SaveOptionEnum so = SaveOptionEnum.Insert;
            if (fPrjFileId != null && fPrjFileId != "")
            {
                so = SaveOptionEnum.Update;
                sl.Add("FID", fPrjFileId);
            }
            else
            {
                sl.Add("FID", Guid.NewGuid().ToString());
                sl.Add("FAppId", this.Session["FAppId"]);
                sl.Add("FPrjListId", dt.Rows[i]["FId"].ToString());
                sl.Add("FCreateTime", DateTime.Now);
            }

            sl.Add("FName", dt.Rows[i]["FFileName"].ToString());
            sl.Add("FManageType", dt.Rows[i]["FManageType"].ToString());

            rq.SaveEBase(EntityTypeEnum.EqPrjFile, sl, "FID", so);

        }

        sb.Remove(0, sb.Length);
        #region 将新加入到表中的新业务数据项
        sb.Append("select * from cf_appprj_file where fappid='" + this.Session["FAppId"].ToString() + "' ");
        sb.Append(" and fmanagetype=" + this.Session["FManageTypeId"]);
        dt = rq.GetTable(sb.ToString());
        #endregion

        #region 将原始业务类别中的附件并加载到olDTable中
        sb.Remove(0, sb.Length);
        sb.Append("select * from cf_appprj_file where fappid='" + this.Session["FAppId"].ToString() + "' ");
        sb.Append(" and fmanagetype!=" + this.Session["FManageTypeId"]);
        DataTable olDtFile = rq.GetTable(sb.ToString());
        #endregion

        if (olDtFile != null && olDtFile.Rows.Count > 0)
        {
            StringBuilder sbfilename = new StringBuilder();
            sb.Remove(0, sb.Length);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < olDtFile.Rows.Count; j++)
                {
                    if (dt.Rows[i]["FName"].ToString() == olDtFile.Rows[j]["FName"].ToString())
                    {
                        sb.Append("update cf_appprj_fileother set fprjfileid='" + dt.Rows[i]["FId"].ToString() + "' where ");
                        sb.Append(" fprjfileid='" + olDtFile.Rows[j]["FId"].ToString() + "'; ");
                        sbfilename.Append("update cf_appprj_file set ffileno='" + olDtFile.Rows[j]["FFileNo"].ToString() + "', ");
                        sbfilename.Append(" fcount='" + olDtFile.Rows[j]["fcount"] + "' ");
                        sbfilename.Append(" where fid='" + dt.Rows[i]["FId"].ToString() + "'; ");
                    }
                }
            }
            rq.PExcute(sb.ToString());
            rq.PExcute(sbfilename.ToString());
            for (int k = 0; k < olDtFile.Rows.Count; k++)
            {
                rq.PExcute("delete from cf_appprj_file where fid='" + olDtFile.Rows[k]["FId"].ToString() + "';");
            }
        }
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("Report.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("../ApplyBaseinfo/Report.aspx");
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("../ApplyBaseinfo/Fillindata.aspx");
    }



}
