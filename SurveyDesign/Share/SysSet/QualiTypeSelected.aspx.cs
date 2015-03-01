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

public partial class Admin_main_QualiTypeSelected : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDg();
            ControlSelect();
            if (Request["fid"] != null && Request["fid"] == "")
            {
                return;
            }
        }

    }
    private void BindDg()
    {
        DataTable dt = null;
        string fmatypeid = Request.QueryString["fmatypeid"];
        if (!string.IsNullOrEmpty(fmatypeid))
        {
            switch (fmatypeid)
            {
                case "100":
                case "300":
                    dt = sh.getDicTbByFNumber("105");
                    break;
                case "101":
                    dt = sh.getDicTbByFNumber("130");
                    break;

                case "105":
                    dt = sh.getDicTbByFNumber("135");
                    break;

                case "108":
                    dt = sh.getDicTbByFNumber("136");
                    break;

                case "150":
                    dt = sh.getDicTbByFNumber("137");
                    break;

                case "160":
                    dt = sh.getDicTbByFNumber("131");
                    break;

                case "170":
                    dt = sh.getDicTbByFNumber("139");
                    break;

                case "180":
                    dt = sh.getDicTbByFNumber("138");
                    break;

                case "190":
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" select fid,fname,fnumber from cf_sys_dic ");
                    sb.Append(" where fparentid in ");
                    sb.Append(" (134001,134002) ");
                    sb.Append(" order by fparentid,forder ");
                    dt = sh.GetTable(sb.ToString());
                    break;

                case "191": //勘查设计报表
                    dt = sh.getDicTbByFNumber("173");
                    break;

                case "192": //勘查设计证书变更
                    dt = sh.getDicTbByFNumber("201");
                    break;

 

                case "200":
                    dt = sh.getDicTbByFNumber("133");
                    break;


                case "210":
                    dt = sh.getDicTbByFNumber("132");
                    break;

                case "240":
                    dt = sh.getDicTbByFNumber("200");
                    break;


                case "250":
                    dt = sh.getDicTbByFNumber("202");
                    break;

                case "500":
                    dt = sh.getDicTbByFNumber("204");
                    break;

                //case "300":
                //    dt = sh.getDicTbByFNumber("205");
                //    break;

                case "320":
                    dt = sh.getDicTbByFNumber("206");
                    break;


                case "600":
                    dt = sh.getDicTbByFNumber("310");
                    break;

                case "220":
                    dt = sh.getDicTbByFNumber("350");
                    break;

                case "370":
                    dt = sh.getDicTbByFNumber("208");
                    break;

                case "350":
                    dt = sh.getDicTbByFNumber("207");
                    break;

                case "620":
                case "650":
                    dt = sh.getDicTbByFNumber("320");
                    break;

                case "910":
                    dt = sh.getDicTbByFNumber("193");
                    break;
                case "682":
                    dt = sh.getDicTbByFNumber("567");
                    break;
                case "683"://家装企业
                    dt = sh.getDicTbByFNumber("265");
                    break;
                case "727"://竣工结算
                    dt = sh.getDicTbByFNumber("482");
                    break;
                case "167"://规划师
                    dt = sh.getDicTbByFNumber("246");
                    break;
                case "460"://外来园林
                    dt = sh.getDicTbByFNumber("568");
                    break;
                case "221"://项目经理
                    dt = sh.getDicTbByFNumber("244");
                    break;
                default:
                    dt = sh.getDicTbByFNumber(fmatypeid);
                    break;
            }
        }


        this.Dg_List.DataSource = dt;
        this.Dg_List.DataBind();
    }
    private void ControlSelect()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FQualiTypeId from CF_App_QualiType where fisdeleted=0 and FProcessId='" + Request["fid"] + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        sb.Remove(0, sb.Length);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return;
        }
        else
        {
            int itemCount = this.Dg_List.Items.Count;
            for (int i = 0; i < itemCount; i++)
            {
                CheckBoxList checkBoxInfo = (CheckBoxList)Dg_List.Items[i].Cells[0].FindControl("checkBoxInfo");
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    for (int j = 0; j < checkBoxInfo.Items.Count; j++)
                    {
                        if (dt.Rows[k]["FQualiTypeId"].ToString().Trim() == checkBoxInfo.Items[j].Value)
                        {
                            checkBoxInfo.Items[j].Selected = true;
                        }
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
        sb.Append("delete from CF_App_QualiType where fProcessId='" + Request["fid"] + "'");
        sh.PExcute(sb.ToString());
        sb.Remove(0, sb.Length);

        int itemCount = this.Dg_List.Items.Count;
        for (int i = 0; i < itemCount; i++)
        {
            Label la_Tilte = (Label)Dg_List.Items[i].Cells[0].FindControl("la_Tilte");
            CheckBoxList checkBoxInfo = (CheckBoxList)Dg_List.Items[i].Cells[0].FindControl("checkBoxInfo");

            for (int j = 0; j < checkBoxInfo.Items.Count; j++)
            {
                if (checkBoxInfo.Items[j].Selected == true)
                {
                    selectCount++;
                    selectValue.Add(checkBoxInfo.Items[j].Value);
                    selectName.Add(la_Tilte.Text.Trim() + " | " + checkBoxInfo.Items[j].Text);
                }
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
                sl[j].Add("FQualiTypeId", selectValue[j].ToString());
                sl[j].Add("FProcessId", Request["fid"]);
                sl[j].Add("FIsDeleted", 0);
                sl[j].Add("FCreateTime", DateTime.Now);
                fkey[j] = "FID";
                so[j] = SaveOptionEnum.Insert;
                en[j] = EntityTypeEnum.EaQualiType;
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
    protected void Dg_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fNumber = e.Item.Cells[e.Item.Cells.Count - 2].Text;

            Label la_Tilte = (Label)e.Item.Cells[0].FindControl("la_Tilte");
            CheckBoxList checkBoxInfo = (CheckBoxList)e.Item.Cells[0].FindControl("checkBoxInfo");
            DataTable dt = sh.getDicTbByFNumber(fNumber);
            if (dt != null && dt.Rows.Count > 0)
            {
                checkBoxInfo.DataSource = dt;
                checkBoxInfo.DataTextField = "FName";
                checkBoxInfo.DataValueField = "FNumber";
                checkBoxInfo.DataBind();
            }
            else
            {
                la_Tilte.Text += "&nbsp;&nbsp;&nbsp;&nbsp;<font color='red'>该项下没有选项</font>";
            }

        }
    }
}

