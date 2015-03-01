<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" %>

<%@ Import Namespace="System.Security.Cryptography" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.IO" %>

<script runat="server">

    // Messages
    private string NoFileMessage = "您没有选择文件。";
    private string UploadSuccessMessage = "上传成功";
    private string UploadFailureMessage = "上传失败。";
    private string NoImagesMessage = "该文件夹不存在或者是空的";
    private string NoFolderSpecifiedMessage = "您要上传到的文件夹不存在。";
    private string NoFileToDeleteMessage = "您没有选中要删除的文件。";
    private string InvalidFileTypeMessage = "您无法上传这种类型的文件。";
    private string[] AcceptedFileTypes = new string[] { "只允许上传", "jpg", "jpeg", "jpe", "gif", "png", "bmp", "txt", "doc", "docx", "xls", "rar", "zip", "pdf", "jxm", "dwg" };

    // Configuration
    private bool UploadIsEnabled = true;         // 是否允许上传文件
    private bool DeleteIsEnabled = true;         // 是否允许删除文件
    private string DefaultImageFolder = "images";  // 默认的起始文件夹
    private string UrlHead = "";
    private void Page_Load(object sender, System.EventArgs e)
    {
        string[] strArray = SecurityEncryption.DesDecrypt(Request.QueryString["p"], "12345687").Split('|');
        if (strArray.Length == 3)
        {
        }
        else
        {
            Response.Write("您没有权限访问该页");
            Response.End();
            return;
        }
        string isframe = "" + Request["frame"];
        if (EConvert.ToInt(strArray[2]) > SecurityEncryption.ConvertDateTimeInt(DateTime.Now))
        {

        }
        else
        {
            Response.Clear();
            Response.Write("链接已失效");
            Response.End();
            return;
        }
        string FUserId = strArray[1];
        StringBuilder sb = new StringBuilder();
        sb.Append("select FID from cf_sys_user where FID=@FID ");
        sb.Append("union ");
        sb.Append("select FID from CF_Emp_BaseInfo where FID=@FID ");
        Approve.RuleCenter.RCenter rc = new Approve.RuleCenter.RCenter();
        System.Data.DataTable dt = rc.GetTable(sb.ToString(), new System.Data.SqlClient.SqlParameter("@FID", FUserId));
        if (dt != null && dt.Rows.Count > 0)
        {
            UrlHead = strArray[0];
        }
        else
        {
            Response.Write("请登录");
            Response.End();
            return;
        }


        DefaultImageFolder = "upload/" + FUserId;
        if (isframe != "")
        {
            MainPage.Visible = true;
            iframePanel.Visible = false;

            string rif = "" + Request["rif"];
            string cif = "" + Request["cif"];

            if (cif != "" && rif != "")
            {
                RootImagesFolder.Value = rif;
                CurrentImagesFolder.Value = cif;
            }
            else
            {

                System.IO.Directory.CreateDirectory(Server.MapPath("~/" + DefaultImageFolder));

                RootImagesFolder.Value = DefaultImageFolder;
                //if (!System.IO.Directory.Exists(Server.MapPath("~/" + DefaultImageFolder + "/" + DateTime.Now.ToString("yyyyMMdd"))))
                //{
                //    System.IO.Directory.CreateDirectory(Server.MapPath("~/" + DefaultImageFolder + "/" + DateTime.Now.ToString("yyyyMMdd")));     
                //}
                if (System.IO.Directory.Exists(Server.MapPath("~/" + DefaultImageFolder + "/" + DateTime.Now.ToString("yyyyMMdd")))
                    && System.IO.Directory.GetFiles(Server.MapPath("~/" + DefaultImageFolder + "/" + DateTime.Now.ToString("yyyyMMdd"))).Length > 0
                    )
                {
                    CurrentImagesFolder.Value = DefaultImageFolder + "\\" + DateTime.Now.ToString("yyyyMMdd");
                }
                else
                {
                    CurrentImagesFolder.Value = DefaultImageFolder;
                }
            }

            UploadPanel.Visible = UploadIsEnabled;
            DeleteImage.Visible = DeleteIsEnabled;

            string FileErrorMessage = "";
            string ValidationString = "(?i)^.+\\.(";
            //[\.jpg]|[\.jpeg]|[\.jpe]|[\.gif]|[\.png])$"
            for (int i = 0; i < AcceptedFileTypes.Length; i++)
            {
                ValidationString += "(" + AcceptedFileTypes[i] + ")";
                if (i < (AcceptedFileTypes.Length - 1)) ValidationString += "|";
                FileErrorMessage += AcceptedFileTypes[i];
                if (i < (AcceptedFileTypes.Length - 1)) FileErrorMessage += ", ";
            }

            FileValidator.ValidationExpression = ValidationString + ")$";
            FileValidator.ErrorMessage = FileErrorMessage;

            if (!IsPostBack)
            {
                if (Request.QueryString["type"] == "image")
                {
                    DisplayImages();
                    cbWaterMark.Visible = true;
                }
                else
                {
                    DisplayFiles();
                    cbWaterMark.Visible = false;
                }
            }
        }
        else
        {

        }
    }

    public void UploadImage_OnClick(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (CurrentImagesFolder.Value != "")
            {
                if (UploadFile.PostedFile.FileName.Trim() != "")
                {
                    if (IsValidFileType(UploadFile.PostedFile.FileName))
                    {
                        if (UploadFile.PostedFile.ContentLength > 1000000)
                        {
                            ResultsMessage.Text = "上传的文件不能大于1M:";
                            return;
                        }

                        try
                        {
                            string UploadFileName = "";
                            string UploadFileDestination = "";
                            UploadFileName = UploadFile.PostedFile.FileName;
                            UploadFileName = UploadFileName.Substring(UploadFileName.LastIndexOf("\\") + 1);
                            UploadFileDestination = HttpContext.Current.Request.PhysicalApplicationPath;
                            if (!System.IO.Directory.Exists(Server.MapPath("~/" + DefaultImageFolder + "/" + DateTime.Now.ToString("yyyyMMdd"))))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/" + DefaultImageFolder + "/" + DateTime.Now.ToString("yyyyMMdd")));
                                CurrentImagesFolder.Value = DefaultImageFolder + "\\" + DateTime.Now.ToString("yyyyMMdd");

                            }

                            UploadFileDestination += CurrentImagesFolder.Value;
                            UploadFileDestination += "\\";
                            RuleAppCode.ImageWaterMark imageWaterMark = new RuleAppCode.ImageWaterMark();
                            if (cbWaterMark.Checked && (Path.GetExtension(UploadFile.PostedFile.FileName).ToLower() == ".jpg" || Path.GetExtension(UploadFile.PostedFile.FileName).ToLower() == ".gif"))
                            {
                                imageWaterMark.AddWaterMark(UploadFile.PostedFile.InputStream, UploadFileDestination + UploadFileName, RuleAppCode.WaterMarkType.ImageMark, Server.MapPath("~/image/logo-水印.gif"));
                            }
                            else
                            {
                                UploadFile.PostedFile.SaveAs(UploadFileDestination + UploadFileName);
                            }
                            ResultsMessage.Text = UploadSuccessMessage;
                            string imagesurl = UrlHead + CurrentImagesFolder.Value.Replace("\\", "/") + "/" + UploadFileName;
                            string res = Request.Url.ToString();
                            ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script>returnImage('" + imagesurl + "'," + UploadFile.PostedFile.ContentLength + ");</sc" + "ript>");
                        }
                        catch (Exception ex)
                        {
                            ResultsMessage.Text = "文件上传失败:";
                            ResultsMessage.Text = UploadFailureMessage + "<br />" + ex.Message; ;
                        }
                    }
                    else
                    {
                        ResultsMessage.Text = InvalidFileTypeMessage;
                    }
                }
                else
                {
                    ResultsMessage.Text = NoFileMessage;
                }
            }
            else
            {
                ResultsMessage.Text = NoFolderSpecifiedMessage;
            }
        }
        else
        {
            ResultsMessage.Text = InvalidFileTypeMessage;
        }
        if (Request.QueryString["type"] == "image")
        {
            DisplayImages();
            cbWaterMark.Visible = true;
        }
        else
        {
            DisplayFiles();
            cbWaterMark.Visible = false;
        }
    }

    public void DeleteImage_OnClick(object sender, EventArgs e)
    {
        if (FileToDelete.Value != "" && FileToDelete.Value != "undefined")
        {
            try
            {
                string AppPath = HttpContext.Current.Request.PhysicalApplicationPath;
                System.IO.File.Delete(AppPath + CurrentImagesFolder.Value + "\\" + FileToDelete.Value);
                ResultsMessage.Text = "已删除: " + FileToDelete.Value;
            }
            catch (Exception ex)
            {
                ResultsMessage.Text = "删除失败。";
            }
        }
        else
        {
            ResultsMessage.Text = NoFileToDeleteMessage;
        }
        if (Request.QueryString["type"] == "image")
        {
            DisplayImages();
            cbWaterMark.Visible = true;
        }
        else
        {
            DisplayFiles();
            cbWaterMark.Visible = false;
        }
    }

    private bool IsValidFileType(string FileName)
    {
        string ext = Path.GetExtension(FileName).Replace(".", "").ToLower();
        for (int i = 0; i < AcceptedFileTypes.Length; i++)
        {
            if (ext == AcceptedFileTypes[i])
            {
                return true;

            }
        }
        return false;
    }


    private string[] ReturnFilesArray()
    {
        if (CurrentImagesFolder.Value != "")
        {
            try
            {
                string AppPath = HttpContext.Current.Request.PhysicalApplicationPath;
                string ImageFolderPath = AppPath + CurrentImagesFolder.Value;
                string[] FilesArray = System.IO.Directory.GetFiles(ImageFolderPath, "*");
                return FilesArray;


            }
            catch
            {

                return null;
            }
        }
        else
        {
            return null;
        }

    }

    private string[] ReturnDirectoriesArray()
    {
        if (CurrentImagesFolder.Value != "")
        {
            try
            {
                string AppPath = HttpContext.Current.Request.PhysicalApplicationPath;
                string CurrentFolderPath = AppPath + CurrentImagesFolder.Value;
                string[] DirectoriesArray = System.IO.Directory.GetDirectories(CurrentFolderPath, "*");
                return DirectoriesArray;
            }
            catch
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
    private string DesDecrypt(string strText, string sDecrKey)
    {
        byte[] byKey = null;
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        byte[] inputByteArray = new Byte[strText.Length];
        try
        {
            byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetString(ms.ToArray());
        }
        catch (System.Exception error)
        {
            Debug.WriteLine(error.Message);
        }
        return "";
    }
    public void DisplayImages()
    {
        string[] FilesArray = ReturnFilesArray();
        string[] DirectoriesArray = ReturnDirectoriesArray();
        string AppPath = HttpContext.Current.Request.PhysicalApplicationPath;
        string AppUrl;

        //Get the application's URL
        if (Request.ApplicationPath == "/")
            AppUrl = Request.ApplicationPath;
        else
            AppUrl = Request.ApplicationPath + "/";

        GalleryPanel.Controls.Clear();
        if ((FilesArray == null || FilesArray.Length == 0) && (DirectoriesArray == null || DirectoriesArray.Length == 0))
        {
            //gallerymessage.Text = NoImagesMessage + ": " + CurrentImagesFolder.Value;            
        }
        lblPath.Text = CurrentImagesFolder.Value.Replace("\\", "/").Replace(RootImagesFolder.Value, "我的文档");



        string ImageFileName = "";
        string ImageFileLocation = "";

        int thumbWidth = 94;
        int thumbHeight = 94;

        if (CurrentImagesFolder.Value != RootImagesFolder.Value)
        {

            System.Web.UI.HtmlControls.HtmlImage myHtmlImage = new System.Web.UI.HtmlControls.HtmlImage();
            myHtmlImage.Src = "folder.up.gif";
            myHtmlImage.Attributes["unselectable"] = "on";
            myHtmlImage.Attributes["align"] = "absmiddle";
            myHtmlImage.Attributes["vspace"] = "36";

            string ParentFolder = CurrentImagesFolder.Value.Substring(0, CurrentImagesFolder.Value.LastIndexOf("\\"));

            System.Web.UI.WebControls.Panel myImageHolder = new System.Web.UI.WebControls.Panel();
            myImageHolder.CssClass = "imageholder";
            myImageHolder.Attributes["unselectable"] = "on";
            myImageHolder.Attributes["onclick"] = "divClick(this,'');";
            myImageHolder.Attributes["ondblclick"] = "gotoFolder('" + RootImagesFolder.Value + "','" + ParentFolder.Replace("\\", "\\\\") + "');";
            myImageHolder.Controls.Add(myHtmlImage);

            System.Web.UI.WebControls.Panel myMainHolder = new System.Web.UI.WebControls.Panel();
            myMainHolder.CssClass = "imagespacer";
            myMainHolder.Controls.Add(myImageHolder);

            System.Web.UI.WebControls.Panel myTitleHolder = new System.Web.UI.WebControls.Panel();
            myTitleHolder.CssClass = "titleHolder";
            myTitleHolder.Controls.Add(new LiteralControl("向上"));
            myMainHolder.Controls.Add(myTitleHolder);

            GalleryPanel.Controls.Add(myMainHolder);

        }

        foreach (string _Directory in DirectoriesArray)
        {

            try
            {
                string DirectoryName = _Directory.ToString();


                System.Web.UI.HtmlControls.HtmlImage myHtmlImage = new System.Web.UI.HtmlControls.HtmlImage();
                myHtmlImage.Src = "folder.big.gif";
                myHtmlImage.Attributes["unselectable"] = "on";
                myHtmlImage.Attributes["align"] = "absmiddle";
                myHtmlImage.Attributes["vspace"] = "29";

                System.Web.UI.WebControls.Panel myImageHolder = new System.Web.UI.WebControls.Panel();
                myImageHolder.CssClass = "imageholder";
                myImageHolder.Attributes["unselectable"] = "on";
                myImageHolder.Attributes["onclick"] = "divClick(this);";
                myImageHolder.Attributes["ondblclick"] = "gotoFolder('" + RootImagesFolder.Value + "','" + DirectoryName.Replace(AppPath, "").Replace("\\", "\\\\") + "');";
                myImageHolder.Controls.Add(myHtmlImage);

                System.Web.UI.WebControls.Panel myMainHolder = new System.Web.UI.WebControls.Panel();
                myMainHolder.CssClass = "imagespacer";
                myMainHolder.Controls.Add(myImageHolder);

                System.Web.UI.WebControls.Panel myTitleHolder = new System.Web.UI.WebControls.Panel();
                myTitleHolder.CssClass = "titleHolder";
                myTitleHolder.Controls.Add(new LiteralControl(DirectoryName.Replace(AppPath + CurrentImagesFolder.Value + "\\", "")));
                myMainHolder.Controls.Add(myTitleHolder);

                GalleryPanel.Controls.Add(myMainHolder);
            }
            catch
            {
                // nothing for error
            }
        }

        foreach (string ImageFile in FilesArray)
        {

            try
            {
                string strImageFile = ImageFile;
                ImageFileName = ImageFile.ToString();
                ImageFileName = ImageFileName.Substring(ImageFileName.LastIndexOf("\\") + 1);
                ImageFileLocation = AppUrl;
                ImageFileLocation = ImageFileLocation.Substring(ImageFileLocation.LastIndexOf("\\") + 1);


                //galleryfilelocation += "/";
                ImageFileLocation += CurrentImagesFolder.Value;
                ImageFileLocation += "/";
                ImageFileLocation += ImageFileName;
                System.Web.UI.HtmlControls.HtmlImage myHtmlImage = new System.Web.UI.HtmlControls.HtmlImage();
                myHtmlImage.Src = ImageFileLocation.Replace("\\", "/");
                myHtmlImage.Alt = Path.GetFileNameWithoutExtension(ImageFileLocation);
                //if ("jpg,jpeg,jpe,gif,png,bmp".IndexOf(Path.GetExtension(strImageFile)) ==-1)
                //{
                //    strImageFile =Server.MapPath( "doc.jpg");
                //    myHtmlImage.Src = "doc.jpg";
                //}
                System.Drawing.Image myImage = System.Drawing.Image.FromFile(strImageFile);
                myHtmlImage.Attributes["unselectable"] = "on";
                //myHtmlImage.border=0;

                // landscape image
                if (myImage.Width > myImage.Height)
                {
                    if (myImage.Width > thumbWidth)
                    {
                        myHtmlImage.Width = thumbWidth;
                        myHtmlImage.Height = Convert.ToInt32(myImage.Height * thumbWidth / myImage.Width);
                    }
                    else
                    {
                        myHtmlImage.Width = myImage.Width;
                        myHtmlImage.Height = myImage.Height;
                    }
                    // portrait image
                }
                else
                {
                    if (myImage.Height > thumbHeight)
                    {
                        myHtmlImage.Height = thumbHeight;
                        myHtmlImage.Width = Convert.ToInt32(myImage.Width * thumbHeight / myImage.Height);
                    }
                    else
                    {
                        myHtmlImage.Width = myImage.Width;
                        myHtmlImage.Height = myImage.Height;
                    }
                }

                if (myHtmlImage.Height < thumbHeight)
                {
                    myHtmlImage.Attributes["vspace"] = Convert.ToInt32((thumbHeight / 2) - (myHtmlImage.Height / 2)).ToString();
                }


                System.Web.UI.WebControls.Panel myImageHolder = new System.Web.UI.WebControls.Panel();
                myImageHolder.CssClass = "imageholder";
                string imagesurl = UrlHead + CurrentImagesFolder.Value.Replace("\\", "/") + "/" + ImageFileName;
                myImageHolder.Attributes["onclick"] = "divClick(this,'" + ImageFileLocation + "','" + imagesurl + "');";
                myImageHolder.Attributes["ondblclick"] = "returnImage('" + imagesurl
                + "','" + myImage.Width.ToString() + "','" + myImage.Height.ToString() + "');";
                myImageHolder.Controls.Add(myHtmlImage);


                System.Web.UI.WebControls.Panel myMainHolder = new System.Web.UI.WebControls.Panel();
                myMainHolder.CssClass = "imagespacer";
                myMainHolder.Controls.Add(myImageHolder);

                System.Web.UI.WebControls.Panel myTitleHolder = new System.Web.UI.WebControls.Panel();
                myTitleHolder.CssClass = "titleHolder";
                myTitleHolder.Controls.Add(new LiteralControl(ImageFileName + "<BR>" + myImage.Width.ToString() + "x" + myImage.Height.ToString()));
                myMainHolder.Controls.Add(myTitleHolder);

                //GalleryPanel.Controls.Add(myImage);
                GalleryPanel.Controls.Add(myMainHolder);

                myImage.Dispose();
            }
            catch
            {

            }

            //gallerymessage.Text = "";
        }
    }
    public void DisplayFolder()
    {
        string[] FilesArray = ReturnFilesArray();
        string[] DirectoriesArray = ReturnDirectoriesArray();
        string AppPath = HttpContext.Current.Request.PhysicalApplicationPath;
        string AppUrl;
        if (Request.ApplicationPath == "/")
            AppUrl = Request.ApplicationPath;
        else
            AppUrl = Request.ApplicationPath + "/";
        TreeView tvFolder = new TreeView();
        //tvFolder.ShowLines = true;
        tvFolder.ImageSet = TreeViewImageSet.XPFileExplorer;
        TreeNode topNode = new TreeNode("服务器");

        topNode.NavigateUrl = "javascript:void(0)";
        foreach (string _Directory in DirectoriesArray)
        {
            TreeNode newNode = new TreeNode();
            newNode.Text = _Directory.Replace(AppPath + CurrentImagesFolder.Value + "\\", "");

            topNode.ChildNodes.Add(newNode);
        }
        tvFolder.Nodes.Add(topNode);
        GalleryPanel.Controls.Clear();
        GalleryPanel.Controls.Add(tvFolder);
    }
    public void DisplayFiles()
    {
        string[] FilesArray = ReturnFilesArray();
        string[] DirectoriesArray = ReturnDirectoriesArray();
        string AppPath = HttpContext.Current.Request.PhysicalApplicationPath;
        string AppUrl;

        //Get the application's URL
        if (Request.ApplicationPath == "/")
            AppUrl = Request.ApplicationPath;
        else
            AppUrl = Request.ApplicationPath + "/";

        GalleryPanel.Controls.Clear();
        lblPath.Text = CurrentImagesFolder.Value.Replace("\\", "/").Replace(RootImagesFolder.Value, "我的文档");
        if ((FilesArray == null || FilesArray.Length == 0) && (DirectoriesArray == null || DirectoriesArray.Length == 0))
        {
            //gallerymessage.Text = NoImagesMessage + ": " + CurrentImagesFolder.Value;
            lblPath.Text += "：" + NoImagesMessage;
        }
        else
        {
        }

        string ImageFileName = "";
        string ImageFileLocation = "";

        Table table = new Table();
        if (CurrentImagesFolder.Value != RootImagesFolder.Value)
        {

            System.Web.UI.HtmlControls.HtmlImage myHtmlImage = new System.Web.UI.HtmlControls.HtmlImage();
            myHtmlImage.Src = "folder.up.gif";
            myHtmlImage.Attributes["unselectable"] = "on";
            myHtmlImage.Attributes["align"] = "absmiddle";
            myHtmlImage.Attributes["vspace"] = "36";

            string ParentFolder = CurrentImagesFolder.Value.Substring(0, CurrentImagesFolder.Value.LastIndexOf("\\"));

            System.Web.UI.WebControls.Panel myImageHolder = new System.Web.UI.WebControls.Panel();
            myImageHolder.CssClass = "fileholder";
            myImageHolder.Attributes["unselectable"] = "on";
            myImageHolder.Attributes["onclick"] = "divClick(this,'');";
            myImageHolder.Attributes["ondblclick"] = "gotoFolder('" + RootImagesFolder.Value + "','" + ParentFolder.Replace("\\", "\\\\") + "');";
            myImageHolder.Controls.Add(myHtmlImage);

            System.Web.UI.WebControls.Panel myMainHolder = new System.Web.UI.WebControls.Panel();
            myMainHolder.CssClass = "imagespacer";
            myMainHolder.Controls.Add(myImageHolder);

            System.Web.UI.WebControls.Panel myTitleHolder = new System.Web.UI.WebControls.Panel();
            myTitleHolder.CssClass = "titleHolder";
            myTitleHolder.Controls.Add(new LiteralControl("返回上一级目录"));
            myMainHolder.Controls.Add(myTitleHolder);


            //TableRow row = new TableRow();
            //Table cell= new TableCell();
            //cell.Controls(new LiteralControl("向上"));
            //row.Cells.Add(cell);
            //table.Rows.Add(row);
            GalleryPanel.Controls.Add(myMainHolder);

        }

        System.Web.UI.WebControls.Panel myList = new System.Web.UI.WebControls.Panel();
        myList.CssClass = "mid";
        foreach (string _Directory in DirectoriesArray)
        {

            try
            {
                string DirectoryName = _Directory.ToString();


                System.Web.UI.HtmlControls.HtmlImage myHtmlImage = new System.Web.UI.HtmlControls.HtmlImage();
                myHtmlImage.Src = "folder.big.gif";
                myHtmlImage.Attributes["unselectable"] = "on";
                myHtmlImage.Attributes["align"] = "absmiddle";
                //myHtmlImage.Attributes["vspace"] = "29";

                System.Web.UI.WebControls.Panel myImageHolder = new System.Web.UI.WebControls.Panel();
                myImageHolder.CssClass = "filesholder";
                myImageHolder.Attributes["unselectable"] = "on";

                myImageHolder.Attributes["ondblclick"] = "gotoFolder('" + RootImagesFolder.Value + "','" + DirectoryName.Replace(AppPath, "").Replace("\\", "\\\\") + "');";
                myImageHolder.Controls.Add(myHtmlImage);

                System.Web.UI.WebControls.Panel myMainHolder = new System.Web.UI.WebControls.Panel();
                myMainHolder.CssClass = "filespacer";
                myMainHolder.Attributes["onclick"] = "divClick(this);";
                myMainHolder.Attributes["ondblclick"] = "gotoFolder('" + RootImagesFolder.Value + "','" + DirectoryName.Replace(AppPath, "").Replace("\\", "\\\\") + "');";
                myMainHolder.Controls.Add(myImageHolder);

                System.Web.UI.WebControls.Panel myTitleHolder = new System.Web.UI.WebControls.Panel();
                myTitleHolder.CssClass = "filestitleholder";
                myTitleHolder.Controls.Add(new LiteralControl(DirectoryName.Replace(AppPath + CurrentImagesFolder.Value + "\\", "")));
                myMainHolder.Controls.Add(myTitleHolder);


                myList.Controls.Add(myMainHolder);
            }
            catch
            {
                // nothing for error
            }
        }

        foreach (string ImageFile in FilesArray)
        {

            try
            {
                string strImageFile = ImageFile;
                ImageFileName = ImageFile.ToString();
                ImageFileName = ImageFileName.Substring(ImageFileName.LastIndexOf("\\") + 1);
                ImageFileLocation = AppUrl;
                ImageFileLocation = ImageFileLocation.Substring(ImageFileLocation.LastIndexOf("\\") + 1);
                long size = (new FileInfo(ImageFile)).Length;

                //galleryfilelocation += "/";
                ImageFileLocation += CurrentImagesFolder.Value;
                ImageFileLocation += "/";
                ImageFileLocation += ImageFileName;
                System.Web.UI.HtmlControls.HtmlImage myHtmlImage = new System.Web.UI.HtmlControls.HtmlImage();
                switch (Path.GetExtension(ImageFile).ToLower())
                {
                    case ".xls":
                        myHtmlImage.Src = "xls.gif";
                        break;
                    case ".doc":
                    case ".docx":
                        myHtmlImage.Src = "doc.gif";
                        break;
                    case ".jpg":
                    case ".gif":
                        myHtmlImage.Src = "jpg.jpg";
                        break;
                    case ".pdf":
                        myHtmlImage.Src = "pdf.gif";
                        break;
                    case ".txt":
                        myHtmlImage.Src = "txt.gif";
                        break;
                    case ".rar":
                        myHtmlImage.Src = "rar.jpg";
                        break;
                    case ".zip":
                        myHtmlImage.Src = "zip.gif";
                        break;
                    default:
                        myHtmlImage.Src = "unknown.gif";
                        break;
                }

                myHtmlImage.Attributes["unselectable"] = "on";
                myHtmlImage.Attributes["align"] = "absmiddle";

                System.Web.UI.WebControls.Panel myImageHolder = new System.Web.UI.WebControls.Panel();
                myImageHolder.CssClass = "filesholder";
                string imagesurl = UrlHead + CurrentImagesFolder.Value.Replace("\\", "/") + "/" + ImageFileName;

                myImageHolder.Attributes["onclick"] = "divClick(this,'" + ImageFileLocation + "','" + imagesurl + "','" + size + "');";
                myImageHolder.Attributes["ondblclick"] = "returnImage('" + imagesurl
                + "','" + size + "');";
                myImageHolder.Controls.Add(myHtmlImage);


                System.Web.UI.WebControls.Panel myMainHolder = new System.Web.UI.WebControls.Panel();
                myMainHolder.CssClass = "filespacer";
                myMainHolder.Controls.Add(myImageHolder);
                myMainHolder.ToolTip = ImageFileName;
                string res = Request.Url.AbsoluteUri.Split('W')[0] + CurrentImagesFolder.Value.Replace("\\", "/") + "/" + ImageFileName;
                myMainHolder.Attributes["onclick"] = "divClick(this,'" + ImageFileLocation + "','" + imagesurl + "','" + size + "');";
                myMainHolder.Attributes["ondblclick"] = "returnImage('" + imagesurl
                  + "','" + size + "','" + res + "');";
                System.Web.UI.WebControls.Panel myTitleHolder = new System.Web.UI.WebControls.Panel();
                myTitleHolder.CssClass = "filestitleholder";
                myTitleHolder.Controls.Add(new LiteralControl(ImageFileName));
                myMainHolder.Controls.Add(myTitleHolder);

                //GalleryPanel.Controls.Add(myImage);
                System.Web.UI.WebControls.Panel selectHolder = new System.Web.UI.WebControls.Panel();
                HtmlAnchor htmlAnchor = new HtmlAnchor();
                htmlAnchor.HRef = "javascript:" + myMainHolder.Attributes["ondblclick"];
                htmlAnchor.InnerText = "选择";
                selectHolder.Controls.Add(htmlAnchor);
                myMainHolder.Controls.Add(selectHolder);

                myList.Controls.Add(myMainHolder);

            }
            catch
            {

            }

            //gallerymessage.Text = "";
        }

        GalleryPanel.Controls.Add(myList);
    }   
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文件管理</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <style type="text/css">
        /*上一级*/ div.imagespacer {
            width: 120px;
            height: 136px;
            text-align: center;
            float: left;
            font: 10pt verdana;
            margin: 0px 10px;
            overflow: hidden;
            cursor: pointer;
            border: 1px solid #CCCCCC;
        }

            div.imagespacer:hover {
                border: 2px solid #CCCCCC;
            }

        div.imageholder {
            margin: 1px auto;
            padding: 0px;
            border: 1px solid #CCCCCC;
            width: 100px;
            height: 100px;
        }

        div.titleholder {
            font-family: ms sans serif, arial;
            font-size: 8pt;
            width: 100px;
            text-overflow: ellipsis;
            overflow: hidden;
            white-space: nowrap;
        }

        div.filesholder {
            margin: 0px;
            padding: 0px 5px 0px 5px;
            width: 20px;
            height: 20px;
            float: left;
        }

            div.filesholder img {
                width: 16px;
                height: 16px;
                margin-top: 2px;
            }
        /*文件*/ div.filespacer {
            width: 320px;
            height: 20px;
            line-height: 20px;
            text-align: center;
            font: 10pt verdana;
            margin-bottom: 10px;
            overflow: hidden;
            border: 1px solid #CCCCCC;
            cursor: pointer;
        }

            div.filespacer:hover {
                border: 1px solid #FF0000;
            }

        div.filestitleholder {
            text-align: left;
            font-family: ms sans serif, arial;
            font-size: 12px;
            line-height: 20px;
            width: 250px;
            text-overflow: ellipsis;
            overflow: hidden;
            white-space: nowrap;
            float: left;
        }
        /*中间那个—*/ div.mid {
            float: left;
        }

        .m_btn_w2 {
        }
    </style>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
        lastDiv = null;
        function divClick(theDiv, filename, imagename, size) {
            if (lastDiv) {
                lastDiv.style.border = "1px solid #CCCCCC";
            }
            lastDiv = theDiv;
            theDiv.style.border = "2px solid #316AC5";
            if (request("type") == "image") {
            }
            else {
                if (request("iseditor") == 1) {
                    imagename = imagename + "|" + size
                }
            }
            if (imagename && imagename != "") {
                window.parent.name = imagename;
            }
            document.getElementById("FileToDelete").value = filename;

        }
        function gotoFolder(rootfolder, newfolder) {
            window.location = "filemanager.aspx?frame=1&p=" + request("p") + "&rif=" + rootfolder + "&cif=" + newfolder + "&type=" + request("type") + "&iseditor=" + request("iseditor");
        }
        function request(paras) {
            var url = unescape(location.href);
            var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
            var paraObj = {}
            for (i = 0; j = paraString[i]; i++) {
                paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
            }
            var returnValue = paraObj[paras.toLowerCase()];
            if (typeof (returnValue) == "undefined") {
                return "";
            }
            else {
                return returnValue;
            }
        }
        function returnImage(imagename, width, height) {
            var arr = new Array();
            arr["filename"] = imagename;
            arr["width"] = width;
            arr["height"] = height;
            if (request("type") == "image") {
            }
            else {
                if (request("iseditor") == 1) {
                    imagename = imagename + "|" + eval(width) + "|" + height;

                }
            }
            window.parent.retrunSrc(imagename);
            //	window.parent.returnValue = arr;
            //	window.parent.close();
        }

        //处理iframe高
        window.document.onselectstart = function () { return false; };
        function reinitIframe(objName) {
            var ifm = document.getElementById(objName);
            var subWeb = document.frames ? document.frames[objName].document : ifm.contentDocument;
            if (ifm != null && subWeb != null) {
                ifm.height = subWeb.body.scrollHeight + 10;
            }
        }

    </script>

</head>
<body>
    <form id="FORM1" enctype="multipart/form-data" runat="server">
        <div id="MainPage" runat="server" visible="false">
            <table width="98%" class="m_title" align="center">
                <tr>
                    <th colspan="2">选择文件（以下是您历次上传过的文件）：
                    </th>
                </tr>
                <tr>
                    <td colspan="2" style="height: 240px">
                        <div id="galleryarea" style="width: 100%; min-height: 100%; height: 100%; overflow: auto; margin-bottom: 4px;">
                            <div style="line-height: 25px;">
                                <asp:Literal ID="gallerymessage" runat="server"></asp:Literal>
                                <asp:Literal ID="lblPath" runat="server"></asp:Literal>
                                <span style="color: Red">请选择以下文件或者点击浏览按钮后再点上传按钮上传新文件。<br>
                                </span>
                            </div>
                            <asp:Panel ID="GalleryPanel" runat="server">
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
            </table>
            <table class="m_title" align="center" width="98%">
                <tr>
                    <th colspan="2">上传新文件：
                    </th>
                </tr>
                <tr id="UploadPanel" runat="server">
                    <td class="t_r t_bg">上传文件：
                    </td>
                    <td style="height: 45px;">
                        <input id="UploadFile" type="file" name="UploadFile" cssclass="m_btn_w2" runat="server"
                            size="20" style="margin-right: 30px;" />
                        <asp:Button ID="UploadImage" Text="上传" runat="server" CssClass="m_btn_w2" OnClick="UploadImage_OnClick" Height="21px" />
                        <asp:Button ID="DeleteImage" Text="删除" runat="server" Visible="false" Style="display: none"
                            CssClass="m_btn_w2" OnClick="DeleteImage_OnClick" />
                        <asp:CheckBox ID="cbWaterMark" Text="是否加水印" TextAlign="Left" runat="server" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="UploadFile" ID="FileValidator"
                            Display="dynamic" />
                        <input type="hidden" id="FileToDelete" value="" runat="server" />
                        <input type="hidden" id="RootImagesFolder" value="images" runat="server" />
                    </td>
                </tr>
            </table>
            <div style="color: Red; width: 98%; margin: 2px auto; line-height: 18px;">
                <tt>只允许上传jpg、gif、doc、xls、rar、pdf格式的文件且大小不能超过1M。</tt>
                <br />
                <asp:Literal ID="ResultsMessage" runat="server" />
                <input type="hidden" id="CurrentImagesFolder" value="images" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                    runat="server"></asp:ValidationSummary>
            </div>
        </div>
        <asp:Panel ID="iframePanel" runat="server">
            <% if (string.IsNullOrEmpty(Request.QueryString["iseditor"]))
               { %>

            <script type="text/javascript" src="../../tiny_mce_popup.js"></script>

            <%}
               else
               {%>

            <script type="text/javascript" src="../../zDialog/zDialog.js"></script>

            <%} %>

            <script type="text/javascript">
                function retrunSrc(s) {
                    if (request("iseditor") == 1) {
                        try {
                            window.name = s;
                            ownerDialog.OKEvent();
                        } catch (ex) {
                            window.returnValue = s;
                            window.close();
                        }
                    }
                    else {

                        var win = tinyMCEPopup.getWindowArg("window");

                        win.document.getElementById(tinyMCEPopup.getWindowArg("input")).value = s;
                        tinyMCEPopup.close();
                    }
                }
            </script>

            <iframe id="iframe_main" width="100%" frameborder="0" scrolling="no" onload="reinitIframe('iframe_main');"
                src="filemanager.aspx?frame=1&<%=Request.QueryString%>"></iframe>
        </asp:Panel>
        <input type="hidden" id="hiShowType" value="images" runat="server" />

        <script type="text/javascript">
            function RegularExpressionValidatorEvaluateIsValid(val) {
                var value = ValidatorGetValue(val.controltovalidate);
                if (ValidatorTrim(value).length == 0)
                    return true;
                var rx = null;
                if (val.validationexpression.indexOf("(?i)") >= 0) {
                    rx = new RegExp(val.validationexpression.replace("(?i)", ""), "i");
                } else {
                    rx = new RegExp(val.validationexpression);
                }
                var matches = rx.exec(value);
                return (matches != null && value == matches[0]);
            }
        </script>

    </form>
</body>
</html>
