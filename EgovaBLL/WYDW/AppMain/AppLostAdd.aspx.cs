using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using EgovaDAO;
using Tools;
using SaveOptionEnum = Approve.EntityBase.SaveOptionEnum;
using System.Text;

public partial class WYDW_AppMain_AppLostAdd : System.Web.UI.Page
{
    public int fMType = 14402;
    RCenter rc = new RCenter();
    EgovaDB db = new EgovaDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        setPage();
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        DataTable dt = new DataTable();
        if (string.IsNullOrEmpty(fMType.ToString()))
        {
            return;
        }
        if (!string.IsNullOrEmpty(t_XMMC.ToString()))
        {
            //string strsql = "select w.Fid,w.XMSD,w.XMDZ,x.* from dbCenter.dbo.CF_App_List c " +
            //                "inner join dbCenter.dbo.YW_WY_XM_JBXX j on c.FId=j.FAppID " +
            //                "inner join XM_BaseInfo.dbo.XM_XMJBXX x on x.XMBH=j.XMBH " +
            //                "inner join dbCenter.dbo.WY_XM_JBXX w on w.XMBH=j.XMBH " +
            //                "where c.FBaseinfoId='" + CurrentEntUser.EntId + "' and c.FState='6' and x.XMMC='" + t_XMMC.Text + "'";

            string strSql = "";




            strSql = "Select * From WY_XM_JBXX Where FBaseInfoID = '" + CurrentEntUser.EntId + "' And XMMC = '" + t_XMMC.Text + "'";

            dt = rc.GetTable(strSql);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (this.Session["FBaseId"] == null)
                    return;

                string fPrjDataId = Guid.NewGuid().ToString();
                string fAppId = Guid.NewGuid().ToString();
                string FPrjId = Guid.NewGuid().ToString();
                CF_App_List lKC = new CF_App_List(); //设计企业业务
                lKC.FId = fAppId; //业务GUID
                lKC.FLinkId = dt.Rows[0]["Fid"].ToString(); ; //项目GUID
                lKC.FBaseinfoId = CurrentEntUser.EntId;
                lKC.FPrjId = dt.Rows[0]["XMBH"].ToString(); //项目编号
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
                lKC.FReportCount = 1;
                lKC.FCreateUser = CurrentEntUser.UserId;
                db.CF_App_List.InsertOnSubmit(lKC);
                db.SubmitChanges();

                //存入项目基本信息表
                StringBuilder sb = new StringBuilder();
                sb.Append("Insert Into YW_WY_XM_JBXX(FID,XMBH,XMMC,XMSD,XMDZ,JSDW,JSDWDZ,XMLX,XMZLX,JSXZ,JSMS,XMZTZ,JSGM,JSNR,BH,JSDWZZJFDM,JSDWFR,JSDWFRDH,JSDWJSFZR,JSDWJSFZRZC,JSDWJSFZRDH ,MapX  ,MapY,FSystemID,FAppID,fTime,FIsDeleted)");
                sb.Append("Select NewID(),XMBH,XMMC,XMSD,XMDZ,JSDW,JSDWDZ,XMLX,XMZLX,JSXZ,JSMS,XMZTZ,JSGM,JSNR,BH,JSDWZZJFDM,JSDWFR,JSDWFRDH,JSDWJSFZR,JSDWJSFZRZC,JSDWJSFZRDH,MapX,MapY,FSystemID,'" + fAppId + "',GetDate(),0 from WY_XM_JBXX where FID = '" + dt.Rows[0]["FID"].ToString() + "'");
                rc.PExcute(sb.ToString());

                //YW_WY_XM_JBXX xmjbxx = new YW_WY_XM_JBXX();
                //SaveOptionEnum so = SaveOptionEnum.Insert;
                //SortedList sl = new SortedList();

                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    sl.Add(dt.Columns0.ColumnName, dt.Rows0dt.Columns0.ColumnName.ToString());
                //}
                //while (sl.IndexOfValue("") != -1)
                //{
                //    sl.RemoveAt(sl.IndexOfValue(""));
                //}
                //xmjbxx.FAppID = fAppId;
                //xmjbxx.XMBH = dt.Rows0"XMBH".ToString();
                //xmjbxx.XMMC = t_XMMC.Text;
                //xmjbxx.FTime = DateTime.Now;
                //xmjbxx.FSystemId = 144;
                //xmjbxx.FCreateTime = DateTime.Now;
                //xmjbxx.FIsDeleted = 0;
                //xmjbxx.StandardStatus = 2;
                //xmjbxx.Fid = fPrjDataId;
                //xmjbxx.FSystemId = 144;
                //xmjbxx.FCreateTime = DateTime.Now;
                //xmjbxx.FIsDeleted = 0;
                //xmjbxx.StandardStatus = 2;
                //xmjbxx.XMSD = dt.Rows0"XMSD".ToString();
                //xmjbxx.XMDZ = dt.Rows0"XMDZ".ToString();
                //db.YW_WY_XM_JBXX.InsertOnSubmit(xmjbxx);
                //db.SubmitChanges();
                //sl.Add("Fid", fPrjDataId);
                //sl.Add("FAppID", fAppId);
                //sl.Add("FSystemId", 144);
                //sl.Add("FCreateTime", DateTime.Now);
                //sl.Add("FIsDeleted", 0);
                //sl.Add("StandardStatus", 2);
                //rc.SaveEBase("YW_WY_XM_JBXX", sl, "Fid", so);
                //else
                //{
                //    xmjbxx.FAppID = fAppId;
                //    xmjbxx.Fid = fPrjDataId;
                //    xmjbxx.XMBH = FPrjId;
                //    xmjbxx.XMMC = t_XMMC.Text;
                //    xmjbxx.FTime = DateTime.Now;
                //    xmjbxx.FSystemId = 144;
                //    xmjbxx.FCreateTime = DateTime.Now;
                //    xmjbxx.FIsDeleted = 0;
                //    xmjbxx.StandardStatus = 1;                    
                //}
            }
            else
            {
                tool.showMessage("该企业没有在管此项目或项目未通过审核，请重新选择！");
                return;
            }
        }

        this.Session["FIsApprove"] = 0;
        this.RegisterStartupScript(new Guid().ToString(), "<script>window.returnValue = 'ok';window.close();</script>");
        //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('dfdf')</script>");
    }

    private void setPage()
    {
        this.t_FYear.Text = DateTime.Now.Year.ToString();

        if (!Page.IsPostBack)
        { 
            
        }
    }
    protected void Btn_Search_Click(object sender, EventArgs e)
    {

    }
}