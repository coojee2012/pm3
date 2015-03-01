using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleApp;
using System.Drawing;
using ProjectBLL;
using ProjectData;

public partial class Government_AppMain_fileQuery : System.Web.UI.Page
{
    RCenter rc = new RCenter(); RApp ra = new RApp(); ProjectDB db = new ProjectDB();
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["FAppId"] != null && !string.IsNullOrEmpty(Request["FAppId"]))
            { t_YWBM.Value = Request["FAppId"].ToString(); }
            bindFile();
        }
    }
    //材料绑定
    public void bindFile()
    {
        //工法内容材料
        string sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                        where l.FAppid='" + t_YWBM.Value + "' and l.FType=1000");
        DataTable dt = sh.GetTable(sql);
        btnUP.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1000 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY.Text = "<tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt>";
            tbBZ.Text = dt.Rows[0]["Fremark"].ToString(); t_FID0.Value = dt.Rows[0]["FID"].ToString();
            cb.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY.Text = "<tt>未检验</tt>";
        }
        //省工法报表
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lBaoBiao.Text = "<tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt>";
            tbBaoBiao.Text = dt.Rows[0]["Fremark"].ToString(); t_BaoBiao.Value = dt.Rows[0]["FID"].ToString();
            cbBaoBiao.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lBaoBiao.Text = "<tt>未检验</tt>";
        }
        //企业级工法批准文件复印件
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=2 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lFYJ.Text = "<tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt>";
            tbFYJ.Text = dt.Rows[0]["Fremark"].ToString(); t_FYJ.Value = dt.Rows[0]["FID"].ToString();
            cbFYJ.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lFYJ.Text = "<tt>未检验</tt>";
        }
        //关键技术评估意见
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1006");
        dt = sh.GetTable(sql);
        btnUP4.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1006 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY4.Text = "<tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt>";
            tbBZ4.Text = dt.Rows[0]["Fremark"].ToString(); t_FID4.Value = dt.Rows[0]["FID"].ToString();
            cb4.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY4.Text = "<tt>未检验</tt>";
        }
        //工程应用证明相关附件
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1002");
        dt = sh.GetTable(sql);
        btnUP2.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1002 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY2.Text = "<tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt>";
            tbBZ2.Text = dt.Rows[0]["Fremark"].ToString(); t_FID2.Value = dt.Rows[0]["FID"].ToString();
            cb2.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY2.Text = "<tt>未检验</tt>";
        }
        //经济效益证明
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1005");
        dt = sh.GetTable(sql);
        btnUP7.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1005 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY7.Text = "<tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt>";
            tbBZ7.Text = dt.Rows[0]["Fremark"].ToString(); t_FID7.Value = dt.Rows[0]["FID"].ToString();
            cb7.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY7.Text = "<tt>未检验</tt>";
        }
        //完成单位意见、无争议声明相关附件
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1001");
        dt = sh.GetTable(sql);
        btnUP1.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1001 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY1.Text = "<tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt>";
            tbBZ1.Text = dt.Rows[0]["Fremark"].ToString(); t_FID1.Value = dt.Rows[0]["FID"].ToString();
            cb1.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY1.Text = "<tt>未检验</tt>";
        }
        //专业技术情报部门提供的科技查新报告复印件
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=3 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lCL.Text = "<tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt>";
            tbCL.Text = dt.Rows[0]["Fremark"].ToString(); t_CL.Value = dt.Rows[0]["FID"].ToString();
            cbCL.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lCL.Text = "<tt>未检验</tt>";
        }
        //科技成果获奖证明相关附件
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1004");
        dt = sh.GetTable(sql);
        btnUP5.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1004 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY5.Text = "<tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt>";
            tbBZ5.Text = dt.Rows[0]["Fremark"].ToString(); t_FID5.Value = dt.Rows[0]["FID"].ToString();
            cb5.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY5.Text = "<tt>未检验</tt>";
        }
        //专业技术专利证明文件
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1007");
        dt = sh.GetTable(sql);
        btnUP6.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1007 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY6.Text = "<tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt>";
            tbBZ6.Text = dt.Rows[0]["Fremark"].ToString(); t_FID6.Value = dt.Rows[0]["FID"].ToString();
            cb6.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY6.Text = "<tt>未检验</tt>";
        }
        //工法操作要点照片（10到15张）
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1008");
        dt = sh.GetTable(sql);
        btnUP8.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1008 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY8.Text = "<tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt>";
            tbBZ8.Text = dt.Rows[0]["Fremark"].ToString(); t_FID8.Value = dt.Rows[0]["FID"].ToString();
            cb8.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY8.Text = "<tt>未检验</tt>";
        }
        //工法成熟可靠性说明文件
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1003");
        dt = sh.GetTable(sql);
        btnUP3.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1003 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY3.Text = "<tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt>";
            tbBZ3.Text = dt.Rows[0]["Fremark"].ToString(); t_FID3.Value = dt.Rows[0]["FID"].ToString();
            cb3.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY3.Text = "<tt>未检验</tt>";
        }
        //技术转让的证明材料
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1009");
        dt = sh.GetTable(sql);
        btnUP9.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1009 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY9.Text = "<tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt>";
            tbBZ9.Text = dt.Rows[0]["Fremark"].ToString(); t_FID9.Value = dt.Rows[0]["FID"].ToString();
            cb9.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY9.Text = "<tt>未检验</tt>";
        }
    }

}