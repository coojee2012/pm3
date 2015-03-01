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
using EgovaDAO;


public partial class JSDW_ApplyZLJDBA_FileUp : System.Web.UI.Page
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
        EgovaDB dbContext = new EgovaDB();

        lType.Text = Request.QueryString["FFileName"];

        if (string.IsNullOrEmpty(lType.Text))
        {
            lType.Text = "附件";
        }

    }
    //保存
    private void saveInfo()
    {
        EgovaDB dbContext = new EgovaDB();
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
        TC_QA_File Emp = new TC_QA_File();
        if (ViewState["FID"] != null)
        {
            Emp.FId = EConvert.ToString(ViewState["FID"]);
        }
        else
        {
            Emp.FId = Guid.NewGuid().ToString();
            Emp.FCreateTime = DateTime.Now;
            Emp.FIsDeleted = false;
        }
        Emp.FAppId = FAppId;//业务ID
        Emp.FFileName = t_FName.Text;//文件名
        Emp.FFilePath = t_FUrl.Value;//文件地址
        Emp.FSize = double.Parse(t_FFileSize.Value);//文件大小
        Emp.FFileType = t_FFileType.Value;//文件类型  
        Emp.FMaterialTypeId = Request.QueryString["FMaterialTypeId"];
        dbContext.TC_QA_File.InsertOnSubmit(Emp);
        dbContext.SubmitChanges();
        tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
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
