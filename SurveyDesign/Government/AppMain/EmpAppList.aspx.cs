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
using Approve.EntityCenter;
using Approve.RuleApp;
using ProjectData;
using System.Linq;

public partial class Government_AppMain_EmpAppList : govBasePage
{
    RCenter rc = new RCenter();
    SaveAsBase sab = new SaveAsBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            ControlBind();
            ShowInfo();
            ShowPostion();
        }
    }

    private void ShowPostion()
    {
        if (Request["fcol"] != null && Request["fcol"] != "")
        {
            lPostion.Text = rc.GetMenuName(Request["fcol"]);
        }
    }

    private void ControlBind()
    {
    }
    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(txtFName.Text.Trim()))
        {
            sb.Append(" and e.FName like '%" + txtFName.Text + "%' ");
        }
        if (!string.IsNullOrEmpty(txtFEntName.Text.Trim()))
        {
            sb.Append(" and b.FName like '%" + txtFEntName.Text + "%' ");
        }
        if (!string.IsNullOrEmpty(ddlFState.SelectedValue))
        {
            sb.Append(" and e.FState='" + ddlFState.SelectedValue + "'");
        }
        if (!string.IsNullOrEmpty(ddlFSystemId.SelectedValue))
        {
            sb.Append(" and exists (select 1 from cf_Sys_Userright where FBaseInfoId=b.FId and FSystemId='" + ddlFSystemId.SelectedValue + "') ");
        }
        return sb.ToString();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select e.FId,e.FName,b.FName FEntName,e.FIdCard,");
        sb.Append("e.FTime,e.FState,case isnull(e.FUserName,'') when '' then '--' else e.FUserName end FUserName ");
        sb.Append("from cf_Emp_BaseInfo e inner join ");
        sb.Append("cf_Ent_BaseInfo b on e.FBaseInfoId=b.FId ");
        sb.Append("where e.FType=3 and e.FState>0 ");
        sb.Append(getCondi());
        sb.Append("order by e.FState,e.FTime desc");
        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + Pager1.pagecount * (Pager1.curpage - 1)).ToString();
            //姓名
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('EmpInfo.aspx?fid=" + FID + "',600,400,document.getElementById('btnReload2'));\">" + e.Item.Cells[2].Text + "</a>";
            string fDesc = string.Empty;
            CheckBox box = e.Item.Cells[0].Controls[1] as CheckBox;
            int fstate = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FState"));
            if (fstate == 1)
            {
                fDesc = "未审核";
                box.Enabled = true;
            }
            else if (fstate == 2)
            {
                fDesc = "<a href='javascript:void(0)' onclick=\"showAddWindow('../AppQualiInfo/ShowAppIdea.aspx?fid=" + FID + "',500,400);\"><font color='red'>已打回</font></a>";
                box.Enabled = false;
            }
            else if (fstate == 6)
            {
                fDesc = "<font color='green'>已审核</font>";
                box.Enabled = false;
            }
            e.Item.Cells[7].Text = fDesc;
            box.Attributes.Add("name", FID);
            box.Attributes["id"] = "span" + box.ClientID;
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnOut_Click(object sender, EventArgs e)
    {
        string sql = this.Pager1.sql.ToString();
        DataTable dt = rc.GetTable(sql);
        if (dt != null)
        {
            this.JustAppInfo_List.DataSource = dt;
            this.JustAppInfo_List.DataBind();
            JustAppInfo_List.Columns[0].Visible = false;
            string fOutTitle = lPostion.Text;
            sab.SaveAsExc(JustAppInfo_List, fOutTitle, Response, "gb2312", 0);
        }
    }
    public void SaveAsExc(DataGrid DG_List, string Title, System.Web.HttpResponse Response, string tablehtml)
    {
        string sFileName = Title;
        string sHeadTittle = Title;
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "utf-8";  //设置了类型为中文防止乱码的出现  
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(sFileName, System.Text.Encoding.UTF8) + ".xls"); //定义输出文件和文件名 
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文 
        Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。  
        System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        oHtmlTextWriter.Write("<div align=center height=25 valign=middle><font size='6'>" + sHeadTittle + "</font></div><br>");
        oHtmlTextWriter.Write(tablehtml);
        DG_List.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        oHtmlTextWriter.Close();
        oStringWriter.Close();
        Response.End();
    }

    private void BatchApp(string pager)
    {
        int iCount = this.JustAppInfo_List.Items.Count;
        ArrayList array = new ArrayList();
        ArrayList array1 = new ArrayList();
        int c = 0;
        for (int i = 0; i < iCount; i++)
        {
            string pId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Items[i].Cells.Count - 1].Text;
            string fisQuali = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Items[i].Cells.Count - 8].Text;
            CheckBox cb = (CheckBox)JustAppInfo_List.Items[i].Cells[0].Controls[1];
            if (cb.Checked == true)
            {
                if (fisQuali != "1")
                {
                    string fManageTypeId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Items[i].Cells.Count - 4].Text;
                    array1.Add(fManageTypeId);
                    array.Add(pId);
                }
                else
                {
                    JustAppInfo_List.Items[i].BackColor = System.Drawing.Color.FromArgb(252, 222, 164);
                    c++;
                }
            }
        }
        if (array.Count == 0)
        {
            string s = "<script>alert('请选择要批量审批的项')</script>";
            if (c > 0)
            {
                s = "<script>alert('您选择的项中要先发放证书')</script>";
            }
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), s);
            return;
        }

        for (int j = 0; j < array.Count - 1; j++)
        {
            if (array1[j].ToString().Trim() != array1[j + 1].ToString().Trim())
            {
                pageTool tool = new pageTool(this.Page);
                tool.showMessage("请选择相同业务类型的数据");
                return;
            }
        }
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < array.Count; i++)
        {
            if (i == 0)
            {
                sb.Append(array[i].ToString());
            }
            else
            {
                sb.Append("," + array[i].ToString());
            }
        }
        if (sb.Length > 0)
        {

            // ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "click", "app('" + pager + "')", true);
            StringBuilder sScript = new StringBuilder();
            sScript.Append("var obj = new Object();");
            sScript.Append("var tmpVal='';");
            sScript.Append("obj.name='51js';");
            sScript.Append(" tmpVal ='" + sb.ToString() + "';");
            sScript.Append(" obj.id= tmpVal;");
            sScript.Append(" ShowWindow('" + pager + "',700,600,obj);");

            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), sScript.ToString());
        }
    }


    protected void btnBatchApp_Click(object sender, EventArgs e)
    {
        BatchApp("../AppQualiInfo/BackUpBatchApp.aspx?e=0&k=3");
    }

    protected void btnBatchBack_Click(object sender, EventArgs e)
    {
        BatchApp("../AppQualiInfo/BackEntBatchApp1.aspx?e=0");
    }
}
