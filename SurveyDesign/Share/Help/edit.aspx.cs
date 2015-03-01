using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;

public partial class Share_Help_edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确定要删除吗？');");
            btnSave.Attributes.Add("onclick", "return CheckInfo();");
            conBind();
            showInfo();
        }
    }

    //绑定默认
    private void conBind()
    {
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(Request.QueryString["FLinkNumber"]))
        {
            t_FLinkNumber.Text = Request.QueryString["FLinkNumber"];
            t_FLinkNumber.ReadOnly = true;
        }

        if (!string.IsNullOrEmpty(Request.QueryString["FID"]))
        {
            ViewState["FID"] = Request.QueryString["FID"];
        }

        //设置上传图片路径
        t_FContent.UploadPath = tool.getHttpUrl();
    }

    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
        ProjectDB db = new ProjectDB();
        CF_Sys_HelpMsg v = db.CF_Sys_HelpMsg.Where(t => (t.FID == EConvert.ToString(ViewState["FID"]) || t.FLinkNumber == Request.QueryString["FLinkNumber"])).FirstOrDefault();
        if (v != null)
        {
            ViewState["FID"] = v.FID;
            tool.fillPageControl(v);
            t_FContent.Text = v.FContent;

            btnDel.Visible = true;
        }
        else
        {
            btnDel.Visible = false;
        }
    }

    //保存
    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        ProjectDB db = new ProjectDB();

        //验证栏目编码
        if (db.CF_Sys_HelpMsg.Count(t => t.FID != EConvert.ToString(ViewState["FID"]) && t.FLinkNumber == t_FLinkNumber.Text) > 0)
        {
            tool.showMessage("该栏目编码已经保存过帮助信息。请注意核查！");
            return;
        }

        CF_Sys_HelpMsg v = db.CF_Sys_HelpMsg.Where(t => t.FID == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
        if (v == null)
        {
            v = new CF_Sys_HelpMsg();
            v.FID = Guid.NewGuid().ToString();
            v.FType = "0";
            db.CF_Sys_HelpMsg.InsertOnSubmit(v);
        }
        v.FTime = DateTime.Now;
        v.FTitle = t_FTitle.Text;
        v.FLinkNumber = t_FLinkNumber.Text;
        v.FContent = t_FContent.Text;

        db.SubmitChanges();
        ViewState["FID"] = v.FID;
        tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
        showInfo();
    }


    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }

    //删除按钮
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        ProjectDB db = new ProjectDB();
        CF_Sys_HelpMsg v = db.CF_Sys_HelpMsg.Where(t => t.FID == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
        if (v != null)
        {
            db.CF_Sys_HelpMsg.DeleteOnSubmit(v);
            db.SubmitChanges();
            tool.showMessageAndRunFunction("删除成功", "window.returnValue=1;window.close();");
        }
        else
        {
            tool.showMessage("删除失败，信息不存在");
            showInfo();
        }
    }
}
