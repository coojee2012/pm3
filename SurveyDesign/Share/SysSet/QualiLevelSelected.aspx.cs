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
using Approve.Common;
using Approve.EntityBase;

public partial class Admin_main_QualiLevelSelected : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindControl();
            ControlSelect();
            if (Request["fid"] == null || Request["fid"] == "")
            {
                return;
            }
        }
    }
    private void BindControl()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fname ,fnumber from CF_Sys_QualiLevel where fisdeleted=0");
        if (Request["fmatypeid"] != null && Request["fmatypeid"] != "")
        {
            if (Request["fmatypeid"] == "300")//商混预制
                sb.Append(" and FSystemId=100 ");
            else if (Request["fmatypeid"] == "650")//入规划
                sb.Append(" and FSystemId=620 ");
            else
                sb.Append(" and FSystemId= " + Request["fmatypeid"]);
        }
        sb.Append(" order by FTime desc");
        DataTable dt = sh.GetTable(sb.ToString());
        this.CheckBoxList1.DataSource = dt;
        this.CheckBoxList1.DataTextField = "fname";
        this.CheckBoxList1.DataValueField = "fnumber";
        this.CheckBoxList1.DataBind();
    }
    private void ControlSelect()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FLevelId from CF_App_QualiLevel where fisdeleted=0 and FProcessId='" + Request["fid"] + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        sb.Remove(0, sb.Length);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < this.CheckBoxList1.Items.Count; j++)
                {
                    if (dt.Rows[i]["FLevelId"].ToString().Trim() == CheckBoxList1.Items[j].Value)
                    {
                        CheckBoxList1.Items[j].Selected = true;
                    }
                }
            }
        }
    }
    private void SaveInfo()
    {
        if (Request["fid"] == null || Request["fid"] == "")
        {
            return;
        }
        pageTool tool = new pageTool(this.Page);
        int selectCount = 0;
        ArrayList selectValue = new ArrayList();
        ArrayList selectName = new ArrayList();
        StringBuilder sReturnValue = new StringBuilder();
        StringBuilder sb = new StringBuilder();
        sb.Append("delete from CF_App_QualiLevel where fProcessId='" + Request["fid"] + "'");
        sh.PExcute(sb.ToString());
        sb.Remove(0, sb.Length);
        for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
        {
            if (this.CheckBoxList1.Items[i].Selected == true)
            {
                selectCount++;
                selectValue.Add(this.CheckBoxList1.Items[i].Value);
                selectName.Add(this.CheckBoxList1.Items[i].Text);
            }
        }
        if (selectCount == 0)
        {
            tool.showMessage("请选择");
            return;
        }
        else
        {
            SortedList[] sl = new SortedList[selectCount];
            string[] fkey = new string[selectCount];
            SaveOptionEnum[] so = new SaveOptionEnum[selectCount];
            EntityTypeEnum[] en = new EntityTypeEnum[selectCount];
            for (int j = 0; j < selectCount; j++)
            {
                sl[j] = new SortedList();
                sl[j].Add("FID", Guid.NewGuid().ToString());
                sl[j].Add("FLevelId", selectValue[j].ToString());
                sl[j].Add("FProcessId", Request["fid"]);
                sl[j].Add("FIsDeleted", 0);
                sl[j].Add("FCreateTime", DateTime.Now);
                fkey[j] = "FID";
                so[j] = SaveOptionEnum.Insert;
                en[j] = EntityTypeEnum.EaQualiLevel;
                sReturnValue.Append(selectName[j].ToString() + "^");
            }
            if (sh.SaveEBaseM(en, sl, fkey, so))
            {
                tool.showMessageAndRunFunction("保存成功", "selectWindowReturnValue('" + sReturnValue.ToString() + "');");
            }
            else
            {
                tool.showMessage("保存失败");
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
}
