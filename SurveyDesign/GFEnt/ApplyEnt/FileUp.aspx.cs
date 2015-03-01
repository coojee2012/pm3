using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.Data;
using Approve.EntityBase;
using Approve.Common;
using System.Data.SqlClient;
using Approve.RuleCenter;
using ProjectData;
using Approve.Common;

public partial class KC_AppMain_FileUp : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Attributes["onclick"] = "return check();";
            t_YWBM.Value = Request["FAppId"].ToString();
            t_type.Value = Request["type"].ToString();
            ShowFileType();
            if (Request["see"] != null && Request["see"] == "1")
            {
                btnSave.Visible = false; btnNext.Visible = false; btnDel.Visible = false;
                tr1.Attributes.Add("style", "display:none;"); tr2.Attributes.Add("style", "display:none;");
                DG_List.Columns[0].Visible = false;
            }
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
        }
    }
    private void ShowFileType()
    {
        switch (t_type.Value)
        {
            case "1000":
                lType.Text = "工法内容材料";
                break;
            case "1001":
                lType.Text = "完成单位意见、无争议声明相关附件";
                break;
            case "1002":
                lType.Text = "工程应用证明相关附件 ";
                break;
            case "1003":
                lType.Text = "工法成熟可靠性说明文件";
                break;
            case "1004":
                lType.Text = "科技成果获奖证明相关附件";
                break;
            case "1005":
                lType.Text = "经济效益证明";
                break;
            case "1006":
                lType.Text = "关键技术评估意见";
                break;
            case "1007":
                lType.Text = "专业技术专利证明文件";
                break;
            case "1008":
                lType.Text = "工法操作要点照片";
                break;
            case "1009":
                lType.Text = "技术转让的证明材料";
                break;
        }
        string sql = string.Format(@"select f.FFileName,f.FFilePath,f.FID from YW_GF_FileList l 
                     inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=" + t_type.Value);

        DataTable dt = rc.GetTable(sql);
        DG_List.DataSource = dt;
        DG_List.DataBind();
    }
    //保存
    private void saveInfo()
    {
        string FAppId = t_YWBM.Value;
        pageTool tool = new pageTool(this.Page);
        if (string.IsNullOrEmpty(t_FUrl.Value))
        {
            tool.showMessage("请选择文件");
            return;
        }
        if (string.IsNullOrEmpty(t_FName.Text))
        {
            tool.showMessage("请填写附件名称");
            t_FName.Focus();
            return;
        }

        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = new SortedList();
        if (ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", ViewState["FID"]);
        }
        else
        {
            sl.Add("FID", Guid.NewGuid());
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FIsdeleted", 0);
        }
        sl.Add("FPrjFileId", Request.QueryString["FPrjFileId"]);
        sl.Add("FAppId", FAppId);//业务ID
        sl.Add("FFileName", t_FName.Text);//文件名
        sl.Add("FFilePath", t_FUrl.Value);//文件地址
        sl.Add("FSize", t_FFileSize.Value);//文件大小
        sl.Add("FFileType", t_FFileType.Value);//文件类型  
        if (rc.SaveEBase("CF_AppPrj_FileOther", sl, "FID", so))
        {
            string sql = string.Format(@"insert YW_GF_FileList (FID,FAppid,FType,FFileID,FTypeName)
                                values (newid(),'" + t_YWBM.Value + "'," + t_type.Value + ",'" + sl["FID"].ToString()
                                                   + "','" + lType.Text + "')");
            rc.PExcute(sql);
            ViewState["FID"] = sl["FID"].ToString();
            tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
            ShowFileType();
            ViewState["FID"] = null;
            t_FName.Text = "";//文件名
            t_FUrl.Value = "";//文件地址
            t_FFileSize.Value = "";//文件大小
            t_FFileType.Value = "";//文件类型
            name.Text = "";
        }
        else
        {
            tool.showMessage("保存失败，请重试");
        }
    }


    //保存按钮 
    protected void btnSave_Click(object sender, EventArgs e)
    {

        RegisterStartupScript("key", "<script>document.getElementById('btnSave').disabled = true;</script>");
        saveInfo();
    }

    //选择文件后返回刷新
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        //附件url
        string str = t_FUrl.Value;
        //文件大小单位
        string n = "Byte";
        //文件大小
        float s = EConvert.ToInt(t_FFileSize.Value);
        if (s > 1024) { s = s / 1024; n = "KB"; }
        if (s > 1024) { s = s / 1024; n = "MB"; }
        //附件类型
        t_FFileType.Value = str.Split('.').ToList().Last();
        //附件文件名
        string m = str.Split('/').ToList().Last();
        name.Text = m + "（大小：" + s.ToString("0.0") + n + "）";
        //if (string.IsNullOrEmpty(t_FName.Text))
        //{
        t_FName.Text = m.Replace("." + m.Split('.').ToList().Last(), "");
        //}
    }

    //添加下一个
    protected void btnNext_Click(object sender, EventArgs e)
    {

        ViewState["FID"] = null;
        t_FName.Text = "";//文件名
        t_FUrl.Value = "";//文件地址
        t_FFileSize.Value = "";//文件大小
        t_FFileType.Value = "";//文件类型
        name.Text = "";
    }
    //删除附件
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        sl.Add("YW_GF_FileList", "FFileID");
        sl.Add("CF_AppPrj_FileOther", "FID");
        tool.DelInfoFromGrid(this.DG_List, sl, "dbShare"); ShowFileType();
        tool.showMessageAndRunFunction("删除成功", "window.returnValue=1;");
    }
    //附件列表
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string Fname = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFileName"));
            string path = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFilePath"));
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1).ToString();
            e.Item.Cells[3].Text = "<a href='" + path + "' target=\"_blank\" title=\"" + t_type.Value + ":" + Fname + "\" >查看</a>";
        }
    }

    public void readOnly()
    {
        btnSave.Enabled = false; btnNext.Enabled = false;
        btnDel.Enabled = false; btnSelect.Attributes.Add("disabled", "disabled");
    }


}
