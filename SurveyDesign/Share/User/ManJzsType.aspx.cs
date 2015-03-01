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
using Approve.RuleCenter;
using System.Text;

public partial class Share_user_ManJzsType : System.Web.UI.Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowInfo();
            if (!string.IsNullOrEmpty(Request.QueryString["fcol"]) && Request.QueryString["fcol"] == "1000")
            {
                SetCheckSpecial();
                lblTitle.Text = "建造师转相关厅专业设置";
            }
            else
            {
                string vValue = Request.QueryString["v"];
                if (!string.IsNullOrEmpty(vValue))
                {
                    foreach (ListItem item in ckbType.Items)
                    {
                        item.Selected = vValue.IndexOf(item.Value) > -1;
                        item.Attributes["title"] = item.Value;
                        item.Attributes["onclick"] = "changeCheck(this)";
                    }
                }
            }
        }
    }
    void SetCheckSpecial()
    {
        string fCheckSpecial = rc.GetSignValue("select top 1 FMemo from CF_Sys_Object where fnumber ='_Sys_Con_ZSpecial'");//查询设定的转厅局的专业
        if (!string.IsNullOrEmpty(fCheckSpecial))
        {
            foreach (ListItem item in ckbType.Items)
            {
                item.Selected = fCheckSpecial.IndexOf(item.Value) > -1;
                item.Attributes["title"] = item.Value;
                item.Attributes["onclick"] = "changeCheck(this)";
            }
        }
    }
    void ShowInfo()
    {
        DataTable dt = rc.GetTable("select FName+'('+Convert(varchar,FNumber)+')' FName,FNumber from cf_sys_dic where fsystemId=165 and fparentId='109009'");
        if (dt != null)
        {
            ckbType.DataSource = dt;
            ckbType.DataTextField = "FName";
            ckbType.DataValueField = "FNumber";
            ckbType.DataBind();
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["fcol"]) && Request.QueryString["fcol"] == "1000")
        {
            SettingSave();
        }
        else
        {
            ManageDeptSave();
        }
    }
    /// <summary>
    /// 系统设置地方保存
    /// </summary>
    void SettingSave()
    {
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < ckbType.Items.Count; i++)
        {
            if (ckbType.Items[i].Selected)
            {
                if (result.Length <= 0)
                    result.Append(ckbType.Items[i].Value + "[" + ckbType.Items[i].Text + "]");
                else
                    result.Append("," + ckbType.Items[i].Value + "[" + ckbType.Items[i].Text + "]");
            }
        }
        if (rc.GetSQLCount("select count(*) from CF_Sys_Object where fnumber ='_Sys_Con_ZSpecial'") > 0)
        {
            rc.PExcute("update CF_Sys_Object set FMemo='" + result + "' where fnumber='_Sys_Con_ZSpecial'");
        }
        else
        {
            rc.PExcute("insert into CF_Sys_Object (FId,FName,FContent,FMemo,FNumber,Forder,FTime,FCreateTime,FIsDeleted) values (newid(),'建造师转厅专业设置','不可在此修改','" + result + "','_Sys_Con_ZSpecial',100,getdate(),getdate(),0)");
        }
        this.RegisterStartupScript("js", "<script>alert(\"设定成功！\");</script>");
    }
    /// <summary>
    /// 管理用户地方保存
    /// </summary>
    void ManageDeptSave()
    {

        StringBuilder result = new StringBuilder();
        StringBuilder resultTxt = new StringBuilder();
        for (int i = 0; i < ckbType.Items.Count; i++)
        {
            if (ckbType.Items[i].Selected)
            {
                string txt = ckbType.Items[i].Text.Trim();
                if (result.Length > 0)
                {
                    result.Append(",");
                    resultTxt.Append(",");
                }
                result.Append(ckbType.Items[i].Value);
                resultTxt.Append(txt.Substring(0, txt.IndexOf('(')));
            }
        }
        if (result.Length <= 0)
            this.RegisterStartupScript("js", "<script>alert(\"请选择指定的专业！\");</script>");
        else
        {
            result.Append("~");
            result.Append(resultTxt);
            this.RegisterStartupScript("js", "<script>window.returnValue=\"" + result + "\";window.close();</script>");
        }
    }
}
