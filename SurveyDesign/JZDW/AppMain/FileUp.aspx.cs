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


public partial class JZDW_AppMain_FileUp : System.Web.UI.Page
{
    RCenter rq = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Attributes["onclick"] = "return check();";
            ShowFileType();
        }
    }
    private void ShowFileType()
    {
        ProjectDB db = new ProjectDB();

        string FPrjFileId = Request.QueryString["FPrjFileId"];
        lType.Text = db.getSysFileName(FPrjFileId);

        if (string.IsNullOrEmpty(lType.Text))
        {
            lType.Text = "附件";
        }

    }
    //保存
    private void saveInfo()
    {
        string FAppId = EConvert.ToString(Session["FAppId"]);
        if (!string.IsNullOrEmpty(Request.QueryString["FAppId"]))
        {
            FAppId = Request.QueryString["FAppId"];
        }


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
        sl.Add("FUserId", Request.QueryString["FUserId"]);//上传人（如果有的话。）
        if (rq.SaveEBase("CF_AppPrj_FileOther", sl, "FID", so))
        {
            ViewState["FID"] = sl["FID"].ToString();
            tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
        }
        else
        {
            tool.showMessage("保存失败，请重试");
        }
    }


    //保存按钮 
    protected void btnSave_Click(object sender, EventArgs e)
    {
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
        if (string.IsNullOrEmpty(t_FName.Text))
        {
            t_FName.Text = m.Replace("." + m.Split('.').ToList().Last(), "");
        }
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
}
