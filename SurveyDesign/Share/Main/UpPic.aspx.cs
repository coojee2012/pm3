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
using Approve.EntitySys;
using Seaskyer.Strings;
using Seaskyer.WebApp.Utility;
public partial class Share_main_UpPic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            showInfo();
        }
    }

    private void showInfo()
    {
        string url = Request.QueryString["imgUrl"];
        if (!string.IsNullOrEmpty(url))
        {
            img_EmpPic.Src = url;
        }
    }

    private void UpInfo()
    {
        pageTool tool = new pageTool(this.Page);
        if (string.IsNullOrEmpty(Request.QueryString["fid"]))
        {
            return;
        }


        //	单文件上传
        HttpPostedFile upFile = Request.Files[0];
        string fFileName = upFile.FileName;
        fFileName = fFileName.ToUpper();
        if ((!fFileName.EndsWith("JPG")) && (!fFileName.EndsWith("GIF")))
        {
            tool.showMessage("请上传jpg或gif格式的图片");
            return;
        }
        string fileType = "gif,jpg";
        int fileSize = 200 * 1024;//200K大小


        string timePath = DateTime.Now.ToString("yyyyMMddHHmmss");
        string uploadSavePath = Function.GetRealPath("~/upload/" + Request.QueryString["fid"] + "/");
        string saveName = "";
        int itemp = fFileName.LastIndexOf("\\");
        if (itemp >= 0)
        {
            saveName = timePath + fFileName.Substring(itemp + 1);
        }
        else
        {
            saveName = timePath + fFileName;
        }
        saveName = saveName.Replace("-", "");
        string fReturnPath = "../../upload/" + Request.QueryString["fid"] + "/";
        string[] uploadInfo = WebUtility.UploadFile(upFile, fileType, fileSize, false, saveName, uploadSavePath);
        if (uploadInfo[4] == "成功")
        {
            string url = fReturnPath + uploadInfo[1];
            tool.ExecuteScript("alert('上传成功');window.returnValue='" + url + "'; window.close();");
        }
        else
        {
            tool.showMessage(uploadInfo[4]);
        }
    }

    protected void btnUp_Click(object sender, EventArgs e)
    {
        UpInfo();
    }
}
