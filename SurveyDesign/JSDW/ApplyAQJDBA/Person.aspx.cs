﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Tools;
using System.Data;
using EgovaDAO;
using System.IO;
//using System.Drawing;

public partial class JSDW_ApplyAQJDBA_Person : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["fid"]))
            {

            }
            else
            {
                TC_AJBA_Human emp = dbContext.TC_AJBA_Human.Where(t => t.FId == Convert.ToString(Request.QueryString["fid"])).FirstOrDefault();
                if (emp != null)
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.fillPageControl(emp);
                    if(emp.FPhoto.Length>0){
                        showImage(image_FPhoto, new MemoryStream(emp.FPhoto.ToArray()));
                    }
                    
                }
                ViewState["FID"] = Request.QueryString["fid"];
            }
            BindControl();
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
        }
    }
    void BindControl()
    {
        //项目职位
        DataTable dt = rc.getDicTbByFNumber("112202");
        t_XMZW.DataSource = dt;
        t_XMZW.DataTextField = "FName";
        t_XMZW.DataValueField = "FNumber";
        t_XMZW.DataBind();
        //证件类型
        dt = rc.getDicTbByFNumber("112203");
        t_ZJLX.DataSource = dt;
        t_ZJLX.DataTextField = "FName";
        t_ZJLX.DataValueField = "FNumber";
        t_ZJLX.DataBind();
        //职称
        dt = rc.getDicTbByFNumber("5080");
        t_ZC.DataSource = dt;
        t_ZC.DataTextField = "FName";
        t_ZC.DataValueField = "FNumber";
        t_ZC.DataBind();
        //最高学历
        dt = rc.getDicTbByFNumber("107");
        t_ZGXL.DataSource = dt;
        t_ZGXL.DataTextField = "FName";
        t_ZGXL.DataValueField = "FNumber";
        t_ZGXL.DataBind();
    }
    //保存
    private void saveInfo()
    {
        string fId = EConvert.ToString(ViewState["FID"]);
        TC_AJBA_Human Emp = new TC_AJBA_Human();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_AJBA_Human.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FPrjItemId = EConvert.ToString(ViewState["FPrjItemId"]);
            Emp.FAppId = EConvert.ToString(ViewState["FAppId"]);
            dbContext.TC_AJBA_Human.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        

        //string name = t_FPhoto.PostedFile.FileName;
        //string type = name.Substring(name.LastIndexOf(".") + 1);
        //FileStream fs = File.OpenRead(name);
        //byte[] content = new byte[fs.Length];
        //fs.Read(content, 0, content.Length);
        //fs.Close();  

        // 把图片存放到数据库里，存为Image类型：  
        string photoPath = EConvert.ToString(this.ViewState["photoPath"]);
        if (string.IsNullOrEmpty(photoPath))
        {
            HttpPostedFile UpFile = this.file_FPhoto.PostedFile;   //HttpPostedFile对象，用于读取图象文件属性  
            int FileLength = UpFile.ContentLength;     //记录文件长度   
            Byte[] FileByteArray = new Byte[FileLength];    //图象文件临时储存Byte数组  
            if (FileLength == 0)
            {      //无图片  

            }
            else
            {
                Stream StreamObject = UpFile.InputStream;       //建立数据流对像  
                //读取图象文件数据，FileByteArray为数据储存体，0为数据指针位置、FileLnegth为数据长度  
                StreamObject.Read(FileByteArray, 0, FileLength);
                // StreamObject.Close();
                Emp.FPhoto = new System.Data.Linq.Binary(FileByteArray);
            }
        }
        else
        {
            Emp.FPhoto = getImage(photoPath);
        }
        dbContext.SubmitChanges();
        ViewState["FID"] = fId;
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void btnAddEmpSG_Click(object sender, EventArgs e)
    {

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        // 把图片存放到数据库里，存为Image类型：  

        HttpPostedFile UpFile = this.file_FPhoto.PostedFile;   //HttpPostedFile对象，用于读取图象文件属性  
        int FileLength = UpFile.ContentLength;     //记录文件长度   
        Byte[] FileByteArray = new Byte[FileLength];    //图象文件临时储存Byte数组  
        if (FileLength == 0)
        {      //无图片  

        }
        else
        {
            showImage(image_FPhoto, UpFile.InputStream);
        }
    }
    void showImage(Image image,Stream file)
    {
        if(file != null){
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(file);
            string FilePath = "upload\\ajphoto\\" + Guid.NewGuid().ToString() + ".jpg";
            bitmap.Save(HttpRuntime.AppDomainAppPath + FilePath);
            image.ImageUrl = "/" + FilePath;
            this.ViewState["photoPath"] = HttpRuntime.AppDomainAppPath + FilePath;
        }
    }
    Byte[] getImage(string photoPath)
    {        
        Stream  stream = File.OpenRead(photoPath);
        int FileLength = (int)stream.Length;
        Byte[] FileByteArray = new Byte[FileLength];    //图象文件临时储存Byte数组  
        stream.Read(FileByteArray, 0, FileLength);
        return FileByteArray;
    }
}