using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.EntityBase;
using System.Data.SqlClient;
using System.Linq;
using ProjectData;
using Tools;

public partial class Share_WebSide_Article1 : System.Web.UI.Page
{
    Share rc = new Share();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }

    //显示
    public void showInfo()
    {
        string FID = Request.QueryString["fid"];
        if (string.IsNullOrEmpty(FID))
        {//如果传的是FCol则从该栏目中得到最头一篇文章
            Response.Clear();
            Response.End();
        }
        pageTool tool = new pageTool(this.Page, "t_");
        CF_Prj_Data prj = db.CF_Prj_Data.Where(t => t.FAppId == FID).FirstOrDefault();



        if (prj != null)
        {
            CF_Prj_BaseInfo prj1 = db.CF_Prj_BaseInfo.Where(t => t.FId == prj.FPrjId).FirstOrDefault();
            if (prj1 != null)
            {
                tool.fillPageControl(prj1);
                this.t_FAllAddress.Text = rc.getDept(prj1.FAddressDept, 1) + prj1.FAllAddress;
                this.t_FLevel.Text = rc.getDicNameByFNumber(EConvert.ToString(prj1.FLevel));
                this.t_FStruType.Text = rc.getDicNameByFNumber(EConvert.ToString(prj1.FStruType));
                this.t_JSMS.Text = rc.getDicNameByFNumber(EConvert.ToString(prj1.JSMS));
                this.t_FFunds.Text = rc.getDicNameByFNumber(EConvert.ToString(prj1.FFunds));
                this.t_FKind.Text = rc.getDicNameByFNumber(EConvert.ToString(prj1.FKind));
                this.t_FScale.Text = rc.getDicNameByFNumber(EConvert.ToString(prj1.FScale));
                //t_FNature.Text = rc.getDicNameByFNumber(EConvert.ToString(prj1.FNature));
                t_FAntiSeismic.Text = rc.getDicNameByFNumber(EConvert.ToString(prj1.FAntiSeismic));
            }
            ShowPrjEnt("e_", FID, 100);
            ShowPrjEnt("k_", FID, 155);
            CF_App_List appHT = db.CF_App_List.Where(t => t.FId == FID).FirstOrDefault();


            CF_App_List app = db.CF_App_List.Where(t => t.FLinkId == appHT.FLinkId && t.FManageTypeId == 298 && t.FState == 6).FirstOrDefault();
            if (app != null)
            {
                this.Msg.Text = "已完成";
            }
            else
            {
                this.Msg.Text = "正在办理";
            }


        }

    }


    private void ShowPrjEnt(string tag, string fAppId, int entType)
    {
        var ent = db.CF_Prj_Ent.Where(t => t.FAppId == fAppId && t.FEntType == entType).FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, tag);
            tool.fillPageControl(ent);
        }
    }
}
