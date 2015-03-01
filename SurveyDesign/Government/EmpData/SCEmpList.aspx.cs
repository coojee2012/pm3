using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Text;
using Approve.RuleCenter;
using System.Data;

public partial class Government_EmpData_SCEmpList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    string interfaceUserName = "";//接口用户名
    string interfacePassword = "";//接口密码
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {


            showBound();
            showEmp();

        }
    }
    private string IsView { get; set; }
    private string FBaseInfoId { get; set; }
    private string FUserId { get; set; }

    void showBound()
    {

        var dic = db.Dic.Where(t => t.FParentId == 123).OrderBy(t => t.FOrder).Select(t => new { t.FName, t.FNumber }).ToList();
        t_FPersonTypeId.DataSource = dic;
        t_FPersonTypeId.DataTextField = "FName";
        t_FPersonTypeId.DataValueField = "FNumber";
        t_FPersonTypeId.DataBind();
        t_FPersonTypeId.Items.Insert(0, new ListItem("--请选择--", ""));
        if (Request.QueryString["fType"] != null && Request.QueryString["fType"] != "")
        {
            this.t_FPersonTypeId.SelectedValue = Request.QueryString["fType"];
            this.t_FPersonTypeId.Enabled = false;
        }
    }



    #region 注册人员

    //显示注册人员
    private void showEmp()
    {
        interfaceUserName = rc.GetSysObjectContent("_InterfaceUserName");
        interfacePassword = rc.GetSysObjectContent("_InterfacePassword");
        string rn = "";
        cn.gov.scjst.zw.JSTJKWebService jst = new cn.gov.scjst.zw.JSTJKWebService();
        DataTable dt = dt = jst.GetTABLE(interfaceUserName, interfacePassword, "", 0, "勘察设计人员（网站）", out rn);

        if (dt != null && dt.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();


            if (this.t_FName.Text != null && this.t_FName.Text != "")
            {
                sb.Append(" and personName like '%" + this.t_FName.Text + "%'");
            }
            if (this.t_FIdCard.Text != null && this.t_FIdCard.Text != "")
            {
                sb.Append(" and identityNumber='" + this.t_FIdCard.Text + "'");
            }
            if (this.t_FPersonTypeId.SelectedValue != "")
            {
                sb.Append(" and  Level='" + this.t_FPersonTypeId.SelectedItem.Text + "' ");
            }
            if (this.t_FEnt.Text != null && this.t_FEnt.Text != "")
            {

                sb.Append(" and  enterName like '%" + this.t_FEnt.Text + "%' ");
            }

            DataRow[] rows = dt.Select(" isZC=1 " + sb.ToString());


            var v = from r in rows
                    select new
                    {
                        personName = r["personName"],
                        enterName = r["enterName"],
                        identityNumber = r["identityNumber"],
                        registCertificate = r["registCertificate"],
                        fzTimeEnd = r["fzTimeEnd"],
                        recordId = r["recordId"]
                    };

            Pager1.RecordCount = v.Count();
            Emp_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            Emp_List.DataBind();
            Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页不显示


        }
    }
    //注册人员列表
    protected void Emp_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();





        }
    }

    //删除按钮 
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(Emp_List, db.CF_Emp_BaseInfo);
        showEmp();
    }
    //刷新
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showEmp();
    }

    #endregion

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showEmp();
    }





    protected void btnReload3_Click(object sender, EventArgs e)
    {

        showEmp();

    }

}
