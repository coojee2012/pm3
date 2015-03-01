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

public partial class WYDW_Common_FileUpload : System.Web.UI.Page
{
    RCenter rq = new RCenter();
    private static string dbFlink = "";
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

        //string FPrjFileId = Request.QueryString["FPrjFileId"];
        //lType.Text = db.getSysFileName(FPrjFileId);
        string FTypeid = Request.QueryString["Ftype"];
        if (string.IsNullOrEmpty(lType.Text))
        {
            switch (FTypeid)
            {
                case "1001":
                    lType.Text = "合同备案";
                    dbFlink = "YW_WY_XM_HTBA";
                    break;
                //case "2001":
                //    lType.Text = "合同备案";
                //    break;
                case "3001":
                    lType.Text = "人员照片";
                    dbFlink = "YW_WY_RY_JBXX";
                    break;
                case "3002":
                    lType.Text = "毕业证书电子件";
                    dbFlink = "YW_WY_RY_JBXX";
                    break;
                case "3003":
                    lType.Text = "学位证书电子件";
                    dbFlink = "YW_WY_RY_JBXX";
                    break;
                case "3004":
                    lType.Text = "职称证书电子件";
                    dbFlink = "YW_WY_RY_JBXX";
                    break;
                default:
                    lType.Text = "其他附件";
                    break;
            }

        }

    }
    //保存
    private void saveInfo()
    {
        string FAppId = "";
        //if (!string.IsNullOrEmpty(Request.QueryString["FAppId"]))
        //{
        //    FAppId = Request.QueryString["FAppId"];
        //}
        if (Session["FAppId"] != null)
        {
            FAppId = (string)Session["FAppId"];
            string FUserId = "";
            string fType = Request.QueryString["Ftype"] == null ? "" : Request.QueryString["Ftype"];
            if (!string.IsNullOrEmpty(Request.QueryString["FUserId"]))
            {
                FUserId = Request.QueryString["FUserId"];
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
            //sl.Add("FPrjFileId", Request.QueryString["FPrjFileId"]);
            sl.Add("FTypeid", Request.QueryString["Ftype"]);
            sl.Add("FAppId", FAppId); //业务ID
            //人员信息获取的flinkid
            if (fType == "3001" || fType == "3002" || fType == "3003" || fType == "3004")
            {
                sl.Add("FLinkid", getSessionRYID());
            }
            else
            {
                sl.Add("FLinkid", rq.GetSignValue("select FID from " + dbFlink + " where FAppID='" + FAppId + "'"));//YW_WY_XM_HTBA FID号
            }
            sl.Add("FFileUrl", t_FFileUrl.Value);
            sl.Add("FFileName", t_FName.Text); //文件名
            sl.Add("FFilePath", t_FUrl.Value); //文件地址
            sl.Add("FSize", t_FFileSize.Value); //文件大小
            sl.Add("FFileType", t_FFileType.Value); //文件类型  
            sl.Add("FUserId", FUserId); //上传人（如果有的话。）
            string fid = "";
            if (!isHave(t_FName.Text, t_FUrl.Value, ref fid))
            {
                if (rq.SaveEBase("WY_FileList", sl, "FID", so))
                {
                    ViewState["FID"] = sl["FID"].ToString();
                    tool.showMessageAndRunFunction("保存成功",
                        "window.returnValue='ok|" + fType + "|" + t_FUrl.Value + "';window.close()");
                }
                else
                {
                    tool.showMessage("保存失败，请重试");
                }
            }
            else
            {
                ViewState["FID"] = fid;
                tool.showMessageAndRunFunction("保存成功",
                        "window.returnValue='ok|" + fType + "|" + t_FUrl.Value + "';window.close()");
            }
        }
        else
        {
            this.RegisterStartupScript(new Guid().ToString(), "<script>alert('数据加载失败！');</script>");
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

    #region [人员id的session读取]
    public string getSessionRYID()
    {
        string result = "";
        if (Session["RYID"] == null)
        {
            result = new Guid().ToString();
        }
        else
        {
            result = Session["RYID"].ToString();
        }
        return result;
    }
    #endregion

    private bool isHave(string Name, string url, ref string fid)
    {
        string strsql = "select Fid from WY_FileList where FFilePath='" + url + "' and FFileName='" + Name + "'";
        fid = rq.GetSignValue(strsql);
        if (fid != "")
        {
            return true;
        }
        return false;
    }
}