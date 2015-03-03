using Approve.Common;
using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyXZYJS_ChooseIDeaList : System.Web.UI.Page
{
    private Share sh = new Share(); 
    private ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //List<Student> list = new List<Student>() { 
            //    new Student(){Name="yujiajun",Path="http://www.google.com"},
            //    new Student(){Name="xueci",Path="http://www.baidu.com"}
            //};
            //rptData.DataSource = list;
            //rptData.DataBind();
            //ControlBind();
            //Show();
        }
    }
    private void Show()
    {
        //System.Text.StringBuilder _builder = new System.Text.StringBuilder();
        //_builder.Append("select * from Test where 1=1");
        //if (!string.IsNullOrEmpty(t_FEntName.Text))
        //    _builder.Append(" and Name like '%" + t_FEntName.Text + "%'");
        //if (!string.IsNullOrEmpty(t_path.Text))
        //    _builder.Append(" and address like '%" + t_path.Text + "%'");
        //_builder.Append(" Order By FId desc");
        //string sql = "";
        //Pager1.sql = _builder.ToString();
        //Pager1.className = "dbCenter";
        //Pager1.pagecount = 15;
        //Pager1.controltopage = "DG_List";
        //Pager1.controltype = "DataGrid";
        //Pager1.dataBind();

        StringBuilder _builder = new StringBuilder();
        _builder.Append("select * from cf_sys_user where FSystemId=100 and  isnull(FIsDeleted,0)=0 And isnull(FType,0)=2");
        //_builder.Append(GetCon());
        _builder.Append(" Order By Fcreatetime Desc,FID ");
        this.Pager1.className = "dbShare";
        this.Pager1.sql = _builder.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Show();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        sl.Add("Test", "FID");
        tool.DelInfoFromGrid(this.DG_List, sl, "RCenter"); 
        Show();
    }
    protected void btnQuery1_Click(object sender, EventArgs e)
    {
        Show();
    }
}