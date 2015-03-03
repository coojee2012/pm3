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
    private int fMType = 7070;
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
        string projectType = hfProjectType.Value;
        int type = 0;
        if (projectType == "2000101") //房屋建筑
        {
            type = 1;
        }
        else if (projectType == "2000102")//市政工程
        {
            type = 2;
            fMType = 7080;
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

        if (!string.IsNullOrEmpty(FId))
        {
            string sql = "EXEC SP_JGYS_AUTOCREATE @ID,@FBaseInfoId,@YWBM,@TYPE,@FID,@XMBH";//自动写入数据
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter() { ParameterName = "@ID", Value = guid, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@FBaseInfoId", Value = CurrentEntUser.EntId, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@YWBM", Value = fAppId, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@TYPE", Value = type, SqlDbType = SqlDbType.Int });
            list.Add(new SqlParameter() { ParameterName = "@FID", Value = FId, SqlDbType = SqlDbType.VarChar });
            list.Add(new SqlParameter() { ParameterName = "@XMBH", Value = hfProId.Value, SqlDbType = SqlDbType.VarChar });
            rc.PExcute(sql, list.ToArray());
           // System.IO.File.AppendAllText("C:\\yujiajun.log", "FID:" + FId + "|TYPE:" + type, System.Text.Encoding.Default);
            var param = "?JG_Id=" + guid + "&fAppId=" + fAppId + "&projectId=" + hfProId.Value;
            if (type == 1)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>parent.parent.document.location='AppMain/aIndex.aspx" + param + "';</script>");
            else if (type == 2)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>parent.parent.document.location='../ApplyJGYSSZ/AppMain/aIndex.aspx" + param + "';</script>");
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>parent.parent.document.location='AppMain/aIndex.aspx" + param + "';</script>");
        }
    }
    private void BindForm()
    {
        string projectType = hfProjectType.Value;
        if (projectType == "2000101") //房屋建筑
            fMType = 7070;
        else if (projectType == "2000102")//市政工程
            fMType = 7080;
        else if (projectType == "2000103")//其它
            fMType = 7070;
        //pageTool tool = new pageTool(this.Page);
//        string sqlY = string.Format(@"select top 1 FYear
//                from YW_CF_DICtime where convert(nvarchar(10),GETDATE(),121) >= convert(nvarchar(10),FStime,121) 
//                and convert(nvarchar(10),GETDATE(),121) <= convert(nvarchar(10),FEtime,121)  ");
//        DataTable dt = rc.GetTable(sqlY);
//        if (dt != null && dt.Rows.Count > 0)
//        {
//            t_FYear.Text = dt.Rows[0]["FYear"].ToString();
//        }
        t_FYear.Text = DateTime.Now.ToString("yyyy");
        this.t_FName.Text = t_FYear.Text + " " + rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + fMType + "'");
    }
    protected void btnChoose_Click(object sender, EventArgs e)
    {
        string fid = hfFid.Value;
        if (!string.IsNullOrEmpty(fid))
        {
            string sql = @"select top 1 A.XMBH,A.DWGCMC,B.XMLX  from GC_DWGCXX A LEFT JOIN XM_XMJBXX B ON A.XMBH=B.XMBH WHERE A.DWGCBH='" + fid + "'";
            DataTable dt = rcXM.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtFPrjName.Text = row["DWGCMC"].ToString();
                hfProId.Value = row["XMBH"].ToString();
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