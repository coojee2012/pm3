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
using System.Data.SqlClient;
public partial class PersonApp_CommonPager_UpLoadPrjPic : UpLoadBasePage
{
    RCenter rc = new RCenter();
    protected override void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);

        }
    }
 

    private void UpInfo()
    {
        pageTool tool = new pageTool(this.Page);
        string entCode =EConvert.ToString( Session["DFUserId"]);

        //	单文件上传
        HttpPostedFile upFile = Request.Files[0];
        string fFileName = HttpUtility.UrlDecode(upFile.FileName, Encoding.Default);
        fFileName = fFileName.ToUpper();
        if ((!fFileName.EndsWith("JPG")) && (!fFileName.EndsWith("GIF")) && (!fFileName.EndsWith("DOC")))
        {
            tool.showMessage("请上传jpg或gif格式的图片,或word文档");
            return;
        }
        fFileName = upFile.FileName.Substring(upFile.FileName.LastIndexOf("\\") + 1);
        fFileName = fFileName.Substring(0, fFileName.LastIndexOf("."));
        string fileType = "gif,jpg,doc";
        int fileSize = 10000 * 1024;//900K

        string timePath = DateTime.Now.ToString("yyyyMMddHHmmss");
        string uploadSavePath = Function.GetRealPath("~/upload/" + entCode + "/");
        string saveName = timePath + fFileName;
        string fReturnPath = "../../upload/" + entCode + "/";

        string[] uploadInfo = WebUtility.UploadFile(upFile, fileType, fileSize, false, saveName, uploadSavePath);
        if (uploadInfo[4] == "成功")
        {

            SortedList sl = new SortedList();

            SaveOptionEnum so = SaveOptionEnum.Insert;
            string fid = string.Empty;
            if (fid != null && fid != "")
            {
                so = SaveOptionEnum.Update;
                sl.Add("FID", fid);
            }
            else
            {
                sl.Add("FID", Guid.NewGuid().ToString());

                sl.Add("FLinkId", Request.QueryString["FLinkId"]);
                sl.Add("FIsDeleted", 0);
                sl.Add("FCreateTime", DateTime.Now);
                sl.Add("FType", "Approval");
            }
            sl.Add("FContent", fReturnPath + uploadInfo[1]);
            rc.SaveEBase(EntityTypeEnum.EPText, sl, "FID", so);
            img_EmpPic.Src = ComFunction.FileServer(fReturnPath + uploadInfo[1]);
            tool.showMessageAndRunFunction("上传成功", "window.returnValue='1';");
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
