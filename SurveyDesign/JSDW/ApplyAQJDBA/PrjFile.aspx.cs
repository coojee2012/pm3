using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Tools;
using System.Data;
using EgovaDAO;

public partial class JSDW_ApplyAQJDBA_PrjFile : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    string n = "Byte";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
             if (string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr1();</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "showTr2", "<script>showTr2();</script>");
                TC_AJBA_PrjFile emp = dbContext.TC_AJBA_PrjFile.Where(t => t.FId == Convert.ToString(Request.QueryString["fid"])).FirstOrDefault();
                if (emp != null)
                {
                    pageTool tool = new pageTool(this.Page,"t_");
                    tool.fillPageControl(emp);
                    string str = t_FFilePath.Value;
                    //文件大小
                    float s = EConvert.ToInt(t_FSize.Value);
                    if (s > 1024) { s = s / 1024; n = "KB"; }
                    if (s > 1024) { s = s / 1024; n = "MB"; }
                    //附件文件名
                    string m = str.Split('/').ToList().Last();
                    name1.Text = m + "（大小：" + s.ToString("0.0") + n + "）" +"(上报时间："+emp.FCreateTime+")";
                    //fileName.Text = "<u><a href='" + emp.FFilePath + "' target='_blank' title='点击查看该文件'>" + emp.FFileName + "." + emp.FFileType + "</a></u>";
                    fileName.Text = "<u><a href='" + emp.FFilePath + "' target='_blank' title='点击查看该文件'>" + m +"</a></u>";
                }
                ViewState["FID"] = Request.QueryString["fid"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["fAppId"]))
            {
                TC_AJBA_Record aj = dbContext.TC_AJBA_Record.Where(t => t.FAppId == Request.QueryString["fAppId"]).FirstOrDefault();
                ViewState["FAppId"] = aj.FAppId;
                ViewState["FPrjItemId"] = aj.FPrjItemId;
            }
            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool1.ExecuteScript("btnEnable();");
            }
            
        }else{
            
        }
       
    }

    //保存
    private void saveInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        string fId = EConvert.ToString(ViewState["FID"]);
        
        TC_AJBA_PrjFile Emp = new TC_AJBA_PrjFile();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_AJBA_PrjFile.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FPrjItemId = EConvert.ToString(ViewState["FPrjItemId"]);
            Emp.FAppId = EConvert.ToString(ViewState["FAppId"]);
            dbContext.TC_AJBA_PrjFile.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        if (string.IsNullOrEmpty(t_FFilePath.Value) && string.IsNullOrEmpty(fileName.Text.Trim()))
        {
            tool.showMessage("请选择文件");
            return;
        }
        if (string.IsNullOrEmpty(t_FFileName.Text) && string.IsNullOrEmpty(fileName.Text.Trim()))
        {
            tool.showMessage("请填写附件名称");
            t_FFileName.Focus();
            return;
        }
        if (n == "MB" && EConvert.ToInt(t_FSize.Value) > 20)
        {
            tool.showMessage("上传文件不能大于20M");
            return;
        }
        //Emp = tool.getPageValue(Emp);        
        Emp.FFileName = t_FFileName.Text; 
              
        Emp.FRemarks = t_FRemarks.Text;
        Emp.FSize = EConvert.ToInt(t_FSize.Value);

        Emp.FFileType = t_FFileType.Value;
        Emp.FCreateTime = DateTime.Now;
        Emp.FFilePath = t_FFilePath.Value;
        dbContext.SubmitChanges();
        ViewState["FID"] = fId;
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    //保存按钮 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //RegisterStartupScript("key", "<script>document.getElementById('btnSave').disabled = true;</script>");
        saveInfo();
    }
    //选择文件后返回刷新
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        //附件url
        string str = t_FFilePath.Value;
        //文件大小
        float s = EConvert.ToInt(t_FSize.Value);
        if (s > 1024) { s = s / 1024; n = "KB"; }
        if (s > 1024) { s = s / 1024; n = "MB"; }
        //附件类型
        t_FFileType.Value = str.Split('.').ToList().Last();
        //附件文件名
        string m = str.Split('/').ToList().Last();
        name.Text = m + "（大小：" + s.ToString("0.0") + n + "）";
        if (string.IsNullOrEmpty(t_FFileName.Text))
        {
          //  t_FFileName.Text = m.Replace("." + m.Split('.').ToList().Last(), "");
        }
    }

    
}