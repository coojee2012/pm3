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
    private int fMType = 6060;
    private ProjectDB db = new ProjectDB();
    private RCenter rc = new RCenter();
    private RCenter rcXM = new RCenter("XM_BaseInfo");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string year = DateTime.Now.ToString("yyyy");
            //t_FYear.Text = year;
            //t_FName.Text = year + "工程规划申请";
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string FId = hfFid.Value;
        string projectType = hfProjectType.Value;
        int type = 0;
        if (projectType == "2000101") //房屋建筑
        {
            type = 1;
        }
        else if (projectType == "2000102")//市政工程
        {
            type = 2;
            fMType = 6070;
        }
        else if (projectType == "2000103")//其它
            type = 3;
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
        var param = "?GC_Id=" + guid + "&fAppId=" + fAppId;
        if (!string.IsNullOrEmpty(FId))
        {
            string sql = "EXEC SP_GCGH_AUTOCREATE @ID,@FBaseInfoId,@YWBM,@TYPE,@XMBH";//自动写入数据
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = guid, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@FBaseInfoId", Value = CurrentEntUser.EntId, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YWBM", Value = fAppId, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@TYPE", Value = type, SqlDbType = SqlDbType.Int });
            list.Add(new SqlParameter() { ParameterName = "@XMBH", Value = FId, SqlDbType = SqlDbType.VarChar });
            rc.PExcute(sql, list.ToArray());
            #region
            //            string sql = @"select top 1 * from TC_Prj_Info where Fid='" + FId + "'";
            //            DataTable dt = rc.GetTable(sql);
            //            if (dt != null && dt.Rows.Count > 0)
            //            {
            //                string GCGHBM = Guid.NewGuid().ToString();
            //                DataRow row = dt.Rows[0];
            //                string insertSql = string.Format(@"INSERT INTO [dbCenter].[dbo].[YW_GCGH]
            //                                                        ([ID]
            //                                                        ,[FBaseInfoId]
            //                                                        ,[YWBM]
            //                                                        ,[GCGHMC]
            //                                                        ,[JSDWMC]
            //                                                        ,[JSDWDZ]
            //                                                        ,[LXR]
            //                                                        ,[LXDH]
            //                                                        ,[XMMC]
            //                                                        ,[JSDZ]
            //                                                        ,[JZZMJ]
            //                                                        ,[LXWH]
            //                                                        ,[LXSJ]
            //                                                        ,[XMBH]
            //                                                        ,[XMNR]
            //                                                        ,[ProjectType]
            //                                                        ,GCGHBM
            //                                                        ,XMSD
            //                                                        ,BH
            //                                                        )
            //                                                    VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',{15},'{16}',{17},'{18}')", guid, CurrentEntUser.EntId, fAppId, txtFPrjName.Text, row["JSDW"], row["JSDWDZ"], row["Contacts"], row["Mobile"], row["ProjectName"], row["Address"], row["Area"], row["ProjectNumber"], row["ProjectTime"], row["FId"], row["ConstrContent"], type, GCGHBM,row["AddressDept"], row["ProjectNo"]);
            //                if (type == 1)
            //                {
            //                    insertSql += string.Format(@"insert into YW_FILE(YWBM,[FILE_NAME])
            //                                select '{0}',[FILE_NAME] from MB_ZMCL where TypeId=5 order by OrderId", guid);
            //                }
            //                else
            //                {
            //                    insertSql += string.Format(@"insert into YW_FILE(YWBM,[FILE_NAME])
            //                                select '{0}',[FILE_NAME] from MB_ZMCL where TypeId=6 order by OrderId", guid);
            //                }
            //                rc.PExcute(insertSql);
            #endregion
            if (type == 1)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>parent.parent.document.location='AppMain/aIndex.aspx" + param + "';</script>");
            else if (type == 2)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>parent.parent.document.location='../ApplyGCGHSZ/AppMain/aIndex.aspx" + param + "';</script>");
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>parent.parent.document.location='AppMain/aIndex.aspx" + param + "';</script>");
            // }
        }
    }
    private void BindForm()
    {
        string projectType = hfProjectType.Value;
        if (projectType == "2000101") //房屋建筑
            fMType = 6060;
        else if (projectType == "2000102")//市政工程
            fMType = 6070;
        else if (projectType == "2000103")//其它
            fMType = 6060;
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
                string XMLX = row["XMLX"].ToString();
                if (PrjEntItem.DicGCLBBM.ContainsKey(XMLX))
                    hfProjectType.Value = PrjEntItem.DicGCLBBM[XMLX];
                else
                    hfProjectType.Value = "2000103";
                BindForm();
            }
        }
    }
}