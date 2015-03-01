using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class ReportPrint_PrintList : System.Web.UI.Page
{
    public string DefaultUrl;
    protected void Page_Load(object sender, EventArgs e)
    {
        string Type = Request["TypeId"].ToString();
        switch (Type)
        {
            //********************************************************************************//
            //安全生产证书打印类型代码说明：前三位代表系统类型 150：安全生产许可


            //1.后两位代表打印业务类型：01 申请、延期 02 证书变更
            case "15001":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB15001.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=建筑施工企业安全生产许可证申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "15002":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB15002.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=建筑施工企业安全生产许可证变更申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "15003":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=AQSCXKZ_ZSZB_SHB.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            //**********
            //********************************************************************************//
            //2.施工企业证书打印类型代码说明：前三位代表系统类型 101：施工企业,120：招标代理企业

            //case "10107":
            //    DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=建筑业企业资质复业申请表（施工总承包、专业承包序列）复业申请.cpt&AppId=" + this.Session["FAppId"].ToString();
            //    break;
            //后两位代表打印业务类型：01 主项升级 、增项升级 、增项申请 、延续申请 、 转正申请 、 其他  02 证书变更备案 08 证书增补 09 备案 10 主增互换 11监督复查  12 许可证申请



            case "10101":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB10101.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=建筑业企业资质申请表（劳务分包序列）.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "10102":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB10102.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=建筑业企业资质申请表（施工总承包、专业承包序列）.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "10103":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB10103.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=施工企业资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "10104":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB10104.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=施工企业资质证书增补审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "10105":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB10105.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=建筑业企业基本信息表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "10106":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB10106.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=施工企业资质证书变更备案表（其他）.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "10107":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB10107.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=建筑业企业资质复业申请表（施工总承包、专业承包序列）复业申请.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "10111":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB10111.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=建筑业企业监督复查申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "10112":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB10112.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=施工企业资质证书变更备案表_报部.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "10113":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB10113.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=施工企业资质证书增补审核表_报部.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "10125":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB10125.cpt&txtFName=" + this.Session["txtFName"] + "&dmType=" + Session["dmType"] + "&bSelfAppState=" + Session["bSelfAppState"] + "&dbFManageDeptId=" + Session["dbFManageDeptId"] + "&dbFBatchNoId=" + Session["dbFBatchNoId"] + "&FRoleId=" + Session["DFRoleId"] + "&stime=" + Session["Stime"] + "&etime=" + Session["Etime"];
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=施工组公示意见汇总表.cpt&txtFName=" + this.Session["txtFName"] + "&dmType=" + Session["dmType"] + "&bSelfAppState=" + Session["bSelfAppState"] + "&dbFManageDeptId=" + Session["dbFManageDeptId"] + "&dbFBatchNoId=" + Session["dbFBatchNoId"] + "&FRoleId=" + Session["DFRoleId"] + "&stime=" + Session["Stime"] + "&etime=" + Session["Etime"];
                break;
            //********************************************************************************//
            //3.工程设计施工一体化资质企业
            case "19601":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB19601.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程设计施工一体化资质申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "19602":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB19602.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=设计施工一体化企业资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "19603":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB19603.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=设计施工一体化企业资质证书增补审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "19604":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB19604.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=设计施工一体化企业基本信息表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "19605":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB19605.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=设计施一体化人员业绩信息表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "19606":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB19606.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=设计施工一体化企业资质证书变更备案表_报部.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "19607":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB19607.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=设计施工一体化企业资质证书增补审核表_报部.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            //********************************************************************************//
            //4.工程勘察、工程设计资质企业


            case "15501":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB15501.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程勘察、工程设计资质申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "15502":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB15502.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=勘察设计企业资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "15503":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB15503.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=勘察设计企业资质证书增补审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "15504":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB15504.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程勘察、工程设计企业基本信息表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "15505":
                string mReview = Request["Review"].ToString();
                if (mReview == "1")
                {
                    DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB15505.cpt&fbid=" + this.Session["FBaseId"].ToString() + "&middleID=" + this.Session["FDesignReportId"].ToString();
                    //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=四川省勘察设计年报表.cpt&fbid=" + this.Session["FBaseId"].ToString() + "&middleID=" + this.Session["FDesignReportId"].ToString();
                }
                else
                    DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB15505.cpt&fbid=" + this.Session["FBaseId"].ToString() + "&middleID=" + this.Session["FDesignReportId"].ToString() + "&op=export&format=excel&extype=page";
                    //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=四川省勘察设计年报表.cpt&fbid=" + this.Session["FBaseId"].ToString() + "&middleID=" + this.Session["FDesignReportId"].ToString() + "&op=export&format=excel&extype=page";
                break;
            case "15506":
                DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB15506.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程勘察、工程设计企业变更基本信息表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "15507":
                DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=KCSJ_PrintALLseason.cpt&AppId=" + this.Session["FAppId"].ToString() + "&op=export&format=excel&extype=page";
                break;
            case "15508":
                DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=KCSJ_PrintALLseason.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "15509":
                DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB15509.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程勘察、工程设计个人业信息表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "15510":
                DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB15510.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=勘察设计企业资质证书变更备案表_报部.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "15511":
                DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB15511.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=勘察设计企业资质证书增补审核表_报部.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            //********************************************************************************//
            //4.工程勘察企业


            case "155001":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=kc/XZSP_SQB155001.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=kc/工程勘察资质申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "155002":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=kc/XZSP_SQB155002.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=kc/勘察企业资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "155003":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=kc/XZSP_SQB155003.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=kc/勘察企业资质证书增补审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "155006":
                DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=kc/XZSP_SQB155006.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=kc/工程勘察企业变更基本信息表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "155009":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=kc/XZSP_SQB155009.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=kc/工程勘察个人业信息表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "155004":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=kc/XZSP_SQB155004.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=kc/工程勘察企业业绩表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            //4.工程设计企业
            case "155101":
                if (Session["FManageTypeId"].ToString() == "1902")
                {
                    DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=sj/XZSP_SQB155101.cpt&AppId=" + this.Session["FAppId"].ToString();
                    //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=sj/工程设计资质重新核定申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                }
                else
                {
                    DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=sj/XZSP_SQB1551011.cpt&AppId=" + this.Session["FAppId"].ToString();
                    //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=sj/工程设计资质申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                }
                break;
            case "155102":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=sj/XZSP_SQB155102.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=sj/设计企业资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "155103":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=sj/XZSP_SQB155103.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=sj/设计企业资质证书增补审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "155106":
                DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=sj/XZSP_SQB155106.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @" http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=sj/工程设计企业变更基本信息表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "155109":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=sj/XZSP_SQB155109.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=sj/工程设计个人业信息表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "155104":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=sj/XZSP_SQB155104.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=sj/工程设计企业业绩表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;

            //********************************************************************************//
            //5.招标代理机构企业 120
            case "12001":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12001.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程建设项目招标代理机构资格申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "12002":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12002.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=招标代理机构法定代表人变更表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "12003":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12003.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=招标代理机构技术经济负责人变更表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "12004":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12004.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程建设项目招标代理机构基本信息表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "12005":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12005.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=建设工程企业资质证书变更审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "12006":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12006.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=建设工程企业资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "12007":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12007.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=建设工程企业资质证书增补审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "12008":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12008.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=招标代理机构资质证书变更备案表_报部.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "12009":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12009.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=建设工程企业资质证书增补审核表_报部.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;

            //********************************************************************************//
            //6.规划编制单位 
            case "20201":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB20201.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=城市规划编制单位资质证书申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "20202":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB20202.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=城市规划编制单位资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "20203":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB20203.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=城市规划编制单位资质证书增补审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "20204":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB20204.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=城市规划编制单位个人业绩表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;

            //********************************************************************************//
            //7.城市园林绿化企业
            case "13501":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB13501.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=城市园林绿化企业资质申请表2.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "13502":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB13502.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=城市园林绿化企业资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "13503":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB13503.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=城市园林绿化企业资质证书增补审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "13504":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB13504.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=城市园林绿化企业资质申请表_报部.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "13505":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB13505.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=城市园林绿化企业资质证书变更备案表_报部.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "13506":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB13506.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=城市园林绿化企业资质证书增补审核表_报部.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            //********************************************************************************//
            //8.检测机构企业


            case "17501":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB17501.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=四川省建设工程质量检测机构资质申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "17502":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB17502.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=质量检测机构资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "17503":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB17503.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=质量检测机构资质证书增补审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;

            //********************************************************************************//
            //9.房地产评估企业


            case "18601":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB18601.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=房地产估价机构资质等级申报表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "18602":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB18602.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=房地产估价机构资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "18603":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB18603.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=房地产估价机构资质证书增补审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;

            //********************************************************************************//
            //10.建设工程监理企业
            case "12501":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12501.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程监理企业资质核准.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "12502":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12502.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程监理企业资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "12503":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12503.cpt&AppId=" + this.Session["FAppId"].ToString();
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程监理企业资质证书增补审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "12504":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12504.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程监理企业基本信息表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "12505":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12505.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程监理企业资质证书变更备案表_报部.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "12506":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB12506.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程监理企业资质证书增补审核表_报部.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            //********************************************************************************//
            //11.物业企业
            case "18701":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB18701.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=物业管理企业资质申报表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "18702":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB18702.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=物业管理企业资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "18703":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB18703.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=物业管理企业资质证书增补审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            //********************************************************************************//
            //12.省外入川企业
            case "18001":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB18001.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=四川省省外企业入川从事建筑活动备案申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "18002":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB18002.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=省外入川企业备案证书变更申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "18003":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB18003.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=四川省省外入川企业年度核验备案申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "18004":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB18004.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=省外企业入川重点监督复查申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;

            //13.建筑节能材料和产品备案


            case "19901":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB19901.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=建筑节能材料和产品备案管理系统.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;

            //14.房地产开发企业


            case "13001":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB13001.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=房地产开发企业资质核准.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "13002":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB13002.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=房地产开发企业资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "13003":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB13003.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=房地产开发企业资质证书增补审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            //15.工程造价咨询单位
            case "18501":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB18501.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程造价咨询单位资质申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "18502":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB18502.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程造价咨询单位资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "18503":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB18503.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=工程造价咨询单位资质证书增补审核表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;


            //16.省外入川企业
            case "14001":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB14001.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=入川工程勘察、工程设计资质申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "14002":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB14002.cpt&fappid=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=省外入川设计企业备案证书变更申请表.cpt&fappid=" + this.Session["FAppId"].ToString();
                break;
            case "14003":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB14003.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=入川工程勘察、工程设计个人业信息表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            //17.施工图审查机构
            case "14501":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB14501.cpt&fappid=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=四川省施工图审查机构申报表.cpt&fappid=" + this.Session["FAppId"].ToString();
                break;
            case "14502":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB14502.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=四川省施工图审查机构申报表_新.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "165":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB165.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=施工图审查机构资质证书变更备案表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            //18.建造师
            case "16501"://市州回执单
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=JZS/HZDSZ.cpt&FId=" + this.Request["FId"].ToString();
                break;
            case "16502"://人员分布统计
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=JZS/JZSJZY_FB.cpt&FNID=" + this.Session["DFId"].ToString();
                break;
            case "16503"://专业分布情况统计
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=JZS/JZSJZY_ZCZYFB_QK.cpt&FNID=" + this.Session["DFId"].ToString();
                break;
            case "16504"://年龄结构统计
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=JZS\JZSJZY_NL_TJ.cpt&FNID=" + this.Session["DFId"].ToString();
                break;
            case "16505"://学历结构统计
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=JZS\JZSJZY_XL_TJ.cpt&FNID=" + this.Session["DFId"].ToString();
                break;
            case "16506"://企业回执单
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=JZS/HZDQY.cpt&FId=" + this.Request["FId"].ToString();
                break;
            case "19909"://新技术示范工程申报
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB19909.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=四川省建筑业新技术应用示范工程申报书.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            //19.入川招标代理机构
            case "45001":
                DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=XZSP_SQB45001.cpt&AppId=" + this.Session["FAppId"].ToString();
                //DefaultUrl = @"http://report.scjst.gov.cn:8888/WebReport/ReportServer?reportlet=四川省省外企业入川招标代理机构备案申请表.cpt&AppId=" + this.Session["FAppId"].ToString();
                break;
            case "8046":
                DefaultUrl = @"http://220.167.25.10:7001/WebReport/ReportServer?reportlet=" + this.Request["RName"].ToString() + "&FNID=" + this.Session["DFId"].ToString();
                break;
            case "8046017":
                DefaultUrl = @"http://sgrpt.scjst.gov.cn:888/CEIN_MB/page/RR.aspx?reportlet=" + this.Request["RName"].ToString();
                break;
            //省外企业管理系统
            case "1001":
                DefaultUrl = @"http://report1.scjst.gov.cn:7001/WebReport/ReportServer?reportlet=" + this.Request["RName"].ToString() + "&AppId=" + this.Session["FAppId"].ToString();
                break;
            //工法系统
            case "22001":
                DefaultUrl = @"http://172.16.0.111:7001/WebReport/ReportServer?reportlet=XZSP_SQB22001.cpt&YWBM=" + this.Session["FAppId"].ToString();
                break;
        }
    }
}
