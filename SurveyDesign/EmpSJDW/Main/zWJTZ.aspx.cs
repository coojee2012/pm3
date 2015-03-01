using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
public partial class EntSelf_MainSelf_zWJTZ : System.Web.UI.Page
{
    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            bindData();
        }
    }

    /// <summary>
    /// 数据绑定
    /// </summary>
    private void bindData()
    {
        ProjectDB db = new ProjectDB();
        string entType = db.CF_Sys_UserRight.Where(d => d.FId == CurrentEntUser.EntUserId).Select(d => d.FSystemId.GetValueOrDefault()).FirstOrDefault().ToString();
        IQueryable<CF_News_Title> newstitle = db.CF_News_Title.Where(t => t.FIsDeleted == false && t.FType == 600 && ("," + t.FWebId + ",").Contains("," + entType + ","));
        newstitle = newstitle.Where(t => t.FIsPub == true && t.FValidEnd.GetValueOrDefault().Date >= DateTime.Now.Date && t.FPubTime.GetValueOrDefault().Date <= DateTime.Now.Date);
        var result = from t1 in newstitle
                     orderby t1.FPubTime descending
                     select new
                     {
                         t1.FID,
                         t1.FName,
                         t1.FOperator,
                         t1.FDepartment,
                         t1.FManageDeptId,
                         t1.FValidEnd,
                         t1.FPubTime,
                         t1.FCount,
                         t1.FCreateTime
                     };
        gv_List.DataSource = result;
        gv_List.DataBind();
    }

    /// <summary>
    /// 绑定到行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbautoid = (Label)e.Row.FindControl("lbautoid");
            lbautoid.Text = (e.Row.RowIndex + 1).ToString();


            Label lbpubinfo = (Label)e.Row.FindControl("lbpubinfo");
            lbpubinfo.Text = EConvert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "FPubTime")).ToShortDateString() + " - " + EConvert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "FValidEnd")).ToShortDateString();
        }
    }

    #region 页面处理函数
    public string getDepartment(object obj, object part)
    {
        string str = string.Empty;
        if (obj != null)
        {
            str = RBase.GetDeptName(EConvert.ToString(obj));
        }
        if (part != null)
        {
            if (str != "")
            {
                str += " - ";
            }
            str += RBase.GetDepartmentName(EConvert.ToString(part));
        }
        return str;
    }

    #endregion
}
