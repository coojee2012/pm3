using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Approve.RuleCenter;

public partial class GFEnt_ApplyFileUp : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            t_YWBM.Value = Session["FAppId"].ToString(); showInfo();
        }
    }
    public void showInfo()
    {
        string sql = string.Format(@"select count(1) from YW_GF_FileList l 
                     inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1000");
        string cou = sh.GetSignValue(sql);
        btnUP.Attributes.Add("value", "文件上传(" + cou + ")");

        sql = string.Format(@"select count(1) from YW_GF_FileList l 
                     inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1001");
        cou = sh.GetSignValue(sql);
        btnUP1.Attributes.Add("value", "文件上传(" + cou + ")");

        sql = string.Format(@"select count(1) from YW_GF_FileList l 
                     inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1002");
        cou = sh.GetSignValue(sql);
        btnUP2.Attributes.Add("value", "文件上传(" + cou + ")");

        sql = string.Format(@"select count(1) from YW_GF_FileList l 
                     inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1003");
        cou = sh.GetSignValue(sql);
        btnUP3.Attributes.Add("value", "文件上传(" + cou + ")");

        sql = string.Format(@"select count(1) from YW_GF_FileList l 
                     inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1006");
        cou = sh.GetSignValue(sql);
        btnUP4.Attributes.Add("value", "文件上传(" + cou + ")");

        sql = string.Format(@"select count(1) from YW_GF_FileList l 
                     inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1004");
        cou = sh.GetSignValue(sql);
        btnUP5.Attributes.Add("value", "文件上传(" + cou + ")");

        sql = string.Format(@"select count(1) from YW_GF_FileList l 
                     inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1007");
        cou = sh.GetSignValue(sql);
        btnUP6.Attributes.Add("value", "文件上传(" + cou + ")");

        sql = string.Format(@"select count(1) from YW_GF_FileList l 
                     inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1005");
        cou = sh.GetSignValue(sql);
        btnUP7.Attributes.Add("value", "文件上传(" + cou + ")");

        sql = string.Format(@"select count(1) from YW_GF_FileList l 
                     inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1008");
        cou = sh.GetSignValue(sql);
        btnUP8.Attributes.Add("value", "文件上传(" + cou + ")");

        sql = string.Format(@"select count(1) from YW_GF_FileList l 
                     inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1009");
        cou = sh.GetSignValue(sql);
        btnUP9.Attributes.Add("value", "文件上传(" + cou + ")");
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}