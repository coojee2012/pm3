/*上传文件公用方法
 * 必须传入fid，如人员修改时传入要修改的fid，增加时先new一个fid在传入
 */
 
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Services;
using Approve.RuleCenter;
using Approve.Common;
using System.Text;
using System.Collections;
using System.Globalization;
using Approve.EntityBase;

public partial class PropertyEntApp_Common_FileManage : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    //public string strPath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FAppId"] == null && Session["XMBH"] == null)
        {
            //Response.Redirect("../../Main/Index.aspx");
        }
        else 
        {
            if (!Page.IsPostBack)
            {
                //hdBaseID.Value = Request.QueryString["FBaseID"];
                //hdPID.Value = Request.QueryString["PID"];
                //hdRelatedName.Value = Request.QueryString["RelatedName"];

                string FTypeid = hdFTypeID.Value = Request.QueryString["Ftype"];
                object obj=Request.QueryString["IsSingle"];
                if (string.IsNullOrEmpty(lblFileName.Text))
                {
                    switch (FTypeid)
                    {
                        case "1001":
                            lblFileName.Text = "合同备案";
                            break;
                        //case "2001":
                        //    lType.Text = "合同备案";
                        //    break;
                        case "3001":
                            lblFileName.Text = "人员照片";
                            break;
                        case "3002":
                            lblFileName.Text = "毕业证书电子件";
                            break;
                        case "3003":
                            lblFileName.Text = "学位证书电子件";
                            break;
                        case "3004":
                            lblFileName.Text = "职称证书电子件";
                            break;
                        default:
                            lblFileName.Text = "其他附件";
                            break;
                    }
                }
                if (string.IsNullOrEmpty(Request["ReadOnly"])) hdReadOnly.Value = "0";
                else hdReadOnly.Value = Request["ReadOnly"];  //1只读

                if (Request.QueryString["FId"] == null)
                {
                    hdIsUpdate.Value = "0";
                }
                else
                {
                    hdFLinkID.Value = Request.QueryString["FId"].ToString();
                    string sql = "select  ROW_NUMBER() OVER (order by FTime Desc) As   Seq,* From WY_FileList Where FTypeid='" + FTypeid + "' and FLinkID = '" + Request.QueryString["FId"].ToString() + "' Order By FTime Desc";
                    //if (FTypeid == "3001" || FTypeid == "3002" || FTypeid == "3003" || FTypeid == "3004")
                    //{ 
                    //    sql="select  ROW_NUMBER() OVER (order by FTime Desc) As   Seq,* From WY_FileList Where FTypeid='"+FTypeid+"' and FAppId='"+Session["FAppId"].ToString()+"' and FLinkID = '"+Session["RYID"].ToString()+"' Order By FTime Desc";
                    //}
                    //else if(FTypeid=="1001")
                    //{
                    //    sql = "select  ROW_NUMBER() OVER (order by FTime Desc) As   Seq,* From WY_FileList Where FTypeid='" + FTypeid + "' and FAppId='" + Session["FAppId"].ToString() + "' and FLinkID = '" + rc.GetSignValue("select FID from YW_WY_XM_HTBA where FAppID='" + Session["FAppId"].ToString() + "'") + "' Order By FTime Desc";
                    //}
                    DataTable dt = rc.GetTable(sql);
                    DataColumn dc = new DataColumn("TDDel");
                    dt.Columns.Add(dc);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Request.QueryString["ReadOnly"] == "0")   //可修改
                        {
                            dt.Rows[i]["TDDel"] = "<a href=\"javascript:FileDel('" + dt.Rows[i]["FID"].ToString() + "')\">删除</a>";
                        }
                        else
                            dt.Rows[i]["TDDel"] = "<span title=\"不能操作\"></span>";
                    }
                    if (obj != null && obj.ToString() == "1" && dt.Rows.Count == 1)
                    {
                        hdIsUpdate.Value = "1";
                    }
                    else
                    {
                        hdIsUpdate.Value = "0";
                    }
                    rptFileList.DataSource = dt;
                    rptFileList.DataBind();
                    hdFileCount.Value = (dt.Rows.Count+1).ToString();
                }

                //lblFileName.Text = rc.GetSignValue(EntityTypeEnum.EbPMDicFile, "ZName", string.Format("FNumber = '{0}'", Request.QueryString["FileNum"]));
                //lblFileName.Text = rc.GetSignValue(string.Format("Select ZName From PM_DicFile Where FNumber = '{0}'", Request.QueryString["FileNum"]));

                //dt.Rows[i]["OldFileName"] = "<a target=_blank href='" + ComFunction.FileServer(dt.Rows[i]["FContent"].ToString()) + "'>" + dt.Rows[i]["OldFileName"].ToString() + "</a>";
            }
        }
        
    }

}