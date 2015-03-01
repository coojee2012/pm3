using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Seaskyer.Strings;
using Seaskyer.WebApp.Utility;
using Approve.Common;
using System.Web.UI;
public partial class Common_Upload : System.Web.UI.UserControl
{
    private string _sSaveCatalog = "";
    private string _sSavePath = "";
    private string _sParentControlId = "";
    int _fileSize = 1000 * 1024;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (this.ViewState["SaveCatalog"] == null)
            {
                this.ViewState["SaveCatalog"] = _sSaveCatalog;
            }
            if (this.ViewState["sParentControlId"] == null)
            {
                this.ViewState["sParentControlId"] = _sParentControlId;
            }
        }
    }
    public string sSaveCatalog
    {
        get
        {
            if (this.ViewState["SaveCatalog"] != null)
            {
                return this.ViewState["SaveCatalog"].ToString();
            }
            else
            {
                return _sSaveCatalog;
            }
        }
        set 
        { 
            this._sSaveCatalog = value;
        }
    }
    public string sParentControlId
    {
        get
        {
            if (this.ViewState["sParentControlId"] != null)
            {
                return this.ViewState["sParentControlId"].ToString();
            }
            else
            {
                return _sParentControlId;
            }
        }
        set 
        { 
            this._sParentControlId = value;
        }
    }
       public int fileSize
    {
        get { return this._fileSize; }
        set { this._fileSize = value; }
    }
    private void UpInfo()
    {

        pageTool tool = new pageTool(this.Page);
        //	单文件上传
        HttpPostedFile upFile = Request.Files[0];
        string fileType = "gif,jpg";
        

        string timePath = String.Format("{0}/{1}/", DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH"));


        string uploadSavePath = Function.GetRealPath("~/upload");
        string userFilePath = String.Format("{1}/{0}",timePath, sSaveCatalog);
        string savePath = uploadSavePath + userFilePath;
        //string	returnPath			=System.Configuration.ConfigurationSettings.AppSettings["BootUrl"]+ "/upload/" + userFilePath;
        string returnPath = "../../upload/" + userFilePath;


        string[] uploadInfo = WebUtility.UploadFile(upFile, fileType, fileSize, false, savePath);
        if (uploadInfo[4] == "成功")
        {
            tool.showMessage("上传成功");
             
            Control con = Parent.FindControl(sParentControlId);
            if (con is System.Web.UI.HtmlControls.HtmlInputHidden)
            {
                ((HtmlInputHidden)con).Value = returnPath + uploadInfo[1];
            }
            if (con is System.Web.UI.WebControls.TextBox)
            {
                ((TextBox)con).Text = returnPath + uploadInfo[1] ;
            }
            if (con is System.Web.UI.WebControls.Image)
            {
                ((System.Web.UI.WebControls.Image)con).ImageUrl = returnPath + uploadInfo[1];
            } 
        } 
    }
    protected void btnUp_Click(object sender, EventArgs e)
    {
        UpInfo();
    }
}
