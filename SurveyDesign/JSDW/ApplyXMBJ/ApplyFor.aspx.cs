using Approve.Common;
using Approve.EntityBase;
using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class JSDW_ApplyYDGH_ApplyFor : System.Web.UI.Page
{
    private int fMType = 8080;
    private ProjectDB db = new ProjectDB();
    private RCenter rc = new RCenter();
    private RCenter rcXM = new RCenter("XM_BaseInfo");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string year = DateTime.Now.ToString("yyyy");
            //t_FYear.Text = year;
            //t_FName.Text = year + "用地规划申请";
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string FId = hfFid.Value;
        string fPrjDataId = Guid.NewGuid().ToString();
        string fAppId = Guid.NewGuid().ToString();
        string FPrjId = Guid.NewGuid().ToString();
        CF_App_List lKC = new CF_App_List();//设计企业业务
        lKC.FId = fAppId;
        lKC.FLinkId = fPrjDataId;
        lKC.FBaseinfoId = CurrentEntUser.EntId;
        lKC.FPrjId = FPrjId;
        lKC.FName = t_FName.Text.Trim();
        lKC.FManageTypeId = fMType;
        lKC.FwriteDate = DateTime.Now;
        lKC.FState = 0;
        lKC.FIsDeleted = false;
        lKC.FYear = EConvert.ToInt(t_FYear.Text.Trim());
        lKC.FMonth = DateTime.Now.Month;
        lKC.FBaseName = CurrentEntUser.EntName;
        lKC.FTime = DateTime.Now;
        lKC.FCreateTime = DateTime.Now;
        lKC.FReportDate = DateTime.Now;
        lKC.FReportCount = 1;
        db.CF_App_List.InsertOnSubmit(lKC);

        CF_Prj_BaseInfo cpb = new CF_Prj_BaseInfo();
        cpb.FId = FPrjId;
        cpb.FBaseinfoId = CurrentEntUser.EntId;
        cpb.FLinkId = fPrjDataId;
        cpb.FPrjName = txtFPrjName.Text;
        db.CF_Prj_BaseInfo.InsertOnSubmit(cpb);
        db.SubmitChanges();
        string guid = Guid.NewGuid().ToString();
        Session["FIsApprove"] = 0;
        var param = "?XM_Id=" + guid + "&fAppId=" + fAppId;
        if (!string.IsNullOrEmpty(FId))
        {
            #region
            //            string sql = @"select top 1 * from TC_Prj_Info where Fid='" + FId + "'";
            //            DataTable dt = rc.GetTable(sql);
            //            if (dt != null && dt.Rows.Count > 0)
            //            {
            //                string XMBJBM = Guid.NewGuid().ToString();
            //                DataRow row = dt.Rows[0];
            //                string insertSql = string.Format(@"INSERT INTO [dbCenter].[dbo].[YW_XMBJ]
            //                                                        ([ID]
            //                                                        ,[FBaseInfoId]
            //                                                        ,[YWBM]
            //                                                        ,[XMBJMC]
            //                                                        ,[XMMC]
            //                                                        ,[XMBH]
            //                                                        ,[YDMJ]
            //                                                        ,[JSDW]
            //                                                        ,[JSDWDZ]
            //                                                        ,[LXR]
            //                                                        ,[LXDH]
            //                                                        ,XMBJBM
            //                                                        ,XMSD
            //                                                        ,BH
            //                                                        ,JSDD
            //                                                        ,JZMJ
            //                                                        ,[LXWH])
            //                                             VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')"
            //                    , guid, CurrentEntUser.EntId, fAppId, txtFPrjName.Text, row["ProjectName"], row["FId"], row["Area"], row["JSDW"], row["JSDWDZ"], row["Contacts"], row["Mobile"], XMBJBM, row["AddressDept"], row["ProjectNo"], row["Address"], row["ConstrScale"], row["ProjectNumber"]);
            //                insertSql += string.Format(@"insert into YW_FILE(YWBM,[FILE_NAME])
            //                                select '{0}',[FILE_NAME] from MB_ZMCL where TypeId=9 order by OrderId", guid);
            #endregion
            string sql = "EXEC SP_XMBJ_AUTOCREATE @ID,@FBaseInfoId,@YWBM,@XMBH";//自动写入数据
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = guid, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@FBaseInfoId", Value = CurrentEntUser.EntId, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YWBM", Value = fAppId, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMBH", Value = FId, SqlDbType = SqlDbType.VarChar });
            //System.IO.File.AppendAllText("C:\\yujiajun.log", FId, System.Text.Encoding.Default);
            bool success = rc.PExcute(sql, list.ToArray());
            if (success)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>parent.parent.document.location='AppMain/aIndex.aspx" + param + "';</script>");
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "1", "<script>alert('申请失败')</script>");
            //}
        }
    }
    private void BindForm()
    {
        t_FYear.Text = DateTime.Now.ToString("yyyy");
        this.t_FName.Text = t_FYear.Text + " " + rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + fMType + "'");
    }
    protected void btnChoose_Click(object sender, EventArgs e)
    {
        string fid = hfFid.Value;
        if (!string.IsNullOrEmpty(fid))
        {
            string sql = @"select top 1 XMMC,XMLX from XM_XMJBXX where XMBH='" + fid + "'";
            DataTable dt = rcXM.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtFPrjName.Text = row["XMMC"].ToString();
                //string XMLX = row["XMLX"].ToString();
                //if (PrjEntItem.DicGCLBBM.ContainsKey(XMLX))
                //    hfProjectType.Value = PrjEntItem.DicGCLBBM[XMLX];
                //else
                //    hfProjectType.Value = "2000103";
                BindForm();
            }
        }
    }
}