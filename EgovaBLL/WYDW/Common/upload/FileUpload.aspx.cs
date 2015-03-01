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
using System.IO;

public partial class PropertyEntApp_Common_FileUpload : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUp_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        string sCode = Guid.NewGuid().ToString();
        string[] strArray = SecurityEncryption.DesDecrypt(Request.QueryString["p"], "12345687").Split('|');
        if (strArray.Length == 2)
        {
        }
        else
        {
            Response.Write("您没有权限访问该页");
            Response.End();
            return;
        }
        string isframe = "" + Request["frame"];
        if (EConvert.ToInt(strArray[1]) > SecurityEncryption.ConvertDateTimeInt(DateTime.Now))
        {

        }
        else
        {
            Response.Clear();
            Response.Write("链接已失效");
            Response.End();
            return;
        }
        string FUserId = strArray[0];
        StringBuilder sb = new StringBuilder();
        sb.Append("select FID from cf_sys_user where FID=@FID ");
        sb.Append("union ");
        sb.Append("select FID from CF_Emp_BaseInfo where FID=@FID ");
        Approve.RuleCenter.RCenter rc = new Approve.RuleCenter.RCenter();
        System.Data.DataTable dt = rc.GetTable(sb.ToString(), new System.Data.SqlClient.SqlParameter("@FID", FUserId));
        if (dt != null && dt.Rows.Count > 0)
        {
        }
        else
        {
            Response.Write("请登录");
            Response.End();
            return;
        }

        string fReturnPath = "upload/" + FUserId + "/";
        //	单文件上传
        HttpPostedFile upFile = Request.Files[0];
        string fFileName = Path.GetFileName(upFile.FileName);
        fFileName = fFileName.ToUpper();

        //if ((!fFileName.EndsWith("JPG")) && (!fFileName.EndsWith("PDF")))
        //{
        //    tool.showMessage("请上传JPG格式的图片或者PDF格式的文件");
        //    this.txtTitle.Value = "";
        //    this.btnUp.Enabled = true;
        //    this.btnUp.Text = "上传";
        //    return;
        //}
        int fileSize = upFile.ContentLength;
        //string fileType = "jpg,gif,png,pdf";
        string fileType = "";

        string timePath = DateTime.Now.ToString("yyyyMMddHHmmss");
        string uploadSavePath = Function.GetRealPath("~/upload/" + FUserId + "/");
        string saveName = "";
        saveName = timePath;

        if (!OnCheckFileName(ref saveName, this)) { return; }

        string[] uploadInfo = WebUtility.UploadFile(upFile, fileType, 1024 * 1024 * 5, false, saveName, uploadSavePath);
        if (uploadInfo[4] == "成功")
        {
            string fTypeId=Request.QueryString["FTypeID"];
            string fAppId=Request.QueryString["FAppID"];
            string isChangeFile = Request.QueryString["IsChangeFile"];

            SortedList sl = new SortedList();
            SaveOptionEnum so = SaveOptionEnum.Insert;
            string strFID = Guid.NewGuid().ToString();
            DataTable dtOldFile = dtOldFile = rc.GetTable(string.Format("select FID From WY_FileList Where FTypeid = '{0}' And FAppID = '{1}'  Order By FTime Desc", fTypeId, fAppId)); 
            if (isChangeFile == "1" && dtOldFile.Rows.Count == 1)
            {
                so = SaveOptionEnum.Update;
                sl.Add("FID", dtOldFile.Rows[0]["FID"].ToString());
            }
            else 
            {
                so = SaveOptionEnum.Insert;
                sl.Add("FID", strFID);
                sl.Add("FAppID", fAppId);
                sl.Add("FTypeid", fTypeId);
                //人员信息获取的flinkid
                if (fTypeId == "3001" || fTypeId == "3002" || fTypeId == "3003" || fTypeId == "3004")
                {
                    sl.Add("FLinkid", getSessionRYID());
                }
                else if(fTypeId=="1001")
                {
                    sl.Add("FLinkid", rc.GetSignValue("select FID from YW_WY_XM_HTBA where FAppID='" + fAppId + "'"));//YW_WY_XM_HTBA FID号
                }
            }
            //sl.Add("OldFileName", fFileName);
            //sl.Add("PID", Request.QueryString["PID"]);
            
            sl.Add("FFileName",Path.GetFileNameWithoutExtension(uploadInfo[1]));
            sl.Add("FFilePath", fReturnPath + uploadInfo[1]);
            //获取上传文件的uri
            string absUri=Request.Url.AbsoluteUri;
            absUri=removePath(absUri,4);
            sl.Add("FFileUrl",  absUri+ "/upload/" + FUserId +"/"+ uploadInfo[1]);
            sl.Add("FSize", fileSize);
            sl.Add("FFileType", Path.GetExtension(uploadInfo[1]).Remove(0,1));
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FIsDeleted", 0);

            if (rc.SaveEBase("WY_FileList", sl, "FID", so) && isChangeFile == "1")
            {
                try 
                {
                    File.Delete(Request.PhysicalApplicationPath+dtOldFile.Rows[0]["FFilePath"].ToString());
                }
                catch { }
            }
            string winReturnValue = "OK|"+fTypeId+"|" + fReturnPath + uploadInfo[1] + "";
            ClientScript.RegisterStartupScript(this.GetType(), "js", "<script> window.returnValue='"+winReturnValue+"';window.close()</script>");//returnValue返回Request的物理路径js会出错
        }
        else
        {
            tool.showMessage(uploadInfo[4]);
        }
    }

    protected virtual bool OnCheckFileName(ref string fFileName, Page CurPage)
    {
        if (IsPostBack)
        {
            if (fFileName.IndexOf(" ") >= 0)
            {
                pageTool tool = new pageTool(CurPage);
                tool.showMessage("文件名中不能包含空格!");
                return false;
            }
        }
        return true;
    }

    #region [人员id的session读取]
    public string getSessionRYID()
    {
        string result = "";
        if (Session["RYID"] == null)
        {
            result =Guid.NewGuid().ToString();
            Session["RYID"] = result;
        }
        else
        {
            result = Session["RYID"].ToString();
        }
        return result;
    }
    #endregion

    #region [依次去掉路径尾部的/后的内容]
    public string removePath(string varPath,int count)
    {
        for (int i = 0; i < count; i++)
        {
            varPath = varPath.Remove(varPath.LastIndexOf("/"));
        }
        return varPath;
    }
    #endregion 
}