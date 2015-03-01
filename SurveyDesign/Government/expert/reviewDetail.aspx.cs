using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Approve.Common;
using Approve.RuleCenter;

public partial class Government_expert_reviewDetail : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["FLinkId"] != null && !string.IsNullOrEmpty(Request["FLinkId"]))
            { t_YWBM.Value = Request["FLinkId"]; }
            if (Request["psid"] != null && !string.IsNullOrEmpty(Request["psid"]))
            { t_psid.Value = Request["psid"]; }
            if (Request["isEnd"] != null && !string.IsNullOrEmpty(Request["isEnd"]))
            {
                t_isEnd.Value = Request["isEnd"];
                if (t_isEnd.Value == "1" || t_isEnd.Value == "-1")
                    readOnly();
            }
            bind();
        }
    }

    public void readOnly()
    {
        rblOne.Enabled = false; tbOne.Enabled = false;
        rblTWO.Enabled = false; tbTWO.Enabled = false;
        rblThree.Enabled = false; tbThree.Enabled = false;
        rblFour.Enabled = false; tbThree.Enabled = false;
        rblFive.Enabled = false; tbFive.Enabled = false;
        rblSix.Enabled = false; tbSix.Enabled = false;
        rblSeven.Enabled = false; tbSeven.Enabled = false;
        rblEghit.Enabled = false; tbEghit.Enabled = false; t_Fresult.Enabled = false;
        btnNo.Enabled = false; btnOK.Enabled = false; btnSave.Enabled = false;
    }

    public void bind()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = string.Format(@"select j.FName,CONVERT(nvarchar(10),l.FReportDate,121) FReportDate
                    ,j.GFMC,j.FListName,j.FTypeName,j.Linkman,j.LinkmanMobile,e.Fresult
                    from YW_GF_Expert e
                    left join YW_GF_JBQK j on j.YWBM=e.Fappid
                    left join CF_App_List l on j.YWBM=l.FId
                     where e.PsID='" + t_psid.Value + "' ");
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
        }
        bindDetail();
    }
    public void bindDetail()
    {
        string sql = string.Format(@"select * from YW_GF_ExpertDetail
                     where PsID='" + t_psid.Value + "' and Fappid='" + t_YWBM.Value + "' and FSteps='zj' ");
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["FType"].ToString())
                {
                    case "1":
                        rblOne.SelectedValue = rblOne.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbOne.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_one.Value = dt.Rows[i]["FID"].ToString();
                        break;
                    case "2":
                        rblTWO.SelectedValue = rblTWO.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbTWO.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_TWO.Value = dt.Rows[i]["FID"].ToString();
                        break;
                    case "3":
                        rblThree.SelectedValue = rblThree.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbThree.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_Three.Value = dt.Rows[i]["FID"].ToString();
                        break;
                    case "4":
                        rblFour.SelectedValue = rblFour.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbFour.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_Four.Value = dt.Rows[i]["FID"].ToString();
                        break;
                    case "5":
                        rblFive.SelectedValue = rblFive.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbFive.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_Five.Value = dt.Rows[i]["FID"].ToString();
                        break;
                    case "6":
                        rblSix.SelectedValue = rblSix.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbSix.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_Six.Value = dt.Rows[i]["FID"].ToString();
                        break;
                    case "7":
                        rblSeven.SelectedValue = rblSeven.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbSeven.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_Seven.Value = dt.Rows[i]["FID"].ToString();
                        break;
                    case "8":
                        rblEghit.SelectedValue = rblEghit.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbEghit.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_Eghit.Value = dt.Rows[i]["FID"].ToString();
                        break;
                }
            }
        }
    }
    protected void btnSee_Click(object sender, EventArgs e)
    {
        this.Session["FAppId"] = t_YWBM.Value;
        this.Session["FManageTypeId"] = "4000";
        Session["FIsApprove"] = 1;
        Response.Write("<script language='javascript'>window.open('../../GFEnt/AppMain/aIndex.aspx');</script>");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        try { saveIdear(); tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';"); }
        catch (Exception ex) { tool.showMessage("保存失败"); }
    }
    public void saveIdear()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = null;
        if (!string.IsNullOrEmpty(t_Fresult.Text.Trim()))
        {
            sql += string.Format(@" update YW_GF_Expert set Fresult = '" + t_Fresult.Text
              + "',Ftime=getdate() where Fappid='" + t_YWBM.Value + "' and PsID='" + t_psid.Value + "' ;");
        }

        if (!string.IsNullOrEmpty(ts_one.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblOne.SelectedValue
              + "',Fremark='" + tbOne.Text + "',FTime=getdate() where FID='" + ts_one.Value + "' ;");
        }
        else
        {
            ts_one.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,PsID,FSteps)
                            values ('" + ts_one.Value + "',1,'" + rblOne.SelectedValue
                                       + "','" + tbOne.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_psid.Value + "','zj') ;");
        }
        if (!string.IsNullOrEmpty(ts_TWO.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblTWO.SelectedValue
              + "',Fremark='" + tbTWO.Text + "',FTime=getdate() where FID='" + ts_TWO.Value + "' ;");
        }
        else
        {
            ts_TWO.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,PsID,FSteps)
                            values ('" + ts_TWO.Value + "',2,'" + rblTWO.SelectedValue
                                       + "','" + tbTWO.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_psid.Value + "','zj') ;");
        }
        if (!string.IsNullOrEmpty(ts_Three.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblThree.SelectedValue
              + "',Fremark='" + tbThree.Text + "',FTime=getdate() where FID='" + ts_Three.Value + "' ;");
        }
        else
        {
            ts_Three.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,PsID,FSteps)
                            values ('" + ts_Three.Value + "',3,'" + rblThree.SelectedValue
                                       + "','" + tbThree.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_psid.Value + "','zj') ;");
        }
        if (!string.IsNullOrEmpty(ts_Four.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblFour.SelectedValue
              + "',Fremark='" + tbFour.Text + "',FTime=getdate() where FID='" + ts_Four.Value + "' ;");
        }
        else
        {
            ts_Four.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,PsID,FSteps)
                            values ('" + ts_Four.Value + "',4,'" + rblFour.SelectedValue
                                       + "','" + tbFour.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_psid.Value + "','zj') ;");
        }
        if (!string.IsNullOrEmpty(ts_Five.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblFive.SelectedValue
              + "',Fremark='" + tbFive.Text + "',FTime=getdate() where FID='" + ts_Five.Value + "' ;");
        }
        else
        {
            ts_Five.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,PsID,FSteps)
                            values ('" + ts_Five.Value + "',5,'" + rblFive.SelectedValue
                                       + "','" + tbFive.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_psid.Value + "','zj') ;");
        }
        if (!string.IsNullOrEmpty(ts_Six.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblSix.SelectedValue
              + "',Fremark='" + tbSix.Text + "',FTime=getdate() where FID='" + ts_Six.Value + "' ;");
        }
        else
        {
            ts_Six.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,PsID,FSteps)
                            values ('" + ts_Six.Value + "',6,'" + rblSix.SelectedValue
                                       + "','" + tbSix.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_psid.Value + "','zj') ;");
        }
        if (!string.IsNullOrEmpty(ts_Seven.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblSeven.SelectedValue
              + "',Fremark='" + tbSeven.Text + "',FTime=getdate() where FID='" + ts_Seven.Value + "' ;");
        }
        else
        {
            ts_Seven.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,PsID,FSteps)
                            values ('" + ts_Seven.Value + "',7,'" + rblSeven.SelectedValue
                                       + "','" + tbSeven.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_psid.Value + "','zj') ;");
        }
        if (!string.IsNullOrEmpty(ts_Eghit.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblEghit.SelectedValue
              + "',Fremark='" + tbEghit.Text + "',FTime=getdate() where FID='" + ts_Eghit.Value + "' ;");
        }
        else
        {
            ts_Eghit.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,PsID,FSteps)
                            values ('" + ts_Eghit.Value + "',8,'" + rblEghit.SelectedValue
                                       + "','" + tbEghit.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_psid.Value + "','zj') ;");
        }
        rc.PExcute(sql);
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        saveIdear();
        string sql = " update YW_GF_ExpertDetail set isEnd=1 where Fappid='" + t_YWBM.Value + "' and PsID='" + t_psid.Value + "' ;";
        sql += " update YW_GF_Expert set isEnd=1 where Fappid='" + t_YWBM.Value + "' and PsID='" + t_psid.Value + "' ;";
        if (rc.PExcute(sql))
        { tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("保存失败"); }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        saveIdear();
        string sql = " update YW_GF_ExpertDetail set isEnd=-1 where Fappid='" + t_YWBM.Value + "' and PsID='" + t_psid.Value + "' ;";
        sql += " update YW_GF_Expert set isEnd=-1 where Fappid='" + t_YWBM.Value + "' and PsID='" + t_psid.Value + "' ;";
        if (rc.PExcute(sql))
        { tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("保存失败"); }
    }
}