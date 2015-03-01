using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Approve.Common;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class JNCLEnt_ApplyInfo_produceEntInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnSave.Attributes.Add("onclick", "if(checkInfo()){return true;}else{return false;}");
            bindCom(); showInfo();
        }
    }
    public void bindCom()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FId,FName,FNumber,FCNumber,FSystemId,FOrder,");
        sb.Append("(select top 1 fname from CF_Sys_SystemName where fnumber=fsystemid) FSystemName ");
        sb.Append("From CF_Sys_DicClass ");
        sb.Append("Where FIsDeleted=0 ");
        sb.Append(" and fsystemid=222001 ");
        sb.Append("Order By Forder,FTime Desc");
        t_JJTypeID.DataSource = rc.GetTable(sb.ToString());
        this.t_JJTypeID.DataTextField = "FName";
        this.t_JJTypeID.DataValueField = "FNumber";
        this.t_JJTypeID.DataBind();
        this.t_JJTypeID.Items.Insert(0, new ListItem("请选择", ""));
    }
    public void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "select * from YW_JN_SCQYInfo where fbaseid='" + CurrentEntUser.EntId + "' ";
        DataTable dt = sh.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            if (t_JJTypeID.Items.FindByValue(dt.Rows[0]["JJTypeID"].ToString()) != null)
                t_JJTypeID.SelectedValue = t_JJTypeID.Items.FindByValue(dt.Rows[0]["JJTypeID"].ToString()).Value;
            t_FMangeDeptId.fNumber = dt.Rows[0]["FMangeDeptId"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = ""; pageTool tool = new pageTool(this.Page, "t_");
        if (!string.IsNullOrEmpty(t_fid.Value))
        {
            sql += string.Format(@" update YW_JN_SCQYInfo set FMangeDeptId='" + t_FMangeDeptId.fNumber + "', QYMC='" + t_QYMC.Text
                + "', address='" + t_address.Text + "', JYDZ='" + t_JYDZ.Text + "', YYZZ='" + t_YYZZ.Text + "', ZZJG='" + t_ZZJG.Text
                + "', postCode='" + t_postCode.Text + "', QYWZ='" + t_QYWZ.Text + "', JJTypeID='" + t_JJTypeID.SelectedValue
                + "', JJType='" + t_JJTypeID.SelectedItem.Text + "', CLTime='" + t_CLTime.Text + "', ZCZJ='" + t_ZCZJ.Text + "', BZ='" + t_BZ.SelectedValue
                + "', QYRS='" + t_QYRS.Text + "', SCNL='" + t_SCNL.Text + "', LJSCL='" + t_LJSCL.Text + "', CFDZ='" + t_CFDZ.Text
                + "', CFMJ='" + t_CFMJ.Text + "', FR='" + t_FR.Text + "', SJ='" + t_SJ.Text + "', ZJType='" + t_ZJType.SelectedValue
                + "', ZJCode='" + t_ZJCode.Text + "', linkMan='" + t_linkMan.Text + "', linkZW='" + t_linkZW.Text + "', linkPho='" + t_linkPho.Text
                + "', linkFax='" + t_linkFax.Text + "', JYFW='" + t_JYFW.Text + "', JYFW2='" + t_JYFW2.Text + "', fbaseid='"
                + CurrentEntUser.EntId + "', editTime=getdate() where fid='" + t_fid.Value);
        }
        else
        {
            t_fid.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_JN_SCQYInfo (FID, FMangeDeptId, QYMC, address, JYDZ, YYZZ, ZZJG, postCode
                    , QYWZ, JJTypeID, JJType, CLTime, ZCZJ, BZ, QYRS, SCNL, LJSCL, CFDZ, CFMJ, FR, SJ, ZJType, ZJCode
                    , linkMan, linkZW, linkPho, linkFax, JYFW, JYFW2, Fappid, fbaseid, ftime, editTime) values ('"
                    + t_fid.Value + "', '" + t_FMangeDeptId.fNumber + "', '" + t_QYMC.Text + "', '" + t_address.Text + "', '" + t_JYDZ.Text + "', '" + t_YYZZ.Text + "', '" + t_ZZJG.Text
                    + "', '" + t_postCode.Text + "', '" + t_QYWZ.Text + "', '" + t_JJTypeID.SelectedValue + "', '" + t_JJTypeID.SelectedItem.Text
                    + "', '" + t_CLTime.Text + "', '" + t_ZCZJ.Text + "', '" + t_BZ.SelectedValue + "', '" + t_QYRS.Text + "', '" + t_SCNL.Text + "', '" + t_LJSCL.Text
                    + "', '" + t_CFDZ.Text + "','" + t_CFMJ.Text + "', '" + t_FR.Text + "', '" + t_SJ.Text + "', '" + t_ZJType.SelectedValue + "', '" + t_ZJCode.Text + "', '" + t_linkMan.Text
                    + "', '" + t_linkZW.Text + "', '" + t_linkPho.Text + "', '" + t_linkFax.Text + "', '" + t_JYFW.Text + "', '" + t_JYFW2.Text + "', 'JBQK', '"
                    + CurrentEntUser.EntId + "', getdate(),getdate())");
        }
        if (sh.PExcute(sql))
        { tool.showMessage("保存成功"); showInfo(); }
        else
        { tool.showMessage("保存失败"); }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}